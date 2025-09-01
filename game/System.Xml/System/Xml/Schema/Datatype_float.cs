using System;

namespace System.Xml.Schema
{
	// Token: 0x02000518 RID: 1304
	internal class Datatype_float : Datatype_anySimpleType
	{
		// Token: 0x06003501 RID: 13569 RVA: 0x0012B3FA File Offset: 0x001295FA
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x0012B402 File Offset: 0x00129602
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x0012B409 File Offset: 0x00129609
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Float;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x0012B40D File Offset: 0x0012960D
		public override Type ValueType
		{
			get
			{
				return Datatype_float.atomicValueType;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x0012B414 File Offset: 0x00129614
		internal override Type ListValueType
		{
			get
			{
				return Datatype_float.listValueType;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x0012B41B File Offset: 0x0012961B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x0012B424 File Offset: 0x00129624
		internal override int Compare(object value1, object value2)
		{
			return ((float)value1).CompareTo(value2);
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x0012B440 File Offset: 0x00129640
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.numeric2FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				float num;
				ex = XmlConvert.TryToSingle(s, out num);
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

		// Token: 0x0600350A RID: 13578 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_float()
		{
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x0012B48A File Offset: 0x0012968A
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_float()
		{
		}

		// Token: 0x04002790 RID: 10128
		private static readonly Type atomicValueType = typeof(float);

		// Token: 0x04002791 RID: 10129
		private static readonly Type listValueType = typeof(float[]);
	}
}
