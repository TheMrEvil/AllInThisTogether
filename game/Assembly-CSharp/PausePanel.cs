using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E9 RID: 489
public class PausePanel : MonoBehaviour
{
	// Token: 0x17000165 RID: 357
	// (get) Token: 0x060014AD RID: 5293 RVA: 0x000811E4 File Offset: 0x0007F3E4
	public static bool IsGamePaused
	{
		get
		{
			return PhotonNetwork.OfflineMode && (RaidManager.IsInRaid || GameplayManager.CurState == GameState.InWave || GameplayManager.CurState == GameState.Vignette_Inside) && (!RaidManager.IsInRaid || RaidManager.IsEncounterStarted) && (PanelManager.CurPanel == PanelType.Pause || PanelManager.CurPanel == PanelType.Augments || PanelManager.CurPanel == PanelType.Settings);
		}
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x060014AE RID: 5294 RVA: 0x0008123F File Offset: 0x0007F43F
	public static bool IsInMPRoom
	{
		get
		{
			return !PhotonNetwork.OfflineMode && !TutorialManager.InTutorial;
		}
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x060014AF RID: 5295 RVA: 0x00081252 File Offset: 0x0007F452
	private bool CanEndGame
	{
		get
		{
			return GameplayManager.CurState != GameState.Post_Traveling && GameplayManager.CurState != GameState.Hub_Traveling && GameplayManager.CurState != GameState.Vignette_Traveling && GameplayManager.CurState != GameState.Reward_Traveling && !VoteManager.IsVoting;
		}
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x00081284 File Offset: 0x0007F484
	private void Awake()
	{
		PausePanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnteredPanel));
		UIPanel component2 = base.GetComponent<UIPanel>();
		component2.OnLeftPanel = (Action)Delegate.Combine(component2.OnLeftPanel, new Action(this.Resume));
		NetworkManager.OnRoomPlayersChanged = (Action)Delegate.Combine(NetworkManager.OnRoomPlayersChanged, new Action(this.PlayersChanged));
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x00081308 File Offset: 0x0007F508
	private void OnEnteredPanel()
	{
		string text = NetworkManager.RoomCode;
		if (Settings.GetBool(SystemSetting.HideRoomCode, false))
		{
			text = "*****";
		}
		if (PausePanel.IsInMPRoom)
		{
			this.CodeGroup.SetActive(true);
			this.LobbyControl.Setup();
			this.GameCode.text = text;
			this.InviteButtonInternal.interactable = !RaidManager.IsInRaid;
		}
		else
		{
			this.CodeGroup.SetActive(false);
		}
		this.EndButton.gameObject.SetActive(GameplayManager.IsInGame && !TutorialManager.InTutorial);
		this.EndButton.ButtonRef.interactable = this.CanEndGame;
		this.LeaveButton.gameObject.SetActive(!GameplayManager.IsInGame || TutorialManager.InTutorial);
		this.Quotes.SetupQuote(InkManager.instance.PauseQuoteID);
		this.InviteButton.SetActive(PausePanel.IsInMPRoom);
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000813F4 File Offset: 0x0007F5F4
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Pause)
		{
			return;
		}
		this.UnstuckButton.TickUpdate();
		this.EndButton.TickUpdate(PlayerControl.PlayerCount > 1);
		this.QuoteGroup.UpdateOpacity(!this.WantInvite && !PausePanel.IsInMPRoom, 3f, false);
		this.LobbyInfo.UpdateOpacity(!this.WantInvite && PausePanel.IsInMPRoom, 3f, false);
		this.FriendsGroup.UpdateOpacity(this.WantInvite, 3f, false);
		this.CopyGroup.alpha = Mathf.Max(0f, this.CopyGroup.alpha - Time.deltaTime * 1.5f);
		if (!this.WantInvite && PausePanel.IsInMPRoom)
		{
			PauseLobbyControl.instance.OnUpdate();
		}
		if (this.leaveAttemptT > 0f)
		{
			this.leaveAttemptT -= Time.unscaledDeltaTime;
		}
		if (this.WantInvite)
		{
			this.t += Time.deltaTime;
			if (this.t >= 1f)
			{
				this.t = 0f;
				this.Friends.UpdateValues();
			}
			this.Friends.TickUpdate();
		}
		this.EndHoldDisplay.SetActive(PlayerControl.PlayerCount > 1);
		int count = VoteManager.EndRunVotes.Count;
		for (int i = 0; i < this.EndVoteDisplays.Count; i++)
		{
			bool active = this.CanEndGame && PlayerControl.PlayerCount > i && count > 0;
			bool active2 = count > i;
			this.EndVoteDisplays[i].SetActive(active);
			this.EndVoteChecks[i].SetActive(active2);
		}
		if (InputManager.IsUsingController)
		{
			if (InputManager.UIAct.UIBack.WasPressed)
			{
				this.Resume();
			}
			if (UISelector.instance.CurrentSelection == this.UnstuckButton.ButtonRef)
			{
				if (InputManager.UIAct.UIPrimary.WasPressed)
				{
					this.UnstuckButton.ButtonDown();
				}
				if (InputManager.UIAct.UIPrimary.WasReleased)
				{
					this.UnstuckButton.ButtonUp();
				}
			}
			else
			{
				this.UnstuckButton.ButtonUp();
			}
			if (UISelector.instance.CurrentSelection == this.EndButton.ButtonRef)
			{
				if (InputManager.UIAct.UIPrimary.WasPressed)
				{
					this.EndButton.ButtonDown();
				}
				if (InputManager.UIAct.UIPrimary.WasReleased)
				{
					this.EndButton.ButtonUp();
					return;
				}
			}
			else
			{
				this.EndButton.ButtonUp();
			}
		}
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x00081687 File Offset: 0x0007F887
	public void Resume()
	{
		CanvasController.Resume();
		this.WantInvite = false;
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x00081695 File Offset: 0x0007F895
	public void Invite()
	{
		this.WantInvite = true;
		this.Friends.Refresh();
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x000816AC File Offset: 0x0007F8AC
	public void VoteToLeave()
	{
		if (!this.CanEndGame || this.leaveAttemptT > 0f)
		{
			return;
		}
		if (PlayerControl.PlayerCount > 1)
		{
			VoteManager.instance.ToggleWantToLeaveVote();
		}
		else
		{
			VoteManager.instance.CancelRunAndReturnToLibrary();
		}
		this.leaveAttemptT = 0.33f;
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x000816F8 File Offset: 0x0007F8F8
	public void LeaveGame()
	{
		if (GameplayManager.IsInGame)
		{
			if (!RaidManager.IsInRaid)
			{
				GameRecord.UploadQuit();
			}
			else if (!RaidManager.PlayerLeftEncounter)
			{
				RaidRecord.UploadResult(RaidRecord.Result.Quit, RaidManager.instance.currentEncounterTime);
			}
		}
		NetworkManager.instance.TryLeaveRoom();
		if (TutorialManager.InTutorial)
		{
			Analytics.TutorialQuit((int)TutorialManager.CurrentStep, GameplayManager.instance.GameTime);
		}
		this.WantInvite = false;
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x0008175D File Offset: 0x0007F95D
	public void CopyRoomCode()
	{
		GUIUtility.systemCopyBuffer = NetworkManager.RoomCode;
		this.CopyGroup.alpha = 1f;
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x00081779 File Offset: 0x0007F979
	public void GoToSettings()
	{
		SettingsPanel.instance.GoToSettings();
		this.WantInvite = false;
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x0008178C File Offset: 0x0007F98C
	private void PlayersChanged()
	{
		if (PanelManager.CurPanel != PanelType.Pause)
		{
			return;
		}
		PauseLobbyControl.instance.Setup();
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x000817A1 File Offset: 0x0007F9A1
	public PausePanel()
	{
	}

	// Token: 0x040013F1 RID: 5105
	public static PausePanel instance;

	// Token: 0x040013F2 RID: 5106
	public GameObject CodeGroup;

	// Token: 0x040013F3 RID: 5107
	public TextMeshProUGUI GameCode;

	// Token: 0x040013F4 RID: 5108
	public CanvasGroup CopyGroup;

	// Token: 0x040013F5 RID: 5109
	public GameObject InviteButton;

	// Token: 0x040013F6 RID: 5110
	public Button InviteButtonInternal;

	// Token: 0x040013F7 RID: 5111
	public EndRunButton EndButton;

	// Token: 0x040013F8 RID: 5112
	public GameObject EndHoldDisplay;

	// Token: 0x040013F9 RID: 5113
	public List<GameObject> EndVoteDisplays;

	// Token: 0x040013FA RID: 5114
	public List<GameObject> EndVoteChecks;

	// Token: 0x040013FB RID: 5115
	public GameObject LeaveButton;

	// Token: 0x040013FC RID: 5116
	public UnstuckButton UnstuckButton;

	// Token: 0x040013FD RID: 5117
	[Header("Right Side")]
	public CanvasGroup QuoteGroup;

	// Token: 0x040013FE RID: 5118
	public CanvasGroup LobbyInfo;

	// Token: 0x040013FF RID: 5119
	public CanvasGroup FriendsGroup;

	// Token: 0x04001400 RID: 5120
	public PauseQuotes Quotes;

	// Token: 0x04001401 RID: 5121
	public FriendList Friends;

	// Token: 0x04001402 RID: 5122
	public PauseLobbyControl LobbyControl;

	// Token: 0x04001403 RID: 5123
	private bool WantInvite;

	// Token: 0x04001404 RID: 5124
	private float t;

	// Token: 0x04001405 RID: 5125
	private float leaveAttemptT;
}
