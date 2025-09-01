using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000166 RID: 358
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x0003A8C8 File Offset: 0x00038AC8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Controller";
			base.DeviceNotes = "Mad Catz Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 18198
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 63746
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61642
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 672
				}
			};
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0003A9F9 File Offset: 0x00038BF9
		public MadCatzControllerMacNativeProfile()
		{
		}
	}
}
