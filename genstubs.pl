#!/usr/bin/perl -w

use strict;
use File::Basename;

$| = 0;
makeDirs();
parseDir("/System/Library/Frameworks/AppKit.framework/Headers", "appkit");
parseDir("/System/Library/Frameworks/Foundation.framework/Headers", "foundation");

sub parseMethod {
    my $origmethod = shift();
    my $class = shift();
    my @return = ();

    chomp($origmethod);

#    print("$origmethod\n");

    # Check for unsupported methods and return commented function
    # Unsupported methods include:
    # <.*>
    if($origmethod =~ /<.*>/ or
       # varargs don't work.
       # Need another method of passing variable number of args (...)
       # until then, comment such methods as UNSUPPORTED
       $origmethod =~ /\.\.\./
      ) {
        $origmethod =~ s:/::g;
        return "/* UNSUPPORTED: \n$origmethod\n */\n\n";
    }

    # It seems that methods take one of two formats.  Zero arguments:
    # - (RETURNTYPE)MethodName;
    # or N arguments
    # - (RETURNTYPE)MethodName:(TYPE0)Arg0 ... ArgNName:(TYPEN)ArgN;

    $origmethod =~ /([+-])\s+\(([^\)]+)\)(.+)/;

    my $methodType = $1;
    my $retType = $2;
    my $remainder = $3;
    my $isClassMethod = (defined($methodType) ? ($methodType eq "+") : 0);

    # Check for weird interface methods with no return
    if(!$retType) {
        $origmethod =~ s:/::g;
        return "/* UNSUPPORTED - no return type: \n$origmethod\n */\n\n";
    }

    # get rid of comments
    $remainder =~ s://.*::;
    $remainder =~ s:/\*.*\*/::;
    
    # These are our arrays that store our args, their names and types
    my(@methodName, @type, @name);

    my $message;
    my $params;

    my $noarg_rx = '^\s*(\w+)\s*([;\{]|$)';
    my $arg_rx   = '(\w+):\s*(?:\(([^\)]+)\))?\s*(\w+)?(?:\s+|;)';

    # The objc message we will be sending
    my @message;


    # If there are no arguments (only matches method name)
    if($remainder =~ /$noarg_rx/){
        push(@methodName, $1);

    # If there are arguments, parse them
    }elsif($remainder =~ /$arg_rx/){
        (my(@remainder)) = ($remainder =~ /$arg_rx/g);

        # Fill our arrays from the remainder of the parsed method
        while(@remainder){
            push( @methodName,  shift @remainder );

            my $argType = shift @remainder;
            my $argName = shift @remainder;

            $argType = "id" unless $argType;

            unless ($argName){
                $argName = $argType;
                $argType = "id";
            }
            
            push( @type,        $argType );
            push( @name,        $argName );
        }

    # If we can't parse the method, complain
    }else{
        print("Couldn't parse method:\n",
              "$origmethod\n",
             );
        return;
    }

    my @params;
    # Build the params and message
    if(int(@methodName) == 1 && int(@name) == 0){
        push(@message, $methodName[0]);

    }else{
        for(my $i = 0; $i < int @methodName; $i++){
            push(@params, "$type[$i] $name[$i]");
            push(@message, "$methodName[$i]:$name[$i]");
        }
    }

    # What object will we be sending messages to?
    my $obj;

    # If the method is a class method
    if($isClassMethod){
        $obj = $class;

        $class .= '$';

    # If the method is an instance method
    }else{
        unshift(@params, "$class* THIS");
        $obj = "THIS";

    }

    # The fully-qualified C function name separated by _s (:s don't work)
    my $methodName = join("_",  $class, @methodName);

    # What does the log entry look like?
    my $logLine = ($isClassMethod ?
                   "\tNSLog(\@\"$methodName\\n\");" :
                   "\tNSLog(\@\"$methodName: \%\@\\n\", THIS);"
                  );

    # The parameters to the C function
    $params     = join(", ", @params);

    # The objc message to send the object
    $message    = join(" ",  @message);

    # Will we be returning?
    my $retter = ($retType =~ /void/) ? "" : "return ";

    # return the method we will be using
    return ( "$retType $methodName ($params) {",
             $logLine,
             "\t${retter}[$obj $message];",
             "}"
            );             

}

# Parse file
sub parseFile {
    my $filename = shift();
    my $methods = 0;

    my @out = ("/* Generated by genstubs.pl",
               " * (c) 2004 kangaroo, C.J. and Urs",
               " * Generation date: " . localtime,
               " */",
               "",
               "",
              );

    (my($class, $path, $suffix)) = fileparse($filename, ".h");
    (my (undef, undef, undef, undef, $dirpart)) = split(/\//, $filename);
    $dirpart =~ s/\.framework//;
    my $interface = $class;
    push(@out, "#include <$dirpart/$class.h>");
    push(@out, "#include <Foundation/NSString.h>");

    open(FILE, "<$filename") or die "Couldn't open $filename: $!";

    while(my $line = <FILE>) {
        chomp $line;

        my $commentRegex = '(?:/\*.*\*/|//.*$)?';

        # Keep #include lines
        if($line =~ /#import/){
            $line =~ s/#import/#include/;
            push(@out, $line);
            next;
        }

        # Determine the interface we are in
        if($line =~ /\@interface (\w+)(.*)/){
            $line =~ /\@interface (\w+)(.*)/;
            $interface = $1;
        }

        # If this is a method definition, parse it
        if($line =~ /^\s*[+-]/){
            while($line !~ /;\s*$commentRegex$/x){
                chomp $line;
                $line .= <FILE>;
            }
            $methods++;
            push(@out, "", parseMethod($line, $interface));
        }
    }

    push(@out,
         "",
         "$class * ${class}_alloc(){",
         "\tNSLog(\@\"${class}_alloc()\\n\");",
         "\treturn [ $class alloc ];",
         "}"
        );

    print " $methods methods.\n";
    return @out;
}

sub parseDir {
    my $sourcedir = shift();
    my $destdir = shift();

    opendir(my $dh, $sourcedir);

    my($name, $path, $suffix);
    
    print "Processing $sourcedir:\n";
    foreach my $filename (readdir($dh)) {
        next if $filename =~ /^\./;
        next unless $filename =~ /\.h$/;

        ($name, $path, $suffix) = fileparse("$sourcedir/$filename", ".h");

        print "\t$filename";

        my @file = parseFile("$path/$filename");
        my $stubfile = "src/$destdir/${name}_stub.m";

        open OUT, ">$stubfile" or die "Can't open $stubfile: $!";
        print OUT join($/, @file);
        close OUT;
    }
    print "\n";
}

sub makeDirs {
    unless(-d "src"){
        mkdir "src" or die "Couldn't make dir 'src': $!";
    }
    unless(-d "src/appkit"){
        mkdir "src/appkit" or die "Couldn't make dir 'src/appkit': $!";
    }
    unless(-d "src/foundation"){
        mkdir "src/foundation" or die "Couldn't make dir 'src/foundation': $!";
    }
}
