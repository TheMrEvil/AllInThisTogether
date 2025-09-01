using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000012 RID: 18
	[JobProducerType(typeof(IJobParallelForDeferExtensions.JobParallelForDeferProducer<>))]
	public interface IJobParallelForDefer
	{
		// Token: 0x0600002F RID: 47
		void Execute(int index);
	}
}
