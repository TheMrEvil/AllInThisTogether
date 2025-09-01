using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000162 RID: 354
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzArcadeStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x0003A6D8 File Offset: 0x000388D8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Arcade Stick";
			base.DeviceNotes = "Mad Catz Arcade Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 18264
				}
			};
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0003A74C File Offset: 0x0003894C
		public MadCatzArcadeStickMacNativeProfile()
		{
		}
	}
}
