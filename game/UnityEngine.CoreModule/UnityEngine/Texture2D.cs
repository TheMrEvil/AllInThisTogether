using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A3 RID: 419
	[UsedByNativeCode]
	[HelpURL("Textures")]
	[NativeHeader("Runtime/Graphics/GeneratedTextures.h")]
	[NativeHeader("Runtime/Graphics/Texture2D.h")]
	public sealed class Texture2D : Texture
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001102 RID: 4354
		public extern TextureFormat format { [NativeName("GetTextureFormat")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001103 RID: 4355
		// (set) Token: 0x06001104 RID: 4356
		public extern bool ignoreMipmapLimit { [NativeName("IgnoreMasterTextureLimit")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SetIgnoreMasterTextureLimitAndReload")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001105 RID: 4357
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D whiteTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001106 RID: 4358
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D blackTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001107 RID: 4359
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D redTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001108 RID: 4360
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D grayTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001109 RID: 4361
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D linearGrayTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600110A RID: 4362
		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D normalTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600110B RID: 4363
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Compress(bool highQuality);

		// Token: 0x0600110C RID: 4364
		[FreeFunction("Texture2DScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex);

		// Token: 0x0600110D RID: 4365 RVA: 0x00016400 File Offset: 0x00014600
		private static void Internal_Create([Writable] Texture2D mono, int w, int h, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Texture2D.Internal_CreateImpl(mono, w, h, mipCount, format, colorSpace, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600110E RID: 4366
		public override extern bool isReadable { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600110F RID: 4367
		[NativeName("VTOnly")]
		[NativeConditional("ENABLE_VIRTUALTEXTURING && UNITY_EDITOR")]
		public extern bool vtOnly { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001110 RID: 4368
		[NativeName("Apply")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06001111 RID: 4369
		[NativeName("Reinitialize")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool ReinitializeImpl(int width, int height);

		// Token: 0x06001112 RID: 4370 RVA: 0x00016431 File Offset: 0x00014631
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int mip, int x, int y, Color color)
		{
			this.SetPixelImpl_Injected(image, mip, x, y, ref color);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00016440 File Offset: 0x00014640
		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int mip, int x, int y)
		{
			Color result;
			this.GetPixelImpl_Injected(image, mip, x, y, out result);
			return result;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0001645C File Offset: 0x0001465C
		[NativeName("GetPixelBilinear")]
		private Color GetPixelBilinearImpl(int image, int mip, float u, float v)
		{
			Color result;
			this.GetPixelBilinearImpl_Injected(image, mip, u, v, out result);
			return result;
		}

		// Token: 0x06001115 RID: 4373
		[FreeFunction(Name = "Texture2DScripting::ReinitializeWithFormat", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool ReinitializeWithFormatImpl(int width, int height, GraphicsFormat format, bool hasMipMap);

		// Token: 0x06001116 RID: 4374 RVA: 0x00016477 File Offset: 0x00014677
		[FreeFunction(Name = "Texture2DScripting::ReadPixels", HasExplicitThis = true)]
		private void ReadPixelsImpl(Rect source, int destX, int destY, bool recalculateMipMaps)
		{
			this.ReadPixelsImpl_Injected(ref source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x06001117 RID: 4375
		[FreeFunction(Name = "Texture2DScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPixelsImpl(int x, int y, int w, int h, Color[] pixel, int miplevel, int frame);

		// Token: 0x06001118 RID: 4376
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool LoadRawTextureDataImpl(IntPtr data, ulong size);

		// Token: 0x06001119 RID: 4377
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool LoadRawTextureDataImplArray(byte[] data);

		// Token: 0x0600111A RID: 4378
		[FreeFunction(Name = "Texture2DScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x0600111B RID: 4379
		[FreeFunction(Name = "Texture2DScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x0600111C RID: 4380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetWritableImageData(int frame);

		// Token: 0x0600111D RID: 4381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ulong GetRawImageDataSize();

		// Token: 0x0600111E RID: 4382
		[FreeFunction("Texture2DScripting::GenerateAtlas")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateAtlasImpl(Vector2[] sizes, int padding, int atlasSize, [Out] Rect[] rect);

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600111F RID: 4383
		internal extern bool isPreProcessed { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001120 RID: 4384
		public extern bool streamingMipmaps { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001121 RID: 4385
		public extern int streamingMipmapsPriority { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001122 RID: 4386
		// (set) Token: 0x06001123 RID: 4387
		public extern int requestedMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001124 RID: 4388
		// (set) Token: 0x06001125 RID: 4389
		public extern int minimumMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetMinimumMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(Name = "GetTextureStreamingManager().SetMinimumMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001126 RID: 4390
		// (set) Token: 0x06001127 RID: 4391
		internal extern bool loadAllMips { [FreeFunction(Name = "GetTextureStreamingManager().GetLoadAllMips", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(Name = "GetTextureStreamingManager().SetLoadAllMips", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001128 RID: 4392
		public extern int calculatedMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetCalculatedMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001129 RID: 4393
		public extern int desiredMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600112A RID: 4394
		public extern int loadingMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600112B RID: 4395
		public extern int loadedMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600112C RID: 4396
		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearRequestedMipmapLevel();

		// Token: 0x0600112D RID: 4397
		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsRequestedMipmapLevelLoaded();

		// Token: 0x0600112E RID: 4398
		[FreeFunction(Name = "GetTextureStreamingManager().ClearMinimumMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearMinimumMipmapLevel();

		// Token: 0x0600112F RID: 4399
		[FreeFunction("Texture2DScripting::UpdateExternalTexture", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateExternalTexture(IntPtr nativeTex);

		// Token: 0x06001130 RID: 4400
		[FreeFunction("Texture2DScripting::SetAllPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetAllPixels32([Unmarshalled] Color32[] colors, int miplevel);

		// Token: 0x06001131 RID: 4401
		[FreeFunction("Texture2DScripting::SetBlockOfPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBlockOfPixels32(int x, int y, int blockWidth, int blockHeight, [Unmarshalled] Color32[] colors, int miplevel);

		// Token: 0x06001132 RID: 4402
		[FreeFunction("Texture2DScripting::GetRawTextureData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern byte[] GetRawTextureData();

		// Token: 0x06001133 RID: 4403
		[FreeFunction("Texture2DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, [DefaultValue("0")] int miplevel);

		// Token: 0x06001134 RID: 4404 RVA: 0x00016488 File Offset: 0x00014688
		[ExcludeFromDocs]
		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			return this.GetPixels(x, y, blockWidth, blockHeight, 0);
		}

		// Token: 0x06001135 RID: 4405
		[FreeFunction("Texture2DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32([DefaultValue("0")] int miplevel);

		// Token: 0x06001136 RID: 4406 RVA: 0x000164A8 File Offset: 0x000146A8
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			return this.GetPixels32(0);
		}

		// Token: 0x06001137 RID: 4407
		[FreeFunction("Texture2DScripting::PackTextures", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable);

		// Token: 0x06001138 RID: 4408 RVA: 0x000164C4 File Offset: 0x000146C4
		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			return this.PackTextures(textures, padding, maximumAtlasSize, false);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000164E0 File Offset: 0x000146E0
		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			return this.PackTextures(textures, padding, 2048);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00016500 File Offset: 0x00014700
		internal bool ValidateFormat(TextureFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = TextureFormat.PVRTC_RGB2 <= format && format <= TextureFormat.PVRTC_RGBA4;
				bool flag4 = flag3 && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0001656C File Offset: 0x0001476C
		internal bool ValidateFormat(GraphicsFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = GraphicsFormatUtility.IsPVRTCFormat(format);
				bool flag4 = flag3 && (width != height || !Mathf.IsPowerOfTwo(width));
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to be square and have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000165D0 File Offset: 0x000147D0
		internal Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags, int mipCount, IntPtr nativeTex)
		{
			bool flag = this.ValidateFormat(format, width, height);
			if (flag)
			{
				Texture2D.Internal_Create(this, width, height, mipCount, format, base.GetTextureColorSpace(format), flags, nativeTex);
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00016609 File Offset: 0x00014809
		[ExcludeFromDocs]
		public Texture2D(int width, int height, DefaultFormat format, TextureCreationFlags flags) : this(width, height, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0001661D File Offset: 0x0001481D
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags) : this(width, height, format, flags, Texture.GenerateAllMips, IntPtr.Zero)
		{
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00016636 File Offset: 0x00014836
		[ExcludeFromDocs]
		public Texture2D(int width, int height, GraphicsFormat format, int mipCount, TextureCreationFlags flags) : this(width, height, format, flags, mipCount, IntPtr.Zero)
		{
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0001664C File Offset: 0x0001484C
		internal Texture2D(int width, int height, TextureFormat textureFormat, int mipCount, bool linear, IntPtr nativeTex)
		{
			bool flag = !this.ValidateFormat(textureFormat, width, height);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				TextureCreationFlags textureCreationFlags = (mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None;
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				Texture2D.Internal_Create(this, width, height, mipCount, graphicsFormat, base.GetTextureColorSpace(linear), textureCreationFlags, nativeTex);
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000166AF File Offset: 0x000148AF
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("-1")] int mipCount, [DefaultValue("false")] bool linear) : this(width, height, textureFormat, mipCount, linear, IntPtr.Zero)
		{
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000166C5 File Offset: 0x000148C5
		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("true")] bool mipChain, [DefaultValue("false")] bool linear) : this(width, height, textureFormat, mipChain ? -1 : 1, linear, IntPtr.Zero)
		{
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x000166E1 File Offset: 0x000148E1
		[ExcludeFromDocs]
		public Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain) : this(width, height, textureFormat, mipChain ? -1 : 1, false, IntPtr.Zero)
		{
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000166FC File Offset: 0x000148FC
		[ExcludeFromDocs]
		public Texture2D(int width, int height) : this(width, height, TextureFormat.RGBA32, Texture.GenerateAllMips, false, IntPtr.Zero)
		{
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00016714 File Offset: 0x00014914
		public static Texture2D CreateExternalTexture(int width, int height, TextureFormat format, bool mipChain, bool linear, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Texture2D(width, height, format, mipChain ? -1 : 1, linear, nativeTex);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00016754 File Offset: 0x00014954
		[ExcludeFromDocs]
		public void SetPixel(int x, int y, Color color)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, 0, x, y, color);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00016784 File Offset: 0x00014984
		public void SetPixel(int x, int y, Color color, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl(0, mipLevel, x, y, color);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x000167B4 File Offset: 0x000149B4
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelsImpl(x, y, blockWidth, blockHeight, colors, miplevel, 0);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000167E8 File Offset: 0x000149E8
		[ExcludeFromDocs]
		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			this.SetPixels(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000167FC File Offset: 0x000149FC
		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			this.SetPixels(0, 0, num, num2, colors, miplevel);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00016843 File Offset: 0x00014A43
		[ExcludeFromDocs]
		public void SetPixels(Color[] colors)
		{
			this.SetPixels(0, 0, this.width, this.height, colors, 0);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00016860 File Offset: 0x00014A60
		[ExcludeFromDocs]
		public Color GetPixel(int x, int y)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, 0, x, y);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00016894 File Offset: 0x00014A94
		public Color GetPixel(int x, int y, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl(0, mipLevel, x, y);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000168C8 File Offset: 0x00014AC8
		[ExcludeFromDocs]
		public Color GetPixelBilinear(float u, float v)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, 0, u, v);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000168FC File Offset: 0x00014AFC
		public Color GetPixelBilinear(float u, float v, [DefaultValue("0")] int mipLevel)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelBilinearImpl(0, mipLevel, u, v);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00016930 File Offset: 0x00014B30
		public void LoadRawTextureData(IntPtr data, int size)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == IntPtr.Zero || size == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImpl(data, (ulong)((long)size));
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00016998 File Offset: 0x00014B98
		public void LoadRawTextureData(byte[] data)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = data == null || data.Length == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else
			{
				bool flag3 = !this.LoadRawTextureDataImplArray(data);
				if (flag3)
				{
					throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
				}
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000169F4 File Offset: 0x00014BF4
		public void LoadRawTextureData<T>(NativeArray<T> data) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = !data.IsCreated || data.Length == 0;
			if (flag2)
			{
				throw new UnityException("No texture data provided to LoadRawTextureData");
			}
			bool flag3 = !this.LoadRawTextureDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), (ulong)((long)data.Length * (long)UnsafeUtility.SizeOf<T>()));
			if (flag3)
			{
				throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00016A70 File Offset: 0x00014C70
		public void SetPixelData<T>(T[] data, int mipLevel, [DefaultValue("0")] int sourceDataStartIndex = 0)
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = data == null || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImplArray(data, mipLevel, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00016AE0 File Offset: 0x00014CE0
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, [DefaultValue("0")] int sourceDataStartIndex = 0) where T : struct
		{
			bool flag = sourceDataStartIndex < 0;
			if (flag)
			{
				throw new UnityException("SetPixelData: sourceDataStartIndex cannot be less than 0.");
			}
			bool flag2 = !this.isReadable;
			if (flag2)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag3 = !data.IsCreated || data.Length == 0;
			if (flag3)
			{
				throw new UnityException("No texture data provided to SetPixelData.");
			}
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00016B5C File Offset: 0x00014D5C
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = mipLevel < 0 || mipLevel >= base.mipmapCount;
			if (flag2)
			{
				throw new ArgumentException("The passed in miplevel " + mipLevel.ToString() + " is invalid. It needs to be in the range 0 and " + (base.mipmapCount - 1).ToString());
			}
			bool flag3 = this.GetWritableImageData(0).ToInt64() == 0L;
			if (flag3)
			{
				throw new UnityException("Texture '" + base.name + "' has no data.");
			}
			ulong pixelDataOffset = base.GetPixelDataOffset(mipLevel, 0);
			ulong pixelDataSize = base.GetPixelDataSize(mipLevel, 0);
			int num = UnsafeUtility.SizeOf<T>();
			ulong num2 = pixelDataSize / (ulong)((long)num);
			bool flag4 = num2 > 2147483647UL;
			if (flag4)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			IntPtr value = new IntPtr((long)this.GetWritableImageData(0) + (long)pixelDataOffset);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)value, (int)num2, Allocator.None);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00016C60 File Offset: 0x00014E60
		public unsafe NativeArray<T> GetRawTextureData<T>() where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			int num = UnsafeUtility.SizeOf<T>();
			ulong num2 = this.GetRawImageDataSize() / (ulong)((long)num);
			bool flag2 = num2 > 2147483647UL;
			if (flag2)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetWritableImageData(0), (int)num2, Allocator.None);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00016CC8 File Offset: 0x00014EC8
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00016CF4 File Offset: 0x00014EF4
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00016D00 File Offset: 0x00014F00
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00016D0C File Offset: 0x00014F0C
		public bool Reinitialize(int width, int height)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ReinitializeImpl(width, height);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00016D3C File Offset: 0x00014F3C
		public bool Reinitialize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.ReinitializeWithFormatImpl(width, height, GraphicsFormatUtility.GetGraphicsFormat(format, base.activeTextureColorSpace == ColorSpace.Gamma), hasMipMap);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00016D68 File Offset: 0x00014F68
		public bool Reinitialize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.ReinitializeWithFormatImpl(width, height, format, hasMipMap);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00016D9C File Offset: 0x00014F9C
		[Obsolete("Texture2D.Resize(int, int) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32)", false)]
		public bool Resize(int width, int height)
		{
			return this.Reinitialize(width, height);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00016DB8 File Offset: 0x00014FB8
		[Obsolete("Texture2D.Resize(int, int, TextureFormat, bool) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int, TextureFormat, bool) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32, UnityEngine.TextureFormat, [*] System.Boolean)", false)]
		public bool Resize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			return this.Reinitialize(width, height, format, hasMipMap);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00016DD8 File Offset: 0x00014FD8
		[Obsolete("Texture2D.Resize(int, int, GraphicsFormat, bool) has been deprecated because it actually reinitializes the texture. Use Texture2D.Reinitialize(int, int, GraphicsFormat, bool) instead (UnityUpgradable) -> Reinitialize([*] System.Int32, [*] System.Int32, UnityEngine.Experimental.Rendering.GraphicsFormat, [*] System.Boolean)", false)]
		public bool Resize(int width, int height, GraphicsFormat format, bool hasMipMap)
		{
			return this.Reinitialize(width, height, format, hasMipMap);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00016DF8 File Offset: 0x00014FF8
		public void ReadPixels(Rect source, int destX, int destY, [DefaultValue("true")] bool recalculateMipMaps)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ReadPixelsImpl(source, destX, destY, recalculateMipMaps);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00016E27 File Offset: 0x00015027
		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			this.ReadPixels(source, destX, destY, true);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00016E38 File Offset: 0x00015038
		public static bool GenerateAtlas(Vector2[] sizes, int padding, int atlasSize, List<Rect> results)
		{
			bool flag = sizes == null;
			if (flag)
			{
				throw new ArgumentException("sizes array can not be null");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentException("results list cannot be null");
			}
			bool flag3 = padding < 0;
			if (flag3)
			{
				throw new ArgumentException("padding can not be negative");
			}
			bool flag4 = atlasSize <= 0;
			if (flag4)
			{
				throw new ArgumentException("atlas size must be positive");
			}
			results.Clear();
			bool flag5 = sizes.Length == 0;
			bool result;
			if (flag5)
			{
				result = true;
			}
			else
			{
				NoAllocHelpers.EnsureListElemCount<Rect>(results, sizes.Length);
				Texture2D.GenerateAtlasImpl(sizes, padding, atlasSize, NoAllocHelpers.ExtractArrayFromListT<Rect>(results));
				result = (results.Count != 0);
			}
			return result;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00016ED4 File Offset: 0x000150D4
		public void SetPixels32(Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetAllPixels32(colors, miplevel);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00016EE0 File Offset: 0x000150E0
		[ExcludeFromDocs]
		public void SetPixels32(Color32[] colors)
		{
			this.SetPixels32(colors, 0);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00016EEC File Offset: 0x000150EC
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, [DefaultValue("0")] int miplevel)
		{
			this.SetBlockOfPixels32(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00016EFF File Offset: 0x000150FF
		[ExcludeFromDocs]
		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors)
		{
			this.SetPixels32(x, y, blockWidth, blockHeight, colors, 0);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00016F14 File Offset: 0x00015114
		public Color[] GetPixels([DefaultValue("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			bool flag = num < 1;
			if (flag)
			{
				num = 1;
			}
			int num2 = this.height >> miplevel;
			bool flag2 = num2 < 1;
			if (flag2)
			{
				num2 = 1;
			}
			return this.GetPixels(0, 0, num, num2, miplevel);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00016F60 File Offset: 0x00015160
		[ExcludeFromDocs]
		public Color[] GetPixels()
		{
			return this.GetPixels(0);
		}

		// Token: 0x06001169 RID: 4457
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPixelImpl_Injected(int image, int mip, int x, int y, ref Color color);

		// Token: 0x0600116A RID: 4458
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelImpl_Injected(int image, int mip, int x, int y, out Color ret);

		// Token: 0x0600116B RID: 4459
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelBilinearImpl_Injected(int image, int mip, float u, float v, out Color ret);

		// Token: 0x0600116C RID: 4460
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReadPixelsImpl_Injected(ref Rect source, int destX, int destY, bool recalculateMipMaps);

		// Token: 0x040005BE RID: 1470
		internal const int streamingMipmapsPriorityMin = -128;

		// Token: 0x040005BF RID: 1471
		internal const int streamingMipmapsPriorityMax = 127;

		// Token: 0x020001A4 RID: 420
		[Flags]
		public enum EXRFlags
		{
			// Token: 0x040005C1 RID: 1473
			None = 0,
			// Token: 0x040005C2 RID: 1474
			OutputAsFloat = 1,
			// Token: 0x040005C3 RID: 1475
			CompressZIP = 2,
			// Token: 0x040005C4 RID: 1476
			CompressRLE = 4,
			// Token: 0x040005C5 RID: 1477
			CompressPIZ = 8
		}
	}
}
