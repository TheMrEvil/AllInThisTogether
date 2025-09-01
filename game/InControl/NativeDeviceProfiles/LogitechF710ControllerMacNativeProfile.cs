using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200015F RID: 351
	[Preserve]
	[NativeInputDeviceProfile]
	public class LogitechF710ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x0003A564 File Offset: 0x00038764
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech F710 Controller";
			base.DeviceNotes = "Logitech F710 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1133,
					ProductID = 49695
				}
			};
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0003A5D8 File Offset: 0x000387D8
		public LogitechF710ControllerMacNativeProfile()
		{
		}
	}
}
