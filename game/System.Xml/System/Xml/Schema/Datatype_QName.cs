using System;

namespace System.Xml.Schema
{
	// Token: 0x0200052E RID: 1326
	internal class Datatype_QName : Datatype_anySimpleType
	{
		// Token: 0x06003574 RID: 13684 RVA: 0x0012B60C File Offset: 0x0012980C
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x0012BB82 File Offset: 0x00129D82
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06003576 RID: 13686 RVA: 0x0012BB89 File Offset: 0x00129D89
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.QName;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000699F5 File Offset: 0x00067BF5
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x0012ACD1 File Offset: 0x00128ED1
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x0012BB8D File Offset: 0x00129D8D
		public override Type ValueType
		{
			get
			{
				return Datatype_QName.atomicValueType;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x0012BB94 File Offset: 0x00129D94
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QName.listValueType;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x0600357B RID: 13691 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x0012BB9C File Offset: 0x00129D9C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.qnameFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XmlQualifiedName xmlQualifiedName = null;
				try
				{
					string text;
					xmlQualifiedName = XmlQualifiedName.Parse(s, nsmgr, out text);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (XmlException ex)
				{
					return ex;
				}
				ex = DatatypeImplementation.qnameFacetsChecker.CheckValueFacets(xmlQualifiedName, this);
				if (ex == null)
				{
					typedValue = xmlQualifiedName;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_QName()
		{
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x0012BC20 File Offset: 0x00129E20
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_QName()
		{
		}

		// Token: 0x040027A2 RID: 10146
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x040027A3 RID: 10147
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
