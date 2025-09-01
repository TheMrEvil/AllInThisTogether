using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000020 RID: 32
	[Preserve]
	internal sealed class ColorGradingRenderer : PostProcessEffectRenderer<ColorGrading>
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00003A38 File Offset: 0x00001C38
		public override void Render(PostProcessRenderContext context)
		{
			GradingMode value = base.settings.gradingMode.value;
			bool flag = SystemInfo.supports3DRenderTextures && SystemInfo.supportsComputeShaders && context.resources.computeShaders.lut3DBaker != null && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLCore && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES3;
			if (value == GradingMode.External)
			{
				this.RenderExternalPipeline3D(context);
				return;
			}
			if (value == GradingMode.HighDefinitionRange && flag)
			{
				this.RenderHDRPipeline3D(context);
				return;
			}
			if (value == GradingMode.HighDefinitionRange)
			{
				this.RenderHDRPipeline2D(context);
				return;
			}
			this.RenderLDRPipeline2D(context);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003AC4 File Offset: 0x00001CC4
		private void RenderExternalPipeline3D(PostProcessRenderContext context)
		{
			Texture value = base.settings.externalLut.value;
			if (value == null)
			{
				return;
			}
			PropertySheet uberSheet = context.uberSheet;
			uberSheet.EnableKeyword("COLOR_GRADING_HDR_3D");
			uberSheet.properties.SetTexture(ShaderIDs.Lut3D, value);
			uberSheet.properties.SetVector(ShaderIDs.Lut3D_Params, new Vector2(1f / (float)value.width, (float)value.width - 1f));
			uberSheet.properties.SetFloat(ShaderIDs.PostExposure, RuntimeUtilities.Exp2(base.settings.postExposure.value));
			context.logLut = value;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003B70 File Offset: 0x00001D70
		private void RenderHDRPipeline3D(PostProcessRenderContext context)
		{
			this.CheckInternalLogLut();
			ComputeShader lut3DBaker = context.resources.computeShaders.lut3DBaker;
			int kernelIndex = 0;
			switch (base.settings.tonemapper.value)
			{
			case Tonemapper.None:
				kernelIndex = lut3DBaker.FindKernel("KGenLut3D_NoTonemap");
				break;
			case Tonemapper.Neutral:
				kernelIndex = lut3DBaker.FindKernel("KGenLut3D_NeutralTonemap");
				break;
			case Tonemapper.ACES:
				kernelIndex = lut3DBaker.FindKernel("KGenLut3D_AcesTonemap");
				break;
			case Tonemapper.Custom:
				kernelIndex = lut3DBaker.FindKernel("KGenLut3D_CustomTonemap");
				break;
			}
			CommandBuffer command = context.command;
			command.SetComputeTextureParam(lut3DBaker, kernelIndex, "_Output", this.m_InternalLogLut);
			command.SetComputeVectorParam(lut3DBaker, "_Size", new Vector4(33f, 0.03125f, 0f, 0f));
			Vector3 v = ColorUtilities.ComputeColorBalance(base.settings.temperature.value, base.settings.tint.value);
			command.SetComputeVectorParam(lut3DBaker, "_ColorBalance", v);
			command.SetComputeVectorParam(lut3DBaker, "_ColorFilter", base.settings.colorFilter.value);
			float x = base.settings.hueShift.value / 360f;
			float y = base.settings.saturation.value / 100f + 1f;
			float z = base.settings.contrast.value / 100f + 1f;
			command.SetComputeVectorParam(lut3DBaker, "_HueSatCon", new Vector4(x, y, z, 0f));
			Vector4 a = new Vector4(base.settings.mixerRedOutRedIn, base.settings.mixerRedOutGreenIn, base.settings.mixerRedOutBlueIn, 0f);
			Vector4 a2 = new Vector4(base.settings.mixerGreenOutRedIn, base.settings.mixerGreenOutGreenIn, base.settings.mixerGreenOutBlueIn, 0f);
			Vector4 a3 = new Vector4(base.settings.mixerBlueOutRedIn, base.settings.mixerBlueOutGreenIn, base.settings.mixerBlueOutBlueIn, 0f);
			command.SetComputeVectorParam(lut3DBaker, "_ChannelMixerRed", a / 100f);
			command.SetComputeVectorParam(lut3DBaker, "_ChannelMixerGreen", a2 / 100f);
			command.SetComputeVectorParam(lut3DBaker, "_ChannelMixerBlue", a3 / 100f);
			Vector3 vector = ColorUtilities.ColorToLift(base.settings.lift.value * 0.2f);
			Vector3 vector2 = ColorUtilities.ColorToGain(base.settings.gain.value * 0.8f);
			Vector3 vector3 = ColorUtilities.ColorToInverseGamma(base.settings.gamma.value * 0.8f);
			command.SetComputeVectorParam(lut3DBaker, "_Lift", new Vector4(vector.x, vector.y, vector.z, 0f));
			command.SetComputeVectorParam(lut3DBaker, "_InvGamma", new Vector4(vector3.x, vector3.y, vector3.z, 0f));
			command.SetComputeVectorParam(lut3DBaker, "_Gain", new Vector4(vector2.x, vector2.y, vector2.z, 0f));
			command.SetComputeTextureParam(lut3DBaker, kernelIndex, "_Curves", this.GetCurveTexture(true));
			if (base.settings.tonemapper.value == Tonemapper.Custom)
			{
				this.m_HableCurve.Init(base.settings.toneCurveToeStrength.value, base.settings.toneCurveToeLength.value, base.settings.toneCurveShoulderStrength.value, base.settings.toneCurveShoulderLength.value, base.settings.toneCurveShoulderAngle.value, base.settings.toneCurveGamma.value);
				command.SetComputeVectorParam(lut3DBaker, "_CustomToneCurve", this.m_HableCurve.uniforms.curve);
				command.SetComputeVectorParam(lut3DBaker, "_ToeSegmentA", this.m_HableCurve.uniforms.toeSegmentA);
				command.SetComputeVectorParam(lut3DBaker, "_ToeSegmentB", this.m_HableCurve.uniforms.toeSegmentB);
				command.SetComputeVectorParam(lut3DBaker, "_MidSegmentA", this.m_HableCurve.uniforms.midSegmentA);
				command.SetComputeVectorParam(lut3DBaker, "_MidSegmentB", this.m_HableCurve.uniforms.midSegmentB);
				command.SetComputeVectorParam(lut3DBaker, "_ShoSegmentA", this.m_HableCurve.uniforms.shoSegmentA);
				command.SetComputeVectorParam(lut3DBaker, "_ShoSegmentB", this.m_HableCurve.uniforms.shoSegmentB);
			}
			context.command.BeginSample("HdrColorGradingLut3D");
			int num = Mathf.CeilToInt(8.25f);
			command.DispatchCompute(lut3DBaker, kernelIndex, num, num, num);
			context.command.EndSample("HdrColorGradingLut3D");
			RenderTexture internalLogLut = this.m_InternalLogLut;
			PropertySheet uberSheet = context.uberSheet;
			uberSheet.EnableKeyword("COLOR_GRADING_HDR_3D");
			uberSheet.properties.SetTexture(ShaderIDs.Lut3D, internalLogLut);
			uberSheet.properties.SetVector(ShaderIDs.Lut3D_Params, new Vector2(1f / (float)internalLogLut.width, (float)internalLogLut.width - 1f));
			uberSheet.properties.SetFloat(ShaderIDs.PostExposure, RuntimeUtilities.Exp2(base.settings.postExposure.value));
			context.logLut = internalLogLut;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004108 File Offset: 0x00002308
		private void RenderHDRPipeline2D(PostProcessRenderContext context)
		{
			this.CheckInternalStripLut();
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.lut2DBaker);
			propertySheet.ClearKeywords();
			propertySheet.properties.SetVector(ShaderIDs.Lut2D_Params, new Vector4(32f, 0.00048828125f, 0.015625f, 1.032258f));
			Vector3 v = ColorUtilities.ComputeColorBalance(base.settings.temperature.value, base.settings.tint.value);
			propertySheet.properties.SetVector(ShaderIDs.ColorBalance, v);
			propertySheet.properties.SetVector(ShaderIDs.ColorFilter, base.settings.colorFilter.value);
			float x = base.settings.hueShift.value / 360f;
			float y = base.settings.saturation.value / 100f + 1f;
			float z = base.settings.contrast.value / 100f + 1f;
			propertySheet.properties.SetVector(ShaderIDs.HueSatCon, new Vector3(x, y, z));
			Vector3 a = new Vector3(base.settings.mixerRedOutRedIn, base.settings.mixerRedOutGreenIn, base.settings.mixerRedOutBlueIn);
			Vector3 a2 = new Vector3(base.settings.mixerGreenOutRedIn, base.settings.mixerGreenOutGreenIn, base.settings.mixerGreenOutBlueIn);
			Vector3 a3 = new Vector3(base.settings.mixerBlueOutRedIn, base.settings.mixerBlueOutGreenIn, base.settings.mixerBlueOutBlueIn);
			propertySheet.properties.SetVector(ShaderIDs.ChannelMixerRed, a / 100f);
			propertySheet.properties.SetVector(ShaderIDs.ChannelMixerGreen, a2 / 100f);
			propertySheet.properties.SetVector(ShaderIDs.ChannelMixerBlue, a3 / 100f);
			Vector3 v2 = ColorUtilities.ColorToLift(base.settings.lift.value * 0.2f);
			Vector3 v3 = ColorUtilities.ColorToGain(base.settings.gain.value * 0.8f);
			Vector3 v4 = ColorUtilities.ColorToInverseGamma(base.settings.gamma.value * 0.8f);
			propertySheet.properties.SetVector(ShaderIDs.Lift, v2);
			propertySheet.properties.SetVector(ShaderIDs.InvGamma, v4);
			propertySheet.properties.SetVector(ShaderIDs.Gain, v3);
			propertySheet.properties.SetTexture(ShaderIDs.Curves, this.GetCurveTexture(true));
			Tonemapper value = base.settings.tonemapper.value;
			if (value == Tonemapper.Custom)
			{
				propertySheet.EnableKeyword("TONEMAPPING_CUSTOM");
				this.m_HableCurve.Init(base.settings.toneCurveToeStrength.value, base.settings.toneCurveToeLength.value, base.settings.toneCurveShoulderStrength.value, base.settings.toneCurveShoulderLength.value, base.settings.toneCurveShoulderAngle.value, base.settings.toneCurveGamma.value);
				propertySheet.properties.SetVector(ShaderIDs.CustomToneCurve, this.m_HableCurve.uniforms.curve);
				propertySheet.properties.SetVector(ShaderIDs.ToeSegmentA, this.m_HableCurve.uniforms.toeSegmentA);
				propertySheet.properties.SetVector(ShaderIDs.ToeSegmentB, this.m_HableCurve.uniforms.toeSegmentB);
				propertySheet.properties.SetVector(ShaderIDs.MidSegmentA, this.m_HableCurve.uniforms.midSegmentA);
				propertySheet.properties.SetVector(ShaderIDs.MidSegmentB, this.m_HableCurve.uniforms.midSegmentB);
				propertySheet.properties.SetVector(ShaderIDs.ShoSegmentA, this.m_HableCurve.uniforms.shoSegmentA);
				propertySheet.properties.SetVector(ShaderIDs.ShoSegmentB, this.m_HableCurve.uniforms.shoSegmentB);
			}
			else if (value == Tonemapper.ACES)
			{
				propertySheet.EnableKeyword("TONEMAPPING_ACES");
			}
			else if (value == Tonemapper.Neutral)
			{
				propertySheet.EnableKeyword("TONEMAPPING_NEUTRAL");
			}
			context.command.BeginSample("HdrColorGradingLut2D");
			context.command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, this.m_InternalLdrLut, propertySheet, 2, false, null, false);
			context.command.EndSample("HdrColorGradingLut2D");
			RenderTexture internalLdrLut = this.m_InternalLdrLut;
			PropertySheet uberSheet = context.uberSheet;
			uberSheet.EnableKeyword("COLOR_GRADING_HDR_2D");
			uberSheet.properties.SetVector(ShaderIDs.Lut2D_Params, new Vector3(1f / (float)internalLdrLut.width, 1f / (float)internalLdrLut.height, (float)internalLdrLut.height - 1f));
			uberSheet.properties.SetTexture(ShaderIDs.Lut2D, internalLdrLut);
			uberSheet.properties.SetFloat(ShaderIDs.PostExposure, RuntimeUtilities.Exp2(base.settings.postExposure.value));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000466C File Offset: 0x0000286C
		private void RenderLDRPipeline2D(PostProcessRenderContext context)
		{
			this.CheckInternalStripLut();
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.lut2DBaker);
			propertySheet.ClearKeywords();
			propertySheet.properties.SetVector(ShaderIDs.Lut2D_Params, new Vector4(32f, 0.00048828125f, 0.015625f, 1.032258f));
			Vector3 v = ColorUtilities.ComputeColorBalance(base.settings.temperature.value, base.settings.tint.value);
			propertySheet.properties.SetVector(ShaderIDs.ColorBalance, v);
			propertySheet.properties.SetVector(ShaderIDs.ColorFilter, base.settings.colorFilter.value);
			float x = base.settings.hueShift.value / 360f;
			float y = base.settings.saturation.value / 100f + 1f;
			float z = base.settings.contrast.value / 100f + 1f;
			propertySheet.properties.SetVector(ShaderIDs.HueSatCon, new Vector3(x, y, z));
			Vector3 a = new Vector3(base.settings.mixerRedOutRedIn, base.settings.mixerRedOutGreenIn, base.settings.mixerRedOutBlueIn);
			Vector3 a2 = new Vector3(base.settings.mixerGreenOutRedIn, base.settings.mixerGreenOutGreenIn, base.settings.mixerGreenOutBlueIn);
			Vector3 a3 = new Vector3(base.settings.mixerBlueOutRedIn, base.settings.mixerBlueOutGreenIn, base.settings.mixerBlueOutBlueIn);
			propertySheet.properties.SetVector(ShaderIDs.ChannelMixerRed, a / 100f);
			propertySheet.properties.SetVector(ShaderIDs.ChannelMixerGreen, a2 / 100f);
			propertySheet.properties.SetVector(ShaderIDs.ChannelMixerBlue, a3 / 100f);
			Vector3 v2 = ColorUtilities.ColorToLift(base.settings.lift.value);
			Vector3 v3 = ColorUtilities.ColorToGain(base.settings.gain.value);
			Vector3 v4 = ColorUtilities.ColorToInverseGamma(base.settings.gamma.value);
			propertySheet.properties.SetVector(ShaderIDs.Lift, v2);
			propertySheet.properties.SetVector(ShaderIDs.InvGamma, v4);
			propertySheet.properties.SetVector(ShaderIDs.Gain, v3);
			propertySheet.properties.SetFloat(ShaderIDs.Brightness, (base.settings.brightness.value + 100f) / 100f);
			propertySheet.properties.SetTexture(ShaderIDs.Curves, this.GetCurveTexture(false));
			context.command.BeginSample("LdrColorGradingLut2D");
			Texture value = base.settings.ldrLut.value;
			if (value == null || value.width != value.height * value.height)
			{
				context.command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, this.m_InternalLdrLut, propertySheet, 0, false, null, false);
			}
			else
			{
				propertySheet.properties.SetVector(ShaderIDs.UserLut2D_Params, new Vector4(1f / (float)value.width, 1f / (float)value.height, (float)value.height - 1f, base.settings.ldrLutContribution));
				context.command.BlitFullscreenTriangle(value, this.m_InternalLdrLut, propertySheet, 1, false, null, false);
			}
			context.command.EndSample("LdrColorGradingLut2D");
			RenderTexture internalLdrLut = this.m_InternalLdrLut;
			PropertySheet uberSheet = context.uberSheet;
			uberSheet.EnableKeyword("COLOR_GRADING_LDR_2D");
			uberSheet.properties.SetVector(ShaderIDs.Lut2D_Params, new Vector3(1f / (float)internalLdrLut.width, 1f / (float)internalLdrLut.height, (float)internalLdrLut.height - 1f));
			uberSheet.properties.SetTexture(ShaderIDs.Lut2D, internalLdrLut);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004AD8 File Offset: 0x00002CD8
		private void CheckInternalLogLut()
		{
			if (this.m_InternalLogLut == null || !this.m_InternalLogLut.IsCreated())
			{
				RuntimeUtilities.Destroy(this.m_InternalLogLut);
				RenderTextureFormat lutFormat = ColorGradingRenderer.GetLutFormat();
				this.m_InternalLogLut = new RenderTexture(33, 33, 0, lutFormat, RenderTextureReadWrite.Linear)
				{
					name = "Color Grading Log Lut",
					dimension = TextureDimension.Tex3D,
					hideFlags = HideFlags.DontSave,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					anisoLevel = 0,
					enableRandomWrite = true,
					volumeDepth = 33,
					autoGenerateMips = false,
					useMipMap = false
				};
				this.m_InternalLogLut.Create();
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004B7C File Offset: 0x00002D7C
		private void CheckInternalStripLut()
		{
			if (this.m_InternalLdrLut == null || !this.m_InternalLdrLut.IsCreated())
			{
				RuntimeUtilities.Destroy(this.m_InternalLdrLut);
				RenderTextureFormat lutFormat = ColorGradingRenderer.GetLutFormat();
				this.m_InternalLdrLut = new RenderTexture(1024, 32, 0, lutFormat, RenderTextureReadWrite.Linear)
				{
					name = "Color Grading Strip Lut",
					hideFlags = HideFlags.DontSave,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					anisoLevel = 0,
					autoGenerateMips = false,
					useMipMap = false
				};
				this.m_InternalLdrLut.Create();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004C0C File Offset: 0x00002E0C
		private Texture2D GetCurveTexture(bool hdr)
		{
			if (this.m_GradingCurves == null)
			{
				TextureFormat curveFormat = ColorGradingRenderer.GetCurveFormat();
				this.m_GradingCurves = new Texture2D(128, 2, curveFormat, false, true)
				{
					name = "Internal Curves Texture",
					hideFlags = HideFlags.DontSave,
					anisoLevel = 0,
					wrapMode = TextureWrapMode.Clamp,
					filterMode = FilterMode.Bilinear
				};
			}
			Spline value = base.settings.hueVsHueCurve.value;
			Spline value2 = base.settings.hueVsSatCurve.value;
			Spline value3 = base.settings.satVsSatCurve.value;
			Spline value4 = base.settings.lumVsSatCurve.value;
			Spline value5 = base.settings.masterCurve.value;
			Spline value6 = base.settings.redCurve.value;
			Spline value7 = base.settings.greenCurve.value;
			Spline value8 = base.settings.blueCurve.value;
			Color[] pixels = this.m_Pixels;
			for (int i = 0; i < 128; i++)
			{
				float r = value.cachedData[i];
				float g = value2.cachedData[i];
				float b = value3.cachedData[i];
				float a = value4.cachedData[i];
				pixels[i] = new Color(r, g, b, a);
				if (!hdr)
				{
					float a2 = value5.cachedData[i];
					float r2 = value6.cachedData[i];
					float g2 = value7.cachedData[i];
					float b2 = value8.cachedData[i];
					pixels[i + 128] = new Color(r2, g2, b2, a2);
				}
			}
			this.m_GradingCurves.SetPixels(pixels);
			this.m_GradingCurves.Apply(false, false);
			return this.m_GradingCurves;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004DCA File Offset: 0x00002FCA
		private static bool IsRenderTextureFormatSupportedForLinearFiltering(RenderTextureFormat format)
		{
			return SystemInfo.IsFormatSupported(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Linear), FormatUsage.Linear);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004DDC File Offset: 0x00002FDC
		private static RenderTextureFormat GetLutFormat()
		{
			RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGBHalf;
			if (!ColorGradingRenderer.IsRenderTextureFormatSupportedForLinearFiltering(renderTextureFormat))
			{
				renderTextureFormat = RenderTextureFormat.ARGB2101010;
				if (!ColorGradingRenderer.IsRenderTextureFormatSupportedForLinearFiltering(renderTextureFormat))
				{
					renderTextureFormat = RenderTextureFormat.ARGB32;
				}
			}
			return renderTextureFormat;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004E00 File Offset: 0x00003000
		private static TextureFormat GetCurveFormat()
		{
			TextureFormat textureFormat = TextureFormat.RGBAHalf;
			if (!SystemInfo.SupportsTextureFormat(textureFormat))
			{
				textureFormat = TextureFormat.ARGB32;
			}
			return textureFormat;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004E1B File Offset: 0x0000301B
		public override void Release()
		{
			RuntimeUtilities.Destroy(this.m_InternalLdrLut);
			this.m_InternalLdrLut = null;
			RuntimeUtilities.Destroy(this.m_InternalLogLut);
			this.m_InternalLogLut = null;
			RuntimeUtilities.Destroy(this.m_GradingCurves);
			this.m_GradingCurves = null;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004E53 File Offset: 0x00003053
		public ColorGradingRenderer()
		{
		}

		// Token: 0x04000087 RID: 135
		private Texture2D m_GradingCurves;

		// Token: 0x04000088 RID: 136
		private readonly Color[] m_Pixels = new Color[256];

		// Token: 0x04000089 RID: 137
		private RenderTexture m_InternalLdrLut;

		// Token: 0x0400008A RID: 138
		private RenderTexture m_InternalLogLut;

		// Token: 0x0400008B RID: 139
		private const int k_Lut2DSize = 32;

		// Token: 0x0400008C RID: 140
		private const int k_Lut3DSize = 33;

		// Token: 0x0400008D RID: 141
		private readonly HableCurve m_HableCurve = new HableCurve();

		// Token: 0x02000076 RID: 118
		private enum Pass
		{
			// Token: 0x040002E2 RID: 738
			LutGenLDRFromScratch,
			// Token: 0x040002E3 RID: 739
			LutGenLDR,
			// Token: 0x040002E4 RID: 740
			LutGenHDR2D
		}
	}
}
