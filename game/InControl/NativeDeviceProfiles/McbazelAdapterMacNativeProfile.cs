using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200017F RID: 383
	[Preserve]
	[NativeInputDeviceProfile]
	public class McbazelAdapterMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x0003B5E4 File Offset: 0x000397E4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mcbazel Adapter";
			base.DeviceNotes = "Mcbazel Adapter on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 654
				}
			};
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0003B658 File Offset: 0x00039858
		public McbazelAdapterMacNativeProfile()
		{
		}
	}
}
