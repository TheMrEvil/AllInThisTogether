using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Gets the section group information for the networking namespaces. This class cannot be inherited.</summary>
	// Token: 0x02000770 RID: 1904
	public sealed class NetSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.NetSectionGroup" /> class.</summary>
		// Token: 0x06003BF7 RID: 15351 RVA: 0x0002EA01 File Offset: 0x0002CC01
		[MonoTODO]
		public NetSectionGroup()
		{
		}

		/// <summary>Gets the configuration section containing the authentication modules registered for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModulesSection" /> object.</returns>
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x000CD754 File Offset: 0x000CB954
		[ConfigurationProperty("authenticationModules")]
		public AuthenticationModulesSection AuthenticationModules
		{
			get
			{
				return (AuthenticationModulesSection)base.Sections["authenticationModules"];
			}
		}

		/// <summary>Gets the configuration section containing the connection management settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> object.</returns>
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x000CD76B File Offset: 0x000CB96B
		[ConfigurationProperty("connectionManagement")]
		public ConnectionManagementSection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementSection)base.Sections["connectionManagement"];
			}
		}

		/// <summary>Gets the configuration section containing the default Web proxy server settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.DefaultProxySection" /> object.</returns>
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x000CD782 File Offset: 0x000CB982
		[ConfigurationProperty("defaultProxy")]
		public DefaultProxySection DefaultProxy
		{
			get
			{
				return (DefaultProxySection)base.Sections["defaultProxy"];
			}
		}

		/// <summary>Gets the configuration section containing the SMTP client email settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> object.</returns>
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x000CD799 File Offset: 0x000CB999
		public MailSettingsSectionGroup MailSettings
		{
			get
			{
				return (MailSettingsSectionGroup)base.SectionGroups["mailSettings"];
			}
		}

		/// <summary>Gets the configuration section containing the cache configuration settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.RequestCachingSection" /> object.</returns>
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003BFC RID: 15356 RVA: 0x000CD7B0 File Offset: 0x000CB9B0
		[ConfigurationProperty("requestCaching")]
		public RequestCachingSection RequestCaching
		{
			get
			{
				return (RequestCachingSection)base.Sections["requestCaching"];
			}
		}

		/// <summary>Gets the configuration section containing the network settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SettingsSection" /> object.</returns>
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000CD7C7 File Offset: 0x000CB9C7
		[ConfigurationProperty("settings")]
		public SettingsSection Settings
		{
			get
			{
				return (SettingsSection)base.Sections["settings"];
			}
		}

		/// <summary>Gets the configuration section containing the modules registered for use with the <see cref="T:System.Net.WebRequest" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModulesSection" /> object.</returns>
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06003BFE RID: 15358 RVA: 0x000CD7DE File Offset: 0x000CB9DE
		[ConfigurationProperty("webRequestModules")]
		public WebRequestModulesSection WebRequestModules
		{
			get
			{
				return (WebRequestModulesSection)base.Sections["webRequestModules"];
			}
		}

		/// <summary>Gets the <see langword="System.Net" /> configuration section group from the specified configuration file.</summary>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> that represents a configuration file.</param>
		/// <returns>A <see cref="T:System.Net.Configuration.NetSectionGroup" /> that represents the <see langword="System.Net" /> settings in <paramref name="config" />.</returns>
		// Token: 0x06003BFF RID: 15359 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public static NetSectionGroup GetSectionGroup(Configuration config)
		{
			throw new NotImplementedException();
		}
	}
}
