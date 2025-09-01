using System;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	public enum AnimationCullingType
	{
		// Token: 0x0400000F RID: 15
		AlwaysAnimate,
		// Token: 0x04000010 RID: 16
		BasedOnRenderers,
		// Token: 0x04000011 RID: 17
		[Obsolete("Enum member AnimatorCullingMode.BasedOnClipBounds has been deprecated. Use AnimationCullingType.AlwaysAnimate or AnimationCullingType.BasedOnRenderers instead")]
		BasedOnClipBounds,
		// Token: 0x04000012 RID: 18
		[Obsolete("Enum member AnimatorCullingMode.BasedOnUserBounds has been deprecated. Use AnimationCullingType.AlwaysAnimate or AnimationCullingType.BasedOnRenderers instead")]
		BasedOnUserBounds
	}
}
