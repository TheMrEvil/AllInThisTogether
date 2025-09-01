using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200031C RID: 796
	internal class ShaderInfoStorage<T> : BaseShaderInfoStorage where T : struct
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x0006E7F0 File Offset: 0x0006C9F0
		public ShaderInfoStorage(TextureFormat format, Func<Color, T> convert, int initialSize = 64, int maxSize = 4096)
		{
			Debug.Assert(maxSize <= SystemInfo.maxTextureSize);
			Debug.Assert(initialSize <= maxSize);
			Debug.Assert(Mathf.IsPowerOfTwo(initialSize));
			Debug.Assert(Mathf.IsPowerOfTwo(maxSize));
			Debug.Assert(convert != null);
			this.m_InitialSize = initialSize;
			this.m_MaxSize = maxSize;
			this.m_Format = format;
			this.m_Convert = convert;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0006E868 File Offset: 0x0006CA68
		protected override void Dispose(bool disposing)
		{
			bool flag = !base.disposed && disposing;
			if (flag)
			{
				UIRUtility.Destroy(this.m_Texture);
				this.m_Texture = null;
				this.m_Texels = default(NativeArray<T>);
				UIRAtlasAllocator allocator = this.m_Allocator;
				if (allocator != null)
				{
					allocator.Dispose();
				}
				this.m_Allocator = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0006E8C7 File Offset: 0x0006CAC7
		public override Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0006E8D0 File Offset: 0x0006CAD0
		public override bool AllocateRect(int width, int height, out RectInt uvs)
		{
			bool disposed = base.disposed;
			bool result;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				uvs = default(RectInt);
				result = false;
			}
			else
			{
				bool flag = this.m_Allocator == null;
				if (flag)
				{
					this.m_Allocator = new UIRAtlasAllocator(this.m_InitialSize, this.m_MaxSize, 0);
				}
				bool flag2 = !this.m_Allocator.TryAllocate(width, height, out uvs);
				if (flag2)
				{
					result = false;
				}
				else
				{
					uvs = new RectInt(uvs.x, uvs.y, width, height);
					this.CreateOrExpandTexture();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0006E960 File Offset: 0x0006CB60
		public override void SetTexel(int x, int y, Color color)
		{
			bool disposed = base.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = !this.m_Texels.IsCreated;
				if (flag)
				{
					BaseShaderInfoStorage.s_MarkerGetTextureData.Begin();
					this.m_Texels = this.m_Texture.GetRawTextureData<T>();
					BaseShaderInfoStorage.s_MarkerGetTextureData.End();
				}
				this.m_Texels[x + y * this.m_Texture.width] = this.m_Convert(color);
			}
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0006E9E4 File Offset: 0x0006CBE4
		public override void UpdateTexture()
		{
			bool disposed = base.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_Texture == null || !this.m_Texels.IsCreated;
				if (!flag)
				{
					BaseShaderInfoStorage.s_MarkerUpdateTexture.Begin();
					this.m_Texture.Apply(false, false);
					this.m_Texels = default(NativeArray<T>);
					BaseShaderInfoStorage.s_MarkerUpdateTexture.End();
				}
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0006EA5C File Offset: 0x0006CC5C
		private void CreateOrExpandTexture()
		{
			int physicalWidth = this.m_Allocator.physicalWidth;
			int physicalHeight = this.m_Allocator.physicalHeight;
			bool flag = false;
			bool flag2 = this.m_Texture != null;
			if (flag2)
			{
				bool flag3 = this.m_Texture.width == physicalWidth && this.m_Texture.height == physicalHeight;
				if (flag3)
				{
					return;
				}
				flag = true;
			}
			Texture2D texture2D = new Texture2D(this.m_Allocator.physicalWidth, this.m_Allocator.physicalHeight, this.m_Format, false)
			{
				name = "UIR Shader Info " + BaseShaderInfoStorage.s_TextureCounter++.ToString(),
				hideFlags = HideFlags.HideAndDontSave,
				filterMode = FilterMode.Point
			};
			bool flag4 = flag;
			if (flag4)
			{
				BaseShaderInfoStorage.s_MarkerCopyTexture.Begin();
				NativeArray<T> src = this.m_Texels.IsCreated ? this.m_Texels : this.m_Texture.GetRawTextureData<T>();
				NativeArray<T> rawTextureData = texture2D.GetRawTextureData<T>();
				ShaderInfoStorage<T>.CpuBlit(src, this.m_Texture.width, this.m_Texture.height, rawTextureData, texture2D.width, texture2D.height);
				this.m_Texels = rawTextureData;
				BaseShaderInfoStorage.s_MarkerCopyTexture.End();
			}
			else
			{
				this.m_Texels = default(NativeArray<T>);
			}
			UIRUtility.Destroy(this.m_Texture);
			this.m_Texture = texture2D;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0006EBC0 File Offset: 0x0006CDC0
		private static void CpuBlit(NativeArray<T> src, int srcWidth, int srcHeight, NativeArray<T> dst, int dstWidth, int dstHeight)
		{
			Debug.Assert(dstWidth >= srcWidth && dstHeight >= srcHeight);
			int num = dstWidth - srcWidth;
			int num2 = dstHeight - srcHeight;
			int num3 = srcWidth * srcHeight;
			int i = 0;
			int num4 = 0;
			int num5 = srcWidth;
			while (i < num3)
			{
				while (i < num5)
				{
					dst[num4] = src[i];
					num4++;
					i++;
				}
				num5 += srcWidth;
				num4 += num;
			}
		}

		// Token: 0x04000BDC RID: 3036
		private readonly int m_InitialSize;

		// Token: 0x04000BDD RID: 3037
		private readonly int m_MaxSize;

		// Token: 0x04000BDE RID: 3038
		private readonly TextureFormat m_Format;

		// Token: 0x04000BDF RID: 3039
		private readonly Func<Color, T> m_Convert;

		// Token: 0x04000BE0 RID: 3040
		private UIRAtlasAllocator m_Allocator;

		// Token: 0x04000BE1 RID: 3041
		private Texture2D m_Texture;

		// Token: 0x04000BE2 RID: 3042
		private NativeArray<T> m_Texels;
	}
}
