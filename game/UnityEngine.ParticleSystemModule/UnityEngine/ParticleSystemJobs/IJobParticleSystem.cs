using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200005D RID: 93
	[JobProducerType(typeof(ParticleSystemJobStruct<>))]
	public interface IJobParticleSystem
	{
		// Token: 0x06000733 RID: 1843
		void Execute(ParticleSystemJobData jobData);
	}
}
