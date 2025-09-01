using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000066 RID: 102
	internal sealed class LogHistogram
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00010962 File Offset: 0x0000EB62
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0001096A File Offset: 0x0000EB6A
		public ComputeBuffer data
		{
			[CompilerGenerated]
			get
			{
				return this.<data>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<data>k__BackingField = value;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00010974 File Offset: 0x0000EB74
		public void Generate(PostProcessRenderContext context)
		{
			if (this.data == null)
			{
				this.data = new ComputeBuffer(128, 4);
			}
			Vector4 histogramScaleOffsetRes = this.GetHistogramScaleOffsetRes(context);
			ComputeShader exposureHistogram = context.resources.computeShaders.exposureHistogram;
			CommandBuffer command = context.command;
			command.BeginSample("LogHistogram");
			int kernelIndex = exposureHistogram.FindKernel("KEyeHistogramClear");
			command.SetComputeBufferParam(exposureHistogram, kernelIndex, "_HistogramBuffer", this.data);
			uint num;
			uint num2;
			uint num3;
			exposureHistogram.GetKernelThreadGroupSizes(kernelIndex, out num, out num2, out num3);
			command.DispatchCompute(exposureHistogram, kernelIndex, Mathf.CeilToInt(128f / num), 1, 1);
			kernelIndex = exposureHistogram.FindKernel("KEyeHistogram");
			command.SetComputeBufferParam(exposureHistogram, kernelIndex, "_HistogramBuffer", this.data);
			command.SetComputeTextureParam(exposureHistogram, kernelIndex, "_Source", context.source);
			command.SetComputeVectorParam(exposureHistogram, "_ScaleOffsetRes", histogramScaleOffsetRes);
			exposureHistogram.GetKernelThreadGroupSizes(kernelIndex, out num, out num2, out num3);
			command.DispatchCompute(exposureHistogram, kernelIndex, Mathf.CeilToInt(histogramScaleOffsetRes.z / 2f / num), Mathf.CeilToInt(histogramScaleOffsetRes.w / 2f / num2), 1);
			command.EndSample("LogHistogram");
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		public Vector4 GetHistogramScaleOffsetRes(PostProcessRenderContext context)
		{
			float num = 18f;
			float num2 = 1f / num;
			float y = 9f * num2;
			return new Vector4(num2, y, (float)context.width, (float)context.height);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00010AE0 File Offset: 0x0000ECE0
		public void Release()
		{
			if (this.data != null)
			{
				this.data.Release();
			}
			this.data = null;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00010AFC File Offset: 0x0000ECFC
		public LogHistogram()
		{
		}

		// Token: 0x04000219 RID: 537
		public const int rangeMin = -9;

		// Token: 0x0400021A RID: 538
		public const int rangeMax = 9;

		// Token: 0x0400021B RID: 539
		private const int k_Bins = 128;

		// Token: 0x0400021C RID: 540
		[CompilerGenerated]
		private ComputeBuffer <data>k__BackingField;
	}
}
