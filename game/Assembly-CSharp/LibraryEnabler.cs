using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class LibraryEnabler : MonoBehaviour
{
	// Token: 0x06000CCB RID: 3275 RVA: 0x00051E24 File Offset: 0x00050024
	private void Start()
	{
		bool flag = false;
		switch (this.Requirement)
		{
		case LibraryEnabler.RequirementType.TomeBeaten:
		{
			int tomeStat = GameStats.GetTomeStat(this.Tome, GameStats.Stat.TomesWon, 0);
			int tomeStat2 = GameStats.GetTomeStat(this.Tome, GameStats.Stat.MaxBinding, 0);
			if (tomeStat < this.Times || tomeStat2 < this.MaxBinding)
			{
				flag = true;
			}
			break;
		}
		case LibraryEnabler.RequirementType.Achievement:
		{
			AchievementTree achievement = this.Achievement;
			if (!AchievementManager.IsUnlocked((achievement != null) ? achievement.Root.ID : null))
			{
				flag = true;
			}
			break;
		}
		case LibraryEnabler.RequirementType.MetaProgression:
			if (Progression.InkLevel < this.InkLevel || Progression.PrestigeCount < this.Prestige)
			{
				flag = true;
			}
			break;
		}
		if (this.InvertActivation)
		{
			flag = !flag;
		}
		base.gameObject.SetActive(!flag);
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x00051ED8 File Offset: 0x000500D8
	public LibraryEnabler()
	{
	}

	// Token: 0x04000A28 RID: 2600
	public LibraryEnabler.RequirementType Requirement;

	// Token: 0x04000A29 RID: 2601
	public GenreTree Tome;

	// Token: 0x04000A2A RID: 2602
	public int Times;

	// Token: 0x04000A2B RID: 2603
	public int MaxBinding;

	// Token: 0x04000A2C RID: 2604
	public AchievementTree Achievement;

	// Token: 0x04000A2D RID: 2605
	public int InkLevel;

	// Token: 0x04000A2E RID: 2606
	public int Prestige;

	// Token: 0x04000A2F RID: 2607
	[Header("Behaviour")]
	public bool InvertActivation;

	// Token: 0x02000510 RID: 1296
	public enum RequirementType
	{
		// Token: 0x040025AF RID: 9647
		TomeBeaten,
		// Token: 0x040025B0 RID: 9648
		Achievement,
		// Token: 0x040025B1 RID: 9649
		MetaProgression
	}
}
