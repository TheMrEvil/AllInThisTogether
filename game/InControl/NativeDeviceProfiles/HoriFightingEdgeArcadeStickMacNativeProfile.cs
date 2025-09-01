using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000143 RID: 323
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriFightingEdgeArcadeStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00039664 File Offset: 0x00037864
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Fighting Edge Arcade Stick";
			base.DeviceNotes = "Hori Fighting Edge Arcade Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21763
				}
			};
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000396D8 File Offset: 0x000378D8
		public HoriFightingEdgeArcadeStickMacNativeProfile()
		{
		}
	}
}
