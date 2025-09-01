using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200016D RID: 365
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzInnoControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x0003AD2C File Offset: 0x00038F2C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Inno Controller";
			base.DeviceNotes = "Mad Catz Inno Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 62465
				}
			};
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0003ADA0 File Offset: 0x00038FA0
		public MadCatzInnoControllerMacNativeProfile()
		{
		}
	}
}
