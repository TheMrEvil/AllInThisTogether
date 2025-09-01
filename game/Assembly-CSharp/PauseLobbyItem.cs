using System;
using System.Collections.Generic;
using Photon.Pun;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000173 RID: 371
public class PauseLobbyItem : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x06000FE0 RID: 4064 RVA: 0x00064028 File Offset: 0x00062228
	public void Setup(PlayerControl player)
	{
		this.playerRef = player;
		Color color = this.BackgroundDisplay.color;
		if (player == null)
		{
			this.ButtonRef.interactable = false;
			color.a = 0.35f;
			this.BackgroundDisplay.color = color;
			foreach (TextMeshProUGUI textMeshProUGUI in this.NameTexts)
			{
				textMeshProUGUI.text = "";
			}
			this.EmptyText.SetActive(true);
			this.HostIndicator.SetActive(false);
			return;
		}
		this.ButtonRef.interactable = true;
		string usernameText = this.playerRef.GetUsernameText();
		foreach (TextMeshProUGUI textMeshProUGUI2 in this.NameTexts)
		{
			textMeshProUGUI2.text = usernameText;
		}
		this.HostIndicator.SetActive(this.playerRef.view.Owner.IsMasterClient);
		this.EmptyText.SetActive(false);
		color.a = 1f;
		this.BackgroundDisplay.color = color;
		this.KickButton.SetActive(false);
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x00064180 File Offset: 0x00062380
	public void OnClick()
	{
		string userID = this.playerRef.UserID;
		ulong ulSteamID;
		if (userID.StartsWith("STEAM_") && ulong.TryParse(userID.Replace("STEAM_", ""), out ulSteamID))
		{
			CSteamID steamID = new CSteamID(ulSteamID);
			SteamFriends.ActivateGameOverlayToUser("steamid", steamID);
		}
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x000641D2 File Offset: 0x000623D2
	public void OnSelect(BaseEventData ev)
	{
		if (this.playerRef.IsMine)
		{
			return;
		}
		this.KickButton.SetActive(PhotonNetwork.IsMasterClient);
		PauseLobbyControl.instance.SelectPlayerItem(this);
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x000641FD File Offset: 0x000623FD
	public void OnDeselect(BaseEventData ev)
	{
		this.KickButton.SetActive(false);
		PauseLobbyControl.instance.DeselectPlayerItem(this);
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00064216 File Offset: 0x00062416
	public void TryKick()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		StateManager.instance.KickPlayer(this.playerRef);
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x00064230 File Offset: 0x00062430
	public PauseLobbyItem()
	{
	}

	// Token: 0x04000DFA RID: 3578
	public Image BackgroundDisplay;

	// Token: 0x04000DFB RID: 3579
	public List<TextMeshProUGUI> NameTexts;

	// Token: 0x04000DFC RID: 3580
	public GameObject EmptyText;

	// Token: 0x04000DFD RID: 3581
	public Button ButtonRef;

	// Token: 0x04000DFE RID: 3582
	public GameObject HostIndicator;

	// Token: 0x04000DFF RID: 3583
	[Header("Kick")]
	public GameObject KickButton;

	// Token: 0x04000E00 RID: 3584
	public Image KickFill;

	// Token: 0x04000E01 RID: 3585
	private PlayerControl playerRef;
}
