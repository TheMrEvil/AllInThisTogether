using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x02000013 RID: 19
	internal class Fsr3GenerateReactivePass : Fsr3UpscalerPass
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00004BF3 File Offset: 0x00002DF3
		public Fsr3GenerateReactivePass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer generateReactiveConstants) : base(contextDescription, resources, null)
		{
			this._generateReactiveConstants = generateReactiveConstants;
			base.InitComputeShader("Auto-Generate Reactive Mask", contextDescription.Shaders.autoGenReactivePass);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004C1B File Offset: 0x00002E1B
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004C20 File Offset: 0x00002E20
		public void ScheduleDispatch(CommandBuffer commandBuffer, Fsr3.GenerateReactiveDescription dispatchParams, int dispatchX, int dispatchY)
		{
			commandBuffer.BeginSample(this.Sampler);
			ref ResourceView ptr = ref dispatchParams.ColorOpaqueOnly;
			ref ResourceView ptr2 = ref dispatchParams.ColorPreUpscale;
			ref ResourceView ptr3 = ref dispatchParams.OutReactive;
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvOpaqueOnly, ptr.RenderTarget, ptr.MipLevel, ptr.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputColor, ptr2.RenderTarget, ptr2.MipLevel, ptr2.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavAutoReactive, ptr3.RenderTarget, ptr3.MipLevel, ptr3.SubElement);
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbGenReactive, this._generateReactiveConstants, 0, Marshal.SizeOf<Fsr3.GenerateReactiveConstants>());
			commandBuffer.DispatchCompute(this.ComputeShader, this.KernelIndex, dispatchX, dispatchY, 1);
			commandBuffer.EndSample(this.Sampler);
		}

		// Token: 0x04000076 RID: 118
		private readonly ComputeBuffer _generateReactiveConstants;
	}
}
