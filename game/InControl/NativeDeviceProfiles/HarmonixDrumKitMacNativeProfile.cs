using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000138 RID: 312
	[Preserve]
	[NativeInputDeviceProfile]
	public class HarmonixDrumKitMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x00038F58 File Offset: 0x00037158
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Harmonix Drum Kit";
			base.DeviceNotes = "Harmonix Drum Kit on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 4408
				}
			};
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00038FCC File Offset: 0x000371CC
		public HarmonixDrumKitMacNativeProfile()
		{
		}
	}
}
