using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200015E RID: 350
public class CosmeticUIListItem : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x06000F4D RID: 3917 RVA: 0x000612D4 File Offset: 0x0005F4D4
	private void Awake()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x000612F4 File Offset: 0x0005F4F4
	public void Setup(Cosmetic item)
	{
		this.cosmetic = item;
		GameDB.QualityInfo qualityInfo = GameDB.Quality(item.Rarity);
		this.Label.color = qualityInfo.DarkTextColor;
		this.Label.text = item.Name;
		this.HoverLabel.text = item.Name;
		bool flag = true;
		bool flag2 = UnlockManager.IsCosmeticUnlocked(item);
		this.LockedDisplay.gameObject.SetActive(!flag2);
		this.AchievementIcon.SetActive(!flag2 && item.UnlockedBy == Unlockable.UnlockType.Achievement);
		this.CostText.gameObject.SetActive(item.UnlockedBy == Unlockable.UnlockType.Purchase && !flag2 && flag);
		if (item.UnlockedBy == Unlockable.UnlockType.Purchase)
		{
			this.CostText.text = item.Cost.ToString();
		}
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x000613BF File Offset: 0x0005F5BF
	public void UpdateDisplay(bool isUnlocked)
	{
		this.LockedDisplay.gameObject.SetActive(!isUnlocked);
		if (isUnlocked)
		{
			this.CostText.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x000613E9 File Offset: 0x0005F5E9
	public void UpdateState(bool isSelected, bool isEquipped)
	{
		this.UnselectedDisplay.SetActive(!isSelected);
		this.SelectedDisplay.SetActive(isSelected);
		this.EquippedDisplay.SetActive(isEquipped);
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00061412 File Offset: 0x0005F612
	public void OnSelect(BaseEventData ev)
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		this.OnClick();
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x00061422 File Offset: 0x0005F622
	private void OnClick()
	{
		CosmeticsPanel.instance.SelectItem(this);
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x0006142F File Offset: 0x0005F62F
	public CosmeticUIListItem()
	{
	}

	// Token: 0x04000D24 RID: 3364
	public TextMeshProUGUI Label;

	// Token: 0x04000D25 RID: 3365
	public TextMeshProUGUI HoverLabel;

	// Token: 0x04000D26 RID: 3366
	public GameObject UnselectedDisplay;

	// Token: 0x04000D27 RID: 3367
	public GameObject SelectedDisplay;

	// Token: 0x04000D28 RID: 3368
	public GameObject EquippedDisplay;

	// Token: 0x04000D29 RID: 3369
	public GameObject LockedDisplay;

	// Token: 0x04000D2A RID: 3370
	public GameObject AchievementIcon;

	// Token: 0x04000D2B RID: 3371
	public TextMeshProUGUI CostText;

	// Token: 0x04000D2C RID: 3372
	[NonSerialized]
	public Cosmetic cosmetic;
}
