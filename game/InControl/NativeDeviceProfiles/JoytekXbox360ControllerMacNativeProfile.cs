using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000158 RID: 344
	[Preserve]
	[NativeInputDeviceProfile]
	public class JoytekXbox360ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0003A1C4 File Offset: 0x000383C4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Joytek Xbox 360 Controller";
			base.DeviceNotes = "Joytek Xbox 360 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5678,
					ProductID = 48879
				}
			};
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0003A238 File Offset: 0x00038438
		public JoytekXbox360ControllerMacNativeProfile()
		{
		}
	}
}
