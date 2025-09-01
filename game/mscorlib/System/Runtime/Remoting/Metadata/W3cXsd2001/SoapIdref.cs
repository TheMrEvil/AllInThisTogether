using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="IDREFS" /> attribute.</summary>
	// Token: 0x020005EB RID: 1515
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdref : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class.</summary>
		// Token: 0x06003979 RID: 14713 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapIdref()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class with an XML <see langword="IDREF" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="IDREF" /> attribute.</param>
		// Token: 0x0600397A RID: 14714 RVA: 0x000CBB5C File Offset: 0x000C9D5C
		public SoapIdref(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML <see langword="IDREF" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="IDREF" /> attribute.</returns>
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x000CBB70 File Offset: 0x000C9D70
		// (set) Token: 0x0600397C RID: 14716 RVA: 0x000CBB78 File Offset: 0x000C9D78
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000CBB81 File Offset: 0x000C9D81
		public static string XsdType
		{
			get
			{
				return "IDREF";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600397E RID: 14718 RVA: 0x000CBB88 File Offset: 0x000C9D88
		public string GetXsdType()
		{
			return SoapIdref.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.String" /> obtained from <paramref name="value" />.</returns>
		// Token: 0x0600397F RID: 14719 RVA: 0x000CBB8F File Offset: 0x000C9D8F
		public static SoapIdref Parse(string value)
		{
			return new SoapIdref(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" />.</returns>
		// Token: 0x06003980 RID: 14720 RVA: 0x000CBB70 File Offset: 0x000C9D70
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x0400262C RID: 9772
		private string _value;
	}
}
