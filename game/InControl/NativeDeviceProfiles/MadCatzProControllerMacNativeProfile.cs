using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000175 RID: 373
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzProControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x0003B10C File Offset: 0x0003930C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Pro Controller";
			base.DeviceNotes = "Mad Catz Pro Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 18214
				}
			};
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0003B180 File Offset: 0x00039380
		public MadCatzProControllerMacNativeProfile()
		{
		}
	}
}
