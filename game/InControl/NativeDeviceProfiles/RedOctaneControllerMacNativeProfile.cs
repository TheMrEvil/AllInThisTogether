using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A4 RID: 420
	[Preserve]
	[NativeInputDeviceProfile]
	public class RedOctaneControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x0003D43C File Offset: 0x0003B63C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Red Octane Controller";
			base.DeviceNotes = "Red Octane Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5168,
					ProductID = 63489
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5168,
					ProductID = 672
				}
			};
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0003D4EF File Offset: 0x0003B6EF
		public RedOctaneControllerMacNativeProfile()
		{
		}
	}
}
