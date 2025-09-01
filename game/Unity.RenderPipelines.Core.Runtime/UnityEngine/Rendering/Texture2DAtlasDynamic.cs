using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000098 RID: 152
	internal class Texture2DAtlasDynamic
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000170BA File Offset: 0x000152BA
		public RTHandle AtlasTexture
		{
			get
			{
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000170C4 File Offset: 0x000152C4
		public Texture2DAtlasDynamic(int width, int height, int capacity, GraphicsFormat format)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_Format = format;
			this.m_AtlasTexture = RTHandles.Alloc(this.m_Width, this.m_Height, 1, DepthBits.None, this.m_Format, FilterMode.Point, TextureWrapMode.Clamp, TextureDimension.Tex2D, false, true, false, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "");
			this.isAtlasTextureOwner = true;
			this.m_AtlasAllocator = new AtlasAllocatorDynamic(width, height, capacity);
			this.m_AllocationCache = new Dictionary<int, Vector4>(capacity);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00017144 File Offset: 0x00015344
		public Texture2DAtlasDynamic(int width, int height, int capacity, RTHandle atlasTexture)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_Format = atlasTexture.rt.graphicsFormat;
			this.m_AtlasTexture = atlasTexture;
			this.isAtlasTextureOwner = false;
			this.m_AtlasAllocator = new AtlasAllocatorDynamic(width, height, capacity);
			this.m_AllocationCache = new Dictionary<int, Vector4>(capacity);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000171A0 File Offset: 0x000153A0
		public void Release()
		{
			this.ResetAllocator();
			if (this.isAtlasTextureOwner)
			{
				RTHandles.Release(this.m_AtlasTexture);
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000171BB File Offset: 0x000153BB
		public void ResetAllocator()
		{
			this.m_AtlasAllocator.Release();
			this.m_AllocationCache.Clear();
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000171D4 File Offset: 0x000153D4
		public bool AddTexture(CommandBuffer cmd, out Vector4 scaleOffset, Texture texture)
		{
			int instanceID = texture.GetInstanceID();
			if (this.m_AllocationCache.TryGetValue(instanceID, out scaleOffset))
			{
				return true;
			}
			int width = texture.width;
			int height = texture.height;
			if (this.m_AtlasAllocator.Allocate(out scaleOffset, instanceID, width, height))
			{
				scaleOffset.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
				for (int i = 0; i < (texture as Texture2D).mipmapCount; i++)
				{
					cmd.SetRenderTarget(this.m_AtlasTexture, i);
					Blitter.BlitQuad(cmd, texture, new Vector4(1f, 1f, 0f, 0f), scaleOffset, i, false);
				}
				this.m_AllocationCache.Add(instanceID, scaleOffset);
				return true;
			}
			return false;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000172C2 File Offset: 0x000154C2
		public bool IsCached(out Vector4 scaleOffset, int key)
		{
			return this.m_AllocationCache.TryGetValue(key, out scaleOffset);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000172D4 File Offset: 0x000154D4
		public bool EnsureTextureSlot(out bool isUploadNeeded, out Vector4 scaleOffset, int key, int width, int height)
		{
			isUploadNeeded = false;
			if (this.m_AllocationCache.TryGetValue(key, out scaleOffset))
			{
				return true;
			}
			if (!this.m_AtlasAllocator.Allocate(out scaleOffset, key, width, height))
			{
				return false;
			}
			isUploadNeeded = true;
			scaleOffset.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
			this.m_AllocationCache.Add(key, scaleOffset);
			return true;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001735F File Offset: 0x0001555F
		public void ReleaseTextureSlot(int key)
		{
			this.m_AtlasAllocator.Release(key);
			this.m_AllocationCache.Remove(key);
		}

		// Token: 0x04000329 RID: 809
		private RTHandle m_AtlasTexture;

		// Token: 0x0400032A RID: 810
		private bool isAtlasTextureOwner;

		// Token: 0x0400032B RID: 811
		private int m_Width;

		// Token: 0x0400032C RID: 812
		private int m_Height;

		// Token: 0x0400032D RID: 813
		private GraphicsFormat m_Format;

		// Token: 0x0400032E RID: 814
		private AtlasAllocatorDynamic m_AtlasAllocator;

		// Token: 0x0400032F RID: 815
		private Dictionary<int, Vector4> m_AllocationCache;
	}
}
