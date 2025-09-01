using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200016A RID: 362
public class LibTalentRow : MonoBehaviour
{
	// Token: 0x06000FA8 RID: 4008 RVA: 0x00062F28 File Offset: 0x00061128
	public void Setup(PlayerDB.LibraryTalent row, int rowIndex)
	{
		this.Row = row;
		this.RowIndex = rowIndex;
		List<AugmentTree> talents = row.Talents;
		for (int i = 0; i < talents.Count; i++)
		{
			AugmentTree augment = talents[i];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TalentItemRef, this.TalentItemRef.transform.parent);
			gameObject.SetActive(true);
			LibTalentItemUI component = gameObject.GetComponent<LibTalentItemUI>();
			component.Setup(augment, i, this);
			this.items.Add(component);
		}
		UISelector.SetupHorizontalListNav<LibTalentItemUI>(this.items, null, null, false);
		this.UpdateSelected();
		this.LockedDisplay.transform.SetAsLastSibling();
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x00062FC4 File Offset: 0x000611C4
	public void TryUnlock()
	{
		if (!this.IsLocked)
		{
			return;
		}
		if (false)
		{
			return;
		}
		if (Currency.Gildings < this.Row.UnlockCost)
		{
			return;
		}
		if (!Currency.TrySpend(this.Row.UnlockCost))
		{
			return;
		}
		Progression.LibTalentsUnlocked++;
		LibTalentsPanel.instance.RowUnlocked();
		Progression.SaveState();
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x0006301F File Offset: 0x0006121F
	public void TalentSelected(int index)
	{
		LibTalentsPanel.instance.UpdateEquippedTalent(this.RowIndex, index);
		this.UpdateSelected();
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x00063038 File Offset: 0x00061238
	public void UpdateSelected()
	{
		this.IsLocked = (Progression.LibTalentsUnlocked <= this.RowIndex);
		bool flag = Progression.LibTalentsUnlocked <= this.RowIndex - 1;
		this.LockedDisplay.SetActive(this.IsLocked);
		this.LockButton.gameObject.SetActive(!flag);
		this.CostLabel.text = this.Row.UnlockCost.ToString();
		this.CostLabel.color = ((Currency.Gildings < this.Row.UnlockCost) ? this.CostUnavailableColor : this.CostAvailableColor);
		for (int i = 0; i < this.items.Count; i++)
		{
			bool isSelected = LibTalentsPanel.IsTalentEquipped(this.RowIndex, i);
			this.items[i].UpdateDisplay(this.IsLocked, isSelected);
		}
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x00063114 File Offset: 0x00061314
	public LibTalentRow()
	{
	}

	// Token: 0x04000DB8 RID: 3512
	public GameObject LockedDisplay;

	// Token: 0x04000DB9 RID: 3513
	public Button LockButton;

	// Token: 0x04000DBA RID: 3514
	public TextMeshProUGUI CostLabel;

	// Token: 0x04000DBB RID: 3515
	public Color CostAvailableColor;

	// Token: 0x04000DBC RID: 3516
	public Color CostUnavailableColor;

	// Token: 0x04000DBD RID: 3517
	public GameObject TalentItemRef;

	// Token: 0x04000DBE RID: 3518
	private PlayerDB.LibraryTalent Row;

	// Token: 0x04000DBF RID: 3519
	[NonSerialized]
	public int RowIndex;

	// Token: 0x04000DC0 RID: 3520
	[NonSerialized]
	public List<LibTalentItemUI> items = new List<LibTalentItemUI>();

	// Token: 0x04000DC1 RID: 3521
	private bool IsLocked;
}
