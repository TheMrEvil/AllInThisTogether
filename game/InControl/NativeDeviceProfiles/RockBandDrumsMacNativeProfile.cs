using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A6 RID: 422
	[Preserve]
	[NativeInputDeviceProfile]
	public class RockBandDrumsMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x0003D574 File Offset: 0x0003B774
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Rock Band Drums";
			base.DeviceNotes = "Rock Band Drums on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 3
				}
			};
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0003D5E4 File Offset: 0x0003B7E4
		public RockBandDrumsMacNativeProfile()
		{
		}
	}
}
