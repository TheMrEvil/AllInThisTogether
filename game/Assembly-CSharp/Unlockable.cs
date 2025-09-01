using System;

// Token: 0x020000F7 RID: 247
[Serializable]
public class Unlockable
{
	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0004C576 File Offset: 0x0004A776
	public virtual string GUID
	{
		get
		{
			return "INVALID";
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0004C57D File Offset: 0x0004A77D
	public virtual string UnlockName
	{
		get
		{
			return "NA";
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0004C584 File Offset: 0x0004A784
	public virtual string CategoryName
	{
		get
		{
			return "Unlock";
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0004C58B File Offset: 0x0004A78B
	public virtual UnlockCategory Type
	{
		get
		{
			return UnlockCategory.Generic;
		}
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x0004C590 File Offset: 0x0004A790
	public string GetUnlockReqText()
	{
		switch (this.UnlockedBy)
		{
		case Unlockable.UnlockType.Default:
			return "";
		case Unlockable.UnlockType.Purchase:
			return "";
		case Unlockable.UnlockType.Achievement:
		{
			if (string.IsNullOrEmpty(this.Achievement))
			{
				return "";
			}
			AchievementRootNode achievement = GraphDB.GetAchievement(this.Achievement);
			return ((achievement != null) ? achievement.Detail : null) ?? "Unknown";
		}
		case Unlockable.UnlockType.Drop:
			return "Dropped from a Torn Boss.";
		case Unlockable.UnlockType.TomeReward:
		{
			string str = "Mend " + this.RewardingTome.Root.ShortName;
			if (this.AtBinding > 0)
			{
				str = str + " at Binding Level " + this.AtBinding.ToString();
			}
			return str + ".";
		}
		case Unlockable.UnlockType.Prestige:
			return "Reach Scribe Prestige " + this.PrestigeLevel.ToString();
		case Unlockable.UnlockType.Raid:
		{
			string text = this.HardMode ? "<sprite name=\"raid_hard\">" : "<sprite name=\"raid\">";
			if (this.TornLords)
			{
				return "Dropped by a " + text + " Torn Lord.";
			}
			RaidDB.Raid raid = RaidDB.GetRaid(this.Raid);
			foreach (RaidDB.Encounter encounter in raid.Encounters)
			{
				if (encounter.HMNookItem == this.GUID)
				{
					return string.Concat(new string[]
					{
						"Dropped by ",
						text,
						" <b>",
						encounter.BossName,
						"</b> on Hard Mode."
					});
				}
			}
			return string.Concat(new string[]
			{
				"Found in ",
				text,
				" <b>",
				raid.RaidName,
				"</b>."
			});
		}
		default:
			return "";
		}
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x0004C76C File Offset: 0x0004A96C
	public Unlockable()
	{
	}

	// Token: 0x04000985 RID: 2437
	public Unlockable.UnlockType UnlockedBy = Unlockable.UnlockType.Drop;

	// Token: 0x04000986 RID: 2438
	public int Cost;

	// Token: 0x04000987 RID: 2439
	public GenreTree RewardingTome;

	// Token: 0x04000988 RID: 2440
	public int AtBinding;

	// Token: 0x04000989 RID: 2441
	public string Achievement;

	// Token: 0x0400098A RID: 2442
	public int PrestigeLevel;

	// Token: 0x0400098B RID: 2443
	public bool HardMode;

	// Token: 0x0400098C RID: 2444
	public bool TornLords;

	// Token: 0x0400098D RID: 2445
	public RaidDB.RaidType Raid;

	// Token: 0x0400098E RID: 2446
	public bool ResetWithPrestige;

	// Token: 0x020004F4 RID: 1268
	public enum UnlockType
	{
		// Token: 0x04002527 RID: 9511
		Default,
		// Token: 0x04002528 RID: 9512
		Purchase,
		// Token: 0x04002529 RID: 9513
		Achievement,
		// Token: 0x0400252A RID: 9514
		Drop,
		// Token: 0x0400252B RID: 9515
		TomeReward,
		// Token: 0x0400252C RID: 9516
		Prestige,
		// Token: 0x0400252D RID: 9517
		Raid
	}
}
