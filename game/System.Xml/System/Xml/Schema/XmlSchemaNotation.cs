using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="notation" /> element from XML Schema as specified by the World Wide Web Consortium (W3C). An XML Schema <see langword="notation" /> declaration is a reconstruction of <see langword="XML 1.0 NOTATION" /> declarations. The purpose of notations is to describe the format of non-XML data within an XML document.</summary>
	// Token: 0x020005C9 RID: 1481
	public class XmlSchemaNotation : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the notation.</summary>
		/// <returns>The name of the notation.</returns>
		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x0014E456 File Offset: 0x0014C656
		// (set) Token: 0x06003B6C RID: 15212 RVA: 0x0014E45E File Offset: 0x0014C65E
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the <see langword="public" /> identifier.</summary>
		/// <returns>The <see langword="public" /> identifier. The value must be a valid Uniform Resource Identifier (URI).</returns>
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x0014E467 File Offset: 0x0014C667
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x0014E46F File Offset: 0x0014C66F
		[XmlAttribute("public")]
		public string Public
		{
			get
			{
				return this.publicId;
			}
			set
			{
				this.publicId = value;
			}
		}

		/// <summary>Gets or sets the <see langword="system" /> identifier.</summary>
		/// <returns>The <see langword="system" /> identifier. The value must be a valid URI.</returns>
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x0014E478 File Offset: 0x0014C678
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x0014E480 File Offset: 0x0014C680
		[XmlAttribute("system")]
		public string System
		{
			get
			{
				return this.systemId;
			}
			set
			{
				this.systemId = value;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x0014E489 File Offset: 0x0014C689
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x0014E491 File Offset: 0x0014C691
		[XmlIgnore]
		internal XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qname;
			}
			set
			{
				this.qname = value;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x0014E49A File Offset: 0x0014C69A
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x0014E4A2 File Offset: 0x0014C6A2
		[XmlIgnore]
		internal override string NameAttribute
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaNotation" /> class.</summary>
		// Token: 0x06003B75 RID: 15221 RVA: 0x0014E4AB File Offset: 0x0014C6AB
		public XmlSchemaNotation()
		{
		}

		// Token: 0x04002B7D RID: 11133
		private string name;

		// Token: 0x04002B7E RID: 11134
		private string publicId;

		// Token: 0x04002B7F RID: 11135
		private string systemId;

		// Token: 0x04002B80 RID: 11136
		private XmlQualifiedName qname = XmlQualifiedName.Empty;
	}
}
