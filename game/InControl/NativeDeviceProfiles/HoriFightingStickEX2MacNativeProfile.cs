using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000144 RID: 324
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriFightingStickEX2MacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x000396E0 File Offset: 0x000378E0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Fighting Stick EX2";
			base.DeviceNotes = "Hori Fighting Stick EX2 on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 10
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 62725
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 13
				}
			};
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000397CC File Offset: 0x000379CC
		public HoriFightingStickEX2MacNativeProfile()
		{
		}
	}
}
