using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200003A RID: 58
	[Preserve]
	[Serializable]
	public sealed class SubpixelMorphologicalAntialiasing
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public bool IsSupported()
		{
			return !RuntimeUtilities.isSinglePassStereoEnabled;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00009AAC File Offset: 0x00007CAC
		internal void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.subpixelMorphologicalAntialiasing);
			propertySheet.properties.SetTexture("_AreaTex", context.resources.smaaLuts.area);
			propertySheet.properties.SetTexture("_SearchTex", context.resources.smaaLuts.search);
			CommandBuffer command = context.command;
			command.BeginSample("SubpixelMorphologicalAntialiasing");
			bool useDynamicScale = RuntimeUtilities.IsDynamicResolutionEnabled(context.camera);
			command.GetTemporaryRT(ShaderIDs.SMAA_Flip, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat, RenderTextureReadWrite.Linear, 1, false, RenderTextureMemoryless.None, useDynamicScale);
			command.GetTemporaryRT(ShaderIDs.SMAA_Flop, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat, RenderTextureReadWrite.Linear, 1, false, RenderTextureMemoryless.None, useDynamicScale);
			command.BlitFullscreenTriangle(context.source, ShaderIDs.SMAA_Flip, propertySheet, (int)this.quality, true, null, false);
			command.BlitFullscreenTriangle(ShaderIDs.SMAA_Flip, ShaderIDs.SMAA_Flop, propertySheet, (int)(3 + this.quality), false, null, false);
			command.SetGlobalTexture("_BlendTex", ShaderIDs.SMAA_Flop);
			command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 6, false, null, false);
			command.ReleaseTemporaryRT(ShaderIDs.SMAA_Flip);
			command.ReleaseTemporaryRT(ShaderIDs.SMAA_Flop);
			command.EndSample("SubpixelMorphologicalAntialiasing");
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00009C24 File Offset: 0x00007E24
		public SubpixelMorphologicalAntialiasing()
		{
		}

		// Token: 0x0400012C RID: 300
		[Tooltip("Lower quality is faster at the expense of visual quality (Low = ~60%, Medium = ~80%).")]
		public SubpixelMorphologicalAntialiasing.Quality quality = SubpixelMorphologicalAntialiasing.Quality.High;

		// Token: 0x02000081 RID: 129
		private enum Pass
		{
			// Token: 0x04000326 RID: 806
			EdgeDetection,
			// Token: 0x04000327 RID: 807
			BlendWeights = 3,
			// Token: 0x04000328 RID: 808
			NeighborhoodBlending = 6
		}

		// Token: 0x02000082 RID: 130
		public enum Quality
		{
			// Token: 0x0400032A RID: 810
			Low,
			// Token: 0x0400032B RID: 811
			Medium,
			// Token: 0x0400032C RID: 812
			High
		}
	}
}
