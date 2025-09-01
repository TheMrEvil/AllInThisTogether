using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks.ServerList
{
	// Token: 0x020000CB RID: 203
	public abstract class Base : IDisposable
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00014B47 File Offset: 0x00012D47
		internal static ISteamMatchmakingServers Internal
		{
			get
			{
				return SteamMatchmakingServers.Internal;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00014B4E File Offset: 0x00012D4E
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x00014B56 File Offset: 0x00012D56
		public AppId AppId
		{
			[CompilerGenerated]
			get
			{
				return this.<AppId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AppId>k__BackingField = value;
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000AC5 RID: 2757 RVA: 0x00014B60 File Offset: 0x00012D60
		// (remove) Token: 0x06000AC6 RID: 2758 RVA: 0x00014B98 File Offset: 0x00012D98
		public event Action OnChanges
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnChanges;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnChanges, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnChanges;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnChanges, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000AC7 RID: 2759 RVA: 0x00014BD0 File Offset: 0x00012DD0
		// (remove) Token: 0x06000AC8 RID: 2760 RVA: 0x00014C08 File Offset: 0x00012E08
		public event Action<ServerInfo> OnResponsiveServer
		{
			[CompilerGenerated]
			add
			{
				Action<ServerInfo> action = this.OnResponsiveServer;
				Action<ServerInfo> action2;
				do
				{
					action2 = action;
					Action<ServerInfo> value2 = (Action<ServerInfo>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ServerInfo>>(ref this.OnResponsiveServer, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ServerInfo> action = this.OnResponsiveServer;
				Action<ServerInfo> action2;
				do
				{
					action2 = action;
					Action<ServerInfo> value2 = (Action<ServerInfo>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ServerInfo>>(ref this.OnResponsiveServer, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00014C40 File Offset: 0x00012E40
		public Base()
		{
			this.AppId = SteamClient.AppId;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00014C94 File Offset: 0x00012E94
		public virtual async Task<bool> RunQueryAsync(float timeoutSeconds = 10f)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.Reset();
			this.LaunchQuery();
			HServerListRequest thisRequest = this.request;
			while (this.IsRefreshing)
			{
				await Task.Delay(33);
				bool result;
				if (this.request.Value == IntPtr.Zero || thisRequest.Value != this.request.Value)
				{
					result = false;
				}
				else if (!SteamClient.IsValid)
				{
					result = false;
				}
				else
				{
					int r = this.Responsive.Count;
					this.UpdatePending();
					this.UpdateResponsive();
					if (r != this.Responsive.Count)
					{
						this.InvokeChanges();
					}
					if (stopwatch.Elapsed.TotalSeconds > (double)timeoutSeconds)
					{
						break;
					}
					continue;
				}
				return result;
			}
			this.MovePendingToUnresponsive();
			this.InvokeChanges();
			return true;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00014CE2 File Offset: 0x00012EE2
		public virtual void Cancel()
		{
			Base.Internal.CancelQuery(this.request);
		}

		// Token: 0x06000ACC RID: 2764
		internal abstract void LaunchQuery();

		// Token: 0x06000ACD RID: 2765 RVA: 0x00014CF5 File Offset: 0x00012EF5
		internal virtual MatchMakingKeyValuePair[] GetFilters()
		{
			return this.filters.ToArray();
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00014D04 File Offset: 0x00012F04
		public void AddFilter(string key, string value)
		{
			this.filters.Add(new MatchMakingKeyValuePair
			{
				Key = key,
				Value = value
			});
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00014D37 File Offset: 0x00012F37
		internal int Count
		{
			get
			{
				return Base.Internal.GetServerCount(this.request);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00014D49 File Offset: 0x00012F49
		internal bool IsRefreshing
		{
			get
			{
				return this.request.Value != IntPtr.Zero && Base.Internal.IsRefreshing(this.request);
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00014D75 File Offset: 0x00012F75
		private void Reset()
		{
			this.ReleaseQuery();
			this.LastCount = 0;
			this.watchList.Clear();
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00014D94 File Offset: 0x00012F94
		private void ReleaseQuery()
		{
			bool flag = this.request.Value != IntPtr.Zero;
			if (flag)
			{
				this.Cancel();
				Base.Internal.ReleaseRequest(this.request);
				this.request = IntPtr.Zero;
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00014DE5 File Offset: 0x00012FE5
		public void Dispose()
		{
			this.ReleaseQuery();
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00014DEF File Offset: 0x00012FEF
		internal void InvokeChanges()
		{
			Action onChanges = this.OnChanges;
			if (onChanges != null)
			{
				onChanges();
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00014E04 File Offset: 0x00013004
		private void UpdatePending()
		{
			int count = this.Count;
			bool flag = count == this.LastCount;
			if (!flag)
			{
				for (int i = this.LastCount; i < count; i++)
				{
					this.watchList.Add(i);
				}
				this.LastCount = count;
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00014E53 File Offset: 0x00013053
		public void UpdateResponsive()
		{
			this.watchList.RemoveAll(delegate(int x)
			{
				gameserveritem_t serverDetails = Base.Internal.GetServerDetails(this.request, x);
				bool hadSuccessfulResponse = serverDetails.HadSuccessfulResponse;
				bool result;
				if (hadSuccessfulResponse)
				{
					this.OnServer(ServerInfo.From(serverDetails), serverDetails.HadSuccessfulResponse);
					result = true;
				}
				else
				{
					result = false;
				}
				return result;
			});
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00014E6E File Offset: 0x0001306E
		private void MovePendingToUnresponsive()
		{
			this.watchList.RemoveAll(delegate(int x)
			{
				gameserveritem_t serverDetails = Base.Internal.GetServerDetails(this.request, x);
				this.OnServer(ServerInfo.From(serverDetails), serverDetails.HadSuccessfulResponse);
				return true;
			});
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00014E8C File Offset: 0x0001308C
		private void OnServer(ServerInfo serverInfo, bool responded)
		{
			if (responded)
			{
				this.Responsive.Add(serverInfo);
				Action<ServerInfo> onResponsiveServer = this.OnResponsiveServer;
				if (onResponsiveServer != null)
				{
					onResponsiveServer(serverInfo);
				}
			}
			else
			{
				this.Unresponsive.Add(serverInfo);
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00014ED0 File Offset: 0x000130D0
		[CompilerGenerated]
		private bool <UpdateResponsive>b__33_0(int x)
		{
			gameserveritem_t serverDetails = Base.Internal.GetServerDetails(this.request, x);
			bool hadSuccessfulResponse = serverDetails.HadSuccessfulResponse;
			bool result;
			if (hadSuccessfulResponse)
			{
				this.OnServer(ServerInfo.From(serverDetails), serverDetails.HadSuccessfulResponse);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00014F18 File Offset: 0x00013118
		[CompilerGenerated]
		private bool <MovePendingToUnresponsive>b__34_0(int x)
		{
			gameserveritem_t serverDetails = Base.Internal.GetServerDetails(this.request, x);
			this.OnServer(ServerInfo.From(serverDetails), serverDetails.HadSuccessfulResponse);
			return true;
		}

		// Token: 0x040007CB RID: 1995
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private AppId <AppId>k__BackingField;

		// Token: 0x040007CC RID: 1996
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action OnChanges;

		// Token: 0x040007CD RID: 1997
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<ServerInfo> OnResponsiveServer;

		// Token: 0x040007CE RID: 1998
		public List<ServerInfo> Responsive = new List<ServerInfo>();

		// Token: 0x040007CF RID: 1999
		public List<ServerInfo> Unresponsive = new List<ServerInfo>();

		// Token: 0x040007D0 RID: 2000
		internal HServerListRequest request;

		// Token: 0x040007D1 RID: 2001
		internal List<MatchMakingKeyValuePair> filters = new List<MatchMakingKeyValuePair>();

		// Token: 0x040007D2 RID: 2002
		internal List<int> watchList = new List<int>();

		// Token: 0x040007D3 RID: 2003
		internal int LastCount = 0;

		// Token: 0x0200027E RID: 638
		[CompilerGenerated]
		private sealed class <RunQueryAsync>d__15 : IAsyncStateMachine
		{
			// Token: 0x06001233 RID: 4659 RVA: 0x000238DB File Offset: 0x00021ADB
			public <RunQueryAsync>d__15()
			{
			}

			// Token: 0x06001234 RID: 4660 RVA: 0x000238E4 File Offset: 0x00021AE4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result;
				try
				{
					if (num != 0)
					{
						stopwatch = Stopwatch.StartNew();
						base.Reset();
						this.LaunchQuery();
						thisRequest = this.request;
						goto IL_18E;
					}
					TaskAwaiter taskAwaiter2;
					TaskAwaiter taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter);
					num2 = -1;
					IL_A2:
					taskAwaiter.GetResult();
					bool flag = this.request.Value == IntPtr.Zero || thisRequest.Value != this.request.Value;
					if (flag)
					{
						result = false;
						goto IL_1D8;
					}
					bool flag2 = !SteamClient.IsValid;
					if (flag2)
					{
						result = false;
						goto IL_1D8;
					}
					r = this.Responsive.Count;
					base.UpdatePending();
					base.UpdateResponsive();
					bool flag3 = r != this.Responsive.Count;
					if (flag3)
					{
						base.InvokeChanges();
					}
					bool flag4 = stopwatch.Elapsed.TotalSeconds > (double)timeoutSeconds;
					if (flag4)
					{
						goto IL_1A2;
					}
					IL_18E:
					if (base.IsRefreshing)
					{
						taskAwaiter = Task.Delay(33).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num2 = 0;
							taskAwaiter2 = taskAwaiter;
							Base.<RunQueryAsync>d__15 <RunQueryAsync>d__ = this;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Base.<RunQueryAsync>d__15>(ref taskAwaiter, ref <RunQueryAsync>d__);
							return;
						}
						goto IL_A2;
					}
					IL_1A2:
					base.MovePendingToUnresponsive();
					base.InvokeChanges();
					result = true;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1D8:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001235 RID: 4661 RVA: 0x00023AFC File Offset: 0x00021CFC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000EE2 RID: 3810
			public int <>1__state;

			// Token: 0x04000EE3 RID: 3811
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000EE4 RID: 3812
			public float timeoutSeconds;

			// Token: 0x04000EE5 RID: 3813
			public Base <>4__this;

			// Token: 0x04000EE6 RID: 3814
			private Stopwatch <stopwatch>5__1;

			// Token: 0x04000EE7 RID: 3815
			private HServerListRequest <thisRequest>5__2;

			// Token: 0x04000EE8 RID: 3816
			private int <r>5__3;

			// Token: 0x04000EE9 RID: 3817
			private TaskAwaiter <>u__1;
		}
	}
}
