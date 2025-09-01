using System;

namespace System.Xml.Schema
{
	// Token: 0x0200052B RID: 1323
	internal class Datatype_hexBinary : Datatype_anySimpleType
	{
		// Token: 0x06003552 RID: 13650 RVA: 0x0012B60C File Offset: 0x0012980C
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x0012B992 File Offset: 0x00129B92
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x0012B999 File Offset: 0x00129B99
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.HexBinary;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x0012B99D File Offset: 0x00129B9D
		public override Type ValueType
		{
			get
			{
				return Datatype_hexBinary.atomicValueType;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x0012B9A4 File Offset: 0x00129BA4
		internal override Type ListValueType
		{
			get
			{
				return Datatype_hexBinary.listValueType;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x00066748 File Offset: 0x00064948
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x0012ACD1 File Offset: 0x00128ED1
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x0012B9AB File Offset: 0x00129BAB
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x0012B9C0 File Offset: 0x00129BC0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.binaryFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte[] array = null;
				try
				{
					array = XmlConvert.FromBinHexString(s, false);
				}
				catch (ArgumentException ex)
				{
					return ex;
				}
				catch (XmlException ex)
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

		// Token: 0x0600355B RID: 13659 RVA: 0x0012B310 File Offset: 0x00129510
		public Datatype_hexBinary()
		{
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x0012BA28 File Offset: 0x00129C28
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_hexBinary()
		{
		}

		// Token: 0x0400279C RID: 10140
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x0400279D RID: 10141
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
