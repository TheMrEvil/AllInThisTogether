using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000024 RID: 36
	[Preserve]
	internal sealed class DepthOfFieldRenderer : PostProcessEffectRenderer<DepthOfField>
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00004F04 File Offset: 0x00003104
		public DepthOfFieldRenderer()
		{
			for (int i = 0; i < 2; i++)
			{
				this.m_CoCHistoryTextures[i] = new RenderTexture[2];
				this.m_HistoryPingPong[i] = 0;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004F52 File Offset: 0x00003152
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004F55 File Offset: 0x00003155
		private RenderTextureFormat SelectFormat(RenderTextureFormat primary, RenderTextureFormat secondary)
		{
			if (primary.IsSupported())
			{
				return primary;
			}
			if (secondary.IsSupported())
			{
				return secondary;
			}
			return RenderTextureFormat.Default;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004F6C File Offset: 0x0000316C
		private float CalculateMaxCoCRadius(int screenHeight)
		{
			float num = (float)base.settings.kernelSize.value * 4f + 6f;
			return Mathf.Min(0.05f, num / (float)screenHeight);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004FA8 File Offset: 0x000031A8
		private RenderTexture CheckHistory(int eye, int id, PostProcessRenderContext context, RenderTextureFormat format)
		{
			RenderTexture renderTexture = this.m_CoCHistoryTextures[eye][id];
			if (this.m_ResetHistory || renderTexture == null || !renderTexture.IsCreated() || renderTexture.width != context.width || renderTexture.height != context.height)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = context.GetScreenSpaceTemporaryRT(0, format, RenderTextureReadWrite.Linear, 0, 0);
				renderTexture.name = "CoC History, Eye: " + eye.ToString() + ", ID: " + id.ToString();
				renderTexture.filterMode = FilterMode.Bilinear;
				renderTexture.Create();
				this.m_CoCHistoryTextures[eye][id] = renderTexture;
			}
			return renderTexture;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00005048 File Offset: 0x00003248
		public override void Render(PostProcessRenderContext context)
		{
			RenderTextureFormat colorFormat = context.camera.allowHDR ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.ARGB32;
			RenderTextureFormat renderTextureFormat = this.SelectFormat(RenderTextureFormat.R8, RenderTextureFormat.RHalf);
			float num = 0.024f * ((float)context.height / 1080f);
			float num2 = base.settings.focalLength.value / 1000f;
			float num3 = Mathf.Max(base.settings.focusDistance.value, num2);
			float num4 = (float)context.screenWidth / (float)context.screenHeight;
			float value = num2 * num2 / (base.settings.aperture.value * (num3 - num2) * num * 2f);
			float num5 = this.CalculateMaxCoCRadius(context.screenHeight);
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.depthOfField);
			propertySheet.properties.Clear();
			propertySheet.properties.SetFloat(ShaderIDs.Distance, num3);
			propertySheet.properties.SetFloat(ShaderIDs.LensCoeff, value);
			propertySheet.properties.SetFloat(ShaderIDs.MaxCoC, num5);
			propertySheet.properties.SetFloat(ShaderIDs.RcpMaxCoC, 1f / num5);
			propertySheet.properties.SetFloat(ShaderIDs.RcpAspect, 1f / num4);
			CommandBuffer command = context.command;
			command.BeginSample("DepthOfField");
			context.GetScreenSpaceTemporaryRT(command, ShaderIDs.CoCTex, 0, renderTextureFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, 0, 0, false);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, ShaderIDs.CoCTex, propertySheet, 0, false, null, false);
			if (context.IsTemporalAntialiasingActive())
			{
				float motionBlending = context.temporalAntialiasing.motionBlending;
				float z = this.m_ResetHistory ? 0f : motionBlending;
				Vector2 jitter = context.temporalAntialiasing.jitter;
				propertySheet.properties.SetVector(ShaderIDs.TaaParams, new Vector3(jitter.x, jitter.y, z));
			}
			else if (context.IsFSR3Active())
			{
				Vector2 jitter2 = context.superResolution3.jitter;
				propertySheet.properties.SetVector(ShaderIDs.TaaParams, new Vector3(jitter2.x, jitter2.y, this.m_ResetHistory ? 0f : 0.85f));
			}
			else if (!context.IsDLSSActive())
			{
				context.IsXeSSActive();
			}
			if (context.IsTemporalAntialiasingActive() || context.IsFSR3Active() || context.IsDLSSActive() || context.IsXeSSActive())
			{
				int num6 = this.m_HistoryPingPong[context.xrActiveEye];
				RenderTexture tex = this.CheckHistory(context.xrActiveEye, ++num6 % 2, context, renderTextureFormat);
				RenderTexture tex2 = this.CheckHistory(context.xrActiveEye, ++num6 % 2, context, renderTextureFormat);
				this.m_HistoryPingPong[context.xrActiveEye] = (num6 + 1) % 2;
				command.BlitFullscreenTriangle(tex, tex2, propertySheet, 1, false, null, false);
				command.ReleaseTemporaryRT(ShaderIDs.CoCTex);
				command.SetGlobalTexture(ShaderIDs.CoCTex, tex2);
			}
			context.GetScreenSpaceTemporaryRT(command, ShaderIDs.DepthOfFieldTex, 0, colorFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, context.width / 2, context.height / 2, false);
			command.BlitFullscreenTriangle(context.source, ShaderIDs.DepthOfFieldTex, propertySheet, 2, false, null, false);
			context.GetScreenSpaceTemporaryRT(command, ShaderIDs.DepthOfFieldTemp, 0, colorFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, context.width / 2, context.height / 2, false);
			command.BlitFullscreenTriangle(ShaderIDs.DepthOfFieldTex, ShaderIDs.DepthOfFieldTemp, propertySheet, (int)(3 + base.settings.kernelSize.value), false, null, false);
			command.BlitFullscreenTriangle(ShaderIDs.DepthOfFieldTemp, ShaderIDs.DepthOfFieldTex, propertySheet, 7, false, null, false);
			command.ReleaseTemporaryRT(ShaderIDs.DepthOfFieldTemp);
			if (context.IsDebugOverlayEnabled(DebugOverlay.DepthOfField))
			{
				context.PushDebugOverlay(command, context.source, propertySheet, 9);
			}
			command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 8, false, null, false);
			command.ReleaseTemporaryRT(ShaderIDs.DepthOfFieldTex);
			if ((!context.IsTemporalAntialiasingActive() && !context.IsFSR3Active()) || context.IsDLSSActive() || context.IsXeSSActive())
			{
				command.ReleaseTemporaryRT(ShaderIDs.CoCTex);
			}
			command.EndSample("DepthOfField");
			this.m_ResetHistory = false;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000054C0 File Offset: 0x000036C0
		public override void Release()
		{
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < this.m_CoCHistoryTextures[i].Length; j++)
				{
					RenderTexture.ReleaseTemporary(this.m_CoCHistoryTextures[i][j]);
					this.m_CoCHistoryTextures[i][j] = null;
				}
				this.m_HistoryPingPong[i] = 0;
			}
			this.ResetHistory();
		}

		// Token: 0x04000097 RID: 151
		private const int k_NumEyes = 2;

		// Token: 0x04000098 RID: 152
		private const int k_NumCoCHistoryTextures = 2;

		// Token: 0x04000099 RID: 153
		private readonly RenderTexture[][] m_CoCHistoryTextures = new RenderTexture[2][];

		// Token: 0x0400009A RID: 154
		private int[] m_HistoryPingPong = new int[2];

		// Token: 0x0400009B RID: 155
		private const float k_FilmHeight = 0.024f;

		// Token: 0x02000077 RID: 119
		private enum Pass
		{
			// Token: 0x040002E6 RID: 742
			CoCCalculation,
			// Token: 0x040002E7 RID: 743
			CoCTemporalFilter,
			// Token: 0x040002E8 RID: 744
			DownsampleAndPrefilter,
			// Token: 0x040002E9 RID: 745
			BokehSmallKernel,
			// Token: 0x040002EA RID: 746
			BokehMediumKernel,
			// Token: 0x040002EB RID: 747
			BokehLargeKernel,
			// Token: 0x040002EC RID: 748
			BokehVeryLargeKernel,
			// Token: 0x040002ED RID: 749
			PostFilter,
			// Token: 0x040002EE RID: 750
			Combine,
			// Token: 0x040002EF RID: 751
			DebugOverlay
		}
	}
}
