using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000188 RID: 392
	[Preserve]
	[NativeInputDeviceProfile]
	public class NaconGC100XFControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x0003BDB8 File Offset: 0x00039FB8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Nacon GC-100XF Controller";
			base.DeviceNotes = "Nacon GC-100XF Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 4553,
					ProductID = 22000
				}
			};
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0003BE2C File Offset: 0x0003A02C
		public NaconGC100XFControllerMacNativeProfile()
		{
		}
	}
}
