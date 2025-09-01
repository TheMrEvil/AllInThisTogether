using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	// Token: 0x02000281 RID: 641
	[JobProducerType(typeof(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<>))]
	public interface IJobParallelForTransform
	{
		// Token: 0x06001BE2 RID: 7138
		void Execute(int index, TransformAccess transform);
	}
}
