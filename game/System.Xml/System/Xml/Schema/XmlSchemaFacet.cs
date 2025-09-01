using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Abstract class for all facets that are used when simple types are derived by restriction.</summary>
	// Token: 0x020005AF RID: 1455
	public abstract class XmlSchemaFacet : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the <see langword="value" /> attribute of the facet.</summary>
		/// <returns>The value attribute.</returns>
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x0014DF90 File Offset: 0x0014C190
		// (set) Token: 0x06003B09 RID: 15113 RVA: 0x0014DF98 File Offset: 0x0014C198
		[XmlAttribute("value")]
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>Gets or sets information that indicates that this facet is fixed.</summary>
		/// <returns>If <see langword="true" />, value is fixed; otherwise, <see langword="false" />. The default is <see langword="false" />.Optional.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x0014DFA1 File Offset: 0x0014C1A1
		// (set) Token: 0x06003B0B RID: 15115 RVA: 0x0014DFA9 File Offset: 0x0014C1A9
		[XmlAttribute("fixed")]
		[DefaultValue(false)]
		public virtual bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				if (!(this is XmlSchemaEnumerationFacet) && !(this is XmlSchemaPatternFacet))
				{
					this.isFixed = value;
				}
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06003B0C RID: 15116 RVA: 0x0014DFC2 File Offset: 0x0014C1C2
		// (set) Token: 0x06003B0D RID: 15117 RVA: 0x0014DFCA File Offset: 0x0014C1CA
		internal FacetType FacetType
		{
			get
			{
				return this.facetType;
			}
			set
			{
				this.facetType = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaFacet" /> class.</summary>
		// Token: 0x06003B0E RID: 15118 RVA: 0x0014BECD File Offset: 0x0014A0CD
		protected XmlSchemaFacet()
		{
		}

		// Token: 0x04002B5B RID: 11099
		private string value;

		// Token: 0x04002B5C RID: 11100
		private bool isFixed;

		// Token: 0x04002B5D RID: 11101
		private FacetType facetType;
	}
}
