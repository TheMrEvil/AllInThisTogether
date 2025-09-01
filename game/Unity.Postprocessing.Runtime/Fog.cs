using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000028 RID: 40
	[Preserve]
	[Serializable]
	public sealed class Fog
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00005648 File Offset: 0x00003848
		internal DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000564C File Offset: 0x0000384C
		internal bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled && RenderSettings.fog && !RuntimeUtilities.scriptableRenderPipelineActive && context.resources.shaders.deferredFog && context.resources.shaders.deferredFog.isSupported && context.camera.actualRenderingPath == RenderingPath.DeferredShading;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000056B0 File Offset: 0x000038B0
		internal void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.deferredFog);
			propertySheet.ClearKeywords();
			Color c = RuntimeUtilities.isLinearColorSpace ? RenderSettings.fogColor.linear : RenderSettings.fogColor;
			propertySheet.properties.SetVector(ShaderIDs.FogColor, c);
			propertySheet.properties.SetVector(ShaderIDs.FogParams, new Vector3(RenderSettings.fogDensity, RenderSettings.fogStartDistance, RenderSettings.fogEndDistance));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, this.excludeSkybox ? 1 : 0, false, null, true);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005769 File Offset: 0x00003969
		public Fog()
		{
		}

		// Token: 0x040000A9 RID: 169
		[Tooltip("Enables the internal deferred fog pass. Actual fog settings should be set in the Lighting panel.")]
		public bool enabled = true;

		// Token: 0x040000AA RID: 170
		[Tooltip("Mark true for the fog to ignore the skybox")]
		public bool excludeSkybox = true;
	}
}
