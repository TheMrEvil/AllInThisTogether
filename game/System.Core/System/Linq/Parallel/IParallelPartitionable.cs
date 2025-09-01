using System;

namespace System.Linq.Parallel
{
	// Token: 0x020000F5 RID: 245
	internal interface IParallelPartitionable<T>
	{
		// Token: 0x06000878 RID: 2168
		QueryOperatorEnumerator<T, int>[] GetPartitions(int partitionCount);
	}
}
