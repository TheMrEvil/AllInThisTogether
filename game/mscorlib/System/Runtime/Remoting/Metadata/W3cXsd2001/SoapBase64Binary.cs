using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="base64Binary" /> type.</summary>
	// Token: 0x020005E1 RID: 1505
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapBase64Binary : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> class.</summary>
		// Token: 0x0600392D RID: 14637 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapBase64Binary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> class with the binary representation of a 64-bit number.</summary>
		/// <param name="value">A <see cref="T:System.Byte" /> array that contains a 64-bit number.</param>
		// Token: 0x0600392E RID: 14638 RVA: 0x000CB295 File Offset: 0x000C9495
		public SoapBase64Binary(byte[] value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the binary representation of a 64-bit number.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the binary representation of a 64-bit number.</returns>
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x0600392F RID: 14639 RVA: 0x000CB2A4 File Offset: 0x000C94A4
		// (set) Token: 0x06003930 RID: 14640 RVA: 0x000CB2AC File Offset: 0x000C94AC
		public byte[] Value
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
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06003931 RID: 14641 RVA: 0x000CB2B5 File Offset: 0x000C94B5
		public static string XsdType
		{
			get
			{
				return "base64Binary";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003932 RID: 14642 RVA: 0x000CB2BC File Offset: 0x000C94BC
		public string GetXsdType()
		{
			return SoapBase64Binary.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> object that is obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following:  
		///
		/// <paramref name="value" /> is <see langword="null" />.  
		///
		/// The length of <paramref name="value" /> is less than 4.  
		///
		/// The length of <paramref name="value" /> is not a multiple of 4.</exception>
		// Token: 0x06003933 RID: 14643 RVA: 0x000CB2C3 File Offset: 0x000C94C3
		public static SoapBase64Binary Parse(string value)
		{
			return new SoapBase64Binary(Convert.FromBase64String(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" />.</returns>
		// Token: 0x06003934 RID: 14644 RVA: 0x000CB2D0 File Offset: 0x000C94D0
		public override string ToString()
		{
			return Convert.ToBase64String(this._value);
		}

		// Token: 0x04002620 RID: 9760
		private byte[] _value;
	}
}
