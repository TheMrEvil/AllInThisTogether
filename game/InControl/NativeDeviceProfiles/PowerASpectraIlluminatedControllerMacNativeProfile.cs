using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000199 RID: 409
	[Preserve]
	[NativeInputDeviceProfile]
	public class PowerASpectraIlluminatedControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x0003CDEC File Offset: 0x0003AFEC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PowerA Spectra Illuminated Controller";
			base.DeviceNotes = "PowerA Spectra Illuminated Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21546
				}
			};
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0003CE60 File Offset: 0x0003B060
		public PowerASpectraIlluminatedControllerMacNativeProfile()
		{
		}
	}
}
