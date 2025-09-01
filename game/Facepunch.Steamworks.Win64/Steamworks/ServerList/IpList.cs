using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks.ServerList
{
	// Token: 0x020000D0 RID: 208
	public class IpList : Internet
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x00015070 File Offset: 0x00013270
		public IpList(IEnumerable<string> list)
		{
			this.Ips.AddRange(list);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00015092 File Offset: 0x00013292
		public IpList(params string[] list)
		{
			this.Ips.AddRange(list);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000150B4 File Offset: 0x000132B4
		public override async Task<bool> RunQueryAsync(float timeoutSeconds = 10f)
		{
			int blockSize = 16;
			int pointer = 0;
			string[] ips = this.Ips.ToArray();
			for (;;)
			{
				IEnumerable<string> sublist = ips.Skip(pointer).Take(blockSize);
				bool flag = sublist.Count<string>() == 0;
				if (flag)
				{
					break;
				}
				using (Internet list = new Internet())
				{
					list.AddFilter("or", sublist.Count<string>().ToString());
					foreach (string server in sublist)
					{
						list.AddFilter("gameaddr", server);
						server = null;
					}
					IEnumerator<string> enumerator = null;
					await list.RunQueryAsync(timeoutSeconds);
					if (this.wantsCancel)
					{
						return false;
					}
					this.Responsive.AddRange(list.Responsive);
					this.Responsive = this.Responsive.Distinct<ServerInfo>().ToList<ServerInfo>();
					this.Unresponsive.AddRange(list.Unresponsive);
					this.Unresponsive = this.Unresponsive.Distinct<ServerInfo>().ToList<ServerInfo>();
				}
				Internet list = null;
				pointer += sublist.Count<string>();
				base.InvokeChanges();
				sublist = null;
			}
			return true;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00015102 File Offset: 0x00013302
		public override void Cancel()
		{
			this.wantsCancel = true;
		}

		// Token: 0x040007D4 RID: 2004
		public List<string> Ips = new List<string>();

		// Token: 0x040007D5 RID: 2005
		private bool wantsCancel;

		// Token: 0x0200027F RID: 639
		[CompilerGenerated]
		private sealed class <RunQueryAsync>d__4 : IAsyncStateMachine
		{
			// Token: 0x06001236 RID: 4662 RVA: 0x00023AFE File Offset: 0x00021CFE
			public <RunQueryAsync>d__4()
			{
			}

			// Token: 0x06001237 RID: 4663 RVA: 0x00023B08 File Offset: 0x00021D08
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				bool result;
				try
				{
					if (num != 0)
					{
						blockSize = 16;
						pointer = 0;
						ips = this.Ips.ToArray();
						goto IL_270;
					}
					IL_7E:
					try
					{
						TaskAwaiter<bool> taskAwaiter;
						if (num != 0)
						{
							list.AddFilter("or", sublist.Count<string>().ToString());
							enumerator = sublist.GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									server = enumerator.Current;
									list.AddFilter("gameaddr", server);
									server = null;
								}
							}
							finally
							{
								if (num < 0 && enumerator != null)
								{
									enumerator.Dispose();
								}
							}
							enumerator = null;
							taskAwaiter = list.RunQueryAsync(timeoutSeconds).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								TaskAwaiter<bool> taskAwaiter2 = taskAwaiter;
								IpList.<RunQueryAsync>d__4 <RunQueryAsync>d__ = this;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, IpList.<RunQueryAsync>d__4>(ref taskAwaiter, ref <RunQueryAsync>d__);
								return;
							}
						}
						else
						{
							TaskAwaiter<bool> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<bool>);
							num = (num2 = -1);
						}
						taskAwaiter.GetResult();
						bool wantsCancel = this.wantsCancel;
						if (wantsCancel)
						{
							result = false;
							goto IL_296;
						}
						this.Responsive.AddRange(list.Responsive);
						this.Responsive = this.Responsive.Distinct<ServerInfo>().ToList<ServerInfo>();
						this.Unresponsive.AddRange(list.Unresponsive);
						this.Unresponsive = this.Unresponsive.Distinct<ServerInfo>().ToList<ServerInfo>();
					}
					finally
					{
						if (num < 0 && list != null)
						{
							((IDisposable)list).Dispose();
						}
					}
					list = null;
					pointer += sublist.Count<string>();
					base.InvokeChanges();
					sublist = null;
					IL_270:
					sublist = ips.Skip(pointer).Take(blockSize);
					bool flag = sublist.Count<string>() == 0;
					if (!flag)
					{
						list = new Internet();
						goto IL_7E;
					}
					result = true;
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_296:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001238 RID: 4664 RVA: 0x00023E0C File Offset: 0x0002200C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000EEA RID: 3818
			public int <>1__state;

			// Token: 0x04000EEB RID: 3819
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000EEC RID: 3820
			public float timeoutSeconds;

			// Token: 0x04000EED RID: 3821
			public IpList <>4__this;

			// Token: 0x04000EEE RID: 3822
			private int <blockSize>5__1;

			// Token: 0x04000EEF RID: 3823
			private int <pointer>5__2;

			// Token: 0x04000EF0 RID: 3824
			private string[] <ips>5__3;

			// Token: 0x04000EF1 RID: 3825
			private IEnumerable<string> <sublist>5__4;

			// Token: 0x04000EF2 RID: 3826
			private Internet <list>5__5;

			// Token: 0x04000EF3 RID: 3827
			private IEnumerator<string> <>s__6;

			// Token: 0x04000EF4 RID: 3828
			private string <server>5__7;

			// Token: 0x04000EF5 RID: 3829
			private TaskAwaiter<bool> <>u__1;
		}
	}
}
