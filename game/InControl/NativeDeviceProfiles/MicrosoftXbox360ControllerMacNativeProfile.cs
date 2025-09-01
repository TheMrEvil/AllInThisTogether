using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000181 RID: 385
	[Preserve]
	[NativeInputDeviceProfile]
	public class MicrosoftXbox360ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x0003B6DC File Offset: 0x000398DC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Microsoft Xbox 360 Controller";
			base.DeviceNotes = "Microsoft Xbox 360 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 654
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 655
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 307
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 63233
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 672
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 62721
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 672
				}
			};
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0003B8CA File Offset: 0x00039ACA
		public MicrosoftXbox360ControllerMacNativeProfile()
		{
		}
	}
}
