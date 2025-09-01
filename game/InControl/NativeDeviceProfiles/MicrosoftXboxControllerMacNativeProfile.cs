using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000182 RID: 386
	[Preserve]
	[NativeInputDeviceProfile]
	public class MicrosoftXboxControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x0003B8D4 File Offset: 0x00039AD4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Microsoft Xbox Controller";
			base.DeviceNotes = "Microsoft Xbox Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = ushort.MaxValue,
					ProductID = ushort.MaxValue
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 649
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 648
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 645
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 514
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 647
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 648
				}
			};
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0003BAC2 File Offset: 0x00039CC2
		public MicrosoftXboxControllerMacNativeProfile()
		{
		}
	}
}
