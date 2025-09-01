using System;

namespace System.Xml.Schema
{
	// Token: 0x02000516 RID: 1302
	internal class Datatype_string : Datatype_anySimpleType
	{
		// Token: 0x060034EE RID: 13550 RVA: 0x0012B320 File Offset: 0x00129520
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlStringConverter.Create(schemaType);
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060034F0 RID: 13552 RVA: 0x0012B328 File Offset: 0x00129528
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060034F1 RID: 13553 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.String;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x0012ACD1 File Offset: 0x00128ED1
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x0012B330 File Offset: 0x00129530
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ex = DatatypeImplementation.stringFacetsChecker.CheckValueFacets(s, this);
				if (ex == null)
				{
					typedValue = s;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_string()
		{
		}
	}
}
