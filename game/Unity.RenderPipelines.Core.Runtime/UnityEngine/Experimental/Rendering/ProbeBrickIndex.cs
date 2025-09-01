using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000006 RID: 6
	internal class ProbeBrickIndex
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000025F7 File Offset: 0x000007F7
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000025FF File Offset: 0x000007FF
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

		// Token: 0x0600001F RID: 31 RVA: 0x00002608 File Offset: 0x00000808
		private int GetVoxelSubdivLevel()
		{
			return Mathf.Min(3, ProbeReferenceVolume.instance.GetMaxSubdivision() - 1);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000261C File Offset: 0x0000081C
		private int SizeOfPhysicalIndexFromBudget(ProbeVolumeTextureMemoryBudget memoryBudget)
		{
			if (memoryBudget == ProbeVolumeTextureMemoryBudget.MemoryBudgetLow)
			{
				return 16000000;
			}
			if (memoryBudget == ProbeVolumeTextureMemoryBudget.MemoryBudgetMedium)
			{
				return 32000000;
			}
			if (memoryBudget != ProbeVolumeTextureMemoryBudget.MemoryBudgetHigh)
			{
				return 32000000;
			}
			return 64000000;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002650 File Offset: 0x00000850
		internal ProbeBrickIndex(ProbeVolumeTextureMemoryBudget memoryBudget)
		{
			this.m_CenterRS = new Vector3Int(0, 0, 0);
			this.m_VoxelToBricks = new Dictionary<Vector3Int, List<ProbeBrickIndex.VoxelMeta>>();
			this.m_BricksToVoxels = new Dictionary<ProbeReferenceVolume.RegId, ProbeBrickIndex.BrickMeta>();
			this.m_NeedUpdateIndexComputeBuffer = false;
			this.m_IndexInChunks = Mathf.CeilToInt((float)this.SizeOfPhysicalIndexFromBudget(memoryBudget) / 243f);
			this.m_IndexChunks = new BitArray(Mathf.Max(1, this.m_IndexInChunks));
			int num = this.m_IndexInChunks * 243;
			this.m_PhysicalIndexBufferData = new int[num];
			this.m_PhysicalIndexBuffer = new ComputeBuffer(num, 4, ComputeBufferType.Structured);
			this.m_NextFreeChunk = 0;
			this.estimatedVMemCost = num * 4;
			this.Clear();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026FC File Offset: 0x000008FC
		internal void UploadIndexData()
		{
			this.m_PhysicalIndexBuffer.SetData(this.m_PhysicalIndexBufferData);
			this.m_NeedUpdateIndexComputeBuffer = false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002718 File Offset: 0x00000918
		internal void Clear()
		{
			for (int i = 0; i < this.m_PhysicalIndexBufferData.Length; i++)
			{
				this.m_PhysicalIndexBufferData[i] = -1;
			}
			this.m_NeedUpdateIndexComputeBuffer = true;
			this.m_NextFreeChunk = 0;
			this.m_IndexChunks.SetAll(false);
			this.m_VoxelToBricks.Clear();
			this.m_BricksToVoxels.Clear();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002774 File Offset: 0x00000974
		private void MapBrickToVoxels(ProbeBrickIndex.Brick brick, HashSet<Vector3Int> voxels)
		{
			int subdivisionLevel = brick.subdivisionLevel;
			int num = (int)Mathf.Pow(3f, (float)Mathf.Max(0, subdivisionLevel - this.GetVoxelSubdivLevel()));
			Vector3Int position = brick.position;
			int num2 = ProbeReferenceVolume.CellSize(brick.subdivisionLevel);
			int num3 = ProbeReferenceVolume.CellSize(this.GetVoxelSubdivLevel());
			if (num <= 1)
			{
				Vector3 vector = brick.position;
				vector *= 1f / (float)num3;
				position = new Vector3Int(Mathf.FloorToInt(vector.x) * num3, Mathf.FloorToInt(vector.y) * num3, Mathf.FloorToInt(vector.z) * num3);
			}
			for (int i = position.z; i < position.z + num2; i += num3)
			{
				for (int j = position.y; j < position.y + num2; j += num3)
				{
					for (int k = position.x; k < position.x + num2; k += num3)
					{
						voxels.Add(new Vector3Int(k, j, i));
					}
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002880 File Offset: 0x00000A80
		private void ClearVoxel(Vector3Int pos, ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
		{
			Vector3Int brickMin;
			Vector3Int brickMax;
			this.ClipToIndexSpace(pos, this.GetVoxelSubdivLevel(), out brickMin, out brickMax, cellInfo);
			this.UpdatePhysicalIndex(brickMin, brickMax, -1, cellInfo);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028A9 File Offset: 0x00000AA9
		internal void GetRuntimeResources(ref ProbeReferenceVolume.RuntimeResources rr)
		{
			if (this.m_NeedUpdateIndexComputeBuffer)
			{
				this.UploadIndexData();
			}
			rr.index = this.m_PhysicalIndexBuffer;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000028C5 File Offset: 0x00000AC5
		internal void Cleanup()
		{
			CoreUtils.SafeRelease(this.m_PhysicalIndexBuffer);
			this.m_PhysicalIndexBuffer = null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028D9 File Offset: 0x00000AD9
		private int MergeIndex(int index, int size)
		{
			return (index & -1879048193) | (size & 7) << 28;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028EC File Offset: 0x00000AEC
		internal bool AssignIndexChunksToCell(ProbeReferenceVolume.Cell cell, int bricksCount, ref ProbeBrickIndex.CellIndexUpdateInfo cellUpdateInfo)
		{
			int num = Mathf.CeilToInt((float)bricksCount / 243f);
			int num2 = -1;
			for (int i = 0; i < this.m_IndexInChunks; i++)
			{
				if (!this.m_IndexChunks[i] && i + num < this.m_IndexInChunks)
				{
					int num3 = 0;
					int num4 = i;
					while (num4 < i + num && !this.m_IndexChunks[num4])
					{
						num3++;
						num4++;
					}
					if (num3 == num)
					{
						num2 = i;
						break;
					}
				}
			}
			if (num2 < 0)
			{
				return false;
			}
			cellUpdateInfo.firstChunkIndex = num2;
			cellUpdateInfo.numberOfChunks = num;
			for (int j = num2; j < num2 + num; j++)
			{
				this.m_IndexChunks[j] = true;
			}
			this.m_NextFreeChunk += Mathf.Max(0, num2 + num - this.m_NextFreeChunk);
			return true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029B4 File Offset: 0x00000BB4
		public void AddBricks(ProbeReferenceVolume.RegId id, List<ProbeBrickIndex.Brick> bricks, List<ProbeBrickPool.BrickChunkAlloc> allocations, int allocationSize, int poolWidth, int poolHeight, ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
		{
			int a = ProbeReferenceVolume.CellSize(7);
			ProbeBrickIndex.BrickMeta brickMeta = default(ProbeBrickIndex.BrickMeta);
			brickMeta.voxels = new HashSet<Vector3Int>();
			brickMeta.bricks = new List<ProbeBrickIndex.ReservedBrick>(bricks.Count);
			this.m_BricksToVoxels.Add(id, brickMeta);
			int num = 0;
			Predicate<ProbeBrickIndex.VoxelMeta> <>9__0;
			for (int i = 0; i < allocations.Count; i++)
			{
				ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = allocations[i];
				int num2 = Mathf.Min(allocationSize, bricks.Count - num);
				int j = 0;
				while (j < num2)
				{
					ProbeBrickIndex.Brick brick = bricks[num];
					int b = ProbeReferenceVolume.CellSize(brick.subdivisionLevel);
					a = Mathf.Min(a, b);
					this.MapBrickToVoxels(brick, brickMeta.voxels);
					ProbeBrickIndex.ReservedBrick item = default(ProbeBrickIndex.ReservedBrick);
					item.brick = brick;
					item.flattenedIdx = this.MergeIndex(brickChunkAlloc.flattenIndex(poolWidth, poolHeight), brick.subdivisionLevel);
					brickMeta.bricks.Add(item);
					foreach (Vector3Int key in brickMeta.voxels)
					{
						List<ProbeBrickIndex.VoxelMeta> list;
						if (!this.m_VoxelToBricks.TryGetValue(key, out list))
						{
							list = new List<ProbeBrickIndex.VoxelMeta>(1);
							this.m_VoxelToBricks.Add(key, list);
						}
						List<ProbeBrickIndex.VoxelMeta> list2 = list;
						Predicate<ProbeBrickIndex.VoxelMeta> match;
						if ((match = <>9__0) == null)
						{
							match = (<>9__0 = ((ProbeBrickIndex.VoxelMeta lhs) => lhs.id == id));
						}
						int num3 = list2.FindIndex(match);
						ProbeBrickIndex.VoxelMeta voxelMeta;
						if (num3 == -1)
						{
							voxelMeta.id = id;
							voxelMeta.brickIndices = new List<ushort>(4);
							list.Add(voxelMeta);
						}
						else
						{
							voxelMeta = list[num3];
						}
						voxelMeta.brickIndices.Add((ushort)num);
					}
					j++;
					num++;
					brickChunkAlloc.x += 4;
				}
			}
			foreach (Vector3Int voxel in brickMeta.voxels)
			{
				this.UpdateIndexForVoxel(voxel, cellInfo);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002BFC File Offset: 0x00000DFC
		public void RemoveBricks(ProbeReferenceVolume.RegId id, ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
		{
			if (!this.m_BricksToVoxels.ContainsKey(id))
			{
				return;
			}
			Predicate<ProbeBrickIndex.VoxelMeta> <>9__0;
			foreach (Vector3Int vector3Int in this.m_BricksToVoxels[id].voxels)
			{
				List<ProbeBrickIndex.VoxelMeta> list = this.m_VoxelToBricks[vector3Int];
				List<ProbeBrickIndex.VoxelMeta> list2 = list;
				Predicate<ProbeBrickIndex.VoxelMeta> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((ProbeBrickIndex.VoxelMeta lhs) => lhs.id == id));
				}
				int num = list2.FindIndex(match);
				if (num >= 0)
				{
					list.RemoveAt(num);
					if (list.Count > 0)
					{
						this.UpdateIndexForVoxel(vector3Int, cellInfo);
					}
					else
					{
						this.ClearVoxel(vector3Int, cellInfo);
						this.m_VoxelToBricks.Remove(vector3Int);
					}
				}
			}
			this.m_BricksToVoxels.Remove(id);
			for (int i = cellInfo.firstChunkIndex; i < cellInfo.firstChunkIndex + cellInfo.numberOfChunks; i++)
			{
				this.m_IndexChunks[i] = false;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D28 File Offset: 0x00000F28
		private void UpdateIndexForVoxel(Vector3Int voxel, ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
		{
			this.ClearVoxel(voxel, cellInfo);
			foreach (ProbeBrickIndex.VoxelMeta voxelMeta in this.m_VoxelToBricks[voxel])
			{
				List<ProbeBrickIndex.ReservedBrick> bricks = this.m_BricksToVoxels[voxelMeta.id].bricks;
				List<ushort> brickIndices = voxelMeta.brickIndices;
				this.UpdateIndexForVoxel(voxel, bricks, brickIndices, cellInfo);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002DAC File Offset: 0x00000FAC
		private void UpdatePhysicalIndex(Vector3Int brickMin, Vector3Int brickMax, int value, ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
		{
			brickMin -= cellInfo.cellPositionInBricksAtMaxRes;
			brickMax -= cellInfo.cellPositionInBricksAtMaxRes;
			brickMin /= ProbeReferenceVolume.CellSize(cellInfo.minSubdivInCell);
			brickMax /= ProbeReferenceVolume.CellSize(cellInfo.minSubdivInCell);
			ProbeReferenceVolume.CellSize(ProbeReferenceVolume.instance.GetMaxSubdivision() - 1 - cellInfo.minSubdivInCell);
			Vector3Int b = cellInfo.minValidBrickIndexForCellAtMaxRes / ProbeReferenceVolume.CellSize(cellInfo.minSubdivInCell);
			Vector3Int a = cellInfo.maxValidBrickIndexForCellAtMaxResPlusOne / ProbeReferenceVolume.CellSize(cellInfo.minSubdivInCell);
			brickMin -= b;
			brickMax -= b;
			Vector3Int vector3Int = a - b;
			int num = cellInfo.firstChunkIndex * 243;
			for (int i = brickMin.z; i < brickMax.z; i++)
			{
				for (int j = brickMin.y; j < brickMax.y; j++)
				{
					for (int k = brickMin.x; k < brickMax.x; k++)
					{
						int num2 = i * (vector3Int.x * vector3Int.y) + k * vector3Int.y + j;
						int num3 = num + num2;
						this.m_PhysicalIndexBufferData[num3] = value;
					}
				}
			}
			this.m_NeedUpdateIndexComputeBuffer = true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002EFC File Offset: 0x000010FC
		private void ClipToIndexSpace(Vector3Int pos, int subdiv, out Vector3Int outMinpos, out Vector3Int outMaxpos, ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
		{
			int num = ProbeReferenceVolume.CellSize(subdiv);
			Vector3Int vector3Int = cellInfo.cellPositionInBricksAtMaxRes + cellInfo.minValidBrickIndexForCellAtMaxRes;
			Vector3Int vector3Int2 = cellInfo.cellPositionInBricksAtMaxRes + cellInfo.maxValidBrickIndexForCellAtMaxResPlusOne - Vector3Int.one;
			int num2 = pos.x - this.m_CenterRS.x;
			int num3 = pos.y;
			int num4 = pos.z - this.m_CenterRS.z;
			int num5 = num2 + num;
			int num6 = num3 + num;
			int num7 = num4 + num;
			num2 = Mathf.Max(num2, vector3Int.x);
			num3 = Mathf.Max(num3, vector3Int.y);
			num4 = Mathf.Max(num4, vector3Int.z);
			num5 = Mathf.Min(num5, vector3Int2.x);
			num6 = Mathf.Min(num6, vector3Int2.y);
			num7 = Mathf.Min(num7, vector3Int2.z);
			outMinpos = new Vector3Int(num2, num3, num4);
			outMaxpos = new Vector3Int(num5, num6, num7);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003008 File Offset: 0x00001208
		private void UpdateIndexForVoxel(Vector3Int voxel, List<ProbeBrickIndex.ReservedBrick> bricks, List<ushort> indices, ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
		{
			Vector3Int vector3Int;
			Vector3Int vector3Int2;
			this.ClipToIndexSpace(voxel, this.GetVoxelSubdivLevel(), out vector3Int, out vector3Int2, cellInfo);
			foreach (ProbeBrickIndex.ReservedBrick reservedBrick in bricks)
			{
				int b = ProbeReferenceVolume.CellSize(reservedBrick.brick.subdivisionLevel);
				Vector3Int position = reservedBrick.brick.position;
				Vector3Int brickMax = reservedBrick.brick.position + Vector3Int.one * b;
				position.x = Mathf.Max(vector3Int.x, position.x - this.m_CenterRS.x);
				position.y = Mathf.Max(vector3Int.y, position.y);
				position.z = Mathf.Max(vector3Int.z, position.z - this.m_CenterRS.z);
				brickMax.x = Mathf.Min(vector3Int2.x, brickMax.x - this.m_CenterRS.x);
				brickMax.y = Mathf.Min(vector3Int2.y, brickMax.y);
				brickMax.z = Mathf.Min(vector3Int2.z, brickMax.z - this.m_CenterRS.z);
				this.UpdatePhysicalIndex(position, brickMax, reservedBrick.flattenedIdx, cellInfo);
			}
		}

		// Token: 0x0400000C RID: 12
		internal const int kMaxSubdivisionLevels = 7;

		// Token: 0x0400000D RID: 13
		internal const int kIndexChunkSize = 243;

		// Token: 0x0400000E RID: 14
		private BitArray m_IndexChunks;

		// Token: 0x0400000F RID: 15
		private int m_IndexInChunks;

		// Token: 0x04000010 RID: 16
		private int m_NextFreeChunk;

		// Token: 0x04000011 RID: 17
		private ComputeBuffer m_PhysicalIndexBuffer;

		// Token: 0x04000012 RID: 18
		private int[] m_PhysicalIndexBufferData;

		// Token: 0x04000013 RID: 19
		[CompilerGenerated]
		private int <estimatedVMemCost>k__BackingField;

		// Token: 0x04000014 RID: 20
		private Vector3Int m_CenterRS;

		// Token: 0x04000015 RID: 21
		private Dictionary<Vector3Int, List<ProbeBrickIndex.VoxelMeta>> m_VoxelToBricks;

		// Token: 0x04000016 RID: 22
		private Dictionary<ProbeReferenceVolume.RegId, ProbeBrickIndex.BrickMeta> m_BricksToVoxels;

		// Token: 0x04000017 RID: 23
		private bool m_NeedUpdateIndexComputeBuffer;

		// Token: 0x0200010B RID: 267
		[DebuggerDisplay("Brick [{position}, {subdivisionLevel}]")]
		[Serializable]
		public struct Brick : IEquatable<ProbeBrickIndex.Brick>
		{
			// Token: 0x060007E5 RID: 2021 RVA: 0x00021FC1 File Offset: 0x000201C1
			internal Brick(Vector3Int position, int subdivisionLevel)
			{
				this.position = position;
				this.subdivisionLevel = subdivisionLevel;
			}

			// Token: 0x060007E6 RID: 2022 RVA: 0x00021FD1 File Offset: 0x000201D1
			public bool Equals(ProbeBrickIndex.Brick other)
			{
				return this.position == other.position && this.subdivisionLevel == other.subdivisionLevel;
			}

			// Token: 0x04000460 RID: 1120
			public Vector3Int position;

			// Token: 0x04000461 RID: 1121
			public int subdivisionLevel;
		}

		// Token: 0x0200010C RID: 268
		[DebuggerDisplay("Brick [{brick.position}, {brick.subdivisionLevel}], {flattenedIdx}")]
		private struct ReservedBrick
		{
			// Token: 0x04000462 RID: 1122
			public ProbeBrickIndex.Brick brick;

			// Token: 0x04000463 RID: 1123
			public int flattenedIdx;
		}

		// Token: 0x0200010D RID: 269
		private struct VoxelMeta
		{
			// Token: 0x04000464 RID: 1124
			public ProbeReferenceVolume.RegId id;

			// Token: 0x04000465 RID: 1125
			public List<ushort> brickIndices;
		}

		// Token: 0x0200010E RID: 270
		private struct BrickMeta
		{
			// Token: 0x04000466 RID: 1126
			public HashSet<Vector3Int> voxels;

			// Token: 0x04000467 RID: 1127
			public List<ProbeBrickIndex.ReservedBrick> bricks;
		}

		// Token: 0x0200010F RID: 271
		public struct CellIndexUpdateInfo
		{
			// Token: 0x04000468 RID: 1128
			public int firstChunkIndex;

			// Token: 0x04000469 RID: 1129
			public int numberOfChunks;

			// Token: 0x0400046A RID: 1130
			public int minSubdivInCell;

			// Token: 0x0400046B RID: 1131
			public Vector3Int minValidBrickIndexForCellAtMaxRes;

			// Token: 0x0400046C RID: 1132
			public Vector3Int maxValidBrickIndexForCellAtMaxResPlusOne;

			// Token: 0x0400046D RID: 1133
			public Vector3Int cellPositionInBricksAtMaxRes;
		}

		// Token: 0x02000110 RID: 272
		[CompilerGenerated]
		private sealed class <>c__DisplayClass31_0
		{
			// Token: 0x060007E7 RID: 2023 RVA: 0x00021FF6 File Offset: 0x000201F6
			public <>c__DisplayClass31_0()
			{
			}

			// Token: 0x060007E8 RID: 2024 RVA: 0x00021FFE File Offset: 0x000201FE
			internal bool <AddBricks>b__0(ProbeBrickIndex.VoxelMeta lhs)
			{
				return lhs.id == this.id;
			}

			// Token: 0x0400046E RID: 1134
			public ProbeReferenceVolume.RegId id;

			// Token: 0x0400046F RID: 1135
			public Predicate<ProbeBrickIndex.VoxelMeta> <>9__0;
		}

		// Token: 0x02000111 RID: 273
		[CompilerGenerated]
		private sealed class <>c__DisplayClass32_0
		{
			// Token: 0x060007E9 RID: 2025 RVA: 0x00022011 File Offset: 0x00020211
			public <>c__DisplayClass32_0()
			{
			}

			// Token: 0x060007EA RID: 2026 RVA: 0x00022019 File Offset: 0x00020219
			internal bool <RemoveBricks>b__0(ProbeBrickIndex.VoxelMeta lhs)
			{
				return lhs.id == this.id;
			}

			// Token: 0x04000470 RID: 1136
			public ProbeReferenceVolume.RegId id;

			// Token: 0x04000471 RID: 1137
			public Predicate<ProbeBrickIndex.VoxelMeta> <>9__0;
		}
	}
}
