using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200018B RID: 395
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPBattlefieldXBoxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x0003C2A8 File Offset: 0x0003A4A8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Battlefield XBox One Controller";
			base.DeviceNotes = "PDP Battlefield XBox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 356
				}
			};
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0003C31C File Offset: 0x0003A51C
		public PDPBattlefieldXBoxOneControllerMacNativeProfile()
		{
		}
	}
}
