using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000166 RID: 358
public class InkTalentColorSelector : MonoBehaviour
{
	// Token: 0x06000F99 RID: 3993 RVA: 0x000629B6 File Offset: 0x00060BB6
	public void Click()
	{
		InkTalentsPanel.instance.SetupTalentDisplays(this.Color);
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x000629C8 File Offset: 0x00060BC8
	public void SetSelected(bool isSelected, bool isEquipped)
	{
		foreach (GameObject gameObject in this.SelectedDisplay)
		{
			gameObject.SetActive(isSelected);
		}
		foreach (GameObject gameObject2 in this.EquippedDisplay)
		{
			gameObject2.SetActive(isEquipped);
		}
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x00062A5C File Offset: 0x00060C5C
	public InkTalentColorSelector()
	{
	}

	// Token: 0x04000D95 RID: 3477
	public MagicColor Color;

	// Token: 0x04000D96 RID: 3478
	public List<GameObject> SelectedDisplay;

	// Token: 0x04000D97 RID: 3479
	public List<GameObject> EquippedDisplay;
}
