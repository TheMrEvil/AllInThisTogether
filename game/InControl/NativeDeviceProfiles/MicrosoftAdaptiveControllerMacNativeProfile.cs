using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000180 RID: 384
	[Preserve]
	[NativeInputDeviceProfile]
	public class MicrosoftAdaptiveControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x0003B660 File Offset: 0x00039860
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Microsoft Adaptive Controller";
			base.DeviceNotes = "Microsoft Adaptive Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 2826
				}
			};
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0003B6D4 File Offset: 0x000398D4
		public MicrosoftAdaptiveControllerMacNativeProfile()
		{
		}
	}
}
