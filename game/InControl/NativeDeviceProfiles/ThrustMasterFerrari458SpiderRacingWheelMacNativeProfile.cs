using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001AF RID: 431
	[Preserve]
	[NativeInputDeviceProfile]
	public class ThrustMasterFerrari458SpiderRacingWheelMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x0003DB48 File Offset: 0x0003BD48
		public override void Define()
		{
			base.Define();
			base.DeviceName = "ThrustMaster Ferrari 458 Spider Racing Wheel";
			base.DeviceNotes = "ThrustMaster Ferrari 458 Spider Racing Wheel on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1103,
					ProductID = 46705
				}
			};
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0003DBBC File Offset: 0x0003BDBC
		public ThrustMasterFerrari458SpiderRacingWheelMacNativeProfile()
		{
		}
	}
}
