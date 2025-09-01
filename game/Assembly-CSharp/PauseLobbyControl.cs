using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class PauseLobbyControl : MonoBehaviour
{
	// Token: 0x06000FD6 RID: 4054 RVA: 0x00063CED File Offset: 0x00061EED
	private void Awake()
	{
		PauseLobbyControl.instance = this;
		NetworkManager.OnRoomPlayersChanged = (Action)Delegate.Combine(NetworkManager.OnRoomPlayersChanged, new Action(this.OnPlayersChanged));
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x00063D18 File Offset: 0x00061F18
	public void Setup()
	{
		this.CurSelPlayer = null;
		List<PlayerControl> allPlayers = PlayerControl.AllPlayers;
		for (int i = 0; i < this.PlayerItems.Count; i++)
		{
			if (i >= allPlayers.Count)
			{
				this.PlayerItems[i].Setup(null);
			}
			else
			{
				this.PlayerItems[i].Setup(allPlayers[i]);
			}
		}
		this.kickTimer = 0f;
		this.KickToggle.gameObject.SetActive(PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient);
		this.OpenRoomToggle.gameObject.SetActive(PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient);
		this.OnRoomSettingChanged();
		if (InputManager.IsUsingController)
		{
			bool flag = false;
			using (List<PauseLobbyItem>.Enumerator enumerator = this.PlayerItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ButtonRef == UISelector.instance.CurrentSelection)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				UISelector.SelectSelectable(this.PlayerItems[0].ButtonRef);
			}
		}
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00063E44 File Offset: 0x00062044
	private void OnPlayersChanged()
	{
		if (PanelManager.CurPanel != PanelType.Pause)
		{
			return;
		}
		base.Invoke("Setup", 0.25f);
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x00063E5F File Offset: 0x0006205F
	public void SelectPlayerItem(PauseLobbyItem item)
	{
		this.CurSelPlayer = item;
		this.kickTimer = 0f;
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x00063E73 File Offset: 0x00062073
	public void DeselectPlayerItem(PauseLobbyItem item)
	{
		if (this.CurSelPlayer != item)
		{
			return;
		}
		this.CurSelPlayer = null;
		this.kickTimer = 0f;
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x00063E98 File Offset: 0x00062098
	public void OnUpdate()
	{
		if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (this.CurSelPlayer != null)
		{
			if (InputManager.UIAct.UISecondary.IsPressed)
			{
				this.kickTimer += Time.deltaTime;
			}
			else
			{
				this.kickTimer = 0f;
			}
			if (this.kickTimer >= this.KickHoldTime)
			{
				this.CurSelPlayer.TryKick();
				this.kickTimer = 0f;
			}
		}
		foreach (PauseLobbyItem pauseLobbyItem in this.PlayerItems)
		{
			if (pauseLobbyItem != this.CurSelPlayer)
			{
				pauseLobbyItem.KickFill.fillAmount = 0f;
			}
			else
			{
				pauseLobbyItem.KickFill.fillAmount = this.kickTimer / this.KickHoldTime;
			}
		}
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00063F90 File Offset: 0x00062190
	public void OnRoomSettingChanged()
	{
		if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.KickToggle.ChangeValue(StateManager.GetBool("KickAFK", false), false, false);
		this.OpenRoomToggle.ChangeValue(StateManager.GetBool("public", false), false, false);
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00063FDC File Offset: 0x000621DC
	public void ChangeAFKSetting(bool shouldKick)
	{
		if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		StateManager.SetValue("KickAFK", shouldKick);
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x00063FFD File Offset: 0x000621FD
	public void ChangeRoomOpen(bool isOpen)
	{
		if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		StateManager.SetRoomOpen(isOpen);
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x00064014 File Offset: 0x00062214
	public PauseLobbyControl()
	{
	}

	// Token: 0x04000DF3 RID: 3571
	public static PauseLobbyControl instance;

	// Token: 0x04000DF4 RID: 3572
	public List<PauseLobbyItem> PlayerItems;

	// Token: 0x04000DF5 RID: 3573
	public ToggleSetting KickToggle;

	// Token: 0x04000DF6 RID: 3574
	public ToggleSetting OpenRoomToggle;

	// Token: 0x04000DF7 RID: 3575
	private PauseLobbyItem CurSelPlayer;

	// Token: 0x04000DF8 RID: 3576
	public float KickHoldTime = 2f;

	// Token: 0x04000DF9 RID: 3577
	private float kickTimer;
}
