using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000165 RID: 357
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzCODControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x0003A84C File Offset: 0x00038A4C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz COD Controller";
			base.DeviceNotes = "Mad Catz COD Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61477
				}
			};
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0003A8C0 File Offset: 0x00038AC0
		public MadCatzCODControllerMacNativeProfile()
		{
		}
	}
}
