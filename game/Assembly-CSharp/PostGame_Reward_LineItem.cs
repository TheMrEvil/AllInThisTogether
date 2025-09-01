using System;
using TMPro;
using UnityEngine;

// Token: 0x0200017A RID: 378
public class PostGame_Reward_LineItem : MonoBehaviour
{
	// Token: 0x06001011 RID: 4113 RVA: 0x00064F24 File Offset: 0x00063124
	public void SetupBasic(string statName, int value, bool isCosmeticCurrency, bool useAdd)
	{
		this.StatTitleText.text = statName;
		this.StatValueText.text = (useAdd ? ("+" + value.ToString()) : value.ToString());
		this.CosmeticIcon.SetActive(isCosmeticCurrency);
		this.QuillmarkIcon.SetActive(!isCosmeticCurrency);
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x00064F81 File Offset: 0x00063181
	public PostGame_Reward_LineItem()
	{
	}

	// Token: 0x04000E2D RID: 3629
	public TextMeshProUGUI StatTitleText;

	// Token: 0x04000E2E RID: 3630
	public TextMeshProUGUI StatValueText;

	// Token: 0x04000E2F RID: 3631
	public GameObject CosmeticIcon;

	// Token: 0x04000E30 RID: 3632
	public GameObject QuillmarkIcon;
}
