using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DA RID: 474
public class CosmeticsPanel : MonoBehaviour
{
	// Token: 0x060013B4 RID: 5044 RVA: 0x0007A240 File Offset: 0x00078440
	private void Awake()
	{
		this.ListItems = new List<CosmeticUIListItem>();
		CosmeticsPanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnPanelEnter));
		component.OnLeftPanel = (Action)Delegate.Combine(component.OnLeftPanel, new Action(this.CancelApplyEmote));
		component.OnNextTab = (Action)Delegate.Combine(component.OnNextTab, new Action(this.NextTab));
		component.OnPrevTab = (Action)Delegate.Combine(component.OnPrevTab, new Action(this.PrevTab));
		component.OnSecondaryAction = (Action)Delegate.Combine(component.OnSecondaryAction, new Action(this.OnSecondary));
		this.ContextButtonRef = this.ContextButton.GetComponent<Button>();
		for (int i = 0; i < this.EmoteApplications.Count; i++)
		{
			int x = i;
			this.EmoteApplications[i].Button.onClick.AddListener(delegate()
			{
				this.ApplyEmote(x);
			});
		}
	}

	// Token: 0x060013B5 RID: 5045 RVA: 0x0007A36F File Offset: 0x0007856F
	private void OnPanelEnter()
	{
		this.CancelApplyEmote();
		this.SelectTab(AugmentsPanel.BookTab.Cosmetic_Head, true);
		this.UpdateDisplay();
	}

	// Token: 0x060013B6 RID: 5046 RVA: 0x0007A388 File Offset: 0x00078588
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Cosmetics)
		{
			return;
		}
		if (this.Scroller != null && InputManager.IsUsingController)
		{
			this.Scroller.TickUpdate();
		}
		bool shouldShow = this.curList == CosmeticType.Emote;
		this.EmoteMiniDisplay.UpdateOpacity(shouldShow, 4f, true);
		this.EmoteApplyDisplay.UpdateOpacity(this.isApplyingEmote, 4f, false);
		this.ListGroup.UpdateOpacity(!this.isApplyingEmote, 4f, false);
		if (InputManager.UIAct.UIBack.WasPressed)
		{
			if (this.isApplyingEmote)
			{
				this.CancelApplyEmote();
				return;
			}
			PanelManager.instance.PopPanel();
		}
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x0007A438 File Offset: 0x00078638
	private void NextTab()
	{
		int num = this.CurrentTabIndex();
		num++;
		if (num >= this.Tabs.Count)
		{
			num = 0;
		}
		this.SelectTab(this.Tabs[num].Tab, false);
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x0007A478 File Offset: 0x00078678
	private void PrevTab()
	{
		int num = this.CurrentTabIndex();
		num--;
		if (num < 0)
		{
			num = this.Tabs.Count - 1;
		}
		this.SelectTab(this.Tabs[num].Tab, false);
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x0007A4BC File Offset: 0x000786BC
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

	// Token: 0x060013BA RID: 5050 RVA: 0x0007A4FC File Offset: 0x000786FC
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
		this.ListGroup.ShowImmediate();
		this.EmoteApplyDisplay.HideImmediate();
		if (this.CurTab == AugmentsPanel.BookTab.Cosmetic_Emote)
		{
			this.UpdateMiniEmotes();
		}
		else
		{
			this.CancelApplyEmote();
		}
		CosmeticType cType;
		switch (tab)
		{
		case AugmentsPanel.BookTab.Cosmetic_Head:
			cType = CosmeticType.Head;
			break;
		case AugmentsPanel.BookTab.Cosmetic_Outfit:
			cType = CosmeticType.Skin;
			break;
		case AugmentsPanel.BookTab.Cosmetic_Book:
			cType = CosmeticType.Book;
			break;
		default:
			if (tab != AugmentsPanel.BookTab.Cosmetic_Emote)
			{
				if (tab != AugmentsPanel.BookTab.Cosmetic_Back)
				{
					cType = CosmeticType.Head;
				}
				else
				{
					cType = CosmeticType.Signature;
				}
			}
			else
			{
				cType = CosmeticType.Emote;
			}
			break;
		}
		this.SelectList(cType, force);
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x0007A5E4 File Offset: 0x000787E4
	private void SelectList(CosmeticType cType, bool force = false)
	{
		if (this.curList == cType && !force)
		{
			return;
		}
		this.ClearList();
		CosmeticsUIControl.SetCosmeticTab(cType);
		this.curList = cType;
		this.ItemType.text = CosmeticDB.CosmeticName(this.curList);
		this.TabTitle.text = this.ItemType.text;
		Cosmetic cosmetic = PlayerControl.myInstance.Display.CurSet.GetCosmetic(cType);
		List<Cosmetic> cosmetics = CosmeticDB.GetCosmetics(cType);
		cosmetics.Sort(delegate(Cosmetic x, Cosmetic y)
		{
			int num = UnlockManager.IsCosmeticUnlocked(y).CompareTo(UnlockManager.IsCosmeticUnlocked(x));
			if (num != 0)
			{
				return num;
			}
			int num2 = x.Rarity.CompareTo(y.Rarity);
			if (num2 != 0)
			{
				return num2;
			}
			return 0;
		});
		CosmeticUIListItem cosmeticUIListItem = null;
		foreach (Cosmetic cosmetic2 in cosmetics)
		{
			if (!cosmetic2.Hidden || UnlockManager.IsCosmeticUnlocked(cosmetic2))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemListRef, this.ItemListRef.transform.parent);
				gameObject.SetActive(true);
				CosmeticUIListItem component = gameObject.GetComponent<CosmeticUIListItem>();
				component.Setup(cosmetic2);
				this.ListItems.Add(component);
				bool flag = cosmetic2 == cosmetic;
				if (cType == CosmeticType.Emote)
				{
					flag = Settings.GetEmotes().Contains(cosmetic2.cosmeticid);
				}
				if (flag || cosmeticUIListItem == null)
				{
					cosmeticUIListItem = component;
				}
				component.UpdateState(false, flag);
			}
		}
		UISelector.SetupVerticalListNav<CosmeticUIListItem>(this.ListItems, null, null, false);
		if (this.Selected != cosmeticUIListItem)
		{
			this.SelectItem(cosmeticUIListItem);
		}
		this.UpdateDisplay();
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x0007A76C File Offset: 0x0007896C
	private void ClearList()
	{
		foreach (CosmeticUIListItem cosmeticUIListItem in this.ListItems)
		{
			UnityEngine.Object.Destroy(cosmeticUIListItem.gameObject);
		}
		this.ListItems.Clear();
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x0007A7CC File Offset: 0x000789CC
	private void UpdateDisplay()
	{
		this.CurrentGildings.text = Currency.Gildings.ToString();
		if (this.Selected != null)
		{
			this.Selected.UpdateDisplay(UnlockManager.IsCosmeticUnlocked(this.Selected.cosmetic));
		}
		int num = 0;
		foreach (CosmeticUIListItem cosmeticUIListItem in this.ListItems)
		{
			bool flag = UnlockManager.IsCosmeticUnlocked(cosmeticUIListItem.cosmetic);
			num += (flag ? 1 : 0);
			cosmeticUIListItem.UpdateDisplay(flag);
		}
		this.OwnedCounter.text = num.ToString() + " / " + this.ListItems.Count.ToString();
		this.UpdateContextInfo();
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x0007A8AC File Offset: 0x00078AAC
	public void SelectItem(CosmeticUIListItem cItem)
	{
		if (cItem == null)
		{
			return;
		}
		Cosmetic cosmetic = cItem.cosmetic;
		bool flag = UnlockManager.IsCosmeticUnlocked(cosmetic);
		if (this.Selected == cItem && cosmetic.CType() != CosmeticType.Emote)
		{
			if (flag)
			{
				this.EquipSelected();
			}
			return;
		}
		this.Selected = cItem;
		UISelector.SelectSelectable(cItem.GetComponent<Button>());
		CosmeticsUIControl.SetCosmetic(cosmetic);
		foreach (CosmeticUIListItem cosmeticUIListItem in this.ListItems)
		{
			cosmeticUIListItem.SelectedDisplay.SetActive(cosmeticUIListItem == cItem);
		}
		this.ItemName.text = cosmetic.Name;
		this.ItemDetail.text = cosmetic.Detail;
		if (cosmetic.UnlockedBy == Unlockable.UnlockType.Achievement)
		{
			if (flag)
			{
				this.ItemDetail.text = "<b>Unlocked</b>: " + TextParser.AugmentDetail(cosmetic.GetUnlockReqText(), null, null, false);
			}
			else if (AchievementManager.GetAchievement(cosmetic.Achievement).RequiresClaim && !AchievementManager.IsClaimed(cosmetic.Achievement) && AchievementManager.IsUnlocked(cosmetic.Achievement))
			{
				if (GameStats.GetTomeStat(this.QuestTome, GameStats.Stat.TomesWon, 0) <= 0)
				{
					this.ItemDetail.text = "Claim at the <style=\"pos\">Questboard</style> after Mending <i>Quests and Trials</i>!";
				}
				else
				{
					this.ItemDetail.text = "Claim at the  <style=\"pos\">Questboard</style>!";
				}
			}
			else
			{
				this.ItemDetail.text = TextParser.AugmentDetail(cosmetic.GetUnlockReqText(), null, null, false);
			}
		}
		else if (cosmetic.UnlockedBy == Unlockable.UnlockType.TomeReward)
		{
			this.ItemDetail.text = (flag ? "<b>Unlocked</b>: " : "") + TextParser.AugmentDetail(cosmetic.GetUnlockReqText(), null, null, false);
		}
		this.CheckAchievement(flag, cosmetic);
		this.UpdateContextInfo();
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x0007AA74 File Offset: 0x00078C74
	private void CheckAchievement(bool isUnlocked, Cosmetic c)
	{
		if (isUnlocked || c.UnlockedBy != Unlockable.UnlockType.Achievement)
		{
			this.ProgressDisplay.SetActive(false);
			return;
		}
		AchievementRootNode achievement = AchievementManager.GetAchievement(c.Achievement);
		if (achievement != null && achievement.NeedsProgressDisplay() && !AchievementManager.IsUnlocked(c.Achievement))
		{
			this.ProgressDisplay.SetActive(true);
			ValueTuple<int, int> progressValues = achievement.GetProgressValues(null);
			int item = progressValues.Item1;
			int item2 = progressValues.Item2;
			this.ProgressText.text = string.Format("{0:N0} / {1:N0}", item, item2);
			this.ProgressFill.fillAmount = (float)item / Mathf.Max((float)item2, 1f);
			base.StartCoroutine("LayoutDelayed");
			return;
		}
		this.ProgressDisplay.SetActive(false);
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x0007AB3E File Offset: 0x00078D3E
	private IEnumerator LayoutDelayed()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.ItemDetail.GetComponent<RectTransform>());
		yield break;
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x0007AB50 File Offset: 0x00078D50
	private void UpdateContextInfo()
	{
		if (this.Selected == null)
		{
			this.ContextButton.SetActive(false);
			return;
		}
		bool flag;
		if (this.Selected.cosmetic is Cosmetic_Emote)
		{
			flag = Settings.GetEmotes().Contains(this.Selected.cosmetic.cosmeticid);
		}
		else
		{
			flag = (this.Selected.cosmetic == PlayerControl.myInstance.Display.CurSet.GetCosmetic(this.curList));
		}
		bool flag2 = !flag;
		bool flag3 = UnlockManager.IsCosmeticUnlocked(this.Selected.cosmetic);
		if (!flag3 && this.Selected.cosmetic.UnlockedBy != Unlockable.UnlockType.Purchase)
		{
			flag2 = false;
		}
		this.EquippedDisplay.SetActive(flag);
		this.ContextButton.SetActive(flag2 && !InputManager.IsUsingController);
		this.ControllerInteractDisplay.SetActive(flag2 && InputManager.IsUsingController);
		if (!flag2)
		{
			return;
		}
		this.ContextButtonText.text = (flag3 ? "Equip" : "Purchase");
		this.ContextButtonRef.interactable = (flag3 || this.Selected.cosmetic.Cost <= Currency.Gildings);
		this.ControllerInteractText.text = (flag3 ? "Equip" : "Purchase");
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x0007AC9D File Offset: 0x00078E9D
	public void OnSecondary()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.isApplyingEmote)
		{
			this.ApplyEmote(this.GetSelectedEmoteSlot());
			return;
		}
		this.ContextAction();
	}

	// Token: 0x060013C3 RID: 5059 RVA: 0x0007ACC2 File Offset: 0x00078EC2
	public void ContextAction()
	{
		if (this.Selected == null)
		{
			return;
		}
		if (UnlockManager.IsCosmeticUnlocked(this.Selected.cosmetic))
		{
			this.EquipSelected();
			return;
		}
		this.TryPurchase();
	}

	// Token: 0x060013C4 RID: 5060 RVA: 0x0007ACF4 File Offset: 0x00078EF4
	public void TryPurchase()
	{
		if (this.Selected.cosmetic.UnlockedBy != Unlockable.UnlockType.Purchase)
		{
			return;
		}
		int cost = this.Selected.cosmetic.Cost;
		if (cost > Currency.Gildings)
		{
			return;
		}
		if (!Currency.TrySpend(cost))
		{
			return;
		}
		LibraryInfoWidget.QuillmarksSpent(cost, this.Selected.transform);
		UnlockManager.UnlockCosmetic(this.Selected.cosmetic);
		AudioManager.PlayInterfaceSFX(this.PurchaseSFX, 1f, 0f);
		this.UpdateDisplay();
		this.EquipSelected();
	}

	// Token: 0x060013C5 RID: 5061 RVA: 0x0007AD7C File Offset: 0x00078F7C
	public void EquipSelected()
	{
		Cosmetic cosmetic = this.Selected.cosmetic;
		if (cosmetic.CType() == CosmeticType.Emote)
		{
			Cosmetic_Emote cosmetic_Emote = cosmetic as Cosmetic_Emote;
			if (cosmetic_Emote != null)
			{
				this.WantApplyEmote(cosmetic_Emote);
				goto IL_9F;
			}
		}
		foreach (CosmeticUIListItem cosmeticUIListItem in this.ListItems)
		{
			cosmeticUIListItem.EquippedDisplay.SetActive(cosmetic == cosmeticUIListItem.cosmetic);
		}
		CosmeticsUIControl.EquipFX();
		PlayerControl.myInstance.Display.ChangeCosmetic(cosmetic);
		AudioManager.PlayUISecondaryAction();
		Settings.SaveOutfit();
		AudioManager.PlayInterfaceSFX(this.EquipSFX, 1f, 0f);
		IL_9F:
		this.UpdateContextInfo();
	}

	// Token: 0x060013C6 RID: 5062 RVA: 0x0007AE40 File Offset: 0x00079040
	private void WantApplyEmote(Cosmetic_Emote c)
	{
		this.isApplyingEmote = true;
		this.toApply = c;
		this.prevSelected = UISelector.instance.CurrentSelection;
		List<string> emotes = Settings.GetEmotes();
		for (int i = 0; i < this.EmoteApplications.Count; i++)
		{
			if (i < emotes.Count)
			{
				string text = emotes[i];
			}
			Cosmetic_Emote emote = CosmeticDB.GetEmote(emotes[i]);
			this.EmoteApplications[i].Icon.sprite = emote.Icon;
			this.EmoteApplications[i].Label.text = emote.Name;
		}
		if (InputManager.IsUsingController)
		{
			UISelector.SelectSelectable(this.EmoteApplications[0].Button);
		}
		this.EquippedDisplay.SetActive(false);
	}

	// Token: 0x060013C7 RID: 5063 RVA: 0x0007AF08 File Offset: 0x00079108
	public void ApplyEmote(int slot)
	{
		if (slot < 0 || slot > 3)
		{
			return;
		}
		this.isApplyingEmote = false;
		this.EmoteApplications[slot].Icon.sprite = this.toApply.Icon;
		this.EmoteApplications[slot].Label.text = this.toApply.Name;
		List<string> emotes = Settings.GetEmotes();
		emotes[slot] = this.toApply.cosmeticid;
		Settings.SaveEmotes(emotes[0], emotes[1], emotes[2], emotes[3]);
		foreach (CosmeticUIListItem cosmeticUIListItem in this.ListItems)
		{
			cosmeticUIListItem.EquippedDisplay.SetActive(emotes.Contains(cosmeticUIListItem.cosmetic.cosmeticid));
		}
		this.UpdateMiniEmotes();
		UISelector.SelectSelectable(this.prevSelected);
		AudioManager.PlayInterfaceSFX(this.EquipSFX, 1f, 0f);
	}

	// Token: 0x060013C8 RID: 5064 RVA: 0x0007B024 File Offset: 0x00079224
	private void CancelApplyEmote()
	{
		if (!this.isApplyingEmote)
		{
			return;
		}
		this.isApplyingEmote = false;
		if (InputManager.IsUsingController)
		{
			UISelector.SelectSelectable(this.prevSelected);
		}
		this.EquippedDisplay.SetActive(Settings.GetEmotes().Contains(this.Selected.cosmetic.cosmeticid));
	}

	// Token: 0x060013C9 RID: 5065 RVA: 0x0007B078 File Offset: 0x00079278
	private void UpdateMiniEmotes()
	{
		List<string> emotes = Settings.GetEmotes();
		for (int i = 0; i < this.MiniIcons.Count; i++)
		{
			if (i < emotes.Count)
			{
				string text = emotes[i];
			}
			Cosmetic_Emote emote = CosmeticDB.GetEmote(emotes[i]);
			this.MiniIcons[i].sprite = emote.Icon;
		}
	}

	// Token: 0x060013CA RID: 5066 RVA: 0x0007B0D8 File Offset: 0x000792D8
	private int GetSelectedEmoteSlot()
	{
		for (int i = 0; i < this.EmoteApplications.Count; i++)
		{
			if (UISelector.instance.CurrentSelection == this.EmoteApplications[i].Button)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x0007B120 File Offset: 0x00079320
	public CosmeticsPanel()
	{
	}

	// Token: 0x040012D1 RID: 4817
	public static CosmeticsPanel instance;

	// Token: 0x040012D2 RID: 4818
	public TextMeshProUGUI TabTitle;

	// Token: 0x040012D3 RID: 4819
	public TextMeshProUGUI OwnedCounter;

	// Token: 0x040012D4 RID: 4820
	public AugmentsPanel.BookTab CurTab;

	// Token: 0x040012D5 RID: 4821
	public List<AugmentsTabElement> Tabs;

	// Token: 0x040012D6 RID: 4822
	public CanvasGroup ListGroup;

	// Token: 0x040012D7 RID: 4823
	public GameObject ItemListRef;

	// Token: 0x040012D8 RID: 4824
	public AutoScrollRect Scroller;

	// Token: 0x040012D9 RID: 4825
	public TextMeshProUGUI ItemType;

	// Token: 0x040012DA RID: 4826
	public TextMeshProUGUI ItemName;

	// Token: 0x040012DB RID: 4827
	public TextMeshProUGUI ItemDetail;

	// Token: 0x040012DC RID: 4828
	public GameObject ProgressDisplay;

	// Token: 0x040012DD RID: 4829
	public Image ProgressFill;

	// Token: 0x040012DE RID: 4830
	public TextMeshProUGUI ProgressText;

	// Token: 0x040012DF RID: 4831
	public GenreTree QuestTome;

	// Token: 0x040012E0 RID: 4832
	public CanvasGroup EmoteApplyDisplay;

	// Token: 0x040012E1 RID: 4833
	public CanvasGroup EmoteMiniDisplay;

	// Token: 0x040012E2 RID: 4834
	public List<Image> MiniIcons;

	// Token: 0x040012E3 RID: 4835
	public List<CosmeticsPanel.EmoteDisplay> EmoteApplications;

	// Token: 0x040012E4 RID: 4836
	public TextMeshProUGUI CurrentGildings;

	// Token: 0x040012E5 RID: 4837
	private Button ContextButtonRef;

	// Token: 0x040012E6 RID: 4838
	public GameObject ContextButton;

	// Token: 0x040012E7 RID: 4839
	public TextMeshProUGUI ContextButtonText;

	// Token: 0x040012E8 RID: 4840
	public GameObject ControllerInteractDisplay;

	// Token: 0x040012E9 RID: 4841
	public TextMeshProUGUI ControllerInteractText;

	// Token: 0x040012EA RID: 4842
	public GameObject EquippedDisplay;

	// Token: 0x040012EB RID: 4843
	public AudioClip PurchaseSFX;

	// Token: 0x040012EC RID: 4844
	public AudioClip EquipSFX;

	// Token: 0x040012ED RID: 4845
	private CosmeticType curList;

	// Token: 0x040012EE RID: 4846
	private List<CosmeticUIListItem> ListItems;

	// Token: 0x040012EF RID: 4847
	private CosmeticUIListItem Selected;

	// Token: 0x040012F0 RID: 4848
	private Selectable prevSelected;

	// Token: 0x040012F1 RID: 4849
	private bool isApplyingEmote;

	// Token: 0x040012F2 RID: 4850
	private Cosmetic_Emote toApply;

	// Token: 0x020005A2 RID: 1442
	[Serializable]
	public class EmoteDisplay
	{
		// Token: 0x06002597 RID: 9623 RVA: 0x000D1DAD File Offset: 0x000CFFAD
		public EmoteDisplay()
		{
		}

		// Token: 0x04002803 RID: 10243
		public Image Icon;

		// Token: 0x04002804 RID: 10244
		public TextMeshProUGUI Label;

		// Token: 0x04002805 RID: 10245
		public Button Button;
	}

	// Token: 0x020005A3 RID: 1443
	[CompilerGenerated]
	private sealed class <>c__DisplayClass34_0
	{
		// Token: 0x06002598 RID: 9624 RVA: 0x000D1DB5 File Offset: 0x000CFFB5
		public <>c__DisplayClass34_0()
		{
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000D1DBD File Offset: 0x000CFFBD
		internal void <Awake>b__0()
		{
			this.<>4__this.ApplyEmote(this.x);
		}

		// Token: 0x04002806 RID: 10246
		public int x;

		// Token: 0x04002807 RID: 10247
		public CosmeticsPanel <>4__this;
	}

	// Token: 0x020005A4 RID: 1444
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600259A RID: 9626 RVA: 0x000D1DD0 File Offset: 0x000CFFD0
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000D1DDC File Offset: 0x000CFFDC
		public <>c()
		{
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000D1DE4 File Offset: 0x000CFFE4
		internal int <SelectList>b__41_0(Cosmetic x, Cosmetic y)
		{
			int num = UnlockManager.IsCosmeticUnlocked(y).CompareTo(UnlockManager.IsCosmeticUnlocked(x));
			if (num != 0)
			{
				return num;
			}
			int num2 = x.Rarity.CompareTo(y.Rarity);
			if (num2 != 0)
			{
				return num2;
			}
			return 0;
		}

		// Token: 0x04002808 RID: 10248
		public static readonly CosmeticsPanel.<>c <>9 = new CosmeticsPanel.<>c();

		// Token: 0x04002809 RID: 10249
		public static Comparison<Cosmetic> <>9__41_0;
	}

	// Token: 0x020005A5 RID: 1445
	[CompilerGenerated]
	private sealed class <LayoutDelayed>d__46 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600259D RID: 9629 RVA: 0x000D1E2E File Offset: 0x000D002E
		[DebuggerHidden]
		public <LayoutDelayed>d__46(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000D1E3D File Offset: 0x000D003D
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000D1E40 File Offset: 0x000D0040
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			CosmeticsPanel cosmeticsPanel = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			LayoutRebuilder.ForceRebuildLayoutImmediate(cosmeticsPanel.ItemDetail.GetComponent<RectTransform>());
			return false;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x000D1E98 File Offset: 0x000D0098
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000D1EA0 File Offset: 0x000D00A0
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000D1EA7 File Offset: 0x000D00A7
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400280A RID: 10250
		private int <>1__state;

		// Token: 0x0400280B RID: 10251
		private object <>2__current;

		// Token: 0x0400280C RID: 10252
		public CosmeticsPanel <>4__this;
	}
}
