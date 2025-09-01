using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200014C RID: 332
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRealArcadeProEXPremiumVLXMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x00039B78 File Offset: 0x00037D78
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Real Arcade Pro EX Premium VLX";
			base.DeviceNotes = "Hori Real Arcade Pro EX Premium VLX on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 62726
				}
			};
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00039BEC File Offset: 0x00037DEC
		public HoriRealArcadeProEXPremiumVLXMacNativeProfile()
		{
		}
	}
}
