using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000057 RID: 87
	[JobProducerType(typeof(IJobExtensions.JobStruct<>))]
	public interface IJob
	{
		// Token: 0x06000159 RID: 345
		void Execute();
	}
}
