using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class InkTalentItemUI : MonoBehaviour
{
	// Token: 0x06000F9C RID: 3996 RVA: 0x00062A64 File Offset: 0x00060C64
	public void Setup(AugmentTree augment, int index, InkTalentRowUI rowRef)
	{
		this.row = rowRef;
		this.TalentIndex = index;
		if (rowRef.RowIndex < 3)
		{
			this.InfoBox.AnchorLoc = this.BottomAnchor;
			this.InfoBox.Setup(augment, 1, ModType.Player, null, TextAnchor.UpperCenter, Vector3.zero);
			return;
		}
		this.InfoBox.Setup(augment, 1, ModType.Player, null, TextAnchor.LowerCenter, Vector3.zero);
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x00062AD0 File Offset: 0x00060CD0
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

	// Token: 0x06000F9E RID: 3998 RVA: 0x00062BB8 File Offset: 0x00060DB8
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

	// Token: 0x06000F9F RID: 3999 RVA: 0x00062C07 File Offset: 0x00060E07
	public InkTalentItemUI()
	{
	}

	// Token: 0x04000D98 RID: 3480
	public AugmentInfoBox InfoBox;

	// Token: 0x04000D99 RID: 3481
	public List<GameObject> LockDisplays;

	// Token: 0x04000D9A RID: 3482
	public List<GameObject> UnlockDisplays;

	// Token: 0x04000D9B RID: 3483
	public List<GameObject> SelectedDisplays;

	// Token: 0x04000D9C RID: 3484
	public RectTransform BottomAnchor;

	// Token: 0x04000D9D RID: 3485
	public ParticleSystem ApplyFX;

	// Token: 0x04000D9E RID: 3486
	public AudioClip SelectedSFX;

	// Token: 0x04000D9F RID: 3487
	private int TalentIndex;

	// Token: 0x04000DA0 RID: 3488
	private InkTalentRowUI row;

	// Token: 0x04000DA1 RID: 3489
	private bool locked;

	// Token: 0x04000DA2 RID: 3490
	private bool selected;
}
