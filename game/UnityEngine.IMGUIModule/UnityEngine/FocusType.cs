using System;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	public enum FocusType
	{
		// Token: 0x04000079 RID: 121
		[Obsolete("FocusType.Native now behaves the same as FocusType.Passive in all OS cases. (UnityUpgradable) -> Passive", false)]
		Native,
		// Token: 0x0400007A RID: 122
		Keyboard,
		// Token: 0x0400007B RID: 123
		Passive
	}
}
