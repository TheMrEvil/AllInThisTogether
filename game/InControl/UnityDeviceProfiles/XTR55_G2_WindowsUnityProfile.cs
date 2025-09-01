using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x02000128 RID: 296
	[Preserve]
	[UnityInputDeviceProfile]
	public class XTR55_G2_WindowsUnityProfile : InputDeviceProfile
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x00038628 File Offset: 0x00036828
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
					NameLiteral = "SAILI Simulator --- XTR5.5+G2+FMS Controller"
				}
			};
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00038694 File Offset: 0x00036894
		public XTR55_G2_WindowsUnityProfile()
		{
		}
	}
}
