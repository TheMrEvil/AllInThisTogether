using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	// Token: 0x020001FE RID: 510
	public struct LobbyQuery
	{
		// Token: 0x06000FFB RID: 4091 RVA: 0x0001A460 File Offset: 0x00018660
		public LobbyQuery FilterDistanceClose()
		{
			this.distance = new LobbyDistanceFilter?(LobbyDistanceFilter.Close);
			return this;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0001A484 File Offset: 0x00018684
		public LobbyQuery FilterDistanceFar()
		{
			this.distance = new LobbyDistanceFilter?(LobbyDistanceFilter.Far);
			return this;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0001A4A8 File Offset: 0x000186A8
		public LobbyQuery FilterDistanceWorldwide()
		{
			this.distance = new LobbyDistanceFilter?(LobbyDistanceFilter.Worldwide);
			return this;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0001A4CC File Offset: 0x000186CC
		public LobbyQuery WithKeyValue(string key, string value)
		{
			bool flag = string.IsNullOrEmpty(key);
			if (flag)
			{
				throw new ArgumentException("Key string provided for LobbyQuery filter is null or empty", "key");
			}
			bool flag2 = key.Length > SteamMatchmaking.MaxLobbyKeyLength;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Key length is longer than {0}", SteamMatchmaking.MaxLobbyKeyLength), "key");
			}
			bool flag3 = this.stringFilters == null;
			if (flag3)
			{
				this.stringFilters = new Dictionary<string, string>();
			}
			this.stringFilters.Add(key, value);
			return this;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0001A554 File Offset: 0x00018754
		public LobbyQuery WithLower(string key, int value)
		{
			this.AddNumericalFilter(key, value, LobbyComparison.LessThan);
			return this;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0001A578 File Offset: 0x00018778
		public LobbyQuery WithHigher(string key, int value)
		{
			this.AddNumericalFilter(key, value, LobbyComparison.GreaterThan);
			return this;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0001A59C File Offset: 0x0001879C
		public LobbyQuery WithEqual(string key, int value)
		{
			this.AddNumericalFilter(key, value, LobbyComparison.Equal);
			return this;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0001A5C0 File Offset: 0x000187C0
		public LobbyQuery WithNotEqual(string key, int value)
		{
			this.AddNumericalFilter(key, value, LobbyComparison.NotEqual);
			return this;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0001A5E4 File Offset: 0x000187E4
		internal void AddNumericalFilter(string key, int value, LobbyComparison compare)
		{
			bool flag = string.IsNullOrEmpty(key);
			if (flag)
			{
				throw new ArgumentException("Key string provided for LobbyQuery filter is null or empty", "key");
			}
			bool flag2 = key.Length > SteamMatchmaking.MaxLobbyKeyLength;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Key length is longer than {0}", SteamMatchmaking.MaxLobbyKeyLength), "key");
			}
			bool flag3 = this.numericalFilters == null;
			if (flag3)
			{
				this.numericalFilters = new List<NumericalFilter>();
			}
			this.numericalFilters.Add(new NumericalFilter(key, value, compare));
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0001A668 File Offset: 0x00018868
		public LobbyQuery OrderByNear(string key, int value)
		{
			bool flag = string.IsNullOrEmpty(key);
			if (flag)
			{
				throw new ArgumentException("Key string provided for LobbyQuery filter is null or empty", "key");
			}
			bool flag2 = key.Length > SteamMatchmaking.MaxLobbyKeyLength;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Key length is longer than {0}", SteamMatchmaking.MaxLobbyKeyLength), "key");
			}
			bool flag3 = this.nearValFilters == null;
			if (flag3)
			{
				this.nearValFilters = new Dictionary<string, int>();
			}
			this.nearValFilters.Add(key, value);
			return this;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0001A6F0 File Offset: 0x000188F0
		public LobbyQuery WithSlotsAvailable(int minSlots)
		{
			this.slotsAvailable = new int?(minSlots);
			return this;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0001A714 File Offset: 0x00018914
		public LobbyQuery WithMaxResults(int max)
		{
			this.maxResults = new int?(max);
			return this;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0001A738 File Offset: 0x00018938
		private void ApplyFilters()
		{
			bool flag = this.distance != null;
			if (flag)
			{
				SteamMatchmaking.Internal.AddRequestLobbyListDistanceFilter(this.distance.Value);
			}
			bool flag2 = this.slotsAvailable != null;
			if (flag2)
			{
				SteamMatchmaking.Internal.AddRequestLobbyListFilterSlotsAvailable(this.slotsAvailable.Value);
			}
			bool flag3 = this.maxResults != null;
			if (flag3)
			{
				SteamMatchmaking.Internal.AddRequestLobbyListResultCountFilter(this.maxResults.Value);
			}
			bool flag4 = this.stringFilters != null;
			if (flag4)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.stringFilters)
				{
					SteamMatchmaking.Internal.AddRequestLobbyListStringFilter(keyValuePair.Key, keyValuePair.Value, LobbyComparison.Equal);
				}
			}
			bool flag5 = this.numericalFilters != null;
			if (flag5)
			{
				foreach (NumericalFilter numericalFilter in this.numericalFilters)
				{
					SteamMatchmaking.Internal.AddRequestLobbyListNumericalFilter(numericalFilter.Key, numericalFilter.Value, numericalFilter.Comparer);
				}
			}
			bool flag6 = this.nearValFilters != null;
			if (flag6)
			{
				foreach (KeyValuePair<string, int> keyValuePair2 in this.nearValFilters)
				{
					SteamMatchmaking.Internal.AddRequestLobbyListNearValueFilter(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0001A908 File Offset: 0x00018B08
		public async Task<Lobby[]> RequestAsync()
		{
			this.ApplyFilters();
			LobbyMatchList_t? lobbyMatchList_t = await SteamMatchmaking.Internal.RequestLobbyList();
			LobbyMatchList_t? list = lobbyMatchList_t;
			lobbyMatchList_t = null;
			Lobby[] result;
			if (list == null || list.Value.LobbiesMatching == 0U)
			{
				result = null;
			}
			else
			{
				Lobby[] lobbies = new Lobby[list.Value.LobbiesMatching];
				int i = 0;
				while ((long)i < (long)((ulong)list.Value.LobbiesMatching))
				{
					lobbies[i] = new Lobby
					{
						Id = SteamMatchmaking.Internal.GetLobbyByIndex(i)
					};
					i++;
				}
				result = lobbies;
			}
			return result;
		}

		// Token: 0x04000C0E RID: 3086
		internal LobbyDistanceFilter? distance;

		// Token: 0x04000C0F RID: 3087
		internal Dictionary<string, string> stringFilters;

		// Token: 0x04000C10 RID: 3088
		internal List<NumericalFilter> numericalFilters;

		// Token: 0x04000C11 RID: 3089
		internal Dictionary<string, int> nearValFilters;

		// Token: 0x04000C12 RID: 3090
		internal int? slotsAvailable;

		// Token: 0x04000C13 RID: 3091
		internal int? maxResults;

		// Token: 0x02000292 RID: 658
		[CompilerGenerated]
		private sealed class <RequestAsync>d__19 : IAsyncStateMachine
		{
			// Token: 0x06001269 RID: 4713 RVA: 0x0002509B File Offset: 0x0002329B
			public <RequestAsync>d__19()
			{
			}

			// Token: 0x0600126A RID: 4714 RVA: 0x000250A4 File Offset: 0x000232A4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Lobby[] result;
				try
				{
					CallResult<LobbyMatchList_t> callResult;
					if (num != 0)
					{
						base.ApplyFilters();
						callResult = SteamMatchmaking.Internal.RequestLobbyList().GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<LobbyMatchList_t> callResult2 = callResult;
							LobbyQuery.<RequestAsync>d__19 <RequestAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<LobbyMatchList_t>, LobbyQuery.<RequestAsync>d__19>(ref callResult, ref <RequestAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<LobbyMatchList_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<LobbyMatchList_t>);
						num2 = -1;
					}
					lobbyMatchList_t = callResult.GetResult();
					list = lobbyMatchList_t;
					lobbyMatchList_t = null;
					bool flag = list == null || list.Value.LobbiesMatching == 0U;
					if (flag)
					{
						result = null;
					}
					else
					{
						lobbies = new Lobby[list.Value.LobbiesMatching];
						i = 0;
						while ((long)i < (long)((ulong)list.Value.LobbiesMatching))
						{
							lobbies[i] = new Lobby
							{
								Id = SteamMatchmaking.Internal.GetLobbyByIndex(i)
							};
							int num3 = i;
							i = num3 + 1;
						}
						result = lobbies;
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

			// Token: 0x0600126B RID: 4715 RVA: 0x00025264 File Offset: 0x00023464
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000F6D RID: 3949
			public int <>1__state;

			// Token: 0x04000F6E RID: 3950
			public AsyncTaskMethodBuilder<Lobby[]> <>t__builder;

			// Token: 0x04000F6F RID: 3951
			public LobbyQuery <>4__this;

			// Token: 0x04000F70 RID: 3952
			private LobbyMatchList_t? <list>5__1;

			// Token: 0x04000F71 RID: 3953
			private Lobby[] <lobbies>5__2;

			// Token: 0x04000F72 RID: 3954
			private LobbyMatchList_t? <>s__3;

			// Token: 0x04000F73 RID: 3955
			private int <i>5__4;

			// Token: 0x04000F74 RID: 3956
			private CallResult<LobbyMatchList_t> <>u__1;
		}
	}
}
