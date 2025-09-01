using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Authentication;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Utilities;

namespace Parse
{
	// Token: 0x02000024 RID: 36
	public static class UserServiceExtensions
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x000084FC File Offset: 0x000066FC
		internal static string GetCurrentSessionToken(this IServiceHub serviceHub)
		{
			Task<string> currentSessionTokenAsync = serviceHub.GetCurrentSessionTokenAsync(default(CancellationToken));
			currentSessionTokenAsync.Wait();
			return currentSessionTokenAsync.Result;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008523 File Offset: 0x00006723
		internal static Task<string> GetCurrentSessionTokenAsync(this IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.CurrentUserController.GetCurrentSessionTokenAsync(serviceHub, cancellationToken);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008532 File Offset: 0x00006732
		public static Task SignUpAsync(this IServiceHub serviceHub, string username, string password, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ParseUser
			{
				Services = serviceHub,
				Username = username,
				Password = password
			}.SignUpAsync(cancellationToken);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008554 File Offset: 0x00006754
		public static Task SignUpAsync(this IServiceHub serviceHub, ParseUser user, CancellationToken cancellationToken = default(CancellationToken))
		{
			user.Bind(serviceHub);
			return user.SignUpAsync(cancellationToken);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008568 File Offset: 0x00006768
		public static Task<ParseUser> LogInAsync(this IServiceHub serviceHub, string username, string password, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.UserController.LogInAsync(username, password, serviceHub, cancellationToken).OnSuccess(delegate(Task<IObjectState> task)
			{
				ParseUser user = serviceHub.GenerateObjectFromState(task.Result, "_User");
				return serviceHub.SaveCurrentUserAsync(user, default(CancellationToken)).OnSuccess((Task _) => user);
			}).Unwrap<ParseUser>();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000085B4 File Offset: 0x000067B4
		public static Task<ParseUser> BecomeAsync(this IServiceHub serviceHub, string sessionToken, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.UserController.GetUserAsync(sessionToken, serviceHub, cancellationToken).OnSuccess(delegate(Task<IObjectState> t)
			{
				ParseUser user = serviceHub.GenerateObjectFromState(t.Result, "_User");
				return serviceHub.SaveCurrentUserAsync(user, default(CancellationToken)).OnSuccess((Task _) => user);
			}).Unwrap<ParseUser>();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000085FC File Offset: 0x000067FC
		public static void LogOut(this IServiceHub serviceHub)
		{
			serviceHub.LogOutAsync().Wait();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008609 File Offset: 0x00006809
		public static Task LogOutAsync(this IServiceHub serviceHub)
		{
			return serviceHub.LogOutAsync(CancellationToken.None);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008618 File Offset: 0x00006818
		public static Task LogOutAsync(this IServiceHub serviceHub, CancellationToken cancellationToken)
		{
			return serviceHub.GetCurrentUserAsync(default(CancellationToken)).OnSuccess(delegate(Task<ParseUser> task)
			{
				UserServiceExtensions.LogOutWithProviders();
				ParseUser user = task.Result;
				if (user == null)
				{
					return Task.CompletedTask;
				}
				return user.TaskQueue.Enqueue<Task>((Task toAwait) => user.LogOutAsync(toAwait, cancellationToken), cancellationToken);
			}).Unwrap();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008658 File Offset: 0x00006858
		private static void LogOutWithProviders()
		{
			foreach (IParseAuthenticationProvider parseAuthenticationProvider in ParseUser.Authenticators.Values)
			{
				parseAuthenticationProvider.Deauthenticate();
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000086A8 File Offset: 0x000068A8
		public static ParseUser GetCurrentUser(this IServiceHub serviceHub)
		{
			Task<ParseUser> currentUserAsync = serviceHub.GetCurrentUserAsync(default(CancellationToken));
			currentUserAsync.Wait();
			return currentUserAsync.Result;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000086CF File Offset: 0x000068CF
		internal static Task<ParseUser> GetCurrentUserAsync(this IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.CurrentUserController.GetAsync(serviceHub, cancellationToken);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000086DE File Offset: 0x000068DE
		internal static Task SaveCurrentUserAsync(this IServiceHub serviceHub, ParseUser user, CancellationToken cancellationToken = default(CancellationToken))
		{
			return serviceHub.CurrentUserController.SetAsync(user, cancellationToken);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000086ED File Offset: 0x000068ED
		internal static void ClearInMemoryUser(this IServiceHub serviceHub)
		{
			serviceHub.CurrentUserController.ClearFromMemory();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000086FA File Offset: 0x000068FA
		public static ParseQuery<ParseUser> GetUserQuery(this IServiceHub serviceHub)
		{
			return serviceHub.GetQuery<ParseUser>();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008704 File Offset: 0x00006904
		public static Task EnableRevocableSessionAsync(this IServiceHub serviceHub, CancellationToken cancellationToken = default(CancellationToken))
		{
			object revocableSessionEnabledMutex = serviceHub.UserController.RevocableSessionEnabledMutex;
			lock (revocableSessionEnabledMutex)
			{
				serviceHub.UserController.RevocableSessionEnabled = true;
			}
			return serviceHub.GetCurrentUserAsync(cancellationToken).OnSuccess((Task<ParseUser> task) => task.Result.UpgradeToRevocableSessionAsync(cancellationToken));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000877C File Offset: 0x0000697C
		internal static void DisableRevocableSession(this IServiceHub serviceHub)
		{
			object revocableSessionEnabledMutex = serviceHub.UserController.RevocableSessionEnabledMutex;
			lock (revocableSessionEnabledMutex)
			{
				serviceHub.UserController.RevocableSessionEnabled = false;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000087C8 File Offset: 0x000069C8
		internal static bool GetIsRevocableSessionEnabled(this IServiceHub serviceHub)
		{
			object revocableSessionEnabledMutex = serviceHub.UserController.RevocableSessionEnabledMutex;
			bool revocableSessionEnabled;
			lock (revocableSessionEnabledMutex)
			{
				revocableSessionEnabled = serviceHub.UserController.RevocableSessionEnabled;
			}
			return revocableSessionEnabled;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008814 File Offset: 0x00006A14
		public static Task RequestPasswordResetAsync(this IServiceHub serviceHub, string email)
		{
			return serviceHub.RequestPasswordResetAsync(email, CancellationToken.None);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008822 File Offset: 0x00006A22
		public static Task RequestPasswordResetAsync(this IServiceHub serviceHub, string email, CancellationToken cancellationToken)
		{
			return serviceHub.UserController.RequestPasswordResetAsync(email, cancellationToken);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008834 File Offset: 0x00006A34
		public static Task<ParseUser> LogInWithAsync(this IServiceHub serviceHub, string authType, IDictionary<string, object> data, CancellationToken cancellationToken)
		{
			ParseUser user = null;
			return serviceHub.UserController.LogInAsync(authType, data, serviceHub, cancellationToken).OnSuccess(delegate(Task<IObjectState> task)
			{
				user = serviceHub.GenerateObjectFromState(task.Result, "_User");
				object mutex = user.Mutex;
				lock (mutex)
				{
					if (user.AuthData == null)
					{
						user.AuthData = new Dictionary<string, IDictionary<string, object>>();
					}
					user.AuthData[authType] = data;
					user.SynchronizeAllAuthData();
				}
				return serviceHub.SaveCurrentUserAsync(user, default(CancellationToken));
			}).Unwrap().OnSuccess((Task t) => user);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000088B0 File Offset: 0x00006AB0
		public static Task<ParseUser> LogInWithAsync(this IServiceHub serviceHub, string authType, CancellationToken cancellationToken)
		{
			return ParseUser.GetProvider(authType).AuthenticateAsync(cancellationToken).OnSuccess((Task<IDictionary<string, object>> authData) => serviceHub.LogInWithAsync(authType, authData.Result, cancellationToken)).Unwrap<ParseUser>();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008904 File Offset: 0x00006B04
		internal static void RegisterProvider(this IServiceHub serviceHub, IParseAuthenticationProvider provider)
		{
			ParseUser.Authenticators[provider.AuthType] = provider;
			ParseUser currentUser = serviceHub.GetCurrentUser();
			if (currentUser != null)
			{
				currentUser.SynchronizeAuthData(provider);
			}
		}

		// Token: 0x020000DE RID: 222
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x0600067F RID: 1663 RVA: 0x00014549 File Offset: 0x00012749
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06000680 RID: 1664 RVA: 0x00014554 File Offset: 0x00012754
			internal Task<ParseUser> <LogInAsync>b__0(Task<IObjectState> task)
			{
				UserServiceExtensions.<>c__DisplayClass4_1 CS$<>8__locals1 = new UserServiceExtensions.<>c__DisplayClass4_1();
				CS$<>8__locals1.user = this.serviceHub.GenerateObjectFromState(task.Result, "_User");
				return this.serviceHub.SaveCurrentUserAsync(CS$<>8__locals1.user, default(CancellationToken)).OnSuccess(new Func<Task, ParseUser>(CS$<>8__locals1.<LogInAsync>b__1));
			}

			// Token: 0x040001C3 RID: 451
			public IServiceHub serviceHub;
		}

		// Token: 0x020000DF RID: 223
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_1
		{
			// Token: 0x06000681 RID: 1665 RVA: 0x000145AE File Offset: 0x000127AE
			public <>c__DisplayClass4_1()
			{
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x000145B6 File Offset: 0x000127B6
			internal ParseUser <LogInAsync>b__1(Task _)
			{
				return this.user;
			}

			// Token: 0x040001C4 RID: 452
			public ParseUser user;
		}

		// Token: 0x020000E0 RID: 224
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000683 RID: 1667 RVA: 0x000145BE File Offset: 0x000127BE
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x000145C8 File Offset: 0x000127C8
			internal Task<ParseUser> <BecomeAsync>b__0(Task<IObjectState> t)
			{
				UserServiceExtensions.<>c__DisplayClass5_1 CS$<>8__locals1 = new UserServiceExtensions.<>c__DisplayClass5_1();
				CS$<>8__locals1.user = this.serviceHub.GenerateObjectFromState(t.Result, "_User");
				return this.serviceHub.SaveCurrentUserAsync(CS$<>8__locals1.user, default(CancellationToken)).OnSuccess(new Func<Task, ParseUser>(CS$<>8__locals1.<BecomeAsync>b__1));
			}

			// Token: 0x040001C5 RID: 453
			public IServiceHub serviceHub;
		}

		// Token: 0x020000E1 RID: 225
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_1
		{
			// Token: 0x06000685 RID: 1669 RVA: 0x00014622 File Offset: 0x00012822
			public <>c__DisplayClass5_1()
			{
			}

			// Token: 0x06000686 RID: 1670 RVA: 0x0001462A File Offset: 0x0001282A
			internal ParseUser <BecomeAsync>b__1(Task _)
			{
				return this.user;
			}

			// Token: 0x040001C6 RID: 454
			public ParseUser user;
		}

		// Token: 0x020000E2 RID: 226
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06000687 RID: 1671 RVA: 0x00014632 File Offset: 0x00012832
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x0001463C File Offset: 0x0001283C
			internal Task <LogOutAsync>b__0(Task<ParseUser> task)
			{
				UserServiceExtensions.<>c__DisplayClass8_1 CS$<>8__locals1 = new UserServiceExtensions.<>c__DisplayClass8_1();
				CS$<>8__locals1.CS$<>8__locals1 = this;
				UserServiceExtensions.LogOutWithProviders();
				CS$<>8__locals1.user = task.Result;
				if (CS$<>8__locals1.user == null)
				{
					return Task.CompletedTask;
				}
				return CS$<>8__locals1.user.TaskQueue.Enqueue<Task>(new Func<Task, Task>(CS$<>8__locals1.<LogOutAsync>b__1), this.cancellationToken);
			}

			// Token: 0x040001C7 RID: 455
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000E3 RID: 227
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_1
		{
			// Token: 0x06000689 RID: 1673 RVA: 0x00014697 File Offset: 0x00012897
			public <>c__DisplayClass8_1()
			{
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x0001469F File Offset: 0x0001289F
			internal Task <LogOutAsync>b__1(Task toAwait)
			{
				return this.user.LogOutAsync(toAwait, this.CS$<>8__locals1.cancellationToken);
			}

			// Token: 0x040001C8 RID: 456
			public ParseUser user;

			// Token: 0x040001C9 RID: 457
			public UserServiceExtensions.<>c__DisplayClass8_0 CS$<>8__locals1;
		}

		// Token: 0x020000E4 RID: 228
		[CompilerGenerated]
		private sealed class <>c__DisplayClass15_0
		{
			// Token: 0x0600068B RID: 1675 RVA: 0x000146B8 File Offset: 0x000128B8
			public <>c__DisplayClass15_0()
			{
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x000146C0 File Offset: 0x000128C0
			internal Task <EnableRevocableSessionAsync>b__0(Task<ParseUser> task)
			{
				return task.Result.UpgradeToRevocableSessionAsync(this.cancellationToken);
			}

			// Token: 0x040001CA RID: 458
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000E5 RID: 229
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x0600068D RID: 1677 RVA: 0x000146D3 File Offset: 0x000128D3
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x000146DC File Offset: 0x000128DC
			internal Task <LogInWithAsync>b__0(Task<IObjectState> task)
			{
				this.user = this.serviceHub.GenerateObjectFromState(task.Result, "_User");
				object mutex = this.user.Mutex;
				lock (mutex)
				{
					if (this.user.AuthData == null)
					{
						this.user.AuthData = new Dictionary<string, IDictionary<string, object>>();
					}
					this.user.AuthData[this.authType] = this.data;
					this.user.SynchronizeAllAuthData();
				}
				return this.serviceHub.SaveCurrentUserAsync(this.user, default(CancellationToken));
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x00014798 File Offset: 0x00012998
			internal ParseUser <LogInWithAsync>b__1(Task t)
			{
				return this.user;
			}

			// Token: 0x040001CB RID: 459
			public ParseUser user;

			// Token: 0x040001CC RID: 460
			public IServiceHub serviceHub;

			// Token: 0x040001CD RID: 461
			public string authType;

			// Token: 0x040001CE RID: 462
			public IDictionary<string, object> data;
		}

		// Token: 0x020000E6 RID: 230
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x06000690 RID: 1680 RVA: 0x000147A0 File Offset: 0x000129A0
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x06000691 RID: 1681 RVA: 0x000147A8 File Offset: 0x000129A8
			internal Task<ParseUser> <LogInWithAsync>b__0(Task<IDictionary<string, object>> authData)
			{
				return this.serviceHub.LogInWithAsync(this.authType, authData.Result, this.cancellationToken);
			}

			// Token: 0x040001CF RID: 463
			public IServiceHub serviceHub;

			// Token: 0x040001D0 RID: 464
			public string authType;

			// Token: 0x040001D1 RID: 465
			public CancellationToken cancellationToken;
		}
	}
}
