using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x0200000C RID: 12
	internal class Fsr3LumaPyramidPass : Fsr3UpscalerPass
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00003F88 File Offset: 0x00002188
		public Fsr3LumaPyramidPass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants, ComputeBuffer spdConstants) : base(contextDescription, resources, constants)
		{
			this._spdConstants = spdConstants;
			base.InitComputeShader("Compute Luminance Pyramid", contextDescription.Shaders.lumaPyramidPass);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003FB4 File Offset: 0x000021B4
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvCurrentLuma, this.Resources.Luma[frameIndex]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvFarthestDepth, Fsr3ShaderIDs.UavIntermediate);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavSpdAtomicCount, this.Resources.SpdAtomicCounter);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavFrameInfo, this.Resources.FrameInfo);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavSpdMip0, this.Resources.SpdMips, 0);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavSpdMip1, this.Resources.SpdMips, 1);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavSpdMip2, this.Resources.SpdMips, 2);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavSpdMip3, this.Resources.SpdMips, 3);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavSpdMip4, this.Resources.SpdMips, 4);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavSpdMip5, this.Resources.SpdMips, 5);
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbFsr3Upscaler, this.Constants, 0, Marshal.SizeOf<Fsr3.UpscalerConstants>());
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbSpd, this._spdConstants, 0, Marshal.SizeOf<Fsr3.SpdConstants>());
			commandBuffer.DispatchCompute(this.ComputeShader, this.KernelIndex, dispatchX, dispatchY, 1);
		}

		// Token: 0x04000071 RID: 113
		private readonly ComputeBuffer _spdConstants;
	}
}
