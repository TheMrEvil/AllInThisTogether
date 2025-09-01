using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000174 RID: 372
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzPortableDrumMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x0003B090 File Offset: 0x00039290
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Portable Drum";
			base.DeviceNotes = "Mad Catz Portable Drum on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 39025
				}
			};
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0003B104 File Offset: 0x00039304
		public MadCatzPortableDrumMacNativeProfile()
		{
		}
	}
}
