using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class GenreMapNode : Node
{
	// Token: 0x06001CAB RID: 7339 RVA: 0x000AE76D File Offset: 0x000AC96D
	public static GameMap GetMap(GenreWaveNode.NextMapType mapType, bool allowMultiBiome, Node explicitNode = null)
	{
		if (mapType == GenreWaveNode.NextMapType.Explicit && explicitNode != null)
		{
			return (explicitNode as GenreMapNode).GetMap(allowMultiBiome);
		}
		if (mapType == GenreWaveNode.NextMapType.Random || mapType == GenreWaveNode.NextMapType._)
		{
			return GenreMapNode.GetMap(false, allowMultiBiome, null);
		}
		return null;
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x000AE79C File Offset: 0x000AC99C
	public static GameMap GetMap(bool allowDuplicates, bool allowMultiBiome, GameMapFilter filter = null)
	{
		List<GameMap> list = new List<GameMap>();
		foreach (GameMap gameMap in AIManager.instance.Waves.Maps)
		{
			if ((gameMap.MapBiome.AnyFlagsMatch(MapManager.TomeBiome) || gameMap.MapBiome == GameMap.Biome.Any || !AIManager.instance.Waves.UseMapBiomes) && (!gameMap.IsMultiBiome || allowMultiBiome || !AIManager.instance.Waves.UseMapBiomes) && (!MapManager.UsedMaps.Contains(gameMap.Scene.SceneName) || allowDuplicates) && (filter != null || gameMap.Type == GameMap.MapType.Default) && (filter != null || gameMap.Rarity != Rarity.Explicit))
			{
				list.Add(gameMap);
			}
		}
		if (list.Count > 1)
		{
			list.RemoveAll((GameMap x) => x.Scene.SceneName == MapManager.LastMap);
		}
		if (filter != null)
		{
			filter.FilterList(list);
		}
		if (list.Count == 0)
		{
			return null;
		}
		return GenreMapNode.ChooseMapFromList(list);
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x000AE8C8 File Offset: 0x000ACAC8
	public GameMap GetMap(bool allowMultiBiome)
	{
		return GenreMapNode.GetMap(this.AllowDuplicates, allowMultiBiome, this.Filter);
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x000AE8DC File Offset: 0x000ACADC
	private static GameMap ChooseMapFromList(List<GameMap> options)
	{
		if (options.Count == 0)
		{
			return null;
		}
		List<GameMap> list = new List<GameMap>();
		foreach (GameMap gameMap in options)
		{
			int num = (int)(100f / (float)GameDB.Rarity(gameMap.Rarity).RelativeChance);
			for (int i = 0; i < num; i++)
			{
				list.Add(gameMap);
			}
		}
		return list[MapManager.GetRandom(0, list.Count)];
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x000AE978 File Offset: 0x000ACB78
	public override void OnCloned()
	{
		this.Filter = this.Filter.Copy();
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x000AE98B File Offset: 0x000ACB8B
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Map Filter",
			AllowMultipleInputs = true,
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x000AE9B9 File Offset: 0x000ACBB9
	public GenreMapNode()
	{
	}

	// Token: 0x04001D63 RID: 7523
	public bool AllowDuplicates;

	// Token: 0x04001D64 RID: 7524
	public GameMapFilter Filter;

	// Token: 0x02000671 RID: 1649
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x060027C4 RID: 10180 RVA: 0x000D6FEF File Offset: 0x000D51EF
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000D6FFB File Offset: 0x000D51FB
		public <>c()
		{
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000D7003 File Offset: 0x000D5203
		internal bool <GetMap>b__3_0(GameMap x)
		{
			return x.Scene.SceneName == MapManager.LastMap;
		}

		// Token: 0x04002B90 RID: 11152
		public static readonly GenreMapNode.<>c <>9 = new GenreMapNode.<>c();

		// Token: 0x04002B91 RID: 11153
		public static Predicate<GameMap> <>9__3_0;
	}
}
