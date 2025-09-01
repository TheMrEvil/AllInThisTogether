using System;

namespace Internal.Runtime.Augments
{
	// Token: 0x020000C4 RID: 196
	internal abstract class TaskTraceCallbacks
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060004AB RID: 1195
		public abstract bool Enabled { get; }

		// Token: 0x060004AC RID: 1196
		public abstract void TaskWaitBegin_Asynchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x060004AD RID: 1197
		public abstract void TaskWaitBegin_Synchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x060004AE RID: 1198
		public abstract void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x060004AF RID: 1199
		public abstract void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions);

		// Token: 0x060004B0 RID: 1200
		public abstract void TaskStarted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID);

		// Token: 0x060004B1 RID: 1201
		public abstract void TaskCompleted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, bool IsExceptional);

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000259F File Offset: 0x0000079F
		protected TaskTraceCallbacks()
		{
		}
	}
}
