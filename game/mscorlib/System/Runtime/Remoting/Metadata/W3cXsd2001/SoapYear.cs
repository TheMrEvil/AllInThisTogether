using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="gYear" /> type.</summary>
	// Token: 0x020005FE RID: 1534
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYear : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> class.</summary>
		// Token: 0x06003A1A RID: 14874 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapYear()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		// Token: 0x06003A1B RID: 14875 RVA: 0x000CC2D7 File Offset: 0x000CA4D7
		public SoapYear(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> class with a specified <see cref="T:System.DateTime" /> object and an integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> is a positive or negative value.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		/// <param name="sign">An integer that indicates whether <paramref name="value" /> is positive.</param>
		// Token: 0x06003A1C RID: 14876 RVA: 0x000CC2E6 File Offset: 0x000CA4E6
		public SoapYear(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		/// <summary>Gets or sets whether the date and time of the current instance is positive or negative.</summary>
		/// <returns>An integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> is positive or negative.</returns>
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06003A1D RID: 14877 RVA: 0x000CC2FC File Offset: 0x000CA4FC
		// (set) Token: 0x06003A1E RID: 14878 RVA: 0x000CC304 File Offset: 0x000CA504
		public int Sign
		{
			get
			{
				return this._sign;
			}
			set
			{
				this._sign = value;
			}
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06003A1F RID: 14879 RVA: 0x000CC30D File Offset: 0x000CA50D
		// (set) Token: 0x06003A20 RID: 14880 RVA: 0x000CC315 File Offset: 0x000CA515
		public DateTime Value
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
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000CC31E File Offset: 0x000CA51E
		public static string XsdType
		{
			get
			{
				return "gYear";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003A22 RID: 14882 RVA: 0x000CC325 File Offset: 0x000CA525
		public string GetXsdType()
		{
			return SoapYear.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear" /> object that is obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns.</exception>
		// Token: 0x06003A23 RID: 14883 RVA: 0x000CC32C File Offset: 0x000CA52C
		public static SoapYear Parse(string value)
		{
			SoapYear soapYear = new SoapYear(DateTime.ParseExact(value, SoapYear._datetimeFormats, null, DateTimeStyles.None));
			if (value.StartsWith("-"))
			{
				soapYear.Sign = -1;
			}
			else
			{
				soapYear.Sign = 0;
			}
			return soapYear;
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Value" /> in the format "yyyy" or "-yyyy" if <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYear.Sign" /> is negative.</returns>
		// Token: 0x06003A24 RID: 14884 RVA: 0x000CC36A File Offset: 0x000CA56A
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy", CultureInfo.InvariantCulture);
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000CC3A0 File Offset: 0x000CA5A0
		// Note: this type is marked as 'beforefieldinit'.
		static SoapYear()
		{
		}

		// Token: 0x04002644 RID: 9796
		private static readonly string[] _datetimeFormats = new string[]
		{
			"yyyy",
			"'+'yyyy",
			"'-'yyyy",
			"yyyyzzz",
			"'+'yyyyzzz",
			"'-'yyyyzzz"
		};

		// Token: 0x04002645 RID: 9797
		private int _sign;

		// Token: 0x04002646 RID: 9798
		private DateTime _value;
	}
}
