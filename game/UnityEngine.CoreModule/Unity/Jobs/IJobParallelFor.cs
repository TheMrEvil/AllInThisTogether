using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200005F RID: 95
	[JobProducerType(typeof(IJobParallelForExtensions.ParallelForJobStruct<>))]
	public interface IJobParallelFor
	{
		// Token: 0x0600016C RID: 364
		void Execute(int index);
	}
}
