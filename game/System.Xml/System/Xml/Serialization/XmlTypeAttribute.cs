using System;

namespace System.Xml.Serialization
{
	/// <summary>Controls the XML schema that is generated when the attribute target is serialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	// Token: 0x02000309 RID: 777
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public class XmlTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> class.</summary>
		// Token: 0x06002053 RID: 8275 RVA: 0x000D0BBE File Offset: 0x000CEDBE
		public XmlTypeAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> class and specifies the name of the XML type.</summary>
		/// <param name="typeName">The name of the XML type that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates when it serializes the class instance (and recognizes when it deserializes the class instance). </param>
		// Token: 0x06002054 RID: 8276 RVA: 0x000D0BCD File Offset: 0x000CEDCD
		public XmlTypeAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		/// <summary>Gets or sets a value that determines whether the resulting schema type is an XSD anonymous type.</summary>
		/// <returns>
		///     <see langword="true" />, if the resulting schema type is an XSD anonymous type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x000D0BE3 File Offset: 0x000CEDE3
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x000D0BEB File Offset: 0x000CEDEB
		public bool AnonymousType
		{
			get
			{
				return this.anonymousType;
			}
			set
			{
				this.anonymousType = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to include the type in XML schema documents.</summary>
		/// <returns>
		///     <see langword="true" /> to include the type in XML schema documents; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x000D0BF4 File Offset: 0x000CEDF4
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x000D0BFC File Offset: 0x000CEDFC
		public bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		/// <summary>Gets or sets the name of the XML type.</summary>
		/// <returns>The name of the XML type.</returns>
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x000D0C05 File Offset: 0x000CEE05
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x000D0C1B File Offset: 0x000CEE1B
		public string TypeName
		{
			get
			{
				if (this.typeName != null)
				{
					return this.typeName;
				}
				return string.Empty;
			}
			set
			{
				this.typeName = value;
			}
		}

		/// <summary>Gets or sets the namespace of the XML type.</summary>
		/// <returns>The namespace of the XML type.</returns>
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x000D0C24 File Offset: 0x000CEE24
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x000D0C2C File Offset: 0x000CEE2C
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

		// Token: 0x04001B27 RID: 6951
		private bool includeInSchema = true;

		// Token: 0x04001B28 RID: 6952
		private bool anonymousType;

		// Token: 0x04001B29 RID: 6953
		private string ns;

		// Token: 0x04001B2A RID: 6954
		private string typeName;
	}
}
