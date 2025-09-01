using System;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000008 RID: 8
	[UsedByNativeCode]
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTexturingSettings.h")]
	[Serializable]
	public struct GPUCacheSetting
	{
		// Token: 0x0400000C RID: 12
		public GraphicsFormat format;

		// Token: 0x0400000D RID: 13
		public uint sizeInMegaBytes;
	}
}
