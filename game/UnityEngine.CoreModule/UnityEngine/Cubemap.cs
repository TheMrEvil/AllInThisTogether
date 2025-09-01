using System;
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
	// Token: 0x020001A5 RID: 421
	[ExcludeFromPreset]
	[NativeHeader("Runtime/Graphics/CubemapTexture.h")]
	public sealed class Cubemap : Texture
	{
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600116D RID: 4461
		public extern TextureFormat format { [NativeName("GetTextureFormat")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600116E RID: 4462
		[FreeFunction("CubemapScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex);

		// Token: 0x0600116F RID: 4463 RVA: 0x00016F7C File Offset: 0x0001517C
		private static void Internal_Create([Writable] Cubemap mono, int ext, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags, IntPtr nativeTex)
		{
			bool flag = !Cubemap.Internal_CreateImpl(mono, ext, mipCount, format, colorSpace, flags, nativeTex);
			if (flag)
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		// Token: 0x06001170 RID: 4464
		[FreeFunction(Name = "CubemapScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x06001171 RID: 4465
		[FreeFunction("CubemapScripting::UpdateExternalTexture", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateExternalTexture(IntPtr nativeTexture);

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001172 RID: 4466
		public override extern bool isReadable { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001173 RID: 4467 RVA: 0x00016FAB File Offset: 0x000151AB
		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int mip, int x, int y, Color color)
		{
			this.SetPixelImpl_Injected(image, mip, x, y, ref color);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00016FBC File Offset: 0x000151BC
		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int mip, int x, int y)
		{
			Color result;
			this.GetPixelImpl_Injected(image, mip, x, y, out result);
			return result;
		}

		// Token: 0x06001175 RID: 4469
		[NativeName("FixupEdges")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SmoothEdges([DefaultValue("1")] int smoothRegionWidthInPixels);

		// Token: 0x06001176 RID: 4470 RVA: 0x00016FD7 File Offset: 0x000151D7
		public void SmoothEdges()
		{
			this.SmoothEdges(1);
		}

		// Token: 0x06001177 RID: 4471
		[FreeFunction(Name = "CubemapScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(CubemapFace face, int miplevel);

		// Token: 0x06001178 RID: 4472 RVA: 0x00016FE4 File Offset: 0x000151E4
		public Color[] GetPixels(CubemapFace face)
		{
			return this.GetPixels(face, 0);
		}

		// Token: 0x06001179 RID: 4473
		[FreeFunction(Name = "CubemapScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels([Unmarshalled] Color[] colors, CubemapFace face, int miplevel);

		// Token: 0x0600117A RID: 4474
		[FreeFunction(Name = "CubemapScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x0600117B RID: 4475
		[FreeFunction(Name = "CubemapScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int face, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x0600117C RID: 4476 RVA: 0x00016FFE File Offset: 0x000151FE
		public void SetPixels(Color[] colors, CubemapFace face)
		{
			this.SetPixels(colors, face, 0);
		}

		// Token: 0x0600117D RID: 4477
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetWritableImageData(int frame);

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600117E RID: 4478
		internal extern bool isPreProcessed { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600117F RID: 4479
		public extern bool streamingMipmaps { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001180 RID: 4480
		public extern int streamingMipmapsPriority { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001181 RID: 4481
		// (set) Token: 0x06001182 RID: 4482
		public extern int requestedMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001183 RID: 4483
		// (set) Token: 0x06001184 RID: 4484
		internal extern bool loadAllMips { [FreeFunction(Name = "GetTextureStreamingManager().GetLoadAllMips", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [FreeFunction(Name = "GetTextureStreamingManager().SetLoadAllMips", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001185 RID: 4485
		public extern int desiredMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001186 RID: 4486
		public extern int loadingMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001187 RID: 4487
		public extern int loadedMipmapLevel { [FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001188 RID: 4488
		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearRequestedMipmapLevel();

		// Token: 0x06001189 RID: 4489
		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsRequestedMipmapLevelLoaded();

		// Token: 0x0600118A RID: 4490 RVA: 0x0001700C File Offset: 0x0001520C
		internal bool ValidateFormat(TextureFormat format, int width)
		{
			bool flag = base.ValidateFormat(format);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = TextureFormat.PVRTC_RGB2 <= format && format <= TextureFormat.PVRTC_RGBA4;
				bool flag4 = flag3 && !Mathf.IsPowerOfTwo(width);
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00017074 File Offset: 0x00015274
		internal bool ValidateFormat(GraphicsFormat format, int width)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sample);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = GraphicsFormatUtility.IsPVRTCFormat(format);
				bool flag4 = flag3 && !Mathf.IsPowerOfTwo(width);
				if (flag4)
				{
					throw new UnityException(string.Format("'{0}' demands texture to have power-of-two dimensions", format.ToString()));
				}
			}
			return flag;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000170D0 File Offset: 0x000152D0
		[ExcludeFromDocs]
		public Cubemap(int width, DefaultFormat format, TextureCreationFlags flags) : this(width, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000170E4 File Offset: 0x000152E4
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags)
		{
			bool flag = this.ValidateFormat(format, width);
			if (flag)
			{
				Cubemap.Internal_Create(this, width, Texture.GenerateAllMips, format, base.GetTextureColorSpace(format), flags, IntPtr.Zero);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00017120 File Offset: 0x00015320
		public Cubemap(int width, TextureFormat format, int mipCount) : this(width, format, mipCount, IntPtr.Zero)
		{
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00017134 File Offset: 0x00015334
		[ExcludeFromDocs]
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags, int mipCount)
		{
			bool flag = !this.ValidateFormat(format, width);
			if (!flag)
			{
				Cubemap.ValidateIsNotCrunched(flags);
				Cubemap.Internal_Create(this, width, mipCount, format, base.GetTextureColorSpace(format), flags, IntPtr.Zero);
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0001717C File Offset: 0x0001537C
		internal Cubemap(int width, TextureFormat textureFormat, int mipCount, IntPtr nativeTex)
		{
			bool flag = !this.ValidateFormat(textureFormat, width);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, false);
				TextureCreationFlags textureCreationFlags = (mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None;
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				Cubemap.ValidateIsNotCrunched(textureCreationFlags);
				Cubemap.Internal_Create(this, width, mipCount, graphicsFormat, base.GetTextureColorSpace(true), textureCreationFlags, nativeTex);
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000171DD File Offset: 0x000153DD
		internal Cubemap(int width, TextureFormat textureFormat, bool mipChain, IntPtr nativeTex) : this(width, textureFormat, mipChain ? -1 : 1, nativeTex)
		{
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x000171F2 File Offset: 0x000153F2
		public Cubemap(int width, TextureFormat textureFormat, bool mipChain) : this(width, textureFormat, mipChain ? -1 : 1, IntPtr.Zero)
		{
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0001720C File Offset: 0x0001540C
		public static Cubemap CreateExternalTexture(int width, TextureFormat format, bool mipmap, IntPtr nativeTex)
		{
			bool flag = nativeTex == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Cubemap(width, format, mipmap, nativeTex);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00017244 File Offset: 0x00015444
		public void SetPixelData<T>(T[] data, int mipLevel, CubemapFace face, [DefaultValue("0")] int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, (int)face, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000172B8 File Offset: 0x000154B8
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, CubemapFace face, [DefaultValue("0")] int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, (int)face, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00017338 File Offset: 0x00015538
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, CubemapFace face) where T : struct
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			bool flag2 = mipLevel < 0 || mipLevel >= base.mipmapCount;
			if (flag2)
			{
				throw new ArgumentException("The passed in miplevel " + mipLevel.ToString() + " is invalid. The valid range is 0 through " + (base.mipmapCount - 1).ToString());
			}
			bool flag3 = face < CubemapFace.PositiveX || face >= (CubemapFace)6;
			if (flag3)
			{
				throw new ArgumentException("The passed in face " + face.ToString() + " is invalid. The valid range is 0 through 5.");
			}
			bool flag4 = this.GetWritableImageData(0).ToInt64() == 0L;
			if (flag4)
			{
				throw new UnityException("Texture '" + base.name + "' has no data.");
			}
			ulong pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, (int)face);
			ulong pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, (int)face);
			ulong pixelDataSize = base.GetPixelDataSize(mipLevel, (int)face);
			int num = UnsafeUtility.SizeOf<T>();
			ulong num2 = pixelDataSize / (ulong)((long)num);
			bool flag5 = num2 > 2147483647UL;
			if (flag5)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			IntPtr value = new IntPtr((long)this.GetWritableImageData(0) + (long)(pixelDataOffset * (ulong)((long)face) + pixelDataOffset2));
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)value, (int)num2, Allocator.None);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00017485 File Offset: 0x00015685
		[ExcludeFromDocs]
		public void SetPixel(CubemapFace face, int x, int y, Color color)
		{
			this.SetPixel(face, x, y, color, 0);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00017498 File Offset: 0x00015698
		public void SetPixel(CubemapFace face, int x, int y, Color color, [DefaultValue("0")] int mip)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.SetPixelImpl((int)face, mip, x, y, color);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000174CC File Offset: 0x000156CC
		[ExcludeFromDocs]
		public Color GetPixel(CubemapFace face, int x, int y)
		{
			return this.GetPixel(face, x, y, 0);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000174E8 File Offset: 0x000156E8
		public Color GetPixel(CubemapFace face, int x, int y, [DefaultValue("0")] int mip)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			return this.GetPixelImpl((int)face, mip, x, y);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0001751C File Offset: 0x0001571C
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00017548 File Offset: 0x00015748
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00017554 File Offset: 0x00015754
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00017560 File Offset: 0x00015760
		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched Cubemap is not supported for textures created from script.");
			}
		}

		// Token: 0x0600119F RID: 4511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPixelImpl_Injected(int image, int mip, int x, int y, ref Color color);

		// Token: 0x060011A0 RID: 4512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelImpl_Injected(int image, int mip, int x, int y, out Color ret);
	}
}
