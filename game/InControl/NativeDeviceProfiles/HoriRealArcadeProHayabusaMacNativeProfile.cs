using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200014E RID: 334
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRealArcadeProHayabusaMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x00039C70 File Offset: 0x00037E70
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Real Arcade Pro Hayabusa";
			base.DeviceNotes = "Hori Real Arcade Pro Hayabusa on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 99
				}
			};
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00039CE1 File Offset: 0x00037EE1
		public HoriRealArcadeProHayabusaMacNativeProfile()
		{
		}
	}
}
