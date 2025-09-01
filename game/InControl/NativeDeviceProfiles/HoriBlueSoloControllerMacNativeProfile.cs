using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200013C RID: 316
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriBlueSoloControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x00039148 File Offset: 0x00037348
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Blue Solo Controller ";
			base.DeviceNotes = "Hori Blue Solo Controller\ton Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 64001
				}
			};
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000391BC File Offset: 0x000373BC
		public HoriBlueSoloControllerMacNativeProfile()
		{
		}
	}
}
