using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000134 RID: 308
	[Preserve]
	[NativeInputDeviceProfile]
	public class ElecomControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x00038C68 File Offset: 0x00036E68
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Elecom Controller";
			base.DeviceNotes = "Elecom Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1390,
					ProductID = 8196
				}
			};
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00038CDC File Offset: 0x00036EDC
		public ElecomControllerMacNativeProfile()
		{
		}
	}
}
