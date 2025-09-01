using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="integer" /> type.</summary>
	// Token: 0x020005ED RID: 1517
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class.</summary>
		// Token: 0x06003989 RID: 14729 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance.</param>
		// Token: 0x0600398A RID: 14730 RVA: 0x000CBBD2 File Offset: 0x000C9DD2
		public SoapInteger(decimal value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x000CBBE1 File Offset: 0x000C9DE1
		// (set) Token: 0x0600398C RID: 14732 RVA: 0x000CBBE9 File Offset: 0x000C9DE9
		public decimal Value
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
		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000CBBF2 File Offset: 0x000C9DF2
		public static string XsdType
		{
			get
			{
				return "integer";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600398E RID: 14734 RVA: 0x000CBBF9 File Offset: 0x000C9DF9
		public string GetXsdType()
		{
			return SoapInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x0600398F RID: 14735 RVA: 0x000CBC00 File Offset: 0x000C9E00
		public static SoapInteger Parse(string value)
		{
			return new SoapInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger.Value" />.</returns>
		// Token: 0x06003990 RID: 14736 RVA: 0x000CBC0D File Offset: 0x000C9E0D
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400262E RID: 9774
		private decimal _value;
	}
}
