using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides programmatic access to the <see langword="configProtectedData" /> configuration section. This class cannot be inherited.</summary>
	// Token: 0x0200005F RID: 95
	public sealed class ProtectedConfigurationSection : ConfigurationSection
	{
		// Token: 0x06000312 RID: 786 RVA: 0x00008B4C File Offset: 0x00006D4C
		static ProtectedConfigurationSection()
		{
			ProtectedConfigurationSection.properties.Add(ProtectedConfigurationSection.defaultProviderProp);
			ProtectedConfigurationSection.properties.Add(ProtectedConfigurationSection.providersProp);
		}

		/// <summary>Gets or sets the name of the default <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object in the <see cref="P:System.Configuration.ProtectedConfigurationSection.Providers" /> collection property.</summary>
		/// <returns>The name of the default <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object in the <see cref="P:System.Configuration.ProtectedConfigurationSection.Providers" /> collection property.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00008BB9 File Offset: 0x00006DB9
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00008BCB File Offset: 0x00006DCB
		[ConfigurationProperty("defaultProvider", DefaultValue = "RsaProtectedConfigurationProvider")]
		public string DefaultProvider
		{
			get
			{
				return (string)base[ProtectedConfigurationSection.defaultProviderProp];
			}
			set
			{
				base[ProtectedConfigurationSection.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of all the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects in all participating configuration files.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of all the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects in all participating configuration files.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00008BD9 File Offset: 0x00006DD9
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[ProtectedConfigurationSection.providersProp];
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00008BEB File Offset: 0x00006DEB
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProtectedConfigurationSection.properties;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00008BF4 File Offset: 0x00006DF4
		internal string EncryptSection(string clearXml, ProtectedConfigurationProvider protectionProvider)
		{
			XmlDocument xmlDocument = new ConfigurationXmlDocument();
			xmlDocument.LoadXml(clearXml);
			return protectionProvider.Encrypt(xmlDocument.DocumentElement).OuterXml;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00008C20 File Offset: 0x00006E20
		internal string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider)
		{
			return protectionProvider.Decrypt(new ConfigurationXmlDocument
			{
				InnerXml = encryptedXml
			}.DocumentElement).OuterXml;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008C4C File Offset: 0x00006E4C
		internal ProtectedConfigurationProviderCollection GetAllProviders()
		{
			if (this.providers == null)
			{
				this.providers = new ProtectedConfigurationProviderCollection();
				foreach (object obj in this.Providers)
				{
					ProviderSettings ps = (ProviderSettings)obj;
					this.providers.Add(this.InstantiateProvider(ps));
				}
			}
			return this.providers;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00008CCC File Offset: 0x00006ECC
		private ProtectedConfigurationProvider InstantiateProvider(ProviderSettings ps)
		{
			ProtectedConfigurationProvider protectedConfigurationProvider = Activator.CreateInstance(Type.GetType(ps.Type, true)) as ProtectedConfigurationProvider;
			if (protectedConfigurationProvider == null)
			{
				throw new Exception("The type specified does not extend ProtectedConfigurationProvider class.");
			}
			protectedConfigurationProvider.Initialize(ps.Name, ps.Parameters);
			return protectedConfigurationProvider;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProtectedConfigurationSection" /> class using default settings.</summary>
		// Token: 0x0600031B RID: 795 RVA: 0x00002147 File Offset: 0x00000347
		public ProtectedConfigurationSection()
		{
		}

		// Token: 0x04000121 RID: 289
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "RsaProtectedConfigurationProvider");

		// Token: 0x04000122 RID: 290
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null);

		// Token: 0x04000123 RID: 291
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000124 RID: 292
		private ProtectedConfigurationProviderCollection providers;
	}
}
