using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000369 RID: 873
	internal sealed class ContinuationTaskFromTask : Task
	{
		// Token: 0x06002474 RID: 9332 RVA: 0x00082850 File Offset: 0x00080A50
		public ContinuationTaskFromTask(Task antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions) : base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x00082884 File Offset: 0x00080A84
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task> action = this.m_action as Action<Task>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task, object> action2 = this.m_action as Action<Task, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001D2E RID: 7470
		private Task m_antecedent;
	}
}
