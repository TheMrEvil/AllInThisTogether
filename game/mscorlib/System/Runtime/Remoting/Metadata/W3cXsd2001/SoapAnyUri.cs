using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD <see langword="anyURI" /> type.</summary>
	// Token: 0x020005E0 RID: 1504
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapAnyUri : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> class.</summary>
		// Token: 0x06003925 RID: 14629 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapAnyUri()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> class with the specified URI.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains a URI.</param>
		// Token: 0x06003926 RID: 14630 RVA: 0x000CB25F File Offset: 0x000C945F
		public SoapAnyUri(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets a URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains a URI.</returns>
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000CB26E File Offset: 0x000C946E
		// (set) Token: 0x06003928 RID: 14632 RVA: 0x000CB276 File Offset: 0x000C9476
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
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x000CB27F File Offset: 0x000C947F
		public static string XsdType
		{
			get
			{
				return "anyUri";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600392A RID: 14634 RVA: 0x000CB286 File Offset: 0x000C9486
		public string GetXsdType()
		{
			return SoapAnyUri.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> object.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x0600392B RID: 14635 RVA: 0x000CB28D File Offset: 0x000C948D
		public static SoapAnyUri Parse(string value)
		{
			return new SoapAnyUri(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" />.</returns>
		// Token: 0x0600392C RID: 14636 RVA: 0x000CB26E File Offset: 0x000C946E
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x0400261F RID: 9759
		private string _value;
	}
}
