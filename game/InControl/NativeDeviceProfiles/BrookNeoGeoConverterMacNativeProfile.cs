using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000130 RID: 304
	[Preserve]
	[NativeInputDeviceProfile]
	public class BrookNeoGeoConverterMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x00038A78 File Offset: 0x00036C78
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Brook NeoGeo Converter";
			base.DeviceNotes = "Brook NeoGeo Converter on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3090,
					ProductID = 2036
				}
			};
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00038AEC File Offset: 0x00036CEC
		public BrookNeoGeoConverterMacNativeProfile()
		{
		}
	}
}
