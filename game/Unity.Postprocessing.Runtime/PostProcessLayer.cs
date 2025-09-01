using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200005D RID: 93
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[ImageEffectAllowedInSceneView]
	[AddComponentMenu("Rendering/Post-process Layer", 1000)]
	[RequireComponent(typeof(Camera))]
	public sealed class PostProcessLayer : MonoBehaviour
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000C1B3 File Offset: 0x0000A3B3
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000C1BB File Offset: 0x0000A3BB
		public Dictionary<PostProcessEvent, List<PostProcessLayer.SerializedBundleRef>> sortedBundles
		{
			[CompilerGenerated]
			get
			{
				return this.<sortedBundles>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<sortedBundles>k__BackingField = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public DepthTextureMode cameraDepthFlags
		{
			[CompilerGenerated]
			get
			{
				return this.<cameraDepthFlags>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<cameraDepthFlags>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000C1D5 File Offset: 0x0000A3D5
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000C1DD File Offset: 0x0000A3DD
		public bool haveBundlesBeenInited
		{
			[CompilerGenerated]
			get
			{
				return this.<haveBundlesBeenInited>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<haveBundlesBeenInited>k__BackingField = value;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		private void OnEnable()
		{
			this.Init(null);
			if (!this.haveBundlesBeenInited)
			{
				this.InitBundles();
			}
			this.m_LogHistogram = new LogHistogram();
			this.m_PropertySheetFactory = new PropertySheetFactory();
			this.m_TargetPool = new TargetPool();
			this.debugLayer.OnEnable();
			if (RuntimeUtilities.scriptableRenderPipelineActive)
			{
				return;
			}
			this.InitLegacy();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000C244 File Offset: 0x0000A444
		private void InitLegacy()
		{
			this.m_LegacyCmdBufferBeforeReflections = new CommandBuffer
			{
				name = "Deferred Ambient Occlusion"
			};
			this.m_LegacyCmdBufferBeforeLighting = new CommandBuffer
			{
				name = "Deferred Ambient Occlusion"
			};
			this.m_LegacyCmdBufferOpaque = new CommandBuffer
			{
				name = "Opaque Only Post-processing"
			};
			this.m_LegacyCmdBufferTransparent = new CommandBuffer
			{
				name = "Before Transparent Only Post-processing"
			};
			this.m_LegacyCmdBuffer = new CommandBuffer
			{
				name = "Post-processing"
			};
			this.m_Camera = base.GetComponent<Camera>();
			this.m_Camera.AddCommandBuffer(CameraEvent.BeforeReflections, this.m_LegacyCmdBufferBeforeReflections);
			this.m_Camera.AddCommandBuffer(CameraEvent.BeforeLighting, this.m_LegacyCmdBufferBeforeLighting);
			this.m_Camera.AddCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_LegacyCmdBufferOpaque);
			this.m_Camera.AddCommandBuffer(CameraEvent.BeforeForwardAlpha, this.m_LegacyCmdBufferTransparent);
			this.m_Camera.AddCommandBuffer(CameraEvent.BeforeImageEffects, this.m_LegacyCmdBuffer);
			this.m_CurrentContext = new PostProcessRenderContext();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000C334 File Offset: 0x0000A534
		private bool DynamicResolutionAllowsFinalBlitToCameraTarget()
		{
			return !RuntimeUtilities.IsDynamicResolutionEnabled(this.m_Camera) || ((double)ScalableBufferManager.heightScaleFactor == 1.0 && (double)ScalableBufferManager.widthScaleFactor == 1.0);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000C36C File Offset: 0x0000A56C
		[ImageEffectUsesCommandBuffer]
		private void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			if (this.m_opaqueOnly != null)
			{
				RenderTexture.ReleaseTemporary(this.m_opaqueOnly);
				this.m_opaqueOnly = null;
			}
			if (this.m_CurrentContext.IsSGSRActive() || this.m_CurrentContext.IsFSR1Active() || this.m_CurrentContext.IsFSR3Active() || this.m_CurrentContext.IsDLSSActive() || this.m_CurrentContext.IsXeSSActive())
			{
				RuntimeUtilities.AllowDynamicResolution = true;
			}
			if (!this.finalBlitToCameraTarget && (this.m_CurrentContext.IsSGSRActive() || this.m_CurrentContext.IsFSR1Active() || this.m_CurrentContext.IsFSR3Active() || this.m_CurrentContext.IsDLSSActive() || this.m_CurrentContext.IsXeSSActive()))
			{
				if (this.m_CurrentContext.IsFSR1Active())
				{
					this.fsr1.ResetCameraViewport(this.m_CurrentContext);
				}
				if (this.m_CurrentContext.IsFSR3Active())
				{
					this.fsr3.ResetCameraViewport(this.m_CurrentContext);
					if (this.m_CurrentContext.stereoActive)
					{
						this.fsr3Stereo.ResetCameraViewport(this.m_CurrentContext);
					}
				}
				if (this.m_originalTargetTexture != null)
				{
					Graphics.Blit(this.m_upscaledOutput, this.m_originalTargetTexture);
					RenderTexture.active = dst;
					this.m_Camera.targetTexture = this.m_originalTargetTexture;
					this.m_originalTargetTexture = null;
				}
				else
				{
					Graphics.Blit(this.m_upscaledOutput, dst);
				}
				RenderTexture.ReleaseTemporary(this.m_upscaledOutput);
				this.m_upscaledOutput = null;
				return;
			}
			if (this.finalBlitToCameraTarget && !this.m_CurrentContext.stereoActive && this.DynamicResolutionAllowsFinalBlitToCameraTarget())
			{
				RenderTexture.active = dst;
				return;
			}
			Graphics.Blit(src, dst);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000C514 File Offset: 0x0000A714
		public void Init(PostProcessResources resources)
		{
			if (resources != null)
			{
				this.m_Resources = resources;
			}
			RuntimeUtilities.CreateIfNull<TemporalAntialiasing>(ref this.temporalAntialiasing);
			RuntimeUtilities.CreateIfNull<SGSR>(ref this.sgsr);
			RuntimeUtilities.CreateIfNull<FSR1>(ref this.fsr1);
			RuntimeUtilities.CreateIfNull<FSR3>(ref this.fsr3);
			RuntimeUtilities.CreateIfNull<FSR3>(ref this.fsr3Stereo);
			RuntimeUtilities.CreateIfNull<DLSS>(ref this.dlss);
			RuntimeUtilities.CreateIfNull<DLSS>(ref this.dlssStereo);
			RuntimeUtilities.CreateIfNull<XeSS>(ref this.xess);
			RuntimeUtilities.CreateIfNull<SubpixelMorphologicalAntialiasing>(ref this.subpixelMorphologicalAntialiasing);
			RuntimeUtilities.CreateIfNull<FastApproximateAntialiasing>(ref this.fastApproximateAntialiasing);
			RuntimeUtilities.CreateIfNull<Dithering>(ref this.dithering);
			RuntimeUtilities.CreateIfNull<Fog>(ref this.fog);
			RuntimeUtilities.CreateIfNull<PostProcessDebugLayer>(ref this.debugLayer);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		public void InitBundles()
		{
			if (this.haveBundlesBeenInited)
			{
				return;
			}
			RuntimeUtilities.CreateIfNull<List<PostProcessLayer.SerializedBundleRef>>(ref this.m_BeforeTransparentBundles);
			RuntimeUtilities.CreateIfNull<List<PostProcessLayer.SerializedBundleRef>>(ref this.m_BeforeUpscalingBundles);
			RuntimeUtilities.CreateIfNull<List<PostProcessLayer.SerializedBundleRef>>(ref this.m_BeforeStackBundles);
			RuntimeUtilities.CreateIfNull<List<PostProcessLayer.SerializedBundleRef>>(ref this.m_AfterStackBundles);
			this.m_Bundles = new Dictionary<Type, PostProcessBundle>();
			foreach (Type type in PostProcessManager.instance.settingsTypes.Keys)
			{
				PostProcessBundle value = new PostProcessBundle((PostProcessEffectSettings)ScriptableObject.CreateInstance(type));
				this.m_Bundles.Add(type, value);
			}
			this.UpdateBundleSortList(this.m_BeforeTransparentBundles, PostProcessEvent.BeforeTransparent);
			this.UpdateBundleSortList(this.m_BeforeUpscalingBundles, PostProcessEvent.BeforeUpscaling);
			this.UpdateBundleSortList(this.m_BeforeStackBundles, PostProcessEvent.BeforeStack);
			this.UpdateBundleSortList(this.m_AfterStackBundles, PostProcessEvent.AfterStack);
			this.sortedBundles = new Dictionary<PostProcessEvent, List<PostProcessLayer.SerializedBundleRef>>(default(PostProcessEventComparer))
			{
				{
					PostProcessEvent.BeforeTransparent,
					this.m_BeforeTransparentBundles
				},
				{
					PostProcessEvent.BeforeUpscaling,
					this.m_BeforeUpscalingBundles
				},
				{
					PostProcessEvent.BeforeStack,
					this.m_BeforeStackBundles
				},
				{
					PostProcessEvent.AfterStack,
					this.m_AfterStackBundles
				}
			};
			this.haveBundlesBeenInited = true;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		private void UpdateBundleSortList(List<PostProcessLayer.SerializedBundleRef> sortedList, PostProcessEvent evt)
		{
			List<PostProcessBundle> effects = (from kvp in this.m_Bundles
			where kvp.Value.attribute.eventType == evt && !kvp.Value.attribute.builtinEffect
			select kvp.Value).ToList<PostProcessBundle>();
			sortedList.RemoveAll(delegate(PostProcessLayer.SerializedBundleRef x)
			{
				string searchStr = x.assemblyQualifiedName;
				return !effects.Exists((PostProcessBundle b) => b.settings.GetType().AssemblyQualifiedName == searchStr);
			});
			foreach (PostProcessBundle postProcessBundle in effects)
			{
				string typeName = postProcessBundle.settings.GetType().AssemblyQualifiedName;
				if (!sortedList.Exists((PostProcessLayer.SerializedBundleRef b) => b.assemblyQualifiedName == typeName))
				{
					PostProcessLayer.SerializedBundleRef item = new PostProcessLayer.SerializedBundleRef
					{
						assemblyQualifiedName = typeName
					};
					sortedList.Add(item);
				}
			}
			foreach (PostProcessLayer.SerializedBundleRef serializedBundleRef in sortedList)
			{
				string typeName = serializedBundleRef.assemblyQualifiedName;
				PostProcessBundle bundle = effects.Find((PostProcessBundle b) => b.settings.GetType().AssemblyQualifiedName == typeName);
				serializedBundleRef.bundle = bundle;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000C86C File Offset: 0x0000AA6C
		private void OnDisable()
		{
			if (this.m_Camera != null)
			{
				if (this.m_LegacyCmdBufferBeforeReflections != null)
				{
					this.m_Camera.RemoveCommandBuffer(CameraEvent.BeforeReflections, this.m_LegacyCmdBufferBeforeReflections);
				}
				if (this.m_LegacyCmdBufferBeforeLighting != null)
				{
					this.m_Camera.RemoveCommandBuffer(CameraEvent.BeforeLighting, this.m_LegacyCmdBufferBeforeLighting);
				}
				if (this.m_LegacyCmdBufferOpaque != null)
				{
					this.m_Camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffectsOpaque, this.m_LegacyCmdBufferOpaque);
				}
				if (this.m_LegacyCmdBufferTransparent != null)
				{
					this.m_Camera.RemoveCommandBuffer(CameraEvent.BeforeForwardAlpha, this.m_LegacyCmdBufferTransparent);
				}
				if (this.m_LegacyCmdBuffer != null)
				{
					this.m_Camera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, this.m_LegacyCmdBuffer);
				}
			}
			this.temporalAntialiasing.Release();
			if (this.m_CurrentContext.IsFSR1Active())
			{
				this.fsr1.Release();
			}
			if (this.m_CurrentContext.IsFSR3Active())
			{
				this.fsr3.Release();
				if (this.fsr3Stereo != null)
				{
					this.fsr3Stereo.Release();
				}
			}
			this.m_LogHistogram.Release();
			foreach (PostProcessBundle postProcessBundle in this.m_Bundles.Values)
			{
				postProcessBundle.Release();
			}
			this.m_Bundles.Clear();
			this.m_PropertySheetFactory.Release();
			if (this.debugLayer != null)
			{
				this.debugLayer.OnDisable();
			}
			TextureLerper.instance.Clear();
			this.haveBundlesBeenInited = false;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000C9EC File Offset: 0x0000ABEC
		private void Reset()
		{
			this.volumeTrigger = base.transform;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		private void LateUpdate()
		{
			if (this.m_Camera.targetTexture != null && (this.m_CurrentContext.IsSGSRActive() || this.m_CurrentContext.IsFSR1Active() || this.m_CurrentContext.IsFSR3Active() || this.m_CurrentContext.IsDLSSActive() || this.m_CurrentContext.IsXeSSActive()))
			{
				this.m_originalTargetTexture = this.m_Camera.targetTexture;
				this.m_Camera.targetTexture = null;
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		private void OnPreCull()
		{
			if (RuntimeUtilities.scriptableRenderPipelineActive)
			{
				return;
			}
			if (this.m_Camera == null || this.m_CurrentContext == null)
			{
				this.InitLegacy();
			}
			if (SystemInfo.usesLoadStoreActions)
			{
				Rect rect = this.m_Camera.rect;
				if (Mathf.Abs(rect.x) > 1E-06f || Mathf.Abs(rect.y) > 1E-06f || Mathf.Abs(1f - rect.width) > 1E-06f || Mathf.Abs(1f - rect.height) > 1E-06f)
				{
					Debug.LogWarning("When used with builtin render pipeline, Postprocessing package expects to be used on a fullscreen Camera.\nPlease note that using Camera viewport may result in visual artefacts or some things not working.", this.m_Camera);
				}
			}
			if (this.m_CurrentContext.IsTemporalAntialiasingActive() || this.m_CurrentContext.IsSGSRActive() || this.m_CurrentContext.IsFSR1Active() || this.m_CurrentContext.IsFSR3Active() || this.m_CurrentContext.IsDLSSActive() || this.m_CurrentContext.IsXeSSActive())
			{
				if (!this.m_Camera.usePhysicalProperties)
				{
					this.m_Camera.ResetProjectionMatrix();
					this.m_Camera.nonJitteredProjectionMatrix = this.m_Camera.projectionMatrix;
				}
			}
			else
			{
				this.m_Camera.nonJitteredProjectionMatrix = this.m_Camera.projectionMatrix;
			}
			Shader.SetGlobalFloat(ShaderIDs.RenderViewportScaleFactor, 1f);
			this.BuildCommandBuffers();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000CBD1 File Offset: 0x0000ADD1
		private void OnPreRender()
		{
			if (RuntimeUtilities.scriptableRenderPipelineActive || this.m_Camera.stereoActiveEye != Camera.MonoOrStereoscopicEye.Right)
			{
				return;
			}
			this.BuildCommandBuffers();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000CBEF File Offset: 0x0000ADEF
		private static bool RequiresInitialBlit(Camera camera, PostProcessRenderContext context)
		{
			return true;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		private void UpdateSrcDstForOpaqueOnly(ref int src, ref int dst, PostProcessRenderContext context, RenderTargetIdentifier cameraTarget, int opaqueOnlyEffectsRemaining)
		{
			if (src > -1)
			{
				context.command.ReleaseTemporaryRT(src);
			}
			context.source = context.destination;
			src = dst;
			if (opaqueOnlyEffectsRemaining == 1)
			{
				context.destination = cameraTarget;
				return;
			}
			dst = this.m_TargetPool.Get();
			context.destination = dst;
			context.GetScreenSpaceTemporaryRT(context.command, dst, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000CC68 File Offset: 0x0000AE68
		private void BuildCommandBuffers()
		{
			PostProcessRenderContext currentContext = this.m_CurrentContext;
			RenderTextureFormat renderTextureFormat = this.m_Camera.targetTexture ? this.m_Camera.targetTexture.format : (this.m_Camera.allowHDR ? RuntimeUtilities.defaultHDRRenderTextureFormat : RenderTextureFormat.Default);
			if (!RuntimeUtilities.isFloatingPointFormat(renderTextureFormat))
			{
				this.m_NaNKilled = true;
			}
			currentContext.Reset();
			currentContext.camera = this.m_Camera;
			currentContext.sourceFormat = renderTextureFormat;
			this.m_LegacyCmdBufferBeforeReflections.Clear();
			this.m_LegacyCmdBufferBeforeLighting.Clear();
			this.m_LegacyCmdBufferOpaque.Clear();
			this.m_LegacyCmdBufferTransparent.Clear();
			this.m_LegacyCmdBuffer.Clear();
			this.SetupContext(currentContext);
			if (currentContext.IsSGSRActive())
			{
				this.antialiasingMode = this.sgsr.fallBackAA;
			}
			else if (currentContext.IsFSR1Active())
			{
				this._upscalerEnabled = true;
				if (!this.fsr1.IsSupported())
				{
					this.antialiasingMode = this.fsr1.fallBackAA;
				}
				if (!currentContext.stereoActive || (currentContext.stereoActive && currentContext.camera.stereoActiveEye != Camera.MonoOrStereoscopicEye.Right))
				{
					this.fsr1.ConfigureCameraViewport(currentContext);
				}
				currentContext.SetRenderSize(this.fsr1.renderSize);
			}
			else if (currentContext.IsFSR3Active())
			{
				this._upscalerEnabled = true;
				if (!this.fsr3.IsSupported())
				{
					this.antialiasingMode = this.fsr3.fallBackAA;
				}
				if (!this.fsr3Stereo.IsSupported())
				{
					this.antialiasingMode = this.fsr3.fallBackAA;
				}
				this.fsr3.ConfigureCameraViewport(currentContext);
				if (currentContext.stereoActive)
				{
					this.fsr3Stereo.ConfigureCameraViewportRightEye(currentContext);
				}
				currentContext.SetRenderSize(this.fsr3.renderSize);
			}
			else if (currentContext.IsDLSSActive())
			{
				this.antialiasingMode = this.dlss.fallBackAA;
			}
			else if (currentContext.IsXeSSActive())
			{
				this.antialiasingMode = this.xess.fallBackAA;
			}
			else
			{
				if (currentContext.camera.cameraType == CameraType.Game && this._upscalerEnabled)
				{
					this._upscalerEnabled = false;
					this.fsr1.Release();
					this.fsr3.Release();
					if (this.fsr3Stereo != null)
					{
						this.fsr3Stereo.Release();
					}
				}
				if (this.m_originalTargetTexture != null)
				{
					this.m_Camera.targetTexture = this.m_originalTargetTexture;
					this.m_originalTargetTexture = null;
				}
			}
			currentContext.command = this.m_LegacyCmdBufferOpaque;
			TextureLerper.instance.BeginFrame(currentContext);
			this.UpdateVolumeSystem(currentContext.camera, currentContext.command);
			PostProcessBundle bundle = this.GetBundle<AmbientOcclusion>();
			AmbientOcclusion ambientOcclusion = bundle.CastSettings<AmbientOcclusion>();
			AmbientOcclusionRenderer ambientOcclusionRenderer = bundle.CastRenderer<AmbientOcclusionRenderer>();
			bool flag = ambientOcclusion.IsEnabledAndSupported(currentContext);
			bool flag2 = ambientOcclusionRenderer.IsAmbientOnly(currentContext);
			bool flag3 = flag && flag2;
			bool flag4 = flag && !flag2;
			PostProcessBundle bundle2 = this.GetBundle<ScreenSpaceReflections>();
			PostProcessEffectSettings settings = bundle2.settings;
			PostProcessEffectRenderer renderer = bundle2.renderer;
			bool flag5 = settings.IsEnabledAndSupported(currentContext);
			if (currentContext.stereoActive)
			{
				currentContext.UpdateSinglePassStereoState(currentContext.IsTemporalAntialiasingActive() || currentContext.IsSGSRActive() || currentContext.IsFSR1Active() || currentContext.IsFSR3Active() || currentContext.IsDLSSActive() || currentContext.IsXeSSActive(), flag, flag5);
			}
			if (flag3)
			{
				IAmbientOcclusionMethod ambientOcclusionMethod = ambientOcclusionRenderer.Get();
				currentContext.command = this.m_LegacyCmdBufferBeforeReflections;
				ambientOcclusionMethod.RenderAmbientOnly(currentContext);
				currentContext.command = this.m_LegacyCmdBufferBeforeLighting;
				ambientOcclusionMethod.CompositeAmbientOnly(currentContext);
			}
			else if (flag4)
			{
				currentContext.command = this.m_LegacyCmdBufferOpaque;
				ambientOcclusionRenderer.Get().RenderAfterOpaque(currentContext);
			}
			bool flag6 = this.fog.IsEnabledAndSupported(currentContext);
			bool flag7 = this.HasOpaqueOnlyEffects(currentContext);
			int num = 0;
			num += (flag5 ? 1 : 0);
			num += (flag6 ? 1 : 0);
			num += (flag7 ? 1 : 0);
			RenderTargetIdentifier renderTargetIdentifier = new RenderTargetIdentifier(BuiltinRenderTextureType.CameraTarget);
			if (num > 0)
			{
				CommandBuffer legacyCmdBufferOpaque = this.m_LegacyCmdBufferOpaque;
				currentContext.command = legacyCmdBufferOpaque;
				currentContext.source = renderTargetIdentifier;
				currentContext.destination = renderTargetIdentifier;
				int nameID = -1;
				int num2 = -1;
				this.UpdateSrcDstForOpaqueOnly(ref nameID, ref num2, currentContext, renderTargetIdentifier, num + 1);
				if (PostProcessLayer.RequiresInitialBlit(this.m_Camera, currentContext) || num == 1)
				{
					legacyCmdBufferOpaque.BuiltinBlit(currentContext.source, currentContext.destination, RuntimeUtilities.copyStdMaterial, this.stopNaNPropagation ? 1 : 0);
					this.UpdateSrcDstForOpaqueOnly(ref nameID, ref num2, currentContext, renderTargetIdentifier, num);
				}
				if (flag5)
				{
					renderer.RenderOrLog(currentContext);
					num--;
					this.UpdateSrcDstForOpaqueOnly(ref nameID, ref num2, currentContext, renderTargetIdentifier, num);
				}
				if (flag6)
				{
					this.fog.Render(currentContext);
					num--;
					this.UpdateSrcDstForOpaqueOnly(ref nameID, ref num2, currentContext, renderTargetIdentifier, num);
				}
				if (flag7)
				{
					this.RenderOpaqueOnly(currentContext);
				}
				legacyCmdBufferOpaque.ReleaseTemporaryRT(nameID);
			}
			if (currentContext.camera.stereoActiveEye == Camera.MonoOrStereoscopicEye.Right)
			{
				this._runRightEyeOnceCommandBuffers = !this._runRightEyeOnceCommandBuffers;
				if (this._runRightEyeOnceCommandBuffers)
				{
					goto IL_51E;
				}
			}
			if (currentContext.IsFSR3Active() && (this.fsr3.autoGenerateReactiveMask || this.fsr3.autoGenerateTransparencyAndComposition))
			{
				Vector2Int scaledRenderSize = this.fsr3.GetScaledRenderSize(currentContext.camera);
				this.m_opaqueOnly = currentContext.GetScreenSpaceTemporaryRT(0, renderTextureFormat, RenderTextureReadWrite.Default, scaledRenderSize.x, scaledRenderSize.y);
				this.m_LegacyCmdBufferTransparent.BuiltinBlit(renderTargetIdentifier, this.m_opaqueOnly);
			}
			IL_51E:
			int num3 = -1;
			bool flag8 = !this.m_NaNKilled && this.stopNaNPropagation && RuntimeUtilities.isFloatingPointFormat(renderTextureFormat);
			if ((!currentContext.stereoActive || currentContext.numberOfEyes <= 1 || currentContext.stereoRenderingMode != PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced) && (PostProcessLayer.RequiresInitialBlit(this.m_Camera, currentContext) || flag8))
			{
				int width = currentContext.width;
				num3 = this.m_TargetPool.Get();
				currentContext.GetScreenSpaceTemporaryRT(this.m_LegacyCmdBuffer, num3, 0, renderTextureFormat, RenderTextureReadWrite.sRGB, FilterMode.Bilinear, width, 0, false);
				this.m_LegacyCmdBuffer.BuiltinBlit(renderTargetIdentifier, num3, RuntimeUtilities.copyStdMaterial, this.stopNaNPropagation ? 1 : 0);
				if (!this.m_NaNKilled)
				{
					this.m_NaNKilled = this.stopNaNPropagation;
				}
				currentContext.source = num3;
			}
			else
			{
				currentContext.source = renderTargetIdentifier;
			}
			currentContext.destination = renderTargetIdentifier;
			if (!this.finalBlitToCameraTarget && this.m_CurrentContext.IsFSR1Active())
			{
				Vector2Int displaySize = this.fsr1.displaySize;
				if (this.m_upscaledOutput == null)
				{
					this.m_upscaledOutput = currentContext.GetScreenSpaceTemporaryRT(0, RenderTextureFormat.Default, RenderTextureReadWrite.Default, displaySize.x, displaySize.y);
				}
				currentContext.destination = this.m_upscaledOutput;
			}
			if (!this.finalBlitToCameraTarget && this.m_CurrentContext.IsFSR3Active())
			{
				Vector2Int displaySize2 = this.fsr3.displaySize;
				if (this.m_upscaledOutput == null)
				{
					this.m_upscaledOutput = currentContext.GetScreenSpaceTemporaryRT(0, RenderTextureFormat.Default, RenderTextureReadWrite.Default, displaySize2.x, displaySize2.y);
				}
				currentContext.destination = this.m_upscaledOutput;
			}
			if (this.finalBlitToCameraTarget && !this.m_CurrentContext.stereoActive && !RuntimeUtilities.scriptableRenderPipelineActive && this.DynamicResolutionAllowsFinalBlitToCameraTarget())
			{
				if (this.m_Camera.targetTexture)
				{
					currentContext.destination = this.m_Camera.targetTexture.colorBuffer;
				}
				else
				{
					currentContext.flip = true;
					currentContext.destination = Display.main.colorBuffer;
				}
			}
			currentContext.command = this.m_LegacyCmdBuffer;
			this.Render(currentContext);
			if (num3 > -1)
			{
				this.m_LegacyCmdBuffer.ReleaseTemporaryRT(num3);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		private void OnPostRender()
		{
			if (RuntimeUtilities.scriptableRenderPipelineActive)
			{
				return;
			}
			if (this.finalBlitToCameraTarget && this.m_CurrentContext.IsFSR1Active())
			{
				this.fsr1.ResetCameraViewport(this.m_CurrentContext);
			}
			if (this.finalBlitToCameraTarget && this.m_CurrentContext.IsFSR3Active())
			{
				this.fsr3.ResetCameraViewport(this.m_CurrentContext);
				if (this.m_CurrentContext.stereoActive)
				{
					this.fsr3Stereo.ResetCameraViewport(this.m_CurrentContext);
				}
			}
			if (this.m_CurrentContext.IsTemporalAntialiasingActive() || this.m_CurrentContext.IsSGSRActive() || this.m_CurrentContext.IsFSR1Active() || this.m_CurrentContext.IsFSR3Active() || this.m_CurrentContext.IsDLSSActive() || this.m_CurrentContext.IsXeSSActive())
			{
				if (this.m_CurrentContext.physicalCamera)
				{
					this.m_Camera.usePhysicalProperties = true;
					return;
				}
				this.m_Camera.ResetProjectionMatrix();
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000D4AA File Offset: 0x0000B6AA
		public PostProcessBundle GetBundle<T>() where T : PostProcessEffectSettings
		{
			return this.GetBundle(typeof(T));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		public PostProcessBundle GetBundle(Type settingsType)
		{
			return this.m_Bundles[settingsType];
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000D4CA File Offset: 0x0000B6CA
		public T GetSettings<T>() where T : PostProcessEffectSettings
		{
			return this.GetBundle<T>().CastSettings<T>();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000D4D7 File Offset: 0x0000B6D7
		public void BakeMSVOMap(CommandBuffer cmd, Camera camera, RenderTargetIdentifier destination, RenderTargetIdentifier? depthMap, bool invert, bool isMSAA = false)
		{
			MultiScaleVO multiScaleVO = this.GetBundle<AmbientOcclusion>().CastRenderer<AmbientOcclusionRenderer>().GetMultiScaleVO();
			multiScaleVO.SetResources(this.m_Resources);
			multiScaleVO.GenerateAOMap(cmd, camera, destination, depthMap, invert, isMSAA);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000D504 File Offset: 0x0000B704
		internal void OverrideSettings(List<PostProcessEffectSettings> baseSettings, float interpFactor)
		{
			foreach (PostProcessEffectSettings postProcessEffectSettings in baseSettings)
			{
				if (postProcessEffectSettings.active)
				{
					PostProcessEffectSettings settings = this.GetBundle(postProcessEffectSettings.GetType()).settings;
					int count = postProcessEffectSettings.parameters.Count;
					for (int i = 0; i < count; i++)
					{
						ParameterOverride parameterOverride = postProcessEffectSettings.parameters[i];
						if (parameterOverride.overrideState)
						{
							ParameterOverride parameterOverride2 = settings.parameters[i];
							parameterOverride2.Interp(parameterOverride2, parameterOverride, interpFactor);
						}
					}
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		private void SetLegacyCameraFlags(PostProcessRenderContext context)
		{
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			foreach (KeyValuePair<Type, PostProcessBundle> keyValuePair in this.m_Bundles)
			{
				if (keyValuePair.Value.settings.IsEnabledAndSupported(context))
				{
					depthTextureMode |= keyValuePair.Value.renderer.GetCameraFlags();
				}
			}
			if (context.IsTemporalAntialiasingActive())
			{
				depthTextureMode |= this.temporalAntialiasing.GetCameraFlags();
			}
			if (context.IsFSR3Active())
			{
				depthTextureMode |= this.fsr3.GetCameraFlags();
			}
			if (this.fog.IsEnabledAndSupported(context))
			{
				depthTextureMode |= this.fog.GetCameraFlags();
			}
			if (this.debugLayer.debugOverlay != DebugOverlay.None)
			{
				depthTextureMode |= this.debugLayer.GetCameraFlags();
			}
			context.camera.depthTextureMode |= depthTextureMode;
			this.cameraDepthFlags = depthTextureMode;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		public void ResetHistory()
		{
			foreach (KeyValuePair<Type, PostProcessBundle> keyValuePair in this.m_Bundles)
			{
				keyValuePair.Value.ResetHistory();
			}
			this.temporalAntialiasing.ResetHistory();
			this.fsr3.OnResetCamera();
			if (this.fsr3Stereo != null)
			{
				this.fsr3Stereo.OnResetCamera();
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000D728 File Offset: 0x0000B928
		public bool HasOpaqueOnlyEffects(PostProcessRenderContext context)
		{
			return this.HasActiveEffects(PostProcessEvent.BeforeTransparent, context);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000D734 File Offset: 0x0000B934
		public bool HasActiveEffects(PostProcessEvent evt, PostProcessRenderContext context)
		{
			foreach (PostProcessLayer.SerializedBundleRef serializedBundleRef in this.sortedBundles[evt])
			{
				bool flag = serializedBundleRef.bundle.settings.IsEnabledAndSupported(context);
				if (context.isSceneView)
				{
					if (serializedBundleRef.bundle.attribute.allowInSceneView && flag)
					{
						return true;
					}
				}
				else if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		private void SetupContext(PostProcessRenderContext context)
		{
			if (this.m_OldResources != this.m_Resources || !RuntimeUtilities.isValidResources())
			{
				RuntimeUtilities.UpdateResources(this.m_Resources);
				this.m_OldResources = this.m_Resources;
			}
			this.m_IsRenderingInSceneView = (context.camera.cameraType == CameraType.SceneView);
			context.isSceneView = this.m_IsRenderingInSceneView;
			context.resources = this.m_Resources;
			context.propertySheets = this.m_PropertySheetFactory;
			context.debugLayer = this.debugLayer;
			context.antialiasing = this.antialiasingMode;
			context.temporalAntialiasing = this.temporalAntialiasing;
			context.sgsr = this.sgsr;
			context.superResolution1 = this.fsr1;
			context.superResolution3 = this.fsr3;
			context.deepLearningSuperSampling = this.dlss;
			context.xeSuperSampling = this.xess;
			context.logHistogram = this.m_LogHistogram;
			context.physicalCamera = context.camera.usePhysicalProperties;
			this.SetLegacyCameraFlags(context);
			this.debugLayer.SetFrameSize(context.width, context.height);
			this.m_CurrentContext = context;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000D8DC File Offset: 0x0000BADC
		public void UpdateVolumeSystem(Camera cam, CommandBuffer cmd)
		{
			if (this.m_SettingsUpdateNeeded)
			{
				cmd.BeginSample("VolumeBlending");
				PostProcessManager.instance.UpdateSettings(this, cam);
				cmd.EndSample("VolumeBlending");
				this.m_TargetPool.Reset();
				if (RuntimeUtilities.scriptableRenderPipelineActive)
				{
					Shader.SetGlobalFloat(ShaderIDs.RenderViewportScaleFactor, 1f);
				}
			}
			this.m_SettingsUpdateNeeded = false;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000D93C File Offset: 0x0000BB3C
		public void RenderOpaqueOnly(PostProcessRenderContext context)
		{
			if (RuntimeUtilities.scriptableRenderPipelineActive)
			{
				this.SetupContext(context);
			}
			TextureLerper.instance.BeginFrame(context);
			this.UpdateVolumeSystem(context.camera, context.command);
			this.RenderList(this.sortedBundles[PostProcessEvent.BeforeTransparent], context, "OpaqueOnly");
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000D98C File Offset: 0x0000BB8C
		public void Render(PostProcessRenderContext context)
		{
			if (RuntimeUtilities.scriptableRenderPipelineActive)
			{
				this.SetupContext(context);
			}
			TextureLerper.instance.BeginFrame(context);
			CommandBuffer command = context.command;
			this.UpdateVolumeSystem(context.camera, context.command);
			int num = -1;
			RenderTargetIdentifier source = context.source;
			if (context.stereoActive && context.numberOfEyes > 1 && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass)
			{
				command.SetSinglePassStereo(SinglePassStereoMode.None);
				command.DisableShaderKeyword("UNITY_SINGLE_PASS_STEREO");
			}
			int i = 0;
			while (i < context.numberOfEyes)
			{
				bool flag = false;
				if (this.stopNaNPropagation && !this.m_NaNKilled)
				{
					num = this.m_TargetPool.Get();
					context.GetScreenSpaceTemporaryRT(command, num, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
					if (context.stereoActive && context.numberOfEyes > 1)
					{
						if (context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced)
						{
							command.BlitFullscreenTriangleFromTexArray(context.source, num, RuntimeUtilities.copyFromTexArraySheet, 1, false, i);
							flag = true;
						}
						else if (context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass)
						{
							command.BlitFullscreenTriangleFromDoubleWide(context.source, num, RuntimeUtilities.copyStdFromDoubleWideMaterial, 1, i);
							flag = true;
						}
					}
					else
					{
						command.BlitFullscreenTriangle(context.source, num, RuntimeUtilities.copySheet, 1, false, null, false);
					}
					context.source = num;
					this.m_NaNKilled = true;
				}
				if (!flag && context.numberOfEyes > 1)
				{
					num = this.m_TargetPool.Get();
					context.GetScreenSpaceTemporaryRT(command, num, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
					if (context.stereoActive)
					{
						if (context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced)
						{
							command.BlitFullscreenTriangleFromTexArray(context.source, num, RuntimeUtilities.copyFromTexArraySheet, 1, false, i);
						}
						else if (context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass)
						{
							command.BlitFullscreenTriangleFromDoubleWide(context.source, num, RuntimeUtilities.copyStdFromDoubleWideMaterial, this.stopNaNPropagation ? 1 : 0, i);
						}
					}
					context.source = num;
				}
				if (this.HasActiveEffects(PostProcessEvent.BeforeUpscaling, context))
				{
					num = this.RenderInjectionPoint(PostProcessEvent.BeforeUpscaling, context, "BeforeUpscaling", num);
				}
				if (!context.stereoActive)
				{
					goto IL_264;
				}
				if (context.IsTemporalAntialiasingActive() || context.IsSGSRActive() || context.IsFSR1Active() || context.IsFSR1Active() || context.IsFSR3Active() || context.IsDLSSActive() || context.IsXeSSActive())
				{
					QualitySettings.antiAliasing = 0;
				}
				if (context.camera.stereoActiveEye != Camera.MonoOrStereoscopicEye.Right)
				{
					goto IL_264;
				}
				this._runRightEyeOnce = !this._runRightEyeOnce;
				if (!this._runRightEyeOnce)
				{
					goto IL_264;
				}
				IL_548:
				bool flag2 = this.HasActiveEffects(PostProcessEvent.BeforeStack, context);
				bool flag3 = this.HasActiveEffects(PostProcessEvent.AfterStack, context) && !this.breakBeforeColorGrading;
				bool flag4 = (flag3 || this.antialiasingMode == PostProcessLayer.Antialiasing.FastApproximateAntialiasing || (this.antialiasingMode == PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing && this.subpixelMorphologicalAntialiasing.IsSupported())) && !this.breakBeforeColorGrading;
				if (flag2)
				{
					num = this.RenderInjectionPoint(PostProcessEvent.BeforeStack, context, "BeforeStack", num);
				}
				num = this.RenderBuiltins(context, !flag4, num, i);
				if (flag3)
				{
					num = this.RenderInjectionPoint(PostProcessEvent.AfterStack, context, "AfterStack", num);
				}
				if (flag4)
				{
					this.RenderFinalPass(context, num, i);
				}
				if (context.stereoActive)
				{
					context.source = source;
				}
				i++;
				continue;
				IL_264:
				if (context.IsTemporalAntialiasingActive() || context.IsSGSRActive() || context.IsFSR1Active())
				{
					if (!RuntimeUtilities.scriptableRenderPipelineActive)
					{
						if (context.stereoActive)
						{
							if (context.camera.stereoActiveEye != Camera.MonoOrStereoscopicEye.Right)
							{
								this.temporalAntialiasing.ConfigureStereoJitteredProjectionMatrices(context);
							}
						}
						else
						{
							this.temporalAntialiasing.ConfigureJitteredProjectionMatrix(context);
						}
					}
					int num2 = this.m_TargetPool.Get();
					RenderTargetIdentifier destination = context.destination;
					context.GetScreenSpaceTemporaryRT(command, num2, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
					context.destination = num2;
					this.temporalAntialiasing.Render(context);
					context.source = num2;
					context.destination = destination;
					if (num > -1)
					{
						command.ReleaseTemporaryRT(num);
					}
					num = num2;
					if (!context.IsSGSRActive() && context.IsFSR1Active())
					{
						context.SetRenderSize(this.fsr1.displaySize);
						int num3 = this.m_TargetPool.Get();
						destination = context.destination;
						context.GetScreenSpaceTemporaryRT(command, num3, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, true);
						context.destination = num3;
						this.fsr1.Render(context);
						context.source = num3;
						context.destination = destination;
						RuntimeUtilities.AllowDynamicResolution = false;
						if (num > -1)
						{
							command.ReleaseTemporaryRT(num);
						}
						num = num3;
						goto IL_548;
					}
					goto IL_548;
				}
				else if (context.IsFSR3Active())
				{
					if (!context.stereoActive || (context.stereoActive && context.camera.stereoActiveEye == Camera.MonoOrStereoscopicEye.Left))
					{
						this.fsr3.ConfigureJitteredProjectionMatrix(context);
						context.SetRenderSize(this.fsr3.displaySize);
						int num4 = this.m_TargetPool.Get();
						RenderTargetIdentifier destination2 = context.destination;
						context.GetScreenSpaceTemporaryRT(command, num4, 0, context.sourceFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, 0, 0, true);
						context.destination = num4;
						this.fsr3.colorOpaqueOnly = this.m_opaqueOnly;
						this.fsr3.Render(context, false);
						context.source = num4;
						context.destination = destination2;
						RuntimeUtilities.AllowDynamicResolution = false;
						if (num > -1)
						{
							command.ReleaseTemporaryRT(num);
						}
						num = num4;
						goto IL_548;
					}
					if (context.stereoActive && context.camera.stereoActiveEye == Camera.MonoOrStereoscopicEye.Right)
					{
						context.SetRenderSize(this.fsr3Stereo.displaySize);
						int num5 = this.m_TargetPool.Get();
						RenderTargetIdentifier destination3 = context.destination;
						context.GetScreenSpaceTemporaryRT(command, num5, 0, context.sourceFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, 0, 0, true);
						context.destination = num5;
						this.fsr3Stereo.colorOpaqueOnly = this.m_opaqueOnly;
						this.fsr3Stereo.Render(context, true);
						context.source = num5;
						context.destination = destination3;
						RuntimeUtilities.AllowDynamicResolution = false;
						if (num > -1)
						{
							command.ReleaseTemporaryRT(num);
						}
						num = num5;
						goto IL_548;
					}
					goto IL_548;
				}
				else
				{
					if (!context.IsDLSSActive())
					{
						context.IsXeSSActive();
						goto IL_548;
					}
					goto IL_548;
				}
			}
			if (context.stereoActive && context.numberOfEyes > 1 && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass)
			{
				command.SetSinglePassStereo(SinglePassStereoMode.SideBySide);
				command.EnableShaderKeyword("UNITY_SINGLE_PASS_STEREO");
			}
			this.debugLayer.RenderSpecialOverlays(context);
			this.debugLayer.RenderMonitors(context);
			TextureLerper.instance.EndFrame();
			this.debugLayer.EndFrame();
			this.m_SettingsUpdateNeeded = true;
			this.m_NaNKilled = false;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		private int RenderInjectionPoint(PostProcessEvent evt, PostProcessRenderContext context, string marker, int releaseTargetAfterUse = -1)
		{
			int num = this.m_TargetPool.Get();
			RenderTargetIdentifier destination = context.destination;
			CommandBuffer command = context.command;
			context.GetScreenSpaceTemporaryRT(command, num, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
			context.destination = num;
			this.RenderList(this.sortedBundles[evt], context, marker);
			context.source = num;
			context.destination = destination;
			if (releaseTargetAfterUse > -1)
			{
				command.ReleaseTemporaryRT(releaseTargetAfterUse);
			}
			return num;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000E074 File Offset: 0x0000C274
		private void RenderList(List<PostProcessLayer.SerializedBundleRef> list, PostProcessRenderContext context, string marker)
		{
			CommandBuffer command = context.command;
			command.BeginSample(marker);
			this.m_ActiveEffects.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				PostProcessBundle bundle = list[i].bundle;
				if (bundle.settings.IsEnabledAndSupported(context) && (!context.isSceneView || (context.isSceneView && bundle.attribute.allowInSceneView)))
				{
					this.m_ActiveEffects.Add(bundle.renderer);
				}
			}
			int count = this.m_ActiveEffects.Count;
			if (count == 1)
			{
				this.m_ActiveEffects[0].RenderOrLog(context);
			}
			else
			{
				this.m_Targets.Clear();
				this.m_Targets.Add(context.source);
				int num = this.m_TargetPool.Get();
				int num2 = this.m_TargetPool.Get();
				for (int j = 0; j < count - 1; j++)
				{
					this.m_Targets.Add((j % 2 == 0) ? num : num2);
				}
				this.m_Targets.Add(context.destination);
				context.GetScreenSpaceTemporaryRT(command, num, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
				if (count > 2)
				{
					context.GetScreenSpaceTemporaryRT(command, num2, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
				}
				for (int k = 0; k < count; k++)
				{
					context.source = this.m_Targets[k];
					context.destination = this.m_Targets[k + 1];
					this.m_ActiveEffects[k].RenderOrLog(context);
				}
				command.ReleaseTemporaryRT(num);
				if (count > 2)
				{
					command.ReleaseTemporaryRT(num2);
				}
			}
			command.EndSample(marker);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000E226 File Offset: 0x0000C426
		private void ApplyFlip(PostProcessRenderContext context, MaterialPropertyBlock properties)
		{
			if (context.flip && !context.isSceneView)
			{
				properties.SetVector(ShaderIDs.UVTransform, new Vector4(1f, 1f, 0f, 0f));
				return;
			}
			this.ApplyDefaultFlip(properties);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000E264 File Offset: 0x0000C464
		private void ApplyDefaultFlip(MaterialPropertyBlock properties)
		{
			properties.SetVector(ShaderIDs.UVTransform, SystemInfo.graphicsUVStartsAtTop ? new Vector4(1f, -1f, 0f, 1f) : new Vector4(1f, 1f, 0f, 0f));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		private int RenderBuiltins(PostProcessRenderContext context, bool isFinalPass, int releaseTargetAfterUse = -1, int eye = -1)
		{
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.uber);
			propertySheet.ClearKeywords();
			propertySheet.properties.Clear();
			context.uberSheet = propertySheet;
			context.autoExposureTexture = RuntimeUtilities.whiteTexture;
			context.bloomBufferNameID = -1;
			if (isFinalPass && context.stereoActive && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced)
			{
				propertySheet.EnableKeyword("STEREO_INSTANCING_ENABLED");
			}
			CommandBuffer command = context.command;
			command.BeginSample("BuiltinStack");
			int num = -1;
			RenderTargetIdentifier destination = context.destination;
			if (!isFinalPass)
			{
				num = this.m_TargetPool.Get();
				context.GetScreenSpaceTemporaryRT(command, num, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
				context.destination = num;
				if (this.antialiasingMode == PostProcessLayer.Antialiasing.FastApproximateAntialiasing && !this.fastApproximateAntialiasing.keepAlpha && RuntimeUtilities.hasAlpha(context.sourceFormat))
				{
					propertySheet.properties.SetFloat(ShaderIDs.LumaInAlpha, 1f);
				}
			}
			int num2 = this.RenderEffect<DepthOfField>(context, true);
			int num3 = this.RenderEffect<MotionBlur>(context, true);
			if (this.ShouldGenerateLogHistogram(context))
			{
				this.m_LogHistogram.Generate(context);
			}
			int xrActiveEye = context.xrActiveEye;
			if (context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.MultiPass)
			{
				context.xrActiveEye = eye;
			}
			this.RenderEffect<AutoExposure>(context, false);
			context.xrActiveEye = xrActiveEye;
			propertySheet.properties.SetTexture(ShaderIDs.AutoExposureTex, context.autoExposureTexture);
			this.RenderEffect<LensDistortion>(context, false);
			this.RenderEffect<ChromaticAberration>(context, false);
			this.RenderEffect<Bloom>(context, false);
			this.RenderEffect<Vignette>(context, false);
			this.RenderEffect<Grain>(context, false);
			if (!this.breakBeforeColorGrading)
			{
				this.RenderEffect<ColorGrading>(context, false);
			}
			if (isFinalPass)
			{
				propertySheet.EnableKeyword("FINALPASS");
				this.dithering.Render(context);
				this.ApplyFlip(context, propertySheet.properties);
			}
			else
			{
				this.ApplyDefaultFlip(propertySheet.properties);
			}
			if (context.stereoActive && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced)
			{
				propertySheet.properties.SetFloat(ShaderIDs.DepthSlice, (float)eye);
				command.BlitFullscreenTriangleToTexArray(context.source, context.destination, propertySheet, 0, false, eye);
			}
			else if (isFinalPass && context.stereoActive && context.numberOfEyes > 1 && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass)
			{
				command.BlitFullscreenTriangleToDoubleWide(context.source, context.destination, propertySheet, 0, eye);
			}
			else
			{
				command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
			}
			context.source = context.destination;
			context.destination = destination;
			if (releaseTargetAfterUse > -1)
			{
				command.ReleaseTemporaryRT(releaseTargetAfterUse);
			}
			if (num3 > -1)
			{
				command.ReleaseTemporaryRT(num3);
			}
			if (num2 > -1)
			{
				command.ReleaseTemporaryRT(num2);
			}
			if (context.bloomBufferNameID > -1)
			{
				command.ReleaseTemporaryRT(context.bloomBufferNameID);
			}
			command.EndSample("BuiltinStack");
			return num;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000E574 File Offset: 0x0000C774
		private void RenderFinalPass(PostProcessRenderContext context, int releaseTargetAfterUse = -1, int eye = -1)
		{
			CommandBuffer command = context.command;
			command.BeginSample("FinalPass");
			if (this.breakBeforeColorGrading)
			{
				PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.discardAlpha);
				if (context.stereoActive && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced)
				{
					propertySheet.EnableKeyword("STEREO_INSTANCING_ENABLED");
				}
				if (context.stereoActive && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced)
				{
					propertySheet.properties.SetFloat(ShaderIDs.DepthSlice, (float)eye);
					command.BlitFullscreenTriangleToTexArray(context.source, context.destination, propertySheet, 0, false, eye);
				}
				else if (context.stereoActive && context.numberOfEyes > 1 && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass)
				{
					command.BlitFullscreenTriangleToDoubleWide(context.source, context.destination, propertySheet, 0, eye);
				}
				else
				{
					command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
				}
			}
			else
			{
				PropertySheet propertySheet2 = context.propertySheets.Get(context.resources.shaders.finalPass);
				propertySheet2.ClearKeywords();
				propertySheet2.properties.Clear();
				context.uberSheet = propertySheet2;
				int num = -1;
				if (context.stereoActive && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced)
				{
					propertySheet2.EnableKeyword("STEREO_INSTANCING_ENABLED");
				}
				if (this.antialiasingMode == PostProcessLayer.Antialiasing.FastApproximateAntialiasing)
				{
					propertySheet2.EnableKeyword(this.fastApproximateAntialiasing.fastMode ? "FXAA_LOW" : "FXAA");
					if (RuntimeUtilities.hasAlpha(context.sourceFormat))
					{
						if (this.fastApproximateAntialiasing.keepAlpha)
						{
							propertySheet2.EnableKeyword("FXAA_KEEP_ALPHA");
						}
					}
					else
					{
						propertySheet2.EnableKeyword("FXAA_NO_ALPHA");
					}
				}
				else if (this.antialiasingMode == PostProcessLayer.Antialiasing.SubpixelMorphologicalAntialiasing && this.subpixelMorphologicalAntialiasing.IsSupported())
				{
					num = this.m_TargetPool.Get();
					RenderTargetIdentifier destination = context.destination;
					context.GetScreenSpaceTemporaryRT(context.command, num, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
					context.destination = num;
					this.subpixelMorphologicalAntialiasing.Render(context);
					context.source = num;
					context.destination = destination;
				}
				this.dithering.Render(context);
				this.ApplyFlip(context, propertySheet2.properties);
				if (context.stereoActive && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced)
				{
					propertySheet2.properties.SetFloat(ShaderIDs.DepthSlice, (float)eye);
					command.BlitFullscreenTriangleToTexArray(context.source, context.destination, propertySheet2, 0, false, eye);
				}
				else if (context.stereoActive && context.numberOfEyes > 1 && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass)
				{
					command.BlitFullscreenTriangleToDoubleWide(context.source, context.destination, propertySheet2, 0, eye);
				}
				else
				{
					command.BlitFullscreenTriangle(context.source, context.destination, propertySheet2, 0, false, null, false);
				}
				if (num > -1)
				{
					command.ReleaseTemporaryRT(num);
				}
			}
			if (releaseTargetAfterUse > -1)
			{
				command.ReleaseTemporaryRT(releaseTargetAfterUse);
			}
			command.EndSample("FinalPass");
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000E85C File Offset: 0x0000CA5C
		private int RenderEffect<T>(PostProcessRenderContext context, bool useTempTarget = false) where T : PostProcessEffectSettings
		{
			PostProcessBundle bundle = this.GetBundle<T>();
			if (!bundle.settings.IsEnabledAndSupported(context))
			{
				return -1;
			}
			if (this.m_IsRenderingInSceneView && !bundle.attribute.allowInSceneView)
			{
				return -1;
			}
			if (!useTempTarget)
			{
				bundle.renderer.RenderOrLog(context);
				return -1;
			}
			RenderTargetIdentifier destination = context.destination;
			int num = this.m_TargetPool.Get();
			context.GetScreenSpaceTemporaryRT(context.command, num, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, 0, 0, false);
			context.destination = num;
			bundle.renderer.RenderOrLog(context);
			context.source = num;
			context.destination = destination;
			return num;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000E904 File Offset: 0x0000CB04
		private bool ShouldGenerateLogHistogram(PostProcessRenderContext context)
		{
			bool flag = this.GetBundle<AutoExposure>().settings.IsEnabledAndSupported(context);
			bool flag2 = this.debugLayer.lightMeter.IsRequestedAndSupported(context);
			return flag || flag2;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000E936 File Offset: 0x0000CB36
		public PostProcessLayer()
		{
		}

		// Token: 0x0400019E RID: 414
		public Transform volumeTrigger;

		// Token: 0x0400019F RID: 415
		public LayerMask volumeLayer;

		// Token: 0x040001A0 RID: 416
		public bool stopNaNPropagation = true;

		// Token: 0x040001A1 RID: 417
		public bool finalBlitToCameraTarget;

		// Token: 0x040001A2 RID: 418
		public PostProcessLayer.Antialiasing antialiasingMode;

		// Token: 0x040001A3 RID: 419
		public TemporalAntialiasing temporalAntialiasing;

		// Token: 0x040001A4 RID: 420
		public SGSR sgsr;

		// Token: 0x040001A5 RID: 421
		public FSR1 fsr1;

		// Token: 0x040001A6 RID: 422
		public FSR3 fsr3;

		// Token: 0x040001A7 RID: 423
		public FSR3 fsr3Stereo;

		// Token: 0x040001A8 RID: 424
		public DLSS dlss;

		// Token: 0x040001A9 RID: 425
		public DLSS dlssStereo;

		// Token: 0x040001AA RID: 426
		public XeSS xess;

		// Token: 0x040001AB RID: 427
		public SubpixelMorphologicalAntialiasing subpixelMorphologicalAntialiasing;

		// Token: 0x040001AC RID: 428
		public FastApproximateAntialiasing fastApproximateAntialiasing;

		// Token: 0x040001AD RID: 429
		public Fog fog;

		// Token: 0x040001AE RID: 430
		private Dithering dithering;

		// Token: 0x040001AF RID: 431
		public PostProcessDebugLayer debugLayer;

		// Token: 0x040001B0 RID: 432
		[SerializeField]
		private PostProcessResources m_Resources;

		// Token: 0x040001B1 RID: 433
		[NonSerialized]
		private PostProcessResources m_OldResources;

		// Token: 0x040001B2 RID: 434
		[Preserve]
		[SerializeField]
		private bool m_ShowToolkit;

		// Token: 0x040001B3 RID: 435
		[Preserve]
		[SerializeField]
		private bool m_ShowCustomSorter;

		// Token: 0x040001B4 RID: 436
		public bool breakBeforeColorGrading;

		// Token: 0x040001B5 RID: 437
		[SerializeField]
		private List<PostProcessLayer.SerializedBundleRef> m_BeforeTransparentBundles;

		// Token: 0x040001B6 RID: 438
		[SerializeField]
		private List<PostProcessLayer.SerializedBundleRef> m_BeforeUpscalingBundles;

		// Token: 0x040001B7 RID: 439
		[SerializeField]
		private List<PostProcessLayer.SerializedBundleRef> m_BeforeStackBundles;

		// Token: 0x040001B8 RID: 440
		[SerializeField]
		private List<PostProcessLayer.SerializedBundleRef> m_AfterStackBundles;

		// Token: 0x040001B9 RID: 441
		[CompilerGenerated]
		private Dictionary<PostProcessEvent, List<PostProcessLayer.SerializedBundleRef>> <sortedBundles>k__BackingField;

		// Token: 0x040001BA RID: 442
		[CompilerGenerated]
		private DepthTextureMode <cameraDepthFlags>k__BackingField;

		// Token: 0x040001BB RID: 443
		[CompilerGenerated]
		private bool <haveBundlesBeenInited>k__BackingField;

		// Token: 0x040001BC RID: 444
		private Dictionary<Type, PostProcessBundle> m_Bundles;

		// Token: 0x040001BD RID: 445
		private PropertySheetFactory m_PropertySheetFactory;

		// Token: 0x040001BE RID: 446
		private CommandBuffer m_LegacyCmdBufferBeforeReflections;

		// Token: 0x040001BF RID: 447
		private CommandBuffer m_LegacyCmdBufferBeforeLighting;

		// Token: 0x040001C0 RID: 448
		private CommandBuffer m_LegacyCmdBufferOpaque;

		// Token: 0x040001C1 RID: 449
		private CommandBuffer m_LegacyCmdBufferTransparent;

		// Token: 0x040001C2 RID: 450
		private CommandBuffer m_LegacyCmdBuffer;

		// Token: 0x040001C3 RID: 451
		private Camera m_Camera;

		// Token: 0x040001C4 RID: 452
		private PostProcessRenderContext m_CurrentContext;

		// Token: 0x040001C5 RID: 453
		private LogHistogram m_LogHistogram;

		// Token: 0x040001C6 RID: 454
		private RenderTexture m_opaqueOnly;

		// Token: 0x040001C7 RID: 455
		private RenderTexture m_upscaledOutput;

		// Token: 0x040001C8 RID: 456
		private RenderTexture m_originalTargetTexture;

		// Token: 0x040001C9 RID: 457
		private bool m_SettingsUpdateNeeded = true;

		// Token: 0x040001CA RID: 458
		private bool m_IsRenderingInSceneView;

		// Token: 0x040001CB RID: 459
		private TargetPool m_TargetPool;

		// Token: 0x040001CC RID: 460
		private bool m_NaNKilled;

		// Token: 0x040001CD RID: 461
		private readonly List<PostProcessEffectRenderer> m_ActiveEffects = new List<PostProcessEffectRenderer>();

		// Token: 0x040001CE RID: 462
		private readonly List<RenderTargetIdentifier> m_Targets = new List<RenderTargetIdentifier>();

		// Token: 0x040001CF RID: 463
		private bool _runRightEyeOnceCommandBuffers;

		// Token: 0x040001D0 RID: 464
		private bool _upscalerEnabled;

		// Token: 0x040001D1 RID: 465
		private bool _runRightEyeOnce;

		// Token: 0x02000087 RID: 135
		public enum Antialiasing
		{
			// Token: 0x0400033E RID: 830
			None,
			// Token: 0x0400033F RID: 831
			FastApproximateAntialiasing,
			// Token: 0x04000340 RID: 832
			SubpixelMorphologicalAntialiasing,
			// Token: 0x04000341 RID: 833
			TemporalAntialiasing,
			// Token: 0x04000342 RID: 834
			SGSR,
			// Token: 0x04000343 RID: 835
			FSR1,
			// Token: 0x04000344 RID: 836
			FSR3,
			// Token: 0x04000345 RID: 837
			DLSS,
			// Token: 0x04000346 RID: 838
			XeSS
		}

		// Token: 0x02000088 RID: 136
		[Serializable]
		public sealed class SerializedBundleRef
		{
			// Token: 0x06000276 RID: 630 RVA: 0x000131A1 File Offset: 0x000113A1
			public SerializedBundleRef()
			{
			}

			// Token: 0x04000347 RID: 839
			public string assemblyQualifiedName;

			// Token: 0x04000348 RID: 840
			public PostProcessBundle bundle;
		}

		// Token: 0x02000089 RID: 137
		[CompilerGenerated]
		private sealed class <>c__DisplayClass66_0
		{
			// Token: 0x06000277 RID: 631 RVA: 0x000131A9 File Offset: 0x000113A9
			public <>c__DisplayClass66_0()
			{
			}

			// Token: 0x06000278 RID: 632 RVA: 0x000131B1 File Offset: 0x000113B1
			internal bool <UpdateBundleSortList>b__0(KeyValuePair<Type, PostProcessBundle> kvp)
			{
				return kvp.Value.attribute.eventType == this.evt && !kvp.Value.attribute.builtinEffect;
			}

			// Token: 0x06000279 RID: 633 RVA: 0x000131E4 File Offset: 0x000113E4
			internal bool <UpdateBundleSortList>b__2(PostProcessLayer.SerializedBundleRef x)
			{
				PostProcessLayer.<>c__DisplayClass66_1 CS$<>8__locals1 = new PostProcessLayer.<>c__DisplayClass66_1();
				CS$<>8__locals1.searchStr = x.assemblyQualifiedName;
				return !this.effects.Exists(new Predicate<PostProcessBundle>(CS$<>8__locals1.<UpdateBundleSortList>b__3));
			}

			// Token: 0x04000349 RID: 841
			public PostProcessEvent evt;

			// Token: 0x0400034A RID: 842
			public List<PostProcessBundle> effects;
		}

		// Token: 0x0200008A RID: 138
		[CompilerGenerated]
		private sealed class <>c__DisplayClass66_1
		{
			// Token: 0x0600027A RID: 634 RVA: 0x0001321D File Offset: 0x0001141D
			public <>c__DisplayClass66_1()
			{
			}

			// Token: 0x0600027B RID: 635 RVA: 0x00013225 File Offset: 0x00011425
			internal bool <UpdateBundleSortList>b__3(PostProcessBundle b)
			{
				return b.settings.GetType().AssemblyQualifiedName == this.searchStr;
			}

			// Token: 0x0400034B RID: 843
			public string searchStr;
		}

		// Token: 0x0200008B RID: 139
		[CompilerGenerated]
		private sealed class <>c__DisplayClass66_2
		{
			// Token: 0x0600027C RID: 636 RVA: 0x00013242 File Offset: 0x00011442
			public <>c__DisplayClass66_2()
			{
			}

			// Token: 0x0600027D RID: 637 RVA: 0x0001324A File Offset: 0x0001144A
			internal bool <UpdateBundleSortList>b__4(PostProcessLayer.SerializedBundleRef b)
			{
				return b.assemblyQualifiedName == this.typeName;
			}

			// Token: 0x0400034C RID: 844
			public string typeName;
		}

		// Token: 0x0200008C RID: 140
		[CompilerGenerated]
		private sealed class <>c__DisplayClass66_3
		{
			// Token: 0x0600027E RID: 638 RVA: 0x0001325D File Offset: 0x0001145D
			public <>c__DisplayClass66_3()
			{
			}

			// Token: 0x0600027F RID: 639 RVA: 0x00013265 File Offset: 0x00011465
			internal bool <UpdateBundleSortList>b__5(PostProcessBundle b)
			{
				return b.settings.GetType().AssemblyQualifiedName == this.typeName;
			}

			// Token: 0x0400034D RID: 845
			public string typeName;
		}

		// Token: 0x0200008D RID: 141
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000280 RID: 640 RVA: 0x00013282 File Offset: 0x00011482
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000281 RID: 641 RVA: 0x0001328E File Offset: 0x0001148E
			public <>c()
			{
			}

			// Token: 0x06000282 RID: 642 RVA: 0x00013296 File Offset: 0x00011496
			internal PostProcessBundle <UpdateBundleSortList>b__66_1(KeyValuePair<Type, PostProcessBundle> kvp)
			{
				return kvp.Value;
			}

			// Token: 0x0400034E RID: 846
			public static readonly PostProcessLayer.<>c <>9 = new PostProcessLayer.<>c();

			// Token: 0x0400034F RID: 847
			public static Func<KeyValuePair<Type, PostProcessBundle>, PostProcessBundle> <>9__66_1;
		}
	}
}
