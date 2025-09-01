using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public sealed class HistogramMonitor : Monitor
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x0000A517 File Offset: 0x00008717
		internal override void OnDisable()
		{
			base.OnDisable();
			if (this.m_Data != null)
			{
				this.m_Data.Release();
			}
			this.m_Data = null;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000A539 File Offset: 0x00008739
		internal override bool NeedsHalfRes()
		{
			return true;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000A53C File Offset: 0x0000873C
		internal override bool ShaderResourcesAvailable(PostProcessRenderContext context)
		{
			return context.resources.computeShaders.gammaHistogram;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A554 File Offset: 0x00008754
		internal override void Render(PostProcessRenderContext context)
		{
			base.CheckOutput(this.width, this.height);
			if (this.m_Data == null)
			{
				this.m_Data = new ComputeBuffer(256, 4);
			}
			ComputeShader gammaHistogram = context.resources.computeShaders.gammaHistogram;
			CommandBuffer command = context.command;
			command.BeginSample("GammaHistogram");
			int kernelIndex = gammaHistogram.FindKernel("KHistogramClear");
			command.SetComputeBufferParam(gammaHistogram, kernelIndex, "_HistogramBuffer", this.m_Data);
			command.DispatchCompute(gammaHistogram, kernelIndex, Mathf.CeilToInt(16f), 1, 1);
			kernelIndex = gammaHistogram.FindKernel("KHistogramGather");
			Vector4 vector = new Vector4((float)(context.width / 2), (float)(context.height / 2), (float)(RuntimeUtilities.isLinearColorSpace ? 1 : 0), (float)this.channel);
			command.SetComputeVectorParam(gammaHistogram, "_Params", vector);
			command.SetComputeTextureParam(gammaHistogram, kernelIndex, "_Source", ShaderIDs.HalfResFinalCopy);
			command.SetComputeBufferParam(gammaHistogram, kernelIndex, "_HistogramBuffer", this.m_Data);
			command.DispatchCompute(gammaHistogram, kernelIndex, Mathf.CeilToInt(vector.x / 16f), Mathf.CeilToInt(vector.y / 16f), 1);
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.gammaHistogram);
			propertySheet.properties.SetVector(ShaderIDs.Params, new Vector4((float)this.width, (float)this.height, 0f, 0f));
			propertySheet.properties.SetBuffer(ShaderIDs.HistogramBuffer, this.m_Data);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, base.output, propertySheet, 0, false, null, false);
			command.EndSample("GammaHistogram");
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000A709 File Offset: 0x00008909
		public HistogramMonitor()
		{
		}

		// Token: 0x04000148 RID: 328
		public int width = 512;

		// Token: 0x04000149 RID: 329
		public int height = 256;

		// Token: 0x0400014A RID: 330
		public HistogramMonitor.Channel channel = HistogramMonitor.Channel.Master;

		// Token: 0x0400014B RID: 331
		private ComputeBuffer m_Data;

		// Token: 0x0400014C RID: 332
		private const int k_NumBins = 256;

		// Token: 0x0400014D RID: 333
		private const int k_ThreadGroupSizeX = 16;

		// Token: 0x0400014E RID: 334
		private const int k_ThreadGroupSizeY = 16;

		// Token: 0x02000084 RID: 132
		public enum Channel
		{
			// Token: 0x04000331 RID: 817
			Red,
			// Token: 0x04000332 RID: 818
			Green,
			// Token: 0x04000333 RID: 819
			Blue,
			// Token: 0x04000334 RID: 820
			Master
		}
	}
}
