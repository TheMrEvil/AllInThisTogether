using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000165 RID: 357
public class InkLevel_Item : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler
{
	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00062687 File Offset: 0x00060887
	public int Level
	{
		get
		{
			PlayerDB.TalentRow talentRow = this.talentRow;
			if (talentRow == null)
			{
				return -1;
			}
			return talentRow.Level;
		}
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x0006269C File Offset: 0x0006089C
	public void Setup(PlayerDB.TalentRow row, int index)
	{
		this.rowIndex = index;
		this.talentRow = row;
		this.LevelText.text = row.Level.ToString();
		this.isUnlocked = (Progression.InkLevel >= row.Level);
		this.augment = this.talentRow.Talent_1;
		if (this.augment == null)
		{
			return;
		}
		foreach (Image image in this.Icons)
		{
			image.sprite = this.augment.Root.Icon;
		}
		this.RarityBorder_Medium.SetActive(row.importance == PlayerDB.TalentRow.Importance.Medium);
		this.RarityBorder_High.SetActive(row.importance == PlayerDB.TalentRow.Importance.High);
		this.UpdateUnlockDisplay();
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x00062784 File Offset: 0x00060984
	public void UpdateUnlockDisplay()
	{
		int inkLevel = Progression.InkLevel;
		this.isUnlocked = (inkLevel >= this.talentRow.Level);
		this.CurLevelDisplay.SetActive(inkLevel == this.talentRow.Level);
		this.LevelText.color = (this.isUnlocked ? this.LevelColorAvailable : this.LevelColorLocked);
		this.LevelBackground.sprite = (this.isUnlocked ? this.LevelBackAvailable : this.LevelBackLocked);
		this.BarDisplay.SetActive(this.talentRow.Level < Progression.MaxInkLevel);
		if (inkLevel > this.talentRow.Level)
		{
			this.BarFill.fillAmount = 1f;
		}
		else if (inkLevel < this.talentRow.Level)
		{
			this.BarFill.fillAmount = 0f;
		}
		else
		{
			this.BarFill.fillAmount = 0f;
		}
		foreach (GameObject gameObject in this.LockedObjects)
		{
			gameObject.SetActive(!this.isUnlocked);
		}
		foreach (GameObject gameObject2 in this.UnlockedObjects)
		{
			gameObject2.SetActive(this.isUnlocked);
		}
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x00062908 File Offset: 0x00060B08
	public void OnPointerEnter(PointerEventData ev)
	{
		Tooltip.Show(this.TooltipAnchor.position, TextAnchor.LowerCenter, this.augment, 1, null);
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x00062923 File Offset: 0x00060B23
	public void OnPointerExit(PointerEventData ev)
	{
		Tooltip.Release();
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x0006292A File Offset: 0x00060B2A
	public void OnSelect(BaseEventData ev)
	{
		Tooltip.Show(this.TooltipAnchor.position, TextAnchor.LowerCenter, this.augment, 1, null);
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x00062948 File Offset: 0x00060B48
	public void Click()
	{
		if (this.talentRow.Button == PlayerDB.TalentRow.ButtonType.None)
		{
			return;
		}
		if (this.talentRow.Button == PlayerDB.TalentRow.ButtonType.NeutralTalent)
		{
			InkTalentsPanel.instance.OpenUI(MagicColor.Neutral);
			return;
		}
		if (this.talentRow.Button == PlayerDB.TalentRow.ButtonType.ColorTalent)
		{
			InkTalentsPanel.instance.OpenUI(PlayerControl.myInstance.actions.core.Root.magicColor);
		}
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x000629AE File Offset: 0x00060BAE
	public InkLevel_Item()
	{
	}

	// Token: 0x04000D82 RID: 3458
	public TextMeshProUGUI LevelText;

	// Token: 0x04000D83 RID: 3459
	public Color LevelColorLocked;

	// Token: 0x04000D84 RID: 3460
	public Color LevelColorAvailable;

	// Token: 0x04000D85 RID: 3461
	public Image LevelBackground;

	// Token: 0x04000D86 RID: 3462
	public Sprite LevelBackLocked;

	// Token: 0x04000D87 RID: 3463
	public Sprite LevelBackAvailable;

	// Token: 0x04000D88 RID: 3464
	public GameObject CurLevelDisplay;

	// Token: 0x04000D89 RID: 3465
	[Header("Progress Bar")]
	public GameObject BarDisplay;

	// Token: 0x04000D8A RID: 3466
	public Image BarFill;

	// Token: 0x04000D8B RID: 3467
	public List<Image> Icons;

	// Token: 0x04000D8C RID: 3468
	public List<GameObject> LockedObjects;

	// Token: 0x04000D8D RID: 3469
	public List<GameObject> UnlockedObjects;

	// Token: 0x04000D8E RID: 3470
	public GameObject RarityBorder_Medium;

	// Token: 0x04000D8F RID: 3471
	public GameObject RarityBorder_High;

	// Token: 0x04000D90 RID: 3472
	public Transform TooltipAnchor;

	// Token: 0x04000D91 RID: 3473
	private int rowIndex;

	// Token: 0x04000D92 RID: 3474
	private PlayerDB.TalentRow talentRow;

	// Token: 0x04000D93 RID: 3475
	private bool isUnlocked;

	// Token: 0x04000D94 RID: 3476
	private AugmentTree augment;
}
