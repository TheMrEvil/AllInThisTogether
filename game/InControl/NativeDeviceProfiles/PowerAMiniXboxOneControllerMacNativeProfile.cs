using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000198 RID: 408
	[Preserve]
	[NativeInputDeviceProfile]
	public class PowerAMiniXboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x0003CD70 File Offset: 0x0003AF70
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Power A Mini Xbox One Controller";
			base.DeviceNotes = "Power A Mini Xbox One Controller on Mac";
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

		// Token: 0x060007FC RID: 2044 RVA: 0x0003CDE4 File Offset: 0x0003AFE4
		public PowerAMiniXboxOneControllerMacNativeProfile()
		{
		}
	}
}
