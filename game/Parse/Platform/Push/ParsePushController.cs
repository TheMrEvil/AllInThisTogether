using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Push;
using Parse.Abstractions.Platform.Users;
using Parse.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Push
{
	// Token: 0x0200002B RID: 43
	internal class ParsePushController : IParsePushController
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009495 File Offset: 0x00007695
		private IParseCommandRunner CommandRunner
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandRunner>k__BackingField;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000949D File Offset: 0x0000769D
		private IParseCurrentUserController CurrentUserController
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentUserController>k__BackingField;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000094A5 File Offset: 0x000076A5
		public ParsePushController(IParseCommandRunner commandRunner, IParseCurrentUserController currentUserController)
		{
			this.CommandRunner = commandRunner;
			this.CurrentUserController = currentUserController;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000094BC File Offset: 0x000076BC
		public Task SendPushNotificationAsync(IPushState state, IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.CurrentUserController.GetCurrentSessionTokenAsync(serviceHub, cancellationToken).OnSuccess((Task<string> sessionTokenTask) => this.CommandRunner.RunCommandAsync(new ParseCommand("push", "POST", sessionTokenTask.Result, null, ParsePushEncoder.Instance.Encode(state)), null, null, cancellationToken)).Unwrap<Tuple<HttpStatusCode, IDictionary<string, object>>>();
		}

		// Token: 0x04000051 RID: 81
		[CompilerGenerated]
		private readonly IParseCommandRunner <CommandRunner>k__BackingField;

		// Token: 0x04000052 RID: 82
		[CompilerGenerated]
		private readonly IParseCurrentUserController <CurrentUserController>k__BackingField;

		// Token: 0x020000FB RID: 251
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x060006C7 RID: 1735 RVA: 0x00014F22 File Offset: 0x00013122
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x00014F2A File Offset: 0x0001312A
			internal Task<Tuple<HttpStatusCode, IDictionary<string, object>>> <SendPushNotificationAsync>b__0(Task<string> sessionTokenTask)
			{
				return this.<>4__this.CommandRunner.RunCommandAsync(new ParseCommand("push", "POST", sessionTokenTask.Result, null, ParsePushEncoder.Instance.Encode(this.state)), null, null, this.cancellationToken);
			}

			// Token: 0x04000201 RID: 513
			public ParsePushController <>4__this;

			// Token: 0x04000202 RID: 514
			public IPushState state;

			// Token: 0x04000203 RID: 515
			public CancellationToken cancellationToken;
		}
	}
}
