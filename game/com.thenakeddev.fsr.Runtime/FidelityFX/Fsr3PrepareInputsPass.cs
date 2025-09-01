using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x0200000B RID: 11
	internal class Fsr3PrepareInputsPass : Fsr3UpscalerPass
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00003DD6 File Offset: 0x00001FD6
		public Fsr3PrepareInputsPass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants) : base(contextDescription, resources, constants)
		{
			base.InitComputeShader("Prepare Inputs", contextDescription.Shaders.prepareInputsPass);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003DF8 File Offset: 0x00001FF8
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			ref ResourceView ptr = ref dispatchParams.Color;
			ref ResourceView ptr2 = ref dispatchParams.Depth;
			ref ResourceView ptr3 = ref dispatchParams.MotionVectors;
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputColor, ptr.RenderTarget, ptr.MipLevel, ptr.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputDepth, ptr2.RenderTarget, ptr2.MipLevel, ptr2.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputMotionVectors, ptr3.RenderTarget, ptr3.MipLevel, ptr3.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavDilatedMotionVectors, this.Resources.DilatedVelocity);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavDilatedDepth, this.Resources.DilatedDepth);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavReconstructedPrevNearestDepth, this.Resources.ReconstructedPrevNearestDepth);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavFarthestDepth, Fsr3ShaderIDs.UavIntermediate);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavCurrentLuma, this.Resources.Luma[frameIndex]);
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbFsr3Upscaler, this.Constants, 0, Marshal.SizeOf<Fsr3.UpscalerConstants>());
			commandBuffer.DispatchCompute(this.ComputeShader, this.KernelIndex, dispatchX, dispatchY, 1);
		}
	}
}
