using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000AE RID: 174
	public struct Friend
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		public Friend(SteamId steamid)
		{
			this.Id = steamid;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00010DAC File Offset: 0x0000EFAC
		public override string ToString()
		{
			return this.Name + " (" + this.Id.ToString() + ")";
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		public bool IsMe
		{
			get
			{
				return this.Id == SteamClient.SteamId;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00010DFD File Offset: 0x0000EFFD
		public bool IsFriend
		{
			get
			{
				return this.Relationship == Relationship.Friend;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00010E08 File Offset: 0x0000F008
		public bool IsBlocked
		{
			get
			{
				return this.Relationship == Relationship.Blocked;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00010E14 File Offset: 0x0000F014
		public bool IsPlayingThisGame
		{
			get
			{
				Friend.FriendGameInfo? friendGameInfo;
				ulong? num = (this.GameInfo != null) ? new ulong?(friendGameInfo.GetValueOrDefault().GameID) : null;
				ulong num2 = (ulong)SteamClient.AppId;
				return num.GetValueOrDefault() == num2 & num != null;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00010E6C File Offset: 0x0000F06C
		public bool IsOnline
		{
			get
			{
				return this.State > FriendState.Offline;
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00010E78 File Offset: 0x0000F078
		public async Task RequestInfoAsync()
		{
			await SteamFriends.CacheUserInformationAsync(this.Id, true);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00010EC4 File Offset: 0x0000F0C4
		public bool IsAway
		{
			get
			{
				return this.State == FriendState.Away;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00010ECF File Offset: 0x0000F0CF
		public bool IsBusy
		{
			get
			{
				return this.State == FriendState.Busy;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00010EDA File Offset: 0x0000F0DA
		public bool IsSnoozing
		{
			get
			{
				return this.State == FriendState.Snooze;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00010EE5 File Offset: 0x0000F0E5
		public Relationship Relationship
		{
			get
			{
				return SteamFriends.Internal.GetFriendRelationship(this.Id);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00010EF7 File Offset: 0x0000F0F7
		public FriendState State
		{
			get
			{
				return SteamFriends.Internal.GetFriendPersonaState(this.Id);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00010F09 File Offset: 0x0000F109
		public string Name
		{
			get
			{
				return SteamFriends.Internal.GetFriendPersonaName(this.Id);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00010F1C File Offset: 0x0000F11C
		public IEnumerable<string> NameHistory
		{
			get
			{
				int num;
				for (int i = 0; i < 32; i = num + 1)
				{
					string j = SteamFriends.Internal.GetFriendPersonaNameHistory(this.Id, i);
					bool flag = string.IsNullOrEmpty(j);
					if (flag)
					{
						break;
					}
					yield return j;
					j = null;
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00010F40 File Offset: 0x0000F140
		public int SteamLevel
		{
			get
			{
				return SteamFriends.Internal.GetFriendSteamLevel(this.Id);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00010F54 File Offset: 0x0000F154
		public Friend.FriendGameInfo? GameInfo
		{
			get
			{
				FriendGameInfo_t i = default(FriendGameInfo_t);
				bool flag = !SteamFriends.Internal.GetFriendGamePlayed(this.Id, ref i);
				Friend.FriendGameInfo? result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = new Friend.FriendGameInfo?(Friend.FriendGameInfo.From(i));
				}
				return result;
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		public bool IsIn(SteamId group_or_room)
		{
			return SteamFriends.Internal.IsUserInSource(this.Id, group_or_room);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public async Task<Image?> GetSmallAvatarAsync()
		{
			return await SteamFriends.GetSmallAvatarAsync(this.Id);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00011010 File Offset: 0x0000F210
		public async Task<Image?> GetMediumAvatarAsync()
		{
			return await SteamFriends.GetMediumAvatarAsync(this.Id);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001105C File Offset: 0x0000F25C
		public async Task<Image?> GetLargeAvatarAsync()
		{
			return await SteamFriends.GetLargeAvatarAsync(this.Id);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000110A8 File Offset: 0x0000F2A8
		public string GetRichPresence(string key)
		{
			string friendRichPresence = SteamFriends.Internal.GetFriendRichPresence(this.Id, key);
			bool flag = string.IsNullOrEmpty(friendRichPresence);
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = friendRichPresence;
			}
			return result;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000110DC File Offset: 0x0000F2DC
		public bool InviteToGame(string Text)
		{
			return SteamFriends.Internal.InviteUserToGame(this.Id, Text);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00011100 File Offset: 0x0000F300
		public bool SendMessage(string message)
		{
			return SteamFriends.Internal.ReplyToFriendMessage(this.Id, message);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00011124 File Offset: 0x0000F324
		public async Task<bool> RequestUserStatsAsync()
		{
			UserStatsReceived_t? userStatsReceived_t = await SteamUserStats.Internal.RequestUserStats(this.Id);
			UserStatsReceived_t? result = userStatsReceived_t;
			userStatsReceived_t = null;
			return result != null && result.Value.Result == Result.OK;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00011170 File Offset: 0x0000F370
		public float GetStatFloat(string statName, float defult = 0f)
		{
			float num = defult;
			bool flag = !SteamUserStats.Internal.GetUserStat(this.Id, statName, ref num);
			float result;
			if (flag)
			{
				result = defult;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000111A4 File Offset: 0x0000F3A4
		public int GetStatInt(string statName, int defult = 0)
		{
			int num = defult;
			bool flag = !SteamUserStats.Internal.GetUserStat(this.Id, statName, ref num);
			int result;
			if (flag)
			{
				result = defult;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000111D8 File Offset: 0x0000F3D8
		public bool GetAchievement(string statName, bool defult = false)
		{
			bool flag = defult;
			bool flag2 = !SteamUserStats.Internal.GetUserAchievement(this.Id, statName, ref flag);
			bool result;
			if (flag2)
			{
				result = defult;
			}
			else
			{
				result = flag;
			}
			return result;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001120C File Offset: 0x0000F40C
		public DateTime GetAchievementUnlockTime(string statName)
		{
			bool flag = false;
			uint value = 0U;
			bool flag2 = !SteamUserStats.Internal.GetUserAchievementAndUnlockTime(this.Id, statName, ref flag, ref value) || !flag;
			DateTime result;
			if (flag2)
			{
				result = DateTime.MinValue;
			}
			else
			{
				result = Epoch.ToDateTime(value);
			}
			return result;
		}

		// Token: 0x04000754 RID: 1876
		public SteamId Id;

		// Token: 0x0200025A RID: 602
		public struct FriendGameInfo
		{
			// Token: 0x17000300 RID: 768
			// (get) Token: 0x060011BA RID: 4538 RVA: 0x000206A7 File Offset: 0x0001E8A7
			public uint IpAddressRaw
			{
				get
				{
					return this.GameIP;
				}
			}

			// Token: 0x17000301 RID: 769
			// (get) Token: 0x060011BB RID: 4539 RVA: 0x000206AF File Offset: 0x0001E8AF
			public IPAddress IpAddress
			{
				get
				{
					return Utility.Int32ToIp(this.GameIP);
				}
			}

			// Token: 0x17000302 RID: 770
			// (get) Token: 0x060011BC RID: 4540 RVA: 0x000206BC File Offset: 0x0001E8BC
			public Lobby? Lobby
			{
				get
				{
					bool flag = this.SteamIDLobby == 0UL;
					Lobby? result;
					if (flag)
					{
						result = null;
					}
					else
					{
						result = new Lobby?(new Lobby(this.SteamIDLobby));
					}
					return result;
				}
			}

			// Token: 0x060011BD RID: 4541 RVA: 0x00020700 File Offset: 0x0001E900
			internal static Friend.FriendGameInfo From(FriendGameInfo_t i)
			{
				return new Friend.FriendGameInfo
				{
					GameID = i.GameID,
					GameIP = i.GameIP,
					ConnectionPort = (int)i.GamePort,
					QueryPort = (int)i.QueryPort,
					SteamIDLobby = i.SteamIDLobby
				};
			}

			// Token: 0x04000DF7 RID: 3575
			internal ulong GameID;

			// Token: 0x04000DF8 RID: 3576
			internal uint GameIP;

			// Token: 0x04000DF9 RID: 3577
			internal ulong SteamIDLobby;

			// Token: 0x04000DFA RID: 3578
			public int ConnectionPort;

			// Token: 0x04000DFB RID: 3579
			public int QueryPort;
		}

		// Token: 0x0200025B RID: 603
		[CompilerGenerated]
		private sealed class <RequestInfoAsync>d__13 : IAsyncStateMachine
		{
			// Token: 0x060011BE RID: 4542 RVA: 0x00020761 File Offset: 0x0001E961
			public <RequestInfoAsync>d__13()
			{
			}

			// Token: 0x060011BF RID: 4543 RVA: 0x0002076C File Offset: 0x0001E96C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = SteamFriends.CacheUserInformationAsync(this.Id, true).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							Friend.<RequestInfoAsync>d__13 <RequestInfoAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Friend.<RequestInfoAsync>d__13>(ref taskAwaiter, ref <RequestInfoAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter);
						num2 = -1;
					}
					taskAwaiter.GetResult();
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060011C0 RID: 4544 RVA: 0x00020830 File Offset: 0x0001EA30
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DFC RID: 3580
			public int <>1__state;

			// Token: 0x04000DFD RID: 3581
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000DFE RID: 3582
			public Friend <>4__this;

			// Token: 0x04000DFF RID: 3583
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200025C RID: 604
		[CompilerGenerated]
		private sealed class <get_NameHistory>d__27 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x060011C1 RID: 4545 RVA: 0x00020832 File Offset: 0x0001EA32
			[DebuggerHidden]
			public <get_NameHistory>d__27(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060011C2 RID: 4546 RVA: 0x0002084D File Offset: 0x0001EA4D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060011C3 RID: 4547 RVA: 0x00020850 File Offset: 0x0001EA50
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					j = null;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i < 32)
				{
					j = SteamFriends.Internal.GetFriendPersonaNameHistory(this.Id, i);
					bool flag = string.IsNullOrEmpty(j);
					if (!flag)
					{
						this.<>2__current = j;
						this.<>1__state = 1;
						return true;
					}
				}
				return false;
			}

			// Token: 0x17000303 RID: 771
			// (get) Token: 0x060011C4 RID: 4548 RVA: 0x000208FC File Offset: 0x0001EAFC
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060011C5 RID: 4549 RVA: 0x00020904 File Offset: 0x0001EB04
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000304 RID: 772
			// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0002090B File Offset: 0x0001EB0B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060011C7 RID: 4551 RVA: 0x00020914 File Offset: 0x0001EB14
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				Friend.<get_NameHistory>d__27 <get_NameHistory>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_NameHistory>d__ = this;
				}
				else
				{
					<get_NameHistory>d__ = new Friend.<get_NameHistory>d__27(0);
				}
				<get_NameHistory>d__.<>4__this = ref this;
				return <get_NameHistory>d__;
			}

			// Token: 0x060011C8 RID: 4552 RVA: 0x00020957 File Offset: 0x0001EB57
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x04000E00 RID: 3584
			private int <>1__state;

			// Token: 0x04000E01 RID: 3585
			private string <>2__current;

			// Token: 0x04000E02 RID: 3586
			private int <>l__initialThreadId;

			// Token: 0x04000E03 RID: 3587
			public Friend <>4__this;

			// Token: 0x04000E04 RID: 3588
			public Friend <>3__<>4__this;

			// Token: 0x04000E05 RID: 3589
			private int <i>5__1;

			// Token: 0x04000E06 RID: 3590
			private string <n>5__2;
		}

		// Token: 0x0200025D RID: 605
		[CompilerGenerated]
		private sealed class <GetSmallAvatarAsync>d__34 : IAsyncStateMachine
		{
			// Token: 0x060011C9 RID: 4553 RVA: 0x0002095F File Offset: 0x0001EB5F
			public <GetSmallAvatarAsync>d__34()
			{
			}

			// Token: 0x060011CA RID: 4554 RVA: 0x00020968 File Offset: 0x0001EB68
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Image? result2;
				try
				{
					TaskAwaiter<Image?> taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = SteamFriends.GetSmallAvatarAsync(this.Id).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<Image?> taskAwaiter2 = taskAwaiter;
							Friend.<GetSmallAvatarAsync>d__34 <GetSmallAvatarAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<Image?>, Friend.<GetSmallAvatarAsync>d__34>(ref taskAwaiter, ref <GetSmallAvatarAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<Image?> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<Image?>);
						num2 = -1;
					}
					Image? result = taskAwaiter.GetResult();
					result2 = result;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060011CB RID: 4555 RVA: 0x00020A38 File Offset: 0x0001EC38
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E07 RID: 3591
			public int <>1__state;

			// Token: 0x04000E08 RID: 3592
			public AsyncTaskMethodBuilder<Image?> <>t__builder;

			// Token: 0x04000E09 RID: 3593
			public Friend <>4__this;

			// Token: 0x04000E0A RID: 3594
			private Image? <>s__1;

			// Token: 0x04000E0B RID: 3595
			private TaskAwaiter<Image?> <>u__1;
		}

		// Token: 0x0200025E RID: 606
		[CompilerGenerated]
		private sealed class <GetMediumAvatarAsync>d__35 : IAsyncStateMachine
		{
			// Token: 0x060011CC RID: 4556 RVA: 0x00020A3A File Offset: 0x0001EC3A
			public <GetMediumAvatarAsync>d__35()
			{
			}

			// Token: 0x060011CD RID: 4557 RVA: 0x00020A44 File Offset: 0x0001EC44
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Image? result2;
				try
				{
					TaskAwaiter<Image?> taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = SteamFriends.GetMediumAvatarAsync(this.Id).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<Image?> taskAwaiter2 = taskAwaiter;
							Friend.<GetMediumAvatarAsync>d__35 <GetMediumAvatarAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<Image?>, Friend.<GetMediumAvatarAsync>d__35>(ref taskAwaiter, ref <GetMediumAvatarAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<Image?> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<Image?>);
						num2 = -1;
					}
					Image? result = taskAwaiter.GetResult();
					result2 = result;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060011CE RID: 4558 RVA: 0x00020B14 File Offset: 0x0001ED14
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E0C RID: 3596
			public int <>1__state;

			// Token: 0x04000E0D RID: 3597
			public AsyncTaskMethodBuilder<Image?> <>t__builder;

			// Token: 0x04000E0E RID: 3598
			public Friend <>4__this;

			// Token: 0x04000E0F RID: 3599
			private Image? <>s__1;

			// Token: 0x04000E10 RID: 3600
			private TaskAwaiter<Image?> <>u__1;
		}

		// Token: 0x0200025F RID: 607
		[CompilerGenerated]
		private sealed class <GetLargeAvatarAsync>d__36 : IAsyncStateMachine
		{
			// Token: 0x060011CF RID: 4559 RVA: 0x00020B16 File Offset: 0x0001ED16
			public <GetLargeAvatarAsync>d__36()
			{
			}

			// Token: 0x060011D0 RID: 4560 RVA: 0x00020B20 File Offset: 0x0001ED20
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Image? result2;
				try
				{
					TaskAwaiter<Image?> taskAwaiter;
					if (num != 0)
					{
						taskAwaiter = SteamFriends.GetLargeAvatarAsync(this.Id).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter<Image?> taskAwaiter2 = taskAwaiter;
							Friend.<GetLargeAvatarAsync>d__36 <GetLargeAvatarAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<Image?>, Friend.<GetLargeAvatarAsync>d__36>(ref taskAwaiter, ref <GetLargeAvatarAsync>d__);
							return;
						}
					}
					else
					{
						TaskAwaiter<Image?> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<Image?>);
						num2 = -1;
					}
					Image? result = taskAwaiter.GetResult();
					result2 = result;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060011D1 RID: 4561 RVA: 0x00020BF0 File Offset: 0x0001EDF0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E11 RID: 3601
			public int <>1__state;

			// Token: 0x04000E12 RID: 3602
			public AsyncTaskMethodBuilder<Image?> <>t__builder;

			// Token: 0x04000E13 RID: 3603
			public Friend <>4__this;

			// Token: 0x04000E14 RID: 3604
			private Image? <>s__1;

			// Token: 0x04000E15 RID: 3605
			private TaskAwaiter<Image?> <>u__1;
		}

		// Token: 0x02000260 RID: 608
		[CompilerGenerated]
		private sealed class <RequestUserStatsAsync>d__40 : IAsyncStateMachine
		{
			// Token: 0x060011D2 RID: 4562 RVA: 0x00020BF2 File Offset: 0x0001EDF2
			public <RequestUserStatsAsync>d__40()
			{
			}

			// Token: 0x060011D3 RID: 4563 RVA: 0x00020BFC File Offset: 0x0001EDFC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result2;
				try
				{
					CallResult<UserStatsReceived_t> callResult;
					if (num != 0)
					{
						callResult = SteamUserStats.Internal.RequestUserStats(this.Id).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<UserStatsReceived_t> callResult2 = callResult;
							Friend.<RequestUserStatsAsync>d__40 <RequestUserStatsAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<UserStatsReceived_t>, Friend.<RequestUserStatsAsync>d__40>(ref callResult, ref <RequestUserStatsAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<UserStatsReceived_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<UserStatsReceived_t>);
						num2 = -1;
					}
					userStatsReceived_t = callResult.GetResult();
					result = userStatsReceived_t;
					userStatsReceived_t = null;
					result2 = (result != null && result.Value.Result == Result.OK);
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060011D4 RID: 4564 RVA: 0x00020D10 File Offset: 0x0001EF10
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000E16 RID: 3606
			public int <>1__state;

			// Token: 0x04000E17 RID: 3607
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000E18 RID: 3608
			public Friend <>4__this;

			// Token: 0x04000E19 RID: 3609
			private UserStatsReceived_t? <result>5__1;

			// Token: 0x04000E1A RID: 3610
			private UserStatsReceived_t? <>s__2;

			// Token: 0x04000E1B RID: 3611
			private CallResult<UserStatsReceived_t> <>u__1;
		}
	}
}
