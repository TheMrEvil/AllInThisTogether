using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000149 RID: 329
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriPadUltimateMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x00039A04 File Offset: 0x00037C04
		public override void Define()
		{
			base.Define();
			base.DeviceName = "HoriPad Ultimate";
			base.DeviceNotes = "HoriPad Ultimate on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 144
				}
			};
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00039A78 File Offset: 0x00037C78
		public HoriPadUltimateMacNativeProfile()
		{
		}
	}
}
