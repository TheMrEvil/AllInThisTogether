using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000016 RID: 22
	[JobProducerType(typeof(JobParallelIndexListExtensions.JobParallelForFilterProducer<>))]
	public interface IJobParallelForFilter
	{
		// Token: 0x0600003E RID: 62
		bool Execute(int index);
	}
}
