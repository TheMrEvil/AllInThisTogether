using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web proxy server usage. This class cannot be inherited.</summary>
	// Token: 0x02000767 RID: 1895
	public sealed class DefaultProxySection : ConfigurationSection
	{
		// Token: 0x06003BBA RID: 15290 RVA: 0x000CCE48 File Offset: 0x000CB048
		static DefaultProxySection()
		{
			DefaultProxySection.properties = new ConfigurationPropertyCollection();
			DefaultProxySection.properties.Add(DefaultProxySection.bypassListProp);
			DefaultProxySection.properties.Add(DefaultProxySection.enabledProp);
			DefaultProxySection.properties.Add(DefaultProxySection.moduleProp);
			DefaultProxySection.properties.Add(DefaultProxySection.proxyProp);
			DefaultProxySection.properties.Add(DefaultProxySection.useDefaultCredentialsProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.DefaultProxySection" /> class.</summary>
		// Token: 0x06003BBB RID: 15291 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		public DefaultProxySection()
		{
		}

		/// <summary>Gets the collection of resources that are not obtained using the Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.BypassElementCollection" /> that contains the addresses of resources that bypass the Web proxy server.</returns>
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000CCF36 File Offset: 0x000CB136
		[ConfigurationProperty("bypasslist")]
		public BypassElementCollection BypassList
		{
			get
			{
				return (BypassElementCollection)base[DefaultProxySection.bypassListProp];
			}
		}

		/// <summary>Gets or sets whether a Web proxy is used.</summary>
		/// <returns>
		///   <see langword="true" /> if a Web proxy will be used; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x000CCF48 File Offset: 0x000CB148
		// (set) Token: 0x06003BBE RID: 15294 RVA: 0x000CCF5A File Offset: 0x000CB15A
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[DefaultProxySection.enabledProp];
			}
			set
			{
				base[DefaultProxySection.enabledProp] = value;
			}
		}

		/// <summary>Gets the type information for a custom Web proxy implementation.</summary>
		/// <returns>The type information for a custom Web proxy implementation.</returns>
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x000CCF6D File Offset: 0x000CB16D
		[ConfigurationProperty("module")]
		public ModuleElement Module
		{
			get
			{
				return (ModuleElement)base[DefaultProxySection.moduleProp];
			}
		}

		/// <summary>Gets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>The URI that identifies the Web proxy server.</returns>
		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x000CCF7F File Offset: 0x000CB17F
		[ConfigurationProperty("proxy")]
		public ProxyElement Proxy
		{
			get
			{
				return (ProxyElement)base[DefaultProxySection.proxyProp];
			}
		}

		/// <summary>Gets or sets whether default credentials are to be used to access a Web proxy server.</summary>
		/// <returns>
		///   <see langword="true" /> if default credentials are to be used; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000CCF91 File Offset: 0x000CB191
		// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x000CCFA3 File Offset: 0x000CB1A3
		[ConfigurationProperty("useDefaultCredentials", DefaultValue = "False")]
		public bool UseDefaultCredentials
		{
			get
			{
				return (bool)base[DefaultProxySection.useDefaultCredentialsProp];
			}
			set
			{
				base[DefaultProxySection.useDefaultCredentialsProp] = value;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000CCFB6 File Offset: 0x000CB1B6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultProxySection.properties;
			}
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
		}

		// Token: 0x04002387 RID: 9095
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04002388 RID: 9096
		private static ConfigurationProperty bypassListProp = new ConfigurationProperty("bypasslist", typeof(BypassElementCollection), null);

		// Token: 0x04002389 RID: 9097
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x0400238A RID: 9098
		private static ConfigurationProperty moduleProp = new ConfigurationProperty("module", typeof(ModuleElement), null);

		// Token: 0x0400238B RID: 9099
		private static ConfigurationProperty proxyProp = new ConfigurationProperty("proxy", typeof(ProxyElement), null);

		// Token: 0x0400238C RID: 9100
		private static ConfigurationProperty useDefaultCredentialsProp = new ConfigurationProperty("useDefaultCredentials", typeof(bool), false);
	}
}
