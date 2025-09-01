using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000511 RID: 1297
	internal class Datatype_List : Datatype_anySimpleType
	{
		// Token: 0x060034BF RID: 13503 RVA: 0x0012AB20 File Offset: 0x00128D20
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			XmlSchemaType xmlSchemaType = null;
			XmlSchemaComplexType xmlSchemaComplexType = schemaType as XmlSchemaComplexType;
			XmlSchemaSimpleType xmlSchemaSimpleType;
			if (xmlSchemaComplexType != null)
			{
				do
				{
					xmlSchemaSimpleType = (xmlSchemaComplexType.BaseXmlSchemaType as XmlSchemaSimpleType);
					if (xmlSchemaSimpleType != null)
					{
						break;
					}
					xmlSchemaComplexType = (xmlSchemaComplexType.BaseXmlSchemaType as XmlSchemaComplexType);
					if (xmlSchemaComplexType == null)
					{
						break;
					}
				}
				while (xmlSchemaComplexType != XmlSchemaComplexType.AnyType);
			}
			else
			{
				xmlSchemaSimpleType = (schemaType as XmlSchemaSimpleType);
			}
			if (xmlSchemaSimpleType != null)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList;
				for (;;)
				{
					xmlSchemaSimpleTypeList = (xmlSchemaSimpleType.Content as XmlSchemaSimpleTypeList);
					if (xmlSchemaSimpleTypeList != null)
					{
						break;
					}
					xmlSchemaSimpleType = (xmlSchemaSimpleType.BaseXmlSchemaType as XmlSchemaSimpleType);
					if (xmlSchemaSimpleType == null || xmlSchemaSimpleType == DatatypeImplementation.AnySimpleType)
					{
						goto IL_6D;
					}
				}
				xmlSchemaType = xmlSchemaSimpleTypeList.BaseItemType;
			}
			IL_6D:
			if (xmlSchemaType == null)
			{
				xmlSchemaType = DatatypeImplementation.GetSimpleTypeFromTypeCode(schemaType.Datatype.TypeCode);
			}
			return XmlListConverter.Create(xmlSchemaType.ValueConverter);
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x0012ABB9 File Offset: 0x00128DB9
		internal Datatype_List(DatatypeImplementation type) : this(type, 0)
		{
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x0012ABC3 File Offset: 0x00128DC3
		internal Datatype_List(DatatypeImplementation type, int minListSize)
		{
			this.itemType = type;
			this.minListSize = minListSize;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x0012ABDC File Offset: 0x00128DDC
		internal override int Compare(object value1, object value2)
		{
			Array array = (Array)value1;
			Array array2 = (Array)value2;
			if (array.Length != array2.Length)
			{
				return -1;
			}
			XmlAtomicValue[] array3 = array as XmlAtomicValue[];
			if (array3 != null)
			{
				XmlAtomicValue[] array4 = array2 as XmlAtomicValue[];
				for (int i = 0; i < array3.Length; i++)
				{
					XmlSchemaType xmlType = array3[i].XmlType;
					if (xmlType != array4[i].XmlType || !xmlType.Datatype.IsEqual(array3[i].TypedValue, array4[i].TypedValue))
					{
						return -1;
					}
				}
				return 0;
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (this.itemType.Compare(array.GetValue(j), array2.GetValue(j)) != 0)
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x0012AC9B File Offset: 0x00128E9B
		public override Type ValueType
		{
			get
			{
				return this.ListValueType;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x0012ACA3 File Offset: 0x00128EA3
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return this.itemType.TokenizedType;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x0012ACB0 File Offset: 0x00128EB0
		internal override Type ListValueType
		{
			get
			{
				return this.itemType.ListValueType;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x0012ACBD File Offset: 0x00128EBD
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.listFacetsChecker;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x0012ACC4 File Offset: 0x00128EC4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return this.itemType.TypeCode;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x0012ACD1 File Offset: 0x00128ED1
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x0012ACD5 File Offset: 0x00128ED5
		internal DatatypeImplementation ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x0012ACE0 File Offset: 0x00128EE0
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text = value as string;
			typedValue = null;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, namespaceResolver, out typedValue);
			}
			Exception ex;
			try
			{
				object obj = this.ValueConverter.ChangeType(value, this.ValueType, namespaceResolver);
				Array array = obj as Array;
				bool hasLexicalFacets = this.itemType.HasLexicalFacets;
				bool hasValueFacets = this.itemType.HasValueFacets;
				FacetsChecker facetsChecker = this.itemType.FacetsChecker;
				XmlValueConverter valueConverter = this.itemType.ValueConverter;
				for (int i = 0; i < array.Length; i++)
				{
					object value2 = array.GetValue(i);
					if (hasLexicalFacets)
					{
						string text2 = (string)valueConverter.ChangeType(value2, typeof(string), namespaceResolver);
						ex = facetsChecker.CheckLexicalFacets(ref text2, this.itemType);
						if (ex != null)
						{
							return ex;
						}
					}
					if (hasValueFacets)
					{
						ex = facetsChecker.CheckValueFacets(value2, this.itemType);
						if (ex != null)
						{
							return ex;
						}
					}
				}
				if (this.HasLexicalFacets)
				{
					string text3 = (string)this.ValueConverter.ChangeType(obj, typeof(string), namespaceResolver);
					ex = DatatypeImplementation.listFacetsChecker.CheckLexicalFacets(ref text3, this);
					if (ex != null)
					{
						return ex;
					}
				}
				if (this.HasValueFacets)
				{
					ex = DatatypeImplementation.listFacetsChecker.CheckValueFacets(obj, this);
					if (ex != null)
					{
						return ex;
					}
				}
				typedValue = obj;
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

		// Token: 0x060034CB RID: 13515 RVA: 0x0012AEA4 File Offset: 0x001290A4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.listFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ArrayList arrayList = new ArrayList();
				object obj2;
				if (this.itemType.Variety == XmlSchemaDatatypeVariety.Union)
				{
					string[] array = XmlConvert.SplitString(s);
					for (int i = 0; i < array.Length; i++)
					{
						object obj;
						ex = this.itemType.TryParseValue(array[i], nameTable, nsmgr, out obj);
						if (ex != null)
						{
							return ex;
						}
						XsdSimpleValue xsdSimpleValue = (XsdSimpleValue)obj;
						arrayList.Add(new XmlAtomicValue(xsdSimpleValue.XmlType, xsdSimpleValue.TypedValue, nsmgr));
					}
					obj2 = arrayList.ToArray(typeof(XmlAtomicValue));
				}
				else
				{
					string[] array2 = XmlConvert.SplitString(s);
					for (int j = 0; j < array2.Length; j++)
					{
						ex = this.itemType.TryParseValue(array2[j], nameTable, nsmgr, out typedValue);
						if (ex != null)
						{
							return ex;
						}
						arrayList.Add(typedValue);
					}
					obj2 = arrayList.ToArray(this.itemType.ValueType);
				}
				if (arrayList.Count < this.minListSize)
				{
					return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
				}
				ex = DatatypeImplementation.listFacetsChecker.CheckValueFacets(obj2, this);
				if (ex == null)
				{
					typedValue = obj2;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x04002787 RID: 10119
		private DatatypeImplementation itemType;

		// Token: 0x04002788 RID: 10120
		private int minListSize;
	}
}
