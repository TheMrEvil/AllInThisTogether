using System;

namespace UnityEngine.XR
{
	// Token: 0x0200000E RID: 14
	[Flags]
	public enum InputTrackingState : uint
	{
		// Token: 0x04000053 RID: 83
		None = 0U,
		// Token: 0x04000054 RID: 84
		Position = 1U,
		// Token: 0x04000055 RID: 85
		Rotation = 2U,
		// Token: 0x04000056 RID: 86
		Velocity = 4U,
		// Token: 0x04000057 RID: 87
		AngularVelocity = 8U,
		// Token: 0x04000058 RID: 88
		Acceleration = 16U,
		// Token: 0x04000059 RID: 89
		AngularAcceleration = 32U,
		// Token: 0x0400005A RID: 90
		All = 63U
	}
}
