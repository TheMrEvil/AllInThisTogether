using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200015A RID: 346
	[Preserve]
	[NativeInputDeviceProfile]
	public class LogitechChillStreamControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0003A2B8 File Offset: 0x000384B8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech Chill Stream Controller";
			base.DeviceNotes = "Logitech Chill Stream Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1133,
					ProductID = 49730
				}
			};
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0003A32C File Offset: 0x0003852C
		public LogitechChillStreamControllerMacNativeProfile()
		{
		}
	}
}
