using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200002A RID: 42
	internal class SignalGate
	{
		// Token: 0x0600013D RID: 317 RVA: 0x000058D4 File Offset: 0x00003AD4
		public SignalGate()
		{
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000058DC File Offset: 0x00003ADC
		internal bool IsLocked
		{
			get
			{
				return this.state == 0;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000058E7 File Offset: 0x00003AE7
		internal bool IsSignalled
		{
			get
			{
				return this.state == 3;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000058F4 File Offset: 0x00003AF4
		public bool Signal()
		{
			int num = this.state;
			if (num == 0)
			{
				num = Interlocked.CompareExchange(ref this.state, 1, 0);
			}
			if (num == 2)
			{
				this.state = 3;
				return true;
			}
			if (num != 0)
			{
				this.ThrowInvalidSignalGateState();
			}
			return false;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005930 File Offset: 0x00003B30
		public bool Unlock()
		{
			int num = this.state;
			if (num == 0)
			{
				num = Interlocked.CompareExchange(ref this.state, 2, 0);
			}
			if (num == 1)
			{
				this.state = 3;
				return true;
			}
			if (num != 0)
			{
				this.ThrowInvalidSignalGateState();
			}
			return false;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000596C File Offset: 0x00003B6C
		private void ThrowInvalidSignalGateState()
		{
			throw Fx.Exception.AsError(new InvalidOperationException("Invalid Semaphore Exit"));
		}

		// Token: 0x040000CA RID: 202
		private int state;

		// Token: 0x02000080 RID: 128
		private static class GateState
		{
			// Token: 0x040002B7 RID: 695
			public const int Locked = 0;

			// Token: 0x040002B8 RID: 696
			public const int SignalPending = 1;

			// Token: 0x040002B9 RID: 697
			public const int Unlocked = 2;

			// Token: 0x040002BA RID: 698
			public const int Signalled = 3;
		}
	}
}
