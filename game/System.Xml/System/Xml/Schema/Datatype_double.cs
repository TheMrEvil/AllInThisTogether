using System;

namespace System.Xml.Schema
{
	// Token: 0x02000519 RID: 1305
	internal class Datatype_double : Datatype_anySimpleType
	{
		// Token: 0x0600350C RID: 13580 RVA: 0x0012B3FA File Offset: 0x001295FA
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x0012B402 File Offset: 0x00129602
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x0012B4AA File Offset: 0x001296AA
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Double;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x0012B4AE File Offset: 0x001296AE
		public override Type ValueType
		{
			get
			{
				return Datatype_double.atomicValueType;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x0012B4B5 File Offset: 0x001296B5
		internal override Type ListValueType
		{
			get
			{
				return Datatype_double.listValueType;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x0012B41B File Offset: 0x0012961B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x0012B4BC File Offset: 0x001296BC
		internal override int Compare(object value1, object value2)
		{
			return ((double)value1).CompareTo(value2);
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x0012B4D8 File Offset: 0x001296D8
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.numeric2FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				double num;
				ex = XmlConvert.TryToDouble(s, out num);
				if (ex == null)
				{
					ex = DatatypeImplementation.numeric2FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_double()
		{
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x0012B522 File Offset: 0x00129722
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_double()
		{
		}

		// Token: 0x04002792 RID: 10130
		private static readonly Type atomicValueType = typeof(double);

		// Token: 0x04002793 RID: 10131
		private static readonly Type listValueType = typeof(double[]);
	}
}
