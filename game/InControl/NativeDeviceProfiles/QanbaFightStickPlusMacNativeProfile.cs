using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200019C RID: 412
	[Preserve]
	[NativeInputDeviceProfile]
	public class QanbaFightStickPlusMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x0003CF60 File Offset: 0x0003B160
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Qanba Fight Stick Plus";
			base.DeviceNotes = "Qanba Fight Stick Plus on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 48879
				}
			};
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0003CFD4 File Offset: 0x0003B1D4
		public QanbaFightStickPlusMacNativeProfile()
		{
		}
	}
}
