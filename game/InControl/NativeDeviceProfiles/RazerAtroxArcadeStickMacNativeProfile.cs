using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200019D RID: 413
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerAtroxArcadeStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x0003CFDC File Offset: 0x0003B1DC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Atrox Arcade Stick";
			base.DeviceNotes = "Razer Atrox Arcade Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5426,
					ProductID = 2560
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 20480
				}
			};
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0003D08F File Offset: 0x0003B28F
		public RazerAtroxArcadeStickMacNativeProfile()
		{
		}
	}
}
