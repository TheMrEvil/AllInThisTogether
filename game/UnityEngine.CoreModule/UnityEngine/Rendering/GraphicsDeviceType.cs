using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003BE RID: 958
	[UsedByNativeCode]
	public enum GraphicsDeviceType
	{
		// Token: 0x04000B4A RID: 2890
		[Obsolete("OpenGL2 is no longer supported in Unity 5.5+")]
		OpenGL2,
		// Token: 0x04000B4B RID: 2891
		[Obsolete("Direct3D 9 is no longer supported in Unity 2017.2+")]
		Direct3D9,
		// Token: 0x04000B4C RID: 2892
		Direct3D11,
		// Token: 0x04000B4D RID: 2893
		[Obsolete("PS3 is no longer supported in Unity 5.5+")]
		PlayStation3,
		// Token: 0x04000B4E RID: 2894
		Null,
		// Token: 0x04000B4F RID: 2895
		[Obsolete("Xbox360 is no longer supported in Unity 5.5+")]
		Xbox360 = 6,
		// Token: 0x04000B50 RID: 2896
		OpenGLES2 = 8,
		// Token: 0x04000B51 RID: 2897
		OpenGLES3 = 11,
		// Token: 0x04000B52 RID: 2898
		[Obsolete("PVita is no longer supported as of Unity 2018")]
		PlayStationVita,
		// Token: 0x04000B53 RID: 2899
		PlayStation4,
		// Token: 0x04000B54 RID: 2900
		XboxOne,
		// Token: 0x04000B55 RID: 2901
		[Obsolete("PlayStationMobile is no longer supported in Unity 5.3+")]
		PlayStationMobile,
		// Token: 0x04000B56 RID: 2902
		Metal,
		// Token: 0x04000B57 RID: 2903
		OpenGLCore,
		// Token: 0x04000B58 RID: 2904
		Direct3D12,
		// Token: 0x04000B59 RID: 2905
		[Obsolete("Nintendo 3DS support is unavailable since 2018.1")]
		N3DS,
		// Token: 0x04000B5A RID: 2906
		Vulkan = 21,
		// Token: 0x04000B5B RID: 2907
		Switch,
		// Token: 0x04000B5C RID: 2908
		XboxOneD3D12,
		// Token: 0x04000B5D RID: 2909
		GameCoreXboxOne,
		// Token: 0x04000B5E RID: 2910
		[Obsolete("GameCoreScarlett is deprecated, please use GameCoreXboxSeries (UnityUpgradable) -> GameCoreXboxSeries", false)]
		GameCoreScarlett = -1,
		// Token: 0x04000B5F RID: 2911
		GameCoreXboxSeries = 25,
		// Token: 0x04000B60 RID: 2912
		PlayStation5,
		// Token: 0x04000B61 RID: 2913
		PlayStation5NGGC
	}
}
