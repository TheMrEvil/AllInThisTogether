using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200005E RID: 94
	[JobProducerType(typeof(ParticleSystemParallelForJobStruct<>))]
	public interface IJobParticleSystemParallelFor
	{
		// Token: 0x06000734 RID: 1844
		void Execute(ParticleSystemJobData jobData, int index);
	}
}
