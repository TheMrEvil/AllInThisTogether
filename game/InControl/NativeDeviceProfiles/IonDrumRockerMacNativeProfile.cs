using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000157 RID: 343
	[Preserve]
	[NativeInputDeviceProfile]
	public class IonDrumRockerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x0003A148 File Offset: 0x00038348
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Ion Drum Rocker";
			base.DeviceNotes = "Ion Drum Rocker on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 304
				}
			};
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0003A1BC File Offset: 0x000383BC
		public IonDrumRockerMacNativeProfile()
		{
		}
	}
}
