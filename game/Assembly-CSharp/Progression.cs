using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using SimpleJSON;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public static class Progression
{
	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000B41 RID: 2881 RVA: 0x000492CF File Offset: 0x000474CF
	public static int BestBinding
	{
		get
		{
			return (from v in Progression.HighestBindingLevel
			select v.Value).Prepend(0).Max();
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00049305 File Offset: 0x00047505
	public static int AttunementTarget
	{
		get
		{
			return Progression.BindingAttunement + 1;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0004930E File Offset: 0x0004750E
	public static int OverbindAllowed
	{
		get
		{
			if (Progression.PrestigeCount > 0)
			{
				return 99;
			}
			return 0;
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00049324 File Offset: 0x00047524
	// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0004931C File Offset: 0x0004751C
	public static int InkLevel
	{
		[CompilerGenerated]
		get
		{
			return Progression.<InkLevel>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			Progression.<InkLevel>k__BackingField = value;
		}
	} = 1;

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0004932B File Offset: 0x0004752B
	public static int MaxInkLevel
	{
		get
		{
			return PlayerDB.InkLevelCount;
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00049332 File Offset: 0x00047532
	public static float LevelProgressProportion
	{
		get
		{
			return (float)Progression.LevelProgress / Mathf.Max((float)PlayerDB.GetNextInkLevelRequirement(Progression.InkLevel), 5f);
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00049350 File Offset: 0x00047550
	public static bool IsMaxPrestige
	{
		get
		{
			return Progression.PrestigeCount >= MetaDB.MaxPrestige;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00049361 File Offset: 0x00047561
	// (set) Token: 0x06000B4A RID: 2890 RVA: 0x00049372 File Offset: 0x00047572
	public static bool Initialized
	{
		get
		{
			if (Progression._initialized)
			{
				return true;
			}
			Progression.Initialize();
			return true;
		}
		set
		{
			Progression._initialized = value;
		}
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x0004937C File Offset: 0x0004757C
	public static void ValidateSaveFile()
	{
		try
		{
			ES3.Load<int>("LCoins", "unlocks.vel");
		}
		catch (Exception ex)
		{
			string str = "Caught Exception durring Unlock file loading: ";
			Exception ex2 = ex;
			Debug.Log(str + ((ex2 != null) ? ex2.ToString() : null));
			if (ES3.RestoreBackup("unlocks.vel"))
			{
				Debug.Log("Backup restored.");
			}
			else
			{
				string path = Path.Combine(Application.persistentDataPath, "unlocks.vel");
				if (File.Exists(path))
				{
					File.Delete(path);
					Progression.BadLoad = true;
				}
				else if (Settings.HasBackupAvailable())
				{
					Progression.BadLoad = true;
				}
				Debug.LogError("Backup could not be restored as no backup exists.");
			}
		}
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00049420 File Offset: 0x00047620
	public static void Initialize()
	{
		if (!UnlockManager.Initialized)
		{
			return;
		}
		Progression.NewAttumnentLevel = false;
		Progression.snapshotAllowed = true;
		ES3Settings settings = new ES3Settings("unlocks.vel", null);
		try
		{
			ES3.CacheFile("unlocks.vel");
			settings = new ES3Settings("unlocks.vel", new Enum[]
			{
				ES3.Location.Cache
			});
			Progression.LossQuills = ES3.Load<int>("Loss_Token", 0, settings);
			Progression.LastGameData = ES3.Load<string>("LastGame", "", settings);
			Progression.TutorialReward = ES3.Load<bool>("Rew_Tut", false, settings);
			Progression.BindingAttunement = ES3.Load<int>("Attune", 0, settings);
			Progression.BindingAttunement = Mathf.Min(Progression.BindingAttunement, 20);
			Progression.InkLevel = ES3.Load<int>("Level", 1, settings);
			Progression.LevelProgress = ES3.Load<int>("LevelProg", 0, settings);
			Progression.PrestigeCount = ES3.Load<int>("Prestige", 0, settings);
			Progression.LibTalentsUnlocked = ES3.Load<int>("LibTalentLvl", 0, settings);
			Progression.UnseenAugments = ES3.Load<HashSet<string>>("New_Aug", new HashSet<string>(), settings);
			Progression.LastBoss = ES3.Load<string>("LastBoss", "", settings);
			Progression.LastSeenBookClubChallenge = ES3.Load<string>("LastChallenge", "", settings);
			Progression.FoundAugments = ES3.Load<HashSet<string>>("Codex_Aug", new HashSet<string>(), settings);
		}
		catch (Exception message)
		{
			Debug.Log(message);
			Progression.BadLoad = true;
			Progression.snapshotAllowed = false;
		}
		try
		{
			List<string> list = ES3.Load<List<string>>("Rew_Aug", new List<string>(), settings);
			Progression.AugmentRewards = new List<AugmentTree>();
			foreach (string guid in list)
			{
				AugmentTree augment = GraphDB.GetAugment(guid);
				if (!(augment == null))
				{
					Progression.AugmentRewards.Add(augment);
				}
			}
		}
		catch (Exception message2)
		{
			Debug.Log(message2);
		}
		try
		{
			List<string> list2 = ES3.Load<List<string>>("Rew_Tome", new List<string>(), settings);
			Progression.TomeRewards = new List<GenreTree>();
			foreach (string guid2 in list2)
			{
				GenreTree genre = GraphDB.GetGenre(guid2);
				if (!(genre == null))
				{
					Progression.TomeRewards.Add(genre);
				}
			}
		}
		catch (Exception message3)
		{
			Debug.Log(message3);
		}
		try
		{
			List<string> list3 = ES3.Load<List<string>>("Rew_Bind", new List<string>(), settings);
			Progression.BindingRewards = new List<AugmentTree>();
			foreach (string guid3 in list3)
			{
				AugmentTree augment2 = GraphDB.GetAugment(guid3);
				if (!(augment2 == null))
				{
					Progression.BindingRewards.Add(augment2);
				}
			}
		}
		catch (Exception message4)
		{
			Debug.Log(message4);
		}
		try
		{
			Progression.LoadEndGameData();
		}
		catch (Exception message5)
		{
			Debug.Log(message5);
		}
		try
		{
			Progression.RecentDaily = ES3.Load<List<string>>("RecentDailyQ", new List<string>(), settings);
			Progression.RecentWeekly = ES3.Load<List<string>>("RecentWeeklyQ", new List<string>(), settings);
			List<string> list4 = ES3.Load<List<string>>("Quests", new List<string>(), settings);
			Progression.Quests = new List<MetaDB.QuestProgress>();
			foreach (string data in list4)
			{
				MetaDB.QuestProgress questProgress = new MetaDB.QuestProgress(data);
				if (questProgress != null)
				{
					Progression.Quests.Add(questProgress);
				}
				Progression.questAugments = null;
			}
			string str = "";
			foreach (MetaDB.QuestProgress questProgress2 in Progression.Quests)
			{
				str = str + questProgress2.ID + ", ";
			}
		}
		catch (Exception message6)
		{
			Debug.Log(message6);
		}
		Progression._initialized = true;
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x000498AC File Offset: 0x00047AAC
	public static void ResetSave()
	{
		Progression.LossQuills = 0;
		Progression.LastGameData = "";
		Progression.UnseenAugments.Clear();
		Progression.FoundAugments.Clear();
		Progression.LastBoss = "";
		Progression.LastSeenBookClubChallenge = "";
		Progression.AugmentRewards.Clear();
		Progression.BindingAttunement = 0;
		Progression.InkLevel = 1;
		Progression.LibTalentsUnlocked = 0;
		Progression.LevelProgress = 0;
		Progression.TomeRewards.Clear();
		Progression.BindingRewards.Clear();
		Progression.TutorialReward = false;
		Progression.PrestigeCount = 0;
		Progression.Quests.Clear();
		Progression.questAugments = null;
		Progression.SaveState();
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00049948 File Offset: 0x00047B48
	public static void ResetProgression()
	{
		Progression.BindingAttunement = 0;
		Progression.InkLevel = 1;
		Progression.LevelProgress = 0;
		Progression.SaveState();
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x00049964 File Offset: 0x00047B64
	public static void SaveState()
	{
		ES3Settings settings = new ES3Settings("unlocks.vel", null);
		bool flag = false;
		try
		{
			ES3.CacheFile("unlocks.vel");
			settings = new ES3Settings("unlocks.vel", new Enum[]
			{
				ES3.Location.Cache
			});
		}
		catch (Exception)
		{
			ES3.DeleteFile("unlocks.vel");
			flag = true;
		}
		List<string> value = (from v in Progression.AugmentRewards
		select v.ID).ToList<string>();
		ES3.Save<int>("Loss_Token", Progression.LossQuills, settings);
		ES3.Save<string>("LastGame", Progression.LastGameData, settings);
		ES3.Save<bool>("Rew_Tut", Progression.TutorialReward, settings);
		ES3.Save<int>("Attune", Progression.BindingAttunement, settings);
		ES3.Save<int>("Level", Progression.InkLevel, settings);
		ES3.Save<int>("LevelProg", Progression.LevelProgress, settings);
		ES3.Save<int>("Prestige", Progression.PrestigeCount, settings);
		ES3.Save<int>("LibTalentLvl", Progression.LibTalentsUnlocked, settings);
		ES3.Save<List<string>>("Rew_Aug", value, settings);
		ES3.Save<List<string>>("Rew_Tome", (from v in Progression.TomeRewards
		select v.ID).ToList<string>(), settings);
		ES3.Save<List<string>>("Rew_Bind", (from v in Progression.BindingRewards
		select v.ID).ToList<string>(), settings);
		ES3.Save<HashSet<string>>("New_Aug", Progression.UnseenAugments, settings);
		ES3.Save<string>("LastBoss", Progression.LastBoss, settings);
		ES3.Save<string>("LastChallenge", Progression.LastSeenBookClubChallenge, settings);
		ES3.Save<HashSet<string>>("Codex_Aug", Progression.FoundAugments, settings);
		List<string> list = new List<string>();
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			list.Add(questProgress.ToString());
		}
		ES3.Save<List<string>>("Quests", list, settings);
		ES3.Save<List<string>>("RecentDailyQ", Progression.RecentDaily, settings);
		ES3.Save<List<string>>("RecentWeeklyQ", Progression.RecentWeekly, settings);
		Progression.SaveEndGameData(settings);
		if (!flag)
		{
			try
			{
				ES3.StoreCachedFile("unlocks.vel");
				ES3.CreateBackup("unlocks.vel");
			}
			catch (IOException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch
			{
			}
		}
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x00049C08 File Offset: 0x00047E08
	public static void Prestige()
	{
		if (Progression.InkLevel != Progression.MaxInkLevel)
		{
			Debug.Log("Can't Prestige - Not Max Level");
			return;
		}
		Progression.PrestigeCount++;
		try
		{
			UnlockManager.ResetForPrestige();
		}
		catch (Exception ex)
		{
			string str = "Failed to reset Unlocks for prestige: ";
			Exception ex2 = ex;
			Debug.LogError(str + ((ex2 != null) ? ex2.ToString() : null));
		}
		try
		{
			AchievementManager.ResetForPrestige();
		}
		catch (Exception ex3)
		{
			string str2 = "Failed to reset Achievements for prestige: ";
			Exception ex4 = ex3;
			Debug.LogError(str2 + ((ex4 != null) ? ex4.ToString() : null));
		}
		try
		{
			Currency.ResetForPrestige();
		}
		catch (Exception ex5)
		{
			string str3 = "Failed to reset Currency for prestige: ";
			Exception ex6 = ex5;
			Debug.LogError(str3 + ((ex6 != null) ? ex6.ToString() : null));
		}
		try
		{
			Progression.TalentBuild = new Progression.EquippedTalents();
			Settings.ResetForPrestige(false);
		}
		catch (Exception ex7)
		{
			string str4 = "Failed to reset Talent/Loadout for prestige: ";
			Exception ex8 = ex7;
			Debug.LogError(str4 + ((ex8 != null) ? ex8.ToString() : null));
		}
		Progression.LastGameData = "";
		Progression.BindingAttunement = 5;
		Progression.LevelProgress = 0;
		Progression.LossQuills = 0;
		Progression.InkLevel = 1;
		Progression.SaveState();
		Action onPrestige = Progression.OnPrestige;
		if (onPrestige != null)
		{
			onPrestige();
		}
		Debug.Log("Prestiged To Level " + Progression.PrestigeCount.ToString());
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x00049D64 File Offset: 0x00047F64
	public static void TryIncrementAttunement(int level)
	{
		if (level > Progression.BindingAttunement && Progression.BindingAttunement < 20)
		{
			Progression.BindingAttunement += Progression.GetAttunementBoost(level);
			Progression.NewAttumnentLevel = true;
			Progression.SaveState();
		}
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x00049D94 File Offset: 0x00047F94
	public static int GetAttunementBoost(int level)
	{
		if (Progression.OverbindAllowed > 0)
		{
			int num = level - Progression.BindingAttunement;
			if (num >= 5)
			{
				return 1 + Mathf.FloorToInt((float)num / 5f);
			}
		}
		if (level > Progression.BindingAttunement && Progression.BindingAttunement < 20)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00049DDC File Offset: 0x00047FDC
	[return: TupleElementNames(new string[]
	{
		"tome",
		"won",
		"level"
	})]
	public static ValueTuple<GenreTree, bool, int> ConsumeLastGameInfo()
	{
		if (string.IsNullOrEmpty(Progression.LastGameData))
		{
			return new ValueTuple<GenreTree, bool, int>(null, false, 0);
		}
		string[] array = Progression.LastGameData.Split('|', StringSplitOptions.None);
		GenreTree item = (array.Length != 0) ? GraphDB.GetGenre(array[0]) : null;
		bool item2 = false;
		if (array.Length > 1)
		{
			item2 = (array[1] == "1");
		}
		int item3 = 0;
		if (array.Length > 2)
		{
			int.TryParse(array[2], out item3);
		}
		Progression.LastGameData = "";
		ES3.Save<string>("LastGame", Progression.LastGameData, "unlocks.vel");
		return new ValueTuple<GenreTree, bool, int>(item, item2, item3);
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00049E6C File Offset: 0x0004806C
	public static void RequestQuillmarkReward(Vector3 point, GenreTree genre, int bindingLevel, bool didWin)
	{
		int num = WaveManager.instance.WavesCompleted;
		if (num == 1)
		{
			num = 0;
		}
		int num2 = (int)((float)(num * 20) * Progression.GetBindingRewardMult(GameplayManager.BindingLevel));
		if (GameStats.GetGlobalStat(GameStats.Stat.TomesWon, 0) <= 0)
		{
			num2 += 20;
		}
		num2 = Mathf.Max(20, num2);
		int num3 = 0;
		if (QuestboardPanel.AreIncentivesUnlocked)
		{
			float num4 = didWin ? 1f : Mathf.Clamp((float)num / 6f, 0.1f, 1f);
			if (GoalManager.IsIncentiveAbilityEquipped())
			{
				num3 += (int)((float)MetaDB.AbilityIncentiveReward * num4);
			}
			if (GoalManager.IsIncentiveTome(GameplayManager.instance.GameRoot))
			{
				num3 += (int)((float)MetaDB.TomeIncentiveReward * num4);
			}
			num2 += num3;
		}
		PostGame_CurrencyRewards.SetCompletionQuills(num2 - num3);
		if (didWin)
		{
			Progression.CreateQuillmarkReward(point, num2);
			return;
		}
		Progression.LossQuills = num2;
		Progression.SaveState();
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x00049F34 File Offset: 0x00048134
	public static void RequestAppendixReward(Vector3 point, int bindingLevel, int AppendixLevel, int AppendixChapter, bool didWin)
	{
		AppendixChapter--;
		int num = (int)((float)(AppendixChapter * 20) * Progression.GetBindingRewardMult(GameplayManager.BindingLevel));
		PostGame_CurrencyRewards.AddAppendixQuills(num);
		if (didWin)
		{
			Progression.CreateQuillmarkReward(point, num);
			return;
		}
		Progression.LossQuills = num;
		Progression.SaveState();
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x00049F78 File Offset: 0x00048178
	public static BossRewardTrigger CreateQuillmarkReward(Vector3 point, int amount)
	{
		if (amount <= 0)
		{
			return null;
		}
		Currency.AddLoadoutCoin(amount, true);
		Vector3 pt = point + UnityEngine.Random.insideUnitSphere * 10f;
		return GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt), Progression.BossRewardType.Quillmarks, new List<GraphTree>(), amount);
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x00049FC0 File Offset: 0x000481C0
	public static void CreateLossQuillReward()
	{
		if (Progression.LossQuills <= 0)
		{
			return;
		}
		Vector3 pt = new Vector3(0f, 4.5f, 13.5f);
		BossRewardTrigger bossRewardTrigger = GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt), Progression.BossRewardType.Quillmarks, new List<GraphTree>(), Progression.LossQuills);
		bossRewardTrigger.OnInteract = (Action)Delegate.Combine(bossRewardTrigger.OnInteract, new Action(delegate()
		{
			Currency.AddLoadoutCoin(Progression.LossQuills, true);
			Progression.LossQuills = 0;
			Progression.SaveState();
		}));
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x0004A03C File Offset: 0x0004823C
	public static float GetBindingRewardMult(int bindingLevel)
	{
		if (bindingLevel <= 0)
		{
			return 1f;
		}
		bindingLevel = Mathf.Min(bindingLevel, Mathf.Min(Progression.AttunementTarget, 20));
		if (bindingLevel == 20 && Progression.AttunementTarget >= 20)
		{
			return 7f;
		}
		return (float)Math.Round((double)Mathf.Pow(1.1f, (float)bindingLevel), 2);
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x0004A090 File Offset: 0x00048290
	public static void TrySpecialRewards(Vector3 point, GenreTree genre, int bindingLevel)
	{
		List<GraphTree> list = new List<GraphTree>();
		foreach (Unlockable unlockable in UnlockDB.GetGenreRewards(genre, bindingLevel))
		{
			UnlockDB.AugmentUnlock augmentUnlock = unlockable as UnlockDB.AugmentUnlock;
			if (augmentUnlock != null)
			{
				AugmentTree augment = augmentUnlock.Augment;
				if (!UnlockManager.IsAugmentUnlocked(augment))
				{
					list.Add(augment);
					Progression.AugmentRewards.Add(augment);
					UnlockManager.UnlockAugment(augment);
				}
			}
			else
			{
				UnlockDB.GenreUnlock genreUnlock = unlockable as UnlockDB.GenreUnlock;
				if (genreUnlock != null)
				{
					GenreTree genre2 = genreUnlock.Genre;
					if (!UnlockManager.IsGenreUnlocked(genre2))
					{
						Debug.Log("Unlocked new tome [" + genre2.Root.ShortName + "] from normal tome progression");
						Vector3 pt = point + UnityEngine.Random.insideUnitSphere * 10f;
						UnlockManager.UnlockGenre(genre2);
						BossRewardTrigger bossRewardTrigger = GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt), Progression.BossRewardType.Tome, genre2, 0);
						string str = "Rewarded for mending <i>" + genre.Root.ShortName + "</i>";
						if (unlockable.AtBinding > 0)
						{
							str += string.Format(" at Binding Level {0}", unlockable.AtBinding);
						}
						bossRewardTrigger.OverrideDetails = str + ".";
					}
				}
				else
				{
					Cosmetic cosmetic = unlockable as Cosmetic;
					if (cosmetic != null)
					{
						if (!UnlockManager.IsCosmeticUnlocked(cosmetic))
						{
							Vector3 pt2 = point + UnityEngine.Random.insideUnitSphere * 10f;
							UnlockManager.UnlockCosmetic(cosmetic);
							GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt2), Progression.BossRewardType.Cosmetic, cosmetic);
						}
					}
					else
					{
						UnlockDB.BindingUnlock bindingUnlock = unlockable as UnlockDB.BindingUnlock;
						if (bindingUnlock != null)
						{
							AugmentTree binding = bindingUnlock.Binding;
							if (!UnlockManager.IsBindingUnlocked(binding))
							{
								Vector3 pt3 = point + UnityEngine.Random.insideUnitSphere * 10f;
								UnlockManager.UnlockBinding(binding);
								BossRewardTrigger bossRewardTrigger2 = GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt3), Progression.BossRewardType.Binding, binding, 0);
								string str2 = "Rewarded for mending <i>" + genre.Root.ShortName + "</i>";
								if (unlockable.AtBinding > 0)
								{
									str2 += string.Format(" at Binding Level {0}", unlockable.AtBinding);
								}
								bossRewardTrigger2.OverrideDetails = str2 + ".";
							}
						}
					}
				}
			}
		}
		if (list.Count > 0)
		{
			Vector3 pt4 = point + UnityEngine.Random.insideUnitSphere * 8f;
			BossRewardTrigger bossRewardTrigger3 = GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt4), Progression.BossRewardType.Pages, list, 0);
			string overrideDetails = "Rewarded for mending <i>" + genre.Root.ShortName + "</i>";
			bossRewardTrigger3.OverrideDetails = overrideDetails;
		}
		Progression.UnlockBindingsReward(genre, bindingLevel, point);
		int num = 1;
		if (Progression.PrestigeCount > 0 && bindingLevel >= Progression.AttunementTarget)
		{
			int num2 = Progression.AttunementTarget - 1;
			int num3 = bindingLevel - num2;
			num += Mathf.FloorToInt((float)num3 / 5f);
			num = Mathf.Clamp(num, 2, 5);
		}
		List<UnlockDB.BindingUnlock> list2 = Progression.GenerateBindingReward(bindingLevel, num);
		if (list2.Count > 0)
		{
			foreach (UnlockDB.BindingUnlock bindingUnlock2 in list2)
			{
				Vector3 pt5 = point + UnityEngine.Random.insideUnitSphere * 8f;
				Progression.BindingRewards.Add(bindingUnlock2.Binding);
				UnlockManager.UnlockAugment(bindingUnlock2.Binding);
				BossRewardTrigger bossRewardTrigger4 = GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt5), Progression.BossRewardType.Binding, bindingUnlock2.Binding, 0);
				if (UnlockDB.GetBindingUnlock(bindingUnlock2.Binding) != null)
				{
					if (bindingUnlock2.Parent != null)
					{
						bossRewardTrigger4.OverrideDetails = "Rewarded for mending a Tome bound with " + bindingUnlock2.Parent.Root.Name + ".";
					}
					else
					{
						bossRewardTrigger4.OverrideDetails = "Rewarded for mending a bound Tome.";
					}
				}
			}
		}
		foreach (Unlockable unlockable2 in UnlockManager.NewAchievementItems)
		{
			UnlockDB.GenreUnlock genreUnlock2 = unlockable2 as UnlockDB.GenreUnlock;
			if (genreUnlock2 != null && !UnlockManager.IsGenreUnlocked(genreUnlock2.Genre))
			{
				Vector3 pt6 = point + UnityEngine.Random.insideUnitSphere * 10f;
				Debug.Log("Unlocked new tome [" + genreUnlock2.Genre.Root.ShortName + "] from Achievement");
				BossRewardTrigger bossRewardTrigger5 = GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt6), Progression.BossRewardType.Tome, genreUnlock2.Genre, 0);
				AchievementRootNode achievement = GraphDB.GetAchievement(unlockable2.Achievement);
				if (achievement != null)
				{
					string overrideDetails2 = "Completed <b>" + achievement.Name + "</b>:\n" + achievement.Detail;
					bossRewardTrigger5.OverrideDetails = overrideDetails2;
				}
			}
		}
		if (NookDB.AllowedDrops())
		{
			NookDB.NookObject nookObject = Progression.GenerateNookItemReward();
			if (nookObject != null)
			{
				Vector3 pt7 = point + UnityEngine.Random.insideUnitSphere * 6f;
				UnlockManager.UnlockNookItem(nookObject);
				GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt7), Progression.BossRewardType.NookItem, nookObject);
			}
		}
		Progression.SaveState();
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x0004A5F4 File Offset: 0x000487F4
	public static void EndGameReward(Vector3 point, bool didWin, GenreTree genre, int bindingLevel)
	{
		Progression.LastGameData = string.Format("{0}|{1}|{2}", genre.ID, didWin ? "1" : "0", bindingLevel);
		Progression.BossRewardType bossRewardType = Progression.BossRewardType.CosmCurrency;
		List<GraphTree> list = new List<GraphTree>();
		int amount = Progression.GenerateCurrencyReward(genre, didWin, bindingLevel);
		if (didWin)
		{
			Progression.GenreCompleted(genre, bindingLevel);
		}
		bossRewardType = Progression.GetRewardType(genre, didWin, bindingLevel);
		if (bossRewardType == Progression.BossRewardType.Pages)
		{
			List<AugmentTree> list2 = Progression.GenerateAugmentRewards(genre, didWin, bindingLevel);
			if (list2.Count > 0)
			{
				using (List<AugmentTree>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AugmentTree item = enumerator.Current;
						list.Add(item);
					}
					goto IL_9A;
				}
			}
			bossRewardType = Progression.BossRewardType.CosmCurrency;
		}
		IL_9A:
		if (bossRewardType == Progression.BossRewardType.CosmCurrency)
		{
			Currency.Add(amount, true);
		}
		if (didWin)
		{
			GoalManager.instance.CreateBossReward(point, bossRewardType, list, amount);
		}
		Progression.SaveState();
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x0004A6D0 File Offset: 0x000488D0
	public static void DoTutorialReward()
	{
		int globalStat = GameStats.GetGlobalStat(GameStats.Stat.TomesPlayed, 0);
		if (TutorialManager.InTutorial && globalStat > 0)
		{
			return;
		}
		Progression.TutorialReward = true;
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x0004A6F7 File Offset: 0x000488F7
	private static int GenerateCurrencyReward(GenreTree genre, bool didWin, int bindingLevel)
	{
		return 50;
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x0004A6FC File Offset: 0x000488FC
	private static List<AugmentTree> GenerateAugmentRewards(GenreTree genre, bool didWin, int bindingLevel)
	{
		int num = didWin ? 3 : 2;
		Progression.AugmentRewards = new List<AugmentTree>();
		List<AugmentTree> availableLockedPlayerMods = GraphDB.GetAvailableLockedPlayerMods();
		for (int i = 0; i < num; i++)
		{
			AugmentTree augmentTree = GraphDB.ChooseModFromList(ModType.Player, availableLockedPlayerMods, true, false);
			if (augmentTree == null)
			{
				break;
			}
			availableLockedPlayerMods.Remove(augmentTree);
			Progression.AugmentRewards.Add(augmentTree);
		}
		UnlockManager.UnlockAugments(Progression.AugmentRewards);
		foreach (AugmentTree augmentTree2 in Progression.AugmentRewards)
		{
			Progression.UnseenAugments.Add(augmentTree2.ID);
		}
		return Progression.AugmentRewards;
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x0004A7B4 File Offset: 0x000489B4
	private static GenreTree GenerateTomeReward(GenreTree genre, bool didWin, int bindingLevel)
	{
		List<UnlockDB.GenreUnlock> availableTomes = UnlockDB.GetAvailableTomes();
		if (availableTomes == null || availableTomes.Count == 0)
		{
			return null;
		}
		List<UnlockDB.GenreUnlock> list = new List<UnlockDB.GenreUnlock>();
		foreach (UnlockDB.GenreUnlock genreUnlock in availableTomes)
		{
			for (int i = 0; i < genreUnlock.Abundance; i++)
			{
				list.Add(genreUnlock);
			}
		}
		list.Shuffle(null);
		UnlockDB.GenreUnlock genreUnlock2 = list[UnityEngine.Random.Range(0, list.Count)];
		if (genreUnlock2 != null)
		{
			Progression.TomeRewards.Add(genreUnlock2.Genre);
			UnlockManager.UnlockGenre(genreUnlock2.Genre);
		}
		if (genreUnlock2 == null)
		{
			return null;
		}
		return genreUnlock2.Genre;
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x0004A878 File Offset: 0x00048A78
	public static void UnlockBindingsReward(GenreTree genre, int bindingLevel, Vector3 loc)
	{
		if (Settings.HasCompletedUITutorial(UITutorial.Tutorial.Bindings))
		{
			return;
		}
		if (genre != GenrePanel.instance.bindingReqGenre)
		{
			return;
		}
		Vector3 pt = loc + UnityEngine.Random.insideUnitSphere * 12f;
		BossRewardTrigger bossRewardTrigger = GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt), Progression.BossRewardType.BindingsAvailable, new List<GraphTree>(), 0);
		string overrideDetails = "";
		bossRewardTrigger.OverrideDetails = overrideDetails;
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x0004A8DC File Offset: 0x00048ADC
	private static List<UnlockDB.BindingUnlock> GenerateBindingReward(int bindingLevel, int numToUnlock)
	{
		List<UnlockDB.BindingUnlock> list = new List<UnlockDB.BindingUnlock>();
		if (bindingLevel == 0)
		{
			return list;
		}
		List<UnlockDB.BindingUnlock> availableBindings = UnlockDB.GetAvailableBindings(null);
		if (availableBindings == null || availableBindings.Count == 0)
		{
			return list;
		}
		int num = 0;
		while (num < numToUnlock && availableBindings.Count != 0)
		{
			UnlockDB.BindingUnlock bindingUnlock = availableBindings[UnityEngine.Random.Range(0, availableBindings.Count)];
			if (bindingUnlock != null)
			{
				availableBindings.Remove(bindingUnlock);
				list.Add(bindingUnlock);
				UnlockManager.UnlockBinding(bindingUnlock.Binding);
			}
			num++;
		}
		return list;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x0004A950 File Offset: 0x00048B50
	private static NookDB.NookObject GenerateNookItemReward()
	{
		List<NookDB.NookObject> availableDrops = NookDB.GetAvailableDrops();
		if (availableDrops == null || availableDrops.Count == 0)
		{
			return null;
		}
		return availableDrops[UnityEngine.Random.Range(0, availableDrops.Count)];
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x0004A982 File Offset: 0x00048B82
	private static Progression.BossRewardType GetRewardType(GenreTree genre, bool didWin, int bindingLevel)
	{
		if (!didWin)
		{
			if (GameStats.GetGlobalStat(GameStats.Stat.TomesPlayed, 0) < 4)
			{
				return Progression.BossRewardType.Pages;
			}
			if (UnityEngine.Random.Range(0, 100) <= 50)
			{
				return Progression.BossRewardType.CosmCurrency;
			}
			return Progression.BossRewardType.Pages;
		}
		else
		{
			if (ParseManager.IsBanned)
			{
				return Progression.BossRewardType.CosmCurrency;
			}
			return Progression.BossRewardType.Pages;
		}
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x0004A9AD File Offset: 0x00048BAD
	public static void ConsumeRewardInfo()
	{
		Progression.AugmentRewards.Clear();
		Progression.TomeRewards.Clear();
		Progression.BindingRewards.Clear();
		Progression.TutorialReward = false;
		Progression.SaveState();
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x0004A9D8 File Offset: 0x00048BD8
	public static void RequestRaidCurrencyReward(Vector3 point, int EncounterIndex, RaidDB.Difficulty difficulty, RaidDB.RaidType raidType, bool didWin)
	{
		bool flag = difficulty == RaidDB.Difficulty.Hard;
		float num = didWin ? 1f : 0.5f;
		float num2 = flag ? RaidDB.instance.HardModeQuillMultiplier : 1f;
		int num3 = (int)(RaidDB.instance.EncounterQuillmarks.Evaluate((float)EncounterIndex) * num2 * num);
		float num4 = flag ? RaidDB.instance.HardModeGildingMultiplier : 1f;
		int num5 = (int)(RaidDB.instance.EncounterGildings.Evaluate((float)EncounterIndex) * num4 * num);
		Currency.Add(num5, true);
		PostGame_CurrencyRewards.SetCompletionQuills(num3);
		PostGame_CurrencyRewards.SetGildingInfo(num5);
		if (didWin)
		{
			Progression.CreateQuillmarkReward(point, num3);
			Vector3 pt = GoalManager.FixPointOnNav(point + UnityEngine.Random.insideUnitSphere * 10f);
			GoalManager.instance.CreateBossReward(pt, Progression.BossRewardType.CosmCurrency, new List<GraphTree>(), num5);
			return;
		}
		Progression.LossQuills = num3;
		Progression.SaveState();
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0004AAB0 File Offset: 0x00048CB0
	public static void RaidEncounterReward(Vector3 point, RaidDB.Encounter encounter, RaidDB.Difficulty difficulty, RaidDB.RaidType raidType)
	{
		if (!UnlockManager.IsAugmentUnlocked(encounter.RewardPage))
		{
			AugmentTree rewardPage = encounter.RewardPage;
			UnlockManager.UnlockAugment(rewardPage);
			Vector3 pt = point + UnityEngine.Random.insideUnitSphere * 8f;
			BossRewardTrigger bossRewardTrigger = GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt), Progression.BossRewardType.Pages, new List<GraphTree>
			{
				rewardPage
			}, 0);
			string overrideDetails = "This page can now show up in <style=pos>future runs</style>.";
			bossRewardTrigger.OverrideDetails = overrideDetails;
		}
		if (difficulty == RaidDB.Difficulty.Hard)
		{
			NookDB.NookObject item = NookDB.GetItem(encounter.HMNookItem);
			if (item != null && !UnlockManager.IsNookItemUnlocked(item))
			{
				Vector3 pt2 = point + UnityEngine.Random.insideUnitSphere * 6f;
				UnlockManager.UnlockNookItem(item);
				GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt2), Progression.BossRewardType.NookItem, item);
			}
		}
		if (RaidDB.IsFinalEncounter(raidType, encounter.ID))
		{
			Progression.RaidCompleteReward(point, difficulty, raidType);
		}
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0004AB7C File Offset: 0x00048D7C
	private static void RaidCompleteReward(Vector3 point, RaidDB.Difficulty difficulty, RaidDB.RaidType raidType)
	{
		List<NookDB.NookObject> list = new List<NookDB.NookObject>();
		foreach (string id in RaidDB.instance.FinalBossNookItems)
		{
			NookDB.NookObject item = NookDB.GetItem(id);
			if (item != null && !UnlockManager.IsNookItemUnlocked(item))
			{
				list.Add(item);
			}
		}
		if (list.Count > 0)
		{
			NookDB.NookObject nookObject = list[UnityEngine.Random.Range(0, list.Count)];
			Vector3 pt = point + UnityEngine.Random.insideUnitSphere * 6f;
			UnlockManager.UnlockNookItem(nookObject);
			GoalManager.instance.CreateBossReward(GoalManager.FixPointOnNav(pt), Progression.BossRewardType.NookItem, nookObject);
		}
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x0004AC38 File Offset: 0x00048E38
	private static void LoadEndGameData()
	{
		Dictionary<string, int> dictionary = ES3.Load<Dictionary<string, int>>("Best_Runs", "unlocks.vel", new Dictionary<string, int>());
		Progression.HighestBindingLevel.Clear();
		foreach (KeyValuePair<string, int> keyValuePair in dictionary)
		{
			string text;
			int num;
			keyValuePair.Deconstruct(out text, out num);
			string guid = text;
			int value = num;
			GenreTree genre = GraphDB.GetGenre(guid);
			if (!(genre == null) && !Progression.HighestBindingLevel.ContainsKey(genre))
			{
				Progression.HighestBindingLevel.Add(genre, value);
			}
		}
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x0004ACD8 File Offset: 0x00048ED8
	private static void SaveEndGameData(ES3Settings settings)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (KeyValuePair<GenreTree, int> keyValuePair in Progression.HighestBindingLevel)
		{
			GenreTree genreTree;
			int num;
			keyValuePair.Deconstruct(out genreTree, out num);
			GenreTree genreTree2 = genreTree;
			int value = num;
			dictionary.Add(genreTree2.ID, value);
		}
		ES3.Save<Dictionary<string, int>>("Best_Runs", dictionary, settings);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x0004AD54 File Offset: 0x00048F54
	private static void GenreCompleted(GenreTree genre, int bindingLevel)
	{
		if (Progression.HighestBindingLevel.ContainsKey(genre) && Progression.HighestBindingLevel[genre] >= bindingLevel)
		{
			return;
		}
		Progression.HighestBindingLevel[genre] = bindingLevel;
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x0004AD7E File Offset: 0x00048F7E
	public static void SawBoss(string bossID)
	{
		Progression.LastBoss = bossID;
		Progression.SaveState();
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x0004AD8B File Offset: 0x00048F8B
	public static void SawBookClubChallenge(string challenge)
	{
		Progression.LastSeenBookClubChallenge = challenge;
		Progression.SaveState();
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x0004AD98 File Offset: 0x00048F98
	public static void SawAugment(string Augment)
	{
		Progression.SawAugment(new List<string>
		{
			Augment
		});
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x0004ADAC File Offset: 0x00048FAC
	public static void SawAugment(List<string> Augments)
	{
		bool flag = false;
		foreach (string item in Augments)
		{
			if (!Progression.FoundAugments.Contains(item))
			{
				flag = true;
				Progression.FoundAugments.Add(item);
			}
		}
		if (flag)
		{
			Progression.SaveState();
		}
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x0004AE18 File Offset: 0x00049018
	public static void AddQuest(MetaDB.DailyQuest quest)
	{
		MetaDB.QuestProgress item = new MetaDB.QuestProgress(quest);
		Progression.Quests.Add(item);
		if (quest.Timing == MetaDB.DailyQuest.Timescale.Daily)
		{
			Progression.RecentDaily.Add(quest.ID);
			if (Progression.RecentDaily.Count > 10)
			{
				Progression.RecentDaily.RemoveAt(0);
			}
		}
		else if (quest.Timing == MetaDB.DailyQuest.Timescale.Weekly)
		{
			Progression.RecentWeekly.Add(quest.ID);
			if (Progression.RecentWeekly.Count > 4)
			{
				Progression.RecentWeekly.RemoveAt(0);
			}
		}
		Progression.questAugments = null;
		Progression.SaveState();
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x0004AEA8 File Offset: 0x000490A8
	public static bool CanCompleteQuest(string ID)
	{
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			if (questProgress.ID == ID)
			{
				return !questProgress.IsComplete;
			}
		}
		return false;
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x0004AF10 File Offset: 0x00049110
	public static MetaDB.QuestProgress GetQuestProgress(string ID)
	{
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			if (questProgress.ID == ID)
			{
				return questProgress;
			}
		}
		return null;
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0004AF70 File Offset: 0x00049170
	public static Augments CurrentQuestAugments()
	{
		if (Progression.questAugments != null)
		{
			return Progression.questAugments;
		}
		Progression.questAugments = new Augments();
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			if (!questProgress.IsComplete)
			{
				MetaDB.DailyQuest quest = MetaDB.GetQuest(questProgress.ID);
				if (quest != null && !(quest.Graph == null))
				{
					Progression.questAugments.Add(quest.Graph, 1);
				}
			}
		}
		return Progression.questAugments;
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x0004B014 File Offset: 0x00049214
	public static void RemoveQuest(string ID)
	{
		MetaDB.QuestProgress questProgress = null;
		foreach (MetaDB.QuestProgress questProgress2 in Progression.Quests)
		{
			if (questProgress2.ID == ID)
			{
				questProgress = questProgress2;
				break;
			}
		}
		if (questProgress != null)
		{
			Debug.Log("Removing Saved Quest Progress for " + ID);
			MetaDB.DailyQuest quest = MetaDB.GetQuest(ID);
			if (quest != null && quest.UsesStat)
			{
				GameStats.ResetEphemeral(quest.StatID);
			}
			Progression.Quests.Remove(questProgress);
			Progression.SaveState();
			Progression.questAugments = null;
		}
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x0004B0BC File Offset: 0x000492BC
	public static void CompleteQuest(string ID)
	{
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			if (questProgress.ID == ID && !questProgress.IsComplete)
			{
				questProgress.IsComplete = true;
				Progression.SaveState();
				Progression.questAugments = null;
				break;
			}
		}
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x0004B134 File Offset: 0x00049334
	public static void CollectQuest(string ID)
	{
		bool flag = false;
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			if (questProgress.ID == ID && !questProgress.IsCollected)
			{
				questProgress.IsCollected = true;
				flag = true;
				break;
			}
		}
		if (flag)
		{
			Progression.questAugments = null;
			Progression.SaveState();
		}
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x0004B1B0 File Offset: 0x000493B0
	public static bool AddLevelProgress(int amount, bool save = true)
	{
		int nextInkLevelRequirement = PlayerDB.GetNextInkLevelRequirement(Progression.InkLevel);
		if (nextInkLevelRequirement < 0)
		{
			return false;
		}
		bool result = false;
		if (Progression.LevelProgress + amount >= nextInkLevelRequirement)
		{
			amount -= nextInkLevelRequirement - Progression.LevelProgress;
			Progression.LevelProgress = 0;
			Progression.InkLevel++;
			result = true;
		}
		Progression.LevelProgress += amount;
		if (save)
		{
			Progression.SaveState();
			if (PlayerControl.myInstance != null)
			{
				PlayerControl.myInstance.TriggerSnippets(EventTrigger.Player_MetaEvent, null, 1f);
			}
		}
		return result;
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x0004B230 File Offset: 0x00049430
	public static int RemainingInkForLevel()
	{
		int nextInkLevelRequirement = PlayerDB.GetNextInkLevelRequirement(Progression.InkLevel);
		if (nextInkLevelRequirement < 0)
		{
			return 0;
		}
		return nextInkLevelRequirement - Progression.LevelProgress;
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x0004B258 File Offset: 0x00049458
	// Note: this type is marked as 'beforefieldinit'.
	static Progression()
	{
	}

	// Token: 0x0400094D RID: 2381
	public static List<AugmentTree> AugmentRewards = new List<AugmentTree>();

	// Token: 0x0400094E RID: 2382
	public static List<GenreTree> TomeRewards = new List<GenreTree>();

	// Token: 0x0400094F RID: 2383
	public static List<AugmentTree> BindingRewards = new List<AugmentTree>();

	// Token: 0x04000950 RID: 2384
	public static string LastGameData = "";

	// Token: 0x04000951 RID: 2385
	public static bool TutorialReward;

	// Token: 0x04000952 RID: 2386
	public static int LossQuills;

	// Token: 0x04000953 RID: 2387
	public static HashSet<string> UnseenAugments = new HashSet<string>();

	// Token: 0x04000954 RID: 2388
	public static string LastBoss = "";

	// Token: 0x04000955 RID: 2389
	public static string LastSeenBookClubChallenge = "";

	// Token: 0x04000956 RID: 2390
	public static HashSet<string> FoundAugments = new HashSet<string>();

	// Token: 0x04000957 RID: 2391
	public static Dictionary<GenreTree, int> HighestBindingLevel = new Dictionary<GenreTree, int>();

	// Token: 0x04000958 RID: 2392
	public static int BindingAttunement = 0;

	// Token: 0x04000959 RID: 2393
	public static bool NewAttumnentLevel = false;

	// Token: 0x0400095A RID: 2394
	public const int MAX_BINDING_ATTUNMENT = 20;

	// Token: 0x0400095B RID: 2395
	public const int OVERBIND_THRESHOLD = 5;

	// Token: 0x0400095C RID: 2396
	[CompilerGenerated]
	private static int <InkLevel>k__BackingField;

	// Token: 0x0400095D RID: 2397
	public static int LevelProgress;

	// Token: 0x0400095E RID: 2398
	public static int PrestigeCount;

	// Token: 0x0400095F RID: 2399
	public static Action OnPrestige;

	// Token: 0x04000960 RID: 2400
	public static List<MetaDB.QuestProgress> Quests = new List<MetaDB.QuestProgress>();

	// Token: 0x04000961 RID: 2401
	public static List<string> RecentDaily = new List<string>();

	// Token: 0x04000962 RID: 2402
	public static List<string> RecentWeekly = new List<string>();

	// Token: 0x04000963 RID: 2403
	public static Progression.EquippedTalents TalentBuild;

	// Token: 0x04000964 RID: 2404
	public static int LibTalentsUnlocked = 0;

	// Token: 0x04000965 RID: 2405
	public static Progression.LibraryLoadout LibraryBuild;

	// Token: 0x04000966 RID: 2406
	public static bool BadLoad;

	// Token: 0x04000967 RID: 2407
	private static bool snapshotAllowed;

	// Token: 0x04000968 RID: 2408
	private static bool _initialized = false;

	// Token: 0x04000969 RID: 2409
	private static Augments questAugments;

	// Token: 0x020004ED RID: 1261
	[Serializable]
	public class Loadout
	{
		// Token: 0x06002338 RID: 9016 RVA: 0x000C8C14 File Offset: 0x000C6E14
		public Loadout(string input)
		{
			string[] array = input.Split('|', StringSplitOptions.None);
			if (array.Length != 4)
			{
				return;
			}
			this.Core = GraphDB.GetAugment(array[0]);
			this.Generator = GraphDB.GetAbility(array[1]);
			this.Spender = GraphDB.GetAbility(array[2]);
			this.Movement = GraphDB.GetAbility(array[3]);
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000C8C70 File Offset: 0x000C6E70
		public Loadout(PlayerControl player)
		{
			this.Core = player.actions.core;
			this.Generator = player.actions.primary;
			this.Spender = player.actions.secondary;
			this.Movement = player.actions.movement;
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000C8CC8 File Offset: 0x000C6EC8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.Core.ID,
				"|",
				this.Generator.ID,
				"|",
				this.Spender.ID,
				"|",
				this.Movement.ID
			});
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000C8D30 File Offset: 0x000C6F30
		public bool Matches(Progression.Loadout l)
		{
			return !(l.Core != this.Core) && !(l.Spender != this.Spender) && !(l.Generator != this.Generator) && !(l.Movement != this.Movement);
		}

		// Token: 0x04002509 RID: 9481
		public AugmentTree Core;

		// Token: 0x0400250A RID: 9482
		public AbilityTree Generator;

		// Token: 0x0400250B RID: 9483
		public AbilityTree Spender;

		// Token: 0x0400250C RID: 9484
		public AbilityTree Movement;
	}

	// Token: 0x020004EE RID: 1262
	public class EquippedTalents
	{
		// Token: 0x0600233C RID: 9020 RVA: 0x000C8D8C File Offset: 0x000C6F8C
		public EquippedTalents()
		{
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000C8DA0 File Offset: 0x000C6FA0
		public EquippedTalents(string input)
		{
			if (input == null)
			{
				return;
			}
			JSONNode jsonnode = JSON.Parse(input);
			foreach (MagicColor magicColor in (MagicColor[])Enum.GetValues(typeof(MagicColor)))
			{
				int num = (int)magicColor;
				string aKey = num.ToString();
				if (jsonnode.HasKey(aKey))
				{
					List<int> list = new List<int>();
					foreach (KeyValuePair<string, JSONNode> keyValuePair in (jsonnode[aKey] as JSONArray))
					{
						list.Add(keyValuePair.Value.AsInt);
					}
					this.SelectedTalents.Add(magicColor, list);
				}
			}
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000C8E58 File Offset: 0x000C7058
		public override string ToString()
		{
			JSONNode jsonnode = new JSONObject();
			foreach (KeyValuePair<MagicColor, List<int>> keyValuePair in this.SelectedTalents)
			{
				MagicColor magicColor;
				List<int> list;
				keyValuePair.Deconstruct(out magicColor, out list);
				MagicColor magicColor2 = magicColor;
				List<int> list2 = list;
				JSONArray jsonarray = new JSONArray();
				for (int i = 0; i < list2.Count; i++)
				{
					jsonarray.Add(list2[i]);
				}
				JSONNode jsonnode2 = jsonnode;
				int num = (int)magicColor2;
				jsonnode2.Add(num.ToString(), jsonarray);
			}
			return jsonnode.ToString();
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000C8F04 File Offset: 0x000C7104
		public Progression.EquippedTalents Clone()
		{
			Progression.EquippedTalents equippedTalents = new Progression.EquippedTalents();
			equippedTalents.SelectedTalents = this.SelectedTalents.ToDictionary((KeyValuePair<MagicColor, List<int>> entry) => entry.Key, (KeyValuePair<MagicColor, List<int>> entry) => new List<int>(entry.Value));
			return equippedTalents;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000C8F68 File Offset: 0x000C7168
		public bool Matches(Progression.EquippedTalents talents)
		{
			if (this.SelectedTalents.Count != talents.SelectedTalents.Count || !this.SelectedTalents.Keys.All(new Func<MagicColor, bool>(talents.SelectedTalents.ContainsKey)))
			{
				return false;
			}
			foreach (KeyValuePair<MagicColor, List<int>> keyValuePair in this.SelectedTalents)
			{
				MagicColor magicColor;
				List<int> list;
				keyValuePair.Deconstruct(out magicColor, out list);
				MagicColor key = magicColor;
				List<int> list2 = list;
				List<int> list3 = talents.SelectedTalents[key];
				if (list2.Count != list3.Count)
				{
					return false;
				}
				for (int i = 0; i < list2.Count; i++)
				{
					if (list2[i] != list3[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0400250D RID: 9485
		public Dictionary<MagicColor, List<int>> SelectedTalents = new Dictionary<MagicColor, List<int>>();

		// Token: 0x020006BF RID: 1727
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600285F RID: 10335 RVA: 0x000D8771 File Offset: 0x000D6971
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002860 RID: 10336 RVA: 0x000D877D File Offset: 0x000D697D
			public <>c()
			{
			}

			// Token: 0x06002861 RID: 10337 RVA: 0x000D8785 File Offset: 0x000D6985
			internal MagicColor <Clone>b__4_0(KeyValuePair<MagicColor, List<int>> entry)
			{
				return entry.Key;
			}

			// Token: 0x06002862 RID: 10338 RVA: 0x000D878E File Offset: 0x000D698E
			internal List<int> <Clone>b__4_1(KeyValuePair<MagicColor, List<int>> entry)
			{
				return new List<int>(entry.Value);
			}

			// Token: 0x04002CD8 RID: 11480
			public static readonly Progression.EquippedTalents.<>c <>9 = new Progression.EquippedTalents.<>c();

			// Token: 0x04002CD9 RID: 11481
			public static Func<KeyValuePair<MagicColor, List<int>>, MagicColor> <>9__4_0;

			// Token: 0x04002CDA RID: 11482
			public static Func<KeyValuePair<MagicColor, List<int>>, List<int>> <>9__4_1;
		}
	}

	// Token: 0x020004EF RID: 1263
	public class LibraryLoadout
	{
		// Token: 0x06002341 RID: 9025 RVA: 0x000C9058 File Offset: 0x000C7258
		public LibraryLoadout()
		{
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000C906C File Offset: 0x000C726C
		public LibraryLoadout(string input)
		{
			if (input == null)
			{
				return;
			}
			JSONNode jsonnode = JSON.Parse(input);
			if (!jsonnode.HasKey("selected"))
			{
				return;
			}
			foreach (KeyValuePair<string, JSONNode> keyValuePair in (jsonnode["selected"] as JSONArray))
			{
				this.TalentSelections.Add(keyValuePair.Value.AsInt);
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000C90E4 File Offset: 0x000C72E4
		public override string ToString()
		{
			JSONNode jsonnode = new JSONObject();
			JSONArray jsonarray = new JSONArray();
			for (int i = 0; i < this.TalentSelections.Count; i++)
			{
				jsonarray.Add(this.TalentSelections[i]);
			}
			jsonnode.Add("selected", jsonarray);
			return jsonnode.ToString();
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000C913C File Offset: 0x000C733C
		public void ChangeSavedTalent(int row, int index)
		{
			if (row >= this.TalentSelections.Count)
			{
				int num = row - this.TalentSelections.Count + 1;
				for (int i = 0; i < num; i++)
				{
					this.TalentSelections.Add(0);
				}
			}
			this.TalentSelections[row] = index;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000C918C File Offset: 0x000C738C
		public List<AugmentTree> GetEquippedTalents()
		{
			List<AugmentTree> list = new List<AugmentTree>();
			int num = 0;
			while (num < Progression.LibTalentsUnlocked && num < PlayerDB.LibraryTalents.Count)
			{
				List<AugmentTree> talents = PlayerDB.LibraryTalents[num].Talents;
				AugmentTree augmentTree = talents[0];
				if (this.TalentSelections.Count > num && this.TalentSelections[num] >= 0 && this.TalentSelections[num] < talents.Count)
				{
					augmentTree = talents[this.TalentSelections[num]];
				}
				if (augmentTree != null)
				{
					list.Add(augmentTree);
				}
				num++;
			}
			return list;
		}

		// Token: 0x0400250E RID: 9486
		public List<int> TalentSelections = new List<int>();
	}

	// Token: 0x020004F0 RID: 1264
	public class FullLoadout
	{
		// Token: 0x06002346 RID: 9030 RVA: 0x000C9230 File Offset: 0x000C7430
		public FullLoadout(string input)
		{
			JSONNode jsonnode = JSON.Parse(input);
			this.Abilities = new Progression.Loadout(jsonnode.GetValueOrDefault("Abilities", ""));
			this.Talents = new Progression.EquippedTalents(jsonnode.GetValueOrDefault("Talents", ""));
			this.Cosmetics = new CosmeticSet(jsonnode.GetValueOrDefault("Cosmetics", ""));
			if (jsonnode.HasKey("Name"))
			{
				this.Name = jsonnode.GetValueOrDefault("Name", "");
				return;
			}
			this.Name = this.GenerateName();
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000C9300 File Offset: 0x000C7500
		public FullLoadout(PlayerControl player)
		{
			this.Abilities = new Progression.Loadout(player);
			this.Talents = Progression.TalentBuild.Clone();
			this.Cosmetics = Settings.GetOutfit();
			this.Name = this.GenerateName();
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000C9354 File Offset: 0x000C7554
		public void SetEquipped()
		{
			PlayerControl.myInstance.actions.ApplyLoadout(this.Abilities, false);
			Progression.TalentBuild = this.Talents.Clone();
			PlayerControl.myInstance.Display.ChangeCosmeticSet(this.Cosmetics);
			Settings.SaveOutfit();
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000C93A4 File Offset: 0x000C75A4
		public void UpdateData(Progression.FullLoadout loadout, bool includeName = false)
		{
			if (this.Abilities.Core != loadout.Abilities.Core)
			{
				this.Name = this.GenerateName();
			}
			else if (includeName)
			{
				this.Name = loadout.Name;
			}
			this.Abilities = loadout.Abilities;
			this.Talents = loadout.Talents;
			this.Cosmetics = loadout.Cosmetics;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000C9410 File Offset: 0x000C7610
		public string GenerateName()
		{
			if (this.Abilities == null)
			{
				return "Loadout";
			}
			PlayerDB.CoreDisplay core = PlayerDB.GetCore(this.Abilities.Core);
			string str = "";
			if (core != null && core.Adjectives.Count > 0)
			{
				str = core.Adjectives[UnityEngine.Random.Range(0, core.Adjectives.Count)] + " ";
			}
			string str2 = MetaDB.LoadoutNounList[UnityEngine.Random.Range(0, MetaDB.LoadoutNounList.Count)];
			return str + str2;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000C949C File Offset: 0x000C769C
		public override string ToString()
		{
			JSONObject jsonobject = new JSONObject();
			jsonobject.Add("Name", this.Name);
			jsonobject.Add("Abilities", this.Abilities.ToString());
			jsonobject.Add("Talents", this.Talents.ToString());
			jsonobject.Add("Cosmetics", this.Cosmetics.ToString());
			return jsonobject.ToString();
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000C951C File Offset: 0x000C771C
		public bool CanEquip()
		{
			return UnlockManager.IsCoreUnlocked(this.Abilities.Core) && UnlockManager.IsAbilityUnlocked(this.Abilities.Generator) && UnlockManager.IsAbilityUnlocked(this.Abilities.Spender) && UnlockManager.IsAbilityUnlocked(this.Abilities.Movement);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000C957C File Offset: 0x000C777C
		public bool Matches(Progression.FullLoadout l)
		{
			return l.Abilities.Matches(this.Abilities) && l.Talents.Matches(this.Talents) && l.Cosmetics.Matches(this.Cosmetics);
		}

		// Token: 0x0400250F RID: 9487
		public string Name = "Loadout";

		// Token: 0x04002510 RID: 9488
		public Progression.Loadout Abilities;

		// Token: 0x04002511 RID: 9489
		public Progression.EquippedTalents Talents;

		// Token: 0x04002512 RID: 9490
		public CosmeticSet Cosmetics;
	}

	// Token: 0x020004F1 RID: 1265
	public enum BossRewardType
	{
		// Token: 0x04002514 RID: 9492
		_,
		// Token: 0x04002515 RID: 9493
		Tome,
		// Token: 0x04002516 RID: 9494
		Binding,
		// Token: 0x04002517 RID: 9495
		Pages,
		// Token: 0x04002518 RID: 9496
		CosmCurrency,
		// Token: 0x04002519 RID: 9497
		Quillmarks,
		// Token: 0x0400251A RID: 9498
		Cosmetic,
		// Token: 0x0400251B RID: 9499
		BindingsAvailable,
		// Token: 0x0400251C RID: 9500
		NookItem
	}

	// Token: 0x020004F2 RID: 1266
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600234E RID: 9038 RVA: 0x000C95C9 File Offset: 0x000C77C9
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000C95D5 File Offset: 0x000C77D5
		public <>c()
		{
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000C95DD File Offset: 0x000C77DD
		internal int <get_BestBinding>b__12_0(KeyValuePair<GenreTree, int> v)
		{
			return v.Value;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000C95E6 File Offset: 0x000C77E6
		internal string <SaveState>b__50_0(AugmentTree v)
		{
			return v.ID;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x000C95EE File Offset: 0x000C77EE
		internal string <SaveState>b__50_1(GenreTree v)
		{
			return v.ID;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000C95F6 File Offset: 0x000C77F6
		internal string <SaveState>b__50_2(AugmentTree v)
		{
			return v.ID;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000C95FE File Offset: 0x000C77FE
		internal void <CreateLossQuillReward>b__58_0()
		{
			Currency.AddLoadoutCoin(Progression.LossQuills, true);
			Progression.LossQuills = 0;
			Progression.SaveState();
		}

		// Token: 0x0400251D RID: 9501
		public static readonly Progression.<>c <>9 = new Progression.<>c();

		// Token: 0x0400251E RID: 9502
		public static Func<KeyValuePair<GenreTree, int>, int> <>9__12_0;

		// Token: 0x0400251F RID: 9503
		public static Func<AugmentTree, string> <>9__50_0;

		// Token: 0x04002520 RID: 9504
		public static Func<GenreTree, string> <>9__50_1;

		// Token: 0x04002521 RID: 9505
		public static Func<AugmentTree, string> <>9__50_2;

		// Token: 0x04002522 RID: 9506
		public static Action <>9__58_0;
	}
}
