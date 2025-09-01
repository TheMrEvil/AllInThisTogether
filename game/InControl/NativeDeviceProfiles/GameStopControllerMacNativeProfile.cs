using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000136 RID: 310
	[Preserve]
	[NativeInputDeviceProfile]
	public class GameStopControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x00038DA0 File Offset: 0x00036FA0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "GameStop Controller";
			base.DeviceNotes = "GameStop Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 1025
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 769
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 4779,
					ProductID = 770
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 63745
				}
			};
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00038ED1 File Offset: 0x000370D1
		public GameStopControllerMacNativeProfile()
		{
		}
	}
}
