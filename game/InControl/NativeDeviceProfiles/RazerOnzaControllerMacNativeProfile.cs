using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200019E RID: 414
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerOnzaControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x0003D098 File Offset: 0x0003B298
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Onza Controller";
			base.DeviceNotes = "Razer Onza Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 64769
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5769,
					ProductID = 64769
				}
			};
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0003D14B File Offset: 0x0003B34B
		public RazerOnzaControllerMacNativeProfile()
		{
		}
	}
}
