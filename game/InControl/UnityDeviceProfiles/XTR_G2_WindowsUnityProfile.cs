using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x02000129 RID: 297
	[Preserve]
	[UnityInputDeviceProfile]
	public class XTR_G2_WindowsUnityProfile : InputDeviceProfile
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x0003869C File Offset: 0x0003689C
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
					NameLiteral = "KMODEL Simulator - XTR+G2+FMS Controller"
				}
			};
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00038708 File Offset: 0x00036908
		public XTR_G2_WindowsUnityProfile()
		{
		}
	}
}
