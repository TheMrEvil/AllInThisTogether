using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008F RID: 143
	public class PowerOfTwoTextureAtlas : Texture2DAtlas
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x00014A2A File Offset: 0x00012C2A
		public PowerOfTwoTextureAtlas(int size, int mipPadding, GraphicsFormat format, FilterMode filterMode = FilterMode.Point, string name = "", bool useMipMap = true) : base(size, size, format, filterMode, true, name, useMipMap)
		{
			this.m_MipPadding = mipPadding;
			int num = size & size - 1;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00014A54 File Offset: 0x00012C54
		public int mipPadding
		{
			get
			{
				return this.m_MipPadding;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00014A5C File Offset: 0x00012C5C
		private int GetTexturePadding()
		{
			return (int)Mathf.Pow(2f, (float)this.m_MipPadding) * 2;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00014A74 File Offset: 0x00012C74
		public Vector4 GetPayloadScaleOffset(Texture texture, in Vector4 scaleOffset)
		{
			int texturePadding = this.GetTexturePadding();
			Vector2 vector = Vector2.one * (float)texturePadding;
			Vector2 powerOfTwoTextureSize = this.GetPowerOfTwoTextureSize(texture);
			return PowerOfTwoTextureAtlas.GetPayloadScaleOffset(powerOfTwoTextureSize, vector, scaleOffset);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00014AA8 File Offset: 0x00012CA8
		public static Vector4 GetPayloadScaleOffset(in Vector2 textureSize, in Vector2 paddingSize, in Vector4 scaleOffset)
		{
			Vector2 a = new Vector2(scaleOffset.x, scaleOffset.y);
			Vector2 a2 = new Vector2(scaleOffset.z, scaleOffset.w);
			Vector2 b = (textureSize + paddingSize) / textureSize;
			Vector2 b2 = paddingSize / 2f / (textureSize + paddingSize);
			Vector2 vector = a / b;
			Vector2 vector2 = a2 + a * b2;
			return new Vector4(vector.x, vector.y, vector2.x, vector2.y);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00014B54 File Offset: 0x00012D54
		private void Blit2DTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips, PowerOfTwoTextureAtlas.BlitType blitType)
		{
			int num = base.GetTextureMipmapCount(texture.width, texture.height);
			int texturePadding = this.GetTexturePadding();
			Vector2 powerOfTwoTextureSize = this.GetPowerOfTwoTextureSize(texture);
			bool bilinear = texture.filterMode > FilterMode.Point;
			if (!blitMips)
			{
				num = 1;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<CoreProfileId>(CoreProfileId.BlitTextureInPotAtlas)))
			{
				for (int i = 0; i < num; i++)
				{
					cmd.SetRenderTarget(this.m_AtlasTexture, i);
					switch (blitType)
					{
					case PowerOfTwoTextureAtlas.BlitType.Padding:
						Blitter.BlitQuadWithPadding(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, bilinear, texturePadding);
						break;
					case PowerOfTwoTextureAtlas.BlitType.PaddingMultiply:
						Blitter.BlitQuadWithPaddingMultiply(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, bilinear, texturePadding);
						break;
					case PowerOfTwoTextureAtlas.BlitType.OctahedralPadding:
						Blitter.BlitOctahedralWithPadding(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, bilinear, texturePadding);
						break;
					case PowerOfTwoTextureAtlas.BlitType.OctahedralPaddingMultiply:
						Blitter.BlitOctahedralWithPaddingMultiply(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, bilinear, texturePadding);
						break;
					}
				}
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00014C40 File Offset: 0x00012E40
		public override void BlitTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.Padding);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00014C70 File Offset: 0x00012E70
		public void BlitTextureMultiply(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.PaddingMultiply);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00014CA0 File Offset: 0x00012EA0
		public override void BlitOctahedralTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.OctahedralPadding);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00014CD0 File Offset: 0x00012ED0
		public void BlitOctahedralTextureMultiply(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.OctahedralPaddingMultiply);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00014D00 File Offset: 0x00012F00
		private void TextureSizeToPowerOfTwo(Texture texture, ref int width, ref int height)
		{
			width = Mathf.NextPowerOfTwo(width);
			height = Mathf.NextPowerOfTwo(height);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00014D14 File Offset: 0x00012F14
		private Vector2 GetPowerOfTwoTextureSize(Texture texture)
		{
			int width = texture.width;
			int height = texture.height;
			this.TextureSizeToPowerOfTwo(texture, ref width, ref height);
			return new Vector2((float)width, (float)height);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00014D44 File Offset: 0x00012F44
		public override bool AllocateTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			if (height != width)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Can't place ",
					(texture != null) ? texture.ToString() : null,
					" in the atlas ",
					this.m_AtlasTexture.name,
					": Only squared texture are allowed in this atlas."
				}));
				return false;
			}
			this.TextureSizeToPowerOfTwo(texture, ref height, ref width);
			return base.AllocateTexture(cmd, ref scaleOffset, texture, width, height, -1);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00014DB8 File Offset: 0x00012FB8
		public void ResetRequestedTexture()
		{
			this.m_RequestedTextures.Clear();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00014DC5 File Offset: 0x00012FC5
		public bool ReserveSpace(Texture texture)
		{
			return this.ReserveSpace(texture, texture.width, texture.height);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00014DDA File Offset: 0x00012FDA
		public bool ReserveSpace(Texture texture, int width, int height)
		{
			return this.ReserveSpace(base.GetTextureID(texture), width, height);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00014DEB File Offset: 0x00012FEB
		public bool ReserveSpace(Texture textureA, Texture textureB, int width, int height)
		{
			return this.ReserveSpace(base.GetTextureID(textureA, textureB), width, height);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00014E00 File Offset: 0x00013000
		private bool ReserveSpace(int id, int width, int height)
		{
			this.m_RequestedTextures[id] = new Vector2Int(width, height);
			Vector2Int cachedTextureSize = base.GetCachedTextureSize(id);
			Vector4 vector;
			if (!base.IsCached(out vector, id) || cachedTextureSize.x != width || cachedTextureSize.y != height)
			{
				Vector4 zero = Vector4.zero;
				if (!this.AllocateTextureWithoutBlit(id, width, height, ref zero))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00014E60 File Offset: 0x00013060
		public bool RelayoutEntries()
		{
			List<ValueTuple<int, Vector2Int>> list = new List<ValueTuple<int, Vector2Int>>();
			foreach (KeyValuePair<int, Vector2Int> keyValuePair in this.m_RequestedTextures)
			{
				list.Add(new ValueTuple<int, Vector2Int>(keyValuePair.Key, keyValuePair.Value));
			}
			base.ResetAllocator();
			list.Sort(([TupleElementNames(new string[]
			{
				"instanceId",
				"size"
			})] ValueTuple<int, Vector2Int> c1, [TupleElementNames(new string[]
			{
				"instanceId",
				"size"
			})] ValueTuple<int, Vector2Int> c2) => c2.Item2.magnitude.CompareTo(c1.Item2.magnitude));
			bool flag = true;
			Vector4 zero = Vector4.zero;
			foreach (ValueTuple<int, Vector2Int> valueTuple in list)
			{
				bool flag2 = flag;
				int item = valueTuple.Item1;
				Vector2Int item2 = valueTuple.Item2;
				int x = item2.x;
				item2 = valueTuple.Item2;
				flag = (flag2 & this.AllocateTextureWithoutBlit(item, x, item2.y, ref zero));
			}
			return flag;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00014F6C File Offset: 0x0001316C
		public static long GetApproxCacheSizeInByte(int nbElement, int resolution, bool hasMipmap, GraphicsFormat format)
		{
			return (long)((double)(nbElement * resolution * resolution) * (double)((hasMipmap ? 1.33f : 1f) * GraphicsFormatUtility.GetBlockSize(format)));
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00014F90 File Offset: 0x00013190
		public static int GetMaxCacheSizeForWeightInByte(int weight, bool hasMipmap, GraphicsFormat format)
		{
			float num = GraphicsFormatUtility.GetBlockSize(format) * (hasMipmap ? 1.33f : 1f);
			return CoreUtils.PreviousPowerOfTwo((int)Mathf.Sqrt((float)weight / num));
		}

		// Token: 0x040002F2 RID: 754
		private readonly int m_MipPadding;

		// Token: 0x040002F3 RID: 755
		private const float k_MipmapFactorApprox = 1.33f;

		// Token: 0x040002F4 RID: 756
		private Dictionary<int, Vector2Int> m_RequestedTextures = new Dictionary<int, Vector2Int>();

		// Token: 0x0200016B RID: 363
		private enum BlitType
		{
			// Token: 0x04000571 RID: 1393
			Padding,
			// Token: 0x04000572 RID: 1394
			PaddingMultiply,
			// Token: 0x04000573 RID: 1395
			OctahedralPadding,
			// Token: 0x04000574 RID: 1396
			OctahedralPaddingMultiply
		}

		// Token: 0x0200016C RID: 364
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060008ED RID: 2285 RVA: 0x00023C39 File Offset: 0x00021E39
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060008EE RID: 2286 RVA: 0x00023C45 File Offset: 0x00021E45
			public <>c()
			{
			}

			// Token: 0x060008EF RID: 2287 RVA: 0x00023C50 File Offset: 0x00021E50
			internal int <RelayoutEntries>b__23_0([TupleElementNames(new string[]
			{
				"instanceId",
				"size"
			})] ValueTuple<int, Vector2Int> c1, [TupleElementNames(new string[]
			{
				"instanceId",
				"size"
			})] ValueTuple<int, Vector2Int> c2)
			{
				return c2.Item2.magnitude.CompareTo(c1.Item2.magnitude);
			}

			// Token: 0x04000575 RID: 1397
			public static readonly PowerOfTwoTextureAtlas.<>c <>9 = new PowerOfTwoTextureAtlas.<>c();

			// Token: 0x04000576 RID: 1398
			[TupleElementNames(new string[]
			{
				"instanceId",
				"size"
			})]
			public static Comparison<ValueTuple<int, Vector2Int>> <>9__23_0;
		}
	}
}
