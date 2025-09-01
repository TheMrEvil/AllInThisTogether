using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x0200000E RID: 14
	internal class Fsr3ShadingChangePass : Fsr3UpscalerPass
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000043E4 File Offset: 0x000025E4
		public Fsr3ShadingChangePass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants) : base(contextDescription, resources, constants)
		{
			base.InitComputeShader("Compute Shading Change", contextDescription.Shaders.shadingChangePass);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004408 File Offset: 0x00002608
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvSpdMips, this.Resources.SpdMips);
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbFsr3Upscaler, this.Constants, 0, Marshal.SizeOf<Fsr3.UpscalerConstants>());
			commandBuffer.DispatchCompute(this.ComputeShader, this.KernelIndex, dispatchX, dispatchY, 1);
		}
	}
}
