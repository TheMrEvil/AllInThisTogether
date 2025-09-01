using System;

namespace System.Configuration
{
	/// <summary>Represents a group of configuration elements that configure the providers for the <see langword="&lt;configProtectedData&gt;" /> configuration section.</summary>
	// Token: 0x02000060 RID: 96
	public class ProtectedProviderSettings : ConfigurationElement
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00008D04 File Offset: 0x00006F04
		static ProtectedProviderSettings()
		{
			ProtectedProviderSettings.properties.Add(ProtectedProviderSettings.providersProp);
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> collection that represents the properties of the providers for the protected configuration data.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> that represents the properties of the providers for the protected configuration data.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00008D3C File Offset: 0x00006F3C
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProtectedProviderSettings.properties;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects that represent the properties of the providers for the protected configuration data.</summary>
		/// <returns>A collection of <see cref="T:System.Configuration.ProviderSettings" /> objects that represent the properties of the providers for the protected configuration data.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00008D43 File Offset: 0x00006F43
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[ProtectedProviderSettings.providersProp];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProtectedProviderSettings" /> class.</summary>
		// Token: 0x0600031F RID: 799 RVA: 0x000067BB File Offset: 0x000049BB
		public ProtectedProviderSettings()
		{
		}

		// Token: 0x04000125 RID: 293
		private static ConfigurationProperty providersProp = new ConfigurationProperty("", typeof(ProviderSettingsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04000126 RID: 294
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
