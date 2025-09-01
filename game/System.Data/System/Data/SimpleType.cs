using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	internal sealed class SimpleType : ISerializable
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x0004535C File Offset: 0x0004355C
		internal SimpleType(string baseType)
		{
			this._baseType = baseType;
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000453E4 File Offset: 0x000435E4
		internal SimpleType(XmlSchemaSimpleType node)
		{
			this._name = node.Name;
			this._ns = ((node.QualifiedName != null) ? node.QualifiedName.Namespace : "");
			this.LoadTypeValues(node);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0004549D File Offset: 0x0004369D
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000454A4 File Offset: 0x000436A4
		internal void LoadTypeValues(XmlSchemaSimpleType node)
		{
			if (node.Content is XmlSchemaSimpleTypeList || node.Content is XmlSchemaSimpleTypeUnion)
			{
				throw ExceptionBuilder.SimpleTypeNotSupported();
			}
			if (node.Content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)node.Content;
				XmlSchemaSimpleType xmlSchemaSimpleType = node.BaseXmlSchemaType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.QualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					this._baseSimpleType = new SimpleType(node.BaseXmlSchemaType as XmlSchemaSimpleType);
				}
				if (xmlSchemaSimpleTypeRestriction.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.Name;
				}
				else
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString();
				}
				if (this._baseSimpleType != null && this._baseSimpleType.Name != null && this._baseSimpleType.Name.Length > 0)
				{
					this._xmlBaseType = this._baseSimpleType.XmlBaseType;
				}
				else
				{
					this._xmlBaseType = xmlSchemaSimpleTypeRestriction.BaseTypeName;
				}
				if (this._baseType == null || this._baseType.Length == 0)
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseType.Name;
					this._xmlBaseType = null;
				}
				if (this._baseType == "NOTATION")
				{
					this._baseType = "string";
				}
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaSimpleTypeRestriction.Facets)
				{
					XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
					if (xmlSchemaFacet is XmlSchemaLengthFacet)
					{
						this._length = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMinLengthFacet)
					{
						this._minLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMaxLengthFacet)
					{
						this._maxLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaPatternFacet)
					{
						this._pattern = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaEnumerationFacet)
					{
						this._enumeration = ((!string.IsNullOrEmpty(this._enumeration)) ? (this._enumeration + " " + xmlSchemaFacet.Value) : xmlSchemaFacet.Value);
					}
					if (xmlSchemaFacet is XmlSchemaMinExclusiveFacet)
					{
						this._minExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMinInclusiveFacet)
					{
						this._minInclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxExclusiveFacet)
					{
						this._maxExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxInclusiveFacet)
					{
						this._maxInclusive = xmlSchemaFacet.Value;
					}
				}
			}
			string msdataAttribute = XSDSchema.GetMsdataAttribute(node, "targetNamespace");
			if (msdataAttribute != null)
			{
				this._ns = msdataAttribute;
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00045764 File Offset: 0x00043964
		internal bool IsPlainString()
		{
			return XSDSchema.QualifiedName(this._baseType) == XSDSchema.QualifiedName("string") && string.IsNullOrEmpty(this._name) && this._length == -1 && this._minLength == -1 && this._maxLength == -1 && string.IsNullOrEmpty(this._pattern) && string.IsNullOrEmpty(this._maxExclusive) && string.IsNullOrEmpty(this._maxInclusive) && string.IsNullOrEmpty(this._minExclusive) && string.IsNullOrEmpty(this._minInclusive) && string.IsNullOrEmpty(this._enumeration);
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00045803 File Offset: 0x00043A03
		internal string BaseType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0004580B File Offset: 0x00043A0B
		internal XmlQualifiedName XmlBaseType
		{
			get
			{
				return this._xmlBaseType;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00045813 File Offset: 0x00043A13
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x0004581B File Offset: 0x00043A1B
		internal string Namespace
		{
			get
			{
				return this._ns;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00045823 File Offset: 0x00043A23
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0004582B File Offset: 0x00043A2B
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x00045833 File Offset: 0x00043A33
		internal int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				this._maxLength = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0004583C File Offset: 0x00043A3C
		internal SimpleType BaseSimpleType
		{
			get
			{
				return this._baseSimpleType;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x00045844 File Offset: 0x00043A44
		public string SimpleTypeQualifiedName
		{
			get
			{
				if (this._ns.Length == 0)
				{
					return this._name;
				}
				return this._ns + ":" + this._name;
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00045870 File Offset: 0x00043A70
		internal string QualifiedName(string name)
		{
			if (name.IndexOf(':') == -1)
			{
				return "xs:" + name;
			}
			return name;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0004588C File Offset: 0x00043A8C
		internal XmlNode ToNode(XmlDocument dc, Hashtable prefixes, bool inRemoting)
		{
			XmlElement xmlElement = dc.CreateElement("xs", "simpleType", "http://www.w3.org/2001/XMLSchema");
			if (this._name != null && this._name.Length != 0)
			{
				xmlElement.SetAttribute("name", this._name);
				if (inRemoting)
				{
					xmlElement.SetAttribute("targetNamespace", "urn:schemas-microsoft-com:xml-msdata", this.Namespace);
				}
			}
			XmlElement xmlElement2 = dc.CreateElement("xs", "restriction", "http://www.w3.org/2001/XMLSchema");
			if (!inRemoting)
			{
				if (this._baseSimpleType != null)
				{
					if (this._baseSimpleType.Namespace != null && this._baseSimpleType.Namespace.Length > 0)
					{
						string text = (prefixes != null) ? ((string)prefixes[this._baseSimpleType.Namespace]) : null;
						if (text != null)
						{
							xmlElement2.SetAttribute("base", text + ":" + this._baseSimpleType.Name);
						}
						else
						{
							xmlElement2.SetAttribute("base", this._baseSimpleType.Name);
						}
					}
					else
					{
						xmlElement2.SetAttribute("base", this._baseSimpleType.Name);
					}
				}
				else
				{
					xmlElement2.SetAttribute("base", this.QualifiedName(this._baseType));
				}
			}
			else
			{
				xmlElement2.SetAttribute("base", (this._baseSimpleType != null) ? this._baseSimpleType.Name : this.QualifiedName(this._baseType));
			}
			if (this._length >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "length", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this._length.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			if (this._maxLength >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "maxLength", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this._maxLength.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00045A7B File Offset: 0x00043C7B
		internal static SimpleType CreateEnumeratedType(string values)
		{
			return new SimpleType("string")
			{
				_enumeration = values
			};
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00045A8E File Offset: 0x00043C8E
		internal static SimpleType CreateByteArrayType(string encoding)
		{
			return new SimpleType("base64Binary");
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00045A9A File Offset: 0x00043C9A
		internal static SimpleType CreateLimitedStringType(int length)
		{
			return new SimpleType("string")
			{
				_maxLength = length
			};
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00045AAD File Offset: 0x00043CAD
		internal static SimpleType CreateSimpleType(StorageType typeCode, Type type)
		{
			if (typeCode == StorageType.Char && type == typeof(char))
			{
				return new SimpleType("string")
				{
					_length = 1
				};
			}
			return null;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00045AD8 File Offset: 0x00043CD8
		internal string HasConflictingDefinition(SimpleType otherSimpleType)
		{
			if (otherSimpleType == null)
			{
				return "otherSimpleType";
			}
			if (this.MaxLength != otherSimpleType.MaxLength)
			{
				return "MaxLength";
			}
			if (!string.Equals(this.BaseType, otherSimpleType.BaseType, StringComparison.Ordinal))
			{
				return "BaseType";
			}
			if (this.BaseSimpleType != null && otherSimpleType.BaseSimpleType != null && this.BaseSimpleType.HasConflictingDefinition(otherSimpleType.BaseSimpleType).Length != 0)
			{
				return "BaseSimpleType";
			}
			return string.Empty;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00045B50 File Offset: 0x00043D50
		internal bool CanHaveMaxLength()
		{
			SimpleType simpleType = this;
			while (simpleType.BaseSimpleType != null)
			{
				simpleType = simpleType.BaseSimpleType;
			}
			return string.Equals(simpleType.BaseType, "string", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00045B84 File Offset: 0x00043D84
		internal void ConvertToAnnonymousSimpleType()
		{
			this._name = null;
			this._ns = string.Empty;
			SimpleType simpleType = this;
			while (simpleType._baseSimpleType != null)
			{
				simpleType = simpleType._baseSimpleType;
			}
			this._baseType = simpleType._baseType;
			this._baseSimpleType = simpleType._baseSimpleType;
			this._xmlBaseType = simpleType._xmlBaseType;
		}

		// Token: 0x04000A14 RID: 2580
		private string _baseType;

		// Token: 0x04000A15 RID: 2581
		private SimpleType _baseSimpleType;

		// Token: 0x04000A16 RID: 2582
		private XmlQualifiedName _xmlBaseType;

		// Token: 0x04000A17 RID: 2583
		private string _name = string.Empty;

		// Token: 0x04000A18 RID: 2584
		private int _length = -1;

		// Token: 0x04000A19 RID: 2585
		private int _minLength = -1;

		// Token: 0x04000A1A RID: 2586
		private int _maxLength = -1;

		// Token: 0x04000A1B RID: 2587
		private string _pattern = string.Empty;

		// Token: 0x04000A1C RID: 2588
		private string _ns = string.Empty;

		// Token: 0x04000A1D RID: 2589
		private string _maxExclusive = string.Empty;

		// Token: 0x04000A1E RID: 2590
		private string _maxInclusive = string.Empty;

		// Token: 0x04000A1F RID: 2591
		private string _minExclusive = string.Empty;

		// Token: 0x04000A20 RID: 2592
		private string _minInclusive = string.Empty;

		// Token: 0x04000A21 RID: 2593
		internal string _enumeration = string.Empty;
	}
}
