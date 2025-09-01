using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x020000E5 RID: 229
	[Preserve]
	[UnityInputDeviceProfile]
	public class XTR_G2_MacUnityProfile : InputDeviceProfile
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x000285F8 File Offset: 0x000267F8
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
					NameLiteral = "FeiYing Model KMODEL Simulator - XTR+G2+FMS Controller"
				}
			};
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00028664 File Offset: 0x00026864
		public XTR_G2_MacUnityProfile()
		{
		}
	}
}
