using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="IDREFS" /> attribute.</summary>
	// Token: 0x020005EC RID: 1516
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdrefs : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> class.</summary>
		// Token: 0x06003981 RID: 14721 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapIdrefs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> class with an XML <see langword="IDREFS" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="IDREFS" /> attribute.</param>
		// Token: 0x06003982 RID: 14722 RVA: 0x000CBB97 File Offset: 0x000C9D97
		public SoapIdrefs(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML <see langword="IDREFS" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="IDREFS" /> attribute.</returns>
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x000CBBAB File Offset: 0x000C9DAB
		// (set) Token: 0x06003984 RID: 14724 RVA: 0x000CBBB3 File Offset: 0x000C9DB3
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
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000CBBBC File Offset: 0x000C9DBC
		public static string XsdType
		{
			get
			{
				return "IDREFS";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003986 RID: 14726 RVA: 0x000CBBC3 File Offset: 0x000C9DC3
		public string GetXsdType()
		{
			return SoapIdrefs.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06003987 RID: 14727 RVA: 0x000CBBCA File Offset: 0x000C9DCA
		public static SoapIdrefs Parse(string value)
		{
			return new SoapIdrefs(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs.Value" />.</returns>
		// Token: 0x06003988 RID: 14728 RVA: 0x000CBBAB File Offset: 0x000C9DAB
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x0400262D RID: 9773
		private string _value;
	}
}
