using System;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Objects;

namespace Parse.Abstractions.Platform.Sessions
{
	// Token: 0x02000078 RID: 120
	public interface IParseSessionController
	{
		// Token: 0x06000502 RID: 1282
		Task<IObjectState> GetSessionAsync(string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000503 RID: 1283
		Task RevokeAsync(string sessionToken, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000504 RID: 1284
		Task<IObjectState> UpgradeToRevocableSessionAsync(string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000505 RID: 1285
		bool IsRevocableSessionToken(string sessionToken);
	}
}
