using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000491 RID: 1169
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class XsltConvert
	{
		// Token: 0x06002D86 RID: 11654 RVA: 0x0010A310 File Offset: 0x00108510
		public static bool ToBoolean(XPathItem item)
		{
			if (item.IsNode)
			{
				return true;
			}
			Type valueType = item.ValueType;
			if (valueType == XsltConvert.StringType)
			{
				return item.Value.Length != 0;
			}
			if (valueType == XsltConvert.DoubleType)
			{
				double valueAsDouble = item.ValueAsDouble;
				return valueAsDouble < 0.0 || 0.0 < valueAsDouble;
			}
			return item.ValueAsBoolean;
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0010A37F File Offset: 0x0010857F
		public static bool ToBoolean(IList<XPathItem> listItems)
		{
			return listItems.Count != 0 && XsltConvert.ToBoolean(listItems[0]);
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x0010A397 File Offset: 0x00108597
		public static double ToDouble(string value)
		{
			return XPathConvert.StringToDouble(value);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x0010A3A0 File Offset: 0x001085A0
		public static double ToDouble(XPathItem item)
		{
			if (item.IsNode)
			{
				return XPathConvert.StringToDouble(item.Value);
			}
			Type valueType = item.ValueType;
			if (valueType == XsltConvert.StringType)
			{
				return XPathConvert.StringToDouble(item.Value);
			}
			if (valueType == XsltConvert.DoubleType)
			{
				return item.ValueAsDouble;
			}
			if (!item.ValueAsBoolean)
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x0010A410 File Offset: 0x00108610
		public static double ToDouble(IList<XPathItem> listItems)
		{
			if (listItems.Count == 0)
			{
				return double.NaN;
			}
			return XsltConvert.ToDouble(listItems[0]);
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x0010A430 File Offset: 0x00108630
		public static XPathNavigator ToNode(XPathItem item)
		{
			if (!item.IsNode)
			{
				XPathDocument xpathDocument = new XPathDocument();
				XmlRawWriter xmlRawWriter = xpathDocument.LoadFromWriter(XPathDocument.LoadFlags.AtomizeNames, string.Empty);
				xmlRawWriter.WriteString(XsltConvert.ToString(item));
				xmlRawWriter.Close();
				return xpathDocument.CreateNavigator();
			}
			RtfNavigator rtfNavigator = item as RtfNavigator;
			if (rtfNavigator != null)
			{
				return rtfNavigator.ToNavigator();
			}
			return (XPathNavigator)item;
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x0010A484 File Offset: 0x00108684
		public static XPathNavigator ToNode(IList<XPathItem> listItems)
		{
			if (listItems.Count == 1)
			{
				return XsltConvert.ToNode(listItems[0]);
			}
			throw new XslTransformException("Cannot convert a node-set which contains zero nodes or more than one node to a single node.", new string[]
			{
				string.Empty
			});
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x0010A4B4 File Offset: 0x001086B4
		public static IList<XPathNavigator> ToNodeSet(XPathItem item)
		{
			return new XmlQueryNodeSequence(XsltConvert.ToNode(item));
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x0010A4C1 File Offset: 0x001086C1
		public static IList<XPathNavigator> ToNodeSet(IList<XPathItem> listItems)
		{
			if (listItems.Count == 1)
			{
				return new XmlQueryNodeSequence(XsltConvert.ToNode(listItems[0]));
			}
			return XmlILStorageConverter.ItemsToNavigators(listItems);
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x0010A4E4 File Offset: 0x001086E4
		public static string ToString(double value)
		{
			return XPathConvert.DoubleToString(value);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x0010A4EC File Offset: 0x001086EC
		public static string ToString(XPathItem item)
		{
			if (!item.IsNode && item.ValueType == XsltConvert.DoubleType)
			{
				return XPathConvert.DoubleToString(item.ValueAsDouble);
			}
			return item.Value;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x0010A51A File Offset: 0x0010871A
		public static string ToString(IList<XPathItem> listItems)
		{
			if (listItems.Count == 0)
			{
				return string.Empty;
			}
			return XsltConvert.ToString(listItems[0]);
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x0010A538 File Offset: 0x00108738
		public static string ToString(DateTime value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.DateTime).ToString();
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x0010A55A File Offset: 0x0010875A
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x0010A563 File Offset: 0x00108763
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x0010A563 File Offset: 0x00108763
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x0010A567 File Offset: 0x00108767
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x0010A56F File Offset: 0x0010876F
		public static int ToInt(double value)
		{
			return checked((int)value);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x0010A573 File Offset: 0x00108773
		public static long ToLong(double value)
		{
			return checked((long)value);
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x0010A577 File Offset: 0x00108777
		public static DateTime ToDateTime(string value)
		{
			return new XsdDateTime(value, XsdDateTimeFlags.AllXsd);
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x0010A58C File Offset: 0x0010878C
		internal static XmlAtomicValue ConvertToType(XmlAtomicValue value, XmlQueryType destinationType)
		{
			XmlTypeCode typeCode = destinationType.TypeCode;
			switch (typeCode)
			{
			case XmlTypeCode.String:
				switch (value.XmlType.TypeCode)
				{
				case XmlTypeCode.String:
				case XmlTypeCode.Boolean:
				case XmlTypeCode.Double:
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToString(value));
				case XmlTypeCode.DateTime:
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToString(value.ValueAsDateTime));
				}
				break;
			case XmlTypeCode.Boolean:
			{
				XmlTypeCode typeCode2 = value.XmlType.TypeCode;
				if (typeCode2 - XmlTypeCode.String <= 1 || typeCode2 == XmlTypeCode.Double)
				{
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToBoolean(value));
				}
				break;
			}
			case XmlTypeCode.Decimal:
				if (value.XmlType.TypeCode == XmlTypeCode.Double)
				{
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDecimal(value.ValueAsDouble));
				}
				break;
			case XmlTypeCode.Float:
			case XmlTypeCode.Duration:
				break;
			case XmlTypeCode.Double:
			{
				XmlTypeCode typeCode2 = value.XmlType.TypeCode;
				switch (typeCode2)
				{
				case XmlTypeCode.String:
				case XmlTypeCode.Boolean:
				case XmlTypeCode.Double:
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDouble(value));
				case XmlTypeCode.Decimal:
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDouble((decimal)value.ValueAs(XsltConvert.DecimalType, null)));
				case XmlTypeCode.Float:
					break;
				default:
					if (typeCode2 - XmlTypeCode.Long <= 1)
					{
						return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDouble(value.ValueAsLong));
					}
					break;
				}
				break;
			}
			case XmlTypeCode.DateTime:
				if (value.XmlType.TypeCode == XmlTypeCode.String)
				{
					return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToDateTime(value.Value));
				}
				break;
			default:
				if (typeCode - XmlTypeCode.Long <= 1)
				{
					if (value.XmlType.TypeCode == XmlTypeCode.Double)
					{
						return new XmlAtomicValue(destinationType.SchemaType, XsltConvert.ToLong(value.ValueAsDouble));
					}
				}
				break;
			}
			return value;
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x0010A75C File Offset: 0x0010895C
		public static IList<XPathNavigator> EnsureNodeSet(IList<XPathItem> listItems)
		{
			if (listItems.Count == 1)
			{
				XPathItem xpathItem = listItems[0];
				if (!xpathItem.IsNode)
				{
					throw new XslTransformException("Expression must evaluate to a node-set.", new string[]
					{
						string.Empty
					});
				}
				if (xpathItem is RtfNavigator)
				{
					throw new XslTransformException("To use a result tree fragment in a path expression, first convert it to a node-set using the msxsl:node-set() function.", new string[]
					{
						string.Empty
					});
				}
			}
			return XmlILStorageConverter.ItemsToNavigators(listItems);
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x0010A7C4 File Offset: 0x001089C4
		internal static XmlQueryType InferXsltType(Type clrType)
		{
			if (clrType == XsltConvert.BooleanType)
			{
				return XmlQueryTypeFactory.BooleanX;
			}
			if (clrType == XsltConvert.ByteType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.DecimalType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.DateTimeType)
			{
				return XmlQueryTypeFactory.StringX;
			}
			if (clrType == XsltConvert.DoubleType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.Int16Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.Int32Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.Int64Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.IXPathNavigableType)
			{
				return XmlQueryTypeFactory.NodeNotRtf;
			}
			if (clrType == XsltConvert.SByteType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.SingleType)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.StringType)
			{
				return XmlQueryTypeFactory.StringX;
			}
			if (clrType == XsltConvert.UInt16Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.UInt32Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.UInt64Type)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.XPathNavigatorArrayType)
			{
				return XmlQueryTypeFactory.NodeSDod;
			}
			if (clrType == XsltConvert.XPathNavigatorType)
			{
				return XmlQueryTypeFactory.NodeNotRtf;
			}
			if (clrType == XsltConvert.XPathNodeIteratorType)
			{
				return XmlQueryTypeFactory.NodeSDod;
			}
			if (clrType.IsEnum)
			{
				return XmlQueryTypeFactory.DoubleX;
			}
			if (clrType == XsltConvert.VoidType)
			{
				return XmlQueryTypeFactory.Empty;
			}
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x0010A950 File Offset: 0x00108B50
		// Note: this type is marked as 'beforefieldinit'.
		static XsltConvert()
		{
		}

		// Token: 0x04002341 RID: 9025
		internal static readonly Type BooleanType = typeof(bool);

		// Token: 0x04002342 RID: 9026
		internal static readonly Type ByteArrayType = typeof(byte[]);

		// Token: 0x04002343 RID: 9027
		internal static readonly Type ByteType = typeof(byte);

		// Token: 0x04002344 RID: 9028
		internal static readonly Type DateTimeType = typeof(DateTime);

		// Token: 0x04002345 RID: 9029
		internal static readonly Type DecimalType = typeof(decimal);

		// Token: 0x04002346 RID: 9030
		internal static readonly Type DoubleType = typeof(double);

		// Token: 0x04002347 RID: 9031
		internal static readonly Type ICollectionType = typeof(ICollection);

		// Token: 0x04002348 RID: 9032
		internal static readonly Type IEnumerableType = typeof(IEnumerable);

		// Token: 0x04002349 RID: 9033
		internal static readonly Type IListType = typeof(IList);

		// Token: 0x0400234A RID: 9034
		internal static readonly Type Int16Type = typeof(short);

		// Token: 0x0400234B RID: 9035
		internal static readonly Type Int32Type = typeof(int);

		// Token: 0x0400234C RID: 9036
		internal static readonly Type Int64Type = typeof(long);

		// Token: 0x0400234D RID: 9037
		internal static readonly Type IXPathNavigableType = typeof(IXPathNavigable);

		// Token: 0x0400234E RID: 9038
		internal static readonly Type ObjectType = typeof(object);

		// Token: 0x0400234F RID: 9039
		internal static readonly Type SByteType = typeof(sbyte);

		// Token: 0x04002350 RID: 9040
		internal static readonly Type SingleType = typeof(float);

		// Token: 0x04002351 RID: 9041
		internal static readonly Type StringType = typeof(string);

		// Token: 0x04002352 RID: 9042
		internal static readonly Type TimeSpanType = typeof(TimeSpan);

		// Token: 0x04002353 RID: 9043
		internal static readonly Type UInt16Type = typeof(ushort);

		// Token: 0x04002354 RID: 9044
		internal static readonly Type UInt32Type = typeof(uint);

		// Token: 0x04002355 RID: 9045
		internal static readonly Type UInt64Type = typeof(ulong);

		// Token: 0x04002356 RID: 9046
		internal static readonly Type UriType = typeof(Uri);

		// Token: 0x04002357 RID: 9047
		internal static readonly Type VoidType = typeof(void);

		// Token: 0x04002358 RID: 9048
		internal static readonly Type XmlAtomicValueType = typeof(XmlAtomicValue);

		// Token: 0x04002359 RID: 9049
		internal static readonly Type XmlQualifiedNameType = typeof(XmlQualifiedName);

		// Token: 0x0400235A RID: 9050
		internal static readonly Type XPathItemType = typeof(XPathItem);

		// Token: 0x0400235B RID: 9051
		internal static readonly Type XPathNavigatorArrayType = typeof(XPathNavigator[]);

		// Token: 0x0400235C RID: 9052
		internal static readonly Type XPathNavigatorType = typeof(XPathNavigator);

		// Token: 0x0400235D RID: 9053
		internal static readonly Type XPathNodeIteratorType = typeof(XPathNodeIterator);
	}
}
