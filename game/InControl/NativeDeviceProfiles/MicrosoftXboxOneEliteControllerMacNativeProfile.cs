using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000184 RID: 388
	[Preserve]
	[NativeInputDeviceProfile]
	public class MicrosoftXboxOneEliteControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x0003BB88 File Offset: 0x00039D88
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Microsoft Xbox One Elite Controller";
			base.DeviceNotes = "Microsoft Xbox One Elite Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 739
				}
			};
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0003BBFC File Offset: 0x00039DFC
		public MicrosoftXboxOneEliteControllerMacNativeProfile()
		{
		}
	}
}
