using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000209 RID: 521
	[Preserve]
	[NativeInputDeviceProfile]
	public class XTR55_G2_WindowsNativeProfile : InputDeviceProfile
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x00052A7C File Offset: 0x00050C7C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "SAILI Simulator XTR5.5 G2 FMS Controller";
			base.DeviceNotes = "SAILI Simulator XTR5.5 G2 FMS Controller on Windows";
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
					NameLiteral = "SAILI Simulator --- XTR5.5+G2+FMS Controller"
				}
			};
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00052B17 File Offset: 0x00050D17
		public XTR55_G2_WindowsNativeProfile()
		{
		}
	}
}
