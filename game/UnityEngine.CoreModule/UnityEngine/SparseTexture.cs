using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001A9 RID: 425
	[NativeHeader("Runtime/Graphics/SparseTexture.h")]
	public sealed class SparseTexture : Texture
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600120D RID: 4621
		public extern int tileWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600120E RID: 4622
		public extern int tileHeight { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600120F RID: 4623
		public extern bool isCreated { [NativeName("IsInitialized")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001210 RID: 4624
		[FreeFunction(Name = "SparseTextureScripting::Create", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] SparseTexture mono, int width, int height, GraphicsFormat format, TextureColorSpace colorSpace, int mipCount);

		// Token: 0x06001211 RID: 4625
		[FreeFunction(Name = "SparseTextureScripting::UpdateTile", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateTile(int tileX, int tileY, int miplevel, [Unmarshalled] Color32[] data);

		// Token: 0x06001212 RID: 4626
		[FreeFunction(Name = "SparseTextureScripting::UpdateTileRaw", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdateTileRaw(int tileX, int tileY, int miplevel, [Unmarshalled] byte[] data);

		// Token: 0x06001213 RID: 4627 RVA: 0x000184D7 File Offset: 0x000166D7
		public void UnloadTile(int tileX, int tileY, int miplevel)
		{
			this.UpdateTileRaw(tileX, tileY, miplevel, null);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x000184E8 File Offset: 0x000166E8
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

		// Token: 0x06001215 RID: 4629 RVA: 0x00018554 File Offset: 0x00016754
		internal bool ValidateFormat(GraphicsFormat format, int width, int height)
		{
			bool flag = base.ValidateFormat(format, FormatUsage.Sparse);
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

		// Token: 0x06001216 RID: 4630 RVA: 0x000185B8 File Offset: 0x000167B8
		internal bool ValidateSize(int width, int height, GraphicsFormat format)
		{
			bool flag = (ulong)GraphicsFormatUtility.GetBlockSize(format) * (ulong)((long)width / (long)((ulong)GraphicsFormatUtility.GetBlockWidth(format))) * (ulong)((long)height / (long)((ulong)GraphicsFormatUtility.GetBlockHeight(format))) < 65536UL;
			bool result;
			if (flag)
			{
				Debug.LogError("SparseTexture creation failed. The minimum size in bytes of a SparseTexture is 64KB.", this);
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00018608 File Offset: 0x00016808
		private static void ValidateIsNotCrunched(TextureFormat textureFormat)
		{
			bool flag = GraphicsFormatUtility.IsCrunchFormat(textureFormat);
			if (flag)
			{
				throw new ArgumentException("Crunched SparseTexture is not supported.");
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0001862B File Offset: 0x0001682B
		[ExcludeFromDocs]
		public SparseTexture(int width, int height, DefaultFormat format, int mipCount) : this(width, height, SystemInfo.GetGraphicsFormat(format), mipCount)
		{
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00018640 File Offset: 0x00016840
		[ExcludeFromDocs]
		public SparseTexture(int width, int height, GraphicsFormat format, int mipCount)
		{
			bool flag = !this.ValidateFormat(format, width, height);
			if (!flag)
			{
				bool flag2 = !this.ValidateSize(width, height, format);
				if (!flag2)
				{
					SparseTexture.Internal_Create(this, width, height, format, base.GetTextureColorSpace(format), mipCount);
				}
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0001868C File Offset: 0x0001688C
		[ExcludeFromDocs]
		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount) : this(width, height, textureFormat, mipCount, false)
		{
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0001869C File Offset: 0x0001689C
		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount, [DefaultValue("false")] bool linear)
		{
			bool flag = !this.ValidateFormat(textureFormat, width, height);
			if (!flag)
			{
				SparseTexture.ValidateIsNotCrunched(textureFormat);
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				bool flag2 = !SystemInfo.IsFormatSupported(graphicsFormat, FormatUsage.Sparse);
				if (flag2)
				{
					Debug.LogError(string.Format("Creation of a SparseTexture with '{0}' is not supported on this platform.", textureFormat));
				}
				else
				{
					bool flag3 = !this.ValidateSize(width, height, graphicsFormat);
					if (!flag3)
					{
						SparseTexture.Internal_Create(this, width, height, graphicsFormat, base.GetTextureColorSpace(linear), mipCount);
					}
				}
			}
		}
	}
}
