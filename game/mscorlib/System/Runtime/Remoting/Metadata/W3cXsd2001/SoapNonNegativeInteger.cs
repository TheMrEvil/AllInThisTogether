using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="nonNegativeInteger" /> type.</summary>
	// Token: 0x020005F6 RID: 1526
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNonNegativeInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> class.</summary>
		// Token: 0x060039D3 RID: 14803 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapNonNegativeInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 0.</exception>
		// Token: 0x060039D4 RID: 14804 RVA: 0x000CBE9A File Offset: 0x000CA09A
		public SoapNonNegativeInteger(decimal value)
		{
			if (value < 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value.ToString());
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is less than 0.</exception>
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000CBECE File Offset: 0x000CA0CE
		// (set) Token: 0x060039D6 RID: 14806 RVA: 0x000CBED6 File Offset: 0x000CA0D6
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
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060039D7 RID: 14807 RVA: 0x000CBEDF File Offset: 0x000CA0DF
		public static string XsdType
		{
			get
			{
				return "nonNegativeInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060039D8 RID: 14808 RVA: 0x000CBEE6 File Offset: 0x000CA0E6
		public string GetXsdType()
		{
			return SoapNonNegativeInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060039D9 RID: 14809 RVA: 0x000CBEED File Offset: 0x000CA0ED
		public static SoapNonNegativeInteger Parse(string value)
		{
			return new SoapNonNegativeInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNonNegativeInteger.Value" />.</returns>
		// Token: 0x060039DA RID: 14810 RVA: 0x000CBEFA File Offset: 0x000CA0FA
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04002639 RID: 9785
		private decimal _value;
	}
}
