using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200035F RID: 863
	internal sealed class CompletionActionInvoker : IThreadPoolWorkItem
	{
		// Token: 0x0600245A RID: 9306 RVA: 0x000824AA File Offset: 0x000806AA
		internal CompletionActionInvoker(ITaskCompletionAction action, Task completingTask)
		{
			this.m_action = action;
			this.m_completingTask = completingTask;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000824C0 File Offset: 0x000806C0
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.m_action.Invoke(this.m_completingTask);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void MarkAborted(ThreadAbortException e)
		{
		}

		// Token: 0x04001D02 RID: 7426
		private readonly ITaskCompletionAction m_action;

		// Token: 0x04001D03 RID: 7427
		private readonly Task m_completingTask;
	}
}
