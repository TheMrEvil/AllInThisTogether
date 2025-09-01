using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000349 RID: 841
	internal enum CommandType
	{
		// Token: 0x04000CF5 RID: 3317
		Draw,
		// Token: 0x04000CF6 RID: 3318
		ImmediateCull,
		// Token: 0x04000CF7 RID: 3319
		Immediate,
		// Token: 0x04000CF8 RID: 3320
		PushView,
		// Token: 0x04000CF9 RID: 3321
		PopView,
		// Token: 0x04000CFA RID: 3322
		PushScissor,
		// Token: 0x04000CFB RID: 3323
		PopScissor,
		// Token: 0x04000CFC RID: 3324
		PushRenderTexture,
		// Token: 0x04000CFD RID: 3325
		PopRenderTexture,
		// Token: 0x04000CFE RID: 3326
		BlitToPreviousRT,
		// Token: 0x04000CFF RID: 3327
		PushDefaultMaterial,
		// Token: 0x04000D00 RID: 3328
		PopDefaultMaterial
	}
}
