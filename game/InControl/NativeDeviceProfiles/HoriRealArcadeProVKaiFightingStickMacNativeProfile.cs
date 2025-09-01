using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000151 RID: 337
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRealArcadeProVKaiFightingStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x00039DE4 File Offset: 0x00037FE4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Real Arcade Pro V Kai Fighting Stick";
			base.DeviceNotes = "Hori Real Arcade Pro V Kai Fighting Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21774
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 120
				}
			};
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00039E94 File Offset: 0x00038094
		public HoriRealArcadeProVKaiFightingStickMacNativeProfile()
		{
		}
	}
}
