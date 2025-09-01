using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000365 RID: 869
	internal struct MatchResultInfo
	{
		// Token: 0x06001C2A RID: 7210 RVA: 0x00084C06 File Offset: 0x00082E06
		public MatchResultInfo(bool success, PseudoStates triggerPseudoMask, PseudoStates dependencyPseudoMask)
		{
			this.success = success;
			this.triggerPseudoMask = triggerPseudoMask;
			this.dependencyPseudoMask = dependencyPseudoMask;
		}

		// Token: 0x04000E03 RID: 3587
		public readonly bool success;

		// Token: 0x04000E04 RID: 3588
		public readonly PseudoStates triggerPseudoMask;

		// Token: 0x04000E05 RID: 3589
		public readonly PseudoStates dependencyPseudoMask;
	}
}
