using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000032 RID: 50
	[Preserve]
	[Serializable]
	internal sealed class ScalableAO : IAmbientOcclusionMethod
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public ScalableAO(AmbientOcclusion settings)
		{
			this.m_Settings = settings;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00008D3C File Offset: 0x00006F3C
		public DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.DepthNormals;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008D40 File Offset: 0x00006F40
		private void DoLazyInitialization(PostProcessRenderContext context)
		{
			this.m_PropertySheet = context.propertySheets.Get(context.resources.shaders.scalableAO);
			bool flag = false;
			if (this.m_Result == null || !this.m_Result.IsCreated())
			{
				this.m_Result = context.GetScreenSpaceTemporaryRT(0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, 0, 0);
				this.m_Result.hideFlags = HideFlags.DontSave;
				this.m_Result.filterMode = FilterMode.Bilinear;
				flag = true;
			}
			else if (this.m_Result.width != context.width || this.m_Result.height != context.height)
			{
				this.m_Result.Release();
				this.m_Result.width = context.width;
				this.m_Result.height = context.height;
				flag = true;
			}
			if (flag)
			{
				this.m_Result.Create();
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008E20 File Offset: 0x00007020
		private void Render(PostProcessRenderContext context, CommandBuffer cmd, int occlusionSource)
		{
			this.DoLazyInitialization(context);
			this.m_Settings.radius.value = Mathf.Max(this.m_Settings.radius.value, 0.0001f);
			bool flag = this.m_Settings.quality.value < AmbientOcclusionQuality.High;
			float value = this.m_Settings.intensity.value;
			float value2 = this.m_Settings.radius.value;
			float z = flag ? 0.5f : 1f;
			float w = (float)this.m_SampleCount[(int)this.m_Settings.quality.value];
			PropertySheet propertySheet = this.m_PropertySheet;
			propertySheet.ClearKeywords();
			propertySheet.properties.SetVector(ShaderIDs.AOParams, new Vector4(value, value2, z, w));
			propertySheet.properties.SetVector(ShaderIDs.AOColor, Color.white - this.m_Settings.color.value);
			if (context.camera.actualRenderingPath == RenderingPath.Forward && RenderSettings.fog)
			{
				propertySheet.EnableKeyword("APPLY_FORWARD_FOG");
				propertySheet.properties.SetVector(ShaderIDs.FogParams, new Vector3(RenderSettings.fogDensity, RenderSettings.fogStartDistance, RenderSettings.fogEndDistance));
			}
			int num = flag ? 2 : 1;
			int occlusionTexture = ShaderIDs.OcclusionTexture1;
			int widthOverride = context.width / num;
			int heightOverride = context.height / num;
			context.GetScreenSpaceTemporaryRT(cmd, occlusionTexture, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, FilterMode.Bilinear, widthOverride, heightOverride, false);
			cmd.BlitFullscreenTriangle(BuiltinRenderTextureType.None, occlusionTexture, propertySheet, occlusionSource, false, null, false);
			int occlusionTexture2 = ShaderIDs.OcclusionTexture2;
			context.GetScreenSpaceTemporaryRT(cmd, occlusionTexture2, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, FilterMode.Bilinear, 0, 0, false);
			cmd.BlitFullscreenTriangle(occlusionTexture, occlusionTexture2, propertySheet, 2 + occlusionSource, false, null, false);
			cmd.ReleaseTemporaryRT(occlusionTexture);
			cmd.BlitFullscreenTriangle(occlusionTexture2, this.m_Result, propertySheet, 4, false, null, false);
			cmd.ReleaseTemporaryRT(occlusionTexture2);
			if (context.IsDebugOverlayEnabled(DebugOverlay.AmbientOcclusion))
			{
				context.PushDebugOverlay(cmd, this.m_Result, propertySheet, 7);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000904C File Offset: 0x0000724C
		public void RenderAfterOpaque(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion");
			this.Render(context, command, 0);
			command.SetGlobalTexture(ShaderIDs.SAOcclusionTexture, this.m_Result);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, BuiltinRenderTextureType.CameraTarget, this.m_PropertySheet, 5, RenderBufferLoadAction.Load, null, false);
			command.EndSample("Ambient Occlusion");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000090BC File Offset: 0x000072BC
		public void RenderAmbientOnly(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion Render");
			this.Render(context, command, 1);
			command.EndSample("Ambient Occlusion Render");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000090F0 File Offset: 0x000072F0
		public void CompositeAmbientOnly(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion Composite");
			command.SetGlobalTexture(ShaderIDs.SAOcclusionTexture, this.m_Result);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, this.m_MRT, BuiltinRenderTextureType.CameraTarget, this.m_PropertySheet, 6, false, null);
			command.EndSample("Ambient Occlusion Composite");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00009157 File Offset: 0x00007357
		public void Release()
		{
			RuntimeUtilities.Destroy(this.m_Result);
			this.m_Result = null;
		}

		// Token: 0x0400010E RID: 270
		private RenderTexture m_Result;

		// Token: 0x0400010F RID: 271
		private PropertySheet m_PropertySheet;

		// Token: 0x04000110 RID: 272
		private AmbientOcclusion m_Settings;

		// Token: 0x04000111 RID: 273
		private readonly RenderTargetIdentifier[] m_MRT = new RenderTargetIdentifier[]
		{
			BuiltinRenderTextureType.GBuffer0,
			BuiltinRenderTextureType.CameraTarget
		};

		// Token: 0x04000112 RID: 274
		private readonly int[] m_SampleCount = new int[]
		{
			4,
			6,
			10,
			8,
			12
		};

		// Token: 0x0200007D RID: 125
		private enum Pass
		{
			// Token: 0x0400030E RID: 782
			OcclusionEstimationForward,
			// Token: 0x0400030F RID: 783
			OcclusionEstimationDeferred,
			// Token: 0x04000310 RID: 784
			HorizontalBlurForward,
			// Token: 0x04000311 RID: 785
			HorizontalBlurDeferred,
			// Token: 0x04000312 RID: 786
			VerticalBlur,
			// Token: 0x04000313 RID: 787
			CompositionForward,
			// Token: 0x04000314 RID: 788
			CompositionDeferred,
			// Token: 0x04000315 RID: 789
			DebugOverlay
		}
	}
}
