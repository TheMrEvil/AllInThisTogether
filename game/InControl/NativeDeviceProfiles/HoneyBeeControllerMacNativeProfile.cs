using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200013B RID: 315
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoneyBeeControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x000390CC File Offset: 0x000372CC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Honey Bee Controller";
			base.DeviceNotes = "Honey Bee Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 4779,
					ProductID = 21760
				}
			};
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00039140 File Offset: 0x00037340
		public HoneyBeeControllerMacNativeProfile()
		{
		}
	}
}
