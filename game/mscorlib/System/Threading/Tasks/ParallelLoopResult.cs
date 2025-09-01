using System;

namespace System.Threading.Tasks
{
	/// <summary>Provides completion status on the execution of a <see cref="T:System.Threading.Tasks.Parallel" /> loop.</summary>
	// Token: 0x0200032E RID: 814
	public struct ParallelLoopResult
	{
		/// <summary>Gets whether the loop ran to completion, such that all iterations of the loop were executed and the loop didn't receive a request to end prematurely.</summary>
		/// <returns>true if the loop ran to completion; otherwise false;</returns>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x0007BD1F File Offset: 0x00079F1F
		public bool IsCompleted
		{
			get
			{
				return this._completed;
			}
		}

		/// <summary>Gets the index of the lowest iteration from which <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> was called.</summary>
		/// <returns>Returns an integer that represents the lowest iteration from which the Break statement was called.</returns>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x0007BD27 File Offset: 0x00079F27
		public long? LowestBreakIteration
		{
			get
			{
				return this._lowestBreakIteration;
			}
		}

		// Token: 0x04001C40 RID: 7232
		internal bool _completed;

		// Token: 0x04001C41 RID: 7233
		internal long? _lowestBreakIteration;
	}
}
