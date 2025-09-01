using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200015D RID: 349
	[Preserve]
	[NativeInputDeviceProfile]
	public class LogitechF310ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000785 RID: 1925 RVA: 0x0003A42C File Offset: 0x0003862C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech F310 Controller";
			base.DeviceNotes = "Logitech F310 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1133,
					ProductID = 49693
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1133,
					ProductID = 49686
				}
			};
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0003A4DF File Offset: 0x000386DF
		public LogitechF310ControllerMacNativeProfile()
		{
		}
	}
}
