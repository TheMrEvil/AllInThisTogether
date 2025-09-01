using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000096 RID: 150
	public class Texture2DAtlas
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00016367 File Offset: 0x00014567
		public static int maxMipLevelPadding
		{
			get
			{
				return Texture2DAtlas.s_MaxMipLevelPadding;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001636E File Offset: 0x0001456E
		public RTHandle AtlasTexture
		{
			get
			{
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00016378 File Offset: 0x00014578
		public Texture2DAtlas(int width, int height, GraphicsFormat format, FilterMode filterMode = FilterMode.Point, bool powerOfTwoPadding = false, string name = "", bool useMipMap = true)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_Format = format;
			this.m_UseMipMaps = useMipMap;
			this.m_AtlasTexture = RTHandles.Alloc(this.m_Width, this.m_Height, 1, DepthBits.None, this.m_Format, filterMode, TextureWrapMode.Clamp, TextureDimension.Tex2D, false, useMipMap, false, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, name);
			this.m_IsAtlasTextureOwner = true;
			int num = useMipMap ? this.GetTextureMipmapCount(this.m_Width, this.m_Height) : 1;
			for (int i = 0; i < num; i++)
			{
				Graphics.SetRenderTarget(this.m_AtlasTexture, i);
				GL.Clear(false, true, Color.clear);
			}
			this.m_AtlasAllocator = new AtlasAllocator(width, height, powerOfTwoPadding);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00016458 File Offset: 0x00014658
		public void Release()
		{
			this.ResetAllocator();
			if (this.m_IsAtlasTextureOwner)
			{
				RTHandles.Release(this.m_AtlasTexture);
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00016473 File Offset: 0x00014673
		public void ResetAllocator()
		{
			this.m_AtlasAllocator.Reset();
			this.m_AllocationCache.Clear();
			this.m_IsGPUTextureUpToDate.Clear();
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00016498 File Offset: 0x00014698
		public void ClearTarget(CommandBuffer cmd)
		{
			int num = this.m_UseMipMaps ? this.GetTextureMipmapCount(this.m_Width, this.m_Height) : 1;
			for (int i = 0; i < num; i++)
			{
				cmd.SetRenderTarget(this.m_AtlasTexture, i);
				Blitter.BlitQuad(cmd, Texture2D.blackTexture, Texture2DAtlas.fullScaleOffset, Texture2DAtlas.fullScaleOffset, i, true);
			}
			this.m_IsGPUTextureUpToDate.Clear();
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00016503 File Offset: 0x00014703
		private protected int GetTextureMipmapCount(int width, int height)
		{
			if (!this.m_UseMipMaps)
			{
				return 1;
			}
			return Mathf.FloorToInt(Mathf.Log((float)Mathf.Max(width, height), 2f)) + 1;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00016528 File Offset: 0x00014728
		private protected bool Is2D(Texture texture)
		{
			RenderTexture renderTexture = texture as RenderTexture;
			return texture is Texture2D || (renderTexture != null && renderTexture.dimension == TextureDimension.Tex2D);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00016554 File Offset: 0x00014754
		private protected bool IsSingleChannelBlit(Texture source, Texture destination)
		{
			uint componentCount = GraphicsFormatUtility.GetComponentCount(source.graphicsFormat);
			uint componentCount2 = GraphicsFormatUtility.GetComponentCount(destination.graphicsFormat);
			if (componentCount == 1U || componentCount2 == 1U)
			{
				if (componentCount != componentCount2)
				{
					return true;
				}
				int num = 1 << (int)(GraphicsFormatUtility.GetSwizzleA(source.graphicsFormat) & (FormatSwizzle)7) << 24 | 1 << (int)(GraphicsFormatUtility.GetSwizzleB(source.graphicsFormat) & (FormatSwizzle)7) << 16 | 1 << (int)(GraphicsFormatUtility.GetSwizzleG(source.graphicsFormat) & (FormatSwizzle)7) << 8 | 1 << (int)(GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) & (FormatSwizzle)7);
				int num2 = 1 << (int)(GraphicsFormatUtility.GetSwizzleA(destination.graphicsFormat) & (FormatSwizzle)7) << 24 | 1 << (int)(GraphicsFormatUtility.GetSwizzleB(destination.graphicsFormat) & (FormatSwizzle)7) << 16 | 1 << (int)(GraphicsFormatUtility.GetSwizzleG(destination.graphicsFormat) & (FormatSwizzle)7) << 8 | 1 << (int)(GraphicsFormatUtility.GetSwizzleR(destination.graphicsFormat) & (FormatSwizzle)7);
				if (num != num2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00016638 File Offset: 0x00014838
		private void Blit2DTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips, Texture2DAtlas.BlitType blitType)
		{
			int num = this.GetTextureMipmapCount(texture.width, texture.height);
			if (!blitMips)
			{
				num = 1;
			}
			for (int i = 0; i < num; i++)
			{
				cmd.SetRenderTarget(this.m_AtlasTexture, i);
				switch (blitType)
				{
				case Texture2DAtlas.BlitType.Default:
					Blitter.BlitQuad(cmd, texture, sourceScaleOffset, scaleOffset, i, true);
					break;
				case Texture2DAtlas.BlitType.CubeTo2DOctahedral:
					Blitter.BlitCubeToOctahedral2DQuad(cmd, texture, scaleOffset, i);
					break;
				case Texture2DAtlas.BlitType.SingleChannel:
					Blitter.BlitQuadSingleChannel(cmd, texture, sourceScaleOffset, scaleOffset, i);
					break;
				case Texture2DAtlas.BlitType.CubeTo2DOctahedralSingleChannel:
					Blitter.BlitCubeToOctahedral2DQuadSingleChannel(cmd, texture, scaleOffset, i);
					break;
				}
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000166C4 File Offset: 0x000148C4
		private protected void MarkGPUTextureValid(int instanceId, bool mipAreValid = false)
		{
			this.m_IsGPUTextureUpToDate[instanceId] = (mipAreValid ? 2 : 1);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000166D9 File Offset: 0x000148D9
		private protected void MarkGPUTextureInvalid(int instanceId)
		{
			this.m_IsGPUTextureUpToDate[instanceId] = 0;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000166E8 File Offset: 0x000148E8
		public virtual void BlitTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (this.Is2D(texture))
			{
				Texture2DAtlas.BlitType blitType = Texture2DAtlas.BlitType.Default;
				if (this.IsSingleChannelBlit(texture, this.m_AtlasTexture.m_RT))
				{
					blitType = Texture2DAtlas.BlitType.SingleChannel;
				}
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, blitType);
				int num = (overrideInstanceID != -1) ? overrideInstanceID : this.GetTextureID(texture);
				this.MarkGPUTextureValid(num, blitMips);
				this.m_TextureHashes[num] = CoreUtils.GetTextureHash(texture);
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00016750 File Offset: 0x00014950
		public virtual void BlitOctahedralTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			this.BlitTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, overrideInstanceID);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00016764 File Offset: 0x00014964
		public virtual void BlitCubeTexture2D(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (texture.dimension == TextureDimension.Cube)
			{
				Texture2DAtlas.BlitType blitType = Texture2DAtlas.BlitType.CubeTo2DOctahedral;
				if (this.IsSingleChannelBlit(texture, this.m_AtlasTexture.m_RT))
				{
					blitType = Texture2DAtlas.BlitType.CubeTo2DOctahedralSingleChannel;
				}
				this.Blit2DTexture(cmd, scaleOffset, texture, new Vector4(1f, 1f, 0f, 0f), blitMips, blitType);
				int num = (overrideInstanceID != -1) ? overrideInstanceID : this.GetTextureID(texture);
				this.MarkGPUTextureValid(num, blitMips);
				this.m_TextureHashes[num] = CoreUtils.GetTextureHash(texture);
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000167E4 File Offset: 0x000149E4
		public virtual bool AllocateTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			int num = (overrideInstanceID != -1) ? overrideInstanceID : this.GetTextureID(texture);
			bool flag = this.AllocateTextureWithoutBlit(num, width, height, ref scaleOffset);
			if (flag)
			{
				if (this.Is2D(texture))
				{
					this.BlitTexture(cmd, scaleOffset, texture, Texture2DAtlas.fullScaleOffset, true, -1);
				}
				else
				{
					this.BlitCubeTexture2D(cmd, scaleOffset, texture, true, -1);
				}
				this.MarkGPUTextureValid(num, true);
				this.m_TextureHashes[num] = CoreUtils.GetTextureHash(texture);
			}
			return flag;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001685B File Offset: 0x00014A5B
		public bool AllocateTextureWithoutBlit(Texture texture, int width, int height, ref Vector4 scaleOffset)
		{
			return this.AllocateTextureWithoutBlit(texture.GetInstanceID(), width, height, ref scaleOffset);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00016870 File Offset: 0x00014A70
		public virtual bool AllocateTextureWithoutBlit(int instanceId, int width, int height, ref Vector4 scaleOffset)
		{
			scaleOffset = Vector4.zero;
			if (this.m_AtlasAllocator.Allocate(ref scaleOffset, width, height))
			{
				scaleOffset.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
				this.m_AllocationCache[instanceId] = new ValueTuple<Vector4, Vector2Int>(scaleOffset, new Vector2Int(width, height));
				this.MarkGPUTextureInvalid(instanceId);
				this.m_TextureHashes[instanceId] = -1;
				return true;
			}
			return false;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00016910 File Offset: 0x00014B10
		private protected int GetTextureHash(Texture textureA, Texture textureB)
		{
			return CoreUtils.GetTextureHash(textureA) + 23 * CoreUtils.GetTextureHash(textureB);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00016922 File Offset: 0x00014B22
		public int GetTextureID(Texture texture)
		{
			return texture.GetInstanceID();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001692A File Offset: 0x00014B2A
		public int GetTextureID(Texture textureA, Texture textureB)
		{
			return this.GetTextureID(textureA) + 23 * this.GetTextureID(textureB);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001693E File Offset: 0x00014B3E
		public bool IsCached(out Vector4 scaleOffset, Texture textureA, Texture textureB)
		{
			return this.IsCached(out scaleOffset, this.GetTextureID(textureA, textureB));
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001694F File Offset: 0x00014B4F
		public bool IsCached(out Vector4 scaleOffset, Texture texture)
		{
			return this.IsCached(out scaleOffset, this.GetTextureID(texture));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00016960 File Offset: 0x00014B60
		public bool IsCached(out Vector4 scaleOffset, int id)
		{
			ValueTuple<Vector4, Vector2Int> valueTuple;
			bool result = this.m_AllocationCache.TryGetValue(id, out valueTuple);
			scaleOffset = valueTuple.Item1;
			return result;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00016988 File Offset: 0x00014B88
		internal Vector2Int GetCachedTextureSize(int id)
		{
			ValueTuple<Vector4, Vector2Int> valueTuple;
			this.m_AllocationCache.TryGetValue(id, out valueTuple);
			return valueTuple.Item2;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000169AC File Offset: 0x00014BAC
		public virtual bool NeedsUpdate(Texture texture, bool needMips = false)
		{
			RenderTexture renderTexture = texture as RenderTexture;
			int textureID = this.GetTextureID(texture);
			int textureHash = CoreUtils.GetTextureHash(texture);
			if (renderTexture != null)
			{
				int num;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(textureID, out num))
				{
					if ((ulong)renderTexture.updateCount != (ulong)((long)num))
					{
						this.m_IsGPUTextureUpToDate[textureID] = (int)renderTexture.updateCount;
						return true;
					}
				}
				else
				{
					this.m_IsGPUTextureUpToDate[textureID] = (int)renderTexture.updateCount;
				}
			}
			else
			{
				int num2;
				if (this.m_TextureHashes.TryGetValue(textureID, out num2) && num2 != textureHash)
				{
					this.m_TextureHashes[textureID] = textureHash;
					return true;
				}
				int num3;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(textureID, out num3))
				{
					return num3 == 0 || (needMips && num3 == 1);
				}
			}
			return false;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00016A64 File Offset: 0x00014C64
		public virtual bool NeedsUpdate(Texture textureA, Texture textureB, bool needMips = false)
		{
			RenderTexture renderTexture = textureA as RenderTexture;
			RenderTexture renderTexture2 = textureB as RenderTexture;
			int textureID = this.GetTextureID(textureA, textureB);
			int textureHash = this.GetTextureHash(textureA, textureB);
			if (renderTexture != null || renderTexture2 != null)
			{
				int num;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(textureID, out num))
				{
					if (renderTexture != null && renderTexture2 != null && (ulong)Math.Min(renderTexture.updateCount, renderTexture2.updateCount) != (ulong)((long)num))
					{
						this.m_IsGPUTextureUpToDate[textureID] = (int)Math.Min(renderTexture.updateCount, renderTexture2.updateCount);
						return true;
					}
					if (renderTexture != null && (ulong)renderTexture.updateCount != (ulong)((long)num))
					{
						this.m_IsGPUTextureUpToDate[textureID] = (int)renderTexture.updateCount;
						return true;
					}
					if (renderTexture2 != null && (ulong)renderTexture2.updateCount != (ulong)((long)num))
					{
						this.m_IsGPUTextureUpToDate[textureID] = (int)renderTexture2.updateCount;
						return true;
					}
				}
				else
				{
					this.m_IsGPUTextureUpToDate[textureID] = textureHash;
				}
			}
			else
			{
				int num2;
				if (this.m_TextureHashes.TryGetValue(textureID, out num2) && num2 != textureHash)
				{
					this.m_TextureHashes[textureID] = textureID;
					return true;
				}
				int num3;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(textureID, out num3))
				{
					return num3 == 0 || (needMips && num3 == 1);
				}
			}
			return false;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00016BA8 File Offset: 0x00014DA8
		public virtual bool AddTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture)
		{
			return this.IsCached(out scaleOffset, texture) || this.AllocateTexture(cmd, ref scaleOffset, texture, texture.width, texture.height, -1);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00016BCC File Offset: 0x00014DCC
		public virtual bool UpdateTexture(CommandBuffer cmd, Texture oldTexture, Texture newTexture, ref Vector4 scaleOffset, Vector4 sourceScaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			if (this.IsCached(out scaleOffset, oldTexture))
			{
				if (updateIfNeeded && this.NeedsUpdate(newTexture, false))
				{
					if (this.Is2D(newTexture))
					{
						this.BlitTexture(cmd, scaleOffset, newTexture, sourceScaleOffset, blitMips, -1);
					}
					else
					{
						this.BlitCubeTexture2D(cmd, scaleOffset, newTexture, blitMips, -1);
					}
					this.MarkGPUTextureValid(this.GetTextureID(newTexture), blitMips);
				}
				return true;
			}
			return this.AllocateTexture(cmd, ref scaleOffset, newTexture, newTexture.width, newTexture.height, -1);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00016C4B File Offset: 0x00014E4B
		public virtual bool UpdateTexture(CommandBuffer cmd, Texture texture, ref Vector4 scaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			return this.UpdateTexture(cmd, texture, texture, ref scaleOffset, Texture2DAtlas.fullScaleOffset, updateIfNeeded, blitMips);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00016C60 File Offset: 0x00014E60
		internal bool EnsureTextureSlot(out bool isUploadNeeded, ref Vector4 scaleBias, int key, int width, int height)
		{
			isUploadNeeded = false;
			ValueTuple<Vector4, Vector2Int> valueTuple;
			if (this.m_AllocationCache.TryGetValue(key, out valueTuple))
			{
				scaleBias = valueTuple.Item1;
				return true;
			}
			if (!this.m_AtlasAllocator.Allocate(ref scaleBias, width, height))
			{
				return false;
			}
			isUploadNeeded = true;
			scaleBias.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
			this.m_AllocationCache.Add(key, new ValueTuple<Vector4, Vector2Int>(scaleBias, new Vector2Int(width, height)));
			return true;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00016D05 File Offset: 0x00014F05
		// Note: this type is marked as 'beforefieldinit'.
		static Texture2DAtlas()
		{
		}

		// Token: 0x04000315 RID: 789
		private protected const int kGPUTexInvalid = 0;

		// Token: 0x04000316 RID: 790
		private protected const int kGPUTexValidMip0 = 1;

		// Token: 0x04000317 RID: 791
		private protected const int kGPUTexValidMipAll = 2;

		// Token: 0x04000318 RID: 792
		private protected RTHandle m_AtlasTexture;

		// Token: 0x04000319 RID: 793
		private protected int m_Width;

		// Token: 0x0400031A RID: 794
		private protected int m_Height;

		// Token: 0x0400031B RID: 795
		private protected GraphicsFormat m_Format;

		// Token: 0x0400031C RID: 796
		private protected bool m_UseMipMaps;

		// Token: 0x0400031D RID: 797
		private bool m_IsAtlasTextureOwner;

		// Token: 0x0400031E RID: 798
		private AtlasAllocator m_AtlasAllocator;

		// Token: 0x0400031F RID: 799
		[TupleElementNames(new string[]
		{
			"scaleOffset",
			"size"
		})]
		private Dictionary<int, ValueTuple<Vector4, Vector2Int>> m_AllocationCache = new Dictionary<int, ValueTuple<Vector4, Vector2Int>>();

		// Token: 0x04000320 RID: 800
		private Dictionary<int, int> m_IsGPUTextureUpToDate = new Dictionary<int, int>();

		// Token: 0x04000321 RID: 801
		private Dictionary<int, int> m_TextureHashes = new Dictionary<int, int>();

		// Token: 0x04000322 RID: 802
		private static readonly Vector4 fullScaleOffset = new Vector4(1f, 1f, 0f, 0f);

		// Token: 0x04000323 RID: 803
		private static readonly int s_MaxMipLevelPadding = 10;

		// Token: 0x02000170 RID: 368
		private enum BlitType
		{
			// Token: 0x04000581 RID: 1409
			Default,
			// Token: 0x04000582 RID: 1410
			CubeTo2DOctahedral,
			// Token: 0x04000583 RID: 1411
			SingleChannel,
			// Token: 0x04000584 RID: 1412
			CubeTo2DOctahedralSingleChannel
		}
	}
}
