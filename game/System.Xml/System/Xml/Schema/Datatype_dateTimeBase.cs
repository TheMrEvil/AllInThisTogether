using System;

namespace System.Xml.Schema
{
	// Token: 0x0200051E RID: 1310
	internal class Datatype_dateTimeBase : Datatype_anySimpleType
	{
		// Token: 0x06003533 RID: 13619 RVA: 0x0012B7C8 File Offset: 0x001299C8
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlDateTimeConverter.Create(schemaType);
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x0012B7D0 File Offset: 0x001299D0
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.dateTimeFacetsChecker;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x0012B7D7 File Offset: 0x001299D7
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DateTime;
			}
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x0012B310 File Offset: 0x00129510
		internal Datatype_dateTimeBase()
		{
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x0012B7DB File Offset: 0x001299DB
		internal Datatype_dateTimeBase(XsdDateTimeFlags dateTimeFlags)
		{
			this.dateTimeFlags = dateTimeFlags;
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x0012B7EA File Offset: 0x001299EA
		public override Type ValueType
		{
			get
			{
				return Datatype_dateTimeBase.atomicValueType;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x0012B7F1 File Offset: 0x001299F1
		internal override Type ListValueType
		{
			get
			{
				return Datatype_dateTimeBase.listValueType;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x0012B41B File Offset: 0x0012961B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x0012B7F8 File Offset: 0x001299F8
		internal override int Compare(object value1, object value2)
		{
			DateTime dateTime = (DateTime)value1;
			DateTime value3 = (DateTime)value2;
			if (dateTime.Kind == DateTimeKind.Unspecified || value3.Kind == DateTimeKind.Unspecified)
			{
				return dateTime.CompareTo(value3);
			}
			return dateTime.ToUniversalTime().CompareTo(value3.ToUniversalTime());
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x0012B844 File Offset: 0x00129A44
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.dateTimeFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XsdDateTime xdt;
				if (!XsdDateTime.TryParse(s, this.dateTimeFlags, out xdt))
				{
					ex = new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[]
					{
						s,
						this.dateTimeFlags.ToString()
					}));
				}
				else
				{
					DateTime dateTime = DateTime.MinValue;
					try
					{
						dateTime = xdt;
					}
					catch (ArgumentException ex)
					{
						return ex;
					}
					ex = DatatypeImplementation.dateTimeFacetsChecker.CheckValueFacets(dateTime, this);
					if (ex == null)
					{
						typedValue = dateTime;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x0012B8E4 File Offset: 0x00129AE4
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_dateTimeBase()
		{
		}

		// Token: 0x04002799 RID: 10137
		private static readonly Type atomicValueType = typeof(DateTime);

		// Token: 0x0400279A RID: 10138
		private static readonly Type listValueType = typeof(DateTime[]);

		// Token: 0x0400279B RID: 10139
		private XsdDateTimeFlags dateTimeFlags;
	}
}
