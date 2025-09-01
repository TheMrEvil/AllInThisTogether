using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000011 RID: 17
	internal interface IAmbientOcclusionMethod
	{
		// Token: 0x06000014 RID: 20
		DepthTextureMode GetCameraFlags();

		// Token: 0x06000015 RID: 21
		void RenderAfterOpaque(PostProcessRenderContext context);

		// Token: 0x06000016 RID: 22
		void RenderAmbientOnly(PostProcessRenderContext context);

		// Token: 0x06000017 RID: 23
		void CompositeAmbientOnly(PostProcessRenderContext context);

		// Token: 0x06000018 RID: 24
		void Release();
	}
}
