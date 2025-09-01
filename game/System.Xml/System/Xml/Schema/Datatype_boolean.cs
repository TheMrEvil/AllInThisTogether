using System;

namespace System.Xml.Schema
{
	// Token: 0x02000517 RID: 1303
	internal class Datatype_boolean : Datatype_anySimpleType
	{
		// Token: 0x060034F6 RID: 13558 RVA: 0x0012B369 File Offset: 0x00129569
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlBooleanConverter.Create(schemaType);
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x0012A7C2 File Offset: 0x001289C2
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x00070A9B File Offset: 0x0006EC9B
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Boolean;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x0012B371 File Offset: 0x00129571
		public override Type ValueType
		{
			get
			{
				return Datatype_boolean.atomicValueType;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x0012B378 File Offset: 0x00129578
		internal override Type ListValueType
		{
			get
			{
				return Datatype_boolean.listValueType;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x0012B37F File Offset: 0x0012957F
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x0012B384 File Offset: 0x00129584
		internal override int Compare(object value1, object value2)
		{
			return ((bool)value1).CompareTo(value2);
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x0012B3A0 File Offset: 0x001295A0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.miscFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				bool flag;
				ex = XmlConvert.TryToBoolean(s, out flag);
				if (ex == null)
				{
					typedValue = flag;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_boolean()
		{
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x0012B3DA File Offset: 0x001295DA
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_boolean()
		{
		}

		// Token: 0x0400278E RID: 10126
		private static readonly Type atomicValueType = typeof(bool);

		// Token: 0x0400278F RID: 10127
		private static readonly Type listValueType = typeof(bool[]);
	}
}
