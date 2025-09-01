using System;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;

namespace Parse.Abstractions.Platform.Objects
{
	// Token: 0x02000080 RID: 128
	public interface IParseObjectCurrentController<T> where T : ParseObject
	{
		// Token: 0x0600052A RID: 1322
		Task SetAsync(T obj, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600052B RID: 1323
		Task<T> GetAsync(IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600052C RID: 1324
		Task<bool> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x0600052D RID: 1325
		bool IsCurrent(T obj);

		// Token: 0x0600052E RID: 1326
		void ClearFromMemory();

		// Token: 0x0600052F RID: 1327
		void ClearFromDisk();
	}
}
