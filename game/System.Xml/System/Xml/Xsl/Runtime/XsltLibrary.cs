using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl.Xslt;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000494 RID: 1172
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XsltLibrary
	{
		// Token: 0x06002DBC RID: 11708 RVA: 0x0010B900 File Offset: 0x00109B00
		internal XsltLibrary(XmlQueryRuntime runtime)
		{
			this.runtime = runtime;
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x0010B910 File Offset: 0x00109B10
		public string FormatMessage(string res, IList<string> args)
		{
			string[] array = new string[args.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = args[i];
			}
			return XslTransformException.CreateMessage(res, array);
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x0010B948 File Offset: 0x00109B48
		public int CheckScriptNamespace(string nsUri)
		{
			if (this.runtime.ExternalContext.GetLateBoundObject(nsUri) != null)
			{
				throw new XslTransformException("Cannot have both an extension object and a script implementing the same namespace '{0}'.", new string[]
				{
					nsUri
				});
			}
			return 0;
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x0010B973 File Offset: 0x00109B73
		public bool ElementAvailable(XmlQualifiedName name)
		{
			return QilGenerator.IsElementAvailable(name);
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x0010B97C File Offset: 0x00109B7C
		public bool FunctionAvailable(XmlQualifiedName name)
		{
			if (this.functionsAvail == null)
			{
				this.functionsAvail = new HybridDictionary();
			}
			else
			{
				object obj = this.functionsAvail[name];
				if (obj != null)
				{
					return (bool)obj;
				}
			}
			bool flag = this.FunctionAvailableHelper(name);
			this.functionsAvail[name] = flag;
			return flag;
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x0010B9D0 File Offset: 0x00109BD0
		private bool FunctionAvailableHelper(XmlQualifiedName name)
		{
			return QilGenerator.IsFunctionAvailable(name.Name, name.Namespace) || (name.Namespace.Length != 0 && !(name.Namespace == "http://www.w3.org/1999/XSL/Transform") && (this.runtime.ExternalContext.LateBoundFunctionExists(name.Name, name.Namespace) || this.runtime.EarlyBoundFunctionExists(name.Name, name.Namespace)));
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x0010BA4A File Offset: 0x00109C4A
		public int RegisterDecimalFormat(XmlQualifiedName name, string infinitySymbol, string nanSymbol, string characters)
		{
			if (this.decimalFormats == null)
			{
				this.decimalFormats = new Dictionary<XmlQualifiedName, DecimalFormat>();
			}
			this.decimalFormats.Add(name, this.CreateDecimalFormat(infinitySymbol, nanSymbol, characters));
			return 0;
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x0010BA78 File Offset: 0x00109C78
		private DecimalFormat CreateDecimalFormat(string infinitySymbol, string nanSymbol, string characters)
		{
			NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
			numberFormatInfo.NumberDecimalSeparator = char.ToString(characters[0]);
			numberFormatInfo.NumberGroupSeparator = char.ToString(characters[1]);
			numberFormatInfo.PositiveInfinitySymbol = infinitySymbol;
			numberFormatInfo.NegativeSign = char.ToString(characters[7]);
			numberFormatInfo.NaNSymbol = nanSymbol;
			numberFormatInfo.PercentSymbol = char.ToString(characters[2]);
			numberFormatInfo.PerMilleSymbol = char.ToString(characters[3]);
			numberFormatInfo.NegativeInfinitySymbol = numberFormatInfo.NegativeSign + numberFormatInfo.PositiveInfinitySymbol;
			return new DecimalFormat(numberFormatInfo, characters[5], characters[4], characters[6]);
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x0010BB25 File Offset: 0x00109D25
		public double RegisterDecimalFormatter(string formatPicture, string infinitySymbol, string nanSymbol, string characters)
		{
			if (this.decimalFormatters == null)
			{
				this.decimalFormatters = new List<DecimalFormatter>();
			}
			this.decimalFormatters.Add(new DecimalFormatter(formatPicture, this.CreateDecimalFormat(infinitySymbol, nanSymbol, characters)));
			return (double)(this.decimalFormatters.Count - 1);
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x0010BB64 File Offset: 0x00109D64
		public string FormatNumberStatic(double value, double decimalFormatterIndex)
		{
			int index = (int)decimalFormatterIndex;
			return this.decimalFormatters[index].Format(value);
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x0010BB88 File Offset: 0x00109D88
		public string FormatNumberDynamic(double value, string formatPicture, XmlQualifiedName decimalFormatName, string errorMessageName)
		{
			DecimalFormat decimalFormat;
			if (this.decimalFormats == null || !this.decimalFormats.TryGetValue(decimalFormatName, out decimalFormat))
			{
				throw new XslTransformException("Decimal format '{0}' is not defined.", new string[]
				{
					errorMessageName
				});
			}
			return new DecimalFormatter(formatPicture, decimalFormat).Format(value);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x0010BBD0 File Offset: 0x00109DD0
		public string NumberFormat(IList<XPathItem> value, string formatString, double lang, string letterValue, string groupingSeparator, double groupingSize)
		{
			return new NumberFormatter(formatString, (int)lang, letterValue, groupingSeparator, (int)groupingSize).FormatSequence(value);
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x0010BBE7 File Offset: 0x00109DE7
		public int LangToLcid(string lang, bool forwardCompatibility)
		{
			return XsltLibrary.LangToLcidInternal(lang, forwardCompatibility, null);
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x0010BBF4 File Offset: 0x00109DF4
		internal static int LangToLcidInternal(string lang, bool forwardCompatibility, IErrorHelper errorHelper)
		{
			int result = 127;
			if (lang != null)
			{
				if (lang.Length == 0)
				{
					if (!forwardCompatibility)
					{
						if (errorHelper == null)
						{
							throw new XslTransformException("'{1}' is an invalid value for the '{0}' attribute.", new string[]
							{
								"lang",
								lang
							});
						}
						errorHelper.ReportError("'{1}' is an invalid value for the '{0}' attribute.", new string[]
						{
							"lang",
							lang
						});
					}
				}
				else
				{
					try
					{
						result = new CultureInfo(lang).LCID;
					}
					catch (ArgumentException)
					{
						if (!forwardCompatibility)
						{
							if (errorHelper == null)
							{
								throw new XslTransformException("'{0}' is not a supported language identifier.", new string[]
								{
									lang
								});
							}
							errorHelper.ReportError("'{0}' is not a supported language identifier.", new string[]
							{
								lang
							});
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x0010BCAC File Offset: 0x00109EAC
		private static TypeCode GetTypeCode(XPathItem item)
		{
			Type valueType = item.ValueType;
			if (valueType == XsltConvert.StringType)
			{
				return TypeCode.String;
			}
			if (valueType == XsltConvert.DoubleType)
			{
				return TypeCode.Double;
			}
			return TypeCode.Boolean;
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x0010BCE1 File Offset: 0x00109EE1
		private static TypeCode WeakestTypeCode(TypeCode typeCode1, TypeCode typeCode2)
		{
			if (typeCode1 >= typeCode2)
			{
				return typeCode2;
			}
			return typeCode1;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x0010BCEC File Offset: 0x00109EEC
		private static bool CompareNumbers(XsltLibrary.ComparisonOperator op, double left, double right)
		{
			switch (op)
			{
			case XsltLibrary.ComparisonOperator.Eq:
				return left == right;
			case XsltLibrary.ComparisonOperator.Ne:
				return left != right;
			case XsltLibrary.ComparisonOperator.Lt:
				return left < right;
			case XsltLibrary.ComparisonOperator.Le:
				return left <= right;
			case XsltLibrary.ComparisonOperator.Gt:
				return left > right;
			default:
				return left >= right;
			}
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x0010BD3C File Offset: 0x00109F3C
		private static bool CompareValues(XsltLibrary.ComparisonOperator op, XPathItem left, XPathItem right, TypeCode compType)
		{
			if (compType == TypeCode.Double)
			{
				return XsltLibrary.CompareNumbers(op, XsltConvert.ToDouble(left), XsltConvert.ToDouble(right));
			}
			if (compType == TypeCode.String)
			{
				return XsltConvert.ToString(left) == XsltConvert.ToString(right) == (op == XsltLibrary.ComparisonOperator.Eq);
			}
			return XsltConvert.ToBoolean(left) == XsltConvert.ToBoolean(right) == (op == XsltLibrary.ComparisonOperator.Eq);
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x0010BD94 File Offset: 0x00109F94
		private static bool CompareNodeSetAndValue(XsltLibrary.ComparisonOperator op, IList<XPathNavigator> nodeset, XPathItem val, TypeCode compType)
		{
			if (compType == TypeCode.Boolean)
			{
				return XsltLibrary.CompareNumbers(op, (double)((nodeset.Count != 0) ? 1 : 0), (double)(XsltConvert.ToBoolean(val) ? 1 : 0));
			}
			int count = nodeset.Count;
			for (int i = 0; i < count; i++)
			{
				if (XsltLibrary.CompareValues(op, nodeset[i], val, compType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x0010BDF0 File Offset: 0x00109FF0
		private static bool CompareNodeSetAndNodeSet(XsltLibrary.ComparisonOperator op, IList<XPathNavigator> left, IList<XPathNavigator> right, TypeCode compType)
		{
			int count = left.Count;
			int count2 = right.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count2; j++)
				{
					if (XsltLibrary.CompareValues(op, left[i], right[j], compType))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x0010BE40 File Offset: 0x0010A040
		public bool EqualityOperator(double opCode, IList<XPathItem> left, IList<XPathItem> right)
		{
			XsltLibrary.ComparisonOperator op = (XsltLibrary.ComparisonOperator)opCode;
			if (XsltLibrary.IsNodeSetOrRtf(left))
			{
				if (XsltLibrary.IsNodeSetOrRtf(right))
				{
					return XsltLibrary.CompareNodeSetAndNodeSet(op, XsltLibrary.ToNodeSetOrRtf(left), XsltLibrary.ToNodeSetOrRtf(right), TypeCode.String);
				}
				XPathItem xpathItem = right[0];
				return XsltLibrary.CompareNodeSetAndValue(op, XsltLibrary.ToNodeSetOrRtf(left), xpathItem, XsltLibrary.GetTypeCode(xpathItem));
			}
			else
			{
				if (XsltLibrary.IsNodeSetOrRtf(right))
				{
					XPathItem xpathItem2 = left[0];
					return XsltLibrary.CompareNodeSetAndValue(op, XsltLibrary.ToNodeSetOrRtf(right), xpathItem2, XsltLibrary.GetTypeCode(xpathItem2));
				}
				XPathItem xpathItem3 = left[0];
				XPathItem xpathItem4 = right[0];
				return XsltLibrary.CompareValues(op, xpathItem3, xpathItem4, XsltLibrary.WeakestTypeCode(XsltLibrary.GetTypeCode(xpathItem3), XsltLibrary.GetTypeCode(xpathItem4)));
			}
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x0010BEE1 File Offset: 0x0010A0E1
		private static XsltLibrary.ComparisonOperator InvertOperator(XsltLibrary.ComparisonOperator op)
		{
			switch (op)
			{
			case XsltLibrary.ComparisonOperator.Lt:
				return XsltLibrary.ComparisonOperator.Gt;
			case XsltLibrary.ComparisonOperator.Le:
				return XsltLibrary.ComparisonOperator.Ge;
			case XsltLibrary.ComparisonOperator.Gt:
				return XsltLibrary.ComparisonOperator.Lt;
			case XsltLibrary.ComparisonOperator.Ge:
				return XsltLibrary.ComparisonOperator.Le;
			default:
				return op;
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x0010BF08 File Offset: 0x0010A108
		public bool RelationalOperator(double opCode, IList<XPathItem> left, IList<XPathItem> right)
		{
			XsltLibrary.ComparisonOperator op = (XsltLibrary.ComparisonOperator)opCode;
			if (XsltLibrary.IsNodeSetOrRtf(left))
			{
				if (XsltLibrary.IsNodeSetOrRtf(right))
				{
					return XsltLibrary.CompareNodeSetAndNodeSet(op, XsltLibrary.ToNodeSetOrRtf(left), XsltLibrary.ToNodeSetOrRtf(right), TypeCode.Double);
				}
				XPathItem xpathItem = right[0];
				return XsltLibrary.CompareNodeSetAndValue(op, XsltLibrary.ToNodeSetOrRtf(left), xpathItem, XsltLibrary.WeakestTypeCode(XsltLibrary.GetTypeCode(xpathItem), TypeCode.Double));
			}
			else
			{
				if (XsltLibrary.IsNodeSetOrRtf(right))
				{
					XPathItem xpathItem2 = left[0];
					op = XsltLibrary.InvertOperator(op);
					return XsltLibrary.CompareNodeSetAndValue(op, XsltLibrary.ToNodeSetOrRtf(right), xpathItem2, XsltLibrary.WeakestTypeCode(XsltLibrary.GetTypeCode(xpathItem2), TypeCode.Double));
				}
				XPathItem left2 = left[0];
				XPathItem right2 = right[0];
				return XsltLibrary.CompareValues(op, left2, right2, TypeCode.Double);
			}
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x0010BFB0 File Offset: 0x0010A1B0
		public bool IsSameNodeSort(XPathNavigator nav1, XPathNavigator nav2)
		{
			XPathNodeType nodeType = nav1.NodeType;
			XPathNodeType nodeType2 = nav2.NodeType;
			if (XPathNodeType.Text <= nodeType && nodeType <= XPathNodeType.Whitespace)
			{
				return XPathNodeType.Text <= nodeType2 && nodeType2 <= XPathNodeType.Whitespace;
			}
			return nodeType == nodeType2 && Ref.Equal(nav1.LocalName, nav2.LocalName) && Ref.Equal(nav1.NamespaceURI, nav2.NamespaceURI);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		internal static void CheckXsltValue(XPathItem item)
		{
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x0010C00C File Offset: 0x0010A20C
		[Conditional("DEBUG")]
		internal static void CheckXsltValue(IList<XPathItem> val)
		{
			if (val.Count == 1)
			{
				XsltFunctions.EXslObjectType(val);
				return;
			}
			int count = val.Count;
			int num = 0;
			while (num < count && val[num].IsNode)
			{
				if (num == 1)
				{
					num += Math.Max(count - 4, 0);
				}
				num++;
			}
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x0010C05B File Offset: 0x0010A25B
		private static bool IsNodeSetOrRtf(IList<XPathItem> val)
		{
			return val.Count != 1 || val[0].IsNode;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x0010C074 File Offset: 0x0010A274
		private static IList<XPathNavigator> ToNodeSetOrRtf(IList<XPathItem> val)
		{
			return XmlILStorageConverter.ItemsToNavigators(val);
		}

		// Token: 0x04002384 RID: 9092
		private XmlQueryRuntime runtime;

		// Token: 0x04002385 RID: 9093
		private HybridDictionary functionsAvail;

		// Token: 0x04002386 RID: 9094
		private Dictionary<XmlQualifiedName, DecimalFormat> decimalFormats;

		// Token: 0x04002387 RID: 9095
		private List<DecimalFormatter> decimalFormatters;

		// Token: 0x04002388 RID: 9096
		internal const int InvariantCultureLcid = 127;

		// Token: 0x02000495 RID: 1173
		internal enum ComparisonOperator
		{
			// Token: 0x0400238A RID: 9098
			Eq,
			// Token: 0x0400238B RID: 9099
			Ne,
			// Token: 0x0400238C RID: 9100
			Lt,
			// Token: 0x0400238D RID: 9101
			Le,
			// Token: 0x0400238E RID: 9102
			Gt,
			// Token: 0x0400238F RID: 9103
			Ge
		}
	}
}
