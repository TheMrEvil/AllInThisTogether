using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E0 RID: 480
public class GenrePanel : MonoBehaviour
{
	// Token: 0x0600140B RID: 5131 RVA: 0x0007C9D4 File Offset: 0x0007ABD4
	private void Awake()
	{
		GenrePanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnOpened));
		component.OnLeftPanel = (Action)Delegate.Combine(component.OnLeftPanel, new Action(this.OnLeft));
		component.OnSecondaryAction = (Action)Delegate.Combine(component.OnSecondaryAction, new Action(this.SetGenreCurrent));
		component.OnTertiaryAction = (Action)Delegate.Combine(component.OnTertiaryAction, new Action(this.TryChangeFocus));
		GameplayManager.OnGenereChanged = (Action<GenreTree>)Delegate.Combine(GameplayManager.OnGenereChanged, new Action<GenreTree>(this.OnGenreChanged));
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x0007CA94 File Offset: 0x0007AC94
	private IEnumerator Start()
	{
		while (GameplayManager.instance == null)
		{
			yield return true;
		}
		this.SetSelectedGenre(this.defaultSelected);
		yield break;
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x0007CAA4 File Offset: 0x0007ACA4
	private void OnOpened()
	{
		this.SelectTab(AugmentsPanel.BookTab.Tome_Info, true);
		this.FocusDisplay.SetCurrentFocus(!this.IsBookSelected);
		if (this.IsBookSelected)
		{
			this.TryChangeFocus();
		}
		else
		{
			GenreUIControl.instance.ControllerSelect(GenreUIControl.instance.selected);
		}
		bool flag = UITutorial.TryTutorial(UITutorial.Tutorial.Tomes);
		if (!Settings.HasCompletedLibraryTutorial(LibraryTutorial.Tomes))
		{
			LibraryManager.FinishedTutorial(LibraryTutorial.Tomes);
		}
		if (!flag && GameStats.GetTomeStat(this.bindingReqGenre, GameStats.Stat.TomesWon, 0) > 0)
		{
			if (!Settings.HasCompletedUITutorial(UITutorial.Tutorial.Bindings))
			{
				UITutorial.TryTutorial(UITutorial.Tutorial.BindingTomes);
			}
			AchievementRootNode achievement = GraphDB.GetAchievement("UNLOCK_LASTSTAND");
			if (achievement != null)
			{
				this.Shelf1Display.SetActive(!AchievementManager.IsUnlocked(achievement.ID));
				this.Shelf1Text.text = achievement.Detail;
			}
			AchievementRootNode achievement2 = GraphDB.GetAchievement("UNLOCK_BOSSRUSH");
			if (achievement2 != null)
			{
				this.Shelf2Display.SetActive(!AchievementManager.IsUnlocked(achievement2.ID));
				this.Shelf2Text.text = achievement2.Detail;
			}
		}
		this.SetupIncentiveDisplay();
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x0007CBB0 File Offset: 0x0007ADB0
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Genre_Selection)
		{
			return;
		}
		this.FocusDisplay.gameObject.SetActive(InputManager.IsUsingController);
		if (InputManager.IsUsingController)
		{
			this.Scroller.TickUpdate();
		}
		if (this.incentiveTTPt != null)
		{
			this.UpdateIncentiveTooltipLoc();
		}
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x0007CC02 File Offset: 0x0007AE02
	private void OnLeft()
	{
		this.selectedGenre = null;
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x0007CC0B File Offset: 0x0007AE0B
	public static void SetGenre(GenreTree g)
	{
		GenrePanel genrePanel = GenrePanel.instance;
		if (genrePanel == null)
		{
			return;
		}
		genrePanel.SetSelectedGenre(g);
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x0007CC20 File Offset: 0x0007AE20
	private void SetSelectedGenre(GenreTree g)
	{
		if (this.selectedGenre == g)
		{
			return;
		}
		this.SelectTab(AugmentsPanel.BookTab.Tome_Info, false);
		this.selectedGenre = g;
		this.BorderBase.SetActive(!this.selectedGenre.Root.IsNegativePower);
		this.BorderTorn.SetActive(this.selectedGenre.Root.IsNegativePower);
		bool flag = UnlockManager.IsGenreUnlocked(this.selectedGenre);
		this.Tabs[1].gameObject.SetActive(flag);
		foreach (GameObject gameObject in this.TabNavs)
		{
			gameObject.SetActive(flag);
		}
		this.SetupInfoArea(flag);
	}

	// Token: 0x06001412 RID: 5138 RVA: 0x0007CCF4 File Offset: 0x0007AEF4
	private void OnGenreChanged(GenreTree tome)
	{
		if (PanelManager.CurPanel != PanelType.Genre_Selection)
		{
			return;
		}
		if (tome != this.selectedGenre)
		{
			return;
		}
		this.UpdatePrepareButton(UnlockManager.IsGenreUnlocked(this.selectedGenre));
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x0007CD20 File Offset: 0x0007AF20
	private void SetupInfoArea(bool isUnlocked)
	{
		this.GenreTitle.text = this.selectedGenre.Root.Name;
		this.GenreDetail.text = (isUnlocked ? TextParser.AugmentDetail(this.selectedGenre.Root.Detail, null, null, false) : "???");
		this.FontPowerDisplay.SetActive(isUnlocked && this.selectedGenre.Root.HasTomePower);
		if (isUnlocked && this.selectedGenre.Root.HasTomePower && this.selectedGenre.Root.TomePowerAugment != null)
		{
			AugmentRootNode root = this.selectedGenre.Root.TomePowerAugment.Root;
			foreach (Image image in this.FontPowerIcon)
			{
				image.sprite = root.Icon;
			}
			this.FontPowerGood.SetActive(!this.selectedGenre.Root.IsNegativePower);
			this.FontPowerTorn.SetActive(this.selectedGenre.Root.IsNegativePower);
			foreach (TextMeshProUGUI textMeshProUGUI in this.FontPowerTitle)
			{
				textMeshProUGUI.text = root.Name;
			}
			this.FontPowerTitle[0].color = (this.selectedGenre.Root.IsNegativePower ? Color.black : new Color(0.3f, 0.26f, 0.12f, 1f));
			this.FontPowerDetail.text = TextParser.AugmentDetail(root.Detail, null, null, false);
		}
		if (isUnlocked)
		{
			this.SetupProgressionInfo();
		}
		this.UpdatePrepareButton(isUnlocked);
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x0007CF18 File Offset: 0x0007B118
	private void UpdatePrepareButton(bool isUnlocked)
	{
		this.PrepareButton.gameObject.SetActive(GameplayManager.instance.GameGraph != this.selectedGenre);
		this.PrepareButton.gameObject.SetActive(GameplayManager.instance.GameGraph != this.selectedGenre);
		bool flag = isUnlocked && GameplayManager.CurState != GameState.Hub_Bindings;
		this.PrepareButton.interactable = flag;
		this.LockedDisplay.SetActive(!flag);
		this.UpdateInfoText(this.selectedGenre);
		this.SelectedIncentiveDisplay.SetActive(this.selectedGenre.ID == GoalManager.TomeIncentive && QuestboardPanel.AreIncentivesUnlocked);
	}

	// Token: 0x06001415 RID: 5141 RVA: 0x0007CFD4 File Offset: 0x0007B1D4
	private void SetupProgressionInfo()
	{
		bool flag = GameStats.GetTomeStat(this.bindingReqGenre, GameStats.Stat.TomesWon, 0) > 0;
		int num = 0;
		this.HighestInfoText.text = "Highest Binding Beaten: <color=\"black\"><size=48>" + num.ToString();
		this.ClearProgressItems();
		List<Unlockable> genreRewards = UnlockDB.GetGenreRewards(this.selectedGenre, -1);
		int i = 0;
		while (i < genreRewards.Count)
		{
			if (flag)
			{
				goto IL_77;
			}
			if (genreRewards[i].AtBinding <= 0)
			{
				UnlockDB.AugmentUnlock augmentUnlock = genreRewards[i] as UnlockDB.AugmentUnlock;
				if (augmentUnlock == null || augmentUnlock.BindingLevel <= 0)
				{
					goto IL_77;
				}
			}
			IL_85:
			i++;
			continue;
			IL_77:
			this.AddProgressItem(genreRewards[i]);
			goto IL_85;
		}
		this.BindingsLockedInfo.transform.SetAsLastSibling();
		this.BindingsLockedInfo.SetActive(!flag);
		List<Button> list = new List<Button>();
		foreach (GameObject gameObject in this.progressItems)
		{
			list.Add(gameObject.GetComponent<TomeRewardItemUI>().MainButton);
		}
		UISelector.SetupVerticalListNav<Button>(list, this.TopFontButton, null, true);
		if (list.Count > 0)
		{
			this.TopFontButton.SetNavigation(list[0], UIDirection.Down, false);
		}
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x0007D11C File Offset: 0x0007B31C
	private void AddProgressItem(Unlockable award)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ProgressionItemRef, this.ProgressionItemRef.transform.parent);
		gameObject.SetActive(true);
		this.progressItems.Add(gameObject);
		UnlockDB.GenreUnlock genreUnlock = award as UnlockDB.GenreUnlock;
		if (genreUnlock != null)
		{
			gameObject.GetComponent<TomeRewardItemUI>().Setup(genreUnlock);
			return;
		}
		UnlockDB.AugmentUnlock augmentUnlock = award as UnlockDB.AugmentUnlock;
		if (augmentUnlock != null)
		{
			gameObject.GetComponent<TomeRewardItemUI>().Setup(augmentUnlock);
			return;
		}
		UnlockDB.BindingUnlock bindingUnlock = award as UnlockDB.BindingUnlock;
		if (bindingUnlock != null)
		{
			gameObject.GetComponent<TomeRewardItemUI>().Setup(bindingUnlock);
			return;
		}
		Cosmetic cosmetic = award as Cosmetic;
		if (cosmetic != null)
		{
			gameObject.GetComponent<TomeRewardItemUI>().Setup(cosmetic);
		}
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x0007D1B8 File Offset: 0x0007B3B8
	private void ClearProgressItems()
	{
		foreach (GameObject obj in this.progressItems)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.progressItems.Clear();
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x0007D214 File Offset: 0x0007B414
	public void TryChangeFocus()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		this.IsBookSelected = !this.IsBookSelected;
		this.FocusDisplay.SetCurrentFocus(!this.IsBookSelected);
		if (!this.IsBookSelected)
		{
			GenreUIControl.instance.ControllerSelect(GenreUIControl.instance.selected);
			return;
		}
		GenreUIControl.instance.HoverBook(null);
		if (this.selectedGenre.Root.HasTomePower)
		{
			UISelector.SelectSelectable(this.TopFontButton);
			return;
		}
		UISelector.SelectSelectable(this.progressItems[0].GetComponent<TomeRewardItemUI>().MainButton);
	}

	// Token: 0x06001419 RID: 5145 RVA: 0x0007D2B0 File Offset: 0x0007B4B0
	public void SelectTab(AugmentsPanel.BookTab tab, bool force = false)
	{
		if (this.CurTab == tab && !force)
		{
			return;
		}
		if (tab == AugmentsPanel.BookTab.Tome_Progress && !UnlockManager.IsGenreUnlocked(this.selectedGenre))
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
		this.ClearProgressItems();
		if (this.selectedGenre != null)
		{
			this.SetupInfoArea(UnlockManager.IsGenreUnlocked(this.selectedGenre));
		}
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x0007D364 File Offset: 0x0007B564
	private IEnumerator RebuildLayout()
	{
		yield return new WaitForEndOfFrame();
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.LayoutHolder);
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.LayoutHolder);
		yield break;
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0007D374 File Offset: 0x0007B574
	public void SetGenreCurrent()
	{
		if (!UnlockManager.IsGenreUnlocked(this.selectedGenre))
		{
			return;
		}
		if (GameplayManager.instance.GameGraph == this.selectedGenre && !GameplayManager.IsChallengeActive)
		{
			return;
		}
		GenrePanel genrePanel = GenrePanel.instance;
		AudioManager.PlaySFX2D((genrePanel != null) ? genrePanel.PrepareSFX : null, 1f, 0.1f);
		this.PrepareButton.gameObject.SetActive(false);
		GenrePanel.ApplyGenre(this.selectedGenre);
		this.UpdateInfoText(this.selectedGenre);
		PanelManager.instance.PopPanel();
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x0007D400 File Offset: 0x0007B600
	private void UpdateInfoText(GenreTree g)
	{
		string text = "";
		if (!UnlockManager.IsGenreUnlocked(this.selectedGenre))
		{
			text = "You have a chance to unlock this when successfully cleansing other Tomes.";
			UnlockDB.GenreUnlock genreUnlock = UnlockDB.GetGenreUnlock(this.selectedGenre);
			if (genreUnlock != null && genreUnlock.UnlockedBy == Unlockable.UnlockType.Achievement && !string.IsNullOrEmpty(genreUnlock.Achievement))
			{
				text = GraphDB.GetAchievement(genreUnlock.Achievement).Detail;
			}
		}
		this.PreparedObj.SetActive(GameplayManager.instance.GameGraph == this.selectedGenre && !GameplayManager.IsChallengeActive);
		this.LockedInfoText.text = text;
		base.StartCoroutine("RebuildLayout");
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x0007D4A1 File Offset: 0x0007B6A1
	public static void ApplyGenre(GenreTree genre)
	{
		if (genre == GenrePanel.instance.defaultSelected && GameStats.GetGlobalStat(GameStats.Stat.TomesWon, 0) < 1)
		{
			GameplayManager.instance.TrySetGenre(GenrePanel.instance.prologueGenre);
			return;
		}
		GameplayManager.instance.TrySetGenre(genre);
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x0007D4E0 File Offset: 0x0007B6E0
	private void SetupIncentiveDisplay()
	{
		GenreTree genre = GraphDB.GetGenre(GoalManager.TomeIncentive);
		if (genre == null || !QuestboardPanel.AreIncentivesUnlocked)
		{
			this.IncentiveDisplay.SetActive(false);
			this.incentiveTTPt = null;
			return;
		}
		GenreUIBook uibook = GenreUIControl.instance.GetUIBook(genre);
		this.incentiveTTPt = uibook.TooltipAnchor;
		this.UpdateIncentiveTooltipLoc();
		this.IncentiveDisplay.SetActive(true);
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x0007D548 File Offset: 0x0007B748
	private void UpdateIncentiveTooltipLoc()
	{
		Vector3 locationFromWorld = Tooltip.GetLocationFromWorld(this.incentiveTTPt.position);
		this.IncentiveDisplay.transform.position = new Vector3(locationFromWorld.x, locationFromWorld.y, this.IncentiveDisplay.transform.position.z);
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x0007D59C File Offset: 0x0007B79C
	public GenrePanel()
	{
	}

	// Token: 0x0400132C RID: 4908
	public static GenrePanel instance;

	// Token: 0x0400132D RID: 4909
	public GenreTree defaultSelected;

	// Token: 0x0400132E RID: 4910
	public GenreTree prologueGenre;

	// Token: 0x0400132F RID: 4911
	public GenreTree bindingReqGenre;

	// Token: 0x04001330 RID: 4912
	private GenreTree selectedGenre;

	// Token: 0x04001331 RID: 4913
	public GameObject Shelf1Display;

	// Token: 0x04001332 RID: 4914
	public TextMeshProUGUI Shelf1Text;

	// Token: 0x04001333 RID: 4915
	public GameObject Shelf2Display;

	// Token: 0x04001334 RID: 4916
	public TextMeshProUGUI Shelf2Text;

	// Token: 0x04001335 RID: 4917
	public List<AugmentsTabElement> Tabs;

	// Token: 0x04001336 RID: 4918
	public List<GameObject> TabNavs;

	// Token: 0x04001337 RID: 4919
	[HideInInspector]
	public AugmentsPanel.BookTab CurTab = AugmentsPanel.BookTab.Tome_Info;

	// Token: 0x04001338 RID: 4920
	public ControllerFocusDisplay FocusDisplay;

	// Token: 0x04001339 RID: 4921
	public TextMeshProUGUI GenreTitle;

	// Token: 0x0400133A RID: 4922
	public TextMeshProUGUI GenreDetail;

	// Token: 0x0400133B RID: 4923
	public GameObject BorderBase;

	// Token: 0x0400133C RID: 4924
	public GameObject BorderTorn;

	// Token: 0x0400133D RID: 4925
	public RectTransform LayoutHolder;

	// Token: 0x0400133E RID: 4926
	public List<GameObject> LockObjects;

	// Token: 0x0400133F RID: 4927
	public TextMeshProUGUI LockedInfoText;

	// Token: 0x04001340 RID: 4928
	public GameObject IncentiveDisplay;

	// Token: 0x04001341 RID: 4929
	[Header("Fountain Power")]
	public Button TopFontButton;

	// Token: 0x04001342 RID: 4930
	public GameObject FontPowerDisplay;

	// Token: 0x04001343 RID: 4931
	public GameObject FontPowerGood;

	// Token: 0x04001344 RID: 4932
	public GameObject FontPowerTorn;

	// Token: 0x04001345 RID: 4933
	public List<Image> FontPowerIcon;

	// Token: 0x04001346 RID: 4934
	public List<TextMeshProUGUI> FontPowerTitle;

	// Token: 0x04001347 RID: 4935
	public TextMeshProUGUI FontPowerDetail;

	// Token: 0x04001348 RID: 4936
	[Header("Placement Interaction")]
	public Button PrepareButton;

	// Token: 0x04001349 RID: 4937
	public GameObject LockedDisplay;

	// Token: 0x0400134A RID: 4938
	public GameObject PreparedObj;

	// Token: 0x0400134B RID: 4939
	public AudioClip PrepareSFX;

	// Token: 0x0400134C RID: 4940
	public GameObject SelectedIncentiveDisplay;

	// Token: 0x0400134D RID: 4941
	public GameObject ProgressionItemRef;

	// Token: 0x0400134E RID: 4942
	public TextMeshProUGUI HighestInfoText;

	// Token: 0x0400134F RID: 4943
	public AutoScrollRect Scroller;

	// Token: 0x04001350 RID: 4944
	public GameObject BindingsLockedInfo;

	// Token: 0x04001351 RID: 4945
	private List<GameObject> progressItems = new List<GameObject>();

	// Token: 0x04001352 RID: 4946
	[NonSerialized]
	public bool IsBookSelected;

	// Token: 0x04001353 RID: 4947
	private Transform incentiveTTPt;

	// Token: 0x020005AA RID: 1450
	[CompilerGenerated]
	private sealed class <Start>d__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025B1 RID: 9649 RVA: 0x000D20B6 File Offset: 0x000D02B6
		[DebuggerHidden]
		public <Start>d__40(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000D20C5 File Offset: 0x000D02C5
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000D20C8 File Offset: 0x000D02C8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GenrePanel genrePanel = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
			}
			if (!(GameplayManager.instance == null))
			{
				genrePanel.SetSelectedGenre(genrePanel.defaultSelected);
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x000D212B File Offset: 0x000D032B
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000D2133 File Offset: 0x000D0333
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000D213A File Offset: 0x000D033A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400281C RID: 10268
		private int <>1__state;

		// Token: 0x0400281D RID: 10269
		private object <>2__current;

		// Token: 0x0400281E RID: 10270
		public GenrePanel <>4__this;
	}

	// Token: 0x020005AB RID: 1451
	[CompilerGenerated]
	private sealed class <RebuildLayout>d__54 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025B7 RID: 9655 RVA: 0x000D2142 File Offset: 0x000D0342
		[DebuggerHidden]
		public <RebuildLayout>d__54(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000D2151 File Offset: 0x000D0351
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000D2154 File Offset: 0x000D0354
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GenrePanel genrePanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForEndOfFrame();
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				LayoutRebuilder.ForceRebuildLayoutImmediate(genrePanel.LayoutHolder);
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				LayoutRebuilder.ForceRebuildLayoutImmediate(genrePanel.LayoutHolder);
				return false;
			default:
				return false;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000D21D8 File Offset: 0x000D03D8
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000D21E0 File Offset: 0x000D03E0
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000D21E7 File Offset: 0x000D03E7
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400281F RID: 10271
		private int <>1__state;

		// Token: 0x04002820 RID: 10272
		private object <>2__current;

		// Token: 0x04002821 RID: 10273
		public GenrePanel <>4__this;
	}
}
