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
	// Token: 0x020001A8 RID: 424
	[ExcludeFromPreset]
	[NativeHeader("Runtime/Graphics/CubemapArrayTexture.h")]
	public sealed class CubemapArray : Texture
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060011EF RID: 4591
		public extern int cubemapCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060011F0 RID: 4592
		public extern TextureFormat format { [NativeName("GetTextureFormat")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060011F1 RID: 4593
		public override extern bool isReadable { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060011F2 RID: 4594
		[FreeFunction("CubemapArrayScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_CreateImpl([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags);

		// Token: 0x060011F3 RID: 4595 RVA: 0x0001808C File Offset: 0x0001628C
		private static void Internal_Create([Writable] CubemapArray mono, int ext, int count, int mipCount, GraphicsFormat format, TextureColorSpace colorSpace, TextureCreationFlags flags)
		{
			bool flag = !CubemapArray.Internal_CreateImpl(mono, ext, count, mipCount, format, colorSpace, flags);
			if (flag)
			{
				throw new UnityException("Failed to create cubemap array texture because of invalid parameters.");
			}
		}

		// Token: 0x060011F4 RID: 4596
		[FreeFunction(Name = "CubemapArrayScripting::Apply", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		// Token: 0x060011F5 RID: 4597
		[FreeFunction(Name = "CubemapArrayScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels(CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x060011F6 RID: 4598 RVA: 0x000180BC File Offset: 0x000162BC
		public Color[] GetPixels(CubemapFace face, int arrayElement)
		{
			return this.GetPixels(face, arrayElement, 0);
		}

		// Token: 0x060011F7 RID: 4599
		[FreeFunction(Name = "CubemapArrayScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32(CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x060011F8 RID: 4600 RVA: 0x000180D8 File Offset: 0x000162D8
		public Color32[] GetPixels32(CubemapFace face, int arrayElement)
		{
			return this.GetPixels32(face, arrayElement, 0);
		}

		// Token: 0x060011F9 RID: 4601
		[FreeFunction(Name = "CubemapArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels([Unmarshalled] Color[] colors, CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x060011FA RID: 4602 RVA: 0x000180F3 File Offset: 0x000162F3
		public void SetPixels(Color[] colors, CubemapFace face, int arrayElement)
		{
			this.SetPixels(colors, face, arrayElement, 0);
		}

		// Token: 0x060011FB RID: 4603
		[FreeFunction(Name = "CubemapArrayScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPixels32([Unmarshalled] Color32[] colors, CubemapFace face, int arrayElement, int miplevel);

		// Token: 0x060011FC RID: 4604 RVA: 0x00018101 File Offset: 0x00016301
		public void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement)
		{
			this.SetPixels32(colors, face, arrayElement, 0);
		}

		// Token: 0x060011FD RID: 4605
		[FreeFunction(Name = "CubemapArrayScripting::SetPixelDataArray", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImplArray(Array data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x060011FE RID: 4606
		[FreeFunction(Name = "CubemapArrayScripting::SetPixelData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetPixelDataImpl(IntPtr data, int mipLevel, int face, int element, int elementSize, int dataArraySize, int sourceDataStartIndex = 0);

		// Token: 0x060011FF RID: 4607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetImageDataPointer();

		// Token: 0x06001200 RID: 4608 RVA: 0x0001810F File Offset: 0x0001630F
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, DefaultFormat format, TextureCreationFlags flags) : this(width, cubemapCount, SystemInfo.GetGraphicsFormat(format), flags)
		{
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00018123 File Offset: 0x00016323
		[RequiredByNativeCode]
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags) : this(width, cubemapCount, format, flags, Texture.GenerateAllMips)
		{
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00018138 File Offset: 0x00016338
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags, [DefaultValue("-1")] int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Sample);
			if (!flag)
			{
				CubemapArray.ValidateIsNotCrunched(flags);
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, format, base.GetTextureColorSpace(format), flags);
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0001817C File Offset: 0x0001637C
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, int mipCount, bool linear)
		{
			bool flag = !base.ValidateFormat(textureFormat);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				TextureCreationFlags textureCreationFlags = (mipCount != 1) ? TextureCreationFlags.MipChain : TextureCreationFlags.None;
				bool flag2 = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
				if (flag2)
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				CubemapArray.ValidateIsNotCrunched(textureCreationFlags);
				CubemapArray.Internal_Create(this, width, cubemapCount, mipCount, graphicsFormat, base.GetTextureColorSpace(linear), textureCreationFlags);
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000181E2 File Offset: 0x000163E2
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear) : this(width, cubemapCount, textureFormat, mipChain ? -1 : 1, linear)
		{
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000181F9 File Offset: 0x000163F9
		[ExcludeFromDocs]
		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain) : this(width, cubemapCount, textureFormat, mipChain ? -1 : 1, false)
		{
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00018210 File Offset: 0x00016410
		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			bool flag = !this.isReadable;
			if (flag)
			{
				throw base.CreateNonReadableException(this);
			}
			this.ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0001823C File Offset: 0x0001643C
		[ExcludeFromDocs]
		public void Apply(bool updateMipmaps)
		{
			this.Apply(updateMipmaps, false);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00018248 File Offset: 0x00016448
		[ExcludeFromDocs]
		public void Apply()
		{
			this.Apply(true, false);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00018254 File Offset: 0x00016454
		public void SetPixelData<T>(T[] data, int mipLevel, CubemapFace face, int element, [DefaultValue("0")] int sourceDataStartIndex = 0)
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
			this.SetPixelDataImplArray(data, mipLevel, (int)face, element, Marshal.SizeOf(data[0]), data.Length, sourceDataStartIndex);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000182C8 File Offset: 0x000164C8
		public void SetPixelData<T>(NativeArray<T> data, int mipLevel, CubemapFace face, int element, [DefaultValue("0")] int sourceDataStartIndex = 0) where T : struct
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
			this.SetPixelDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), mipLevel, (int)face, element, UnsafeUtility.SizeOf<T>(), data.Length, sourceDataStartIndex);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00018348 File Offset: 0x00016548
		public unsafe NativeArray<T> GetPixelData<T>(int mipLevel, CubemapFace face, int element) where T : struct
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
				throw new ArgumentException("The passed in face " + face.ToString() + " is invalid.  The valid range is 0 through 5");
			}
			bool flag4 = element < 0 || element >= this.cubemapCount;
			if (flag4)
			{
				throw new ArgumentException("The passed in element " + element.ToString() + " is invalid. The valid range is 0 through " + (this.cubemapCount - 1).ToString());
			}
			int num = (int)(element * 6 + face);
			ulong pixelDataOffset = base.GetPixelDataOffset(base.mipmapCount, num);
			ulong pixelDataOffset2 = base.GetPixelDataOffset(mipLevel, num);
			ulong pixelDataSize = base.GetPixelDataSize(mipLevel, num);
			int num2 = UnsafeUtility.SizeOf<T>();
			ulong num3 = pixelDataSize / (ulong)((long)num2);
			bool flag5 = num3 > 2147483647UL;
			if (flag5)
			{
				throw base.CreateNativeArrayLengthOverflowException();
			}
			IntPtr value = new IntPtr((long)this.GetImageDataPointer() + (long)(pixelDataOffset * (ulong)((long)num) + pixelDataOffset2));
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)value, (int)num3, Allocator.None);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000184B0 File Offset: 0x000166B0
		private static void ValidateIsNotCrunched(TextureCreationFlags flags)
		{
			bool flag = (flags &= TextureCreationFlags.Crunch) > TextureCreationFlags.None;
			if (flag)
			{
				throw new ArgumentException("Crunched TextureCubeArray is not supported.");
			}
		}
	}
}
