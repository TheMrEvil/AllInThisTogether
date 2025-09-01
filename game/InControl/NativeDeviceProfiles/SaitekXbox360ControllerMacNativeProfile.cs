using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001AC RID: 428
	[Preserve]
	[NativeInputDeviceProfile]
	public class SaitekXbox360ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x0003D994 File Offset: 0x0003BB94
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Saitek Xbox 360 Controller";
			base.DeviceNotes = "Saitek Xbox 360 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 51970
				}
			};
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0003DA08 File Offset: 0x0003BC08
		public SaitekXbox360ControllerMacNativeProfile()
		{
		}
	}
}
