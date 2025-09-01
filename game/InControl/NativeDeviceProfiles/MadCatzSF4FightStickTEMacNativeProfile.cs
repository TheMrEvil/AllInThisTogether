using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000179 RID: 377
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzSF4FightStickTEMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x0003B2FC File Offset: 0x000394FC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz SF4 Fight Stick TE";
			base.DeviceNotes = "Mad Catz SF4 Fight Stick TE on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 18232
				}
			};
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0003B370 File Offset: 0x00039570
		public MadCatzSF4FightStickTEMacNativeProfile()
		{
		}
	}
}
