using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000156 RID: 342
	[Preserve]
	[NativeInputDeviceProfile]
	public class InjusticeFightStickMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x0003A0CC File Offset: 0x000382CC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Injustice Fight Stick";
			base.DeviceNotes = "Injustice Fight Stick on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 3695,
					ProductID = 293
				}
			};
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0003A140 File Offset: 0x00038340
		public InjusticeFightStickMacNativeProfile()
		{
		}
	}
}
