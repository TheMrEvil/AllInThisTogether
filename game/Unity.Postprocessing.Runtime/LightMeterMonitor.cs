using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public sealed class LightMeterMonitor : Monitor
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x0000A72E File Offset: 0x0000892E
		internal override bool ShaderResourcesAvailable(PostProcessRenderContext context)
		{
			return context.resources.shaders.lightMeter && context.resources.shaders.lightMeter.isSupported;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000A760 File Offset: 0x00008960
		internal override void Render(PostProcessRenderContext context)
		{
			base.CheckOutput(this.width, this.height);
			LogHistogram logHistogram = context.logHistogram;
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.lightMeter);
			propertySheet.ClearKeywords();
			propertySheet.properties.SetBuffer(ShaderIDs.HistogramBuffer, logHistogram.data);
			Vector4 histogramScaleOffsetRes = logHistogram.GetHistogramScaleOffsetRes(context);
			histogramScaleOffsetRes.z = 1f / (float)this.width;
			histogramScaleOffsetRes.w = 1f / (float)this.height;
			propertySheet.properties.SetVector(ShaderIDs.ScaleOffsetRes, histogramScaleOffsetRes);
			if (context.logLut != null && this.showCurves)
			{
				propertySheet.EnableKeyword("COLOR_GRADING_HDR");
				propertySheet.properties.SetTexture(ShaderIDs.Lut3D, context.logLut);
			}
			AutoExposure autoExposure = context.autoExposure;
			if (autoExposure != null)
			{
				float num = autoExposure.filtering.value.x;
				float num2 = autoExposure.filtering.value.y;
				num2 = Mathf.Clamp(num2, 1.01f, 99f);
				num = Mathf.Clamp(num, 1f, num2 - 0.01f);
				Vector4 value = new Vector4(num * 0.01f, num2 * 0.01f, RuntimeUtilities.Exp2(autoExposure.minLuminance.value), RuntimeUtilities.Exp2(autoExposure.maxLuminance.value));
				propertySheet.EnableKeyword("AUTO_EXPOSURE");
				propertySheet.properties.SetVector(ShaderIDs.Params, value);
			}
			CommandBuffer command = context.command;
			command.BeginSample("LightMeter");
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, base.output, propertySheet, 0, false, null, false);
			command.EndSample("LightMeter");
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000A92A File Offset: 0x00008B2A
		public LightMeterMonitor()
		{
		}

		// Token: 0x0400014F RID: 335
		public int width = 512;

		// Token: 0x04000150 RID: 336
		public int height = 256;

		// Token: 0x04000151 RID: 337
		public bool showCurves = true;
	}
}
