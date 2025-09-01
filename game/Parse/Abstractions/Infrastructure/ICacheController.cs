using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x0200008C RID: 140
	public interface ICacheController
	{
		// Token: 0x0600055F RID: 1375
		void Clear();

		// Token: 0x06000560 RID: 1376
		FileInfo GetRelativeFile(string path);

		// Token: 0x06000561 RID: 1377
		Task TransferAsync(string originFilePath, string targetFilePath);

		// Token: 0x06000562 RID: 1378
		Task<IDataCache<string, object>> LoadAsync();

		// Token: 0x06000563 RID: 1379
		Task<IDataCache<string, object>> SaveAsync(IDictionary<string, object> contents);
	}
}
