using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200013D RID: 317
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x000391C4 File Offset: 0x000373C4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Controller";
			base.DeviceNotes = "Hori Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 220
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 103
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 256
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 21760
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 654
				}
			};
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00039331 File Offset: 0x00037531
		public HoriControllerMacNativeProfile()
		{
		}
	}
}
