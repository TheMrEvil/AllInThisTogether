using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000161 RID: 353
	[Preserve]
	[NativeInputDeviceProfile]
	public class LogitechThunderpadMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x0003A65C File Offset: 0x0003885C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech Thunderpad";
			base.DeviceNotes = "Logitech Thunderpad on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1133,
					ProductID = 51848
				}
			};
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0003A6D0 File Offset: 0x000388D0
		public LogitechThunderpadMacNativeProfile()
		{
		}
	}
}
