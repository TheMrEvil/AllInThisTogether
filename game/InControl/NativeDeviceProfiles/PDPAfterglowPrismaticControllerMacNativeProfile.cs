using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200018A RID: 394
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPAfterglowPrismaticControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007DF RID: 2015 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Afterglow Prismatic Controller";
			base.DeviceNotes = "PDP Afterglow Prismatic Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 313
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 691
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 696
				}
			};
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0003C29E File Offset: 0x0003A49E
		public PDPAfterglowPrismaticControllerMacNativeProfile()
		{
		}
	}
}
