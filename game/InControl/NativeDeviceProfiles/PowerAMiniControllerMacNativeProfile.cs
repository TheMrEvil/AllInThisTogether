using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000196 RID: 406
	[Preserve]
	[NativeInputDeviceProfile]
	public class PowerAMiniControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x0003CBF8 File Offset: 0x0003ADF8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PowerA Mini Controller";
			base.DeviceNotes = "PowerA Mini Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21530
				}
			};
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0003CC6C File Offset: 0x0003AE6C
		public PowerAMiniControllerMacNativeProfile()
		{
		}
	}
}
