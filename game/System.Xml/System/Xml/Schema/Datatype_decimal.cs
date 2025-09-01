using System;

namespace System.Xml.Schema
{
	// Token: 0x0200051A RID: 1306
	internal class Datatype_decimal : Datatype_anySimpleType
	{
		// Token: 0x06003517 RID: 13591 RVA: 0x0012B542 File Offset: 0x00129742
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric10Converter.Create(schemaType);
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x0012B54A File Offset: 0x0012974A
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_decimal.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000707D5 File Offset: 0x0006E9D5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Decimal;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x0012B551 File Offset: 0x00129751
		public override Type ValueType
		{
			get
			{
				return Datatype_decimal.atomicValueType;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x0012B558 File Offset: 0x00129758
		internal override Type ListValueType
		{
			get
			{
				return Datatype_decimal.listValueType;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x0012B55F File Offset: 0x0012975F
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits;
			}
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x0012B568 File Offset: 0x00129768
		internal override int Compare(object value1, object value2)
		{
			return ((decimal)value1).CompareTo(value2);
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x0012B584 File Offset: 0x00129784
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = Datatype_decimal.numeric10FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				decimal num;
				ex = XmlConvert.TryToDecimal(s, out num);
				if (ex == null)
				{
					ex = Datatype_decimal.numeric10FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_decimal()
		{
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x0012B5CE File Offset: 0x001297CE
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_decimal()
		{
		}

		// Token: 0x04002794 RID: 10132
		private static readonly Type atomicValueType = typeof(decimal);

		// Token: 0x04002795 RID: 10133
		private static readonly Type listValueType = typeof(decimal[]);

		// Token: 0x04002796 RID: 10134
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, decimal.MaxValue);
	}
}
