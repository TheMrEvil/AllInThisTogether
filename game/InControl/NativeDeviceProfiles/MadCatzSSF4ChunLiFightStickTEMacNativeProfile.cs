using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200017B RID: 379
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzSSF4ChunLiFightStickTEMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x0003B3F4 File Offset: 0x000395F4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz SSF4 Chun-Li Fight Stick TE";
			base.DeviceNotes = "Mad Catz SSF4 Chun-Li Fight Stick TE on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61501
				}
			};
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0003B468 File Offset: 0x00039668
		public MadCatzSSF4ChunLiFightStickTEMacNativeProfile()
		{
		}
	}
}
