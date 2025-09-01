using System;

// Token: 0x02000059 RID: 89
[Serializable]
public class UpgradeLayout
{
	// Token: 0x060002DA RID: 730 RVA: 0x000189FB File Offset: 0x00016BFB
	public UpgradeLayout()
	{
	}

	// Token: 0x02000473 RID: 1139
	public enum UpgradeFocus
	{
		// Token: 0x040022A8 RID: 8872
		_,
		// Token: 0x040022A9 RID: 8873
		Mana,
		// Token: 0x040022AA RID: 8874
		Keyword
	}

	// Token: 0x02000474 RID: 1140
	public enum UpgradeSelection
	{
		// Token: 0x040022AC RID: 8876
		Primary,
		// Token: 0x040022AD RID: 8877
		Secondary,
		// Token: 0x040022AE RID: 8878
		ExplicitFilter
	}
}
