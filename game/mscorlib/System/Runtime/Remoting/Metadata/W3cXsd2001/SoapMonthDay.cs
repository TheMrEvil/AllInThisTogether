using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="gMonthDay" /> type.</summary>
	// Token: 0x020005F0 RID: 1520
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonthDay : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> class.</summary>
		// Token: 0x060039A2 RID: 14754 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapMonthDay()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		// Token: 0x060039A3 RID: 14755 RVA: 0x000CBCCB File Offset: 0x000C9ECB
		public SoapMonthDay(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000CBCDA File Offset: 0x000C9EDA
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000CBCE2 File Offset: 0x000C9EE2
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
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000CBCEB File Offset: 0x000C9EEB
		public static string XsdType
		{
			get
			{
				return "gMonthDay";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060039A7 RID: 14759 RVA: 0x000CBCF2 File Offset: 0x000C9EF2
		public string GetXsdType()
		{
			return SoapMonthDay.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> object that is obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns.</exception>
		// Token: 0x060039A8 RID: 14760 RVA: 0x000CBCF9 File Offset: 0x000C9EF9
		public static SoapMonthDay Parse(string value)
		{
			return new SoapMonthDay(DateTime.ParseExact(value, SoapMonthDay._datetimeFormats, null, DateTimeStyles.None));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay.Value" /> in the format "'--'MM'-'dd".</returns>
		// Token: 0x060039A9 RID: 14761 RVA: 0x000CBD0D File Offset: 0x000C9F0D
		public override string ToString()
		{
			return this._value.ToString("--MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000CBD24 File Offset: 0x000C9F24
		// Note: this type is marked as 'beforefieldinit'.
		static SoapMonthDay()
		{
		}

		// Token: 0x04002632 RID: 9778
		private static readonly string[] _datetimeFormats = new string[]
		{
			"--MM-dd",
			"--MM-ddzzz"
		};

		// Token: 0x04002633 RID: 9779
		private DateTime _value;
	}
}
