using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Platform.Objects;

namespace Parse.Abstractions.Platform.Queries
{
	// Token: 0x02000079 RID: 121
	public interface IParseQueryController
	{
		// Token: 0x06000506 RID: 1286
		Task<IEnumerable<IObjectState>> FindAsync<T>(ParseQuery<T> query, ParseUser user, CancellationToken cancellationToken = default(CancellationToken)) where T : ParseObject;

		// Token: 0x06000507 RID: 1287
		Task<int> CountAsync<T>(ParseQuery<T> query, ParseUser user, CancellationToken cancellationToken = default(CancellationToken)) where T : ParseObject;

		// Token: 0x06000508 RID: 1288
		Task<IObjectState> FirstAsync<T>(ParseQuery<T> query, ParseUser user, CancellationToken cancellationToken = default(CancellationToken)) where T : ParseObject;
	}
}
