using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000150 RID: 336
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriRealArcadeProVHayabusaMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00039D68 File Offset: 0x00037F68
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Real Arcade Pro V Hayabusa";
			base.DeviceNotes = "Hori Real Arcade Pro V Hayabusa on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 216
				}
			};
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00039DDC File Offset: 0x00037FDC
		public HoriRealArcadeProVHayabusaMacNativeProfile()
		{
		}
	}
}
