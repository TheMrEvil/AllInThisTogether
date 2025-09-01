using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000060 RID: 96
	public sealed class PostProcessRenderContext
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000F5FA File Offset: 0x0000D7FA
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000F604 File Offset: 0x0000D804
		public Camera camera
		{
			get
			{
				return this.m_Camera;
			}
			set
			{
				this.m_Camera = value;
				this.width = this.m_Camera.pixelWidth;
				this.height = this.m_Camera.pixelHeight;
				this.m_sourceDescriptor.width = this.width;
				this.m_sourceDescriptor.height = this.height;
				this.screenWidth = this.width;
				this.screenHeight = this.height;
				this.stereoActive = false;
				this.numberOfEyes = 1;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000F682 File Offset: 0x0000D882
		public void SetRenderSize(Vector2Int renderSize)
		{
			this.width = renderSize.x;
			this.height = renderSize.y;
			this.m_sourceDescriptor.width = this.width;
			this.m_sourceDescriptor.height = this.height;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		public CommandBuffer command
		{
			[CompilerGenerated]
			get
			{
				return this.<command>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<command>k__BackingField = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000F6D1 File Offset: 0x0000D8D1
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000F6D9 File Offset: 0x0000D8D9
		public RenderTargetIdentifier source
		{
			[CompilerGenerated]
			get
			{
				return this.<source>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<source>k__BackingField = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000F6E2 File Offset: 0x0000D8E2
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000F6EA File Offset: 0x0000D8EA
		public RenderTargetIdentifier destination
		{
			[CompilerGenerated]
			get
			{
				return this.<destination>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<destination>k__BackingField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000F6F3 File Offset: 0x0000D8F3
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000F6FB File Offset: 0x0000D8FB
		public RenderTextureFormat sourceFormat
		{
			[CompilerGenerated]
			get
			{
				return this.<sourceFormat>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<sourceFormat>k__BackingField = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000F704 File Offset: 0x0000D904
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000F70C File Offset: 0x0000D90C
		public bool flip
		{
			[CompilerGenerated]
			get
			{
				return this.<flip>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<flip>k__BackingField = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000F715 File Offset: 0x0000D915
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000F71D File Offset: 0x0000D91D
		public PostProcessResources resources
		{
			[CompilerGenerated]
			get
			{
				return this.<resources>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<resources>k__BackingField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000F726 File Offset: 0x0000D926
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000F72E File Offset: 0x0000D92E
		public PropertySheetFactory propertySheets
		{
			[CompilerGenerated]
			get
			{
				return this.<propertySheets>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<propertySheets>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000F737 File Offset: 0x0000D937
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000F73F File Offset: 0x0000D93F
		public Dictionary<string, object> userData
		{
			[CompilerGenerated]
			get
			{
				return this.<userData>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<userData>k__BackingField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000F748 File Offset: 0x0000D948
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000F750 File Offset: 0x0000D950
		public PostProcessDebugLayer debugLayer
		{
			[CompilerGenerated]
			get
			{
				return this.<debugLayer>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<debugLayer>k__BackingField = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000F759 File Offset: 0x0000D959
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000F761 File Offset: 0x0000D961
		public int width
		{
			[CompilerGenerated]
			get
			{
				return this.<width>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<width>k__BackingField = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000F76A File Offset: 0x0000D96A
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000F772 File Offset: 0x0000D972
		public int height
		{
			[CompilerGenerated]
			get
			{
				return this.<height>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<height>k__BackingField = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000F77B File Offset: 0x0000D97B
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000F783 File Offset: 0x0000D983
		public bool stereoActive
		{
			[CompilerGenerated]
			get
			{
				return this.<stereoActive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<stereoActive>k__BackingField = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000F78C File Offset: 0x0000D98C
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000F794 File Offset: 0x0000D994
		public int xrActiveEye
		{
			[CompilerGenerated]
			get
			{
				return this.<xrActiveEye>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<xrActiveEye>k__BackingField = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000F79D File Offset: 0x0000D99D
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000F7A5 File Offset: 0x0000D9A5
		public int numberOfEyes
		{
			[CompilerGenerated]
			get
			{
				return this.<numberOfEyes>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<numberOfEyes>k__BackingField = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000F7AE File Offset: 0x0000D9AE
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000F7B6 File Offset: 0x0000D9B6
		public PostProcessRenderContext.StereoRenderingMode stereoRenderingMode
		{
			[CompilerGenerated]
			get
			{
				return this.<stereoRenderingMode>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<stereoRenderingMode>k__BackingField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000F7BF File Offset: 0x0000D9BF
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000F7C7 File Offset: 0x0000D9C7
		public int screenWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<screenWidth>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<screenWidth>k__BackingField = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000F7D0 File Offset: 0x0000D9D0
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		public int screenHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<screenHeight>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<screenHeight>k__BackingField = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000F7E1 File Offset: 0x0000D9E1
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000F7E9 File Offset: 0x0000D9E9
		public bool isSceneView
		{
			[CompilerGenerated]
			get
			{
				return this.<isSceneView>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<isSceneView>k__BackingField = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000F7F2 File Offset: 0x0000D9F2
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000F7FA File Offset: 0x0000D9FA
		public PostProcessLayer.Antialiasing antialiasing
		{
			[CompilerGenerated]
			get
			{
				return this.<antialiasing>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<antialiasing>k__BackingField = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000F803 File Offset: 0x0000DA03
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000F80B File Offset: 0x0000DA0B
		public TemporalAntialiasing temporalAntialiasing
		{
			[CompilerGenerated]
			get
			{
				return this.<temporalAntialiasing>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<temporalAntialiasing>k__BackingField = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000F814 File Offset: 0x0000DA14
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000F81C File Offset: 0x0000DA1C
		public SGSR sgsr
		{
			[CompilerGenerated]
			get
			{
				return this.<sgsr>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<sgsr>k__BackingField = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000F825 File Offset: 0x0000DA25
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000F82D File Offset: 0x0000DA2D
		public FSR1 superResolution1
		{
			[CompilerGenerated]
			get
			{
				return this.<superResolution1>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<superResolution1>k__BackingField = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000F836 File Offset: 0x0000DA36
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000F83E File Offset: 0x0000DA3E
		public FSR3 superResolution3
		{
			[CompilerGenerated]
			get
			{
				return this.<superResolution3>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<superResolution3>k__BackingField = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000F847 File Offset: 0x0000DA47
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000F84F File Offset: 0x0000DA4F
		public DLSS deepLearningSuperSampling
		{
			[CompilerGenerated]
			get
			{
				return this.<deepLearningSuperSampling>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<deepLearningSuperSampling>k__BackingField = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000F858 File Offset: 0x0000DA58
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000F860 File Offset: 0x0000DA60
		public XeSS xeSuperSampling
		{
			[CompilerGenerated]
			get
			{
				return this.<xeSuperSampling>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<xeSuperSampling>k__BackingField = value;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000F86C File Offset: 0x0000DA6C
		public void Reset()
		{
			this.m_Camera = null;
			this.width = 0;
			this.height = 0;
			this.m_sourceDescriptor = new RenderTextureDescriptor(0, 0);
			this.physicalCamera = false;
			this.stereoActive = false;
			this.xrActiveEye = 0;
			this.screenWidth = 0;
			this.screenHeight = 0;
			this.command = null;
			this.source = 0;
			this.destination = 0;
			this.sourceFormat = RenderTextureFormat.ARGB32;
			this.flip = false;
			this.resources = null;
			this.propertySheets = null;
			this.debugLayer = null;
			this.isSceneView = false;
			this.antialiasing = PostProcessLayer.Antialiasing.None;
			this.temporalAntialiasing = null;
			this.uberSheet = null;
			this.autoExposureTexture = null;
			this.logLut = null;
			this.autoExposure = null;
			this.bloomBufferNameID = -1;
			if (this.userData == null)
			{
				this.userData = new Dictionary<string, object>();
			}
			this.userData.Clear();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000F956 File Offset: 0x0000DB56
		public bool IsTemporalAntialiasingActive()
		{
			return this.antialiasing == PostProcessLayer.Antialiasing.TemporalAntialiasing || this.antialiasing == PostProcessLayer.Antialiasing.SGSR || (this.antialiasing == PostProcessLayer.Antialiasing.FSR1 && !this.isSceneView);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000F980 File Offset: 0x0000DB80
		public bool IsSGSRActive()
		{
			return this.antialiasing == PostProcessLayer.Antialiasing.SGSR && Application.isPlaying && !this.isSceneView;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000F99D File Offset: 0x0000DB9D
		public bool IsFSR1Active()
		{
			return this.antialiasing == PostProcessLayer.Antialiasing.FSR1 && Application.isPlaying && !this.isSceneView;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000F9BA File Offset: 0x0000DBBA
		public bool IsFSR3Active()
		{
			return this.antialiasing == PostProcessLayer.Antialiasing.FSR3 && Application.isPlaying && !this.isSceneView;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000F9D7 File Offset: 0x0000DBD7
		public bool IsDLSSActive()
		{
			return this.antialiasing == PostProcessLayer.Antialiasing.DLSS && Application.isPlaying && !this.isSceneView;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		public bool IsXeSSActive()
		{
			return this.antialiasing == PostProcessLayer.Antialiasing.XeSS && Application.isPlaying && !this.isSceneView;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public bool IsDebugOverlayEnabled(DebugOverlay overlay)
		{
			return this.debugLayer.debugOverlay == overlay;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000FA21 File Offset: 0x0000DC21
		public void PushDebugOverlay(CommandBuffer cmd, RenderTargetIdentifier source, PropertySheet sheet, int pass)
		{
			this.debugLayer.PushDebugOverlay(cmd, source, sheet, pass);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000FA34 File Offset: 0x0000DC34
		internal RenderTextureDescriptor GetDescriptor(int depthBufferBits = 0, RenderTextureFormat colorFormat = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default)
		{
			RenderTextureDescriptor result = new RenderTextureDescriptor(this.m_sourceDescriptor.width, this.m_sourceDescriptor.height, this.m_sourceDescriptor.colorFormat, depthBufferBits);
			result.dimension = this.m_sourceDescriptor.dimension;
			result.volumeDepth = this.m_sourceDescriptor.volumeDepth;
			result.vrUsage = this.m_sourceDescriptor.vrUsage;
			result.msaaSamples = this.m_sourceDescriptor.msaaSamples;
			result.memoryless = this.m_sourceDescriptor.memoryless;
			result.useMipMap = this.m_sourceDescriptor.useMipMap;
			result.autoGenerateMips = this.m_sourceDescriptor.autoGenerateMips;
			result.enableRandomWrite = this.m_sourceDescriptor.enableRandomWrite;
			result.shadowSamplingMode = this.m_sourceDescriptor.shadowSamplingMode;
			if (RuntimeUtilities.IsDynamicResolutionEnabled(this.m_Camera))
			{
				result.useDynamicScale = true;
			}
			if (colorFormat != RenderTextureFormat.Default)
			{
				result.colorFormat = colorFormat;
			}
			if (readWrite == RenderTextureReadWrite.sRGB)
			{
				result.sRGB = true;
			}
			else if (readWrite == RenderTextureReadWrite.Linear)
			{
				result.sRGB = false;
			}
			else if (readWrite == RenderTextureReadWrite.Default)
			{
				result.sRGB = (QualitySettings.activeColorSpace > ColorSpace.Gamma);
			}
			return result;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public void GetScreenSpaceTemporaryRT(CommandBuffer cmd, int nameID, int depthBufferBits = 0, RenderTextureFormat colorFormat = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, FilterMode filter = FilterMode.Bilinear, int widthOverride = 0, int heightOverride = 0, bool isUpscaleOutput = false)
		{
			RenderTextureDescriptor descriptor = this.GetDescriptor(depthBufferBits, colorFormat, readWrite);
			if (widthOverride > 0)
			{
				descriptor.width = widthOverride;
			}
			if (heightOverride > 0)
			{
				descriptor.height = heightOverride;
			}
			if (this.stereoActive && descriptor.dimension == TextureDimension.Tex2DArray)
			{
				descriptor.dimension = TextureDimension.Tex2D;
			}
			if (isUpscaleOutput)
			{
				descriptor.enableRandomWrite = true;
				descriptor.useDynamicScale = false;
			}
			cmd.GetTemporaryRT(nameID, descriptor, filter);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		public RenderTexture GetScreenSpaceTemporaryRT(int depthBufferBits = 0, RenderTextureFormat colorFormat = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, int widthOverride = 0, int heightOverride = 0)
		{
			RenderTextureDescriptor descriptor = this.GetDescriptor(depthBufferBits, colorFormat, readWrite);
			if (widthOverride > 0)
			{
				descriptor.width = widthOverride;
			}
			if (heightOverride > 0)
			{
				descriptor.height = heightOverride;
			}
			return RenderTexture.GetTemporary(descriptor);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000FC05 File Offset: 0x0000DE05
		public void UpdateSinglePassStereoState(bool isTAAEnabled, bool isAOEnabled, bool isSSREnabled)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000FC07 File Offset: 0x0000DE07
		public PostProcessRenderContext()
		{
		}

		// Token: 0x040001DC RID: 476
		private Camera m_Camera;

		// Token: 0x040001DD RID: 477
		[CompilerGenerated]
		private CommandBuffer <command>k__BackingField;

		// Token: 0x040001DE RID: 478
		[CompilerGenerated]
		private RenderTargetIdentifier <source>k__BackingField;

		// Token: 0x040001DF RID: 479
		[CompilerGenerated]
		private RenderTargetIdentifier <destination>k__BackingField;

		// Token: 0x040001E0 RID: 480
		[CompilerGenerated]
		private RenderTextureFormat <sourceFormat>k__BackingField;

		// Token: 0x040001E1 RID: 481
		[CompilerGenerated]
		private bool <flip>k__BackingField;

		// Token: 0x040001E2 RID: 482
		[CompilerGenerated]
		private PostProcessResources <resources>k__BackingField;

		// Token: 0x040001E3 RID: 483
		[CompilerGenerated]
		private PropertySheetFactory <propertySheets>k__BackingField;

		// Token: 0x040001E4 RID: 484
		[CompilerGenerated]
		private Dictionary<string, object> <userData>k__BackingField;

		// Token: 0x040001E5 RID: 485
		[CompilerGenerated]
		private PostProcessDebugLayer <debugLayer>k__BackingField;

		// Token: 0x040001E6 RID: 486
		[CompilerGenerated]
		private int <width>k__BackingField;

		// Token: 0x040001E7 RID: 487
		[CompilerGenerated]
		private int <height>k__BackingField;

		// Token: 0x040001E8 RID: 488
		[CompilerGenerated]
		private bool <stereoActive>k__BackingField;

		// Token: 0x040001E9 RID: 489
		[CompilerGenerated]
		private int <xrActiveEye>k__BackingField;

		// Token: 0x040001EA RID: 490
		[CompilerGenerated]
		private int <numberOfEyes>k__BackingField;

		// Token: 0x040001EB RID: 491
		[CompilerGenerated]
		private PostProcessRenderContext.StereoRenderingMode <stereoRenderingMode>k__BackingField;

		// Token: 0x040001EC RID: 492
		[CompilerGenerated]
		private int <screenWidth>k__BackingField;

		// Token: 0x040001ED RID: 493
		[CompilerGenerated]
		private int <screenHeight>k__BackingField;

		// Token: 0x040001EE RID: 494
		[CompilerGenerated]
		private bool <isSceneView>k__BackingField;

		// Token: 0x040001EF RID: 495
		[CompilerGenerated]
		private PostProcessLayer.Antialiasing <antialiasing>k__BackingField;

		// Token: 0x040001F0 RID: 496
		[CompilerGenerated]
		private TemporalAntialiasing <temporalAntialiasing>k__BackingField;

		// Token: 0x040001F1 RID: 497
		[CompilerGenerated]
		private SGSR <sgsr>k__BackingField;

		// Token: 0x040001F2 RID: 498
		[CompilerGenerated]
		private FSR1 <superResolution1>k__BackingField;

		// Token: 0x040001F3 RID: 499
		[CompilerGenerated]
		private FSR3 <superResolution3>k__BackingField;

		// Token: 0x040001F4 RID: 500
		[CompilerGenerated]
		private DLSS <deepLearningSuperSampling>k__BackingField;

		// Token: 0x040001F5 RID: 501
		[CompilerGenerated]
		private XeSS <xeSuperSampling>k__BackingField;

		// Token: 0x040001F6 RID: 502
		internal PropertySheet uberSheet;

		// Token: 0x040001F7 RID: 503
		internal Texture autoExposureTexture;

		// Token: 0x040001F8 RID: 504
		internal LogHistogram logHistogram;

		// Token: 0x040001F9 RID: 505
		internal Texture logLut;

		// Token: 0x040001FA RID: 506
		internal AutoExposure autoExposure;

		// Token: 0x040001FB RID: 507
		internal int bloomBufferNameID;

		// Token: 0x040001FC RID: 508
		internal bool physicalCamera;

		// Token: 0x040001FD RID: 509
		private RenderTextureDescriptor m_sourceDescriptor;

		// Token: 0x02000090 RID: 144
		public enum StereoRenderingMode
		{
			// Token: 0x04000355 RID: 853
			MultiPass,
			// Token: 0x04000356 RID: 854
			SinglePass,
			// Token: 0x04000357 RID: 855
			SinglePassInstanced,
			// Token: 0x04000358 RID: 856
			SinglePassMultiview
		}
	}
}
