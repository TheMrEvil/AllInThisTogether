using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000137 RID: 311
	[Preserve]
	[NativeInputDeviceProfile]
	public class GuitarHeroControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00038EDC File Offset: 0x000370DC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Guitar Hero Controller";
			base.DeviceNotes = "Guitar Hero Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5168,
					ProductID = 18248
				}
			};
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00038F50 File Offset: 0x00037150
		public GuitarHeroControllerMacNativeProfile()
		{
		}
	}
}
