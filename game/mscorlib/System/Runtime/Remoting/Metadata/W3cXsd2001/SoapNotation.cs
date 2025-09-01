using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="NOTATION" /> attribute type.</summary>
	// Token: 0x020005F9 RID: 1529
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNotation : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> class.</summary>
		// Token: 0x060039EB RID: 14827 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapNotation()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> class with an XML <see langword="NOTATION" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="NOTATION" /> attribute.</param>
		// Token: 0x060039EC RID: 14828 RVA: 0x000CBFAF File Offset: 0x000CA1AF
		public SoapNotation(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML <see langword="NOTATION" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="NOTATION" /> attribute.</returns>
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000CBFBE File Offset: 0x000CA1BE
		// (set) Token: 0x060039EE RID: 14830 RVA: 0x000CBFC6 File Offset: 0x000CA1C6
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
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x000CBFCF File Offset: 0x000CA1CF
		public static string XsdType
		{
			get
			{
				return "NOTATION";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060039F0 RID: 14832 RVA: 0x000CBFD6 File Offset: 0x000CA1D6
		public string GetXsdType()
		{
			return SoapNotation.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x060039F1 RID: 14833 RVA: 0x000CBFDD File Offset: 0x000CA1DD
		public static SoapNotation Parse(string value)
		{
			return new SoapNotation(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation.Value" />.</returns>
		// Token: 0x060039F2 RID: 14834 RVA: 0x000CBFBE File Offset: 0x000CA1BE
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x0400263C RID: 9788
		private string _value;
	}
}
