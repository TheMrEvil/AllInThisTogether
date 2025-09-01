using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000172 RID: 370
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzMLGFightStickTEMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x0003AF98 File Offset: 0x00039198
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz MLG Fight Stick TE";
			base.DeviceNotes = "Mad Catz MLG Fight Stick TE on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61502
				}
			};
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0003B00C File Offset: 0x0003920C
		public MadCatzMLGFightStickTEMacNativeProfile()
		{
		}
	}
}
