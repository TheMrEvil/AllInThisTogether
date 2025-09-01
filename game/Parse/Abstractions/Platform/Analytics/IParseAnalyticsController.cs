using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;

namespace Parse.Abstractions.Platform.Analytics
{
	// Token: 0x0200008A RID: 138
	public interface IParseAnalyticsController
	{
		// Token: 0x06000543 RID: 1347
		Task TrackEventAsync(string name, IDictionary<string, string> dimensions, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000544 RID: 1348
		Task TrackAppOpenedAsync(string pushHash, string sessionToken, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));
	}
}
