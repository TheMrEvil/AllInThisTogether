using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000135 RID: 309
	[Preserve]
	[NativeInputDeviceProfile]
	public class FusionXboxOneControllerMacNativeProfile : XboxOneDriverMacNativeProfile
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x00038CE4 File Offset: 0x00036EE4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Fusion Xbox One Controller";
			base.DeviceNotes = "Fusion Xbox One Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21786
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 22042
				}
			};
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00038D97 File Offset: 0x00036F97
		public FusionXboxOneControllerMacNativeProfile()
		{
		}
	}
}
