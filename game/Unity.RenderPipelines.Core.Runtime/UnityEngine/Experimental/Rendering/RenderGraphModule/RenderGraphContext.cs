using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200001C RID: 28
	public class RenderGraphContext
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00006FF6 File Offset: 0x000051F6
		public RenderGraphContext()
		{
		}

		// Token: 0x040000C6 RID: 198
		public ScriptableRenderContext renderContext;

		// Token: 0x040000C7 RID: 199
		public CommandBuffer cmd;

		// Token: 0x040000C8 RID: 200
		public RenderGraphObjectPool renderGraphPool;

		// Token: 0x040000C9 RID: 201
		public RenderGraphDefaultResources defaultResources;
	}
}
