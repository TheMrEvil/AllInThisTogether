using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020005FA RID: 1530
	internal class XmlAnyListConverter : XmlListConverter
	{
		// Token: 0x06003EAE RID: 16046 RVA: 0x0015AD33 File Offset: 0x00158F33
		protected XmlAnyListConverter(XmlBaseConverter atomicConverter) : base(atomicConverter)
		{
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x0015AD3C File Offset: 0x00158F3C
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(value is IEnumerable) || value.GetType() == XmlBaseConverter.StringType || value.GetType() == XmlBaseConverter.ByteArrayType)
			{
				value = new object[]
				{
					value
				};
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x0015ADAC File Offset: 0x00158FAC
		// Note: this type is marked as 'beforefieldinit'.
		static XmlAnyListConverter()
		{
		}

		// Token: 0x04002C93 RID: 11411
		public static readonly XmlValueConverter ItemList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.Item);

		// Token: 0x04002C94 RID: 11412
		public static readonly XmlValueConverter AnyAtomicList = new XmlAnyListConverter((XmlBaseConverter)XmlAnyConverter.AnyAtomic);
	}
}
