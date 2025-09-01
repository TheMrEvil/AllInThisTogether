using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000167 RID: 359
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzFightPadControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x0003AA04 File Offset: 0x00038C04
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz FightPad Controller";
			base.DeviceNotes = "Mad Catz FightPad Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61480
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 18216
				}
			};
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0003AAB7 File Offset: 0x00038CB7
		public MadCatzFightPadControllerMacNativeProfile()
		{
		}
	}
}
