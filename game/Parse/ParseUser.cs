using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Abstractions.Infrastructure.Control;
using Parse.Abstractions.Platform.Authentication;
using Parse.Abstractions.Platform.Objects;
using Parse.Infrastructure.Utilities;
using Parse.Platform.Objects;

namespace Parse
{
	// Token: 0x02000018 RID: 24
	[ParseClassName("_User")]
	public class ParseUser : ParseObject
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006F00 File Offset: 0x00005100
		public bool IsAuthenticated
		{
			get
			{
				object mutex = base.Mutex;
				bool result;
				lock (mutex)
				{
					bool flag2;
					if (this.SessionToken != null)
					{
						ParseUser currentUser = base.Services.GetCurrentUser();
						if (currentUser != null)
						{
							flag2 = (currentUser.ObjectId == base.ObjectId);
							goto IL_3C;
						}
					}
					flag2 = false;
					IL_3C:
					result = flag2;
				}
				return result;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006F68 File Offset: 0x00005168
		public override void Remove(string key)
		{
			if (key == "username")
			{
				throw new InvalidOperationException("Cannot remove the username key.");
			}
			base.Remove(key);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006F89 File Offset: 0x00005189
		protected override bool CheckKeyMutable(string key)
		{
			return !ParseUser.ImmutableKeys.Contains(key);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006F99 File Offset: 0x00005199
		internal override void HandleSave(IObjectState serverState)
		{
			base.HandleSave(serverState);
			this.SynchronizeAllAuthData();
			this.CleanupAuthData();
			base.MutateState(delegate(MutableObjectState mutableClone)
			{
				mutableClone.ServerData.Remove("password");
			});
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006FD3 File Offset: 0x000051D3
		public string SessionToken
		{
			get
			{
				if (!base.State.ContainsKey("sessionToken"))
				{
					return null;
				}
				return base.State["sessionToken"] as string;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006FFE File Offset: 0x000051FE
		internal Task SetSessionTokenAsync(string newSessionToken)
		{
			return this.SetSessionTokenAsync(newSessionToken, CancellationToken.None);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000700C File Offset: 0x0000520C
		internal Task SetSessionTokenAsync(string newSessionToken, CancellationToken cancellationToken)
		{
			base.MutateState(delegate(MutableObjectState mutableClone)
			{
				mutableClone.ServerData["sessionToken"] = newSessionToken;
			});
			return base.Services.SaveCurrentUserAsync(this, default(CancellationToken));
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000704D File Offset: 0x0000524D
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000705B File Offset: 0x0000525B
		[ParseFieldName("username")]
		public string Username
		{
			get
			{
				return base.GetProperty<string>(null, "Username");
			}
			set
			{
				base.SetProperty<string>(value, "Username");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007069 File Offset: 0x00005269
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00007077 File Offset: 0x00005277
		[ParseFieldName("password")]
		public string Password
		{
			get
			{
				return base.GetProperty<string>(null, "Password");
			}
			set
			{
				base.SetProperty<string>(value, "Password");
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00007085 File Offset: 0x00005285
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00007093 File Offset: 0x00005293
		[ParseFieldName("email")]
		public string Email
		{
			get
			{
				return base.GetProperty<string>(null, "Email");
			}
			set
			{
				base.SetProperty<string>(value, "Email");
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000070A4 File Offset: 0x000052A4
		internal Task SignUpAsync(Task toAwait, CancellationToken cancellationToken)
		{
			if (this.AuthData == null)
			{
				if (string.IsNullOrEmpty(this.Username))
				{
					TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
					taskCompletionSource.TrySetException(new InvalidOperationException("Cannot sign up user with an empty name."));
					return taskCompletionSource.Task;
				}
				if (string.IsNullOrEmpty(this.Password))
				{
					TaskCompletionSource<object> taskCompletionSource2 = new TaskCompletionSource<object>();
					taskCompletionSource2.TrySetException(new InvalidOperationException("Cannot sign up user with an empty password."));
					return taskCompletionSource2.Task;
				}
			}
			if (!string.IsNullOrEmpty(base.ObjectId))
			{
				TaskCompletionSource<object> taskCompletionSource3 = new TaskCompletionSource<object>();
				taskCompletionSource3.TrySetException(new InvalidOperationException("Cannot sign up a user that already exists."));
				return taskCompletionSource3.Task;
			}
			IDictionary<string, IParseFieldOperation> currentOperations = base.StartSave();
			return toAwait.OnSuccess((Task _) => this.Services.UserController.SignUpAsync(this.State, currentOperations, this.Services, cancellationToken)).Unwrap<IObjectState>().ContinueWith<Task<IObjectState>>(delegate(Task<IObjectState> t)
			{
				if (t.IsFaulted || t.IsCanceled)
				{
					this.HandleFailedSave(currentOperations);
				}
				else
				{
					this.HandleSave(t.Result);
				}
				return t;
			}).Unwrap<IObjectState>().OnSuccess((Task<IObjectState> _) => this.Services.SaveCurrentUserAsync(this, default(CancellationToken))).Unwrap();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007197 File Offset: 0x00005397
		public Task SignUpAsync()
		{
			return this.SignUpAsync(CancellationToken.None);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000071A4 File Offset: 0x000053A4
		public Task SignUpAsync(CancellationToken cancellationToken)
		{
			return base.TaskQueue.Enqueue<Task>((Task toAwait) => this.SignUpAsync(toAwait, cancellationToken), cancellationToken);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000071E4 File Offset: 0x000053E4
		protected override Task SaveAsync(Task toAwait, CancellationToken cancellationToken)
		{
			object mutex = base.Mutex;
			Task result;
			lock (mutex)
			{
				if (base.ObjectId == null)
				{
					throw new InvalidOperationException("You must call SignUpAsync before calling SaveAsync.");
				}
				result = base.SaveAsync(toAwait, cancellationToken).OnSuccess(delegate(Task _)
				{
					if (!base.Services.CurrentUserController.IsCurrent(this))
					{
						return Task.CompletedTask;
					}
					return base.Services.SaveCurrentUserAsync(this, default(CancellationToken));
				}).Unwrap();
			}
			return result;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007254 File Offset: 0x00005454
		internal override Task<ParseObject> FetchAsyncInternal(Task toAwait, CancellationToken cancellationToken)
		{
			return base.FetchAsyncInternal(toAwait, cancellationToken).OnSuccess(delegate(Task<ParseObject> t)
			{
				if (base.Services.CurrentUserController.IsCurrent(this))
				{
					return base.Services.SaveCurrentUserAsync(this, default(CancellationToken)).OnSuccess((Task _) => t.Result);
				}
				return Task.FromResult<ParseObject>(t.Result);
			}).Unwrap<ParseObject>();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007274 File Offset: 0x00005474
		internal Task LogOutAsync(Task toAwait, CancellationToken cancellationToken)
		{
			string sessionToken = this.SessionToken;
			if (sessionToken == null)
			{
				return Task.FromResult<int>(0);
			}
			base.MutateState(delegate(MutableObjectState mutableClone)
			{
				mutableClone.ServerData.Remove("sessionToken");
			});
			Task task = base.Services.RevokeSessionAsync(sessionToken, cancellationToken);
			return Task.WhenAll(new Task[]
			{
				task,
				base.Services.CurrentUserController.LogOutAsync(base.Services, cancellationToken)
			});
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000072EE File Offset: 0x000054EE
		internal Task UpgradeToRevocableSessionAsync()
		{
			return this.UpgradeToRevocableSessionAsync(CancellationToken.None);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000072FC File Offset: 0x000054FC
		internal Task UpgradeToRevocableSessionAsync(CancellationToken cancellationToken)
		{
			return base.TaskQueue.Enqueue<Task>((Task toAwait) => this.UpgradeToRevocableSessionAsync(toAwait, cancellationToken), cancellationToken);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000733C File Offset: 0x0000553C
		internal Task UpgradeToRevocableSessionAsync(Task toAwait, CancellationToken cancellationToken)
		{
			string sessionToken = this.SessionToken;
			return toAwait.OnSuccess((Task _) => this.Services.UpgradeToRevocableSessionAsync(sessionToken, cancellationToken)).Unwrap<string>().OnSuccess((Task<string> task) => this.SetSessionTokenAsync(task.Result)).Unwrap();
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007398 File Offset: 0x00005598
		// (set) Token: 0x06000177 RID: 375 RVA: 0x000073B7 File Offset: 0x000055B7
		public IDictionary<string, IDictionary<string, object>> AuthData
		{
			get
			{
				IDictionary<string, IDictionary<string, object>> result;
				if (!base.TryGetValue<IDictionary<string, IDictionary<string, object>>>("authData", out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				this["authData"] = value;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000073C8 File Offset: 0x000055C8
		private void CleanupAuthData()
		{
			object mutex = base.Mutex;
			lock (mutex)
			{
				if (base.Services.CurrentUserController.IsCurrent(this))
				{
					IDictionary<string, IDictionary<string, object>> authData = this.AuthData;
					if (authData != null)
					{
						foreach (KeyValuePair<string, IDictionary<string, object>> keyValuePair in new Dictionary<string, IDictionary<string, object>>(authData))
						{
							if (keyValuePair.Value == null)
							{
								authData.Remove(keyValuePair.Key);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007474 File Offset: 0x00005674
		internal static IParseAuthenticationProvider GetProvider(string providerName)
		{
			IParseAuthenticationProvider result;
			if (!ParseUser.Authenticators.TryGetValue(providerName, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00007493 File Offset: 0x00005693
		internal static IDictionary<string, IParseAuthenticationProvider> Authenticators
		{
			[CompilerGenerated]
			get
			{
				return ParseUser.<Authenticators>k__BackingField;
			}
		} = new Dictionary<string, IParseAuthenticationProvider>();

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000749A File Offset: 0x0000569A
		internal static HashSet<string> ImmutableKeys
		{
			[CompilerGenerated]
			get
			{
				return ParseUser.<ImmutableKeys>k__BackingField;
			}
		} = new HashSet<string>
		{
			"sessionToken",
			"isNew"
		};

		// Token: 0x0600017C RID: 380 RVA: 0x000074A4 File Offset: 0x000056A4
		internal void SynchronizeAllAuthData()
		{
			object mutex = base.Mutex;
			lock (mutex)
			{
				IDictionary<string, IDictionary<string, object>> authData = this.AuthData;
				if (authData != null)
				{
					foreach (KeyValuePair<string, IDictionary<string, object>> keyValuePair in authData)
					{
						this.SynchronizeAuthData(ParseUser.GetProvider(keyValuePair.Key));
					}
				}
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000752C File Offset: 0x0000572C
		internal void SynchronizeAuthData(IParseAuthenticationProvider provider)
		{
			bool flag = false;
			object mutex = base.Mutex;
			lock (mutex)
			{
				IDictionary<string, IDictionary<string, object>> authData = this.AuthData;
				if (authData == null || provider == null)
				{
					return;
				}
				IDictionary<string, object> authData2;
				if (authData.TryGetValue(provider.AuthType, out authData2))
				{
					flag = provider.RestoreAuthentication(authData2);
				}
			}
			if (!flag)
			{
				this.UnlinkFromAsync(provider.AuthType, CancellationToken.None);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000075A8 File Offset: 0x000057A8
		internal Task LinkWithAsync(string authType, IDictionary<string, object> data, CancellationToken cancellationToken)
		{
			return base.TaskQueue.Enqueue<Task>(delegate(Task toAwait)
			{
				IDictionary<string, IDictionary<string, object>> dictionary = this.AuthData;
				if (dictionary == null)
				{
					dictionary = (this.AuthData = new Dictionary<string, IDictionary<string, object>>());
				}
				dictionary[authType] = data;
				this.AuthData = dictionary;
				return this.SaveAsync(cancellationToken);
			}, cancellationToken);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000075F4 File Offset: 0x000057F4
		internal Task LinkWithAsync(string authType, CancellationToken cancellationToken)
		{
			return ParseUser.GetProvider(authType).AuthenticateAsync(cancellationToken).OnSuccess((Task<IDictionary<string, object>> t) => this.LinkWithAsync(authType, t.Result, cancellationToken)).Unwrap();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007648 File Offset: 0x00005848
		internal Task UnlinkFromAsync(string authType, CancellationToken cancellationToken)
		{
			return this.LinkWithAsync(authType, null, cancellationToken);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007654 File Offset: 0x00005854
		internal bool IsLinked(string authType)
		{
			object mutex = base.Mutex;
			bool result;
			lock (mutex)
			{
				result = (this.AuthData != null && this.AuthData.ContainsKey(authType) && this.AuthData[authType] != null);
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000076B8 File Offset: 0x000058B8
		public ParseUser() : base(null)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000076C1 File Offset: 0x000058C1
		// Note: this type is marked as 'beforefieldinit'.
		static ParseUser()
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000076F0 File Offset: 0x000058F0
		[CompilerGenerated]
		private Task <SaveAsync>b__21_0(Task _)
		{
			if (!base.Services.CurrentUserController.IsCurrent(this))
			{
				return Task.CompletedTask;
			}
			return base.Services.SaveCurrentUserAsync(this, default(CancellationToken));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000772C File Offset: 0x0000592C
		[CompilerGenerated]
		private Task<ParseObject> <FetchAsyncInternal>b__22_0(Task<ParseObject> t)
		{
			if (base.Services.CurrentUserController.IsCurrent(this))
			{
				return base.Services.SaveCurrentUserAsync(this, default(CancellationToken)).OnSuccess((Task _) => t.Result);
			}
			return Task.FromResult<ParseObject>(t.Result);
		}

		// Token: 0x04000036 RID: 54
		[CompilerGenerated]
		private static readonly IDictionary<string, IParseAuthenticationProvider> <Authenticators>k__BackingField;

		// Token: 0x04000037 RID: 55
		[CompilerGenerated]
		private static readonly HashSet<string> <ImmutableKeys>k__BackingField;

		// Token: 0x020000BC RID: 188
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600060D RID: 1549 RVA: 0x00013446 File Offset: 0x00011646
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x00013452 File Offset: 0x00011652
			public <>c()
			{
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x0001345A File Offset: 0x0001165A
			internal void <HandleSave>b__4_0(MutableObjectState mutableClone)
			{
				mutableClone.ServerData.Remove("password");
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x0001346D File Offset: 0x0001166D
			internal void <LogOutAsync>b__23_0(MutableObjectState mutableClone)
			{
				mutableClone.ServerData.Remove("sessionToken");
			}

			// Token: 0x04000151 RID: 337
			public static readonly ParseUser.<>c <>9 = new ParseUser.<>c();

			// Token: 0x04000152 RID: 338
			public static Action<MutableObjectState> <>9__4_0;

			// Token: 0x04000153 RID: 339
			public static Action<MutableObjectState> <>9__23_0;
		}

		// Token: 0x020000BD RID: 189
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06000611 RID: 1553 RVA: 0x00013480 File Offset: 0x00011680
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x00013488 File Offset: 0x00011688
			internal void <SetSessionTokenAsync>b__0(MutableObjectState mutableClone)
			{
				mutableClone.ServerData["sessionToken"] = this.newSessionToken;
			}

			// Token: 0x04000154 RID: 340
			public string newSessionToken;
		}

		// Token: 0x020000BE RID: 190
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x06000613 RID: 1555 RVA: 0x000134A0 File Offset: 0x000116A0
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x000134A8 File Offset: 0x000116A8
			internal Task<IObjectState> <SignUpAsync>b__0(Task _)
			{
				return this.<>4__this.Services.UserController.SignUpAsync(this.<>4__this.State, this.currentOperations, this.<>4__this.Services, this.cancellationToken);
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x000134E1 File Offset: 0x000116E1
			internal Task<IObjectState> <SignUpAsync>b__1(Task<IObjectState> t)
			{
				if (t.IsFaulted || t.IsCanceled)
				{
					this.<>4__this.HandleFailedSave(this.currentOperations);
				}
				else
				{
					this.<>4__this.HandleSave(t.Result);
				}
				return t;
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x00013518 File Offset: 0x00011718
			internal Task <SignUpAsync>b__2(Task<IObjectState> _)
			{
				return this.<>4__this.Services.SaveCurrentUserAsync(this.<>4__this, default(CancellationToken));
			}

			// Token: 0x04000155 RID: 341
			public ParseUser <>4__this;

			// Token: 0x04000156 RID: 342
			public IDictionary<string, IParseFieldOperation> currentOperations;

			// Token: 0x04000157 RID: 343
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000BF RID: 191
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x06000617 RID: 1559 RVA: 0x00013544 File Offset: 0x00011744
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x0001354C File Offset: 0x0001174C
			internal Task <SignUpAsync>b__0(Task toAwait)
			{
				return this.<>4__this.SignUpAsync(toAwait, this.cancellationToken);
			}

			// Token: 0x04000158 RID: 344
			public ParseUser <>4__this;

			// Token: 0x04000159 RID: 345
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000C0 RID: 192
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x06000619 RID: 1561 RVA: 0x00013560 File Offset: 0x00011760
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x00013568 File Offset: 0x00011768
			internal ParseObject <FetchAsyncInternal>b__1(Task _)
			{
				return this.t.Result;
			}

			// Token: 0x0400015A RID: 346
			public Task<ParseObject> t;
		}

		// Token: 0x020000C1 RID: 193
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x0600061B RID: 1563 RVA: 0x00013575 File Offset: 0x00011775
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x0001357D File Offset: 0x0001177D
			internal Task <UpgradeToRevocableSessionAsync>b__0(Task toAwait)
			{
				return this.<>4__this.UpgradeToRevocableSessionAsync(toAwait, this.cancellationToken);
			}

			// Token: 0x0400015B RID: 347
			public ParseUser <>4__this;

			// Token: 0x0400015C RID: 348
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000C2 RID: 194
		[CompilerGenerated]
		private sealed class <>c__DisplayClass26_0
		{
			// Token: 0x0600061D RID: 1565 RVA: 0x00013591 File Offset: 0x00011791
			public <>c__DisplayClass26_0()
			{
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x00013599 File Offset: 0x00011799
			internal Task<string> <UpgradeToRevocableSessionAsync>b__0(Task _)
			{
				return this.<>4__this.Services.UpgradeToRevocableSessionAsync(this.sessionToken, this.cancellationToken);
			}

			// Token: 0x0600061F RID: 1567 RVA: 0x000135B7 File Offset: 0x000117B7
			internal Task <UpgradeToRevocableSessionAsync>b__1(Task<string> task)
			{
				return this.<>4__this.SetSessionTokenAsync(task.Result);
			}

			// Token: 0x0400015D RID: 349
			public ParseUser <>4__this;

			// Token: 0x0400015E RID: 350
			public string sessionToken;

			// Token: 0x0400015F RID: 351
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000C3 RID: 195
		[CompilerGenerated]
		private sealed class <>c__DisplayClass40_0
		{
			// Token: 0x06000620 RID: 1568 RVA: 0x000135CA File Offset: 0x000117CA
			public <>c__DisplayClass40_0()
			{
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x000135D4 File Offset: 0x000117D4
			internal Task <LinkWithAsync>b__0(Task toAwait)
			{
				IDictionary<string, IDictionary<string, object>> dictionary = this.<>4__this.AuthData;
				if (dictionary == null)
				{
					dictionary = (this.<>4__this.AuthData = new Dictionary<string, IDictionary<string, object>>());
				}
				dictionary[this.authType] = this.data;
				this.<>4__this.AuthData = dictionary;
				return this.<>4__this.SaveAsync(this.cancellationToken);
			}

			// Token: 0x04000160 RID: 352
			public ParseUser <>4__this;

			// Token: 0x04000161 RID: 353
			public string authType;

			// Token: 0x04000162 RID: 354
			public IDictionary<string, object> data;

			// Token: 0x04000163 RID: 355
			public CancellationToken cancellationToken;
		}

		// Token: 0x020000C4 RID: 196
		[CompilerGenerated]
		private sealed class <>c__DisplayClass41_0
		{
			// Token: 0x06000622 RID: 1570 RVA: 0x00013633 File Offset: 0x00011833
			public <>c__DisplayClass41_0()
			{
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x0001363B File Offset: 0x0001183B
			internal Task <LinkWithAsync>b__0(Task<IDictionary<string, object>> t)
			{
				return this.<>4__this.LinkWithAsync(this.authType, t.Result, this.cancellationToken);
			}

			// Token: 0x04000164 RID: 356
			public ParseUser <>4__this;

			// Token: 0x04000165 RID: 357
			public string authType;

			// Token: 0x04000166 RID: 358
			public CancellationToken cancellationToken;
		}
	}
}
