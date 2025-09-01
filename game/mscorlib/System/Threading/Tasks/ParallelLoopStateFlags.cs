using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200032B RID: 811
	internal class ParallelLoopStateFlags
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0007BAF0 File Offset: 0x00079CF0
		internal int LoopStateFlags
		{
			get
			{
				return this._loopStateFlags;
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x0007BAFC File Offset: 0x00079CFC
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates)
		{
			int num = 0;
			return this.AtomicLoopStateUpdate(newState, illegalStates, ref num);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0007BB18 File Offset: 0x00079D18
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates, ref int oldState)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldState = this._loopStateFlags;
				if ((oldState & illegalStates) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this._loopStateFlags, oldState | newState, oldState) == oldState)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0007BB5E File Offset: 0x00079D5E
		internal void SetExceptional()
		{
			this.AtomicLoopStateUpdate(1, 0);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x0007BB69 File Offset: 0x00079D69
		internal void Stop()
		{
			if (!this.AtomicLoopStateUpdate(4, 2))
			{
				throw new InvalidOperationException("Stop was called after Break was called.");
			}
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x0007BB80 File Offset: 0x00079D80
		internal bool Cancel()
		{
			return this.AtomicLoopStateUpdate(8, 0);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0000259F File Offset: 0x0000079F
		public ParallelLoopStateFlags()
		{
		}

		// Token: 0x04001C38 RID: 7224
		internal const int ParallelLoopStateNone = 0;

		// Token: 0x04001C39 RID: 7225
		internal const int ParallelLoopStateExceptional = 1;

		// Token: 0x04001C3A RID: 7226
		internal const int ParallelLoopStateBroken = 2;

		// Token: 0x04001C3B RID: 7227
		internal const int ParallelLoopStateStopped = 4;

		// Token: 0x04001C3C RID: 7228
		internal const int ParallelLoopStateCanceled = 8;

		// Token: 0x04001C3D RID: 7229
		private volatile int _loopStateFlags;
	}
}
