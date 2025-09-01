using System;

namespace System.Xml.Schema
{
	// Token: 0x02000513 RID: 1299
	internal class Datatype_anySimpleType : DatatypeImplementation
	{
		// Token: 0x060034DA RID: 13530 RVA: 0x0012B2AC File Offset: 0x001294AC
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x0012A7C2 File Offset: 0x001289C2
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x0012B2B3 File Offset: 0x001294B3
		public override Type ValueType
		{
			get
			{
				return Datatype_anySimpleType.atomicValueType;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000699F5 File Offset: 0x00067BF5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x0012B2BA File Offset: 0x001294BA
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anySimpleType.listValueType;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x0012B2C1 File Offset: 0x001294C1
		internal override int Compare(object value1, object value2)
		{
			return string.Compare(value1.ToString(), value2.ToString(), StringComparison.Ordinal);
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x0012B2D5 File Offset: 0x001294D5
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = XmlComplianceUtil.NonCDataNormalize(s);
			return null;
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x0012B2E1 File Offset: 0x001294E1
		public Datatype_anySimpleType()
		{
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x0012B2E9 File Offset: 0x001294E9
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_anySimpleType()
		{
		}

		// Token: 0x0400278C RID: 10124
		private static readonly Type atomicValueType = typeof(string);

		// Token: 0x0400278D RID: 10125
		private static readonly Type listValueType = typeof(string[]);
	}
}
