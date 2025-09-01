using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000191 RID: 401
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPXbox360ControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x0003C590 File Offset: 0x0003A790
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Xbox 360 Controller";
			base.DeviceNotes = "PDP Xbox 360 Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 1281
				}
			};
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0003C604 File Offset: 0x0003A804
		public PDPXbox360ControllerMacNativeProfile()
		{
		}
	}
}
