using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200012A RID: 298
	[Preserve]
	[NativeInputDeviceProfile]
	public class AirFloControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00038710 File Offset: 0x00036910
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Air Flo Controller";
			base.DeviceNotes = "Air Flo Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21251
				}
			};
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00038784 File Offset: 0x00036984
		public AirFloControllerMacNativeProfile()
		{
		}
	}
}
