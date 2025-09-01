using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200035E RID: 862
public class GenreRewardNode : Node
{
	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x000AEB38 File Offset: 0x000ACD38
	public int EnemyDifficulty
	{
		get
		{
			if (GameplayManager.HasGameOverride("Page_Difficulty_2"))
			{
				return Mathf.Min(101, this.Difficulty + 60);
			}
			if (GameplayManager.HasGameOverride("Page_Difficulty_1"))
			{
				return Mathf.Min(100, this.Difficulty + 30);
			}
			return this.Difficulty;
		}
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x000AEB85 File Offset: 0x000ACD85
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Rewards",
			MinInspectorSize = new Vector2(200f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x000AEBB4 File Offset: 0x000ACDB4
	public List<AugmentTree> GetEnemyMods()
	{
		GenreRewardNode.RewardType enemyType = this.EnemyType;
		List<AugmentTree> result;
		switch (enemyType)
		{
		case GenreRewardNode.RewardType.Algorithm:
			result = this.EnemyAlgorithmPick();
			break;
		case GenreRewardNode.RewardType.Random:
			result = this.GetRandomAugments(ModType.Enemy, QualitySelector.Any);
			break;
		case GenreRewardNode.RewardType.Explicit:
			result = this.GetExplicitModifiers(this.EnemyReward, ModType.Enemy);
			break;
		case GenreRewardNode.RewardType.None:
			result = new List<AugmentTree>();
			break;
		default:
			throw new SwitchExpressionException(enemyType);
		}
		return result;
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x000AEC1C File Offset: 0x000ACE1C
	public List<AugmentTree> GetPlayerMods()
	{
		GenreRewardNode.RewardType playerType = this.PlayerType;
		List<AugmentTree> result;
		switch (playerType)
		{
		case GenreRewardNode.RewardType.Algorithm:
			result = GenreRewardNode.PlayerAlgorithmReward(this.PlayerRarity, null);
			break;
		case GenreRewardNode.RewardType.Random:
			result = this.GetRandomAugments(ModType.Player, this.PlayerRarity);
			break;
		case GenreRewardNode.RewardType.Explicit:
			result = this.GetExplicitModifiers(this.PlayerReward, ModType.Player);
			break;
		case GenreRewardNode.RewardType.None:
			result = new List<AugmentTree>();
			break;
		default:
			throw new SwitchExpressionException(playerType);
		}
		return result;
	}

	// Token: 0x06001CBC RID: 7356 RVA: 0x000AEC8C File Offset: 0x000ACE8C
	public List<AugmentTree> GetExplicitModifiers(List<Node> choices, ModType modType)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		if (choices == null || choices.Count == 0)
		{
			return list;
		}
		int num = 3;
		if (choices.Count == 1)
		{
			GenreRewardOptionNode genreRewardOptionNode = choices[0] as GenreRewardOptionNode;
			List<string> list2 = new List<string>();
			if (modType == ModType.Enemy)
			{
				foreach (string text in AIManager.GlobalEnemyMods.TreeIDs)
				{
					Debug.Log("Excluding " + GraphDB.GetAugment(text).Root.Name);
					list2.Add(text);
				}
			}
			List<AugmentTree> modifiers = genreRewardOptionNode.GetModifiers(modType, list2);
			int num2 = Mathf.Min(num, modifiers.Count);
			for (int i = list.Count; i < num2; i++)
			{
				AugmentTree augmentTree = GraphDB.ChooseModFromList(modType, modifiers, false, GameplayManager.IsChallengeActive);
				if (augmentTree != null)
				{
					modifiers.Remove(augmentTree);
					list.Add(augmentTree);
				}
				else
				{
					Debug.Log("No valid mods found from option filter: " + genreRewardOptionNode.titleOverride);
				}
			}
		}
		else
		{
			List<string> list3 = new List<string>();
			if (modType == ModType.Enemy)
			{
				foreach (string text2 in AIManager.GlobalEnemyMods.TreeIDs)
				{
					Debug.Log("Excluding " + GraphDB.GetAugment(text2).Root.Name);
					list3.Add(text2);
				}
			}
			for (int j = 0; j < num; j++)
			{
				int index = j % choices.Count;
				GenreRewardOptionNode genreRewardOptionNode2 = choices[index] as GenreRewardOptionNode;
				List<AugmentTree> modifiers2 = genreRewardOptionNode2.GetModifiers(modType, list3);
				foreach (AugmentTree item in list)
				{
					modifiers2.Remove(item);
				}
				AugmentTree augmentTree2 = GraphDB.ChooseModFromList(modType, modifiers2, false, GameplayManager.IsChallengeActive);
				if (augmentTree2 != null)
				{
					list.Add(augmentTree2);
				}
				else
				{
					Debug.Log("No valid mods found from option filter: " + genreRewardOptionNode2.titleOverride);
				}
			}
		}
		return list;
	}

	// Token: 0x06001CBD RID: 7357 RVA: 0x000AEEE4 File Offset: 0x000AD0E4
	private List<AugmentTree> GetRandomAugments(ModType modType, QualitySelector rarity)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		int num = 3;
		List<AugmentTree> list2;
		if (modType != ModType.Player)
		{
			if (modType == ModType.Enemy)
			{
				list2 = GraphDB.GetValidMods(ModType.Enemy);
			}
			else
			{
				list2 = new List<AugmentTree>();
			}
		}
		else
		{
			list2 = GraphDB.GetValidMods(ModType.Player);
		}
		List<AugmentTree> list3 = list2;
		for (int i = list3.Count - 1; i >= 0; i--)
		{
			if (list3[i].Root.Filters.HasFilter(ModFilter.Enemy_Boss))
			{
				list3.RemoveAt(i);
			}
			else if (!rarity.Matches(list3[i].Root.DisplayQuality))
			{
				list3.RemoveAt(i);
			}
		}
		for (int j = 0; j < num; j++)
		{
			AugmentTree augmentTree = GraphDB.ChooseModFromList(modType, list3, false, GameplayManager.IsChallengeActive);
			if (augmentTree != null)
			{
				list3.Remove(augmentTree);
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06001CBE RID: 7358 RVA: 0x000AEFB8 File Offset: 0x000AD1B8
	private List<AugmentTree> EnemyAlgorithmPick()
	{
		List<AugmentTree> list = new List<AugmentTree>();
		int num = 3;
		List<AugmentTree> validMods = GraphDB.GetValidMods(ModType.Enemy);
		ModDifficultyNode.FilterDifficultyMods(ref validMods, this.EnemyDifficulty);
		for (int i = validMods.Count - 1; i >= 0; i--)
		{
			if (validMods[i].Root.Filters.HasFilter(ModFilter.Enemy_Boss))
			{
				validMods.RemoveAt(i);
			}
		}
		for (int j = 0; j < num; j++)
		{
			AugmentTree augmentTree = GraphDB.ChooseModFromList(ModType.Enemy, validMods, false, GameplayManager.IsChallengeActive);
			validMods.Remove(augmentTree);
			if (augmentTree != null)
			{
				list.Add(augmentTree);
			}
		}
		if (list.Count != num)
		{
			Debug.LogError(string.Format("Couldn't find enough Enemy Augments (Got {0}) - Filling with randoms!", list.Count));
			List<AugmentTree> randomAugments = this.GetRandomAugments(ModType.Enemy, QualitySelector.Any);
			for (int k = list.Count; k < num; k++)
			{
				list.Add(randomAugments[k]);
			}
		}
		return list;
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x000AF0A8 File Offset: 0x000AD2A8
	public static List<AugmentTree> PlayerAlgorithmReward(QualitySelector rarity = QualitySelector.Any, List<AugmentTree> Ignore = null)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		int num = 3;
		int num2 = 2;
		int num3 = 0;
		for (int i = 0; i < num; i++)
		{
			AugmentFilter augmentFilter = GenreRewardNode.GetPlayerAlgorithmFilter();
			num3++;
			if (num3 > num2)
			{
				augmentFilter = null;
			}
			List<AugmentTree> list2 = (augmentFilter != null) ? augmentFilter.GetModifiers(ModType.Player, null) : GraphDB.GetValidMods(ModType.Player);
			for (int j = list2.Count - 1; j >= 0; j--)
			{
				if (!rarity.Matches(list2[j].Root.DisplayQuality))
				{
					list2.RemoveAt(j);
				}
			}
			if (Ignore != null)
			{
				foreach (AugmentTree item in Ignore)
				{
					list2.Remove(item);
				}
			}
			foreach (AugmentTree item2 in list)
			{
				list2.Remove(item2);
			}
			AugmentTree augmentTree = GraphDB.ChooseModFromList(ModType.Player, list2, false, GameplayManager.IsChallengeActive);
			if (augmentTree == null)
			{
				augmentTree = GenreRewardNode.GetRandomPlayerMod(rarity, list);
			}
			if (augmentTree == null)
			{
				augmentTree = GenreRewardNode.GetRandomPlayerMod(QualitySelector.Uncommon | QualitySelector.Rare, list);
			}
			if (augmentTree != null)
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x000AF210 File Offset: 0x000AD410
	private static AugmentTree GetRandomPlayerMod(QualitySelector rarity, List<AugmentTree> already = null)
	{
		if (already == null)
		{
			already = new List<AugmentTree>();
		}
		List<AugmentTree> validMods = GraphDB.GetValidMods(ModType.Player);
		foreach (AugmentTree augmentTree in already)
		{
			validMods.Remove(already);
		}
		for (int i = validMods.Count - 1; i >= 0; i--)
		{
			if (!rarity.Matches(validMods[i].Root.DisplayQuality))
			{
				validMods.RemoveAt(i);
			}
		}
		return GraphDB.ChooseModFromList(ModType.Player, validMods, false, GameplayManager.IsChallengeActive);
	}

	// Token: 0x06001CC1 RID: 7361 RVA: 0x000AF2B0 File Offset: 0x000AD4B0
	private static AugmentFilter GetPlayerAlgorithmFilter()
	{
		Augments augments = PlayerControl.myInstance.AllAugments(false, null);
		List<ModFilter> list = new List<ModFilter>();
		foreach (ModFilter item in GenreRewardNode.AlwaysFilters)
		{
			list.Add(item);
		}
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in augments.trees)
		{
			AugmentRootNode augmentRootNode;
			int num;
			keyValuePair.Deconstruct(out augmentRootNode, out num);
			foreach (ModFilter item2 in augmentRootNode.Filters.Filters)
			{
				if (!GenreRewardNode.IgnoreFilters.Contains(item2))
				{
					list.Add(item2);
				}
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		AugmentFilter augmentFilter = new AugmentFilter();
		augmentFilter.RestrictTags = true;
		augmentFilter.IncludeAnyTag = true;
		augmentFilter.IncludeModFilter = new ModFilterList();
		augmentFilter.IncludeModFilter.Filters = new List<ModFilter>();
		ModFilter item3 = list[GameplayManager.IsChallengeActive ? PlayerControl.myInstance.GetRandom(0, list.Count) : UnityEngine.Random.Range(0, list.Count)];
		augmentFilter.IncludeModFilter.Filters.Add(item3);
		return augmentFilter;
	}

	// Token: 0x06001CC2 RID: 7362 RVA: 0x000AF430 File Offset: 0x000AD630
	public bool HasReward(GameState phase)
	{
		switch (phase)
		{
		case GameState.Reward_Player:
			return this.PlayerType != GenreRewardNode.RewardType.None;
		case GameState.Reward_Fountain:
			return true;
		case GameState.Reward_FontPages:
			return InkManager.FontPagesOwed > 0;
		case GameState.Reward_Map:
			return true;
		case GameState.Reward_PreEnemy:
			return true;
		case GameState.Reward_Enemy:
			return this.EnemyType != GenreRewardNode.RewardType.None;
		}
		return false;
	}

	// Token: 0x06001CC3 RID: 7363 RVA: 0x000AF49B File Offset: 0x000AD69B
	public bool ExplicitPlayerReward()
	{
		return this.PlayerType == GenreRewardNode.RewardType.Explicit;
	}

	// Token: 0x06001CC4 RID: 7364 RVA: 0x000AF4A6 File Offset: 0x000AD6A6
	public bool ExplicitEnemyReward()
	{
		return this.EnemyType == GenreRewardNode.RewardType.Explicit;
	}

	// Token: 0x06001CC5 RID: 7365 RVA: 0x000AF4B1 File Offset: 0x000AD6B1
	public GenreRewardNode()
	{
	}

	// Token: 0x06001CC6 RID: 7366 RVA: 0x000AF4E0 File Offset: 0x000AD6E0
	// Note: this type is marked as 'beforefieldinit'.
	static GenreRewardNode()
	{
	}

	// Token: 0x04001D70 RID: 7536
	[ShowPort("ExplicitPlayerReward")]
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreRewardOptionNode), true, "Player", PortLocation.Default)]
	public List<Node> PlayerReward = new List<Node>();

	// Token: 0x04001D71 RID: 7537
	[ShowPort("ExplicitEnemyReward")]
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(GenreRewardOptionNode), true, "Enemy", PortLocation.Default)]
	public List<Node> EnemyReward = new List<Node>();

	// Token: 0x04001D72 RID: 7538
	public GenreRewardNode.RewardType PlayerType;

	// Token: 0x04001D73 RID: 7539
	public QualitySelector PlayerRarity = QualitySelector.Any;

	// Token: 0x04001D74 RID: 7540
	public bool NoFountainPoints;

	// Token: 0x04001D75 RID: 7541
	public GenreRewardNode.RewardType EnemyType;

	// Token: 0x04001D76 RID: 7542
	[Range(0f, 100f)]
	[SerializeField]
	private int Difficulty = 50;

	// Token: 0x04001D77 RID: 7543
	private static readonly List<ModFilter> AlwaysFilters = new List<ModFilter>
	{
		ModFilter.Mech_Keyword,
		ModFilter.Player_Primary,
		ModFilter.Player_Secondary,
		ModFilter.Player_Movement,
		ModFilter.AbilityChanger,
		ModFilter.Player_CoreAbility,
		ModFilter.Player_CoreKeyword
	};

	// Token: 0x04001D78 RID: 7544
	private static readonly HashSet<ModFilter> IgnoreFilters = new HashSet<ModFilter>
	{
		ModFilter.General_Offense,
		ModFilter.General_Defense,
		ModFilter.General_Movement,
		ModFilter.Mech_Statmod,
		ModFilter.Mech_Tradeoff,
		ModFilter.General_Simple,
		ModFilter.Mech_AddKeyword
	};

	// Token: 0x02000672 RID: 1650
	public enum FillMode
	{
		// Token: 0x04002B93 RID: 11155
		Fill,
		// Token: 0x04002B94 RID: 11156
		Duplicate,
		// Token: 0x04002B95 RID: 11157
		RepeatSelection,
		// Token: 0x04002B96 RID: 11158
		None
	}

	// Token: 0x02000673 RID: 1651
	public enum RewardType
	{
		// Token: 0x04002B98 RID: 11160
		Algorithm,
		// Token: 0x04002B99 RID: 11161
		Random,
		// Token: 0x04002B9A RID: 11162
		Explicit,
		// Token: 0x04002B9B RID: 11163
		None
	}
}
