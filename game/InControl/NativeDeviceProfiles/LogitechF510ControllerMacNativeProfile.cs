using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200015E RID: 350
	[Preserve]
	[NativeInputDeviceProfile]
	public class LogitechF510ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x0003A4E8 File Offset: 0x000386E8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech F510 Controller";
			base.DeviceNotes = "Logitech F510 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1133,
					ProductID = 49694
				}
			};
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0003A55C File Offset: 0x0003875C
		public LogitechF510ControllerMacNativeProfile()
		{
		}
	}
}
