using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A1 RID: 417
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerStrikeControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x0003D2CC File Offset: 0x0003B4CC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Strike Controller";
			base.DeviceNotes = "Razer Strike Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5769,
					ProductID = 1
				}
			};
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0003D33C File Offset: 0x0003B53C
		public RazerStrikeControllerMacNativeProfile()
		{
		}
	}
}
