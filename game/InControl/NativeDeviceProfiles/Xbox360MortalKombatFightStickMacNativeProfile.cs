using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001B5 RID: 437
	[Preserve]
	[NativeInputDeviceProfile]
	public class Xbox360MortalKombatFightStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x0003DE6C File Offset: 0x0003C06C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Xbox 360 Mortal Kombat Fight Stick";
			base.DeviceNotes = "Xbox 360 Mortal Kombat Fight Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 63750
				}
			};
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0003DEE0 File Offset: 0x0003C0E0
		public Xbox360MortalKombatFightStickMacNativeProfile()
		{
		}
	}
}
