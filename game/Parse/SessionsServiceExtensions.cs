using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Utilities;

namespace Parse
{
	// Token: 0x02000023 RID: 35
	public static class SessionsServiceExtensions
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00008410 File Offset: 0x00006610
		public static ParseQuery<ParseSession> GetSessionQuery(this IServiceHub serviceHub)
		{
			return serviceHub.GetQuery<ParseSession>();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008418 File Offset: 0x00006618
		public static Task<ParseSession> GetCurrentSessionAsync(this IServiceHub serviceHub)
		{
			return serviceHub.GetCurrentSessionAsync(CancellationToken.None);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008428 File Offset: 0x00006628
		public static Task<ParseSession> GetCurrentSessionAsync(this IServiceHub serviceHub, CancellationToken cancellationToken)
		{
			Func<Task<IObjectState>, ParseSession> <>9__1;
			return serviceHub.GetCurrentUserAsync(default(CancellationToken)).OnSuccess(delegate(Task<ParseUser> task)
			{
				ParseUser result = task.Result;
				Task<ParseSession> result2;
				if (result != null)
				{
					string sessionToken = result.SessionToken;
					if (sessionToken != null)
					{
						Task<IObjectState> sessionAsync = serviceHub.SessionController.GetSessionAsync(sessionToken, serviceHub, cancellationToken);
						Func<Task<IObjectState>, ParseSession> continuation;
						if ((continuation = <>9__1) == null)
						{
							continuation = (<>9__1 = ((Task<IObjectState> successTask) => serviceHub.GenerateObjectFromState(successTask.Result, "_Session")));
						}
						result2 = sessionAsync.OnSuccess(continuation);
					}
					else
					{
						result2 = Task.FromResult<ParseSession>(null);
					}
				}
				else
				{
					result2 = Task.FromResult<ParseSession>(null);
				}
				return result2;
			}).Unwrap<ParseSession>();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008473 File Offset: 0x00006673
		public static Task RevokeSessionAsync(this IServiceHub serviceHub, string sessionToken, CancellationToken cancellationToken)
		{
			if (sessionToken != null && serviceHub.SessionController.IsRevocableSessionToken(sessionToken))
			{
				return serviceHub.SessionController.RevokeAsync(sessionToken, cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000849C File Offset: 0x0000669C
		public static Task<string> UpgradeToRevocableSessionAsync(this IServiceHub serviceHub, string sessionToken, CancellationToken cancellationToken)
		{
			if (sessionToken != null && !serviceHub.SessionController.IsRevocableSessionToken(sessionToken))
			{
				return serviceHub.SessionController.UpgradeToRevocableSessionAsync(sessionToken, serviceHub, cancellationToken).OnSuccess((Task<IObjectState> task) => serviceHub.GenerateObjectFromState(task.Result, "_Session").SessionToken);
			}
			return Task.FromResult<string>(sessionToken);
		}

		// Token: 0x020000DC RID: 220
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x0600067A RID: 1658 RVA: 0x00014489 File Offset: 0x00012689
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x0600067B RID: 1659 RVA: 0x00014494 File Offset: 0x00012694
			internal Task<ParseSession> <GetCurrentSessionAsync>b__0(Task<ParseUser> task)
			{
				ParseUser result = task.Result;
				Task<ParseSession> result2;
				if (result != null)
				{
					string sessionToken = result.SessionToken;
					if (sessionToken != null)
					{
						Task<IObjectState> sessionAsync = this.serviceHub.SessionController.GetSessionAsync(sessionToken, this.serviceHub, this.cancellationToken);
						Func<Task<IObjectState>, ParseSession> continuation;
						if ((continuation = this.<>9__1) == null)
						{
							continuation = (this.<>9__1 = ((Task<IObjectState> successTask) => this.serviceHub.GenerateObjectFromState(successTask.Result, "_Session")));
						}
						result2 = sessionAsync.OnSuccess(continuation);
					}
					else
					{
						result2 = Task.FromResult<ParseSession>(null);
					}
				}
				else
				{
					result2 = Task.FromResult<ParseSession>(null);
				}
				return result2;
			}

			// Token: 0x0600067C RID: 1660 RVA: 0x0001450C File Offset: 0x0001270C
			internal ParseSession <GetCurrentSessionAsync>b__1(Task<IObjectState> successTask)
			{
				return this.serviceHub.GenerateObjectFromState(successTask.Result, "_Session");
			}

			// Token: 0x040001BF RID: 447
			public IServiceHub serviceHub;

			// Token: 0x040001C0 RID: 448
			public CancellationToken cancellationToken;

			// Token: 0x040001C1 RID: 449
			public Func<Task<IObjectState>, ParseSession> <>9__1;
		}

		// Token: 0x020000DD RID: 221
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x0600067D RID: 1661 RVA: 0x00014524 File Offset: 0x00012724
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x0001452C File Offset: 0x0001272C
			internal string <UpgradeToRevocableSessionAsync>b__0(Task<IObjectState> task)
			{
				return this.serviceHub.GenerateObjectFromState(task.Result, "_Session").SessionToken;
			}

			// Token: 0x040001C2 RID: 450
			public IServiceHub serviceHub;
		}
	}
}
