using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001CE RID: 462
	[Preserve]
	[NativeInputDeviceProfile]
	public class XTR55_G2_MacNativeProfile : InputDeviceProfile
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x000436A4 File Offset: 0x000418A4
		public override void Define()
		{
			base.Define();
			base.DeviceName = "SAILI Simulator XTR5.5 G2 FMS Controller";
			base.DeviceNotes = "SAILI Simulator XTR5.5 G2 FMS Controller on OS X";
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
					NameLiteral = "SAILI Simulator --- XTR5.5+G2+FMS Controller"
				}
			};
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0004373F File Offset: 0x0004193F
		public XTR55_G2_MacNativeProfile()
		{
		}
	}
}
