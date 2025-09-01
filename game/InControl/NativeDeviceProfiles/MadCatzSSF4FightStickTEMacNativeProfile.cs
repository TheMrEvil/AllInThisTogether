using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200017C RID: 380
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzSSF4FightStickTEMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x0003B470 File Offset: 0x00039670
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz SSF4 Fight Stick TE";
			base.DeviceNotes = "Mad Catz SSF4 Fight Stick TE on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 63288
				}
			};
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0003B4E4 File Offset: 0x000396E4
		public MadCatzSSF4FightStickTEMacNativeProfile()
		{
		}
	}
}
