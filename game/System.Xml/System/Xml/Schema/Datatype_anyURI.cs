using System;

namespace System.Xml.Schema
{
	// Token: 0x0200052D RID: 1325
	internal class Datatype_anyURI : Datatype_anySimpleType
	{
		// Token: 0x06003568 RID: 13672 RVA: 0x0012B60C File Offset: 0x0012980C
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x0012B328 File Offset: 0x00129528
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x0012BAE4 File Offset: 0x00129CE4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyUri;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x0012BAE8 File Offset: 0x00129CE8
		public override Type ValueType
		{
			get
			{
				return Datatype_anyURI.atomicValueType;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x0012BAEF File Offset: 0x00129CEF
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anyURI.listValueType;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600356E RID: 13678 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x0012ACD1 File Offset: 0x00128ED1
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x0012BAF6 File Offset: 0x00129CF6
		internal override int Compare(object value1, object value2)
		{
			if (!((Uri)value1).Equals((Uri)value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x0012BB10 File Offset: 0x00129D10
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				Uri uri;
				ex = XmlConvert.TryToUri(s, out uri);
				if (ex == null)
				{
					string originalString = uri.OriginalString;
					ex = ((StringFacetsChecker)DatatypeImplementation.stringFacetsChecker).CheckValueFacets(originalString, this, false);
					if (ex == null)
					{
						typedValue = uri;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_anyURI()
		{
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x0012BB62 File Offset: 0x00129D62
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_anyURI()
		{
		}

		// Token: 0x040027A0 RID: 10144
		private static readonly Type atomicValueType = typeof(Uri);

		// Token: 0x040027A1 RID: 10145
		private static readonly Type listValueType = typeof(Uri[]);
	}
}
