using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000186 RID: 390
public class VoteIcon : MonoBehaviour
{
	// Token: 0x06001068 RID: 4200 RVA: 0x00066C38 File Offset: 0x00064E38
	public void Setup(bool self)
	{
		this.Setup(PlayerControl.myInstance.actions.core.Root.magicColor);
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x00066C5C File Offset: 0x00064E5C
	public void Setup(int PlayerID)
	{
		PlayerControl player = PlayerControl.GetPlayer(PlayerID);
		if (player == null)
		{
			return;
		}
		this.Setup(player.actions.core.Root.magicColor);
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x00066C95 File Offset: 0x00064E95
	private void Setup(MagicColor e)
	{
		this.ElementIcon.sprite = GameDB.GetElement(e).Icon;
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x00066CAD File Offset: 0x00064EAD
	public VoteIcon()
	{
	}

	// Token: 0x04000E92 RID: 3730
	public Image Background;

	// Token: 0x04000E93 RID: 3731
	public Image ElementIcon;
}
