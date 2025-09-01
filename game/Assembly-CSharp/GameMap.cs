using System;
using System.Collections.Generic;

// Token: 0x0200005C RID: 92
[Serializable]
public class GameMap
{
	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060002E6 RID: 742 RVA: 0x00018C9F File Offset: 0x00016E9F
	public bool IsMultiBiome
	{
		get
		{
			return (this.MapBiome & this.MapBiome - 1) > GameMap.Biome.None;
		}
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00018CB3 File Offset: 0x00016EB3
	private string HeaderText()
	{
		if (this.Scene != null)
		{
			return this.Scene.SceneName;
		}
		return "UNDEFINED";
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00018CD0 File Offset: 0x00016ED0
	public GameMap.Biome GetOtherBiome(GameMap.Biome ignored)
	{
		if (!this.IsMultiBiome)
		{
			return GameMap.Biome.None;
		}
		List<GameMap.Biome> list = new List<GameMap.Biome>();
		foreach (object obj in Enum.GetValues(typeof(GameMap.Biome)))
		{
			GameMap.Biome biome = (GameMap.Biome)obj;
			if (biome != GameMap.Biome.None && (this.MapBiome & biome) != GameMap.Biome.None && (ignored & biome) == GameMap.Biome.None)
			{
				list.Add(biome);
			}
		}
		if (list.Count == 0)
		{
			return GameMap.Biome.None;
		}
		return list[MapManager.GetRandom(0, list.Count)];
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x00018D70 File Offset: 0x00016F70
	public GameMap()
	{
	}

	// Token: 0x040002C9 RID: 713
	public string Name;

	// Token: 0x040002CA RID: 714
	public SceneField Scene;

	// Token: 0x040002CB RID: 715
	public GameMap.Biome MapBiome;

	// Token: 0x040002CC RID: 716
	public GameMap.MapSize Size;

	// Token: 0x040002CD RID: 717
	public GameMap.MapType Type;

	// Token: 0x040002CE RID: 718
	public Rarity Rarity;

	// Token: 0x040002CF RID: 719
	public bool SupportsBoss;

	// Token: 0x02000477 RID: 1143
	public enum MapSize
	{
		// Token: 0x040022B5 RID: 8885
		Tiny,
		// Token: 0x040022B6 RID: 8886
		Small,
		// Token: 0x040022B7 RID: 8887
		Medium,
		// Token: 0x040022B8 RID: 8888
		Large,
		// Token: 0x040022B9 RID: 8889
		Giant
	}

	// Token: 0x02000478 RID: 1144
	public enum MapType
	{
		// Token: 0x040022BB RID: 8891
		Default,
		// Token: 0x040022BC RID: 8892
		BossArena
	}

	// Token: 0x02000479 RID: 1145
	[Flags]
	public enum Biome
	{
		// Token: 0x040022BE RID: 8894
		None = 0,
		// Token: 0x040022BF RID: 8895
		Forest = 1,
		// Token: 0x040022C0 RID: 8896
		Desert = 2,
		// Token: 0x040022C1 RID: 8897
		Snow = 4,
		// Token: 0x040022C2 RID: 8898
		Cave = 8,
		// Token: 0x040022C3 RID: 8899
		Island = 16,
		// Token: 0x040022C4 RID: 8900
		_ = 32,
		// Token: 0x040022C5 RID: 8901
		__ = 64,
		// Token: 0x040022C6 RID: 8902
		___ = 128,
		// Token: 0x040022C7 RID: 8903
		Library = 256,
		// Token: 0x040022C8 RID: 8904
		Any = 31
	}
}
