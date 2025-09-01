using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001CF RID: 463
	[Preserve]
	[NativeInputDeviceProfile]
	public class XTR_G2_MacNativeProfile : InputDeviceProfile
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x00043748 File Offset: 0x00041948
		public override void Define()
		{
			base.Define();
			base.DeviceName = "KMODEL Simulator XTR G2 FMS Controller";
			base.DeviceNotes = "KMODEL Simulator XTR G2 FMS Controller on OS X";
			base.DeviceClass = InputDeviceClass.Controller;
			base.IncludePlatforms = new string[]
			{
				"OS X"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 2971,
					ProductID = 16402,
					NameLiteral = "KMODEL Simulator - XTR+G2+FMS Controller"
				}
			};
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000437E3 File Offset: 0x000419E3
		public XTR_G2_MacNativeProfile()
		{
		}
	}
}
