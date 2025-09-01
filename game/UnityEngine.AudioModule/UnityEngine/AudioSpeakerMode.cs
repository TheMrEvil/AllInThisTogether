using System;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	public enum AudioSpeakerMode
	{
		// Token: 0x04000002 RID: 2
		[Obsolete("Raw speaker mode is not supported. Do not use.", true)]
		Raw,
		// Token: 0x04000003 RID: 3
		Mono,
		// Token: 0x04000004 RID: 4
		Stereo,
		// Token: 0x04000005 RID: 5
		Quad,
		// Token: 0x04000006 RID: 6
		Surround,
		// Token: 0x04000007 RID: 7
		Mode5point1,
		// Token: 0x04000008 RID: 8
		Mode7point1,
		// Token: 0x04000009 RID: 9
		Prologic
	}
}
