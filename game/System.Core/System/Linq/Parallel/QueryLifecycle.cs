using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001D8 RID: 472
	internal static class QueryLifecycle
	{
		// Token: 0x06000BC5 RID: 3013 RVA: 0x000298FC File Offset: 0x00027AFC
		internal static void LogicalQueryExecutionBegin(int queryID)
		{
			PlinqEtwProvider.Log.ParallelQueryBegin(queryID);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00029909 File Offset: 0x00027B09
		internal static void LogicalQueryExecutionEnd(int queryID)
		{
			PlinqEtwProvider.Log.ParallelQueryEnd(queryID);
		}
	}
}
