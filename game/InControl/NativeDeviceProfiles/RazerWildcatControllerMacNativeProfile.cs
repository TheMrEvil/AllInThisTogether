using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A2 RID: 418
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerWildcatControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x0003D344 File Offset: 0x0003B544
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Wildcat Controller";
			base.DeviceNotes = "Razer Wildcat Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5426,
					ProductID = 2563
				}
			};
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0003D3B8 File Offset: 0x0003B5B8
		public RazerWildcatControllerMacNativeProfile()
		{
		}
	}
}
