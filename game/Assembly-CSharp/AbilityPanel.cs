using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x020001D2 RID: 466
public class AbilityPanel : MonoBehaviour
{
	// Token: 0x060012FD RID: 4861 RVA: 0x00074B4C File Offset: 0x00072D4C
	private void Awake()
	{
		AbilityPanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnSelected));
		component.OnSecondaryAction = (Action)Delegate.Combine(component.OnSecondaryAction, new Action(this.OnSecondaryInteraction));
		component.OnLeftPanel = (Action)Delegate.Combine(component.OnLeftPanel, new Action(CanvasController.StopVideo));
		component.OnNextTab = (Action)Delegate.Combine(component.OnNextTab, new Action(this.NextTab));
		component.OnPrevTab = (Action)Delegate.Combine(component.OnPrevTab, new Action(this.PrevTab));
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x00074C10 File Offset: 0x00072E10
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Customize_Abilities)
		{
			return;
		}
		if (InputManager.IsUsingController)
		{
			foreach (AutoScrollRect autoScrollRect in this.AutoScrollers)
			{
				autoScrollRect.TickUpdate();
			}
		}
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x00074C70 File Offset: 0x00072E70
	private void OnSecondaryInteraction()
	{
		if (!InputManager.IsUsingController || this.SelectedAbility == null)
		{
			return;
		}
		if (UnlockManager.IsAbilityUnlocked(this.SelectedAbility))
		{
			this.Select(this.SelectedAbilityBox);
			return;
		}
		this.TryUnlockAbility();
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x00074CA8 File Offset: 0x00072EA8
	public void Leave()
	{
		if (PanelManager.CurPanel == PanelType.Customize_Abilities)
		{
			PanelManager.instance.PopPanel();
		}
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x00074CBC File Offset: 0x00072EBC
	private void OnSelected()
	{
		this.SelectTab(AugmentsPanel.BookTab.Ability_Generator, true);
		this.UpdateCurrents();
		if (!Settings.HasCompletedLibraryTutorial(LibraryTutorial.Abilities))
		{
			if (Currency.LoadoutCoin < 100)
			{
				Currency.AddLoadoutCoin(100 - Currency.LoadoutCoin, true);
			}
			LibraryManager.FinishedTutorial(LibraryTutorial.Abilities);
			Library_LorePages.instance.TryNextLibraryTutorial();
		}
		UITutorial.TryTutorial(UITutorial.Tutorial.Abilities);
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x00074D0E File Offset: 0x00072F0E
	public void NextTab()
	{
		if (this.CurTab == AugmentsPanel.BookTab.Ability_Generator)
		{
			this.SelectTab(AugmentsPanel.BookTab.Ability_Spender, false);
			return;
		}
		if (this.CurTab == AugmentsPanel.BookTab.Ability_Spender)
		{
			this.SelectTab(AugmentsPanel.BookTab.Ability_Movement, false);
			return;
		}
		if (this.CurTab == AugmentsPanel.BookTab.Ability_Movement)
		{
			this.SelectTab(AugmentsPanel.BookTab.Ability_Generator, false);
		}
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x00074D4B File Offset: 0x00072F4B
	public void PrevTab()
	{
		if (this.CurTab == AugmentsPanel.BookTab.Ability_Generator)
		{
			this.SelectTab(AugmentsPanel.BookTab.Ability_Movement, false);
			return;
		}
		if (this.CurTab == AugmentsPanel.BookTab.Ability_Spender)
		{
			this.SelectTab(AugmentsPanel.BookTab.Ability_Generator, false);
			return;
		}
		if (this.CurTab == AugmentsPanel.BookTab.Ability_Movement)
		{
			this.SelectTab(AugmentsPanel.BookTab.Ability_Spender, false);
		}
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x00074D88 File Offset: 0x00072F88
	public void SelectTab(AugmentsPanel.BookTab tab, bool force = false)
	{
		if (this.CurTab == tab && !force)
		{
			return;
		}
		this.CurTab = tab;
		foreach (AbilityPanel.AbilityTab abilityTab in this.Tabs)
		{
			if (abilityTab.Tab.Tab == this.CurTab)
			{
				abilityTab.Tab.Select();
			}
			else
			{
				abilityTab.Tab.Release();
			}
		}
		if (QuestboardPanel.AreIncentivesUnlocked)
		{
			AbilityTree abilityIncentive = GoalManager.GetAbilityIncentive();
			foreach (AbilityPanel.AbilityTab abilityTab2 in this.Tabs)
			{
				if (abilityTab2.Tab.Tab == this.CurTab)
				{
					abilityTab2.IncentiveDisplay.SetActive(false);
				}
				else
				{
					abilityTab2.IncentiveDisplay.SetActive(abilityIncentive != null && abilityIncentive.Root.PlrAbilityType == abilityTab2.AbilityType);
				}
			}
		}
		PlayerAbilityType abilityType;
		switch (tab)
		{
		case AugmentsPanel.BookTab.Ability_Generator:
			abilityType = PlayerAbilityType.Primary;
			break;
		case AugmentsPanel.BookTab.Ability_Spender:
			abilityType = PlayerAbilityType.Secondary;
			break;
		case AugmentsPanel.BookTab.Ability_Movement:
			abilityType = PlayerAbilityType.Movement;
			break;
		default:
			abilityType = PlayerAbilityType.Primary;
			break;
		}
		this.SelectAbilityType(abilityType);
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x00074EDC File Offset: 0x000730DC
	private void SelectAbilityType(PlayerAbilityType abilityType)
	{
		this.AbilityGrid.Setup(abilityType);
		UISelector.SelectSelectable(this.AbilityGrid.boxes[0].GetComponent<Button>());
		this.Hover(this.AbilityGrid.boxes[0]);
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x00074F1C File Offset: 0x0007311C
	public void Select(AbilityBoxUI selected)
	{
		Ability ability = PlayerControl.myInstance.actions.GetAbility(selected.abilityType);
		object obj;
		if (ability == null)
		{
			obj = null;
		}
		else
		{
			AbilityTree abilityTree = ability.AbilityTree;
			obj = ((abilityTree != null) ? abilityTree.RootNode : null);
		}
		AbilityRootNode abilityRootNode = obj as AbilityRootNode;
		bool flag = this.SelectedAbilityBox == selected;
		this.Hover(selected);
		if (selected.abilityTree.Root != abilityRootNode.AbilityRoot && !selected.isLocked && flag)
		{
			PlayerControl.myInstance.actions.LoadAbility(selected.abilityType, this.SelectedAbility.Root.guid, false);
			abilityRootNode = this.SelectedAbility;
			selected.Grouping.UpdateEquipped(selected);
			selected.EquipFX.Play();
			Settings.SaveLoadout();
			this.UpdateCurrents();
			AudioManager.PlayInterfaceSFX(this.EquipSFX, 1f, 0f);
		}
		AudioManager.PlayUISecondaryAction();
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x00075008 File Offset: 0x00073208
	public void TryEquipSelected()
	{
		if (this.SelectedAbilityBox == null)
		{
			return;
		}
		Ability ability = PlayerControl.myInstance.actions.GetAbility(this.SelectedAbilityBox.abilityType);
		object obj;
		if (ability == null)
		{
			obj = null;
		}
		else
		{
			AbilityTree abilityTree = ability.AbilityTree;
			obj = ((abilityTree != null) ? abilityTree.RootNode : null);
		}
		AbilityRootNode abilityRootNode = obj as AbilityRootNode;
		if (this.SelectedAbilityBox.abilityTree.Root != abilityRootNode.AbilityRoot && !this.SelectedAbilityBox.isLocked)
		{
			PlayerControl.myInstance.actions.LoadAbility(this.SelectedAbilityBox.abilityType, this.SelectedAbility.Root.guid, false);
			this.SelectedAbilityBox.Grouping.UpdateEquipped(this.SelectedAbilityBox);
			this.AbilityEquipButton.gameObject.SetActive(false);
			this.SelectedAbilityBox.EquipFX.Play();
			Settings.SaveLoadout();
			this.UpdateCurrents();
			AudioManager.PlayInterfaceSFX(this.EquipSFX, 1f, 0f);
		}
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x00075110 File Offset: 0x00073310
	public void Hover(AbilityBoxUI box)
	{
		this.SelectedAbilityBox = box;
		this.SelectedAbility = box.abilityTree;
		AbilityRootNode root = box.abilityTree.Root;
		this.AbilityTitle.text = root.Usage.AbilityMetadata.Name;
		this.AbilityDetail.text = TextParser.AugmentDetail(root.Usage.AbilityMetadata.Detail, null, null, false);
		this.CooldownLabel.text = root.Usage.Cooldown.ToString() + "s";
		this.ManaHolder.SetActive(root.Usage.AbilityMetadata.ManaCost > 0f);
		string text = "";
		int num = 0;
		while ((float)num < root.Usage.AbilityMetadata.ManaCost)
		{
			text += "<size=36><sprite name=\"mana_neutral\"> ";
			num++;
		}
		this.ManaLabel.text = text;
		string damageText = root.Usage.AbilityMetadata.DamageText;
		this.DamageHolder.SetActive(!string.IsNullOrEmpty(damageText));
		this.DamageLabel.text = damageText;
		this.UpdateVideo(box);
		foreach (AbilityBoxUI abilityBoxUI in this.AbilityGrid.boxes)
		{
			abilityBoxUI.SelectedDisplay.SetActive(abilityBoxUI == box);
		}
		this.UpdateInteractionInfo();
		this.IncentiveDisplay.gameObject.SetActive(GoalManager.IsIncentiveAbility(box.abilityTree));
		bool flag = RaidDB.IsAnyRaidUnlocked();
		this.StickerDisplay.gameObject.SetActive(flag);
		if (flag)
		{
			this.StickerDisplay.ShowStickers(box.abilityTree.ID);
		}
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x000752E4 File Offset: 0x000734E4
	private void UpdateCurrents()
	{
		PlayerStatAbilityUIGroup curPrimary = this.CurPrimary;
		Ability ability = PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Primary);
		curPrimary.Setup((ability != null) ? ability.AbilityTree : null, PlayerControl.myInstance, 0);
		PlayerStatAbilityUIGroup curSecondary = this.CurSecondary;
		Ability ability2 = PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Secondary);
		curSecondary.Setup((ability2 != null) ? ability2.AbilityTree : null, PlayerControl.myInstance, 0);
		PlayerStatAbilityUIGroup curMovement = this.CurMovement;
		Ability ability3 = PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Movement);
		curMovement.Setup((ability3 != null) ? ability3.AbilityTree : null, PlayerControl.myInstance, 0);
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x00075378 File Offset: 0x00073578
	private void UpdateVideo(AbilityBoxUI box)
	{
		PlayerAbilityType abilityType = box.abilityType;
		AbilityTree abilityTree = box.abilityTree;
		foreach (AbilityPanel.VideoInfo videoInfo in this.InfoItems)
		{
			if (videoInfo.AbilityType == abilityType)
			{
				this.VidBinding.Binding = videoInfo.Binding;
				this.VidBinding.UpdateIcon();
				this.VideoText.text = videoInfo.Text;
				break;
			}
		}
		foreach (AbilityPanel.AbilityVideo abilityVideo in this.Videos)
		{
			if (!(abilityVideo.Ability != abilityTree))
			{
				CanvasController.ChangeVideo(abilityVideo.Clip);
				break;
			}
		}
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x00075464 File Offset: 0x00073664
	private void UpdateInteractionInfo()
	{
		AbilityTree abilityTree = this.SelectedAbilityBox.abilityTree;
		bool flag = UnlockManager.IsAbilityUnlocked(abilityTree);
		bool flag2 = Currency.LoadoutCoin >= UnlockDB.GetAbilityUnlock(abilityTree).Cost;
		this.AbilityUnlockButton.gameObject.SetActive(!flag && !InputManager.IsUsingController);
		this.AbilityUnlockButton.interactable = flag2;
		Ability ability = PlayerControl.myInstance.actions.GetAbility(this.SelectedAbilityBox.abilityType);
		object obj;
		if (ability == null)
		{
			obj = null;
		}
		else
		{
			AbilityTree abilityTree2 = ability.AbilityTree;
			obj = ((abilityTree2 != null) ? abilityTree2.RootNode : null);
		}
		AbilityRootNode abilityRootNode = obj as AbilityRootNode;
		bool flag3 = this.SelectedAbilityBox.abilityTree.Root == abilityRootNode.AbilityRoot;
		this.AbilityEquipButton.gameObject.SetActive(flag && !flag3 && !InputManager.IsUsingController);
		this.ControllerInteractDisplay.gameObject.SetActive(InputManager.IsUsingController);
		this.ControllerInteractText.text = (flag ? "Equip" : "Unlock");
		Color color = (!flag && !flag2) ? this.ControlNegColor : this.ControlNeutralColor;
		this.ControllerInteractText.color = color;
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x00075590 File Offset: 0x00073790
	public void TryUnlockAbility()
	{
		if (this.SelectedAbility == null || UnlockManager.IsAbilityUnlocked(this.SelectedAbility))
		{
			return;
		}
		UnlockDB.AbilityUnlock abilityUnlock = UnlockDB.GetAbilityUnlock(this.SelectedAbility);
		int num = (abilityUnlock != null) ? abilityUnlock.Cost : int.MaxValue;
		if (Currency.LoadoutCoin < num)
		{
			return;
		}
		UnlockManager.UnlockAbility(this.SelectedAbility);
		Currency.SpendLoadoutCoin(num, true);
		this.AbilityUnlockButton.gameObject.SetActive(false);
		this.SelectedAbilityBox.UpdateLockedState();
		this.SelectedAbilityBox.UnlockFX.Play();
		this.Select(this.SelectedAbilityBox);
		this.AbilityEquipButton.gameObject.SetActive(false);
		LibraryInfoWidget.QuillmarksSpent(num, this.SelectedAbilityBox.transform);
		this.AbilityGrid.UpdateLockInfo();
		AudioManager.PlayInterfaceSFX(this.UnlockSFX, 1f, 0f);
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x0007566C File Offset: 0x0007386C
	private void AddKeywords(string input)
	{
		this.ClearKeywords();
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(input, PlayerControl.myInstance))
		{
			KeywordBoxUI.CreateBox(parsable, this.keywordHolder, ref this.keywordBoxes, PlayerControl.myInstance);
		}
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x000756D8 File Offset: 0x000738D8
	private void ClearKeywords()
	{
		foreach (KeywordBoxUI keywordBoxUI in this.keywordBoxes)
		{
			if (keywordBoxUI != null)
			{
				UnityEngine.Object.Destroy(keywordBoxUI.gameObject);
			}
		}
		this.keywordBoxes.Clear();
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x00075744 File Offset: 0x00073944
	public AbilityPanel()
	{
	}

	// Token: 0x0400121B RID: 4635
	public static AbilityPanel instance;

	// Token: 0x0400121C RID: 4636
	[Header("Ability Selection")]
	public List<AbilityPanel.AbilityTab> Tabs;

	// Token: 0x0400121D RID: 4637
	public AbilityGroupBar AbilityGrid;

	// Token: 0x0400121E RID: 4638
	private List<AutoScrollRect> AutoScrollers = new List<AutoScrollRect>();

	// Token: 0x0400121F RID: 4639
	[HideInInspector]
	public AugmentsPanel.BookTab CurTab = AugmentsPanel.BookTab.Ability_Generator;

	// Token: 0x04001220 RID: 4640
	public AudioClip UnlockSFX;

	// Token: 0x04001221 RID: 4641
	public AudioClip EquipSFX;

	// Token: 0x04001222 RID: 4642
	public PlayerStatAbilityUIGroup CurPrimary;

	// Token: 0x04001223 RID: 4643
	public PlayerStatAbilityUIGroup CurSecondary;

	// Token: 0x04001224 RID: 4644
	public PlayerStatAbilityUIGroup CurMovement;

	// Token: 0x04001225 RID: 4645
	public TextMeshProUGUI VideoText;

	// Token: 0x04001226 RID: 4646
	public UIBindingDisplay VidBinding;

	// Token: 0x04001227 RID: 4647
	public List<AbilityPanel.VideoInfo> InfoItems;

	// Token: 0x04001228 RID: 4648
	public List<AbilityPanel.AbilityVideo> Videos;

	// Token: 0x04001229 RID: 4649
	public TextMeshProUGUI AbilityTitle;

	// Token: 0x0400122A RID: 4650
	public TextMeshProUGUI AbilityDetail;

	// Token: 0x0400122B RID: 4651
	public TextMeshProUGUI CooldownLabel;

	// Token: 0x0400122C RID: 4652
	public GameObject ManaHolder;

	// Token: 0x0400122D RID: 4653
	public TextMeshProUGUI ManaLabel;

	// Token: 0x0400122E RID: 4654
	public GameObject DamageHolder;

	// Token: 0x0400122F RID: 4655
	public TextMeshProUGUI DamageLabel;

	// Token: 0x04001230 RID: 4656
	public GameObject IncentiveDisplay;

	// Token: 0x04001231 RID: 4657
	public RaidStickers StickerDisplay;

	// Token: 0x04001232 RID: 4658
	public Button AbilityUnlockButton;

	// Token: 0x04001233 RID: 4659
	public Button AbilityEquipButton;

	// Token: 0x04001234 RID: 4660
	public GameObject ControllerInteractDisplay;

	// Token: 0x04001235 RID: 4661
	public TextMeshProUGUI ControllerInteractText;

	// Token: 0x04001236 RID: 4662
	public Color ControlNeutralColor;

	// Token: 0x04001237 RID: 4663
	public Color ControlNegColor;

	// Token: 0x04001238 RID: 4664
	private AbilityBoxUI SelectedAbilityBox;

	// Token: 0x04001239 RID: 4665
	private AbilityTree SelectedAbility;

	// Token: 0x0400123A RID: 4666
	private bool didSetup;

	// Token: 0x0400123B RID: 4667
	public RectTransform keywordHolder;

	// Token: 0x0400123C RID: 4668
	private List<KeywordBoxUI> keywordBoxes = new List<KeywordBoxUI>();

	// Token: 0x02000591 RID: 1425
	[Serializable]
	public class AbilityVideo
	{
		// Token: 0x06002575 RID: 9589 RVA: 0x000D1744 File Offset: 0x000CF944
		public AbilityVideo()
		{
		}

		// Token: 0x040027B3 RID: 10163
		public AbilityTree Ability;

		// Token: 0x040027B4 RID: 10164
		public VideoClip Clip;
	}

	// Token: 0x02000592 RID: 1426
	[Serializable]
	public class VideoInfo
	{
		// Token: 0x06002576 RID: 9590 RVA: 0x000D174C File Offset: 0x000CF94C
		public VideoInfo()
		{
		}

		// Token: 0x040027B5 RID: 10165
		public PlayerAbilityType AbilityType;

		// Token: 0x040027B6 RID: 10166
		public InputActions.InputAction Binding;

		// Token: 0x040027B7 RID: 10167
		[TextArea(4, 5)]
		public string Text;
	}

	// Token: 0x02000593 RID: 1427
	[Serializable]
	public class AbilityTab
	{
		// Token: 0x06002577 RID: 9591 RVA: 0x000D1754 File Offset: 0x000CF954
		public AbilityTab()
		{
		}

		// Token: 0x040027B8 RID: 10168
		public AugmentsPanel.BookTab TabType;

		// Token: 0x040027B9 RID: 10169
		public PlayerAbilityType AbilityType;

		// Token: 0x040027BA RID: 10170
		public AugmentsTabElement Tab;

		// Token: 0x040027BB RID: 10171
		public GameObject IncentiveDisplay;
	}
}
