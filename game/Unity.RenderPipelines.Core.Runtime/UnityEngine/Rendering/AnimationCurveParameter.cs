using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public class AnimationCurveParameter : VolumeParameter<AnimationCurve>
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x0001E1FC File Offset: 0x0001C3FC
		public AnimationCurveParameter(AnimationCurve value, bool overrideState = false) : base(value, overrideState)
		{
		}
	}
}
