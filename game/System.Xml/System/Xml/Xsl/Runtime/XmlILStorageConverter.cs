using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200046E RID: 1134
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class XmlILStorageConverter
	{
		// Token: 0x06002BEF RID: 11247 RVA: 0x00105CF0 File Offset: 0x00103EF0
		public static XmlAtomicValue StringToAtomicValue(string value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x00105D04 File Offset: 0x00103F04
		public static XmlAtomicValue DecimalToAtomicValue(decimal value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x00105D1D File Offset: 0x00103F1D
		public static XmlAtomicValue Int64ToAtomicValue(long value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x00105D31 File Offset: 0x00103F31
		public static XmlAtomicValue Int32ToAtomicValue(int value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x00105D45 File Offset: 0x00103F45
		public static XmlAtomicValue BooleanToAtomicValue(bool value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x00105D59 File Offset: 0x00103F59
		public static XmlAtomicValue DoubleToAtomicValue(double value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x00105D6D File Offset: 0x00103F6D
		public static XmlAtomicValue SingleToAtomicValue(float value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, (double)value);
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x00105D82 File Offset: 0x00103F82
		public static XmlAtomicValue DateTimeToAtomicValue(DateTime value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x00105D96 File Offset: 0x00103F96
		public static XmlAtomicValue XmlQualifiedNameToAtomicValue(XmlQualifiedName value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x00105DAA File Offset: 0x00103FAA
		public static XmlAtomicValue TimeSpanToAtomicValue(TimeSpan value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x00105D96 File Offset: 0x00103F96
		public static XmlAtomicValue BytesToAtomicValue(byte[] value, int index, XmlQueryRuntime runtime)
		{
			return new XmlAtomicValue(runtime.GetXmlType(index).SchemaType, value);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x00105DC4 File Offset: 0x00103FC4
		public static IList<XPathItem> NavigatorsToItems(IList<XPathNavigator> listNavigators)
		{
			IList<XPathItem> list = listNavigators as IList<XPathItem>;
			if (list != null)
			{
				return list;
			}
			return new XmlQueryNodeSequence(listNavigators);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x00105DE4 File Offset: 0x00103FE4
		public static IList<XPathNavigator> ItemsToNavigators(IList<XPathItem> listItems)
		{
			IList<XPathNavigator> list = listItems as IList<XPathNavigator>;
			if (list != null)
			{
				return list;
			}
			XmlQueryNodeSequence xmlQueryNodeSequence = new XmlQueryNodeSequence(listItems.Count);
			for (int i = 0; i < listItems.Count; i++)
			{
				xmlQueryNodeSequence.Add((XPathNavigator)listItems[i]);
			}
			return xmlQueryNodeSequence;
		}
	}
}
