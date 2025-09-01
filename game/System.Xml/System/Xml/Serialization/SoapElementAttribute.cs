using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the public member value be serialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> as an encoded SOAP XML element.</summary>
	// Token: 0x020002AF RID: 687
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class SoapElementAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> class.</summary>
		// Token: 0x060019E6 RID: 6630 RVA: 0x000021EA File Offset: 0x000003EA
		public SoapElementAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapElementAttribute" /> class and specifies the name of the XML element.</summary>
		/// <param name="elementName">The XML element name of the serialized member. </param>
		// Token: 0x060019E7 RID: 6631 RVA: 0x00094CC9 File Offset: 0x00092EC9
		public SoapElementAttribute(string elementName)
		{
			this.elementName = elementName;
		}

		/// <summary>Gets or sets the name of the generated XML element.</summary>
		/// <returns>The name of the generated XML element. The default is the member identifier.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00094CD8 File Offset: 0x00092ED8
		// (set) Token: 0x060019E9 RID: 6633 RVA: 0x00094CEE File Offset: 0x00092EEE
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

		/// <summary>Gets or sets the XML Schema definition language (XSD) data type of the generated XML element.</summary>
		/// <returns>One of the XML Schema data types.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00094CF7 File Offset: 0x00092EF7
		// (set) Token: 0x060019EB RID: 6635 RVA: 0x00094D0D File Offset: 0x00092F0D
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlSerializer" /> must serialize a member that has the <see langword="xsi:null" /> attribute set to "1".</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates the <see langword="xsi:null" /> attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00094D16 File Offset: 0x00092F16
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x00094D1E File Offset: 0x00092F1E
		public bool IsNullable
		{
			get
			{
				return this.nullable;
			}
			set
			{
				this.nullable = value;
			}
		}

		// Token: 0x0400194D RID: 6477
		private string elementName;

		// Token: 0x0400194E RID: 6478
		private string dataType;

		// Token: 0x0400194F RID: 6479
		private bool nullable;
	}
}
