using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200019B RID: 411
	[Preserve]
	[NativeInputDeviceProfile]
	public class ProEXXboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x0003CEE4 File Offset: 0x0003B0E4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Pro EX Xbox One Controller";
			base.DeviceNotes = "Pro EX Xbox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21562
				}
			};
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0003CF58 File Offset: 0x0003B158
		public ProEXXboxOneControllerMacNativeProfile()
		{
		}
	}
}
