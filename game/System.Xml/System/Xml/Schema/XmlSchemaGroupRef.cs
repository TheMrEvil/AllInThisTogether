using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the <see langword="group" /> element with <see langword="ref" /> attribute from the XML Schema as specified by the World Wide Web Consortium (W3C). This class is used within complex types that reference a <see langword="group" /> defined at the <see langword="schema" /> level.</summary>
	// Token: 0x020005C0 RID: 1472
	public class XmlSchemaGroupRef : XmlSchemaParticle
	{
		/// <summary>Gets or sets the name of a group defined in this schema (or another schema indicated by the specified namespace).</summary>
		/// <returns>The name of a group defined in this schema.</returns>
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x0014E171 File Offset: 0x0014C371
		// (set) Token: 0x06003B31 RID: 15153 RVA: 0x0014E179 File Offset: 0x0014C379
		[XmlAttribute("ref")]
		public XmlQualifiedName RefName
		{
			get
			{
				return this.refName;
			}
			set
			{
				this.refName = ((value == null) ? XmlQualifiedName.Empty : value);
			}
		}

		/// <summary>Gets one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes, which holds the post-compilation value of the <see langword="Particle" /> property.</summary>
		/// <returns>The post-compilation value of the <see langword="Particle" /> property, which is one of the <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaAll" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" /> classes.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x0014E192 File Offset: 0x0014C392
		[XmlIgnore]
		public XmlSchemaGroupBase Particle
		{
			get
			{
				return this.particle;
			}
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x0014E19A File Offset: 0x0014C39A
		internal void SetParticle(XmlSchemaGroupBase value)
		{
			this.particle = value;
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x0014E1A3 File Offset: 0x0014C3A3
		// (set) Token: 0x06003B35 RID: 15157 RVA: 0x0014E1AB File Offset: 0x0014C3AB
		[XmlIgnore]
		internal XmlSchemaGroup Redefined
		{
			get
			{
				return this.refined;
			}
			set
			{
				this.refined = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" /> class.</summary>
		// Token: 0x06003B36 RID: 15158 RVA: 0x0014E1B4 File Offset: 0x0014C3B4
		public XmlSchemaGroupRef()
		{
		}

		// Token: 0x04002B68 RID: 11112
		private XmlQualifiedName refName = XmlQualifiedName.Empty;

		// Token: 0x04002B69 RID: 11113
		private XmlSchemaGroupBase particle;

		// Token: 0x04002B6A RID: 11114
		private XmlSchemaGroup refined;
	}
}
