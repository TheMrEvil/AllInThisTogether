using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000183 RID: 387
	[Preserve]
	[NativeInputDeviceProfile]
	public class MicrosoftXboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x0003BACC File Offset: 0x00039CCC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Microsoft Xbox One Controller";
			base.DeviceNotes = "Microsoft Xbox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 721
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1118,
					ProductID = 733
				}
			};
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0003BB7F File Offset: 0x00039D7F
		public MicrosoftXboxOneControllerMacNativeProfile()
		{
		}
	}
}
