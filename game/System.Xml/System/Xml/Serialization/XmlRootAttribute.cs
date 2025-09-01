using System;

namespace System.Xml.Serialization
{
	/// <summary>Controls XML serialization of the attribute target as an XML root element.</summary>
	// Token: 0x020002E0 RID: 736
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.ReturnValue)]
	public class XmlRootAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> class.</summary>
		// Token: 0x06001C8F RID: 7311 RVA: 0x000A3B10 File Offset: 0x000A1D10
		public XmlRootAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> class and specifies the name of the XML root element.</summary>
		/// <param name="elementName">The name of the XML root element. </param>
		// Token: 0x06001C90 RID: 7312 RVA: 0x000A3B1F File Offset: 0x000A1D1F
		public XmlRootAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Gets or sets the name of the XML element that is generated and recognized by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class's <see cref="M:System.Xml.Serialization.XmlSerializer.Serialize(System.IO.TextWriter,System.Object)" /> and <see cref="M:System.Xml.Serialization.XmlSerializer.Deserialize(System.IO.Stream)" /> methods, respectively.</summary>
		/// <returns>The name of the XML root element that is generated and recognized in an XML-document instance. The default is the name of the serialized class.</returns>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x000A3B35 File Offset: 0x000A1D35
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x000A3B4B File Offset: 0x000A1D4B
		public string ElementName
		{
			get
			{
				if (this.elementName != null)
				{
					return this.elementName;
				}
				return string.Empty;
			}
			set
			{
				this.elementName = value;
			}
		}

		/// <summary>Gets or sets the namespace for the XML root element.</summary>
		/// <returns>The namespace for the XML element.</returns>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x000A3B54 File Offset: 0x000A1D54
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x000A3B5C File Offset: 0x000A1D5C
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets the XSD data type of the XML root element.</summary>
		/// <returns>An XSD (XML Schema Document) data type, as defined by the World Wide Web Consortium (www.w3.org) document named "XML Schema: DataTypes".</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x000A3B65 File Offset: 0x000A1D65
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x000A3B7B File Offset: 0x000A1D7B
		public string DataType
		{
			get
			{
				if (this.dataType != null)
				{
					return this.dataType;
				}
				return string.Empty;
			}
			set
			{
				this.dataType = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member that is set to <see langword="null" /> into the <see langword="xsi:nil" /> attribute set to <see langword="true" />.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the <see langword="xsi:nil" /> attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x000A3B84 File Offset: 0x000A1D84
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x000A3B8C File Offset: 0x000A1D8C
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
				this.nullableSpecified = true;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x000A3B9C File Offset: 0x000A1D9C
		internal bool IsNullableSpecified
		{
			get
			{
				return this.nullableSpecified;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x000A3BA4 File Offset: 0x000A1DA4
		internal string Key
		{
			get
			{
				return string.Concat(new string[]
				{
					(this.ns == null) ? string.Empty : this.ns,
					":",
					this.ElementName,
					":",
					this.nullable.ToString()
				});
			}
		}

		// Token: 0x04001A23 RID: 6691
		private string elementName;

		// Token: 0x04001A24 RID: 6692
		private string ns;

		// Token: 0x04001A25 RID: 6693
		private string dataType;

		// Token: 0x04001A26 RID: 6694
		private bool nullable = true;

		// Token: 0x04001A27 RID: 6695
		private bool nullableSpecified;
	}
}
