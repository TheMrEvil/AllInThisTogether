using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020001AD RID: 429
	public struct RenderTextureDescriptor
	{
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00019288 File Offset: 0x00017488
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x00019290 File Offset: 0x00017490
		public int width
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<width>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<width>k__BackingField = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00019299 File Offset: 0x00017499
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x000192A1 File Offset: 0x000174A1
		public int height
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<height>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<height>k__BackingField = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x000192AA File Offset: 0x000174AA
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x000192B2 File Offset: 0x000174B2
		public int msaaSamples
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<msaaSamples>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<msaaSamples>k__BackingField = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000192BB File Offset: 0x000174BB
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x000192C3 File Offset: 0x000174C3
		public int volumeDepth
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<volumeDepth>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<volumeDepth>k__BackingField = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x000192CC File Offset: 0x000174CC
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x000192D4 File Offset: 0x000174D4
		public int mipCount
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<mipCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<mipCount>k__BackingField = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x000192E0 File Offset: 0x000174E0
		// (set) Token: 0x060012CC RID: 4812 RVA: 0x000192F8 File Offset: 0x000174F8
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this._graphicsFormat;
			}
			set
			{
				this._graphicsFormat = value;
				this.SetOrClearRenderTextureCreationFlag(GraphicsFormatUtility.IsSRGBFormat(value), RenderTextureCreationFlags.SRGB);
				this.depthBufferBits = this.depthBufferBits;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0001931D File Offset: 0x0001751D
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x00019325 File Offset: 0x00017525
		public GraphicsFormat stencilFormat
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<stencilFormat>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<stencilFormat>k__BackingField = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0001932E File Offset: 0x0001752E
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x00019336 File Offset: 0x00017536
		public GraphicsFormat depthStencilFormat
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<depthStencilFormat>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<depthStencilFormat>k__BackingField = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00019340 File Offset: 0x00017540
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x0001937C File Offset: 0x0001757C
		public RenderTextureFormat colorFormat
		{
			get
			{
				bool flag = this.graphicsFormat > GraphicsFormat.None;
				RenderTextureFormat result;
				if (flag)
				{
					result = GraphicsFormatUtility.GetRenderTextureFormat(this.graphicsFormat);
				}
				else
				{
					result = ((this.shadowSamplingMode != ShadowSamplingMode.None) ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				}
				return result;
			}
			set
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
				this.graphicsFormat = SystemInfo.GetCompatibleFormat(graphicsFormat, FormatUsage.Render);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x000193A8 File Offset: 0x000175A8
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x000193C8 File Offset: 0x000175C8
		public bool sRGB
		{
			get
			{
				return GraphicsFormatUtility.IsSRGBFormat(this.graphicsFormat);
			}
			set
			{
				this.graphicsFormat = ((value && QualitySettings.activeColorSpace == ColorSpace.Linear && this.colorFormat != RenderTextureFormat.R8 && this.colorFormat != RenderTextureFormat.RG16) ? GraphicsFormatUtility.GetSRGBFormat(this.graphicsFormat) : GraphicsFormatUtility.GetLinearFormat(this.graphicsFormat));
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00019414 File Offset: 0x00017614
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x00019431 File Offset: 0x00017631
		public int depthBufferBits
		{
			get
			{
				return GraphicsFormatUtility.GetDepthBits(this.depthStencilFormat);
			}
			set
			{
				this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(value, this.graphicsFormat);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00019447 File Offset: 0x00017647
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0001944F File Offset: 0x0001764F
		public TextureDimension dimension
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<dimension>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<dimension>k__BackingField = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00019458 File Offset: 0x00017658
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x00019460 File Offset: 0x00017660
		public ShadowSamplingMode shadowSamplingMode
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<shadowSamplingMode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<shadowSamplingMode>k__BackingField = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00019469 File Offset: 0x00017669
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x00019471 File Offset: 0x00017671
		public VRTextureUsage vrUsage
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<vrUsage>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<vrUsage>k__BackingField = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0001947C File Offset: 0x0001767C
		public RenderTextureCreationFlags flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00019494 File Offset: 0x00017694
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x0001949C File Offset: 0x0001769C
		public RenderTextureMemoryless memoryless
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<memoryless>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<memoryless>k__BackingField = value;
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x000194A5 File Offset: 0x000176A5
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height)
		{
			this = new RenderTextureDescriptor(width, height, RenderTextureFormat.Default);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000194B2 File Offset: 0x000176B2
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, 0);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000194C0 File Offset: 0x000176C0
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000194D4 File Offset: 0x000176D4
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, Texture.GenerateAllMips);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000194E8 File Offset: 0x000176E8
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, RenderTextureFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthBufferBits, mipCount, RenderTextureReadWrite.Linear);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000194FC File Offset: 0x000176FC
		public RenderTextureDescriptor(int width, int height, [DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat colorFormat, [DefaultValue("0")] int depthBufferBits, [DefaultValue("Texture.GenerateAllMips")] int mipCount, [DefaultValue("RenderTextureReadWrite.Linear")] RenderTextureReadWrite readWrite)
		{
			GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(colorFormat, readWrite);
			GraphicsFormat compatibleFormat = SystemInfo.GetCompatibleFormat(graphicsFormat, FormatUsage.Render);
			this = new RenderTextureDescriptor(width, height, compatibleFormat, RenderTexture.GetDepthStencilFormatLegacy(depthBufferBits, colorFormat), mipCount);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00019534 File Offset: 0x00017734
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, int depthBufferBits, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = (RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip);
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depthBufferBits, colorFormat);
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000195B4 File Offset: 0x000177B4
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat)
		{
			this = new RenderTextureDescriptor(width, height, colorFormat, depthStencilFormat, Texture.GenerateAllMips);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000195C8 File Offset: 0x000177C8
		[ExcludeFromDocs]
		public RenderTextureDescriptor(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int mipCount)
		{
			this = default(RenderTextureDescriptor);
			this._flags = (RenderTextureCreationFlags.AutoGenerateMips | RenderTextureCreationFlags.AllowVerticalFlip);
			this.width = width;
			this.height = height;
			this.volumeDepth = 1;
			this.msaaSamples = 1;
			this.graphicsFormat = colorFormat;
			this.depthStencilFormat = depthStencilFormat;
			this.mipCount = mipCount;
			this.dimension = TextureDimension.Tex2D;
			this.shadowSamplingMode = ShadowSamplingMode.None;
			this.vrUsage = VRTextureUsage.None;
			this.memoryless = RenderTextureMemoryless.None;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00019644 File Offset: 0x00017844
		private void SetOrClearRenderTextureCreationFlag(bool value, RenderTextureCreationFlags flag)
		{
			if (value)
			{
				this._flags |= flag;
			}
			else
			{
				this._flags &= ~flag;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0001967C File Offset: 0x0001787C
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x00019699 File Offset: 0x00017899
		public bool useMipMap
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.MipMap) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.MipMap);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x000196A8 File Offset: 0x000178A8
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x000196C5 File Offset: 0x000178C5
		public bool autoGenerateMips
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.AutoGenerateMips) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.AutoGenerateMips);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x000196D4 File Offset: 0x000178D4
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x000196F2 File Offset: 0x000178F2
		public bool enableRandomWrite
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.EnableRandomWrite) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.EnableRandomWrite);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00019700 File Offset: 0x00017900
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x00019721 File Offset: 0x00017921
		public bool bindMS
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.BindMS) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.BindMS);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00019734 File Offset: 0x00017934
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x00019752 File Offset: 0x00017952
		internal bool createdFromScript
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.CreatedFromScript) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.CreatedFromScript);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00019760 File Offset: 0x00017960
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00019781 File Offset: 0x00017981
		public bool useDynamicScale
		{
			get
			{
				return (this._flags & RenderTextureCreationFlags.DynamicallyScalable) > (RenderTextureCreationFlags)0;
			}
			set
			{
				this.SetOrClearRenderTextureCreationFlag(value, RenderTextureCreationFlags.DynamicallyScalable);
			}
		}

		// Token: 0x040005CB RID: 1483
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <width>k__BackingField;

		// Token: 0x040005CC RID: 1484
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <height>k__BackingField;

		// Token: 0x040005CD RID: 1485
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <msaaSamples>k__BackingField;

		// Token: 0x040005CE RID: 1486
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <volumeDepth>k__BackingField;

		// Token: 0x040005CF RID: 1487
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <mipCount>k__BackingField;

		// Token: 0x040005D0 RID: 1488
		private GraphicsFormat _graphicsFormat;

		// Token: 0x040005D1 RID: 1489
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private GraphicsFormat <stencilFormat>k__BackingField;

		// Token: 0x040005D2 RID: 1490
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private GraphicsFormat <depthStencilFormat>k__BackingField;

		// Token: 0x040005D3 RID: 1491
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private TextureDimension <dimension>k__BackingField;

		// Token: 0x040005D4 RID: 1492
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ShadowSamplingMode <shadowSamplingMode>k__BackingField;

		// Token: 0x040005D5 RID: 1493
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VRTextureUsage <vrUsage>k__BackingField;

		// Token: 0x040005D6 RID: 1494
		private RenderTextureCreationFlags _flags;

		// Token: 0x040005D7 RID: 1495
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private RenderTextureMemoryless <memoryless>k__BackingField;
	}
}
