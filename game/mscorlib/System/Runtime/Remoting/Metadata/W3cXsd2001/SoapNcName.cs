using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="NcName" /> type.</summary>
	// Token: 0x020005F2 RID: 1522
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNcName : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class.</summary>
		// Token: 0x060039B3 RID: 14771 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapNcName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class with an XML <see langword="NcName" /> type.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="NcName" /> type.</param>
		// Token: 0x060039B4 RID: 14772 RVA: 0x000CBD7C File Offset: 0x000C9F7C
		public SoapNcName(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML <see langword="NcName" /> type.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="NcName" /> type.</returns>
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x000CBD90 File Offset: 0x000C9F90
		// (set) Token: 0x060039B6 RID: 14774 RVA: 0x000CBD98 File Offset: 0x000C9F98
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
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x060039B7 RID: 14775 RVA: 0x000CBDA1 File Offset: 0x000C9FA1
		public static string XsdType
		{
			get
			{
				return "NCName";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060039B8 RID: 14776 RVA: 0x000CBDA8 File Offset: 0x000C9FA8
		public string GetXsdType()
		{
			return SoapNcName.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060039B9 RID: 14777 RVA: 0x000CBDAF File Offset: 0x000C9FAF
		public static SoapNcName Parse(string value)
		{
			return new SoapNcName(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" />.</returns>
		// Token: 0x060039BA RID: 14778 RVA: 0x000CBD90 File Offset: 0x000C9F90
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002635 RID: 9781
		private string _value;
	}
}
