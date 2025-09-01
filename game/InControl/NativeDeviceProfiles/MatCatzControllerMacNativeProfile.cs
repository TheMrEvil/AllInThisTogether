using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200017D RID: 381
	[Preserve]
	[NativeInputDeviceProfile]
	public class MatCatzControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x0003B4EC File Offset: 0x000396EC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mat Catz Controller";
			base.DeviceNotes = "Mat Catz Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61462
				}
			};
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0003B560 File Offset: 0x00039760
		public MatCatzControllerMacNativeProfile()
		{
		}
	}
}
