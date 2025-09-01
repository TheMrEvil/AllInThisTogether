using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000142 RID: 322
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriFightingCommanderMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x000395A8 File Offset: 0x000377A8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Fighting Commander";
			base.DeviceNotes = "Hori Fighting Commander on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 197
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21776
				}
			};
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0003965B File Offset: 0x0003785B
		public HoriFightingCommanderMacNativeProfile()
		{
		}
	}
}
