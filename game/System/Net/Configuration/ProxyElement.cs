using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Identifies the configuration settings for Web proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000772 RID: 1906
	public sealed class ProxyElement : ConfigurationElement
	{
		// Token: 0x06003C05 RID: 15365 RVA: 0x000CD85C File Offset: 0x000CBA5C
		static ProxyElement()
		{
			ProxyElement.properties = new ConfigurationPropertyCollection();
			ProxyElement.properties.Add(ProxyElement.autoDetectProp);
			ProxyElement.properties.Add(ProxyElement.bypassOnLocalProp);
			ProxyElement.properties.Add(ProxyElement.proxyAddressProp);
			ProxyElement.properties.Add(ProxyElement.scriptLocationProp);
			ProxyElement.properties.Add(ProxyElement.useSystemDefaultProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ProxyElement" /> class.</summary>
		// Token: 0x06003C06 RID: 15366 RVA: 0x00031238 File Offset: 0x0002F438
		public ProxyElement()
		{
		}

		/// <summary>Gets or sets an <see cref="T:System.Net.Configuration.ProxyElement.AutoDetectValues" /> value that controls whether the Web proxy is automatically detected.</summary>
		/// <returns>
		///   <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.True" /> if the <see cref="T:System.Net.WebProxy" /> is automatically detected; <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.False" /> if the <see cref="T:System.Net.WebProxy" /> is not automatically detected; or <see cref="F:System.Net.Configuration.ProxyElement.AutoDetectValues.Unspecified" />.</returns>
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06003C07 RID: 15367 RVA: 0x000CD94F File Offset: 0x000CBB4F
		// (set) Token: 0x06003C08 RID: 15368 RVA: 0x000CD961 File Offset: 0x000CBB61
		[ConfigurationProperty("autoDetect", DefaultValue = "Unspecified")]
		public ProxyElement.AutoDetectValues AutoDetect
		{
			get
			{
				return (ProxyElement.AutoDetectValues)base[ProxyElement.autoDetectProp];
			}
			set
			{
				base[ProxyElement.autoDetectProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether local resources are retrieved by using a Web proxy server.</summary>
		/// <returns>A value that indicates whether local resources are retrieved by using a Web proxy server.</returns>
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06003C09 RID: 15369 RVA: 0x000CD974 File Offset: 0x000CBB74
		// (set) Token: 0x06003C0A RID: 15370 RVA: 0x000CD986 File Offset: 0x000CBB86
		[ConfigurationProperty("bypassonlocal", DefaultValue = "Unspecified")]
		public ProxyElement.BypassOnLocalValues BypassOnLocal
		{
			get
			{
				return (ProxyElement.BypassOnLocalValues)base[ProxyElement.bypassOnLocalProp];
			}
			set
			{
				base[ProxyElement.bypassOnLocalProp] = value;
			}
		}

		/// <summary>Gets or sets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>The URI that identifies the Web proxy server to use.</returns>
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x000CD999 File Offset: 0x000CBB99
		// (set) Token: 0x06003C0C RID: 15372 RVA: 0x000CD9AB File Offset: 0x000CBBAB
		[ConfigurationProperty("proxyaddress")]
		public Uri ProxyAddress
		{
			get
			{
				return (Uri)base[ProxyElement.proxyAddressProp];
			}
			set
			{
				base[ProxyElement.proxyAddressProp] = value;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Uri" /> value that specifies the location of the automatic proxy detection script.</summary>
		/// <returns>A <see cref="T:System.Uri" /> specifying the location of the automatic proxy detection script.</returns>
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06003C0D RID: 15373 RVA: 0x000CD9B9 File Offset: 0x000CBBB9
		// (set) Token: 0x06003C0E RID: 15374 RVA: 0x000CD9CB File Offset: 0x000CBBCB
		[ConfigurationProperty("scriptLocation")]
		public Uri ScriptLocation
		{
			get
			{
				return (Uri)base[ProxyElement.scriptLocationProp];
			}
			set
			{
				base[ProxyElement.scriptLocationProp] = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the Internet Explorer Web proxy settings are used.</summary>
		/// <returns>
		///   <see langword="true" /> if the Internet Explorer LAN settings are used to detect and configure the default <see cref="T:System.Net.WebProxy" /> used for requests; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x000CD9D9 File Offset: 0x000CBBD9
		// (set) Token: 0x06003C10 RID: 15376 RVA: 0x000CD9EB File Offset: 0x000CBBEB
		[ConfigurationProperty("usesystemdefault", DefaultValue = "Unspecified")]
		public ProxyElement.UseSystemDefaultValues UseSystemDefault
		{
			get
			{
				return (ProxyElement.UseSystemDefaultValues)base[ProxyElement.useSystemDefaultProp];
			}
			set
			{
				base[ProxyElement.useSystemDefaultProp] = value;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000CD9FE File Offset: 0x000CBBFE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProxyElement.properties;
			}
		}

		// Token: 0x0400239F RID: 9119
		private static ConfigurationPropertyCollection properties;

		// Token: 0x040023A0 RID: 9120
		private static ConfigurationProperty autoDetectProp = new ConfigurationProperty("autoDetect", typeof(ProxyElement.AutoDetectValues), ProxyElement.AutoDetectValues.Unspecified);

		// Token: 0x040023A1 RID: 9121
		private static ConfigurationProperty bypassOnLocalProp = new ConfigurationProperty("bypassonlocal", typeof(ProxyElement.BypassOnLocalValues), ProxyElement.BypassOnLocalValues.Unspecified);

		// Token: 0x040023A2 RID: 9122
		private static ConfigurationProperty proxyAddressProp = new ConfigurationProperty("proxyaddress", typeof(Uri), null);

		// Token: 0x040023A3 RID: 9123
		private static ConfigurationProperty scriptLocationProp = new ConfigurationProperty("scriptLocation", typeof(Uri), null);

		// Token: 0x040023A4 RID: 9124
		private static ConfigurationProperty useSystemDefaultProp = new ConfigurationProperty("usesystemdefault", typeof(ProxyElement.UseSystemDefaultValues), ProxyElement.UseSystemDefaultValues.Unspecified);

		/// <summary>Specifies whether the proxy is bypassed for local resources.</summary>
		// Token: 0x02000773 RID: 1907
		public enum BypassOnLocalValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x040023A6 RID: 9126
			Unspecified = -1,
			/// <summary>Access local resources directly.</summary>
			// Token: 0x040023A7 RID: 9127
			True = 1,
			/// <summary>All requests for local resources should go through the proxy</summary>
			// Token: 0x040023A8 RID: 9128
			False = 0
		}

		/// <summary>Specifies whether to use the local system proxy settings to determine whether the proxy is bypassed for local resources.</summary>
		// Token: 0x02000774 RID: 1908
		public enum UseSystemDefaultValues
		{
			/// <summary>The system default proxy setting is unspecified.</summary>
			// Token: 0x040023AA RID: 9130
			Unspecified = -1,
			/// <summary>Use system default proxy setting values.</summary>
			// Token: 0x040023AB RID: 9131
			True = 1,
			/// <summary>Do not use system default proxy setting values</summary>
			// Token: 0x040023AC RID: 9132
			False = 0
		}

		/// <summary>Specifies whether the proxy is automatically detected.</summary>
		// Token: 0x02000775 RID: 1909
		public enum AutoDetectValues
		{
			/// <summary>Unspecified.</summary>
			// Token: 0x040023AE RID: 9134
			Unspecified = -1,
			/// <summary>The proxy is automatically detected.</summary>
			// Token: 0x040023AF RID: 9135
			True = 1,
			/// <summary>The proxy is not automatically detected.</summary>
			// Token: 0x040023B0 RID: 9136
			False = 0
		}
	}
}
