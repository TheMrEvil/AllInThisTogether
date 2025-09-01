using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200014B RID: 331
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRealArcadeProEXMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x00039AFC File Offset: 0x00037CFC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Real Arcade Pro EX";
			base.DeviceNotes = "Hori Real Arcade Pro EX on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 62724
				}
			};
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00039B70 File Offset: 0x00037D70
		public HoriRealArcadeProEXMacNativeProfile()
		{
		}
	}
}
