using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200014F RID: 335
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRealArcadeProIVMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x00039CEC File Offset: 0x00037EEC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Real Arcade Pro IV";
			base.DeviceNotes = "Hori Real Arcade Pro IV on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 140
				}
			};
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00039D60 File Offset: 0x00037F60
		public HoriRealArcadeProIVMacNativeProfile()
		{
		}
	}
}
