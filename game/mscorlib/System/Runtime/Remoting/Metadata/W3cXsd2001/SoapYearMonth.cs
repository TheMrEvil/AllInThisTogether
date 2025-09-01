using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="gYearMonth" /> type.</summary>
	// Token: 0x020005FF RID: 1535
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapYearMonth : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> class.</summary>
		// Token: 0x06003A26 RID: 14886 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapYearMonth()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		// Token: 0x06003A27 RID: 14887 RVA: 0x000CC3DD File Offset: 0x000CA5DD
		public SoapYearMonth(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> class with a specified <see cref="T:System.DateTime" /> object and an integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Value" /> is a positive or negative value.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		/// <param name="sign">An integer that indicates whether <paramref name="value" /> is positive.</param>
		// Token: 0x06003A28 RID: 14888 RVA: 0x000CC3EC File Offset: 0x000CA5EC
		public SoapYearMonth(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		/// <summary>Gets or sets whether the date and time of the current instance is positive or negative.</summary>
		/// <returns>An integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Value" /> is positive or negative.</returns>
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000CC402 File Offset: 0x000CA602
		// (set) Token: 0x06003A2A RID: 14890 RVA: 0x000CC40A File Offset: 0x000CA60A
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
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000CC413 File Offset: 0x000CA613
		// (set) Token: 0x06003A2C RID: 14892 RVA: 0x000CC41B File Offset: 0x000CA61B
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
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x000CC424 File Offset: 0x000CA624
		public static string XsdType
		{
			get
			{
				return "gYearMonth";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003A2E RID: 14894 RVA: 0x000CC42B File Offset: 0x000CA62B
		public string GetXsdType()
		{
			return SoapYearMonth.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth" /> object that is obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns.</exception>
		// Token: 0x06003A2F RID: 14895 RVA: 0x000CC434 File Offset: 0x000CA634
		public static SoapYearMonth Parse(string value)
		{
			SoapYearMonth soapYearMonth = new SoapYearMonth(DateTime.ParseExact(value, SoapYearMonth._datetimeFormats, null, DateTimeStyles.None));
			if (value.StartsWith("-"))
			{
				soapYearMonth.Sign = -1;
			}
			else
			{
				soapYearMonth.Sign = 0;
			}
			return soapYearMonth;
		}

		/// <summary>Returns a <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Value" /> in the format "yyyy-MM" or "'-'yyyy-MM" if <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapYearMonth.Sign" /> is negative.</returns>
		// Token: 0x06003A30 RID: 14896 RVA: 0x000CC472 File Offset: 0x000CA672
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy-MM", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy-MM", CultureInfo.InvariantCulture);
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000CC4A8 File Offset: 0x000CA6A8
		// Note: this type is marked as 'beforefieldinit'.
		static SoapYearMonth()
		{
		}

		// Token: 0x04002647 RID: 9799
		private static readonly string[] _datetimeFormats = new string[]
		{
			"yyyy-MM",
			"'+'yyyy-MM",
			"'-'yyyy-MM",
			"yyyy-MMzzz",
			"'+'yyyy-MMzzz",
			"'-'yyyy-MMzzz"
		};

		// Token: 0x04002648 RID: 9800
		private int _sign;

		// Token: 0x04002649 RID: 9801
		private DateTime _value;
	}
}
