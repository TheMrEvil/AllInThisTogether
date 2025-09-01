using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="NMTOKENS" /> attribute.</summary>
	// Token: 0x020005F5 RID: 1525
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtokens : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class.</summary>
		// Token: 0x060039CB RID: 14795 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapNmtokens()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class with an XML <see langword="NMTOKENS" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="NMTOKENS" /> attribute.</param>
		// Token: 0x060039CC RID: 14796 RVA: 0x000CBE5F File Offset: 0x000CA05F
		public SoapNmtokens(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML <see langword="NMTOKENS" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="NMTOKENS" /> attribute.</returns>
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x000CBE73 File Offset: 0x000CA073
		// (set) Token: 0x060039CE RID: 14798 RVA: 0x000CBE7B File Offset: 0x000CA07B
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
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000CBE84 File Offset: 0x000CA084
		public static string XsdType
		{
			get
			{
				return "NMTOKENS";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060039D0 RID: 14800 RVA: 0x000CBE8B File Offset: 0x000CA08B
		public string GetXsdType()
		{
			return SoapNmtokens.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060039D1 RID: 14801 RVA: 0x000CBE92 File Offset: 0x000CA092
		public static SoapNmtokens Parse(string value)
		{
			return new SoapNmtokens(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" />.</returns>
		// Token: 0x060039D2 RID: 14802 RVA: 0x000CBE73 File Offset: 0x000CA073
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002638 RID: 9784
		private string _value;
	}
}
