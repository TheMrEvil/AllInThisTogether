using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="date" /> type.</summary>
	// Token: 0x020005E2 RID: 1506
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDate : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class.</summary>
		// Token: 0x06003935 RID: 14645 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapDate()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		// Token: 0x06003936 RID: 14646 RVA: 0x000CB2DD File Offset: 0x000C94DD
		public SoapDate(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> class with a specified <see cref="T:System.DateTime" /> object and an integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> is a positive or negative value.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		/// <param name="sign">An integer that indicates whether <paramref name="value" /> is positive.</param>
		// Token: 0x06003937 RID: 14647 RVA: 0x000CB2EC File Offset: 0x000C94EC
		public SoapDate(DateTime value, int sign)
		{
			this._value = value;
			this._sign = sign;
		}

		/// <summary>Gets or sets whether the date and time of the current instance is positive or negative.</summary>
		/// <returns>An integer that indicates whether <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> is positive or negative.</returns>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06003938 RID: 14648 RVA: 0x000CB302 File Offset: 0x000C9502
		// (set) Token: 0x06003939 RID: 14649 RVA: 0x000CB30A File Offset: 0x000C950A
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
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600393A RID: 14650 RVA: 0x000CB313 File Offset: 0x000C9513
		// (set) Token: 0x0600393B RID: 14651 RVA: 0x000CB31B File Offset: 0x000C951B
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
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600393C RID: 14652 RVA: 0x000CB324 File Offset: 0x000C9524
		public static string XsdType
		{
			get
			{
				return "date";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600393D RID: 14653 RVA: 0x000CB32B File Offset: 0x000C952B
		public string GetXsdType()
		{
			return SoapDate.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate" /> object that is obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns.</exception>
		// Token: 0x0600393E RID: 14654 RVA: 0x000CB334 File Offset: 0x000C9534
		public static SoapDate Parse(string value)
		{
			SoapDate soapDate = new SoapDate(DateTime.ParseExact(value, SoapDate._datetimeFormats, null, DateTimeStyles.None));
			if (value.StartsWith("-"))
			{
				soapDate.Sign = -1;
			}
			else
			{
				soapDate.Sign = 0;
			}
			return soapDate;
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Value" /> in the format "yyyy-MM-dd" or "'-'yyyy-MM-dd" if <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDate.Sign" /> is negative.</returns>
		// Token: 0x0600393F RID: 14655 RVA: 0x000CB372 File Offset: 0x000C9572
		public override string ToString()
		{
			if (this._sign >= 0)
			{
				return this._value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
			}
			return this._value.ToString("'-'yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000CB3A8 File Offset: 0x000C95A8
		// Note: this type is marked as 'beforefieldinit'.
		static SoapDate()
		{
		}

		// Token: 0x04002621 RID: 9761
		private static readonly string[] _datetimeFormats = new string[]
		{
			"yyyy-MM-dd",
			"'+'yyyy-MM-dd",
			"'-'yyyy-MM-dd",
			"yyyy-MM-ddzzz",
			"'+'yyyy-MM-ddzzz",
			"'-'yyyy-MM-ddzzz"
		};

		// Token: 0x04002622 RID: 9762
		private int _sign;

		// Token: 0x04002623 RID: 9763
		private DateTime _value;
	}
}
