using System;

namespace System.Net.Cache
{
	// Token: 0x02000787 RID: 1927
	internal class RequestCacheBinding
	{
		// Token: 0x06003CC0 RID: 15552 RVA: 0x000CEB87 File Offset: 0x000CCD87
		internal RequestCacheBinding(RequestCache requestCache, RequestCacheValidator cacheValidator, RequestCachePolicy policy)
		{
			this.m_RequestCache = requestCache;
			this.m_CacheValidator = cacheValidator;
			this.m_Policy = policy;
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000CEBA4 File Offset: 0x000CCDA4
		internal RequestCache Cache
		{
			get
			{
				return this.m_RequestCache;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x000CEBAC File Offset: 0x000CCDAC
		internal RequestCacheValidator Validator
		{
			get
			{
				return this.m_CacheValidator;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000CEBB4 File Offset: 0x000CCDB4
		internal RequestCachePolicy Policy
		{
			get
			{
				return this.m_Policy;
			}
		}

		// Token: 0x040023E7 RID: 9191
		private RequestCache m_RequestCache;

		// Token: 0x040023E8 RID: 9192
		private RequestCacheValidator m_CacheValidator;

		// Token: 0x040023E9 RID: 9193
		private RequestCachePolicy m_Policy;
	}
}
