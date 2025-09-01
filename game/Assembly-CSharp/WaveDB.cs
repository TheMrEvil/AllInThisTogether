using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class WaveDB : ScriptableObject
{
	// Token: 0x060002DD RID: 733 RVA: 0x00018A30 File Offset: 0x00016C30
	public Wave_PlayerValues GetPlayerValues(int PlayerCount)
	{
		foreach (Wave_PlayerValues wave_PlayerValues in this.PlayerValues)
		{
			if (wave_PlayerValues.PlayerCount == PlayerCount)
			{
				return wave_PlayerValues;
			}
		}
		if (PlayerCount > 4)
		{
			List<Wave_PlayerValues> playerValues = this.PlayerValues;
			int index = playerValues.Count - 1;
			return playerValues[index];
		}
		return this.PlayerValues[0];
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00018AB4 File Offset: 0x00016CB4
	public static GameMap GetMap()
	{
		return AIManager.instance.Waves.GetValidMap();
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00018AC8 File Offset: 0x00016CC8
	public GameMap GetMapByName(string mapName)
	{
		mapName = mapName.ToLower().Replace("_", " ");
		foreach (GameMap gameMap in this.Maps)
		{
			if (gameMap.Name.ToLower().Equals(mapName))
			{
				return gameMap;
			}
		}
		return null;
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00018B48 File Offset: 0x00016D48
	public Vignette GetVignetteByName(string mapName)
	{
		mapName = mapName.ToLower().Replace("_", " ");
		foreach (Vignette vignette in this.Vignettes)
		{
			if (vignette.Name.ToLower().Equals(mapName))
			{
				return vignette;
			}
		}
		return null;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x00018BC8 File Offset: 0x00016DC8
	public static List<AugmentTree> GetGlobalBindings(int bindingLevel, int appendixLevel)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (WaveDB.GlobalBinding globalBinding in AIManager.instance.Waves.GlobalBindings)
		{
			if (globalBinding.ActiveAt <= bindingLevel || globalBinding.AppendixLevel <= appendixLevel)
			{
				list.Add(globalBinding.Binding);
			}
		}
		return list;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00018C44 File Offset: 0x00016E44
	public static AugmentTree GetAppendixBinding(int appendix)
	{
		appendix--;
		if (appendix < 0)
		{
			appendix = 0;
		}
		return AIManager.instance.Waves.AppendixSimpleBindings[Mathf.Min(appendix, AIManager.instance.Waves.AppendixSimpleBindings.Count - 1)].Binding;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00018C92 File Offset: 0x00016E92
	private GameMap GetValidMap()
	{
		return null;
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00018C95 File Offset: 0x00016E95
	public void EditorButton()
	{
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00018C97 File Offset: 0x00016E97
	public WaveDB()
	{
	}

	// Token: 0x040002C2 RID: 706
	public List<Wave_PlayerValues> PlayerValues;

	// Token: 0x040002C3 RID: 707
	public EnemyScalingData EnemyScaling;

	// Token: 0x040002C4 RID: 708
	public bool UseMapBiomes;

	// Token: 0x040002C5 RID: 709
	public List<GameMap> Maps;

	// Token: 0x040002C6 RID: 710
	public List<Vignette> Vignettes;

	// Token: 0x040002C7 RID: 711
	public List<WaveDB.GlobalBinding> GlobalBindings;

	// Token: 0x040002C8 RID: 712
	public List<WaveDB.AppendixBinding> AppendixSimpleBindings;

	// Token: 0x02000475 RID: 1141
	[Serializable]
	public class GlobalBinding
	{
		// Token: 0x06002195 RID: 8597 RVA: 0x000C34CD File Offset: 0x000C16CD
		public GlobalBinding()
		{
		}

		// Token: 0x040022AF RID: 8879
		public AugmentTree Binding;

		// Token: 0x040022B0 RID: 8880
		public int ActiveAt;

		// Token: 0x040022B1 RID: 8881
		public int AppendixLevel;
	}

	// Token: 0x02000476 RID: 1142
	[Serializable]
	public class AppendixBinding
	{
		// Token: 0x06002196 RID: 8598 RVA: 0x000C34D5 File Offset: 0x000C16D5
		public AppendixBinding()
		{
		}

		// Token: 0x040022B2 RID: 8882
		public AugmentTree Binding;

		// Token: 0x040022B3 RID: 8883
		public int Value;
	}
}
