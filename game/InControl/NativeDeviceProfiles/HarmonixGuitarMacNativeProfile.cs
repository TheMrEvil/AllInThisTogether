using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000139 RID: 313
	[Preserve]
	[NativeInputDeviceProfile]
	public class HarmonixGuitarMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x00038FD4 File Offset: 0x000371D4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Harmonix Guitar";
			base.DeviceNotes = "Harmonix Guitar on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 5432
				}
			};
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00039048 File Offset: 0x00037248
		public HarmonixGuitarMacNativeProfile()
		{
		}
	}
}
