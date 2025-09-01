using System;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000027 RID: 39
	[Preserve]
	[Serializable]
	public sealed class FastApproximateAntialiasing
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00005640 File Offset: 0x00003840
		public FastApproximateAntialiasing()
		{
		}

		// Token: 0x040000A7 RID: 167
		[FormerlySerializedAs("mobileOptimized")]
		[Tooltip("Boost performances by lowering the effect quality. This setting is meant to be used on mobile and other low-end platforms but can also provide a nice performance boost on desktops and consoles.")]
		public bool fastMode;

		// Token: 0x040000A8 RID: 168
		[Tooltip("Keep alpha channel. This will slightly lower the effect quality but allows rendering against a transparent background.\nThis setting has no effect if the camera render target has no alpha channel.")]
		public bool keepAlpha;
	}
}
