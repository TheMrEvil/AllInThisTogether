using System;

namespace System.Xml.Serialization
{
	/// <summary>Provides mappings between code entities in .NET Framework Web service methods and the content of Web Services Description Language (WSDL) messages that are defined for SOAP Web services. </summary>
	// Token: 0x020002DF RID: 735
	public class XmlReflectionMember
	{
		/// <summary>Gets or sets the type of the Web service method member code entity that is represented by this mapping. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the Web service method member code entity that is represented by this mapping.</returns>
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x000A3A7E File Offset: 0x000A1C7E
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x000A3A86 File Offset: 0x000A1C86
		public Type MemberType
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

		/// <summary>Gets or sets an <see cref="T:System.Xml.Serialization.XmlAttributes" /> with the collection of <see cref="T:System.Xml.Serialization.XmlSerializer" />-related attributes that have been applied to the member code entity. </summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributes" /> that represents XML attributes that have been applied to the member code.</returns>
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000A3A8F File Offset: 0x000A1C8F
		// (set) Token: 0x06001C85 RID: 7301 RVA: 0x000A3A97 File Offset: 0x000A1C97
		public XmlAttributes XmlAttributes
		{
			get
			{
				return this.xmlAttributes;
			}
			set
			{
				this.xmlAttributes = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Xml.Serialization.SoapAttributes" /> with the collection of SOAP-related attributes that have been applied to the member code entity. </summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.SoapAttributes" /> that contains the objects that represent SOAP attributes applied to the member.</returns>
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x000A3AA0 File Offset: 0x000A1CA0
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x000A3AA8 File Offset: 0x000A1CA8
		public SoapAttributes SoapAttributes
		{
			get
			{
				return this.soapAttributes;
			}
			set
			{
				this.soapAttributes = value;
			}
		}

		/// <summary>Gets or sets the name of the Web service method member for this mapping. </summary>
		/// <returns>The name of the Web service method.</returns>
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x000A3AB1 File Offset: 0x000A1CB1
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x000A3AC7 File Offset: 0x000A1CC7
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.Serialization.XmlReflectionMember" /> represents a Web service method return value, as opposed to an output parameter. </summary>
		/// <returns>
		///     <see langword="true" />, if the member represents a Web service return value; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x000A3AD0 File Offset: 0x000A1CD0
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x000A3AD8 File Offset: 0x000A1CD8
		public bool IsReturnValue
		{
			get
			{
				return this.isReturnValue;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		/// <summary>Gets or sets a value that indicates that the value of the corresponding XML element definition's isNullable attribute is <see langword="false" />.</summary>
		/// <returns>
		///     <see langword="True" /> to override the <see cref="P:System.Xml.Serialization.XmlElementAttribute.IsNullable" /> property; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x000A3AE1 File Offset: 0x000A1CE1
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x000A3AE9 File Offset: 0x000A1CE9
		public bool OverrideIsNullable
		{
			get
			{
				return this.overrideIsNullable;
			}
			set
			{
				this.overrideIsNullable = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlReflectionMember" /> class. </summary>
		// Token: 0x06001C8E RID: 7310 RVA: 0x000A3AF2 File Offset: 0x000A1CF2
		public XmlReflectionMember()
		{
		}

		// Token: 0x04001A1D RID: 6685
		private string memberName;

		// Token: 0x04001A1E RID: 6686
		private Type type;

		// Token: 0x04001A1F RID: 6687
		private XmlAttributes xmlAttributes = new XmlAttributes();

		// Token: 0x04001A20 RID: 6688
		private SoapAttributes soapAttributes = new SoapAttributes();

		// Token: 0x04001A21 RID: 6689
		private bool isReturnValue;

		// Token: 0x04001A22 RID: 6690
		private bool overrideIsNullable;
	}
}
