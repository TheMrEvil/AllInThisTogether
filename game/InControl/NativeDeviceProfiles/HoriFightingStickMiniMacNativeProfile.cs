using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000145 RID: 325
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriFightingStickMiniMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x000397D4 File Offset: 0x000379D4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Fighting Stick Mini";
			base.DeviceNotes = "Hori Fighting Stick Mini on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 237
				}
			};
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00039848 File Offset: 0x00037A48
		public HoriFightingStickMiniMacNativeProfile()
		{
		}
	}
}
