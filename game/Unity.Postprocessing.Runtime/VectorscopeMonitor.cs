using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	public sealed class VectorscopeMonitor : Monitor
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x0000AA1D File Offset: 0x00008C1D
		internal override void OnDisable()
		{
			base.OnDisable();
			if (this.m_Data != null)
			{
				this.m_Data.Release();
			}
			this.m_Data = null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000AA3F File Offset: 0x00008C3F
		internal override bool NeedsHalfRes()
		{
			return true;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000AA42 File Offset: 0x00008C42
		internal override bool ShaderResourcesAvailable(PostProcessRenderContext context)
		{
			return context.resources.computeShaders.vectorscope;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000AA5C File Offset: 0x00008C5C
		internal override void Render(PostProcessRenderContext context)
		{
			base.CheckOutput(this.size, this.size);
			this.exposure = Mathf.Max(0f, this.exposure);
			int num = this.size * this.size;
			if (this.m_Data == null)
			{
				this.m_Data = new ComputeBuffer(num, 4);
			}
			else if (this.m_Data.count != num)
			{
				this.m_Data.Release();
				this.m_Data = new ComputeBuffer(num, 4);
			}
			ComputeShader vectorscope = context.resources.computeShaders.vectorscope;
			CommandBuffer command = context.command;
			command.BeginSample("Vectorscope");
			Vector4 vector = new Vector4((float)(context.width / 2), (float)(context.height / 2), (float)this.size, (float)(RuntimeUtilities.isLinearColorSpace ? 1 : 0));
			int kernelIndex = vectorscope.FindKernel("KVectorscopeClear");
			command.SetComputeBufferParam(vectorscope, kernelIndex, "_VectorscopeBuffer", this.m_Data);
			command.SetComputeVectorParam(vectorscope, "_Params", vector);
			command.DispatchCompute(vectorscope, kernelIndex, Mathf.CeilToInt((float)this.size / 16f), Mathf.CeilToInt((float)this.size / 16f), 1);
			kernelIndex = vectorscope.FindKernel("KVectorscopeGather");
			command.SetComputeBufferParam(vectorscope, kernelIndex, "_VectorscopeBuffer", this.m_Data);
			command.SetComputeTextureParam(vectorscope, kernelIndex, "_Source", ShaderIDs.HalfResFinalCopy);
			command.DispatchCompute(vectorscope, kernelIndex, Mathf.CeilToInt(vector.x / 16f), Mathf.CeilToInt(vector.y / 16f), 1);
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.vectorscope);
			propertySheet.properties.SetVector(ShaderIDs.Params, new Vector4((float)this.size, (float)this.size, this.exposure, 0f));
			propertySheet.properties.SetBuffer(ShaderIDs.VectorscopeBuffer, this.m_Data);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, base.output, propertySheet, 0, false, null, false);
			command.EndSample("Vectorscope");
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000AC77 File Offset: 0x00008E77
		public VectorscopeMonitor()
		{
		}

		// Token: 0x04000159 RID: 345
		public int size = 256;

		// Token: 0x0400015A RID: 346
		public float exposure = 0.12f;

		// Token: 0x0400015B RID: 347
		private ComputeBuffer m_Data;

		// Token: 0x0400015C RID: 348
		private const int k_ThreadGroupSizeX = 16;

		// Token: 0x0400015D RID: 349
		private const int k_ThreadGroupSizeY = 16;
	}
}
