using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public class AugmentsPanel : MonoBehaviour
{
	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06001310 RID: 4880 RVA: 0x0007576A File Offset: 0x0007396A
	public static int UpgradesAvailable
	{
		get
		{
			return AugmentsPanel.upgradesAvailable.Count;
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x06001311 RID: 4881 RVA: 0x00075778 File Offset: 0x00073978
	public static List<AugmentTree> ChoicesOnDeck
	{
		get
		{
			List<AugmentTree> list = new List<AugmentTree>();
			foreach (AugmentsPanel.UpgradePoint upgradePoint in AugmentsPanel.upgradesAvailable)
			{
				foreach (AugmentTree item in upgradePoint.modifiers)
				{
					list.Add(item);
				}
			}
			return list;
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06001312 RID: 4882 RVA: 0x0007580C File Offset: 0x00073A0C
	private bool CanUnfocusBook
	{
		get
		{
			return PlayerChoicePanel.instance.ShouldShow || FountainStoreUI.wantVisible;
		}
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x00075824 File Offset: 0x00073A24
	private void Awake()
	{
		AugmentsPanel.instance = this;
		AugmentsPanel.upgradesAvailable = new List<AugmentsPanel.UpgradePoint>();
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnOpened));
		component.OnLeftPanel = (Action)Delegate.Combine(component.OnLeftPanel, new Action(this.OnLeftPanel));
		component.OnNextTab = (Action)Delegate.Combine(component.OnNextTab, new Action(this.NextTab));
		component.OnPrevTab = (Action)Delegate.Combine(component.OnPrevTab, new Action(this.PrevTab));
		component.OnTertiaryAction = (Action)Delegate.Combine(component.OnTertiaryAction, new Action(this.TryToggleFocus));
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.GameStateChanged));
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.ControllerSelectDefault));
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x00075930 File Offset: 0x00073B30
	private void Start()
	{
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.SceneChanged));
		PlayerChoicePanel playerChoicePanel = PlayerChoicePanel.instance;
		playerChoicePanel.PlayerScrollChosen = (Action<AugmentTree>)Delegate.Combine(playerChoicePanel.PlayerScrollChosen, new Action<AugmentTree>(AugmentsPanel.PlayerScrollChosen));
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x00075984 File Offset: 0x00073B84
	public static void AwardUpgradeChoice()
	{
		GenreRewardNode genreRewardNode = RewardManager.instance.RewardConfig();
		List<AugmentTree> options = (genreRewardNode == null) ? new List<AugmentTree>() : genreRewardNode.GetPlayerMods();
		if (GameplayManager.IsChallengeActive && GameplayManager.Challenge.FirstPick.Count > 0 && WaveManager.CurrentWave < 0)
		{
			options = GameplayManager.Challenge.FirstPick;
		}
		AugmentsPanel.AwardUpgradeChoice(options);
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x000759E8 File Offset: 0x00073BE8
	public static void AwardUpgradeChoice(AugmentFilter filter)
	{
		List<AugmentTree> validMods = GraphDB.GetValidMods(ModType.Player);
		filter.FilterList(validMods, PlayerControl.myInstance);
		List<AugmentTree> list = new List<AugmentTree>();
		for (int i = 0; i < 3; i++)
		{
			AugmentTree augmentTree = GraphDB.ChooseModFromList(ModType.Player, validMods, false, GameplayManager.IsChallengeActive);
			if (augmentTree != null)
			{
				list.Add(augmentTree);
				validMods.Remove(augmentTree);
			}
		}
		if (list.Count > 0)
		{
			AugmentsPanel.AwardUpgradeChoice(list);
		}
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x00075A50 File Offset: 0x00073C50
	public static void AwardUpgradeChoice(List<AugmentTree> options)
	{
		if (options.Count == 0)
		{
			Debug.LogError("No modifiers provided for player - skipping reward choice");
			return;
		}
		AudioManager.PlaySFX2D(AugmentsPanel.instance.PlayerChoiceAwarded.GetRandomClip(-1), 0.25f, 0.1f);
		AugmentsPanel.UpgradePoint upgradePoint = new AugmentsPanel.UpgradePoint();
		upgradePoint.modifiers = options;
		AugmentsPanel.upgradesAvailable.Add(upgradePoint);
		PlayerChoicePanel.ChoiceTotal++;
		AugmentsPanel.instance.TryNextUpgrade();
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x00075AC0 File Offset: 0x00073CC0
	public static void TryToggle()
	{
		if (VoteManager.IsVoting && VoteManager.CurrentVote == ChoiceType.Bindings)
		{
			BindingsPanel.instance.ToggleUI();
			return;
		}
		if (VoteManager.IsVoting && VoteManager.CurrentVote == ChoiceType.EnemyScroll && PanelManager.CurPanel == PanelType.GameInvisible)
		{
			EnemySelectionPanel.instance.GoToUI();
			return;
		}
		if (PanelManager.CurPanel != PanelType.Augments)
		{
			AugmentsPanel.TryOpen();
			return;
		}
		AugmentsPanel.TryClose();
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x00075B20 File Offset: 0x00073D20
	public static void TryClose()
	{
		if (PanelManager.CurPanel != PanelType.Augments)
		{
			return;
		}
		if (FountainStoreUI.wantVisible && !RewardManager.instance.ReadyPlayers.Contains(PlayerControl.MyViewID))
		{
			return;
		}
		EnemySelectionPanel.IsLoadingScrolls = false;
		PlayerChoicePanel.instance.StopSequences();
		PanelManager.instance.PopPanel();
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x00075B70 File Offset: 0x00073D70
	public static void TryOpen()
	{
		if (PanelManager.CurPanel == PanelType.Augments)
		{
			return;
		}
		if (PanelManager.CurPanel != PanelType.GameInvisible && PanelManager.CurPanel != PanelType.EnemyAugments)
		{
			return;
		}
		if (PanelManager.CurPanel == PanelType.EnemyAugments && RaidManager.IsInRaid)
		{
			return;
		}
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		if (GameplayManager.CurState == GameState.Reward_PreEnemy)
		{
			return;
		}
		PanelManager.instance.PushPanel(PanelType.Augments);
		if (MapManager.InLobbyScene)
		{
			return;
		}
		if (PlayerChoicePanel.instance.HasChoices)
		{
			PlayerChoicePanel.instance.Open();
			return;
		}
		if (AugmentsPanel.upgradesAvailable.Count > 0 && !PlayerControl.myInstance.IsDead)
		{
			AugmentsPanel.NextUpgrade();
		}
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x00075C0C File Offset: 0x00073E0C
	public bool TryNextUpgrade()
	{
		if (PanelManager.CurPanel != PanelType.Augments)
		{
			if (PanelManager.CurPanel != PanelType.GameInvisible)
			{
				return false;
			}
			AugmentsPanel.TryToggle();
		}
		if (AugmentsPanel.upgradesAvailable.Count == 0 || PlayerControl.myInstance.IsDead)
		{
			return false;
		}
		if (PlayerChoicePanel.IsSelecting || PlayerChoicePanel.InApplySequence)
		{
			return false;
		}
		if (VoteManager.IsVoting || VoteManager.CurrentState == VoteState.VotePrepared)
		{
			return false;
		}
		AugmentsPanel.NextUpgrade();
		return true;
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x00075C74 File Offset: 0x00073E74
	private static void NextUpgrade()
	{
		AugmentsPanel.UpgradePoint upgradePoint = AugmentsPanel.upgradesAvailable[0];
		AugmentsPanel.upgradesAvailable.RemoveAt(0);
		AugmentsPanel.instance.SelectTab(AugmentsPanel.BookTab.Players, false);
		PlayerChoicePanel.LoadPlayerScrolls(upgradePoint.modifiers);
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x00075CA4 File Offset: 0x00073EA4
	private static void PlayerScrollChosen(AugmentTree augment)
	{
		if (GameplayManager.CurState == GameState.Vignette_PreWait && AugmentsPanel.UpgradesAvailable <= 0)
		{
			GoalManager.instance.PlayerPageRewardSelected();
		}
		if (GameplayManager.CurState == GameState.Reward_FontPages)
		{
			InkManager.instance.RewardPagePicked();
		}
		if (RaidManager.IsInRaid)
		{
			RaidManager.instance.CheckLocalReadyToContinueNextEncounter();
			UnityMainThreadDispatcher.Instance().Invoke(new Action(AugmentsPanel.TryClose), 1f);
		}
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x00075D0B File Offset: 0x00073F0B
	private void OpenFountainTab()
	{
		if (PanelManager.CurPanel == PanelType.Augments && InkManager.Store.Count > 0 && WaveManager.instance.WavesCompleted > 0)
		{
			UITutorial.TryTutorial(UITutorial.Tutorial.Fountain);
			this.SelectTab(AugmentsPanel.BookTab.Fountain, false);
		}
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x00075D40 File Offset: 0x00073F40
	private void ActivateTabs()
	{
		this.SelectTab(this.GetOpenTab(), true);
		this.TabsActive = 1;
		foreach (AugmentsTabElement augmentsTabElement in this.Tabs)
		{
			if (augmentsTabElement.Tab == AugmentsPanel.BookTab.Fountain)
			{
				bool flag = !TutorialManager.InTutorial;
				flag &= !MapManager.InLobbyScene;
				augmentsTabElement.gameObject.SetActive(flag);
				this.TabsActive += (flag ? 1 : 0);
			}
			else if (augmentsTabElement.Tab == AugmentsPanel.BookTab.Map)
			{
				bool flag = !MapManager.InLobbyScene;
				augmentsTabElement.gameObject.SetActive(flag);
				this.TabsActive += (flag ? 1 : 0);
			}
			else if (augmentsTabElement.Tab == AugmentsPanel.BookTab.Bindings)
			{
				bool flag = GameplayManager.BindingLevel > 0;
				augmentsTabElement.gameObject.SetActive(flag);
				this.BindingNumberTabText.text = GameplayManager.BindingLevel.ToString();
				this.TabsActive += (flag ? 1 : 0);
			}
		}
		foreach (GameObject gameObject in this.NavTabs)
		{
			gameObject.SetActive(this.TabsActive > 1 && InputManager.IsUsingController);
		}
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x00075EB8 File Offset: 0x000740B8
	private void NextTab()
	{
		Debug.Log("Next Tab");
		int num = this.CurrentTabIndex();
		bool flag = false;
		int num2 = 0;
		while ((!flag || !this.Tabs[num].gameObject.activeSelf) && num2 < 5)
		{
			flag = true;
			num2++;
			num++;
			if (num >= this.Tabs.Count)
			{
				num = 0;
			}
		}
		this.SelectTab(this.Tabs[num].Tab, false);
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x00075F30 File Offset: 0x00074130
	private void PrevTab()
	{
		Debug.Log("Prev Tab");
		int num = this.CurrentTabIndex();
		bool flag = false;
		int num2 = 0;
		while ((!flag || !this.Tabs[num].gameObject.activeSelf) && num2 < 5)
		{
			flag = true;
			num2++;
			num--;
			if (num < 0)
			{
				num = this.Tabs.Count - 1;
			}
		}
		this.SelectTab(this.Tabs[num].Tab, false);
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x00075FA8 File Offset: 0x000741A8
	private int CurrentTabIndex()
	{
		for (int i = 0; i < this.Tabs.Count; i++)
		{
			if (this.Tabs[i].Tab == this.CurTab)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x00075FE8 File Offset: 0x000741E8
	public void SelectTab(AugmentsPanel.BookTab tab, bool force = false)
	{
		if (this.CurTab == tab && !force)
		{
			return;
		}
		this.CurTab = tab;
		foreach (AugmentsTabElement augmentsTabElement in this.Tabs)
		{
			if (augmentsTabElement.Tab == this.CurTab)
			{
				augmentsTabElement.Select();
			}
			else
			{
				augmentsTabElement.Release();
			}
		}
		this.SetBookControllerSelection();
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x0007606C File Offset: 0x0007426C
	private AugmentsPanel.BookTab GetOpenTab()
	{
		if (GameplayManager.CurState == GameState.Reward_Fountain && FountainStoreUI.wantVisible)
		{
			return AugmentsPanel.BookTab.Fountain;
		}
		return AugmentsPanel.BookTab.Players;
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x00076080 File Offset: 0x00074280
	private void OnOpened()
	{
		this.PlayerPage.SetPlayer(PlayerControl.myInstance);
		this.Refresh();
		this.ActivateTabs();
		this.OnControllerSelected();
		if (MapManager.InLobbyScene)
		{
			this.ProgressDisplay.gameObject.SetActive(false);
			return;
		}
		this.ProgressDisplay.gameObject.SetActive(true);
		this.ProgressDisplay.Refresh();
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x000760E4 File Offset: 0x000742E4
	private void OnLeftPanel()
	{
		PlayerChoicePanel.instance.StopSequences();
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x000760F0 File Offset: 0x000742F0
	public void Refresh()
	{
		if (PanelManager.CurPanel != PanelType.Augments)
		{
			return;
		}
		this.SetupAugments();
		if (FountainStoreUI.wantVisible)
		{
			FountainStoreUI.instance.Refresh();
		}
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x00076113 File Offset: 0x00074313
	public void GameAugmentsChanged()
	{
		if (PanelManager.CurPanel != PanelType.Augments)
		{
			return;
		}
		this.SetupAugments();
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x00076128 File Offset: 0x00074328
	private void SetupAugments()
	{
		this.FountainPage.Setup();
		this.PlayerPage.Refresh();
		this.MapPage.Setup();
		this.BindingPage.Setup();
		this.BindingNumberTabText.text = GameplayManager.BindingLevel.ToString();
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x00076179 File Offset: 0x00074379
	private void SceneChanged()
	{
		AugmentsPanel.upgradesAvailable.Clear();
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x00076188 File Offset: 0x00074388
	private void GameStateChanged(GameState from, GameState to)
	{
		if (from == GameState.PostRewards)
		{
			AugmentsPanel.TryClose();
		}
		if (to == GameState.Hub || to == GameState.Hub_Traveling)
		{
			FountainStoreUI.instance.Invalidate();
			AugmentsPanel.upgradesAvailable.Clear();
			PlayerChoicePanel.instance.Reset();
		}
		if (to == GameState.Reward_Fountain)
		{
			UnityMainThreadDispatcher.Instance().Invoke(new Action(this.OpenFountainTab), 0.5f);
		}
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x000761E4 File Offset: 0x000743E4
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Augments)
		{
			return;
		}
		this.FountainPage.Group.UpdateOpacity(this.CurTab == AugmentsPanel.BookTab.Fountain, 4f, false);
		this.PlayerPage.Group.UpdateOpacity(this.CurTab == AugmentsPanel.BookTab.Players, 4f, false);
		this.MapPage.Group.UpdateOpacity(this.CurTab == AugmentsPanel.BookTab.Map, 4f, false);
		this.BindingPage.Group.UpdateOpacity(this.CurTab == AugmentsPanel.BookTab.Bindings, 4f, false);
		switch (this.CurTab)
		{
		case AugmentsPanel.BookTab.Map:
			this.MapPage.TickUpdate();
			break;
		case AugmentsPanel.BookTab.Players:
			this.PlayerPage.TickUpdate();
			break;
		case AugmentsPanel.BookTab.Fountain:
			this.FountainPage.TickUpdate();
			break;
		case AugmentsPanel.BookTab.Bindings:
			this.BindingPage.TickUpdate();
			break;
		}
		this.UpdateControllerValues();
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x000762CE File Offset: 0x000744CE
	private void OnControllerSelected()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		this.IsOnBookSelection = !this.CanUnfocusBook;
		this.TryToggleFocus();
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000762F0 File Offset: 0x000744F0
	private void UpdateControllerValues()
	{
		this.FocusDisplay.gameObject.SetActive(InputManager.IsUsingController && (GameplayManager.CurState == GameState.Reward_Fountain || PlayerChoicePanel.instance.ShouldShow));
		if (!InputManager.IsUsingController)
		{
			return;
		}
		this.FocusDisplay.SetCurrentFocus(this.IsOnBookSelection);
		if (!this.IsOnBookSelection && GameplayManager.CurState != GameState.Reward_Fountain && !PlayerChoicePanel.instance.ShouldShow)
		{
			this.TryToggleFocus();
		}
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x00076367 File Offset: 0x00074567
	public void TryToggleFocus()
	{
		if (!this.IsOnBookSelection)
		{
			this.IsOnBookSelection = true;
			this.ControllerSelectDefault();
			return;
		}
		if (this.CanUnfocusBook)
		{
			this.IsOnBookSelection = false;
			this.ControllerSelectDefault();
		}
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x00076394 File Offset: 0x00074594
	private void ControllerSelectDefault()
	{
		if (PanelManager.CurPanel != PanelType.Augments)
		{
			return;
		}
		foreach (GameObject gameObject in this.NavTabs)
		{
			gameObject.SetActive(this.TabsActive > 1 && InputManager.IsUsingController);
		}
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.IsOnBookSelection)
		{
			this.SetBookControllerSelection();
			return;
		}
		if (PlayerChoicePanel.instance.ShouldShow)
		{
			PlayerChoicePanel.instance.SelectDefault();
			return;
		}
		if (FountainStoreUI.wantVisible)
		{
			FountainStoreUI.instance.SelectDefault();
		}
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x00076440 File Offset: 0x00074640
	private void SetBookControllerSelection()
	{
		if (!this.IsOnBookSelection || !InputManager.IsUsingController)
		{
			return;
		}
		switch (this.CurTab)
		{
		case AugmentsPanel.BookTab.Map:
			this.MapPage.SelectDefaultUI();
			return;
		case AugmentsPanel.BookTab.Players:
			this.PlayerPage.SelectDefaultUI();
			return;
		case AugmentsPanel.BookTab.Fountain:
			this.FountainPage.SelectDefaultUI();
			return;
		case AugmentsPanel.BookTab.Bindings:
			this.BindingPage.SelectDefaultUI();
			return;
		default:
			return;
		}
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000764AA File Offset: 0x000746AA
	public AugmentsPanel()
	{
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000764B9 File Offset: 0x000746B9
	// Note: this type is marked as 'beforefieldinit'.
	static AugmentsPanel()
	{
	}

	// Token: 0x0400123D RID: 4669
	public static AugmentsPanel instance;

	// Token: 0x0400123E RID: 4670
	private static List<AugmentsPanel.UpgradePoint> upgradesAvailable = new List<AugmentsPanel.UpgradePoint>();

	// Token: 0x0400123F RID: 4671
	public TomeProgressSmall ProgressDisplay;

	// Token: 0x04001240 RID: 4672
	public AugmentsPanel.BookTab CurTab;

	// Token: 0x04001241 RID: 4673
	public List<AugmentsTabElement> Tabs;

	// Token: 0x04001242 RID: 4674
	public List<GameObject> NavTabs;

	// Token: 0x04001243 RID: 4675
	public ControllerFocusDisplay FocusDisplay;

	// Token: 0x04001244 RID: 4676
	public TextMeshProUGUI BindingNumberTabText;

	// Token: 0x04001245 RID: 4677
	private int TabsActive;

	// Token: 0x04001246 RID: 4678
	public AugmentBook_Fountain FountainPage;

	// Token: 0x04001247 RID: 4679
	public AugmentBook_Player PlayerPage;

	// Token: 0x04001248 RID: 4680
	public AugmentBook_Map MapPage;

	// Token: 0x04001249 RID: 4681
	public AugmentBook_Bindings BindingPage;

	// Token: 0x0400124A RID: 4682
	public GameObject KeywordPrefab;

	// Token: 0x0400124B RID: 4683
	public List<AudioClip> PlayerChoiceAwarded;

	// Token: 0x0400124C RID: 4684
	[NonSerialized]
	public bool IsOnBookSelection = true;

	// Token: 0x02000594 RID: 1428
	public enum BookTab
	{
		// Token: 0x040027BD RID: 10173
		Map,
		// Token: 0x040027BE RID: 10174
		Players,
		// Token: 0x040027BF RID: 10175
		Fountain,
		// Token: 0x040027C0 RID: 10176
		Bindings,
		// Token: 0x040027C1 RID: 10177
		Cosmetic_Head,
		// Token: 0x040027C2 RID: 10178
		Cosmetic_Outfit,
		// Token: 0x040027C3 RID: 10179
		Cosmetic_Book,
		// Token: 0x040027C4 RID: 10180
		Post_Global,
		// Token: 0x040027C5 RID: 10181
		Post_Player,
		// Token: 0x040027C6 RID: 10182
		Tome_Info,
		// Token: 0x040027C7 RID: 10183
		Tome_Progress,
		// Token: 0x040027C8 RID: 10184
		Ability_Generator,
		// Token: 0x040027C9 RID: 10185
		Ability_Spender,
		// Token: 0x040027CA RID: 10186
		Ability_Movement,
		// Token: 0x040027CB RID: 10187
		Cosmetic_Emote,
		// Token: 0x040027CC RID: 10188
		Cosmetic_Back,
		// Token: 0x040027CD RID: 10189
		Cosmetic_Special_2,
		// Token: 0x040027CE RID: 10190
		Nook_Furniture,
		// Token: 0x040027CF RID: 10191
		Nook_Furnishings,
		// Token: 0x040027D0 RID: 10192
		Nook_Decorations,
		// Token: 0x040027D1 RID: 10193
		Nook_Utility,
		// Token: 0x040027D2 RID: 10194
		Nook_Treasures,
		// Token: 0x040027D3 RID: 10195
		Nook_Knowledge
	}

	// Token: 0x02000595 RID: 1429
	private class UpgradePoint
	{
		// Token: 0x06002578 RID: 9592 RVA: 0x000D175C File Offset: 0x000CF95C
		public UpgradePoint()
		{
		}

		// Token: 0x040027D4 RID: 10196
		public List<AugmentTree> modifiers = new List<AugmentTree>();
	}
}
