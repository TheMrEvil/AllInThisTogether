using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000159 RID: 345
	[Preserve]
	[NativeInputDeviceProfile]
	public class KonamiDancePadMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x0003A240 File Offset: 0x00038440
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Konami Dance Pad";
			base.DeviceNotes = "Konami Dance Pad on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 4779,
					ProductID = 4
				}
			};
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0003A2B0 File Offset: 0x000384B0
		public KonamiDancePadMacNativeProfile()
		{
		}
	}
}
