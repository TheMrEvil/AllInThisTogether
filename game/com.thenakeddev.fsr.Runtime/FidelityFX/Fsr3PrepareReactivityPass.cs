using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x0200000F RID: 15
	internal class Fsr3PrepareReactivityPass : Fsr3UpscalerPass
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00004470 File Offset: 0x00002670
		public Fsr3PrepareReactivityPass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants) : base(contextDescription, resources, constants)
		{
			base.InitComputeShader("Prepare Reactivity", contextDescription.Shaders.prepareReactivityPass);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004494 File Offset: 0x00002694
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			ref ResourceView ptr = ref dispatchParams.Exposure;
			ref ResourceView ptr2 = ref dispatchParams.Reactive;
			ref ResourceView ptr3 = ref dispatchParams.TransparencyAndComposition;
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvReconstructedPrevNearestDepth, this.Resources.ReconstructedPrevNearestDepth);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvDilatedMotionVectors, this.Resources.DilatedVelocity);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvDilatedDepth, this.Resources.DilatedDepth);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvReactiveMask, ptr2.RenderTarget, ptr2.MipLevel, ptr2.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvTransparencyAndCompositionMask, ptr3.RenderTarget, ptr3.MipLevel, ptr3.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvAccumulation, this.Resources.Accumulation[frameIndex ^ 1]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvShadingChange, Fsr3ShaderIDs.UavShadingChange);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvCurrentLuma, this.Resources.Luma[frameIndex]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputExposure, ptr.RenderTarget, ptr.MipLevel, ptr.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavAccumulation, this.Resources.Accumulation[frameIndex]);
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbFsr3Upscaler, this.Constants, 0, Marshal.SizeOf<Fsr3.UpscalerConstants>());
			commandBuffer.DispatchCompute(this.ComputeShader, this.KernelIndex, dispatchX, dispatchY, 1);
		}
	}
}
