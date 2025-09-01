using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001AD RID: 429
	[Preserve]
	[NativeInputDeviceProfile]
	public class ThrustMasterFerrari430RacingWheelMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x0003DA10 File Offset: 0x0003BC10
		public override void Define()
		{
			base.Define();
			base.DeviceName = "ThrustMaster Ferrari 430 Racing Wheel";
			base.DeviceNotes = "ThrustMaster Ferrari 430 Racing Wheel on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1103,
					ProductID = 46683
				}
			};
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0003DA84 File Offset: 0x0003BC84
		public ThrustMasterFerrari430RacingWheelMacNativeProfile()
		{
		}
	}
}
