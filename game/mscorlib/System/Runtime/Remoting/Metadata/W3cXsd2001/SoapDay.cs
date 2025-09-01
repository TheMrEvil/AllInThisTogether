using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="gDay" /> type.</summary>
	// Token: 0x020005E4 RID: 1508
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDay : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> class.</summary>
		// Token: 0x06003946 RID: 14662 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapDay()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance.</param>
		// Token: 0x06003947 RID: 14663 RVA: 0x000CB4F8 File Offset: 0x000C96F8
		public SoapDay(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000CB507 File Offset: 0x000C9707
		// (set) Token: 0x06003949 RID: 14665 RVA: 0x000CB50F File Offset: 0x000C970F
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
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600394A RID: 14666 RVA: 0x000CB518 File Offset: 0x000C9718
		public static string XsdType
		{
			get
			{
				return "gDay";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600394B RID: 14667 RVA: 0x000CB51F File Offset: 0x000C971F
		public string GetXsdType()
		{
			return SoapDay.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> object that is obtained from <paramref name="value" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns.</exception>
		// Token: 0x0600394C RID: 14668 RVA: 0x000CB526 File Offset: 0x000C9726
		public static SoapDay Parse(string value)
		{
			return new SoapDay(DateTime.ParseExact(value, SoapDay._datetimeFormats, null, DateTimeStyles.None));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay.Value" /> in the format "---dd".</returns>
		// Token: 0x0600394D RID: 14669 RVA: 0x000CB53A File Offset: 0x000C973A
		public override string ToString()
		{
			return this._value.ToString("---dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000CB551 File Offset: 0x000C9751
		// Note: this type is marked as 'beforefieldinit'.
		static SoapDay()
		{
		}

		// Token: 0x04002625 RID: 9765
		private static readonly string[] _datetimeFormats = new string[]
		{
			"---dd",
			"---ddzzz"
		};

		// Token: 0x04002626 RID: 9766
		private DateTime _value;
	}
}
