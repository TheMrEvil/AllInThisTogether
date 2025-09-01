using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Installations;
using Parse.Abstractions.Platform.Push;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Push
{
	// Token: 0x0200002A RID: 42
	internal class ParsePushChannelsController : IParsePushChannelsController
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000093E6 File Offset: 0x000075E6
		private IParseCurrentInstallationController CurrentInstallationController
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentInstallationController>k__BackingField;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000093EE File Offset: 0x000075EE
		public ParsePushChannelsController(IParseCurrentInstallationController currentInstallationController)
		{
			this.CurrentInstallationController = currentInstallationController;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009400 File Offset: 0x00007600
		public Task SubscribeAsync(IEnumerable<string> channels, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CurrentInstallationController.GetAsync(serviceHub, cancellationToken).OnSuccess(delegate(Task<ParseInstallation> task)
			{
				task.Result.AddRangeUniqueToList<string>("channels", channels);
				return task.Result.SaveAsync(cancellationToken);
			}).Unwrap();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000944C File Offset: 0x0000764C
		public Task UnsubscribeAsync(IEnumerable<string> channels, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CurrentInstallationController.GetAsync(serviceHub, cancellationToken).OnSuccess(delegate(Task<ParseInstallation> task)
			{
				task.Result.RemoveAllFromList<string>("channels", channels);
				return task.Result.SaveAsync(cancellationToken);
			}).Unwrap();
		}

		// Token: 0x04000050 RID: 80
		[CompilerGenerated]
		private readonly IParseCurrentInstallationController <CurrentInstallationController>k__BackingField;

		// Token: 0x020000F9 RID: 249
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060006C3 RID: 1731 RVA: 0x00014EC0 File Offset: 0x000130C0
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x00014EC8 File Offset: 0x000130C8
			internal Task <SubscribeAsync>b__0(Task<ParseInstallation> task)
			{
				task.Result.AddRangeUniqueToList<string>("channels", this.channels);
				return task.Result.SaveAsync(this.cancellationToken);
			}

			// Token: 0x040001FD RID: 509
			public IEnumerable<string> channels;

			// Token: 0x040001FE RID: 510
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000FA RID: 250
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060006C5 RID: 1733 RVA: 0x00014EF1 File Offset: 0x000130F1
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x00014EF9 File Offset: 0x000130F9
			internal Task <UnsubscribeAsync>b__0(Task<ParseInstallation> task)
			{
				task.Result.RemoveAllFromList<string>("channels", this.channels);
				return task.Result.SaveAsync(this.cancellationToken);
			}

			// Token: 0x040001FF RID: 511
			public IEnumerable<string> channels;

			// Token: 0x04000200 RID: 512
			public CancellationToken cancellationToken;
		}
	}
}
