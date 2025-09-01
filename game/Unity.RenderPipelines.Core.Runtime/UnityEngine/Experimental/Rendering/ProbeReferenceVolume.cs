using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200000D RID: 13
	public class ProbeReferenceVolume
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000040E7 File Offset: 0x000022E7
		private void InvalidateAllCellRefs()
		{
			this.m_CellRefCounting.Clear();
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000040F4 File Offset: 0x000022F4
		internal bool isInitialized
		{
			get
			{
				return this.m_ProbeReferenceVolumeInit;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000040FC File Offset: 0x000022FC
		internal bool enabledBySRP
		{
			get
			{
				return this.m_EnabledBySRP;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004104 File Offset: 0x00002304
		public ProbeVolumeSHBands shBands
		{
			get
			{
				return this.m_SHBands;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000410C File Offset: 0x0000230C
		public ProbeVolumeTextureMemoryBudget memoryBudget
		{
			get
			{
				return this.m_MemoryBudget;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00004114 File Offset: 0x00002314
		public static ProbeReferenceVolume instance
		{
			get
			{
				return ProbeReferenceVolume._instance;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000411B File Offset: 0x0000231B
		public void SetNumberOfCellsLoadedPerFrame(int numberOfCells)
		{
			this.m_NumberOfCellsLoadedPerFrame = Mathf.Max(1, numberOfCells);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000412C File Offset: 0x0000232C
		public void Initialize(in ProbeVolumeSystemParameters parameters)
		{
			if (this.m_IsInitialized)
			{
				Debug.LogError("Probe Volume System has already been initialized.");
				return;
			}
			this.m_MemoryBudget = parameters.memoryBudget;
			this.m_SHBands = parameters.shBands;
			this.InitializeDebug(parameters.probeDebugMesh, parameters.probeDebugShader);
			this.InitProbeReferenceVolume(128, this.m_MemoryBudget, this.m_SHBands);
			this.m_IsInitialized = true;
			this.m_NeedsIndexRebuild = true;
			this.sceneData = parameters.sceneData;
			this.m_EnabledBySRP = true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000041AE File Offset: 0x000023AE
		public void SetEnableStateFromSRP(bool srpEnablesPV)
		{
			this.m_EnabledBySRP = srpEnablesPV;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000041B7 File Offset: 0x000023B7
		internal void ForceSHBand(ProbeVolumeSHBands shBands)
		{
			if (this.m_ProbeReferenceVolumeInit)
			{
				this.CleanupLoadedData();
			}
			this.m_SHBands = shBands;
			this.m_ProbeReferenceVolumeInit = false;
			this.InitProbeReferenceVolume(128, this.m_MemoryBudget, shBands);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000041E7 File Offset: 0x000023E7
		public void Cleanup()
		{
			if (!this.m_ProbeReferenceVolumeInit)
			{
				return;
			}
			if (!this.m_IsInitialized)
			{
				Debug.LogError("Probe Volume System has not been initialized first before calling cleanup.");
				return;
			}
			this.CleanupLoadedData();
			this.CleanupDebug();
			this.m_IsInitialized = false;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004218 File Offset: 0x00002418
		public int GetVideoMemoryCost()
		{
			if (!this.m_ProbeReferenceVolumeInit)
			{
				return 0;
			}
			return this.m_Pool.estimatedVMemCost + this.m_Index.estimatedVMemCost + this.m_CellIndices.estimatedVMemCost;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004248 File Offset: 0x00002448
		private void RemoveCell(ProbeReferenceVolume.Cell cell)
		{
			if (cell.loaded)
			{
				bool flag = true;
				if (this.m_CellRefCounting.ContainsKey(cell.index))
				{
					Dictionary<int, int> cellRefCounting = this.m_CellRefCounting;
					int index = cell.index;
					int num = cellRefCounting[index];
					cellRefCounting[index] = num - 1;
					flag = (this.m_CellRefCounting[cell.index] <= 0);
					if (flag)
					{
						this.m_CellRefCounting[cell.index] = 0;
					}
				}
				if (flag)
				{
					if (this.cells.ContainsKey(cell.index))
					{
						this.cells.Remove(cell.index);
					}
					if (this.m_ChunkInfo.ContainsKey(cell.index))
					{
						this.m_ChunkInfo.Remove(cell.index);
					}
					if (cell.flatIdxInCellIndices >= 0)
					{
						this.m_CellIndices.MarkCellAsUnloaded(cell.flatIdxInCellIndices);
					}
					ProbeReferenceVolume.RegId id = default(ProbeReferenceVolume.RegId);
					if (this.m_CellToBricks.TryGetValue(cell, out id))
					{
						this.ReleaseBricks(id);
						this.m_CellToBricks.Remove(cell);
					}
				}
			}
			cell.loaded = false;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004360 File Offset: 0x00002560
		private void AddCell(ProbeReferenceVolume.Cell cell, List<ProbeBrickPool.BrickChunkAlloc> chunks)
		{
			if (this.m_CellRefCounting.ContainsKey(cell.index))
			{
				Dictionary<int, int> cellRefCounting = this.m_CellRefCounting;
				int index = cell.index;
				int num = cellRefCounting[index];
				cellRefCounting[index] = num + 1;
			}
			else
			{
				this.m_CellRefCounting.Add(cell.index, 1);
			}
			cell.loaded = true;
			this.cells[cell.index] = cell;
			ProbeReferenceVolume.CellChunkInfo cellChunkInfo = new ProbeReferenceVolume.CellChunkInfo();
			cellChunkInfo.chunks = chunks;
			this.m_ChunkInfo[cell.index] = cellChunkInfo;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000043EC File Offset: 0x000025EC
		private bool CheckCompatibilityWithCollection(ProbeVolumeAsset asset, Dictionary<string, ProbeVolumeAsset> collection)
		{
			if (collection.Count > 0)
			{
				foreach (ProbeVolumeAsset probeVolumeAsset in collection.Values)
				{
					if (!this.m_PendingAssetsToBeUnloaded.ContainsKey(probeVolumeAsset.GetSerializedFullPath()))
					{
						return probeVolumeAsset.CompatibleWith(asset);
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004464 File Offset: 0x00002664
		internal void AddPendingAssetLoading(ProbeVolumeAsset asset)
		{
			string serializedFullPath = asset.GetSerializedFullPath();
			if (this.m_PendingAssetsToBeLoaded.ContainsKey(serializedFullPath))
			{
				this.m_PendingAssetsToBeLoaded.Remove(serializedFullPath);
			}
			if (!this.CheckCompatibilityWithCollection(asset, this.m_ActiveAssets))
			{
				Debug.LogError("Trying to load Probe Volume data for a scene that has been baked with different settings than currently loaded ones. Please make sure all loaded scenes are in the same baking set.");
				return;
			}
			if (!this.CheckCompatibilityWithCollection(asset, this.m_PendingAssetsToBeLoaded))
			{
				Debug.LogError("Trying to load Probe Volume data for a scene that has been baked with different settings from other scenes that are being loaded. Please make sure all loaded scenes are in the same baking set.");
				return;
			}
			this.m_PendingAssetsToBeLoaded.Add(serializedFullPath, asset);
			this.m_NeedLoadAsset = true;
			Vector3Int zero = Vector3Int.zero;
			Vector3Int vector3Int = Vector3Int.zero;
			Vector3Int vector3Int2 = Vector3Int.zero;
			bool flag = true;
			foreach (ProbeVolumeAsset probeVolumeAsset in this.m_PendingAssetsToBeLoaded.Values)
			{
				vector3Int = Vector3Int.Min(vector3Int, probeVolumeAsset.minCellPosition);
				vector3Int2 = Vector3Int.Max(vector3Int2, probeVolumeAsset.maxCellPosition);
				if (flag)
				{
					this.m_CurrGlobalBounds = probeVolumeAsset.globalBounds;
					flag = false;
				}
				else
				{
					this.m_CurrGlobalBounds.Encapsulate(probeVolumeAsset.globalBounds);
				}
			}
			foreach (ProbeVolumeAsset probeVolumeAsset2 in this.m_ActiveAssets.Values)
			{
				vector3Int = Vector3Int.Min(vector3Int, probeVolumeAsset2.minCellPosition);
				vector3Int2 = Vector3Int.Max(vector3Int2, probeVolumeAsset2.maxCellPosition);
				if (flag)
				{
					this.m_CurrGlobalBounds = probeVolumeAsset2.globalBounds;
					flag = false;
				}
				else
				{
					this.m_CurrGlobalBounds.Encapsulate(probeVolumeAsset2.globalBounds);
				}
			}
			this.m_NeedsIndexRebuild |= (this.m_Index == null || this.m_PendingInitInfo.pendingMinCellPosition != vector3Int || this.m_PendingInitInfo.pendingMaxCellPosition != vector3Int2);
			this.m_PendingInitInfo.pendingMinCellPosition = vector3Int;
			this.m_PendingInitInfo.pendingMaxCellPosition = vector3Int2;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004650 File Offset: 0x00002850
		internal void AddPendingAssetRemoval(ProbeVolumeAsset asset)
		{
			string serializedFullPath = asset.GetSerializedFullPath();
			if (this.m_PendingAssetsToBeUnloaded.ContainsKey(serializedFullPath))
			{
				this.m_PendingAssetsToBeUnloaded.Remove(serializedFullPath);
			}
			this.m_PendingAssetsToBeUnloaded.Add(asset.GetSerializedFullPath(), asset);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004694 File Offset: 0x00002894
		internal void RemovePendingAsset(ProbeVolumeAsset asset)
		{
			string serializedFullPath = asset.GetSerializedFullPath();
			for (int i = this.m_CellsToBeLoaded.Count - 1; i >= 0; i--)
			{
				if (this.m_CellsToBeLoaded[i].sourceAsset == serializedFullPath)
				{
					this.m_CellsToBeLoaded.RemoveAt(i);
				}
			}
			if (this.m_ActiveAssets.ContainsKey(serializedFullPath))
			{
				this.m_ActiveAssets.Remove(serializedFullPath);
			}
			foreach (ProbeReferenceVolume.Cell cell in asset.cells)
			{
				this.RemoveCell(cell);
			}
			this.ClearDebugData();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000474C File Offset: 0x0000294C
		private void PerformPendingIndexChangeAndInit()
		{
			if (this.m_NeedsIndexRebuild)
			{
				this.CleanupLoadedData();
				this.InitProbeReferenceVolume(128, this.m_MemoryBudget, this.m_SHBands);
				this.m_HasChangedIndex = true;
				this.m_NeedsIndexRebuild = false;
				return;
			}
			this.m_HasChangedIndex = false;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004789 File Offset: 0x00002989
		internal void SetMinBrickAndMaxSubdiv(float minBrickSize, int maxSubdiv)
		{
			this.SetTRS(Vector3.zero, Quaternion.identity, minBrickSize);
			this.SetMaxSubdivision(maxSubdiv);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000047A4 File Offset: 0x000029A4
		private void LoadAsset(ProbeVolumeAsset asset)
		{
			if (asset.Version != 3)
			{
				Debug.LogWarning("Trying to load an asset " + asset.GetSerializedFullPath() + " that has been baked with a previous version of the system. Please re-bake the data.");
				return;
			}
			asset.GetSerializedFullPath();
			this.SetMinBrickAndMaxSubdiv(asset.minBrickSize, asset.maxSubdivision);
			for (int i = 0; i < asset.cells.Count; i++)
			{
				ProbeReferenceVolume.Cell cell = asset.cells[i];
				ProbeReferenceVolume.CellSortInfo cellSortInfo = new ProbeReferenceVolume.CellSortInfo();
				cellSortInfo.cell = cell;
				cellSortInfo.position = cell.position * this.MaxBrickSize() * 0.5f + this.m_Transform.posWS;
				cellSortInfo.sourceAsset = asset.GetSerializedFullPath();
				this.m_CellsToBeLoaded.Add(cellSortInfo);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000486C File Offset: 0x00002A6C
		private void PerformPendingLoading()
		{
			if ((this.m_PendingAssetsToBeLoaded.Count == 0 && this.m_ActiveAssets.Count == 0) || !this.m_NeedLoadAsset || !this.m_ProbeReferenceVolumeInit)
			{
				return;
			}
			this.m_Pool.EnsureTextureValidity();
			if (this.m_HasChangedIndex)
			{
				this.InvalidateAllCellRefs();
				foreach (ProbeVolumeAsset asset in this.m_ActiveAssets.Values)
				{
					this.LoadAsset(asset);
				}
			}
			foreach (ProbeVolumeAsset probeVolumeAsset in this.m_PendingAssetsToBeLoaded.Values)
			{
				this.LoadAsset(probeVolumeAsset);
				if (!this.m_ActiveAssets.ContainsKey(probeVolumeAsset.GetSerializedFullPath()))
				{
					this.m_ActiveAssets.Add(probeVolumeAsset.GetSerializedFullPath(), probeVolumeAsset);
				}
			}
			this.m_PendingAssetsToBeLoaded.Clear();
			this.m_NeedLoadAsset = false;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004988 File Offset: 0x00002B88
		private void PerformPendingDeletion()
		{
			if (!this.m_ProbeReferenceVolumeInit)
			{
				this.m_PendingAssetsToBeUnloaded.Clear();
			}
			foreach (ProbeVolumeAsset asset in this.m_PendingAssetsToBeUnloaded.Values)
			{
				this.RemovePendingAsset(asset);
			}
			this.m_PendingAssetsToBeUnloaded.Clear();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004A00 File Offset: 0x00002C00
		private int GetNumberOfBricksAtSubdiv(ProbeReferenceVolume.Cell cell, out Vector3Int minValidLocalIdxAtMaxRes, out Vector3Int sizeOfValidIndicesAtMaxRes)
		{
			minValidLocalIdxAtMaxRes = Vector3Int.zero;
			sizeOfValidIndicesAtMaxRes = Vector3Int.one;
			Vector3 vector = new Vector3((float)cell.position.x * this.MaxBrickSize(), (float)cell.position.y * this.MaxBrickSize(), (float)cell.position.z * this.MaxBrickSize());
			Bounds bounds = default(Bounds);
			bounds.min = vector;
			bounds.max = vector + Vector3.one * this.MaxBrickSize();
			Bounds bounds2 = default(Bounds);
			bounds2.min = Vector3.Max(bounds.min, this.m_CurrGlobalBounds.min);
			bounds2.max = Vector3.Min(bounds.max, this.m_CurrGlobalBounds.max);
			bounds2.max - bounds2.min;
			Vector3 vector2 = bounds2.min - bounds.min;
			minValidLocalIdxAtMaxRes.x = Mathf.CeilToInt(vector2.x / this.MinBrickSize());
			minValidLocalIdxAtMaxRes.y = Mathf.CeilToInt(vector2.y / this.MinBrickSize());
			minValidLocalIdxAtMaxRes.z = Mathf.CeilToInt(vector2.z / this.MinBrickSize());
			Vector3 vector3 = bounds2.max - bounds.min;
			sizeOfValidIndicesAtMaxRes.x = Mathf.CeilToInt(vector3.x / this.MinBrickSize()) - minValidLocalIdxAtMaxRes.x + 1;
			sizeOfValidIndicesAtMaxRes.y = Mathf.CeilToInt(vector3.y / this.MinBrickSize()) - minValidLocalIdxAtMaxRes.y + 1;
			sizeOfValidIndicesAtMaxRes.z = Mathf.CeilToInt(vector3.z / this.MinBrickSize()) - minValidLocalIdxAtMaxRes.z + 1;
			Vector3Int vector3Int = default(Vector3Int);
			vector3Int = sizeOfValidIndicesAtMaxRes / ProbeReferenceVolume.CellSize(cell.minSubdiv);
			return vector3Int.x * vector3Int.y * vector3Int.z;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004BF8 File Offset: 0x00002DF8
		private bool GetCellIndexUpdate(ProbeReferenceVolume.Cell cell, out ProbeBrickIndex.CellIndexUpdateInfo cellUpdateInfo)
		{
			cellUpdateInfo = default(ProbeBrickIndex.CellIndexUpdateInfo);
			Vector3Int vector3Int;
			Vector3Int a;
			int numberOfBricksAtSubdiv = this.GetNumberOfBricksAtSubdiv(cell, out vector3Int, out a);
			cellUpdateInfo.cellPositionInBricksAtMaxRes = cell.position * ProbeReferenceVolume.CellSize(this.m_MaxSubdivision - 1);
			cellUpdateInfo.minSubdivInCell = cell.minSubdiv;
			cellUpdateInfo.minValidBrickIndexForCellAtMaxRes = vector3Int;
			cellUpdateInfo.maxValidBrickIndexForCellAtMaxResPlusOne = a + vector3Int;
			return this.m_Index.AssignIndexChunksToCell(cell, numberOfBricksAtSubdiv, ref cellUpdateInfo);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004C64 File Offset: 0x00002E64
		private void LoadPendingCells(bool loadAll = false)
		{
			int num = Mathf.Min(this.m_NumberOfCellsLoadedPerFrame, this.m_CellsToBeLoaded.Count);
			num = (loadAll ? this.m_CellsToBeLoaded.Count : num);
			if (this.m_PendingInitInfo.pendingMinCellPosition == this.m_PendingInitInfo.pendingMaxCellPosition && num > 1)
			{
				return;
			}
			if (num != 0)
			{
				this.ClearDebugData();
			}
			for (int i = 0; i < num; i++)
			{
				ProbeReferenceVolume.CellSortInfo cellSortInfo = this.m_CellsToBeLoaded[0];
				ProbeReferenceVolume.Cell cell = cellSortInfo.cell;
				string sourceAsset = cellSortInfo.sourceAsset;
				bool compressed = false;
				int num2 = 0;
				ProbeBrickPool.DataLocation dataloc = ProbeBrickPool.CreateDataLocation(cell.sh.Length, compressed, this.m_SHBands, out num2);
				ProbeBrickPool.FillDataLocation(ref dataloc, cell.sh, this.m_SHBands);
				cell.flatIdxInCellIndices = this.m_CellIndices.GetFlatIdxForCell(cell.position);
				ProbeBrickIndex.CellIndexUpdateInfo cellIndexUpdateInfo;
				if (!this.GetCellIndexUpdate(cell, out cellIndexUpdateInfo))
				{
					return;
				}
				List<ProbeBrickIndex.Brick> list = new List<ProbeBrickIndex.Brick>();
				list.AddRange(cell.bricks);
				List<ProbeBrickPool.BrickChunkAlloc> chunks = new List<ProbeBrickPool.BrickChunkAlloc>();
				ProbeReferenceVolume.RegId regId = this.AddBricks(list, dataloc, cellIndexUpdateInfo, out chunks);
				this.m_BricksToCellUpdateInfo.Add(regId, cellIndexUpdateInfo);
				this.m_CellIndices.AddCell(cell.flatIdxInCellIndices, cellIndexUpdateInfo);
				this.AddCell(cell, chunks);
				this.m_CellToBricks[cell] = regId;
				dataloc.Cleanup();
				this.m_CellsToBeLoaded.RemoveAt(0);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004DBC File Offset: 0x00002FBC
		public void PerformPendingOperations(bool loadAllCells = false)
		{
			this.PerformPendingDeletion();
			this.PerformPendingIndexChangeAndInit();
			this.PerformPendingLoading();
			this.LoadPendingCells(loadAllCells);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004DD8 File Offset: 0x00002FD8
		private void InitProbeReferenceVolume(int allocationSize, ProbeVolumeTextureMemoryBudget memoryBudget, ProbeVolumeSHBands shBands)
		{
			Vector3Int pendingMinCellPosition = this.m_PendingInitInfo.pendingMinCellPosition;
			Vector3Int pendingMaxCellPosition = this.m_PendingInitInfo.pendingMaxCellPosition;
			if (!this.m_ProbeReferenceVolumeInit)
			{
				this.m_Pool = new ProbeBrickPool(allocationSize, memoryBudget, shBands);
				this.m_Index = new ProbeBrickIndex(memoryBudget);
				this.m_CellIndices = new ProbeCellIndices(pendingMinCellPosition, pendingMaxCellPosition, (int)Mathf.Pow(3f, (float)(this.m_MaxSubdivision - 1)));
				this.m_PositionOffsets[0] = 0f;
				float num = 0.33333334f;
				for (int i = 1; i < 3; i++)
				{
					this.m_PositionOffsets[i] = (float)i * num;
				}
				this.m_PositionOffsets[this.m_PositionOffsets.Length - 1] = 1f;
				this.m_ProbeReferenceVolumeInit = true;
				this.ClearDebugData();
				this.m_NeedLoadAsset = true;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004E98 File Offset: 0x00003098
		public void SortPendingCells(Vector3 cameraPosition)
		{
			if (this.m_CellsToBeLoaded.Count > 0)
			{
				for (int i = 0; i < this.m_CellsToBeLoaded.Count; i++)
				{
					this.m_CellsToBeLoaded[i].distanceToCamera = Vector3.Distance(cameraPosition, this.m_CellsToBeLoaded[i].position);
				}
				this.m_CellsToBeLoaded.Sort();
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004EFC File Offset: 0x000030FC
		private ProbeReferenceVolume()
		{
			this.m_Transform.posWS = Vector3.zero;
			this.m_Transform.rot = Quaternion.identity;
			this.m_Transform.scale = 1f;
			this.m_Transform.refSpaceToWS = Matrix4x4.identity;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005030 File Offset: 0x00003230
		public ProbeReferenceVolume.RuntimeResources GetRuntimeResources()
		{
			if (!this.m_ProbeReferenceVolumeInit)
			{
				return default(ProbeReferenceVolume.RuntimeResources);
			}
			ProbeReferenceVolume.RuntimeResources result = default(ProbeReferenceVolume.RuntimeResources);
			this.m_Index.GetRuntimeResources(ref result);
			this.m_CellIndices.GetRuntimeResources(ref result);
			this.m_Pool.GetRuntimeResources(ref result);
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005080 File Offset: 0x00003280
		internal void SetTRS(Vector3 position, Quaternion rotation, float minBrickSize)
		{
			this.m_Transform.posWS = position;
			this.m_Transform.rot = rotation;
			this.m_Transform.scale = minBrickSize;
			this.m_Transform.refSpaceToWS = Matrix4x4.TRS(this.m_Transform.posWS, this.m_Transform.rot, Vector3.one * this.m_Transform.scale);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000050EC File Offset: 0x000032EC
		internal void SetMaxSubdivision(int maxSubdivision)
		{
			this.m_MaxSubdivision = Math.Min(maxSubdivision, 7);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000050FB File Offset: 0x000032FB
		internal static int CellSize(int subdivisionLevel)
		{
			return (int)Mathf.Pow(3f, (float)subdivisionLevel);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000510A File Offset: 0x0000330A
		internal float BrickSize(int subdivisionLevel)
		{
			return this.m_Transform.scale * (float)ProbeReferenceVolume.CellSize(subdivisionLevel);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000511F File Offset: 0x0000331F
		internal float MinBrickSize()
		{
			return this.m_Transform.scale;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000512C File Offset: 0x0000332C
		internal float MaxBrickSize()
		{
			return this.BrickSize(this.m_MaxSubdivision - 1);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000513C File Offset: 0x0000333C
		internal Matrix4x4 GetRefSpaceToWS()
		{
			return this.m_Transform.refSpaceToWS;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005149 File Offset: 0x00003349
		internal ProbeReferenceVolume.RefVolTransform GetTransform()
		{
			return this.m_Transform;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005151 File Offset: 0x00003351
		internal int GetMaxSubdivision()
		{
			return this.m_MaxSubdivision;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005159 File Offset: 0x00003359
		internal int GetMaxSubdivision(float multiplier)
		{
			return Mathf.CeilToInt((float)this.m_MaxSubdivision * multiplier);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005169 File Offset: 0x00003369
		internal float GetDistanceBetweenProbes(int subdivisionLevel)
		{
			return this.BrickSize(subdivisionLevel) / 3f;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005178 File Offset: 0x00003378
		internal float MinDistanceBetweenProbes()
		{
			return this.GetDistanceBetweenProbes(0);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005181 File Offset: 0x00003381
		public bool DataHasBeenLoaded()
		{
			return this.m_BricksLoaded;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000518C File Offset: 0x0000338C
		internal void Clear()
		{
			if (this.m_ProbeReferenceVolumeInit)
			{
				this.m_Pool.Clear();
				this.m_Index.Clear();
				this.cells.Clear();
				this.m_ChunkInfo.Clear();
			}
			if (this.clearAssetsOnVolumeClear)
			{
				this.m_PendingAssetsToBeLoaded.Clear();
				this.m_ActiveAssets.Clear();
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000051EC File Offset: 0x000033EC
		private ProbeReferenceVolume.RegId AddBricks(List<ProbeBrickIndex.Brick> bricks, ProbeBrickPool.DataLocation dataloc, ProbeBrickIndex.CellIndexUpdateInfo cellUpdateInfo, out List<ProbeBrickPool.BrickChunkAlloc> ch_list)
		{
			int chunkSize = this.m_Pool.GetChunkSize();
			ch_list = new List<ProbeBrickPool.BrickChunkAlloc>((bricks.Count + chunkSize - 1) / chunkSize);
			this.m_Pool.Allocate(ch_list.Capacity, ch_list);
			this.m_TmpSrcChunks.Clear();
			this.m_TmpSrcChunks.Capacity = ch_list.Count;
			ProbeBrickPool.BrickChunkAlloc brickChunkAlloc;
			brickChunkAlloc.x = 0;
			brickChunkAlloc.y = 0;
			brickChunkAlloc.z = 0;
			for (int i = 0; i < ch_list.Count; i++)
			{
				this.m_TmpSrcChunks.Add(brickChunkAlloc);
				brickChunkAlloc.x += chunkSize * 4;
				if (brickChunkAlloc.x >= dataloc.width)
				{
					brickChunkAlloc.x = 0;
					brickChunkAlloc.y += 4;
					if (brickChunkAlloc.y >= dataloc.height)
					{
						brickChunkAlloc.y = 0;
						brickChunkAlloc.z += 4;
					}
				}
			}
			this.m_Pool.Update(dataloc, this.m_TmpSrcChunks, ch_list, this.m_SHBands);
			this.m_BricksLoaded = true;
			this.m_ID++;
			ProbeReferenceVolume.RegId regId;
			regId.id = this.m_ID;
			this.m_Registry.Add(regId, ch_list);
			this.m_Index.AddBricks(regId, bricks, ch_list, this.m_Pool.GetChunkSize(), this.m_Pool.GetPoolWidth(), this.m_Pool.GetPoolHeight(), cellUpdateInfo);
			return regId;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005358 File Offset: 0x00003558
		private void ReleaseBricks(ProbeReferenceVolume.RegId id)
		{
			List<ProbeBrickPool.BrickChunkAlloc> allocations;
			if (!this.m_Registry.TryGetValue(id, out allocations))
			{
				Debug.Log("Tried to release bricks with id=" + id.id.ToString() + " but no bricks were registered under this id.");
				return;
			}
			this.m_Index.RemoveBricks(id, this.m_BricksToCellUpdateInfo[id]);
			this.m_Pool.Deallocate(allocations);
			this.m_Registry.Remove(id);
			this.m_BricksToCellUpdateInfo.Remove(id);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000053D4 File Offset: 0x000035D4
		public void UpdateConstantBuffer(CommandBuffer cmd, ProbeVolumeShadingParameters parameters)
		{
			float num = parameters.normalBias;
			float num2 = parameters.viewBias;
			if (parameters.scaleBiasByMinDistanceBetweenProbes)
			{
				num *= this.MinDistanceBetweenProbes();
				num2 *= this.MinDistanceBetweenProbes();
			}
			ShaderVariablesProbeVolumes shaderVariablesProbeVolumes;
			shaderVariablesProbeVolumes._NormalBias = num;
			shaderVariablesProbeVolumes._PoolDim = this.m_Pool.GetPoolDimensions();
			shaderVariablesProbeVolumes._ViewBias = num2;
			shaderVariablesProbeVolumes._PVSamplingNoise = parameters.samplingNoise;
			shaderVariablesProbeVolumes._CellInMinBricks = (float)((int)Mathf.Pow(3f, (float)(this.m_MaxSubdivision - 1)));
			shaderVariablesProbeVolumes._CellIndicesDim = this.m_CellIndices.GetCellIndexDimension();
			shaderVariablesProbeVolumes._MinCellPosition = this.m_CellIndices.GetCellMinPosition();
			shaderVariablesProbeVolumes._MinBrickSize = this.MinBrickSize();
			shaderVariablesProbeVolumes._IndexChunkSize = 243;
			shaderVariablesProbeVolumes._CellInMeters = this.MaxBrickSize();
			ConstantBuffer.PushGlobal<ShaderVariablesProbeVolumes>(cmd, shaderVariablesProbeVolumes, this.m_CBShaderID);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000054BB File Offset: 0x000036BB
		private void CleanupLoadedData()
		{
			this.m_BricksLoaded = false;
			if (this.m_ProbeReferenceVolumeInit)
			{
				this.m_Index.Cleanup();
				this.m_CellIndices.Cleanup();
				this.m_Pool.Cleanup();
			}
			this.m_ProbeReferenceVolumeInit = false;
			this.ClearDebugData();
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000054FA File Offset: 0x000036FA
		internal ProbeVolumeDebug debugDisplay
		{
			[CompilerGenerated]
			get
			{
				return this.<debugDisplay>k__BackingField;
			}
		} = new ProbeVolumeDebug();

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005502 File Offset: 0x00003702
		public Color[] subdivisionDebugColors
		{
			[CompilerGenerated]
			get
			{
				return this.<subdivisionDebugColors>k__BackingField;
			}
		} = new Color[7];

		// Token: 0x06000083 RID: 131 RVA: 0x0000550A File Offset: 0x0000370A
		public void RenderDebug(Camera camera)
		{
			if (camera.cameraType != CameraType.Reflection && camera.cameraType != CameraType.Preview && this.debugDisplay.drawProbes)
			{
				this.DrawProbeDebug(camera);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005534 File Offset: 0x00003734
		private void InitializeDebug(Mesh debugProbeMesh, Shader debugProbeShader)
		{
			this.m_DebugMesh = debugProbeMesh;
			this.m_DebugMaterial = CoreUtils.CreateEngineMaterial(debugProbeShader);
			this.m_DebugMaterial.enableInstancing = true;
			this.subdivisionDebugColors[0] = new Color(1f, 0f, 0f);
			this.subdivisionDebugColors[1] = new Color(0f, 1f, 0f);
			this.subdivisionDebugColors[2] = new Color(0f, 0f, 1f);
			this.subdivisionDebugColors[3] = new Color(1f, 1f, 0f);
			this.subdivisionDebugColors[4] = new Color(1f, 0f, 1f);
			this.subdivisionDebugColors[5] = new Color(0f, 1f, 1f);
			this.subdivisionDebugColors[6] = new Color(0.5f, 0.5f, 0.5f);
			this.RegisterDebug();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005646 File Offset: 0x00003846
		private void CleanupDebug()
		{
			this.UnregisterDebug(true);
			CoreUtils.Destroy(this.m_DebugMaterial);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000565A File Offset: 0x0000385A
		private void RefreshDebug<T>(DebugUI.Field<T> field, T value)
		{
			this.UnregisterDebug(false);
			this.RegisterDebug();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005669 File Offset: 0x00003869
		private void DebugCellIndexChanged<T>(DebugUI.Field<T> field, T value)
		{
			this.ClearDebugData();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005674 File Offset: 0x00003874
		private void RegisterDebug()
		{
			List<DebugUI.Widget> list = new List<DebugUI.Widget>();
			DebugUI.Container container = new DebugUI.Container
			{
				displayName = "Subdivision Visualization"
			};
			container.children.Add(new DebugUI.BoolField
			{
				displayName = "Display Cells",
				getter = (() => this.debugDisplay.drawCells),
				setter = delegate(bool value)
				{
					this.debugDisplay.drawCells = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshDebug<bool>)
			});
			container.children.Add(new DebugUI.BoolField
			{
				displayName = "Display Bricks",
				getter = (() => this.debugDisplay.drawBricks),
				setter = delegate(bool value)
				{
					this.debugDisplay.drawBricks = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshDebug<bool>)
			});
			if (this.debugDisplay.drawCells || this.debugDisplay.drawBricks)
			{
				ObservableList<DebugUI.Widget> children = container.children;
				DebugUI.FloatField floatField = new DebugUI.FloatField();
				floatField.displayName = "Culling Distance";
				floatField.getter = (() => this.debugDisplay.subdivisionViewCullingDistance);
				floatField.setter = delegate(float value)
				{
					this.debugDisplay.subdivisionViewCullingDistance = value;
				};
				floatField.min = (() => 0f);
				children.Add(floatField);
			}
			DebugUI.Container container2 = new DebugUI.Container
			{
				displayName = "Probe Visualization"
			};
			container2.children.Add(new DebugUI.BoolField
			{
				displayName = "Display Probes",
				getter = (() => this.debugDisplay.drawProbes),
				setter = delegate(bool value)
				{
					this.debugDisplay.drawProbes = value;
				},
				onValueChanged = new Action<DebugUI.Field<bool>, bool>(this.RefreshDebug<bool>)
			});
			if (this.debugDisplay.drawProbes)
			{
				container2.children.Add(new DebugUI.EnumField
				{
					displayName = "Probe Shading Mode",
					getter = (() => (int)this.debugDisplay.probeShading),
					setter = delegate(int value)
					{
						this.debugDisplay.probeShading = (DebugProbeShadingMode)value;
					},
					autoEnum = typeof(DebugProbeShadingMode),
					getIndex = (() => (int)this.debugDisplay.probeShading),
					setIndex = delegate(int value)
					{
						this.debugDisplay.probeShading = (DebugProbeShadingMode)value;
					},
					onValueChanged = new Action<DebugUI.Field<int>, int>(this.RefreshDebug<int>)
				});
				ObservableList<DebugUI.Widget> children2 = container2.children;
				DebugUI.FloatField floatField2 = new DebugUI.FloatField();
				floatField2.displayName = "Probe Size";
				floatField2.getter = (() => this.debugDisplay.probeSize);
				floatField2.setter = delegate(float value)
				{
					this.debugDisplay.probeSize = value;
				};
				floatField2.min = (() => 0.1f);
				floatField2.max = (() => 10f);
				children2.Add(floatField2);
				if (this.debugDisplay.probeShading == DebugProbeShadingMode.SH)
				{
					container2.children.Add(new DebugUI.FloatField
					{
						displayName = "Probe Exposure Compensation",
						getter = (() => this.debugDisplay.exposureCompensation),
						setter = delegate(float value)
						{
							this.debugDisplay.exposureCompensation = value;
						}
					});
				}
				ObservableList<DebugUI.Widget> children3 = container2.children;
				DebugUI.FloatField floatField3 = new DebugUI.FloatField();
				floatField3.displayName = "Culling Distance";
				floatField3.getter = (() => this.debugDisplay.probeCullingDistance);
				floatField3.setter = delegate(float value)
				{
					this.debugDisplay.probeCullingDistance = value;
				};
				floatField3.min = (() => 0f);
				children3.Add(floatField3);
				ObservableList<DebugUI.Widget> children4 = container2.children;
				DebugUI.IntField intField = new DebugUI.IntField();
				intField.displayName = "Max subdivision displayed";
				intField.getter = (() => this.debugDisplay.maxSubdivToVisualize);
				intField.setter = delegate(int v)
				{
					this.debugDisplay.maxSubdivToVisualize = Mathf.Min(v, ProbeReferenceVolume.instance.GetMaxSubdivision());
				};
				intField.min = (() => 0);
				intField.max = (() => ProbeReferenceVolume.instance.GetMaxSubdivision());
				children4.Add(intField);
			}
			list.Add(container);
			list.Add(container2);
			this.m_DebugItems = list.ToArray();
			DebugManager.instance.GetPanel("Probe Volume", true, 0, false).children.Add(this.m_DebugItems);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005AAD File Offset: 0x00003CAD
		private void UnregisterDebug(bool destroyPanel)
		{
			if (destroyPanel)
			{
				DebugManager.instance.RemovePanel("Probe Volume");
				return;
			}
			DebugManager.instance.GetPanel("Probe Volume", false, 0, false).children.Remove(this.m_DebugItems);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005AE8 File Offset: 0x00003CE8
		private bool ShouldCullCell(Vector3 cellPosition, Transform cameraTransform, Plane[] frustumPlanes)
		{
			float num = this.MaxBrickSize();
			Vector3 posWS = this.GetTransform().posWS;
			Vector3 vector = cellPosition * num + posWS + Vector3.one * (num / 2f);
			float num2 = (float)Mathf.CeilToInt(this.debugDisplay.probeCullingDistance / num) * num;
			if (Vector3.Distance(cameraTransform.position, vector) > num2)
			{
				return true;
			}
			Bounds bounds = new Bounds(vector, num * Vector3.one);
			return !GeometryUtility.TestPlanesAABB(frustumPlanes, bounds);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005B74 File Offset: 0x00003D74
		private void DrawProbeDebug(Camera camera)
		{
			if (this.debugDisplay.drawProbes)
			{
				if (this.m_CellDebugData.Count == 0)
				{
					this.CreateInstancedProbes();
				}
				GeometryUtility.CalculateFrustumPlanes(camera, this.m_DebugFrustumPlanes);
				this.m_DebugMaterial.shaderKeywords = null;
				if (this.m_SHBands == ProbeVolumeSHBands.SphericalHarmonicsL1)
				{
					this.m_DebugMaterial.EnableKeyword("PROBE_VOLUMES_L1");
				}
				else if (this.m_SHBands == ProbeVolumeSHBands.SphericalHarmonicsL2)
				{
					this.m_DebugMaterial.EnableKeyword("PROBE_VOLUMES_L2");
				}
				foreach (ProbeReferenceVolume.CellInstancedDebugProbes cellInstancedDebugProbes in this.m_CellDebugData)
				{
					if (!this.ShouldCullCell(cellInstancedDebugProbes.cellPosition, camera.transform, this.m_DebugFrustumPlanes))
					{
						for (int i = 0; i < cellInstancedDebugProbes.probeBuffers.Count; i++)
						{
							Matrix4x4[] array = cellInstancedDebugProbes.probeBuffers[i];
							MaterialPropertyBlock materialPropertyBlock = cellInstancedDebugProbes.props[i];
							materialPropertyBlock.SetInt("_ShadingMode", (int)this.debugDisplay.probeShading);
							materialPropertyBlock.SetFloat("_ExposureCompensation", this.debugDisplay.exposureCompensation);
							materialPropertyBlock.SetFloat("_ProbeSize", this.debugDisplay.probeSize);
							materialPropertyBlock.SetFloat("_CullDistance", this.debugDisplay.probeCullingDistance);
							materialPropertyBlock.SetInt("_MaxAllowedSubdiv", this.debugDisplay.maxSubdivToVisualize);
							materialPropertyBlock.SetFloat("_ValidityThreshold", this.dilationValidtyThreshold);
							Graphics.DrawMeshInstanced(this.m_DebugMesh, 0, this.m_DebugMaterial, array, array.Length, materialPropertyBlock, ShadowCastingMode.Off, false, 0, camera, LightProbeUsage.Off, null);
						}
					}
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005D38 File Offset: 0x00003F38
		private void ClearDebugData()
		{
			this.m_CellDebugData.Clear();
			this.realtimeSubdivisionInfo.Clear();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005D50 File Offset: 0x00003F50
		private void CreateInstancedProbes()
		{
			int num = ProbeReferenceVolume.instance.GetMaxSubdivision() - 1;
			foreach (ProbeReferenceVolume.Cell cell in ProbeReferenceVolume.instance.cells.Values)
			{
				if (cell.sh != null && cell.sh.Length != 0)
				{
					if (cell.bricks.Count != 0)
					{
						ProbeBrickIndex.Brick brick = cell.bricks[0];
					}
					List<Matrix4x4[]> list = new List<Matrix4x4[]>();
					List<MaterialPropertyBlock> list2 = new List<MaterialPropertyBlock>();
					ProbeReferenceVolume.CellChunkInfo cellChunkInfo;
					if (this.m_ChunkInfo.TryGetValue(cell.index, out cellChunkInfo))
					{
						Vector4[] array = new Vector4[1023];
						float[] array2 = new float[1023];
						float[] array3 = new float[1023];
						List<Matrix4x4> list3 = new List<Matrix4x4>();
						ProbeReferenceVolume.CellInstancedDebugProbes cellInstancedDebugProbes = new ProbeReferenceVolume.CellInstancedDebugProbes();
						cellInstancedDebugProbes.probeBuffers = list;
						cellInstancedDebugProbes.props = list2;
						cellInstancedDebugProbes.cellPosition = cell.position;
						int num2 = 0;
						for (int i = 0; i < cell.probePositions.Length; i++)
						{
							int subdivisionLevel = cell.bricks[i / 64].subdivisionLevel;
							int index = i / this.m_Pool.GetChunkSizeInProbeCount();
							ProbeBrickPool.BrickChunkAlloc brickChunkAlloc = cellChunkInfo.chunks[index];
							int num3 = i % this.m_Pool.GetChunkSizeInProbeCount();
							int num4 = num3 / 64;
							int num5 = num3 % 64;
							Vector2Int vector2Int = new Vector2Int(brickChunkAlloc.x + num4 * 4, brickChunkAlloc.y);
							int num6 = num5 % 16;
							Vector3Int vector3Int = new Vector3Int(vector2Int.x + num6 % 4, vector2Int.y + num6 / 4, num5 / 16);
							list3.Add(Matrix4x4.TRS(cell.probePositions[i], Quaternion.identity, Vector3.one * (0.3f * (float)(subdivisionLevel + 1))));
							array2[num2] = cell.validity[i];
							array[num2] = new Vector4((float)vector3Int.x, (float)vector3Int.y, (float)vector3Int.z, (float)subdivisionLevel);
							array3[num2] = (float)subdivisionLevel / (float)num;
							num2++;
							if (list3.Count >= 1023 || i == cell.probePositions.Length - 1)
							{
								num2 = 0;
								MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
								materialPropertyBlock.SetFloatArray("_Validity", array2);
								materialPropertyBlock.SetFloatArray("_RelativeSize", array3);
								materialPropertyBlock.SetVectorArray("_IndexInAtlas", array);
								list2.Add(materialPropertyBlock);
								list.Add(list3.ToArray());
								list3 = new List<Matrix4x4>();
							}
						}
						this.m_CellDebugData.Add(cellInstancedDebugProbes);
					}
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000601C File Offset: 0x0000421C
		private void OnClearLightingdata()
		{
			this.ClearDebugData();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006024 File Offset: 0x00004224
		// Note: this type is marked as 'beforefieldinit'.
		static ProbeReferenceVolume()
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006030 File Offset: 0x00004230
		[CompilerGenerated]
		private bool <RegisterDebug>b__119_0()
		{
			return this.debugDisplay.drawCells;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000603D File Offset: 0x0000423D
		[CompilerGenerated]
		private void <RegisterDebug>b__119_1(bool value)
		{
			this.debugDisplay.drawCells = value;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000604B File Offset: 0x0000424B
		[CompilerGenerated]
		private bool <RegisterDebug>b__119_2()
		{
			return this.debugDisplay.drawBricks;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006058 File Offset: 0x00004258
		[CompilerGenerated]
		private void <RegisterDebug>b__119_3(bool value)
		{
			this.debugDisplay.drawBricks = value;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006066 File Offset: 0x00004266
		[CompilerGenerated]
		private float <RegisterDebug>b__119_4()
		{
			return this.debugDisplay.subdivisionViewCullingDistance;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006073 File Offset: 0x00004273
		[CompilerGenerated]
		private void <RegisterDebug>b__119_5(float value)
		{
			this.debugDisplay.subdivisionViewCullingDistance = value;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006081 File Offset: 0x00004281
		[CompilerGenerated]
		private bool <RegisterDebug>b__119_7()
		{
			return this.debugDisplay.drawProbes;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000608E File Offset: 0x0000428E
		[CompilerGenerated]
		private void <RegisterDebug>b__119_8(bool value)
		{
			this.debugDisplay.drawProbes = value;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000609C File Offset: 0x0000429C
		[CompilerGenerated]
		private int <RegisterDebug>b__119_9()
		{
			return (int)this.debugDisplay.probeShading;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000060A9 File Offset: 0x000042A9
		[CompilerGenerated]
		private void <RegisterDebug>b__119_10(int value)
		{
			this.debugDisplay.probeShading = (DebugProbeShadingMode)value;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000060B7 File Offset: 0x000042B7
		[CompilerGenerated]
		private int <RegisterDebug>b__119_11()
		{
			return (int)this.debugDisplay.probeShading;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000060C4 File Offset: 0x000042C4
		[CompilerGenerated]
		private void <RegisterDebug>b__119_12(int value)
		{
			this.debugDisplay.probeShading = (DebugProbeShadingMode)value;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000060D2 File Offset: 0x000042D2
		[CompilerGenerated]
		private float <RegisterDebug>b__119_13()
		{
			return this.debugDisplay.probeSize;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000060DF File Offset: 0x000042DF
		[CompilerGenerated]
		private void <RegisterDebug>b__119_14(float value)
		{
			this.debugDisplay.probeSize = value;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000060ED File Offset: 0x000042ED
		[CompilerGenerated]
		private float <RegisterDebug>b__119_17()
		{
			return this.debugDisplay.exposureCompensation;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000060FA File Offset: 0x000042FA
		[CompilerGenerated]
		private void <RegisterDebug>b__119_18(float value)
		{
			this.debugDisplay.exposureCompensation = value;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006108 File Offset: 0x00004308
		[CompilerGenerated]
		private float <RegisterDebug>b__119_19()
		{
			return this.debugDisplay.probeCullingDistance;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006115 File Offset: 0x00004315
		[CompilerGenerated]
		private void <RegisterDebug>b__119_20(float value)
		{
			this.debugDisplay.probeCullingDistance = value;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006123 File Offset: 0x00004323
		[CompilerGenerated]
		private int <RegisterDebug>b__119_22()
		{
			return this.debugDisplay.maxSubdivToVisualize;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006130 File Offset: 0x00004330
		[CompilerGenerated]
		private void <RegisterDebug>b__119_23(int v)
		{
			this.debugDisplay.maxSubdivToVisualize = Mathf.Min(v, ProbeReferenceVolume.instance.GetMaxSubdivision());
		}

		// Token: 0x0400003B RID: 59
		private const int kProbeIndexPoolAllocationSize = 128;

		// Token: 0x0400003C RID: 60
		private bool m_IsInitialized;

		// Token: 0x0400003D RID: 61
		private int m_ID;

		// Token: 0x0400003E RID: 62
		private ProbeReferenceVolume.RefVolTransform m_Transform;

		// Token: 0x0400003F RID: 63
		private int m_MaxSubdivision;

		// Token: 0x04000040 RID: 64
		private ProbeBrickPool m_Pool;

		// Token: 0x04000041 RID: 65
		private ProbeBrickIndex m_Index;

		// Token: 0x04000042 RID: 66
		private ProbeCellIndices m_CellIndices;

		// Token: 0x04000043 RID: 67
		private List<ProbeBrickPool.BrickChunkAlloc> m_TmpSrcChunks = new List<ProbeBrickPool.BrickChunkAlloc>();

		// Token: 0x04000044 RID: 68
		private float[] m_PositionOffsets = new float[4];

		// Token: 0x04000045 RID: 69
		private Dictionary<ProbeReferenceVolume.RegId, List<ProbeBrickPool.BrickChunkAlloc>> m_Registry = new Dictionary<ProbeReferenceVolume.RegId, List<ProbeBrickPool.BrickChunkAlloc>>();

		// Token: 0x04000046 RID: 70
		private Bounds m_CurrGlobalBounds;

		// Token: 0x04000047 RID: 71
		internal Dictionary<int, ProbeReferenceVolume.Cell> cells = new Dictionary<int, ProbeReferenceVolume.Cell>();

		// Token: 0x04000048 RID: 72
		private Dictionary<int, ProbeReferenceVolume.CellChunkInfo> m_ChunkInfo = new Dictionary<int, ProbeReferenceVolume.CellChunkInfo>();

		// Token: 0x04000049 RID: 73
		internal ProbeVolumeSceneData sceneData;

		// Token: 0x0400004A RID: 74
		public Action<ProbeReferenceVolume.ExtraDataActionInput> retrieveExtraDataAction;

		// Token: 0x0400004B RID: 75
		private bool m_BricksLoaded;

		// Token: 0x0400004C RID: 76
		private Dictionary<ProbeReferenceVolume.Cell, ProbeReferenceVolume.RegId> m_CellToBricks = new Dictionary<ProbeReferenceVolume.Cell, ProbeReferenceVolume.RegId>();

		// Token: 0x0400004D RID: 77
		private Dictionary<ProbeReferenceVolume.RegId, ProbeBrickIndex.CellIndexUpdateInfo> m_BricksToCellUpdateInfo = new Dictionary<ProbeReferenceVolume.RegId, ProbeBrickIndex.CellIndexUpdateInfo>();

		// Token: 0x0400004E RID: 78
		private Dictionary<string, ProbeVolumeAsset> m_PendingAssetsToBeLoaded = new Dictionary<string, ProbeVolumeAsset>();

		// Token: 0x0400004F RID: 79
		private Dictionary<string, ProbeVolumeAsset> m_PendingAssetsToBeUnloaded = new Dictionary<string, ProbeVolumeAsset>();

		// Token: 0x04000050 RID: 80
		private Dictionary<string, ProbeVolumeAsset> m_ActiveAssets = new Dictionary<string, ProbeVolumeAsset>();

		// Token: 0x04000051 RID: 81
		private List<ProbeReferenceVolume.CellSortInfo> m_CellsToBeLoaded = new List<ProbeReferenceVolume.CellSortInfo>();

		// Token: 0x04000052 RID: 82
		private Dictionary<int, int> m_CellRefCounting = new Dictionary<int, int>();

		// Token: 0x04000053 RID: 83
		private bool m_NeedLoadAsset;

		// Token: 0x04000054 RID: 84
		private bool m_ProbeReferenceVolumeInit;

		// Token: 0x04000055 RID: 85
		private bool m_EnabledBySRP;

		// Token: 0x04000056 RID: 86
		private ProbeReferenceVolume.InitInfo m_PendingInitInfo;

		// Token: 0x04000057 RID: 87
		private bool m_NeedsIndexRebuild;

		// Token: 0x04000058 RID: 88
		private bool m_HasChangedIndex;

		// Token: 0x04000059 RID: 89
		private int m_CBShaderID = Shader.PropertyToID("ShaderVariablesProbeVolumes");

		// Token: 0x0400005A RID: 90
		private int m_NumberOfCellsLoadedPerFrame = 2;

		// Token: 0x0400005B RID: 91
		private ProbeVolumeTextureMemoryBudget m_MemoryBudget;

		// Token: 0x0400005C RID: 92
		private ProbeVolumeSHBands m_SHBands;

		// Token: 0x0400005D RID: 93
		internal bool clearAssetsOnVolumeClear;

		// Token: 0x0400005E RID: 94
		private static ProbeReferenceVolume _instance = new ProbeReferenceVolume();

		// Token: 0x0400005F RID: 95
		private const int kProbesPerBatch = 1023;

		// Token: 0x04000060 RID: 96
		[CompilerGenerated]
		private readonly ProbeVolumeDebug <debugDisplay>k__BackingField;

		// Token: 0x04000061 RID: 97
		[CompilerGenerated]
		private readonly Color[] <subdivisionDebugColors>k__BackingField;

		// Token: 0x04000062 RID: 98
		private DebugUI.Widget[] m_DebugItems;

		// Token: 0x04000063 RID: 99
		private Mesh m_DebugMesh;

		// Token: 0x04000064 RID: 100
		private Material m_DebugMaterial;

		// Token: 0x04000065 RID: 101
		private List<ProbeReferenceVolume.CellInstancedDebugProbes> m_CellDebugData = new List<ProbeReferenceVolume.CellInstancedDebugProbes>();

		// Token: 0x04000066 RID: 102
		private Plane[] m_DebugFrustumPlanes = new Plane[6];

		// Token: 0x04000067 RID: 103
		internal float dilationValidtyThreshold = 0.25f;

		// Token: 0x04000068 RID: 104
		internal Dictionary<ProbeReferenceVolume.Volume, List<ProbeBrickIndex.Brick>> realtimeSubdivisionInfo = new Dictionary<ProbeReferenceVolume.Volume, List<ProbeBrickIndex.Brick>>();

		// Token: 0x02000115 RID: 277
		[Serializable]
		internal class Cell
		{
			// Token: 0x060007EE RID: 2030 RVA: 0x000221CB File Offset: 0x000203CB
			public Cell()
			{
			}

			// Token: 0x04000483 RID: 1155
			public int index;

			// Token: 0x04000484 RID: 1156
			public Vector3Int position;

			// Token: 0x04000485 RID: 1157
			public List<ProbeBrickIndex.Brick> bricks;

			// Token: 0x04000486 RID: 1158
			public Vector3[] probePositions;

			// Token: 0x04000487 RID: 1159
			public SphericalHarmonicsL2[] sh;

			// Token: 0x04000488 RID: 1160
			public float[] validity;

			// Token: 0x04000489 RID: 1161
			public int minSubdiv;

			// Token: 0x0400048A RID: 1162
			[NonSerialized]
			public int flatIdxInCellIndices = -1;

			// Token: 0x0400048B RID: 1163
			[NonSerialized]
			public bool loaded;
		}

		// Token: 0x02000116 RID: 278
		private class CellChunkInfo
		{
			// Token: 0x060007EF RID: 2031 RVA: 0x000221DA File Offset: 0x000203DA
			public CellChunkInfo()
			{
			}

			// Token: 0x0400048C RID: 1164
			public List<ProbeBrickPool.BrickChunkAlloc> chunks;
		}

		// Token: 0x02000117 RID: 279
		private class CellSortInfo : IComparable
		{
			// Token: 0x060007F0 RID: 2032 RVA: 0x000221E4 File Offset: 0x000203E4
			public int CompareTo(object obj)
			{
				ProbeReferenceVolume.CellSortInfo cellSortInfo = obj as ProbeReferenceVolume.CellSortInfo;
				if (this.distanceToCamera < cellSortInfo.distanceToCamera)
				{
					return 1;
				}
				if (this.distanceToCamera > cellSortInfo.distanceToCamera)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x00022219 File Offset: 0x00020419
			public CellSortInfo()
			{
			}

			// Token: 0x0400048D RID: 1165
			internal string sourceAsset;

			// Token: 0x0400048E RID: 1166
			internal ProbeReferenceVolume.Cell cell;

			// Token: 0x0400048F RID: 1167
			internal float distanceToCamera;

			// Token: 0x04000490 RID: 1168
			internal Vector3 position;
		}

		// Token: 0x02000118 RID: 280
		internal struct Volume : IEquatable<ProbeReferenceVolume.Volume>
		{
			// Token: 0x060007F2 RID: 2034 RVA: 0x00022224 File Offset: 0x00020424
			public Volume(Matrix4x4 trs, float maxSubdivision, float minSubdivision)
			{
				this.X = trs.GetColumn(0);
				this.Y = trs.GetColumn(1);
				this.Z = trs.GetColumn(2);
				this.corner = trs.GetColumn(3) - this.X * 0.5f - this.Y * 0.5f - this.Z * 0.5f;
				this.maxSubdivisionMultiplier = maxSubdivision;
				this.minSubdivisionMultiplier = minSubdivision;
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x000222CA File Offset: 0x000204CA
			public Volume(Vector3 corner, Vector3 X, Vector3 Y, Vector3 Z, float maxSubdivision = 1f, float minSubdivision = 0f)
			{
				this.corner = corner;
				this.X = X;
				this.Y = Y;
				this.Z = Z;
				this.maxSubdivisionMultiplier = maxSubdivision;
				this.minSubdivisionMultiplier = minSubdivision;
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x000222FC File Offset: 0x000204FC
			public Volume(ProbeReferenceVolume.Volume copy)
			{
				this.X = copy.X;
				this.Y = copy.Y;
				this.Z = copy.Z;
				this.corner = copy.corner;
				this.maxSubdivisionMultiplier = copy.maxSubdivisionMultiplier;
				this.minSubdivisionMultiplier = copy.minSubdivisionMultiplier;
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x00022354 File Offset: 0x00020554
			public Bounds CalculateAABB()
			{
				Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
				for (int i = 0; i < 2; i++)
				{
					for (int j = 0; j < 2; j++)
					{
						for (int k = 0; k < 2; k++)
						{
							Vector3 vector3 = new Vector3((float)i, (float)j, (float)k);
							Vector3 rhs = this.corner + this.X * vector3.x + this.Y * vector3.y + this.Z * vector3.z;
							vector = Vector3.Min(vector, rhs);
							vector2 = Vector3.Max(vector2, rhs);
						}
					}
				}
				return new Bounds((vector + vector2) / 2f, vector2 - vector);
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x0002244C File Offset: 0x0002064C
			public void CalculateCenterAndSize(out Vector3 center, out Vector3 size)
			{
				size = new Vector3(this.X.magnitude, this.Y.magnitude, this.Z.magnitude);
				center = this.corner + this.X * 0.5f + this.Y * 0.5f + this.Z * 0.5f;
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x000224D0 File Offset: 0x000206D0
			public void Transform(Matrix4x4 trs)
			{
				this.corner = trs.MultiplyPoint(this.corner);
				this.X = trs.MultiplyVector(this.X);
				this.Y = trs.MultiplyVector(this.Y);
				this.Z = trs.MultiplyVector(this.Z);
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x0002252C File Offset: 0x0002072C
			public override string ToString()
			{
				return string.Format("Corner: {0}, X: {1}, Y: {2}, Z: {3}, MaxSubdiv: {4}", new object[]
				{
					this.corner,
					this.X,
					this.Y,
					this.Z,
					this.maxSubdivisionMultiplier
				});
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x00022590 File Offset: 0x00020790
			public bool Equals(ProbeReferenceVolume.Volume other)
			{
				return this.corner == other.corner && this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.minSubdivisionMultiplier == other.minSubdivisionMultiplier && this.maxSubdivisionMultiplier == other.maxSubdivisionMultiplier;
			}

			// Token: 0x04000491 RID: 1169
			internal Vector3 corner;

			// Token: 0x04000492 RID: 1170
			internal Vector3 X;

			// Token: 0x04000493 RID: 1171
			internal Vector3 Y;

			// Token: 0x04000494 RID: 1172
			internal Vector3 Z;

			// Token: 0x04000495 RID: 1173
			internal float maxSubdivisionMultiplier;

			// Token: 0x04000496 RID: 1174
			internal float minSubdivisionMultiplier;
		}

		// Token: 0x02000119 RID: 281
		internal struct RefVolTransform
		{
			// Token: 0x04000497 RID: 1175
			public Matrix4x4 refSpaceToWS;

			// Token: 0x04000498 RID: 1176
			public Vector3 posWS;

			// Token: 0x04000499 RID: 1177
			public Quaternion rot;

			// Token: 0x0400049A RID: 1178
			public float scale;
		}

		// Token: 0x0200011A RID: 282
		public struct RuntimeResources
		{
			// Token: 0x0400049B RID: 1179
			public ComputeBuffer index;

			// Token: 0x0400049C RID: 1180
			public ComputeBuffer cellIndices;

			// Token: 0x0400049D RID: 1181
			public Texture3D L0_L1rx;

			// Token: 0x0400049E RID: 1182
			public Texture3D L1_G_ry;

			// Token: 0x0400049F RID: 1183
			public Texture3D L1_B_rz;

			// Token: 0x040004A0 RID: 1184
			public Texture3D L2_0;

			// Token: 0x040004A1 RID: 1185
			public Texture3D L2_1;

			// Token: 0x040004A2 RID: 1186
			public Texture3D L2_2;

			// Token: 0x040004A3 RID: 1187
			public Texture3D L2_3;
		}

		// Token: 0x0200011B RID: 283
		internal struct RegId
		{
			// Token: 0x060007FA RID: 2042 RVA: 0x00022607 File Offset: 0x00020807
			public bool IsValid()
			{
				return this.id != 0;
			}

			// Token: 0x060007FB RID: 2043 RVA: 0x00022612 File Offset: 0x00020812
			public void Invalidate()
			{
				this.id = 0;
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x0002261B File Offset: 0x0002081B
			public static bool operator ==(ProbeReferenceVolume.RegId lhs, ProbeReferenceVolume.RegId rhs)
			{
				return lhs.id == rhs.id;
			}

			// Token: 0x060007FD RID: 2045 RVA: 0x0002262B File Offset: 0x0002082B
			public static bool operator !=(ProbeReferenceVolume.RegId lhs, ProbeReferenceVolume.RegId rhs)
			{
				return lhs.id != rhs.id;
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x0002263E File Offset: 0x0002083E
			public override bool Equals(object obj)
			{
				return obj != null && base.GetType().Equals(obj.GetType()) && (ProbeReferenceVolume.RegId)obj == this;
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x00022673 File Offset: 0x00020873
			public override int GetHashCode()
			{
				return this.id;
			}

			// Token: 0x040004A4 RID: 1188
			internal int id;
		}

		// Token: 0x0200011C RID: 284
		public struct ExtraDataActionInput
		{
		}

		// Token: 0x0200011D RID: 285
		private struct InitInfo
		{
			// Token: 0x040004A5 RID: 1189
			public Vector3Int pendingMinCellPosition;

			// Token: 0x040004A6 RID: 1190
			public Vector3Int pendingMaxCellPosition;
		}

		// Token: 0x0200011E RID: 286
		private class CellInstancedDebugProbes
		{
			// Token: 0x06000800 RID: 2048 RVA: 0x0002267B File Offset: 0x0002087B
			public CellInstancedDebugProbes()
			{
			}

			// Token: 0x040004A7 RID: 1191
			public List<Matrix4x4[]> probeBuffers;

			// Token: 0x040004A8 RID: 1192
			public List<MaterialPropertyBlock> props;

			// Token: 0x040004A9 RID: 1193
			public Hash128 cellHash;

			// Token: 0x040004AA RID: 1194
			public Vector3 cellPosition;
		}

		// Token: 0x0200011F RID: 287
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000801 RID: 2049 RVA: 0x00022683 File Offset: 0x00020883
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x0002268F File Offset: 0x0002088F
			public <>c()
			{
			}

			// Token: 0x06000803 RID: 2051 RVA: 0x00022697 File Offset: 0x00020897
			internal float <RegisterDebug>b__119_6()
			{
				return 0f;
			}

			// Token: 0x06000804 RID: 2052 RVA: 0x0002269E File Offset: 0x0002089E
			internal float <RegisterDebug>b__119_15()
			{
				return 0.1f;
			}

			// Token: 0x06000805 RID: 2053 RVA: 0x000226A5 File Offset: 0x000208A5
			internal float <RegisterDebug>b__119_16()
			{
				return 10f;
			}

			// Token: 0x06000806 RID: 2054 RVA: 0x000226AC File Offset: 0x000208AC
			internal float <RegisterDebug>b__119_21()
			{
				return 0f;
			}

			// Token: 0x06000807 RID: 2055 RVA: 0x000226B3 File Offset: 0x000208B3
			internal int <RegisterDebug>b__119_24()
			{
				return 0;
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x000226B6 File Offset: 0x000208B6
			internal int <RegisterDebug>b__119_25()
			{
				return ProbeReferenceVolume.instance.GetMaxSubdivision();
			}

			// Token: 0x040004AB RID: 1195
			public static readonly ProbeReferenceVolume.<>c <>9 = new ProbeReferenceVolume.<>c();

			// Token: 0x040004AC RID: 1196
			public static Func<float> <>9__119_6;

			// Token: 0x040004AD RID: 1197
			public static Func<float> <>9__119_15;

			// Token: 0x040004AE RID: 1198
			public static Func<float> <>9__119_16;

			// Token: 0x040004AF RID: 1199
			public static Func<float> <>9__119_21;

			// Token: 0x040004B0 RID: 1200
			public static Func<int> <>9__119_24;

			// Token: 0x040004B1 RID: 1201
			public static Func<int> <>9__119_25;
		}
	}
}
