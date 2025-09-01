using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000007 RID: 7
	internal class ProbeBrickPool
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003190 File Offset: 0x00001390
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00003198 File Offset: 0x00001398
		internal int estimatedVMemCost
		{
			[CompilerGenerated]
			get
			{
				return this.<estimatedVMemCost>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<estimatedVMemCost>k__BackingField = value;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000031A4 File Offset: 0x000013A4
		internal ProbeBrickPool(int allocationSize, ProbeVolumeTextureMemoryBudget memoryBudget, ProbeVolumeSHBands shBands)
		{
			this.m_NextFreeChunk.x = (this.m_NextFreeChunk.y = (this.m_NextFreeChunk.z = 0));
			this.m_AllocationSize = allocationSize;
			this.m_MemoryBudget = memoryBudget;
			this.m_SHBands = shBands;
			this.m_FreeList = new Stack<ProbeBrickPool.BrickChunkAlloc>(256);
			int num;
			int num2;
			int num3;
			this.DerivePoolSizeFromBudget(allocationSize, memoryBudget, out num, out num2, out num3);
			int estimatedVMemCost = 0;
			this.m_Pool = ProbeBrickPool.CreateDataLocation(num * num2 * num3, false, shBands, out estimatedVMemCost);
			this.estimatedVMemCost = estimatedVMemCost;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003234 File Offset: 0x00001434
		internal void EnsureTextureValidity()
		{
			if (this.m_Pool.TexL0_L1rx == null)
			{
				this.m_Pool.Cleanup();
				int estimatedVMemCost = 0;
				this.m_Pool = ProbeBrickPool.CreateDataLocation(this.m_Pool.width * this.m_Pool.height * this.m_Pool.depth, false, this.m_SHBands, out estimatedVMemCost);
				this.estimatedVMemCost = estimatedVMemCost;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000329F File Offset: 0x0000149F
		internal int GetChunkSize()
		{
			return this.m_AllocationSize;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000032A7 File Offset: 0x000014A7
		internal int GetChunkSizeInProbeCount()
		{
			return this.m_AllocationSize * 64;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000032B2 File Offset: 0x000014B2
		internal int GetPoolWidth()
		{
			return this.m_Pool.width;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000032BF File Offset: 0x000014BF
		internal int GetPoolHeight()
		{
			return this.m_Pool.height;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000032CC File Offset: 0x000014CC
		internal Vector3Int GetPoolDimensions()
		{
			return new Vector3Int(this.m_Pool.width, this.m_Pool.height, this.m_Pool.depth);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000032F4 File Offset: 0x000014F4
		internal void GetRuntimeResources(ref ProbeReferenceVolume.RuntimeResources rr)
		{
			rr.L0_L1rx = this.m_Pool.TexL0_L1rx;
			rr.L1_G_ry = this.m_Pool.TexL1_G_ry;
			rr.L1_B_rz = this.m_Pool.TexL1_B_rz;
			rr.L2_0 = this.m_Pool.TexL2_0;
			rr.L2_1 = this.m_Pool.TexL2_1;
			rr.L2_2 = this.m_Pool.TexL2_2;
			rr.L2_3 = this.m_Pool.TexL2_3;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003378 File Offset: 0x00001578
		internal void Clear()
		{
			this.m_FreeList.Clear();
			this.m_NextFreeChunk.x = (this.m_NextFreeChunk.y = (this.m_NextFreeChunk.z = 0));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000033B8 File Offset: 0x000015B8
		internal void Allocate(int numberOfBrickChunks, List<ProbeBrickPool.BrickChunkAlloc> outAllocations)
		{
			while (this.m_FreeList.Count > 0 && numberOfBrickChunks > 0)
			{
				outAllocations.Add(this.m_FreeList.Pop());
				numberOfBrickChunks--;
			}
			uint num = 0U;
			while ((ulong)num < (ulong)((long)numberOfBrickChunks) && this.m_NextFreeChunk.z < this.m_Pool.depth)
			{
				outAllocations.Add(this.m_NextFreeChunk);
				this.m_NextFreeChunk.x = this.m_NextFreeChunk.x + this.m_AllocationSize * 4;
				if (this.m_NextFreeChunk.x >= this.m_Pool.width)
				{
					this.m_NextFreeChunk.x = 0;
					this.m_NextFreeChunk.y = this.m_NextFreeChunk.y + 4;
					if (this.m_NextFreeChunk.y >= this.m_Pool.height)
					{
						this.m_NextFreeChunk.y = 0;
						this.m_NextFreeChunk.z = this.m_NextFreeChunk.z + 4;
					}
				}
				num += 1U;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000034AC File Offset: 0x000016AC
		internal void Deallocate(List<ProbeBrickPool.BrickChunkAlloc> allocations)
		{
			foreach (ProbeBrickPool.BrickChunkAlloc item in allocations)
			{
				this.m_FreeList.Push(item);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003500 File Offset: 0x00001700
		internal void Update(ProbeBrickPool.DataLocation source, List<ProbeBrickPool.BrickChunkAlloc> srcLocations, List<ProbeBrickPool.BrickChunkAlloc> dstLocations, ProbeVolumeSHBands bands)
		{
			for (int i = 0; i < srcLocations.Count; i++)
			{
				ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = srcLocations[i];
				ProbeBrickPool.BrickChunkAlloc brickChunkAlloc2 = dstLocations[i];
				for (int j = 0; j < 4; j++)
				{
					int srcWidth = Mathf.Min(this.m_AllocationSize * 4, source.width - brickChunkAlloc.x);
					Graphics.CopyTexture(source.TexL0_L1rx, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, this.m_Pool.TexL0_L1rx, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					Graphics.CopyTexture(source.TexL1_G_ry, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, this.m_Pool.TexL1_G_ry, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					Graphics.CopyTexture(source.TexL1_B_rz, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, this.m_Pool.TexL1_B_rz, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
					{
						Graphics.CopyTexture(source.TexL2_0, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, this.m_Pool.TexL2_0, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
						Graphics.CopyTexture(source.TexL2_1, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, this.m_Pool.TexL2_1, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
						Graphics.CopyTexture(source.TexL2_2, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, this.m_Pool.TexL2_2, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
						Graphics.CopyTexture(source.TexL2_3, brickChunkAlloc.z + j, 0, brickChunkAlloc.x, brickChunkAlloc.y, srcWidth, 4, this.m_Pool.TexL2_3, brickChunkAlloc2.z + j, 0, brickChunkAlloc2.x, brickChunkAlloc2.y);
					}
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003740 File Offset: 0x00001940
		private static Vector3Int ProbeCountToDataLocSize(int numProbes)
		{
			int num = numProbes / 64;
			int num2 = 512;
			int num3 = (num + num2 * num2 - 1) / (num2 * num2);
			int num5;
			int num4;
			if (num3 > 1)
			{
				num4 = (num5 = num2);
			}
			else
			{
				num4 = (num + num2 - 1) / num2;
				if (num4 > 1)
				{
					num5 = num2;
				}
				else
				{
					num5 = num;
				}
			}
			num5 *= 4;
			num4 *= 4;
			num3 *= 4;
			return new Vector3Int(num5, num4, num3);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000379C File Offset: 0x0000199C
		public static ProbeBrickPool.DataLocation CreateDataLocation(int numProbes, bool compressed, ProbeVolumeSHBands bands, out int allocatedBytes)
		{
			Vector3Int vector3Int = ProbeBrickPool.ProbeCountToDataLocSize(numProbes);
			int x = vector3Int.x;
			int y = vector3Int.y;
			int z = vector3Int.z;
			int num = x * y * z;
			allocatedBytes = 0;
			ProbeBrickPool.DataLocation result;
			result.TexL0_L1rx = new Texture3D(x, y, z, GraphicsFormat.R16G16B16A16_SFloat, TextureCreationFlags.None, 1);
			allocatedBytes += num * 8;
			result.TexL1_G_ry = new Texture3D(x, y, z, compressed ? GraphicsFormat.RGBA_BC7_UNorm : GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.None, 1);
			allocatedBytes += num * (compressed ? 1 : 4);
			result.TexL1_B_rz = new Texture3D(x, y, z, compressed ? GraphicsFormat.RGBA_BC7_UNorm : GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.None, 1);
			allocatedBytes += num * (compressed ? 1 : 4);
			if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				result.TexL2_0 = new Texture3D(x, y, z, compressed ? GraphicsFormat.RGBA_BC7_UNorm : GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.None, 1);
				allocatedBytes += num * (compressed ? 1 : 4);
				result.TexL2_1 = new Texture3D(x, y, z, compressed ? GraphicsFormat.RGBA_BC7_UNorm : GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.None, 1);
				allocatedBytes += num * (compressed ? 1 : 4);
				result.TexL2_2 = new Texture3D(x, y, z, compressed ? GraphicsFormat.RGBA_BC7_UNorm : GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.None, 1);
				allocatedBytes += num * (compressed ? 1 : 4);
				result.TexL2_3 = new Texture3D(x, y, z, compressed ? GraphicsFormat.RGBA_BC7_UNorm : GraphicsFormat.R8G8B8A8_UNorm, TextureCreationFlags.None, 1);
				allocatedBytes += num * (compressed ? 1 : 4);
			}
			else
			{
				result.TexL2_0 = null;
				result.TexL2_1 = null;
				result.TexL2_2 = null;
				result.TexL2_3 = null;
			}
			result.width = x;
			result.height = y;
			result.depth = z;
			return result;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003924 File Offset: 0x00001B24
		private static void SetPixel(ref Color[] data, int x, int y, int z, int dataLocWidth, int dataLocHeight, Color value)
		{
			int num = x + dataLocWidth * (y + dataLocHeight * z);
			data[num] = value;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003948 File Offset: 0x00001B48
		public static void FillDataLocation(ref ProbeBrickPool.DataLocation loc, SphericalHarmonicsL2[] shl2, ProbeVolumeSHBands bands)
		{
			int num = shl2.Length / 64;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			Color value = default(Color);
			Color[] pixels = new Color[loc.width * loc.height * loc.depth * 2];
			Color[] pixels2 = new Color[loc.width * loc.height * loc.depth * 2];
			Color[] pixels3 = new Color[loc.width * loc.height * loc.depth * 2];
			Color[] pixels4 = null;
			Color[] pixels5 = null;
			Color[] pixels6 = null;
			Color[] pixels7 = null;
			if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				pixels4 = new Color[loc.width * loc.height * loc.depth];
				pixels5 = new Color[loc.width * loc.height * loc.depth];
				pixels6 = new Color[loc.width * loc.height * loc.depth];
				pixels7 = new Color[loc.width * loc.height * loc.depth];
			}
			for (int i = 0; i < shl2.Length; i += 64)
			{
				for (int j = 0; j < 4; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						for (int l = 0; l < 4; l++)
						{
							int x = num3 + l;
							int y = num4 + k;
							int z = num5 + j;
							value.r = shl2[num2][0, 0];
							value.g = shl2[num2][1, 0];
							value.b = shl2[num2][2, 0];
							value.a = shl2[num2][0, 1];
							ProbeBrickPool.SetPixel(ref pixels, x, y, z, loc.width, loc.height, value);
							value.r = shl2[num2][1, 1];
							value.g = shl2[num2][1, 2];
							value.b = shl2[num2][1, 3];
							value.a = shl2[num2][0, 2];
							ProbeBrickPool.SetPixel(ref pixels2, x, y, z, loc.width, loc.height, value);
							value.r = shl2[num2][2, 1];
							value.g = shl2[num2][2, 2];
							value.b = shl2[num2][2, 3];
							value.a = shl2[num2][0, 3];
							ProbeBrickPool.SetPixel(ref pixels3, x, y, z, loc.width, loc.height, value);
							if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
							{
								value.r = shl2[num2][0, 4];
								value.g = shl2[num2][0, 5];
								value.b = shl2[num2][0, 6];
								value.a = shl2[num2][0, 7];
								ProbeBrickPool.SetPixel(ref pixels4, x, y, z, loc.width, loc.height, value);
								value.r = shl2[num2][1, 4];
								value.g = shl2[num2][1, 5];
								value.b = shl2[num2][1, 6];
								value.a = shl2[num2][1, 7];
								ProbeBrickPool.SetPixel(ref pixels5, x, y, z, loc.width, loc.height, value);
								value.r = shl2[num2][2, 4];
								value.g = shl2[num2][2, 5];
								value.b = shl2[num2][2, 6];
								value.a = shl2[num2][2, 7];
								ProbeBrickPool.SetPixel(ref pixels6, x, y, z, loc.width, loc.height, value);
								value.r = shl2[num2][0, 8];
								value.g = shl2[num2][1, 8];
								value.b = shl2[num2][2, 8];
								value.a = 1f;
								ProbeBrickPool.SetPixel(ref pixels7, x, y, z, loc.width, loc.height, value);
							}
							num2++;
						}
					}
				}
				num3 += 4;
				if (num3 >= loc.width)
				{
					num3 = 0;
					num4 += 4;
					if (num4 >= loc.height)
					{
						num4 = 0;
						num5 += 4;
					}
				}
			}
			loc.TexL0_L1rx.SetPixels(pixels);
			loc.TexL0_L1rx.Apply(false);
			loc.TexL1_G_ry.SetPixels(pixels2);
			loc.TexL1_G_ry.Apply(false);
			loc.TexL1_B_rz.SetPixels(pixels3);
			loc.TexL1_B_rz.Apply(false);
			if (bands == ProbeVolumeSHBands.SphericalHarmonicsL2)
			{
				loc.TexL2_0.SetPixels(pixels4);
				loc.TexL2_0.Apply(false);
				loc.TexL2_1.SetPixels(pixels5);
				loc.TexL2_1.Apply(false);
				loc.TexL2_2.SetPixels(pixels6);
				loc.TexL2_2.Apply(false);
				loc.TexL2_3.SetPixels(pixels7);
				loc.TexL2_3.Apply(false);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003E8C File Offset: 0x0000208C
		private void DerivePoolSizeFromBudget(int allocationSize, ProbeVolumeTextureMemoryBudget memoryBudget, out int width, out int height, out int depth)
		{
			width = (int)memoryBudget;
			height = (int)memoryBudget;
			depth = 4;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003E99 File Offset: 0x00002099
		internal void Cleanup()
		{
			this.m_Pool.Cleanup();
		}

		// Token: 0x04000018 RID: 24
		internal const int kBrickCellCount = 3;

		// Token: 0x04000019 RID: 25
		internal const int kBrickProbeCountPerDim = 4;

		// Token: 0x0400001A RID: 26
		internal const int kBrickProbeCountTotal = 64;

		// Token: 0x0400001B RID: 27
		[CompilerGenerated]
		private int <estimatedVMemCost>k__BackingField;

		// Token: 0x0400001C RID: 28
		private const int kMaxPoolWidth = 2048;

		// Token: 0x0400001D RID: 29
		private int m_AllocationSize;

		// Token: 0x0400001E RID: 30
		private ProbeVolumeTextureMemoryBudget m_MemoryBudget;

		// Token: 0x0400001F RID: 31
		private ProbeBrickPool.DataLocation m_Pool;

		// Token: 0x04000020 RID: 32
		private ProbeBrickPool.BrickChunkAlloc m_NextFreeChunk;

		// Token: 0x04000021 RID: 33
		private Stack<ProbeBrickPool.BrickChunkAlloc> m_FreeList;

		// Token: 0x04000022 RID: 34
		private ProbeVolumeSHBands m_SHBands;

		// Token: 0x02000112 RID: 274
		[DebuggerDisplay("Chunk ({x}, {y}, {z})")]
		public struct BrickChunkAlloc
		{
			// Token: 0x060007EB RID: 2027 RVA: 0x0002202C File Offset: 0x0002022C
			internal int flattenIndex(int sx, int sy)
			{
				return this.z * (sx * sy) + this.y * sx + this.x;
			}

			// Token: 0x04000472 RID: 1138
			public int x;

			// Token: 0x04000473 RID: 1139
			public int y;

			// Token: 0x04000474 RID: 1140
			public int z;
		}

		// Token: 0x02000113 RID: 275
		public struct DataLocation
		{
			// Token: 0x060007EC RID: 2028 RVA: 0x00022048 File Offset: 0x00020248
			internal void Cleanup()
			{
				CoreUtils.Destroy(this.TexL0_L1rx);
				CoreUtils.Destroy(this.TexL1_G_ry);
				CoreUtils.Destroy(this.TexL1_B_rz);
				CoreUtils.Destroy(this.TexL2_0);
				CoreUtils.Destroy(this.TexL2_1);
				CoreUtils.Destroy(this.TexL2_2);
				CoreUtils.Destroy(this.TexL2_3);
				this.TexL0_L1rx = null;
				this.TexL1_G_ry = null;
				this.TexL1_B_rz = null;
				this.TexL2_0 = null;
				this.TexL2_1 = null;
				this.TexL2_2 = null;
				this.TexL2_3 = null;
			}

			// Token: 0x04000475 RID: 1141
			internal Texture3D TexL0_L1rx;

			// Token: 0x04000476 RID: 1142
			internal Texture3D TexL1_G_ry;

			// Token: 0x04000477 RID: 1143
			internal Texture3D TexL1_B_rz;

			// Token: 0x04000478 RID: 1144
			internal Texture3D TexL2_0;

			// Token: 0x04000479 RID: 1145
			internal Texture3D TexL2_1;

			// Token: 0x0400047A RID: 1146
			internal Texture3D TexL2_2;

			// Token: 0x0400047B RID: 1147
			internal Texture3D TexL2_3;

			// Token: 0x0400047C RID: 1148
			internal int width;

			// Token: 0x0400047D RID: 1149
			internal int height;

			// Token: 0x0400047E RID: 1150
			internal int depth;
		}
	}
}
