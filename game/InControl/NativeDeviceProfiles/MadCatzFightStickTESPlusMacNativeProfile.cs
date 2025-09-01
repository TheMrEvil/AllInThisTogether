using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200016A RID: 362
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzFightStickTESPlusMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x0003ABB8 File Offset: 0x00038DB8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Fight Stick TES Plus";
			base.DeviceNotes = "Mad Catz Fight Stick TES Plus on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61506
				}
			};
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0003AC2C File Offset: 0x00038E2C
		public MadCatzFightStickTESPlusMacNativeProfile()
		{
		}
	}
}
