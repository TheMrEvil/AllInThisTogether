using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200000E RID: 14
	[JobProducerType(typeof(IJobParallelForExtensionsBurstSchedulable.JobParallelForBurstSchedulableProducer<>))]
	public interface IJobParallelForBurstSchedulable
	{
		// Token: 0x06000023 RID: 35
		void Execute(int index);
	}
}
