using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="negativeInteger" /> type.</summary>
	// Token: 0x020005F3 RID: 1523
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNegativeInteger : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> class.</summary>
		// Token: 0x060039BB RID: 14779 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapNegativeInteger()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> class with a <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Decimal" /> value to initialize the current instance.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than -1.</exception>
		// Token: 0x060039BC RID: 14780 RVA: 0x000CBDB7 File Offset: 0x000C9FB7
		public SoapNegativeInteger(decimal value)
		{
			if (value >= 0m)
			{
				throw SoapHelper.GetException(this, "invalid " + value.ToString());
			}
			this._value = value;
		}

		/// <summary>Gets or sets the numeric value of the current instance.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that indicates the numeric value of the current instance.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> is greater than -1.</exception>
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000CBDEB File Offset: 0x000C9FEB
		// (set) Token: 0x060039BE RID: 14782 RVA: 0x000CBDF3 File Offset: 0x000C9FF3
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
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000CBDFC File Offset: 0x000C9FFC
		public static string XsdType
		{
			get
			{
				return "negativeInteger";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060039C0 RID: 14784 RVA: 0x000CBE03 File Offset: 0x000CA003
		public string GetXsdType()
		{
			return SoapNegativeInteger.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060039C1 RID: 14785 RVA: 0x000CBE0A File Offset: 0x000CA00A
		public static SoapNegativeInteger Parse(string value)
		{
			return new SoapNegativeInteger(decimal.Parse(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNegativeInteger.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see langword="Value" />.</returns>
		// Token: 0x060039C2 RID: 14786 RVA: 0x000CBE17 File Offset: 0x000CA017
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x04002636 RID: 9782
		private decimal _value;
	}
}
