using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000018 RID: 24
	[Preserve]
	internal sealed class BloomRenderer : PostProcessEffectRenderer<Bloom>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public override void Init()
		{
			this.m_Pyramid = new BloomRenderer.Level[16];
			for (int i = 0; i < 16; i++)
			{
				this.m_Pyramid[i] = new BloomRenderer.Level
				{
					down = Shader.PropertyToID("_BloomMipDown" + i.ToString()),
					up = Shader.PropertyToID("_BloomMipUp" + i.ToString())
				};
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C5C File Offset: 0x00000E5C
		public override void Render(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("BloomPyramid");
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.bloom);
			propertySheet.properties.SetTexture(ShaderIDs.AutoExposureTex, context.autoExposureTexture);
			float num = Mathf.Clamp(base.settings.anamorphicRatio, -1f, 1f);
			float num2 = (num < 0f) ? (-num) : 0f;
			float num3 = (num > 0f) ? num : 0f;
			int num4 = Mathf.FloorToInt((float)context.screenWidth / (2f - num2));
			int num5 = Mathf.FloorToInt((float)context.screenHeight / (2f - num3));
			bool flag = context.stereoActive && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass && context.camera.stereoTargetEye == StereoTargetEyeMask.Both;
			int num6 = flag ? (num4 * 2) : num4;
			float num7 = Mathf.Log((float)Mathf.Max(num4, num5), 2f) + Mathf.Min(base.settings.diffusion.value, 10f) - 10f;
			int num8 = Mathf.FloorToInt(num7);
			int num9 = Mathf.Clamp(num8, 1, 16);
			float num10 = 0.5f + num7 - (float)num8;
			propertySheet.properties.SetFloat(ShaderIDs.SampleScale, num10);
			float num11 = Mathf.GammaToLinearSpace(base.settings.threshold.value);
			float num12 = num11 * base.settings.softKnee.value + 1E-05f;
			Vector4 value = new Vector4(num11, num11 - num12, num12 * 2f, 0.25f / num12);
			propertySheet.properties.SetVector(ShaderIDs.Threshold, value);
			float x = Mathf.GammaToLinearSpace(base.settings.clamp.value);
			propertySheet.properties.SetVector(ShaderIDs.Params, new Vector4(x, 0f, 0f, 0f));
			int num13 = base.settings.fastMode ? 1 : 0;
			RenderTargetIdentifier source = context.source;
			for (int i = 0; i < num9; i++)
			{
				int down = this.m_Pyramid[i].down;
				int up = this.m_Pyramid[i].up;
				int pass = (i == 0) ? num13 : (2 + num13);
				context.GetScreenSpaceTemporaryRT(command, down, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, num6, num5, false);
				context.GetScreenSpaceTemporaryRT(command, up, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, num6, num5, false);
				command.BlitFullscreenTriangle(source, down, propertySheet, pass, false, null, false);
				source = down;
				num6 = ((flag && num6 / 2 % 2 > 0) ? (1 + num6 / 2) : (num6 / 2));
				num6 = Mathf.Max(num6, 1);
				num5 = Mathf.Max(num5 / 2, 1);
			}
			int num14 = this.m_Pyramid[num9 - 1].down;
			for (int j = num9 - 2; j >= 0; j--)
			{
				int down2 = this.m_Pyramid[j].down;
				int up2 = this.m_Pyramid[j].up;
				command.SetGlobalTexture(ShaderIDs.BloomTex, down2);
				command.BlitFullscreenTriangle(num14, up2, propertySheet, 4 + num13, false, null, false);
				num14 = up2;
			}
			Color linear = base.settings.color.value.linear;
			float num15 = RuntimeUtilities.Exp2(base.settings.intensity.value / 10f) - 1f;
			Vector4 value2 = new Vector4(num10, num15, base.settings.dirtIntensity.value, (float)num9);
			if (context.IsDebugOverlayEnabled(DebugOverlay.BloomThreshold))
			{
				context.PushDebugOverlay(command, context.source, propertySheet, 6);
			}
			else if (context.IsDebugOverlayEnabled(DebugOverlay.BloomBuffer))
			{
				propertySheet.properties.SetVector(ShaderIDs.ColorIntensity, new Vector4(linear.r, linear.g, linear.b, num15));
				context.PushDebugOverlay(command, this.m_Pyramid[0].up, propertySheet, 7 + num13);
			}
			Texture texture = (base.settings.dirtTexture.value == null) ? RuntimeUtilities.blackTexture : base.settings.dirtTexture.value;
			float num16 = (float)texture.width / (float)texture.height;
			float num17 = (float)context.screenWidth / (float)context.screenHeight;
			Vector4 vector = new Vector4(1f, 1f, 0f, 0f);
			if (num16 > num17)
			{
				vector.x = num17 / num16;
				vector.z = (1f - vector.x) * 0.5f;
			}
			else if (num17 > num16)
			{
				vector.y = num16 / num17;
				vector.w = (1f - vector.y) * 0.5f;
			}
			PropertySheet uberSheet = context.uberSheet;
			if (base.settings.fastMode)
			{
				uberSheet.EnableKeyword("BLOOM_LOW");
			}
			else
			{
				uberSheet.EnableKeyword("BLOOM");
			}
			uberSheet.properties.SetVector(ShaderIDs.Bloom_DirtTileOffset, vector);
			uberSheet.properties.SetVector(ShaderIDs.Bloom_Settings, value2);
			uberSheet.properties.SetColor(ShaderIDs.Bloom_Color, linear);
			uberSheet.properties.SetTexture(ShaderIDs.Bloom_DirtTex, texture);
			command.SetGlobalTexture(ShaderIDs.BloomTex, num14);
			for (int k = 0; k < num9; k++)
			{
				if (this.m_Pyramid[k].down != num14)
				{
					command.ReleaseTemporaryRT(this.m_Pyramid[k].down);
				}
				if (this.m_Pyramid[k].up != num14)
				{
					command.ReleaseTemporaryRT(this.m_Pyramid[k].up);
				}
			}
			command.EndSample("BloomPyramid");
			context.bloomBufferNameID = num14;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003280 File Offset: 0x00001480
		public BloomRenderer()
		{
		}

		// Token: 0x04000051 RID: 81
		private BloomRenderer.Level[] m_Pyramid;

		// Token: 0x04000052 RID: 82
		private const int k_MaxPyramidSize = 16;

		// Token: 0x02000074 RID: 116
		private enum Pass
		{
			// Token: 0x040002D6 RID: 726
			Prefilter13,
			// Token: 0x040002D7 RID: 727
			Prefilter4,
			// Token: 0x040002D8 RID: 728
			Downsample13,
			// Token: 0x040002D9 RID: 729
			Downsample4,
			// Token: 0x040002DA RID: 730
			UpsampleTent,
			// Token: 0x040002DB RID: 731
			UpsampleBox,
			// Token: 0x040002DC RID: 732
			DebugOverlayThreshold,
			// Token: 0x040002DD RID: 733
			DebugOverlayTent,
			// Token: 0x040002DE RID: 734
			DebugOverlayBox
		}

		// Token: 0x02000075 RID: 117
		private struct Level
		{
			// Token: 0x040002DF RID: 735
			internal int down;

			// Token: 0x040002E0 RID: 736
			internal int up;
		}
	}
}
