using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200012F RID: 303
	[Preserve]
	[NativeInputDeviceProfile]
	public class BigBenControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x000389FC File Offset: 0x00036BFC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Big Ben Controller";
			base.DeviceNotes = "Big Ben Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5227,
					ProductID = 1537
				}
			};
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00038A70 File Offset: 0x00036C70
		public BigBenControllerMacNativeProfile()
		{
		}
	}
}
