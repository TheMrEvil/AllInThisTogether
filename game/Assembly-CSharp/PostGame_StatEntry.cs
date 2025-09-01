using System;
using TMPro;
using UnityEngine;

// Token: 0x0200017B RID: 379
public class PostGame_StatEntry : MonoBehaviour
{
	// Token: 0x06001013 RID: 4115 RVA: 0x00064F89 File Offset: 0x00063189
	public void SetupBasic(string statName, int value, bool wasBest)
	{
		this.StatTitleText.text = statName;
		this.StatValueText.text = this.GetNumberString(value);
		this.TopIndicator.SetActive(wasBest);
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00064FB5 File Offset: 0x000631B5
	public void SetupBasic(string statName, ulong value, bool wasBest)
	{
		this.StatTitleText.text = statName;
		this.StatValueText.text = this.GetNumberString(value);
		this.TopIndicator.SetActive(wasBest);
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x00064FE1 File Offset: 0x000631E1
	private string GetNumberString(int value)
	{
		if (value < 1000)
		{
			return value.ToString();
		}
		return string.Format("{0:N0}", value);
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x00065003 File Offset: 0x00063203
	private string GetNumberString(ulong value)
	{
		if (value < 1000UL)
		{
			return value.ToString();
		}
		return string.Format("{0:N0}", value);
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x00065026 File Offset: 0x00063226
	public PostGame_StatEntry()
	{
	}

	// Token: 0x04000E31 RID: 3633
	public TextMeshProUGUI StatTitleText;

	// Token: 0x04000E32 RID: 3634
	public TextMeshProUGUI StatValueText;

	// Token: 0x04000E33 RID: 3635
	public GameObject TopIndicator;
}
