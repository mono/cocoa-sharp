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
//	$Header: /home/miguel/third-conversion/public/cocoa-sharp/generator/Attic/Method.cs,v 1.39 2004/06/28 21:31:22 gnorton Exp $
//

using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace ObjCManagedExporter 
{

	[XmlRoot("mappings")]
	public class Mappings
	{
		[XmlElement("property")] public PropertyMapping[] Properties;
		[XmlElement("method")] public MethodMapping[] Methods;
	}

	public class PropertyMapping {
		[XmlAttribute("name")] public string Name;
		[XmlAttribute("instance")] public bool Instance;
		[XmlAttribute("get")] public string GetSelector;
		[XmlAttribute("set")] public string SetSelector;
		[XmlAttribute("returntype")] public string ReturnType;
	}

	public class MethodMapping {
		[XmlAttribute("name")] public string Name;
		[XmlAttribute("instance")] public bool Instance;
		[XmlAttribute("selector")] public string Selector;
		[XmlAttribute("returntype")] public string ReturnType;
	}
		

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
		[XmlAttribute("gluearg")] public string GlueArg;
		[XmlAttribute("format")] public string Format;
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
		private string[] mArgumentGlueTypes;
		private string[] mArgumentAPITypes;
		private string[] mCSAPIParameters;
		private string[] mCSGlueArguments;
		private bool mIsClassMethod, mIsUnsupported, mCSAPIDone;
		private string mReturnDeclarationType;
		private string mReturnGlueType;
		private string mReturnAPIType;

		private static TypeConversions sConversions;
		private static Mappings sNameMappings;
		private static IDictionary Conversions;
		private static IDictionary NameMappings;

		private static Regex[] sUnsupported = new Regex[] 
		{
			new Regex(@"<.*>"),
			new Regex(@"\.\.\.")
		};
		private static Regex sMatch1 = new Regex(@"\s*([+-])\s*(?:\(([^\)]+)\))?(.+)");
		#endregion

		#region -- Constructor --
		static Method() {
			XmlSerializer _ser = new XmlSerializer(typeof(TypeConversions));
			XmlTextReader _xtr = new XmlTextReader("generator/typeconversion.xml");
			sConversions = (TypeConversions)_ser.Deserialize(_xtr);
			_xtr.Close();
			Conversions = new Hashtable();
			foreach (NativeData nd in sConversions.Conversions)
				Conversions[nd.Native] = nd;

			_ser = new XmlSerializer(typeof(Mappings));
			_xtr = new XmlTextReader("generator/mapping.xml");
			sNameMappings = (Mappings)_ser.Deserialize(_xtr);
			_xtr.Close();
			
			NameMappings = new Hashtable();
			if(sNameMappings.Properties != null)
                foreach (PropertyMapping map in sNameMappings.Properties) {
                    if(map.GetSelector != null)
                        NameMappings[map.GetSelector] = map;
                    if(map.SetSelector != null)
                        NameMappings[map.SetSelector] = map;
                }
			if(sNameMappings.Methods != null)
                foreach (MethodMapping map in sNameMappings.Methods)
                    NameMappings[map.Selector] = map;
		} 

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
			mReturnDeclarationType = StripComments(match.Groups[2].Value.Trim());
			if (mReturnDeclarationType.Length == 0)
				mReturnDeclarationType = "id";
			mReturnGlueType = ConvertTypeGlue(mReturnDeclarationType,false);
			mReturnAPIType = ConvertType(mReturnDeclarationType,false);
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
				mArgumentGlueTypes = new string[0];
				mArgumentAPITypes = new string[0];
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
						else
							argType = StripComments(argType);

						argTypes.Add(argType);
						argNames.Add(argName);
					}
				}
				mArgumentNames = (string[])argNames.ToArray(typeof(string));
				mArgumentDeclarationTypes = (string[])argTypes.ToArray(typeof(string));
				mArgumentGlueTypes = new string[mArgumentDeclarationTypes.Length];
				mArgumentAPITypes = new string[mArgumentDeclarationTypes.Length];
				for (int i = 0; i < mArgumentDeclarationTypes.Length; ++i)
				{
					mArgumentGlueTypes[i] = ConvertTypeGlue(mArgumentDeclarationTypes[i],true);
					mArgumentAPITypes[i] = ConvertType(mArgumentDeclarationTypes[i],true);
				}
			} 
			else 
			{
				// If we can't parse the method, complain
				mIsUnsupported = true;
				return;
			}

			mMessageParts = (string[])messageParts.ToArray(typeof(string));
			
			mGlueMethodName = string.Empty;
			mCSMethodName = MakeCSMethodName(/*mMessageParts[0]*/ string.Join("_", mMessageParts));
			if (mIsClassMethod)
				mGlueMethodName += "_";

			mGlueMethodName += string.Join("_",mMessageParts) + mArgumentNames.Length;
		}
		#endregion

		#region -- Properties --
		public bool IsUnsupported { get { return mIsUnsupported; } }
		public bool IsClassMethod { get { return mIsClassMethod; } }
		public string GlueMethodName { get { return mGlueMethodName; } }
		public string MethodDeclaration { get { return mMethodDeclaration; } }
		
		public void SetCSAPIDone()
		{
            mCSAPIDone = true;
		}

        public string ReturnDeclarationType { get { return mReturnDeclarationType; } }
        public string ReturnGlueType { get { return mReturnGlueType; } }
        public string ReturnAPIType { get { return mReturnAPIType; } }
		public string FirstCSGlueArgument { get { return mCSGlueArguments[0]; } }
		public string FirstArgumentDeclarationType { get { return mArgumentDeclarationTypes[0]; } }
		public string FirstArgumentGlueType { get { return mArgumentGlueTypes[0]; } }
		public string FirstArgumentAPIType { get { return mArgumentAPITypes[0]; } }

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
					argTypes.Add(StripComments(mArgumentAPITypes[i]));
	
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
				string t = mArgumentAPITypes[i];
				_params.Add(t + " p" + i + "/*" + mArgumentNames[i] + "*/");
				_glueArgs.Add(ArgumentExpression(mArgumentDeclarationTypes[i],
					mArgumentGlueTypes[i],mArgumentAPITypes[i],
					"p" + i + "/*" + mArgumentNames[i] + "*/"));
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
			string formatArgs = string.Empty, logArgs = string.Empty;
			string receiver = mIsClassMethod ? "CLASS" : "THIS";

			_params.Add(mIsClassMethod ? "Class CLASS" : "id THIS");

			if (mMessageParts.Length == 1 && mArgumentNames.Length == 0) 
				_message.Add(mMessageParts[0]);
			else
			{
				for(int i = 0; i < mMessageParts.Length; ++i)
				{
					string pName = "p" + i;
					_params.Add(string.Format("{0} {1}",mArgumentDeclarationTypes[i],pName));
					_message.Add(string.Format("{0}: {1}", mMessageParts[i],pName));
					LogFormatForType(mArgumentDeclarationTypes[i],pName,",",ref formatArgs,ref logArgs);
				}
			}

			// The objc message to send the object
			string message = string.Join(" ", (string[])_message.ToArray(typeof(string)));
			// The parameters to the C function
			string paramsStr = string.Join(", ", (string[])_params.ToArray(typeof(string)));

			string expr = "[" + receiver + " " + message + "]";
			bool isVoid = mReturnDeclarationType == "void";
			
			if (!isVoid)
				LogFormatForType(mReturnDeclarationType,"_ret"," --> ",ref formatArgs,ref logArgs);

			w.WriteLine("{0} {1}_{2}({3}) {{",mReturnDeclarationType,name,mGlueMethodName,paramsStr);
			if (mIsClassMethod)
				w.WriteLine("\tif (!CLASS) CLASS = [{0} class];",name);
			if (!isVoid)
				w.WriteLine("\t{0} _ret = {1};",mReturnDeclarationType,expr);
			w.WriteLine("\tNSLog(@\"{0}: %@{2}\\n\", {1}{3});",name + "_" + mGlueMethodName, receiver, formatArgs, logArgs);
			if (isVoid)
				w.WriteLine("\t{0};",expr);
			else
				w.WriteLine("\treturn _ret;");
			w.WriteLine("}");
		}
		
		private static void LogFormatForType(string type, string a, string sep, ref string format, ref string arg)
		{
			NativeData nd = (NativeData)Conversions[type];
			format += sep;
			arg += ",";
			if(nd != null && nd.Format != null)
			{
				format += nd.Format;
				arg += a;
			}
			else if (type == "id")
			{
				format += "<%@: %p>";
				arg += "[" + a + " class]," + a;
			}
			else
			{
				format += "%s";
				arg += "\"" + type + "\"";
			}
		}
		#endregion

		#region -- C# Glue --
		public void CSGlueMethod(string name,string glueLib,System.IO.TextWriter w, Overrides _o)
		{
			if (mIsUnsupported)
			{
				w.WriteLine("        // " + mMethodDeclaration + ": not supported");
				return;
			}
			
			if(_o != null && _o.GlueMethods != null)
				foreach(MethodOverride _mo in _o.GlueMethods) 
					if(_mo.Selector == Selector && _mo.InstanceMethod != mIsClassMethod) {
						w.WriteLine("        //{0} is overridden", Selector);
						w.WriteLine(_mo.Method);
						return;
					}


			string _type = mReturnGlueType;
			ArrayList _params = new ArrayList();

			if (mIsClassMethod)
				_params.Add("IntPtr CLASS");
			else
				_params.Add("IntPtr THIS");

			for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
			{
				string t = mArgumentGlueTypes[i];
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
			if ("void" == mReturnAPIType)
				return false;
			if (mArgumentDeclarationTypes.Length > 0)
				return false;
			mCSAPIDone = true;
			return true;
		}
		
		public Method GetGetMethod(IDictionary methods, out string propName)
		{
			propName = mCSMethodName.Substring(3);

			Method get = (Method)methods[propName.Substring(0,1).ToLower() + propName.Substring(1) + "0"];
			
			if (get == null)
				get = (Method)methods["is" + propName + "0"];
			if (get == null)
				get = (Method)methods[propName + "0"];
			
			propName = MakeCSMethodName(propName);
			return get;
		}

        private void GenerateProperty(string name,System.IO.TextWriter w, Method get, Method set, string propName) {
            bool hasGet = get != null;
            bool hasSet = set != null;
            string t = hasGet ? get.ReturnAPIType : set.FirstArgumentAPIType;
            bool isClassMethod = hasGet ? get.IsClassMethod : set.IsClassMethod;
            
            if(hasSet)
                w.WriteLine("        // setSelector: {0}", set.MethodDeclaration);
            if (hasGet)
                w.WriteLine("        // getSelector: {0}", get.MethodDeclaration);
            
            w.WriteLine("        public {0}{1} {2} {{", (isClassMethod ? "static " : ""), t, propName);
            
            if (hasGet) {
                get.BuildArgs(name);
                w.WriteLine("            get {{ {0}; }}", ReturnExpression(
                    get.ReturnDeclarationType,get.ReturnGlueType,get.ReturnAPIType, 
                        string.Format("{0}_{1}({2})",name, get.GlueMethodName, get.FirstCSGlueArgument)));
                get.SetCSAPIDone();
            }

            if (hasSet) {
                set.BuildArgs(name);
                w.WriteLine("            set {{ {0}_{1}({2},{3}); }}", name, set.GlueMethodName, set.FirstCSGlueArgument,
                ArgumentExpression(set.FirstArgumentDeclarationType,set.FirstArgumentGlueType,set.FirstArgumentAPIType,
                    "value"));
                set.SetCSAPIDone();
            }
            w.WriteLine("        }");
            // Check to see if this selector is in our map
            if (hasGet && !NameMappings.Contains(get.Selector))
                NameMappings[get.Selector] = GeneratePropertyMapping(propName, get, set);
            if (hasSet && !NameMappings.Contains(set.Selector))
                NameMappings[set.Selector] = GeneratePropertyMapping(propName, get, set);
        }	
		public void CSAPIMethod(string name,IDictionary methods,bool propOnly,System.IO.TextWriter w, Overrides _o)
		{
			if (mIsUnsupported)
				return;
			if (mCSAPIDone)
				return;

			// Check to see if we're overridden
			if(_o != null && _o.Methods != null)
				foreach(MethodOverride _mo in _o.Methods) 
					if(_mo.Selector == Selector && _mo.InstanceMethod != mIsClassMethod) {
						w.WriteLine("        //{0} is overridden", Selector);
						w.WriteLine(_mo.Method);
						mCSAPIDone = true;
						return;
					}

			string _type = mReturnAPIType;
			BuildArgs(name);
			string paramsStr = string.Join(", ", mCSAPIParameters);
			string glueArgsStr = string.Join(", ", mCSGlueArguments);
			bool isVoid = _type == "void";
			
            if(NameMappings.Contains(Selector)) {
                object Mapping = NameMappings[Selector];
                if(Mapping is PropertyMapping) {
                    PropertyMapping propMap = (PropertyMapping)Mapping;
                    Method getMethod = null;
                    Method setMethod = null;
                    if(propMap.GetSelector != null)
                        getMethod = (Method)methods[propMap.GetSelector];
                    if(propMap.SetSelector != null)
                        setMethod = (Method)methods[propMap.SetSelector];
                    GenerateProperty(name, w, getMethod, setMethod, propMap.Name);
                }
                if(Mapping is MethodMapping) {
                     // Output according to map
                }
                return;
            }
			
			if (isVoid && mArgumentDeclarationTypes.Length == 1 && mCSMethodName.StartsWith("set"))
			{
                string propName;
                Method get = GetGetMethod(methods, out propName);
                GenerateProperty(name, w, get, this, propName);
                return;
			}
			
			if (propOnly)
				return;

			if (!mIsClassMethod && !isVoid && mArgumentDeclarationTypes.Length == 0)
			{
				string _propName = MakeCSMethodName(mCSMethodName);
				GenerateProperty(name, w, this, null, _propName); 
			    return;
			}

			w.WriteLine("        // {0}", mMethodDeclaration);
			w.WriteLine("        public {0}{1} {2} ({3}) {{", (mIsClassMethod ? "static " : ""), _type, mCSMethodName, paramsStr); 
			w.WriteLine("            {0};",ReturnExpression(mReturnDeclarationType,mReturnGlueType,mReturnAPIType,
				string.Format("{0}_{1}({2})", name, mGlueMethodName, glueArgsStr)));
			w.WriteLine("        }");
			
			// Check to see if this selector is in our map
            if(!NameMappings.Contains(Selector))
                NameMappings[Selector] = GenerateMethodMapping();
            return;    
		}
		
		private PropertyMapping GeneratePropertyMapping(String propName, Method get, Method set) {
            PropertyMapping pm = new PropertyMapping();
            pm.Name = propName;
            if(get != null)
                pm.GetSelector = get.Selector;
            if(set != null)
                pm.SetSelector = set.Selector;
            pm.Instance = !mIsClassMethod;
            return pm;
        }
        
		private MethodMapping GenerateMethodMapping() {
            MethodMapping mm = new MethodMapping();
            mm.Name = mCSMethodName;
            mm.Instance = !mIsClassMethod;
            mm.Selector = Selector;
            return mm;
        }
        
        public static void SaveMapping()
        {
            IDictionary pMaps = new Hashtable();
            ArrayList mMaps = new ArrayList();
            foreach(object val in NameMappings.Values) {
                if(val is PropertyMapping)
                    pMaps[((PropertyMapping)val).Name] = val;
                if(val is MethodMapping)
                    mMaps.Add(val);
            }
            
            Mappings toOutput = new Mappings();
            toOutput.Properties = (PropertyMapping[])new ArrayList(pMaps.Values).ToArray(typeof(PropertyMapping));
            toOutput.Methods = (MethodMapping[])mMaps.ToArray(typeof(MethodMapping));
            
            XmlSerializer _ser = new XmlSerializer(typeof(Mappings));
            StreamWriter _sw = new StreamWriter("generator/mapping.xml");
            _ser.Serialize(_sw, toOutput);
            _sw.Close();
        }
		private static string ReturnExpression(string declType,string glueType,string apiType,string expression)
		{
			if(declType == "SEL")
				return string.Format("return NSString.FromSEL({0}).ToString()", expression);
			if (apiType == "string" && declType.Replace("const ",string.Empty).Replace(" ",string.Empty) == "char*")
				return string.Format("return Marshal.PtrToStringAnsi({0})", expression);
			if(glueType != apiType)
				return string.Format("return ({0})NSObject.NS2Net({1})", apiType, expression);
			if (apiType == "void")
				return expression;
			return "return " + expression;
		}

		private static string ArgumentExpression(string declType,string glueType,string apiType,string expression)
		{
			if(declType == "SEL")
				return string.Format("NSString.NSSelector({0})", expression);
			if(glueType != apiType)
				return string.Format("NSObject.Net2NS({0})", expression);
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

			w.WriteLine("        public {0}({1}) : this() {{", name, string.Join(", ", mCSAPIParameters));
			w.WriteLine("            {0}({1});", mCSMethodName, string.Join(", ", (string[])args.ToArray(typeof(string))));
			w.WriteLine("        }");
		}
		#endregion

		#region -- C# Interface --
		public void CSInterfaceMethod(string name,IDictionary methods,bool propOnly,System.IO.TextWriter w)
		{
			if (mIsUnsupported || mIsClassMethod || mCSAPIDone)
				return;

			string _type = mReturnAPIType;

			if (_type == "void" && mArgumentDeclarationTypes.Length == 1 && mCSMethodName.StartsWith("set")) {
				string t = mArgumentAPITypes[0], propName;
				Method get = GetGetMethod(methods, out propName);
				bool hasGet = get != null && get.IsGetMethod(t);
				
				w.Write("        {0} {1} {{", t, propName);
				if (hasGet)
					w.Write(" get;");

				w.WriteLine(" set; }");
				mCSAPIDone = true;
				// Check to see if this selector is in our map
                if (!NameMappings.Contains(Selector))
                    NameMappings[Selector] = GeneratePropertyMapping(propName, get, this);
                if (hasGet && !NameMappings.Contains(get.Selector))
                    NameMappings[get.Selector] = GeneratePropertyMapping(propName, get, this);
				
				return;
			}
			
			if (propOnly)
				return;

			if (_type != "void" && mArgumentDeclarationTypes.Length == 0) {
				string t = _type, propName = mCSMethodName;
				
				propName = MakeCSMethodName(propName);

				w.WriteLine("        // {0}", mMethodDeclaration);
				w.WriteLine("        {0} {1} {{ get; }}", t, propName);

                if (!NameMappings.Contains(Selector))
                    NameMappings[Selector] = GeneratePropertyMapping(propName, this, null);
			    return;
			}

			ArrayList _params = new ArrayList();

			for(int i = 0; i < mArgumentDeclarationTypes.Length; ++i) 
			{
				string t = mArgumentAPITypes[i];
				_params.Add(t + " p" + i + "/*" + mArgumentNames[i] + "*/");
			}

			string paramsStr = string.Join(", ", (string[])_params.ToArray(typeof(string)));
			w.WriteLine("        // {0}", mMethodDeclaration);
			w.WriteLine("        {0} {1} ({2}); ", _type, mCSMethodName, paramsStr);

            if (!NameMappings.Contains(Selector))
                NameMappings[Selector] = GenerateMethodMapping();
		}
		#endregion

		#region -- Private Functions --
		private string MakeCSMethodName(string name)
		{
			if (mIsClassMethod)
				name = name.Substring(0,1).ToUpper() + name.Substring(1);
			else
				name = name.Substring(0,1).ToLower() + name.Substring(1);

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

		private static string ConvertTypeGlue(string type,bool arg) 
		{
			type = type.Replace("const ",string.Empty);
			{
				NativeData nd = (NativeData)Conversions[type];
				if(nd != null && nd.Glue != null)
					return arg ? (nd.GlueArg != null ? nd.GlueArg : nd.Glue) : nd.Glue;
			}

			if (sConversions.Regexs != null)
    			foreach (NativeData nd in sConversions.Regexs)
    				if(new Regex(nd.Native).IsMatch(type) && nd.Glue != null)
    					return arg ? (nd.GlueArg != null ? nd.GlueArg : nd.Glue) : nd.Glue;

            if (sConversions.Replaces != null)
    			foreach (ReplaceData rd in sConversions.Replaces)
    				if(rd.Type == "glue")
    					if(new Regex(rd.Regex).IsMatch(type))
    						return type.Replace(rd.ToReplace, rd.ReplaceWith).Trim();
			
			return type;
		}

		private static string ConvertType(string type,bool arg) 
		{
			type = type.Replace("const ",string.Empty);
			{
				NativeData nd = (NativeData)Conversions[type];
				if(nd != null && nd.Api != null)
					return nd.Api;
			}

			if (sConversions.Regexs != null)
    			foreach (NativeData nd in sConversions.Regexs)
    				if(new Regex(nd.Native).IsMatch(type) && nd.Api != null) {
    				    if (nd.Api == "{detect}") {
    				        string cls = type.Substring(0,type.Length-1).Replace(" ",string.Empty);
    				        if (cls.StartsWith("NS") && !cls.EndsWith("*"))
    				            return cls;
    				        else if (ObjCClassInspector.IsObjCClass(cls))
    				            return cls;
    				        return "IntPtr /*(" + type + ")*/";
    				    }
    					return nd.Api;
    				}

            if (sConversions.Replaces != null)
    			foreach (ReplaceData rd in sConversions.Replaces)
    				if(rd.Type == "api")
    					if(new Regex(rd.Regex).IsMatch(type))
    						return type.Replace(rd.ToReplace, rd.ReplaceWith).Trim();

			return type;
		}
		#endregion
	}
}

//	$Log: Method.cs,v $
//	Revision 1.39  2004/06/28 21:31:22  gnorton
//	Initial mapping support in the gen.
//
//	Revision 1.38  2004/06/28 19:20:38  gnorton
//	Added mapping classes
//	
//	Revision 1.37  2004/06/28 19:18:31  urs
//	Implement latest name bindings changes, and using objective-c reflection to see is a type is a OC class
//	
//	Revision 1.36  2004/06/25 22:30:07  urs
//	Add better logging
//	
//	Revision 1.35  2004/06/25 17:39:10  urs
//	Handle char* as argument and return value
//	
//	Revision 1.34  2004/06/25 02:49:14  gnorton
//	Sample 2 now runs.
//	
//	Revision 1.33  2004/06/24 20:09:24  urs
//	fix constructor gen
//	
//	Revision 1.32  2004/06/24 18:56:53  gnorton
//	AppKit compiles
//	Foundation compiles
//	Output setMethod() for protocols not just the property so Interfaces are met.
//	Ignore static protocol methods (.NET doesn't support static in interfaces).
//	Resolve compiler errors.
//	
//	Revision 1.31  2004/06/24 06:29:36  gnorton
//	Make foundation compile.
//	
//	Revision 1.30  2004/06/24 05:21:04  urs
//	Fix typo
//	
//	Revision 1.29  2004/06/24 05:00:38  urs
//	Unflattern C# API methods to reduce conflicts
//	Rename static methods to start with a capital letter (to reduce conflict with instance methods)
//	
//	Revision 1.28  2004/06/24 03:37:07  gnorton
//	Some performance increates on the dynamic type converter (convert the <type /> entries to a IDictionary to access an indexer; rather than foreaching)
//	
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
