using System;
using System.Collections.Generic;

// Token: 0x02000072 RID: 114
[Serializable]
public class InkRow
{
	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000474 RID: 1140 RVA: 0x00022174 File Offset: 0x00020374
	public int InvestedInRow
	{
		get
		{
			int num = 0;
			foreach (InkTalent inkTalent in this.Options)
			{
				num += inkTalent.CurrentValue;
			}
			return num;
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000475 RID: 1141 RVA: 0x000221CC File Offset: 0x000203CC
	public int RowCost
	{
		get
		{
			int num = 0;
			foreach (InkTalent inkTalent in this.Options)
			{
				num += inkTalent.Cost;
			}
			return num;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000476 RID: 1142 RVA: 0x00022224 File Offset: 0x00020424
	public int RemainingForUnlock
	{
		get
		{
			return this.UnlockCost - InkManager.GetInvested(-1);
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000477 RID: 1143 RVA: 0x00022233 File Offset: 0x00020433
	public bool IsUnlocked
	{
		get
		{
			return this.RemainingForUnlock <= 0;
		}
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00022241 File Offset: 0x00020441
	public InkRow()
	{
	}

	// Token: 0x040003B2 RID: 946
	public int Layer;

	// Token: 0x040003B3 RID: 947
	public int UnlockCost;

	// Token: 0x040003B4 RID: 948
	public List<InkTalent> Options = new List<InkTalent>();
}
