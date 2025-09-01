using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000197 RID: 407
	[Preserve]
	[NativeInputDeviceProfile]
	public class PowerAMiniProExControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x0003CC74 File Offset: 0x0003AE74
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PowerA Mini Pro Ex Controller";
			base.DeviceNotes = "PowerA Mini Pro Ex Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5604,
					ProductID = 16128
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21274
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21248
				}
			};
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0003CD66 File Offset: 0x0003AF66
		public PowerAMiniProExControllerMacNativeProfile()
		{
		}
	}
}
