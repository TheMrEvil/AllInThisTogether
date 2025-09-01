using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200016B RID: 363
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzFPSProMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007A1 RID: 1953 RVA: 0x0003AC34 File Offset: 0x00038E34
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz FPS Pro";
			base.DeviceNotes = "Mad Catz FPS Pro on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61479
				}
			};
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0003ACA8 File Offset: 0x00038EA8
		public MadCatzFPSProMacNativeProfile()
		{
		}
	}
}
