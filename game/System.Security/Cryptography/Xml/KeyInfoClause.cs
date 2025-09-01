using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class from which all implementations of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> subelements inherit.</summary>
	// Token: 0x02000041 RID: 65
	public abstract class KeyInfoClause
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</summary>
		// Token: 0x060001AC RID: 428 RVA: 0x00002145 File Offset: 0x00000345
		protected KeyInfoClause()
		{
		}

		/// <summary>When overridden in a derived class, returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</returns>
		// Token: 0x060001AD RID: 429
		public abstract XmlElement GetXml();

		// Token: 0x060001AE RID: 430 RVA: 0x000081F4 File Offset: 0x000063F4
		internal virtual XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xml = this.GetXml();
			return (XmlElement)xmlDocument.ImportNode(xml, true);
		}

		/// <summary>When overridden in a derived class, parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> to match.</summary>
		/// <param name="element">The <see cref="T:System.Xml.XmlElement" /> that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" />.</param>
		// Token: 0x060001AF RID: 431
		public abstract void LoadXml(XmlElement element);
	}
}
