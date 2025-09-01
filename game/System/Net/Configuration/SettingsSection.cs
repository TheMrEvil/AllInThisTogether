using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for sockets, IPv6, response headers, and service points. This class cannot be inherited.</summary>
	// Token: 0x02000778 RID: 1912
	public sealed class SettingsSection : ConfigurationSection
	{
		// Token: 0x06003C33 RID: 15411 RVA: 0x000CDE28 File Offset: 0x000CC028
		static SettingsSection()
		{
			SettingsSection.webProxyScriptProp = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement));
			SettingsSection.properties = new ConfigurationPropertyCollection();
			SettingsSection.properties.Add(SettingsSection.httpWebRequestProp);
			SettingsSection.properties.Add(SettingsSection.ipv6Prop);
			SettingsSection.properties.Add(SettingsSection.performanceCountersProp);
			SettingsSection.properties.Add(SettingsSection.servicePointManagerProp);
			SettingsSection.properties.Add(SettingsSection.socketProp);
			SettingsSection.properties.Add(SettingsSection.webProxyScriptProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> class.</summary>
		// Token: 0x06003C34 RID: 15412 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		public SettingsSection()
		{
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</summary>
		/// <returns>The configuration element that controls the maximum response header length and other settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</returns>
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000CDF2F File Offset: 0x000CC12F
		[ConfigurationProperty("httpWebRequest")]
		public HttpWebRequestElement HttpWebRequest
		{
			get
			{
				return (HttpWebRequestElement)base[SettingsSection.httpWebRequestProp];
			}
		}

		/// <summary>Gets the configuration element that enables Internet Protocol version 6 (IPv6).</summary>
		/// <returns>The configuration element that controls the setting used by IPv6.</returns>
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x000CDF41 File Offset: 0x000CC141
		[ConfigurationProperty("ipv6")]
		public Ipv6Element Ipv6
		{
			get
			{
				return (Ipv6Element)base[SettingsSection.ipv6Prop];
			}
		}

		/// <summary>Gets the configuration element that controls whether network performance counters are enabled.</summary>
		/// <returns>The configuration element that controls usage of network performance counters.</returns>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x000CDF53 File Offset: 0x000CC153
		[ConfigurationProperty("performanceCounters")]
		public PerformanceCountersElement PerformanceCounters
		{
			get
			{
				return (PerformanceCountersElement)base[SettingsSection.performanceCountersProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for connections to remote host computers.</summary>
		/// <returns>The configuration element that controls settings for connections to remote host computers.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003C38 RID: 15416 RVA: 0x000CDF65 File Offset: 0x000CC165
		[ConfigurationProperty("servicePointManager")]
		public ServicePointManagerElement ServicePointManager
		{
			get
			{
				return (ServicePointManagerElement)base[SettingsSection.servicePointManagerProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for sockets.</summary>
		/// <returns>The configuration element that controls settings for sockets.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x000CDF77 File Offset: 0x000CC177
		[ConfigurationProperty("socket")]
		public SocketElement Socket
		{
			get
			{
				return (SocketElement)base[SettingsSection.socketProp];
			}
		}

		/// <summary>Gets the configuration element that controls the execution timeout and download timeout of Web proxy scripts.</summary>
		/// <returns>The configuration element that controls settings for the execution timeout and download timeout used by the Web proxy scripts.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x000CDF89 File Offset: 0x000CC189
		[ConfigurationProperty("webProxyScript")]
		public WebProxyScriptElement WebProxyScript
		{
			get
			{
				return (WebProxyScriptElement)base[SettingsSection.webProxyScriptProp];
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06003C3B RID: 15419 RVA: 0x000CDF9B File Offset: 0x000CC19B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingsSection.properties;
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.Configuration.HttpListenerElement" /> object.  
		///  The configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x00032884 File Offset: 0x00030A84
		public HttpListenerElement HttpListener
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>The configuration element that controls the settings used by a <see cref="T:System.Net.WebUtility" /> object.</returns>
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x00032884 File Offset: 0x00030A84
		public WebUtilityElement WebUtility
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the configuration element that controls the number of handles for default network credentials.</summary>
		/// <returns>The configuration element that controls the number of handles for default network credentials.</returns>
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06003C3E RID: 15422 RVA: 0x00032884 File Offset: 0x00030A84
		public WindowsAuthenticationElement WindowsAuthentication
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x040023BF RID: 9151
		private static ConfigurationPropertyCollection properties;

		// Token: 0x040023C0 RID: 9152
		private static ConfigurationProperty httpWebRequestProp = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement));

		// Token: 0x040023C1 RID: 9153
		private static ConfigurationProperty ipv6Prop = new ConfigurationProperty("ipv6", typeof(Ipv6Element));

		// Token: 0x040023C2 RID: 9154
		private static ConfigurationProperty performanceCountersProp = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement));

		// Token: 0x040023C3 RID: 9155
		private static ConfigurationProperty servicePointManagerProp = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement));

		// Token: 0x040023C4 RID: 9156
		private static ConfigurationProperty webProxyScriptProp;

		// Token: 0x040023C5 RID: 9157
		private static ConfigurationProperty socketProp = new ConfigurationProperty("socket", typeof(SocketElement));
	}
}
