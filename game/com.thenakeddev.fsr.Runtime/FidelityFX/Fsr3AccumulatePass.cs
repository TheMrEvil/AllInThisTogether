using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x02000011 RID: 17
	internal class Fsr3AccumulatePass : Fsr3UpscalerPass
	{
		// Token: 0x06000041 RID: 65 RVA: 0x0000483B File Offset: 0x00002A3B
		public Fsr3AccumulatePass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants) : base(contextDescription, resources, constants)
		{
			base.InitComputeShader("Accumulate", contextDescription.Shaders.accumulatePass);
			this._sharpeningKeyword = new LocalKeyword(this.ComputeShader, "FFX_FSR3UPSCALER_OPTION_APPLY_SHARPENING");
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004874 File Offset: 0x00002A74
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			if (dispatchParams.EnableSharpening)
			{
				commandBuffer.EnableKeyword(this.ComputeShader, this._sharpeningKeyword);
			}
			else
			{
				commandBuffer.DisableKeyword(this.ComputeShader, this._sharpeningKeyword);
			}
			ref ResourceView ptr = ref dispatchParams.Color;
			ref ResourceView ptr2 = ref dispatchParams.Exposure;
			ref ResourceView ptr3 = ref dispatchParams.Output;
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputExposure, ptr2.RenderTarget, ptr2.MipLevel, ptr2.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvDilatedReactiveMasks, Fsr3ShaderIDs.UavDilatedReactiveMasks);
			if ((this.ContextDescription.Flags & Fsr3.InitializationFlags.EnableDisplayResolutionMotionVectors) == (Fsr3.InitializationFlags)0)
			{
				commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvDilatedMotionVectors, this.Resources.DilatedVelocity);
			}
			else
			{
				ref ResourceView ptr4 = ref dispatchParams.MotionVectors;
				commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputMotionVectors, ptr4.RenderTarget, ptr4.MipLevel, ptr4.SubElement);
			}
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInternalUpscaled, this.Resources.InternalUpscaled[frameIndex ^ 1]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvLanczosLut, this.Resources.LanczosLut);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvFarthestDepthMip1, Fsr3ShaderIDs.UavFarthestDepthMip1);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvCurrentLuma, this.Resources.Luma[frameIndex]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvLumaInstability, Fsr3ShaderIDs.UavIntermediate);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputColor, ptr.RenderTarget, ptr.MipLevel, ptr.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavInternalUpscaled, this.Resources.InternalUpscaled[frameIndex]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavUpscaledOutput, ptr3.RenderTarget, ptr3.MipLevel, ptr3.SubElement);
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbFsr3Upscaler, this.Constants, 0, Marshal.SizeOf<Fsr3.UpscalerConstants>());
			commandBuffer.DispatchCompute(this.ComputeShader, this.KernelIndex, dispatchX, dispatchY, 1);
		}

		// Token: 0x04000073 RID: 115
		private const string SharpeningKeyword = "FFX_FSR3UPSCALER_OPTION_APPLY_SHARPENING";

		// Token: 0x04000074 RID: 116
		private readonly LocalKeyword _sharpeningKeyword;
	}
}
