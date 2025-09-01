using System;
using System.Diagnostics;
using Unity;

namespace System.Threading.Tasks
{
	/// <summary>Enables iterations of parallel loops to interact with other iterations. An instance of this class is provided by the <see cref="T:System.Threading.Tasks.Parallel" /> class to each loop; you can not create instances in your code.</summary>
	// Token: 0x02000328 RID: 808
	[DebuggerDisplay("ShouldExitCurrentIteration = {ShouldExitCurrentIteration}")]
	public class ParallelLoopState
	{
		// Token: 0x06002237 RID: 8759 RVA: 0x0007B918 File Offset: 0x00079B18
		internal ParallelLoopState(ParallelLoopStateFlags fbase)
		{
			this._flagsBase = fbase;
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x0007B927 File Offset: 0x00079B27
		internal virtual bool InternalShouldExitCurrentIteration
		{
			get
			{
				throw new NotSupportedException("This method is not supported.");
			}
		}

		/// <summary>Gets whether the current iteration of the loop should exit based on requests made by this or other iterations.</summary>
		/// <returns>
		///   <see langword="true" /> if the current iteration should exit; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x0007B933 File Offset: 0x00079B33
		public bool ShouldExitCurrentIteration
		{
			get
			{
				return this.InternalShouldExitCurrentIteration;
			}
		}

		/// <summary>Gets whether any iteration of the loop has called the <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if any iteration has stopped the loop by calling the <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x0007B93B File Offset: 0x00079B3B
		public bool IsStopped
		{
			get
			{
				return (this._flagsBase.LoopStateFlags & 4) != 0;
			}
		}

		/// <summary>Gets whether any iteration of the loop has thrown an exception that went unhandled by that iteration.</summary>
		/// <returns>
		///   <see langword="true" /> if an unhandled exception was thrown; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0007B94D File Offset: 0x00079B4D
		public bool IsExceptional
		{
			get
			{
				return (this._flagsBase.LoopStateFlags & 1) != 0;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x0007B927 File Offset: 0x00079B27
		internal virtual long? InternalLowestBreakIteration
		{
			get
			{
				throw new NotSupportedException("This method is not supported.");
			}
		}

		/// <summary>Gets the lowest iteration of the loop from which <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> was called.</summary>
		/// <returns>The lowest iteration from which <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> was called. In the case of a <see cref="M:System.Threading.Tasks.Parallel.ForEach``1(System.Collections.Concurrent.Partitioner{``0},System.Action{``0})" /> loop, the value is based on an internally-generated index.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0007B95F File Offset: 0x00079B5F
		public long? LowestBreakIteration
		{
			get
			{
				return this.InternalLowestBreakIteration;
			}
		}

		/// <summary>Communicates that the <see cref="T:System.Threading.Tasks.Parallel" /> loop should cease execution at the system's earliest convenience.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> method was called previously. <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> and <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> may not be used in combination by iterations of the same loop.</exception>
		// Token: 0x0600223E RID: 8766 RVA: 0x0007B967 File Offset: 0x00079B67
		public void Stop()
		{
			this._flagsBase.Stop();
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0007B927 File Offset: 0x00079B27
		internal virtual void InternalBreak()
		{
			throw new NotSupportedException("This method is not supported.");
		}

		/// <summary>Communicates that the <see cref="T:System.Threading.Tasks.Parallel" /> loop should cease execution of iterations beyond the current iteration at the system's earliest convenience.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> method was previously called. <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> and <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> may not be used in combination by iterations of the same loop.</exception>
		// Token: 0x06002240 RID: 8768 RVA: 0x0007B974 File Offset: 0x00079B74
		public void Break()
		{
			this.InternalBreak();
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0007B97C File Offset: 0x00079B7C
		internal static void Break(int iteration, ParallelLoopStateFlags32 pflags)
		{
			int num = 0;
			if (pflags.AtomicLoopStateUpdate(2, 13, ref num))
			{
				int lowestBreakIteration = pflags._lowestBreakIteration;
				if (iteration < lowestBreakIteration)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags._lowestBreakIteration, iteration, lowestBreakIteration) != lowestBreakIteration)
					{
						spinWait.SpinOnce();
						lowestBreakIteration = pflags._lowestBreakIteration;
						if (iteration > lowestBreakIteration)
						{
							break;
						}
					}
				}
				return;
			}
			if ((num & 4) != 0)
			{
				throw new InvalidOperationException("Break was called after Stop was called.");
			}
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0007B9E4 File Offset: 0x00079BE4
		internal static void Break(long iteration, ParallelLoopStateFlags64 pflags)
		{
			int num = 0;
			if (pflags.AtomicLoopStateUpdate(2, 13, ref num))
			{
				long lowestBreakIteration = pflags.LowestBreakIteration;
				if (iteration < lowestBreakIteration)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags._lowestBreakIteration, iteration, lowestBreakIteration) != lowestBreakIteration)
					{
						spinWait.SpinOnce();
						lowestBreakIteration = pflags.LowestBreakIteration;
						if (iteration > lowestBreakIteration)
						{
							break;
						}
					}
				}
				return;
			}
			if ((num & 4) != 0)
			{
				throw new InvalidOperationException("Break was called after Stop was called.");
			}
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000173AD File Offset: 0x000155AD
		internal ParallelLoopState()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001C33 RID: 7219
		private readonly ParallelLoopStateFlags _flagsBase;
	}
}
