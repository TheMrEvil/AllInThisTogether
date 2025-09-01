using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200018D RID: 397
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPMetallicsLEControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x0003C3A0 File Offset: 0x0003A5A0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Metallics LE Controller";
			base.DeviceNotes = "PDP Metallics LE Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 345
				}
			};
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0003C414 File Offset: 0x0003A614
		public PDPMetallicsLEControllerMacNativeProfile()
		{
		}
	}
}
