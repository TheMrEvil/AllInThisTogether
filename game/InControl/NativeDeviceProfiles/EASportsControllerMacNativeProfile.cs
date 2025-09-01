using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000133 RID: 307
	[Preserve]
	[NativeInputDeviceProfile]
	public class EASportsControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00038BEC File Offset: 0x00036DEC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "EA Sports Controller";
			base.DeviceNotes = "EA Sports Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 305
				}
			};
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00038C60 File Offset: 0x00036E60
		public EASportsControllerMacNativeProfile()
		{
		}
	}
}
