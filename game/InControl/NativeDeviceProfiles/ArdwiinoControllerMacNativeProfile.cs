using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200012B RID: 299
	[Preserve]
	[NativeInputDeviceProfile]
	public class ArdwiinoControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x0003878C File Offset: 0x0003698C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Ardwiino Controller";
			base.DeviceNotes = "Ardwiino Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 4617,
					ProductID = 10370
				}
			};
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00038800 File Offset: 0x00036A00
		public ArdwiinoControllerMacNativeProfile()
		{
		}
	}
}
