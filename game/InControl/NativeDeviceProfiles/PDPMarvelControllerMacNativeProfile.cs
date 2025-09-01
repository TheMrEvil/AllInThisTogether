using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200018C RID: 396
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPMarvelControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x0003C324 File Offset: 0x0003A524
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Marvel Controller";
			base.DeviceNotes = "PDP Marvel Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 327
				}
			};
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0003C398 File Offset: 0x0003A598
		public PDPMarvelControllerMacNativeProfile()
		{
		}
	}
}
