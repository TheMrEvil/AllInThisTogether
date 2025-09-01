using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200013E RID: 318
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriDOA4ArcadeStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x0003933C File Offset: 0x0003753C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori DOA4 Arcade Stick";
			base.DeviceNotes = "Hori DOA4 Arcade Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 10
				}
			};
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000393AD File Offset: 0x000375AD
		public HoriDOA4ArcadeStickMacNativeProfile()
		{
		}
	}
}
