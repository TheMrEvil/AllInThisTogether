using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="ID" /> attribute.</summary>
	// Token: 0x020005EA RID: 1514
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapId : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> class.</summary>
		// Token: 0x06003971 RID: 14705 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapId()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> class with an XML <see langword="ID" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="ID" /> attribute.</param>
		// Token: 0x06003972 RID: 14706 RVA: 0x000CBB21 File Offset: 0x000C9D21
		public SoapId(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML <see langword="ID" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="ID" /> attribute.</returns>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x000CBB35 File Offset: 0x000C9D35
		// (set) Token: 0x06003974 RID: 14708 RVA: 0x000CBB3D File Offset: 0x000C9D3D
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
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x000CBB46 File Offset: 0x000C9D46
		public static string XsdType
		{
			get
			{
				return "ID";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003976 RID: 14710 RVA: 0x000CBB4D File Offset: 0x000C9D4D
		public string GetXsdType()
		{
			return SoapId.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06003977 RID: 14711 RVA: 0x000CBB54 File Offset: 0x000C9D54
		public static SoapId Parse(string value)
		{
			return new SoapId(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId.Value" />.</returns>
		// Token: 0x06003978 RID: 14712 RVA: 0x000CBB35 File Offset: 0x000C9D35
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x0400262B RID: 9771
		private string _value;
	}
}
