using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="token" /> type.</summary>
	// Token: 0x020005FD RID: 1533
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapToken : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> class.</summary>
		// Token: 0x06003A12 RID: 14866 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapToken()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> class with an XML <see langword="token" />.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="token" />.</param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following:  
		///
		/// <paramref name="value" /> contains invalid characters (0xD or 0x9).  
		///
		/// <paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />.Length - 1] contains white space.  
		///
		/// <paramref name="value" /> contains any spaces.</exception>
		// Token: 0x06003A13 RID: 14867 RVA: 0x000CC29C File Offset: 0x000CA49C
		public SoapToken(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML <see langword="token" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="token" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following: <paramref name="value" /> contains invalid characters (0xD or 0x9).  
		///
		/// <paramref name="value" /> [0] or <paramref name="value" /> [ <paramref name="value" />.Length - 1] contains white space.  
		///
		/// <paramref name="value" /> contains any spaces.</exception>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x000CC2B0 File Offset: 0x000CA4B0
		// (set) Token: 0x06003A15 RID: 14869 RVA: 0x000CC2B8 File Offset: 0x000CA4B8
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
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000CC2C1 File Offset: 0x000CA4C1
		public static string XsdType
		{
			get
			{
				return "token";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003A17 RID: 14871 RVA: 0x000CC2C8 File Offset: 0x000CA4C8
		public string GetXsdType()
		{
			return SoapToken.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06003A18 RID: 14872 RVA: 0x000CC2CF File Offset: 0x000CA4CF
		public static SoapToken Parse(string value)
		{
			return new SoapToken(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapToken.Value" />.</returns>
		// Token: 0x06003A19 RID: 14873 RVA: 0x000CC2B0 File Offset: 0x000CA4B0
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002643 RID: 9795
		private string _value;
	}
}
