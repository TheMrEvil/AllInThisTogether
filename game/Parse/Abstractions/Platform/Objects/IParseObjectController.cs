using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Control;

namespace Parse.Abstractions.Platform.Objects
{
	// Token: 0x0200007F RID: 127
	public interface IParseObjectController
	{
		// Token: 0x06000525 RID: 1317
		Task<IObjectState> FetchAsync(IObjectState state, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000526 RID: 1318
		Task<IObjectState> SaveAsync(IObjectState state, IDictionary<string, IParseFieldOperation> operations, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000527 RID: 1319
		IList<Task<IObjectState>> SaveAllAsync(IList<IObjectState> states, IList<IDictionary<string, IParseFieldOperation>> operationsList, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000528 RID: 1320
		Task DeleteAsync(IObjectState state, string sessionToken, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000529 RID: 1321
		IList<Task> DeleteAllAsync(IList<IObjectState> states, string sessionToken, CancellationToken cancellationToken = default(CancellationToken));
	}
}
