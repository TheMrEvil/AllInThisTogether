using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000016 RID: 22
	[Preserve]
	internal sealed class AutoExposureRenderer : PostProcessEffectRenderer<AutoExposure>
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000026C4 File Offset: 0x000008C4
		public AutoExposureRenderer()
		{
			for (int i = 0; i < 2; i++)
			{
				this.m_AutoExposurePool[i] = new RenderTexture[2];
				this.m_AutoExposurePingPong[i] = 0;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002714 File Offset: 0x00000914
		private void CheckTexture(int eye, int id)
		{
			if (this.m_AutoExposurePool[eye][id] == null || !this.m_AutoExposurePool[eye][id].IsCreated())
			{
				this.m_AutoExposurePool[eye][id] = new RenderTexture(1, 1, 0, RenderTextureFormat.RFloat)
				{
					enableRandomWrite = true
				};
				this.m_AutoExposurePool[eye][id].Create();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002770 File Offset: 0x00000970
		public override void Render(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("AutoExposureLookup");
			this.CheckTexture(context.xrActiveEye, 0);
			this.CheckTexture(context.xrActiveEye, 1);
			float num = base.settings.filtering.value.x;
			float num2 = base.settings.filtering.value.y;
			num2 = Mathf.Clamp(num2, 1.01f, 99f);
			num = Mathf.Clamp(num, 1f, num2 - 0.01f);
			float value = base.settings.minLuminance.value;
			float value2 = base.settings.maxLuminance.value;
			base.settings.minLuminance.value = Mathf.Min(value, value2);
			base.settings.maxLuminance.value = Mathf.Max(value, value2);
			bool flag = this.m_ResetHistory || !Application.isPlaying;
			string name;
			if (flag || base.settings.eyeAdaptation.value == EyeAdaptation.Fixed)
			{
				name = "KAutoExposureAvgLuminance_fixed";
			}
			else
			{
				name = "KAutoExposureAvgLuminance_progressive";
			}
			ComputeShader autoExposure = context.resources.computeShaders.autoExposure;
			int kernelIndex = autoExposure.FindKernel(name);
			command.SetComputeBufferParam(autoExposure, kernelIndex, "_HistogramBuffer", context.logHistogram.data);
			command.SetComputeVectorParam(autoExposure, "_Params1", new Vector4(num * 0.01f, num2 * 0.01f, RuntimeUtilities.Exp2(base.settings.minLuminance.value), RuntimeUtilities.Exp2(base.settings.maxLuminance.value)));
			command.SetComputeVectorParam(autoExposure, "_Params2", new Vector4(base.settings.speedDown.value, base.settings.speedUp.value, base.settings.keyValue.value, Time.deltaTime));
			command.SetComputeVectorParam(autoExposure, "_ScaleOffsetRes", context.logHistogram.GetHistogramScaleOffsetRes(context));
			if (flag)
			{
				this.m_CurrentAutoExposure = this.m_AutoExposurePool[context.xrActiveEye][0];
				command.SetComputeTextureParam(autoExposure, kernelIndex, "_Destination", this.m_CurrentAutoExposure);
				command.DispatchCompute(autoExposure, kernelIndex, 1, 1, 1);
				RuntimeUtilities.CopyTexture(command, this.m_AutoExposurePool[context.xrActiveEye][0], this.m_AutoExposurePool[context.xrActiveEye][1]);
				this.m_ResetHistory = false;
			}
			else
			{
				int num3 = this.m_AutoExposurePingPong[context.xrActiveEye];
				RenderTexture tex = this.m_AutoExposurePool[context.xrActiveEye][++num3 % 2];
				RenderTexture renderTexture = this.m_AutoExposurePool[context.xrActiveEye][++num3 % 2];
				command.SetComputeTextureParam(autoExposure, kernelIndex, "_Source", tex);
				command.SetComputeTextureParam(autoExposure, kernelIndex, "_Destination", renderTexture);
				command.DispatchCompute(autoExposure, kernelIndex, 1, 1, 1);
				this.m_AutoExposurePingPong[context.xrActiveEye] = (num3 + 1) % 2;
				this.m_CurrentAutoExposure = renderTexture;
			}
			command.EndSample("AutoExposureLookup");
			context.autoExposureTexture = this.m_CurrentAutoExposure;
			context.autoExposure = base.settings;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A9C File Offset: 0x00000C9C
		public override void Release()
		{
			foreach (RenderTexture[] array in this.m_AutoExposurePool)
			{
				for (int j = 0; j < array.Length; j++)
				{
					RuntimeUtilities.Destroy(array[j]);
				}
			}
		}

		// Token: 0x04000042 RID: 66
		private const int k_NumEyes = 2;

		// Token: 0x04000043 RID: 67
		private const int k_NumAutoExposureTextures = 2;

		// Token: 0x04000044 RID: 68
		private readonly RenderTexture[][] m_AutoExposurePool = new RenderTexture[2][];

		// Token: 0x04000045 RID: 69
		private int[] m_AutoExposurePingPong = new int[2];

		// Token: 0x04000046 RID: 70
		private RenderTexture m_CurrentAutoExposure;
	}
}
