using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class SettingsCredits : MonoBehaviour
{
	// Token: 0x06001026 RID: 4134 RVA: 0x00065459 File Offset: 0x00063659
	private void Awake()
	{
		this.SetIndex(0);
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x00065462 File Offset: 0x00063662
	public void NextPage()
	{
		this.curIndex++;
		if (this.curIndex >= this.Pages.Count)
		{
			this.curIndex = 0;
		}
		this.UpdateDisplay();
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x00065492 File Offset: 0x00063692
	public void PrevPage()
	{
		this.curIndex--;
		if (this.curIndex < 0)
		{
			this.curIndex = this.Pages.Count - 1;
		}
		this.UpdateDisplay();
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x000654C4 File Offset: 0x000636C4
	public void SetIndex(int id)
	{
		this.curIndex = id;
		this.UpdateDisplay();
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x000654D4 File Offset: 0x000636D4
	private void UpdateDisplay()
	{
		for (int i = 0; i < this.Pages.Count; i++)
		{
			this.Pages[i].SetActive(i == this.curIndex);
		}
		for (int j = 0; j < this.PipDisplays.Count; j++)
		{
			this.PipDisplays[j].SetActive(j == this.curIndex);
		}
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x00065541 File Offset: 0x00063741
	public SettingsCredits()
	{
	}

	// Token: 0x04000E40 RID: 3648
	private int curIndex;

	// Token: 0x04000E41 RID: 3649
	public List<GameObject> Pages;

	// Token: 0x04000E42 RID: 3650
	public List<GameObject> PipDisplays;
}
