using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class MetaDB : ScriptableObject
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000284 RID: 644 RVA: 0x00016231 File Offset: 0x00014431
	public static DateTime BookClubStartDate
	{
		get
		{
			return new DateTime(2024, 12, 18, 20, 0, 0, DateTimeKind.Utc);
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000285 RID: 645 RVA: 0x00016246 File Offset: 0x00014446
	public static int SignatureUnlockCost
	{
		get
		{
			return MetaDB.instance.InkCoreCost;
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000286 RID: 646 RVA: 0x00016252 File Offset: 0x00014452
	public static int MaxPrestige
	{
		get
		{
			return MetaDB.instance.PrestigeIcons.Count;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000287 RID: 647 RVA: 0x00016263 File Offset: 0x00014463
	public static List<string> LoadoutNounList
	{
		get
		{
			return MetaDB.instance.LoadoutNouns;
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000288 RID: 648 RVA: 0x0001626F File Offset: 0x0001446F
	public static int AbilityIncentiveReward
	{
		get
		{
			return MetaDB.instance.AbilityIncentiveValue;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000289 RID: 649 RVA: 0x0001627B File Offset: 0x0001447B
	public static int TomeIncentiveReward
	{
		get
		{
			return MetaDB.instance.TomeIncentiveValue;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x0600028A RID: 650 RVA: 0x00016287 File Offset: 0x00014487
	public static MetaDB.BookClubChallenge CurrentChallenge
	{
		get
		{
			return MetaDB.instance.GetCurrentBookClubChallenge();
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x0600028B RID: 651 RVA: 0x00016293 File Offset: 0x00014493
	public static List<MetaDB.BookClubChallenge> AllChallenges
	{
		get
		{
			return MetaDB.instance.BookClubChallenges;
		}
	}

	// Token: 0x0600028C RID: 652 RVA: 0x000162A0 File Offset: 0x000144A0
	public static void SetInstance(MetaDB db)
	{
		MetaDB.instance = db;
		MetaDB.instance.AllQuests = new Dictionary<string, MetaDB.DailyQuest>();
		foreach (MetaDB.DailyQuest dailyQuest in MetaDB.instance.Quests)
		{
			MetaDB.instance.AllQuests.Add(dailyQuest.ID, dailyQuest);
		}
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0001631C File Offset: 0x0001451C
	public static Sprite GetPrestigeIcon(int level)
	{
		if (MetaDB.instance.PrestigeIcons.Count == 0)
		{
			return null;
		}
		if (level <= 0)
		{
			return null;
		}
		return MetaDB.instance.PrestigeIcons[Mathf.Min(level - 1, MetaDB.instance.PrestigeIcons.Count - 1)];
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0001636C File Offset: 0x0001456C
	public static MetaDB.DailyQuest GetQuest(string ID)
	{
		if (MetaDB.instance == null)
		{
			return null;
		}
		MetaDB.DailyQuest result;
		if (MetaDB.instance.AllQuests.TryGetValue(ID, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x000163A0 File Offset: 0x000145A0
	public static int GetRewardValue(MetaDB.DailyQuest.RewardLevel level)
	{
		if (MetaDB.instance == null)
		{
			return 0;
		}
		foreach (MetaDB.QuestReward questReward in MetaDB.instance.QuestRewards)
		{
			if (questReward.Level == level)
			{
				return questReward.Value;
			}
		}
		return 0;
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00016414 File Offset: 0x00014614
	public static MetaDB.DailyQuest GetNewQuest(MetaDB.DailyQuest.Timescale timing)
	{
		if (MetaDB.instance == null)
		{
			return null;
		}
		List<string> list = new List<string>();
		foreach (MetaDB.QuestProgress questProgress in Progression.Quests)
		{
			list.Add(questProgress.ID);
		}
		List<MetaDB.DailyQuest> list2 = new List<MetaDB.DailyQuest>();
		foreach (MetaDB.DailyQuest dailyQuest in MetaDB.instance.Quests)
		{
			if (dailyQuest.Timing == timing && !list.Contains(dailyQuest.ID) && dailyQuest.CanAccess())
			{
				list2.Add(dailyQuest);
			}
		}
		if (list2.Count == 0)
		{
			return null;
		}
		list2.Shuffle(null);
		List<string> list3 = (timing == MetaDB.DailyQuest.Timescale.Daily) ? Progression.RecentDaily : Progression.RecentWeekly;
		for (int i = list2.Count - 1; i >= 1; i--)
		{
			if (list3.Contains(list2[i].ID))
			{
				list2.RemoveAt(i);
			}
		}
		return list2[UnityEngine.Random.Range(0, list2.Count)];
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0001655C File Offset: 0x0001475C
	public static MetaDB.BookClubChallenge GetBookClubChallenge(string ID)
	{
		if (MetaDB.instance == null)
		{
			return null;
		}
		if (MetaDB.instance.BookClubChallenges.Count == 0)
		{
			return null;
		}
		foreach (MetaDB.BookClubChallenge bookClubChallenge in MetaDB.instance.BookClubChallenges)
		{
			if (bookClubChallenge.ID == ID)
			{
				return bookClubChallenge;
			}
		}
		return null;
	}

	// Token: 0x06000292 RID: 658 RVA: 0x000165E4 File Offset: 0x000147E4
	private MetaDB.BookClubChallenge GetCurrentBookClubChallenge()
	{
		int currentChallengeNumber = MetaDB.GetCurrentChallengeNumber();
		int index = this.CalculateChallengeIndex(currentChallengeNumber);
		return this.BookClubChallenges[index];
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0001660C File Offset: 0x0001480C
	private int CalculateChallengeIndex(int periodNumber)
	{
		int num = 0;
		for (int i = 0; i < this.ChallengeUpdates.Count; i++)
		{
			MetaDB.ChallengeUpdate challengeUpdate = this.ChallengeUpdates[i];
			MetaDB.ChallengeUpdate challengeUpdate2 = (i + 1 < this.ChallengeUpdates.Count) ? this.ChallengeUpdates[i + 1] : null;
			int num2;
			if (challengeUpdate2 != null)
			{
				num2 = challengeUpdate2.Loop - challengeUpdate.Loop;
			}
			else
			{
				num2 = periodNumber - challengeUpdate.Loop + 1;
			}
			num += num2;
		}
		return (num - 1) % this.BookClubChallenges.Count;
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00016694 File Offset: 0x00014894
	public void CreateChallengeUpdate()
	{
		MetaDB.ChallengeUpdate item = new MetaDB.ChallengeUpdate
		{
			Loop = MetaDB.GetCurrentChallengeNumber(),
			Count = this.BookClubChallenges.Count
		};
		this.ChallengeUpdates.Add(item);
	}

	// Token: 0x06000295 RID: 661 RVA: 0x000166D0 File Offset: 0x000148D0
	public static int GetCurrentChallengeNumber()
	{
		return (int)((DateTime.UtcNow - MetaDB.BookClubStartDate).TotalDays / 3.5);
	}

	// Token: 0x06000296 RID: 662 RVA: 0x00016700 File Offset: 0x00014900
	public static TimeSpan GetTimeUntilNextChallenge()
	{
		int num = (int)((DateTime.UtcNow - MetaDB.BookClubStartDate).TotalDays / 3.5);
		return MetaDB.BookClubStartDate.AddDays((double)((float)(num + 1) * 3.5f)) - DateTime.UtcNow;
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00016754 File Offset: 0x00014954
	public static void ChallengeEnded(bool didWin, float baseTime, int specialStat, Vector3 point)
	{
		if (!GameplayManager.IsChallengeActive || GameplayManager.Challenge == null)
		{
			return;
		}
		if (!didWin && WaveManager.instance.AppendixLevel <= 0)
		{
			return;
		}
		string challenge = GameplayManager.Challenge.ID + "_" + MetaDB.GetCurrentChallengeNumber().ToString();
		if (GameStats.GetBookClubStat(challenge, GameStats.BookClubStat.TimesCompleted, 0) == 0)
		{
			Progression.CreateQuillmarkReward(point, 500).OverrideDetails = "Rewarded for first Book Club Reading!";
			Currency.Add(500, true);
			Vector3 pt = GoalManager.FixPointOnNav(point + UnityEngine.Random.insideUnitSphere * 10f);
			GoalManager.instance.CreateBossReward(pt, Progression.BossRewardType.CosmCurrency, new List<GraphTree>(), 500).OverrideDetails = "Rewarded for first Book Club Reading!";
		}
		GameStats.IncrementBookClub(challenge, GameStats.BookClubStat.TimesCompleted, 1, false);
		GameStats.TryUpdateBookClubMin(challenge, GameStats.BookClubStat.FastestTime, Mathf.FloorToInt(baseTime), false);
		GameStats.TryUpdateBookClubMax(challenge, GameStats.BookClubStat.MaxAppendix, WaveManager.instance.AppendixLevel, false);
		GameStats.TryUpdateBookClubMax(challenge, GameStats.BookClubStat.UniqueStat, specialStat, false);
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0001683B File Offset: 0x00014A3B
	public MetaDB()
	{
	}

	// Token: 0x04000279 RID: 633
	private static MetaDB instance;

	// Token: 0x0400027A RID: 634
	public int InkCoreCost = 100;

	// Token: 0x0400027B RID: 635
	public List<Sprite> PrestigeIcons;

	// Token: 0x0400027C RID: 636
	public List<string> LoadoutNouns;

	// Token: 0x0400027D RID: 637
	public List<MetaDB.DailyQuest> Quests;

	// Token: 0x0400027E RID: 638
	private Dictionary<string, MetaDB.DailyQuest> AllQuests;

	// Token: 0x0400027F RID: 639
	public List<MetaDB.QuestReward> QuestRewards;

	// Token: 0x04000280 RID: 640
	public int AbilityIncentiveValue = 120;

	// Token: 0x04000281 RID: 641
	public int TomeIncentiveValue = 120;

	// Token: 0x04000282 RID: 642
	public List<MetaDB.ChallengeUpdate> ChallengeUpdates;

	// Token: 0x04000283 RID: 643
	public List<MetaDB.BookClubChallenge> BookClubChallenges;

	// Token: 0x04000284 RID: 644
	private const float BOOK_CLUB_DAYS = 3.5f;

	// Token: 0x0200044E RID: 1102
	[Serializable]
	public class DailyQuest
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x000C27E6 File Offset: 0x000C09E6
		public string Title
		{
			get
			{
				if (!(this.Graph != null))
				{
					return "Undefined Quest";
				}
				return this.Graph.Root.Name;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x000C280C File Offset: 0x000C0A0C
		public string Description
		{
			get
			{
				if (!(this.Graph != null))
				{
					return "- Quest description text.";
				}
				return this.Graph.Root.Detail;
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000C2834 File Offset: 0x000C0A34
		public bool CanAccess()
		{
			return !this.Requirements || ((!(this.TomeReq != null) || UnlockManager.IsGenreUnlocked(this.TomeReq)) && (!(this.BindingReq != null) || UnlockManager.IsBindingUnlocked(this.BindingReq)) && (!(this.InkColor != null) || UnlockManager.IsCoreUnlocked(this.InkColor)) && (!(this.AbilityReq != null) || UnlockManager.IsAbilityUnlocked(this.AbilityReq)) && (this.Attunement <= 0 || Progression.BindingAttunement >= this.Attunement));
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000C28D8 File Offset: 0x000C0AD8
		public DailyQuest()
		{
		}

		// Token: 0x040021DF RID: 8671
		public string ID;

		// Token: 0x040021E0 RID: 8672
		public MetaDB.DailyQuest.Timescale Timing;

		// Token: 0x040021E1 RID: 8673
		public AugmentTree Graph;

		// Token: 0x040021E2 RID: 8674
		public MetaDB.DailyQuest.RewardType Reward;

		// Token: 0x040021E3 RID: 8675
		public MetaDB.DailyQuest.RewardLevel RewardValue;

		// Token: 0x040021E4 RID: 8676
		public bool UsesStat;

		// Token: 0x040021E5 RID: 8677
		public string StatID;

		// Token: 0x040021E6 RID: 8678
		public int StatTargetValue;

		// Token: 0x040021E7 RID: 8679
		public bool Requirements;

		// Token: 0x040021E8 RID: 8680
		public GenreTree TomeReq;

		// Token: 0x040021E9 RID: 8681
		public AugmentTree BindingReq;

		// Token: 0x040021EA RID: 8682
		public AugmentTree InkColor;

		// Token: 0x040021EB RID: 8683
		public AbilityTree AbilityReq;

		// Token: 0x040021EC RID: 8684
		public int Attunement;

		// Token: 0x020006B4 RID: 1716
		public enum Timescale
		{
			// Token: 0x04002CB1 RID: 11441
			Daily,
			// Token: 0x04002CB2 RID: 11442
			Weekly
		}

		// Token: 0x020006B5 RID: 1717
		public enum RewardType
		{
			// Token: 0x04002CB4 RID: 11444
			Quillmarks,
			// Token: 0x04002CB5 RID: 11445
			Gildings
		}

		// Token: 0x020006B6 RID: 1718
		public enum RewardLevel
		{
			// Token: 0x04002CB7 RID: 11447
			Daily_1,
			// Token: 0x04002CB8 RID: 11448
			Daily_2,
			// Token: 0x04002CB9 RID: 11449
			Daily_3,
			// Token: 0x04002CBA RID: 11450
			Weekly_1,
			// Token: 0x04002CBB RID: 11451
			Weekly_2,
			// Token: 0x04002CBC RID: 11452
			Weekly_3
		}
	}

	// Token: 0x0200044F RID: 1103
	[Serializable]
	public struct QuestReward
	{
		// Token: 0x040021ED RID: 8685
		public MetaDB.DailyQuest.RewardLevel Level;

		// Token: 0x040021EE RID: 8686
		public int Value;
	}

	// Token: 0x02000450 RID: 1104
	public class QuestProgress
	{
		// Token: 0x06002148 RID: 8520 RVA: 0x000C28E0 File Offset: 0x000C0AE0
		public QuestProgress(MetaDB.DailyQuest quest)
		{
			this.ID = quest.ID;
			this.Timescale = quest.Timing;
			if (this.Timescale == MetaDB.DailyQuest.Timescale.Daily)
			{
				this.ExpiresAt = MetaDB.QuestProgress.NextDailyQuestTime();
				return;
			}
			if (this.Timescale == MetaDB.DailyQuest.Timescale.Weekly)
			{
				this.ExpiresAt = MetaDB.QuestProgress.NextWeekyQuestTime();
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000C2934 File Offset: 0x000C0B34
		public QuestProgress(string Data)
		{
			string[] array = Data.Split('|', StringSplitOptions.None);
			if (array.Length != 5)
			{
				this.ID = "INVALID";
				Debug.LogError("Invalid Quest Progress Data");
				return;
			}
			this.ID = array[0];
			int timescale;
			int.TryParse(array[1], out timescale);
			int num;
			int.TryParse(array[2], out num);
			int num2;
			int.TryParse(array[3], out num2);
			this.Timescale = (MetaDB.DailyQuest.Timescale)timescale;
			this.IsComplete = (num == 1);
			this.IsCollected = (num2 == 1);
			this.ExpiresAt = DateTime.ParseExact(array[4], "yyyyMMddTHH", null);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000C29C8 File Offset: 0x000C0BC8
		public override string ToString()
		{
			string text = this.ExpiresAt.ToString("yyyyMMddTHH");
			return string.Format("{0}|{1}|{2}|{3}|{4}", new object[]
			{
				this.ID,
				(int)this.Timescale,
				this.IsComplete ? 1 : 0,
				this.IsCollected ? 1 : 0,
				text
			});
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x000C2A39 File Offset: 0x000C0C39
		public bool IsExpired
		{
			get
			{
				return DateTime.Now > this.ExpiresAt;
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000C2A4C File Offset: 0x000C0C4C
		public static DateTime NextDailyQuestTime()
		{
			DateTime now = DateTime.Now;
			DateTime result = new DateTime(now.Year, now.Month, now.Day, 5, 0, 0);
			if (now.TimeOfDay > new TimeSpan(5, 0, 0))
			{
				result = result.AddDays(1.0);
			}
			return result;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000C2AA8 File Offset: 0x000C0CA8
		public static DateTime NextWeekyQuestTime()
		{
			DateTime now = DateTime.Now;
			int num = (int)((DayOfWeek.Thursday - (int)now.DayOfWeek + 7) % (DayOfWeek)7);
			if (num == 0 && now.TimeOfDay > new TimeSpan(5, 0, 0))
			{
				num = 7;
			}
			return now.Date.AddDays((double)num).Add(new TimeSpan(5, 0, 0));
		}

		// Token: 0x040021EF RID: 8687
		public string ID;

		// Token: 0x040021F0 RID: 8688
		public MetaDB.DailyQuest.Timescale Timescale;

		// Token: 0x040021F1 RID: 8689
		public DateTime ExpiresAt;

		// Token: 0x040021F2 RID: 8690
		public bool IsComplete;

		// Token: 0x040021F3 RID: 8691
		public bool IsCollected;
	}

	// Token: 0x02000451 RID: 1105
	[Serializable]
	public class BookClubChallenge
	{
		// Token: 0x0600214E RID: 8526 RVA: 0x000C2B05 File Offset: 0x000C0D05
		public AugmentTree GetCore()
		{
			return GameDB.GetElement(this.Signature).Core;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000C2B18 File Offset: 0x000C0D18
		public void SetLoadout()
		{
			if (PlayerControl.myInstance == null)
			{
				return;
			}
			PlayerControl.myInstance.actions.SetCore(this.GetCore());
			PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Primary, this.Generator.ID, false);
			PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Secondary, this.Spender.ID, false);
			PlayerControl.myInstance.actions.LoadAbility(PlayerAbilityType.Movement, this.Movement.ID, false);
			foreach (AugmentTree mod in this.PlayerPages)
			{
				PlayerControl.myInstance.AddAugment(mod, 1);
			}
			PlayerControl.myInstance.actions.ResetCooldown(PlayerAbilityType.Utility, true);
			if (PanelManager.CurPanel == PanelType.Augments)
			{
				AugmentsPanel.instance.GameAugmentsChanged();
			}
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000C2C0C File Offset: 0x000C0E0C
		public int GetCurrentRunStat()
		{
			PlayerGameStats pstats = PlayerControl.myInstance.PStats;
			int result;
			switch (this.StatTracking)
			{
			case PlayerStat.Primary:
				result = pstats.GetDamage(this.StatTracking);
				break;
			case PlayerStat.Secondary:
				result = pstats.GetDamage(this.StatTracking);
				break;
			case PlayerStat.Utility:
				result = pstats.GetDamage(this.StatTracking);
				break;
			case PlayerStat.Movement:
				result = pstats.GetDamage(this.StatTracking);
				break;
			default:
				result = pstats.GetStat(this.StatTracking);
				break;
			}
			return result;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000C2C90 File Offset: 0x000C0E90
		public string GetStatLabel()
		{
			PlayerStat statTracking = this.StatTracking;
			switch (statTracking)
			{
			case PlayerStat.Primary:
				return this.Generator.Root.Name + " Damage";
			case PlayerStat.Secondary:
				return this.Spender.Root.Name + " Damage";
			case PlayerStat.Utility:
				return "Signature Damage";
			case PlayerStat.Movement:
				return this.Movement.Root.Name + " Damage";
			default:
				return PostGame_Stats_Display.GetTitle(statTracking);
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000C2D19 File Offset: 0x000C0F19
		public BookClubChallenge()
		{
		}

		// Token: 0x040021F4 RID: 8692
		public string Name;

		// Token: 0x040021F5 RID: 8693
		public string ID;

		// Token: 0x040021F6 RID: 8694
		public GenreTree Tome;

		// Token: 0x040021F7 RID: 8695
		[TextArea]
		public string Description = "Description";

		// Token: 0x040021F8 RID: 8696
		public MetaDB.BookClubChallenge.Difficulty DifficultyLevel = MetaDB.BookClubChallenge.Difficulty.Medium;

		// Token: 0x040021F9 RID: 8697
		public PlayerStat StatTracking;

		// Token: 0x040021FA RID: 8698
		public int StatMin;

		// Token: 0x040021FB RID: 8699
		public int StatMax;

		// Token: 0x040021FC RID: 8700
		public bool OverrideEnemySet;

		// Token: 0x040021FD RID: 8701
		public AILayout AILayout;

		// Token: 0x040021FE RID: 8702
		public int BindingBoost;

		// Token: 0x040021FF RID: 8703
		public List<AugmentTree> Bindings;

		// Token: 0x04002200 RID: 8704
		public List<AugmentTree> TornPages;

		// Token: 0x04002201 RID: 8705
		public MagicColor Signature;

		// Token: 0x04002202 RID: 8706
		public AbilityTree Generator;

		// Token: 0x04002203 RID: 8707
		public AbilityTree Spender;

		// Token: 0x04002204 RID: 8708
		public AbilityTree Movement;

		// Token: 0x04002205 RID: 8709
		public List<AugmentTree> PlayerPages;

		// Token: 0x04002206 RID: 8710
		public List<AugmentTree> FirstPick;

		// Token: 0x020006B7 RID: 1719
		public enum StatType
		{
			// Token: 0x04002CBE RID: 11454
			Player,
			// Token: 0x04002CBF RID: 11455
			Signature
		}

		// Token: 0x020006B8 RID: 1720
		public enum PStatCategory
		{
			// Token: 0x04002CC1 RID: 11457
			Damage,
			// Token: 0x04002CC2 RID: 11458
			Healing,
			// Token: 0x04002CC3 RID: 11459
			Count,
			// Token: 0x04002CC4 RID: 11460
			Max
		}

		// Token: 0x020006B9 RID: 1721
		public enum Difficulty
		{
			// Token: 0x04002CC6 RID: 11462
			Easy,
			// Token: 0x04002CC7 RID: 11463
			Medium,
			// Token: 0x04002CC8 RID: 11464
			Hard,
			// Token: 0x04002CC9 RID: 11465
			VeryHard
		}
	}

	// Token: 0x02000452 RID: 1106
	[Serializable]
	public class ChallengeUpdate
	{
		// Token: 0x06002153 RID: 8531 RVA: 0x000C2D33 File Offset: 0x000C0F33
		public ChallengeUpdate()
		{
		}

		// Token: 0x04002207 RID: 8711
		public int Loop;

		// Token: 0x04002208 RID: 8712
		public int Count;
	}
}
