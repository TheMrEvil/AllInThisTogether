using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default HTTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x02000769 RID: 1897
	public sealed class HttpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x06003BCD RID: 15309 RVA: 0x000CD024 File Offset: 0x000CB224
		static HttpCachePolicyElement()
		{
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumAgeProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.maximumStaleProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.minimumFreshProp);
			HttpCachePolicyElement.properties.Add(HttpCachePolicyElement.policyLevelProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.HttpCachePolicyElement" /> class.</summary>
		// Token: 0x06003BCE RID: 15310 RVA: 0x00031238 File Offset: 0x0002F438
		public HttpCachePolicyElement()
		{
		}

		/// <summary>Gets or sets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for cached resources specified in the configuration file.</returns>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06003BCF RID: 15311 RVA: 0x000CD100 File Offset: 0x000CB300
		// (set) Token: 0x06003BD0 RID: 15312 RVA: 0x000CD112 File Offset: 0x000CB312
		[ConfigurationProperty("maximumAge", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan MaximumAge
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumAgeProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumAgeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum staleness value permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified in the configuration file.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x000CD125 File Offset: 0x000CB325
		// (set) Token: 0x06003BD2 RID: 15314 RVA: 0x000CD137 File Offset: 0x000CB337
		[ConfigurationProperty("maximumStale", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MaximumStale
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.maximumStaleProp];
			}
			set
			{
				base[HttpCachePolicyElement.maximumStaleProp] = value;
			}
		}

		/// <summary>Gets or sets the minimum freshness permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified in the configuration file.</returns>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x000CD14A File Offset: 0x000CB34A
		// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x000CD15C File Offset: 0x000CB35C
		[ConfigurationProperty("minimumFresh", DefaultValue = "-10675199.02:48:05.4775808")]
		public TimeSpan MinimumFresh
		{
			get
			{
				return (TimeSpan)base[HttpCachePolicyElement.minimumFreshProp];
			}
			set
			{
				base[HttpCachePolicyElement.minimumFreshProp] = value;
			}
		}

		/// <summary>Gets or sets HTTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x000CD16F File Offset: 0x000CB36F
		// (set) Token: 0x06003BD6 RID: 15318 RVA: 0x000CD181 File Offset: 0x000CB381
		[ConfigurationProperty("policyLevel", DefaultValue = "Default", Options = ConfigurationPropertyOptions.IsRequired)]
		public HttpRequestCacheLevel PolicyLevel
		{
			get
			{
				return (HttpRequestCacheLevel)base[HttpCachePolicyElement.policyLevelProp];
			}
			set
			{
				base[HttpCachePolicyElement.policyLevelProp] = value;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000CD194 File Offset: 0x000CB394
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpCachePolicyElement.properties;
			}
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400238F RID: 9103
		private static ConfigurationProperty maximumAgeProp = new ConfigurationProperty("maximumAge", typeof(TimeSpan), TimeSpan.MaxValue);

		// Token: 0x04002390 RID: 9104
		private static ConfigurationProperty maximumStaleProp = new ConfigurationProperty("maximumStale", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x04002391 RID: 9105
		private static ConfigurationProperty minimumFreshProp = new ConfigurationProperty("minimumFresh", typeof(TimeSpan), TimeSpan.MinValue);

		// Token: 0x04002392 RID: 9106
		private static ConfigurationProperty policyLevelProp = new ConfigurationProperty("policyLevel", typeof(HttpRequestCacheLevel), HttpRequestCacheLevel.Default, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002393 RID: 9107
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
