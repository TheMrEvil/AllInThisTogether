using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x02000014 RID: 20
	internal class Fsr3AutogeneratePass : Fsr3UpscalerPass
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00004D08 File Offset: 0x00002F08
		public Fsr3AutogeneratePass(Fsr3.ContextDescription contextDescription, Fsr3UpscalerResources resources, ComputeBuffer constants, ComputeBuffer tcrAutogenerateConstants) : base(contextDescription, resources, constants)
		{
			this._tcrAutogenerateConstants = tcrAutogenerateConstants;
			base.InitComputeShader("Auto-Generate Transparency & Composition Mask", contextDescription.Shaders.tcrAutoGenPass);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004D34 File Offset: 0x00002F34
		protected override void DoScheduleDispatch(CommandBuffer commandBuffer, Fsr3.DispatchDescription dispatchParams, int frameIndex, int dispatchX, int dispatchY)
		{
			ref ResourceView ptr = ref dispatchParams.Color;
			ref ResourceView ptr2 = ref dispatchParams.MotionVectors;
			ref ResourceView ptr3 = ref dispatchParams.ColorOpaqueOnly;
			ref ResourceView ptr4 = ref dispatchParams.Reactive;
			ref ResourceView ptr5 = ref dispatchParams.TransparencyAndComposition;
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvOpaqueOnly, ptr3.RenderTarget, ptr3.MipLevel, ptr3.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputColor, ptr.RenderTarget, ptr.MipLevel, ptr.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvInputMotionVectors, ptr2.RenderTarget, ptr2.MipLevel, ptr2.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvPrevColorPreAlpha, this.Resources.PrevPreAlpha[frameIndex ^ 1]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvPrevColorPostAlpha, this.Resources.PrevPostAlpha[frameIndex ^ 1]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvReactiveMask, ptr4.RenderTarget, ptr4.MipLevel, ptr4.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.SrvTransparencyAndCompositionMask, ptr5.RenderTarget, ptr5.MipLevel, ptr5.SubElement);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavAutoReactive, this.Resources.AutoReactive);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavAutoComposition, this.Resources.AutoComposition);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavPrevColorPreAlpha, this.Resources.PrevPreAlpha[frameIndex]);
			commandBuffer.SetComputeTextureParam(this.ComputeShader, this.KernelIndex, Fsr3ShaderIDs.UavPrevColorPostAlpha, this.Resources.PrevPostAlpha[frameIndex]);
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbFsr3Upscaler, this.Constants, 0, Marshal.SizeOf<Fsr3.UpscalerConstants>());
			commandBuffer.SetComputeConstantBufferParam(this.ComputeShader, Fsr3ShaderIDs.CbGenReactive, this._tcrAutogenerateConstants, 0, Marshal.SizeOf<Fsr3.GenerateReactiveConstants2>());
			commandBuffer.DispatchCompute(this.ComputeShader, this.KernelIndex, dispatchX, dispatchY, 1);
		}

		// Token: 0x04000077 RID: 119
		private readonly ComputeBuffer _tcrAutogenerateConstants;
	}
}
