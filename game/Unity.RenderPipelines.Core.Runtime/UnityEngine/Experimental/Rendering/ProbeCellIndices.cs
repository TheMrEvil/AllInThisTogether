using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000008 RID: 8
	internal class ProbeCellIndices
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003EA6 File Offset: 0x000020A6
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00003EAE File Offset: 0x000020AE
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

		// Token: 0x06000046 RID: 70 RVA: 0x00003EB7 File Offset: 0x000020B7
		internal Vector3Int GetCellIndexDimension()
		{
			return this.m_CellCount;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003EBF File Offset: 0x000020BF
		internal Vector3Int GetCellMinPosition()
		{
			return this.m_CellMin;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003EC7 File Offset: 0x000020C7
		private int GetFlatIndex(Vector3Int normalizedPos)
		{
			return normalizedPos.z * (this.m_CellCount.x * this.m_CellCount.y) + normalizedPos.y * this.m_CellCount.x + normalizedPos.x;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003F04 File Offset: 0x00002104
		internal ProbeCellIndices(Vector3Int cellMin, Vector3Int cellMax, int cellSizeInMinBricks)
		{
			Vector3Int cellCount = new Vector3Int(Mathf.Abs(cellMax.x - cellMin.x), Mathf.Abs(cellMax.y - cellMin.y), Mathf.Abs(cellMax.z - cellMin.z));
			this.m_CellCount = cellCount;
			this.m_CellMin = cellMin;
			this.m_CellSizeInMinBricks = cellSizeInMinBricks;
			int num = cellCount.x * cellCount.y * cellCount.z;
			num = ((num == 0) ? 1 : num);
			int num2 = 3 * num;
			this.m_IndexOfIndicesBuffer = new ComputeBuffer(num, 12);
			this.m_IndexOfIndicesData = new uint[num2];
			this.m_NeedUpdateComputeBuffer = false;
			this.estimatedVMemCost = num * 3 * 4;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003FC0 File Offset: 0x000021C0
		internal int GetFlatIdxForCell(Vector3Int cellPosition)
		{
			Vector3Int normalizedPos = cellPosition - this.m_CellMin;
			return this.GetFlatIndex(normalizedPos);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003FE4 File Offset: 0x000021E4
		internal void AddCell(int cellFlatIdx, ProbeBrickIndex.CellIndexUpdateInfo cellUpdateInfo)
		{
			int b = ProbeReferenceVolume.CellSize(cellUpdateInfo.minSubdivInCell);
			ProbeCellIndices.IndexMetaData indexMetaData = default(ProbeCellIndices.IndexMetaData);
			indexMetaData.minSubdiv = cellUpdateInfo.minSubdivInCell;
			indexMetaData.minLocalIdx = cellUpdateInfo.minValidBrickIndexForCellAtMaxRes / b;
			indexMetaData.maxLocalIdx = cellUpdateInfo.maxValidBrickIndexForCellAtMaxResPlusOne / b;
			indexMetaData.firstChunkIndex = cellUpdateInfo.firstChunkIndex;
			uint[] array;
			indexMetaData.Pack(out array);
			for (int i = 0; i < 3; i++)
			{
				this.m_IndexOfIndicesData[cellFlatIdx * 3 + i] = array[i];
			}
			this.m_NeedUpdateComputeBuffer = true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004070 File Offset: 0x00002270
		internal void MarkCellAsUnloaded(int cellFlatIdx)
		{
			for (int i = 0; i < 3; i++)
			{
				this.m_IndexOfIndicesData[cellFlatIdx * 3 + i] = uint.MaxValue;
			}
			this.m_NeedUpdateComputeBuffer = true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000409D File Offset: 0x0000229D
		internal void PushComputeData()
		{
			this.m_IndexOfIndicesBuffer.SetData(this.m_IndexOfIndicesData);
			this.m_NeedUpdateComputeBuffer = false;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000040B7 File Offset: 0x000022B7
		internal void GetRuntimeResources(ref ProbeReferenceVolume.RuntimeResources rr)
		{
			if (this.m_NeedUpdateComputeBuffer)
			{
				this.PushComputeData();
			}
			rr.cellIndices = this.m_IndexOfIndicesBuffer;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000040D3 File Offset: 0x000022D3
		internal void Cleanup()
		{
			CoreUtils.SafeRelease(this.m_IndexOfIndicesBuffer);
			this.m_IndexOfIndicesBuffer = null;
		}

		// Token: 0x04000023 RID: 35
		private const int kUintPerEntry = 3;

		// Token: 0x04000024 RID: 36
		[CompilerGenerated]
		private int <estimatedVMemCost>k__BackingField;

		// Token: 0x04000025 RID: 37
		private ComputeBuffer m_IndexOfIndicesBuffer;

		// Token: 0x04000026 RID: 38
		private uint[] m_IndexOfIndicesData;

		// Token: 0x04000027 RID: 39
		private Vector3Int m_CellCount;

		// Token: 0x04000028 RID: 40
		private Vector3Int m_CellMin;

		// Token: 0x04000029 RID: 41
		private int m_CellSizeInMinBricks;

		// Token: 0x0400002A RID: 42
		private bool m_NeedUpdateComputeBuffer;

		// Token: 0x02000114 RID: 276
		internal struct IndexMetaData
		{
			// Token: 0x060007ED RID: 2029 RVA: 0x000220D4 File Offset: 0x000202D4
			internal void Pack(out uint[] vals)
			{
				vals = new uint[3];
				for (int i = 0; i < 3; i++)
				{
					vals[i] = 0U;
				}
				vals[0] = (uint)(this.firstChunkIndex & 536870911);
				vals[0] |= (uint)((uint)(this.minSubdiv & 7) << 29);
				vals[1] = (uint)(this.minLocalIdx.x & 1023);
				vals[1] |= (uint)((uint)(this.minLocalIdx.y & 1023) << 10);
				vals[1] |= (uint)((uint)(this.minLocalIdx.z & 1023) << 20);
				vals[2] = (uint)(this.maxLocalIdx.x & 1023);
				vals[2] |= (uint)((uint)(this.maxLocalIdx.y & 1023) << 10);
				vals[2] |= (uint)((uint)(this.maxLocalIdx.z & 1023) << 20);
			}

			// Token: 0x0400047F RID: 1151
			internal Vector3Int minLocalIdx;

			// Token: 0x04000480 RID: 1152
			internal Vector3Int maxLocalIdx;

			// Token: 0x04000481 RID: 1153
			internal int firstChunkIndex;

			// Token: 0x04000482 RID: 1154
			internal int minSubdiv;
		}
	}
}
