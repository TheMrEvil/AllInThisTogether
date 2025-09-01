using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	// Token: 0x020001FA RID: 506
	public struct Leaderboard
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00019C2B File Offset: 0x00017E2B
		public string Name
		{
			get
			{
				return SteamUserStats.Internal.GetLeaderboardName(this.Id);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00019C3D File Offset: 0x00017E3D
		public LeaderboardSort Sort
		{
			get
			{
				return SteamUserStats.Internal.GetLeaderboardSortMethod(this.Id);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00019C4F File Offset: 0x00017E4F
		public LeaderboardDisplay Display
		{
			get
			{
				return SteamUserStats.Internal.GetLeaderboardDisplayType(this.Id);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00019C61 File Offset: 0x00017E61
		public int EntryCount
		{
			get
			{
				return SteamUserStats.Internal.GetLeaderboardEntryCount(this.Id);
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00019C74 File Offset: 0x00017E74
		public async Task<LeaderboardUpdate?> ReplaceScore(int score, int[] details = null)
		{
			bool flag = details == null;
			if (flag)
			{
				details = Leaderboard.noDetails;
			}
			LeaderboardScoreUploaded_t? leaderboardScoreUploaded_t = await SteamUserStats.Internal.UploadLeaderboardScore(this.Id, LeaderboardUploadScoreMethod.ForceUpdate, score, details, details.Length);
			LeaderboardScoreUploaded_t? r = leaderboardScoreUploaded_t;
			leaderboardScoreUploaded_t = null;
			LeaderboardUpdate? result;
			if (r == null)
			{
				result = null;
			}
			else
			{
				result = new LeaderboardUpdate?(LeaderboardUpdate.From(r.Value));
			}
			return result;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00019CD0 File Offset: 0x00017ED0
		public async Task<LeaderboardUpdate?> SubmitScoreAsync(int score, int[] details = null)
		{
			bool flag = details == null;
			if (flag)
			{
				details = Leaderboard.noDetails;
			}
			LeaderboardScoreUploaded_t? leaderboardScoreUploaded_t = await SteamUserStats.Internal.UploadLeaderboardScore(this.Id, LeaderboardUploadScoreMethod.KeepBest, score, details, details.Length);
			LeaderboardScoreUploaded_t? r = leaderboardScoreUploaded_t;
			leaderboardScoreUploaded_t = null;
			LeaderboardUpdate? result;
			if (r == null)
			{
				result = null;
			}
			else
			{
				result = new LeaderboardUpdate?(LeaderboardUpdate.From(r.Value));
			}
			return result;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00019D2C File Offset: 0x00017F2C
		public async Task<Result> AttachUgc(Ugc file)
		{
			LeaderboardUGCSet_t? leaderboardUGCSet_t = await SteamUserStats.Internal.AttachLeaderboardUGC(this.Id, file.Handle);
			LeaderboardUGCSet_t? r = leaderboardUGCSet_t;
			leaderboardUGCSet_t = null;
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

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00019D80 File Offset: 0x00017F80
		public async Task<LeaderboardEntry[]> GetScoresAsync(int count, int offset = 1)
		{
			bool flag = offset <= 0;
			if (flag)
			{
				throw new ArgumentException("Should be 1+", "offset");
			}
			LeaderboardScoresDownloaded_t? leaderboardScoresDownloaded_t = await SteamUserStats.Internal.DownloadLeaderboardEntries(this.Id, LeaderboardDataRequest.Global, offset, offset + count);
			LeaderboardScoresDownloaded_t? r = leaderboardScoresDownloaded_t;
			leaderboardScoresDownloaded_t = null;
			LeaderboardEntry[] result;
			if (r == null)
			{
				result = null;
			}
			else
			{
				LeaderboardEntry[] array = await this.LeaderboardResultToEntries(r.Value);
				result = array;
			}
			return result;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00019DDC File Offset: 0x00017FDC
		public async Task<LeaderboardEntry[]> GetScoresAroundUserAsync(int start = -10, int end = 10)
		{
			LeaderboardScoresDownloaded_t? leaderboardScoresDownloaded_t = await SteamUserStats.Internal.DownloadLeaderboardEntries(this.Id, LeaderboardDataRequest.GlobalAroundUser, start, end);
			LeaderboardScoresDownloaded_t? r = leaderboardScoresDownloaded_t;
			leaderboardScoresDownloaded_t = null;
			LeaderboardEntry[] result;
			if (r == null)
			{
				result = null;
			}
			else
			{
				LeaderboardEntry[] array = await this.LeaderboardResultToEntries(r.Value);
				result = array;
			}
			return result;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00019E38 File Offset: 0x00018038
		public async Task<LeaderboardEntry[]> GetScoresFromFriendsAsync()
		{
			LeaderboardScoresDownloaded_t? leaderboardScoresDownloaded_t = await SteamUserStats.Internal.DownloadLeaderboardEntries(this.Id, LeaderboardDataRequest.Friends, 0, 0);
			LeaderboardScoresDownloaded_t? r = leaderboardScoresDownloaded_t;
			leaderboardScoresDownloaded_t = null;
			LeaderboardEntry[] result;
			if (r == null)
			{
				result = null;
			}
			else
			{
				LeaderboardEntry[] array = await this.LeaderboardResultToEntries(r.Value);
				result = array;
			}
			return result;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00019E84 File Offset: 0x00018084
		internal async Task<LeaderboardEntry[]> LeaderboardResultToEntries(LeaderboardScoresDownloaded_t r)
		{
			bool flag = r.CEntryCount <= 0;
			LeaderboardEntry[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LeaderboardEntry[] output = new LeaderboardEntry[r.CEntryCount];
				LeaderboardEntry_t e = default(LeaderboardEntry_t);
				int num;
				for (int i = 0; i < output.Length; i = num + 1)
				{
					bool downloadedLeaderboardEntry = SteamUserStats.Internal.GetDownloadedLeaderboardEntry(r.SteamLeaderboardEntries, i, ref e, Leaderboard.detailsBuffer, Leaderboard.detailsBuffer.Length);
					if (downloadedLeaderboardEntry)
					{
						output[i] = LeaderboardEntry.From(e, Leaderboard.detailsBuffer);
					}
					num = i;
				}
				await Leaderboard.WaitForUserNames(output);
				result = output;
			}
			return result;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00019ED8 File Offset: 0x000180D8
		internal static async Task WaitForUserNames(LeaderboardEntry[] entries)
		{
			bool gotAll = false;
			while (!gotAll)
			{
				gotAll = true;
				foreach (LeaderboardEntry entry in entries)
				{
					bool flag = entry.User.Id == 0UL;
					if (!flag)
					{
						bool flag2 = !SteamFriends.Internal.RequestUserInformation(entry.User.Id, true);
						if (!flag2)
						{
							gotAll = false;
							entry = default(LeaderboardEntry);
						}
					}
				}
				LeaderboardEntry[] array = null;
				await Task.Delay(1);
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00019F1F File Offset: 0x0001811F
		// Note: this type is marked as 'beforefieldinit'.
		static Leaderboard()
		{
		}

		// Token: 0x04000C02 RID: 3074
		internal SteamLeaderboard_t Id;

		// Token: 0x04000C03 RID: 3075
		private static int[] detailsBuffer = new int[64];

		// Token: 0x04000C04 RID: 3076
		private static int[] noDetails = Array.Empty<int>();

		// Token: 0x02000287 RID: 647
		[CompilerGenerated]
		private sealed class <ReplaceScore>d__11 : IAsyncStateMachine
		{
			// Token: 0x0600123E RID: 4670 RVA: 0x0002406E File Offset: 0x0002226E
			public <ReplaceScore>d__11()
			{
			}

			// Token: 0x0600123F RID: 4671 RVA: 0x00024078 File Offset: 0x00022278
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				LeaderboardUpdate? result;
				try
				{
					CallResult<LeaderboardScoreUploaded_t> callResult;
					if (num != 0)
					{
						bool flag = details == null;
						if (flag)
						{
							details = Leaderboard.noDetails;
						}
						callResult = SteamUserStats.Internal.UploadLeaderboardScore(this.Id, LeaderboardUploadScoreMethod.ForceUpdate, score, details, details.Length).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LeaderboardScoreUploaded_t> callResult2 = callResult;
							Leaderboard.<ReplaceScore>d__11 <ReplaceScore>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LeaderboardScoreUploaded_t>, Leaderboard.<ReplaceScore>d__11>(ref callResult, ref <ReplaceScore>d__);
							return;
						}
					}
					else
					{
						CallResult<LeaderboardScoreUploaded_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LeaderboardScoreUploaded_t>);
						num2 = -1;
					}
					leaderboardScoreUploaded_t = callResult.GetResult();
					r = leaderboardScoreUploaded_t;
					leaderboardScoreUploaded_t = null;
					bool flag2 = r == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = new LeaderboardUpdate?(LeaderboardUpdate.From(r.Value));
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

			// Token: 0x06001240 RID: 4672 RVA: 0x000241C8 File Offset: 0x000223C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F15 RID: 3861
			public int <>1__state;

			// Token: 0x04000F16 RID: 3862
			public AsyncTaskMethodBuilder<LeaderboardUpdate?> <>t__builder;

			// Token: 0x04000F17 RID: 3863
			public int score;

			// Token: 0x04000F18 RID: 3864
			public int[] details;

			// Token: 0x04000F19 RID: 3865
			public Leaderboard <>4__this;

			// Token: 0x04000F1A RID: 3866
			private LeaderboardScoreUploaded_t? <r>5__1;

			// Token: 0x04000F1B RID: 3867
			private LeaderboardScoreUploaded_t? <>s__2;

			// Token: 0x04000F1C RID: 3868
			private CallResult<LeaderboardScoreUploaded_t> <>u__1;
		}

		// Token: 0x02000288 RID: 648
		[CompilerGenerated]
		private sealed class <SubmitScoreAsync>d__12 : IAsyncStateMachine
		{
			// Token: 0x06001241 RID: 4673 RVA: 0x000241CA File Offset: 0x000223CA
			public <SubmitScoreAsync>d__12()
			{
			}

			// Token: 0x06001242 RID: 4674 RVA: 0x000241D4 File Offset: 0x000223D4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				LeaderboardUpdate? result;
				try
				{
					CallResult<LeaderboardScoreUploaded_t> callResult;
					if (num != 0)
					{
						bool flag = details == null;
						if (flag)
						{
							details = Leaderboard.noDetails;
						}
						callResult = SteamUserStats.Internal.UploadLeaderboardScore(this.Id, LeaderboardUploadScoreMethod.KeepBest, score, details, details.Length).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LeaderboardScoreUploaded_t> callResult2 = callResult;
							Leaderboard.<SubmitScoreAsync>d__12 <SubmitScoreAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LeaderboardScoreUploaded_t>, Leaderboard.<SubmitScoreAsync>d__12>(ref callResult, ref <SubmitScoreAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<LeaderboardScoreUploaded_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LeaderboardScoreUploaded_t>);
						num2 = -1;
					}
					leaderboardScoreUploaded_t = callResult.GetResult();
					r = leaderboardScoreUploaded_t;
					leaderboardScoreUploaded_t = null;
					bool flag2 = r == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = new LeaderboardUpdate?(LeaderboardUpdate.From(r.Value));
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

			// Token: 0x06001243 RID: 4675 RVA: 0x00024324 File Offset: 0x00022524
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F1D RID: 3869
			public int <>1__state;

			// Token: 0x04000F1E RID: 3870
			public AsyncTaskMethodBuilder<LeaderboardUpdate?> <>t__builder;

			// Token: 0x04000F1F RID: 3871
			public int score;

			// Token: 0x04000F20 RID: 3872
			public int[] details;

			// Token: 0x04000F21 RID: 3873
			public Leaderboard <>4__this;

			// Token: 0x04000F22 RID: 3874
			private LeaderboardScoreUploaded_t? <r>5__1;

			// Token: 0x04000F23 RID: 3875
			private LeaderboardScoreUploaded_t? <>s__2;

			// Token: 0x04000F24 RID: 3876
			private CallResult<LeaderboardScoreUploaded_t> <>u__1;
		}

		// Token: 0x02000289 RID: 649
		[CompilerGenerated]
		private sealed class <AttachUgc>d__13 : IAsyncStateMachine
		{
			// Token: 0x06001244 RID: 4676 RVA: 0x00024326 File Offset: 0x00022526
			public <AttachUgc>d__13()
			{
			}

			// Token: 0x06001245 RID: 4677 RVA: 0x00024330 File Offset: 0x00022530
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Result result;
				try
				{
					CallResult<LeaderboardUGCSet_t> callResult;
					if (num != 0)
					{
						callResult = SteamUserStats.Internal.AttachLeaderboardUGC(this.Id, file.Handle).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LeaderboardUGCSet_t> callResult2 = callResult;
							Leaderboard.<AttachUgc>d__13 <AttachUgc>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LeaderboardUGCSet_t>, Leaderboard.<AttachUgc>d__13>(ref callResult, ref <AttachUgc>d__);
							return;
						}
					}
					else
					{
						CallResult<LeaderboardUGCSet_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LeaderboardUGCSet_t>);
						num2 = -1;
					}
					leaderboardUGCSet_t = callResult.GetResult();
					r = leaderboardUGCSet_t;
					leaderboardUGCSet_t = null;
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

			// Token: 0x06001246 RID: 4678 RVA: 0x00024454 File Offset: 0x00022654
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F25 RID: 3877
			public int <>1__state;

			// Token: 0x04000F26 RID: 3878
			public AsyncTaskMethodBuilder<Result> <>t__builder;

			// Token: 0x04000F27 RID: 3879
			public Ugc file;

			// Token: 0x04000F28 RID: 3880
			public Leaderboard <>4__this;

			// Token: 0x04000F29 RID: 3881
			private LeaderboardUGCSet_t? <r>5__1;

			// Token: 0x04000F2A RID: 3882
			private LeaderboardUGCSet_t? <>s__2;

			// Token: 0x04000F2B RID: 3883
			private CallResult<LeaderboardUGCSet_t> <>u__1;
		}

		// Token: 0x0200028A RID: 650
		[CompilerGenerated]
		private sealed class <GetScoresAsync>d__14 : IAsyncStateMachine
		{
			// Token: 0x06001247 RID: 4679 RVA: 0x00024456 File Offset: 0x00022656
			public <GetScoresAsync>d__14()
			{
			}

			// Token: 0x06001248 RID: 4680 RVA: 0x00024460 File Offset: 0x00022660
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				LeaderboardEntry[] result;
				try
				{
					TaskAwaiter<LeaderboardEntry[]> taskAwaiter;
					CallResult<LeaderboardScoresDownloaded_t> callResult;
					if (num != 0)
					{
						if (num == 1)
						{
							TaskAwaiter<LeaderboardEntry[]> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<LeaderboardEntry[]>);
							num2 = -1;
							goto IL_166;
						}
						bool flag = offset <= 0;
						if (flag)
						{
							throw new ArgumentException("Should be 1+", "offset");
						}
						callResult = SteamUserStats.Internal.DownloadLeaderboardEntries(this.Id, LeaderboardDataRequest.Global, offset, offset + count).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LeaderboardScoresDownloaded_t> callResult2 = callResult;
							Leaderboard.<GetScoresAsync>d__14 <GetScoresAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LeaderboardScoresDownloaded_t>, Leaderboard.<GetScoresAsync>d__14>(ref callResult, ref <GetScoresAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<LeaderboardScoresDownloaded_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LeaderboardScoresDownloaded_t>);
						num2 = -1;
					}
					leaderboardScoresDownloaded_t = callResult.GetResult();
					r = leaderboardScoresDownloaded_t;
					leaderboardScoresDownloaded_t = null;
					bool flag2 = r == null;
					if (flag2)
					{
						result = null;
						goto IL_196;
					}
					taskAwaiter = base.LeaderboardResultToEntries(r.Value).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						num2 = 1;
						TaskAwaiter<LeaderboardEntry[]> taskAwaiter2 = taskAwaiter;
						Leaderboard.<GetScoresAsync>d__14 <GetScoresAsync>d__ = this;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<LeaderboardEntry[]>, Leaderboard.<GetScoresAsync>d__14>(ref taskAwaiter, ref <GetScoresAsync>d__);
						return;
					}
					IL_166:
					array = taskAwaiter.GetResult();
					result = array;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_196:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001249 RID: 4681 RVA: 0x00024634 File Offset: 0x00022834
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F2C RID: 3884
			public int <>1__state;

			// Token: 0x04000F2D RID: 3885
			public AsyncTaskMethodBuilder<LeaderboardEntry[]> <>t__builder;

			// Token: 0x04000F2E RID: 3886
			public int count;

			// Token: 0x04000F2F RID: 3887
			public int offset;

			// Token: 0x04000F30 RID: 3888
			public Leaderboard <>4__this;

			// Token: 0x04000F31 RID: 3889
			private LeaderboardScoresDownloaded_t? <r>5__1;

			// Token: 0x04000F32 RID: 3890
			private LeaderboardScoresDownloaded_t? <>s__2;

			// Token: 0x04000F33 RID: 3891
			private LeaderboardEntry[] <>s__3;

			// Token: 0x04000F34 RID: 3892
			private CallResult<LeaderboardScoresDownloaded_t> <>u__1;

			// Token: 0x04000F35 RID: 3893
			private TaskAwaiter<LeaderboardEntry[]> <>u__2;
		}

		// Token: 0x0200028B RID: 651
		[CompilerGenerated]
		private sealed class <GetScoresAroundUserAsync>d__15 : IAsyncStateMachine
		{
			// Token: 0x0600124A RID: 4682 RVA: 0x00024636 File Offset: 0x00022836
			public <GetScoresAroundUserAsync>d__15()
			{
			}

			// Token: 0x0600124B RID: 4683 RVA: 0x00024640 File Offset: 0x00022840
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				LeaderboardEntry[] result;
				try
				{
					TaskAwaiter<LeaderboardEntry[]> taskAwaiter;
					CallResult<LeaderboardScoresDownloaded_t> callResult;
					if (num != 0)
					{
						if (num == 1)
						{
							TaskAwaiter<LeaderboardEntry[]> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<LeaderboardEntry[]>);
							num2 = -1;
							goto IL_13B;
						}
						callResult = SteamUserStats.Internal.DownloadLeaderboardEntries(this.Id, LeaderboardDataRequest.GlobalAroundUser, start, end).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LeaderboardScoresDownloaded_t> callResult2 = callResult;
							Leaderboard.<GetScoresAroundUserAsync>d__15 <GetScoresAroundUserAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LeaderboardScoresDownloaded_t>, Leaderboard.<GetScoresAroundUserAsync>d__15>(ref callResult, ref <GetScoresAroundUserAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<LeaderboardScoresDownloaded_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LeaderboardScoresDownloaded_t>);
						num2 = -1;
					}
					leaderboardScoresDownloaded_t = callResult.GetResult();
					r = leaderboardScoresDownloaded_t;
					leaderboardScoresDownloaded_t = null;
					bool flag = r == null;
					if (flag)
					{
						result = null;
						goto IL_16B;
					}
					taskAwaiter = base.LeaderboardResultToEntries(r.Value).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						num2 = 1;
						TaskAwaiter<LeaderboardEntry[]> taskAwaiter2 = taskAwaiter;
						Leaderboard.<GetScoresAroundUserAsync>d__15 <GetScoresAroundUserAsync>d__ = this;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<LeaderboardEntry[]>, Leaderboard.<GetScoresAroundUserAsync>d__15>(ref taskAwaiter, ref <GetScoresAroundUserAsync>d__);
						return;
					}
					IL_13B:
					array = taskAwaiter.GetResult();
					result = array;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_16B:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600124C RID: 4684 RVA: 0x000247EC File Offset: 0x000229EC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F36 RID: 3894
			public int <>1__state;

			// Token: 0x04000F37 RID: 3895
			public AsyncTaskMethodBuilder<LeaderboardEntry[]> <>t__builder;

			// Token: 0x04000F38 RID: 3896
			public int start;

			// Token: 0x04000F39 RID: 3897
			public int end;

			// Token: 0x04000F3A RID: 3898
			public Leaderboard <>4__this;

			// Token: 0x04000F3B RID: 3899
			private LeaderboardScoresDownloaded_t? <r>5__1;

			// Token: 0x04000F3C RID: 3900
			private LeaderboardScoresDownloaded_t? <>s__2;

			// Token: 0x04000F3D RID: 3901
			private LeaderboardEntry[] <>s__3;

			// Token: 0x04000F3E RID: 3902
			private CallResult<LeaderboardScoresDownloaded_t> <>u__1;

			// Token: 0x04000F3F RID: 3903
			private TaskAwaiter<LeaderboardEntry[]> <>u__2;
		}

		// Token: 0x0200028C RID: 652
		[CompilerGenerated]
		private sealed class <GetScoresFromFriendsAsync>d__16 : IAsyncStateMachine
		{
			// Token: 0x0600124D RID: 4685 RVA: 0x000247EE File Offset: 0x000229EE
			public <GetScoresFromFriendsAsync>d__16()
			{
			}

			// Token: 0x0600124E RID: 4686 RVA: 0x000247F8 File Offset: 0x000229F8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				LeaderboardEntry[] result;
				try
				{
					TaskAwaiter<LeaderboardEntry[]> taskAwaiter;
					CallResult<LeaderboardScoresDownloaded_t> callResult;
					if (num != 0)
					{
						if (num == 1)
						{
							TaskAwaiter<LeaderboardEntry[]> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<LeaderboardEntry[]>);
							num2 = -1;
							goto IL_131;
						}
						callResult = SteamUserStats.Internal.DownloadLeaderboardEntries(this.Id, LeaderboardDataRequest.Friends, 0, 0).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LeaderboardScoresDownloaded_t> callResult2 = callResult;
							Leaderboard.<GetScoresFromFriendsAsync>d__16 <GetScoresFromFriendsAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LeaderboardScoresDownloaded_t>, Leaderboard.<GetScoresFromFriendsAsync>d__16>(ref callResult, ref <GetScoresFromFriendsAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<LeaderboardScoresDownloaded_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LeaderboardScoresDownloaded_t>);
						num2 = -1;
					}
					leaderboardScoresDownloaded_t = callResult.GetResult();
					r = leaderboardScoresDownloaded_t;
					leaderboardScoresDownloaded_t = null;
					bool flag = r == null;
					if (flag)
					{
						result = null;
						goto IL_161;
					}
					taskAwaiter = base.LeaderboardResultToEntries(r.Value).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						num2 = 1;
						TaskAwaiter<LeaderboardEntry[]> taskAwaiter2 = taskAwaiter;
						Leaderboard.<GetScoresFromFriendsAsync>d__16 <GetScoresFromFriendsAsync>d__ = this;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<LeaderboardEntry[]>, Leaderboard.<GetScoresFromFriendsAsync>d__16>(ref taskAwaiter, ref <GetScoresFromFriendsAsync>d__);
						return;
					}
					IL_131:
					array = taskAwaiter.GetResult();
					result = array;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_161:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600124F RID: 4687 RVA: 0x00024998 File Offset: 0x00022B98
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F40 RID: 3904
			public int <>1__state;

			// Token: 0x04000F41 RID: 3905
			public AsyncTaskMethodBuilder<LeaderboardEntry[]> <>t__builder;

			// Token: 0x04000F42 RID: 3906
			public Leaderboard <>4__this;

			// Token: 0x04000F43 RID: 3907
			private LeaderboardScoresDownloaded_t? <r>5__1;

			// Token: 0x04000F44 RID: 3908
			private LeaderboardScoresDownloaded_t? <>s__2;

			// Token: 0x04000F45 RID: 3909
			private LeaderboardEntry[] <>s__3;

			// Token: 0x04000F46 RID: 3910
			private CallResult<LeaderboardScoresDownloaded_t> <>u__1;

			// Token: 0x04000F47 RID: 3911
			private TaskAwaiter<LeaderboardEntry[]> <>u__2;
		}

		// Token: 0x0200028D RID: 653
		[CompilerGenerated]
		private sealed class <LeaderboardResultToEntries>d__17 : IAsyncStateMachine
		{
			// Token: 0x06001250 RID: 4688 RVA: 0x0002499A File Offset: 0x00022B9A
			public <LeaderboardResultToEntries>d__17()
			{
			}

			// Token: 0x06001251 RID: 4689 RVA: 0x000249A4 File Offset: 0x00022BA4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				LeaderboardEntry[] result;
				try
				{
					TaskAwaiter taskAwaiter;
					if (num != 0)
					{
						bool flag = r.CEntryCount <= 0;
						if (flag)
						{
							result = null;
							goto IL_168;
						}
						output = new LeaderboardEntry[r.CEntryCount];
						e = default(LeaderboardEntry_t);
						int num3;
						for (i = 0; i < output.Length; i = num3 + 1)
						{
							bool downloadedLeaderboardEntry = SteamUserStats.Internal.GetDownloadedLeaderboardEntry(r.SteamLeaderboardEntries, i, ref e, Leaderboard.detailsBuffer, Leaderboard.detailsBuffer.Length);
							if (downloadedLeaderboardEntry)
							{
								output[i] = LeaderboardEntry.From(e, Leaderboard.detailsBuffer);
							}
							num3 = i;
						}
						taskAwaiter = Leaderboard.WaitForUserNames(output).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							TaskAwaiter taskAwaiter2 = taskAwaiter;
							Leaderboard.<LeaderboardResultToEntries>d__17 <LeaderboardResultToEntries>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Leaderboard.<LeaderboardResultToEntries>d__17>(ref taskAwaiter, ref <LeaderboardResultToEntries>d__);
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
					result = output;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_168:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001252 RID: 4690 RVA: 0x00024B4C File Offset: 0x00022D4C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F48 RID: 3912
			public int <>1__state;

			// Token: 0x04000F49 RID: 3913
			public AsyncTaskMethodBuilder<LeaderboardEntry[]> <>t__builder;

			// Token: 0x04000F4A RID: 3914
			public LeaderboardScoresDownloaded_t r;

			// Token: 0x04000F4B RID: 3915
			public Leaderboard <>4__this;

			// Token: 0x04000F4C RID: 3916
			private LeaderboardEntry[] <output>5__1;

			// Token: 0x04000F4D RID: 3917
			private LeaderboardEntry_t <e>5__2;

			// Token: 0x04000F4E RID: 3918
			private int <i>5__3;

			// Token: 0x04000F4F RID: 3919
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200028E RID: 654
		[CompilerGenerated]
		private sealed class <WaitForUserNames>d__18 : IAsyncStateMachine
		{
			// Token: 0x06001253 RID: 4691 RVA: 0x00024B4E File Offset: 0x00022D4E
			public <WaitForUserNames>d__18()
			{
			}

			// Token: 0x06001254 RID: 4692 RVA: 0x00024B58 File Offset: 0x00022D58
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					if (num != 0)
					{
						gotAll = false;
						goto IL_132;
					}
					TaskAwaiter taskAwaiter2;
					TaskAwaiter taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter);
					num2 = -1;
					IL_129:
					taskAwaiter.GetResult();
					IL_132:
					if (!gotAll)
					{
						gotAll = true;
						array = entries;
						for (i = 0; i < array.Length; i++)
						{
							entry = array[i];
							bool flag = entry.User.Id == 0UL;
							if (!flag)
							{
								bool flag2 = !SteamFriends.Internal.RequestUserInformation(entry.User.Id, true);
								if (!flag2)
								{
									gotAll = false;
									entry = default(LeaderboardEntry);
								}
							}
						}
						array = null;
						taskAwaiter = Task.Delay(1).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							taskAwaiter2 = taskAwaiter;
							Leaderboard.<WaitForUserNames>d__18 <WaitForUserNames>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Leaderboard.<WaitForUserNames>d__18>(ref taskAwaiter, ref <WaitForUserNames>d__);
							return;
						}
						goto IL_129;
					}
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

			// Token: 0x06001255 RID: 4693 RVA: 0x00024CF8 File Offset: 0x00022EF8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F50 RID: 3920
			public int <>1__state;

			// Token: 0x04000F51 RID: 3921
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000F52 RID: 3922
			public LeaderboardEntry[] entries;

			// Token: 0x04000F53 RID: 3923
			private bool <gotAll>5__1;

			// Token: 0x04000F54 RID: 3924
			private LeaderboardEntry[] <>s__2;

			// Token: 0x04000F55 RID: 3925
			private int <>s__3;

			// Token: 0x04000F56 RID: 3926
			private LeaderboardEntry <entry>5__4;

			// Token: 0x04000F57 RID: 3927
			private TaskAwaiter <>u__1;
		}
	}
}
