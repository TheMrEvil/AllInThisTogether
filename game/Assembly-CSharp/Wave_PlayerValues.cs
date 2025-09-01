using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
[Serializable]
public class Wave_PlayerValues
{
	// Token: 0x060002EC RID: 748 RVA: 0x00018DA2 File Offset: 0x00016FA2
	private string HeaderText()
	{
		return "Team Size - " + this.PlayerCount.ToString();
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00018DBC File Offset: 0x00016FBC
	public Wave_PlayerValues()
	{
	}

	// Token: 0x040002D9 RID: 729
	public int PlayerCount = 1;

	// Token: 0x040002DA RID: 730
	public float TotalBudgetMult = 1f;

	// Token: 0x040002DB RID: 731
	public float GroupSizeMult = 1f;

	// Token: 0x040002DC RID: 732
	public float NextGrpMult = 1f;

	// Token: 0x040002DD RID: 733
	[Header("Enemy Health Scaling")]
	public float BaseHealthScale = 1f;

	// Token: 0x040002DE RID: 734
	public float BaseDamageScale = 1f;

	// Token: 0x040002DF RID: 735
	public float EliteHealthScale = 1f;

	// Token: 0x040002E0 RID: 736
	public float BossHealthScale = 1f;

	// Token: 0x040002E1 RID: 737
	public float BossGroupTimerMult = 1f;

	// Token: 0x040002E2 RID: 738
	public float RaidBossHealthScale = 1f;
}
