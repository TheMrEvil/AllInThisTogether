using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000164 RID: 356
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzBrawlStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x0003A7D0 File Offset: 0x000389D0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Brawl Stick";
			base.DeviceNotes = "Mad Catz Brawl Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61465
				}
			};
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0003A844 File Offset: 0x00038A44
		public MadCatzBrawlStickMacNativeProfile()
		{
		}
	}
}
