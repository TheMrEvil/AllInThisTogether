using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class UnlockDB : ScriptableObject
{
	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060002BE RID: 702 RVA: 0x000177F1 File Offset: 0x000159F1
	public static UnlockDB DB
	{
		get
		{
			if (UnlockDB._db == null)
			{
				UnlockDB._db = Resources.Load<UnlockDB>("UnlockDB");
			}
			return UnlockDB._db;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060002BF RID: 703 RVA: 0x00017814 File Offset: 0x00015A14
	private static HashSet<Unlockable> AllUnlocks
	{
		get
		{
			if (UnlockDB._allUnlocks != null && UnlockDB._allUnlocks.Count > 0)
			{
				return UnlockDB._allUnlocks;
			}
			UnlockDB._allUnlocks = new HashSet<Unlockable>();
			foreach (UnlockDB.GenreUnlock item in UnlockDB.DB.Genres)
			{
				UnlockDB._allUnlocks.Add(item);
			}
			foreach (UnlockDB.AbilityUnlock item2 in UnlockDB.DB.Abilities)
			{
				UnlockDB._allUnlocks.Add(item2);
			}
			foreach (UnlockDB.CoreUnlock item3 in UnlockDB.DB.Cores)
			{
				UnlockDB._allUnlocks.Add(item3);
			}
			foreach (UnlockDB.BindingUnlock item4 in UnlockDB.DB.Bindings)
			{
				UnlockDB._allUnlocks.Add(item4);
			}
			foreach (Cosmetic item5 in CosmeticDB.AllCosmetics)
			{
				UnlockDB._allUnlocks.Add(item5);
			}
			foreach (NookDB.NookObject item6 in NookDB.DB.AllObjects)
			{
				UnlockDB._allUnlocks.Add(item6);
			}
			return UnlockDB._allUnlocks;
		}
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00017A18 File Offset: 0x00015C18
	public static void Init()
	{
		UnlockDB._allUnlocks = new HashSet<Unlockable>();
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00017A24 File Offset: 0x00015C24
	public static Unlockable GetUnlock(string id)
	{
		foreach (UnlockDB.GenreUnlock genreUnlock in UnlockDB.DB.Genres)
		{
			if (genreUnlock.GUID == id)
			{
				return genreUnlock;
			}
		}
		foreach (UnlockDB.AbilityUnlock abilityUnlock in UnlockDB.DB.Abilities)
		{
			if (abilityUnlock.GUID == id)
			{
				return abilityUnlock;
			}
		}
		foreach (UnlockDB.CoreUnlock coreUnlock in UnlockDB.DB.Cores)
		{
			if (coreUnlock.GUID == id)
			{
				return coreUnlock;
			}
		}
		foreach (UnlockDB.BindingUnlock bindingUnlock in UnlockDB.DB.Bindings)
		{
			if (bindingUnlock.GUID == id)
			{
				return bindingUnlock;
			}
		}
		foreach (NookDB.NookObject nookObject in NookDB.DB.AllObjects)
		{
			if (nookObject.GUID == id)
			{
				return nookObject;
			}
		}
		return CosmeticDB.GetCosmetic(id);
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00017BEC File Offset: 0x00015DEC
	public static UnlockDB.GenreUnlock GetGenreUnlock(GenreTree genre)
	{
		foreach (UnlockDB.GenreUnlock genreUnlock in UnlockDB.DB.Genres)
		{
			if (genreUnlock.Genre == genre)
			{
				return genreUnlock;
			}
		}
		return null;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00017C54 File Offset: 0x00015E54
	public static UnlockDB.AbilityUnlock GetAbilityUnlock(AbilityTree ability)
	{
		foreach (UnlockDB.AbilityUnlock abilityUnlock in UnlockDB.DB.Abilities)
		{
			if (abilityUnlock.Ability == ability)
			{
				return abilityUnlock;
			}
		}
		return null;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00017CBC File Offset: 0x00015EBC
	public static UnlockDB.CoreUnlock GetCoreUnlock(AugmentTree ability)
	{
		foreach (UnlockDB.CoreUnlock coreUnlock in UnlockDB.DB.Cores)
		{
			if (coreUnlock.Core == ability)
			{
				return coreUnlock;
			}
		}
		return null;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x00017D24 File Offset: 0x00015F24
	public static UnlockDB.BindingUnlock GetBindingUnlock(AugmentTree binding)
	{
		foreach (UnlockDB.BindingUnlock bindingUnlock in UnlockDB.DB.Bindings)
		{
			if (bindingUnlock.Binding == binding)
			{
				return bindingUnlock;
			}
		}
		return null;
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00017D8C File Offset: 0x00015F8C
	public static UnlockDB.AugmentUnlock GetGenreAugmentUnlock(AugmentTree augment)
	{
		foreach (UnlockDB.AugmentUnlock augmentUnlock in UnlockDB.DB.GenreAugments)
		{
			if (augmentUnlock.Augment == augment)
			{
				return augmentUnlock;
			}
		}
		return null;
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00017DF4 File Offset: 0x00015FF4
	public static List<Unlockable> GetGenreRewards(GenreTree genre, int bindingLevel = 0)
	{
		List<ValueTuple<Unlockable, int>> list = new List<ValueTuple<Unlockable, int>>();
		foreach (UnlockDB.AugmentUnlock augmentUnlock in UnlockDB.DB.GenreAugments)
		{
			if (!(augmentUnlock.Genre != genre) && (augmentUnlock.BindingLevel <= bindingLevel || bindingLevel < 0))
			{
				list.Add(new ValueTuple<Unlockable, int>(augmentUnlock, augmentUnlock.BindingLevel));
			}
		}
		foreach (Unlockable unlockable in UnlockDB.AllUnlocks)
		{
			if (unlockable.UnlockedBy == Unlockable.UnlockType.TomeReward && !(unlockable.RewardingTome != genre) && (unlockable.AtBinding <= bindingLevel || bindingLevel < 0))
			{
				list.Add(new ValueTuple<Unlockable, int>(unlockable, unlockable.AtBinding));
			}
		}
		list.Sort(([TupleElementNames(new string[]
		{
			"reward",
			"level"
		})] ValueTuple<Unlockable, int> x, [TupleElementNames(new string[]
		{
			"reward",
			"level"
		})] ValueTuple<Unlockable, int> y) => x.Item2.CompareTo(y.Item2));
		List<Unlockable> list2 = new List<Unlockable>();
		foreach (ValueTuple<Unlockable, int> valueTuple in list)
		{
			list2.Add(valueTuple.Item1);
		}
		return list2;
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00017F60 File Offset: 0x00016160
	public static List<Unlockable> GetPrestigeRewards(int prestigeLevel = 0)
	{
		List<ValueTuple<Unlockable, int>> list = new List<ValueTuple<Unlockable, int>>();
		foreach (Unlockable unlockable in UnlockDB.AllUnlocks)
		{
			if (unlockable.UnlockedBy == Unlockable.UnlockType.Prestige && (unlockable.PrestigeLevel <= prestigeLevel || prestigeLevel <= 0))
			{
				list.Add(new ValueTuple<Unlockable, int>(unlockable, unlockable.PrestigeLevel));
			}
		}
		list.Sort(([TupleElementNames(new string[]
		{
			"reward",
			"level"
		})] ValueTuple<Unlockable, int> x, [TupleElementNames(new string[]
		{
			"reward",
			"level"
		})] ValueTuple<Unlockable, int> y) => x.Item2.CompareTo(y.Item2));
		List<Unlockable> list2 = new List<Unlockable>();
		foreach (ValueTuple<Unlockable, int> valueTuple in list)
		{
			list2.Add(valueTuple.Item1);
		}
		return list2;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0001804C File Offset: 0x0001624C
	public static Unlockable GetPrestigeRewardDisplay(int prestigeLevel)
	{
		List<Unlockable> prestigeRewards = UnlockDB.GetPrestigeRewards(prestigeLevel);
		if (prestigeRewards.Count <= 0)
		{
			return null;
		}
		for (int i = prestigeRewards.Count - 1; i >= 0; i--)
		{
			if (prestigeRewards[i].PrestigeLevel == prestigeLevel)
			{
				return prestigeRewards[i];
			}
		}
		return null;
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00018098 File Offset: 0x00016298
	public static List<Unlockable> GetAchivementRewards(string achievementID)
	{
		List<Unlockable> list = new List<Unlockable>();
		foreach (Unlockable unlockable in UnlockDB.AllUnlocks)
		{
			if (unlockable.UnlockedBy == Unlockable.UnlockType.Achievement && !(unlockable.Achievement != achievementID))
			{
				list.Add(unlockable);
			}
		}
		return list;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00018108 File Offset: 0x00016308
	public static Unlockable FindUnlock(string GUID)
	{
		foreach (Unlockable unlockable in UnlockDB.AllUnlocks)
		{
			if (unlockable.GUID == GUID)
			{
				return unlockable;
			}
		}
		return null;
	}

	// Token: 0x060002CC RID: 716 RVA: 0x00018168 File Offset: 0x00016368
	private static UnlockDB.Connection_Core GetConnection(Keyword key)
	{
		foreach (UnlockDB.Connection_Core connection_Core in UnlockDB.DB.CoreConnections)
		{
			if (connection_Core.Keywords.Contains(key))
			{
				return connection_Core;
			}
		}
		return null;
	}

	// Token: 0x060002CD RID: 717 RVA: 0x000181D0 File Offset: 0x000163D0
	private static UnlockDB.Connection_Core GetConnection(MagicColor color)
	{
		foreach (UnlockDB.Connection_Core connection_Core in UnlockDB.DB.CoreConnections)
		{
			if (connection_Core.Color == color)
			{
				return connection_Core;
			}
		}
		return null;
	}

	// Token: 0x060002CE RID: 718 RVA: 0x00018230 File Offset: 0x00016430
	public static bool CanUnlock(AugmentTree augment)
	{
		List<LogicNode> list = new List<LogicNode>();
		if (UnlockDB.GetGenreAugmentUnlock(augment) != null)
		{
			return false;
		}
		if (RaidDB.IsRaidReward(augment))
		{
			return false;
		}
		foreach (Node node in augment.nodes)
		{
			WorldNumberNode worldNumberNode = node as WorldNumberNode;
			if (worldNumberNode != null && worldNumberNode.Stat == WorldNumberNode.WorldNumStat.PlayerCount && PlayerControl.AllPlayers.Count <= 1)
			{
				return false;
			}
			LogicNode logicNode = node as LogicNode;
			if (logicNode != null && (logicNode is Logic_EntityHas || logicNode is Logic_Ability || logicNode is Logic_Genre || logicNode is Logic_Unlocked || logicNode is Logic_Augment || logicNode is Logic_World))
			{
				bool flag = false;
				if (logicNode.TestNode.Count > 0)
				{
					Node node2 = logicNode.TestNode[0];
					int num = 0;
					while (num < 3 && node2 != null)
					{
						if (node2 is RootNode)
						{
							flag = true;
							break;
						}
						LogicNode logicNode2 = node2 as LogicNode;
						if (logicNode2 == null || logicNode2.TestNode.Count <= 0)
						{
							break;
						}
						node2 = logicNode2.TestNode[0];
						num++;
					}
				}
				if (flag)
				{
					list.Add(logicNode);
				}
			}
		}
		if (list.Count == 0)
		{
			return true;
		}
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		foreach (LogicNode logicNode3 in list)
		{
			Logic_Ability logic_Ability = logicNode3 as Logic_Ability;
			if (logic_Ability != null)
			{
				flag2 |= UnlockManager.IsAbilityUnlocked(logic_Ability.Graph);
			}
			else
			{
				Logic_Genre logic_Genre = logicNode3 as Logic_Genre;
				if (logic_Genre != null)
				{
					flag2 |= UnlockManager.IsGenreUnlocked(logic_Genre.Graph);
				}
				else
				{
					Logic_Unlocked logic_Unlocked = logicNode3 as Logic_Unlocked;
					if (logic_Unlocked != null)
					{
						flag3 |= logic_Unlocked.Evaluate(null);
						flag2 = (flag2 || flag3);
						flag4 = true;
					}
					else
					{
						Logic_Augment logic_Augment = logicNode3 as Logic_Augment;
						if (logic_Augment != null && logic_Augment.Test == Logic_Augment.AugmentTest.EntityHas)
						{
							flag2 |= (UnlockManager.IsAugmentUnlocked(logic_Augment.Graph) || UnlockManager.IsCoreUnlocked(logic_Augment.Graph));
						}
						else
						{
							Logic_EntityHas logic_EntityHas = logicNode3 as Logic_EntityHas;
							if (logic_EntityHas != null)
							{
								if (logic_EntityHas.Test == Logic_EntityHas.EntityHasTest.AugmentType && logic_EntityHas.AugmentT == Logic_EntityHas.AugmentFlagTest.ModTag && logic_EntityHas.ModTag.Detail.AbilityFeature == AbilityFeature.Player_Keyword)
								{
									UnlockDB.Connection_Core connection = UnlockDB.GetConnection(logic_EntityHas.ModTag.Detail.Keyword);
									if (connection != null)
									{
										flag2 |= UnlockManager.IsCoreUnlocked(connection.Core);
									}
									else
									{
										flag2 |= UnlockDB.unlockedKeywords.Contains(logic_EntityHas.ModTag.Detail.Keyword);
									}
								}
								else if (logic_EntityHas.Test == Logic_EntityHas.EntityHasTest.ManaElement && logic_EntityHas.manaMagicColor != MagicColor.Neutral)
								{
									UnlockDB.Connection_Core connection2 = UnlockDB.GetConnection(logic_EntityHas.manaMagicColor);
									flag2 |= (connection2 == null || UnlockManager.IsCoreUnlocked(connection2.Core));
								}
								else
								{
									flag2 = true;
								}
							}
						}
					}
				}
			}
		}
		return flag2 && (flag3 || !flag4);
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0001856C File Offset: 0x0001676C
	public static List<UnlockDB.GenreUnlock> GetAvailableTomes()
	{
		List<UnlockDB.GenreUnlock> list = new List<UnlockDB.GenreUnlock>();
		int globalStat = GameStats.GetGlobalStat(GameStats.Stat.MaxBinding, 0);
		int num = 999999;
		foreach (UnlockDB.GenreUnlock genreUnlock in UnlockDB.DB.Genres)
		{
			if (globalStat >= genreUnlock.MinBindingEver && genreUnlock.UnlockedBy == Unlockable.UnlockType.Drop && !UnlockManager.IsGenreUnlocked(genreUnlock.Genre))
			{
				list.Add(genreUnlock);
				if (genreUnlock.PriorityLevel < num)
				{
					num = genreUnlock.PriorityLevel;
				}
			}
		}
		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (list[i].PriorityLevel > num)
			{
				list.RemoveAt(i);
			}
		}
		return list;
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00018640 File Offset: 0x00016840
	public static List<UnlockDB.BindingUnlock> GetAvailableBindings(List<AugmentTree> bindingOverride = null)
	{
		List<UnlockDB.BindingUnlock> list = new List<UnlockDB.BindingUnlock>();
		int globalStat = GameStats.GetGlobalStat(GameStats.Stat.MaxBinding, 0);
		foreach (UnlockDB.BindingUnlock bindingUnlock in UnlockDB.DB.Bindings)
		{
			if (globalStat >= bindingUnlock.MinBindingEver && bindingUnlock.UnlockedBy == Unlockable.UnlockType.Drop && !UnlockManager.IsBindingUnlocked(bindingUnlock.Binding) && (!(bindingUnlock.Parent != null) || UnlockManager.IsBindingUnlocked(bindingUnlock.Parent)))
			{
				if (bindingOverride == null)
				{
					if (bindingUnlock.Parent != null && !GameplayManager.instance.GenreBindings.trees.ContainsKey(bindingUnlock.Parent.Root))
					{
						continue;
					}
				}
				else if (bindingUnlock.Parent != null && !bindingOverride.Contains(bindingUnlock.Parent))
				{
					continue;
				}
				list.Add(bindingUnlock);
			}
		}
		return list;
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x0001873C File Offset: 0x0001693C
	public static void GetKeywords()
	{
		UnlockDB.unlockedKeywords.Clear();
		foreach (AugmentTree augmentTree in GraphDB.instance.PlayerMods)
		{
			if (augmentTree.Root.Rarity != Rarity.Explicit && UnlockManager.IsAugmentUnlocked(augmentTree))
			{
				foreach (ModTag modTag in augmentTree.Root.AddedTags)
				{
					if (modTag.Detail.AbilityFeature == AbilityFeature.Player_Keyword)
					{
						UnlockDB.unlockedKeywords.Add(modTag.Detail.Keyword);
					}
				}
			}
		}
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x00018810 File Offset: 0x00016A10
	public void TestUnlockCheck()
	{
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00018814 File Offset: 0x00016A14
	public static Sprite GetUnlockIcon(Unlockable ul)
	{
		UnlockCategory type = ul.Type;
		foreach (UnlockDB.UnlockIcon unlockIcon in UnlockDB.DB.UnlockIcons)
		{
			if (unlockIcon.Category == type)
			{
				return unlockIcon.Icon;
			}
		}
		return UnlockDB.DB.UnlockIcons[0].Icon;
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00018894 File Offset: 0x00016A94
	public static Sprite GetUnlockIcon(UnlockCategory ul)
	{
		foreach (UnlockDB.UnlockIcon unlockIcon in UnlockDB.DB.UnlockIcons)
		{
			if (unlockIcon.Category == ul)
			{
				return unlockIcon.Icon;
			}
		}
		return UnlockDB.DB.UnlockIcons[0].Icon;
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00018910 File Offset: 0x00016B10
	[return: TupleElementNames(new string[]
	{
		"value",
		"info"
	})]
	public ValueTuple<int, UnlockDB.RewardInfo> GetRewardValue(UnlockDB.RewardType reward, bool didWin)
	{
		UnlockDB.RewardInfo rewardInfo = this.GetRewardInfo(reward);
		if (rewardInfo == null)
		{
			return new ValueTuple<int, UnlockDB.RewardInfo>(-1, null);
		}
		int num = rewardInfo.Value;
		if (num == 0)
		{
			num = this.GetDynamicRewardValue(reward, didWin);
		}
		if (num == 0)
		{
			return new ValueTuple<int, UnlockDB.RewardInfo>(-1, rewardInfo);
		}
		return new ValueTuple<int, UnlockDB.RewardInfo>(num, rewardInfo);
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00018958 File Offset: 0x00016B58
	private UnlockDB.RewardInfo GetRewardInfo(UnlockDB.RewardType reward)
	{
		foreach (UnlockDB.RewardInfo rewardInfo in this.Rewards)
		{
			if (rewardInfo.Reward == reward)
			{
				return rewardInfo;
			}
		}
		return null;
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x000189B4 File Offset: 0x00016BB4
	private int GetDynamicRewardValue(UnlockDB.RewardType reward, bool didWin)
	{
		if (reward != UnlockDB.RewardType.Bindings)
		{
			if (reward != UnlockDB.RewardType.PerWave)
			{
				return 0;
			}
			return WaveManager.instance.WavesCompleted * 10;
		}
		else
		{
			if (didWin)
			{
				return GameplayManager.BindingLevel * 5;
			}
			return Mathf.Min(GameplayManager.BindingLevel, 20);
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x000189E7 File Offset: 0x00016BE7
	public UnlockDB()
	{
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x000189EF File Offset: 0x00016BEF
	// Note: this type is marked as 'beforefieldinit'.
	static UnlockDB()
	{
	}

	// Token: 0x040002B4 RID: 692
	public List<UnlockDB.GenreUnlock> Genres;

	// Token: 0x040002B5 RID: 693
	public List<UnlockDB.AbilityUnlock> Abilities;

	// Token: 0x040002B6 RID: 694
	public List<UnlockDB.CoreUnlock> Cores;

	// Token: 0x040002B7 RID: 695
	public List<UnlockDB.BindingUnlock> Bindings;

	// Token: 0x040002B8 RID: 696
	public List<UnlockDB.AugmentUnlock> GenreAugments;

	// Token: 0x040002B9 RID: 697
	public List<UnlockDB.Connection_Core> CoreConnections;

	// Token: 0x040002BA RID: 698
	public List<UnlockDB.UnlockIcon> UnlockIcons;

	// Token: 0x040002BB RID: 699
	public UnlockDB.DemoUnlockables DemoData;

	// Token: 0x040002BC RID: 700
	public List<UnlockDB.RewardInfo> Rewards;

	// Token: 0x040002BD RID: 701
	private static UnlockDB _db;

	// Token: 0x040002BE RID: 702
	private static HashSet<Unlockable> _allUnlocks;

	// Token: 0x040002BF RID: 703
	private static HashSet<Keyword> unlockedKeywords = new HashSet<Keyword>();

	// Token: 0x02000468 RID: 1128
	[Serializable]
	public class UnlockIcon
	{
		// Token: 0x06002174 RID: 8564 RVA: 0x000C332D File Offset: 0x000C152D
		public UnlockIcon()
		{
		}

		// Token: 0x04002284 RID: 8836
		public UnlockCategory Category;

		// Token: 0x04002285 RID: 8837
		public Sprite Icon;
	}

	// Token: 0x02000469 RID: 1129
	[Serializable]
	public class GenreUnlock : Unlockable
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x000C3335 File Offset: 0x000C1535
		public override string GUID
		{
			get
			{
				GenreTree genre = this.Genre;
				if (genre == null)
				{
					return null;
				}
				return genre.ID;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000C3348 File Offset: 0x000C1548
		public override string UnlockName
		{
			get
			{
				GenreTree genre = this.Genre;
				if (genre == null)
				{
					return null;
				}
				return genre.Root.ShortName;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000C3360 File Offset: 0x000C1560
		public override string CategoryName
		{
			get
			{
				return "Tome";
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x000C3367 File Offset: 0x000C1567
		public override UnlockCategory Type
		{
			get
			{
				return UnlockCategory.Tome;
			}
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000C336A File Offset: 0x000C156A
		public GenreUnlock()
		{
		}

		// Token: 0x04002286 RID: 8838
		public GenreTree Genre;

		// Token: 0x04002287 RID: 8839
		public int MinBindingEver;

		// Token: 0x04002288 RID: 8840
		[Tooltip("Lower values drop first")]
		public int PriorityLevel;

		// Token: 0x04002289 RID: 8841
		public int Abundance = 100;
	}

	// Token: 0x0200046A RID: 1130
	[Serializable]
	public class AbilityUnlock : Unlockable
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x000C337A File Offset: 0x000C157A
		public override string GUID
		{
			get
			{
				AbilityTree ability = this.Ability;
				if (ability == null)
				{
					return null;
				}
				return ability.ID;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x000C338D File Offset: 0x000C158D
		public override string UnlockName
		{
			get
			{
				AbilityTree ability = this.Ability;
				if (ability == null)
				{
					return null;
				}
				return ability.Root.Name;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x000C33A5 File Offset: 0x000C15A5
		public override string CategoryName
		{
			get
			{
				return "Spell";
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600217D RID: 8573 RVA: 0x000C33AC File Offset: 0x000C15AC
		public override UnlockCategory Type
		{
			get
			{
				return UnlockCategory.Ability;
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000C33AF File Offset: 0x000C15AF
		public AbilityUnlock()
		{
		}

		// Token: 0x0400228A RID: 8842
		public AbilityTree Ability;
	}

	// Token: 0x0200046B RID: 1131
	[Serializable]
	public class CoreUnlock : Unlockable
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x000C33B7 File Offset: 0x000C15B7
		public override string GUID
		{
			get
			{
				AugmentTree core = this.Core;
				if (core == null)
				{
					return null;
				}
				return core.ID;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x000C33CA File Offset: 0x000C15CA
		public override string UnlockName
		{
			get
			{
				AugmentTree core = this.Core;
				if (core == null)
				{
					return null;
				}
				return core.Root.Name;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x000C33E2 File Offset: 0x000C15E2
		public override string CategoryName
		{
			get
			{
				return "Signature";
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x000C33E9 File Offset: 0x000C15E9
		public override UnlockCategory Type
		{
			get
			{
				return UnlockCategory.Signature;
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000C33EC File Offset: 0x000C15EC
		public CoreUnlock()
		{
		}

		// Token: 0x0400228B RID: 8843
		public AugmentTree Core;
	}

	// Token: 0x0200046C RID: 1132
	[Serializable]
	public class BindingUnlock : Unlockable
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x000C33F4 File Offset: 0x000C15F4
		public override string GUID
		{
			get
			{
				AugmentTree binding = this.Binding;
				if (binding == null)
				{
					return null;
				}
				return binding.ID;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x000C3407 File Offset: 0x000C1607
		public override string UnlockName
		{
			get
			{
				AugmentTree binding = this.Binding;
				if (binding == null)
				{
					return null;
				}
				return binding.Root.Name;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000C341F File Offset: 0x000C161F
		public override string CategoryName
		{
			get
			{
				return "Binding";
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x000C3426 File Offset: 0x000C1626
		public override UnlockCategory Type
		{
			get
			{
				return UnlockCategory.Binding;
			}
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000C3429 File Offset: 0x000C1629
		public BindingUnlock()
		{
		}

		// Token: 0x0400228C RID: 8844
		public AugmentTree Binding;

		// Token: 0x0400228D RID: 8845
		public AugmentTree Parent;

		// Token: 0x0400228E RID: 8846
		public int MinBindingEver;

		// Token: 0x0400228F RID: 8847
		public string UnlockInfo = "";
	}

	// Token: 0x0200046D RID: 1133
	[Serializable]
	public class AugmentUnlock : Unlockable
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x000C343C File Offset: 0x000C163C
		public override string GUID
		{
			get
			{
				AugmentTree augment = this.Augment;
				if (augment == null)
				{
					return null;
				}
				return augment.ID;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x000C344F File Offset: 0x000C164F
		public override string UnlockName
		{
			get
			{
				AugmentTree augment = this.Augment;
				if (augment == null)
				{
					return null;
				}
				return augment.Root.Name;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x000C3467 File Offset: 0x000C1667
		public override string CategoryName
		{
			get
			{
				return "Page";
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x000C346E File Offset: 0x000C166E
		public override UnlockCategory Type
		{
			get
			{
				return UnlockCategory.Augment;
			}
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000C3471 File Offset: 0x000C1671
		public AugmentUnlock()
		{
		}

		// Token: 0x04002290 RID: 8848
		public AugmentTree Augment;

		// Token: 0x04002291 RID: 8849
		public GenreTree Genre;

		// Token: 0x04002292 RID: 8850
		public int BindingLevel;
	}

	// Token: 0x0200046E RID: 1134
	[Serializable]
	public class Connection_Core
	{
		// Token: 0x0600218E RID: 8590 RVA: 0x000C3479 File Offset: 0x000C1679
		public Connection_Core()
		{
		}

		// Token: 0x04002293 RID: 8851
		public AugmentTree Core;

		// Token: 0x04002294 RID: 8852
		public MagicColor Color;

		// Token: 0x04002295 RID: 8853
		public List<Keyword> Keywords;
	}

	// Token: 0x0200046F RID: 1135
	[Serializable]
	public class DemoUnlockables
	{
		// Token: 0x0600218F RID: 8591 RVA: 0x000C3481 File Offset: 0x000C1681
		public DemoUnlockables()
		{
		}

		// Token: 0x04002296 RID: 8854
		public List<AugmentTree> Augments;

		// Token: 0x04002297 RID: 8855
		public List<AbilityTree> Abilities;

		// Token: 0x04002298 RID: 8856
		public List<AugmentTree> Signatures;

		// Token: 0x04002299 RID: 8857
		public List<AugmentTree> Bindings;

		// Token: 0x0400229A RID: 8858
		public List<GenreTree> Tomes;
	}

	// Token: 0x02000470 RID: 1136
	public enum RewardType
	{
		// Token: 0x0400229C RID: 8860
		GameBase,
		// Token: 0x0400229D RID: 8861
		Bindings,
		// Token: 0x0400229E RID: 8862
		SuccessBonus,
		// Token: 0x0400229F RID: 8863
		Friends,
		// Token: 0x040022A0 RID: 8864
		PerWave
	}

	// Token: 0x02000471 RID: 1137
	[Serializable]
	public class RewardInfo
	{
		// Token: 0x06002190 RID: 8592 RVA: 0x000C3489 File Offset: 0x000C1689
		public RewardInfo()
		{
		}

		// Token: 0x040022A1 RID: 8865
		public UnlockDB.RewardType Reward;

		// Token: 0x040022A2 RID: 8866
		public string Name;

		// Token: 0x040022A3 RID: 8867
		public int Value;
	}

	// Token: 0x02000472 RID: 1138
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002191 RID: 8593 RVA: 0x000C3491 File Offset: 0x000C1691
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000C349D File Offset: 0x000C169D
		public <>c()
		{
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000C34A5 File Offset: 0x000C16A5
		internal int <GetGenreRewards>b__22_0([TupleElementNames(new string[]
		{
			"reward",
			"level"
		})] ValueTuple<Unlockable, int> x, [TupleElementNames(new string[]
		{
			"reward",
			"level"
		})] ValueTuple<Unlockable, int> y)
		{
			return x.Item2.CompareTo(y.Item2);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000C34B9 File Offset: 0x000C16B9
		internal int <GetPrestigeRewards>b__23_0([TupleElementNames(new string[]
		{
			"reward",
			"level"
		})] ValueTuple<Unlockable, int> x, [TupleElementNames(new string[]
		{
			"reward",
			"level"
		})] ValueTuple<Unlockable, int> y)
		{
			return x.Item2.CompareTo(y.Item2);
		}

		// Token: 0x040022A4 RID: 8868
		public static readonly UnlockDB.<>c <>9 = new UnlockDB.<>c();

		// Token: 0x040022A5 RID: 8869
		[TupleElementNames(new string[]
		{
			"reward",
			"level"
		})]
		public static Comparison<ValueTuple<Unlockable, int>> <>9__22_0;

		// Token: 0x040022A6 RID: 8870
		[TupleElementNames(new string[]
		{
			"reward",
			"level"
		})]
		public static Comparison<ValueTuple<Unlockable, int>> <>9__23_0;
	}
}
