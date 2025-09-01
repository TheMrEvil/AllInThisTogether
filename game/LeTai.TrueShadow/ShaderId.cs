using System;
using UnityEngine;

namespace LeTai
{
	// Token: 0x02000004 RID: 4
	public static class ShaderId
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000242C File Offset: 0x0000062C
		// Note: this type is marked as 'beforefieldinit'.
		static ShaderId()
		{
		}

		// Token: 0x04000002 RID: 2
		public static readonly int MAIN_TEX = Shader.PropertyToID("_MainTex");

		// Token: 0x04000003 RID: 3
		public static readonly int SHADOW_TEX = Shader.PropertyToID("_ShadowTex");

		// Token: 0x04000004 RID: 4
		public static readonly int CLIP_RECT = Shader.PropertyToID("_ClipRect");

		// Token: 0x04000005 RID: 5
		public static readonly int TEXTURE_SAMPLE_ADD = Shader.PropertyToID("_TextureSampleAdd");

		// Token: 0x04000006 RID: 6
		public static readonly int COLOR_MASK = Shader.PropertyToID("_ColorMask");

		// Token: 0x04000007 RID: 7
		public static readonly int STENCIL_OP = Shader.PropertyToID("_StencilOp");

		// Token: 0x04000008 RID: 8
		public static readonly int STENCIL_ID = Shader.PropertyToID("_Stencil");

		// Token: 0x04000009 RID: 9
		public static readonly int STENCIL_READ_MASK = Shader.PropertyToID("_StencilReadMask");

		// Token: 0x0400000A RID: 10
		public static readonly int OFFSET = Shader.PropertyToID("_Offset");

		// Token: 0x0400000B RID: 11
		public static readonly int OVERFLOW_ALPHA = Shader.PropertyToID("_OverflowAlpha");

		// Token: 0x0400000C RID: 12
		public static readonly int ALPHA_MULTIPLIER = Shader.PropertyToID("_AlphaMultiplier");

		// Token: 0x0400000D RID: 13
		public static readonly int SCREEN_PARAMS = Shader.PropertyToID("_ScreenParams");

		// Token: 0x0400000E RID: 14
		public static readonly int SCALE_X = Shader.PropertyToID("_ScaleX");

		// Token: 0x0400000F RID: 15
		public static readonly int SCALE_Y = Shader.PropertyToID("_ScaleY");
	}
}
