using System;

namespace System.Xml.Schema
{
	// Token: 0x0200051B RID: 1307
	internal class Datatype_duration : Datatype_anySimpleType
	{
		// Token: 0x06003522 RID: 13602 RVA: 0x0012B60C File Offset: 0x0012980C
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x0012B614 File Offset: 0x00129814
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.durationFacetsChecker;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x00067B53 File Offset: 0x00065D53
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Duration;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x0012B61B File Offset: 0x0012981B
		public override Type ValueType
		{
			get
			{
				return Datatype_duration.atomicValueType;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x0012B622 File Offset: 0x00129822
		internal override Type ListValueType
		{
			get
			{
				return Datatype_duration.listValueType;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x0012B41B File Offset: 0x0012961B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x0012B62C File Offset: 0x0012982C
		internal override int Compare(object value1, object value2)
		{
			return ((TimeSpan)value1).CompareTo(value2);
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x0012B648 File Offset: 0x00129848
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
			}
			Exception ex = DatatypeImplementation.durationFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				TimeSpan timeSpan;
				ex = XmlConvert.TryToTimeSpan(s, out timeSpan);
				if (ex == null)
				{
					ex = DatatypeImplementation.durationFacetsChecker.CheckValueFacets(timeSpan, this);
					if (ex == null)
					{
						typedValue = timeSpan;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_duration()
		{
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x0012B6AD File Offset: 0x001298AD
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_duration()
		{
		}

		// Token: 0x04002797 RID: 10135
		private static readonly Type atomicValueType = typeof(TimeSpan);

		// Token: 0x04002798 RID: 10136
		private static readonly Type listValueType = typeof(TimeSpan[]);
	}
}
