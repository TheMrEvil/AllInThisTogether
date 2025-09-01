using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.TerrainUtils
{
	// Token: 0x0200001E RID: 30
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainUtility
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00004B70 File Offset: 0x00002D70
		internal static bool ValidTerrainsExist()
		{
			return Terrain.activeTerrains != null && Terrain.activeTerrains.Length != 0;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004B98 File Offset: 0x00002D98
		internal static void ClearConnectivity()
		{
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				bool allowAutoConnect = terrain.allowAutoConnect;
				if (allowAutoConnect)
				{
					terrain.SetNeighbors(null, null, null, null);
				}
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004BD8 File Offset: 0x00002DD8
		internal static Dictionary<int, TerrainMap> CollectTerrains(bool onlyAutoConnectedTerrains = true)
		{
			bool flag = !TerrainUtility.ValidTerrainsExist();
			Dictionary<int, TerrainMap> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Dictionary<int, TerrainMap> dictionary = new Dictionary<int, TerrainMap>();
				Terrain[] activeTerrains = Terrain.activeTerrains;
				for (int i = 0; i < activeTerrains.Length; i++)
				{
					Terrain t = activeTerrains[i];
					bool flag2 = onlyAutoConnectedTerrains && !t.allowAutoConnect;
					if (!flag2)
					{
						bool flag3 = !dictionary.ContainsKey(t.groupingID);
						if (flag3)
						{
							TerrainMap terrainMap = TerrainMap.CreateFromPlacement(t, (Terrain x) => x.groupingID == t.groupingID && (!onlyAutoConnectedTerrains || x.allowAutoConnect), true);
							bool flag4 = terrainMap != null;
							if (flag4)
							{
								dictionary.Add(t.groupingID, terrainMap);
							}
						}
					}
				}
				result = ((dictionary.Count != 0) ? dictionary : null);
			}
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004CDC File Offset: 0x00002EDC
		[RequiredByNativeCode]
		public static void AutoConnect()
		{
			bool flag = !TerrainUtility.ValidTerrainsExist();
			if (!flag)
			{
				TerrainUtility.ClearConnectivity();
				Dictionary<int, TerrainMap> dictionary = TerrainUtility.CollectTerrains(true);
				bool flag2 = dictionary == null;
				if (!flag2)
				{
					foreach (KeyValuePair<int, TerrainMap> keyValuePair in dictionary)
					{
						TerrainMap value = keyValuePair.Value;
						foreach (KeyValuePair<TerrainTileCoord, Terrain> keyValuePair2 in value.terrainTiles)
						{
							TerrainTileCoord key = keyValuePair2.Key;
							Terrain terrain = value.GetTerrain(key.tileX, key.tileZ);
							Terrain terrain2 = value.GetTerrain(key.tileX - 1, key.tileZ);
							Terrain terrain3 = value.GetTerrain(key.tileX + 1, key.tileZ);
							Terrain terrain4 = value.GetTerrain(key.tileX, key.tileZ + 1);
							Terrain terrain5 = value.GetTerrain(key.tileX, key.tileZ - 1);
							terrain.SetNeighbors(terrain2, terrain4, terrain3, terrain5);
						}
					}
				}
			}
		}

		// Token: 0x0200001F RID: 31
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x060001A5 RID: 421 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x04000073 RID: 115
			public bool onlyAutoConnectedTerrains;
		}

		// Token: 0x02000020 RID: 32
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_1
		{
			// Token: 0x060001A6 RID: 422 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c__DisplayClass2_1()
			{
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x00004E40 File Offset: 0x00003040
			internal bool <CollectTerrains>b__0(Terrain x)
			{
				return x.groupingID == this.t.groupingID && (!this.CS$<>8__locals1.onlyAutoConnectedTerrains || x.allowAutoConnect);
			}

			// Token: 0x04000074 RID: 116
			public Terrain t;

			// Token: 0x04000075 RID: 117
			public TerrainUtility.<>c__DisplayClass2_0 CS$<>8__locals1;
		}
	}
}
