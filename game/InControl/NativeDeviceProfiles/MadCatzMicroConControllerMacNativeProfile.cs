using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000170 RID: 368
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzMicroConControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x0003AEA0 File Offset: 0x000390A0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz MicroCon Controller";
			base.DeviceNotes = "Mad Catz MicroCon Controller on Mac";
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

		// Token: 0x060007AC RID: 1964 RVA: 0x0003AF14 File Offset: 0x00039114
		public MadCatzMicroConControllerMacNativeProfile()
		{
		}
	}
}
