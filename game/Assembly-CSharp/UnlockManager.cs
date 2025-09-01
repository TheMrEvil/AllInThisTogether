using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public static class UnlockManager
{
	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0004B654 File Offset: 0x00049854
	// (set) Token: 0x06000B92 RID: 2962 RVA: 0x0004B667 File Offset: 0x00049867
	public static bool Initialized
	{
		get
		{
			if (!UnlockManager._initialized)
			{
				UnlockManager.Initialize();
			}
			return UnlockManager._initialized;
		}
		set
		{
			UnlockManager._initialized = value;
		}
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0004B670 File Offset: 0x00049870
	private static void Initialize()
	{
		UnlockManager.snapshotAllowed = true;
		UnlockManager.OnUnlocked = null;
		try
		{
			UnlockManager.Unlocked = ES3.Load<HashSet<string>>("Unlocked", "unlocks.vel", new HashSet<string>());
			UnlockManager.Augments = ES3.Load<HashSet<string>>("Augments", "unlocks.vel", new HashSet<string>());
			UnlockManager.SeenLorePages = ES3.Load<HashSet<string>>("LorePages", "unlocks.vel", new HashSet<string>());
		}
		catch (Exception)
		{
			Debug.Log("!! - Failed to load Unlock data - !!");
			UnlockManager.snapshotAllowed = false;
			Progression.BadLoad = true;
		}
		UnlockManager._initialized = true;
		UnlockDB.Init();
		foreach (string text in UnlockManager.Unlocked)
		{
			if (UnlockDB.GetUnlock(text) == null)
			{
				Debug.LogError("Could not find Unlock with ID " + text);
			}
		}
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x0004B75C File Offset: 0x0004995C
	public static void LoadFromBackup()
	{
		UnlockManager._initialized = false;
		if (Settings.LoadLatestBackup("unlocks.vel"))
		{
			UnlockManager.Initialize();
			Progression.Initialize();
			return;
		}
		Debug.LogError("Failed to load backup file");
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x0004B788 File Offset: 0x00049988
	private static void Save()
	{
		if (UnlockManager.Unlocked.Contains(""))
		{
			UnlockManager.Unlocked.Remove("");
		}
		if (UnlockManager.Augments.Contains(""))
		{
			UnlockManager.Augments.Remove("");
		}
		ES3.Save<HashSet<string>>("Unlocked", UnlockManager.Unlocked, "unlocks.vel");
		ES3.Save<HashSet<string>>("Augments", UnlockManager.Augments, "unlocks.vel");
		ES3.Save<HashSet<string>>("LorePages", UnlockManager.SeenLorePages, "unlocks.vel");
		ParseManager.SaveUnlocks();
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x0004B818 File Offset: 0x00049A18
	public static void TryTakeSnapshot()
	{
		if (!UnlockManager.snapshotAllowed)
		{
			return;
		}
		Settings.SaveInBackup("unlocks.vel");
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x0004B82C File Offset: 0x00049A2C
	public static void AchievementUnlocked(string achievementID)
	{
		foreach (Unlockable unlockable in UnlockManager.GetAllLocked())
		{
			if (unlockable.UnlockedBy == Unlockable.UnlockType.Achievement && unlockable.Achievement == achievementID)
			{
				UnlockManager.UnlockItem(unlockable, true);
				UnlockManager.NewAchievementItems.Add(unlockable);
				break;
			}
		}
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x0004B8A4 File Offset: 0x00049AA4
	public static void UnlockAbility(AbilityTree ability)
	{
		UnlockManager.UnlockItem(UnlockDB.GetAbilityUnlock(ability), true);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x0004B8B2 File Offset: 0x00049AB2
	public static void UnlockCore(AugmentTree core)
	{
		UnlockManager.UnlockItem(UnlockDB.GetCoreUnlock(core), true);
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x0004B8C0 File Offset: 0x00049AC0
	public static void UnlockGenre(GenreTree genre)
	{
		UnlockManager.UnlockItem(UnlockDB.GetGenreUnlock(genre), true);
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x0004B8CE File Offset: 0x00049ACE
	public static void UnlockBinding(AugmentTree binding)
	{
		UnlockManager.UnlockItem(UnlockDB.GetBindingUnlock(binding), true);
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x0004B8DC File Offset: 0x00049ADC
	public static void UnlockAugment(AugmentTree augment)
	{
		UnlockManager.UnlockAugment(augment.ID);
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0004B8EC File Offset: 0x00049AEC
	public static void UnlockAugments(List<AugmentTree> augments)
	{
		if (augments == null || augments.Count == 0)
		{
			return;
		}
		bool flag = false;
		foreach (AugmentTree augmentTree in augments)
		{
			flag |= UnlockManager.Augments.Add(augmentTree.ID);
		}
		if (flag)
		{
			UnlockManager.Save();
		}
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x0004B95C File Offset: 0x00049B5C
	public static void UnlockCosmetic(Cosmetic c)
	{
		UnlockManager.UnlockItem(c, true);
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x0004B965 File Offset: 0x00049B65
	public static void UnlockNookItem(NookDB.NookObject o)
	{
		UnlockManager.UnlockItem(o, true);
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0004B96E File Offset: 0x00049B6E
	public static void SaveSeenLorePage(string PageID)
	{
		if (string.IsNullOrEmpty(PageID) || UnlockManager.SeenLorePages.Contains(PageID))
		{
			return;
		}
		UnlockManager.SeenLorePages.Add(PageID);
		UnlockManager.Save();
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x0004B997 File Offset: 0x00049B97
	private static void UnlockItem(Unlockable item, bool save = true)
	{
		if (item == null || UnlockManager.Unlocked.Contains(item.GUID))
		{
			return;
		}
		UnlockManager.UnlockItem(item.GUID, save);
		Action<Unlockable> onUnlocked = UnlockManager.OnUnlocked;
		if (onUnlocked == null)
		{
			return;
		}
		onUnlocked(item);
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0004B9CC File Offset: 0x00049BCC
	private static void UnlockItem(string itemID, bool save = true)
	{
		if (UnlockManager.Unlocked.Contains(itemID))
		{
			return;
		}
		UnlockManager.Unlocked.Add(itemID);
		if (save)
		{
			UnlockManager.Save();
		}
		if (PlayerControl.myInstance != null)
		{
			PlayerControl.myInstance.TriggerSnippets(EventTrigger.Player_MetaEvent, null, 1f);
		}
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0004BA1C File Offset: 0x00049C1C
	private static bool UnlockAugment(string augmentID)
	{
		if (UnlockManager.Augments.Contains(augmentID))
		{
			return false;
		}
		UnlockManager.Augments.Add(augmentID);
		UnlockManager.Save();
		if (PlayerControl.myInstance != null)
		{
			PlayerControl.myInstance.TriggerSnippets(EventTrigger.Player_MetaEvent, null, 1f);
		}
		return true;
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0004BA6C File Offset: 0x00049C6C
	public static bool IsAbilityUnlocked(AbilityTree ability)
	{
		UnlockDB.AbilityUnlock abilityUnlock = UnlockDB.GetAbilityUnlock(ability);
		return abilityUnlock != null && UnlockManager.IsUnlocked(abilityUnlock);
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x0004BA8C File Offset: 0x00049C8C
	public static bool IsCoreUnlocked(AugmentTree core)
	{
		UnlockDB.CoreUnlock coreUnlock = UnlockDB.GetCoreUnlock(core);
		return coreUnlock != null && UnlockManager.IsUnlocked(coreUnlock);
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x0004BAAC File Offset: 0x00049CAC
	public static bool IsGenreUnlocked(GenreTree genre)
	{
		UnlockDB.GenreUnlock genreUnlock = UnlockDB.GetGenreUnlock(genre);
		return genreUnlock != null && UnlockManager.IsUnlocked(genreUnlock);
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x0004BACC File Offset: 0x00049CCC
	public static bool IsBindingUnlocked(AugmentTree binding)
	{
		UnlockDB.BindingUnlock bindingUnlock = UnlockDB.GetBindingUnlock(binding);
		return bindingUnlock != null && UnlockManager.IsUnlocked(bindingUnlock);
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x0004BAEB File Offset: 0x00049CEB
	public static bool IsAugmentUnlocked(AugmentTree augment)
	{
		return augment == null || augment.Root.StartUnlocked || UnlockManager.Augments.Contains(augment.ID);
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x0004BB18 File Offset: 0x00049D18
	public static List<Cosmetic> GetUnlockedCosmetics(CosmeticType ct = CosmeticType._)
	{
		List<Cosmetic> list = new List<Cosmetic>();
		foreach (Cosmetic cosmetic in CosmeticDB.AllCosmetics)
		{
			if ((ct == CosmeticType._ || cosmetic.CType() == ct) && UnlockManager.IsUnlocked(cosmetic))
			{
				list.Add(cosmetic);
			}
		}
		return list;
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x0004BB88 File Offset: 0x00049D88
	public static List<AbilityTree> GetUnlockedAbilities()
	{
		List<AbilityTree> list = new List<AbilityTree>();
		foreach (UnlockDB.AbilityUnlock abilityUnlock in UnlockDB.DB.Abilities)
		{
			if (UnlockManager.IsUnlocked(abilityUnlock))
			{
				list.Add(abilityUnlock.Ability);
			}
		}
		return list;
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x0004BBF4 File Offset: 0x00049DF4
	public static int AbilitiesUnlocked()
	{
		return UnlockManager.GetUnlockedAbilities().Count;
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0004BC00 File Offset: 0x00049E00
	public static int RemainingAbilities()
	{
		int count = UnlockDB.DB.Abilities.Count;
		int num = 0;
		foreach (UnlockDB.AbilityUnlock unlock in UnlockDB.DB.Abilities)
		{
			num += (UnlockManager.IsUnlocked(unlock) ? 1 : 0);
		}
		return count - num;
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x0004BC74 File Offset: 0x00049E74
	public static List<AugmentTree> GetUnlockedCores()
	{
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (UnlockDB.CoreUnlock coreUnlock in UnlockDB.DB.Cores)
		{
			if (UnlockManager.IsUnlocked(coreUnlock))
			{
				list.Add(coreUnlock.Core);
			}
		}
		return list;
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x0004BCE0 File Offset: 0x00049EE0
	public static int CoresUnlocked()
	{
		return UnlockManager.GetUnlockedCores().Count;
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x0004BCEC File Offset: 0x00049EEC
	public static int RemainingCores()
	{
		int count = UnlockDB.DB.Cores.Count;
		int num = 0;
		foreach (UnlockDB.CoreUnlock unlock in UnlockDB.DB.Cores)
		{
			num += (UnlockManager.IsUnlocked(unlock) ? 1 : 0);
		}
		return count - num;
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x0004BD60 File Offset: 0x00049F60
	public static int GenresUnlocked()
	{
		int num = 0;
		foreach (UnlockDB.GenreUnlock unlock in UnlockDB.DB.Genres)
		{
			num += (UnlockManager.IsUnlocked(unlock) ? 1 : 0);
		}
		return num;
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x0004BDC4 File Offset: 0x00049FC4
	public static List<GenreTree> GetUnlockedTomes()
	{
		List<GenreTree> list = new List<GenreTree>();
		foreach (UnlockDB.GenreUnlock genreUnlock in UnlockDB.DB.Genres)
		{
			if (UnlockManager.IsUnlocked(genreUnlock))
			{
				list.Add(genreUnlock.Genre);
			}
		}
		return list;
	}

	// Token: 0x06000BB2 RID: 2994 RVA: 0x0004BE30 File Offset: 0x0004A030
	public static int RemainingGenres()
	{
		int count = UnlockDB.DB.Genres.Count;
		int num = 0;
		foreach (UnlockDB.GenreUnlock unlock in UnlockDB.DB.Genres)
		{
			num += (UnlockManager.IsUnlocked(unlock) ? 1 : 0);
		}
		return count - num;
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x0004BEA4 File Offset: 0x0004A0A4
	public static int RemainingBindings()
	{
		int count = UnlockDB.DB.Bindings.Count;
		int num = 0;
		foreach (UnlockDB.AbilityUnlock unlock in UnlockDB.DB.Abilities)
		{
			num += (UnlockManager.IsUnlocked(unlock) ? 1 : 0);
		}
		return count - num;
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0004BF18 File Offset: 0x0004A118
	public static int CosmeticsUnlocked()
	{
		int num = 0;
		foreach (Cosmetic unlock in CosmeticDB.AllCosmetics)
		{
			num += (UnlockManager.IsUnlocked(unlock) ? 1 : 0);
		}
		return num;
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0004BF78 File Offset: 0x0004A178
	public static bool IsCosmeticUnlocked(Cosmetic c)
	{
		return UnlockManager.IsUnlocked(c);
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x0004BF80 File Offset: 0x0004A180
	public static bool IsNookItemUnlocked(NookDB.NookObject item)
	{
		return (item.UnlockedBy == Unlockable.UnlockType.Achievement && AchievementManager.IsUnlocked(item.Achievement)) || UnlockManager.IsUnlocked(item);
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x0004BFA0 File Offset: 0x0004A1A0
	public static bool IsUnlocked(Unlockable unlock)
	{
		if (unlock.UnlockedBy == Unlockable.UnlockType.Default)
		{
			return true;
		}
		UnlockDB.AugmentUnlock augmentUnlock = unlock as UnlockDB.AugmentUnlock;
		if (augmentUnlock != null)
		{
			return UnlockManager.IsAugmentUnlocked(augmentUnlock.Augment);
		}
		return UnlockManager.IsUnlocked(unlock.GUID);
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x0004BFD8 File Offset: 0x0004A1D8
	public static bool HasSeenLorePage(string ID)
	{
		return UnlockManager.SeenLorePages.Contains(ID);
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x0004BFE5 File Offset: 0x0004A1E5
	private static bool IsUnlocked(string itemID)
	{
		return UnlockManager.Unlocked.Contains(itemID);
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x0004BFF4 File Offset: 0x0004A1F4
	private static List<Unlockable> GetAllLocked()
	{
		List<Unlockable> allUnlockables = UnlockManager.GetAllUnlockables();
		for (int i = allUnlockables.Count - 1; i >= 0; i--)
		{
			if (UnlockManager.IsUnlocked(allUnlockables[i]))
			{
				allUnlockables.RemoveAt(i);
			}
		}
		return allUnlockables;
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x0004C030 File Offset: 0x0004A230
	private static List<Unlockable> GetAllUnlockables()
	{
		List<Unlockable> list = new List<Unlockable>();
		list.AddRange(CosmeticDB.DB.Heads);
		list.AddRange(CosmeticDB.DB.Skins);
		list.AddRange(CosmeticDB.DB.Books);
		list.AddRange(CosmeticDB.DB.Signatures);
		list.AddRange(NookDB.DB.AllObjects);
		list.AddRange(UnlockDB.DB.Abilities);
		list.AddRange(UnlockDB.DB.Bindings);
		list.AddRange(UnlockDB.DB.Genres);
		list.AddRange(UnlockDB.DB.GenreAugments);
		list.AddRange(UnlockDB.DB.Cores);
		return list;
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x0004C0E4 File Offset: 0x0004A2E4
	public static void PrestigeLevelUpdated(int level)
	{
		List<Unlockable> prestigeRewards = UnlockDB.GetPrestigeRewards(level);
		bool flag = false;
		foreach (Unlockable unlockable in prestigeRewards)
		{
			if (!UnlockManager.IsUnlocked(unlockable))
			{
				UnlockManager.UnlockItem(unlockable, false);
				flag = true;
			}
		}
		if (flag)
		{
			UnlockManager.Save();
		}
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x0004C14C File Offset: 0x0004A34C
	public static void ResetForPrestige()
	{
		foreach (string text in UnlockManager.Unlocked.ToList<string>())
		{
			Unlockable unlockable = UnlockDB.FindUnlock(text);
			if (unlockable == null)
			{
				UnlockManager.Unlocked.Remove(text);
			}
			else if (!(unlockable is Cosmetic) && !(unlockable is NookDB.NookObject) && !(unlockable is UnlockDB.GenreUnlock) && unlockable.ResetWithPrestige)
			{
				UnlockManager.Unlocked.Remove(text);
			}
		}
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance != null && myInstance.actions != null)
		{
			PlayerActions actions = myInstance.actions;
			List<Unlockable> list = new List<Unlockable>();
			if (actions.primary != null)
			{
				UnlockDB.AbilityUnlock abilityUnlock = UnlockDB.GetAbilityUnlock(actions.primary);
				if (abilityUnlock != null)
				{
					list.Add(abilityUnlock);
				}
			}
			if (actions.secondary != null)
			{
				UnlockDB.AbilityUnlock abilityUnlock2 = UnlockDB.GetAbilityUnlock(actions.secondary);
				if (abilityUnlock2 != null)
				{
					list.Add(abilityUnlock2);
				}
			}
			if (actions.movement != null)
			{
				UnlockDB.AbilityUnlock abilityUnlock3 = UnlockDB.GetAbilityUnlock(actions.movement);
				if (abilityUnlock3 != null)
				{
					list.Add(abilityUnlock3);
				}
			}
			if (actions.core != null)
			{
				UnlockDB.CoreUnlock coreUnlock = UnlockDB.GetCoreUnlock(actions.core);
				if (coreUnlock != null)
				{
					list.Add(coreUnlock);
				}
			}
			foreach (Unlockable unlockable2 in list)
			{
				if (!UnlockManager.Unlocked.Contains(unlockable2.GUID))
				{
					UnlockManager.Unlocked.Add(unlockable2.GUID);
				}
			}
		}
		UnlockManager.Save();
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x0004C320 File Offset: 0x0004A520
	public static void ResetAugments()
	{
		UnlockManager.Augments.Clear();
		UnlockManager.Save();
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x0004C331 File Offset: 0x0004A531
	public static void ResetTalents()
	{
		UnlockManager.Augments.Clear();
		UnlockManager.Save();
		Progression.TalentBuild = new Progression.EquippedTalents();
		Settings.SaveTalentBuild();
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x0004C354 File Offset: 0x0004A554
	public static void ResetAbilities()
	{
		foreach (string text in UnlockManager.Unlocked.ToList<string>())
		{
			Unlockable unlockable = UnlockDB.FindUnlock(text);
			if (unlockable is UnlockDB.AbilityUnlock || unlockable is UnlockDB.CoreUnlock)
			{
				UnlockManager.Unlocked.Remove(text);
			}
		}
		UnlockManager.Save();
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x0004C3CC File Offset: 0x0004A5CC
	public static void ResetGenres()
	{
		foreach (string text in UnlockManager.Unlocked.ToList<string>())
		{
			if (UnlockDB.FindUnlock(text) is UnlockDB.GenreUnlock)
			{
				UnlockManager.Unlocked.Remove(text);
			}
		}
		UnlockManager.Save();
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x0004C43C File Offset: 0x0004A63C
	public static void ResetBindings()
	{
		foreach (string text in UnlockManager.Unlocked.ToList<string>())
		{
			if (UnlockDB.FindUnlock(text) is UnlockDB.BindingUnlock)
			{
				UnlockManager.Unlocked.Remove(text);
			}
		}
		UnlockManager.Save();
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x0004C4AC File Offset: 0x0004A6AC
	public static void ResetCosmetics()
	{
		foreach (string text in UnlockManager.Unlocked.ToList<string>())
		{
			if (UnlockDB.FindUnlock(text) is Cosmetic)
			{
				UnlockManager.Unlocked.Remove(text);
			}
		}
		UnlockManager.Save();
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x0004C51C File Offset: 0x0004A71C
	public static void Reset()
	{
		UnlockManager.Unlocked.Clear();
		UnlockManager.Augments.Clear();
		Progression.TalentBuild = new Progression.EquippedTalents();
		Settings.SaveTalentBuild();
		UnlockManager.Save();
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0004C546 File Offset: 0x0004A746
	// Note: this type is marked as 'beforefieldinit'.
	static UnlockManager()
	{
	}

	// Token: 0x0400097A RID: 2426
	public const string SAVE_FILE = "unlocks.vel";

	// Token: 0x0400097B RID: 2427
	public static Action<Unlockable> OnUnlocked;

	// Token: 0x0400097C RID: 2428
	private const string UNLOCK_KEY = "Unlocked";

	// Token: 0x0400097D RID: 2429
	public static HashSet<string> Unlocked = new HashSet<string>();

	// Token: 0x0400097E RID: 2430
	private const string AUGMENT_KEY = "Augments";

	// Token: 0x0400097F RID: 2431
	public static HashSet<string> Augments = new HashSet<string>();

	// Token: 0x04000980 RID: 2432
	private const string LOREPAGE_KEY = "LorePages";

	// Token: 0x04000981 RID: 2433
	public static HashSet<string> SeenLorePages = new HashSet<string>();

	// Token: 0x04000982 RID: 2434
	public static List<Unlockable> NewAchievementItems = new List<Unlockable>();

	// Token: 0x04000983 RID: 2435
	private static bool snapshotAllowed = true;

	// Token: 0x04000984 RID: 2436
	private static bool _initialized;
}
