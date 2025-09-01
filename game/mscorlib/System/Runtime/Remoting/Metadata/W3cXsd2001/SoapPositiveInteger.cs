using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="positiveInteger" /> type.</summary>
	// Token: 0x020005FA RID: 1530
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapPositiveInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> class.</summary>
		// Token: 0x060039F3 RID: 14835 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapPositiveInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x060039F4 RID: 14836 RVA: 0x000CBFE5 File Offset: 0x000CA1E5
		public SoapPositiveInteger(decimal value)
		{
			if (value <= 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value.ToString());
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> indicating the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x000CC019 File Offset: 0x000CA219
		// (set) Token: 0x060039F6 RID: 14838 RVA: 0x000CC021 File Offset: 0x000CA221
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
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x000CC02A File Offset: 0x000CA22A
		public static string XsdType
		{
			get
			{
				return "positiveInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060039F8 RID: 14840 RVA: 0x000CC031 File Offset: 0x000CA231
		public string GetXsdType()
		{
			return SoapPositiveInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060039F9 RID: 14841 RVA: 0x000CC038 File Offset: 0x000CA238
		public static SoapPositiveInteger Parse(string value)
		{
			return new SoapPositiveInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapPositiveInteger.Value" />.</returns>
		// Token: 0x060039FA RID: 14842 RVA: 0x000CC045 File Offset: 0x000CA245
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x0400263D RID: 9789
		private decimal _value;
	}
}
