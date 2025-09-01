using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A5 RID: 421
	[Preserve]
	[NativeInputDeviceProfile]
	public class RedOctaneGuitarMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x0003D4F8 File Offset: 0x0003B6F8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "RedOctane Guitar";
			base.DeviceNotes = "RedOctane Guitar on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5168,
					ProductID = 1803
				}
			};
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0003D56C File Offset: 0x0003B76C
		public RedOctaneGuitarMacNativeProfile()
		{
		}
	}
}
