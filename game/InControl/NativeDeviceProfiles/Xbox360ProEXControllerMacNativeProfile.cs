using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001B6 RID: 438
	[Preserve]
	[NativeInputDeviceProfile]
	public class Xbox360ProEXControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x0003DEE8 File Offset: 0x0003C0E8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Xbox 360 Pro EX Controller";
			base.DeviceNotes = "Xbox 360 Pro EX Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 8406,
					ProductID = 10271
				}
			};
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0003DF5C File Offset: 0x0003C15C
		public Xbox360ProEXControllerMacNativeProfile()
		{
		}
	}
}
