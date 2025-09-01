using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AA RID: 426
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/RenderBufferManager.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	public class RenderTexture : Texture
	{
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600121C RID: 4636
		// (set) Token: 0x0600121D RID: 4637
		public override extern int width { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600121E RID: 4638
		// (set) Token: 0x0600121F RID: 4639
		public override extern int height { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001220 RID: 4640
		// (set) Token: 0x06001221 RID: 4641
		public override extern TextureDimension dimension { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001222 RID: 4642
		// (set) Token: 0x06001223 RID: 4643
		[NativeProperty("ColorFormat")]
		public new extern GraphicsFormat graphicsFormat { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001224 RID: 4644
		// (set) Token: 0x06001225 RID: 4645
		[NativeProperty("MipMap")]
		public extern bool useMipMap { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001226 RID: 4646
		[NativeProperty("SRGBReadWrite")]
		public extern bool sRGB { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001227 RID: 4647
		// (set) Token: 0x06001228 RID: 4648
		[NativeProperty("VRUsage")]
		public extern VRTextureUsage vrUsage { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001229 RID: 4649
		// (set) Token: 0x0600122A RID: 4650
		[NativeProperty("Memoryless")]
		public extern RenderTextureMemoryless memorylessMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00018724 File Offset: 0x00016924
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x00018768 File Offset: 0x00016968
		public RenderTextureFormat format
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
					result = ((this.GetDescriptor().shadowSamplingMode != ShadowSamplingMode.None) ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth);
				}
				return result;
			}
			set
			{
				this.graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(value, this.sRGB);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600122D RID: 4653
		// (set) Token: 0x0600122E RID: 4654
		public extern GraphicsFormat stencilFormat { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600122F RID: 4655
		// (set) Token: 0x06001230 RID: 4656
		public extern GraphicsFormat depthStencilFormat { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001231 RID: 4657
		// (set) Token: 0x06001232 RID: 4658
		public extern bool autoGenerateMips { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001233 RID: 4659
		// (set) Token: 0x06001234 RID: 4660
		public extern int volumeDepth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001235 RID: 4661
		// (set) Token: 0x06001236 RID: 4662
		public extern int antiAliasing { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001237 RID: 4663
		// (set) Token: 0x06001238 RID: 4664
		public extern bool bindTextureMS { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001239 RID: 4665
		// (set) Token: 0x0600123A RID: 4666
		public extern bool enableRandomWrite { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600123B RID: 4667
		// (set) Token: 0x0600123C RID: 4668
		public extern bool useDynamicScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600123D RID: 4669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetIsPowerOfTwo();

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00018780 File Offset: 0x00016980
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x00004563 File Offset: 0x00002763
		public bool isPowerOfTwo
		{
			get
			{
				return this.GetIsPowerOfTwo();
			}
			set
			{
			}
		}

		// Token: 0x06001240 RID: 4672
		[FreeFunction("RenderTexture::GetActive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderTexture GetActive();

		// Token: 0x06001241 RID: 4673
		[FreeFunction("RenderTextureScripting::SetActive")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActive(RenderTexture rt);

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00018798 File Offset: 0x00016998
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x000187AF File Offset: 0x000169AF
		public static RenderTexture active
		{
			get
			{
				return RenderTexture.GetActive();
			}
			set
			{
				RenderTexture.SetActive(value);
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000187BC File Offset: 0x000169BC
		[FreeFunction(Name = "RenderTextureScripting::GetColorBuffer", HasExplicitThis = true)]
		private RenderBuffer GetColorBuffer()
		{
			RenderBuffer result;
			this.GetColorBuffer_Injected(out result);
			return result;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000187D4 File Offset: 0x000169D4
		[FreeFunction(Name = "RenderTextureScripting::GetDepthBuffer", HasExplicitThis = true)]
		private RenderBuffer GetDepthBuffer()
		{
			RenderBuffer result;
			this.GetDepthBuffer_Injected(out result);
			return result;
		}

		// Token: 0x06001246 RID: 4678
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMipMapCount(int count);

		// Token: 0x06001247 RID: 4679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetShadowSamplingMode(ShadowSamplingMode samplingMode);

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x000187EC File Offset: 0x000169EC
		public RenderBuffer colorBuffer
		{
			get
			{
				return this.GetColorBuffer();
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00018804 File Offset: 0x00016A04
		public RenderBuffer depthBuffer
		{
			get
			{
				return this.GetDepthBuffer();
			}
		}

		// Token: 0x0600124A RID: 4682
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeDepthBufferPtr();

		// Token: 0x0600124B RID: 4683
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DiscardContents(bool discardColor, bool discardDepth);

		// Token: 0x0600124C RID: 4684
		[Obsolete("This function has no effect.", false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void MarkRestoreExpected();

		// Token: 0x0600124D RID: 4685 RVA: 0x0001881C File Offset: 0x00016A1C
		public void DiscardContents()
		{
			this.DiscardContents(true, true);
		}

		// Token: 0x0600124E RID: 4686
		[NativeName("ResolveAntiAliasedSurface")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResolveAA();

		// Token: 0x0600124F RID: 4687
		[NativeName("ResolveAntiAliasedSurface")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResolveAATo(RenderTexture rt);

		// Token: 0x06001250 RID: 4688 RVA: 0x00018828 File Offset: 0x00016A28
		public void ResolveAntiAliasedSurface()
		{
			this.ResolveAA();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00018832 File Offset: 0x00016A32
		public void ResolveAntiAliasedSurface(RenderTexture target)
		{
			this.ResolveAATo(target);
		}

		// Token: 0x06001252 RID: 4690
		[FreeFunction(Name = "RenderTextureScripting::SetGlobalShaderProperty", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetGlobalShaderProperty(string propertyName);

		// Token: 0x06001253 RID: 4691
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Create();

		// Token: 0x06001254 RID: 4692
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Release();

		// Token: 0x06001255 RID: 4693
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsCreated();

		// Token: 0x06001256 RID: 4694
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GenerateMips();

		// Token: 0x06001257 RID: 4695
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ConvertToEquirect(RenderTexture equirect, Camera.MonoOrStereoscopicEye eye = Camera.MonoOrStereoscopicEye.Mono);

		// Token: 0x06001258 RID: 4696
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetSRGBReadWrite(bool srgb);

		// Token: 0x06001259 RID: 4697
		[FreeFunction("RenderTextureScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] RenderTexture rt);

		// Token: 0x0600125A RID: 4698
		[FreeFunction("RenderTextureSupportsStencil")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SupportsStencil(RenderTexture rt);

		// Token: 0x0600125B RID: 4699 RVA: 0x0001883D File Offset: 0x00016A3D
		[NativeName("SetRenderTextureDescFromScript")]
		private void SetRenderTextureDescriptor(RenderTextureDescriptor desc)
		{
			this.SetRenderTextureDescriptor_Injected(ref desc);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00018848 File Offset: 0x00016A48
		[NativeName("GetRenderTextureDesc")]
		private RenderTextureDescriptor GetDescriptor()
		{
			RenderTextureDescriptor result;
			this.GetDescriptor_Injected(out result);
			return result;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0001885E File Offset: 0x00016A5E
		[FreeFunction("GetRenderBufferManager().GetTextures().GetTempBuffer")]
		private static RenderTexture GetTemporary_Internal(RenderTextureDescriptor desc)
		{
			return RenderTexture.GetTemporary_Internal_Injected(ref desc);
		}

		// Token: 0x0600125E RID: 4702
		[FreeFunction("GetRenderBufferManager().GetTextures().ReleaseTempBuffer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ReleaseTemporary(RenderTexture temp);

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600125F RID: 4703
		// (set) Token: 0x06001260 RID: 4704
		public extern int depth { [FreeFunction("RenderTextureScripting::GetDepth", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction("RenderTextureScripting::SetDepth", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001261 RID: 4705 RVA: 0x00018867 File Offset: 0x00016A67
		[RequiredByNativeCode]
		protected internal RenderTexture()
		{
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00018871 File Offset: 0x00016A71
		public RenderTexture(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(desc);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(desc);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00018894 File Offset: 0x00016A94
		public RenderTexture(RenderTexture textureToCopy)
		{
			bool flag = textureToCopy == null;
			if (flag)
			{
				throw new ArgumentNullException("textureToCopy");
			}
			RenderTexture.ValidateRenderTextureDesc(textureToCopy.descriptor);
			RenderTexture.Internal_Create(this);
			this.SetRenderTextureDescriptor(textureToCopy.descriptor);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000188DF File Offset: 0x00016ADF
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, DefaultFormat format) : this(width, height, RenderTexture.GetDefaultColorFormat(format), RenderTexture.GetDefaultDepthStencilFormat(format, depth), Texture.GenerateAllMips)
		{
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000188FF File Offset: 0x00016AFF
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, GraphicsFormat format) : this(width, height, depth, format, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00018914 File Offset: 0x00016B14
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, GraphicsFormat format, int mipCount)
		{
			bool flag = format != GraphicsFormat.None && !base.ValidateFormat(format, FormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.depthStencilFormat = RenderTexture.GetDepthStencilFormatLegacy(depth, format);
				this.width = width;
				this.height = height;
				this.graphicsFormat = format;
				this.SetMipMapCount(mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00018988 File Offset: 0x00016B88
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat, int mipCount)
		{
			bool flag = colorFormat != GraphicsFormat.None && !base.ValidateFormat(colorFormat, FormatUsage.Render);
			if (!flag)
			{
				RenderTexture.Internal_Create(this);
				this.width = width;
				this.height = height;
				this.depthStencilFormat = depthStencilFormat;
				this.graphicsFormat = colorFormat;
				this.SetMipMapCount(mipCount);
				this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(colorFormat));
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x000189F2 File Offset: 0x00016BF2
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, GraphicsFormat colorFormat, GraphicsFormat depthStencilFormat) : this(width, height, colorFormat, depthStencilFormat, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00018A06 File Offset: 0x00016C06
		public RenderTexture(int width, int height, int depth, [UnityEngine.Internal.DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [UnityEngine.Internal.DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite)
		{
			this.Initialize(width, height, depth, format, readWrite, Texture.GenerateAllMips);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00018A23 File Offset: 0x00016C23
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format) : this(width, height, depth, format, Texture.GenerateAllMips)
		{
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00018A37 File Offset: 0x00016C37
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth) : this(width, height, depth, RenderTextureFormat.Default)
		{
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00018A45 File Offset: 0x00016C45
		[ExcludeFromDocs]
		public RenderTexture(int width, int height, int depth, RenderTextureFormat format, int mipCount)
		{
			this.Initialize(width, height, depth, format, RenderTextureReadWrite.Default, mipCount);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00018A60 File Offset: 0x00016C60
		private void Initialize(int width, int height, int depth, RenderTextureFormat format, RenderTextureReadWrite readWrite, int mipCount)
		{
			GraphicsFormat compatibleFormat = RenderTexture.GetCompatibleFormat(format, readWrite);
			GraphicsFormat depthStencilFormatLegacy = RenderTexture.GetDepthStencilFormatLegacy(depth, format);
			bool flag = compatibleFormat > GraphicsFormat.None;
			if (flag)
			{
				bool flag2 = !base.ValidateFormat(compatibleFormat, FormatUsage.Render);
				if (flag2)
				{
					return;
				}
			}
			RenderTexture.Internal_Create(this);
			this.width = width;
			this.height = height;
			this.depthStencilFormat = depthStencilFormatLegacy;
			this.graphicsFormat = compatibleFormat;
			this.SetMipMapCount(mipCount);
			this.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(compatibleFormat));
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00018ADC File Offset: 0x00016CDC
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, GraphicsFormat colorFormat)
		{
			return (colorFormat == GraphicsFormat.ShadowAuto) ? GraphicsFormatUtility.GetDepthStencilFormat(depthBits, 0) : GraphicsFormatUtility.GetDepthStencilFormat(depthBits);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00018B08 File Offset: 0x00016D08
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, RenderTextureFormat format)
		{
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, format == RenderTextureFormat.Shadowmap);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00018B24 File Offset: 0x00016D24
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, DefaultFormat format)
		{
			return RenderTexture.GetDepthStencilFormatLegacy(depthBits, format == DefaultFormat.Shadow);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00018B40 File Offset: 0x00016D40
		internal static GraphicsFormat GetDepthStencilFormatLegacy(int depthBits, bool requestedShadowMap)
		{
			return requestedShadowMap ? GraphicsFormatUtility.GetDepthStencilFormat(depthBits, 0) : GraphicsFormatUtility.GetDepthStencilFormat(depthBits);
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00018B64 File Offset: 0x00016D64
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00018B7C File Offset: 0x00016D7C
		public RenderTextureDescriptor descriptor
		{
			get
			{
				return this.GetDescriptor();
			}
			set
			{
				RenderTexture.ValidateRenderTextureDesc(value);
				this.SetRenderTextureDescriptor(value);
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00018B90 File Offset: 0x00016D90
		private static void ValidateRenderTextureDesc(RenderTextureDescriptor desc)
		{
			bool flag = desc.graphicsFormat == GraphicsFormat.None && desc.depthStencilFormat == GraphicsFormat.None;
			if (flag)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat and depthStencilFormat cannot both be None.");
			}
			bool flag2 = desc.graphicsFormat != GraphicsFormat.None && !SystemInfo.IsFormatSupported(desc.graphicsFormat, FormatUsage.Render);
			if (flag2)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must be a supported GraphicsFormat. " + desc.graphicsFormat.ToString() + " is not supported on this platform.", "desc.graphicsFormat");
			}
			bool flag3 = desc.depthStencilFormat != GraphicsFormat.None && !GraphicsFormatUtility.IsDepthFormat(desc.depthStencilFormat) && !GraphicsFormatUtility.IsStencilFormat(desc.depthStencilFormat);
			if (flag3)
			{
				throw new ArgumentException("RenderTextureDesc depthStencilFormat must be a supported depth/stencil GraphicsFormat. " + desc.depthStencilFormat.ToString() + " is not supported on this platform.", "desc.depthStencilFormat");
			}
			bool flag4 = desc.width <= 0;
			if (flag4)
			{
				throw new ArgumentException("RenderTextureDesc width must be greater than zero.", "desc.width");
			}
			bool flag5 = desc.height <= 0;
			if (flag5)
			{
				throw new ArgumentException("RenderTextureDesc height must be greater than zero.", "desc.height");
			}
			bool flag6 = desc.volumeDepth <= 0;
			if (flag6)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be greater than zero.", "desc.volumeDepth");
			}
			bool flag7 = desc.msaaSamples != 1 && desc.msaaSamples != 2 && desc.msaaSamples != 4 && desc.msaaSamples != 8;
			if (flag7)
			{
				throw new ArgumentException("RenderTextureDesc msaaSamples must be 1, 2, 4, or 8.", "desc.msaaSamples");
			}
			bool flag8 = desc.dimension == TextureDimension.CubeArray && desc.volumeDepth % 6 != 0;
			if (flag8)
			{
				throw new ArgumentException("RenderTextureDesc volumeDepth must be a multiple of 6 when dimension is CubeArray", "desc.volumeDepth");
			}
			bool flag9 = desc.graphicsFormat != GraphicsFormat.ShadowAuto && desc.graphicsFormat != GraphicsFormat.DepthAuto && (GraphicsFormatUtility.IsDepthFormat(desc.graphicsFormat) || GraphicsFormatUtility.IsStencilFormat(desc.graphicsFormat));
			if (flag9)
			{
				throw new ArgumentException("RenderTextureDesc graphicsFormat must not be a depth/stencil format. " + desc.graphicsFormat.ToString() + " is not supported.", "desc.graphicsFormat");
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00018DC0 File Offset: 0x00016FC0
		internal static GraphicsFormat GetDefaultColorFormat(DefaultFormat format)
		{
			GraphicsFormat result;
			if (format != DefaultFormat.DepthStencil)
			{
				if (format != DefaultFormat.Shadow)
				{
					result = SystemInfo.GetGraphicsFormat(format);
				}
				else
				{
					result = GraphicsFormat.ShadowAuto;
				}
			}
			else
			{
				result = GraphicsFormat.DepthAuto;
			}
			return result;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00018DF8 File Offset: 0x00016FF8
		internal static GraphicsFormat GetDefaultDepthStencilFormat(DefaultFormat format, int depth)
		{
			GraphicsFormat result;
			if (format - DefaultFormat.DepthStencil > 1)
			{
				result = RenderTexture.GetDepthStencilFormatLegacy(depth, format);
			}
			else
			{
				result = SystemInfo.GetGraphicsFormat(format);
			}
			return result;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00018E28 File Offset: 0x00017028
		internal static GraphicsFormat GetCompatibleFormat(RenderTextureFormat renderTextureFormat, RenderTextureReadWrite readWrite)
		{
			GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(renderTextureFormat, readWrite);
			GraphicsFormat compatibleFormat = SystemInfo.GetCompatibleFormat(graphicsFormat, FormatUsage.Render);
			bool flag = graphicsFormat == compatibleFormat;
			GraphicsFormat result;
			if (flag)
			{
				result = graphicsFormat;
			}
			else
			{
				Debug.LogWarning(string.Format("'{0}' is not supported. RenderTexture::GetTemporary fallbacks to {1} format on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", graphicsFormat.ToString(), compatibleFormat.ToString()));
				result = compatibleFormat;
			}
			return result;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00018E84 File Offset: 0x00017084
		public static RenderTexture GetTemporary(RenderTextureDescriptor desc)
		{
			RenderTexture.ValidateRenderTextureDesc(desc);
			desc.createdFromScript = true;
			return RenderTexture.GetTemporary_Internal(desc);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00018EAC File Offset: 0x000170AC
		private static RenderTexture GetTemporaryImpl(int width, int height, GraphicsFormat depthStencilFormat, GraphicsFormat colorFormat, int antiAliasing = 1, RenderTextureMemoryless memorylessMode = RenderTextureMemoryless.None, VRTextureUsage vrUsage = VRTextureUsage.None, bool useDynamicScale = false)
		{
			return RenderTexture.GetTemporary(new RenderTextureDescriptor(width, height, colorFormat, depthStencilFormat)
			{
				msaaSamples = antiAliasing,
				memoryless = memorylessMode,
				vrUsage = vrUsage,
				useDynamicScale = useDynamicScale
			});
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00018EF8 File Offset: 0x000170F8
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, [UnityEngine.Internal.DefaultValue("1")] int antiAliasing, [UnityEngine.Internal.DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [UnityEngine.Internal.DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [UnityEngine.Internal.DefaultValue("false")] bool useDynamicScale)
		{
			return RenderTexture.GetTemporaryImpl(width, height, RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format), format, antiAliasing, memorylessMode, vrUsage, useDynamicScale);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00018F24 File Offset: 0x00017124
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00018F48 File Offset: 0x00017148
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, antiAliasing, memorylessMode, VRTextureUsage.None);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00018F68 File Offset: 0x00017168
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format, int antiAliasing)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, antiAliasing, RenderTextureMemoryless.None);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00018F88 File Offset: 0x00017188
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, GraphicsFormat format)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, 1);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00018FA4 File Offset: 0x000171A4
		public static RenderTexture GetTemporary(int width, int height, [UnityEngine.Internal.DefaultValue("0")] int depthBuffer, [UnityEngine.Internal.DefaultValue("RenderTextureFormat.Default")] RenderTextureFormat format, [UnityEngine.Internal.DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite, [UnityEngine.Internal.DefaultValue("1")] int antiAliasing, [UnityEngine.Internal.DefaultValue("RenderTextureMemoryless.None")] RenderTextureMemoryless memorylessMode, [UnityEngine.Internal.DefaultValue("VRTextureUsage.None")] VRTextureUsage vrUsage, [UnityEngine.Internal.DefaultValue("false")] bool useDynamicScale)
		{
			GraphicsFormat compatibleFormat = RenderTexture.GetCompatibleFormat(format, readWrite);
			GraphicsFormat depthStencilFormatLegacy = RenderTexture.GetDepthStencilFormatLegacy(depthBuffer, format);
			return RenderTexture.GetTemporaryImpl(width, height, depthStencilFormatLegacy, compatibleFormat, antiAliasing, memorylessMode, vrUsage, useDynamicScale);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00018FD8 File Offset: 0x000171D8
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode, VRTextureUsage vrUsage)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing, memorylessMode, vrUsage, false);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00018FFC File Offset: 0x000171FC
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing, RenderTextureMemoryless memorylessMode)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing, memorylessMode, VRTextureUsage.None);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00019020 File Offset: 0x00017220
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite, int antiAliasing)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing, RenderTextureMemoryless.None);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00019040 File Offset: 0x00017240
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format, RenderTextureReadWrite readWrite)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, 1);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00019060 File Offset: 0x00017260
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer, RenderTextureFormat format)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, format, RenderTextureReadWrite.Default);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0001907C File Offset: 0x0001727C
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height, int depthBuffer)
		{
			return RenderTexture.GetTemporary(width, height, depthBuffer, RenderTextureFormat.Default);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00019098 File Offset: 0x00017298
		[ExcludeFromDocs]
		public static RenderTexture GetTemporary(int width, int height)
		{
			return RenderTexture.GetTemporary(width, height, 0);
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x000190B4 File Offset: 0x000172B4
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x000190CF File Offset: 0x000172CF
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isCubemap
		{
			get
			{
				return this.dimension == TextureDimension.Cube;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Cube : TextureDimension.Tex2D);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x000190E0 File Offset: 0x000172E0
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x000190FB File Offset: 0x000172FB
		[Obsolete("Use RenderTexture.dimension instead.", false)]
		public bool isVolume
		{
			get
			{
				return this.dimension == TextureDimension.Tex3D;
			}
			set
			{
				this.dimension = (value ? TextureDimension.Tex3D : TextureDimension.Tex2D);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0001910C File Offset: 0x0001730C
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x00004563 File Offset: 0x00002763
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("RenderTexture.enabled is always now, no need to use it.", false)]
		public static bool enabled
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00019120 File Offset: 0x00017320
		[Obsolete("GetTexelOffset always returns zero now, no point in using it.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Vector2 GetTexelOffset()
		{
			return Vector2.zero;
		}

		// Token: 0x0600128E RID: 4750
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetColorBuffer_Injected(out RenderBuffer ret);

		// Token: 0x0600128F RID: 4751
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetDepthBuffer_Injected(out RenderBuffer ret);

		// Token: 0x06001290 RID: 4752
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRenderTextureDescriptor_Injected(ref RenderTextureDescriptor desc);

		// Token: 0x06001291 RID: 4753
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetDescriptor_Injected(out RenderTextureDescriptor ret);

		// Token: 0x06001292 RID: 4754
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderTexture GetTemporary_Internal_Injected(ref RenderTextureDescriptor desc);
	}
}
