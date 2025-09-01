using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200018E RID: 398
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPTitanfall2XboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x0003C41C File Offset: 0x0003A61C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Titanfall 2 Xbox One Controller";
			base.DeviceNotes = "PDP Titanfall 2 Xbox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 357
				}
			};
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0003C490 File Offset: 0x0003A690
		public PDPTitanfall2XboxOneControllerMacNativeProfile()
		{
		}
	}
}
