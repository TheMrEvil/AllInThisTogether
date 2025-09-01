using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018E RID: 398
public class Diagetic_PortalUI : MonoBehaviour
{
	// Token: 0x060010C4 RID: 4292 RVA: 0x000686C0 File Offset: 0x000668C0
	public void Setup(PortalType pType)
	{
		string text = "Return to Library";
		if (pType == PortalType.Endless)
		{
			text = "Continue";
		}
		else if (pType == PortalType.Raid)
		{
			text = "Continue";
		}
		this.Title.text = text;
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x00068700 File Offset: 0x00066900
	public Diagetic_PortalUI()
	{
	}

	// Token: 0x04000F0E RID: 3854
	public TextMeshProUGUI Title;
}
