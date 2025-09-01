using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A8 RID: 424
	[Preserve]
	[NativeInputDeviceProfile]
	public class RockCandyControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x0003D664 File Offset: 0x0003B864
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Rock Candy Controller";
			base.DeviceNotes = "Rock Candy Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 287
				}
			};
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0003D6D8 File Offset: 0x0003B8D8
		public RockCandyControllerMacNativeProfile()
		{
		}
	}
}
