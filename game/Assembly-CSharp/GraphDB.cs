using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class GraphDB : ScriptableObject
{
	// Token: 0x0600025C RID: 604 RVA: 0x00014575 File Offset: 0x00012775
	public static void SetInstance(GraphDB db)
	{
		GraphDB.instance = db;
		GraphDB.instance.SetupDictionaries();
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00014588 File Offset: 0x00012788
	public void SetupDictionaries()
	{
		this.Actions = new Dictionary<string, ActionTree>();
		foreach (ActionTree actionTree in this.AllActions)
		{
			if (actionTree != null)
			{
				if (this.Actions.ContainsKey(actionTree.RootNode.guid))
				{
					string str = "Duplicate GUID for Ability Graph ";
					ActionTree actionTree2 = actionTree;
					Debug.LogError(str + ((actionTree2 != null) ? actionTree2.ToString() : null));
				}
				this.Actions.Add(actionTree.RootNode.guid, actionTree);
			}
			else
			{
				Debug.LogError("Action List had Null Entry");
			}
		}
		this.Abilities = new Dictionary<string, AbilityTree>();
		foreach (AbilityTree abilityTree in this.AllAbilities)
		{
			if (abilityTree != null)
			{
				if (this.Abilities.ContainsKey(abilityTree.RootNode.guid))
				{
					string str2 = "Duplicate GUID for Ability Graph ";
					AbilityTree abilityTree2 = abilityTree;
					Debug.LogError(str2 + ((abilityTree2 != null) ? abilityTree2.ToString() : null));
				}
				this.Abilities.Add(abilityTree.RootNode.guid, abilityTree);
			}
			else
			{
				Debug.LogError("Ability List had Null Entry");
			}
		}
		this.PlayerAbilities = new Dictionary<PlayerAbilityType, List<AbilityTree>>();
		foreach (AbilityTree abilityTree3 in this.AllAbilities)
		{
			if (abilityTree3 == null)
			{
				Debug.LogError("Ability List had Null Entry");
			}
			else
			{
				AbilityRootNode abilityRootNode = abilityTree3.RootNode as AbilityRootNode;
				if (abilityRootNode.Usage.IsPlayerAbility)
				{
					PlayerAbilityType plrAbilityType = abilityRootNode.PlrAbilityType;
					if (!this.PlayerAbilities.ContainsKey(plrAbilityType))
					{
						this.PlayerAbilities.Add(plrAbilityType, new List<AbilityTree>());
					}
					this.PlayerAbilities[plrAbilityType].Add(abilityTree3);
				}
			}
		}
		this.StatusEffects = new Dictionary<int, StatusTree>();
		foreach (StatusTree statusTree in this.AllStatuses)
		{
			if (statusTree != null)
			{
				if (this.StatusEffects.ContainsKey(statusTree.RootNode.guid.GetHashCode()))
				{
					string str3 = "Duplicate Hashcode for Status Graph ";
					StatusTree statusTree2 = statusTree;
					Debug.LogError(str3 + ((statusTree2 != null) ? statusTree2.ToString() : null));
				}
				this.StatusEffects.Add(statusTree.RootNode.guid.GetHashCode(), statusTree);
			}
		}
		this.Genres = new Dictionary<string, GenreTree>();
		foreach (GenreTree genreTree in this.AllGenres)
		{
			if (genreTree != null)
			{
				if (this.Genres.ContainsKey(genreTree.RootNode.guid))
				{
					string str4 = "Duplicate GUID for Genre Graph ";
					GenreTree genreTree2 = genreTree;
					Debug.LogError(str4 + ((genreTree2 != null) ? genreTree2.ToString() : null));
				}
				this.Genres.Add(genreTree.RootNode.guid, genreTree);
			}
		}
		this.Brains = new Dictionary<string, AITree>();
		foreach (AITree aitree in this.AIBrains)
		{
			if (aitree != null)
			{
				if (this.Brains.ContainsKey(aitree.RootNode.guid))
				{
					string str5 = "Duplicate GUID for AI Graph ";
					AITree aitree2 = aitree;
					Debug.LogError(str5 + ((aitree2 != null) ? aitree2.ToString() : null));
				}
				this.Brains.Add(aitree.RootNode.guid, aitree);
			}
		}
		this.Augments = new Dictionary<string, AugmentTree>();
		foreach (AugmentTree augmentTree in this.PlayerMods)
		{
			if (augmentTree != null)
			{
				try
				{
					this.Augments.Add(augmentTree.ID, augmentTree);
				}
				catch
				{
					Debug.LogError("Error with " + augmentTree.Root.Name + " - ID Duplicate: " + this.Augments[augmentTree.ID].Root.Name);
				}
			}
		}
		foreach (AugmentTree augmentTree2 in this.EnemyMods)
		{
			if (augmentTree2 != null)
			{
				this.Augments.Add(augmentTree2.ID, augmentTree2);
			}
		}
		foreach (AugmentTree augmentTree3 in this.FountainMods)
		{
			if (augmentTree3 != null)
			{
				this.Augments.Add(augmentTree3.ID, augmentTree3);
			}
		}
		foreach (AugmentTree augmentTree4 in this.WorldMods)
		{
			if (augmentTree4 != null)
			{
				this.Augments.Add(augmentTree4.ID, augmentTree4);
			}
		}
		foreach (AugmentTree augmentTree5 in this.BonusObjectives)
		{
			if (augmentTree5 != null)
			{
				this.Augments.Add(augmentTree5.ID, augmentTree5);
			}
		}
		this.Achievements = new Dictionary<string, AchievementRootNode>();
		foreach (AchievementTree achievementTree in this.AllAchievements)
		{
			if (achievementTree != null)
			{
				if (this.Achievements.ContainsKey(achievementTree.RootNode.guid))
				{
					string str6 = "Duplicate GUID for Genre Graph ";
					AchievementTree achievementTree2 = achievementTree;
					Debug.LogError(str6 + ((achievementTree2 != null) ? achievementTree2.ToString() : null));
				}
				this.Achievements.Add(achievementTree.Root.ID, achievementTree.Root);
			}
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00014C54 File Offset: 0x00012E54
	public static List<AugmentTree> GetAllAugments(ModType modType)
	{
		switch (modType)
		{
		case ModType.Player:
			return GraphDB.instance.PlayerMods;
		case ModType.Enemy:
			return GraphDB.instance.EnemyMods;
		case ModType.Fountain:
			return GraphDB.instance.FountainMods;
		case ModType.BonusObjective:
			return GraphDB.instance.BonusObjectives;
		}
		return new List<AugmentTree>();
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00014CB8 File Offset: 0x00012EB8
	public static List<AugmentTree> GetValidMods(ModType modType)
	{
		switch (modType)
		{
		case ModType.Player:
			return GraphDB.GetValidPlayerMods(PlayerControl.myInstance);
		case ModType.Enemy:
			return GraphDB.GetValidEnemyMods();
		case ModType.Fountain:
			return GraphDB.GetValidInkMods();
		case ModType.Binding:
			return GraphDB.GetValidWorldMods();
		case ModType.BonusObjective:
			return GraphDB.GetValidBonusObjectives();
		default:
			return null;
		}
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00014D08 File Offset: 0x00012F08
	private static List<AugmentTree> GetValidPlayerMods(PlayerControl player)
	{
		EffectProperties props = new EffectProperties(player);
		List<AugmentTree> list = new List<AugmentTree>();
		HashSet<AugmentTree> hashSet = new HashSet<AugmentTree>();
		if (player == PlayerControl.myInstance)
		{
			foreach (ScrollPickup scrollPickup in UnityEngine.Object.FindObjectsOfType<ScrollPickup>())
			{
				if (!hashSet.Contains(scrollPickup.Augment))
				{
					hashSet.Add(scrollPickup.Augment);
				}
			}
			foreach (AugmentTree item in AugmentsPanel.ChoicesOnDeck)
			{
				hashSet.Add(item);
			}
			if (PlayerChoicePanel.instance.HasChoices)
			{
				foreach (AugmentTree item2 in PlayerChoicePanel.instance.CurrentAugmentChoices)
				{
					hashSet.Add(item2);
				}
			}
		}
		foreach (AugmentTree augmentTree in GraphDB.instance.PlayerMods)
		{
			if (augmentTree.Root.Rarity != Rarity.Explicit && !hashSet.Contains(augmentTree) && (!RaidManager.IsInRaid || augmentTree.Root.Raid) && !player.HasAugment(augmentTree.ID, false) && augmentTree.Root.Validate(props) && (GameplayManager.IsChallengeActive || UnlockManager.IsAugmentUnlocked(augmentTree)))
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00014EBC File Offset: 0x000130BC
	private static List<AugmentTree> GetValidEnemyMods()
	{
		int bindingLevel = GameplayManager.BindingLevel;
		List<AugmentTree> list = new List<AugmentTree>();
		EffectProperties props = new EffectProperties();
		foreach (AugmentTree augmentTree in GraphDB.instance.EnemyMods)
		{
			if (augmentTree.Root.Rarity != Rarity.Explicit && augmentTree.Root.EMinHeat <= bindingLevel && !AIManager.GlobalEnemyMods.TreeIDs.Contains(augmentTree.ID) && augmentTree.Root.Validate(props))
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06000262 RID: 610 RVA: 0x00014F70 File Offset: 0x00013170
	private static List<AugmentTree> GetValidInkMods()
	{
		List<AugmentTree> list = new List<AugmentTree>();
		List<AugmentTree> list2 = new List<AugmentTree>();
		GameplayManager gameplayManager = GameplayManager.instance;
		GenreTree y = (gameplayManager != null) ? gameplayManager.GameGraph : null;
		foreach (InkRow inkRow in InkManager.Store)
		{
			foreach (AugmentTree item in (from x in inkRow.Options
			select x.Augment).ToList<AugmentTree>())
			{
				list2.Add(item);
			}
		}
		foreach (AugmentTree augmentTree in GraphDB.instance.FountainMods)
		{
			if (augmentTree.Root.Rarity != Rarity.Explicit && !list2.Contains(augmentTree) && (!(augmentTree.Root.Tome != null) || !(augmentTree.Root.Tome != y)))
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06000263 RID: 611 RVA: 0x000150D0 File Offset: 0x000132D0
	private static List<AugmentTree> GetValidWorldMods()
	{
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (AugmentTree augmentTree in GraphDB.instance.WorldMods)
		{
			if (augmentTree.Root.Rarity != Rarity.Explicit && !GameplayManager.instance.GetGameAugments(ModType.Binding).TreeIDs.Contains(augmentTree.ID))
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0001515C File Offset: 0x0001335C
	private static List<AugmentTree> GetValidBonusObjectives()
	{
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (AugmentTree augmentTree in GraphDB.instance.BonusObjectives)
		{
			if (augmentTree.Root.Rarity != Rarity.Explicit)
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06000265 RID: 613 RVA: 0x000151C8 File Offset: 0x000133C8
	public static List<AugmentTree> GetAvailableLockedPlayerMods()
	{
		UnlockDB.GetKeywords();
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (AugmentTree augmentTree in GraphDB.instance.PlayerMods)
		{
			if (augmentTree.Root.Rarity != Rarity.Explicit && !UnlockManager.IsAugmentUnlocked(augmentTree) && UnlockDB.CanUnlock(augmentTree))
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0001524C File Offset: 0x0001344C
	public static AugmentTree ChooseModFromList(ModType mt, List<AugmentTree> options, bool priorityFirst = false, bool useSeed = false)
	{
		if (options.Count == 0)
		{
			return null;
		}
		List<AugmentTree> list = new List<AugmentTree>();
		List<AugmentTree> list2 = new List<AugmentTree>();
		foreach (AugmentTree augmentTree in options)
		{
			int num = (int)(100f / (float)GameDB.Rarity(augmentTree.Root.Rarity).RelativeChance);
			if (!GameplayManager.IsChallengeActive && Progression.UnseenAugments.Contains(augmentTree.ID))
			{
				num *= 4;
			}
			bool priorityUnlock = augmentTree.Root.PriorityUnlock;
			for (int i = 0; i < num; i++)
			{
				list.Add(augmentTree);
				if (priorityUnlock && priorityFirst)
				{
					list2.Add(augmentTree);
				}
			}
		}
		List<AugmentTree> list3 = (list2.Count > 0 && priorityFirst) ? list2 : list;
		if (useSeed)
		{
			if (mt == ModType.BonusObjective)
			{
				return list3[MapManager.GetRandom(0, list3.Count)];
			}
			if (mt == ModType.Player)
			{
				return list3[PlayerControl.myInstance.GetRandom(0, list3.Count)];
			}
			if (mt == ModType.Fountain)
			{
				return list3[MapManager.GetRandom(0, list3.Count)];
			}
			if (mt == ModType.Enemy)
			{
				return list3[MapManager.GetRandom(0, list3.Count)];
			}
		}
		return list3[UnityEngine.Random.Range(0, list3.Count)];
	}

	// Token: 0x06000267 RID: 615 RVA: 0x000153AC File Offset: 0x000135AC
	public static AugmentTree ChooseFountainModFromList(List<AugmentTree> options, int costNeeded)
	{
		if (options.Count == 0)
		{
			return null;
		}
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (AugmentTree augmentTree in options)
		{
			if (augmentTree.Root.InkCost >= costNeeded)
			{
				int num = GameDB.Rarity(augmentTree.Root.Rarity).RelativeChance;
				if (augmentTree.Root.Filters.Filters.Contains(ModFilter.Mech_Tradeoff))
				{
					num += 2;
				}
				int num2 = (int)(100f / (float)num);
				for (int i = 0; i < num2; i++)
				{
					list.Add(augmentTree);
				}
			}
		}
		if (list.Count == 0)
		{
			Debug.LogError("Invalid Fountain Cost Request - No valid mods could found meeting cost requirement of " + costNeeded.ToString());
			return GraphDB.ChooseModFromList(ModType.Fountain, options, false, GameplayManager.IsChallengeActive);
		}
		if (GameplayManager.IsChallengeActive)
		{
			return list[MapManager.GetRandom(0, list.Count)];
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x06000268 RID: 616 RVA: 0x000154C4 File Offset: 0x000136C4
	public static List<AugmentTree> GetModifiersByRarity(ModType modType, Rarity Min, Rarity Max)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (AugmentTree augmentTree in GraphDB.GetValidMods(modType))
		{
			int rarity = (int)augmentTree.Root.Rarity;
			if (Min <= (Rarity)rarity && rarity <= (int)Max)
			{
				list.Add(augmentTree);
			}
		}
		return list;
	}

	// Token: 0x06000269 RID: 617 RVA: 0x00015534 File Offset: 0x00013734
	public static ActionTree GetAction(string GUID)
	{
		if (GraphDB.instance.Actions.ContainsKey(GUID))
		{
			return GraphDB.instance.Actions[GUID];
		}
		return null;
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0001555A File Offset: 0x0001375A
	public static AITree GetAITree(string GUID)
	{
		if (GraphDB.instance.Brains.ContainsKey(GUID))
		{
			return GraphDB.instance.Brains[GUID];
		}
		return null;
	}

	// Token: 0x0600026B RID: 619 RVA: 0x00015580 File Offset: 0x00013780
	public static AugmentTree GetAugment(string GUID)
	{
		GUID = GUID.ToLower().Replace(" ", "_");
		AugmentTree result;
		if (GraphDB.instance.Augments.TryGetValue(GUID, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x000155BC File Offset: 0x000137BC
	public static AugmentTree GetAugmentByName(string name)
	{
		name = name.ToLower().Replace(" ", "_");
		foreach (KeyValuePair<string, AugmentTree> keyValuePair in GraphDB.instance.Augments)
		{
			if (!(keyValuePair.Value == null) && !(keyValuePair.Value.Root == null) && keyValuePair.Value.Root.Name != null && keyValuePair.Value.Root.Name.ToLower().Replace(" ", "_") == name)
			{
				return keyValuePair.Value;
			}
		}
		return null;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00015694 File Offset: 0x00013894
	public static AbilityTree GetAbility(string GUID)
	{
		if (GraphDB.instance.Abilities.ContainsKey(GUID))
		{
			return GraphDB.instance.Abilities[GUID];
		}
		return null;
	}

	// Token: 0x0600026E RID: 622 RVA: 0x000156BA File Offset: 0x000138BA
	public static List<AbilityTree> GetPlayerAbilities(PlayerAbilityType abilityType)
	{
		if (GraphDB.instance.PlayerAbilities.ContainsKey(abilityType))
		{
			return GraphDB.instance.PlayerAbilities[abilityType];
		}
		return new List<AbilityTree>();
	}

	// Token: 0x0600026F RID: 623 RVA: 0x000156E4 File Offset: 0x000138E4
	public static StatusTree GetStatusEffect(int HashID)
	{
		if (GraphDB.instance.StatusEffects.ContainsKey(HashID))
		{
			return GraphDB.instance.StatusEffects[HashID];
		}
		return null;
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0001570C File Offset: 0x0001390C
	public static StatusTree GetStatusByName(string Name)
	{
		Name = Name.ToLower().Replace(" ", "_");
		foreach (StatusTree statusTree in GraphDB.instance.AllStatuses)
		{
			if (statusTree.Root.EffectName.ToLower().Replace(" ", "_") == Name)
			{
				return statusTree;
			}
		}
		return null;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x000157A4 File Offset: 0x000139A4
	public static GenreTree GetGenre(string GUID)
	{
		if (GraphDB.instance.Genres.ContainsKey(GUID))
		{
			return GraphDB.instance.Genres[GUID];
		}
		return null;
	}

	// Token: 0x06000272 RID: 626 RVA: 0x000157CC File Offset: 0x000139CC
	public static GenreTree GetGenreByName(string name)
	{
		name = name.ToLower().Replace(" ", "_");
		foreach (KeyValuePair<string, GenreTree> keyValuePair in GraphDB.instance.Genres)
		{
			if ((keyValuePair.Value.RootNode as GenreRootNode).Name.ToLower().Replace(" ", "_") == name)
			{
				return keyValuePair.Value;
			}
		}
		return null;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x00015874 File Offset: 0x00013A74
	public static List<GenreTree> GetGenreOptions(int num)
	{
		List<GenreTree> list = new List<GenreTree>();
		foreach (GenreTree genreTree in GraphDB.instance.AllGenres)
		{
			if (genreTree.Root.IsAvailable)
			{
				list.Add(genreTree);
			}
		}
		List<GenreTree> list2 = new List<GenreTree>();
		for (int i = 0; i < num; i++)
		{
			if (list.Count == 0)
			{
				return list2;
			}
			int index = UnityEngine.Random.Range(0, list.Count);
			list2.Add(list[index]);
			list.RemoveAt(index);
		}
		return list2;
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00015924 File Offset: 0x00013B24
	public ActionTree GetSnippetTree(string ID)
	{
		foreach (ActionTree actionTree in this.AllActions)
		{
			if (actionTree.name.ToLower().Equals(ID.ToLower()))
			{
				return actionTree;
			}
		}
		Debug.LogError("Could not find Action Graph with name: " + ID);
		return null;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x000159A0 File Offset: 0x00013BA0
	public static PagePreset GetPagePreset(string ID)
	{
		foreach (PagePreset pagePreset in GraphDB.instance.PagePresets)
		{
			if (pagePreset.guid == ID)
			{
				return pagePreset;
			}
		}
		return null;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x00015A08 File Offset: 0x00013C08
	public static PagePreset GetRandomPagePreset(List<PagePreset> exclude = null)
	{
		List<PagePreset> list = new List<PagePreset>();
		foreach (PagePreset pagePreset in GraphDB.instance.PagePresets)
		{
			if (exclude == null || !exclude.Contains(pagePreset))
			{
				int num = 0;
				while ((float)num < pagePreset.Abundance)
				{
					list.Add(pagePreset);
					num++;
				}
			}
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x06000277 RID: 631 RVA: 0x00015A98 File Offset: 0x00013C98
	public static bool HasAchievementWithTrigger(EventTrigger trigger)
	{
		using (List<AchievementTree>.Enumerator enumerator = GraphDB.instance.AllAchievements.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Root.Trigger == trigger)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00015AFC File Offset: 0x00013CFC
	public static AchievementRootNode GetAchievement(string ID)
	{
		AchievementRootNode result;
		if (GraphDB.instance.Achievements.TryGetValue(ID, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00015B20 File Offset: 0x00013D20
	public static List<AchievementRootNode> GetAchievements(EventTrigger trigger)
	{
		List<AchievementRootNode> list = new List<AchievementRootNode>();
		foreach (KeyValuePair<string, AchievementRootNode> keyValuePair in GraphDB.instance.Achievements)
		{
			string text;
			AchievementRootNode achievementRootNode;
			keyValuePair.Deconstruct(out text, out achievementRootNode);
			AchievementRootNode achievementRootNode2 = achievementRootNode;
			if (achievementRootNode2.Trigger == trigger)
			{
				list.Add(achievementRootNode2);
			}
		}
		return list;
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00015B98 File Offset: 0x00013D98
	public void CollectAllGraphs()
	{
		XGraphCollector.CollectData();
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00015BA0 File Offset: 0x00013DA0
	public void CheckDebugGraph()
	{
		string graphIDDebug = this.GraphIDDebug;
		foreach (AbilityTree abilityTree in this.AllAbilities)
		{
			if (abilityTree.ID == graphIDDebug)
			{
				string str = "Found Graph: ";
				AbilityTree abilityTree2 = abilityTree;
				Debug.Log(str + ((abilityTree2 != null) ? abilityTree2.ToString() : null));
			}
		}
		foreach (GenreTree genreTree in this.AllGenres)
		{
			if (genreTree.ID == graphIDDebug)
			{
				string str2 = "Found Graph: ";
				GenreTree genreTree2 = genreTree;
				Debug.Log(str2 + ((genreTree2 != null) ? genreTree2.ToString() : null));
			}
		}
		foreach (ActionTree actionTree in this.AllActions)
		{
			if (actionTree.ID == graphIDDebug)
			{
				string str3 = "Found Graph: ";
				ActionTree actionTree2 = actionTree;
				Debug.Log(str3 + ((actionTree2 != null) ? actionTree2.ToString() : null));
			}
		}
		foreach (StatusTree statusTree in this.AllStatuses)
		{
			if (statusTree.ID == graphIDDebug)
			{
				string str4 = "Found Graph: ";
				StatusTree statusTree2 = statusTree;
				Debug.Log(str4 + ((statusTree2 != null) ? statusTree2.ToString() : null));
			}
		}
		foreach (AITree aitree in this.AIBrains)
		{
			if (aitree.ID == graphIDDebug)
			{
				string str5 = "Found Graph: ";
				AITree aitree2 = aitree;
				Debug.Log(str5 + ((aitree2 != null) ? aitree2.ToString() : null));
			}
		}
		foreach (AugmentTree augmentTree in this.FountainMods)
		{
			if (augmentTree.ID == graphIDDebug)
			{
				string str6 = "Found Graph: ";
				AugmentTree augmentTree2 = augmentTree;
				Debug.Log(str6 + ((augmentTree2 != null) ? augmentTree2.ToString() : null));
			}
		}
		foreach (AugmentTree augmentTree3 in this.PlayerMods)
		{
			if (augmentTree3.ID == graphIDDebug)
			{
				string str7 = "Found Graph: ";
				AugmentTree augmentTree4 = augmentTree3;
				Debug.Log(str7 + ((augmentTree4 != null) ? augmentTree4.ToString() : null));
			}
		}
		foreach (AugmentTree augmentTree5 in this.WorldMods)
		{
			if (augmentTree5.ID == graphIDDebug)
			{
				string str8 = "Found Graph: ";
				AugmentTree augmentTree6 = augmentTree5;
				Debug.Log(str8 + ((augmentTree6 != null) ? augmentTree6.ToString() : null));
			}
		}
		foreach (AugmentTree augmentTree7 in this.EnemyMods)
		{
			if (augmentTree7.ID == graphIDDebug)
			{
				string str9 = "Found Graph: ";
				AugmentTree augmentTree8 = augmentTree7;
				Debug.Log(str9 + ((augmentTree8 != null) ? augmentTree8.ToString() : null));
			}
		}
		foreach (AugmentTree augmentTree9 in this.BonusObjectives)
		{
			if (augmentTree9.ID == graphIDDebug)
			{
				string str10 = "Found Graph: ";
				AugmentTree augmentTree10 = augmentTree9;
				Debug.Log(str10 + ((augmentTree10 != null) ? augmentTree10.ToString() : null));
			}
		}
	}

	// Token: 0x0600027C RID: 636 RVA: 0x00015FD0 File Offset: 0x000141D0
	public GraphDB()
	{
	}

	// Token: 0x0400025F RID: 607
	public static GraphDB instance;

	// Token: 0x04000260 RID: 608
	public List<ActionTree> AllActions;

	// Token: 0x04000261 RID: 609
	public List<AbilityTree> AllAbilities;

	// Token: 0x04000262 RID: 610
	public List<StatusTree> AllStatuses;

	// Token: 0x04000263 RID: 611
	public List<GenreTree> AllGenres;

	// Token: 0x04000264 RID: 612
	public List<AugmentTree> PlayerMods;

	// Token: 0x04000265 RID: 613
	public List<AugmentTree> EnemyMods;

	// Token: 0x04000266 RID: 614
	public List<AugmentTree> FountainMods;

	// Token: 0x04000267 RID: 615
	public List<AugmentTree> WorldMods;

	// Token: 0x04000268 RID: 616
	public List<AugmentTree> BonusObjectives;

	// Token: 0x04000269 RID: 617
	public List<AITree> AIBrains;

	// Token: 0x0400026A RID: 618
	public List<PagePreset> PagePresets;

	// Token: 0x0400026B RID: 619
	public List<AchievementTree> AllAchievements;

	// Token: 0x0400026C RID: 620
	private Dictionary<string, ActionTree> Actions;

	// Token: 0x0400026D RID: 621
	private Dictionary<string, AbilityTree> Abilities;

	// Token: 0x0400026E RID: 622
	private Dictionary<PlayerAbilityType, List<AbilityTree>> PlayerAbilities;

	// Token: 0x0400026F RID: 623
	private Dictionary<int, StatusTree> StatusEffects;

	// Token: 0x04000270 RID: 624
	private Dictionary<string, AugmentTree> Augments;

	// Token: 0x04000271 RID: 625
	private Dictionary<string, GenreTree> Genres;

	// Token: 0x04000272 RID: 626
	private Dictionary<string, AITree> Brains;

	// Token: 0x04000273 RID: 627
	private Dictionary<string, AchievementRootNode> Achievements;

	// Token: 0x04000274 RID: 628
	public string GraphIDDebug;

	// Token: 0x02000446 RID: 1094
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600212F RID: 8495 RVA: 0x000C25E9 File Offset: 0x000C07E9
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000C25F5 File Offset: 0x000C07F5
		public <>c()
		{
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000C25FD File Offset: 0x000C07FD
		internal AugmentTree <GetValidInkMods>b__28_0(InkTalent x)
		{
			return x.Augment;
		}

		// Token: 0x040021B4 RID: 8628
		public static readonly GraphDB.<>c <>9 = new GraphDB.<>c();

		// Token: 0x040021B5 RID: 8629
		public static Func<InkTalent, AugmentTree> <>9__28_0;
	}
}
