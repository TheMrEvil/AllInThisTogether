using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001B2 RID: 434
	[Preserve]
	[NativeInputDeviceProfile]
	public class ThrustmasterTXGIPMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x0003DCFC File Offset: 0x0003BEFC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Thrustmaster TX GIP";
			base.DeviceNotes = "Thrustmaster TX GIP on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1103,
					ProductID = 46692
				}
			};
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0003DD70 File Offset: 0x0003BF70
		public ThrustmasterTXGIPMacNativeProfile()
		{
		}
	}
}
