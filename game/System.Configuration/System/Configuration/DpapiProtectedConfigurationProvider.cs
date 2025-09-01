using System;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides a <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object that uses the Windows data protection API (DPAPI) to encrypt and decrypt configuration data.</summary>
	// Token: 0x0200003E RID: 62
	public sealed class DpapiProtectedConfigurationProvider : ProtectedConfigurationProvider
	{
		/// <summary>Decrypts the passed <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <param name="encryptedNode">The <see cref="T:System.Xml.XmlNode" /> object to decrypt.</param>
		/// <returns>A decrypted <see cref="T:System.Xml.XmlNode" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="encryptedNode" /> does not have <see cref="P:System.Xml.XmlNode.Name" /> set to "EncryptedData" and <see cref="T:System.Xml.XmlNodeType" /> set to <see cref="F:System.Xml.XmlNodeType.Element" />.  
		/// -or-
		///  <paramref name="encryptedNode" /> does not have a child node named "CipherData" with a child node named "CipherValue".  
		/// -or-
		///  The child node named "CipherData" is an empty node.</exception>
		// Token: 0x06000222 RID: 546 RVA: 0x0000774D File Offset: 0x0000594D
		[MonoNotSupported("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.")]
		public override XmlNode Decrypt(XmlNode encryptedNode)
		{
			throw new NotSupportedException("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.");
		}

		/// <summary>Encrypts the passed <see cref="T:System.Xml.XmlNode" /> object.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object to encrypt.</param>
		/// <returns>An encrypted <see cref="T:System.Xml.XmlNode" /> object.</returns>
		// Token: 0x06000223 RID: 547 RVA: 0x0000774D File Offset: 0x0000594D
		[MonoNotSupported("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.")]
		public override XmlNode Encrypt(XmlNode node)
		{
			throw new NotSupportedException("DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.");
		}

		/// <summary>Initializes the provider with default settings.</summary>
		/// <param name="name">The provider name to use for the object.</param>
		/// <param name="configurationValues">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> collection of values to use when initializing the object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="configurationValues" /> contains an unrecognized configuration setting.</exception>
		// Token: 0x06000224 RID: 548 RVA: 0x0000775C File Offset: 0x0000595C
		[MonoTODO]
		public override void Initialize(string name, NameValueCollection configurationValues)
		{
			base.Initialize(name, configurationValues);
			string text = configurationValues["useMachineProtection"];
			if (text != null && text.ToLowerInvariant() == "true")
			{
				this.useMachineProtection = true;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Configuration.DpapiProtectedConfigurationProvider" /> object is using machine-specific or user-account-specific protection.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.DpapiProtectedConfigurationProvider" /> is using machine-specific protection; <see langword="false" /> if it is using user-account-specific protection.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00007799 File Offset: 0x00005999
		public bool UseMachineProtection
		{
			get
			{
				return this.useMachineProtection;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.DpapiProtectedConfigurationProvider" /> class using default settings.</summary>
		// Token: 0x06000226 RID: 550 RVA: 0x000077A1 File Offset: 0x000059A1
		public DpapiProtectedConfigurationProvider()
		{
		}

		// Token: 0x040000E0 RID: 224
		private bool useMachineProtection;

		// Token: 0x040000E1 RID: 225
		private const string NotSupportedReason = "DpapiProtectedConfigurationProvider depends on the Microsoft Data\nProtection API, and is unimplemented in Mono.  For portability's sake,\nit is suggested that you use the RsaProtectedConfigurationProvider.";
	}
}
