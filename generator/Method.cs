using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace ObjCManagedExporter {

	public class Method 
	{
		private string mMethodDeclaration;
		private string mGlueMethodName;
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

			string methodType = match.Groups[0].Value;
			mReturnDeclarationType = match.Groups[1].Value;
			if (mReturnDeclarationType == string.Empty)
				mReturnDeclarationType = "id";
			string remainder = match.Groups[2].Value;

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
			} 
			else if(arg_rx.IsMatch(remainder)) 
			{
				// If there are arguments, parse them
				GroupCollection grps = arg_rx.Match(remainder).Groups;
				for (int i = 0; i < grps.Count; )
				{
					messageParts.Add(grps[i++].Value);
					string argType = grps[i++].Value;
					string argName = grps[i++].Value;

					if (argType == string.Empty)
						argType = "id";

					if (argName == string.Empty)
					{
						argName = argType;
						argType = "id";
					}
            
					argTypes.Add(argType);
					argNames.Add(argName);
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
			if (mIsClassMethod) mGlueMethodName += "_";
			mGlueMethodName += string.Join("_",mMessageParts);
		}
        
		public void ObjCMethod(string name,System.IO.TextWriter w)
		{
			ArrayList _message = new ArrayList();
			ArrayList _params = new ArrayList();

			if (mMessageParts.Length == 1 && mArgumentNames.Length == 0) 
				_message.Add(mMessageParts);
			else
			{
				for(int i = 0; i < mMessageParts.Length; ++i)
				{
					_params.Add(string.Format("{0} {1}",mArgumentNames[i],"p" + i));
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
				body = "\tif (!CLASS) CLASS = [$class class];\n";
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
			w.WriteLine(mReturnDeclarationType + " " + mGlueMethodName + "(" + paramsStr + ") {");
			w.WriteLine(body);
			w.WriteLine("\t" + retter + "[" + receiver + " " + message + "];");
			w.WriteLine("}");
		}
	}
}
