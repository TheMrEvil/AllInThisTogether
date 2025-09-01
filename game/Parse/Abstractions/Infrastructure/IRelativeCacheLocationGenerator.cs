using System;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x02000096 RID: 150
	public interface IRelativeCacheLocationGenerator
	{
		// Token: 0x06000590 RID: 1424
		string GetRelativeCacheFilePath(IServiceHub serviceHub);
	}
}
