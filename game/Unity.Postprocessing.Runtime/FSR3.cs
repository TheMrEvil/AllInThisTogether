using System;
using System.Runtime.CompilerServices;
using FidelityFX;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200002A RID: 42
	[Preserve]
	[Serializable]
	public class FSR3
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000060A9 File Offset: 0x000042A9
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000060B1 File Offset: 0x000042B1
		public Vector2 jitter
		{
			[CompilerGenerated]
			get
			{
				return this.<jitter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<jitter>k__BackingField = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000060BA File Offset: 0x000042BA
		public Vector2Int renderSize
		{
			get
			{
				return this._maxRenderSize;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000060C2 File Offset: 0x000042C2
		public Vector2Int displaySize
		{
			get
			{
				return this._displaySize;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000060CA File Offset: 0x000042CA
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000060D2 File Offset: 0x000042D2
		public RenderTargetIdentifier colorOpaqueOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<colorOpaqueOnly>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<colorOpaqueOnly>k__BackingField = value;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000060DB File Offset: 0x000042DB
		public void OnResetCamera()
		{
			this._resetHistory = true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000060E4 File Offset: 0x000042E4
		public void OnMipmapSingleTexture(Texture texture)
		{
			MipMapUtils.OnMipMapSingleTexture(texture, (float)this.renderSize.x, (float)this.displaySize.x, this.mipMapBiasOverride);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000611C File Offset: 0x0000431C
		public void OnMipMapAllTextures()
		{
			MipMapUtils.OnMipMapAllTextures((float)this.renderSize.x, (float)this.displaySize.x, this.mipMapBiasOverride);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00006152 File Offset: 0x00004352
		public void OnResetAllMipMaps()
		{
			MipMapUtils.OnResetAllMipMaps();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00006159 File Offset: 0x00004359
		public bool IsSupported()
		{
			return SystemInfo.supportsComputeShaders && SystemInfo.supportsMotionVectors;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00006169 File Offset: 0x00004369
		internal DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000616C File Offset: 0x0000436C
		internal void Release()
		{
			this.DestroyFsrContext();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006174 File Offset: 0x00004374
		internal void ConfigureJitteredProjectionMatrix(PostProcessRenderContext context)
		{
			if (this.qualityMode == Fsr3.QualityMode.Off)
			{
				this.Release();
				return;
			}
			this.ApplyJitter(context.camera, context);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00006194 File Offset: 0x00004394
		public void ConfigureCameraViewport(PostProcessRenderContext context)
		{
			if (context.camera.stereoEnabled && context.camera.stereoActiveEye == Camera.MonoOrStereoscopicEye.Right)
			{
				return;
			}
			Camera camera = context.camera;
			this._originalRect = camera.rect;
			this._displaySize = new Vector2Int(camera.pixelWidth, camera.pixelHeight);
			int x;
			int y;
			Fsr3.GetRenderResolutionFromQualityMode(out x, out y, this._displaySize.x, this._displaySize.y, this.qualityMode);
			this._maxRenderSize = new Vector2Int(x, y);
			camera.aspect = (float)this._displaySize.x * this._originalRect.width / ((float)this._displaySize.y * this._originalRect.height);
			camera.rect = new Rect(0f, 0f, this._originalRect.width * (float)this._maxRenderSize.x / (float)this._displaySize.x, this._originalRect.height * (float)this._maxRenderSize.y / (float)this._displaySize.y);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000062B0 File Offset: 0x000044B0
		public void ConfigureCameraViewportRightEye(PostProcessRenderContext context)
		{
			if (context.camera.stereoEnabled && context.camera.stereoActiveEye == Camera.MonoOrStereoscopicEye.Left)
			{
				return;
			}
			Camera camera = context.camera;
			this._originalRect = context.superResolution3._originalRect;
			this._displaySize = new Vector2Int(context.superResolution3._displaySize.x, context.superResolution3._displaySize.y);
			this.qualityMode = context.superResolution3.qualityMode;
			int x;
			int y;
			Fsr3.GetRenderResolutionFromQualityMode(out x, out y, this._displaySize.x, this._displaySize.y, this.qualityMode);
			this._maxRenderSize = new Vector2Int(x, y);
			camera.aspect = (float)this._displaySize.x * this._originalRect.width / ((float)this._displaySize.y * this._originalRect.height);
			camera.rect = new Rect(0f, 0f, this._originalRect.width * (float)this._maxRenderSize.x / (float)this._displaySize.x, this._originalRect.height * (float)this._maxRenderSize.y / (float)this._displaySize.y);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000063F2 File Offset: 0x000045F2
		internal void ResetCameraViewport(PostProcessRenderContext context)
		{
			context.camera.rect = this._originalRect;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006408 File Offset: 0x00004608
		internal void Render(PostProcessRenderContext context, bool _stereoRendering = false)
		{
			CommandBuffer command = context.command;
			if (this.qualityMode == Fsr3.QualityMode.Off)
			{
				command.Blit(context.source, context.destination);
				return;
			}
			if (_stereoRendering)
			{
				this.isStereoRendering = _stereoRendering;
				command.BeginSample("FSR3 Right Eye");
			}
			else
			{
				command.BeginSample("FSR3");
			}
			if (this.autoTextureUpdate && !this.isStereoRendering)
			{
				MipMapUtils.AutoUpdateMipMaps((float)this.renderSize.x, (float)this.displaySize.x, this.mipMapBiasOverride, this.updateFrequency, ref this._prevMipMapBias, ref this._mipMapTimer, ref this._previousLength);
			}
			if (this._fsrContext == null || this._displaySize.x != this._prevDisplaySize.x || this._displaySize.y != this._prevDisplaySize.y || this.qualityMode != this._prevQualityMode || this.exposureSource != this._prevExposureSource)
			{
				this.DestroyFsrContext();
				this.CreateFsrContext(context);
				this._mipMapTimer = float.PositiveInfinity;
			}
			this.SetupDispatchDescription(context);
			if (this.autoGenerateReactiveMask)
			{
				this.SetupAutoReactiveDescription(context);
				Vector2Int renderSize = this._genReactiveDescription.RenderSize;
				command.GetTemporaryRT(Fsr3ShaderIDs.UavAutoReactive, renderSize.x, renderSize.y, 0, FilterMode.Point, GraphicsFormat.R8_UNorm, 1, true);
				this._fsrContext.GenerateReactiveMask(this._genReactiveDescription, command);
				Fsr3.DispatchDescription dispatchDescription = this._dispatchDescription;
				RenderTargetIdentifier renderTargetIdentifier = Fsr3ShaderIDs.UavAutoReactive;
				dispatchDescription.Reactive = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			this._fsrContext.Dispatch(this._dispatchDescription, command);
			if (_stereoRendering)
			{
				command.EndSample("FSR3 Right Eye");
			}
			else
			{
				command.EndSample("FSR3");
			}
			this._resetHistory = false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000065BC File Offset: 0x000047BC
		private void CreateFsrContext(PostProcessRenderContext context)
		{
			this._prevQualityMode = this.qualityMode;
			this._prevExposureSource = this.exposureSource;
			this._prevDisplaySize = this._displaySize;
			this.enableFP16 = false;
			Fsr3.InitializationFlags initializationFlags = (Fsr3.InitializationFlags)0;
			if (context.camera.allowHDR)
			{
				initializationFlags |= Fsr3.InitializationFlags.EnableHighDynamicRange;
			}
			if (this.enableFP16)
			{
				initializationFlags |= Fsr3.InitializationFlags.EnableFP16Usage;
			}
			if (this.exposureSource == FSR3.ExposureSource.Auto)
			{
				initializationFlags |= Fsr3.InitializationFlags.EnableAutoExposure;
			}
			if (RuntimeUtilities.IsDynamicResolutionEnabled(context.camera))
			{
				initializationFlags |= Fsr3.InitializationFlags.EnableDynamicResolution;
			}
			if (context.camera.stereoEnabled)
			{
				initializationFlags |= Fsr3.InitializationFlags.EnableDisplayResolutionMotionVectors;
			}
			if (this._fsrAssets == null)
			{
				this._fsrAssets = Resources.Load<Fsr3UpscalerAssets>("Fsr3UpscalerAssets");
			}
			this._fsrContext = Fsr3.CreateContext(this._displaySize, this._maxRenderSize, this._fsrAssets.shaders, initializationFlags);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006689 File Offset: 0x00004889
		private void DestroyFsrContext()
		{
			if (this._fsrContext != null)
			{
				this._fsrContext.Destroy();
				this._fsrContext = null;
			}
			MipMapUtils.OnResetAllMipMaps();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000066AC File Offset: 0x000048AC
		private void ApplyJitter(Camera camera, PostProcessRenderContext context)
		{
			Vector2Int scaledRenderSize = this.GetScaledRenderSize(camera);
			int jitterPhaseCount = Fsr3.GetJitterPhaseCount(scaledRenderSize.x, this._displaySize.x);
			float num;
			float num2;
			Fsr3.GetJitterOffset(out num, out num2, Time.frameCount, jitterPhaseCount);
			this._dispatchDescription.JitterOffset = new Vector2(num, num2);
			num = 2f * num / (float)scaledRenderSize.x;
			num2 = 2f * num2 / (float)scaledRenderSize.y;
			num += Random.Range(-0.001f * this.antiGhosting, 0.001f * this.antiGhosting);
			num2 += Random.Range(-0.001f * this.antiGhosting, 0.001f * this.antiGhosting);
			this.jitter = new Vector2(num, num2);
			if (camera.stereoEnabled)
			{
				if (camera.stereoActiveEye != Camera.MonoOrStereoscopicEye.Right)
				{
					this.ConfigureStereoJitteredProjectionMatrices(context, camera);
					return;
				}
			}
			else
			{
				this.ConfigureJitteredProjectionMatrix(camera, num, num2);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000678C File Offset: 0x0000498C
		public void ConfigureJitteredProjectionMatrix(Camera camera, float jitterX, float jitterY)
		{
			Matrix4x4 lhs = Matrix4x4.Translate(new Vector3(jitterX, jitterY, 0f));
			Matrix4x4 projectionMatrix = camera.projectionMatrix;
			camera.nonJitteredProjectionMatrix = projectionMatrix;
			camera.projectionMatrix = lhs * camera.nonJitteredProjectionMatrix;
			camera.useJitteredProjectionMatrixForTransparentRendering = false;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000067D4 File Offset: 0x000049D4
		public void ConfigureStereoJitteredProjectionMatrices(PostProcessRenderContext context, Camera camera)
		{
			for (Camera.StereoscopicEye stereoscopicEye = Camera.StereoscopicEye.Left; stereoscopicEye <= Camera.StereoscopicEye.Right; stereoscopicEye++)
			{
				camera.CopyStereoDeviceProjectionMatrixToNonJittered(stereoscopicEye);
				Matrix4x4 stereoNonJitteredProjectionMatrix = camera.GetStereoNonJitteredProjectionMatrix(stereoscopicEye);
				Matrix4x4 matrix = RuntimeUtilities.GenerateJitteredProjectionMatrixFromOriginal(context, stereoNonJitteredProjectionMatrix, this.jitter);
				camera.SetStereoProjectionMatrix(stereoscopicEye, matrix);
			}
			this.jitter = new Vector2(this.jitter.x / (float)context.screenWidth, this.jitter.y / (float)context.screenHeight);
			camera.useJitteredProjectionMatrixForTransparentRendering = false;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000684C File Offset: 0x00004A4C
		private void SetupDispatchDescription(PostProcessRenderContext context)
		{
			Camera camera = context.camera;
			Fsr3.DispatchDescription dispatchDescription = this._dispatchDescription;
			RenderTargetIdentifier renderTargetIdentifier = context.source;
			dispatchDescription.Color = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Color, 0);
			Fsr3.DispatchDescription dispatchDescription2 = this._dispatchDescription;
			renderTargetIdentifier = BuiltinRenderTextureType.CameraTarget;
			dispatchDescription2.Depth = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Depth, 0);
			Fsr3.DispatchDescription dispatchDescription3 = this._dispatchDescription;
			renderTargetIdentifier = BuiltinRenderTextureType.MotionVectors;
			dispatchDescription3.MotionVectors = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			this._dispatchDescription.Exposure = ResourceView.Unassigned;
			this._dispatchDescription.Reactive = ResourceView.Unassigned;
			this._dispatchDescription.TransparencyAndComposition = ResourceView.Unassigned;
			Vector2Int scaledRenderSize = this.GetScaledRenderSize(context.camera);
			if (camera.stereoEnabled)
			{
				this._dispatchDescription.MotionVectorScale.x = (float)(-(float)this.displaySize.x);
				this._dispatchDescription.MotionVectorScale.y = (float)(-(float)this.displaySize.y);
			}
			else
			{
				this._dispatchDescription.MotionVectorScale.x = (float)(-(float)scaledRenderSize.x);
				this._dispatchDescription.MotionVectorScale.y = (float)(-(float)scaledRenderSize.y);
			}
			if (SystemInfo.usesReversedZBuffer)
			{
				Fsr3.DispatchDescription dispatchDescription4 = this._dispatchDescription;
				Fsr3.DispatchDescription dispatchDescription5 = this._dispatchDescription;
				float cameraFar = this._dispatchDescription.CameraFar;
				float cameraNear = this._dispatchDescription.CameraNear;
				dispatchDescription4.CameraNear = cameraFar;
				dispatchDescription5.CameraFar = cameraNear;
			}
			if (!this.isStereoRendering)
			{
				if (this.exposureSource == FSR3.ExposureSource.Manual && this.exposure != null)
				{
					Fsr3.DispatchDescription dispatchDescription6 = this._dispatchDescription;
					renderTargetIdentifier = this.exposure;
					dispatchDescription6.Exposure = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				}
				if (this.exposureSource == FSR3.ExposureSource.Unity)
				{
					Fsr3.DispatchDescription dispatchDescription7 = this._dispatchDescription;
					renderTargetIdentifier = context.autoExposureTexture;
					dispatchDescription7.Exposure = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				}
				if (this.reactiveMask != null)
				{
					Fsr3.DispatchDescription dispatchDescription8 = this._dispatchDescription;
					renderTargetIdentifier = this.reactiveMask;
					dispatchDescription8.Reactive = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				}
				if (this.transparencyAndCompositionMask != null)
				{
					Fsr3.DispatchDescription dispatchDescription9 = this._dispatchDescription;
					renderTargetIdentifier = this.transparencyAndCompositionMask;
					dispatchDescription9.TransparencyAndComposition = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
				}
				Fsr3.DispatchDescription dispatchDescription10 = this._dispatchDescription;
				renderTargetIdentifier = context.destination;
				dispatchDescription10.Output = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Color, 0);
				this._dispatchDescription.PreExposure = this.preExposure;
				this._dispatchDescription.EnableSharpening = this.Sharpening;
				this._dispatchDescription.Sharpness = this.sharpness;
				this._dispatchDescription.RenderSize = scaledRenderSize;
				this._dispatchDescription.UpscaleSize = this.displaySize;
				this._dispatchDescription.FrameTimeDelta = Time.unscaledDeltaTime;
				this._dispatchDescription.CameraNear = camera.nearClipPlane;
				this._dispatchDescription.CameraFar = camera.farClipPlane;
				this._dispatchDescription.CameraFovAngleVertical = camera.fieldOfView * 0.017453292f;
				this._dispatchDescription.ViewSpaceToMetersFactor = 1f;
				this._dispatchDescription.Reset = this._resetHistory;
				this._dispatchDescription.EnableAutoReactive = this.autoGenerateTransparencyAndComposition;
				return;
			}
			if (this.exposureSource == FSR3.ExposureSource.Manual && context.superResolution3.exposure != null)
			{
				Fsr3.DispatchDescription dispatchDescription11 = this._dispatchDescription;
				renderTargetIdentifier = context.superResolution3.exposure;
				dispatchDescription11.Exposure = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			if (this.exposureSource == FSR3.ExposureSource.Unity)
			{
				Fsr3.DispatchDescription dispatchDescription12 = this._dispatchDescription;
				renderTargetIdentifier = context.autoExposureTexture;
				dispatchDescription12.Exposure = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			if (this.reactiveMask != null)
			{
				Fsr3.DispatchDescription dispatchDescription13 = this._dispatchDescription;
				renderTargetIdentifier = context.superResolution3.reactiveMask;
				dispatchDescription13.Reactive = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			if (this.transparencyAndCompositionMask != null)
			{
				Fsr3.DispatchDescription dispatchDescription14 = this._dispatchDescription;
				renderTargetIdentifier = context.superResolution3.transparencyAndCompositionMask;
				dispatchDescription14.TransparencyAndComposition = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			}
			Fsr3.DispatchDescription dispatchDescription15 = this._dispatchDescription;
			renderTargetIdentifier = context.destination;
			dispatchDescription15.Output = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			this._dispatchDescription.PreExposure = context.superResolution3.preExposure;
			this._dispatchDescription.EnableSharpening = context.superResolution3.Sharpening;
			this._dispatchDescription.Sharpness = context.superResolution3.sharpness;
			this._dispatchDescription.RenderSize = scaledRenderSize;
			this._dispatchDescription.UpscaleSize = this.displaySize;
			this._dispatchDescription.FrameTimeDelta = Time.unscaledDeltaTime;
			this._dispatchDescription.CameraNear = camera.nearClipPlane;
			this._dispatchDescription.CameraFar = camera.farClipPlane;
			this._dispatchDescription.CameraFovAngleVertical = camera.fieldOfView * 0.017453292f;
			this._dispatchDescription.ViewSpaceToMetersFactor = 1f;
			this._dispatchDescription.Reset = context.superResolution3._resetHistory;
			this.autoGenerateReactiveMask = context.superResolution3.autoGenerateReactiveMask;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00006D24 File Offset: 0x00004F24
		private void SetupAutoReactiveDescription(PostProcessRenderContext context)
		{
			Fsr3.GenerateReactiveDescription genReactiveDescription = this._genReactiveDescription;
			RenderTargetIdentifier renderTargetIdentifier = this.colorOpaqueOnly;
			genReactiveDescription.ColorOpaqueOnly = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			Fsr3.GenerateReactiveDescription genReactiveDescription2 = this._genReactiveDescription;
			renderTargetIdentifier = context.source;
			genReactiveDescription2.ColorPreUpscale = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			Fsr3.GenerateReactiveDescription genReactiveDescription3 = this._genReactiveDescription;
			renderTargetIdentifier = Fsr3ShaderIDs.UavAutoReactive;
			genReactiveDescription3.OutReactive = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			this._genReactiveDescription.RenderSize = this.GetScaledRenderSize(context.camera);
			if (!this.isStereoRendering)
			{
				this._genReactiveDescription.Scale = this.ReactiveScale;
				this._genReactiveDescription.CutoffThreshold = this.ReactiveThreshold;
				this._genReactiveDescription.BinaryValue = this.ReactiveBinaryValue;
				this._genReactiveDescription.Flags = this.flags;
				return;
			}
			this._genReactiveDescription.Scale = context.superResolution3.ReactiveScale;
			this._genReactiveDescription.CutoffThreshold = context.superResolution3.ReactiveThreshold;
			this._genReactiveDescription.BinaryValue = context.superResolution3.ReactiveBinaryValue;
			this._genReactiveDescription.Flags = context.superResolution3.flags;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006E44 File Offset: 0x00005044
		internal Vector2Int GetScaledRenderSize(Camera camera)
		{
			if (!RuntimeUtilities.IsDynamicResolutionEnabled(camera))
			{
				return this._maxRenderSize;
			}
			return new Vector2Int(Mathf.CeilToInt((float)this._maxRenderSize.x * ScalableBufferManager.widthScaleFactor), Mathf.CeilToInt((float)this._maxRenderSize.y * ScalableBufferManager.heightScaleFactor));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00006E94 File Offset: 0x00005094
		public FSR3()
		{
		}

		// Token: 0x040000CA RID: 202
		[Tooltip("Fallback AA for when FSR 3 is not supported")]
		public PostProcessLayer.Antialiasing fallBackAA;

		// Token: 0x040000CB RID: 203
		[Range(0f, 1f)]
		public float antiGhosting;

		// Token: 0x040000CC RID: 204
		[Tooltip("Standard scaling ratio presets.")]
		[Header("FSR 3 Settings")]
		public Fsr3.QualityMode qualityMode = Fsr3.QualityMode.Quality;

		// Token: 0x040000CD RID: 205
		[Tooltip("Apply RCAS sharpening to the image after upscaling.")]
		public bool Sharpening = true;

		// Token: 0x040000CE RID: 206
		[Tooltip("Strength of the sharpening effect.")]
		[Range(0f, 1f)]
		public float sharpness = 0.5f;

		// Token: 0x040000CF RID: 207
		[HideInInspector]
		[Tooltip("Allow the use of half precision compute operations, potentially improving performance if the platform supports it.")]
		public bool enableFP16;

		// Token: 0x040000D0 RID: 208
		[Header("Transparency Settings")]
		[Tooltip("Automatically generate a reactive mask based on the difference between opaque-only render output and the final render output including alpha transparencies.")]
		public bool autoGenerateReactiveMask = true;

		// Token: 0x040000D1 RID: 209
		[Tooltip("A value to scale the output")]
		[Range(0f, 1f)]
		public float ReactiveScale = 0.9f;

		// Token: 0x040000D2 RID: 210
		[Tooltip("A threshold value to generate a binary reactive mask")]
		[Range(0f, 1f)]
		public float ReactiveThreshold = 0.1f;

		// Token: 0x040000D3 RID: 211
		[Tooltip("A value to set for the binary reactive mask")]
		[Range(0f, 1f)]
		public float ReactiveBinaryValue = 0.5f;

		// Token: 0x040000D4 RID: 212
		[Tooltip("Flags to determine how to generate the reactive mask")]
		public Fsr3.GenerateReactiveFlags flags = Fsr3.GenerateReactiveFlags.ApplyTonemap | Fsr3.GenerateReactiveFlags.ApplyThreshold | Fsr3.GenerateReactiveFlags.UseComponentsMax;

		// Token: 0x040000D5 RID: 213
		[Header("MipMap Settings")]
		public bool autoTextureUpdate = true;

		// Token: 0x040000D6 RID: 214
		public float updateFrequency = 2f;

		// Token: 0x040000D7 RID: 215
		[Range(0f, 1f)]
		public float mipMapBiasOverride = 1f;

		// Token: 0x040000D8 RID: 216
		[HideInInspector]
		[Tooltip("Optional texture to control the influence of the current frame on the reconstructed output. If unset, either an auto-generated or a default cleared reactive mask will be used.")]
		public Texture reactiveMask;

		// Token: 0x040000D9 RID: 217
		[HideInInspector]
		[Tooltip("Optional texture for marking areas of specialist rendering which should be accounted for during the upscaling process. If unset, a default cleared mask will be used.")]
		public Texture transparencyAndCompositionMask;

		// Token: 0x040000DA RID: 218
		[HideInInspector]
		[Tooltip("Choose where to get the exposure value from. Use auto-exposure from either FSR or Unity, provide a manual exposure texture, or use a default value.")]
		public FSR3.ExposureSource exposureSource = FSR3.ExposureSource.Auto;

		// Token: 0x040000DB RID: 219
		[HideInInspector]
		[Tooltip("Value by which the input signal will be divided, to get back to the original signal produced by the game.")]
		public float preExposure = 1f;

		// Token: 0x040000DC RID: 220
		[HideInInspector]
		[Tooltip("Optional 1x1 texture containing the exposure value for the current frame.")]
		public Texture exposure;

		// Token: 0x040000DD RID: 221
		[HideInInspector]
		[Tooltip("(Experimental) Automatically generate and use Reactive mask and Transparency & composition mask internally.")]
		public bool autoGenerateTransparencyAndComposition;

		// Token: 0x040000DE RID: 222
		[Tooltip("Parameters to control the process of auto-generating transparency and composition masks.")]
		public FSR3.GenerateTcrParameters generateTransparencyAndCompositionParameters = new FSR3.GenerateTcrParameters();

		// Token: 0x040000DF RID: 223
		[CompilerGenerated]
		private Vector2 <jitter>k__BackingField;

		// Token: 0x040000E0 RID: 224
		[CompilerGenerated]
		private RenderTargetIdentifier <colorOpaqueOnly>k__BackingField;

		// Token: 0x040000E1 RID: 225
		private Fsr3UpscalerContext _fsrContext;

		// Token: 0x040000E2 RID: 226
		private Fsr3UpscalerAssets _fsrAssets;

		// Token: 0x040000E3 RID: 227
		private Vector2Int _maxRenderSize;

		// Token: 0x040000E4 RID: 228
		private Vector2Int _displaySize;

		// Token: 0x040000E5 RID: 229
		private bool _resetHistory;

		// Token: 0x040000E6 RID: 230
		private readonly Fsr3.DispatchDescription _dispatchDescription = new Fsr3.DispatchDescription();

		// Token: 0x040000E7 RID: 231
		private readonly Fsr3.GenerateReactiveDescription _genReactiveDescription = new Fsr3.GenerateReactiveDescription();

		// Token: 0x040000E8 RID: 232
		private Fsr3.QualityMode _prevQualityMode;

		// Token: 0x040000E9 RID: 233
		private FSR3.ExposureSource _prevExposureSource;

		// Token: 0x040000EA RID: 234
		private Vector2Int _prevDisplaySize;

		// Token: 0x040000EB RID: 235
		private Rect _originalRect;

		// Token: 0x040000EC RID: 236
		protected ulong _previousLength;

		// Token: 0x040000ED RID: 237
		protected float _prevMipMapBias;

		// Token: 0x040000EE RID: 238
		protected float _mipMapTimer = float.MaxValue;

		// Token: 0x040000EF RID: 239
		private bool isStereoRendering;

		// Token: 0x02000078 RID: 120
		public enum ExposureSource
		{
			// Token: 0x040002F1 RID: 753
			Default,
			// Token: 0x040002F2 RID: 754
			Auto,
			// Token: 0x040002F3 RID: 755
			Unity,
			// Token: 0x040002F4 RID: 756
			Manual
		}

		// Token: 0x02000079 RID: 121
		[Serializable]
		public class GenerateTcrParameters
		{
			// Token: 0x0600026F RID: 623 RVA: 0x0001310C File Offset: 0x0001130C
			public GenerateTcrParameters()
			{
			}

			// Token: 0x040002F5 RID: 757
			[Tooltip("Setting this value too small will cause visual instability. Larger values can cause ghosting.")]
			[Range(0f, 1f)]
			public float autoTcThreshold = 0.05f;

			// Token: 0x040002F6 RID: 758
			[Tooltip("Smaller values will increase stability at hard edges of translucent objects.")]
			[Range(0f, 2f)]
			public float autoTcScale = 1f;

			// Token: 0x040002F7 RID: 759
			[Tooltip("Larger values result in more reactive pixels.")]
			[Range(0f, 10f)]
			public float autoReactiveScale = 5f;

			// Token: 0x040002F8 RID: 760
			[Tooltip("Maximum value reactivity can reach.")]
			[Range(0f, 1f)]
			public float autoReactiveMax = 0.9f;
		}
	}
}
