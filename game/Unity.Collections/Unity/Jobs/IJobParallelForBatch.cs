using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200000A RID: 10
	[JobProducerType(typeof(IJobParallelForBatchExtensions.JobParallelForBatchProducer<>))]
	public interface IJobParallelForBatch
	{
		// Token: 0x06000017 RID: 23
		void Execute(int startIndex, int count);
	}
}
