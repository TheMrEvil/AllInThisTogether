using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200013A RID: 314
	[Preserve]
	[NativeInputDeviceProfile]
	public class HarmonixKeyboardMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x00039050 File Offset: 0x00037250
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Harmonix Keyboard";
			base.DeviceNotes = "Harmonix Keyboard on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 4920
				}
			};
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000390C4 File Offset: 0x000372C4
		public HarmonixKeyboardMacNativeProfile()
		{
		}
	}
}
