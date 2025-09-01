using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200005F RID: 95
	[JobProducerType(typeof(ParticleSystemParallelForBatchJobStruct<>))]
	public interface IJobParticleSystemParallelForBatch
	{
		// Token: 0x06000735 RID: 1845
		void Execute(ParticleSystemJobData jobData, int startIndex, int count);
	}
}
