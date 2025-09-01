using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000176 RID: 374
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzSaitekAV8R02MacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0003B188 File Offset: 0x00039388
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Saitek AV8R02";
			base.DeviceNotes = "Mad Catz Saitek AV8R02 on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 52009
				}
			};
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0003B1FC File Offset: 0x000393FC
		public MadCatzSaitekAV8R02MacNativeProfile()
		{
		}
	}
}
