using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015D RID: 349
public class ChallengeItemDisplay : MonoBehaviour
{
	// Token: 0x06000F49 RID: 3913 RVA: 0x00060FC0 File Offset: 0x0005F1C0
	public void SetupDisplay(AchievementTree ach)
	{
		if (ach == null)
		{
			return;
		}
		this.achievement = ach;
		AchievementRootNode root = ach.Root;
		this.isUnlocked = AchievementManager.IsUnlocked(root.ID);
		this.isClaimed = (this.isUnlocked && (!root.RequiresClaim || AchievementManager.IsClaimed(root.ID)));
		this.CompletedDisplay.SetActive(this.isUnlocked);
		if (this.isUnlocked)
		{
			this.Background.sprite = this.CompletedBack;
		}
		ValueTuple<int, int> progressValues = root.GetProgressValues(null);
		int num = progressValues.Item1;
		int item = progressValues.Item2;
		if (this.isUnlocked)
		{
			num = item;
		}
		this.ProgressText.text = string.Format("{0:N0} <color=#4d431f><font=\"Alegreya_Black\">/ {1:N0}</font></color>", num, item);
		this.ProgressFill.fillAmount = (float)num / Mathf.Max((float)item, 1f);
		this.Title.text = root.Name;
		this.Detail.text = "- " + TextParser.AugmentDetail(root.Detail, null, null, false);
		this.DisplayRewards();
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x000610DC File Offset: 0x0005F2DC
	private void DisplayRewards()
	{
		AchievementRootNode root = this.achievement.Root;
		foreach (GameObject gameObject in this.UnclaimedDisplay)
		{
			gameObject.SetActive(this.isUnlocked && !this.isClaimed);
		}
		this.ClaimedDisplay.SetActive(this.isUnlocked && this.isClaimed);
		if (root.RewardsCurrency)
		{
			this.CosmeticDisplay.gameObject.SetActive(false);
			this.QuillmarkDisplay.SetActive(root.Quillmarks > 0);
			this.QuillmarkValue.text = root.Quillmarks.ToString();
			this.GildingDisplay.SetActive(root.Gildings > 0 && !this.QuillmarkDisplay.activeSelf);
			this.GildingValue.text = root.Gildings.ToString();
			return;
		}
		this.CosmeticDisplay.gameObject.SetActive(true);
		List<Unlockable> achivementRewards = UnlockDB.GetAchivementRewards(root.ID);
		if (achivementRewards.Count > 0)
		{
			this.CosmeticDisplay.sprite = UnlockDB.GetUnlockIcon(achivementRewards[0]);
		}
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x00061224 File Offset: 0x0005F424
	public void TryClaim()
	{
		if (!this.isUnlocked || this.isClaimed)
		{
			return;
		}
		if (!AchievementManager.ClaimAchievement(this.achievement.Root.ID))
		{
			return;
		}
		foreach (GameObject gameObject in this.UnclaimedDisplay)
		{
			gameObject.SetActive(false);
		}
		this.ClaimedDisplay.SetActive(true);
		SignatureChallengePanel.instance.UpdatedUnclaimed();
		AudioManager.PlayInterfaceSFX(this.ClaimSFX, 1f, 0f);
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x000612CC File Offset: 0x0005F4CC
	public ChallengeItemDisplay()
	{
	}

	// Token: 0x04000D12 RID: 3346
	public TextMeshProUGUI Title;

	// Token: 0x04000D13 RID: 3347
	public TextMeshProUGUI Detail;

	// Token: 0x04000D14 RID: 3348
	public Image ProgressFill;

	// Token: 0x04000D15 RID: 3349
	public TextMeshProUGUI ProgressText;

	// Token: 0x04000D16 RID: 3350
	public GameObject CompletedDisplay;

	// Token: 0x04000D17 RID: 3351
	public Sprite CompletedBack;

	// Token: 0x04000D18 RID: 3352
	public Image Background;

	// Token: 0x04000D19 RID: 3353
	public List<GameObject> UnclaimedDisplay;

	// Token: 0x04000D1A RID: 3354
	public GameObject ClaimedDisplay;

	// Token: 0x04000D1B RID: 3355
	public GameObject QuillmarkDisplay;

	// Token: 0x04000D1C RID: 3356
	public TextMeshProUGUI QuillmarkValue;

	// Token: 0x04000D1D RID: 3357
	public GameObject GildingDisplay;

	// Token: 0x04000D1E RID: 3358
	public TextMeshProUGUI GildingValue;

	// Token: 0x04000D1F RID: 3359
	public Image CosmeticDisplay;

	// Token: 0x04000D20 RID: 3360
	public AudioClip ClaimSFX;

	// Token: 0x04000D21 RID: 3361
	private AchievementTree achievement;

	// Token: 0x04000D22 RID: 3362
	private bool isUnlocked;

	// Token: 0x04000D23 RID: 3363
	private bool isClaimed;
}
