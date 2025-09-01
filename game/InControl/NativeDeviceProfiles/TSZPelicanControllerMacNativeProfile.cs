using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001B4 RID: 436
	[Preserve]
	[NativeInputDeviceProfile]
	public class TSZPelicanControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x0003DDF0 File Offset: 0x0003BFF0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "TSZ Pelican Controller";
			base.DeviceNotes = "TSZ Pelican Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 513
				}
			};
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0003DE64 File Offset: 0x0003C064
		public TSZPelicanControllerMacNativeProfile()
		{
		}
	}
}
