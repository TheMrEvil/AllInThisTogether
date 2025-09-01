using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;

namespace Parse.Abstractions.Platform.Push
{
	// Token: 0x0200007A RID: 122
	public interface IParsePushChannelsController
	{
		// Token: 0x06000509 RID: 1289
		Task SubscribeAsync(IEnumerable<string> channels, IServiceHub serviceHub, CancellationToken cancellationToken);

		// Token: 0x0600050A RID: 1290
		Task UnsubscribeAsync(IEnumerable<string> channels, IServiceHub serviceHub, CancellationToken cancellationToken);
	}
}
