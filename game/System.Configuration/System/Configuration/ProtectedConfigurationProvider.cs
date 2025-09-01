using System;
using System.Configuration.Provider;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Is the base class to create providers for encrypting and decrypting protected-configuration data.</summary>
	// Token: 0x0200005D RID: 93
	public abstract class ProtectedConfigurationProvider : ProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> class using default settings.</summary>
		// Token: 0x0600030C RID: 780 RVA: 0x00008B22 File Offset: 0x00006D22
		protected ProtectedConfigurationProvider()
		{
		}

		/// <summary>Decrypts the passed <see cref="T:System.Xml.XmlNode" /> object from a configuration file.</summary>
		/// <param name="encryptedNode">The <see cref="T:System.Xml.XmlNode" /> object to decrypt.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> object containing decrypted data.</returns>
		// Token: 0x0600030D RID: 781
		public abstract XmlNode Decrypt(XmlNode encryptedNode);

		/// <summary>Encrypts the passed <see cref="T:System.Xml.XmlNode" /> object from a configuration file.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object to encrypt.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> object containing encrypted data.</returns>
		// Token: 0x0600030E RID: 782
		public abstract XmlNode Encrypt(XmlNode node);
	}
}
