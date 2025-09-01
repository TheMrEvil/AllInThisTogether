using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000385 RID: 901
	[FriendAccessAllowed]
	internal static class AsyncCausalityTracer
	{
		// Token: 0x0600254B RID: 9547 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal static void EnableToETW(bool enabled)
		{
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[FriendAccessAllowed]
		internal static bool LoggingOn
		{
			[FriendAccessAllowed]
			get
			{
				return false;
			}
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCreation(CausalityTraceLevel traceLevel, int taskId, string operationName, ulong relatedContext)
		{
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCompletion(CausalityTraceLevel traceLevel, int taskId, AsyncCausalityStatus status)
		{
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationRelation(CausalityTraceLevel traceLevel, int taskId, CausalityRelation relation)
		{
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, int taskId, CausalitySynchronousWork work)
		{
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
		{
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x0008475E File Offset: 0x0008295E
		private static ulong GetOperationId(uint taskId)
		{
			return (ulong)(((long)AppDomain.CurrentDomain.Id << 32) + (long)((ulong)taskId));
		}
	}
}
