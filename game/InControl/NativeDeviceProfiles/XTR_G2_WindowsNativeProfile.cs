using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x0200020A RID: 522
	[Preserve]
	[NativeInputDeviceProfile]
	public class XTR_G2_WindowsNativeProfile : InputDeviceProfile
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x00052B20 File Offset: 0x00050D20
		public override void Define()
		{
			base.Define();
			base.DeviceName = "KMODEL Simulator XTR G2 FMS Controller";
			base.DeviceNotes = "KMODEL Simulator XTR G2 FMS Controller on Windows";
			base.DeviceClass = InputDeviceClass.Controller;
			base.IncludePlatforms = new string[]
			{
				"Windows"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.DirectInput,
					VendorID = 2971,
					ProductID = 16402,
					NameLiteral = "KMODEL Simulator - XTR+G2+FMS Controller"
				}
			};
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00052BBB File Offset: 0x00050DBB
		public XTR_G2_WindowsNativeProfile()
		{
		}
	}
}
