using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A0 RID: 416
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerSabertoothEliteControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x0003D210 File Offset: 0x0003B410
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Sabertooth Elite Controller";
			base.DeviceNotes = "Razer Sabertooth Elite Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5769,
					ProductID = 65024
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 23812
				}
			};
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0003D2C3 File Offset: 0x0003B4C3
		public RazerSabertoothEliteControllerMacNativeProfile()
		{
		}
	}
}
