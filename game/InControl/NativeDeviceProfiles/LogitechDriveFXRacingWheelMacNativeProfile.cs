using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200015C RID: 348
	[Preserve]
	[NativeInputDeviceProfile]
	public class LogitechDriveFXRacingWheelMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x0003A3B0 File Offset: 0x000385B0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech DriveFX Racing Wheel";
			base.DeviceNotes = "Logitech DriveFX Racing Wheel on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1133,
					ProductID = 51875
				}
			};
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0003A424 File Offset: 0x00038624
		public LogitechDriveFXRacingWheelMacNativeProfile()
		{
		}
	}
}
