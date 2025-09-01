using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000035 RID: 53
	public enum InputDeviceDriverType : ushort
	{
		// Token: 0x0400027E RID: 638
		Unknown,
		// Token: 0x0400027F RID: 639
		HID,
		// Token: 0x04000280 RID: 640
		USB,
		// Token: 0x04000281 RID: 641
		Bluetooth,
		// Token: 0x04000282 RID: 642
		[InspectorName("XInput")]
		XInput,
		// Token: 0x04000283 RID: 643
		[InspectorName("DirectInput")]
		DirectInput,
		// Token: 0x04000284 RID: 644
		[InspectorName("RawInput")]
		RawInput,
		// Token: 0x04000285 RID: 645
		[InspectorName("AppleGameController")]
		AppleGameController,
		// Token: 0x04000286 RID: 646
		[InspectorName("SDLJoystick")]
		SDLJoystick,
		// Token: 0x04000287 RID: 647
		[InspectorName("SDLController")]
		SDLController
	}
}
