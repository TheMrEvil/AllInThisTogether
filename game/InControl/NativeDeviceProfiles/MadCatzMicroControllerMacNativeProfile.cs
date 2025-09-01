using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000171 RID: 369
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzMicroControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x0003AF1C File Offset: 0x0003911C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Micro Controller";
			base.DeviceNotes = "Mad Catz Micro Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 18230
				}
			};
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0003AF90 File Offset: 0x00039190
		public MadCatzMicroControllerMacNativeProfile()
		{
		}
	}
}
