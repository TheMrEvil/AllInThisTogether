using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	// Token: 0x02000205 RID: 517
	public struct Stat
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0001AF82 File Offset: 0x00019182
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x0001AF8A File Offset: 0x0001918A
		public string Name
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0001AF93 File Offset: 0x00019193
		// (set) Token: 0x06001057 RID: 4183 RVA: 0x0001AF9B File Offset: 0x0001919B
		public SteamId UserId
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<UserId>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<UserId>k__BackingField = value;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0001AFA4 File Offset: 0x000191A4
		public Stat(string name)
		{
			this.Name = name;
			this.UserId = 0UL;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0001AFBD File Offset: 0x000191BD
		public Stat(string name, SteamId user)
		{
			this.Name = name;
			this.UserId = user;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0001AFD0 File Offset: 0x000191D0
		internal void LocalUserOnly([CallerMemberName] string caller = null)
		{
			bool flag = this.UserId == 0UL;
			if (flag)
			{
				return;
			}
			throw new Exception("Stat." + caller + " can only be called for the local user");
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0001B00C File Offset: 0x0001920C
		public double GetGlobalFloat()
		{
			double num = 0.0;
			bool globalStat = SteamUserStats.Internal.GetGlobalStat(this.Name, ref num);
			double result;
			if (globalStat)
			{
				result = num;
			}
			else
			{
				result = 0.0;
			}
			return result;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0001B04C File Offset: 0x0001924C
		public long GetGlobalInt()
		{
			long result = 0L;
			SteamUserStats.Internal.GetGlobalStat(this.Name, ref result);
			return result;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0001B078 File Offset: 0x00019278
		public async Task<long[]> GetGlobalIntDaysAsync(int days)
		{
			GlobalStatsReceived_t? globalStatsReceived_t = await SteamUserStats.Internal.RequestGlobalStats(days);
			GlobalStatsReceived_t? result = globalStatsReceived_t;
			globalStatsReceived_t = null;
			long[] result2;
			if (result == null || result.GetValueOrDefault().Result != Result.OK)
			{
				result2 = null;
			}
			else
			{
				long[] r = new long[days];
				int rows = SteamUserStats.Internal.GetGlobalStatHistory(this.Name, r, (uint)(r.Length * 8));
				if (days != rows)
				{
					r = r.Take(rows).ToArray<long>();
				}
				result2 = r;
			}
			return result2;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0001B0CC File Offset: 0x000192CC
		public async Task<double[]> GetGlobalFloatDays(int days)
		{
			GlobalStatsReceived_t? globalStatsReceived_t = await SteamUserStats.Internal.RequestGlobalStats(days);
			GlobalStatsReceived_t? result = globalStatsReceived_t;
			globalStatsReceived_t = null;
			double[] result2;
			if (result == null || result.GetValueOrDefault().Result != Result.OK)
			{
				result2 = null;
			}
			else
			{
				double[] r = new double[days];
				int rows = SteamUserStats.Internal.GetGlobalStatHistory(this.Name, r, (uint)(r.Length * 8));
				if (days != rows)
				{
					r = r.Take(rows).ToArray<double>();
				}
				result2 = r;
			}
			return result2;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0001B120 File Offset: 0x00019320
		public float GetFloat()
		{
			float num = 0f;
			bool flag = this.UserId > 0UL;
			if (flag)
			{
				SteamUserStats.Internal.GetUserStat(this.UserId, this.Name, ref num);
			}
			else
			{
				SteamUserStats.Internal.GetStat(this.Name, ref num);
			}
			return 0f;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0001B184 File Offset: 0x00019384
		public int GetInt()
		{
			int result = 0;
			bool flag = this.UserId > 0UL;
			if (flag)
			{
				SteamUserStats.Internal.GetUserStat(this.UserId, this.Name, ref result);
			}
			else
			{
				SteamUserStats.Internal.GetStat(this.Name, ref result);
			}
			return result;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0001B1E0 File Offset: 0x000193E0
		public bool Set(int val)
		{
			this.LocalUserOnly("Set");
			return SteamUserStats.Internal.SetStat(this.Name, val);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0001B210 File Offset: 0x00019410
		public bool Set(float val)
		{
			this.LocalUserOnly("Set");
			return SteamUserStats.Internal.SetStat(this.Name, val);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0001B240 File Offset: 0x00019440
		public bool Add(int val)
		{
			this.LocalUserOnly("Add");
			return this.Set(this.GetInt() + val);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0001B26C File Offset: 0x0001946C
		public bool Add(float val)
		{
			this.LocalUserOnly("Add");
			return this.Set(this.GetFloat() + val);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0001B298 File Offset: 0x00019498
		public bool UpdateAverageRate(float count, float sessionlength)
		{
			this.LocalUserOnly("UpdateAverageRate");
			return SteamUserStats.Internal.UpdateAvgRateStat(this.Name, count, (double)sessionlength);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0001B2CC File Offset: 0x000194CC
		public bool Store()
		{
			this.LocalUserOnly("Store");
			return SteamUserStats.Internal.StoreStats();
		}

		// Token: 0x04000C36 RID: 3126
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Name>k__BackingField;

		// Token: 0x04000C37 RID: 3127
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SteamId <UserId>k__BackingField;

		// Token: 0x02000294 RID: 660
		[CompilerGenerated]
		private sealed class <GetGlobalIntDaysAsync>d__13 : IAsyncStateMachine
		{
			// Token: 0x0600126F RID: 4719 RVA: 0x0002533E File Offset: 0x0002353E
			public <GetGlobalIntDaysAsync>d__13()
			{
			}

			// Token: 0x06001270 RID: 4720 RVA: 0x00025348 File Offset: 0x00023548
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				long[] result2;
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
							Stat.<GetGlobalIntDaysAsync>d__13 <GetGlobalIntDaysAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<GlobalStatsReceived_t>, Stat.<GetGlobalIntDaysAsync>d__13>(ref callResult, ref <GetGlobalIntDaysAsync>d__);
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
					bool flag = result == null || result.GetValueOrDefault().Result != Result.OK;
					if (flag)
					{
						result2 = null;
					}
					else
					{
						r = new long[days];
						rows = SteamUserStats.Internal.GetGlobalStatHistory(base.Name, r, (uint)(r.Length * 8));
						bool flag2 = days != rows;
						if (flag2)
						{
							r = r.Take(rows).ToArray<long>();
						}
						result2 = r;
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

			// Token: 0x06001271 RID: 4721 RVA: 0x000254E4 File Offset: 0x000236E4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F7A RID: 3962
			public int <>1__state;

			// Token: 0x04000F7B RID: 3963
			public AsyncTaskMethodBuilder<long[]> <>t__builder;

			// Token: 0x04000F7C RID: 3964
			public int days;

			// Token: 0x04000F7D RID: 3965
			public Stat <>4__this;

			// Token: 0x04000F7E RID: 3966
			private GlobalStatsReceived_t? <result>5__1;

			// Token: 0x04000F7F RID: 3967
			private long[] <r>5__2;

			// Token: 0x04000F80 RID: 3968
			private int <rows>5__3;

			// Token: 0x04000F81 RID: 3969
			private GlobalStatsReceived_t? <>s__4;

			// Token: 0x04000F82 RID: 3970
			private CallResult<GlobalStatsReceived_t> <>u__1;
		}

		// Token: 0x02000295 RID: 661
		[CompilerGenerated]
		private sealed class <GetGlobalFloatDays>d__14 : IAsyncStateMachine
		{
			// Token: 0x06001272 RID: 4722 RVA: 0x000254E6 File Offset: 0x000236E6
			public <GetGlobalFloatDays>d__14()
			{
			}

			// Token: 0x06001273 RID: 4723 RVA: 0x000254F0 File Offset: 0x000236F0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				double[] result2;
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
							Stat.<GetGlobalFloatDays>d__14 <GetGlobalFloatDays>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<GlobalStatsReceived_t>, Stat.<GetGlobalFloatDays>d__14>(ref callResult, ref <GetGlobalFloatDays>d__);
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
					bool flag = result == null || result.GetValueOrDefault().Result != Result.OK;
					if (flag)
					{
						result2 = null;
					}
					else
					{
						r = new double[days];
						rows = SteamUserStats.Internal.GetGlobalStatHistory(base.Name, r, (uint)(r.Length * 8));
						bool flag2 = days != rows;
						if (flag2)
						{
							r = r.Take(rows).ToArray<double>();
						}
						result2 = r;
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

			// Token: 0x06001274 RID: 4724 RVA: 0x0002568C File Offset: 0x0002388C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F83 RID: 3971
			public int <>1__state;

			// Token: 0x04000F84 RID: 3972
			public AsyncTaskMethodBuilder<double[]> <>t__builder;

			// Token: 0x04000F85 RID: 3973
			public int days;

			// Token: 0x04000F86 RID: 3974
			public Stat <>4__this;

			// Token: 0x04000F87 RID: 3975
			private GlobalStatsReceived_t? <result>5__1;

			// Token: 0x04000F88 RID: 3976
			private double[] <r>5__2;

			// Token: 0x04000F89 RID: 3977
			private int <rows>5__3;

			// Token: 0x04000F8A RID: 3978
			private GlobalStatsReceived_t? <>s__4;

			// Token: 0x04000F8B RID: 3979
			private CallResult<GlobalStatsReceived_t> <>u__1;
		}
	}
}
