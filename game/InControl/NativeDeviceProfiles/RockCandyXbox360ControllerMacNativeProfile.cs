using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001AA RID: 426
	[Preserve]
	[NativeInputDeviceProfile]
	public class RockCandyXbox360ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x0003D75C File Offset: 0x0003B95C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Rock Candy Xbox 360 Controller";
			base.DeviceNotes = "Rock Candy Xbox 360 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 543
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 64254
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 338
				}
			};
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0003D84E File Offset: 0x0003BA4E
		public RockCandyXbox360ControllerMacNativeProfile()
		{
		}
	}
}
