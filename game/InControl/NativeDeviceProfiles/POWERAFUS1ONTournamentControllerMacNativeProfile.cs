using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000195 RID: 405
	[Preserve]
	[NativeInputDeviceProfile]
	public class POWERAFUS1ONTournamentControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x0003CB7C File Offset: 0x0003AD7C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "POWER A FUS1ON Tournament Controller";
			base.DeviceNotes = "POWER A FUS1ON Tournament Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21399
				}
			};
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0003CBF0 File Offset: 0x0003ADF0
		public POWERAFUS1ONTournamentControllerMacNativeProfile()
		{
		}
	}
}
