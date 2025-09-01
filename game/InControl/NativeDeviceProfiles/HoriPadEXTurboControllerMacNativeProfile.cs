using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000148 RID: 328
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriPadEXTurboControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00039988 File Offset: 0x00037B88
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Pad EX Turbo Controller";
			base.DeviceNotes = "Hori Pad EX Turbo Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 12
				}
			};
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000399F9 File Offset: 0x00037BF9
		public HoriPadEXTurboControllerMacNativeProfile()
		{
		}
	}
}
