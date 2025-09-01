using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200015B RID: 347
	[Preserve]
	[NativeInputDeviceProfile]
	public class LogitechControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x0003A334 File Offset: 0x00038534
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech Controller";
			base.DeviceNotes = "Logitech Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1133,
					ProductID = 62209
				}
			};
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0003A3A8 File Offset: 0x000385A8
		public LogitechControllerMacNativeProfile()
		{
		}
	}
}
