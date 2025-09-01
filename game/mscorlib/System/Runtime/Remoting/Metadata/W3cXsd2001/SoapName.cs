using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="Name" /> type.</summary>
	// Token: 0x020005F1 RID: 1521
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapName : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> class.</summary>
		// Token: 0x060039AB RID: 14763 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> class with an XML <see langword="Name" /> type.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="Name" /> type.</param>
		// Token: 0x060039AC RID: 14764 RVA: 0x000CBD41 File Offset: 0x000C9F41
		public SoapName(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML <see langword="Name" /> type.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="Name" /> type.</returns>
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000CBD55 File Offset: 0x000C9F55
		// (set) Token: 0x060039AE RID: 14766 RVA: 0x000CBD5D File Offset: 0x000C9F5D
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
		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000CBD66 File Offset: 0x000C9F66
		public static string XsdType
		{
			get
			{
				return "Name";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060039B0 RID: 14768 RVA: 0x000CBD6D File Offset: 0x000C9F6D
		public string GetXsdType()
		{
			return SoapName.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060039B1 RID: 14769 RVA: 0x000CBD74 File Offset: 0x000C9F74
		public static SoapName Parse(string value)
		{
			return new SoapName(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapName.Value" />.</returns>
		// Token: 0x060039B2 RID: 14770 RVA: 0x000CBD55 File Offset: 0x000C9F55
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002634 RID: 9780
		private string _value;
	}
}
