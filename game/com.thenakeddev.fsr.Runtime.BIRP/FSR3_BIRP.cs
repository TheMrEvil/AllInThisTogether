using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FidelityFX;
using UnityEngine;
using UnityEngine.Rendering;

namespace TND.FSR
{
	// Token: 0x02000002 RID: 2
	public class FSR3_BIRP : FSR3_BASE
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override void InitializeFSR()
		{
			base.InitializeFSR();
			if (this.m_assets == null)
			{
				this.m_assets = Resources.Load<Fsr3UpscalerAssets>("Fsr3UpscalerAssets");
			}
			this.m_mainCamera.depthTextureMode |= (DepthTextureMode.Depth | DepthTextureMode.MotionVectors);
			this.m_colorGrabPass = new CommandBuffer
			{
				name = "AMD FSR: Color Grab Pass"
			};
			this.m_opaqueOnlyGrabPass = new CommandBuffer
			{
				name = "AMD FSR: Opaque Only Grab Pass"
			};
			this.m_afterOpaqueOnlyGrabPass = new CommandBuffer
			{
				name = "AMD FSR: After Opaque Only Grab Pass"
			};
			this.m_fsrComputePass = new CommandBuffer
			{
				name = "AMD FSR: Compute Pass"
			};
			this.m_blitToCamPass = new CommandBuffer
			{
				name = "AMD FSR: Blit to Camera"
			};
			base.SendMessage("RemovePPV2CommandBuffers", SendMessageOptions.DontRequireReceiver);
			this.SetupResolution();
			if (!this.m_fsrInitialized)
			{
				Camera.onPreRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPreRender, new Camera.CameraCallback(this.OnPreRenderCamera));
				Camera.onPostRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPostRender, new Camera.CameraCallback(this.OnPostRenderCamera));
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000215C File Offset: 0x0000035C
		private void SetupCommandBuffer()
		{
			this.ClearCommandBufferCoroutine();
			if (this.m_colorBuffer)
			{
				if (this.m_opaqueOnlyColorBuffer)
				{
					this.m_opaqueOnlyColorBuffer.Release();
					this.m_afterOpaqueOnlyColorBuffer.Release();
					this.m_reactiveMaskOutput.Release();
				}
				this.m_colorBuffer.Release();
				this.m_fsrOutput.Release();
			}
			this.m_renderWidth = (int)((float)this.m_displayWidth / this.m_scaleFactor);
			this.m_renderHeight = (int)((float)this.m_displayHeight / this.m_scaleFactor);
			this.m_colorBuffer = new RenderTexture(this.m_renderWidth, this.m_renderHeight, 0, RenderTextureFormat.Default);
			this.m_colorBuffer.Create();
			this.m_fsrOutput = new RenderTexture(this.m_displayWidth, this.m_displayHeight, 0, this.m_mainCamera.allowHDR ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default);
			this.m_fsrOutput.enableRandomWrite = true;
			this.m_fsrOutput.Create();
			this.m_dispatchDescription.UpscaleSize = new Vector2Int(this.m_displayWidth, this.m_displayHeight);
			Fsr3.DispatchDescription dispatchDescription = this.m_dispatchDescription;
			RenderTargetIdentifier renderTargetIdentifier = this.m_colorBuffer;
			dispatchDescription.Color = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			if (this.m_mainCamera.actualRenderingPath == RenderingPath.Forward)
			{
				Fsr3.DispatchDescription dispatchDescription2 = this.m_dispatchDescription;
				renderTargetIdentifier = BuiltinRenderTextureType.Depth;
				dispatchDescription2.Depth = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			else
			{
				Fsr3.DispatchDescription dispatchDescription3 = this.m_dispatchDescription;
				renderTargetIdentifier = BuiltinRenderTextureType.ResolvedDepth;
				dispatchDescription3.Depth = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			Fsr3.DispatchDescription dispatchDescription4 = this.m_dispatchDescription;
			renderTargetIdentifier = BuiltinRenderTextureType.MotionVectors;
			dispatchDescription4.MotionVectors = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			Fsr3.DispatchDescription dispatchDescription5 = this.m_dispatchDescription;
			renderTargetIdentifier = this.m_fsrOutput;
			dispatchDescription5.Output = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			if (this.generateReactiveMask)
			{
				this.m_opaqueOnlyColorBuffer = new RenderTexture(this.m_colorBuffer);
				this.m_opaqueOnlyColorBuffer.Create();
				this.m_afterOpaqueOnlyColorBuffer = new RenderTexture(this.m_colorBuffer);
				this.m_afterOpaqueOnlyColorBuffer.Create();
				this.m_reactiveMaskOutput = new RenderTexture(this.m_colorBuffer);
				this.m_reactiveMaskOutput.enableRandomWrite = true;
				this.m_reactiveMaskOutput.Create();
				Fsr3.GenerateReactiveDescription genReactiveDescription = this.m_genReactiveDescription;
				renderTargetIdentifier = this.m_opaqueOnlyColorBuffer;
				genReactiveDescription.ColorOpaqueOnly = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				Fsr3.GenerateReactiveDescription genReactiveDescription2 = this.m_genReactiveDescription;
				renderTargetIdentifier = this.m_afterOpaqueOnlyColorBuffer;
				genReactiveDescription2.ColorPreUpscale = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				Fsr3.GenerateReactiveDescription genReactiveDescription3 = this.m_genReactiveDescription;
				renderTargetIdentifier = this.m_reactiveMaskOutput;
				genReactiveDescription3.OutReactive = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				Fsr3.DispatchDescription dispatchDescription6 = this.m_dispatchDescription;
				renderTargetIdentifier = this.m_reactiveMaskOutput;
				dispatchDescription6.Reactive = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			else
			{
				this.m_genReactiveDescription.ColorOpaqueOnly = ResourceView.Unassigned;
				this.m_genReactiveDescription.ColorPreUpscale = ResourceView.Unassigned;
				this.m_genReactiveDescription.OutReactive = ResourceView.Unassigned;
				this.m_dispatchDescription.Reactive = ResourceView.Unassigned;
			}
			if (this.generateTCMask)
			{
				if (this.generateReactiveMask)
				{
					Fsr3.DispatchDescription dispatchDescription7 = this.m_dispatchDescription;
					renderTargetIdentifier = this.m_reactiveMaskOutput;
					dispatchDescription7.ColorOpaqueOnly = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				}
				else
				{
					Fsr3.DispatchDescription dispatchDescription8 = this.m_dispatchDescription;
					renderTargetIdentifier = this.m_opaqueOnlyColorBuffer;
					dispatchDescription8.ColorOpaqueOnly = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				}
			}
			else
			{
				this.m_dispatchDescription.ColorOpaqueOnly = ResourceView.Unassigned;
			}
			if (this.m_fsrComputePass != null)
			{
				this.m_mainCamera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, this.m_colorGrabPass);
				this.m_mainCamera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, this.m_fsrComputePass);
				this.m_mainCamera.RemoveCommandBuffer(CameraEvent.AfterImageEffects, this.m_blitToCamPass);
				if (this.m_opaqueOnlyGrabPass != null)
				{
					this.m_mainCamera.RemoveCommandBuffer(CameraEvent.BeforeForwardAlpha, this.m_opaqueOnlyGrabPass);
					this.m_mainCamera.RemoveCommandBuffer(CameraEvent.AfterForwardAlpha, this.m_afterOpaqueOnlyGrabPass);
				}
			}
			this.m_colorGrabPass.Clear();
			this.m_fsrComputePass.Clear();
			this.m_blitToCamPass.Clear();
			this.m_colorGrabPass.Blit(BuiltinRenderTextureType.CameraTarget, this.m_colorBuffer);
			if (this.generateReactiveMask)
			{
				this.m_opaqueOnlyGrabPass.Clear();
				this.m_opaqueOnlyGrabPass.Blit(BuiltinRenderTextureType.CameraTarget, this.m_opaqueOnlyColorBuffer);
				this.m_afterOpaqueOnlyGrabPass.Clear();
				this.m_afterOpaqueOnlyGrabPass.Blit(BuiltinRenderTextureType.CameraTarget, this.m_afterOpaqueOnlyColorBuffer);
			}
			this.m_blitToCamPass.Blit(this.m_fsrOutput, BuiltinRenderTextureType.None);
			base.SendMessage("OverridePPV2TargetTexture", this.m_colorBuffer, SendMessageOptions.DontRequireReceiver);
			this.buildCommandBuffers = base.StartCoroutine(this.BuildCommandBuffer());
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000025E0 File Offset: 0x000007E0
		private IEnumerator BuildCommandBuffer()
		{
			base.SendMessage("RemovePPV2CommandBuffers", SendMessageOptions.DontRequireReceiver);
			yield return null;
			if (this.generateReactiveMask && this.m_opaqueOnlyGrabPass != null)
			{
				this.m_mainCamera.AddCommandBuffer(CameraEvent.BeforeForwardAlpha, this.m_opaqueOnlyGrabPass);
				this.m_mainCamera.AddCommandBuffer(CameraEvent.AfterForwardAlpha, this.m_afterOpaqueOnlyGrabPass);
			}
			yield return null;
			base.SendMessage("AddPPV2CommandBuffer", SendMessageOptions.DontRequireReceiver);
			yield return null;
			if (this.m_fsrComputePass != null)
			{
				this.m_mainCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, this.m_colorGrabPass);
				this.m_mainCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, this.m_fsrComputePass);
				this.m_mainCamera.AddCommandBuffer(CameraEvent.AfterImageEffects, this.m_blitToCamPass);
			}
			this.buildCommandBuffers = null;
			yield break;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000025EF File Offset: 0x000007EF
		private void ClearCommandBufferCoroutine()
		{
			if (this.buildCommandBuffers != null)
			{
				base.StopCoroutine(this.buildCommandBuffers);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002608 File Offset: 0x00000808
		private void OnPreRenderCamera(Camera camera)
		{
			if (camera != this.m_mainCamera)
			{
				return;
			}
			if (this.generateReactiveMask)
			{
				this.m_genReactiveDescription.RenderSize = new Vector2Int(this.m_renderWidth, this.m_renderHeight);
				this.m_genReactiveDescription.Scale = this.autoReactiveScale;
				this.m_genReactiveDescription.CutoffThreshold = this.autoReactiveThreshold;
				this.m_genReactiveDescription.BinaryValue = this.autoReactiveBinaryValue;
				this.m_genReactiveDescription.Flags = this.reactiveFlags;
			}
			this.m_dispatchDescription.Exposure = ResourceView.Unassigned;
			this.m_dispatchDescription.PreExposure = 1f;
			this.m_dispatchDescription.EnableSharpening = this.sharpening;
			this.m_dispatchDescription.Sharpness = this.sharpness;
			this.m_dispatchDescription.MotionVectorScale.x = (float)(-(float)this.m_renderWidth);
			this.m_dispatchDescription.MotionVectorScale.y = (float)(-(float)this.m_renderHeight);
			this.m_dispatchDescription.RenderSize = new Vector2Int(this.m_renderWidth, this.m_renderHeight);
			this.m_dispatchDescription.FrameTimeDelta = Time.deltaTime;
			this.m_dispatchDescription.CameraNear = this.m_mainCamera.nearClipPlane;
			this.m_dispatchDescription.CameraFar = this.m_mainCamera.farClipPlane;
			this.m_dispatchDescription.CameraFovAngleVertical = this.m_mainCamera.fieldOfView * 0.017453292f;
			this.m_dispatchDescription.ViewSpaceToMetersFactor = 1f;
			this.m_dispatchDescription.Reset = this.m_resetCamera;
			this.m_dispatchDescription.EnableAutoReactive = this.generateTCMask;
			this.m_dispatchDescription.AutoTcThreshold = this.autoTcThreshold;
			this.m_dispatchDescription.AutoTcScale = this.autoTcScale;
			this.m_dispatchDescription.AutoReactiveScale = this.autoReactiveScale;
			this.m_dispatchDescription.AutoReactiveMax = this.autoTcReactiveMax;
			this.m_resetCamera = false;
			if (SystemInfo.usesReversedZBuffer)
			{
				Fsr3.DispatchDescription dispatchDescription = this.m_dispatchDescription;
				Fsr3.DispatchDescription dispatchDescription2 = this.m_dispatchDescription;
				float cameraFar = this.m_dispatchDescription.CameraFar;
				float cameraNear = this.m_dispatchDescription.CameraNear;
				dispatchDescription.CameraNear = cameraFar;
				dispatchDescription2.CameraFar = cameraNear;
			}
			this.JitterTAA();
			this.m_mainCamera.targetTexture = this.m_colorBuffer;
			if (this.m_displayWidth != Display.main.renderingWidth || this.m_displayHeight != Display.main.renderingHeight || this.m_previousHDR != this.m_mainCamera.allowHDR)
			{
				this.SetupResolution();
			}
			if (this.m_previousScaleFactor != this.m_scaleFactor || this.m_previousReactiveMask != this.generateReactiveMask || this.m_previousTCMask != this.generateTCMask || this.m_previousRenderingPath != this.m_mainCamera.actualRenderingPath || !this.initFirstFrame)
			{
				this.initFirstFrame = true;
				this.SetupFrameBuffers();
			}
			this.UpdateDispatch();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000028D4 File Offset: 0x00000AD4
		private void OnPostRenderCamera(Camera camera)
		{
			if (camera != this.m_mainCamera)
			{
				return;
			}
			this.m_mainCamera.targetTexture = null;
			this.m_mainCamera.ResetProjectionMatrix();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000028FC File Offset: 0x00000AFC
		private void JitterTAA()
		{
			int jitterPhaseCount = Fsr3.GetJitterPhaseCount(this.m_renderWidth, (int)((float)this.m_renderWidth * this.m_scaleFactor));
			float num;
			float num2;
			Fsr3.GetJitterOffset(out num, out num2, Time.frameCount, jitterPhaseCount);
			this.m_dispatchDescription.JitterOffset = new Vector2(num, num2);
			num = 2f * num / (float)this.m_renderWidth;
			num2 = 2f * num2 / (float)this.m_renderHeight;
			num += UnityEngine.Random.Range(-0.001f * this.antiGhosting, 0.001f * this.antiGhosting);
			num2 += UnityEngine.Random.Range(-0.001f * this.antiGhosting, 0.001f * this.antiGhosting);
			this.m_jitterMatrix = Matrix4x4.Translate(new Vector2(num, num2));
			this.m_projectionMatrix = this.m_mainCamera.projectionMatrix;
			this.m_mainCamera.nonJitteredProjectionMatrix = this.m_projectionMatrix;
			this.m_mainCamera.projectionMatrix = this.m_jitterMatrix * this.m_projectionMatrix;
			this.m_mainCamera.useJitteredProjectionMatrixForTransparentRendering = true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002A06 File Offset: 0x00000C06
		private void SetupFrameBuffers()
		{
			this.m_previousScaleFactor = this.m_scaleFactor;
			this.m_previousReactiveMask = this.generateReactiveMask;
			this.m_previousTCMask = this.generateTCMask;
			this.SetupCommandBuffer();
			this.m_previousRenderingPath = this.m_mainCamera.actualRenderingPath;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002A44 File Offset: 0x00000C44
		private void SetupResolution()
		{
			this.m_displayWidth = Display.main.renderingWidth;
			this.m_displayHeight = Display.main.renderingHeight;
			this.m_previousHDR = this.m_mainCamera.allowHDR;
			Fsr3.InitializationFlags initializationFlags = Fsr3.InitializationFlags.EnableAutoExposure;
			if (this.m_mainCamera.allowHDR)
			{
				initializationFlags |= Fsr3.InitializationFlags.EnableHighDynamicRange;
			}
			if (this.m_context != null)
			{
				this.m_context.Destroy();
				this.m_context = null;
			}
			this.m_context = Fsr3.CreateContext(new Vector2Int(this.m_displayWidth, this.m_displayHeight), new Vector2Int(this.m_displayWidth, this.m_displayHeight), this.m_assets.shaders, initializationFlags);
			this.SetupFrameBuffers();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002AF0 File Offset: 0x00000CF0
		private void UpdateDispatch()
		{
			if (this.m_fsrComputePass != null)
			{
				this.m_fsrComputePass.Clear();
				if (this.generateReactiveMask)
				{
					this.m_context.GenerateReactiveMask(this.m_genReactiveDescription, this.m_fsrComputePass);
				}
				this.m_context.Dispatch(this.m_dispatchDescription, this.m_fsrComputePass);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002B48 File Offset: 0x00000D48
		protected override void DisableFSR()
		{
			base.DisableFSR();
			Camera.onPreRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPreRender, new Camera.CameraCallback(this.OnPreRenderCamera));
			Camera.onPostRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPostRender, new Camera.CameraCallback(this.OnPostRenderCamera));
			this.initFirstFrame = false;
			this.ClearCommandBufferCoroutine();
			base.SendMessage("ResetPPV2CommandBuffer", SendMessageOptions.DontRequireReceiver);
			base.SendMessage("ResetPPV2TargetTexture", SendMessageOptions.DontRequireReceiver);
			base.OnResetAllMipMaps();
			if (this.m_mainCamera != null)
			{
				this.m_mainCamera.targetTexture = null;
				this.m_mainCamera.ResetProjectionMatrix();
				if (this.m_opaqueOnlyGrabPass != null)
				{
					this.m_mainCamera.RemoveCommandBuffer(CameraEvent.BeforeForwardAlpha, this.m_opaqueOnlyGrabPass);
					this.m_mainCamera.RemoveCommandBuffer(CameraEvent.AfterForwardAlpha, this.m_afterOpaqueOnlyGrabPass);
				}
				if (this.m_fsrComputePass != null)
				{
					this.m_mainCamera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, this.m_colorGrabPass);
					this.m_mainCamera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, this.m_fsrComputePass);
					this.m_mainCamera.RemoveCommandBuffer(CameraEvent.AfterImageEffects, this.m_blitToCamPass);
				}
			}
			this.m_fsrComputePass = (this.m_colorGrabPass = (this.m_opaqueOnlyGrabPass = (this.m_afterOpaqueOnlyGrabPass = (this.m_blitToCamPass = null))));
			if (this.m_colorBuffer)
			{
				if (this.m_opaqueOnlyColorBuffer)
				{
					this.m_opaqueOnlyColorBuffer.Release();
					this.m_afterOpaqueOnlyColorBuffer.Release();
					this.m_reactiveMaskOutput.Release();
				}
				this.m_colorBuffer.Release();
				this.m_fsrOutput.Release();
			}
			if (this.m_context != null)
			{
				this.m_context.Destroy();
				this.m_context = null;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002CF3 File Offset: 0x00000EF3
		public FSR3_BIRP()
		{
		}

		// Token: 0x04000001 RID: 1
		private CommandBuffer m_colorGrabPass;

		// Token: 0x04000002 RID: 2
		private CommandBuffer m_fsrComputePass;

		// Token: 0x04000003 RID: 3
		private CommandBuffer m_opaqueOnlyGrabPass;

		// Token: 0x04000004 RID: 4
		private CommandBuffer m_afterOpaqueOnlyGrabPass;

		// Token: 0x04000005 RID: 5
		private CommandBuffer m_blitToCamPass;

		// Token: 0x04000006 RID: 6
		private RenderTexture m_opaqueOnlyColorBuffer;

		// Token: 0x04000007 RID: 7
		private RenderTexture m_afterOpaqueOnlyColorBuffer;

		// Token: 0x04000008 RID: 8
		private RenderTexture m_reactiveMaskOutput;

		// Token: 0x04000009 RID: 9
		private RenderTexture m_colorBuffer;

		// Token: 0x0400000A RID: 10
		private RenderTexture m_fsrOutput;

		// Token: 0x0400000B RID: 11
		private const CameraEvent m_OPAQUE_ONLY_EVENT = CameraEvent.BeforeForwardAlpha;

		// Token: 0x0400000C RID: 12
		private const CameraEvent m_AFTER_OPAQUE_ONLY_EVENT = CameraEvent.AfterForwardAlpha;

		// Token: 0x0400000D RID: 13
		private const CameraEvent m_COLOR_EVENT = CameraEvent.BeforeImageEffects;

		// Token: 0x0400000E RID: 14
		private const CameraEvent m_FSR_EVENT = CameraEvent.BeforeImageEffects;

		// Token: 0x0400000F RID: 15
		private const CameraEvent m_BlITFSR_EVENT = CameraEvent.AfterImageEffects;

		// Token: 0x04000010 RID: 16
		private Matrix4x4 m_jitterMatrix;

		// Token: 0x04000011 RID: 17
		private Matrix4x4 m_projectionMatrix;

		// Token: 0x04000012 RID: 18
		private readonly Fsr3.DispatchDescription m_dispatchDescription = new Fsr3.DispatchDescription();

		// Token: 0x04000013 RID: 19
		private readonly Fsr3.GenerateReactiveDescription m_genReactiveDescription = new Fsr3.GenerateReactiveDescription();

		// Token: 0x04000014 RID: 20
		private Fsr3UpscalerContext m_context;

		// Token: 0x04000015 RID: 21
		private Fsr3UpscalerAssets m_assets;

		// Token: 0x04000016 RID: 22
		private bool initFirstFrame;

		// Token: 0x04000017 RID: 23
		private Coroutine buildCommandBuffers;

		// Token: 0x02000003 RID: 3
		[CompilerGenerated]
		private sealed class <BuildCommandBuffer>d__25 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600000D RID: 13 RVA: 0x00002D11 File Offset: 0x00000F11
			[DebuggerHidden]
			public <BuildCommandBuffer>d__25(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600000E RID: 14 RVA: 0x00002D20 File Offset: 0x00000F20
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600000F RID: 15 RVA: 0x00002D24 File Offset: 0x00000F24
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				FSR3_BIRP fsr3_BIRP = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					fsr3_BIRP.SendMessage("RemovePPV2CommandBuffers", SendMessageOptions.DontRequireReceiver);
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					if (fsr3_BIRP.generateReactiveMask && fsr3_BIRP.m_opaqueOnlyGrabPass != null)
					{
						fsr3_BIRP.m_mainCamera.AddCommandBuffer(CameraEvent.BeforeForwardAlpha, fsr3_BIRP.m_opaqueOnlyGrabPass);
						fsr3_BIRP.m_mainCamera.AddCommandBuffer(CameraEvent.AfterForwardAlpha, fsr3_BIRP.m_afterOpaqueOnlyGrabPass);
					}
					this.<>2__current = null;
					this.<>1__state = 2;
					return true;
				case 2:
					this.<>1__state = -1;
					fsr3_BIRP.SendMessage("AddPPV2CommandBuffer", SendMessageOptions.DontRequireReceiver);
					this.<>2__current = null;
					this.<>1__state = 3;
					return true;
				case 3:
					this.<>1__state = -1;
					if (fsr3_BIRP.m_fsrComputePass != null)
					{
						fsr3_BIRP.m_mainCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, fsr3_BIRP.m_colorGrabPass);
						fsr3_BIRP.m_mainCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, fsr3_BIRP.m_fsrComputePass);
						fsr3_BIRP.m_mainCamera.AddCommandBuffer(CameraEvent.AfterImageEffects, fsr3_BIRP.m_blitToCamPass);
					}
					fsr3_BIRP.buildCommandBuffers = null;
					return false;
				default:
					return false;
				}
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000010 RID: 16 RVA: 0x00002E3A File Offset: 0x0000103A
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000011 RID: 17 RVA: 0x00002E42 File Offset: 0x00001042
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000012 RID: 18 RVA: 0x00002E49 File Offset: 0x00001049
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000018 RID: 24
			private int <>1__state;

			// Token: 0x04000019 RID: 25
			private object <>2__current;

			// Token: 0x0400001A RID: 26
			public FSR3_BIRP <>4__this;
		}
	}
}
