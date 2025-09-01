using System;
using System.Collections.Generic;
using Parse.Platform.Push;

namespace Parse.Abstractions.Platform.Push
{
	// Token: 0x0200007C RID: 124
	public interface IPushState
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600050C RID: 1292
		ParseQuery<ParseInstallation> Query { get; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600050D RID: 1293
		IEnumerable<string> Channels { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600050E RID: 1294
		DateTime? Expiration { get; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600050F RID: 1295
		TimeSpan? ExpirationInterval { get; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000510 RID: 1296
		DateTime? PushTime { get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000511 RID: 1297
		IDictionary<string, object> Data { get; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000512 RID: 1298
		string Alert { get; }

		// Token: 0x06000513 RID: 1299
		IPushState MutatedClone(Action<MutablePushState> func);
	}
}
