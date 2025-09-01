using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013A RID: 314
public class BindingAllyAttunementTargetDisplay : MonoBehaviour
{
	// Token: 0x06000E62 RID: 3682 RVA: 0x0005B0B0 File Offset: 0x000592B0
	public void Setup(PlayerControl plr, int attunementLevel)
	{
		float num = -(BindingProgressAreaUI.GetPointOnRing((float)attunementLevel) * 360f) - 1f;
		base.transform.localEulerAngles = new Vector3(0f, 0f, num);
		this.InnerRotate.localEulerAngles = new Vector3(0f, 0f, -num);
		this.TargetText.text = attunementLevel.ToString();
		PlayerDB.CoreDisplay core = PlayerDB.GetCore(plr.actions.core);
		foreach (Image image in this.InkIcon)
		{
			image.sprite = core.BigIcon;
		}
		this.PingRef.SetupAsAttunement(attunementLevel);
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0005B184 File Offset: 0x00059384
	public BindingAllyAttunementTargetDisplay()
	{
	}

	// Token: 0x04000BD0 RID: 3024
	public Transform InnerRotate;

	// Token: 0x04000BD1 RID: 3025
	public TextMeshProUGUI TargetText;

	// Token: 0x04000BD2 RID: 3026
	public List<Image> InkIcon;

	// Token: 0x04000BD3 RID: 3027
	public UIPingable PingRef;
}
