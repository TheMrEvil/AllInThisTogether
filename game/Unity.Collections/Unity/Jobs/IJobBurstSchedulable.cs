using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000006 RID: 6
	[JobProducerType(typeof(IJobBurstSchedulableExtensions.JobBurstSchedulableProducer<>))]
	public interface IJobBurstSchedulable
	{
		// Token: 0x0600000B RID: 11
		void Execute();
	}
}
