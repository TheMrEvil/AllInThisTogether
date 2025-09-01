using System;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x02000090 RID: 144
	public interface IDiskFileCacheController : ICacheController
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000569 RID: 1385
		// (set) Token: 0x0600056A RID: 1386
		string AbsoluteCacheFilePath { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600056B RID: 1387
		// (set) Token: 0x0600056C RID: 1388
		string RelativeCacheFilePath { get; set; }

		// Token: 0x0600056D RID: 1389
		void RefreshPaths();
	}
}
