using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000168 RID: 360
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzFightPadMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0003AAC0 File Offset: 0x00038CC0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz FightPad";
			base.DeviceNotes = "Mad Catz FightPad on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61486
				}
			};
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0003AB34 File Offset: 0x00038D34
		public MadCatzFightPadMacNativeProfile()
		{
		}
	}
}
