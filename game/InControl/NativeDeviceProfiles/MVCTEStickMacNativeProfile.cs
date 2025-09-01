using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000187 RID: 391
	[Preserve]
	[NativeInputDeviceProfile]
	public class MVCTEStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x0003BCFC File Offset: 0x00039EFC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "MVC TE Stick";
			base.DeviceNotes = "MVC TE Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 7085,
					ProductID = 61497
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 1848,
					ProductID = 46904
				}
			};
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0003BDAF File Offset: 0x00039FAF
		public MVCTEStickMacNativeProfile()
		{
		}
	}
}
