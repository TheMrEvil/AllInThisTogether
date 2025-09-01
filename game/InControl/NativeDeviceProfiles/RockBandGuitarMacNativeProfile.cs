using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A7 RID: 423
	[Preserve]
	[NativeInputDeviceProfile]
	public class RockBandGuitarMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x0003D5EC File Offset: 0x0003B7EC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Rock Band Guitar";
			base.DeviceNotes = "Rock Band Guitar on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 2
				}
			};
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0003D65C File Offset: 0x0003B85C
		public RockBandGuitarMacNativeProfile()
		{
		}
	}
}
