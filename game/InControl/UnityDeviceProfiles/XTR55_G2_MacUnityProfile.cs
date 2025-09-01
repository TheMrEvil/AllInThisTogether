using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x020000E4 RID: 228
	[Preserve]
	[UnityInputDeviceProfile]
	public class XTR55_G2_MacUnityProfile : InputDeviceProfile
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x00028584 File Offset: 0x00026784
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
					NameLiteral = "              SAILI Simulator --- XTR5.5+G2+FMS Controller"
				}
			};
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000285F0 File Offset: 0x000267F0
		public XTR55_G2_MacUnityProfile()
		{
		}
	}
}
