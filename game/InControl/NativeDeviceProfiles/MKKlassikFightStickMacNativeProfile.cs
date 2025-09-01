using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000185 RID: 389
	[Preserve]
	[NativeInputDeviceProfile]
	public class MKKlassikFightStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x0003BC04 File Offset: 0x00039E04
		public override void Define()
		{
			base.Define();
			base.DeviceName = "MK Klassik Fight Stick";
			base.DeviceNotes = "MK Klassik Fight Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 4779,
					ProductID = 771
				}
			};
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0003BC78 File Offset: 0x00039E78
		public MKKlassikFightStickMacNativeProfile()
		{
		}
	}
}
