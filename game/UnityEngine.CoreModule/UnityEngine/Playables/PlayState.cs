using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000446 RID: 1094
	public enum PlayState
	{
		// Token: 0x04000E28 RID: 3624
		Paused,
		// Token: 0x04000E29 RID: 3625
		Playing,
		// Token: 0x04000E2A RID: 3626
		[Obsolete("Delayed is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		Delayed
	}
}
