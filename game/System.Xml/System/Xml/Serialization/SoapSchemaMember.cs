using System;

namespace System.Xml.Serialization
{
	/// <summary>Represents certain attributes of a XSD &lt;<see langword="part" />&gt; element in a WSDL document for generating classes from the document. </summary>
	// Token: 0x020002B6 RID: 694
	public class SoapSchemaMember
	{
		/// <summary>Gets or sets a value that corresponds to the type attribute of the WSDL part element.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that corresponds to the XML type.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000987CD File Offset: 0x000969CD
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x000987D5 File Offset: 0x000969D5
		public XmlQualifiedName MemberType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets a value that corresponds to the name attribute of the WSDL part element. </summary>
		/// <returns>The element name.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x000987DE File Offset: 0x000969DE
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x000987F4 File Offset: 0x000969F4
		public string MemberName
		{
			get
			{
				if (this.memberName != null)
				{
					return this.memberName;
				}
				return string.Empty;
			}
			set
			{
				this.memberName = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapSchemaMember" /> class. </summary>
		// Token: 0x06001A56 RID: 6742 RVA: 0x000987FD File Offset: 0x000969FD
		public SoapSchemaMember()
		{
		}

		// Token: 0x04001961 RID: 6497
		private string memberName;

		// Token: 0x04001962 RID: 6498
		private XmlQualifiedName type = XmlQualifiedName.Empty;
	}
}
