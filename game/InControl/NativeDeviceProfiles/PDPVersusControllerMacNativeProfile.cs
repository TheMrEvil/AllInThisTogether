using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000190 RID: 400
	[Preserve]
	[NativeInputDeviceProfile]
	public class PDPVersusControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x0003C514 File Offset: 0x0003A714
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PDP Versus Controller";
			base.DeviceNotes = "PDP Versus Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 63748
				}
			};
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0003C588 File Offset: 0x0003A788
		public PDPVersusControllerMacNativeProfile()
		{
		}
	}
}
