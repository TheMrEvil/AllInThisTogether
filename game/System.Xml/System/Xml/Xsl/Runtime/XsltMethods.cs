using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000493 RID: 1171
	internal static class XsltMethods
	{
		// Token: 0x06002DB9 RID: 11705 RVA: 0x0010B4BC File Offset: 0x001096BC
		public static MethodInfo GetMethod(Type className, string methName)
		{
			return className.GetMethod(methName);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x0010B4C5 File Offset: 0x001096C5
		public static MethodInfo GetMethod(Type className, string methName, params Type[] args)
		{
			return className.GetMethod(methName, args);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x0010B4D0 File Offset: 0x001096D0
		// Note: this type is marked as 'beforefieldinit'.
		static XsltMethods()
		{
		}

		// Token: 0x0400235F RID: 9055
		public static readonly MethodInfo FormatMessage = XsltMethods.GetMethod(typeof(XsltLibrary), "FormatMessage");

		// Token: 0x04002360 RID: 9056
		public static readonly MethodInfo EnsureNodeSet = XsltMethods.GetMethod(typeof(XsltConvert), "EnsureNodeSet", new Type[]
		{
			typeof(IList<XPathItem>)
		});

		// Token: 0x04002361 RID: 9057
		public static readonly MethodInfo EqualityOperator = XsltMethods.GetMethod(typeof(XsltLibrary), "EqualityOperator");

		// Token: 0x04002362 RID: 9058
		public static readonly MethodInfo RelationalOperator = XsltMethods.GetMethod(typeof(XsltLibrary), "RelationalOperator");

		// Token: 0x04002363 RID: 9059
		public static readonly MethodInfo StartsWith = XsltMethods.GetMethod(typeof(XsltFunctions), "StartsWith");

		// Token: 0x04002364 RID: 9060
		public static readonly MethodInfo Contains = XsltMethods.GetMethod(typeof(XsltFunctions), "Contains");

		// Token: 0x04002365 RID: 9061
		public static readonly MethodInfo SubstringBefore = XsltMethods.GetMethod(typeof(XsltFunctions), "SubstringBefore");

		// Token: 0x04002366 RID: 9062
		public static readonly MethodInfo SubstringAfter = XsltMethods.GetMethod(typeof(XsltFunctions), "SubstringAfter");

		// Token: 0x04002367 RID: 9063
		public static readonly MethodInfo Substring2 = XsltMethods.GetMethod(typeof(XsltFunctions), "Substring", new Type[]
		{
			typeof(string),
			typeof(double)
		});

		// Token: 0x04002368 RID: 9064
		public static readonly MethodInfo Substring3 = XsltMethods.GetMethod(typeof(XsltFunctions), "Substring", new Type[]
		{
			typeof(string),
			typeof(double),
			typeof(double)
		});

		// Token: 0x04002369 RID: 9065
		public static readonly MethodInfo NormalizeSpace = XsltMethods.GetMethod(typeof(XsltFunctions), "NormalizeSpace");

		// Token: 0x0400236A RID: 9066
		public static readonly MethodInfo Translate = XsltMethods.GetMethod(typeof(XsltFunctions), "Translate");

		// Token: 0x0400236B RID: 9067
		public static readonly MethodInfo Lang = XsltMethods.GetMethod(typeof(XsltFunctions), "Lang");

		// Token: 0x0400236C RID: 9068
		public static readonly MethodInfo Floor = XsltMethods.GetMethod(typeof(Math), "Floor", new Type[]
		{
			typeof(double)
		});

		// Token: 0x0400236D RID: 9069
		public static readonly MethodInfo Ceiling = XsltMethods.GetMethod(typeof(Math), "Ceiling", new Type[]
		{
			typeof(double)
		});

		// Token: 0x0400236E RID: 9070
		public static readonly MethodInfo Round = XsltMethods.GetMethod(typeof(XsltFunctions), "Round");

		// Token: 0x0400236F RID: 9071
		public static readonly MethodInfo SystemProperty = XsltMethods.GetMethod(typeof(XsltFunctions), "SystemProperty");

		// Token: 0x04002370 RID: 9072
		public static readonly MethodInfo BaseUri = XsltMethods.GetMethod(typeof(XsltFunctions), "BaseUri");

		// Token: 0x04002371 RID: 9073
		public static readonly MethodInfo OuterXml = XsltMethods.GetMethod(typeof(XsltFunctions), "OuterXml");

		// Token: 0x04002372 RID: 9074
		public static readonly MethodInfo OnCurrentNodeChanged = XsltMethods.GetMethod(typeof(XmlQueryRuntime), "OnCurrentNodeChanged");

		// Token: 0x04002373 RID: 9075
		public static readonly MethodInfo MSFormatDateTime = XsltMethods.GetMethod(typeof(XsltFunctions), "MSFormatDateTime");

		// Token: 0x04002374 RID: 9076
		public static readonly MethodInfo MSStringCompare = XsltMethods.GetMethod(typeof(XsltFunctions), "MSStringCompare");

		// Token: 0x04002375 RID: 9077
		public static readonly MethodInfo MSUtc = XsltMethods.GetMethod(typeof(XsltFunctions), "MSUtc");

		// Token: 0x04002376 RID: 9078
		public static readonly MethodInfo MSNumber = XsltMethods.GetMethod(typeof(XsltFunctions), "MSNumber");

		// Token: 0x04002377 RID: 9079
		public static readonly MethodInfo MSLocalName = XsltMethods.GetMethod(typeof(XsltFunctions), "MSLocalName");

		// Token: 0x04002378 RID: 9080
		public static readonly MethodInfo MSNamespaceUri = XsltMethods.GetMethod(typeof(XsltFunctions), "MSNamespaceUri");

		// Token: 0x04002379 RID: 9081
		public static readonly MethodInfo EXslObjectType = XsltMethods.GetMethod(typeof(XsltFunctions), "EXslObjectType");

		// Token: 0x0400237A RID: 9082
		public static readonly MethodInfo CheckScriptNamespace = XsltMethods.GetMethod(typeof(XsltLibrary), "CheckScriptNamespace");

		// Token: 0x0400237B RID: 9083
		public static readonly MethodInfo FunctionAvailable = XsltMethods.GetMethod(typeof(XsltLibrary), "FunctionAvailable");

		// Token: 0x0400237C RID: 9084
		public static readonly MethodInfo ElementAvailable = XsltMethods.GetMethod(typeof(XsltLibrary), "ElementAvailable");

		// Token: 0x0400237D RID: 9085
		public static readonly MethodInfo RegisterDecimalFormat = XsltMethods.GetMethod(typeof(XsltLibrary), "RegisterDecimalFormat");

		// Token: 0x0400237E RID: 9086
		public static readonly MethodInfo RegisterDecimalFormatter = XsltMethods.GetMethod(typeof(XsltLibrary), "RegisterDecimalFormatter");

		// Token: 0x0400237F RID: 9087
		public static readonly MethodInfo FormatNumberStatic = XsltMethods.GetMethod(typeof(XsltLibrary), "FormatNumberStatic");

		// Token: 0x04002380 RID: 9088
		public static readonly MethodInfo FormatNumberDynamic = XsltMethods.GetMethod(typeof(XsltLibrary), "FormatNumberDynamic");

		// Token: 0x04002381 RID: 9089
		public static readonly MethodInfo IsSameNodeSort = XsltMethods.GetMethod(typeof(XsltLibrary), "IsSameNodeSort");

		// Token: 0x04002382 RID: 9090
		public static readonly MethodInfo LangToLcid = XsltMethods.GetMethod(typeof(XsltLibrary), "LangToLcid");

		// Token: 0x04002383 RID: 9091
		public static readonly MethodInfo NumberFormat = XsltMethods.GetMethod(typeof(XsltLibrary), "NumberFormat");
	}
}
