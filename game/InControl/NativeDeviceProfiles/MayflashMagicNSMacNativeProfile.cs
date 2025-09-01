using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200017E RID: 382
	[Preserve]
	[NativeInputDeviceProfile]
	public class MayflashMagicNSMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x0003B568 File Offset: 0x00039768
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mayflash Magic-NS";
			base.DeviceNotes = "Mayflash Magic-NS on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 121,
					ProductID = 6355
				}
			};
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0003B5D9 File Offset: 0x000397D9
		public MayflashMagicNSMacNativeProfile()
		{
		}
	}
}
