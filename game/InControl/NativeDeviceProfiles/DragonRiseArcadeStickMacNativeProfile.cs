using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000132 RID: 306
	[Preserve]
	[NativeInputDeviceProfile]
	public class DragonRiseArcadeStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x00038B70 File Offset: 0x00036D70
		public override void Define()
		{
			base.Define();
			base.DeviceName = "DragonRise Arcade Stick";
			base.DeviceNotes = "DragonRise Arcade Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 121,
					ProductID = 6268
				}
			};
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00038BE1 File Offset: 0x00036DE1
		public DragonRiseArcadeStickMacNativeProfile()
		{
		}
	}
}
