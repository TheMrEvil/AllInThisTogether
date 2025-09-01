using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.TerrainUtils
{
	// Token: 0x0200001B RID: 27
	public class TerrainMap
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000433C File Offset: 0x0000253C
		public Terrain GetTerrain(int tileX, int tileZ)
		{
			Terrain result = null;
			this.m_terrainTiles.TryGetValue(new TerrainTileCoord(tileX, tileZ), out result);
			return result;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004368 File Offset: 0x00002568
		public static TerrainMap CreateFromConnectedNeighbors(Terrain originTerrain, Predicate<Terrain> filter = null, bool fullValidation = true)
		{
			bool flag = originTerrain == null;
			TerrainMap result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = originTerrain.terrainData == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					TerrainMap terrainMap = new TerrainMap();
					Queue<TerrainMap.QueueElement> queue = new Queue<TerrainMap.QueueElement>();
					queue.Enqueue(new TerrainMap.QueueElement(0, 0, originTerrain));
					int num = Terrain.activeTerrains.Length;
					while (queue.Count > 0)
					{
						TerrainMap.QueueElement queueElement = queue.Dequeue();
						bool flag3 = filter == null || filter(queueElement.terrain);
						if (flag3)
						{
							bool flag4 = terrainMap.TryToAddTerrain(queueElement.tileX, queueElement.tileZ, queueElement.terrain);
							if (flag4)
							{
								bool flag5 = terrainMap.m_terrainTiles.Count > num;
								if (flag5)
								{
									break;
								}
								bool flag6 = queueElement.terrain.leftNeighbor != null;
								if (flag6)
								{
									queue.Enqueue(new TerrainMap.QueueElement(queueElement.tileX - 1, queueElement.tileZ, queueElement.terrain.leftNeighbor));
								}
								bool flag7 = queueElement.terrain.bottomNeighbor != null;
								if (flag7)
								{
									queue.Enqueue(new TerrainMap.QueueElement(queueElement.tileX, queueElement.tileZ - 1, queueElement.terrain.bottomNeighbor));
								}
								bool flag8 = queueElement.terrain.rightNeighbor != null;
								if (flag8)
								{
									queue.Enqueue(new TerrainMap.QueueElement(queueElement.tileX + 1, queueElement.tileZ, queueElement.terrain.rightNeighbor));
								}
								bool flag9 = queueElement.terrain.topNeighbor != null;
								if (flag9)
								{
									queue.Enqueue(new TerrainMap.QueueElement(queueElement.tileX, queueElement.tileZ + 1, queueElement.terrain.topNeighbor));
								}
							}
						}
					}
					if (fullValidation)
					{
						terrainMap.Validate();
					}
					result = terrainMap;
				}
			}
			return result;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000455C File Offset: 0x0000275C
		public static TerrainMap CreateFromPlacement(Terrain originTerrain, Predicate<Terrain> filter = null, bool fullValidation = true)
		{
			bool flag = Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0 || originTerrain == null;
			TerrainMap result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = originTerrain.terrainData == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					int groupID = originTerrain.groupingID;
					float x3 = originTerrain.transform.position.x;
					float z = originTerrain.transform.position.z;
					float x2 = originTerrain.terrainData.size.x;
					float z2 = originTerrain.terrainData.size.z;
					bool flag3 = filter == null;
					if (flag3)
					{
						filter = ((Terrain x) => x.groupingID == groupID);
					}
					result = TerrainMap.CreateFromPlacement(new Vector2(x3, z), new Vector2(x2, z2), filter, fullValidation);
				}
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004638 File Offset: 0x00002838
		public static TerrainMap CreateFromPlacement(Vector2 gridOrigin, Vector2 gridSize, Predicate<Terrain> filter = null, bool fullValidation = true)
		{
			bool flag = Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0;
			TerrainMap result;
			if (flag)
			{
				result = null;
			}
			else
			{
				TerrainMap terrainMap = new TerrainMap();
				float num = 1f / gridSize.x;
				float num2 = 1f / gridSize.y;
				foreach (Terrain terrain in Terrain.activeTerrains)
				{
					bool flag2 = terrain.terrainData == null;
					if (!flag2)
					{
						bool flag3 = filter == null || filter(terrain);
						if (flag3)
						{
							Vector3 position = terrain.transform.position;
							int tileX = Mathf.RoundToInt((position.x - gridOrigin.x) * num);
							int tileZ = Mathf.RoundToInt((position.z - gridOrigin.y) * num2);
							terrainMap.TryToAddTerrain(tileX, tileZ, terrain);
						}
					}
				}
				if (fullValidation)
				{
					terrainMap.Validate();
				}
				result = ((terrainMap.m_terrainTiles.Count > 0) ? terrainMap : null);
			}
			return result;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00004749 File Offset: 0x00002949
		public Dictionary<TerrainTileCoord, Terrain> terrainTiles
		{
			get
			{
				return this.m_terrainTiles;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004751 File Offset: 0x00002951
		public TerrainMap()
		{
			this.m_errorCode = TerrainMapStatusCode.OK;
			this.m_terrainTiles = new Dictionary<TerrainTileCoord, Terrain>();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004770 File Offset: 0x00002970
		private void AddTerrainInternal(int x, int z, Terrain terrain)
		{
			bool flag = this.m_terrainTiles.Count == 0;
			if (flag)
			{
				this.m_patchSize = terrain.terrainData.size;
			}
			else
			{
				bool flag2 = terrain.terrainData.size != this.m_patchSize;
				if (flag2)
				{
					this.m_errorCode |= TerrainMapStatusCode.SizeMismatch;
				}
			}
			this.m_terrainTiles.Add(new TerrainTileCoord(x, z), terrain);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000047E4 File Offset: 0x000029E4
		private bool TryToAddTerrain(int tileX, int tileZ, Terrain terrain)
		{
			bool result = false;
			bool flag = terrain != null;
			if (flag)
			{
				Terrain terrain2 = this.GetTerrain(tileX, tileZ);
				bool flag2 = terrain2 != null;
				if (flag2)
				{
					bool flag3 = terrain2 != terrain;
					if (flag3)
					{
						this.m_errorCode |= TerrainMapStatusCode.Overlapping;
					}
				}
				else
				{
					this.AddTerrainInternal(tileX, tileZ, terrain);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000484C File Offset: 0x00002A4C
		private void ValidateTerrain(int tileX, int tileZ)
		{
			Terrain terrain = this.GetTerrain(tileX, tileZ);
			bool flag = terrain != null;
			if (flag)
			{
				Terrain terrain2 = this.GetTerrain(tileX - 1, tileZ);
				Terrain terrain3 = this.GetTerrain(tileX + 1, tileZ);
				Terrain terrain4 = this.GetTerrain(tileX, tileZ + 1);
				Terrain terrain5 = this.GetTerrain(tileX, tileZ - 1);
				bool flag2 = terrain2;
				if (flag2)
				{
					bool flag3 = !Mathf.Approximately(terrain.transform.position.x, terrain2.transform.position.x + terrain2.terrainData.size.x) || !Mathf.Approximately(terrain.transform.position.z, terrain2.transform.position.z);
					if (flag3)
					{
						this.m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
					}
				}
				bool flag4 = terrain3;
				if (flag4)
				{
					bool flag5 = !Mathf.Approximately(terrain.transform.position.x + terrain.terrainData.size.x, terrain3.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, terrain3.transform.position.z);
					if (flag5)
					{
						this.m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
					}
				}
				bool flag6 = terrain4;
				if (flag6)
				{
					bool flag7 = !Mathf.Approximately(terrain.transform.position.x, terrain4.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z + terrain.terrainData.size.z, terrain4.transform.position.z);
					if (flag7)
					{
						this.m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
					}
				}
				bool flag8 = terrain5;
				if (flag8)
				{
					bool flag9 = !Mathf.Approximately(terrain.transform.position.x, terrain5.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, terrain5.transform.position.z + terrain5.terrainData.size.z);
					if (flag9)
					{
						this.m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
					}
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004AC8 File Offset: 0x00002CC8
		private TerrainMapStatusCode Validate()
		{
			foreach (TerrainTileCoord terrainTileCoord in this.m_terrainTiles.Keys)
			{
				this.ValidateTerrain(terrainTileCoord.tileX, terrainTileCoord.tileZ);
			}
			return this.m_errorCode;
		}

		// Token: 0x0400006C RID: 108
		private Vector3 m_patchSize;

		// Token: 0x0400006D RID: 109
		private TerrainMapStatusCode m_errorCode;

		// Token: 0x0400006E RID: 110
		private Dictionary<TerrainTileCoord, Terrain> m_terrainTiles;

		// Token: 0x0200001C RID: 28
		private struct QueueElement
		{
			// Token: 0x0600019E RID: 414 RVA: 0x00004B3C File Offset: 0x00002D3C
			public QueueElement(int tileX, int tileZ, Terrain terrain)
			{
				this.tileX = tileX;
				this.tileZ = tileZ;
				this.terrain = terrain;
			}

			// Token: 0x0400006F RID: 111
			public readonly int tileX;

			// Token: 0x04000070 RID: 112
			public readonly int tileZ;

			// Token: 0x04000071 RID: 113
			public readonly Terrain terrain;
		}

		// Token: 0x0200001D RID: 29
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x0600019F RID: 415 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x00004B5D File Offset: 0x00002D5D
			internal bool <CreateFromPlacement>b__0(Terrain x)
			{
				return x.groupingID == this.groupID;
			}

			// Token: 0x04000072 RID: 114
			public int groupID;
		}
	}
}
