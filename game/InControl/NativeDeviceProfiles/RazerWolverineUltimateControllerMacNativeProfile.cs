using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001A3 RID: 419
	[Preserve]
	[NativeInputDeviceProfile]
	public class RazerWolverineUltimateControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x0003D3C0 File Offset: 0x0003B5C0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Razer Wolverine Ultimate Controller";
			base.DeviceNotes = "Razer Wolverine Ultimate Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5426,
					ProductID = 2580
				}
			};
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0003D434 File Offset: 0x0003B634
		public RazerWolverineUltimateControllerMacNativeProfile()
		{
		}
	}
}
