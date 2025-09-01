using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000317 RID: 791
	internal static class TaskAwaiters
	{
		// Token: 0x060021C4 RID: 8644 RVA: 0x00079175 File Offset: 0x00077375
		public static ForceAsyncAwaiter ForceAsync(this Task task)
		{
			return new ForceAsyncAwaiter(task);
		}
	}
}
