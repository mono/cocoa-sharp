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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Method.cs,v 1.21 2004/06/23 16:32:35 urs Exp $
//

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter 
{

	public class Method 
	{
		#region -- Members --
		private string mMethodDeclaration;
		private string mGlueMethodName;
		private string mCSMethodName;
		private string[] mMessageParts;
		private string[] mArgumentNames;
		private string[] mArgumentDeclarationTypes;
		private string[] mCSAPIParameters;
		private string[] mCSGlueArguments;
		private bool mIsClassMethod, mIsUnsupported, mCSAPIDone;
		private string mReturnDeclarationType;

		private static Regex[] sUnsupported = new Regex[] 
		{
			new Regex(@"<.*>"),
			new Regex(@"\.\.\.")
		};
		private static Regex sMatch1 = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)");
		#endregion

		#region -- Constructor --
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
			string methodDecl = mMethodDeclaration.Replace("oneway ",string.Empty);
			methodDecl = methodDecl.Replace("IBAction","void");
			Match match = sMatch1.Match(methodDecl);

			string methodType = match.Groups[1].Value;
			mReturnDeclarationType = match.Groups[2].Value.Trim();
			if (mReturnDeclarationType.Length == 0)
				mReturnDeclarationType = "id";
			string remainder = match.Groups[3].Value;

			mIsClassMethod = methodType == "+";

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
		#endregion

		#region -- Properties --
		public bool IsUnsupported
		{
			get { return mIsUnsupported; }
		}
        
		public string GlueMethodName 
		{
			get { return mGlueMethodName; }
		}

		public bool IsConstructor
		{
			get { return !mIsUnsupported
				&& !mIsClassMethod && mCSMethodName.StartsWith("init") 
				&& mReturnDeclarationType == "id" && mArgumentDeclarationTypes.Length > 0; }
		}

		public string CSConstructorSignature
		{
			get
			{
				if (!IsConstructor)
					return null;
	
				ArrayList argTypes = new ArrayList();
				for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
					argTypes.Add(ConvertType(mArgumentDeclarationTypes[i]));
	
				return string.Join(",",(string[])argTypes.ToArray(typeof(string)));
			}
		}
		#endregion

		public void BuildArgs(string name)
		{
			if (mCSAPIParameters != null)
				return;

			ArrayList _params = new ArrayList();
			ArrayList _glueArgs = new ArrayList();

			if (mIsClassMethod)
				_glueArgs.Add(name + "_classPtr");
			else
				_glueArgs.Add("Raw");
			
			for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
			{
				string t = ConvertType(mArgumentDeclarationTypes[i]);
				_params.Add(t + " p" + i + "/*" + mArgumentNames[i] + "*/");
				_glueArgs.Add(ArgumentExpression(mArgumentDeclarationTypes[i],"p" + i + "/*" + mArgumentNames[i] + "*/"));
			}

			mCSAPIParameters = (string[])_params.ToArray(typeof(string));
			mCSGlueArguments = (string[])_glueArgs.ToArray(typeof(string));
		}

		#region -- Objective-C --
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
		#endregion

		#region -- C# Glue --
		public void CSGlueMethod(string name,string glueLib,System.IO.TextWriter w)
		{
			if (mIsUnsupported)
			{
				w.WriteLine("        // " + mMethodDeclaration + ": not supported");
				return;
			}

			string _type = ConvertTypeGlue(mReturnDeclarationType);
			ArrayList _params = new ArrayList();

			if (mIsClassMethod)
				_params.Add("IntPtr CLASS");
			else
				_params.Add("IntPtr THIS");

			for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
			{
				string t = ConvertTypeGlue(mArgumentDeclarationTypes[i]);
				_params.Add(t + " p" + i + "/*" + mArgumentNames[i] + "*/");
			}

			string paramsStr = string.Join(", ", (string[])_params.ToArray(typeof(string)));

			// [DllImport("AppKitGlue")]
			// protected internal static extern void NSButton_setTitle(IntPtr THIS, IntPtr aString);
			w.WriteLine("        [DllImport(\"" + glueLib + "\")]");
			w.WriteLine("        protected internal static extern " +
				_type + " " + name + "_" + mGlueMethodName + " (" + paramsStr + ");");
		}
		#endregion

		#region -- C# Public API --
		public bool IsGetMethod(string type)
		{
			if (type != ConvertType(mReturnDeclarationType))
				return false;
			if (mArgumentDeclarationTypes.Length > 0)
				return false;
			mCSAPIDone = true;
			return true;
		}
	
		public void CSAPIMethod(string name,IDictionary methods,bool propOnly,System.IO.TextWriter w)
		{
			if (mIsUnsupported || mCSAPIDone)
				return;

			string _type = ConvertType(mReturnDeclarationType);
			BuildArgs(name);
			string paramsStr = string.Join(", ", mCSAPIParameters);
			string glueArgsStr = string.Join(", ", mCSGlueArguments);
			bool isVoid = _type == "void";
			
			if (isVoid && mArgumentDeclarationTypes.Length == 1 && mCSMethodName.StartsWith("set"))
			{
				string t = ConvertType(mArgumentDeclarationTypes[0]);
				string propName = mCSMethodName.Substring(3);
				string getPropName = propName.Substring(0,1).ToLower() + propName.Substring(1);

				Method get = (Method)methods[getPropName];
				
				if (get == null)
					get = (Method)methods["is" + propName];
				
				w.WriteLine("        public {0}{1} {2} {{", (mIsClassMethod ? "static " : ""), t, propName);
				if (get != null && get.IsGetMethod(t))
					w.WriteLine("            get {{ {0}; }}", ReturnExpression(mArgumentDeclarationTypes[0],string.Format("{0}_{1}({2})",name, get.GlueMethodName, mCSGlueArguments[0])));

				w.WriteLine("            set {{ {0}_{1}({2},{3}); }}", name, mGlueMethodName, mCSGlueArguments[0],ArgumentExpression(mArgumentDeclarationTypes[0],"value"));
				w.WriteLine("        }");
				mCSAPIDone = true;
				
				return;
			}
			
			if (propOnly)
				return;

			w.WriteLine("        public {0}{1} {2} ({3}) {{", (mIsClassMethod ? "static " : ""), _type, mCSMethodName, paramsStr); 
			w.WriteLine("            {0};",ReturnExpression(mReturnDeclarationType,string.Format("{0}_{1}({2})", name, mGlueMethodName, glueArgsStr)));
			w.WriteLine("        }");
		}
		
		private static string ReturnExpression(string declType, string expression)
		{
			string t = ConvertType(declType);
			if(StripComments(declType) == "SEL")
				return string.Format("return NSString.FromSEL({0}).ToString()", expression);
			else if(t != ConvertTypeGlue(declType))
				return string.Format("return ({0})NS2Net({1})", t, expression);
			else if (t == "void")
				return expression;
			else
				return "return " + expression;
		}

		private static string ArgumentExpression(string declType, string expression)
		{
			string t = ConvertType(declType);
			if(StripComments(declType) == "SEL")
				return string.Format("NSString.NSSelector({0})", expression);
			else if(t != ConvertTypeGlue(declType))
				return string.Format("Net2NS({0})", expression);
			else
				return expression;
		}

		public void CSConstructor(string name,TextWriter w)
		{
			if (!IsConstructor)
				return;

			BuildArgs(name);
			ArrayList args = new ArrayList();
			for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
				args.Add("p" + i);

			w.WriteLine("        public {0}({1}) {{", name, string.Join(", ", mCSAPIParameters));
			w.WriteLine("            {0}({1});", mCSMethodName, string.Join(", ", (string[])args.ToArray(typeof(string))));
			w.WriteLine("        }");
		}
		#endregion

		#region -- C# Interface --
		public void CSInterfaceMethod(string name,System.IO.TextWriter w)
		{
			if (mIsUnsupported)
				return;

			string _type = ConvertType(mReturnDeclarationType);
			ArrayList _params = new ArrayList();

			for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
			{
				string t = ConvertType(mArgumentDeclarationTypes[i]);
				_params.Add(t + " p" + i + "/*" + mArgumentNames[i] + "*/");
			}

			string paramsStr = string.Join(", ", (string[])_params.ToArray(typeof(string)));
			w.WriteLine("        {0} {1} {2} ({3}); ", (mIsClassMethod ? "static" : ""), _type, mCSMethodName, paramsStr);
		}
		#endregion

		#region -- Private Functions --
		private static string MakeCSMethodName(string name)
		{
			switch (name) 
			{
				case "new": case "override": case "virtual": case "typeof":
				case "is": case "as": case "delegate": case "this":
				case "base": case "lock": case "object": case "string":
				case "int": case "short": case "long": case "bool":
				case "void": case "char": case "static": case "class":
				case "interface": case "struct": case "enum": case "null":
				case "private": case "public": case "protected":
				case "internal": case "if": case "else": case "switch":
				case "for": case "foreach": case "while": case "do":
				case "case": case "return": case "default":
				case "continue": case "break":
					return name + "_";
			}
			return name;
		}

		internal static string StripComments(string str)
		{
			int ndx = str.IndexOf("/*");
			while (ndx >= 0)
			{
				int ndx2 = str.IndexOf("*/",ndx,str.Length-ndx);
				if (ndx2 >= ndx)
				{
					str = str.Remove(ndx,ndx2-ndx+2);
					ndx = str.IndexOf("/*");
				}
				else
					ndx = -1;
			}
			return str.Trim();
		}

		private static string ConvertTypeNative(string type)
		{
			type = StripComments(type);
			switch (type) 
			{
				case "void": return "void";
				case "BOOL": return "bool";
				case "float": return "float";
				case "double": return "double";
				case "unichar": return "char";
				case "char": return "sbyte";
				case "unsigned char": case "uint8_t": return "byte";
				case "short": case "short int":
					return "short";
				case "unsigned short": case "unsigned short int":
					return "ushort";
				case "int": case "int32_t": case "SInt32":
					return "int";
				case "unsigned": case "unsigned int": case "UInt32": case "UTF32Char":
					return "uint";
				case "long": case "long int":
					return "long";
				case "unsigned long": case "unsigned long int":
					return "ulong";
				case "long long": case "int64_t": case "SInt64":
					return "Int64";
				case "unsigned long long": case "UInt64":
					return "UInt64";

				case "OSErr":
					return "Int16";
				case "OSType":
					return "int";

				case "va_list":
				case "IMP":
					return "IntPtr /*(" + type + ")*/";
			}

			if (new Regex(@"char\s*\*").IsMatch(type))
				return "string";

			return type;
		}

		private static string ConvertTypeGlue(string type) 
		{
			type = ConvertTypeNative(type.Replace("const ",string.Empty));
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

		private static string ConvertType(string type) 
		{
			type = ConvertTypeNative(type.Replace("const ",string.Empty));
			switch (type) 
			{
				case "id": return "object";
				case "Class": return "Class";
				case "SEL": return "string";
				default:
					if (type.EndsWith("*"))
						if (type.StartsWith("NS"))
							return type.StartsWith("NSString") ? "string" : type.Replace("*", "").Trim();
						else
							return "IntPtr /*(" + type + ")*/";
					break;
			}
			return type;
		}
		#endregion
	}
}

//	$Log: Method.cs,v $
//	Revision 1.21  2004/06/23 16:32:35  urs
//	Add SEL support
//
//	Revision 1.20  2004/06/23 15:29:29  urs
//	Major refactor, allow inheriting parent constructors
//	
//	Revision 1.19  2004/06/22 19:54:21  urs
//	Add property support
//	
//	Revision 1.18  2004/06/22 15:13:18  urs
//	New fixing
//	
//	Revision 1.17  2004/06/22 14:16:20  urs
//	Minor fix
//	
//	Revision 1.16  2004/06/22 13:38:59  urs
//	More cleanup and refactoring start
//	Make output actually compile (diverse fixes)
//	
//	Revision 1.15  2004/06/22 12:04:12  urs
//	Cleanup, Headers, -out:[CS|OC], VS proj
//	
//
