using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200003E RID: 62
	public enum MeshSyncState
	{
		// Token: 0x0400015F RID: 351
		Null,
		// Token: 0x04000160 RID: 352
		[Obsolete("InstanceIDMismatch is no longer used. Mesh references are not tracked by Instance ID.")]
		InstanceIDMismatch,
		// Token: 0x04000161 RID: 353
		Lightmap,
		// Token: 0x04000162 RID: 354
		InSync,
		// Token: 0x04000163 RID: 355
		NeedsRebuild
	}
}
