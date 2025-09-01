using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200001D RID: 29
	public struct RenderGraphParameters
	{
		// Token: 0x040000CA RID: 202
		public string executionName;

		// Token: 0x040000CB RID: 203
		public int currentFrameIndex;

		// Token: 0x040000CC RID: 204
		public bool rendererListCulling;

		// Token: 0x040000CD RID: 205
		public ScriptableRenderContext scriptableRenderContext;

		// Token: 0x040000CE RID: 206
		public CommandBuffer commandBuffer;
	}
}
