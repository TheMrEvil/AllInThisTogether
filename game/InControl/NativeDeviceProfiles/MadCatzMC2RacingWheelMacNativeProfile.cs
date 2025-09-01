using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200016F RID: 367
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzMC2RacingWheelMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x0003AE24 File Offset: 0x00039024
		public override void Define()
		{
			base.Define();
			base.DeviceName = "MadCatz MC2 Racing Wheel";
			base.DeviceNotes = "MadCatz MC2 Racing Wheel on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61472
				}
			};
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0003AE98 File Offset: 0x00039098
		public MadCatzMC2RacingWheelMacNativeProfile()
		{
		}
	}
}
