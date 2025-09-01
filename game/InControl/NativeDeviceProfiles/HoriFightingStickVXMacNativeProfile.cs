using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000146 RID: 326
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriFightingStickVXMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00039850 File Offset: 0x00037A50
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Fighting Stick VX";
			base.DeviceNotes = "Hori Fighting Stick VX on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 62723
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21762
				}
			};
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00039903 File Offset: 0x00037B03
		public HoriFightingStickVXMacNativeProfile()
		{
		}
	}
}
