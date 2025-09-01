using System;
using UnityEngine.Playables;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000043 RID: 67
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationJobPlayable : IPlayable
	{
		// Token: 0x060002A2 RID: 674
		T GetJobData<T>() where T : struct, IAnimationJob;

		// Token: 0x060002A3 RID: 675
		void SetJobData<T>(T jobData) where T : struct, IAnimationJob;
	}
}
