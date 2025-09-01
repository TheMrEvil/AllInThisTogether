using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000178 RID: 376
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzSF4FightStickSEMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x0003B280 File Offset: 0x00039480
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz SF4 Fight Stick SE";
			base.DeviceNotes = "Mad Catz SF4 Fight Stick SE on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 18200
				}
			};
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0003B2F4 File Offset: 0x000394F4
		public MadCatzSF4FightStickSEMacNativeProfile()
		{
		}
	}
}
