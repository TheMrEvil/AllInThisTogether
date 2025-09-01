using System;

namespace UnityEngine.XR
{
	// Token: 0x02000007 RID: 7
	[Flags]
	internal enum AvailableTrackingData
	{
		// Token: 0x04000015 RID: 21
		None = 0,
		// Token: 0x04000016 RID: 22
		PositionAvailable = 1,
		// Token: 0x04000017 RID: 23
		RotationAvailable = 2,
		// Token: 0x04000018 RID: 24
		VelocityAvailable = 4,
		// Token: 0x04000019 RID: 25
		AngularVelocityAvailable = 8,
		// Token: 0x0400001A RID: 26
		AccelerationAvailable = 16,
		// Token: 0x0400001B RID: 27
		AngularAccelerationAvailable = 32
	}
}
