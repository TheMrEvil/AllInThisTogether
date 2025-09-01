using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000186 RID: 390
	[Preserve]
	[NativeInputDeviceProfile]
	public class MLGControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x0003BC80 File Offset: 0x00039E80
		public override void Define()
		{
			base.Define();
			base.DeviceName = "MLG Controller";
			base.DeviceNotes = "MLG Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61475
				}
			};
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0003BCF4 File Offset: 0x00039EF4
		public MLGControllerMacNativeProfile()
		{
		}
	}
}
