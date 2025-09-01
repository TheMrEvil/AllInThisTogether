using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;
using MS.Internal.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003C1 RID: 961
	internal class XsltCompileContext : XsltContext
	{
		// Token: 0x060026E7 RID: 9959 RVA: 0x000E8C79 File Offset: 0x000E6E79
		internal XsltCompileContext(InputScopeManager manager, Processor processor) : base(false)
		{
			this.manager = manager;
			this.processor = processor;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000E8C90 File Offset: 0x000E6E90
		internal XsltCompileContext() : base(false)
		{
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000E8C99 File Offset: 0x000E6E99
		internal void Recycle()
		{
			this.manager = null;
			this.processor = null;
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000E8CA9 File Offset: 0x000E6EA9
		internal void Reinitialize(InputScopeManager manager, Processor processor)
		{
			this.manager = manager;
			this.processor = processor;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000E8CB9 File Offset: 0x000E6EB9
		public override int CompareDocument(string baseUri, string nextbaseUri)
		{
			return string.Compare(baseUri, nextbaseUri, StringComparison.Ordinal);
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string DefaultNamespace
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000E8CC3 File Offset: 0x000E6EC3
		public override string LookupNamespace(string prefix)
		{
			return this.manager.ResolveXPathNamespace(prefix);
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000E8CD4 File Offset: 0x000E6ED4
		public override IXsltContextVariable ResolveVariable(string prefix, string name)
		{
			string ns = this.LookupNamespace(prefix);
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, ns);
			IXsltContextVariable xsltContextVariable = this.manager.VariableScope.ResolveVariable(xmlQualifiedName);
			if (xsltContextVariable == null)
			{
				throw XsltException.Create("The variable or parameter '{0}' is either not defined or it is out of scope.", new string[]
				{
					xmlQualifiedName.ToString()
				});
			}
			return xsltContextVariable;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000E8D24 File Offset: 0x000E6F24
		internal object EvaluateVariable(VariableAction variable)
		{
			object variableValue = this.processor.GetVariableValue(variable);
			if (variableValue == null && !variable.IsGlobal)
			{
				VariableAction variableAction = this.manager.VariableScope.ResolveGlobalVariable(variable.Name);
				if (variableAction != null)
				{
					variableValue = this.processor.GetVariableValue(variableAction);
				}
			}
			if (variableValue == null)
			{
				throw XsltException.Create("The variable or parameter '{0}' is either not defined or it is out of scope.", new string[]
				{
					variable.Name.ToString()
				});
			}
			return variableValue;
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x000E8D93 File Offset: 0x000E6F93
		public override bool Whitespace
		{
			get
			{
				return this.processor.Stylesheet.Whitespace;
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x000E8DA5 File Offset: 0x000E6FA5
		public override bool PreserveWhitespace(XPathNavigator node)
		{
			node = node.Clone();
			node.MoveToParent();
			return this.processor.Stylesheet.PreserveWhiteSpace(this.processor, node);
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000E8DD0 File Offset: 0x000E6FD0
		private MethodInfo FindBestMethod(MethodInfo[] methods, bool ignoreCase, bool publicOnly, string name, XPathResultType[] argTypes)
		{
			int num = methods.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(name, methods[i].Name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0 && (!publicOnly || methods[i].GetBaseDefinition().IsPublic))
				{
					methods[num2++] = methods[i];
				}
			}
			num = num2;
			if (num == 0)
			{
				return null;
			}
			if (argTypes == null)
			{
				return methods[0];
			}
			num2 = 0;
			for (int j = 0; j < num; j++)
			{
				if (methods[j].GetParameters().Length == argTypes.Length)
				{
					methods[num2++] = methods[j];
				}
			}
			num = num2;
			if (num <= 1)
			{
				return methods[0];
			}
			num2 = 0;
			for (int k = 0; k < num; k++)
			{
				bool flag = true;
				ParameterInfo[] parameters = methods[k].GetParameters();
				for (int l = 0; l < parameters.Length; l++)
				{
					XPathResultType xpathResultType = argTypes[l];
					if (xpathResultType != XPathResultType.Any)
					{
						XPathResultType xpathType = XsltCompileContext.GetXPathType(parameters[l].ParameterType);
						if (xpathType != xpathResultType && xpathType != XPathResultType.Any)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					methods[num2++] = methods[k];
				}
			}
			return methods[0];
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x000E8ED8 File Offset: 0x000E70D8
		private IXsltContextFunction GetExtentionMethod(string ns, string name, XPathResultType[] argTypes, out object extension)
		{
			XsltCompileContext.FuncExtension result = null;
			extension = this.processor.GetScriptObject(ns);
			if (extension != null)
			{
				MethodInfo methodInfo = this.FindBestMethod(extension.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), true, false, name, argTypes);
				if (methodInfo != null)
				{
					result = new XsltCompileContext.FuncExtension(extension, methodInfo, null);
				}
				return result;
			}
			extension = this.processor.GetExtensionObject(ns);
			if (extension != null)
			{
				MethodInfo methodInfo2 = this.FindBestMethod(extension.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), false, true, name, argTypes);
				if (methodInfo2 != null)
				{
					result = new XsltCompileContext.FuncExtension(extension, methodInfo2, this.processor.permissions);
				}
				return result;
			}
			return null;
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000E8F7C File Offset: 0x000E717C
		public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] argTypes)
		{
			IXsltContextFunction xsltContextFunction;
			if (prefix.Length == 0)
			{
				xsltContextFunction = (XsltCompileContext.s_FunctionTable[name] as IXsltContextFunction);
			}
			else
			{
				string text = this.LookupNamespace(prefix);
				if (text == "urn:schemas-microsoft-com:xslt" && name == "node-set")
				{
					xsltContextFunction = XsltCompileContext.s_FuncNodeSet;
				}
				else
				{
					object obj;
					xsltContextFunction = this.GetExtentionMethod(text, name, argTypes, out obj);
					if (obj == null)
					{
						throw XsltException.Create("Cannot find the script or external object that implements prefix '{0}'.", new string[]
						{
							prefix
						});
					}
				}
			}
			if (xsltContextFunction == null)
			{
				throw XsltException.Create("'{0}()' is an unknown XSLT function.", new string[]
				{
					name
				});
			}
			if (argTypes.Length < xsltContextFunction.Minargs || xsltContextFunction.Maxargs < argTypes.Length)
			{
				throw XsltException.Create("XSLT function '{0}()' has the wrong number of arguments.", new string[]
				{
					name,
					argTypes.Length.ToString(CultureInfo.InvariantCulture)
				});
			}
			return xsltContextFunction;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x000E904C File Offset: 0x000E724C
		private Uri ComposeUri(string thisUri, string baseUri)
		{
			XmlResolver resolver = this.processor.Resolver;
			Uri baseUri2 = null;
			if (baseUri.Length != 0)
			{
				baseUri2 = resolver.ResolveUri(null, baseUri);
			}
			return resolver.ResolveUri(baseUri2, thisUri);
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000E9080 File Offset: 0x000E7280
		private XPathNodeIterator Document(object arg0, string baseUri)
		{
			if (this.processor.permissions != null)
			{
				this.processor.permissions.PermitOnly();
			}
			XPathNodeIterator xpathNodeIterator = arg0 as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				ArrayList arrayList = new ArrayList();
				Hashtable hashtable = new Hashtable();
				while (xpathNodeIterator.MoveNext())
				{
					Uri uri = this.ComposeUri(xpathNodeIterator.Current.Value, baseUri ?? xpathNodeIterator.Current.BaseURI);
					if (!hashtable.ContainsKey(uri))
					{
						hashtable.Add(uri, null);
						arrayList.Add(this.processor.GetNavigator(uri));
					}
				}
				return new XPathArrayIterator(arrayList);
			}
			return new XPathSingletonIterator(this.processor.GetNavigator(this.ComposeUri(XmlConvert.ToXPathString(arg0), baseUri ?? this.manager.Navigator.BaseURI)));
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000E9148 File Offset: 0x000E7348
		private Hashtable BuildKeyTable(Key key, XPathNavigator root)
		{
			Hashtable hashtable = new Hashtable();
			string queryExpression = this.processor.GetQueryExpression(key.MatchKey);
			Query compiledQuery = this.processor.GetCompiledQuery(key.MatchKey);
			Query compiledQuery2 = this.processor.GetCompiledQuery(key.UseKey);
			XPathNodeIterator xpathNodeIterator = root.SelectDescendants(XPathNodeType.All, false);
			while (xpathNodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathNodeIterator.Current;
				XsltCompileContext.EvaluateKey(xpathNavigator, compiledQuery, queryExpression, compiledQuery2, hashtable);
				if (xpathNavigator.MoveToFirstAttribute())
				{
					do
					{
						XsltCompileContext.EvaluateKey(xpathNavigator, compiledQuery, queryExpression, compiledQuery2, hashtable);
					}
					while (xpathNavigator.MoveToNextAttribute());
					xpathNavigator.MoveToParent();
				}
			}
			return hashtable;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000E91E4 File Offset: 0x000E73E4
		private static void AddKeyValue(Hashtable keyTable, string key, XPathNavigator value, bool checkDuplicates)
		{
			ArrayList arrayList = (ArrayList)keyTable[key];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				keyTable.Add(key, arrayList);
			}
			else if (checkDuplicates && value.ComparePosition((XPathNavigator)arrayList[arrayList.Count - 1]) == XmlNodeOrder.Same)
			{
				return;
			}
			arrayList.Add(value.Clone());
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000E9240 File Offset: 0x000E7440
		private static void EvaluateKey(XPathNavigator node, Query matchExpr, string matchStr, Query useExpr, Hashtable keyTable)
		{
			try
			{
				if (matchExpr.MatchNode(node) == null)
				{
					return;
				}
			}
			catch (XPathException)
			{
				throw XsltException.Create("'{0}' is an invalid XSLT pattern.", new string[]
				{
					matchStr
				});
			}
			object obj = useExpr.Evaluate(new XPathSingletonIterator(node, true));
			XPathNodeIterator xpathNodeIterator = obj as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				bool checkDuplicates = false;
				while (xpathNodeIterator.MoveNext())
				{
					XPathNavigator xpathNavigator = xpathNodeIterator.Current;
					XsltCompileContext.AddKeyValue(keyTable, xpathNavigator.Value, node, checkDuplicates);
					checkDuplicates = true;
				}
				return;
			}
			string key = XmlConvert.ToXPathString(obj);
			XsltCompileContext.AddKeyValue(keyTable, key, node, false);
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000E92CC File Offset: 0x000E74CC
		private DecimalFormat ResolveFormatName(string formatName)
		{
			string ns = string.Empty;
			string empty = string.Empty;
			if (formatName != null)
			{
				string prefix;
				PrefixQName.ParseQualifiedName(formatName, out prefix, out empty);
				ns = this.LookupNamespace(prefix);
			}
			DecimalFormat decimalFormat = this.processor.RootAction.GetDecimalFormat(new XmlQualifiedName(empty, ns));
			if (decimalFormat == null)
			{
				if (formatName != null)
				{
					throw XsltException.Create("Decimal format '{0}' is not defined.", new string[]
					{
						formatName
					});
				}
				decimalFormat = new DecimalFormat(new NumberFormatInfo(), '#', '0', ';');
			}
			return decimalFormat;
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000E9340 File Offset: 0x000E7540
		private bool ElementAvailable(string qname)
		{
			string prefix;
			string a;
			PrefixQName.ParseQualifiedName(qname, out prefix, out a);
			return this.manager.ResolveXmlNamespace(prefix) == "http://www.w3.org/1999/XSL/Transform" && (a == "apply-imports" || a == "apply-templates" || a == "attribute" || a == "call-template" || a == "choose" || a == "comment" || a == "copy" || a == "copy-of" || a == "element" || a == "fallback" || a == "for-each" || a == "if" || a == "message" || a == "number" || a == "processing-instruction" || a == "text" || a == "value-of" || a == "variable");
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000E9478 File Offset: 0x000E7678
		private bool FunctionAvailable(string qname)
		{
			string prefix;
			string text;
			PrefixQName.ParseQualifiedName(qname, out prefix, out text);
			string text2 = this.LookupNamespace(prefix);
			if (text2 == "urn:schemas-microsoft-com:xslt")
			{
				return text == "node-set";
			}
			if (text2.Length == 0)
			{
				return text == "last" || text == "position" || text == "name" || text == "namespace-uri" || text == "local-name" || text == "count" || text == "id" || text == "string" || text == "concat" || text == "starts-with" || text == "contains" || text == "substring-before" || text == "substring-after" || text == "substring" || text == "string-length" || text == "normalize-space" || text == "translate" || text == "boolean" || text == "not" || text == "true" || text == "false" || text == "lang" || text == "number" || text == "sum" || text == "floor" || text == "ceiling" || text == "round" || (XsltCompileContext.s_FunctionTable[text] != null && text != "unparsed-entity-uri");
			}
			object obj;
			return this.GetExtentionMethod(text2, text, null, out obj) != null;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000E9680 File Offset: 0x000E7880
		private XPathNodeIterator Current()
		{
			XPathNavigator xpathNavigator = this.processor.Current;
			if (xpathNavigator != null)
			{
				return new XPathSingletonIterator(xpathNavigator.Clone());
			}
			return XPathEmptyIterator.Instance;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000E96B0 File Offset: 0x000E78B0
		private string SystemProperty(string qname)
		{
			string result = string.Empty;
			string text;
			string a;
			PrefixQName.ParseQualifiedName(qname, out text, out a);
			string text2 = this.LookupNamespace(text);
			if (text2 == "http://www.w3.org/1999/XSL/Transform")
			{
				if (a == "version")
				{
					result = "1";
				}
				else if (a == "vendor")
				{
					result = "Microsoft";
				}
				else if (a == "vendor-url")
				{
					result = "http://www.microsoft.com";
				}
				return result;
			}
			if (text2 == null && text != null)
			{
				throw XsltException.Create("Prefix '{0}' is not defined.", new string[]
				{
					text
				});
			}
			return string.Empty;
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000E9744 File Offset: 0x000E7944
		public static XPathResultType GetXPathType(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			if (typeCode <= TypeCode.Boolean)
			{
				if (typeCode != TypeCode.Object)
				{
					if (typeCode == TypeCode.Boolean)
					{
						return XPathResultType.Boolean;
					}
				}
				else
				{
					if (typeof(XPathNavigator).IsAssignableFrom(type) || typeof(IXPathNavigable).IsAssignableFrom(type))
					{
						return XPathResultType.String;
					}
					if (typeof(XPathNodeIterator).IsAssignableFrom(type))
					{
						return XPathResultType.NodeSet;
					}
					return XPathResultType.Any;
				}
			}
			else
			{
				if (typeCode == TypeCode.DateTime)
				{
					return XPathResultType.Error;
				}
				if (typeCode == TypeCode.String)
				{
					return XPathResultType.String;
				}
			}
			return XPathResultType.Number;
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000E97B4 File Offset: 0x000E79B4
		private static Hashtable CreateFunctionTable()
		{
			Hashtable hashtable = new Hashtable(10);
			hashtable["current"] = new XsltCompileContext.FuncCurrent();
			hashtable["unparsed-entity-uri"] = new XsltCompileContext.FuncUnEntityUri();
			hashtable["generate-id"] = new XsltCompileContext.FuncGenerateId();
			hashtable["system-property"] = new XsltCompileContext.FuncSystemProp();
			hashtable["element-available"] = new XsltCompileContext.FuncElementAvailable();
			hashtable["function-available"] = new XsltCompileContext.FuncFunctionAvailable();
			hashtable["document"] = new XsltCompileContext.FuncDocument();
			hashtable["key"] = new XsltCompileContext.FuncKey();
			hashtable["format-number"] = new XsltCompileContext.FuncFormatNumber();
			return hashtable;
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000E9858 File Offset: 0x000E7A58
		// Note: this type is marked as 'beforefieldinit'.
		static XsltCompileContext()
		{
		}

		// Token: 0x04001E89 RID: 7817
		private InputScopeManager manager;

		// Token: 0x04001E8A RID: 7818
		private Processor processor;

		// Token: 0x04001E8B RID: 7819
		private static Hashtable s_FunctionTable = XsltCompileContext.CreateFunctionTable();

		// Token: 0x04001E8C RID: 7820
		private static IXsltContextFunction s_FuncNodeSet = new XsltCompileContext.FuncNodeSet();

		// Token: 0x04001E8D RID: 7821
		private const string f_NodeSet = "node-set";

		// Token: 0x04001E8E RID: 7822
		private const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x020003C2 RID: 962
		private abstract class XsltFunctionImpl : IXsltContextFunction
		{
			// Token: 0x06002702 RID: 9986 RVA: 0x0000216B File Offset: 0x0000036B
			public XsltFunctionImpl()
			{
			}

			// Token: 0x06002703 RID: 9987 RVA: 0x000E986E File Offset: 0x000E7A6E
			public XsltFunctionImpl(int minArgs, int maxArgs, XPathResultType returnType, XPathResultType[] argTypes)
			{
				this.Init(minArgs, maxArgs, returnType, argTypes);
			}

			// Token: 0x06002704 RID: 9988 RVA: 0x000E9881 File Offset: 0x000E7A81
			protected void Init(int minArgs, int maxArgs, XPathResultType returnType, XPathResultType[] argTypes)
			{
				this.minargs = minArgs;
				this.maxargs = maxArgs;
				this.returnType = returnType;
				this.argTypes = argTypes;
			}

			// Token: 0x170007AF RID: 1967
			// (get) Token: 0x06002705 RID: 9989 RVA: 0x000E98A0 File Offset: 0x000E7AA0
			public int Minargs
			{
				get
				{
					return this.minargs;
				}
			}

			// Token: 0x170007B0 RID: 1968
			// (get) Token: 0x06002706 RID: 9990 RVA: 0x000E98A8 File Offset: 0x000E7AA8
			public int Maxargs
			{
				get
				{
					return this.maxargs;
				}
			}

			// Token: 0x170007B1 RID: 1969
			// (get) Token: 0x06002707 RID: 9991 RVA: 0x000E98B0 File Offset: 0x000E7AB0
			public XPathResultType ReturnType
			{
				get
				{
					return this.returnType;
				}
			}

			// Token: 0x170007B2 RID: 1970
			// (get) Token: 0x06002708 RID: 9992 RVA: 0x000E98B8 File Offset: 0x000E7AB8
			public XPathResultType[] ArgTypes
			{
				get
				{
					return this.argTypes;
				}
			}

			// Token: 0x06002709 RID: 9993
			public abstract object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext);

			// Token: 0x0600270A RID: 9994 RVA: 0x000E98C0 File Offset: 0x000E7AC0
			public static XPathNodeIterator ToIterator(object argument)
			{
				XPathNodeIterator xpathNodeIterator = argument as XPathNodeIterator;
				if (xpathNodeIterator == null)
				{
					throw XsltException.Create("Cannot convert the operand to a node-set.", Array.Empty<string>());
				}
				return xpathNodeIterator;
			}

			// Token: 0x0600270B RID: 9995 RVA: 0x000E98DB File Offset: 0x000E7ADB
			public static XPathNavigator ToNavigator(object argument)
			{
				XPathNavigator xpathNavigator = argument as XPathNavigator;
				if (xpathNavigator == null)
				{
					throw XsltException.Create("Cannot convert the operand to 'Result tree fragment'.", Array.Empty<string>());
				}
				return xpathNavigator;
			}

			// Token: 0x0600270C RID: 9996 RVA: 0x000E98F6 File Offset: 0x000E7AF6
			private static string IteratorToString(XPathNodeIterator it)
			{
				if (it.MoveNext())
				{
					return it.Current.Value;
				}
				return string.Empty;
			}

			// Token: 0x0600270D RID: 9997 RVA: 0x000E9914 File Offset: 0x000E7B14
			public static string ToString(object argument)
			{
				XPathNodeIterator xpathNodeIterator = argument as XPathNodeIterator;
				if (xpathNodeIterator != null)
				{
					return XsltCompileContext.XsltFunctionImpl.IteratorToString(xpathNodeIterator);
				}
				return XmlConvert.ToXPathString(argument);
			}

			// Token: 0x0600270E RID: 9998 RVA: 0x000E9938 File Offset: 0x000E7B38
			public static bool ToBoolean(object argument)
			{
				XPathNodeIterator xpathNodeIterator = argument as XPathNodeIterator;
				if (xpathNodeIterator != null)
				{
					return Convert.ToBoolean(XsltCompileContext.XsltFunctionImpl.IteratorToString(xpathNodeIterator), CultureInfo.InvariantCulture);
				}
				XPathNavigator xpathNavigator = argument as XPathNavigator;
				if (xpathNavigator != null)
				{
					return Convert.ToBoolean(xpathNavigator.ToString(), CultureInfo.InvariantCulture);
				}
				return Convert.ToBoolean(argument, CultureInfo.InvariantCulture);
			}

			// Token: 0x0600270F RID: 9999 RVA: 0x000E9988 File Offset: 0x000E7B88
			public static double ToNumber(object argument)
			{
				XPathNodeIterator xpathNodeIterator = argument as XPathNodeIterator;
				if (xpathNodeIterator != null)
				{
					return XmlConvert.ToXPathDouble(XsltCompileContext.XsltFunctionImpl.IteratorToString(xpathNodeIterator));
				}
				XPathNavigator xpathNavigator = argument as XPathNavigator;
				if (xpathNavigator != null)
				{
					return XmlConvert.ToXPathDouble(xpathNavigator.ToString());
				}
				return XmlConvert.ToXPathDouble(argument);
			}

			// Token: 0x06002710 RID: 10000 RVA: 0x000E99C7 File Offset: 0x000E7BC7
			private static object ToNumeric(object argument, TypeCode typeCode)
			{
				return Convert.ChangeType(XsltCompileContext.XsltFunctionImpl.ToNumber(argument), typeCode, CultureInfo.InvariantCulture);
			}

			// Token: 0x06002711 RID: 10001 RVA: 0x000E99E0 File Offset: 0x000E7BE0
			public static object ConvertToXPathType(object val, XPathResultType xt, TypeCode typeCode)
			{
				switch (xt)
				{
				case XPathResultType.Number:
					return XsltCompileContext.XsltFunctionImpl.ToNumeric(val, typeCode);
				case XPathResultType.String:
					if (typeCode == TypeCode.String)
					{
						return XsltCompileContext.XsltFunctionImpl.ToString(val);
					}
					return XsltCompileContext.XsltFunctionImpl.ToNavigator(val);
				case XPathResultType.Boolean:
					return XsltCompileContext.XsltFunctionImpl.ToBoolean(val);
				case XPathResultType.NodeSet:
					return XsltCompileContext.XsltFunctionImpl.ToIterator(val);
				case XPathResultType.Any:
				case XPathResultType.Error:
					return val;
				}
				return val;
			}

			// Token: 0x04001E8F RID: 7823
			private int minargs;

			// Token: 0x04001E90 RID: 7824
			private int maxargs;

			// Token: 0x04001E91 RID: 7825
			private XPathResultType returnType;

			// Token: 0x04001E92 RID: 7826
			private XPathResultType[] argTypes;
		}

		// Token: 0x020003C3 RID: 963
		private class FuncCurrent : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06002712 RID: 10002 RVA: 0x000E9A42 File Offset: 0x000E7C42
			public FuncCurrent() : base(0, 0, XPathResultType.NodeSet, new XPathResultType[0])
			{
			}

			// Token: 0x06002713 RID: 10003 RVA: 0x000E9A53 File Offset: 0x000E7C53
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return ((XsltCompileContext)xsltContext).Current();
			}
		}

		// Token: 0x020003C4 RID: 964
		private class FuncUnEntityUri : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06002714 RID: 10004 RVA: 0x000E9A60 File Offset: 0x000E7C60
			public FuncUnEntityUri() : base(1, 1, XPathResultType.String, new XPathResultType[]
			{
				XPathResultType.String
			})
			{
			}

			// Token: 0x06002715 RID: 10005 RVA: 0x000E9A75 File Offset: 0x000E7C75
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				throw XsltException.Create("'{0}()' is an unsupported XSLT function.", new string[]
				{
					"unparsed-entity-uri"
				});
			}
		}

		// Token: 0x020003C5 RID: 965
		private class FuncGenerateId : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06002716 RID: 10006 RVA: 0x000E9A8F File Offset: 0x000E7C8F
			public FuncGenerateId() : base(0, 1, XPathResultType.String, new XPathResultType[]
			{
				XPathResultType.NodeSet
			})
			{
			}

			// Token: 0x06002717 RID: 10007 RVA: 0x000E9AA4 File Offset: 0x000E7CA4
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				if (args.Length == 0)
				{
					return docContext.UniqueId;
				}
				XPathNodeIterator xpathNodeIterator = XsltCompileContext.XsltFunctionImpl.ToIterator(args[0]);
				if (xpathNodeIterator.MoveNext())
				{
					return xpathNodeIterator.Current.UniqueId;
				}
				return string.Empty;
			}
		}

		// Token: 0x020003C6 RID: 966
		private class FuncSystemProp : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06002718 RID: 10008 RVA: 0x000E9A60 File Offset: 0x000E7C60
			public FuncSystemProp() : base(1, 1, XPathResultType.String, new XPathResultType[]
			{
				XPathResultType.String
			})
			{
			}

			// Token: 0x06002719 RID: 10009 RVA: 0x000E9ADE File Offset: 0x000E7CDE
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return ((XsltCompileContext)xsltContext).SystemProperty(XsltCompileContext.XsltFunctionImpl.ToString(args[0]));
			}
		}

		// Token: 0x020003C7 RID: 967
		private class FuncElementAvailable : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x0600271A RID: 10010 RVA: 0x000E9AF3 File Offset: 0x000E7CF3
			public FuncElementAvailable() : base(1, 1, XPathResultType.Boolean, new XPathResultType[]
			{
				XPathResultType.String
			})
			{
			}

			// Token: 0x0600271B RID: 10011 RVA: 0x000E9B08 File Offset: 0x000E7D08
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return ((XsltCompileContext)xsltContext).ElementAvailable(XsltCompileContext.XsltFunctionImpl.ToString(args[0]));
			}
		}

		// Token: 0x020003C8 RID: 968
		private class FuncFunctionAvailable : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x0600271C RID: 10012 RVA: 0x000E9AF3 File Offset: 0x000E7CF3
			public FuncFunctionAvailable() : base(1, 1, XPathResultType.Boolean, new XPathResultType[]
			{
				XPathResultType.String
			})
			{
			}

			// Token: 0x0600271D RID: 10013 RVA: 0x000E9B22 File Offset: 0x000E7D22
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return ((XsltCompileContext)xsltContext).FunctionAvailable(XsltCompileContext.XsltFunctionImpl.ToString(args[0]));
			}
		}

		// Token: 0x020003C9 RID: 969
		private class FuncDocument : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x0600271E RID: 10014 RVA: 0x000E9B3C File Offset: 0x000E7D3C
			public FuncDocument() : base(1, 2, XPathResultType.NodeSet, new XPathResultType[]
			{
				XPathResultType.Any,
				XPathResultType.NodeSet
			})
			{
			}

			// Token: 0x0600271F RID: 10015 RVA: 0x000E9B58 File Offset: 0x000E7D58
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				string baseUri = null;
				if (args.Length == 2)
				{
					XPathNodeIterator xpathNodeIterator = XsltCompileContext.XsltFunctionImpl.ToIterator(args[1]);
					if (xpathNodeIterator.MoveNext())
					{
						baseUri = xpathNodeIterator.Current.BaseURI;
					}
					else
					{
						baseUri = string.Empty;
					}
				}
				object result;
				try
				{
					result = ((XsltCompileContext)xsltContext).Document(args[0], baseUri);
				}
				catch (Exception e)
				{
					if (!XmlException.IsCatchableException(e))
					{
						throw;
					}
					result = XPathEmptyIterator.Instance;
				}
				return result;
			}
		}

		// Token: 0x020003CA RID: 970
		private class FuncKey : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06002720 RID: 10016 RVA: 0x000E9BC8 File Offset: 0x000E7DC8
			public FuncKey() : base(2, 2, XPathResultType.NodeSet, new XPathResultType[]
			{
				XPathResultType.String,
				XPathResultType.Any
			})
			{
			}

			// Token: 0x06002721 RID: 10017 RVA: 0x000E9BE4 File Offset: 0x000E7DE4
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				XsltCompileContext xsltCompileContext = (XsltCompileContext)xsltContext;
				string prefix;
				string name;
				PrefixQName.ParseQualifiedName(XsltCompileContext.XsltFunctionImpl.ToString(args[0]), out prefix, out name);
				string ns = xsltContext.LookupNamespace(prefix);
				XmlQualifiedName b = new XmlQualifiedName(name, ns);
				XPathNavigator xpathNavigator = docContext.Clone();
				xpathNavigator.MoveToRoot();
				ArrayList arrayList = null;
				foreach (Key key in xsltCompileContext.processor.KeyList)
				{
					if (key.Name == b)
					{
						Hashtable hashtable = key.GetKeys(xpathNavigator);
						if (hashtable == null)
						{
							hashtable = xsltCompileContext.BuildKeyTable(key, xpathNavigator);
							key.AddKey(xpathNavigator, hashtable);
						}
						XPathNodeIterator xpathNodeIterator = args[1] as XPathNodeIterator;
						if (xpathNodeIterator != null)
						{
							xpathNodeIterator = xpathNodeIterator.Clone();
							while (xpathNodeIterator.MoveNext())
							{
								XPathNavigator xpathNavigator2 = xpathNodeIterator.Current;
								arrayList = XsltCompileContext.FuncKey.AddToList(arrayList, (ArrayList)hashtable[xpathNavigator2.Value]);
							}
						}
						else
						{
							arrayList = XsltCompileContext.FuncKey.AddToList(arrayList, (ArrayList)hashtable[XsltCompileContext.XsltFunctionImpl.ToString(args[1])]);
						}
					}
				}
				if (arrayList == null)
				{
					return XPathEmptyIterator.Instance;
				}
				if (arrayList[0] is XPathNavigator)
				{
					return new XPathArrayIterator(arrayList);
				}
				return new XPathMultyIterator(arrayList);
			}

			// Token: 0x06002722 RID: 10018 RVA: 0x000E9D1C File Offset: 0x000E7F1C
			private static ArrayList AddToList(ArrayList resultCollection, ArrayList newList)
			{
				if (newList == null)
				{
					return resultCollection;
				}
				if (resultCollection == null)
				{
					return newList;
				}
				if (!(resultCollection[0] is ArrayList))
				{
					ArrayList value = resultCollection;
					resultCollection = new ArrayList();
					resultCollection.Add(value);
				}
				resultCollection.Add(newList);
				return resultCollection;
			}
		}

		// Token: 0x020003CB RID: 971
		private class FuncFormatNumber : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06002723 RID: 10019 RVA: 0x000E9D5B File Offset: 0x000E7F5B
			public FuncFormatNumber() : base(2, 3, XPathResultType.String, new XPathResultType[]
			{
				XPathResultType.Number,
				XPathResultType.String,
				XPathResultType.String
			})
			{
			}

			// Token: 0x06002724 RID: 10020 RVA: 0x000E9D74 File Offset: 0x000E7F74
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				DecimalFormat decimalFormat = ((XsltCompileContext)xsltContext).ResolveFormatName((args.Length == 3) ? XsltCompileContext.XsltFunctionImpl.ToString(args[2]) : null);
				return DecimalFormatter.Format(XsltCompileContext.XsltFunctionImpl.ToNumber(args[0]), XsltCompileContext.XsltFunctionImpl.ToString(args[1]), decimalFormat);
			}
		}

		// Token: 0x020003CC RID: 972
		private class FuncNodeSet : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06002725 RID: 10021 RVA: 0x000E9DB4 File Offset: 0x000E7FB4
			public FuncNodeSet() : base(1, 1, XPathResultType.NodeSet, new XPathResultType[]
			{
				XPathResultType.String
			})
			{
			}

			// Token: 0x06002726 RID: 10022 RVA: 0x000E9DC9 File Offset: 0x000E7FC9
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				return new XPathSingletonIterator(XsltCompileContext.XsltFunctionImpl.ToNavigator(args[0]));
			}
		}

		// Token: 0x020003CD RID: 973
		private class FuncExtension : XsltCompileContext.XsltFunctionImpl
		{
			// Token: 0x06002727 RID: 10023 RVA: 0x000E9DD8 File Offset: 0x000E7FD8
			public FuncExtension(object extension, MethodInfo method, PermissionSet permissions)
			{
				this.extension = extension;
				this.method = method;
				this.permissions = permissions;
				XPathResultType xpathType = XsltCompileContext.GetXPathType(method.ReturnType);
				ParameterInfo[] parameters = method.GetParameters();
				int num = parameters.Length;
				int maxArgs = parameters.Length;
				this.typeCodes = new TypeCode[parameters.Length];
				XPathResultType[] array = new XPathResultType[parameters.Length];
				bool flag = true;
				int num2 = parameters.Length - 1;
				while (0 <= num2)
				{
					this.typeCodes[num2] = Type.GetTypeCode(parameters[num2].ParameterType);
					array[num2] = XsltCompileContext.GetXPathType(parameters[num2].ParameterType);
					if (flag)
					{
						if (parameters[num2].IsOptional)
						{
							num--;
						}
						else
						{
							flag = false;
						}
					}
					num2--;
				}
				base.Init(num, maxArgs, xpathType, array);
			}

			// Token: 0x06002728 RID: 10024 RVA: 0x000E9E98 File Offset: 0x000E8098
			public override object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
			{
				int num = args.Length - 1;
				while (0 <= num)
				{
					args[num] = XsltCompileContext.XsltFunctionImpl.ConvertToXPathType(args[num], base.ArgTypes[num], this.typeCodes[num]);
					num--;
				}
				if (this.permissions != null)
				{
					this.permissions.PermitOnly();
				}
				return this.method.Invoke(this.extension, args);
			}

			// Token: 0x04001E93 RID: 7827
			private object extension;

			// Token: 0x04001E94 RID: 7828
			private MethodInfo method;

			// Token: 0x04001E95 RID: 7829
			private TypeCode[] typeCodes;

			// Token: 0x04001E96 RID: 7830
			private PermissionSet permissions;
		}
	}
}
