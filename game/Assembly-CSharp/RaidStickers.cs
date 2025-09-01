using System;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class RaidStickers : MonoBehaviour
{
	// Token: 0x06001024 RID: 4132 RVA: 0x000653C0 File Offset: 0x000635C0
	public void ShowStickers(string ID)
	{
		bool flag = GameStats.HasRaidSticker(GameStats.RaidStickerType.Raving_Hard, ID);
		this.RavingHard.SetActive(flag);
		this.RavingBase.SetActive(!flag && GameStats.HasRaidSticker(GameStats.RaidStickerType.Raving, ID));
		bool flag2 = GameStats.HasRaidSticker(GameStats.RaidStickerType.Splice_Hard, ID);
		this.SpliceHard.SetActive(flag2);
		this.SpliceBase.SetActive(!flag2 && GameStats.HasRaidSticker(GameStats.RaidStickerType.Splice, ID));
		bool flag3 = GameStats.HasRaidSticker(GameStats.RaidStickerType.Tangent_Hard, ID);
		this.TangentHard.SetActive(flag3);
		this.TangentBase.SetActive(!flag3 && GameStats.HasRaidSticker(GameStats.RaidStickerType.Tangent, ID));
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x00065451 File Offset: 0x00063651
	public RaidStickers()
	{
	}

	// Token: 0x04000E3A RID: 3642
	public GameObject RavingBase;

	// Token: 0x04000E3B RID: 3643
	public GameObject RavingHard;

	// Token: 0x04000E3C RID: 3644
	public GameObject SpliceBase;

	// Token: 0x04000E3D RID: 3645
	public GameObject SpliceHard;

	// Token: 0x04000E3E RID: 3646
	public GameObject TangentBase;

	// Token: 0x04000E3F RID: 3647
	public GameObject TangentHard;
}
