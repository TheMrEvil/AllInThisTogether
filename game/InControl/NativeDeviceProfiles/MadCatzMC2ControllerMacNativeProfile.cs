using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200016E RID: 366
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzMC2ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x0003ADA8 File Offset: 0x00038FA8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "MadCatz MC2 Controller";
			base.DeviceNotes = "MadCatz MC2 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 18208
				}
			};
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0003AE1C File Offset: 0x0003901C
		public MadCatzMC2ControllerMacNativeProfile()
		{
		}
	}
}
