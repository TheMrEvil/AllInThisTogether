using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200037B RID: 891
	internal sealed class SynchronizationContextTaskScheduler : TaskScheduler
	{
		// Token: 0x06002529 RID: 9513 RVA: 0x00084408 File Offset: 0x00082608
		internal SynchronizationContextTaskScheduler()
		{
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext == null)
			{
				throw new InvalidOperationException("The current SynchronizationContext may not be used as a TaskScheduler.");
			}
			this.m_synchronizationContext = synchronizationContext;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00084436 File Offset: 0x00082636
		protected internal override void QueueTask(Task task)
		{
			this.m_synchronizationContext.Post(SynchronizationContextTaskScheduler.s_postCallback, task);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00084449 File Offset: 0x00082649
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return SynchronizationContext.Current == this.m_synchronizationContext && base.TryExecuteTask(task);
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x0000AF5E File Offset: 0x0000915E
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return null;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x000040F7 File Offset: 0x000022F7
		public override int MaximumConcurrencyLevel
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00084461 File Offset: 0x00082661
		// Note: this type is marked as 'beforefieldinit'.
		static SynchronizationContextTaskScheduler()
		{
		}

		// Token: 0x04001D54 RID: 7508
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x04001D55 RID: 7509
		private static readonly SendOrPostCallback s_postCallback = delegate(object s)
		{
			((Task)s).ExecuteEntry(true);
		};

		// Token: 0x0200037C RID: 892
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600252F RID: 9519 RVA: 0x00084478 File Offset: 0x00082678
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002530 RID: 9520 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06002531 RID: 9521 RVA: 0x00084484 File Offset: 0x00082684
			internal void <.cctor>b__8_0(object s)
			{
				((Task)s).ExecuteEntry(true);
			}

			// Token: 0x04001D56 RID: 7510
			public static readonly SynchronizationContextTaskScheduler.<>c <>9 = new SynchronizationContextTaskScheduler.<>c();
		}
	}
}
