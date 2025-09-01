using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000EF RID: 239
public static class GameStats
{
	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0004699C File Offset: 0x00044B9C
	// (set) Token: 0x06000AEB RID: 2795 RVA: 0x000469AF File Offset: 0x00044BAF
	public static bool Initialized
	{
		get
		{
			if (!GameStats._initialized)
			{
				GameStats.Initialize();
			}
			return GameStats._initialized;
		}
		set
		{
			GameStats._initialized = value;
		}
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x000469B8 File Offset: 0x00044BB8
	private static void Initialize()
	{
		if (!UnlockManager.Initialized)
		{
			return;
		}
		GameStats.snapshotAllowed = true;
		ES3Settings settings = new ES3Settings("stats.vel", null);
		GameStats.PlayerStats = new Dictionary<PlayerStat, ulong>();
		GameStats.GlobalStats = new Dictionary<GameStats.Stat, int>();
		GameStats.SignatureStats = new Dictionary<MagicColor, Dictionary<GameStats.SignatureStat, ulong>>();
		GameStats.SpecialStats = new Dictionary<GameStats.SpecialStat, ulong>();
		GameStats.TomeStats = new Dictionary<string, Dictionary<GameStats.Stat, int>>();
		GameStats.RaidStats = new Dictionary<string, Dictionary<GameStats.RaidStat, int>>();
		GameStats.RaidStickers = new Dictionary<GameStats.RaidStickerType, HashSet<string>>();
		GameStats.BookClubStats = new Dictionary<string, Dictionary<GameStats.BookClubStat, int>>();
		GameStats.EnemyStats = new Dictionary<string, ValueTuple<int, int>>();
		GameStats.EphemeralStats = new Dictionary<string, ulong>();
		GameStats.LibraryRaces = new Dictionary<string, float>();
		GameStats.ValidateStatFile();
		try
		{
			ES3.CacheFile("stats.vel");
			settings = new ES3Settings("stats.vel", new Enum[]
			{
				ES3.Location.Cache
			});
			GameStats.GlobalStats = ES3.Load<Dictionary<GameStats.Stat, int>>("GlobalStats", new Dictionary<GameStats.Stat, int>(), settings);
			GameStats.SignatureStats = ES3.Load<Dictionary<MagicColor, Dictionary<GameStats.SignatureStat, ulong>>>("ColorStats", new Dictionary<MagicColor, Dictionary<GameStats.SignatureStat, ulong>>(), settings);
			GameStats.TomeStats = ES3.Load<Dictionary<string, Dictionary<GameStats.Stat, int>>>("TomeStats", new Dictionary<string, Dictionary<GameStats.Stat, int>>(), settings);
			GameStats.RaidStats = ES3.Load<Dictionary<string, Dictionary<GameStats.RaidStat, int>>>("RaidStats", new Dictionary<string, Dictionary<GameStats.RaidStat, int>>(), settings);
			GameStats.RaidStickers = ES3.Load<Dictionary<GameStats.RaidStickerType, HashSet<string>>>("RaidStickers", new Dictionary<GameStats.RaidStickerType, HashSet<string>>(), settings);
			GameStats.BookClubStats = ES3.Load<Dictionary<string, Dictionary<GameStats.BookClubStat, int>>>("BookClubStats", new Dictionary<string, Dictionary<GameStats.BookClubStat, int>>(), settings);
			GameStats.SpecialStats = ES3.Load<Dictionary<GameStats.SpecialStat, ulong>>("SpecialStats", new Dictionary<GameStats.SpecialStat, ulong>(), settings);
			GameStats.PlayerStats = ES3.Load<Dictionary<PlayerStat, ulong>>("PlrStats", new Dictionary<PlayerStat, ulong>(), settings);
			GameStats.EphemeralStats = ES3.Load<Dictionary<string, ulong>>("EphStats", new Dictionary<string, ulong>(), settings);
			GameStats.EnemyStats = ES3.Load<Dictionary<string, ValueTuple<int, int>>>("EnemyStats", new Dictionary<string, ValueTuple<int, int>>(), settings);
			GameStats.LibraryRaces = ES3.Load<Dictionary<string, float>>("LibraryRaces", new Dictionary<string, float>(), settings);
		}
		catch (Exception ex)
		{
			Debug.Log("Failed to load stat values: " + ex.ToString());
			GameStats.snapshotAllowed = false;
			Progression.BadLoad = true;
		}
		GameStats._initialized = true;
		GameStats.NeedsSave = false;
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x00046BAC File Offset: 0x00044DAC
	private static void ValidateStatFile()
	{
		try
		{
			ES3.Load<Dictionary<GameStats.Stat, int>>("GlobalStats", "stats.vel");
		}
		catch (Exception ex)
		{
			string str = "Caught Exception durring Stat file loading: ";
			Exception ex2 = ex;
			Debug.Log(str + ((ex2 != null) ? ex2.ToString() : null));
			if (ES3.RestoreBackup("stats.vel"))
			{
				Debug.Log("Backup Stat File Restored");
			}
			else
			{
				string path = Path.Combine(Application.persistentDataPath, "stats.vel");
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

	// Token: 0x06000AEE RID: 2798 RVA: 0x00046C50 File Offset: 0x00044E50
	public static void LoadFromBackup()
	{
		GameStats._initialized = false;
		if (Settings.LoadLatestBackup("stats.vel"))
		{
			GameStats.Initialize();
			return;
		}
		Debug.LogError("Failed to load backup file");
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x00046C74 File Offset: 0x00044E74
	public static void SaveIfNeeded()
	{
		if (!GameStats.NeedsSave)
		{
			return;
		}
		GameStats.Save();
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x00046C84 File Offset: 0x00044E84
	public static void PlayerDamageDone(DamageInfo dmg)
	{
		if (!GameplayManager.ShouldTrackInGameStats)
		{
			return;
		}
		switch (dmg.DamageType)
		{
		case DNumType.Crit:
			if (dmg.TotalAmount > 2500f)
			{
				GameStats.IncrementStat(MagicColor.Yellow, GameStats.SignatureStat.BigFlourishHits, 1U, false);
			}
			GameStats.TryUpdateMax(MagicColor.Yellow, GameStats.SignatureStat.FlourishBiggestHit, (uint)dmg.TotalAmount, false);
			GameStats.IncrementStat(MagicColor.Yellow, GameStats.SignatureStat.FlourishDamage, (uint)dmg.TotalAmount, false);
			return;
		case DNumType.Blot:
			if (dmg.TotalAmount >= 7500f)
			{
				AchievementManager.Unlock("CHALLENGE_BLUE_TICKDAMAGE");
			}
			GameStats.IncrementStat(MagicColor.Blue, GameStats.SignatureStat.BlotDamage, (uint)dmg.TotalAmount, false);
			return;
		case DNumType.Finalize:
			if (dmg.TotalAmount > 100000f)
			{
				AchievementManager.Unlock("CHALLENGE_PINK_BIGHIT");
			}
			GameStats.IncrementStat(MagicColor.Pink, GameStats.SignatureStat.FinalizeDamage, (uint)dmg.TotalAmount, false);
			return;
		case DNumType.Greenling:
			if (dmg.TotalAmount >= 20000f)
			{
				AchievementManager.Unlock("CHALLENGE_GREEN_BIGHIT");
			}
			GameStats.IncrementStat(MagicColor.Green, GameStats.SignatureStat.InklingDamage, (uint)dmg.TotalAmount, false);
			return;
		case DNumType.Red:
			GameStats.IncrementStat(MagicColor.Red, GameStats.SignatureStat.TetherDamage, (uint)dmg.TotalAmount, false);
			return;
		case DNumType.Orange:
			GameStats.IncrementStat(MagicColor.Orange, GameStats.SignatureStat.Atlas_Damage, (uint)dmg.TotalAmount, false);
			return;
		case DNumType.Teal:
			GameStats.IncrementStat(MagicColor.Teal, GameStats.SignatureStat.ShardDamage, (uint)dmg.TotalAmount, false);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x00046DC4 File Offset: 0x00044FC4
	public static int GetGlobalStat(GameStats.Stat stat, int defaultVal = 0)
	{
		if (!GameStats.Initialized)
		{
			return 0;
		}
		if (stat == GameStats.Stat.BindingAttunement)
		{
			return Progression.BindingAttunement;
		}
		if (stat == GameStats.Stat.InkLevel)
		{
			return Progression.InkLevel;
		}
		if (stat == GameStats.Stat.PrestigeLevel)
		{
			return Progression.PrestigeCount;
		}
		int result;
		if (!GameStats.GlobalStats.TryGetValue(stat, out result))
		{
			return defaultVal;
		}
		return result;
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x00046E0C File Offset: 0x0004500C
	public static ulong GetPlayerStat(PlayerStat stat, uint defaultVal = 0U)
	{
		if (!GameStats.Initialized)
		{
			return 0UL;
		}
		ulong result;
		if (!GameStats.PlayerStats.TryGetValue(stat, out result))
		{
			return (ulong)defaultVal;
		}
		return result;
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00046E36 File Offset: 0x00045036
	public static int GetSpecialStat(GameStats.SpecialStat stat)
	{
		if (GameStats.SpecialStats.ContainsKey(stat))
		{
			return (int)GameStats.SpecialStats[stat];
		}
		return 0;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00046E54 File Offset: 0x00045054
	public static int GetCompositStat(GameStats.CompositeStat stat)
	{
		int num = 0;
		switch (stat)
		{
		case GameStats.CompositeStat.UniqueTomesPlayed:
			return GameStats.TomeStats.Count;
		case GameStats.CompositeStat.UniqueTomesBeaten:
			foreach (KeyValuePair<string, Dictionary<GameStats.Stat, int>> keyValuePair in GameStats.TomeStats)
			{
				if (keyValuePair.Value.ContainsKey(GameStats.Stat.TomesWon))
				{
					num++;
				}
			}
			return num;
		case GameStats.CompositeStat.MaxBindings:
			foreach (KeyValuePair<string, Dictionary<GameStats.Stat, int>> keyValuePair2 in GameStats.TomeStats)
			{
				int num2;
				if (keyValuePair2.Value.TryGetValue(GameStats.Stat.MaxBinding, out num2))
				{
					num += num2;
				}
			}
			return num;
		case GameStats.CompositeStat.BookClubsCompleted:
			return GameStats.BookClubStats.Count;
		case GameStats.CompositeStat.RaidEncountersCompleted:
			foreach (KeyValuePair<string, Dictionary<GameStats.RaidStat, int>> keyValuePair3 in GameStats.RaidStats)
			{
				int num3;
				if (keyValuePair3.Value.TryGetValue(GameStats.RaidStat.Completed, out num3))
				{
					num += num3;
				}
			}
			return num;
		case GameStats.CompositeStat.RaidHardModeEncountersCompleted:
			foreach (KeyValuePair<string, Dictionary<GameStats.RaidStat, int>> keyValuePair4 in GameStats.RaidStats)
			{
				int num4;
				if (keyValuePair4.Value.TryGetValue(GameStats.RaidStat.HardMode_Completed, out num4))
				{
					num += num4;
				}
			}
			return num;
		default:
			return num;
		}
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00046FEC File Offset: 0x000451EC
	[return: TupleElementNames(new string[]
	{
		"killed",
		"killedBy"
	})]
	public static ValueTuple<int, int> GetEnemyStat(string enemyID)
	{
		ValueTuple<int, int> result;
		if (GameStats.EnemyStats.TryGetValue(enemyID, out result))
		{
			return result;
		}
		return new ValueTuple<int, int>(0, 0);
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00047014 File Offset: 0x00045214
	public static int GetMetaStat(GameStats.MetaStat stat)
	{
		int result;
		switch (stat)
		{
		case GameStats.MetaStat.InkLevel:
			result = Progression.InkLevel;
			break;
		case GameStats.MetaStat.SpellsUnlocked:
			result = UnlockManager.AbilitiesUnlocked();
			break;
		case GameStats.MetaStat.CoresUnlocked:
			result = UnlockManager.CoresUnlocked();
			break;
		case GameStats.MetaStat.CosmeticsUnlocked:
			result = UnlockManager.CosmeticsUnlocked();
			break;
		case GameStats.MetaStat.GildingsSpent:
			result = Currency.GildingsSpent;
			break;
		case GameStats.MetaStat.QuillmarksSpent:
			result = Currency.LCoinSpent;
			break;
		default:
			result = 0;
			break;
		}
		return result;
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x00047074 File Offset: 0x00045274
	public static void IncrementStat(PlayerStat stat, uint value = 1U)
	{
		if (!GameStats.PlayerStats.ContainsKey(stat))
		{
			GameStats.PlayerStats.Add(stat, (ulong)value);
		}
		else if (GameStats.PlayerStats[stat] + (ulong)value > (ulong)value)
		{
			Dictionary<PlayerStat, ulong> playerStats = GameStats.PlayerStats;
			playerStats[stat] += (ulong)value;
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x000470D0 File Offset: 0x000452D0
	public static void TryUpdateMax(PlayerStat stat, uint value)
	{
		bool flag = false;
		if (!GameStats.PlayerStats.ContainsKey(stat))
		{
			flag = true;
			GameStats.PlayerStats.Add(stat, (ulong)value);
		}
		else if (GameStats.PlayerStats[stat] < (ulong)value)
		{
			GameStats.PlayerStats[stat] = (ulong)value;
			flag = true;
		}
		if (flag)
		{
			GameStats.Save();
		}
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x00047124 File Offset: 0x00045324
	public static void IncrementStat(GameStats.SpecialStat stat, uint value = 1U, bool saveImmediate = false)
	{
		Debug.Log("Initial Check - Key Exists for " + stat.ToString() + ": " + GameStats.SpecialStats.ContainsKey(stat).ToString());
		if (!GameStats.SpecialStats.ContainsKey(stat))
		{
			GameStats.SpecialStats.Add(stat, (ulong)value);
		}
		else if (GameStats.SpecialStats[stat] + (ulong)value > GameStats.SpecialStats[stat])
		{
			Dictionary<GameStats.SpecialStat, ulong> specialStats = GameStats.SpecialStats;
			GameStats.SpecialStat key = stat;
			specialStats[key] += (ulong)value;
		}
		Debug.Log("Unique stat " + stat.ToString() + " incremeneted to " + GameStats.SpecialStats[stat].ToString());
		if (saveImmediate)
		{
			GameStats.Save();
			return;
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x000471F8 File Offset: 0x000453F8
	public static void TryUpdateMax(GameStats.SpecialStat stat, uint value, bool saveImmediate = false)
	{
		bool flag = false;
		if (!GameStats.SpecialStats.ContainsKey(stat))
		{
			flag = true;
			GameStats.SpecialStats.Add(stat, (ulong)value);
		}
		else if (GameStats.SpecialStats[stat] < (ulong)value)
		{
			GameStats.SpecialStats[stat] = (ulong)value;
			flag = true;
		}
		if (flag)
		{
			if (saveImmediate)
			{
				GameStats.Save();
				return;
			}
			GameStats.NeedsSave = true;
		}
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x00047258 File Offset: 0x00045458
	public static void IncrementEnemyStat(string enemyID, bool killedBy)
	{
		if (GameStats.EnemyStats.ContainsKey(enemyID))
		{
			ValueTuple<int, int> valueTuple = GameStats.EnemyStats[enemyID];
			if (killedBy)
			{
				GameStats.EnemyStats[enemyID] = new ValueTuple<int, int>(valueTuple.Item1, valueTuple.Item2 + 1);
			}
			else
			{
				GameStats.EnemyStats[enemyID] = new ValueTuple<int, int>(valueTuple.Item1 + 1, valueTuple.Item2);
			}
		}
		else
		{
			GameStats.EnemyStats[enemyID] = (killedBy ? new ValueTuple<int, int>(0, 1) : new ValueTuple<int, int>(1, 0));
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x000472E5 File Offset: 0x000454E5
	public static void SaveBestRace(string ID, float time)
	{
		if (!GameStats.LibraryRaces.ContainsKey(ID))
		{
			GameStats.LibraryRaces.Add(ID, time);
		}
		else if (GameStats.LibraryRaces[ID] > time)
		{
			GameStats.LibraryRaces[ID] = time;
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x00047322 File Offset: 0x00045522
	public static float GetBestRace(string ID)
	{
		if (!GameStats.LibraryRaces.ContainsKey(ID))
		{
			return 0f;
		}
		return GameStats.LibraryRaces[ID];
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00047344 File Offset: 0x00045544
	public static int GetTomeStat(GenreTree tome, GameStats.Stat stat, int defaultVal = 0)
	{
		if (!GameStats.Initialized || tome == null)
		{
			return defaultVal;
		}
		if (GameStats.TomeStats == null || !GameStats.TomeStats.ContainsKey(tome.ID))
		{
			return defaultVal;
		}
		int result;
		if (!GameStats.TomeStats[tome.ID].TryGetValue(stat, out result))
		{
			return defaultVal;
		}
		return result;
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0004739C File Offset: 0x0004559C
	public static void IncrementStat(GenreTree tome, MagicColor color, GameStats.Stat stat, int amount = 1, bool saveImmediate = true)
	{
		if (!GameStats.GlobalStats.ContainsKey(stat))
		{
			GameStats.GlobalStats.Add(stat, amount);
		}
		else
		{
			Dictionary<GameStats.Stat, int> dictionary = GameStats.GlobalStats;
			dictionary[stat] += amount;
		}
		if (stat == GameStats.Stat.TomesWon)
		{
			GameStats.IncrementStat(color, GameStats.SignatureStat.TomesWon, (uint)amount, false);
		}
		else if (stat == GameStats.Stat.ChaptersComplete)
		{
			GameStats.IncrementStat(color, GameStats.SignatureStat.ChaptersComplete, (uint)amount, false);
		}
		else if (stat == GameStats.Stat.TomesPlayed)
		{
			GameStats.IncrementStat(color, GameStats.SignatureStat.TomesPlayed, (uint)amount, false);
		}
		if (tome != null)
		{
			if (!GameStats.TomeStats.ContainsKey(tome.ID))
			{
				GameStats.TomeStats.Add(tome.ID, new Dictionary<GameStats.Stat, int>());
			}
			if (!GameStats.TomeStats[tome.ID].ContainsKey(stat))
			{
				GameStats.TomeStats[tome.ID].Add(stat, amount);
			}
			else
			{
				Dictionary<GameStats.Stat, int> dictionary = GameStats.TomeStats[tome.ID];
				dictionary[stat] += amount;
			}
		}
		if (saveImmediate)
		{
			GameStats.Save();
			return;
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0004749C File Offset: 0x0004569C
	public static void TryUpdateMax(GenreTree tome, MagicColor color, GameStats.Stat stat, int value)
	{
		bool flag = false;
		if (!GameStats.GlobalStats.ContainsKey(stat))
		{
			flag = true;
			GameStats.GlobalStats.Add(stat, value);
		}
		else if (GameStats.GlobalStats[stat] < value)
		{
			GameStats.GlobalStats[stat] = value;
			flag = true;
		}
		if (stat == GameStats.Stat.MaxBinding)
		{
			GameStats.TryUpdateMax(color, GameStats.SignatureStat.MaxBinding, (uint)value, false);
		}
		else if (stat == GameStats.Stat.MaxAppendix)
		{
			GameStats.TryUpdateMax(color, GameStats.SignatureStat.MaxAppendix, (uint)value, false);
		}
		if (tome != null)
		{
			if (!GameStats.TomeStats.ContainsKey(tome.ID))
			{
				GameStats.TomeStats.Add(tome.ID, new Dictionary<GameStats.Stat, int>());
			}
			if (!GameStats.TomeStats[tome.ID].ContainsKey(stat))
			{
				flag = true;
				GameStats.TomeStats[tome.ID].Add(stat, value);
			}
			else if (GameStats.TomeStats[tome.ID][stat] < value)
			{
				GameStats.TomeStats[tome.ID][stat] = value;
				flag = true;
			}
		}
		if (flag)
		{
			GameStats.Save();
		}
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x000475A0 File Offset: 0x000457A0
	public static int GetRaidStat(string encounterID, GameStats.RaidStat stat, int defaultVal = 0)
	{
		if (!GameStats.Initialized)
		{
			return defaultVal;
		}
		if (GameStats.RaidStats == null || !GameStats.RaidStats.ContainsKey(encounterID))
		{
			return defaultVal;
		}
		int result;
		if (!GameStats.RaidStats[encounterID].TryGetValue(stat, out result))
		{
			return defaultVal;
		}
		return result;
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x000475E4 File Offset: 0x000457E4
	public static void IncrementRaidStat(string encounterID, GameStats.RaidStat stat, int value = 1, bool saveImmediate = false)
	{
		if (!GameStats.RaidStats.ContainsKey(encounterID))
		{
			GameStats.RaidStats.Add(encounterID, new Dictionary<GameStats.RaidStat, int>());
		}
		if (!GameStats.RaidStats[encounterID].ContainsKey(stat))
		{
			GameStats.RaidStats[encounterID].Add(stat, value);
		}
		else if (GameStats.RaidStats[encounterID][stat] + value > GameStats.RaidStats[encounterID][stat])
		{
			Dictionary<GameStats.RaidStat, int> dictionary = GameStats.RaidStats[encounterID];
			dictionary[stat] += value;
		}
		if (saveImmediate)
		{
			GameStats.Save();
			return;
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x00047688 File Offset: 0x00045888
	public static void AddStickerProgress(GameStats.RaidStickerType sticker, List<string> ids)
	{
		foreach (string id in ids)
		{
			GameStats.AddStickerProgress(sticker, id);
		}
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x000476D8 File Offset: 0x000458D8
	public static void AddStickerProgress(GameStats.RaidStickerType sticker, string ID)
	{
		if (!GameStats.RaidStickers.ContainsKey(sticker))
		{
			GameStats.RaidStickers.Add(sticker, new HashSet<string>());
		}
		if (!GameStats.RaidStickers[sticker].Contains(ID))
		{
			GameStats.RaidStickers[sticker].Add(ID);
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00047730 File Offset: 0x00045930
	public static bool HasRaidSticker(GameStats.RaidStickerType sticker, string ID)
	{
		HashSet<string> hashSet;
		return GameStats.RaidStickers.TryGetValue(sticker, out hashSet) && hashSet.Contains(ID);
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00047758 File Offset: 0x00045958
	public static bool HasCoreRaidSticker(GameStats.RaidStickerType sticker, MagicColor color)
	{
		GameDB.ElementInfo element = GameDB.GetElement(color);
		AugmentTree augmentTree = (element != null) ? element.Core : null;
		if (augmentTree == null)
		{
			return false;
		}
		bool flag = GameStats.HasRaidSticker(sticker, augmentTree.ID);
		if (!flag)
		{
			switch (sticker)
			{
			case GameStats.RaidStickerType.Raving:
				flag |= GameStats.HasRaidSticker(GameStats.RaidStickerType.Raving_Hard, augmentTree.ID);
				break;
			case GameStats.RaidStickerType.Splice:
				flag |= GameStats.HasRaidSticker(GameStats.RaidStickerType.Splice_Hard, augmentTree.ID);
				break;
			case GameStats.RaidStickerType.Tangent:
				flag |= GameStats.HasRaidSticker(GameStats.RaidStickerType.Tangent_Hard, augmentTree.ID);
				break;
			}
		}
		return flag;
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x000477E4 File Offset: 0x000459E4
	public static ulong GetColorStat(MagicColor color, GameStats.SignatureStat stat, uint defaultVal = 0U)
	{
		if (!GameStats.Initialized)
		{
			return (ulong)defaultVal;
		}
		if (GameStats.SignatureStats == null || !GameStats.SignatureStats.ContainsKey(color))
		{
			return (ulong)defaultVal;
		}
		ulong result;
		if (!GameStats.SignatureStats[color].TryGetValue(stat, out result))
		{
			return (ulong)defaultVal;
		}
		return result;
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x0004782C File Offset: 0x00045A2C
	public static void TryUpdateMax(MagicColor color, GameStats.SignatureStat stat, uint value, bool saveImmediate = false)
	{
		bool flag = false;
		if (!GameStats.SignatureStats.ContainsKey(color))
		{
			GameStats.SignatureStats.Add(color, new Dictionary<GameStats.SignatureStat, ulong>());
		}
		if (!GameStats.SignatureStats[color].ContainsKey(stat))
		{
			flag = true;
			GameStats.SignatureStats[color].Add(stat, (ulong)value);
		}
		else if (GameStats.SignatureStats[color][stat] < (ulong)value)
		{
			GameStats.SignatureStats[color][stat] = (ulong)value;
			flag = true;
		}
		if (flag)
		{
			if (saveImmediate)
			{
				GameStats.Save();
				return;
			}
			GameStats.NeedsSave = true;
		}
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x000478C0 File Offset: 0x00045AC0
	public static void IncrementStat(MagicColor color, GameStats.SignatureStat stat, uint value = 1U, bool saveImmediate = false)
	{
		if (!GameStats.SignatureStats.ContainsKey(color))
		{
			GameStats.SignatureStats.Add(color, new Dictionary<GameStats.SignatureStat, ulong>());
		}
		if (!GameStats.SignatureStats[color].ContainsKey(stat))
		{
			GameStats.SignatureStats[color].Add(stat, (ulong)value);
		}
		else if (GameStats.SignatureStats[color][stat] + (ulong)value > GameStats.SignatureStats[color][stat])
		{
			Dictionary<GameStats.SignatureStat, ulong> dictionary = GameStats.SignatureStats[color];
			dictionary[stat] += (ulong)value;
		}
		if (saveImmediate)
		{
			GameStats.Save();
			return;
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00047968 File Offset: 0x00045B68
	public static int GetBookClubStat(string challenge, GameStats.BookClubStat stat, int defaultVal = 0)
	{
		if (!GameStats.Initialized || challenge == null)
		{
			return defaultVal;
		}
		if (GameStats.BookClubStats == null || !GameStats.BookClubStats.ContainsKey(challenge))
		{
			return defaultVal;
		}
		int result;
		if (!GameStats.BookClubStats[challenge].TryGetValue(stat, out result))
		{
			return defaultVal;
		}
		return result;
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x000479B0 File Offset: 0x00045BB0
	public static void TryUpdateBookClubMax(string challenge, GameStats.BookClubStat stat, int value, bool saveImmediate = false)
	{
		bool flag = false;
		if (!GameStats.BookClubStats.ContainsKey(challenge))
		{
			GameStats.BookClubStats.Add(challenge, new Dictionary<GameStats.BookClubStat, int>());
		}
		if (!GameStats.BookClubStats[challenge].ContainsKey(stat))
		{
			flag = true;
			GameStats.BookClubStats[challenge].Add(stat, value);
		}
		else if (GameStats.BookClubStats[challenge][stat] < value)
		{
			GameStats.BookClubStats[challenge][stat] = value;
			flag = true;
		}
		if (flag)
		{
			if (saveImmediate)
			{
				GameStats.Save();
				return;
			}
			GameStats.NeedsSave = true;
		}
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00047A40 File Offset: 0x00045C40
	public static void TryUpdateBookClubMin(string challenge, GameStats.BookClubStat stat, int value, bool saveImmediate = false)
	{
		bool flag = false;
		if (!GameStats.BookClubStats.ContainsKey(challenge))
		{
			GameStats.BookClubStats.Add(challenge, new Dictionary<GameStats.BookClubStat, int>());
		}
		if (!GameStats.BookClubStats[challenge].ContainsKey(stat))
		{
			flag = true;
			GameStats.BookClubStats[challenge].Add(stat, value);
		}
		else
		{
			int num = GameStats.BookClubStats[challenge][stat];
			if (num > value || num <= 0)
			{
				GameStats.BookClubStats[challenge][stat] = value;
				flag = true;
			}
		}
		if (flag)
		{
			if (saveImmediate)
			{
				GameStats.Save();
				return;
			}
			GameStats.NeedsSave = true;
		}
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00047AD8 File Offset: 0x00045CD8
	public static void IncrementBookClub(string challenge, GameStats.BookClubStat stat, int value = 1, bool saveImmediate = false)
	{
		if (!GameStats.BookClubStats.ContainsKey(challenge))
		{
			GameStats.BookClubStats.Add(challenge, new Dictionary<GameStats.BookClubStat, int>());
		}
		if (!GameStats.BookClubStats[challenge].ContainsKey(stat))
		{
			GameStats.BookClubStats[challenge].Add(stat, value);
		}
		else if (GameStats.BookClubStats[challenge][stat] + value > GameStats.BookClubStats[challenge][stat])
		{
			Dictionary<GameStats.BookClubStat, int> dictionary = GameStats.BookClubStats[challenge];
			dictionary[stat] += value;
		}
		if (saveImmediate)
		{
			GameStats.Save();
			return;
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00047B7C File Offset: 0x00045D7C
	public static void IncrementEphemeral(string ID, uint value = 1U)
	{
		if (!GameStats.EphemeralStats.ContainsKey(ID))
		{
			GameStats.EphemeralStats.Add(ID, (ulong)value);
		}
		else
		{
			Dictionary<string, ulong> ephemeralStats = GameStats.EphemeralStats;
			ephemeralStats[ID] += (ulong)value;
		}
		GameStats.NeedsSave = true;
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00047BC4 File Offset: 0x00045DC4
	public static ulong GetEphemeralStat(string ID)
	{
		if (!GameStats.EphemeralStats.ContainsKey(ID))
		{
			return 0UL;
		}
		return GameStats.EphemeralStats[ID];
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00047BE1 File Offset: 0x00045DE1
	public static void ResetEphemeral(string ID)
	{
		if (!GameStats.EphemeralStats.ContainsKey(ID))
		{
			return;
		}
		GameStats.EphemeralStats.Remove(ID);
		GameStats.Save();
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00047C04 File Offset: 0x00045E04
	public static int GetTotalTimePlayed()
	{
		int num = 0;
		foreach (MagicColor key in GameStats.SignatureStats.Keys)
		{
			if (GameStats.SignatureStats[key].ContainsKey(GameStats.SignatureStat.TimePlayed))
			{
				num += (int)GameStats.SignatureStats[key][GameStats.SignatureStat.TimePlayed];
			}
		}
		return num;
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x00047C80 File Offset: 0x00045E80
	public static ulong GetTotalDamageDone()
	{
		return 0UL + GameStats.GetPlayerStat(PlayerStat.Primary, 0U) + GameStats.GetPlayerStat(PlayerStat.Secondary, 0U) + GameStats.GetPlayerStat(PlayerStat.Movement, 0U) + GameStats.GetPlayerStat(PlayerStat.Utility, 0U) + GameStats.GetPlayerStat(PlayerStat.Ghost, 0U) + GameStats.GetPlayerStat(PlayerStat.Status, 0U) + GameStats.GetPlayerStat(PlayerStat.Inkling, 0U) + GameStats.GetPlayerStat(PlayerStat.AtlasDmg, 0U) + GameStats.GetPlayerStat(PlayerStat.MiscDamage, 0U);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00047CDA File Offset: 0x00045EDA
	public static ulong GetTotalHealingDone()
	{
		return 0UL + GameStats.GetPlayerStat(PlayerStat.SelfHeal, 0U) + GameStats.GetPlayerStat(PlayerStat.OtherHeal, 0U) + GameStats.GetPlayerStat(PlayerStat.Pickup, 0U);
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x00047CF8 File Offset: 0x00045EF8
	public static void ResetSaved()
	{
		GameStats.GlobalStats.Clear();
		GameStats.PlayerStats.Clear();
		GameStats.TomeStats.Clear();
		GameStats.RaidStats.Clear();
		GameStats.RaidStickers.Clear();
		GameStats.BookClubStats.Clear();
		GameStats.EphemeralStats.Clear();
		GameStats.Save();
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00047D50 File Offset: 0x00045F50
	private static void Save()
	{
		GameStats.NeedsSave = false;
		try
		{
			ES3Settings settings = new ES3Settings("stats.vel", null);
			try
			{
				ES3.CacheFile("stats.vel");
				settings = new ES3Settings("stats.vel", new Enum[]
				{
					ES3.Location.Cache
				});
			}
			catch (Exception)
			{
				Debug.LogError("Couldn't cache Save file for Stats Saving");
			}
			bool flag = true;
			try
			{
				ES3.Save<Dictionary<GameStats.Stat, int>>("GlobalStats", GameStats.GlobalStats, settings);
				ES3.Save<Dictionary<MagicColor, Dictionary<GameStats.SignatureStat, ulong>>>("ColorStats", GameStats.SignatureStats, settings);
				ES3.Save<Dictionary<string, Dictionary<GameStats.Stat, int>>>("TomeStats", GameStats.TomeStats, settings);
				ES3.Save<Dictionary<string, Dictionary<GameStats.RaidStat, int>>>("RaidStats", GameStats.RaidStats, settings);
				ES3.Save<Dictionary<GameStats.RaidStickerType, HashSet<string>>>("RaidStickers", GameStats.RaidStickers, settings);
				ES3.Save<Dictionary<string, Dictionary<GameStats.BookClubStat, int>>>("BookClubStats", GameStats.BookClubStats, settings);
				ES3.Save<Dictionary<GameStats.SpecialStat, ulong>>("SpecialStats", GameStats.SpecialStats, settings);
				ES3.Save<Dictionary<PlayerStat, ulong>>("PlrStats", GameStats.PlayerStats, settings);
				ES3.Save<Dictionary<string, ulong>>("EphStats", GameStats.EphemeralStats, settings);
				ES3.Save<Dictionary<string, ValueTuple<int, int>>>("EnemyStats", GameStats.EnemyStats, settings);
				ES3.Save<Dictionary<string, float>>("LibraryRaces", GameStats.LibraryRaces, settings);
				ES3.StoreCachedFile("stats.vel");
			}
			catch (Exception)
			{
				flag = false;
			}
			if (flag)
			{
				ES3.CreateBackup("stats.vel");
				ParseManager.SaveStats();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to save global stats: " + ex.ToString());
		}
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00047EE0 File Offset: 0x000460E0
	public static void TryTakeSnapshot()
	{
		if (!GameStats.snapshotAllowed)
		{
			return;
		}
		Settings.SaveInBackup("stats.vel");
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00047EF4 File Offset: 0x000460F4
	// Note: this type is marked as 'beforefieldinit'.
	static GameStats()
	{
	}

	// Token: 0x0400091E RID: 2334
	private static Dictionary<GameStats.Stat, int> GlobalStats = new Dictionary<GameStats.Stat, int>();

	// Token: 0x0400091F RID: 2335
	private static Dictionary<string, Dictionary<GameStats.Stat, int>> TomeStats = new Dictionary<string, Dictionary<GameStats.Stat, int>>();

	// Token: 0x04000920 RID: 2336
	private static Dictionary<string, Dictionary<GameStats.RaidStat, int>> RaidStats = new Dictionary<string, Dictionary<GameStats.RaidStat, int>>();

	// Token: 0x04000921 RID: 2337
	private static Dictionary<GameStats.RaidStickerType, HashSet<string>> RaidStickers = new Dictionary<GameStats.RaidStickerType, HashSet<string>>();

	// Token: 0x04000922 RID: 2338
	private static Dictionary<MagicColor, Dictionary<GameStats.SignatureStat, ulong>> SignatureStats = new Dictionary<MagicColor, Dictionary<GameStats.SignatureStat, ulong>>();

	// Token: 0x04000923 RID: 2339
	private static Dictionary<PlayerStat, ulong> PlayerStats = new Dictionary<PlayerStat, ulong>();

	// Token: 0x04000924 RID: 2340
	private static Dictionary<GameStats.SpecialStat, ulong> SpecialStats = new Dictionary<GameStats.SpecialStat, ulong>();

	// Token: 0x04000925 RID: 2341
	[TupleElementNames(new string[]
	{
		"killed",
		"killedBy"
	})]
	private static Dictionary<string, ValueTuple<int, int>> EnemyStats = new Dictionary<string, ValueTuple<int, int>>();

	// Token: 0x04000926 RID: 2342
	private static Dictionary<string, ulong> EphemeralStats = new Dictionary<string, ulong>();

	// Token: 0x04000927 RID: 2343
	private static Dictionary<string, Dictionary<GameStats.BookClubStat, int>> BookClubStats = new Dictionary<string, Dictionary<GameStats.BookClubStat, int>>();

	// Token: 0x04000928 RID: 2344
	private static Dictionary<string, float> LibraryRaces = new Dictionary<string, float>();

	// Token: 0x04000929 RID: 2345
	private static bool NeedsSave = false;

	// Token: 0x0400092A RID: 2346
	private static float timeSinceLastSnapshot;

	// Token: 0x0400092B RID: 2347
	private static bool snapshotAllowed;

	// Token: 0x0400092C RID: 2348
	private const string STAT_FILE = "stats.vel";

	// Token: 0x0400092D RID: 2349
	private static bool HadNewStatFile;

	// Token: 0x0400092E RID: 2350
	private static bool _initialized;

	// Token: 0x020004E1 RID: 1249
	public enum Stat
	{
		// Token: 0x040024A9 RID: 9385
		TomesPlayed,
		// Token: 0x040024AA RID: 9386
		TomesWon,
		// Token: 0x040024AB RID: 9387
		MaxBinding,
		// Token: 0x040024AC RID: 9388
		BindingAttunement,
		// Token: 0x040024AD RID: 9389
		InkLevel,
		// Token: 0x040024AE RID: 9390
		PrestigeLevel,
		// Token: 0x040024AF RID: 9391
		MaxAppendix,
		// Token: 0x040024B0 RID: 9392
		ChaptersComplete
	}

	// Token: 0x020004E2 RID: 1250
	public enum RaidStat
	{
		// Token: 0x040024B2 RID: 9394
		Attempts,
		// Token: 0x040024B3 RID: 9395
		Completed,
		// Token: 0x040024B4 RID: 9396
		HardMode_Attempts,
		// Token: 0x040024B5 RID: 9397
		HardMode_Completed
	}

	// Token: 0x020004E3 RID: 1251
	public enum RaidStickerType
	{
		// Token: 0x040024B7 RID: 9399
		Raving,
		// Token: 0x040024B8 RID: 9400
		Raving_Hard,
		// Token: 0x040024B9 RID: 9401
		Splice,
		// Token: 0x040024BA RID: 9402
		Splice_Hard,
		// Token: 0x040024BB RID: 9403
		Tangent,
		// Token: 0x040024BC RID: 9404
		Tangent_Hard
	}

	// Token: 0x020004E4 RID: 1252
	public enum SignatureStat
	{
		// Token: 0x040024BE RID: 9406
		TomesPlayed,
		// Token: 0x040024BF RID: 9407
		TomesWon,
		// Token: 0x040024C0 RID: 9408
		ChaptersComplete,
		// Token: 0x040024C1 RID: 9409
		MaxBinding,
		// Token: 0x040024C2 RID: 9410
		MaxAppendix,
		// Token: 0x040024C3 RID: 9411
		ManaSpent,
		// Token: 0x040024C4 RID: 9412
		UltsCast,
		// Token: 0x040024C5 RID: 9413
		TimePlayed,
		// Token: 0x040024C6 RID: 9414
		Blue_ = 64,
		// Token: 0x040024C7 RID: 9415
		BlutUltBlots,
		// Token: 0x040024C8 RID: 9416
		BlotDamage,
		// Token: 0x040024C9 RID: 9417
		BlotSpread,
		// Token: 0x040024CA RID: 9418
		Yellow_ = 128,
		// Token: 0x040024CB RID: 9419
		YellowUltCrits,
		// Token: 0x040024CC RID: 9420
		FlourishDamage,
		// Token: 0x040024CD RID: 9421
		BigFlourishHits,
		// Token: 0x040024CE RID: 9422
		FlourishBiggestHit,
		// Token: 0x040024CF RID: 9423
		Green_ = 192,
		// Token: 0x040024D0 RID: 9424
		InklingEvolveSeconds,
		// Token: 0x040024D1 RID: 9425
		InklingDamage,
		// Token: 0x040024D2 RID: 9426
		InklingCasts,
		// Token: 0x040024D3 RID: 9427
		Red_ = 256,
		// Token: 0x040024D4 RID: 9428
		RedUltTethers,
		// Token: 0x040024D5 RID: 9429
		TetherDamage,
		// Token: 0x040024D6 RID: 9430
		LeechHeal,
		// Token: 0x040024D7 RID: 9431
		Pink_ = 320,
		// Token: 0x040024D8 RID: 9432
		OutlinedEnemies,
		// Token: 0x040024D9 RID: 9433
		FinalizeDamage,
		// Token: 0x040024DA RID: 9434
		DraftsCreated,
		// Token: 0x040024DB RID: 9435
		DraftsExploded,
		// Token: 0x040024DC RID: 9436
		Orange_ = 400,
		// Token: 0x040024DD RID: 9437
		Survey_Deposited,
		// Token: 0x040024DE RID: 9438
		Atlas_Damage,
		// Token: 0x040024DF RID: 9439
		Teal = 450,
		// Token: 0x040024E0 RID: 9440
		ShardDamage,
		// Token: 0x040024E1 RID: 9441
		ShardsFired,
		// Token: 0x040024E2 RID: 9442
		ArmorCreated
	}

	// Token: 0x020004E5 RID: 1253
	public enum SpecialStat
	{
		// Token: 0x040024E4 RID: 9444
		ToxicDeaths,
		// Token: 0x040024E5 RID: 9445
		MimicChestsOpened
	}

	// Token: 0x020004E6 RID: 1254
	public enum CompositeStat
	{
		// Token: 0x040024E7 RID: 9447
		UniqueTomesPlayed,
		// Token: 0x040024E8 RID: 9448
		UniqueTomesBeaten,
		// Token: 0x040024E9 RID: 9449
		MaxBindings,
		// Token: 0x040024EA RID: 9450
		BookClubsCompleted,
		// Token: 0x040024EB RID: 9451
		RaidEncountersCompleted,
		// Token: 0x040024EC RID: 9452
		RaidHardModeEncountersCompleted
	}

	// Token: 0x020004E7 RID: 1255
	public enum MetaStat
	{
		// Token: 0x040024EE RID: 9454
		InkLevel,
		// Token: 0x040024EF RID: 9455
		SpellsUnlocked,
		// Token: 0x040024F0 RID: 9456
		CoresUnlocked,
		// Token: 0x040024F1 RID: 9457
		CosmeticsUnlocked,
		// Token: 0x040024F2 RID: 9458
		GildingsSpent,
		// Token: 0x040024F3 RID: 9459
		QuillmarksSpent
	}

	// Token: 0x020004E8 RID: 1256
	public enum BookClubStat
	{
		// Token: 0x040024F5 RID: 9461
		FastestTime,
		// Token: 0x040024F6 RID: 9462
		MaxAppendix,
		// Token: 0x040024F7 RID: 9463
		TimesCompleted,
		// Token: 0x040024F8 RID: 9464
		UniqueStat
	}
}
