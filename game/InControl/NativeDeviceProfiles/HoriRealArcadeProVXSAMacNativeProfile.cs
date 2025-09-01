using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000153 RID: 339
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRealArcadeProVXSAMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x00039F18 File Offset: 0x00038118
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Real Arcade Pro VX SA";
			base.DeviceNotes = "Hori Real Arcade Pro VX SA on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 62722
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21761
				}
			};
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00039FCB File Offset: 0x000381CB
		public HoriRealArcadeProVXSAMacNativeProfile()
		{
		}
	}
}
