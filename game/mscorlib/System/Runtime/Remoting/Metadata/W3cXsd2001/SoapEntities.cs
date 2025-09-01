using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="ENTITIES" /> attribute.</summary>
	// Token: 0x020005E6 RID: 1510
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntities : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> class.</summary>
		// Token: 0x06003953 RID: 14675 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapEntities()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> class with an XML <see langword="ENTITIES" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="ENTITIES" /> attribute.</param>
		// Token: 0x06003954 RID: 14676 RVA: 0x000CB901 File Offset: 0x000C9B01
		public SoapEntities(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML <see langword="ENTITIES" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="ENTITIES" /> attribute.</returns>
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000CB915 File Offset: 0x000C9B15
		// (set) Token: 0x06003956 RID: 14678 RVA: 0x000CB91D File Offset: 0x000C9B1D
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
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000CB926 File Offset: 0x000C9B26
		public static string XsdType
		{
			get
			{
				return "ENTITIES";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003958 RID: 14680 RVA: 0x000CB92D File Offset: 0x000C9B2D
		public string GetXsdType()
		{
			return SoapEntities.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06003959 RID: 14681 RVA: 0x000CB934 File Offset: 0x000C9B34
		public static SoapEntities Parse(string value)
		{
			return new SoapEntities(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities.Value" />.</returns>
		// Token: 0x0600395A RID: 14682 RVA: 0x000CB915 File Offset: 0x000C9B15
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002627 RID: 9767
		private string _value;
	}
}
