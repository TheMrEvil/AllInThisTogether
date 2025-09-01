using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000173 RID: 371
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzNeoFightStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x0003B014 File Offset: 0x00039214
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Neo Fight Stick";
			base.DeviceNotes = "Mad Catz Neo Fight Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61498
				}
			};
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0003B088 File Offset: 0x00039288
		public MadCatzNeoFightStickMacNativeProfile()
		{
		}
	}
}
