using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001B0 RID: 432
	[Preserve]
	[NativeInputDeviceProfile]
	public class ThrustmasterGPXControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x0003DBC4 File Offset: 0x0003BDC4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Thrustmaster GPX Controller";
			base.DeviceNotes = "Thrustmaster GPX Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1103,
					ProductID = 45862
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 23298
				}
			};
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0003DC77 File Offset: 0x0003BE77
		public ThrustmasterGPXControllerMacNativeProfile()
		{
		}
	}
}
