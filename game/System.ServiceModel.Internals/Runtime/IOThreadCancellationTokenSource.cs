using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200001F RID: 31
	internal class IOThreadCancellationTokenSource : IDisposable
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000040E0 File Offset: 0x000022E0
		public IOThreadCancellationTokenSource(TimeSpan timeout)
		{
			TimeoutHelper.ThrowIfNegativeArgument(timeout);
			this.timeout = timeout;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000040F5 File Offset: 0x000022F5
		public IOThreadCancellationTokenSource(int timeout) : this(TimeSpan.FromMilliseconds((double)timeout))
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004104 File Offset: 0x00002304
		public CancellationToken Token
		{
			get
			{
				if (this.token == null)
				{
					if (this.timeout >= TimeoutHelper.MaxWait)
					{
						this.token = new CancellationToken?(CancellationToken.None);
					}
					else
					{
						this.timer = new IOThreadTimer(IOThreadCancellationTokenSource.onCancel, this, true);
						this.source = new CancellationTokenSource();
						this.timer.Set(this.timeout);
						this.token = new CancellationToken?(this.source.Token);
					}
				}
				return this.token.Value;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004191 File Offset: 0x00002391
		public void Dispose()
		{
			if (this.source != null && this.timer.Cancel())
			{
				this.source.Dispose();
				this.source = null;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000041BA File Offset: 0x000023BA
		private static void OnCancel(object obj)
		{
			((IOThreadCancellationTokenSource)obj).Cancel();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000041C7 File Offset: 0x000023C7
		private void Cancel()
		{
			this.source.Cancel();
			this.source.Dispose();
			this.source = null;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000041E6 File Offset: 0x000023E6
		// Note: this type is marked as 'beforefieldinit'.
		static IOThreadCancellationTokenSource()
		{
		}

		// Token: 0x0400009D RID: 157
		private static readonly Action<object> onCancel = Fx.ThunkCallback<object>(new Action<object>(IOThreadCancellationTokenSource.OnCancel));

		// Token: 0x0400009E RID: 158
		private readonly TimeSpan timeout;

		// Token: 0x0400009F RID: 159
		private CancellationTokenSource source;

		// Token: 0x040000A0 RID: 160
		private CancellationToken? token;

		// Token: 0x040000A1 RID: 161
		private IOThreadTimer timer;
	}
}
