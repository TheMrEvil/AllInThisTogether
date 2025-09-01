using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200012D RID: 301
	[Preserve]
	[NativeInputDeviceProfile]
	public class BatarangControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x00038904 File Offset: 0x00036B04
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Batarang Controller";
			base.DeviceNotes = "Batarang Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5604,
					ProductID = 16144
				}
			};
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00038978 File Offset: 0x00036B78
		public BatarangControllerMacNativeProfile()
		{
		}
	}
}
