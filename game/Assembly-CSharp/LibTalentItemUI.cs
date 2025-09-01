using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class LibTalentItemUI : MonoBehaviour
{
	// Token: 0x06000FA4 RID: 4004 RVA: 0x00062D74 File Offset: 0x00060F74
	public void Setup(AugmentTree augment, int index, LibTalentRow rowRef)
	{
		this.row = rowRef;
		this.TalentIndex = index;
		if (augment == null)
		{
			return;
		}
		if (rowRef.RowIndex < 3)
		{
			this.InfoBox.AnchorLoc = this.BottomAnchor;
			this.InfoBox.Setup(augment, 1, ModType.Player, null, TextAnchor.UpperCenter, Vector3.zero);
			return;
		}
		this.InfoBox.Setup(augment, 1, ModType.Player, null, TextAnchor.LowerCenter, Vector3.zero);
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x00062DE8 File Offset: 0x00060FE8
	public void UpdateDisplay(bool isLocked, bool isSelected)
	{
		this.locked = isLocked;
		this.selected = isSelected;
		foreach (GameObject gameObject in this.LockDisplays)
		{
			gameObject.SetActive(isLocked);
		}
		foreach (GameObject gameObject2 in this.UnlockDisplays)
		{
			gameObject2.SetActive(!isLocked);
		}
		foreach (GameObject gameObject3 in this.SelectedDisplays)
		{
			gameObject3.SetActive(!isLocked && isSelected);
		}
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x00062ED0 File Offset: 0x000610D0
	public void Click()
	{
		if (this.locked || this.selected)
		{
			return;
		}
		this.row.TalentSelected(this.TalentIndex);
		this.ApplyFX.Play();
		AudioManager.PlayInterfaceSFX(this.SelectedSFX, 1f, 0f);
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x00062F1F File Offset: 0x0006111F
	public LibTalentItemUI()
	{
	}

	// Token: 0x04000DAD RID: 3501
	public AugmentInfoBox InfoBox;

	// Token: 0x04000DAE RID: 3502
	public List<GameObject> LockDisplays;

	// Token: 0x04000DAF RID: 3503
	public List<GameObject> UnlockDisplays;

	// Token: 0x04000DB0 RID: 3504
	public List<GameObject> SelectedDisplays;

	// Token: 0x04000DB1 RID: 3505
	public RectTransform BottomAnchor;

	// Token: 0x04000DB2 RID: 3506
	public ParticleSystem ApplyFX;

	// Token: 0x04000DB3 RID: 3507
	public AudioClip SelectedSFX;

	// Token: 0x04000DB4 RID: 3508
	private int TalentIndex;

	// Token: 0x04000DB5 RID: 3509
	private LibTalentRow row;

	// Token: 0x04000DB6 RID: 3510
	private bool locked;

	// Token: 0x04000DB7 RID: 3511
	private bool selected;
}
