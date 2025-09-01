using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003BB RID: 955
	public enum PassType
	{
		// Token: 0x04000B2F RID: 2863
		Normal,
		// Token: 0x04000B30 RID: 2864
		Vertex,
		// Token: 0x04000B31 RID: 2865
		VertexLM,
		// Token: 0x04000B32 RID: 2866
		[Obsolete("VertexLMRGBM PassType is obsolete. Please use VertexLM PassType together with DecodeLightmap shader function.")]
		VertexLMRGBM,
		// Token: 0x04000B33 RID: 2867
		ForwardBase,
		// Token: 0x04000B34 RID: 2868
		ForwardAdd,
		// Token: 0x04000B35 RID: 2869
		LightPrePassBase,
		// Token: 0x04000B36 RID: 2870
		LightPrePassFinal,
		// Token: 0x04000B37 RID: 2871
		ShadowCaster,
		// Token: 0x04000B38 RID: 2872
		Deferred = 10,
		// Token: 0x04000B39 RID: 2873
		Meta,
		// Token: 0x04000B3A RID: 2874
		MotionVectors,
		// Token: 0x04000B3B RID: 2875
		ScriptableRenderPipeline,
		// Token: 0x04000B3C RID: 2876
		ScriptableRenderPipelineDefaultUnlit,
		// Token: 0x04000B3D RID: 2877
		GrabPass
	}
}
