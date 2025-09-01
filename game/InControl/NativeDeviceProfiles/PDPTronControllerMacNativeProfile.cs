using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200018F RID: 399
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPTronControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x0003C498 File Offset: 0x0003A698
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Tron Controller";
			base.DeviceNotes = "PDP Tron Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 63747
				}
			};
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0003C50C File Offset: 0x0003A70C
		public PDPTronControllerMacNativeProfile()
		{
		}
	}
}
