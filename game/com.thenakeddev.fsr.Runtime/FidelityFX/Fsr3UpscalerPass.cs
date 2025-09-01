using System;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x0200000A RID: 10
	internal abstract class Fsr3UpscalerPass : IDisposable
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00003CA3 File Offset: 0x00001EA3
		protected Fsr3UpscalerPass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants)
		{
			this.ContextDescription = contextDescription;
			this.Resources = resources;
			this.Constants = constants;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public virtual void Dispose()
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003CC2 File Offset: 0x00001EC2
		public void ScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			commandBuffer.BeginSample(this.Sampler);
			this.DoScheduleDispatch(commandBuffer, dispatchParams, frameIndex, dispatchX, dispatchY);
			commandBuffer.EndSample(this.Sampler);
		}

		// Token: 0x06000032 RID: 50
		protected abstract void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY);

		// Token: 0x06000033 RID: 51 RVA: 0x00003CE9 File Offset: 0x00001EE9
		protected void InitComputeShader(string passName, ComputeShader shader)
		{
			this.InitComputeShader(passName, shader, this.ContextDescription.Flags);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003D00 File Offset: 0x00001F00
		private void InitComputeShader(string passName, ComputeShader shader, Fsr3.InitializationFlags flags)
		{
			if (shader == null)
			{
				throw new MissingReferenceException("Shader for FSR3 Upscaler '" + passName + "' could not be loaded! Please ensure it is included in the project correctly.");
			}
			this.ComputeShader = shader;
			this.KernelIndex = this.ComputeShader.FindKernel("CS");
			this.Sampler = CustomSampler.Create(passName, false);
			bool flag = false;
			if ((flags & Fsr3.InitializationFlags.EnableHighDynamicRange) != (Fsr3.InitializationFlags)0)
			{
				this.ComputeShader.EnableKeyword("FFX_FSR3UPSCALER_OPTION_HDR_COLOR_INPUT");
			}
			if ((flags & Fsr3.InitializationFlags.EnableDisplayResolutionMotionVectors) == (Fsr3.InitializationFlags)0)
			{
				this.ComputeShader.EnableKeyword("FFX_FSR3UPSCALER_OPTION_LOW_RESOLUTION_MOTION_VECTORS");
			}
			if ((flags & Fsr3.InitializationFlags.EnableMotionVectorsJitterCancellation) != (Fsr3.InitializationFlags)0)
			{
				this.ComputeShader.EnableKeyword("FFX_FSR3UPSCALER_OPTION_JITTERED_MOTION_VECTORS");
			}
			if ((flags & Fsr3.InitializationFlags.EnableDepthInverted) != (Fsr3.InitializationFlags)0)
			{
				this.ComputeShader.EnableKeyword("FFX_FSR3UPSCALER_OPTION_INVERTED_DEPTH");
			}
			if (flag)
			{
				this.ComputeShader.EnableKeyword("FFX_FSR3UPSCALER_OPTION_REPROJECT_USE_LANCZOS_TYPE");
			}
			if ((flags & Fsr3.InitializationFlags.EnableFP16Usage) != (Fsr3.InitializationFlags)0)
			{
				this.ComputeShader.EnableKeyword("FFX_HALF");
			}
		}

		// Token: 0x0400006B RID: 107
		protected readonly Fsr3.ContextDescription ContextDescription;

		// Token: 0x0400006C RID: 108
		protected readonly Fsr3UpscalerResources Resources;

		// Token: 0x0400006D RID: 109
		protected readonly ComputeBuffer Constants;

		// Token: 0x0400006E RID: 110
		protected ComputeShader ComputeShader;

		// Token: 0x0400006F RID: 111
		protected int KernelIndex;

		// Token: 0x04000070 RID: 112
		protected CustomSampler Sampler;
	}
}
