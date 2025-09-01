using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000A6 RID: 166
	public class SteamUserStats : SteamClientClass<SteamUserStats>
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		internal static ISteamUserStats Internal
		{
			get
			{
				return SteamClientClass<SteamUserStats>.Interface as ISteamUserStats;
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0000FEBC File Offset: 0x0000E0BC
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamUserStats(server));
			SteamUserStats.InstallEvents();
			SteamUserStats.RequestCurrentStats();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0000FED9 File Offset: 0x0000E0D9
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		public static bool StatsRecieved
		{
			[CompilerGenerated]
			get
			{
				return SteamUserStats.<StatsRecieved>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				SteamUserStats.<StatsRecieved>k__BackingField = value;
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		internal static void InstallEvents()
		{
			Dispatch.Install<UserStatsReceived_t>(delegate(UserStatsReceived_t x)
			{
				bool flag = x.SteamIDUser == SteamClient.SteamId;
				if (flag)
				{
					SteamUserStats.StatsRecieved = true;
				}
				Action<SteamId, Result> onUserStatsReceived = SteamUserStats.OnUserStatsReceived;
				if (onUserStatsReceived != null)
				{
					onUserStatsReceived(x.SteamIDUser, x.Result);
				}
			}, false);
			Dispatch.Install<UserStatsStored_t>(delegate(UserStatsStored_t x)
			{
				Action<Result> onUserStatsStored = SteamUserStats.OnUserStatsStored;
				if (onUserStatsStored != null)
				{
					onUserStatsStored(x.Result);
				}
			}, false);
			Dispatch.Install<UserAchievementStored_t>(delegate(UserAchievementStored_t x)
			{
				Action<Achievement, int, int> onAchievementProgress = SteamUserStats.OnAchievementProgress;
				if (onAchievementProgress != null)
				{
					onAchievementProgress(new Achievement(x.AchievementNameUTF8()), (int)x.CurProgress, (int)x.MaxProgress);
				}
			}, false);
			Dispatch.Install<UserStatsUnloaded_t>(delegate(UserStatsUnloaded_t x)
			{
				Action<SteamId> onUserStatsUnloaded = SteamUserStats.OnUserStatsUnloaded;
				if (onUserStatsUnloaded != null)
				{
					onUserStatsUnloaded(x.SteamIDUser);
				}
			}, false);
			Dispatch.Install<UserAchievementIconFetched_t>(delegate(UserAchievementIconFetched_t x)
			{
				Action<string, int> onAchievementIconFetched = SteamUserStats.OnAchievementIconFetched;
				if (onAchievementIconFetched != null)
				{
					onAchievementIconFetched(x.AchievementNameUTF8(), x.IconHandle);
				}
			}, false);
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000907 RID: 2311 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		// (remove) Token: 0x06000908 RID: 2312 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		internal static event Action<string, int> OnAchievementIconFetched
		{
			[CompilerGenerated]
			add
			{
				Action<string, int> action = SteamUserStats.OnAchievementIconFetched;
				Action<string, int> action2;
				do
				{
					action2 = action;
					Action<string, int> value2 = (Action<string, int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string, int>>(ref SteamUserStats.OnAchievementIconFetched, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string, int> action = SteamUserStats.OnAchievementIconFetched;
				Action<string, int> action2;
				do
				{
					action2 = action;
					Action<string, int> value2 = (Action<string, int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string, int>>(ref SteamUserStats.OnAchievementIconFetched, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000909 RID: 2313 RVA: 0x0001001C File Offset: 0x0000E21C
		// (remove) Token: 0x0600090A RID: 2314 RVA: 0x00010050 File Offset: 0x0000E250
		public static event Action<SteamId, Result> OnUserStatsReceived
		{
			[CompilerGenerated]
			add
			{
				Action<SteamId, Result> action = SteamUserStats.OnUserStatsReceived;
				Action<SteamId, Result> action2;
				do
				{
					action2 = action;
					Action<SteamId, Result> value2 = (Action<SteamId, Result>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<SteamId, Result>>(ref SteamUserStats.OnUserStatsReceived, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<SteamId, Result> action = SteamUserStats.OnUserStatsReceived;
				Action<SteamId, Result> action2;
				do
				{
					action2 = action;
					Action<SteamId, Result> value2 = (Action<SteamId, Result>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<SteamId, Result>>(ref SteamUserStats.OnUserStatsReceived, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600090B RID: 2315 RVA: 0x00010084 File Offset: 0x0000E284
		// (remove) Token: 0x0600090C RID: 2316 RVA: 0x000100B8 File Offset: 0x0000E2B8
		public static event Action<Result> OnUserStatsStored
		{
			[CompilerGenerated]
			add
			{
				Action<Result> action = SteamUserStats.OnUserStatsStored;
				Action<Result> action2;
				do
				{
					action2 = action;
					Action<Result> value2 = (Action<Result>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Result>>(ref SteamUserStats.OnUserStatsStored, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Result> action = SteamUserStats.OnUserStatsStored;
				Action<Result> action2;
				do
				{
					action2 = action;
					Action<Result> value2 = (Action<Result>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Result>>(ref SteamUserStats.OnUserStatsStored, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600090D RID: 2317 RVA: 0x000100EC File Offset: 0x0000E2EC
		// (remove) Token: 0x0600090E RID: 2318 RVA: 0x00010120 File Offset: 0x0000E320
		public static event Action<Achievement, int, int> OnAchievementProgress
		{
			[CompilerGenerated]
			add
			{
				Action<Achievement, int, int> action = SteamUserStats.OnAchievementProgress;
				Action<Achievement, int, int> action2;
				do
				{
					action2 = action;
					Action<Achievement, int, int> value2 = (Action<Achievement, int, int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Achievement, int, int>>(ref SteamUserStats.OnAchievementProgress, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Achievement, int, int> action = SteamUserStats.OnAchievementProgress;
				Action<Achievement, int, int> action2;
				do
				{
					action2 = action;
					Action<Achievement, int, int> value2 = (Action<Achievement, int, int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Achievement, int, int>>(ref SteamUserStats.OnAchievementProgress, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600090F RID: 2319 RVA: 0x00010154 File Offset: 0x0000E354
		// (remove) Token: 0x06000910 RID: 2320 RVA: 0x00010188 File Offset: 0x0000E388
		public static event Action<SteamId> OnUserStatsUnloaded
		{
			[CompilerGenerated]
			add
			{
				Action<SteamId> action = SteamUserStats.OnUserStatsUnloaded;
				Action<SteamId> action2;
				do
				{
					action2 = action;
					Action<SteamId> value2 = (Action<SteamId>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<SteamId>>(ref SteamUserStats.OnUserStatsUnloaded, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<SteamId> action = SteamUserStats.OnUserStatsUnloaded;
				Action<SteamId> action2;
				do
				{
					action2 = action;
					Action<SteamId> value2 = (Action<SteamId>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<SteamId>>(ref SteamUserStats.OnUserStatsUnloaded, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x000101BC File Offset: 0x0000E3BC
		public static IEnumerable<Achievement> Achievements
		{
			get
			{
				int i = 0;
				while ((long)i < (long)((ulong)SteamUserStats.Internal.GetNumAchievements()))
				{
					yield return new Achievement(SteamUserStats.Internal.GetAchievementName((uint)i));
					int num = i;
					i = num + 1;
				}
				yield break;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000101D4 File Offset: 0x0000E3D4
		public static bool IndicateAchievementProgress(string achName, int curProg, int maxProg)
		{
			bool flag = string.IsNullOrEmpty(achName);
			if (flag)
			{
				throw new ArgumentNullException("Achievement string is null or empty");
			}
			bool flag2 = curProg >= maxProg;
			if (flag2)
			{
				throw new ArgumentException(string.Format(" Current progress [{0}] arguement toward achievement greater than or equal to max [{1}]", curProg, maxProg));
			}
			return SteamUserStats.Internal.IndicateAchievementProgress(achName, (uint)curProg, (uint)maxProg);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00010230 File Offset: 0x0000E430
		public static async Task<int> PlayerCountAsync()
		{
			NumberOfCurrentPlayers_t? numberOfCurrentPlayers_t = await SteamUserStats.Internal.GetNumberOfCurrentPlayers();
			NumberOfCurrentPlayers_t? result = numberOfCurrentPlayers_t;
			numberOfCurrentPlayers_t = null;
			int result2;
			if (result == null || result.Value.Success == 0)
			{
				result2 = -1;
			}
			else
			{
				result2 = result.Value.CPlayers;
			}
			return result2;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00010270 File Offset: 0x0000E470
		public static bool StoreStats()
		{
			return SteamUserStats.Internal.StoreStats();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001028C File Offset: 0x0000E48C
		public static bool RequestCurrentStats()
		{
			return SteamUserStats.Internal.RequestCurrentStats();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000102A8 File Offset: 0x0000E4A8
		public static async Task<Result> RequestGlobalStatsAsync(int days)
		{
			GlobalStatsReceived_t? globalStatsReceived_t = await SteamUserStats.Internal.RequestGlobalStats(days);
			GlobalStatsReceived_t? result = globalStatsReceived_t;
			globalStatsReceived_t = null;
			Result result2;
			if (result == null)
			{
				result2 = Result.Fail;
			}
			else
			{
				result2 = result.Value.Result;
			}
			return result2;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000102F0 File Offset: 0x0000E4F0
		public static async Task<Leaderboard?> FindOrCreateLeaderboardAsync(string name, LeaderboardSort sort, LeaderboardDisplay display)
		{
			LeaderboardFindResult_t? leaderboardFindResult_t = await SteamUserStats.Internal.FindOrCreateLeaderboard(name, sort, display);
			LeaderboardFindResult_t? result = leaderboardFindResult_t;
			leaderboardFindResult_t = null;
			Leaderboard? result2;
			if (result == null || result.Value.LeaderboardFound == 0)
			{
				result2 = null;
			}
			else
			{
				result2 = new Leaderboard?(new Leaderboard
				{
					Id = result.Value.SteamLeaderboard
				});
			}
			return result2;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00010348 File Offset: 0x0000E548
		public static async Task<Leaderboard?> FindLeaderboardAsync(string name)
		{
			LeaderboardFindResult_t? leaderboardFindResult_t = await SteamUserStats.Internal.FindLeaderboard(name);
			LeaderboardFindResult_t? result = leaderboardFindResult_t;
			leaderboardFindResult_t = null;
			Leaderboard? result2;
			if (result == null || result.Value.LeaderboardFound == 0)
			{
				result2 = null;
			}
			else
			{
				result2 = new Leaderboard?(new Leaderboard
				{
					Id = result.Value.SteamLeaderboard
				});
			}
			return result2;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00010390 File Offset: 0x0000E590
		public static bool AddStat(string name, int amount = 1)
		{
			int num = SteamUserStats.GetStatInt(name);
			num += amount;
			return SteamUserStats.SetStat(name, num);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public static bool AddStat(string name, float amount = 1f)
		{
			float num = SteamUserStats.GetStatFloat(name);
			num += amount;
			return SteamUserStats.SetStat(name, num);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000103D8 File Offset: 0x0000E5D8
		public static bool SetStat(string name, int value)
		{
			return SteamUserStats.Internal.SetStat(name, value);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000103F8 File Offset: 0x0000E5F8
		public static bool SetStat(string name, float value)
		{
			return SteamUserStats.Internal.SetStat(name, value);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00010418 File Offset: 0x0000E618
		public static int GetStatInt(string name)
		{
			int result = 0;
			SteamUserStats.Internal.GetStat(name, ref result);
			return result;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001043C File Offset: 0x0000E63C
		public static float GetStatFloat(string name)
		{
			float result = 0f;
			SteamUserStats.Internal.GetStat(name, ref result);
			return result;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00010464 File Offset: 0x0000E664
		public static bool ResetAll(bool includeAchievements)
		{
			return SteamUserStats.Internal.ResetAllStats(includeAchievements);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00010481 File Offset: 0x0000E681
		public SteamUserStats()
		{
		}

		// Token: 0x04000735 RID: 1845
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static bool <StatsRecieved>k__BackingField;

		// Token: 0x04000736 RID: 1846
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<string, int> OnAchievementIconFetched;

		// Token: 0x04000737 RID: 1847
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<SteamId, Result> OnUserStatsReceived;

		// Token: 0x04000738 RID: 1848
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Result> OnUserStatsStored;

		// Token: 0x04000739 RID: 1849
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Achievement, int, int> OnAchievementProgress;

		// Token: 0x0400073A RID: 1850
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<SteamId> OnUserStatsUnloaded;

		// Token: 0x02000251 RID: 593
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001192 RID: 4498 RVA: 0x0001FDE6 File Offset: 0x0001DFE6
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001193 RID: 4499 RVA: 0x0001FDF2 File Offset: 0x0001DFF2
			public <>c()
			{
			}

			// Token: 0x06001194 RID: 4500 RVA: 0x0001FDFC File Offset: 0x0001DFFC
			internal void <InstallEvents>b__7_0(UserStatsReceived_t x)
			{
				bool flag = x.SteamIDUser == SteamClient.SteamId;
				if (flag)
				{
					SteamUserStats.StatsRecieved = true;
				}
				Action<SteamId, Result> onUserStatsReceived = SteamUserStats.OnUserStatsReceived;
				if (onUserStatsReceived != null)
				{
					onUserStatsReceived(x.SteamIDUser, x.Result);
				}
			}

			// Token: 0x06001195 RID: 4501 RVA: 0x0001FE49 File Offset: 0x0001E049
			internal void <InstallEvents>b__7_1(UserStatsStored_t x)
			{
				Action<Result> onUserStatsStored = SteamUserStats.OnUserStatsStored;
				if (onUserStatsStored != null)
				{
					onUserStatsStored(x.Result);
				}
			}

			// Token: 0x06001196 RID: 4502 RVA: 0x0001FE62 File Offset: 0x0001E062
			internal void <InstallEvents>b__7_2(UserAchievementStored_t x)
			{
				Action<Achievement, int, int> onAchievementProgress = SteamUserStats.OnAchievementProgress;
				if (onAchievementProgress != null)
				{
					onAchievementProgress(new Achievement(x.AchievementNameUTF8()), (int)x.CurProgress, (int)x.MaxProgress);
				}
			}

			// Token: 0x06001197 RID: 4503 RVA: 0x0001FE8D File Offset: 0x0001E08D
			internal void <InstallEvents>b__7_3(UserStatsUnloaded_t x)
			{
				Action<SteamId> onUserStatsUnloaded = SteamUserStats.OnUserStatsUnloaded;
				if (onUserStatsUnloaded != null)
				{
					onUserStatsUnloaded(x.SteamIDUser);
				}
			}

			// Token: 0x06001198 RID: 4504 RVA: 0x0001FEAB File Offset: 0x0001E0AB
			internal void <InstallEvents>b__7_4(UserAchievementIconFetched_t x)
			{
				Action<string, int> onAchievementIconFetched = SteamUserStats.OnAchievementIconFetched;
				if (onAchievementIconFetched != null)
				{
					onAchievementIconFetched(x.AchievementNameUTF8(), x.IconHandle);
				}
			}

			// Token: 0x04000DC6 RID: 3526
			public static readonly SteamUserStats.<>c <>9 = new SteamUserStats.<>c();

			// Token: 0x04000DC7 RID: 3527
			public static Action<UserStatsReceived_t> <>9__7_0;

			// Token: 0x04000DC8 RID: 3528
			public static Action<UserStatsStored_t> <>9__7_1;

			// Token: 0x04000DC9 RID: 3529
			public static Action<UserAchievementStored_t> <>9__7_2;

			// Token: 0x04000DCA RID: 3530
			public static Action<UserStatsUnloaded_t> <>9__7_3;

			// Token: 0x04000DCB RID: 3531
			public static Action<UserAchievementIconFetched_t> <>9__7_4;
		}

		// Token: 0x02000252 RID: 594
		[CompilerGenerated]
		private sealed class <get_Achievements>d__24 : IEnumerable<Achievement>, IEnumerable, IEnumerator<Achievement>, IDisposable, IEnumerator
		{
			// Token: 0x06001199 RID: 4505 RVA: 0x0001FECB File Offset: 0x0001E0CB
			[DebuggerHidden]
			public <get_Achievements>d__24(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600119A RID: 4506 RVA: 0x0001FEE6 File Offset: 0x0001E0E6
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600119B RID: 4507 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
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
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if ((long)i >= (long)((ulong)SteamUserStats.Internal.GetNumAchievements()))
				{
					return false;
				}
				this.<>2__current = new Achievement(SteamUserStats.Internal.GetAchievementName((uint)i));
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002FE RID: 766
			// (get) Token: 0x0600119C RID: 4508 RVA: 0x0001FF74 File Offset: 0x0001E174
			Achievement IEnumerator<Achievement>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600119D RID: 4509 RVA: 0x0001FF7C File Offset: 0x0001E17C
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002FF RID: 767
			// (get) Token: 0x0600119E RID: 4510 RVA: 0x0001FF83 File Offset: 0x0001E183
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600119F RID: 4511 RVA: 0x0001FF90 File Offset: 0x0001E190
			[DebuggerHidden]
			IEnumerator<Achievement> IEnumerable<Achievement>.GetEnumerator()
			{
				SteamUserStats.<get_Achievements>d__24 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SteamUserStats.<get_Achievements>d__24(0);
				}
				return result;
			}

			// Token: 0x060011A0 RID: 4512 RVA: 0x0001FFC7 File Offset: 0x0001E1C7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Data.Achievement>.GetEnumerator();
			}

			// Token: 0x04000DCC RID: 3532
			private int <>1__state;

			// Token: 0x04000DCD RID: 3533
			private Achievement <>2__current;

			// Token: 0x04000DCE RID: 3534
			private int <>l__initialThreadId;

			// Token: 0x04000DCF RID: 3535
			private int <i>5__1;
		}

		// Token: 0x02000253 RID: 595
		[CompilerGenerated]
		private sealed class <PlayerCountAsync>d__26 : IAsyncStateMachine
		{
			// Token: 0x060011A1 RID: 4513 RVA: 0x0001FFCF File Offset: 0x0001E1CF
			public <PlayerCountAsync>d__26()
			{
			}

			// Token: 0x060011A2 RID: 4514 RVA: 0x0001FFD8 File Offset: 0x0001E1D8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				int result2;
				try
				{
					CallResult<NumberOfCurrentPlayers_t> callResult;
					if (num != 0)
					{
						callResult = SteamUserStats.Internal.GetNumberOfCurrentPlayers().GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<NumberOfCurrentPlayers_t> callResult2 = callResult;
							SteamUserStats.<PlayerCountAsync>d__26 <PlayerCountAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<NumberOfCurrentPlayers_t>, SteamUserStats.<PlayerCountAsync>d__26>(ref callResult, ref <PlayerCountAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<NumberOfCurrentPlayers_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<NumberOfCurrentPlayers_t>);
						num2 = -1;
					}
					numberOfCurrentPlayers_t = callResult.GetResult();
					result = numberOfCurrentPlayers_t;
					numberOfCurrentPlayers_t = null;
					bool flag = result == null || result.Value.Success == 0;
					if (flag)
					{
						result2 = -1;
					}
					else
					{
						result2 = result.Value.CPlayers;
					}
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

			// Token: 0x060011A3 RID: 4515 RVA: 0x000200F8 File Offset: 0x0001E2F8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DD0 RID: 3536
			public int <>1__state;

			// Token: 0x04000DD1 RID: 3537
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000DD2 RID: 3538
			private NumberOfCurrentPlayers_t? <result>5__1;

			// Token: 0x04000DD3 RID: 3539
			private NumberOfCurrentPlayers_t? <>s__2;

			// Token: 0x04000DD4 RID: 3540
			private CallResult<NumberOfCurrentPlayers_t> <>u__1;
		}

		// Token: 0x02000254 RID: 596
		[CompilerGenerated]
		private sealed class <RequestGlobalStatsAsync>d__29 : IAsyncStateMachine
		{
			// Token: 0x060011A4 RID: 4516 RVA: 0x000200FA File Offset: 0x0001E2FA
			public <RequestGlobalStatsAsync>d__29()
			{
			}

			// Token: 0x060011A5 RID: 4517 RVA: 0x00020104 File Offset: 0x0001E304
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Result result2;
				try
				{
					CallResult<GlobalStatsReceived_t> callResult;
					if (num != 0)
					{
						callResult = SteamUserStats.Internal.RequestGlobalStats(days).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<GlobalStatsReceived_t> callResult2 = callResult;
							SteamUserStats.<RequestGlobalStatsAsync>d__29 <RequestGlobalStatsAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<GlobalStatsReceived_t>, SteamUserStats.<RequestGlobalStatsAsync>d__29>(ref callResult, ref <RequestGlobalStatsAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<GlobalStatsReceived_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<GlobalStatsReceived_t>);
						num2 = -1;
					}
					globalStatsReceived_t = callResult.GetResult();
					result = globalStatsReceived_t;
					globalStatsReceived_t = null;
					bool flag = result == null;
					if (flag)
					{
						result2 = Result.Fail;
					}
					else
					{
						result2 = result.Value.Result;
					}
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

			// Token: 0x060011A6 RID: 4518 RVA: 0x00020218 File Offset: 0x0001E418
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DD5 RID: 3541
			public int <>1__state;

			// Token: 0x04000DD6 RID: 3542
			public AsyncTaskMethodBuilder<Result> <>t__builder;

			// Token: 0x04000DD7 RID: 3543
			public int days;

			// Token: 0x04000DD8 RID: 3544
			private GlobalStatsReceived_t? <result>5__1;

			// Token: 0x04000DD9 RID: 3545
			private GlobalStatsReceived_t? <>s__2;

			// Token: 0x04000DDA RID: 3546
			private CallResult<GlobalStatsReceived_t> <>u__1;
		}

		// Token: 0x02000255 RID: 597
		[CompilerGenerated]
		private sealed class <FindOrCreateLeaderboardAsync>d__30 : IAsyncStateMachine
		{
			// Token: 0x060011A7 RID: 4519 RVA: 0x0002021A File Offset: 0x0001E41A
			public <FindOrCreateLeaderboardAsync>d__30()
			{
			}

			// Token: 0x060011A8 RID: 4520 RVA: 0x00020224 File Offset: 0x0001E424
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Leaderboard? result2;
				try
				{
					CallResult<LeaderboardFindResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUserStats.Internal.FindOrCreateLeaderboard(name, sort, display).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LeaderboardFindResult_t> callResult2 = callResult;
							SteamUserStats.<FindOrCreateLeaderboardAsync>d__30 <FindOrCreateLeaderboardAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LeaderboardFindResult_t>, SteamUserStats.<FindOrCreateLeaderboardAsync>d__30>(ref callResult, ref <FindOrCreateLeaderboardAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<LeaderboardFindResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LeaderboardFindResult_t>);
						num2 = -1;
					}
					leaderboardFindResult_t = callResult.GetResult();
					result = leaderboardFindResult_t;
					leaderboardFindResult_t = null;
					bool flag = result == null || result.Value.LeaderboardFound == 0;
					if (flag)
					{
						result2 = null;
					}
					else
					{
						result2 = new Leaderboard?(new Leaderboard
						{
							Id = result.Value.SteamLeaderboard
						});
					}
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

			// Token: 0x060011A9 RID: 4521 RVA: 0x00020384 File Offset: 0x0001E584
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DDB RID: 3547
			public int <>1__state;

			// Token: 0x04000DDC RID: 3548
			public AsyncTaskMethodBuilder<Leaderboard?> <>t__builder;

			// Token: 0x04000DDD RID: 3549
			public string name;

			// Token: 0x04000DDE RID: 3550
			public LeaderboardSort sort;

			// Token: 0x04000DDF RID: 3551
			public LeaderboardDisplay display;

			// Token: 0x04000DE0 RID: 3552
			private LeaderboardFindResult_t? <result>5__1;

			// Token: 0x04000DE1 RID: 3553
			private LeaderboardFindResult_t? <>s__2;

			// Token: 0x04000DE2 RID: 3554
			private CallResult<LeaderboardFindResult_t> <>u__1;
		}

		// Token: 0x02000256 RID: 598
		[CompilerGenerated]
		private sealed class <FindLeaderboardAsync>d__31 : IAsyncStateMachine
		{
			// Token: 0x060011AA RID: 4522 RVA: 0x00020386 File Offset: 0x0001E586
			public <FindLeaderboardAsync>d__31()
			{
			}

			// Token: 0x060011AB RID: 4523 RVA: 0x00020390 File Offset: 0x0001E590
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Leaderboard? result2;
				try
				{
					CallResult<LeaderboardFindResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamUserStats.Internal.FindLeaderboard(name).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LeaderboardFindResult_t> callResult2 = callResult;
							SteamUserStats.<FindLeaderboardAsync>d__31 <FindLeaderboardAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LeaderboardFindResult_t>, SteamUserStats.<FindLeaderboardAsync>d__31>(ref callResult, ref <FindLeaderboardAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<LeaderboardFindResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LeaderboardFindResult_t>);
						num2 = -1;
					}
					leaderboardFindResult_t = callResult.GetResult();
					result = leaderboardFindResult_t;
					leaderboardFindResult_t = null;
					bool flag = result == null || result.Value.LeaderboardFound == 0;
					if (flag)
					{
						result2 = null;
					}
					else
					{
						result2 = new Leaderboard?(new Leaderboard
						{
							Id = result.Value.SteamLeaderboard
						});
					}
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

			// Token: 0x060011AC RID: 4524 RVA: 0x000204D8 File Offset: 0x0001E6D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DE3 RID: 3555
			public int <>1__state;

			// Token: 0x04000DE4 RID: 3556
			public AsyncTaskMethodBuilder<Leaderboard?> <>t__builder;

			// Token: 0x04000DE5 RID: 3557
			public string name;

			// Token: 0x04000DE6 RID: 3558
			private LeaderboardFindResult_t? <result>5__1;

			// Token: 0x04000DE7 RID: 3559
			private LeaderboardFindResult_t? <>s__2;

			// Token: 0x04000DE8 RID: 3560
			private CallResult<LeaderboardFindResult_t> <>u__1;
		}
	}
}
