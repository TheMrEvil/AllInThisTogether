using System;

namespace System.Xml.Schema
{
	// Token: 0x02000512 RID: 1298
	internal class Datatype_union : Datatype_anySimpleType
	{
		// Token: 0x060034CC RID: 13516 RVA: 0x0012AFCD File Offset: 0x001291CD
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUnionConverter.Create(schemaType);
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x0012AFD5 File Offset: 0x001291D5
		internal Datatype_union(XmlSchemaSimpleType[] types)
		{
			this.types = types;
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x0012AFE4 File Offset: 0x001291E4
		internal override int Compare(object value1, object value2)
		{
			XsdSimpleValue xsdSimpleValue = value1 as XsdSimpleValue;
			XsdSimpleValue xsdSimpleValue2 = value2 as XsdSimpleValue;
			if (xsdSimpleValue == null || xsdSimpleValue2 == null)
			{
				return -1;
			}
			XmlSchemaType xmlType = xsdSimpleValue.XmlType;
			XmlSchemaType xmlType2 = xsdSimpleValue2.XmlType;
			if (xmlType == xmlType2)
			{
				return xmlType.Datatype.Compare(xsdSimpleValue.TypedValue, xsdSimpleValue2.TypedValue);
			}
			return -1;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x0012B032 File Offset: 0x00129232
		public override Type ValueType
		{
			get
			{
				return Datatype_union.atomicValueType;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x000699F5 File Offset: 0x00067BF5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x0012B039 File Offset: 0x00129239
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.unionFacetsChecker;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060034D2 RID: 13522 RVA: 0x0012B040 File Offset: 0x00129240
		internal override Type ListValueType
		{
			get
			{
				return Datatype_union.listValueType;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x0012B047 File Offset: 0x00129247
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x0012B04B File Offset: 0x0012924B
		internal XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x0012B054 File Offset: 0x00129254
		internal bool HasAtomicMembers()
		{
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i].Datatype.Variety == XmlSchemaDatatypeVariety.List)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x0012B08C File Offset: 0x0012928C
		internal bool IsUnionBaseOf(DatatypeImplementation derivedType)
		{
			for (int i = 0; i < this.types.Length; i++)
			{
				if (derivedType.IsDerivedFrom(this.types[i].Datatype))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x0012B0C4 File Offset: 0x001292C4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			typedValue = null;
			Exception ex = DatatypeImplementation.unionFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				for (int i = 0; i < this.types.Length; i++)
				{
					if (this.types[i].Datatype.TryParseValue(s, nameTable, nsmgr, out typedValue) == null)
					{
						xmlSchemaSimpleType = this.types[i];
						break;
					}
				}
				if (xmlSchemaSimpleType == null)
				{
					ex = new XmlSchemaException("The value '{0}' is not valid according to any of the memberTypes of the union.", s);
				}
				else
				{
					typedValue = new XsdSimpleValue(xmlSchemaSimpleType, typedValue);
					ex = DatatypeImplementation.unionFacetsChecker.CheckValueFacets(typedValue, this);
					if (ex == null)
					{
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x0012B154 File Offset: 0x00129354
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			typedValue = null;
			string text = value as string;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, nsmgr, out typedValue);
			}
			object obj = null;
			XmlSchemaSimpleType st = null;
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i].Datatype.TryParseValue(value, nameTable, nsmgr, out obj) == null)
				{
					st = this.types[i];
					break;
				}
			}
			Exception ex;
			if (obj != null)
			{
				try
				{
					if (this.HasLexicalFacets)
					{
						string text2 = (string)this.ValueConverter.ChangeType(obj, typeof(string), nsmgr);
						ex = DatatypeImplementation.unionFacetsChecker.CheckLexicalFacets(ref text2, this);
						if (ex != null)
						{
							return ex;
						}
					}
					typedValue = new XsdSimpleValue(st, obj);
					if (this.HasValueFacets)
					{
						ex = DatatypeImplementation.unionFacetsChecker.CheckValueFacets(typedValue, this);
						if (ex != null)
						{
							return ex;
						}
					}
					return null;
				}
				catch (FormatException ex)
				{
				}
				catch (InvalidCastException ex)
				{
				}
				catch (OverflowException ex)
				{
				}
				catch (ArgumentException ex)
				{
				}
				return ex;
			}
			ex = new XmlSchemaException("The value '{0}' is not valid according to any of the memberTypes of the union.", value.ToString());
			return ex;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x0012B28C File Offset: 0x0012948C
		// Note: this type is marked as 'beforefieldinit'.
		static Datatype_union()
		{
		}

		// Token: 0x04002789 RID: 10121
		private static readonly Type atomicValueType = typeof(object);

		// Token: 0x0400278A RID: 10122
		private static readonly Type listValueType = typeof(object[]);

		// Token: 0x0400278B RID: 10123
		private XmlSchemaSimpleType[] types;
	}
}
