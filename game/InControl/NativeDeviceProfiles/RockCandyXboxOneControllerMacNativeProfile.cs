using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001AB RID: 427
	[Preserve]
	[NativeInputDeviceProfile]
	public class RockCandyXboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x0003D858 File Offset: 0x0003BA58
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Rock Candy Xbox One Controller";
			base.DeviceNotes = "Rock Candy Xbox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 326
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 582
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 838
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 719
				}
			};
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0003D989 File Offset: 0x0003BB89
		public RockCandyXboxOneControllerMacNativeProfile()
		{
		}
	}
}
