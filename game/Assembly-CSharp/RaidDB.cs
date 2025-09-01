using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class RaidDB : ScriptableObject
{
	// Token: 0x060002B4 RID: 692 RVA: 0x00017354 File Offset: 0x00015554
	public static void SetInstance(RaidDB db)
	{
		if (RaidDB.instance == db)
		{
			return;
		}
		RaidDB.instance = db;
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0001736C File Offset: 0x0001556C
	public static RaidDB.Raid GetRaid(RaidDB.RaidType type)
	{
		if (RaidDB.instance == null)
		{
			RaidDB.instance = Resources.Load<RaidDB>("RaidDB");
		}
		RaidDB.Raid result;
		switch (type)
		{
		case RaidDB.RaidType.Myriad:
			result = RaidDB.instance.MyriadRaid;
			break;
		case RaidDB.RaidType.Verse:
			result = RaidDB.instance.VerseRaid;
			break;
		case RaidDB.RaidType.Horizon:
			result = RaidDB.instance.HorizonRaid;
			break;
		default:
			if (type != RaidDB.RaidType.Test)
			{
				result = null;
			}
			else
			{
				result = RaidDB.instance.TestRaid;
			}
			break;
		}
		return result;
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x000173E8 File Offset: 0x000155E8
	public static RaidDB.Raid GetRaidByEnemyType(EnemyType enemyType)
	{
		if (RaidDB.instance == null)
		{
			RaidDB.instance = Resources.Load<RaidDB>("RaidDB");
		}
		RaidDB.Raid result;
		if (enemyType != EnemyType.Splice)
		{
			if (enemyType != EnemyType.Tangent)
			{
				if (enemyType == EnemyType.Raving)
				{
					result = RaidDB.instance.MyriadRaid;
				}
				else
				{
					result = null;
				}
			}
			else
			{
				result = RaidDB.instance.HorizonRaid;
			}
		}
		else
		{
			result = RaidDB.instance.VerseRaid;
		}
		return result;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00017448 File Offset: 0x00015648
	public static RaidDB.RaidType GetRaidFromEncounter(string id)
	{
		using (List<RaidDB.Encounter>.Enumerator enumerator = RaidDB.instance.MyriadRaid.Encounters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ID == id)
				{
					return RaidDB.RaidType.Myriad;
				}
			}
		}
		using (List<RaidDB.Encounter>.Enumerator enumerator = RaidDB.instance.VerseRaid.Encounters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ID == id)
				{
					return RaidDB.RaidType.Verse;
				}
			}
		}
		using (List<RaidDB.Encounter>.Enumerator enumerator = RaidDB.instance.HorizonRaid.Encounters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ID == id)
				{
					return RaidDB.RaidType.Horizon;
				}
			}
		}
		using (List<RaidDB.Encounter>.Enumerator enumerator = RaidDB.instance.TestRaid.Encounters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ID == id)
				{
					return RaidDB.RaidType.Test;
				}
			}
		}
		return RaidDB.RaidType.Myriad;
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x000175B4 File Offset: 0x000157B4
	public static RaidDB.Encounter GetEncounter(RaidDB.RaidType type, string id)
	{
		RaidDB.Raid raid = RaidDB.GetRaid(type);
		if (raid == null)
		{
			return null;
		}
		return raid.GetEncounter(id);
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x000175D4 File Offset: 0x000157D4
	public static bool IsFinalEncounter(RaidDB.RaidType type, string id)
	{
		RaidDB.Raid raid = RaidDB.GetRaid(type);
		if (raid == null)
		{
			return false;
		}
		List<RaidDB.Encounter> encounters = raid.Encounters;
		int index = encounters.Count - 1;
		return encounters[index].ID == id;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00017610 File Offset: 0x00015810
	public static string GetRaidUnlockText(RaidDB.RaidType type)
	{
		string result;
		switch (type)
		{
		case RaidDB.RaidType.Myriad:
			result = RaidDB.instance.MyriadRaid.GetUnlockText();
			break;
		case RaidDB.RaidType.Verse:
			result = RaidDB.instance.VerseRaid.GetUnlockText();
			break;
		case RaidDB.RaidType.Horizon:
			result = RaidDB.instance.HorizonRaid.GetUnlockText();
			break;
		default:
			result = "";
			break;
		}
		return result;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0001766E File Offset: 0x0001586E
	public static bool IsAnyRaidUnlocked()
	{
		return RaidDB.instance.MyriadRaid.IsUnlocked() || RaidDB.instance.VerseRaid.IsUnlocked() || RaidDB.instance.HorizonRaid.IsUnlocked();
	}

	// Token: 0x060002BC RID: 700 RVA: 0x000176A4 File Offset: 0x000158A4
	public static bool IsRaidReward(AugmentTree augment)
	{
		if (RaidDB.instance == null)
		{
			return false;
		}
		using (List<RaidDB.Encounter>.Enumerator enumerator = RaidDB.instance.MyriadRaid.Encounters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.RewardPage == augment)
				{
					return true;
				}
			}
		}
		using (List<RaidDB.Encounter>.Enumerator enumerator = RaidDB.instance.VerseRaid.Encounters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.RewardPage == augment)
				{
					return true;
				}
			}
		}
		using (List<RaidDB.Encounter>.Enumerator enumerator = RaidDB.instance.HorizonRaid.Encounters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.RewardPage == augment)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x000177C8 File Offset: 0x000159C8
	public RaidDB()
	{
	}

	// Token: 0x040002A8 RID: 680
	public static RaidDB instance;

	// Token: 0x040002A9 RID: 681
	public Sprite RaidIcon;

	// Token: 0x040002AA RID: 682
	public Sprite RaidIconHard;

	// Token: 0x040002AB RID: 683
	public RaidDB.Raid MyriadRaid;

	// Token: 0x040002AC RID: 684
	public RaidDB.Raid VerseRaid;

	// Token: 0x040002AD RID: 685
	public RaidDB.Raid HorizonRaid;

	// Token: 0x040002AE RID: 686
	public RaidDB.Raid TestRaid;

	// Token: 0x040002AF RID: 687
	public List<string> FinalBossNookItems = new List<string>();

	// Token: 0x040002B0 RID: 688
	public AnimationCurve EncounterQuillmarks;

	// Token: 0x040002B1 RID: 689
	public float HardModeQuillMultiplier = 2f;

	// Token: 0x040002B2 RID: 690
	public AnimationCurve EncounterGildings;

	// Token: 0x040002B3 RID: 691
	public float HardModeGildingMultiplier = 2f;

	// Token: 0x02000463 RID: 1123
	[Serializable]
	public class Raid
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x000C3120 File Offset: 0x000C1320
		public bool ReachedAttunmentRequirement
		{
			get
			{
				return Progression.PrestigeCount > 0 || Progression.BindingAttunement >= this.AttunementRequired;
			}
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000C313C File Offset: 0x000C133C
		public RaidDB.Encounter GetEncounter(string id)
		{
			foreach (RaidDB.Encounter encounter in this.Encounters)
			{
				if (encounter.ID == id)
				{
					return encounter;
				}
			}
			return null;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000C31A0 File Offset: 0x000C13A0
		public string GetUnlockText()
		{
			string str = string.Format("Reach Attunement {0}", this.AttunementRequired);
			if (this.ReachedAttunmentRequirement)
			{
				str = "<s>  " + str + "  </s>";
			}
			string text = "";
			if (this.RaidType == RaidDB.RaidType.Verse)
			{
				text = "Complete The Grey Area";
			}
			else if (this.RaidType == RaidDB.RaidType.Horizon)
			{
				text = "Complete The Forgery";
			}
			if (this.HasCompletedPrerequisites() && this.RaidType != RaidDB.RaidType.Myriad)
			{
				text = "<s>  " + text + "  </s>";
			}
			string str2 = "";
			if (!string.IsNullOrEmpty(text))
			{
				str2 = str2 + text + "\n<size=30>and</size>\n";
			}
			return str2 + str;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000C3247 File Offset: 0x000C1447
		public bool IsUnlocked()
		{
			return this.ReachedAttunmentRequirement && this.HasCompletedPrerequisites();
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000C3260 File Offset: 0x000C1460
		private bool HasCompletedPrerequisites()
		{
			return this.RaidType == RaidDB.RaidType.Myriad || (this.RaidType == RaidDB.RaidType.Verse && GameStats.GetRaidStat("myriad_boss", GameStats.RaidStat.Completed, 0) > 0) || (this.RaidType == RaidDB.RaidType.Horizon && GameStats.GetRaidStat("verse_boss", GameStats.RaidStat.Completed, 0) > 0);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000C32AC File Offset: 0x000C14AC
		public Raid()
		{
		}

		// Token: 0x04002256 RID: 8790
		public string RaidName;

		// Token: 0x04002257 RID: 8791
		public RaidDB.RaidType RaidType;

		// Token: 0x04002258 RID: 8792
		public Sprite NormalDifficultyBgr;

		// Token: 0x04002259 RID: 8793
		[TextArea]
		public string NormalDifficultyInfo;

		// Token: 0x0400225A RID: 8794
		public Sprite HardDifficultyBgr;

		// Token: 0x0400225B RID: 8795
		[TextArea]
		public string HardDifficultyInfo;

		// Token: 0x0400225C RID: 8796
		public Sprite StampNormal;

		// Token: 0x0400225D RID: 8797
		public Sprite StampHard;

		// Token: 0x0400225E RID: 8798
		public int AttunementRequired;

		// Token: 0x0400225F RID: 8799
		public List<RaidDB.Encounter> Encounters = new List<RaidDB.Encounter>();
	}

	// Token: 0x02000464 RID: 1124
	[Serializable]
	public class Encounter
	{
		// Token: 0x06002172 RID: 8562 RVA: 0x000C32C0 File Offset: 0x000C14C0
		public void DoBossIntro()
		{
			AIControl boss = AIManager.GetBoss();
			if (boss == null || boss.Display == null || boss.Display.Anim == null)
			{
				return;
			}
			boss.Display.Anim.Play(this.IntroAnim, this.AnimLayer);
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000C331A File Offset: 0x000C151A
		public Encounter()
		{
		}

		// Token: 0x04002260 RID: 8800
		public string ID;

		// Token: 0x04002261 RID: 8801
		public string Name;

		// Token: 0x04002262 RID: 8802
		public string ShortName;

		// Token: 0x04002263 RID: 8803
		public Sprite Icon;

		// Token: 0x04002264 RID: 8804
		public RaidDB.EncounterType Type;

		// Token: 0x04002265 RID: 8805
		public GameObject Boss;

		// Token: 0x04002266 RID: 8806
		public List<AugmentTree> NormalEnemyAugments;

		// Token: 0x04002267 RID: 8807
		public List<AugmentTree> HardEnemyAugments;

		// Token: 0x04002268 RID: 8808
		public bool HasPageReward;

		// Token: 0x04002269 RID: 8809
		public AugmentFilter RewardFilter;

		// Token: 0x0400226A RID: 8810
		public AugmentTree Objective;

		// Token: 0x0400226B RID: 8811
		public bool DoIntroAnimation;

		// Token: 0x0400226C RID: 8812
		public string IntroAnim;

		// Token: 0x0400226D RID: 8813
		public int AnimLayer;

		// Token: 0x0400226E RID: 8814
		public float StartDelay;

		// Token: 0x0400226F RID: 8815
		public string BossName;

		// Token: 0x04002270 RID: 8816
		public string BossSubname;

		// Token: 0x04002271 RID: 8817
		public bool StartWithEnemyPage;

		// Token: 0x04002272 RID: 8818
		public string EnemyTopText;

		// Token: 0x04002273 RID: 8819
		public string EnemyMainText;

		// Token: 0x04002274 RID: 8820
		public List<AugmentTree> BossAugments = new List<AugmentTree>();

		// Token: 0x04002275 RID: 8821
		public AugmentTree RewardPage;

		// Token: 0x04002276 RID: 8822
		public string HMNookItem;

		// Token: 0x04002277 RID: 8823
		public string SceneID;
	}

	// Token: 0x02000465 RID: 1125
	public enum EncounterType
	{
		// Token: 0x04002279 RID: 8825
		Boss,
		// Token: 0x0400227A RID: 8826
		CombatEvent,
		// Token: 0x0400227B RID: 8827
		Vignette
	}

	// Token: 0x02000466 RID: 1126
	public enum RaidType
	{
		// Token: 0x0400227D RID: 8829
		Myriad,
		// Token: 0x0400227E RID: 8830
		Verse,
		// Token: 0x0400227F RID: 8831
		Horizon,
		// Token: 0x04002280 RID: 8832
		Test = 99
	}

	// Token: 0x02000467 RID: 1127
	public enum Difficulty
	{
		// Token: 0x04002282 RID: 8834
		Normal,
		// Token: 0x04002283 RID: 8835
		Hard
	}
}
