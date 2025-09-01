using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000155 RID: 341
	[Preserve]
	[NativeInputDeviceProfile]
	public class HyperkinX91MacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x0003A050 File Offset: 0x00038250
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hyperkin X91";
			base.DeviceNotes = "Hyperkin X91 on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 11812,
					ProductID = 5768
				}
			};
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0003A0C4 File Offset: 0x000382C4
		public HyperkinX91MacNativeProfile()
		{
		}
	}
}
