using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200081D RID: 2077
	internal static class AsyncTaskCache
	{
		// Token: 0x06004685 RID: 18053 RVA: 0x000E6B68 File Offset: 0x000E4D68
		private static Task<int>[] CreateInt32Tasks()
		{
			Task<int>[] array = new Task<int>[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AsyncTaskCache.CreateCacheableTask<int>(i + -1);
			}
			return array;
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x000E6B98 File Offset: 0x000E4D98
		internal static Task<TResult> CreateCacheableTask<TResult>(TResult result)
		{
			return new Task<TResult>(false, result, (TaskCreationOptions)16384, default(CancellationToken));
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x000E6BBA File Offset: 0x000E4DBA
		// Note: this type is marked as 'beforefieldinit'.
		static AsyncTaskCache()
		{
		}

		// Token: 0x04002D5E RID: 11614
		internal static readonly Task<bool> TrueTask = AsyncTaskCache.CreateCacheableTask<bool>(true);

		// Token: 0x04002D5F RID: 11615
		internal static readonly Task<bool> FalseTask = AsyncTaskCache.CreateCacheableTask<bool>(false);

		// Token: 0x04002D60 RID: 11616
		internal static readonly Task<int>[] Int32Tasks = AsyncTaskCache.CreateInt32Tasks();

		// Token: 0x04002D61 RID: 11617
		internal const int INCLUSIVE_INT32_MIN = -1;

		// Token: 0x04002D62 RID: 11618
		internal const int EXCLUSIVE_INT32_MAX = 9;
	}
}
