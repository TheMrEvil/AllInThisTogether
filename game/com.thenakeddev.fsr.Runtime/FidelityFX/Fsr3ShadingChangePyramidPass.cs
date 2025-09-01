using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x0200000D RID: 13
	internal class Fsr3ShadingChangePyramidPass : Fsr3UpscalerPass
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000419A File Offset: 0x0000239A
		public Fsr3ShadingChangePyramidPass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants, ComputeBuffer spdConstants) : base(contextDescription, resources, constants)
		{
			this._spdConstants = spdConstants;
			base.InitComputeShader("Compute Shading Change Pyramid", contextDescription.Shaders.shadingChangePyramidPass);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000041C4 File Offset: 0x000023C4
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			ref ResourceView ptr = ref dispatchParams.Exposure;
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvCurrentLuma, this.Resources.Luma[frameIndex]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvPreviousLuma, this.Resources.Luma[frameIndex ^ 1]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvDilatedMotionVectors, this.Resources.DilatedVelocity);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputExposure, ptr.RenderTarget, ptr.MipLevel, ptr.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavSpdAtomicCount, this.Resources.SpdAtomicCounter);
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

		// Token: 0x04000072 RID: 114
		private readonly ComputeBuffer _spdConstants;
	}
}
