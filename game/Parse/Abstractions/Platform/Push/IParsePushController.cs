using System;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;

namespace Parse.Abstractions.Platform.Push
{
	// Token: 0x0200007B RID: 123
	public interface IParsePushController
	{
		// Token: 0x0600050B RID: 1291
		Task SendPushNotificationAsync(IPushState state, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken));
	}
}
