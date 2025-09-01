using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000152 RID: 338
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRealArcadeProVXMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x00039E9C File Offset: 0x0003809C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Real Arcade Pro VX";
			base.DeviceNotes = "Hori Real Arcade Pro VX on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 27
				}
			};
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00039F0D File Offset: 0x0003810D
		public HoriRealArcadeProVXMacNativeProfile()
		{
		}
	}
}
