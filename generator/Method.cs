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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Method.cs,v 1.27 2004/06/24 02:16:05 gnorton Exp $
//

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace ObjCManagedExporter 
{

	[XmlRoot("conversions")]
	public class TypeConversions
	{
		[XmlElement("type")] public NativeData[] Conversions;
		[XmlElement("regex")] public NativeData[] Regexs;
		[XmlElement("replace")] public ReplaceData[] Replaces;
	}

	public class NativeData
	{
		[XmlAttribute("native")] public string Native;
		[XmlAttribute("api")] public string Api;
		[XmlAttribute("glue")] public string Glue;
	}

	public class ReplaceData
	{
		[XmlAttribute("type")] public string Type;
		[XmlAttribute("regex")] public string Regex;
		[XmlAttribute("old")] public string ToReplace;
		[XmlAttribute("new")] public string ReplaceWith;
	}

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
		private static TypeConversions mConversions;

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
			XmlSerializer _ser = new XmlSerializer(typeof(TypeConversions));
			XmlTextReader _xtr = new XmlTextReader("generator/typeconversion.xml");
			mConversions = (TypeConversions)_ser.Deserialize(_xtr);
			_xtr.Close();

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
			mCSMethodName = MakeCSMethodName(mMessageParts[0] /*string.Join("_", mMessageParts)*/);
			if (mIsClassMethod)
				mGlueMethodName += "_";
			mGlueMethodName += string.Join("_",mMessageParts) + mArgumentNames.Length;
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
		
		public string Selector
		{
			get
			{
				if (mMessageParts.Length == 1 && mArgumentNames.Length == 0) 
					return mMessageParts[0];

				string ret = string.Empty;
				
				for(int i = 0; i < mMessageParts.Length; ++i)
					ret += mMessageParts[i] + ":";
				return ret;
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
			body += string.Format("\tNSLog(@\"{0}: %@\\n\", {1});",name + "_" + mGlueMethodName,receiver);

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
	
		public void CSAPIMethod(string name,IDictionary methods,bool propOnly,System.IO.TextWriter w, Overrides _o)
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

				Method get = (Method)methods[propName.Substring(0,1).ToLower() + propName.Substring(1) + "0"];
				
				if (get == null)
					get = (Method)methods["is" + propName + "0"];
				if (get == null)
					get = (Method)methods[propName + "0"];
				
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

			// Check to see if we're overridden
			if(_o != null)
				foreach(MethodOverride _mo in _o.Methods) 
					if(_mo.Selector == Selector && _mo.InstanceMethod != mIsClassMethod) {
						w.WriteLine("        //{0} is overridden", Selector);
						w.WriteLine(_mo.Method);
						return;
					}

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

		private static string ConvertTypeGlue(string type) 
		{
			type = StripComments(type.Replace("const ",string.Empty));
			foreach (NativeData nd in mConversions.Conversions)
				if(type == nd.Native)
					return nd.Glue;

			foreach (NativeData nd in mConversions.Regexs)
				if(new Regex(nd.Native).IsMatch(type))
					return nd.Glue;

			foreach (ReplaceData rd in mConversions.Replaces)
				if(rd.Type == "glue")
					if(new Regex(rd.Regex).IsMatch(type))
						return type.Replace(rd.ToReplace, rd.ReplaceWith);
			
			return type;
		}

		private static string ConvertType(string type) 
		{
			type = StripComments(type.Replace("const ",string.Empty));
			foreach (NativeData nd in mConversions.Conversions)
				if(type == nd.Native)
					return nd.Api;

			foreach (NativeData nd in mConversions.Regexs)
				if(new Regex(nd.Native).IsMatch(type))
					return nd.Api;

			foreach (ReplaceData rd in mConversions.Replaces)
				if(rd.Type == "api")
					if(new Regex(rd.Regex).IsMatch(type))
						return type.Replace(rd.ToReplace, rd.ReplaceWith);
			
			return type;
		}
		#endregion
	}
}

//	$Log: Method.cs,v $
//	Revision 1.27  2004/06/24 02:16:05  gnorton
//	Updated out typeconversions to be loaded from an XML file; instead of being hard coded.  In the future we wont need to update the app to update the types.
//
//	Revision 1.26  2004/06/23 18:18:32  urs
//	Allow same case get/set properties
//	
//	Revision 1.25  2004/06/23 17:55:41  urs
//	Make test compile with the lasted glue API name change
//	
//	Revision 1.24  2004/06/23 17:52:41  gnorton
//	Added ability to override what the generator outputs on a per-file/per-method basis
//	
//	Revision 1.23  2004/06/23 17:23:41  urs
//	Rename glue methods to include argument count to differenciate 'init' from 'init:'.
//	
//	Revision 1.22  2004/06/23 17:05:33  urs
//	Add selector to Method
//	
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
