using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Class for the identity constraints: <see langword="key" />, <see langword="keyref" />, and <see langword="unique" /> elements.</summary>
	// Token: 0x020005C1 RID: 1473
	public class XmlSchemaIdentityConstraint : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the name of the identity constraint.</summary>
		/// <returns>The name of the identity constraint.</returns>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06003B37 RID: 15159 RVA: 0x0014E1C7 File Offset: 0x0014C3C7
		// (set) Token: 0x06003B38 RID: 15160 RVA: 0x0014E1CF File Offset: 0x0014C3CF
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

		/// <summary>Gets or sets the XPath expression <see langword="selector" /> element.</summary>
		/// <returns>The XPath expression <see langword="selector" /> element.</returns>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06003B39 RID: 15161 RVA: 0x0014E1D8 File Offset: 0x0014C3D8
		// (set) Token: 0x06003B3A RID: 15162 RVA: 0x0014E1E0 File Offset: 0x0014C3E0
		[XmlElement("selector", typeof(XmlSchemaXPath))]
		public XmlSchemaXPath Selector
		{
			get
			{
				return this.selector;
			}
			set
			{
				this.selector = value;
			}
		}

		/// <summary>Gets the collection of fields that apply as children for the XML Path Language (XPath) expression selector.</summary>
		/// <returns>The collection of fields.</returns>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06003B3B RID: 15163 RVA: 0x0014E1E9 File Offset: 0x0014C3E9
		[XmlElement("field", typeof(XmlSchemaXPath))]
		public XmlSchemaObjectCollection Fields
		{
			get
			{
				return this.fields;
			}
		}

		/// <summary>Gets the qualified name of the identity constraint, which holds the post-compilation value of the <see langword="QualifiedName" /> property.</summary>
		/// <returns>The post-compilation value of the <see langword="QualifiedName" /> property.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x0014E1F1 File Offset: 0x0014C3F1
		[XmlIgnore]
		public XmlQualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x0014E1F9 File Offset: 0x0014C3F9
		internal void SetQualifiedName(XmlQualifiedName value)
		{
			this.qualifiedName = value;
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x0014E202 File Offset: 0x0014C402
		// (set) Token: 0x06003B3F RID: 15167 RVA: 0x0014E20A File Offset: 0x0014C40A
		[XmlIgnore]
		internal CompiledIdentityConstraint CompiledConstraint
		{
			get
			{
				return this.compiledConstraint;
			}
			set
			{
				this.compiledConstraint = value;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x0014E213 File Offset: 0x0014C413
		// (set) Token: 0x06003B41 RID: 15169 RVA: 0x0014E21B File Offset: 0x0014C41B
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaIdentityConstraint" /> class.</summary>
		// Token: 0x06003B42 RID: 15170 RVA: 0x0014E224 File Offset: 0x0014C424
		public XmlSchemaIdentityConstraint()
		{
		}

		// Token: 0x04002B6B RID: 11115
		private string name;

		// Token: 0x04002B6C RID: 11116
		private XmlSchemaXPath selector;

		// Token: 0x04002B6D RID: 11117
		private XmlSchemaObjectCollection fields = new XmlSchemaObjectCollection();

		// Token: 0x04002B6E RID: 11118
		private XmlQualifiedName qualifiedName = XmlQualifiedName.Empty;

		// Token: 0x04002B6F RID: 11119
		private CompiledIdentityConstraint compiledConstraint;
	}
}
