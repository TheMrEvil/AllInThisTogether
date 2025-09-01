using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200017A RID: 378
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzSoulCaliberFightStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007BF RID: 1983 RVA: 0x0003B378 File Offset: 0x00039578
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Soul Caliber Fight Stick";
			base.DeviceNotes = "Mad Catz Soul Caliber Fight Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61503
				}
			};
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0003B3EC File Offset: 0x000395EC
		public MadCatzSoulCaliberFightStickMacNativeProfile()
		{
		}
	}
}
