using System;
using Photon.Pun;
using UnityEngine;

// Token: 0x020001B1 RID: 433
public class GameplayUI : MonoBehaviour
{
	// Token: 0x1700014A RID: 330
	// (get) Token: 0x060011EE RID: 4590 RVA: 0x0006F360 File Offset: 0x0006D560
	private bool WantVisible
	{
		get
		{
			if (!GameplayUI.InGame)
			{
				return false;
			}
			if (PanelManager.CurPanel != PanelType.GameInvisible)
			{
				return PanelManager.CurPanel == PanelType.Augments || PanelManager.CurPanel == PanelType.Pause;
			}
			if (GameHUD.Mode == GameHUD.HUDMode.Off)
			{
				return false;
			}
			if (RaidManager.IsInRaid)
			{
				if (RaidManager.InEncounterIntro)
				{
					return false;
				}
				if (!MapManager.InLobbyScene)
				{
					return true;
				}
			}
			return !SignatureInkUIControl.MyPlayerPrestiging && GameplayManager.CurState != GameState.Hub_Traveling && GameplayManager.CurState != GameState.Pregame && GameplayManager.CurState != GameState.Reward_Map && GameplayManager.CurState != GameState.Reward_Traveling && (!TutorialManager.InTutorial || !(LibraryManager.instance != null));
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x060011EF RID: 4591 RVA: 0x0006F3F9 File Offset: 0x0006D5F9
	public static bool InGame
	{
		get
		{
			return PhotonNetwork.InRoom;
		}
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x0006F400 File Offset: 0x0006D600
	private void Awake()
	{
		GameplayUI.instance = this;
		GameplayUI.CurrentState = GameUIState.Base;
		UIPanel gamePanel = this.GamePanel;
		gamePanel.OnEnteredPanel = (Action)Delegate.Combine(gamePanel.OnEnteredPanel, new Action(this.OnEnteredGameUI));
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x0006F435 File Offset: 0x0006D635
	private void OnEnteredGameUI()
	{
		GameplayUI.InputLockoutTimer = 0.33f;
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x0006F444 File Offset: 0x0006D644
	private void Update()
	{
		this.UpdateGroup(this.WantVisible, this.HUDGroup);
		if (GameplayUI.InputLockoutTimer > 0f)
		{
			GameplayUI.InputLockoutTimer -= Time.deltaTime;
		}
		this.GameStartDisplay.CGroup.UpdateOpacity(this.GameStartDisplay.IsInStart, 3f, true);
		if (this.GameStartDisplay.IsInStart)
		{
			this.GameStartDisplay.TickUpdate();
		}
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x0006F4B8 File Offset: 0x0006D6B8
	private void UpdateGroup(bool shouldShow, CanvasGroup g)
	{
		g.interactable = shouldShow;
		g.blocksRaycasts = shouldShow;
		if (shouldShow && g.alpha < 1f)
		{
			g.alpha += Time.unscaledDeltaTime * 3f;
			return;
		}
		if (!shouldShow && g.alpha > 0f)
		{
			g.alpha -= Time.unscaledDeltaTime * 3f;
		}
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x0006F524 File Offset: 0x0006D724
	public static void SetUIState(GameUIState state)
	{
		GameplayUI.CurrentState = state;
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x0006F52C File Offset: 0x0006D72C
	public GameplayUI()
	{
	}

	// Token: 0x040010AA RID: 4266
	public static GameplayUI instance;

	// Token: 0x040010AB RID: 4267
	public CanvasGroup HUDGroup;

	// Token: 0x040010AC RID: 4268
	public static GameUIState CurrentState;

	// Token: 0x040010AD RID: 4269
	public UIPanel GamePanel;

	// Token: 0x040010AE RID: 4270
	public Augments_GameStartInfo GameStartDisplay;

	// Token: 0x040010AF RID: 4271
	public static float InputLockoutTimer;
}
