using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200013F RID: 319
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriEdgeFightingStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x000393B8 File Offset: 0x000375B8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Edge Fighting Stick";
			base.DeviceNotes = "Hori Edge Fighting Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3853,
					ProductID = 109
				}
			};
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00039429 File Offset: 0x00037629
		public HoriEdgeFightingStickMacNativeProfile()
		{
		}
	}
}
