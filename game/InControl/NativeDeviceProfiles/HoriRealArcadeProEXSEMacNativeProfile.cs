using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200014D RID: 333
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRealArcadeProEXSEMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x00039BF4 File Offset: 0x00037DF4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Real Arcade Pro EX SE";
			base.DeviceNotes = "Hori Real Arcade Pro EX SE on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 22
				}
			};
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00039C65 File Offset: 0x00037E65
		public HoriRealArcadeProEXSEMacNativeProfile()
		{
		}
	}
}
