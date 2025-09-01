using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A9 RID: 425
	[Preserve]
	[NativeInputDeviceProfile]
	public class RockCandyPS3ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x0003D6E0 File Offset: 0x0003B8E0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Rock Candy PS3 Controller";
			base.DeviceNotes = "Rock Candy PS3 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 286
				}
			};
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0003D754 File Offset: 0x0003B954
		public RockCandyPS3ControllerMacNativeProfile()
		{
		}
	}
}
