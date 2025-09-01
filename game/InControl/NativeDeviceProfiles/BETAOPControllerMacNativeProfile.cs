using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200012E RID: 302
	[Preserve]
	[NativeInputDeviceProfile]
	public class BETAOPControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x00038980 File Offset: 0x00036B80
		public override void Define()
		{
			base.Define();
			base.DeviceName = "BETAOP Controller";
			base.DeviceNotes = "BETAOP Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 4544,
					ProductID = 21766
				}
			};
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000389F4 File Offset: 0x00036BF4
		public BETAOPControllerMacNativeProfile()
		{
		}
	}
}
