using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Xml.Schema
{
	/// <summary>The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> class is an abstract class for mapping XML Schema definition language (XSD) types to Common Language Runtime (CLR) types.</summary>
	// Token: 0x020005A8 RID: 1448
	public abstract class XmlSchemaDatatype
	{
		/// <summary>When overridden in a derived class, gets the Common Language Runtime (CLR) type of the item.</summary>
		/// <returns>The Common Language Runtime (CLR) type of the item.</returns>
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06003A82 RID: 14978
		public abstract Type ValueType { get; }

		/// <summary>When overridden in a derived class, gets the type for the <see langword="string" /> as specified in the World Wide Web Consortium (W3C) XML 1.0 specification.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlTokenizedType" /> value for the <see langword="string" />.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06003A83 RID: 14979
		public abstract XmlTokenizedType TokenizedType { get; }

		/// <summary>When overridden in a derived class, validates the <see langword="string" /> specified against a built-in or user-defined simple type.</summary>
		/// <param name="s">The <see langword="string" /> to validate against the simple type.</param>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> to use for atomization while parsing the <see langword="string" /> if this <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> object represents the xs:NCName type. </param>
		/// <param name="nsmgr">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object to use while parsing the <see langword="string" /> if this <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> object represents the xs:QName type.</param>
		/// <returns>An <see cref="T:System.Object" /> that can be cast safely to the type returned by the <see cref="P:System.Xml.Schema.XmlSchemaDatatype.ValueType" /> property.</returns>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The input value is not a valid instance of this W3C XML Schema type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value to parse cannot be <see langword="null" />.</exception>
		// Token: 0x06003A84 RID: 14980
		public abstract object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr);

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaDatatypeVariety" /> value for the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaDatatypeVariety" /> value for the simple type.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06003A85 RID: 14981 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual XmlSchemaDatatypeVariety Variety
		{
			get
			{
				return XmlSchemaDatatypeVariety.Atomic;
			}
		}

		/// <summary>Converts the value specified, whose type is one of the valid Common Language Runtime (CLR) representations of the XML schema type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />, to the CLR type specified.</summary>
		/// <param name="value">The input value to convert to the specified type.</param>
		/// <param name="targetType">The target type to convert the input value to.</param>
		/// <returns>The converted input value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Object" /> or <see cref="T:System.Type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />   does not support a conversion from type of the value specified to the type specified.</exception>
		// Token: 0x06003A86 RID: 14982 RVA: 0x0014D338 File Offset: 0x0014B538
		public virtual object ChangeType(object value, Type targetType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return this.ValueConverter.ChangeType(value, targetType);
		}

		/// <summary>Converts the value specified, whose type is one of the valid Common Language Runtime (CLR) representations of the XML schema type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />, to the CLR type specified using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> if the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> represents the xs:QName type or a type derived from it.</summary>
		/// <param name="value">The input value to convert to the specified type.</param>
		/// <param name="targetType">The target type to convert the input value to.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> used for resolving namespace prefixes. This is only of use if the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />  represents the xs:QName type or a type derived from it.</param>
		/// <returns>The converted input value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Object" /> or <see cref="T:System.Type" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />   does not support a conversion from type of the value specified to the type specified.</exception>
		// Token: 0x06003A87 RID: 14983 RVA: 0x0014D369 File Offset: 0x0014B569
		public virtual object ChangeType(object value, Type targetType, IXmlNamespaceResolver namespaceResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (namespaceResolver == null)
			{
				throw new ArgumentNullException("namespaceResolver");
			}
			return this.ValueConverter.ChangeType(value, targetType, namespaceResolver);
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlTypeCode" /> value for the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlTypeCode" /> value for the simple type.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.None;
			}
		}

		/// <summary>The <see cref="M:System.Xml.Schema.XmlSchemaDatatype.IsDerivedFrom(System.Xml.Schema.XmlSchemaDatatype)" /> method always returns <see langword="false" />.</summary>
		/// <param name="datatype">The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />.</param>
		/// <returns>Always returns <see langword="false" />.</returns>
		// Token: 0x06003A89 RID: 14985 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool IsDerivedFrom(XmlSchemaDatatype datatype)
		{
			return false;
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06003A8A RID: 14986
		internal abstract bool HasLexicalFacets { get; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06003A8B RID: 14987
		internal abstract bool HasValueFacets { get; }

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003A8C RID: 14988
		internal abstract XmlValueConverter ValueConverter { get; }

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06003A8D RID: 14989
		// (set) Token: 0x06003A8E RID: 14990
		internal abstract RestrictionFacets Restriction { get; set; }

		// Token: 0x06003A8F RID: 14991
		internal abstract int Compare(object value1, object value2);

		// Token: 0x06003A90 RID: 14992
		internal abstract object ParseValue(string s, Type typDest, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr);

		// Token: 0x06003A91 RID: 14993
		internal abstract object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, bool createAtomicValue);

		// Token: 0x06003A92 RID: 14994
		internal abstract Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue);

		// Token: 0x06003A93 RID: 14995
		internal abstract Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue);

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06003A94 RID: 14996
		internal abstract FacetsChecker FacetsChecker { get; }

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06003A95 RID: 14997
		internal abstract XmlSchemaWhiteSpace BuiltInWhitespaceFacet { get; }

		// Token: 0x06003A96 RID: 14998
		internal abstract XmlSchemaDatatype DeriveByRestriction(XmlSchemaObjectCollection facets, XmlNameTable nameTable, XmlSchemaType schemaType);

		// Token: 0x06003A97 RID: 14999
		internal abstract XmlSchemaDatatype DeriveByList(XmlSchemaType schemaType);

		// Token: 0x06003A98 RID: 15000
		internal abstract void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller);

		// Token: 0x06003A99 RID: 15001
		internal abstract bool IsEqual(object o1, object o2);

		// Token: 0x06003A9A RID: 15002
		internal abstract bool IsComparable(XmlSchemaDatatype dtype);

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06003A9B RID: 15003 RVA: 0x0014D3AC File Offset: 0x0014B5AC
		internal string TypeCodeString
		{
			get
			{
				string result = string.Empty;
				XmlTypeCode typeCode = this.TypeCode;
				switch (this.Variety)
				{
				case XmlSchemaDatatypeVariety.Atomic:
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						result = "anySimpleType";
					}
					else
					{
						result = this.TypeCodeToString(typeCode);
					}
					break;
				case XmlSchemaDatatypeVariety.List:
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						result = "List of Union";
					}
					else
					{
						result = "List of " + this.TypeCodeToString(typeCode);
					}
					break;
				case XmlSchemaDatatypeVariety.Union:
					result = "Union";
					break;
				}
				return result;
			}
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x0014D420 File Offset: 0x0014B620
		internal string TypeCodeToString(XmlTypeCode typeCode)
		{
			switch (typeCode)
			{
			case XmlTypeCode.None:
				return "None";
			case XmlTypeCode.Item:
				return "AnyType";
			case XmlTypeCode.AnyAtomicType:
				return "AnyAtomicType";
			case XmlTypeCode.String:
				return "String";
			case XmlTypeCode.Boolean:
				return "Boolean";
			case XmlTypeCode.Decimal:
				return "Decimal";
			case XmlTypeCode.Float:
				return "Float";
			case XmlTypeCode.Double:
				return "Double";
			case XmlTypeCode.Duration:
				return "Duration";
			case XmlTypeCode.DateTime:
				return "DateTime";
			case XmlTypeCode.Time:
				return "Time";
			case XmlTypeCode.Date:
				return "Date";
			case XmlTypeCode.GYearMonth:
				return "GYearMonth";
			case XmlTypeCode.GYear:
				return "GYear";
			case XmlTypeCode.GMonthDay:
				return "GMonthDay";
			case XmlTypeCode.GDay:
				return "GDay";
			case XmlTypeCode.GMonth:
				return "GMonth";
			case XmlTypeCode.HexBinary:
				return "HexBinary";
			case XmlTypeCode.Base64Binary:
				return "Base64Binary";
			case XmlTypeCode.AnyUri:
				return "AnyUri";
			case XmlTypeCode.QName:
				return "QName";
			case XmlTypeCode.Notation:
				return "Notation";
			case XmlTypeCode.NormalizedString:
				return "NormalizedString";
			case XmlTypeCode.Token:
				return "Token";
			case XmlTypeCode.Language:
				return "Language";
			case XmlTypeCode.NmToken:
				return "NmToken";
			case XmlTypeCode.Name:
				return "Name";
			case XmlTypeCode.NCName:
				return "NCName";
			case XmlTypeCode.Id:
				return "Id";
			case XmlTypeCode.Idref:
				return "Idref";
			case XmlTypeCode.Entity:
				return "Entity";
			case XmlTypeCode.Integer:
				return "Integer";
			case XmlTypeCode.NonPositiveInteger:
				return "NonPositiveInteger";
			case XmlTypeCode.NegativeInteger:
				return "NegativeInteger";
			case XmlTypeCode.Long:
				return "Long";
			case XmlTypeCode.Int:
				return "Int";
			case XmlTypeCode.Short:
				return "Short";
			case XmlTypeCode.Byte:
				return "Byte";
			case XmlTypeCode.NonNegativeInteger:
				return "NonNegativeInteger";
			case XmlTypeCode.UnsignedLong:
				return "UnsignedLong";
			case XmlTypeCode.UnsignedInt:
				return "UnsignedInt";
			case XmlTypeCode.UnsignedShort:
				return "UnsignedShort";
			case XmlTypeCode.UnsignedByte:
				return "UnsignedByte";
			case XmlTypeCode.PositiveInteger:
				return "PositiveInteger";
			}
			return typeCode.ToString();
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x0014D624 File Offset: 0x0014B824
		internal static string ConcatenatedToString(object value)
		{
			Type type = value.GetType();
			string result = string.Empty;
			if (type == typeof(IEnumerable) && type != typeof(string))
			{
				StringBuilder stringBuilder = new StringBuilder();
				IEnumerator enumerator = (value as IEnumerable).GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("{");
					object obj = enumerator.Current;
					if (obj is IFormattable)
					{
						stringBuilder.Append(((IFormattable)obj).ToString("", CultureInfo.InvariantCulture));
					}
					else
					{
						stringBuilder.Append(obj.ToString());
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(" , ");
						obj = enumerator.Current;
						if (obj is IFormattable)
						{
							stringBuilder.Append(((IFormattable)obj).ToString("", CultureInfo.InvariantCulture));
						}
						else
						{
							stringBuilder.Append(obj.ToString());
						}
					}
					stringBuilder.Append("}");
					result = stringBuilder.ToString();
				}
			}
			else if (value is IFormattable)
			{
				result = ((IFormattable)value).ToString("", CultureInfo.InvariantCulture);
			}
			else
			{
				result = value.ToString();
			}
			return result;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x0014D760 File Offset: 0x0014B960
		internal static XmlSchemaDatatype FromXmlTokenizedType(XmlTokenizedType token)
		{
			return DatatypeImplementation.FromXmlTokenizedType(token);
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x0014D768 File Offset: 0x0014B968
		internal static XmlSchemaDatatype FromXmlTokenizedTypeXsd(XmlTokenizedType token)
		{
			return DatatypeImplementation.FromXmlTokenizedTypeXsd(token);
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x0014D770 File Offset: 0x0014B970
		internal static XmlSchemaDatatype FromXdrName(string name)
		{
			return DatatypeImplementation.FromXdrName(name);
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x0014D778 File Offset: 0x0014B978
		internal static XmlSchemaDatatype DeriveByUnion(XmlSchemaSimpleType[] types, XmlSchemaType schemaType)
		{
			return DatatypeImplementation.DeriveByUnion(types, schemaType);
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x0014D784 File Offset: 0x0014B984
		internal static string XdrCanonizeUri(string uri, XmlNameTable nameTable, SchemaNames schemaNames)
		{
			int num = 5;
			bool flag = false;
			if (uri.Length > 5 && uri.StartsWith("uuid:", StringComparison.Ordinal))
			{
				flag = true;
			}
			else if (uri.Length > 9 && uri.StartsWith("urn:uuid:", StringComparison.Ordinal))
			{
				flag = true;
				num = 9;
			}
			string text;
			if (flag)
			{
				text = nameTable.Add(uri.Substring(0, num) + uri.Substring(num, uri.Length - num).ToUpper(CultureInfo.InvariantCulture));
			}
			else
			{
				text = uri;
			}
			if (Ref.Equal(schemaNames.NsDataTypeAlias, text) || Ref.Equal(schemaNames.NsDataTypeOld, text))
			{
				text = schemaNames.NsDataType;
			}
			else if (Ref.Equal(schemaNames.NsXdrAlias, text))
			{
				text = schemaNames.NsXdr;
			}
			return text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> class.</summary>
		// Token: 0x06003AA3 RID: 15011 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlSchemaDatatype()
		{
		}
	}
}
