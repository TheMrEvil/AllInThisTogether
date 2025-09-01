using System;

namespace System.Net.Cache
{
	/// <summary>Specifies the meaning of time values that control caching behavior for resources obtained using <see cref="T:System.Net.HttpWebRequest" /> objects.</summary>
	// Token: 0x0200078B RID: 1931
	public enum HttpCacheAgeControl
	{
		/// <summary>For internal use only. The Framework will throw an <see cref="T:System.ArgumentException" /> if you try to use this member.</summary>
		// Token: 0x040023FE RID: 9214
		None,
		/// <summary>Content can be taken from the cache if the time remaining before expiration is greater than or equal to the time specified with this value.</summary>
		// Token: 0x040023FF RID: 9215
		MinFresh,
		/// <summary>Content can be taken from the cache until it is older than the age specified with this value.</summary>
		// Token: 0x04002400 RID: 9216
		MaxAge,
		/// <summary>Content can be taken from the cache after it has expired, until the time specified with this value elapses.</summary>
		// Token: 0x04002401 RID: 9217
		MaxStale = 4,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MinFresh" />.</summary>
		// Token: 0x04002402 RID: 9218
		MaxAgeAndMinFresh = 3,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxStale" />.</summary>
		// Token: 0x04002403 RID: 9219
		MaxAgeAndMaxStale = 6
	}
}
