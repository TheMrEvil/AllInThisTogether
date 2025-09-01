using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000131 RID: 305
	[Preserve]
	[NativeInputDeviceProfile]
	public class BrookPS2ConverterMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x00038AF4 File Offset: 0x00036CF4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Brook PS2 Converter";
			base.DeviceNotes = "Brook PS2 Converter on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3090,
					ProductID = 2289
				}
			};
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00038B68 File Offset: 0x00036D68
		public BrookPS2ConverterMacNativeProfile()
		{
		}
	}
}
