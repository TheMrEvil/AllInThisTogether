using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000192 RID: 402
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPXboxOneArcadeStickMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x0003C60C File Offset: 0x0003A80C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Xbox One Arcade Stick";
			base.DeviceNotes = "PDP Xbox One Arcade Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 348
				}
			};
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0003C680 File Offset: 0x0003A880
		public PDPXboxOneArcadeStickMacNativeProfile()
		{
		}
	}
}
