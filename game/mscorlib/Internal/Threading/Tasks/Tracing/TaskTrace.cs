using System;
using Internal.Runtime.Augments;

namespace Internal.Threading.Tasks.Tracing
{
	// Token: 0x020000BD RID: 189
	internal static class TaskTrace
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001772C File Offset: 0x0001592C
		public static bool Enabled
		{
			get
			{
				TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
				return taskTraceCallbacks != null && taskTraceCallbacks.Enabled;
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001774F File Offset: 0x0001594F
		public static void Initialize(TaskTraceCallbacks callbacks)
		{
			TaskTrace.s_callbacks = callbacks;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00017758 File Offset: 0x00015958
		public static void TaskWaitBegin_Asynchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskWaitBegin_Asynchronous(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00017778 File Offset: 0x00015978
		public static void TaskWaitBegin_Synchronous(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskWaitBegin_Synchronous(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00017798 File Offset: 0x00015998
		public static void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskWaitEnd(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000177B8 File Offset: 0x000159B8
		public static void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskScheduled(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID, CreatingTaskID, TaskCreationOptions);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000177DC File Offset: 0x000159DC
		public static void TaskStarted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskStarted(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000177FC File Offset: 0x000159FC
		public static void TaskCompleted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, bool IsExceptional)
		{
			TaskTraceCallbacks taskTraceCallbacks = TaskTrace.s_callbacks;
			if (taskTraceCallbacks == null)
			{
				return;
			}
			taskTraceCallbacks.TaskCompleted(OriginatingTaskSchedulerID, OriginatingTaskID, TaskID, IsExceptional);
		}

		// Token: 0x04000FB7 RID: 4023
		private static TaskTraceCallbacks s_callbacks;
	}
}
