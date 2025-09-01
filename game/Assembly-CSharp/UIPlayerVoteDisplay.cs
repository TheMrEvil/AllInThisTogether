using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000185 RID: 389
public class UIPlayerVoteDisplay : MonoBehaviour
{
	// Token: 0x06001065 RID: 4197 RVA: 0x00066B5C File Offset: 0x00064D5C
	public void Setup(int playerID)
	{
		PlayerControl player = PlayerControl.GetPlayer(playerID);
		if (player == null)
		{
			Debug.Log("Couldn't find Player with ID" + playerID.ToString());
		}
		this.Setup(player);
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x00066B98 File Offset: 0x00064D98
	public void Setup(PlayerControl player)
	{
		if (player == null)
		{
			return;
		}
		this.PlayerID = player.view.OwnerActorNr;
		this.Control = player;
		if (this.UsernameText != null)
		{
			this.UsernameText.text = player.GetUsernameText();
		}
		PlayerDB.CoreDisplay core = PlayerDB.GetCore(player.actions.core);
		if (core != null && this.IconImage != null)
		{
			this.IconImage.sprite = (this.UseBigIcon ? core.MajorIcon : core.MinorIcon);
		}
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x00066C29 File Offset: 0x00064E29
	public UIPlayerVoteDisplay()
	{
	}

	// Token: 0x04000E8D RID: 3725
	public Image IconImage;

	// Token: 0x04000E8E RID: 3726
	public TextMeshProUGUI UsernameText;

	// Token: 0x04000E8F RID: 3727
	public bool UseBigIcon;

	// Token: 0x04000E90 RID: 3728
	[NonSerialized]
	public int PlayerID = -1;

	// Token: 0x04000E91 RID: 3729
	[NonSerialized]
	public PlayerControl Control;
}
