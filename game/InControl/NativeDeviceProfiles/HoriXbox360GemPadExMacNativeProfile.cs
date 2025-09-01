using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000154 RID: 340
	[Preserve]
	[NativeInputDeviceProfile]
	public class HoriXbox360GemPadExMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x00039FD4 File Offset: 0x000381D4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Hori Xbox 360 Gem Pad Ex";
			base.DeviceNotes = "Hori Xbox 360 Gem Pad Ex on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 9414,
					ProductID = 21773
				}
			};
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0003A048 File Offset: 0x00038248
		public HoriXbox360GemPadExMacNativeProfile()
		{
		}
	}
}
