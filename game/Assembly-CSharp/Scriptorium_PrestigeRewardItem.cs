using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016C RID: 364
public class Scriptorium_PrestigeRewardItem : MonoBehaviour
{
	// Token: 0x06000FB2 RID: 4018 RVA: 0x00063164 File Offset: 0x00061364
	public void Setup(int levelRequired, Unlockable reward)
	{
		bool isUnlocked = Progression.PrestigeCount >= levelRequired;
		this.SetupLevel(levelRequired, isUnlocked);
		this.SetupInfo(reward);
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x0006318C File Offset: 0x0006138C
	private void SetupLevel(int levelReq, bool isUnlocked)
	{
		if (this.RankImage != null)
		{
			this.RankImage.sprite = MetaDB.GetPrestigeIcon(levelReq);
		}
		if (this.LevelBack == null)
		{
			return;
		}
		this.LevelBack.sprite = (isUnlocked ? this.BackAchieved : this.BackLocked);
		this.Checkmark.SetActive(isUnlocked);
		this.LevelText.text = (isUnlocked ? "" : levelReq.ToString());
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x0006320C File Offset: 0x0006140C
	private void SetupInfo(Unlockable ul)
	{
		if (ul == null)
		{
			this.TitleText.text = "Pride and Accomplishment";
			this.DetailText.text = "A Sense of";
			this.RewardIcon.gameObject.SetActive(false);
			return;
		}
		this.TitleText.text = ul.UnlockName;
		this.DetailText.text = "(" + ul.CategoryName + ")";
		this.RewardIcon.gameObject.SetActive(true);
		this.RewardIcon.sprite = UnlockDB.GetUnlockIcon(ul);
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x000632A1 File Offset: 0x000614A1
	public Scriptorium_PrestigeRewardItem()
	{
	}

	// Token: 0x04000DC3 RID: 3523
	public Image LevelBack;

	// Token: 0x04000DC4 RID: 3524
	public Sprite BackAchieved;

	// Token: 0x04000DC5 RID: 3525
	public Sprite BackLocked;

	// Token: 0x04000DC6 RID: 3526
	public GameObject Checkmark;

	// Token: 0x04000DC7 RID: 3527
	public TextMeshProUGUI LevelText;

	// Token: 0x04000DC8 RID: 3528
	public Image RankImage;

	// Token: 0x04000DC9 RID: 3529
	public TextMeshProUGUI TitleText;

	// Token: 0x04000DCA RID: 3530
	public TextMeshProUGUI DetailText;

	// Token: 0x04000DCB RID: 3531
	public Image RewardIcon;
}
