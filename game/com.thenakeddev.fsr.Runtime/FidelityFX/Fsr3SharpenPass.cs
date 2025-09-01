using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x02000012 RID: 18
	internal class Fsr3SharpenPass : Fsr3UpscalerPass
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00004AE2 File Offset: 0x00002CE2
		public Fsr3SharpenPass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants, ComputeBuffer rcasConstants) : base(contextDescription, resources, constants)
		{
			this._rcasConstants = rcasConstants;
			base.InitComputeShader("RCAS Sharpening", contextDescription.Shaders.sharpenPass);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004B0C File Offset: 0x00002D0C
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			ref ResourceView ptr = ref dispatchParams.Exposure;
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputExposure, ptr.RenderTarget, ptr.MipLevel, ptr.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvRcasInput, this.Resources.InternalUpscaled[frameIndex]);
			ref ResourceView ptr2 = ref dispatchParams.Output;
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavUpscaledOutput, ptr2.RenderTarget, ptr2.MipLevel, ptr2.SubElement);
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbFsr3Upscaler, this.Constants, 0, Marshal.SizeOf<Fsr3.UpscalerConstants>());
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbRcas, this._rcasConstants, 0, Marshal.SizeOf<Fsr3.RcasConstants>());
			commandBuffer.DispatchCompute(this.ComputeShader, this.KernelIndex, dispatchX, dispatchY, 1);
		}

		// Token: 0x04000075 RID: 117
		private readonly ComputeBuffer _rcasConstants;
	}
}
