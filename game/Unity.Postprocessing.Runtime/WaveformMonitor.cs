using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public sealed class WaveformMonitor : Monitor
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x0000AC95 File Offset: 0x00008E95
		internal override void OnDisable()
		{
			base.OnDisable();
			if (this.m_Data != null)
			{
				this.m_Data.Release();
			}
			this.m_Data = null;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000ACB7 File Offset: 0x00008EB7
		internal override bool NeedsHalfRes()
		{
			return true;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000ACBA File Offset: 0x00008EBA
		internal override bool ShaderResourcesAvailable(PostProcessRenderContext context)
		{
			return context.resources.computeShaders.waveform;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		internal override void Render(PostProcessRenderContext context)
		{
			float num = (float)context.width / 2f / ((float)context.height / 2f);
			int num2 = Mathf.FloorToInt((float)this.height * num);
			base.CheckOutput(num2, this.height);
			this.exposure = Mathf.Max(0f, this.exposure);
			int num3 = num2 * this.height;
			if (this.m_Data == null)
			{
				this.m_Data = new ComputeBuffer(num3, 16);
			}
			else if (this.m_Data.count < num3)
			{
				this.m_Data.Release();
				this.m_Data = new ComputeBuffer(num3, 16);
			}
			ComputeShader waveform = context.resources.computeShaders.waveform;
			CommandBuffer command = context.command;
			command.BeginSample("Waveform");
			Vector4 val = new Vector4((float)num2, (float)this.height, (float)(RuntimeUtilities.isLinearColorSpace ? 1 : 0), 0f);
			int kernelIndex = waveform.FindKernel("KWaveformClear");
			command.SetComputeBufferParam(waveform, kernelIndex, "_WaveformBuffer", this.m_Data);
			command.SetComputeVectorParam(waveform, "_Params", val);
			command.DispatchCompute(waveform, kernelIndex, Mathf.CeilToInt((float)num2 / 16f), Mathf.CeilToInt((float)this.height / 16f), 1);
			command.GetTemporaryRT(ShaderIDs.WaveformSource, num2, this.height, 0, FilterMode.Bilinear, context.sourceFormat);
			command.BlitFullscreenTriangle(ShaderIDs.HalfResFinalCopy, ShaderIDs.WaveformSource, false, null, false);
			kernelIndex = waveform.FindKernel("KWaveformGather");
			command.SetComputeBufferParam(waveform, kernelIndex, "_WaveformBuffer", this.m_Data);
			command.SetComputeTextureParam(waveform, kernelIndex, "_Source", ShaderIDs.WaveformSource);
			command.SetComputeVectorParam(waveform, "_Params", val);
			command.DispatchCompute(waveform, kernelIndex, num2, Mathf.CeilToInt((float)this.height / 256f), 1);
			command.ReleaseTemporaryRT(ShaderIDs.WaveformSource);
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.waveform);
			propertySheet.properties.SetVector(ShaderIDs.Params, new Vector4((float)num2, (float)this.height, this.exposure, 0f));
			propertySheet.properties.SetBuffer(ShaderIDs.WaveformBuffer, this.m_Data);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, base.output, propertySheet, 0, false, null, false);
			command.EndSample("Waveform");
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000AF4F File Offset: 0x0000914F
		public WaveformMonitor()
		{
		}

		// Token: 0x0400015E RID: 350
		public float exposure = 0.12f;

		// Token: 0x0400015F RID: 351
		public int height = 256;

		// Token: 0x04000160 RID: 352
		private ComputeBuffer m_Data;

		// Token: 0x04000161 RID: 353
		private const int k_ThreadGroupSize = 256;

		// Token: 0x04000162 RID: 354
		private const int k_ThreadGroupSizeX = 16;

		// Token: 0x04000163 RID: 355
		private const int k_ThreadGroupSizeY = 16;
	}
}
