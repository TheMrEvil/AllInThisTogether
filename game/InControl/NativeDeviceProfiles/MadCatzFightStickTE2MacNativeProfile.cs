using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000169 RID: 361
	[Preserve]
	[NativeInputDeviceProfile]
	public class MadCatzFightStickTE2MacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0003AB3C File Offset: 0x00038D3C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Mad Catz Fight Stick TE2";
			base.DeviceNotes = "Mad Catz Fight Stick TE2 on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61568
				}
			};
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0003ABB0 File Offset: 0x00038DB0
		public MadCatzFightStickTE2MacNativeProfile()
		{
		}
	}
}
