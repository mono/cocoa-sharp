#!/usr/bin/perl -w

use strict;
use File::Basename;

$| = 1;
my %protocols = ();
my %imported = ();

makeDirs();

my $appKitPath     = "/System/Library/Frameworks/AppKit.framework/Headers";
my $foundationPath = "/System/Library/Frameworks/Foundation.framework/Headers";

# output interfaces
parseDir($appKitPath, "appkit");
parseDir($foundationPath, "foundation");


# Some ideas for ParseMethod:
# - Only parse the method!  Store method parts in the %objC hash
sub parseMethod {
    my $origmethod = shift();
    my $class      = shift();
    my $methodHash = shift();
    my @return     = ();

    chomp($origmethod);

    # Check for unsupported methods and return commented function
    # Unsupported methods include:
    # <.*>
    if($origmethod =~ /<.*>/ or
       # varargs don't work.
       # Need another method of passing variable number of args (...)
       # until then, comment such methods as UNSUPPORTED
       $origmethod =~ /\.\.\./
      ) {
	return ("unsupported" => $origmethod);
    }

    # It seems that methods take one of two formats.  Zero arguments:
    # - (RETURNTYPE)MethodName;
    # or N arguments
    # - (RETURNTYPE)MethodName:(TYPE0)Arg0 ... ArgNName:(TYPEN)ArgN;

    unless($origmethod =~ /\s*([+-])\s*(?:\(([^\)]+)\))?(.+)/ ){
	print("Couldn't parse method: $origmethod\n");

	return ("unsupported" => $origmethod);
    }

    my $methodType = $1;
    my $retType = ($2 ? $2 : "id");
    my $remainder = $3;

    my $isClassMethod =
	(defined($methodType) ? ($methodType eq "+") : 0);

    $retType =~ s/oneway //;

    # get rid of comments
    $remainder =~ s://.*::;
    $remainder =~ s:/\*.*\*/::;
    
    # These arrays store our method names, their arg names and types
    my(@methodName, @name, @type);

    my $noarg_rx = '^\s*(\w+)\s*([;\{]|$)';
    my $arg_rx   = '(\w+):\s*(?:\(([^\)]+)\))?\s*(\w+)?(?:\s+|;)';

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
	print("Couldn't parse method: $origmethod\n");
	return("unsupported" => $origmethod);
    }

    # Who receives this message?
    # What object will we be sending messages to?
    my($receiver, $logLine);

    # Build the params and message
    my(@message, @params);

    if(int(@methodName) == 1 && int(@name) == 0){
	push(@message, $methodName[0]);

    }else{
	for(my $i = 0; $i < int @methodName; $i++){
	    push(@params, "$type[$i] p$i");
	    push(@message, "$methodName[$i]:p$i");
	}
    }

    # The objc message to send the object
    my $message = join(" ",  @message);

    # If the method is a class method
    if($isClassMethod){
	unshift(@params, "Class CLASS");
	$receiver = "CLASS";
	$logLine =
	    "\tif (!CLASS) CLASS = NSClassFromString(\@\"$class\");\n";
        $class .= '_';

    # If the method is an instance method
    }else{
	unshift(@params, "$class* THIS");
	$receiver = "THIS";
	$logLine = "";

    }

    # The fully-qualified C function name separated by _s (:s don't work)
    my $methodName = join("_",  $class, @methodName);

    # Add the log call
    $logLine .= "\tNSLog(\@\"$methodName: \%\@\\n\", $receiver);";

    # The parameters to the C function
    my $params     = join(", ", @params);

    if(exists $methodHash->{$methodName}){
	print("\t\tDuplicate method name: $methodName\n");
	return ("dup", $origmethod);
    }
    
    $methodHash->{$methodName} = "1";

    return ( "method name"        => $methodName,
	     "method parts"       => [ @methodName ],
	     "arg names"          => [ @name ],
	     "arg types"          => [ @type ],
	     "message parts"      => [ @message ],
	     "message"            => $message,
	     "is class method"    => $isClassMethod,
	     "log line"           => $logLine,
	     "params"             => $params,
	     "method name"        => $methodName,
	     "receiver"           => $receiver,
	     "return type"        => $retType,
	     "param list"         => [ @params ],
	     "original method"    => $origmethod,

	   );

}

# Parse file
my %parsedFiles = ();
sub parseFile {
    # The name of the file we will be parsing
    my $filename = shift();

    # Classes that have been imported in this traversal
    my $currentImports = (defined $_[0] ? shift() : {});

    # Note that we have imported this file so that we don't do it again
    $currentImports->{$filename} = 1;

    if(exists $imported{$filename}){
	return @{ $imported{$filename} };
    }

    # Set to undef when started, 1 when finished
    $parsedFiles{$filename} = undef;

    my %methods = ();
    my $genDate = scalar localtime;

    my @out = ("/* Generated by genstubs.pl",
               " * (c) 2004 kangaroo, C.J. and Urs",
               " * Generation date: $genDate",
               " */",
               "",
               "",
              );
    my @protocolOut = ();

    (my($name, $path, $suffix)) = fileparse($filename, ".h");

    $filename =~ m:.*/([^\.]*)\.[^/]+/:;
    my $dirpart = $1;

    my $skip = 0;
    my $isProtocol = 0;
    my $isInterface = 0;
    my $protocol;

    my @imported = ();
    
    push(@out,
	 "#import <$dirpart/$name.h>",
	 "#import <Foundation/NSString.h>",
	 "",
	);

    my($class, $super, $protocols);

    my @objC;

    my %common = 
	( interface            => $name,
	  super                => undef,
	  isInterface          => undef,
	  isProtocol           => undef,
	);

    open(my $fh, "<$filename") or die "Couldn't open $filename: $!";

    while(my $line = <$fh>) {
	chomp $line;

	commentsBeGone(\$line, $fh);

	my %objC;

	# Traverse import lines
	if($line =~ m:#import\s+[<"]([^>"]+)[>"]:){
	    my $importString = $1;
	    (my($importName, $importDir, $importSuffix)) =
		fileparse($importString, ".h");

	    my($fqImportDir, $fqImportFile) = ("", "");

	    # Are we importing from the Appkit or the Foundation dirs?
	    if($importDir eq "AppKit/"){
		$fqImportDir = $appKitPath;
	    }elsif($importDir eq "Foundation/"){
		$fqImportDir = $foundationPath;
	    }

	    $fqImportFile = "$fqImportDir/$importName.h";

	    # If the import dir is either AppKit or Foundation
	    # And we haven't already imported this file, do so now
	    unless($fqImportDir){
		# Not an appkit or foundation include file.
	    }elsif($fqImportDir && 
		   !exists($currentImports->{$fqImportFile})){

		# Verify that this file exists
		print(" ----------------------- \n",
		      " This SHOULD NOT HAPPEN! \n",
		      " ----------------------- \n",
		      " '$fqImportFile' does not exist \n",
		      " But import string is '$importString' \n",
		     ) unless -f "$fqImportFile";

		# Cache the results of the parse
		if(!exists $imported{$fqImportFile}){

		    # This would cause an infinite loop.
		    if(exists $parsedFiles{$fqImportFile} &&
		       $parsedFiles{$fqImportFile} == undef){
			die "Infinite loop detected";
		    }

		    # Note that we have already imported this file
		    $currentImports->{$fqImportFile} = 1;

		    if($fqImportFile =~ /^NS.*\.h/ ){
			$imported{$fqImportFile} =
			    [ parseFile($fqImportFile, { %$currentImports }) ];
		    }else{
			$imported{$fqImportFile} = [];
		    }

		}
	    }
	# Determine the interface we are in
	}elsif($line =~ /^\s*\@interface\s+(\w+)(.*)/){
	    @common{'isInterface', 'interface'} = (1, $1);
	    
	    my $remainder = $2;

	    $remainder =~ /\s*:\s*(\w+)\s*(?:<([^>]+)>)?/;

	    # Capture superclass and protocols
	    @common{'super', 'protocols'} = ($1, $2);

	    # If the interface has a superclass
	    if(exists $common{super} && defined $common{super}){
		# TODO: Do something in this case.
	    }

	    # If the interface follows a particular protocol
	    if($protocols){
		my @protocols = split(/,\s*/, $protocols);

		print(" $common{interface} implements: ",
		      join(", ", @protocols), "\n" );

		# Place the protocol definitions directly into the interface
		foreach my $p (@protocols){
		    next unless exists $protocols{$p};

		    print(" lines read from protocol $p: ",
			  int @{ $protocols{$p} }, "\n");

		    foreach my $protoLine (@{ $protocols{$p} }){
			push(@out,
			     genObjCStub( \%methods,
					  parseMethod($protoLine,
						      $common{interface})
					)
			    );
		    }
		}
	    }

	# Are we processing a @protocol line?
	}elsif($line =~ /\@protocol\s+(\w+)/){
	    my $remainder = $1;

	    $remainder =~ /(\w+)\s*(?:<([^>]+)>)?/;
	    $protocol = $1;

	    if($protocol eq $common{interface}){
		$protocol .= '_';
	    }

	    # TODO: Do something with extended class information
	    my $extendedClasses = $2;
	    my @extendedClasses;

	    if($extendedClasses){
		@extendedClasses = split(/,\s*/, $extendedClasses);
	    }

	    $isProtocol = 1;

	}elsif($line =~ /\@end/ ){

	    @common{'class', 'super'} = (undef, undef);
	    
	    if($isProtocol == 1){
		$protocols{$protocol} = [ @protocolOut ];
		$isProtocol = 0;
	    }elsif($isInterface == 1){
		$isInterface = 0;
	    }

	# If this is a class or instance method definition
	}elsif($line =~ /^\s*[+-]/){
	    # For lines that end in a definition,
	    # Replace { ... } with a semicolon
	    $line =~ s/\{[^\}]*\}\s*/;/;

	    # If the line doesn't end with a semicolon, whitespace, end of line
	    # Do the following until it does
	    while($line !~ /;\s*$/ ){

		$line =~ s://.*::;
		# Append the next line
		$line .= <$fh>;
		# Remove trailing newline
		chomp $line;
		# Get rid of comments
		commentsBeGone(\$line, $fh);
		# Replace { ... } with a semicolon
		$line =~ s/\{[^\}]*\}/;/;
	    }

	    if($isProtocol){
		push(@protocolOut, $line);

	    }else{
		push(@objC,
		     { parseMethod($line, $common{interface}, \%methods),
		       %common 
		     });
	    }
	}
    }

    my @uniq;
    # Generate the objC/C wrapper
    foreach my $objC (@objC){
	if(exists $objC->{unsupported}){
	    push(@out, "/* UNSUPPORTED: \n$objC->{unsupported}\n */\n\n");

	}else{
	    push(@out, genObjCStub(\%methods, %$objC));
	    push(@uniq, $objC);

	}
    }

    $filename =~ m:.*/([^\.]*)\.[^/]+/:;
    my $destdir = lc $1;

    my $stubfile = "src/$destdir/${name}_stub.m";
    
    open OUT, ">$stubfile" or die "Can't open $stubfile: $!";
    print OUT join($/, @out);
    close OUT;

    my @csout;
    # Generate the C# wrapper
    foreach my $objC (@uniq){
	push(@csout, genCSharpStub(getCSharpHash(%$objC)));
    }

    my $wrapperFile;
    if($destdir eq "appkit"){
	$wrapperFile = "src/Apple.Appkit/$name.cs";
    }elsif($destdir eq "foundation"){
	$wrapperFile = "src/Apple.Foundation/$name.cs";
    }else{
	print("This shouldn't happen.  \$destdir = '$destdir'\n");
    }

#    open OUT, ">$wrapperFile" or die "Can't open $wrapperFile: $!";
#    print OUT join($/, @csout);
#    close OUT;

    my $numMethods = int(keys %methods);
    print " $numMethods methods in $name.\n";

    $parsedFiles{$filename} = 1;

    return keys %{ $currentImports };

}

sub parseDir {
    my $sourcedir = shift();

    opendir(my $dh, $sourcedir);

    my($name, $path, $suffix);
    print "Processing directory: $sourcedir:\n";

    foreach my $filename (readdir($dh)) {
	next if $filename =~ /^\./;
	next unless $filename =~ /^NS.*\.h$/;

	($name, $path, $suffix) = fileparse("$sourcedir/$filename", ".h");

        parseFile("$path/$filename");
    }

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

sub commentsBeGone()
{
    my $line = shift();
    my $FH = shift();

    # Rid ourselves of multi-line comments
    if( $$line =~ m:/\*: ){
	while( $$line !~ m:/\*.*\*/:){
	    $$line .= <$FH>;
	    chomp $$line;
	}

	$^W = 0;
	$$line =~ s{
                     /\*         ##  Start of /* ... */ comment
                     [^*]*\*+    ##  Non-* followed by 1-or-more *'s
                     (
                       [^/*][^*]*\*+
                     )*          ##  0-or-more things which don't start with /
                                 ##    but do end with '*'
                     /           ##  End of /* ... */ comment

                   |         ##     OR  various things which aren't comments:

                     (
                       "           ##  Start of " ... " string
                       (
                         \\.           ##  Escaped char
                       |               ##    OR
                         [^"\\]        ##  Non "\
                       )*
                       "           ##  End of " ... " string

                     |         ##     OR

                       '           ##  Start of ' ... ' string
                       (
                         \\.           ##  Escaped char
                       |               ##    OR
                         [^'\\]        ##  Non '\
                       )*
                       '           ##  End of ' ... ' string

                     |         ##     OR

                       .           ##  Anything other char
                       [^/"'\\]*   ##  Chars which doesn't start a comment, string or escape
                     )
                   }{$2}gxs;
	$^W = 1;

	$$line =~ s://.*::;
    }
}

sub genObjCStub {
    my $metods = shift();
    my %objC = @_;

    if(exists $objC{dup}){
	# Duplicate.  Don't return anything
	return ();
    }

    # Will we be returning?
    my $retter = ($objC{"return type"} =~ /void/) ? "" : "return ";

    # Return the lines of the wrapper
    return ( "$objC{'return type'} $objC{'method name'} ($objC{params}) {",
             $objC{"log line"},
             "\t${retter}[$objC{receiver} $objC{message}];",
             "}",
	     "",
	   );
}

sub getCSharpHash {
    my %objC = @_;

    return (

    );
}

sub genCSharpStub {
    my %cSharp = @_;

    return (

    );
}
