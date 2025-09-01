using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200012C RID: 300
	[Preserve]
	[NativeInputDeviceProfile]
	public class AtPlayControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x00038808 File Offset: 0x00036A08
		public override void Define()
		{
			base.Define();
			base.DeviceName = "At Play Controller";
			base.DeviceNotes = "At Play Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 64250
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 64251
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 690
				}
			};
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000388FA File Offset: 0x00036AFA
		public AtPlayControllerMacNativeProfile()
		{
		}
	}
}
