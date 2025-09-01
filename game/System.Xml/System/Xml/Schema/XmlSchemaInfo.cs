using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the post-schema-validation infoset of a validated XML node.</summary>
	// Token: 0x020005C8 RID: 1480
	public class XmlSchemaInfo : IXmlSchemaInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> class.</summary>
		// Token: 0x06003B55 RID: 15189 RVA: 0x0014E2E0 File Offset: 0x0014C4E0
		public XmlSchemaInfo()
		{
			this.Clear();
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x0014E2EE File Offset: 0x0014C4EE
		internal XmlSchemaInfo(XmlSchemaValidity validity) : this()
		{
			this.validity = validity;
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value.</returns>
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x0014E2FD File Offset: 0x0014C4FD
		// (set) Token: 0x06003B58 RID: 15192 RVA: 0x0014E305 File Offset: 0x0014C505
		public XmlSchemaValidity Validity
		{
			get
			{
				return this.validity;
			}
			set
			{
				this.validity = value;
			}
		}

		/// <summary>Gets or sets a value indicating if this validated XML node was set as the result of a default being applied during XML Schema Definition Language (XSD) schema validation.</summary>
		/// <returns>A <see langword="bool" /> value.</returns>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x0014E30E File Offset: 0x0014C50E
		// (set) Token: 0x06003B5A RID: 15194 RVA: 0x0014E316 File Offset: 0x0014C516
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
			set
			{
				this.isDefault = value;
			}
		}

		/// <summary>Gets or sets a value indicating if the value for this validated XML node is nil.</summary>
		/// <returns>A <see langword="bool" /> value.</returns>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x0014E31F File Offset: 0x0014C51F
		// (set) Token: 0x06003B5C RID: 15196 RVA: 0x0014E327 File Offset: 0x0014C527
		public bool IsNil
		{
			get
			{
				return this.isNil;
			}
			set
			{
				this.isNil = value;
			}
		}

		/// <summary>Gets or sets the dynamic schema type for this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object.</returns>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x0014E330 File Offset: 0x0014C530
		// (set) Token: 0x06003B5E RID: 15198 RVA: 0x0014E338 File Offset: 0x0014C538
		public XmlSchemaSimpleType MemberType
		{
			get
			{
				return this.memberType;
			}
			set
			{
				this.memberType = value;
			}
		}

		/// <summary>Gets or sets the static XML Schema Definition Language (XSD) schema type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</returns>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x0014E341 File Offset: 0x0014C541
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x0014E349 File Offset: 0x0014C549
		public XmlSchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
				if (this.schemaType != null)
				{
					this.contentType = this.schemaType.SchemaContentType;
					return;
				}
				this.contentType = XmlSchemaContentType.Empty;
			}
		}

		/// <summary>Gets or sets the compiled <see cref="T:System.Xml.Schema.XmlSchemaElement" /> object that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> object.</returns>
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x0014E373 File Offset: 0x0014C573
		// (set) Token: 0x06003B62 RID: 15202 RVA: 0x0014E37B File Offset: 0x0014C57B
		public XmlSchemaElement SchemaElement
		{
			get
			{
				return this.schemaElement;
			}
			set
			{
				this.schemaElement = value;
				if (value != null)
				{
					this.schemaAttribute = null;
				}
			}
		}

		/// <summary>Gets or sets the compiled <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> object that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> object.</returns>
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x0014E38E File Offset: 0x0014C58E
		// (set) Token: 0x06003B64 RID: 15204 RVA: 0x0014E396 File Offset: 0x0014C596
		public XmlSchemaAttribute SchemaAttribute
		{
			get
			{
				return this.schemaAttribute;
			}
			set
			{
				this.schemaAttribute = value;
				if (value != null)
				{
					this.schemaElement = null;
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaContentType" /> object that corresponds to the content type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaContentType" /> object.</returns>
		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x0014E3A9 File Offset: 0x0014C5A9
		// (set) Token: 0x06003B66 RID: 15206 RVA: 0x0014E3B1 File Offset: 0x0014C5B1
		public XmlSchemaContentType ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x0014E3BA File Offset: 0x0014C5BA
		internal XmlSchemaType XmlType
		{
			get
			{
				if (this.memberType != null)
				{
					return this.memberType;
				}
				return this.schemaType;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x0014E3D1 File Offset: 0x0014C5D1
		internal bool HasDefaultValue
		{
			get
			{
				return this.schemaElement != null && this.schemaElement.ElementDecl.DefaultValueTyped != null;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x0014E3F0 File Offset: 0x0014C5F0
		internal bool IsUnionType
		{
			get
			{
				return this.schemaType != null && this.schemaType.Datatype != null && this.schemaType.Datatype.Variety == XmlSchemaDatatypeVariety.Union;
			}
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x0014E41C File Offset: 0x0014C61C
		internal void Clear()
		{
			this.isNil = false;
			this.isDefault = false;
			this.schemaType = null;
			this.schemaElement = null;
			this.schemaAttribute = null;
			this.memberType = null;
			this.validity = XmlSchemaValidity.NotKnown;
			this.contentType = XmlSchemaContentType.Empty;
		}

		// Token: 0x04002B75 RID: 11125
		private bool isDefault;

		// Token: 0x04002B76 RID: 11126
		private bool isNil;

		// Token: 0x04002B77 RID: 11127
		private XmlSchemaElement schemaElement;

		// Token: 0x04002B78 RID: 11128
		private XmlSchemaAttribute schemaAttribute;

		// Token: 0x04002B79 RID: 11129
		private XmlSchemaType schemaType;

		// Token: 0x04002B7A RID: 11130
		private XmlSchemaSimpleType memberType;

		// Token: 0x04002B7B RID: 11131
		private XmlSchemaValidity validity;

		// Token: 0x04002B7C RID: 11132
		private XmlSchemaContentType contentType;
	}
}
