//
//  Method.cs
//
//  Authors
//    - Kangaroo, Geoff Norton
//    - C.J. Collier, Collier Technologies, <cjcollier@colliertech.org>
//    - Urs C. Muff, Quark Inc., <umuff@quark.com>
//    - Adham Findlay
//
//  Copyright (c) 2004 Quark Inc. and Collier Technologies.  All rights reserved.
//
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Method.cs,v 1.16 2004/06/22 13:38:59 urs Exp $
//

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	public class Method 
	{
		private string mMethodDeclaration;
		private string mGlueMethodName;
		private string mCSMethodName;
		private string[] mMessageParts;
		private string[] mArgumentNames;
		private string[] mArgumentDeclarationTypes;
		private bool mIsClassMethod, mIsUnsupported;
		private string mReturnDeclarationType;

		private static Regex[] sUnsupported = new Regex[] 
		{
			new Regex(@"<.*>"),
			new Regex(@"\.\.\.")
		};
		private static Regex sMatch1 = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)"); 

		public Method(string methodDeclaration) 
		{
			mMethodDeclaration = methodDeclaration.Trim();

			// Check for unsupported methods and return commented function
			// Unsupported methods include:
			// <.*> or ...
			foreach (Regex r in sUnsupported)
				if (r.IsMatch(mMethodDeclaration))
				{
					mIsUnsupported = true;
					return;
				}

			// It seems that methods take one of two formats.  Zero arguments:
			// - (RETURNTYPE)MethodName;
			// or N arguments
			// - (RETURNTYPE)MethodName:(TYPE0)Arg0 ... ArgNName:(TYPEN)ArgN;
			if (!sMatch1.IsMatch(mMethodDeclaration))
			{
				mIsUnsupported = true;
				return;
			}

			// \s*([+-])\s*(?:\(([^\)]+)\))?(.+)
			Match match = sMatch1.Match(mMethodDeclaration);

			string methodType = match.Groups[1].Value;
			mReturnDeclarationType = match.Groups[2].Value.Trim();
			if (mReturnDeclarationType.Length == 0)
				mReturnDeclarationType = "id";
			string remainder = match.Groups[3].Value;

			mIsClassMethod = methodType == "+";

			mReturnDeclarationType = mReturnDeclarationType.Replace("oneway ",string.Empty);

			// get rid of comments
			// remainder =~ s://.*::;
			// remainder =~ s:/\*.*\*/::;
    
			// These arrays store our method names, their arg names and types
			Regex noarg_rx = new Regex(@"^\s*(\w+)\s*([;\{]|$)");
			Regex arg_rx   = new Regex(@"(\w+):\s*(?:\(([^\)]+)\))?\s*(\w+)?(?:\s+|;)");

			ArrayList messageParts = new ArrayList();
			ArrayList argTypes = new ArrayList();
			ArrayList argNames = new ArrayList();
			if(noarg_rx.IsMatch(remainder))
			{
				// If there are no arguments (only matches method name)
				match = noarg_rx.Match(remainder);
				messageParts.Add(match.Groups[1].Value);
				mArgumentNames = new string[0];
				mArgumentDeclarationTypes = new string[0];
			} 
			else if(arg_rx.IsMatch(remainder)) 
			{
				while(arg_rx.IsMatch(remainder)) 
				{
					// If there are arguments, parse them
					GroupCollection grps = arg_rx.Match(remainder).Groups;
					for (int i = 1; i < grps.Count; )
					{
						messageParts.Add(grps[i++].Value);
						string argType = grps[i++].Value.Trim();
						string argName = grps[i++].Value.Trim();
						remainder = remainder.Replace(grps[0].Value, "");

						if (argType == string.Empty)
							argType = "id";
						else if (argName == string.Empty)
						{
							argName = argType;
							argType = "id";
						}
            
						argTypes.Add(argType);
						argNames.Add(argName);
					}
				}
				mArgumentNames = (string[])argNames.ToArray(typeof(string));
				mArgumentDeclarationTypes = (string[])argTypes.ToArray(typeof(string));
			} 
			else 
			{
				// If we can't parse the method, complain
				mIsUnsupported = true;
				return;
			}

			mMessageParts = (string[])messageParts.ToArray(typeof(string));
			
			mGlueMethodName = string.Empty;
			mCSMethodName = MakeCSMethodName(string.Join("_", mMessageParts));
			if (mIsClassMethod)
				mGlueMethodName += "_";
			mGlueMethodName += string.Join("_",mMessageParts);
		}

		public bool IsUnsupported
		{
			get { return mIsUnsupported; }
		}
        
		public string GlueMethodName 
		{
			get { return mGlueMethodName; }
		}
	
		public void ObjCMethod(string name,System.IO.TextWriter w)
		{
			if (mIsUnsupported)
			{
				w.WriteLine("// " + name + mGlueMethodName + ": not supported");
				return;
			}

			ArrayList _message = new ArrayList();
			ArrayList _params = new ArrayList();

			if (mMessageParts.Length == 1 && mArgumentNames.Length == 0) 
				_message.Add(mMessageParts[0]);
			else
			{
				for(int i = 0; i < mMessageParts.Length; ++i)
				{
					_params.Add(string.Format("{0} {1}",mArgumentDeclarationTypes[i],"p" + i));
					_message.Add(string.Format("{0}: {1}", mMessageParts[i],"p" + i));
				}
			}

			// The objc message to send the object
			string message = string.Join(" ", (string[])_message.ToArray(typeof(string)));
			string receiver, body;

			if(mIsClassMethod) 
			{
				// If the method is a class method
				_params.Insert(0,"Class CLASS");
				receiver = "CLASS";
				body = "\tif (!CLASS) CLASS = [" + name + " class];\n";
			}
			else
			{
				// If the method is an instance method
				_params.Insert(0,"id THIS");
				receiver = "THIS";
				body = string.Empty;
			}

			// Add the log call
			body += string.Format("\tNSLog(@\"{0}: %@\\n\", {1});",mGlueMethodName,receiver);

			// The parameters to the C function
			string paramsStr = string.Join(", ", (string[])_params.ToArray(typeof(string)));

			// Will we be returning?
			string retter = mReturnDeclarationType == "void" ? string.Empty : "return ";

			// Return the lines of the wrapper
			w.WriteLine(mReturnDeclarationType + " " + name + "_" + mGlueMethodName + "(" + paramsStr + ") {");
			w.WriteLine(body);
			w.WriteLine("\t" + retter + "[" + receiver + " " + message + "];");
			w.WriteLine("}");
		}

		public void CSGlueMethod(string name,string glueLib,System.IO.TextWriter w)
		{
			if (mIsUnsupported)
			{
				w.WriteLine("// " + name + mGlueMethodName + ": not supported");
				return;
			}

			string _type = convertTypeGlue(mReturnDeclarationType);
			ArrayList _params = new ArrayList();

			if (mIsClassMethod)
				_params.Add("IntPtr CLASS");
			else
				_params.Add("IntPtr THIS");

			for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
			{
				string t = convertTypeGlue(mArgumentDeclarationTypes[i]);
				_params.Add(t + " p" + i + "/*" + mArgumentNames[i] + "*/");
			}

			string paramsStr = string.Join(", ", (string[])_params.ToArray(typeof(string)));

			// [DllImport("AppKitGlue")]
			// protected internal static extern void NSButton_setTitle(IntPtr THIS, IntPtr aString);
			w.WriteLine("        [DllImport(\"" + glueLib + "\")]");
			w.WriteLine("        protected internal static extern " +
				_type + " " + name + "_" + mGlueMethodName + " (" + paramsStr + ");");
		}
	
		public void CSAPIMethod(string name,System.IO.TextWriter w)
		{
			if (mIsUnsupported)
				return;

			string _type = convertType(mReturnDeclarationType);
			ArrayList _params = new ArrayList();
			ArrayList _csparams = new ArrayList();

			if (mIsClassMethod)
				_csparams.Add(name + "_class");
			else
				_csparams.Add("Raw");
			

			for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
			{
				string t = convertType(mArgumentDeclarationTypes[i]);
				_params.Add(t + " p" + i + "/*" + mArgumentNames[i] + "*/");
				if(t != convertTypeGlue(mArgumentDeclarationTypes[i]))
					_csparams.Add("Net2NS(p" + i + ") /* " + mArgumentNames[i] + "*/");
				else 
					_csparams.Add("p" + i + "/*" + mArgumentNames[i] + "*/");
			}

			string paramsStr = string.Join(", ", (string[])_params.ToArray(typeof(string)));
			string csparamsStr = string.Join(", ", (string[])_csparams.ToArray(typeof(string)));

			w.WriteLine("        public {0} {1} {2} ({3}) {{", (mIsClassMethod ? "static" : ""), _type, mCSMethodName, paramsStr); 
			if(_type != convertTypeGlue(mReturnDeclarationType))
				w.WriteLine("            return ({0})NS2Net({1}_{2}({3}));", _type, name, mGlueMethodName, csparamsStr);
			else 
				w.WriteLine("            {0}{1}_{2}({3});", _type == "void" ? "" : "return ", name, mGlueMethodName, csparamsStr);
			w.WriteLine("        }");

			if (!mIsClassMethod && mCSMethodName.StartsWith("init") && _type == "id" && _params.Count > 0)
			{
				w.WriteLine("        public {0} ({1}) {{", name, paramsStr); 
				w.WriteLine("            SetRaw({0}_{1}({2}),_release);", name, mGlueMethodName, csparamsStr);
				w.WriteLine("        }");
			}
		}

		public void CSInterfaceMethod(string name,System.IO.TextWriter w)
		{
			if (mIsUnsupported)
				return;

			string _type = convertType(mReturnDeclarationType);
			ArrayList _params = new ArrayList();

			for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
			{
				string t = convertType(mArgumentDeclarationTypes[i]);
				_params.Add(t + " p" + i + "/*" + mArgumentNames[i] + "*/");
			}

			string paramsStr = string.Join(", ", (string[])_params.ToArray(typeof(string)));
			w.WriteLine("        {0} {1} {2} ({3}); ", (mIsClassMethod ? "static" : ""), _type, mCSMethodName, paramsStr);
		}

		private static string MakeCSMethodName(string name)
		{
			switch (name) 
			{
				case "delegate":
				case "this":
				case "lock":
				case "base":
				case "int":
				case "string":
				case "class":
				case "object":
					return "_" + name;
			}
			return name;
		}

		private static string convertTypeNative(string type)
		{
			type = type.Trim();
			switch (type) 
			{
				case "void": return "void";
				case "BOOL": return "bool";
				case "float": return "float";
				case "double": return "double";
				case "unichar": return "char";
				case "char": return "sbyte";
				case "unsigned char": return "byte";
				case "unsigned short": return "ushort";
				case "short": return "short";
				case "int": case "long int": case "int32_t": case "SInt32":
					return "int";
				case "unsigned": case "unsigned int": case "unsigned long int": 
					return "uint";
				case "unsigned long": return "ulong";
				case "long long": case "int64_t": return "Int64";
				case "unsigned long long": return "UInt64";

				case "OSType":
					return "int";

				case "va_list":
				case "IMP":
					return "IntPtr /*(" + type + ")*/";
			}

			return type;
		}

		private static string convertTypeGlue(string type) 
		{
			type = convertTypeNative(type.Replace("const ",string.Empty));
			switch (type) 
			{
				case "id": case "Class": case "SEL":
					return "IntPtr /*(" + type + ")*/";
				default:
					if (type.EndsWith("*"))
						return "IntPtr /*(" + type + ")*/";
					break;
			}
			return type;
		}

		private static string convertType(string type) 
		{
			type = convertTypeNative(type.Replace("const ",string.Empty));
			switch (type) 
			{
				case "id": return "object";
				case "Class": return "Class";
				case "SEL": return "string";
				default:
					if (type.EndsWith("*"))
						if (type.StartsWith("NS"))
							return type.StartsWith("NSString") ? "string" : type.Replace("*", "");
						else
							return "IntPtr /*(" + type + ")*/";
					break;
			}
			return type;
		}
	}
}

//	$Log: Method.cs,v $
//	Revision 1.16  2004/06/22 13:38:59  urs
//	More cleanup and refactoring start
//	Make output actually compile (diverse fixes)
//
//	Revision 1.15  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//	
//
