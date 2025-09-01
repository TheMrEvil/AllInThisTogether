using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML <see langword="ENTITY" /> attribute.</summary>
	// Token: 0x020005E7 RID: 1511
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntity : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> class.</summary>
		// Token: 0x0600395B RID: 14683 RVA: 0x0000259F File Offset: 0x0000079F
		public SoapEntity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> class with an XML <see langword="ENTITY" /> attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="ENTITY" /> attribute.</param>
		// Token: 0x0600395C RID: 14684 RVA: 0x000CB93C File Offset: 0x000C9B3C
		public SoapEntity(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML <see langword="ENTITY" /> attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="ENTITY" /> attribute.</returns>
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x000CB950 File Offset: 0x000C9B50
		// (set) Token: 0x0600395E RID: 14686 RVA: 0x000CB958 File Offset: 0x000C9B58
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
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000CB961 File Offset: 0x000C9B61
		public static string XsdType
		{
			get
			{
				return "ENTITY";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003960 RID: 14688 RVA: 0x000CB968 File Offset: 0x000C9B68
		public string GetXsdType()
		{
			return SoapEntity.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> object.</summary>
		/// <param name="value">The <see langword="String" /> to convert.</param>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object that is obtained from <paramref name="value" />.</returns>
		// Token: 0x06003961 RID: 14689 RVA: 0x000CB96F File Offset: 0x000C9B6F
		public static SoapEntity Parse(string value)
		{
			return new SoapEntity(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity.Value" />.</returns>
		// Token: 0x06003962 RID: 14690 RVA: 0x000CB950 File Offset: 0x000C9B50
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04002628 RID: 9768
		private string _value;
	}
}
