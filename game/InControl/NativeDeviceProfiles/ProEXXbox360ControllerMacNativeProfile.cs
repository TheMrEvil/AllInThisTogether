using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200019A RID: 410
	[Preserve]
	[NativeInputDeviceProfile]
	public class ProEXXbox360ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x0003CE68 File Offset: 0x0003B068
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Pro EX Xbox 360 Controller";
			base.DeviceNotes = "Pro EX Xbox 360 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21258
				}
			};
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0003CEDC File Offset: 0x0003B0DC
		public ProEXXbox360ControllerMacNativeProfile()
		{
		}
	}
}
