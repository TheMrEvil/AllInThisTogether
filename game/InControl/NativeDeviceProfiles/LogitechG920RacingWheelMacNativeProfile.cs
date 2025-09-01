using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000160 RID: 352
	[Preserve]
	[NativeInputDeviceProfile]
	public class LogitechG920RacingWheelMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x0003A5E0 File Offset: 0x000387E0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech G920 Racing Wheel";
			base.DeviceNotes = "Logitech G920 Racing Wheel on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1133,
					ProductID = 49761
				}
			};
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0003A654 File Offset: 0x00038854
		public LogitechG920RacingWheelMacNativeProfile()
		{
		}
	}
}
