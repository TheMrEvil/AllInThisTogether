using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200016C RID: 364
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzGhostReconFightingStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x0003ACB0 File Offset: 0x00038EB0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Ghost Recon Fighting Stick";
			base.DeviceNotes = "Mad Catz Ghost Recon Fighting Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61473
				}
			};
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0003AD24 File Offset: 0x00038F24
		public MadCatzGhostReconFightingStickMacNativeProfile()
		{
		}
	}
}
