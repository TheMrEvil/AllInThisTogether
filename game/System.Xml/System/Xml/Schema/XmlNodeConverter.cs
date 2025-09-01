using System;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020005F8 RID: 1528
	internal class XmlNodeConverter : XmlBaseConverter
	{
		// Token: 0x06003E94 RID: 16020 RVA: 0x0015A1EA File Offset: 0x001583EA
		protected XmlNodeConverter() : base(XmlTypeCode.Node)
		{
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x0015A1F4 File Offset: 0x001583F4
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
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (destinationType == XmlBaseConverter.XPathNavigatorType && XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
			{
				return (XPathNavigator)value;
			}
			if (destinationType == XmlBaseConverter.XPathItemType && XmlBaseConverter.IsDerivedFrom(type, XmlBaseConverter.XPathNavigatorType))
			{
				return (XPathItem)value;
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x0015A28A File Offset: 0x0015848A
		// Note: this type is marked as 'beforefieldinit'.
		static XmlNodeConverter()
		{
		}

		// Token: 0x04002C90 RID: 11408
		public static readonly XmlValueConverter Node = new XmlNodeConverter();
	}
}
