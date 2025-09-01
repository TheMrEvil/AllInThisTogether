using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Photon.Pun;
using TMPro;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class NextWaveUI : MonoBehaviour
{
	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0005FD64 File Offset: 0x0005DF64
	private static bool ShouldShow
	{
		get
		{
			return !(GameplayManager.instance == null) && (!RewardManager.ShouldSkipFontSelection || PlayerControl.PlayerCount > 1) && (GameplayManager.instance.CurrentState == GameState.Reward_Fountain && PanelManager.CurPanel == PanelType.Augments && !PlayerChoicePanel.IsSelecting) && !PlayerChoicePanel.InApplySequence;
		}
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x0005FDB8 File Offset: 0x0005DFB8
	private void Update()
	{
		this.CanvasGroup.UpdateOpacity(NextWaveUI.ShouldShow, 2f, false);
		if (!NextWaveUI.ShouldShow)
		{
			return;
		}
		if (this.autoTimer > 0f)
		{
			this.autoTimer -= Time.deltaTime;
		}
		if (RewardManager.ShouldSkipFontSelection && PlayerControl.PlayerCount > 1 && !RewardManager.instance.ReadyPlayers.Contains(PlayerControl.MyViewID) && this.autoTimer <= 0f)
		{
			this.ButtonInteract();
			AugmentsPanel.TryClose();
			this.autoTimer = 1f;
		}
		this.UpdateButtonText();
		this.UpdateVoteDisplay();
		this.UpdateDetailDisplay();
		if (InputManager.IsUsingController && InputManager.UIAct.UISecondary.WasPressed)
		{
			this.ButtonInteract();
		}
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x0005FE7C File Offset: 0x0005E07C
	private void UpdateButtonText()
	{
		bool flag = RewardManager.instance.ReadyPlayers.Contains(PlayerControl.MyViewID);
		string text = "Continue";
		if (flag)
		{
			text = "Wait!";
		}
		if (this.ButtonLabel.text != text)
		{
			this.ButtonLabel.text = text;
		}
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x0005FECC File Offset: 0x0005E0CC
	private void UpdateVoteDisplay()
	{
		this.VoteGroup.alpha = (float)((PhotonNetwork.CurrentRoom.PlayerCount <= 1) ? 0 : 1);
		if (PhotonNetwork.CurrentRoom.PlayerCount <= 1)
		{
			return;
		}
		if (this.voteDisplayT > 0f)
		{
			this.voteDisplayT -= Time.deltaTime;
			return;
		}
		this.voteDisplayT = 0.33f;
		this.ClearVotes();
		List<int> readyPlayers = RewardManager.instance.ReadyPlayers;
		if (readyPlayers.Contains(PlayerControl.MyViewID))
		{
			this.AddVoteDisplay(PlayerControl.myInstance);
		}
		foreach (int viewID in (from v in readyPlayers
		where v != PlayerControl.MyViewID
		select v).ToList<int>())
		{
			this.AddVoteDisplay(PlayerControl.GetPlayerFromViewID(viewID));
		}
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x0005FFC8 File Offset: 0x0005E1C8
	private void ClearVotes()
	{
		foreach (UIPlayerVoteDisplay uiplayerVoteDisplay in this.voteDisplays)
		{
			UnityEngine.Object.Destroy(uiplayerVoteDisplay.gameObject);
		}
		this.voteDisplays.Clear();
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x00060028 File Offset: 0x0005E228
	private void AddVoteDisplay(PlayerControl player)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.VoteRef, this.VoteGroup.transform);
		gameObject.SetActive(true);
		UIPlayerVoteDisplay component = gameObject.GetComponent<UIPlayerVoteDisplay>();
		if (player != null)
		{
			component.Setup(player);
		}
		this.voteDisplays.Add(component);
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x00060074 File Offset: 0x0005E274
	private void UpdateDetailDisplay()
	{
		if (PhotonNetwork.CurrentRoom.PlayerCount <= 1)
		{
			this.DetailGroup.alpha = 0f;
			return;
		}
		bool flag = RewardManager.instance.ReadyPlayers.Count > 0;
		this.DetailGroup.UpdateOpacity(flag, 2f, true);
		if (!flag)
		{
			return;
		}
		this.DetailLabel.text = "Continues in " + Mathf.CeilToInt(GameplayManager.instance.Timer).ToString();
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x000600F4 File Offset: 0x0005E2F4
	public void ButtonInteract()
	{
		bool flag = !RewardManager.instance.ReadyPlayers.Contains(PlayerControl.MyViewID) && InkManager.FontPagesOwed <= 0;
		if (NextWaveUI.ShouldShow)
		{
			RewardManager.instance.NextRewardInteraction();
		}
		if (flag)
		{
			AugmentsPanel.TryClose();
		}
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x00060132 File Offset: 0x0005E332
	public NextWaveUI()
	{
	}

	// Token: 0x04000CD9 RID: 3289
	public TextMeshProUGUI ButtonLabel;

	// Token: 0x04000CDA RID: 3290
	public CanvasGroup CanvasGroup;

	// Token: 0x04000CDB RID: 3291
	[Header("Votes")]
	public CanvasGroup VoteGroup;

	// Token: 0x04000CDC RID: 3292
	public GameObject VoteRef;

	// Token: 0x04000CDD RID: 3293
	private List<UIPlayerVoteDisplay> voteDisplays = new List<UIPlayerVoteDisplay>();

	// Token: 0x04000CDE RID: 3294
	[Header("Detail")]
	public CanvasGroup DetailGroup;

	// Token: 0x04000CDF RID: 3295
	public TextMeshProUGUI DetailLabel;

	// Token: 0x04000CE0 RID: 3296
	private float autoTimer;

	// Token: 0x04000CE1 RID: 3297
	private float voteDisplayT;

	// Token: 0x0200054D RID: 1357
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600245D RID: 9309 RVA: 0x000CDEF3 File Offset: 0x000CC0F3
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000CDEFF File Offset: 0x000CC0FF
		public <>c()
		{
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000CDF07 File Offset: 0x000CC107
		internal bool <UpdateVoteDisplay>b__13_0(int v)
		{
			return v != PlayerControl.MyViewID;
		}

		// Token: 0x040026A9 RID: 9897
		public static readonly NextWaveUI.<>c <>9 = new NextWaveUI.<>c();

		// Token: 0x040026AA RID: 9898
		public static Func<int, bool> <>9__13_0;
	}
}
