using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200019F RID: 415
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerOnzaTEControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x0003D154 File Offset: 0x0003B354
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Onza TE Controller";
			base.DeviceNotes = "Razer Onza TE Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 64768
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5769,
					ProductID = 64768
				}
			};
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0003D207 File Offset: 0x0003B407
		public RazerOnzaTEControllerMacNativeProfile()
		{
		}
	}
}
