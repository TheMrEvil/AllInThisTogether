using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000141 RID: 321
	[Preserve]
	[NativeInputDeviceProfile]
	public class HORIFightingCommanderControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x0003952C File Offset: 0x0003772C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "HORI Fighting Commander Controller";
			base.DeviceNotes = "HORI Fighting Commander Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 134
				}
			};
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000395A0 File Offset: 0x000377A0
		public HORIFightingCommanderControllerMacNativeProfile()
		{
		}
	}
}
