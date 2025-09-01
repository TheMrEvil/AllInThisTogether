using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001B1 RID: 433
	[Preserve]
	[NativeInputDeviceProfile]
	public class ThrustmasterTMXMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x0003DC80 File Offset: 0x0003BE80
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Thrustmaster TMX";
			base.DeviceNotes = "Thrustmaster TMX on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1103,
					ProductID = 46718
				}
			};
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0003DCF4 File Offset: 0x0003BEF4
		public ThrustmasterTMXMacNativeProfile()
		{
		}
	}
}
