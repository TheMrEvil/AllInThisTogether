using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014C RID: 332
public class DPSMeterEntryUI : MonoBehaviour
{
	// Token: 0x06000EDF RID: 3807 RVA: 0x0005EB34 File Offset: 0x0005CD34
	public void Setup(PlayerControl plr, float dps)
	{
		PlayerDB.CoreDisplay core = PlayerDB.GetCore(plr.actions.core.Root.magicColor);
		if (core != null)
		{
			this.Icon.sprite = core.BigIcon;
		}
		this.UsernameText.text = plr.GetUsernameText();
		int num = (int)dps;
		if (num < 1000)
		{
			this.DPSText.text = num.ToString();
			return;
		}
		this.DPSText.text = string.Format("{0:N0}", num);
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x0005EBBA File Offset: 0x0005CDBA
	public DPSMeterEntryUI()
	{
	}

	// Token: 0x04000C91 RID: 3217
	public Image Icon;

	// Token: 0x04000C92 RID: 3218
	public TextMeshProUGUI UsernameText;

	// Token: 0x04000C93 RID: 3219
	public TextMeshProUGUI DPSText;
}
