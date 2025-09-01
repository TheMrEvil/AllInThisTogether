using System;

namespace System.Net.Cache
{
	// Token: 0x02000784 RID: 1924
	internal sealed class RequestCacheManager
	{
		// Token: 0x06003CB7 RID: 15543 RVA: 0x0000219B File Offset: 0x0000039B
		private RequestCacheManager()
		{
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x000CE9F4 File Offset: 0x000CCBF4
		internal static RequestCacheBinding GetBinding(string internedScheme)
		{
			if (internedScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return RequestCacheManager.s_BypassCacheBinding;
			}
			if (internedScheme.Length == 0)
			{
				return RequestCacheManager.s_DefaultGlobalBinding;
			}
			if (internedScheme == Uri.UriSchemeHttp || internedScheme == Uri.UriSchemeHttps)
			{
				return RequestCacheManager.s_DefaultHttpBinding;
			}
			if (internedScheme == Uri.UriSchemeFtp)
			{
				return RequestCacheManager.s_DefaultFtpBinding;
			}
			return RequestCacheManager.s_BypassCacheBinding;
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000CEA6E File Offset: 0x000CCC6E
		internal static bool IsCachingEnabled
		{
			get
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.LoadConfigSettings();
				}
				return !RequestCacheManager.s_CacheConfigSettings.DisableAllCaching;
			}
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000CEA90 File Offset: 0x000CCC90
		internal static void SetBinding(string uriScheme, RequestCacheBinding binding)
		{
			if (uriScheme == null)
			{
				throw new ArgumentNullException("uriScheme");
			}
			if (RequestCacheManager.s_CacheConfigSettings == null)
			{
				RequestCacheManager.LoadConfigSettings();
			}
			if (RequestCacheManager.s_CacheConfigSettings.DisableAllCaching)
			{
				return;
			}
			if (uriScheme.Length == 0)
			{
				RequestCacheManager.s_DefaultGlobalBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeHttp || uriScheme == Uri.UriSchemeHttps)
			{
				RequestCacheManager.s_DefaultHttpBinding = binding;
				return;
			}
			if (uriScheme == Uri.UriSchemeFtp)
			{
				RequestCacheManager.s_DefaultFtpBinding = binding;
			}
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x000CEB14 File Offset: 0x000CCD14
		private static void LoadConfigSettings()
		{
			RequestCacheBinding obj = RequestCacheManager.s_BypassCacheBinding;
			lock (obj)
			{
				if (RequestCacheManager.s_CacheConfigSettings == null)
				{
					RequestCacheManager.s_CacheConfigSettings = new RequestCachingSectionInternal();
				}
			}
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x000CEB64 File Offset: 0x000CCD64
		// Note: this type is marked as 'beforefieldinit'.
		static RequestCacheManager()
		{
		}

		// Token: 0x040023E1 RID: 9185
		private static volatile RequestCachingSectionInternal s_CacheConfigSettings;

		// Token: 0x040023E2 RID: 9186
		private static readonly RequestCacheBinding s_BypassCacheBinding = new RequestCacheBinding(null, null, new RequestCachePolicy(RequestCacheLevel.BypassCache));

		// Token: 0x040023E3 RID: 9187
		private static volatile RequestCacheBinding s_DefaultGlobalBinding;

		// Token: 0x040023E4 RID: 9188
		private static volatile RequestCacheBinding s_DefaultHttpBinding;

		// Token: 0x040023E5 RID: 9189
		private static volatile RequestCacheBinding s_DefaultFtpBinding;
	}
}
