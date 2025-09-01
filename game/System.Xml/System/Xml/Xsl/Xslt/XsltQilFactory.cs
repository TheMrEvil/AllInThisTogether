using System;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200041F RID: 1055
	internal class XsltQilFactory : XPathQilFactory
	{
		// Token: 0x06002A06 RID: 10758 RVA: 0x000FE25A File Offset: 0x000FC45A
		public XsltQilFactory(QilFactory f, bool debug) : base(f, debug)
		{
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000FE264 File Offset: 0x000FC464
		[Conditional("DEBUG")]
		public void CheckXsltType(QilNode n)
		{
			XmlTypeCode typeCode = n.XmlType.TypeCode;
			if (typeCode <= XmlTypeCode.Boolean)
			{
				if (typeCode > XmlTypeCode.Item)
				{
					int num = typeCode - XmlTypeCode.String;
					return;
				}
			}
			else if (typeCode != XmlTypeCode.Double)
			{
			}
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void CheckQName(QilNode n)
		{
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000FE298 File Offset: 0x000FC498
		public QilNode DefaultValueMarker()
		{
			return base.QName("default-value", "urn:schemas-microsoft-com:xslt-debug");
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x000FE2AA File Offset: 0x000FC4AA
		public QilNode IsDefaultValueMarker(QilNode n)
		{
			return base.IsType(n, XmlQueryTypeFactory.QNameX);
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000FE2B8 File Offset: 0x000FC4B8
		public QilNode InvokeIsSameNodeSort(QilNode n1, QilNode n2)
		{
			return base.XsltInvokeEarlyBound(base.QName("is-same-node-sort"), XsltMethods.IsSameNodeSort, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				n1,
				n2
			});
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000FE2E3 File Offset: 0x000FC4E3
		public QilNode InvokeSystemProperty(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("system-property"), XsltMethods.SystemProperty, XmlQueryTypeFactory.Choice(XmlQueryTypeFactory.DoubleX, XmlQueryTypeFactory.StringX), new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000FE314 File Offset: 0x000FC514
		public QilNode InvokeElementAvailable(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("element-available"), XsltMethods.ElementAvailable, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000FE33C File Offset: 0x000FC53C
		public QilNode InvokeCheckScriptNamespace(string nsUri)
		{
			return base.XsltInvokeEarlyBound(base.QName("register-script-namespace"), XsltMethods.CheckScriptNamespace, XmlQueryTypeFactory.IntX, new QilNode[]
			{
				base.String(nsUri)
			});
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000FE374 File Offset: 0x000FC574
		public QilNode InvokeFunctionAvailable(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("function-available"), XsltMethods.FunctionAvailable, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000FE39B File Offset: 0x000FC59B
		public QilNode InvokeBaseUri(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("base-uri"), XsltMethods.BaseUri, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000FE3C2 File Offset: 0x000FC5C2
		public QilNode InvokeOnCurrentNodeChanged(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("on-current-node-changed"), XsltMethods.OnCurrentNodeChanged, XmlQueryTypeFactory.IntX, new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000FE3EC File Offset: 0x000FC5EC
		public QilNode InvokeLangToLcid(QilNode n, bool fwdCompat)
		{
			return base.XsltInvokeEarlyBound(base.QName("lang-to-lcid"), XsltMethods.LangToLcid, XmlQueryTypeFactory.IntX, new QilNode[]
			{
				n,
				base.Boolean(fwdCompat)
			});
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000FE428 File Offset: 0x000FC628
		public QilNode InvokeNumberFormat(QilNode value, QilNode format, QilNode lang, QilNode letterValue, QilNode groupingSeparator, QilNode groupingSize)
		{
			return base.XsltInvokeEarlyBound(base.QName("number-format"), XsltMethods.NumberFormat, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				value,
				format,
				lang,
				letterValue,
				groupingSeparator,
				groupingSize
			});
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000FE468 File Offset: 0x000FC668
		public QilNode InvokeRegisterDecimalFormat(DecimalFormatDecl format)
		{
			return base.XsltInvokeEarlyBound(base.QName("register-decimal-format"), XsltMethods.RegisterDecimalFormat, XmlQueryTypeFactory.IntX, new QilNode[]
			{
				base.QName(format.Name.Name, format.Name.Namespace),
				base.String(format.InfinitySymbol),
				base.String(format.NanSymbol),
				base.String(new string(format.Characters))
			});
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000FE4E8 File Offset: 0x000FC6E8
		public QilNode InvokeRegisterDecimalFormatter(QilNode formatPicture, DecimalFormatDecl format)
		{
			return base.XsltInvokeEarlyBound(base.QName("register-decimal-formatter"), XsltMethods.RegisterDecimalFormatter, XmlQueryTypeFactory.DoubleX, new QilNode[]
			{
				formatPicture,
				base.String(format.InfinitySymbol),
				base.String(format.NanSymbol),
				base.String(new string(format.Characters))
			});
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000FE54C File Offset: 0x000FC74C
		public QilNode InvokeFormatNumberStatic(QilNode value, QilNode decimalFormatIndex)
		{
			return base.XsltInvokeEarlyBound(base.QName("format-number-static"), XsltMethods.FormatNumberStatic, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				value,
				decimalFormatIndex
			});
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000FE577 File Offset: 0x000FC777
		public QilNode InvokeFormatNumberDynamic(QilNode value, QilNode formatPicture, QilNode decimalFormatName, QilNode errorMessageName)
		{
			return base.XsltInvokeEarlyBound(base.QName("format-number-dynamic"), XsltMethods.FormatNumberDynamic, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				value,
				formatPicture,
				decimalFormatName,
				errorMessageName
			});
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000FE5AB File Offset: 0x000FC7AB
		public QilNode InvokeOuterXml(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("outer-xml"), XsltMethods.OuterXml, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000FE5D2 File Offset: 0x000FC7D2
		public QilNode InvokeMsFormatDateTime(QilNode datetime, QilNode format, QilNode lang, QilNode isDate)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:format-date-time"), XsltMethods.MSFormatDateTime, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				datetime,
				format,
				lang,
				isDate
			});
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x000FE606 File Offset: 0x000FC806
		public QilNode InvokeMsStringCompare(QilNode x, QilNode y, QilNode lang, QilNode options)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:string-compare"), XsltMethods.MSStringCompare, XmlQueryTypeFactory.DoubleX, new QilNode[]
			{
				x,
				y,
				lang,
				options
			});
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000FE63A File Offset: 0x000FC83A
		public QilNode InvokeMsUtc(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:utc"), XsltMethods.MSUtc, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000FE661 File Offset: 0x000FC861
		public QilNode InvokeMsNumber(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:number"), XsltMethods.MSNumber, XmlQueryTypeFactory.DoubleX, new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000FE688 File Offset: 0x000FC888
		public QilNode InvokeMsLocalName(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:local-name"), XsltMethods.MSLocalName, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000FE6AF File Offset: 0x000FC8AF
		public QilNode InvokeMsNamespaceUri(QilNode n, QilNode currentNode)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:namespace-uri"), XsltMethods.MSNamespaceUri, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				n,
				currentNode
			});
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000FE6DA File Offset: 0x000FC8DA
		public QilNode InvokeEXslObjectType(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("exsl:object-type"), XsltMethods.EXslObjectType, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				n
			});
		}
	}
}
