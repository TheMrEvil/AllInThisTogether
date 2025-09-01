using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using InControl;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EA RID: 490
public class PostGamePanel : MonoBehaviour
{
	// Token: 0x060014BB RID: 5307 RVA: 0x000817AC File Offset: 0x0007F9AC
	private void Awake()
	{
		PostGamePanel.instance = this;
		this.panel = base.GetComponent<UIPanel>();
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.GameStateChanged));
		VoteManager.OnVotesChanged = (Action<ChoiceType>)Delegate.Combine(VoteManager.OnVotesChanged, new Action<ChoiceType>(this.OnVotesChanged));
		global::InputManager.OnInputMethodChanged = (Action)Delegate.Combine(global::InputManager.OnInputMethodChanged, new Action(this.OnInputChanged));
		UIPanel uipanel = this.panel;
		uipanel.OnNextTab = (Action)Delegate.Combine(uipanel.OnNextTab, new Action(this.SwapTab));
		UIPanel uipanel2 = this.panel;
		uipanel2.OnPrevTab = (Action)Delegate.Combine(uipanel2.OnPrevTab, new Action(this.SwapTab));
		UIPanel uipanel3 = this.panel;
		uipanel3.OnPageNext = (Action)Delegate.Combine(uipanel3.OnPageNext, new Action(this.NextPlayer));
		UIPanel uipanel4 = this.panel;
		uipanel4.OnPagePrev = (Action)Delegate.Combine(uipanel4.OnPagePrev, new Action(this.PrevPlayer));
		UIPanel uipanel5 = this.panel;
		uipanel5.OnSecondaryAction = (Action)Delegate.Combine(uipanel5.OnSecondaryAction, new Action(this.SecondaryAction));
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000818EE File Offset: 0x0007FAEE
	public void DebugEnter()
	{
		UnityEngine.Debug.Log("Saving");
		this.SaveRaidToFile();
	}

	// Token: 0x060014BD RID: 5309 RVA: 0x00081900 File Offset: 0x0007FB00
	public void DebugExit()
	{
		if (PanelManager.CurPanel == PanelType.PostGame)
		{
			PanelManager.instance.PopPanel();
		}
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x00081918 File Offset: 0x0007FB18
	public void Ended(bool wasSuccess)
	{
		this.openedFromCodex = false;
		this.waitDelay = 3f;
		this.SetupCoreElements(wasSuccess);
		PostGame_CurrencyRewards.instance.Setup(wasSuccess);
		PanelManager.instance.PushPanel(PanelType.PostGame);
		this.ReturnButtonText.text = "Library";
		this.SelectTab(AugmentsPanel.BookTab.Post_Player, true);
		this.BindingAttuneDisplay.gameObject.SetActive(Progression.NewAttumnentLevel);
		if (Progression.NewAttumnentLevel)
		{
			this.BindingAttuneText.text = Progression.BindingAttunement.ToString();
		}
		this.didSaveFile = false;
		base.Invoke("SaveToFile", 2.5f);
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x000819B8 File Offset: 0x0007FBB8
	public void EndedRaid(bool wasSuccess)
	{
		this.openedFromCodex = false;
		this.waitDelay = 3f;
		RaidDB.Raid raid = RaidDB.GetRaid(RaidManager.instance.CurrentRaid);
		this.SetupCoreElementsRaid(wasSuccess, raid);
		PostGame_CurrencyRewards.instance.SetupRaid(wasSuccess);
		PanelManager.instance.PushPanel(PanelType.PostGame);
		this.ReturnButtonText.text = "Library";
		this.SelectTab(AugmentsPanel.BookTab.Post_Player, true);
		this.BindingAttuneDisplay.gameObject.SetActive(false);
		this.didSaveFile = false;
		base.Invoke("SaveRaidToFile", 2.5f);
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x00081A48 File Offset: 0x0007FC48
	public void OpenFromCodex(LocalRunRecord record)
	{
		UnityEngine.Debug.Log("Opening PostGame UI from Codex");
		this.openedFromCodex = true;
		this.codexRecord = record;
		List<PlayerGameStats> list = new List<PlayerGameStats>();
		foreach (LocalRunRecord.PlayerInfo playerInfo in record.OtherPlayerInfo)
		{
			list.Add(playerInfo.Stats);
		}
		record.MyInfo.Stats.PlayerRef = PlayerControl.myInstance;
		list.Add(record.MyInfo.Stats);
		this.PlayerStats = list;
		this.BindingAttuneDisplay.gameObject.SetActive(false);
		if (record.IsRaid)
		{
			this.SetupCoreElementsRaid(record.Won, RaidDB.GetRaid(record.Raid));
		}
		else
		{
			this.SetupCoreElements(record.Won);
		}
		PanelManager.instance.PushPanel(PanelType.PostGame);
		this.SelectTab(AugmentsPanel.BookTab.Post_Player, true);
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x00081B40 File Offset: 0x0007FD40
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.PostGame)
		{
			return;
		}
		if (this.waitDelay > 0f)
		{
			this.waitDelay -= Time.deltaTime;
		}
		CanvasGroup playerGroup = this.PlayerGroup;
		if (playerGroup != null)
		{
			playerGroup.UpdateOpacity(this.CurTab == AugmentsPanel.BookTab.Post_Player, 4f, false);
		}
		CanvasGroup globalGroup = this.GlobalGroup;
		if (globalGroup != null)
		{
			globalGroup.UpdateOpacity(this.CurTab == AugmentsPanel.BookTab.Post_Global, 4f, false);
		}
		if (this.CurTab == AugmentsPanel.BookTab.Players)
		{
			UIActions uiact = global::InputManager.UIAct;
			bool? flag;
			if (uiact == null)
			{
				flag = null;
			}
			else
			{
				PlayerAction tab_Next = uiact.Tab_Next;
				flag = ((tab_Next != null) ? new bool?(tab_Next.WasPressed) : null);
			}
			bool? flag2 = flag;
			if (flag2.GetValueOrDefault())
			{
				this.NextPlayer();
			}
			else
			{
				UIActions uiact2 = global::InputManager.UIAct;
				bool? flag3;
				if (uiact2 == null)
				{
					flag3 = null;
				}
				else
				{
					PlayerAction tab_Prev = uiact2.Tab_Prev;
					flag3 = ((tab_Prev != null) ? new bool?(tab_Prev.WasPressed) : null);
				}
				flag2 = flag3;
				if (flag2.GetValueOrDefault())
				{
					this.PrevPlayer();
				}
			}
		}
		if (global::InputManager.IsUsingController)
		{
			this.AutoScroller.TickUpdate();
		}
		if (this.openedFromCodex && global::InputManager.UIAct.UIBack.WasPressed)
		{
			base.StartCoroutine("GoBack");
		}
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x00081C7F File Offset: 0x0007FE7F
	private IEnumerator GoBack()
	{
		yield return new WaitForEndOfFrame();
		PanelManager.instance.PopPanel();
		yield break;
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x00081C88 File Offset: 0x0007FE88
	private void SetupCoreElements(bool didWin)
	{
		this.wasWin = didWin;
		this.GameResultText.text = (didWin ? "Victory" : "Defeat");
		this.WinDisplay.SetActive(didWin);
		this.LostDisplay.SetActive(!didWin);
		this.GameLengthText.text = (this.openedFromCodex ? TimeSpan.FromSeconds((double)this.codexRecord.Timer).ToString("hh':'mm':'ss") : GameplayManager.TimerText);
		this.TomeIcon.sprite = (this.openedFromCodex ? GraphDB.GetGenre(this.codexRecord.TomeID).Root.Icon : GameplayManager.instance.GameGraph.Root.Icon);
		int num = this.openedFromCodex ? this.codexRecord.Appendix : WaveManager.instance.AppendixLevel;
		int num2 = this.openedFromCodex ? this.codexRecord.Chapter : WaveManager.instance.AppendixChapterNumber;
		this.AppendixGroup.SetActive(num > 0);
		this.AppendixText.text = string.Format("Appendix {0}.{1}", num, num2 - 1);
		this.RaidIcon.gameObject.SetActive(false);
		if (!this.openedFromCodex)
		{
			this.ReturnButtonText.text = "Library";
			this.OnVotesChanged(ChoiceType.ReturnLibrary);
			this.ClearVotes();
			if (PhotonNetwork.IsMasterClient)
			{
				VoteManager.StartVote(ChoiceType.ReturnLibrary);
			}
		}
	}

	// Token: 0x060014C4 RID: 5316 RVA: 0x00081E04 File Offset: 0x00080004
	private void SetupCoreElementsRaid(bool didWin, RaidDB.Raid raid)
	{
		this.wasWin = didWin;
		this.GameResultText.text = (didWin ? "Victory" : "Defeat");
		this.WinDisplay.SetActive(didWin);
		this.LostDisplay.SetActive(!didWin);
		this.GameLengthText.text = (this.openedFromCodex ? TimeSpan.FromSeconds((double)this.codexRecord.Timer).ToString("hh':'mm':'ss") : GameplayManager.TimerText);
		this.AppendixGroup.SetActive(false);
		this.RaidIcon.gameObject.SetActive(true);
		bool flag = this.openedFromCodex ? this.codexRecord.HardMode : (RaidManager.instance.Difficulty == RaidDB.Difficulty.Hard);
		this.RaidIcon.sprite = (flag ? raid.StampHard : raid.StampNormal);
		this.TomeIcon.sprite = (flag ? RaidDB.instance.RaidIconHard : RaidDB.instance.RaidIcon);
		if (!this.openedFromCodex)
		{
			this.ReturnButtonText.text = "Library";
			this.OnVotesChanged(ChoiceType.ReturnLibrary);
			this.ClearVotes();
			if (PhotonNetwork.IsMasterClient)
			{
				VoteManager.StartVote(ChoiceType.ReturnLibrary);
			}
		}
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x00081F37 File Offset: 0x00080137
	private void SetupPlayerStats()
	{
		this.selectedStats = this.GetStats(PlayerControl.myInstance);
		this.RefreshPlayerStats();
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x00081F50 File Offset: 0x00080150
	private void SwapTab()
	{
		AugmentsPanel.BookTab curTab = this.CurTab;
		if (curTab == AugmentsPanel.BookTab.Post_Global)
		{
			this.SelectTab(AugmentsPanel.BookTab.Post_Player, false);
			return;
		}
		if (curTab != AugmentsPanel.BookTab.Post_Player)
		{
			return;
		}
		this.SelectTab(AugmentsPanel.BookTab.Post_Global, false);
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x00081F80 File Offset: 0x00080180
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
		Tooltip.Release();
		if (tab == AugmentsPanel.BookTab.Post_Player)
		{
			this.SetupPlayerStats();
			return;
		}
		if (tab == AugmentsPanel.BookTab.Post_Global)
		{
			if (RaidManager.IsInRaid || (this.openedFromCodex && this.codexRecord.IsRaid))
			{
				this.SetupGlobalStatsRaid();
				return;
			}
			this.SetupGlobalStats();
		}
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x0008203C File Offset: 0x0008023C
	public void GoToGlobal()
	{
		this.SelectTab(AugmentsPanel.BookTab.Post_Global, false);
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x00082048 File Offset: 0x00080248
	private void RefreshPlayerStats()
	{
		if (this.selectedStats == null || (this.selectedStats.PlayerRef == null && !this.openedFromCodex))
		{
			return;
		}
		PlayerControl playerRef = this.selectedStats.PlayerRef;
		this.UpdateNav();
		AbilityTree ability = this.openedFromCodex ? this.codexRecord.GetCoreAbility(this.selectedStats) : playerRef.actions.utility;
		AbilityTree ability2 = this.openedFromCodex ? this.codexRecord.GetPrimaryAbility(this.selectedStats) : playerRef.actions.primary;
		AbilityTree ability3 = this.openedFromCodex ? this.codexRecord.GetSecondaryAbility(this.selectedStats) : playerRef.actions.secondary;
		AbilityTree ability4 = this.openedFromCodex ? this.codexRecord.GetMovementAbility(this.selectedStats) : playerRef.actions.movement;
		this.PlayerName.text = (this.openedFromCodex ? this.codexRecord.GetPlayerInfo(this.selectedStats).Username : playerRef.GetUsernameText());
		this.CoreAbility.Setup(ability, this.openedFromCodex ? null : playerRef, this.selectedStats.GetDamage(PlayerStat.Utility));
		this.PrimaryAbility.Setup(ability2, this.openedFromCodex ? null : playerRef, this.selectedStats.GetDamage(PlayerStat.Primary));
		this.SecondaryAbility.Setup(ability3, this.openedFromCodex ? null : playerRef, this.selectedStats.GetDamage(PlayerStat.Secondary));
		this.MovementAbility.Setup(ability4, this.openedFromCodex ? null : playerRef, this.selectedStats.GetDamage(PlayerStat.Movement));
		this.Stats.Clear();
		this.ClearAugmentBoxes();
		List<AugmentRootNode> list = this.openedFromCodex ? this.codexRecord.GetPlayerInfo(this.selectedStats).GetAugments() : playerRef.Augment.trees.Keys.ToList<AugmentRootNode>();
		this.PlayerUpgradeTitle.text = "Upgrades - " + list.Count.ToString();
		foreach (AugmentRootNode augment in list)
		{
			this.AddAugmentBox(augment, this.PlayerAugmentList, this.openedFromCodex ? null : playerRef).SetupDamage(this.selectedStats.GetAugmentDamage(augment));
		}
		UISelector.SetupGridListNav<AugmentInfoBox>(this.AugmentDisplays, 6, this.PrimaryAbility.GetComponent<Button>(), null, false);
		if (this.AugmentDisplays.Count > 0)
		{
			Button component = this.AugmentDisplays[0].GetComponent<Button>();
			this.CoreAbility.GetComponent<Button>().SetNavigation(component, UIDirection.Down, false);
			this.PrimaryAbility.GetComponent<Button>().SetNavigation(component, UIDirection.Down, false);
			this.SecondaryAbility.GetComponent<Button>().SetNavigation(component, UIDirection.Down, false);
			this.MovementAbility.GetComponent<Button>().SetNavigation(component, UIDirection.Down, false);
		}
		int num = 24 - this.AugmentDisplays.Count;
		for (int i = 0; i < num; i++)
		{
			this.AddAugmentBox(null, this.PlayerAugmentList, null);
		}
		this.Stats.LoadPlayerStats(this.selectedStats, this.PlayerStats);
		this.SelectControllerDefault();
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x00082398 File Offset: 0x00080598
	public void NextPlayer()
	{
		if (this.PlayerStats.Count <= 1 || this.CurTab != AugmentsPanel.BookTab.Post_Player)
		{
			return;
		}
		this.selectedStats = this.GetStats(this.CurPlayerIndex() + 1);
		this.RefreshPlayerStats();
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x000823CC File Offset: 0x000805CC
	public void PrevPlayer()
	{
		if (this.PlayerStats.Count <= 1 || this.CurTab != AugmentsPanel.BookTab.Post_Player)
		{
			return;
		}
		int num = this.CurPlayerIndex() - 1;
		if (num < 0)
		{
			num = this.PlayerStats.Count - 1;
		}
		this.selectedStats = this.GetStats(num);
		this.RefreshPlayerStats();
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x00082420 File Offset: 0x00080620
	public void SetPlayer(PlayerGameStats stats)
	{
		if (this.selectedStats == stats)
		{
			return;
		}
		foreach (PlayerGameStats playerGameStats in this.PlayerStats)
		{
			if (playerGameStats == stats)
			{
				this.selectedStats = playerGameStats;
				break;
			}
		}
		this.RefreshPlayerStats();
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x0008248C File Offset: 0x0008068C
	private void UpdateNav()
	{
		bool flag = this.PlayerStats.Count > 1;
		foreach (GameObject gameObject in this.PlayerNavController)
		{
			gameObject.SetActive(flag && global::InputManager.IsUsingController);
		}
		foreach (GameObject gameObject2 in this.PlayerNavKBM)
		{
			gameObject2.SetActive(flag && !global::InputManager.IsUsingController);
		}
		this.PipHolder.SetActive(flag);
		if (flag)
		{
			foreach (GameObject obj in this.pips)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.pips.Clear();
			for (int i = 0; i < this.PlayerStats.Count; i++)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.PipRef, this.PipRef.transform.parent);
				gameObject3.SetActive(true);
				int x = i;
				gameObject3.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.SetPlayer(this.GetStats(x));
				});
				this.pips.Add(gameObject3);
				gameObject3.GetComponent<Image>().sprite = ((this.CurPlayerIndex() == i) ? this.Pip_Filled : this.Pip_Empty);
			}
		}
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x00082644 File Offset: 0x00080844
	private int CurPlayerIndex()
	{
		if (this.selectedStats == null || (this.selectedStats.PlayerRef == null && !this.openedFromCodex))
		{
			return 0;
		}
		return this.PlayerStats.IndexOf(this.selectedStats);
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x0008267C File Offset: 0x0008087C
	private PlayerGameStats GetStats(int index)
	{
		if (this.PlayerStats.Count <= 0)
		{
			return null;
		}
		index %= this.PlayerStats.Count;
		return this.PlayerStats[index];
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x000826AC File Offset: 0x000808AC
	private PlayerGameStats GetStats(PlayerControl player)
	{
		foreach (PlayerGameStats playerGameStats in this.PlayerStats)
		{
			if (playerGameStats.PlayerRef == player)
			{
				return playerGameStats;
			}
		}
		return null;
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x00082710 File Offset: 0x00080910
	public void AddPlayerData(PlayerGameStats stats)
	{
		for (int i = 0; i < this.PlayerStats.Count; i++)
		{
			if (!(this.PlayerStats[i].PlayerRef != stats.PlayerRef))
			{
				this.PlayerStats[i] = stats;
				return;
			}
		}
		this.PlayerStats.Add(stats);
		if (PanelManager.CurPanel == PanelType.PostGame && this.CurTab == AugmentsPanel.BookTab.Post_Player)
		{
			this.RefreshPlayerStats();
		}
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x00082784 File Offset: 0x00080984
	private void SetupGlobalStats()
	{
		this.Stats.Clear();
		this.ClearAugmentBoxes();
		foreach (GameObject gameObject in this.TomeDisplays)
		{
			gameObject.SetActive(true);
		}
		foreach (GameObject gameObject2 in this.RaidDisplays)
		{
			gameObject2.SetActive(false);
		}
		string str = this.openedFromCodex ? GraphDB.GetGenre(this.codexRecord.TomeID).Root.Name : GameplayManager.instance.GameGraph.Root.Name;
		if (GameplayManager.IsChallengeActive)
		{
			str = GameplayManager.Challenge.Name;
		}
		this.GenreTitleText.text = "<i>" + str + "</i>";
		this.ChallengeDisplay.SetActive(GameplayManager.IsChallengeActive);
		List<AugmentRootNode> list = this.openedFromCodex ? this.codexRecord.GetFontPowers() : InkManager.PurchasedMods.trees.Keys.ToList<AugmentRootNode>();
		List<AugmentInfoBox> list2 = new List<AugmentInfoBox>();
		this.FontRewardTitle.text = "Font Powers - " + list.Count.ToString();
		foreach (AugmentRootNode augment in list)
		{
			AugmentInfoBox augmentInfoBox = this.AddAugmentBox(augment, this.FontRewardGroup, null);
			augmentInfoBox.GetComponent<UIPingable>().PingType = UIPing.UIPingType.Augment_Font;
			list2.Add(augmentInfoBox);
		}
		int num = 12 - this.AugmentDisplays.Count;
		for (int i = 0; i < num; i++)
		{
			this.AddAugmentBox(null, this.FontRewardGroup, null);
		}
		List<AugmentRootNode> list3 = this.openedFromCodex ? this.codexRecord.GetEnemyMods() : AIManager.GetEnemyAugments();
		List<AugmentInfoBox> list4 = new List<AugmentInfoBox>();
		for (int j = 0; j < 10; j++)
		{
			AugmentRootNode augmentRootNode = (j < list3.Count) ? list3[j] : null;
			AugmentInfoBox item = this.AddEnemyBox((augmentRootNode == null) ? null : augmentRootNode, this.EnemyRewardGroup);
			if (augmentRootNode != null)
			{
				list4.Add(item);
			}
		}
		int num2 = this.openedFromCodex ? this.codexRecord.BindingLevel : GameplayManager.BindingLevel;
		this.BindingLevel.text = num2.ToString();
		this.NoBindingsObj.SetActive(num2 <= 0);
		foreach (AugmentRootNode augment2 in (this.openedFromCodex ? this.codexRecord.GetBindings() : GameplayManager.instance.GenreBindings.trees.Keys.ToList<AugmentRootNode>()))
		{
			this.AddBindingBox(augment2, this.BindingRef.transform.parent);
		}
		UISelector.SetupGridListNav<AugmentInfoBox>(list2, 6, null, null, false);
		UISelector.SetupGridListNav<AugmentInfoBox>(list4, 5, null, null, false);
		UISelector.SetupGridListNav<AugmentInfoBox>(this.BindingDisplays, 7, null, null, false);
		if (list4.Count > 0)
		{
			UISelector.SetupCrossGridNav<AugmentInfoBox, AugmentInfoBox>(list2, 6, list4, 5);
			UISelector.SetupCrossGridNav<AugmentInfoBox, AugmentInfoBox>(list4, 5, this.BindingDisplays, 7);
		}
		else
		{
			UISelector.SetupCrossGridNav<AugmentInfoBox, AugmentInfoBox>(list2, 6, this.BindingDisplays, 7);
		}
		if (this.openedFromCodex)
		{
			this.SetupGlobalCodexSection();
		}
		else
		{
			this.PostGame_Objects.SetActive(true);
			this.Codex_Objects.SetActive(false);
		}
		this.SelectControllerDefault();
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x00082B40 File Offset: 0x00080D40
	private void SetupGlobalStatsRaid()
	{
		RaidDB.Raid raid = RaidDB.GetRaid(this.openedFromCodex ? this.codexRecord.Raid : RaidManager.instance.CurrentRaid);
		this.Stats.Clear();
		this.ClearAugmentBoxes();
		foreach (GameObject gameObject in this.TomeDisplays)
		{
			gameObject.SetActive(false);
		}
		foreach (GameObject gameObject2 in this.RaidDisplays)
		{
			gameObject2.SetActive(true);
		}
		this.GenreTitleText.text = "<i>" + raid.RaidName + "</i>";
		this.ChallengeDisplay.SetActive(false);
		List<int> list = this.openedFromCodex ? this.codexRecord.AttemptCounts : RaidManager.instance.AttemptCounts;
		for (int i = 0; i < raid.Encounters.Count; i++)
		{
			if (raid.Encounters[i].Type != RaidDB.EncounterType.Vignette)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.RaidProgressRef, this.RaidProgressRef.transform.parent);
				gameObject3.SetActive(true);
				bool completed;
				if (this.openedFromCodex)
				{
					completed = (this.codexRecord.Won || list.Count > i);
				}
				else
				{
					completed = (i < RaidManager.instance.CurrentEncounterIndex);
				}
				int attempts = 0;
				if (i < list.Count)
				{
					attempts = list[i];
				}
				PostGame_RaidEncounter component = gameObject3.GetComponent<PostGame_RaidEncounter>();
				component.Setup(raid.Encounters[i], completed, i < raid.Encounters.Count - 1, attempts);
				this.RaidEncounterDisplays.Add(component);
			}
		}
		List<AugmentRootNode> list2 = this.openedFromCodex ? this.codexRecord.GetEnemyMods() : AIManager.GetEnemyAugments();
		List<AugmentInfoBox> list3 = new List<AugmentInfoBox>();
		for (int j = 0; j < 10; j++)
		{
			AugmentRootNode augmentRootNode = (j < list2.Count) ? list2[j] : null;
			AugmentInfoBox item = this.AddEnemyBox((augmentRootNode == null) ? null : augmentRootNode, this.EnemyRewardGroup);
			if (augmentRootNode != null)
			{
				list3.Add(item);
			}
		}
		UISelector.SetupHorizontalListNav<PostGame_RaidEncounter>(this.RaidEncounterDisplays, null, (list3.Count > 0) ? list3[0].GetComponent<Button>() : null, false);
		UISelector.SetupGridListNav<AugmentInfoBox>(list3, 5, (this.RaidEncounterDisplays.Count > 0) ? this.RaidEncounterDisplays[0].GetComponent<Button>() : null, null, false);
		if (this.openedFromCodex)
		{
			this.SetupGlobalCodexSection();
		}
		else
		{
			this.PostGame_Objects.SetActive(true);
			this.Codex_Objects.SetActive(false);
		}
		this.SelectControllerDefault();
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x00082E34 File Offset: 0x00081034
	private void SetupGlobalCodexSection()
	{
		this.Codex_Objects.SetActive(true);
		this.PostGame_Objects.SetActive(false);
		this.RecordRunDateText.text = this.codexRecord.RunDate.ToString("MM/dd/yyyy - HH:mm");
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x00082E70 File Offset: 0x00081070
	private AugmentInfoBox AddAugmentBox(AugmentRootNode augment, Transform holder, PlayerControl owner)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>((augment == null) ? this.UpgradeBoxEmptyRef : this.UpgradeBoxRef, holder);
		gameObject.SetActive(true);
		AugmentInfoBox component = gameObject.GetComponent<AugmentInfoBox>();
		component.Setup(augment, 1, ModType.Player, owner, TextAnchor.UpperCenter, Vector3.zero);
		this.AugmentDisplays.Add(component);
		return component;
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x00082EC4 File Offset: 0x000810C4
	private AugmentInfoBox AddEnemyBox(AugmentRootNode augment, Transform holder)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>((augment == null) ? this.EnemyUpgradeEmptyRef : this.EnemyUpgradeRef, holder);
		gameObject.SetActive(true);
		AugmentInfoBox component = gameObject.GetComponent<AugmentInfoBox>();
		component.Setup(augment, 1, ModType.Enemy, null, TextAnchor.UpperCenter, Vector3.zero);
		this.EnemyDisplays.Add(component);
		return component;
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x00082F18 File Offset: 0x00081118
	private AugmentInfoBox AddBindingBox(AugmentRootNode augment, Transform holder)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BindingRef, holder);
		gameObject.SetActive(true);
		AugmentInfoBox component = gameObject.GetComponent<AugmentInfoBox>();
		component.Setup(augment, 1, ModType.Binding, null, TextAnchor.LowerCenter, Vector3.zero);
		this.BindingDisplays.Add(component);
		return component;
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x00082F5C File Offset: 0x0008115C
	private void ClearAugmentBoxes()
	{
		foreach (AugmentInfoBox augmentInfoBox in this.AugmentDisplays)
		{
			UnityEngine.Object.Destroy(augmentInfoBox.gameObject);
		}
		this.AugmentDisplays.Clear();
		foreach (AugmentInfoBox augmentInfoBox2 in this.EnemyDisplays)
		{
			UnityEngine.Object.Destroy(augmentInfoBox2.gameObject);
		}
		this.EnemyDisplays.Clear();
		foreach (AugmentInfoBox augmentInfoBox3 in this.BindingDisplays)
		{
			UnityEngine.Object.Destroy(augmentInfoBox3.gameObject);
		}
		this.BindingDisplays.Clear();
		foreach (PostGame_RaidEncounter postGame_RaidEncounter in this.RaidEncounterDisplays)
		{
			UnityEngine.Object.Destroy(postGame_RaidEncounter.gameObject);
		}
		this.RaidEncounterDisplays.Clear();
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000830AC File Offset: 0x000812AC
	private void SelectControllerDefault()
	{
		if (!global::InputManager.IsUsingController)
		{
			return;
		}
		if (this.CurTab == AugmentsPanel.BookTab.Post_Player)
		{
			if (this.AugmentDisplays.Count > 0 && this.AugmentDisplays[0].GetComponent<Button>() != null)
			{
				UISelector.SelectSelectable(this.AugmentDisplays[0].GetComponent<Button>());
				return;
			}
			UISelector.SelectSelectable(this.DefaultSelectPlayer);
			return;
		}
		else
		{
			if (this.AugmentDisplays.Count > 0 && !this.AugmentDisplays[0].IsEmpty)
			{
				UISelector.SelectSelectable(this.AugmentDisplays[0].GetComponent<Button>());
				return;
			}
			if (this.EnemyDisplays.Count > 0 && !this.EnemyDisplays[0].IsEmpty)
			{
				UISelector.SelectSelectable(this.EnemyDisplays[0].GetComponent<Button>());
				return;
			}
			if (this.BindingDisplays.Count > 0)
			{
				UISelector.SelectSelectable(this.BindingDisplays[0].GetComponent<Button>());
			}
			return;
		}
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x000831A8 File Offset: 0x000813A8
	private void SaveToFile()
	{
		if (this.didSaveFile)
		{
			return;
		}
		this.didSaveFile = true;
		LocalRunRecord.SaveRecord(this.PlayerStats, this.wasWin);
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x000831CB File Offset: 0x000813CB
	private void SaveRaidToFile()
	{
		if (this.didSaveFile)
		{
			return;
		}
		this.didSaveFile = true;
		LocalRunRecord.SaveRaidRecord(this.PlayerStats, this.wasWin, RaidManager.instance.CurrentRaid);
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x000831F8 File Offset: 0x000813F8
	private void GameStateChanged(GameState from, GameState to)
	{
		if (to == GameState.Hub || to == GameState.Pregame || to == GameState.InWave)
		{
			this.ResetData();
		}
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x0008320C File Offset: 0x0008140C
	private void ResetData()
	{
		this.PlayerStats.Clear();
		PostGame_CurrencyRewards.instance.Clear();
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x00083223 File Offset: 0x00081423
	private void OnInputChanged()
	{
		if (PanelManager.CurPanel != PanelType.PostGame)
		{
			return;
		}
		if (!global::InputManager.IsUsingController)
		{
			return;
		}
		this.SelectControllerDefault();
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x00083240 File Offset: 0x00081440
	private void OnVotesChanged(ChoiceType ct)
	{
		if (ct != ChoiceType.ReturnLibrary)
		{
			return;
		}
		string text = "Library";
		if (VoteManager.PlayerVotes.ContainsKey(PhotonNetwork.LocalPlayer.ActorNumber))
		{
			text = "Wait!";
		}
		this.ReturnButtonText.text = text;
		this.ClearVotes();
		foreach (KeyValuePair<int, int> keyValuePair in VoteManager.PlayerVotes)
		{
			this.AddVoteDisplay(keyValuePair.Key);
		}
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x000832D4 File Offset: 0x000814D4
	private void AddVoteDisplay(int PlayerID)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ReturnVoteRef, this.ReturnVoteRef.transform.parent);
		gameObject.gameObject.SetActive(true);
		UIPlayerVoteDisplay component = gameObject.GetComponent<UIPlayerVoteDisplay>();
		component.Setup(PlayerID);
		this.VoteRefs.Add(component);
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x00083324 File Offset: 0x00081524
	private void ClearVotes()
	{
		foreach (UIPlayerVoteDisplay uiplayerVoteDisplay in this.VoteRefs)
		{
			UnityEngine.Object.Destroy(uiplayerVoteDisplay.gameObject);
		}
		this.VoteRefs.Clear();
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x00083384 File Offset: 0x00081584
	private void SecondaryAction()
	{
		if (this.CurTab == AugmentsPanel.BookTab.Post_Global)
		{
			this.ReturnVoteClick();
			return;
		}
		this.GoToGlobal();
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x0008339C File Offset: 0x0008159C
	public void ReturnVoteClick()
	{
		if (this.waitDelay > 0f)
		{
			return;
		}
		if (!VoteManager.IsVoting)
		{
			return;
		}
		if (!RaidManager.IsInRaid)
		{
			this.SaveToFile();
		}
		else
		{
			this.SaveRaidToFile();
		}
		if (VoteManager.PlayerVotes.ContainsKey(PlayerControl.myInstance.view.OwnerActorNr))
		{
			VoteManager.Vote(-1);
			return;
		}
		VoteManager.Vote(1);
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x000833FC File Offset: 0x000815FC
	public PostGamePanel()
	{
	}

	// Token: 0x04001406 RID: 5126
	public static PostGamePanel instance;

	// Token: 0x04001407 RID: 5127
	public AugmentsPanel.BookTab CurTab;

	// Token: 0x04001408 RID: 5128
	public List<AugmentsTabElement> Tabs;

	// Token: 0x04001409 RID: 5129
	public TextMeshProUGUI GameResultText;

	// Token: 0x0400140A RID: 5130
	public TextMeshProUGUI GameLengthText;

	// Token: 0x0400140B RID: 5131
	public GameObject WinDisplay;

	// Token: 0x0400140C RID: 5132
	public GameObject LostDisplay;

	// Token: 0x0400140D RID: 5133
	public GameObject UpgradeBoxRef;

	// Token: 0x0400140E RID: 5134
	public GameObject UpgradeBoxEmptyRef;

	// Token: 0x0400140F RID: 5135
	public GameObject AppendixGroup;

	// Token: 0x04001410 RID: 5136
	public TextMeshProUGUI AppendixText;

	// Token: 0x04001411 RID: 5137
	public PostGame_Stats_Display Stats;

	// Token: 0x04001412 RID: 5138
	public GameObject PostGame_Objects;

	// Token: 0x04001413 RID: 5139
	public GameObject Codex_Objects;

	// Token: 0x04001414 RID: 5140
	public Image TomeIcon;

	// Token: 0x04001415 RID: 5141
	public TextMeshProUGUI RecordRunDateText;

	// Token: 0x04001416 RID: 5142
	private List<AugmentInfoBox> AugmentDisplays = new List<AugmentInfoBox>();

	// Token: 0x04001417 RID: 5143
	public CanvasGroup PlayerGroup;

	// Token: 0x04001418 RID: 5144
	public Selectable DefaultSelectPlayer;

	// Token: 0x04001419 RID: 5145
	public TextMeshProUGUI PlayerName;

	// Token: 0x0400141A RID: 5146
	public List<GameObject> PlayerNavController;

	// Token: 0x0400141B RID: 5147
	public List<GameObject> PlayerNavKBM;

	// Token: 0x0400141C RID: 5148
	public GameObject PipHolder;

	// Token: 0x0400141D RID: 5149
	public GameObject PipRef;

	// Token: 0x0400141E RID: 5150
	public Sprite Pip_Filled;

	// Token: 0x0400141F RID: 5151
	public Sprite Pip_Empty;

	// Token: 0x04001420 RID: 5152
	public TextMeshProUGUI PlayerUpgradeTitle;

	// Token: 0x04001421 RID: 5153
	public PlayerStatAbilityUIGroup PrimaryAbility;

	// Token: 0x04001422 RID: 5154
	public PlayerStatAbilityUIGroup SecondaryAbility;

	// Token: 0x04001423 RID: 5155
	public PlayerStatAbilityUIGroup MovementAbility;

	// Token: 0x04001424 RID: 5156
	public PlayerStatAbilityUIGroup CoreAbility;

	// Token: 0x04001425 RID: 5157
	public Transform PlayerAugmentList;

	// Token: 0x04001426 RID: 5158
	public AutoScrollRect AutoScroller;

	// Token: 0x04001427 RID: 5159
	private List<GameObject> pips = new List<GameObject>();

	// Token: 0x04001428 RID: 5160
	private List<PlayerGameStats> PlayerStats = new List<PlayerGameStats>();

	// Token: 0x04001429 RID: 5161
	private PlayerGameStats selectedStats;

	// Token: 0x0400142A RID: 5162
	public CanvasGroup GlobalGroup;

	// Token: 0x0400142B RID: 5163
	public List<GameObject> TomeDisplays;

	// Token: 0x0400142C RID: 5164
	public TextMeshProUGUI GenreTitleText;

	// Token: 0x0400142D RID: 5165
	public TextMeshProUGUI BindingLevel;

	// Token: 0x0400142E RID: 5166
	public GameObject BindingAttuneDisplay;

	// Token: 0x0400142F RID: 5167
	public TextMeshProUGUI BindingAttuneText;

	// Token: 0x04001430 RID: 5168
	public GameObject ChallengeDisplay;

	// Token: 0x04001431 RID: 5169
	public TextMeshProUGUI FontRewardTitle;

	// Token: 0x04001432 RID: 5170
	public Transform FontRewardGroup;

	// Token: 0x04001433 RID: 5171
	public Transform EnemyRewardGroup;

	// Token: 0x04001434 RID: 5172
	public GameObject EnemyUpgradeRef;

	// Token: 0x04001435 RID: 5173
	public GameObject EnemyUpgradeEmptyRef;

	// Token: 0x04001436 RID: 5174
	public GameObject NoBindingsObj;

	// Token: 0x04001437 RID: 5175
	public GameObject BindingRef;

	// Token: 0x04001438 RID: 5176
	public TextMeshProUGUI ReturnButtonText;

	// Token: 0x04001439 RID: 5177
	public GameObject ReturnVoteRef;

	// Token: 0x0400143A RID: 5178
	public Image RaidIcon;

	// Token: 0x0400143B RID: 5179
	public List<GameObject> RaidDisplays;

	// Token: 0x0400143C RID: 5180
	public Transform RaidProgressGroup;

	// Token: 0x0400143D RID: 5181
	public GameObject RaidProgressRef;

	// Token: 0x0400143E RID: 5182
	private List<AugmentInfoBox> EnemyDisplays = new List<AugmentInfoBox>();

	// Token: 0x0400143F RID: 5183
	private List<AugmentInfoBox> BindingDisplays = new List<AugmentInfoBox>();

	// Token: 0x04001440 RID: 5184
	private List<UIPlayerVoteDisplay> VoteRefs = new List<UIPlayerVoteDisplay>();

	// Token: 0x04001441 RID: 5185
	private List<PostGame_RaidEncounter> RaidEncounterDisplays = new List<PostGame_RaidEncounter>();

	// Token: 0x04001442 RID: 5186
	private bool didRewards;

	// Token: 0x04001443 RID: 5187
	private bool wasWin;

	// Token: 0x04001444 RID: 5188
	private bool didSaveFile;

	// Token: 0x04001445 RID: 5189
	private bool openedFromCodex;

	// Token: 0x04001446 RID: 5190
	private LocalRunRecord codexRecord;

	// Token: 0x04001447 RID: 5191
	private float waitDelay;

	// Token: 0x04001448 RID: 5192
	private UIPanel panel;

	// Token: 0x020005B8 RID: 1464
	[CompilerGenerated]
	private sealed class <GoBack>d__74 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025EC RID: 9708 RVA: 0x000D27A9 File Offset: 0x000D09A9
		[DebuggerHidden]
		public <GoBack>d__74(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000D27B8 File Offset: 0x000D09B8
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000D27BC File Offset: 0x000D09BC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForEndOfFrame();
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			PanelManager.instance.PopPanel();
			return false;
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x000D2806 File Offset: 0x000D0A06
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000D280E File Offset: 0x000D0A0E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000D2815 File Offset: 0x000D0A15
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002852 RID: 10322
		private int <>1__state;

		// Token: 0x04002853 RID: 10323
		private object <>2__current;
	}

	// Token: 0x020005B9 RID: 1465
	[CompilerGenerated]
	private sealed class <>c__DisplayClass85_0
	{
		// Token: 0x060025F2 RID: 9714 RVA: 0x000D281D File Offset: 0x000D0A1D
		public <>c__DisplayClass85_0()
		{
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000D2825 File Offset: 0x000D0A25
		internal void <UpdateNav>b__0()
		{
			this.<>4__this.SetPlayer(this.<>4__this.GetStats(this.x));
		}

		// Token: 0x04002854 RID: 10324
		public int x;

		// Token: 0x04002855 RID: 10325
		public PostGamePanel <>4__this;
	}
}
