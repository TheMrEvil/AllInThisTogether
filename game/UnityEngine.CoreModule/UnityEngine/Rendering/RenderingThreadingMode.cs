using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020003DC RID: 988
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public enum RenderingThreadingMode
	{
		// Token: 0x04000C14 RID: 3092
		Direct,
		// Token: 0x04000C15 RID: 3093
		SingleThreaded,
		// Token: 0x04000C16 RID: 3094
		MultiThreaded,
		// Token: 0x04000C17 RID: 3095
		LegacyJobified,
		// Token: 0x04000C18 RID: 3096
		NativeGraphicsJobs,
		// Token: 0x04000C19 RID: 3097
		NativeGraphicsJobsWithoutRenderThread
	}
}
