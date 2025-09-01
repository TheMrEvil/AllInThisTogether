using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000A3 RID: 163
	public class SteamServerStats : SteamServerClass<SteamServerStats>
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0000EE2F File Offset: 0x0000D02F
		internal static ISteamGameServerStats Internal
		{
			get
			{
				return SteamServerClass<SteamServerStats>.Interface as ISteamGameServerStats;
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0000EE3B File Offset: 0x0000D03B
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamGameServerStats(server));
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public static async Task<Result> RequestUserStatsAsync(SteamId steamid)
		{
			GSStatsReceived_t? gsstatsReceived_t = await SteamServerStats.Internal.RequestUserStats(steamid);
			GSStatsReceived_t? r = gsstatsReceived_t;
			gsstatsReceived_t = null;
			Result result;
			if (r == null)
			{
				result = Result.Fail;
			}
			else
			{
				result = r.Value.Result;
			}
			return result;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0000EE94 File Offset: 0x0000D094
		public static bool SetInt(SteamId steamid, string name, int stat)
		{
			return SteamServerStats.Internal.SetUserStat(steamid, name, stat);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		public static bool SetFloat(SteamId steamid, string name, float stat)
		{
			return SteamServerStats.Internal.SetUserStat(steamid, name, stat);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public static int GetInt(SteamId steamid, string name, int defaultValue = 0)
		{
			int num = defaultValue;
			bool flag = !SteamServerStats.Internal.GetUserStat(steamid, name, ref num);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0000EF04 File Offset: 0x0000D104
		public static float GetFloat(SteamId steamid, string name, float defaultValue = 0f)
		{
			float num = defaultValue;
			bool flag = !SteamServerStats.Internal.GetUserStat(steamid, name, ref num);
			float result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0000EF34 File Offset: 0x0000D134
		public static bool SetAchievement(SteamId steamid, string name)
		{
			return SteamServerStats.Internal.SetUserAchievement(steamid, name);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0000EF54 File Offset: 0x0000D154
		public static bool ClearAchievement(SteamId steamid, string name)
		{
			return SteamServerStats.Internal.ClearUserAchievement(steamid, name);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0000EF74 File Offset: 0x0000D174
		public static bool GetAchievement(SteamId steamid, string name)
		{
			bool flag = false;
			bool flag2 = !SteamServerStats.Internal.GetUserAchievement(steamid, name, ref flag);
			return !flag2 && flag;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		public static async Task<Result> StoreUserStats(SteamId steamid)
		{
			GSStatsStored_t? gsstatsStored_t = await SteamServerStats.Internal.StoreUserStats(steamid);
			GSStatsStored_t? r = gsstatsStored_t;
			gsstatsStored_t = null;
			Result result;
			if (r == null)
			{
				result = Result.Fail;
			}
			else
			{
				result = r.Value.Result;
			}
			return result;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0000EFEB File Offset: 0x0000D1EB
		public SteamServerStats()
		{
		}

		// Token: 0x02000240 RID: 576
		[CompilerGenerated]
		private sealed class <RequestUserStatsAsync>d__3 : IAsyncStateMachine
		{
			// Token: 0x06001158 RID: 4440 RVA: 0x0001E82E File Offset: 0x0001CA2E
			public <RequestUserStatsAsync>d__3()
			{
			}

			// Token: 0x06001159 RID: 4441 RVA: 0x0001E838 File Offset: 0x0001CA38
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Result result;
				try
				{
					CallResult<GSStatsReceived_t> callResult;
					if (num != 0)
					{
						callResult = SteamServerStats.Internal.RequestUserStats(steamid).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<GSStatsReceived_t> callResult2 = callResult;
							SteamServerStats.<RequestUserStatsAsync>d__3 <RequestUserStatsAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<GSStatsReceived_t>, SteamServerStats.<RequestUserStatsAsync>d__3>(ref callResult, ref <RequestUserStatsAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<GSStatsReceived_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<GSStatsReceived_t>);
						num2 = -1;
					}
					gsstatsReceived_t = callResult.GetResult();
					r = gsstatsReceived_t;
					gsstatsReceived_t = null;
					bool flag = r == null;
					if (flag)
					{
						result = Result.Fail;
					}
					else
					{
						result = r.Value.Result;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600115A RID: 4442 RVA: 0x0001E94C File Offset: 0x0001CB4C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D5F RID: 3423
			public int <>1__state;

			// Token: 0x04000D60 RID: 3424
			public AsyncTaskMethodBuilder<Result> <>t__builder;

			// Token: 0x04000D61 RID: 3425
			public SteamId steamid;

			// Token: 0x04000D62 RID: 3426
			private GSStatsReceived_t? <r>5__1;

			// Token: 0x04000D63 RID: 3427
			private GSStatsReceived_t? <>s__2;

			// Token: 0x04000D64 RID: 3428
			private CallResult<GSStatsReceived_t> <>u__1;
		}

		// Token: 0x02000241 RID: 577
		[CompilerGenerated]
		private sealed class <StoreUserStats>d__11 : IAsyncStateMachine
		{
			// Token: 0x0600115B RID: 4443 RVA: 0x0001E94E File Offset: 0x0001CB4E
			public <StoreUserStats>d__11()
			{
			}

			// Token: 0x0600115C RID: 4444 RVA: 0x0001E958 File Offset: 0x0001CB58
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Result result;
				try
				{
					CallResult<GSStatsStored_t> callResult;
					if (num != 0)
					{
						callResult = SteamServerStats.Internal.StoreUserStats(steamid).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<GSStatsStored_t> callResult2 = callResult;
							SteamServerStats.<StoreUserStats>d__11 <StoreUserStats>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<GSStatsStored_t>, SteamServerStats.<StoreUserStats>d__11>(ref callResult, ref <StoreUserStats>d__);
							return;
						}
					}
					else
					{
						CallResult<GSStatsStored_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<GSStatsStored_t>);
						num2 = -1;
					}
					gsstatsStored_t = callResult.GetResult();
					r = gsstatsStored_t;
					gsstatsStored_t = null;
					bool flag = r == null;
					if (flag)
					{
						result = Result.Fail;
					}
					else
					{
						result = r.Value.Result;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600115D RID: 4445 RVA: 0x0001EA6C File Offset: 0x0001CC6C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000D65 RID: 3429
			public int <>1__state;

			// Token: 0x04000D66 RID: 3430
			public AsyncTaskMethodBuilder<Result> <>t__builder;

			// Token: 0x04000D67 RID: 3431
			public SteamId steamid;

			// Token: 0x04000D68 RID: 3432
			private GSStatsStored_t? <r>5__1;

			// Token: 0x04000D69 RID: 3433
			private GSStatsStored_t? <>s__2;

			// Token: 0x04000D6A RID: 3434
			private CallResult<GSStatsStored_t> <>u__1;
		}
	}
}
