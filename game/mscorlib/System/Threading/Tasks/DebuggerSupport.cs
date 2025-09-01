using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	// Token: 0x0200033E RID: 830
	internal static class DebuggerSupport
	{
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public static bool LoggingOn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void TraceOperationCreation(CausalityTraceLevel traceLevel, Task task, string operationName, ulong relatedContext)
		{
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void TraceOperationCompletion(CausalityTraceLevel traceLevel, Task task, AsyncStatus status)
		{
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void TraceOperationRelation(CausalityTraceLevel traceLevel, Task task, CausalityRelation relation)
		{
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, Task task, CausalitySynchronousWork work)
		{
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
		{
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0007CCAD File Offset: 0x0007AEAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddToActiveTasks(Task task)
		{
			if (Task.s_asyncDebuggingEnabled)
			{
				DebuggerSupport.AddToActiveTasksNonInlined(task);
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0007CCBC File Offset: 0x0007AEBC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void AddToActiveTasksNonInlined(Task task)
		{
			int id = task.Id;
			object obj = DebuggerSupport.s_activeTasksLock;
			lock (obj)
			{
				DebuggerSupport.s_activeTasks[id] = task;
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0007CD08 File Offset: 0x0007AF08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveFromActiveTasks(Task task)
		{
			if (Task.s_asyncDebuggingEnabled)
			{
				DebuggerSupport.RemoveFromActiveTasksNonInlined(task);
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0007CD18 File Offset: 0x0007AF18
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void RemoveFromActiveTasksNonInlined(Task task)
		{
			int id = task.Id;
			object obj = DebuggerSupport.s_activeTasksLock;
			lock (obj)
			{
				DebuggerSupport.s_activeTasks.Remove(id);
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0007CD64 File Offset: 0x0007AF64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task GetActiveTaskFromId(int taskId)
		{
			Task result = null;
			DebuggerSupport.s_activeTasks.TryGetValue(taskId, out result);
			return result;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0007CD82 File Offset: 0x0007AF82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task GetTaskIfDebuggingEnabled(this AsyncVoidMethodBuilder builder)
		{
			if (DebuggerSupport.LoggingOn || Task.s_asyncDebuggingEnabled)
			{
				return builder.Task;
			}
			return null;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0007CD9B File Offset: 0x0007AF9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task GetTaskIfDebuggingEnabled(this AsyncTaskMethodBuilder builder)
		{
			if (DebuggerSupport.LoggingOn || Task.s_asyncDebuggingEnabled)
			{
				return builder.Task;
			}
			return null;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x0007CDB4 File Offset: 0x0007AFB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Task GetTaskIfDebuggingEnabled<TResult>(this AsyncTaskMethodBuilder<TResult> builder)
		{
			if (DebuggerSupport.LoggingOn || Task.s_asyncDebuggingEnabled)
			{
				return builder.Task;
			}
			return null;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0007CDCD File Offset: 0x0007AFCD
		// Note: this type is marked as 'beforefieldinit'.
		static DebuggerSupport()
		{
		}

		// Token: 0x04001C81 RID: 7297
		private static readonly LowLevelDictionary<int, Task> s_activeTasks = new LowLevelDictionary<int, Task>();

		// Token: 0x04001C82 RID: 7298
		private static readonly object s_activeTasksLock = new object();
	}
}
