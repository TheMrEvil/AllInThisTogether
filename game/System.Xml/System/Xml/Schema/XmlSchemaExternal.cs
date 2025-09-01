using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>An abstract class. Provides information about the included schema.</summary>
	// Token: 0x020005AD RID: 1453
	public abstract class XmlSchemaExternal : XmlSchemaObject
	{
		/// <summary>Gets or sets the Uniform Resource Identifier (URI) location for the schema, which tells the schema processor where the schema physically resides.</summary>
		/// <returns>The URI location for the schema.Optional for imported schemas.</returns>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x0014DF19 File Offset: 0x0014C119
		// (set) Token: 0x06003AF9 RID: 15097 RVA: 0x0014DF21 File Offset: 0x0014C121
		[XmlAttribute("schemaLocation", DataType = "anyURI")]
		public string SchemaLocation
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		/// <summary>Gets or sets the <see langword="XmlSchema" /> for the referenced schema.</summary>
		/// <returns>The <see langword="XmlSchema" /> for the referenced schema.</returns>
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x0014DF2A File Offset: 0x0014C12A
		// (set) Token: 0x06003AFB RID: 15099 RVA: 0x0014DF32 File Offset: 0x0014C132
		[XmlIgnore]
		public XmlSchema Schema
		{
			get
			{
				return this.schema;
			}
			set
			{
				this.schema = value;
			}
		}

		/// <summary>Gets or sets the string id.</summary>
		/// <returns>The string id. The default is <see langword="String.Empty" />.Optional.</returns>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x0014DF3B File Offset: 0x0014C13B
		// (set) Token: 0x06003AFD RID: 15101 RVA: 0x0014DF43 File Offset: 0x0014C143
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets and sets the qualified attributes, which do not belong to the schema target namespace.</summary>
		/// <returns>Qualified attributes that belong to another target namespace.</returns>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x0014DF4C File Offset: 0x0014C14C
		// (set) Token: 0x06003AFF RID: 15103 RVA: 0x0014DF54 File Offset: 0x0014C154
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x0014DF5D File Offset: 0x0014C15D
		// (set) Token: 0x06003B01 RID: 15105 RVA: 0x0014DF65 File Offset: 0x0014C165
		[XmlIgnore]
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x0014DF6E File Offset: 0x0014C16E
		// (set) Token: 0x06003B03 RID: 15107 RVA: 0x0014DF76 File Offset: 0x0014C176
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x0014DF54 File Offset: 0x0014C154
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x0014DF7F File Offset: 0x0014C17F
		// (set) Token: 0x06003B06 RID: 15110 RVA: 0x0014DF87 File Offset: 0x0014C187
		internal Compositor Compositor
		{
			get
			{
				return this.compositor;
			}
			set
			{
				this.compositor = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaExternal" /> class.</summary>
		// Token: 0x06003B07 RID: 15111 RVA: 0x0014BB8E File Offset: 0x00149D8E
		protected XmlSchemaExternal()
		{
		}

		// Token: 0x04002B47 RID: 11079
		private string location;

		// Token: 0x04002B48 RID: 11080
		private Uri baseUri;

		// Token: 0x04002B49 RID: 11081
		private XmlSchema schema;

		// Token: 0x04002B4A RID: 11082
		private string id;

		// Token: 0x04002B4B RID: 11083
		private XmlAttribute[] moreAttributes;

		// Token: 0x04002B4C RID: 11084
		private Compositor compositor;
	}
}
