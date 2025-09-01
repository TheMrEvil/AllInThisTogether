using System;

namespace UnityEngine.XR
{
	// Token: 0x0200000D RID: 13
	[Flags]
	public enum InputDeviceCharacteristics : uint
	{
		// Token: 0x04000046 RID: 70
		None = 0U,
		// Token: 0x04000047 RID: 71
		HeadMounted = 1U,
		// Token: 0x04000048 RID: 72
		Camera = 2U,
		// Token: 0x04000049 RID: 73
		HeldInHand = 4U,
		// Token: 0x0400004A RID: 74
		HandTracking = 8U,
		// Token: 0x0400004B RID: 75
		EyeTracking = 16U,
		// Token: 0x0400004C RID: 76
		TrackedDevice = 32U,
		// Token: 0x0400004D RID: 77
		Controller = 64U,
		// Token: 0x0400004E RID: 78
		TrackingReference = 128U,
		// Token: 0x0400004F RID: 79
		Left = 256U,
		// Token: 0x04000050 RID: 80
		Right = 512U,
		// Token: 0x04000051 RID: 81
		Simulated6DOF = 1024U
	}
}
