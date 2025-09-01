using System;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class Scriptorium_PrestigeArea : MonoBehaviour
{
	// Token: 0x06000FAD RID: 4013 RVA: 0x00063127 File Offset: 0x00061327
	private void Awake()
	{
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x0006312C File Offset: 0x0006132C
	public void Setup()
	{
		int num = Progression.PrestigeCount + 1;
		Unlockable prestigeRewardDisplay = UnlockDB.GetPrestigeRewardDisplay(num);
		this.rewardInfo.Setup(num, prestigeRewardDisplay);
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x00063155 File Offset: 0x00061355
	private void CreateRewardItem(Unlockable ul)
	{
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x00063157 File Offset: 0x00061357
	private void Clear()
	{
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x00063159 File Offset: 0x00061359
	public Scriptorium_PrestigeArea()
	{
	}

	// Token: 0x04000DC2 RID: 3522
	public Scriptorium_PrestigeRewardItem rewardInfo;
}
