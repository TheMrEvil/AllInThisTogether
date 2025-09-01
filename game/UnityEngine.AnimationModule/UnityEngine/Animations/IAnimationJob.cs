using System;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000042 RID: 66
	[JobProducerType(typeof(ProcessAnimationJobStruct<>))]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationJob
	{
		// Token: 0x060002A0 RID: 672
		void ProcessAnimation(AnimationStream stream);

		// Token: 0x060002A1 RID: 673
		void ProcessRootMotion(AnimationStream stream);
	}
}
