//!/usr/bin/perl -w
//
//  genstubs.pl
//
//  Authors
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Kangaroo, Geoff Norton
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Main.cs,v 1.1 2004/06/19 20:17:40 urs Exp $
//

using System;
using System.Collections;
using System.IO;

namespace ObjCManagedExport
{
	/// <summary>
	/// Summary description for ObjcMangedExporter.
	/// </summary>
	class ObjCManagedExporter
	{
		IDictionary protocols = new Hashtable();
		IDictionary imported = new Hashtable();
		IDictionary parsedFiles = new Hashtable();

		// TODO: don't hardcode those paths, this should work for any objc file, 
		// for instance a client project header file
		const string foundationPath = "/System/Library/Frameworks/Foundation.framework/Headers";
		const string appKitPath     = "/System/Library/Frameworks/AppKit.framework/Headers";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			ObjCManagedExporter main = new ObjCManagedExporter();
			main.makeDirs();
			// output interfaces
			main.parseDir(foundationPath, "foundation");
			main.parseDir(appKitPath, "appkit");
		}

		public void makeDirs() 
		{
			//temporary while not clobbering old .cs 
			if (!Directory.Exists("tmp/src/Apple.Foundation"))
				Directory.CreateDirectory("tmp/src/Apple.Foundation");
			if (!Directory.Exists("tmp/src/Apple.AppKit"))
				Directory.CreateDirectory("tmp/src/Apple.AppKit");

			if (!Directory.Exists("src/appkit"))
				Directory.CreateDirectory("src/appkit");
			if (!Directory.Exists("src/foundation"))
				Directory.CreateDirectory("src/foundation");
		}

		public void parseDir(string sourcedir,string outputDir)
		{
			Console.WriteLine("Processing directory: {0}:",sourcedir);

			foreach(string filename in Directory.GetFiles(sourcedir,"*.h")) 
				parseFile(Path.Combine(sourcedir,filename));
		}

		IDictionary currentImports = new Hashtable();
		class Common {
			public string Interface;
			public string Super;
			public bool isInterface;
			public bool isProtocol;
		}
		/// <summary>
		/// function parseFile
		/// </summary>
		/// <param name="filename">The name of the file we will be parsing</param>
		public void parseFile(string filename) 
		{
			// Classes that have been imported in this traversal

			if (currentImports.Contains(filename))
				return;

			// Note that we have imported this file so that we don't do it again
			currentImports[filename] = 1;

			// Set to undef when started, 1 when finished
			parsedFiles[filename] = null;

		    IDictionary methods = new Hashtable();
			string genDate = DateTime.Now.ToString();

		    IList protocolOut = new ArrayList();

			string name = Path.GetFileNameWithoutExtension(filename);
			string path = Path.GetDirectoryName(filename);
			string suffix = Path.GetExtension(filename);

			//??? $filename =~ m:.*/([^\.]*)\.[^/]+/:;
			string dirpart = Directory.GetParent(path);

			bool skip = false;
			bool isProtocol = false;
			bool isInterface = false;
			string protocol;

			IList imported = new ArrayList();
			IList objC;

			Common common = new Common();
			common.Interface = name;

			System.IO.StreamReader fh = File.OpenText(filename);

			string line = fh.ReadLine();
			while(line != string.Empty) {
				//?? chomp $line;

				//??commentsBeGone(\$line, $fh);

				IDictionary objC;
				IDictionary cSharp;

				// Traverse import lines
				if(line.IndexOf("#import") >= 0/*line =~ m:#import\s+[<"]([^>"]+)[>"]*/){
					string importString = string.Empty;//$1;
					string importDir = new DirectoryInfo(Path.GetDirectoryName(importString)).Name + "/";
					string importName = Path.GetFileNameWithoutExtension(importString);

					string fqImportDir = string.Empty;
					string fqImportFile = string.Empty;

					// Are we importing from the Appkit or the Foundation dirs?
					if(importDir == "AppKit/"){
						fqImportDir = appKitPath;
					}else if(importDir == "Foundation/"){
						fqImportDir = foundationPath;
					}

					fqImportFile = fqImportDir + "/" + importName + ".h";

					// If the import dir is either AppKit or Foundation
					// And we haven't already imported this file, do so now
					if (fqImportDir == string.Empty){
						// Not an appkit or foundation include file.

					}else if( 
						!exists(currentImports.Contains(fqImportFile))){

						// Verify that this file exists
						Console.Write(" ----------------------- \n",
							" This SHOULD NOT HAPPEN! \n",
							" ----------------------- \n",
							" '$fqImportFile' does not exist \n",
							" But import string is '$importString' \n",
							) unless -f "$fqImportFile";

						// Cache the results of the parse
						if(!imported.Contains(fqImportFile)){

							// This would cause an infinite loop.
							if(parsedFiles.Contains(fqImportFile) &&
								parsedFiles[fqImportFile] == null) {
								die("Infinite loop detected");
							}

							// Note that we have already imported this file
							currentImports[fqImportFile] = 1;
		                    
							imported[fqImportFile] =
								parseFile(fqImportFile, currentImports);

						}
					}
				// Determine the interface we are in
				}else if(false/* $line =~ /^\s*\@interface\s+(\w+)(.*)/ */){
					common.isInterface = 1;
					common.Interface = "";//??$1);
		            
					string remainder = "";//??$2;

					//??$remainder =~ /\s*(?::\s*(\w+)\s*?)?(?:<([^>]+)>)?/;

					// Capture superclass and protocols
					//??@common{'super', 'protocols'} = ($1, $2);

					// If the interface has a superclass
					if(exists $common{super} && defined $common{super}){
						// TODO: Do something in this case.
					}

					// If the interface follows a particular protocol
					if(exists $common{protocols} && defined $common{protocols}){
						IList protocols = split(/,\s*/, $common{protocols});

						Console.Write(" $common{interface} implements: $common{protocols}" );

						// Place the protocol definitions directly into the interface
						foreach string p (@protocols){
							if(! exists $protocols{$p} ) {
								Console.Write(" WARNING: Protocol $p is missing\n");
								next;
							}
							Console.Write(" lines read from protocol $p: ",
								int @{ $protocols{$p} }, "\n");

							foreach string protoLine (@{ $protocols{$p} }){
								// DONE (since already filtered when collected): only parseMethod on /^\s*[+-]/ lines
								objC.Add(
									{ parseMethod($protoLine, $common{interface}, \%methods),
									%common 
									});
							}
						}
					}

				// Are we processing a @protocol line?
				}else if($line =~ /\@protocol\s+(\w+)/){
					string remainder = $1;

					$remainder =~ /(\w+)\s*(?:<([^>]+)>)?/;
					$protocol = $1;

					// TODO: Do something with extended class information
					string extendedClasses = $2;
					IList extendedClasses;

					if($extendedClasses){
						@extendedClasses = split(/,\s*/, $extendedClasses);
					}

					$isProtocol = 1;

				}else if($line =~ /\@end/ ){

					@common{'class', 'super'} = (undef, undef);
		            
					if($isProtocol == 1){
						$protocols{$protocol} = [ @protocolOut ];
						$isProtocol = 0;
					}else if($isInterface == 1){
						$isInterface = 0;
					}

				// If this is a class or instance method definition
				}else if($line =~ /^\s*[+-]/){
					// For lines that end in a definition,
					// Replace { ... } with a semicolon
					$line =~ s/\{[^\}]*\}\s*/;/;

					// If the line doesn't end with a semicolon, whitespace, end of line
					// Do the following until it does
					while($line !~ /;\s*$/ ){

						$line =~ s://.*::;
						// Append the next line
						$line .= <$fh>;
						// Remove trailing newline
						chomp $line;
						// Get rid of comments
						commentsBeGone(\$line, $fh);
						// Replace { ... } with a semicolon
						$line =~ s/\{[^\}]*\}/;/;
					}

					if($isProtocol){
						protocolOut.Add( $line);

					}else{
						objC.Add(
							{ parseMethod($line, $common{interface}, \%methods),
							%common 
							});
					}
				}
			}

			$filename =~ m:.*/([^\.]*)\.[^/]+/:;
			string destdir = lc $1;
			IList uniq;
		    
			{
				IList objCOut = new ArrayList(new string[]{"/* Generated by genstubs.pl",
						" * (c) 2004 kangaroo, C.J. and Urs",
						" * Generation date: $genDate",
						" */",
						"",
						"",
						"#import <$dirpart/$name.h>",
						"#import <Foundation/NSString.h>",
						"",
						});
		    
				// Generate the objC/C wrapper
				foreach (string objC in objC){
					if(exists $objC->{unsupported}){
						objCOut.Add("/* UNSUPPORTED: \n$objC->{unsupported}\n */\n\n");
						next;
					}
		    
					objCOut.Add( genObjCStub(\%methods, %$objC));
					uniq.Add( $objC);
				}
		    
				string stubfile = "src/$destdir/${name}_stub.m";
		        
				open OUT, ">$stubfile" or die "Can't open $stubfile: $!";
				Console.Write OUT join($/, @objCOut);
				close OUT;
			}

			{
				// TODO: don't hardcode mappings: things from /System/Library/Frameworks will be prefixed with Apple
				string namespace;

				if($destdir eq "appkit"){
					$namespace = "Apple.Appkit";
				}else if($destdir eq "foundation"){
					$namespace = "Apple.Foundation";
				}else{
					Console.Write("This shouldn't happen.  \$destdir = '$destdir'\n");
				}

				IList csOut = ("/* Generated by genstubs.pl",
						" * (c) 2004 kangaroo, C.J. and Urs",
						" * Generation date: $genDate",
						" */",
						"",
						"using System;",
						"using System.Collections;",
						"using System.Runtime.InteropServices;",
						"",
						"namespace $namespace {",
						"    using Apple.Foundation;",
						"",
						"    public class $name : NSObject {",
						"        protected internal static IntPtr ${name}_class = Class.Get(\"$name\");",
						"",
						);

				// Generate the C# wrapper
				csOut.Add("        #region -- Generated Stubs --", "");
				foreach string objC (@uniq){
					csOut.Add( genCSharpStub(%$objC));
				}
				csOut.Add("        #endregion", "");

				csOut.Add("        #region -- Instance Methods --", "");
				foreach string objC (@uniq){
					csOut.Add( genCSharpInstanceMethod(%$objC));
				}
				csOut.Add("        #endregion", "");

				csOut.Add( 
					"",
					"    }",
					"}");

				string wrapperFile = "src/$namespace/$name.cs";

				//if (!(-r "$wrapperFile")) {
				//    Console.Write "\n$wrapperFile does not exist: creating\n";
					open OUT, ">tmp/$wrapperFile" or die "Can't open tmp/$wrapperFile: $!";
					Console.Write OUT join($/, @csOut);
					close OUT;
				//}
			}

			string numMethods = int(keys %methods);
			Console.Write " $numMethods methods in $name.\n";

			$parsedFiles{$filename} = 1;

			return keys %{ $currentImports };

		}

	}
}

// Some ideas for ParseMethod:
// - Only parse the method!  Store method parts in the %objC hash
sub parseMethod {
    string origmethod = shift();
    string class      = shift();
    string methodHash = shift();
    IList return     = ();

    chomp($origmethod);

    // Check for unsupported methods and return commented function
    // Unsupported methods include:
    // <.*>
    if($origmethod =~ /<.*>/ or
       // varargs don't work.
       // Need another method of passing variable number of args (...)
       // until then, comment such methods as UNSUPPORTED
       $origmethod =~ /\.\.\./
      ) {
        return ("unsupported" => $origmethod);
    }

    // It seems that methods take one of two formats.  Zero arguments:
    // - (RETURNTYPE)MethodName;
    // or N arguments
    // - (RETURNTYPE)MethodName:(TYPE0)Arg0 ... ArgNName:(TYPEN)ArgN;

    unless($origmethod =~ /\s*([+-])\s*(?:\(([^\)]+)\))?(.+)/ ){
        Console.Write("Couldn't parse method: $origmethod\n");

        return ("unsupported" => $origmethod);
    }

    string methodType = $1;
    string retType = ($2 ? $2 : "id");
    string remainder = $3;

    string isClassMethod =
        (defined($methodType) ? ($methodType eq "+") : 0);

    $retType =~ s/oneway //;

    // get rid of comments
    $remainder =~ s://.*::;
    $remainder =~ s:/\*.*\*/::;
    
    // These arrays store our method names, their arg names and types
    my(@methodName, @name, @type);

    string noarg_rx = '^\s*(\w+)\s*([;\{]|$)';
    string arg_rx   = '(\w+):\s*(?:\(([^\)]+)\))?\s*(\w+)?(?:\s+|;)';

    // If there are no arguments (only matches method name)
    if($remainder =~ /$noarg_rx/){
        methodName.Add( $1);

    // If there are arguments, parse them
    }else if($remainder =~ /$arg_rx/){
        (my(@remainder)) = ($remainder =~ /$arg_rx/g);

        // Fill our arrays from the remainder of the parsed method
        while(@remainder){
            methodName.Add(  shift @remainder );

            string argType = shift @remainder;
            string argName = shift @remainder;

            if (argType == string.Empty)
				argType = "id";

            if (argName == string.Empty) {
                argName = argType;
                argType = "id";
            }
            
            type.Add(argType);
            name.Add(argName);
        }

    // If we can't parse the method, complain
    }else{
        Console.Write("Couldn't parse method: $origmethod\n");
        return("unsupported" => $origmethod);
    }

    // Who receives this message?
    // What object will we be sending messages to?
    my($receiver, $logLine);

    // Build the params and message
    my(@message, @params);

    if(int(@methodName) == 1 && int(@name) == 0){
        message.Add( $methodName[0]);

    }else{
        for(string i = 0; $i < int @methodName; $i++){
            params.Add("$type[$i] p$i");
            message.Add("$methodName[$i]:p$i");
        }
    }

    // The objc message to send the object
    string message = join(" ",  @message);

    // If the method is a class method
    if($isClassMethod){
        unshift(@params, "Class CLASS");
        $receiver = "CLASS";
        $logLine =
            "\tif (!CLASS) CLASS = [$class class];\n";
        $class .= '_';

    // If the method is an instance method
    }else{
        unshift(@params, "$class* THIS");
        $receiver = "THIS";
        $logLine = "";

    }

    // The fully-qualified C function name separated by _s (:s don't work)
    string methodName = join("_",  $class, @methodName);

    // Add the log call
    $logLine .= "\tNSLog(\@\"$methodName: \%\@\\n\", $receiver);";

    // The parameters to the C function
    string params     = join(", ", @params);

    if(exists $methodHash->{$methodName}){
        Console.Write("\t\tDuplicate method name: $methodName\n");
        return ("dup", $origmethod);
    }
    
    $methodHash->{$methodName} = "1";

    return ("method name"        => $methodName,
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

sub commentsBeGone()
{
    string line = shift();
    string FH = shift();

    // Rid ourselves of multi-line comments
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
    string metods = shift();
    IDictionary objC = @_;

    if(exists $objC{dup}){
        // Duplicate.  Don't return anything
        return ();
    }

    // Will we be returning?
    string retter = ($objC{"return type"} =~ /void/) ? "" : "return ";

    // Return the lines of the wrapper
    return ("$objC{'return type'} $objC{'method name'} ($objC{params}) {",
             $objC{"log line"},
             "\t${retter}[$objC{receiver} $objC{message}];",
             "}",
             "",
           );
}

sub genCSharpStub {
    IDictionary objC = @_;

    // BUG: Why are we getting empty method names in here?
    return unless defined($objC{'method name'});

    if(  ( ($objC{'return type'} !~ /(\w+)\s+\*/) && (!defined($typeMap{$objC{'return type'}})))) {
        Console.Write "WARNING: Not processing " . $objC{'method name'} . " because I dont know how to map: " . $objC{'return type'} . "\n";
        return;
    }
    string type = (($objC{'return type'} =~ /(\w+)\s*\*/) ? "IntPtr /*$1*/" : $typeMap{$objC{'return type'}});
    IList params = ();
    IList names = defined $objC{'arg names'} ? @{ $objC{'arg names'} } : ();
    IList types = defined $objC{'arg types'} ? @{ $objC{'arg types'} } : ();

    if ( $objC{'is class method'}) {
        params.Add("IntPtr CLASS");
    } else {
        params.Add("IntPtr THIS");
    }

    for(string i = 0; $i < int @types; $i++){
        if(  ( ($types[$i] !~ /(\w+)\s+\*/) && (!defined($typeMap{$types[$i]})))) {
            Console.Write "WARNING: Not processing " . $objC{'method name'} . " because I dont know how to map: " . $types[$i] . "\n";
            return;
        }
        string t = (($types[$i] =~ /(\w+)\s*\*/) ? "IntPtr /*$1*/" : $typeMap{$types[$i]});
        params.Add("$t p$i/*$names[$i]*/");
    }

    string params = join(", ", @params);

    // [DllImport("AppKitGlue")]
    // protected internal static extern void NSButton_setTitle(IntPtr THIS, IntPtr aString);
    return (
        "        [DllImport(\"AppKitGlue\")]",
        "        protected internal static extern $type $objC{'method name'} ($params);"
    );
}


sub genCSharpInstanceMethod {
    IDictionary objC = @_;

    //BUG: Why are we getting undefined method names in here
    return unless defined($objC{'method name'});

    if(  ( ($objC{'return type'} !~ /(\w+)\s+\*/) && (!defined($typeMap{$objC{'return type'}})))) {
        Console.Write "WARNING: Not processing " . $objC{'method name'} . " because I dont know how to map: " . $objC{'return type'} . "\n";
        return;
    }
    string type = (($objC{'return type'} =~ /(\w+)\s*\*/) ? "IntPtr /*$1*/" : $typeMap{$objC{'return type'}});
    string retter = ($type =~ /void/) ? "" : "return ";
    IList args = ();
    IList params = ();
    IList names = defined $objC{'arg names'} ? @{ $objC{'arg names'} } : ();
    IList types = defined $objC{'arg types'} ? @{ $objC{'arg types'} } : ();
    IList messageParts = @{ $objC{'message parts'} };
    string methodName = substr($objC{'method name'}, index($objC{'method name'}, "_")+1);
    $methodName = $1 if ($methodName =~ /^_(.+)/);
    $methodName = (defined($typeMap{$methodName}) ? $typeMap{$methodName} : $methodName);

    if ($objC{'is class method'}) {
        $type = "static $type";
        params.Add("IntPtr.Zero");
    } else {
        params.Add("Raw");
    }

    for(string i = 0; $i < int @types; $i++){
        if(  ( ($types[$i] !~ /(\w+)\s+\*/) && (!defined($typeMap{$types[$i]})))) {
            Console.Write "WARNING: Not processing " . $objC{'method name'} . " because I dont know how to map: " . $types[$i] . "\n";
            return;
        }
        string t = (($types[$i] =~ /(\w+)\s*\*/) ? "IntPtr /*$1*/" : $typeMap{$types[$i]});
        args.Add("$t p$i/*$names[$i]*/");
        params.Add("p$i/*$names[$i]*/");
    }

    string args = join(", ", @args);
    string params = join(", ", @params);

    // void setTitle(string aString);
    return (
        "        public $type $methodName ($args) {",
        "            $retter$objC{'method name'} ($params);",
        "        }"
    );
}

//sub convertTypeGlue {
//    string type = shift();
//    
//    return "IntPtr /*(??)*/" unless defined $type;
//    
//    if ($type eq "BOOL") {
//        return "bool";
//    } else if ($type eq "long long") {
//        return "Int64";
//    } else if ($type eq "unsigned long long") {
//        return "UInt64";
//    } else if ($type eq "unsigned") {
//        return "uint";
//    } else if($type eq "id" || $type eq "Class" || $type eq "SEL" || $type eq "IMP" || $type =~ /.*\*$/) {
//        return "IntPtr /*($type)*/";
//    }
//    if($type =~ /^unsigned (\w+)$/) {
//        $type = "u" . $1;
//    } 
//    
//    return $type;
//}

//sub convertType {
//    string type = shift();
//    
//    return "object /*(??)*/" unless defined $type;
//    
//    if ($type eq "BOOL") {
//        return "bool";
//    } else if ($type eq "unsigned") {
//        return "uint";
//    } else if($type eq "id") {
//        return "object /*($type)*/";
//    } else if($type eq "Class") {
//        return "Class";
//    } else if($type eq "SEL") {
//        return "string /*SEL*/";
//    } else if($type eq "IMP") {
//        return "IntPtr /*IMP*/";
//    } else if($type =~ /NSString.*\*$/) {
//        return "string /*($type)*/";
//    } else if($type =~ /(\w+).*\*$/) {
//        return "$1 /*($type)*/";
//    }
//    
//    return $type;
//}

sub getCSharpHash {
    IDictionary objC = @_;

    IDictionary cSharp =
	    ("arg names" => $objC{"arg names"},
	    );

    return (
        
    );
}

//	$Log: Main.cs,v $
//	Revision 1.1  2004/06/19 20:17:40  urs
//	*** empty log message ***
//
//	Revision 1.20  2004/06/19 02:34:32  urs
//	some cleanup
//	
//	Revision 1.19  2004/06/18 22:41:40  gnorton
//	Forgot one case for __ statics
//	
//	Revision 1.18  2004/06/18 22:36:18  gnorton
//	Better .cs handling; still broken but closer
//	
//	Revision 1.17  2004/06/18 17:52:52  urs
//	Some .cs file gen improv.
//	
//	Revision 1.16  2004/06/18 15:09:31  gnorton
//	* Resolve some warning in the.cs generation
//	* Temporarily make our tmp directories if needed
//	* Why are we getting undefined %objC{'method name'} into our generators?
//	
//	Revision 1.15  2004/06/18 13:54:57  urs
//	*** empty log message ***
//	
//	Revision 1.14  2004/06/17 06:01:15  cjcollier
//		* typeConversion.pl
//		- Created.  Enter type mapping from ObjC to C#
//	
//		* genstubs.pl
//		- Reading in typeConversion.pl and building a hash from it
//		- Cleaned up unsupported function handling slightyl
//		- Created a %cSharp hash that will eventually be populated by %objC.  This is what inspired typeConversion.pl
//	
//	Revision 1.13  2004/06/17 02:55:38  urs
//	Some cleanup and POC of glue change
//	
//	Revision 1.12  2004/06/16 12:20:26  urs
//	Add CVS headers comments, authors and Copyright info, feel free to add your name or change what is appropriate
//	
//	Revision 1.11  2004/06/15 19:02:09  urs
//	Add headers
//	
//
