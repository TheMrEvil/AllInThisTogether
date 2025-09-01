using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000177 RID: 375
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzSF4FightStickRound2TEMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x0003B204 File Offset: 0x00039404
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz SF4 Fight Stick Round 2 TE";
			base.DeviceNotes = "Mad Catz SF4 Fight Stick Round 2 TE on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61496
				}
			};
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0003B278 File Offset: 0x00039478
		public MadCatzSF4FightStickRound2TEMacNativeProfile()
		{
		}
	}
}
