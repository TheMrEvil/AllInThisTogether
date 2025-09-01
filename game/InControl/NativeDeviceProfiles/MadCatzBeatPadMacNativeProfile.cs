using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000163 RID: 355
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzBeatPadMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x0003A754 File Offset: 0x00038954
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Beat Pad";
			base.DeviceNotes = "Mad Catz Beat Pad on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 18240
				}
			};
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0003A7C8 File Offset: 0x000389C8
		public MadCatzBeatPadMacNativeProfile()
		{
		}
	}
}
