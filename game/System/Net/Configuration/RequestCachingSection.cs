using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for cache behavior. This class cannot be inherited.</summary>
	// Token: 0x02000776 RID: 1910
	public sealed class RequestCachingSection : ConfigurationSection
	{
		// Token: 0x06003C12 RID: 15378 RVA: 0x000CDA08 File Offset: 0x000CBC08
		static RequestCachingSection()
		{
			RequestCachingSection.properties = new ConfigurationPropertyCollection();
			RequestCachingSection.properties.Add(RequestCachingSection.defaultFtpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultHttpCachePolicyProp);
			RequestCachingSection.properties.Add(RequestCachingSection.defaultPolicyLevelProp);
			RequestCachingSection.properties.Add(RequestCachingSection.disableAllCachingProp);
			RequestCachingSection.properties.Add(RequestCachingSection.isPrivateCacheProp);
			RequestCachingSection.properties.Add(RequestCachingSection.unspecifiedMaximumAgeProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.RequestCachingSection" /> class.</summary>
		// Token: 0x06003C13 RID: 15379 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		public RequestCachingSection()
		{
		}

		/// <summary>Gets the default FTP caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.FtpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003C14 RID: 15380 RVA: 0x000CDB2F File Offset: 0x000CBD2F
		[ConfigurationProperty("defaultFtpCachePolicy")]
		public FtpCachePolicyElement DefaultFtpCachePolicy
		{
			get
			{
				return (FtpCachePolicyElement)base[RequestCachingSection.defaultFtpCachePolicyProp];
			}
		}

		/// <summary>Gets the default caching behavior for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpCachePolicyElement" /> that defines the default cache policy.</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000CDB41 File Offset: 0x000CBD41
		[ConfigurationProperty("defaultHttpCachePolicy")]
		public HttpCachePolicyElement DefaultHttpCachePolicy
		{
			get
			{
				return (HttpCachePolicyElement)base[RequestCachingSection.defaultHttpCachePolicyProp];
			}
		}

		/// <summary>Gets or sets the default cache policy level.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> enumeration value.</returns>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003C16 RID: 15382 RVA: 0x000CDB53 File Offset: 0x000CBD53
		// (set) Token: 0x06003C17 RID: 15383 RVA: 0x000CDB65 File Offset: 0x000CBD65
		[ConfigurationProperty("defaultPolicyLevel", DefaultValue = "BypassCache")]
		public RequestCacheLevel DefaultPolicyLevel
		{
			get
			{
				return (RequestCacheLevel)base[RequestCachingSection.defaultPolicyLevelProp];
			}
			set
			{
				base[RequestCachingSection.defaultPolicyLevelProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that enables caching on the local computer.</summary>
		/// <returns>
		///   <see langword="true" /> if caching is disabled on the local computer; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000CDB78 File Offset: 0x000CBD78
		// (set) Token: 0x06003C19 RID: 15385 RVA: 0x000CDB8A File Offset: 0x000CBD8A
		[ConfigurationProperty("disableAllCaching", DefaultValue = "False")]
		public bool DisableAllCaching
		{
			get
			{
				return (bool)base[RequestCachingSection.disableAllCachingProp];
			}
			set
			{
				base[RequestCachingSection.disableAllCachingProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the local computer cache is private.</summary>
		/// <returns>
		///   <see langword="true" /> if the cache provides user isolation; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003C1A RID: 15386 RVA: 0x000CDB9D File Offset: 0x000CBD9D
		// (set) Token: 0x06003C1B RID: 15387 RVA: 0x000CDBAF File Offset: 0x000CBDAF
		[ConfigurationProperty("isPrivateCache", DefaultValue = "True")]
		public bool IsPrivateCache
		{
			get
			{
				return (bool)base[RequestCachingSection.isPrivateCacheProp];
			}
			set
			{
				base[RequestCachingSection.isPrivateCacheProp] = value;
			}
		}

		/// <summary>Gets or sets a value used as the maximum age for cached resources that do not have expiration information.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that provides a default maximum age for cached resources.</returns>
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x000CDBC2 File Offset: 0x000CBDC2
		// (set) Token: 0x06003C1D RID: 15389 RVA: 0x000CDBD4 File Offset: 0x000CBDD4
		[ConfigurationProperty("unspecifiedMaximumAge", DefaultValue = "1.00:00:00")]
		public TimeSpan UnspecifiedMaximumAge
		{
			get
			{
				return (TimeSpan)base[RequestCachingSection.unspecifiedMaximumAgeProp];
			}
			set
			{
				base[RequestCachingSection.unspecifiedMaximumAgeProp] = value;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000CDBE7 File Offset: 0x000CBDE7
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RequestCachingSection.properties;
			}
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x00066D7A File Offset: 0x00064F7A
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x000CDBEE File Offset: 0x000CBDEE
		[MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x040023B1 RID: 9137
		private static ConfigurationPropertyCollection properties;

		// Token: 0x040023B2 RID: 9138
		private static ConfigurationProperty defaultFtpCachePolicyProp = new ConfigurationProperty("defaultFtpCachePolicy", typeof(FtpCachePolicyElement));

		// Token: 0x040023B3 RID: 9139
		private static ConfigurationProperty defaultHttpCachePolicyProp = new ConfigurationProperty("defaultHttpCachePolicy", typeof(HttpCachePolicyElement));

		// Token: 0x040023B4 RID: 9140
		private static ConfigurationProperty defaultPolicyLevelProp = new ConfigurationProperty("defaultPolicyLevel", typeof(RequestCacheLevel), RequestCacheLevel.BypassCache);

		// Token: 0x040023B5 RID: 9141
		private static ConfigurationProperty disableAllCachingProp = new ConfigurationProperty("disableAllCaching", typeof(bool), false);

		// Token: 0x040023B6 RID: 9142
		private static ConfigurationProperty isPrivateCacheProp = new ConfigurationProperty("isPrivateCache", typeof(bool), true);

		// Token: 0x040023B7 RID: 9143
		private static ConfigurationProperty unspecifiedMaximumAgeProp = new ConfigurationProperty("unspecifiedMaximumAge", typeof(TimeSpan), new TimeSpan(1, 0, 0, 0));
	}
}
