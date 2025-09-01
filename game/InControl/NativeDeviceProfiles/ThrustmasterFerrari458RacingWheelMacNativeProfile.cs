using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001AE RID: 430
	[Preserve]
	[NativeInputDeviceProfile]
	public class ThrustmasterFerrari458RacingWheelMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x0003DA8C File Offset: 0x0003BC8C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Thrustmaster Ferrari 458 Racing Wheel";
			base.DeviceNotes = "Thrustmaster Ferrari 458 Racing Wheel on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 23296
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 23299
				}
			};
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0003DB3F File Offset: 0x0003BD3F
		public ThrustmasterFerrari458RacingWheelMacNativeProfile()
		{
		}
	}
}
