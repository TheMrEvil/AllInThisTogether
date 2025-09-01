using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x02000010 RID: 16
	internal class Fsr3LumaInstabilityPass : Fsr3UpscalerPass
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00004678 File Offset: 0x00002878
		public Fsr3LumaInstabilityPass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants) : base(contextDescription, resources, constants)
		{
			base.InitComputeShader("Compute Luminance Instability", contextDescription.Shaders.lumaInstabilityPass);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000469C File Offset: 0x0000289C
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			ref ResourceView ptr = ref dispatchParams.Exposure;
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputExposure, ptr.RenderTarget, ptr.MipLevel, ptr.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvDilatedReactiveMasks, Fsr3ShaderIDs.UavDilatedReactiveMasks);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvDilatedMotionVectors, this.Resources.DilatedVelocity);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvFrameInfo, this.Resources.FrameInfo);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvLumaHistory, this.Resources.LumaHistory[frameIndex ^ 1]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvFarthestDepthMip1, Fsr3ShaderIDs.UavFarthestDepthMip1);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvCurrentLuma, this.Resources.Luma[frameIndex]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavLumaHistory, this.Resources.LumaHistory[frameIndex]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavLumaInstability, Fsr3ShaderIDs.UavIntermediate);
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbFsr3Upscaler, this.Constants, 0, Marshal.SizeOf<Fsr3.UpscalerConstants>());
			commandBuffer.DispatchCompute(this.ComputeShader, this.KernelIndex, dispatchX, dispatchY, 1);
		}
	}
}
