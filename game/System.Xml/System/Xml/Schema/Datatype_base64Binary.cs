using System;

namespace System.Xml.Schema
{
	// Token: 0x0200052C RID: 1324
	internal class Datatype_base64Binary : Datatype_anySimpleType
	{
		// Token: 0x0600355D RID: 13661 RVA: 0x0012B60C File Offset: 0x0012980C
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x0012B992 File Offset: 0x00129B92
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x0012BA48 File Offset: 0x00129C48
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Base64Binary;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x0012BA4C File Offset: 0x00129C4C
		public override Type ValueType
		{
			get
			{
				return Datatype_base64Binary.atomicValueType;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x0012BA53 File Offset: 0x00129C53
		internal override Type ListValueType
		{
			get
			{
				return Datatype_base64Binary.listValueType;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x0012ACD1 File Offset: 0x00128ED1
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x0012B9AB File Offset: 0x00129BAB
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x0012BA5C File Offset: 0x00129C5C
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.binaryFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte[] array = null;
				try
				{
					array = Convert.FromBase64String(s);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (FormatException ex)
				{
					return ex;
				}
				ex = DatatypeImplementation.binaryFacetsChecker.CheckValueFacets(array, this);
				if (ex == null)
				{
					typedValue = array;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_base64Binary()
		{
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x0012BAC4 File Offset: 0x00129CC4
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_base64Binary()
		{
		}

		// Token: 0x0400279E RID: 10142
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x0400279F RID: 10143
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
