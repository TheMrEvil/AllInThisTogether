using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000168 RID: 360
public class InkTalentRowUI : MonoBehaviour
{
	// Token: 0x06000FA0 RID: 4000 RVA: 0x00062C10 File Offset: 0x00060E10
	public void Setup(PlayerDB.TalentRow row, int rowIndex)
	{
		this.Row = row;
		this.RowIndex = rowIndex;
		this.TitleText.text = this.Row.Level.ToString();
		List<AugmentTree> talents = row.Talents;
		for (int i = 0; i < talents.Count; i++)
		{
			AugmentTree augment = talents[i];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TalentItemRef, this.TalentItemRef.transform.parent);
			gameObject.SetActive(true);
			InkTalentItemUI component = gameObject.GetComponent<InkTalentItemUI>();
			component.Setup(augment, i, this);
			this.items.Add(component);
		}
		UISelector.SetupHorizontalListNav<InkTalentItemUI>(this.items, null, null, false);
		this.UpdateSelected();
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x00062CB7 File Offset: 0x00060EB7
	public void TalentSelected(int index)
	{
		InkTalentsPanel.instance.UpdateEquippedTalent(this.RowIndex, index);
		this.UpdateSelected();
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x00062CD0 File Offset: 0x00060ED0
	private void UpdateSelected()
	{
		bool flag = Progression.InkLevel < this.Row.Level;
		this.LevelBackDisplay.sprite = (flag ? this.LockedLevelBack : this.UnlockedLevelBack);
		this.TitleText.color = (flag ? this.LevelColorLocked : this.LevelColorUnlocked);
		for (int i = 0; i < this.items.Count; i++)
		{
			bool isSelected = InkTalentsPanel.IsTalentEquipped(this.RowIndex, i);
			this.items[i].UpdateDisplay(flag, isSelected);
		}
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x00062D5E File Offset: 0x00060F5E
	public InkTalentRowUI()
	{
	}

	// Token: 0x04000DA3 RID: 3491
	public TextMeshProUGUI TitleText;

	// Token: 0x04000DA4 RID: 3492
	public Color LevelColorUnlocked;

	// Token: 0x04000DA5 RID: 3493
	public Color LevelColorLocked;

	// Token: 0x04000DA6 RID: 3494
	public Image LevelBackDisplay;

	// Token: 0x04000DA7 RID: 3495
	public Sprite UnlockedLevelBack;

	// Token: 0x04000DA8 RID: 3496
	public Sprite LockedLevelBack;

	// Token: 0x04000DA9 RID: 3497
	public GameObject TalentItemRef;

	// Token: 0x04000DAA RID: 3498
	private PlayerDB.TalentRow Row;

	// Token: 0x04000DAB RID: 3499
	[NonSerialized]
	public int RowIndex;

	// Token: 0x04000DAC RID: 3500
	[NonSerialized]
	public List<InkTalentItemUI> items = new List<InkTalentItemUI>();
}
