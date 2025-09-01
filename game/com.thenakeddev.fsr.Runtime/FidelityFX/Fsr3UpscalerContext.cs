using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x02000009 RID: 9
	public class Fsr3UpscalerContext
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002801 File Offset: 0x00000A01
		private ref Fsr3.UpscalerConstants UpscalerConsts
		{
			get
			{
				return ref this._upscalerConstantsArray[0];
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000280F File Offset: 0x00000A0F
		private ref Fsr3.SpdConstants SpdConsts
		{
			get
			{
				return ref this._spdConstantsArray[0];
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000281D File Offset: 0x00000A1D
		private ref Fsr3.RcasConstants RcasConsts
		{
			get
			{
				return ref this._rcasConstantsArray[0];
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000282B File Offset: 0x00000A2B
		private ref Fsr3.GenerateReactiveConstants GenReactiveConsts
		{
			get
			{
				return ref this._generateReactiveConstantsArray[0];
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002839 File Offset: 0x00000A39
		private ref Fsr3.GenerateReactiveConstants2 TcrAutoGenConsts
		{
			get
			{
				return ref this._tcrAutogenerateConstantsArray[0];
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002848 File Offset: 0x00000A48
		public void Create(Fsr3.ContextDescription contextDescription)
		{
			this._contextDescription = contextDescription;
			this._commandBuffer = new CommandBuffer
			{
				name = "FSR3 Upscaler"
			};
			this._upscalerConstantsBuffer = Fsr3UpscalerContext.CreateConstantBuffer<Fsr3.UpscalerConstants>();
			this._spdConstantsBuffer = Fsr3UpscalerContext.CreateConstantBuffer<Fsr3.SpdConstants>();
			this._rcasConstantsBuffer = Fsr3UpscalerContext.CreateConstantBuffer<Fsr3.RcasConstants>();
			this._generateReactiveConstantsBuffer = Fsr3UpscalerContext.CreateConstantBuffer<Fsr3.GenerateReactiveConstants>();
			this._tcrAutogenerateConstantsBuffer = Fsr3UpscalerContext.CreateConstantBuffer<Fsr3.GenerateReactiveConstants2>();
			this._firstExecution = true;
			this._resourceFrameIndex = 0;
			this.UpscalerConsts.maxUpscaleSize = this._contextDescription.MaxUpscaleSize;
			this._resources.Create(this._contextDescription);
			this.CreatePasses();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028E4 File Offset: 0x00000AE4
		private void CreatePasses()
		{
			this._prepareInputsPass = new Fsr3PrepareInputsPass(this._contextDescription, this._resources, this._upscalerConstantsBuffer);
			this._lumaPyramidPass = new Fsr3LumaPyramidPass(this._contextDescription, this._resources, this._upscalerConstantsBuffer, this._spdConstantsBuffer);
			this._shadingChangePyramidPass = new Fsr3ShadingChangePyramidPass(this._contextDescription, this._resources, this._upscalerConstantsBuffer, this._spdConstantsBuffer);
			this._shadingChangePass = new Fsr3ShadingChangePass(this._contextDescription, this._resources, this._upscalerConstantsBuffer);
			this._prepareReactivityPass = new Fsr3PrepareReactivityPass(this._contextDescription, this._resources, this._upscalerConstantsBuffer);
			this._lumaInstabilityPass = new Fsr3LumaInstabilityPass(this._contextDescription, this._resources, this._upscalerConstantsBuffer);
			this._accumulatePass = new Fsr3AccumulatePass(this._contextDescription, this._resources, this._upscalerConstantsBuffer);
			this._sharpenPass = new Fsr3SharpenPass(this._contextDescription, this._resources, this._upscalerConstantsBuffer, this._rcasConstantsBuffer);
			this._generateReactivePass = new Fsr3GenerateReactivePass(this._contextDescription, this._resources, this._generateReactiveConstantsBuffer);
			this._tcrAutogeneratePass = new Fsr3AutogeneratePass(this._contextDescription, this._resources, this._upscalerConstantsBuffer, this._tcrAutogenerateConstantsBuffer);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A2C File Offset: 0x00000C2C
		public void Destroy()
		{
			Fsr3UpscalerContext.DestroyPass(ref this._tcrAutogeneratePass);
			Fsr3UpscalerContext.DestroyPass(ref this._generateReactivePass);
			Fsr3UpscalerContext.DestroyPass(ref this._lumaPyramidPass);
			Fsr3UpscalerContext.DestroyPass(ref this._sharpenPass);
			Fsr3UpscalerContext.DestroyPass(ref this._accumulatePass);
			Fsr3UpscalerContext.DestroyPass(ref this._prepareReactivityPass);
			Fsr3UpscalerContext.DestroyPass(ref this._shadingChangePass);
			Fsr3UpscalerContext.DestroyPass(ref this._shadingChangePyramidPass);
			Fsr3UpscalerContext.DestroyPass(ref this._lumaInstabilityPass);
			Fsr3UpscalerContext.DestroyPass(ref this._prepareInputsPass);
			this._resources.Destroy();
			Fsr3UpscalerContext.DestroyConstantBuffer(ref this._tcrAutogenerateConstantsBuffer);
			Fsr3UpscalerContext.DestroyConstantBuffer(ref this._generateReactiveConstantsBuffer);
			Fsr3UpscalerContext.DestroyConstantBuffer(ref this._rcasConstantsBuffer);
			Fsr3UpscalerContext.DestroyConstantBuffer(ref this._spdConstantsBuffer);
			Fsr3UpscalerContext.DestroyConstantBuffer(ref this._upscalerConstantsBuffer);
			if (this._commandBuffer != null)
			{
				this._commandBuffer.Dispose();
				this._commandBuffer = null;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B03 File Offset: 0x00000D03
		public void Dispatch(Fsr3.DispatchDescription dispatchParams)
		{
			this._commandBuffer.Clear();
			this.Dispatch(dispatchParams, this._commandBuffer);
			Graphics.ExecuteCommandBuffer(this._commandBuffer);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002B28 File Offset: 0x00000D28
		public void Dispatch(Fsr3.DispatchDescription dispatchParams, CommandBuffer commandBuffer)
		{
			if ((this._contextDescription.Flags & Fsr3.InitializationFlags.EnableDebugChecking) != (Fsr3.InitializationFlags)0)
			{
				this.DebugCheckDispatch(dispatchParams);
			}
			if (dispatchParams.UseTextureArrays)
			{
				commandBuffer.EnableShaderKeyword("UNITY_FSR_TEXTURE2D_X_ARRAY");
			}
			if (this._firstExecution)
			{
				commandBuffer.SetRenderTarget(this._resources.Accumulation[0]);
				commandBuffer.ClearRenderTarget(false, true, Color.clear);
				commandBuffer.SetRenderTarget(this._resources.Accumulation[1]);
				commandBuffer.ClearRenderTarget(false, true, Color.clear);
				commandBuffer.SetRenderTarget(this._resources.Luma[0]);
				commandBuffer.ClearRenderTarget(false, true, Color.clear);
				commandBuffer.SetRenderTarget(this._resources.Luma[1]);
				commandBuffer.ClearRenderTarget(false, true, Color.clear);
			}
			int num = this._resourceFrameIndex % 2;
			bool flag = dispatchParams.Reset || this._firstExecution;
			this._firstExecution = false;
			if ((this._contextDescription.Flags & Fsr3.InitializationFlags.EnableAutoExposure) != (Fsr3.InitializationFlags)0)
			{
				RenderTargetIdentifier renderTargetIdentifier = this._resources.FrameInfo;
				dispatchParams.Exposure = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			else if (!dispatchParams.Exposure.IsValid)
			{
				RenderTargetIdentifier renderTargetIdentifier = this._resources.DefaultExposure;
				dispatchParams.Exposure = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			if (dispatchParams.EnableAutoReactive)
			{
				if (this._resources.AutoReactive == null)
				{
					this._resources.CreateTcrAutogenResources(this._contextDescription);
				}
				if (flag)
				{
					RenderTargetIdentifier dest = dispatchParams.ColorOpaqueOnly.IsValid ? dispatchParams.ColorOpaqueOnly.RenderTarget : Fsr3ShaderIDs.SrvOpaqueOnly;
					commandBuffer.Blit(this._resources.PrevPreAlpha[num ^ 1], dest);
				}
			}
			else if (this._resources.AutoReactive != null)
			{
				this._resources.DestroyTcrAutogenResources();
			}
			if (!dispatchParams.Reactive.IsValid)
			{
				RenderTargetIdentifier renderTargetIdentifier = this._resources.DefaultReactive;
				dispatchParams.Reactive = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			if (!dispatchParams.TransparencyAndComposition.IsValid)
			{
				RenderTargetIdentifier renderTargetIdentifier = this._resources.DefaultReactive;
				dispatchParams.TransparencyAndComposition = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			Fsr3UpscalerResources.CreateAliasableResources(commandBuffer, this._contextDescription, dispatchParams);
			this.SetupConstants(dispatchParams, flag);
			int dispatchX = (this.UpscalerConsts.renderSize.x + 7) / 8;
			int dispatchY = (this.UpscalerConsts.renderSize.y + 7) / 8;
			int dispatchX2 = (this.UpscalerConsts.upscaleSize.x + 7) / 8;
			int dispatchY2 = (this.UpscalerConsts.upscaleSize.y + 7) / 8;
			int dispatchX3 = (this.UpscalerConsts.renderSize.x / 2 + 7) / 8;
			int dispatchY3 = (this.UpscalerConsts.renderSize.y / 2 + 7) / 8;
			if (flag)
			{
				commandBuffer.SetRenderTarget(this._resources.Accumulation[num ^ 1]);
				commandBuffer.ClearRenderTarget(false, true, Color.clear);
				commandBuffer.SetRenderTarget(this._resources.SpdMips);
				commandBuffer.ClearRenderTarget(false, true, Color.clear);
				commandBuffer.SetRenderTarget(this._resources.FrameInfo);
				commandBuffer.ClearRenderTarget(false, true, new Color(0f, 1f, 0f, 0f));
				commandBuffer.SetRenderTarget(this._resources.SpdAtomicCounter);
				commandBuffer.ClearRenderTarget(false, true, Color.clear);
			}
			bool flag2 = (this._contextDescription.Flags & Fsr3.InitializationFlags.EnableDepthInverted) == Fsr3.InitializationFlags.EnableDepthInverted;
			commandBuffer.SetRenderTarget(this._resources.ReconstructedPrevNearestDepth);
			commandBuffer.ClearRenderTarget(false, true, flag2 ? Color.clear : Color.white);
			Vector2Int vector2Int;
			this.SetupSpdConstants(dispatchParams, out vector2Int);
			commandBuffer.SetBufferData(this._upscalerConstantsBuffer, this._upscalerConstantsArray);
			commandBuffer.SetBufferData(this._spdConstantsBuffer, this._spdConstantsArray);
			if (dispatchParams.EnableAutoReactive)
			{
				this.GenerateTransparencyCompositionReactive(dispatchParams, commandBuffer, num);
				RenderTargetIdentifier renderTargetIdentifier = this._resources.AutoReactive;
				dispatchParams.Reactive = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				renderTargetIdentifier = this._resources.AutoComposition;
				dispatchParams.TransparencyAndComposition = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			this._prepareInputsPass.ScheduleDispatch(commandBuffer, dispatchParams, num, dispatchX, dispatchY);
			this._lumaPyramidPass.ScheduleDispatch(commandBuffer, dispatchParams, num, vector2Int.x, vector2Int.y);
			this._shadingChangePyramidPass.ScheduleDispatch(commandBuffer, dispatchParams, num, vector2Int.x, vector2Int.y);
			this._shadingChangePass.ScheduleDispatch(commandBuffer, dispatchParams, num, dispatchX3, dispatchY3);
			this._prepareReactivityPass.ScheduleDispatch(commandBuffer, dispatchParams, num, dispatchX, dispatchY);
			this._lumaInstabilityPass.ScheduleDispatch(commandBuffer, dispatchParams, num, dispatchX, dispatchY);
			this._accumulatePass.ScheduleDispatch(commandBuffer, dispatchParams, num, dispatchX2, dispatchY2);
			if (dispatchParams.EnableSharpening)
			{
				this.SetupRcasConstants(dispatchParams);
				commandBuffer.SetBufferData(this._rcasConstantsBuffer, this._rcasConstantsArray);
				int dispatchX4 = (this.UpscalerConsts.upscaleSize.x + 16 - 1) / 16;
				int dispatchY4 = (this.UpscalerConsts.upscaleSize.y + 16 - 1) / 16;
				this._sharpenPass.ScheduleDispatch(commandBuffer, dispatchParams, num, dispatchX4, dispatchY4);
			}
			this._resourceFrameIndex = (this._resourceFrameIndex + 1) % 16;
			Fsr3UpscalerResources.DestroyAliasableResources(commandBuffer);
			commandBuffer.DisableShaderKeyword("UNITY_FSR_TEXTURE2D_X_ARRAY");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003086 File Offset: 0x00001286
		public void GenerateReactiveMask(Fsr3.GenerateReactiveDescription dispatchParams)
		{
			this._commandBuffer.Clear();
			this.GenerateReactiveMask(dispatchParams, this._commandBuffer);
			Graphics.ExecuteCommandBuffer(this._commandBuffer);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000030AC File Offset: 0x000012AC
		public void GenerateReactiveMask(Fsr3.GenerateReactiveDescription dispatchParams, CommandBuffer commandBuffer)
		{
			int dispatchX = (dispatchParams.RenderSize.x + 7) / 8;
			int dispatchY = (dispatchParams.RenderSize.y + 7) / 8;
			this.GenReactiveConsts.scale = dispatchParams.Scale;
			this.GenReactiveConsts.threshold = dispatchParams.CutoffThreshold;
			this.GenReactiveConsts.binaryValue = dispatchParams.BinaryValue;
			this.GenReactiveConsts.flags = (uint)dispatchParams.Flags;
			commandBuffer.SetBufferData(this._generateReactiveConstantsBuffer, this._generateReactiveConstantsArray);
			((Fsr3GenerateReactivePass)this._generateReactivePass).ScheduleDispatch(commandBuffer, dispatchParams, dispatchX, dispatchY);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003144 File Offset: 0x00001344
		private void GenerateTransparencyCompositionReactive(Fsr3.DispatchDescription dispatchParams, CommandBuffer commandBuffer, int frameIndex)
		{
			int dispatchX = (dispatchParams.RenderSize.x + 7) / 8;
			int dispatchY = (dispatchParams.RenderSize.y + 7) / 8;
			this.TcrAutoGenConsts.autoTcThreshold = dispatchParams.AutoTcThreshold;
			this.TcrAutoGenConsts.autoTcScale = dispatchParams.AutoTcScale;
			this.TcrAutoGenConsts.autoReactiveScale = dispatchParams.AutoReactiveScale;
			this.TcrAutoGenConsts.autoReactiveMax = dispatchParams.AutoReactiveMax;
			commandBuffer.SetBufferData(this._tcrAutogenerateConstantsBuffer, this._tcrAutogenerateConstantsArray);
			this._tcrAutogeneratePass.ScheduleDispatch(commandBuffer, dispatchParams, frameIndex, dispatchX, dispatchY);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000031D8 File Offset: 0x000013D8
		private void SetupConstants(Fsr3.DispatchDescription dispatchParams, bool resetAccumulation)
		{
			ref Fsr3.UpscalerConstants upscalerConsts = ref this.UpscalerConsts;
			upscalerConsts.previousFrameJitterOffset = upscalerConsts.jitterOffset;
			upscalerConsts.jitterOffset = dispatchParams.JitterOffset;
			upscalerConsts.previousFrameRenderSize = upscalerConsts.renderSize;
			upscalerConsts.renderSize = dispatchParams.RenderSize;
			upscalerConsts.maxRenderSize = this._contextDescription.MaxRenderSize;
			float num = (float)dispatchParams.RenderSize.x / (float)dispatchParams.RenderSize.y;
			float num2 = Mathf.Atan(Mathf.Tan(dispatchParams.CameraFovAngleVertical / 2f) * num) * 2f;
			upscalerConsts.tanHalfFOV = Mathf.Tan(num2 * 0.5f);
			upscalerConsts.viewSpaceToMetersFactor = ((dispatchParams.ViewSpaceToMetersFactor > 0f) ? dispatchParams.ViewSpaceToMetersFactor : 1f);
			upscalerConsts.deviceToViewDepth = this.SetupDeviceDepthToViewSpaceDepthParams(dispatchParams);
			upscalerConsts.previousFrameUpscaleSize = upscalerConsts.upscaleSize;
			if (dispatchParams.UpscaleSize.x == 0 && dispatchParams.UpscaleSize.y == 0)
			{
				upscalerConsts.upscaleSize = this._contextDescription.MaxUpscaleSize;
			}
			else
			{
				upscalerConsts.upscaleSize = dispatchParams.UpscaleSize;
			}
			upscalerConsts.downscaleFactor = new Vector2((float)upscalerConsts.renderSize.x / (float)upscalerConsts.upscaleSize.x, (float)upscalerConsts.renderSize.y / (float)upscalerConsts.upscaleSize.y);
			upscalerConsts.deltaPreExposure = 1f;
			this._previousFramePreExposure = this._preExposure;
			this._preExposure = ((dispatchParams.PreExposure != 0f) ? dispatchParams.PreExposure : 1f);
			if (this._previousFramePreExposure > 0f)
			{
				upscalerConsts.deltaPreExposure = this._preExposure / this._previousFramePreExposure;
			}
			Vector2Int v = ((this._contextDescription.Flags & Fsr3.InitializationFlags.EnableDisplayResolutionMotionVectors) != (Fsr3.InitializationFlags)0) ? upscalerConsts.upscaleSize : upscalerConsts.renderSize;
			upscalerConsts.motionVectorScale = dispatchParams.MotionVectorScale / v;
			if ((this._contextDescription.Flags & Fsr3.InitializationFlags.EnableMotionVectorsJitterCancellation) != (Fsr3.InitializationFlags)0)
			{
				upscalerConsts.motionVectorJitterCancellation = (this._previousJitterOffset - upscalerConsts.jitterOffset) / v;
				this._previousJitterOffset = upscalerConsts.jitterOffset;
			}
			int jitterPhaseCount = Fsr3.GetJitterPhaseCount(dispatchParams.RenderSize.x, this._contextDescription.MaxUpscaleSize.x);
			if (resetAccumulation || upscalerConsts.jitterPhaseCount == 0f)
			{
				upscalerConsts.jitterPhaseCount = (float)jitterPhaseCount;
			}
			else
			{
				int num3 = (int)((float)jitterPhaseCount - upscalerConsts.jitterPhaseCount);
				if (num3 > 0)
				{
					upscalerConsts.jitterPhaseCount += 1f;
				}
				else if (num3 < 0)
				{
					upscalerConsts.jitterPhaseCount -= 1f;
				}
			}
			upscalerConsts.deltaTime = Mathf.Clamp01(dispatchParams.FrameTimeDelta);
			if (resetAccumulation)
			{
				upscalerConsts.frameIndex = 0f;
				return;
			}
			upscalerConsts.frameIndex += 1f;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003494 File Offset: 0x00001694
		private Vector4 SetupDeviceDepthToViewSpaceDepthParams(Fsr3.DispatchDescription dispatchParams)
		{
			bool flag = (this._contextDescription.Flags & Fsr3.InitializationFlags.EnableDepthInverted) > (Fsr3.InitializationFlags)0;
			bool flag2 = (this._contextDescription.Flags & Fsr3.InitializationFlags.EnableDepthInfinite) > (Fsr3.InitializationFlags)0;
			float num = Mathf.Min(dispatchParams.CameraNear, dispatchParams.CameraFar);
			float num2 = Mathf.Max(dispatchParams.CameraNear, dispatchParams.CameraFar);
			if (flag)
			{
				float num3 = num2;
				float num4 = num;
				num = num3;
				num2 = num4;
			}
			float num5 = num2 / (num - num2);
			float num6 = -1f;
			Vector4 vector = new Vector4(num5, -1f - Mathf.Epsilon, num5, 0f + Mathf.Epsilon);
			Vector4 vector2 = new Vector4(num5 * num, -num - Mathf.Epsilon, num5 * num, num2);
			float num7 = (float)dispatchParams.RenderSize.x / (float)dispatchParams.RenderSize.y;
			float num8 = Mathf.Cos(0.5f * dispatchParams.CameraFovAngleVertical) / Mathf.Sin(0.5f * dispatchParams.CameraFovAngleVertical);
			int index = (flag ? 2 : 0) + (flag2 ? 1 : 0);
			return new Vector4(num6 * vector[index], vector2[index], num7 / num8, 1f / num8);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000035B0 File Offset: 0x000017B0
		private unsafe void SetupRcasConstants(Fsr3.DispatchDescription dispatchParams)
		{
			int num = Mathf.RoundToInt(Mathf.Clamp01(dispatchParams.Sharpness) * (float)(Fsr3UpscalerContext.RcasConfigs.Length - 1));
			*this.RcasConsts = Fsr3UpscalerContext.RcasConfigs[num];
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000035F0 File Offset: 0x000017F0
		private void SetupSpdConstants(Fsr3.DispatchDescription dispatchParams, out Vector2Int dispatchThreadGroupCount)
		{
			Vector2Int vector2Int;
			Vector2Int vector2Int2;
			Fsr3UpscalerContext.SpdSetup(new RectInt(0, 0, dispatchParams.RenderSize.x, dispatchParams.RenderSize.y), out dispatchThreadGroupCount, out vector2Int, out vector2Int2, -1);
			ref Fsr3.SpdConstants spdConsts = ref this.SpdConsts;
			spdConsts.numWorkGroups = (uint)vector2Int2.x;
			spdConsts.mips = (uint)vector2Int2.y;
			spdConsts.workGroupOffsetX = (uint)vector2Int.x;
			spdConsts.workGroupOffsetY = (uint)vector2Int.y;
			spdConsts.renderSizeX = (uint)dispatchParams.RenderSize.x;
			spdConsts.renderSizeY = (uint)dispatchParams.RenderSize.y;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003680 File Offset: 0x00001880
		private static void SpdSetup(RectInt rectInfo, out Vector2Int dispatchThreadGroupCount, out Vector2Int workGroupOffset, out Vector2Int numWorkGroupsAndMips, int mips = -1)
		{
			workGroupOffset = new Vector2Int(rectInfo.x / 64, rectInfo.y / 64);
			int num = (rectInfo.x + rectInfo.width - 1) / 64;
			int num2 = (rectInfo.y + rectInfo.height - 1) / 64;
			dispatchThreadGroupCount = new Vector2Int(num + 1 - workGroupOffset.x, num2 + 1 - workGroupOffset.y);
			numWorkGroupsAndMips = new Vector2Int(dispatchThreadGroupCount.x * dispatchThreadGroupCount.y, mips);
			if (mips < 0)
			{
				float f = (float)Math.Max(rectInfo.width, rectInfo.height);
				numWorkGroupsAndMips.y = Math.Min(Mathf.FloorToInt(Mathf.Log(f, 2f)), 12);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003748 File Offset: 0x00001948
		private void DebugCheckDispatch(Fsr3.DispatchDescription dispatchParams)
		{
			if (!dispatchParams.Color.IsValid)
			{
				Debug.LogError("Color resource is null");
			}
			if (!dispatchParams.Depth.IsValid)
			{
				Debug.LogError("Depth resource is null");
			}
			if (!dispatchParams.MotionVectors.IsValid)
			{
				Debug.LogError("MotionVectors resource is null");
			}
			if (dispatchParams.Exposure.IsValid && (this._contextDescription.Flags & Fsr3.InitializationFlags.EnableAutoExposure) != (Fsr3.InitializationFlags)0)
			{
				Debug.LogWarning("Exposure resource provided, however auto exposure flag is present");
			}
			if (!dispatchParams.Output.IsValid)
			{
				Debug.LogError("Output resource is null");
			}
			if (Mathf.Abs(dispatchParams.JitterOffset.x) > 1f || Mathf.Abs(dispatchParams.JitterOffset.y) > 1f)
			{
				Debug.LogWarning("JitterOffset contains value outside of expected range [-1.0, 1.0]");
			}
			if (dispatchParams.MotionVectorScale.x > (float)this._contextDescription.MaxRenderSize.x || dispatchParams.MotionVectorScale.y > (float)this._contextDescription.MaxRenderSize.y)
			{
				Debug.LogWarning("MotionVectorScale contains scale value greater than MaxRenderSize");
			}
			if (dispatchParams.MotionVectorScale.x == 0f || dispatchParams.MotionVectorScale.y == 0f)
			{
				Debug.LogWarning("MotionVectorScale contains zero scale value");
			}
			if (dispatchParams.RenderSize.x > this._contextDescription.MaxRenderSize.x || dispatchParams.RenderSize.y > this._contextDescription.MaxRenderSize.y)
			{
				Debug.LogWarning("RenderSize is greater than context MaxRenderSize");
			}
			if (dispatchParams.RenderSize.x == 0 || dispatchParams.RenderSize.y == 0)
			{
				Debug.LogWarning("RenderSize contains zero dimension");
			}
			if (dispatchParams.Sharpness < 0f || dispatchParams.Sharpness > 1f)
			{
				Debug.LogWarning("Sharpness contains value outside of expected range [0.0, 1.0]");
			}
			if (dispatchParams.FrameTimeDelta > 1f)
			{
				Debug.LogWarning("FrameTimeDelta is greater than 1.0f - this value should be seconds (~0.0166 for 60fps)");
			}
			if (dispatchParams.PreExposure == 0f)
			{
				Debug.LogError("PreExposure provided as 0.0f which is invalid");
			}
			bool flag = (this._contextDescription.Flags & Fsr3.InitializationFlags.EnableDepthInfinite) > (Fsr3.InitializationFlags)0;
			if ((this._contextDescription.Flags & Fsr3.InitializationFlags.EnableDepthInverted) > (Fsr3.InitializationFlags)0)
			{
				if (dispatchParams.CameraNear < dispatchParams.CameraFar)
				{
					Debug.LogWarning("EnableDepthInverted flag is present yet CameraNear is less than CameraFar");
				}
				if (flag && dispatchParams.CameraNear < 3.4028235E+38f)
				{
					Debug.LogWarning("EnableDepthInfinite and EnableDepthInverted present, yet CameraNear != float.MaxValue");
				}
				if (dispatchParams.CameraFar < 0.075f)
				{
					Debug.LogWarning("EnableDepthInverted present, CameraFar value is very low which may result in depth separation artefacting");
				}
			}
			else
			{
				if (dispatchParams.CameraNear > dispatchParams.CameraFar)
				{
					Debug.LogWarning("CameraNear is greater than CameraFar in non-inverted-depth context");
				}
				if (flag && dispatchParams.CameraFar < 3.4028235E+38f)
				{
					Debug.LogWarning("EnableDepthInfinite present, yet CameraFar != float.MaxValue");
				}
				if (dispatchParams.CameraNear < 0.075f)
				{
					Debug.LogWarning("CameraNear value is very low which may result in depth separation artefacting");
				}
			}
			if (dispatchParams.CameraFovAngleVertical <= 0f)
			{
				Debug.LogError("CameraFovAngleVertical is 0.0f - this value should be > 0.0f");
			}
			if (dispatchParams.CameraFovAngleVertical > 3.1415927f)
			{
				Debug.LogError("CameraFovAngleVertical is greater than 180 degrees/PI");
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003A24 File Offset: 0x00001C24
		private static ComputeBuffer CreateConstantBuffer<TConstants>() where TConstants : struct
		{
			return new ComputeBuffer(1, Marshal.SizeOf<TConstants>(), ComputeBufferType.Constant);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003A32 File Offset: 0x00001C32
		private static void DestroyConstantBuffer(ref ComputeBuffer bufferRef)
		{
			if (bufferRef == null)
			{
				return;
			}
			bufferRef.Release();
			bufferRef = null;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003A43 File Offset: 0x00001C43
		private static void DestroyPass(ref Fsr3UpscalerPass pass)
		{
			if (pass == null)
			{
				return;
			}
			pass.Dispose();
			pass = null;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003A54 File Offset: 0x00001C54
		public Fsr3UpscalerContext()
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003AB0 File Offset: 0x00001CB0
		// Note: this type is marked as 'beforefieldinit'.
		static Fsr3UpscalerContext()
		{
		}

		// Token: 0x0400004D RID: 77
		private const int MaxQueuedFrames = 16;

		// Token: 0x0400004E RID: 78
		private Fsr3.ContextDescription _contextDescription;

		// Token: 0x0400004F RID: 79
		private CommandBuffer _commandBuffer;

		// Token: 0x04000050 RID: 80
		private Fsr3UpscalerPass _prepareInputsPass;

		// Token: 0x04000051 RID: 81
		private Fsr3UpscalerPass _lumaPyramidPass;

		// Token: 0x04000052 RID: 82
		private Fsr3UpscalerPass _shadingChangePyramidPass;

		// Token: 0x04000053 RID: 83
		private Fsr3UpscalerPass _shadingChangePass;

		// Token: 0x04000054 RID: 84
		private Fsr3UpscalerPass _prepareReactivityPass;

		// Token: 0x04000055 RID: 85
		private Fsr3UpscalerPass _lumaInstabilityPass;

		// Token: 0x04000056 RID: 86
		private Fsr3UpscalerPass _accumulatePass;

		// Token: 0x04000057 RID: 87
		private Fsr3UpscalerPass _sharpenPass;

		// Token: 0x04000058 RID: 88
		private Fsr3UpscalerPass _generateReactivePass;

		// Token: 0x04000059 RID: 89
		private Fsr3UpscalerPass _tcrAutogeneratePass;

		// Token: 0x0400005A RID: 90
		private readonly Fsr3UpscalerResources _resources = new Fsr3UpscalerResources();

		// Token: 0x0400005B RID: 91
		private ComputeBuffer _upscalerConstantsBuffer;

		// Token: 0x0400005C RID: 92
		private readonly Fsr3.UpscalerConstants[] _upscalerConstantsArray = new Fsr3.UpscalerConstants[1];

		// Token: 0x0400005D RID: 93
		private ComputeBuffer _spdConstantsBuffer;

		// Token: 0x0400005E RID: 94
		private readonly Fsr3.SpdConstants[] _spdConstantsArray = new Fsr3.SpdConstants[1];

		// Token: 0x0400005F RID: 95
		private ComputeBuffer _rcasConstantsBuffer;

		// Token: 0x04000060 RID: 96
		private readonly Fsr3.RcasConstants[] _rcasConstantsArray = new Fsr3.RcasConstants[1];

		// Token: 0x04000061 RID: 97
		private ComputeBuffer _generateReactiveConstantsBuffer;

		// Token: 0x04000062 RID: 98
		private readonly Fsr3.GenerateReactiveConstants[] _generateReactiveConstantsArray = new Fsr3.GenerateReactiveConstants[1];

		// Token: 0x04000063 RID: 99
		private ComputeBuffer _tcrAutogenerateConstantsBuffer;

		// Token: 0x04000064 RID: 100
		private readonly Fsr3.GenerateReactiveConstants2[] _tcrAutogenerateConstantsArray = new Fsr3.GenerateReactiveConstants2[1];

		// Token: 0x04000065 RID: 101
		private bool _firstExecution;

		// Token: 0x04000066 RID: 102
		private int _resourceFrameIndex;

		// Token: 0x04000067 RID: 103
		private Vector2 _previousJitterOffset;

		// Token: 0x04000068 RID: 104
		private float _preExposure;

		// Token: 0x04000069 RID: 105
		private float _previousFramePreExposure;

		// Token: 0x0400006A RID: 106
		private static readonly Fsr3.RcasConstants[] RcasConfigs = new Fsr3.RcasConstants[]
		{
			new Fsr3.RcasConstants(1048576000U, 872428544U),
			new Fsr3.RcasConstants(1049178080U, 877212745U),
			new Fsr3.RcasConstants(1049823372U, 882390168U),
			new Fsr3.RcasConstants(1050514979U, 887895276U),
			new Fsr3.RcasConstants(1051256227U, 893859143U),
			new Fsr3.RcasConstants(1052050675U, 900216232U),
			new Fsr3.RcasConstants(1052902144U, 907032080U),
			new Fsr3.RcasConstants(1053814727U, 914306687U),
			new Fsr3.RcasConstants(1054792807U, 922105590U),
			new Fsr3.RcasConstants(1055841087U, 930494326U),
			new Fsr3.RcasConstants(1056964608U, 939538432U),
			new Fsr3.RcasConstants(1057566688U, 944322633U),
			new Fsr3.RcasConstants(1058211980U, 949500056U),
			new Fsr3.RcasConstants(1058903587U, 955005164U),
			new Fsr3.RcasConstants(1059644835U, 960969031U),
			new Fsr3.RcasConstants(1060439283U, 967326120U),
			new Fsr3.RcasConstants(1061290752U, 974141968U),
			new Fsr3.RcasConstants(1062203335U, 981416575U),
			new Fsr3.RcasConstants(1063181415U, 989215478U),
			new Fsr3.RcasConstants(1064229695U, 997604214U),
			new Fsr3.RcasConstants(1065353216U, 1006648320U)
		};
	}
}
