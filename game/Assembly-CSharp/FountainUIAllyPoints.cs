using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AD RID: 429
public class FountainUIAllyPoints : MonoBehaviour
{
	// Token: 0x060011D0 RID: 4560 RVA: 0x0006EA14 File Offset: 0x0006CC14
	public void Setup(PlayerControl player)
	{
		if (player == null)
		{
			return;
		}
		InkManager.PlayerInk orCreateInk = InkManager.instance.GetOrCreateInk(player.ViewID);
		int numberValue = (orCreateInk != null) ? orCreateInk.Amount : 0;
		this.AmountText.text = numberValue.ToString();
		this.PlayerIcon.sprite = GameDB.GetElement(player.actions.core.Root.magicColor).Icon;
		base.GetComponent<UIPingable>().SetNumberValue(numberValue);
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x0006EA90 File Offset: 0x0006CC90
	public FountainUIAllyPoints()
	{
	}

	// Token: 0x04001087 RID: 4231
	public Image PlayerIcon;

	// Token: 0x04001088 RID: 4232
	public TextMeshProUGUI AmountText;
}
