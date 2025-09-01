using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001B3 RID: 435
	[Preserve]
	[NativeInputDeviceProfile]
	public class TrustPredatorJoystickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x0003DD78 File Offset: 0x0003BF78
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Trust Predator Joystick";
			base.DeviceNotes = "Trust Predator Joystick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 2064,
					ProductID = 3
				}
			};
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0003DDE8 File Offset: 0x0003BFE8
		public TrustPredatorJoystickMacNativeProfile()
		{
		}
	}
}
