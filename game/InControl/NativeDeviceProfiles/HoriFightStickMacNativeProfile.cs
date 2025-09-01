using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000147 RID: 327
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriFightStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x0003990C File Offset: 0x00037B0C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Fight Stick";
			base.DeviceNotes = "Hori Fight Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 13
				}
			};
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0003997D File Offset: 0x00037B7D
		public HoriFightStickMacNativeProfile()
		{
		}
	}
}
