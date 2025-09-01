using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200014A RID: 330
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRAPNFightingStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x00039A80 File Offset: 0x00037C80
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori RAP N Fighting Stick";
			base.DeviceNotes = "Hori RAP N Fighting Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 174
				}
			};
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00039AF4 File Offset: 0x00037CF4
		public HoriRAPNFightingStickMacNativeProfile()
		{
		}
	}
}
