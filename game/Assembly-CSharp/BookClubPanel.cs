using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D5 RID: 469
public class BookClubPanel : MonoBehaviour
{
	// Token: 0x17000161 RID: 353
	// (get) Token: 0x06001355 RID: 4949 RVA: 0x00077419 File Offset: 0x00075619
	public static MetaDB.BookClubChallenge Challenge
	{
		get
		{
			return MetaDB.CurrentChallenge;
		}
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x00077420 File Offset: 0x00075620
	private void Awake()
	{
		BookClubPanel.instance = this;
		BookClubPanel.editorOverrideChallenge = null;
		this.fetchedChallengeID = "";
		this.panel = base.GetComponent<UIPanel>();
		UIPanel uipanel = this.panel;
		uipanel.OnEnteredPanel = (Action)Delegate.Combine(uipanel.OnEnteredPanel, new Action(this.OnEnteredPanel));
		UIPanel uipanel2 = this.panel;
		uipanel2.OnLeftPanel = (Action)Delegate.Combine(uipanel2.OnLeftPanel, new Action(this.OnLeavePanel));
		UIPanel uipanel3 = this.panel;
		uipanel3.OnNextTab = (Action)Delegate.Combine(uipanel3.OnNextTab, new Action(this.NextPage));
		UIPanel uipanel4 = this.panel;
		uipanel4.OnPrevTab = (Action)Delegate.Combine(uipanel4.OnPrevTab, new Action(this.PrevPage));
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.OnInputChanged));
		this.BindingDisplayRef.SetActive(false);
		this.TornPageDisplayRef.SetActive(false);
		this.PlayerPageRef.SetActive(false);
		this.statBarRef.SetActive(false);
		GameplayManager.OnGenereChanged = (Action<GenreTree>)Delegate.Combine(GameplayManager.OnGenereChanged, new Action<GenreTree>(this.OnGenreChanged));
		this.BonusDisplay.GetComponent<AugmentInfoBox>().Setup(this.BonusBindingAugment.Root, 1, ModType.Binding, null, TextAnchor.UpperCenter, Vector2.zero);
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x00077588 File Offset: 0x00075788
	private void OnEnteredPanel()
	{
		int compositStat = GameStats.GetCompositStat(GameStats.CompositeStat.BookClubsCompleted);
		string text = string.Format("Completed <style=pos>{0}</style> Book Club challenge{1}.", compositStat, (compositStat != 1) ? "s" : "");
		this.LocalStatText.text = text;
		this.LoadChallenge(BookClubPanel.Challenge);
		base.InvokeRepeating("UpdateTimerText", 0f, 5f);
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x000775E9 File Offset: 0x000757E9
	private void OnLeavePanel()
	{
		base.CancelInvoke("UpdateTimerText");
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x000775F8 File Offset: 0x000757F8
	public void NextPage()
	{
		Codex_GlobalStatsDisplay.ComparisonType currentStat;
		switch (this.CurrentStat)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			currentStat = Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat;
			goto IL_2D;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			currentStat = Codex_GlobalStatsDisplay.ComparisonType.Duration;
			goto IL_2D;
		case Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat:
			currentStat = Codex_GlobalStatsDisplay.ComparisonType.AppendixReached;
			goto IL_2D;
		}
		currentStat = Codex_GlobalStatsDisplay.ComparisonType.Duration;
		IL_2D:
		this.CurrentStat = currentStat;
		this.ShowStats(this.CurrentStat);
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x00077648 File Offset: 0x00075848
	public void PrevPage()
	{
		Codex_GlobalStatsDisplay.ComparisonType currentStat;
		switch (this.CurrentStat)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			currentStat = Codex_GlobalStatsDisplay.ComparisonType.AppendixReached;
			goto IL_2D;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			currentStat = Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat;
			goto IL_2D;
		case Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat:
			currentStat = Codex_GlobalStatsDisplay.ComparisonType.Duration;
			goto IL_2D;
		}
		currentStat = Codex_GlobalStatsDisplay.ComparisonType.Duration;
		IL_2D:
		this.CurrentStat = currentStat;
		this.ShowStats(this.CurrentStat);
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x00077698 File Offset: 0x00075898
	private void LoadChallenge(MetaDB.BookClubChallenge c)
	{
		this.challengeCache = c;
		this.challengeID = c.ID;
		if (this.fetchedChallengeID == this.challengeID)
		{
			this.fetchedChallengeID = "";
		}
		this.statID = this.challengeID + "_" + MetaDB.GetCurrentChallengeNumber().ToString();
		this.Title.text = c.Name;
		this.DetailText.text = TextParser.AugmentDetail(c.Description, null, null, false);
		this.TomeName.text = c.Tome.Root.Name;
		this.TomeIcon.sprite = c.Tome.Root.Icon;
		this.SetupDifficultyBanners(c.DifficultyLevel);
		this.SetupTornInfo(c);
		this.SetupPlayerInfo(c);
		this.SetupNavigation(c);
		this.UpdateRewards();
		if (!string.IsNullOrEmpty(this.fetchedChallengeID))
		{
			this.SetupStats();
		}
		this.RefreshChallengeStats();
		Progression.SawBookClubChallenge(this.challengeID);
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x000777A4 File Offset: 0x000759A4
	private void SetupDifficultyBanners(MetaDB.BookClubChallenge.Difficulty d)
	{
		for (int i = 0; i < this.DifficultyPips.Count; i++)
		{
			if (i <= (int)d)
			{
				this.DifficultyPips[i].color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				this.DifficultyPips[i].color = new Color(0.33f, 0.33f, 0.33f, 0.92f);
			}
		}
		foreach (BookClubPanel.DifficultyBannerDisplay difficultyBannerDisplay in this.DifficultyBanners)
		{
			if (difficultyBannerDisplay.Difficulty == d)
			{
				this.Difficulty.color = difficultyBannerDisplay.TextColor;
				this.DifficultyBanner.color = difficultyBannerDisplay.HSVColor;
				break;
			}
		}
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x00077890 File Offset: 0x00075A90
	private void SetupTornInfo(MetaDB.BookClubChallenge c)
	{
		int num = c.BindingBoost;
		foreach (AugmentTree augmentTree in c.Bindings)
		{
			num += augmentTree.Root.HeatLevel;
		}
		this.BindingText.text = string.Format("<sprite name=binding> {0}", num);
		foreach (GameObject gameObject in this.BindingPages)
		{
			if (gameObject != this.BonusDisplay)
			{
				UnityEngine.Object.Destroy(gameObject.gameObject);
			}
		}
		this.BindingPages.Clear();
		if (c.Bindings.Count > 0)
		{
			int num2 = 95;
			if (c.Bindings.Count > 7 && c.TornPages.Count > 0)
			{
				num2 = 60;
			}
			this.BindingLayout.cellSize = new Vector2((float)num2, (float)num2);
			foreach (AugmentTree t in c.Bindings)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.BindingDisplayRef, this.BindingDisplayRef.transform.parent);
				gameObject2.SetActive(true);
				gameObject2.GetComponent<AugmentInfoBox>().Setup(t, 1, ModType.Binding, null, TextAnchor.UpperCenter, Vector2.zero);
				this.BindingPages.Add(gameObject2);
			}
		}
		if (c.Bindings.Count > 0 || c.BindingBoost >= 7)
		{
			foreach (AugmentTree t2 in WaveDB.GetGlobalBindings(num, WaveManager.instance.AppendixLevel))
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.BindingDisplayRef, this.BindingDisplayRef.transform.parent);
				gameObject3.SetActive(true);
				gameObject3.GetComponent<AugmentInfoBox>().Setup(t2, 1, ModType.Binding, null, TextAnchor.UpperCenter, Vector2.zero);
				this.BindingPages.Add(gameObject3);
			}
		}
		if (c.BindingBoost > 0)
		{
			this.BonusDisplay.transform.SetAsLastSibling();
			this.BonusDisplay.SetActive(true);
			this.BonusBindingLabel.text = string.Format("+{0}", c.BindingBoost);
			this.BindingPages.Add(this.BonusDisplay);
		}
		else
		{
			this.BonusDisplay.SetActive(false);
		}
		foreach (GameObject gameObject4 in this.TornPages)
		{
			UnityEngine.Object.Destroy(gameObject4.gameObject);
		}
		this.TornPages.Clear();
		if (c.TornPages.Count > 0)
		{
			this.TornPageGroup.SetActive(true);
			using (List<AugmentTree>.Enumerator enumerator = c.TornPages.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AugmentTree t3 = enumerator.Current;
					GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.TornPageDisplayRef, this.TornPageDisplayRef.transform.parent);
					gameObject5.SetActive(true);
					gameObject5.GetComponent<AugmentInfoBox>().Setup(t3, 1, ModType.Enemy, null, TextAnchor.UpperCenter, Vector2.zero);
					this.TornPages.Add(gameObject5);
				}
				return;
			}
		}
		this.TornPageGroup.SetActive(false);
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x00077C68 File Offset: 0x00075E68
	private void SetupPlayerInfo(MetaDB.BookClubChallenge c)
	{
		this.Signature.Setup(c.GetCore(), null, 0);
		this.SignatureIcon.sprite = PlayerDB.GetCore(c.Signature).MajorIcon;
		this.PrimaryAbility.Setup(c.Generator, null, 0);
		this.SecondaryAbility.Setup(c.Spender, null, 0);
		this.MovementAbility.Setup(c.Movement, null, 0);
		if (c.PlayerPages.Count > 0)
		{
			this.PlayerPageGroup.SetActive(true);
			this.LoadoutRect.anchoredPosition = new Vector2(this.LoadoutRect.anchoredPosition.x, this.LoadoutUpperY);
			this.SetupPlayerPages(c);
			return;
		}
		this.PlayerPageGroup.SetActive(false);
		this.LoadoutRect.anchoredPosition = new Vector2(this.LoadoutRect.anchoredPosition.x, this.LoadoutMiddleY);
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x00077D58 File Offset: 0x00075F58
	private void SetupPlayerPages(MetaDB.BookClubChallenge c)
	{
		foreach (GameObject gameObject in this.PlayerPages)
		{
			UnityEngine.Object.Destroy(gameObject.gameObject);
		}
		this.PlayerPages.Clear();
		foreach (AugmentTree t in c.PlayerPages)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.PlayerPageRef, this.PlayerPageRef.transform.parent);
			gameObject2.SetActive(true);
			gameObject2.GetComponent<AugmentInfoBox>().Setup(t, 1, ModType.Player, null, TextAnchor.UpperCenter, Vector2.zero);
			this.PlayerPages.Add(gameObject2);
		}
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x00077E44 File Offset: 0x00076044
	private void SetupNavigation(MetaDB.BookClubChallenge c)
	{
		Selectable component = this.Signature.GetComponent<Selectable>();
		int num = 7;
		if (c.Bindings.Count > num && c.TornPages.Count > 0)
		{
			num = 10;
		}
		Selectable belowList = component;
		if (this.TornPages.Count > 0)
		{
			belowList = this.TornPages[0].GetComponent<Selectable>();
		}
		UISelector.SetupGridListNav(this.BindingPages, num, null, belowList, false);
		if (this.TornPages.Count > 0)
		{
			Selectable selectable = null;
			if (this.BindingPages.Count > 0)
			{
				List<GameObject> bindingPages = this.BindingPages;
				int index = bindingPages.Count - 1;
				selectable = bindingPages[index].GetComponent<Selectable>();
			}
			Selectable component2 = this.Signature.GetComponent<Selectable>();
			UISelector.SetupHorizontalListNav(this.TornPages, selectable, component2, false);
			foreach (GameObject gameObject in this.TornPages)
			{
				Selectable component3 = gameObject.GetComponent<Selectable>();
				component3.SetNavigation(component2, UIDirection.Down, false);
				component3.SetNavigation(selectable, UIDirection.Up, false);
			}
		}
		Selectable target = null;
		if (this.BindingPages.Count > 0)
		{
			List<GameObject> bindingPages2 = this.BindingPages;
			int index = bindingPages2.Count - 1;
			target = bindingPages2[index].GetComponent<Selectable>();
		}
		if (this.TornPages.Count > 0)
		{
			List<GameObject> tornPages = this.TornPages;
			int index = tornPages.Count - 1;
			target = tornPages[index].GetComponent<Selectable>();
		}
		component.SetNavigation(target, UIDirection.Up, false);
		this.PrimaryAbility.GetComponent<Selectable>().SetNavigation(target, UIDirection.Up, false);
		this.SecondaryAbility.GetComponent<Selectable>().SetNavigation(target, UIDirection.Up, false);
		this.MovementAbility.GetComponent<Selectable>().SetNavigation(target, UIDirection.Up, false);
		if (c.PlayerPages.Count > 0)
		{
			UISelector.SetupHorizontalListNav(this.PlayerPages, component, null, false);
			foreach (GameObject gameObject2 in this.PlayerPages)
			{
				gameObject2.GetComponent<Selectable>().SetNavigation(component, UIDirection.Up, false);
			}
			Selectable component4 = this.PlayerPages[0].GetComponent<Selectable>();
			component.SetNavigation(component4, UIDirection.Down, false);
			this.PrimaryAbility.GetComponent<Selectable>().SetNavigation(component4, UIDirection.Down, false);
			this.SecondaryAbility.GetComponent<Selectable>().SetNavigation(component4, UIDirection.Down, false);
			this.MovementAbility.GetComponent<Selectable>().SetNavigation(component4, UIDirection.Down, false);
		}
		else
		{
			component.SetNavigation(this.PrepareButton, UIDirection.Down, false);
			this.PrimaryAbility.GetComponent<Selectable>().SetNavigation(this.PrepareButton, UIDirection.Down, false);
			this.SecondaryAbility.GetComponent<Selectable>().SetNavigation(this.PrepareButton, UIDirection.Down, false);
			this.MovementAbility.GetComponent<Selectable>().SetNavigation(this.PrepareButton, UIDirection.Down, false);
		}
		if (this.PlayerPages.Count > 0)
		{
			Selectable prepareButton = this.PrepareButton;
			List<GameObject> playerPages = this.PlayerPages;
			int index = playerPages.Count - 1;
			prepareButton.SetNavigation(playerPages[index].GetComponent<Selectable>(), UIDirection.Up, false);
			return;
		}
		this.PrepareButton.SetNavigation(this.PrimaryAbility.GetComponent<Selectable>(), UIDirection.Up, false);
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x00078168 File Offset: 0x00076368
	private void UpdatePrepareButton()
	{
		bool isChallengeActive = GameplayManager.IsChallengeActive;
		this.PrepareText.text = (isChallengeActive ? "Prepared" : "Prepare");
		if (!InputManager.IsUsingController)
		{
			this.PrepareButton.interactable = !isChallengeActive;
		}
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x000781AB File Offset: 0x000763AB
	private void SetupStats()
	{
		if (PanelManager.CurPanel != PanelType.BookClub)
		{
			return;
		}
		this.ShowStats(Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat);
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x000781C0 File Offset: 0x000763C0
	private void ShowStats(Codex_GlobalStatsDisplay.ComparisonType type)
	{
		this.NoDataFader.HideImmediate();
		TextMeshProUGUI statTypeTitle = this.StatTypeTitle;
		string text;
		switch (type)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			text = "Duration";
			goto IL_4D;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			text = "Appendix";
			goto IL_4D;
		case Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat:
			text = BookClubPanel.Challenge.GetStatLabel();
			goto IL_4D;
		}
		text = "";
		IL_4D:
		statTypeTitle.text = text;
		this.CurrentStat = type;
		int left = (type == Codex_GlobalStatsDisplay.ComparisonType.Duration) ? 74 : 35;
		if (this.CurrentStat == Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat)
		{
			if (BookClubPanel.Challenge.StatMax < 100)
			{
				left = 35;
			}
			else if (BookClubPanel.Challenge.StatMax < 1000)
			{
				left = 68;
			}
			else
			{
				left = 86;
			}
		}
		this.statGroup.padding.left = left;
		this.CreateBars();
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x00078280 File Offset: 0x00076480
	private void CreateBars()
	{
		this.ClearBars();
		if (this.TotalStats == 0)
		{
			this.NoDataFader.ShowImmediate();
			return;
		}
		List<Codex_GlobalStatsDisplay.StatBucket> buckets = this.GetBuckets();
		bool flag = GameStats.GetBookClubStat(this.statID, GameStats.BookClubStat.FastestTime, 0) > 0;
		float value = this.MyBestStat();
		float num = 0f;
		foreach (Codex_GlobalStatsDisplay.StatBucket statBucket in buckets)
		{
			num = Mathf.Max(num, (float)statBucket.Count);
		}
		foreach (Codex_GlobalStatsDisplay.StatBucket statBucket2 in buckets)
		{
			bool isSelf = statBucket2.MyValueFits(value) && flag;
			this.CreateBar(statBucket2.Label, Mathf.Sqrt((float)statBucket2.Count), Mathf.Sqrt(num), isSelf);
		}
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x00078380 File Offset: 0x00076580
	private float MyBestStat()
	{
		int num;
		switch (this.CurrentStat)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			num = GameStats.GetBookClubStat(this.statID, GameStats.BookClubStat.FastestTime, 0);
			goto IL_51;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			num = GameStats.GetBookClubStat(this.statID, GameStats.BookClubStat.MaxAppendix, 0);
			goto IL_51;
		case Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat:
			num = GameStats.GetBookClubStat(this.statID, GameStats.BookClubStat.UniqueStat, 0);
			goto IL_51;
		}
		num = 0;
		IL_51:
		return (float)num;
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x000783E0 File Offset: 0x000765E0
	private List<Codex_GlobalStatsDisplay.StatBucket> GetBuckets()
	{
		List<Codex_GlobalStatsDisplay.StatBucket> buckets = this.GetBuckets(this.CurrentStat);
		Dictionary<int, int> dictionary;
		switch (this.CurrentStat)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			dictionary = this.Timers;
			goto IL_4B;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			dictionary = this.Appendix;
			goto IL_4B;
		case Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat:
			dictionary = this.UniqueStat;
			goto IL_4B;
		}
		dictionary = null;
		IL_4B:
		Dictionary<int, int> dictionary2 = dictionary;
		if (dictionary2 == null)
		{
			return buckets;
		}
		int num = 0;
		foreach (KeyValuePair<int, int> keyValuePair in dictionary2)
		{
			num += keyValuePair.Value;
		}
		foreach (Codex_GlobalStatsDisplay.StatBucket statBucket in buckets)
		{
			foreach (KeyValuePair<int, int> keyValuePair2 in dictionary2)
			{
				if (statBucket.ValueFits((float)keyValuePair2.Key))
				{
					statBucket.Count += keyValuePair2.Value;
				}
			}
		}
		return buckets;
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x00078520 File Offset: 0x00076720
	private void CreateBar(string stat, float value, float max, bool isSelf)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.statBarRef, this.statBarHolder);
		gameObject.SetActive(true);
		Codex_RunStatBar component = gameObject.GetComponent<Codex_RunStatBar>();
		component.Setup(stat, value / max, isSelf);
		this.statBarObjects.Add(component);
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x00078564 File Offset: 0x00076764
	private void ClearBars()
	{
		foreach (Codex_RunStatBar codex_RunStatBar in this.statBarObjects)
		{
			UnityEngine.Object.Destroy(codex_RunStatBar.gameObject);
		}
		this.statBarObjects.Clear();
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x000785C4 File Offset: 0x000767C4
	private List<Codex_GlobalStatsDisplay.StatBucket> GetBuckets(Codex_GlobalStatsDisplay.ComparisonType type)
	{
		List<Codex_GlobalStatsDisplay.StatBucket> list = new List<Codex_GlobalStatsDisplay.StatBucket>();
		switch (type)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("< 5:00", 0f, 240f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("5-8:00", 300f, 480f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("8-10:00", 480f, 600f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("10-12:00", 600f, 720f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("12-14:00", 720f, 840f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("14-16:00", 840f, 960f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("16-18:00", 960f, 1080f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("18-20:00", 1080f, 1200f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("20-25:00", 1200f, 1500f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("25:00+", 1500f, 2.1474836E+09f, -1f, -1f));
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("0", -1f, 0f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("1", 1f, 1f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("2", 2f, 2f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("3", 3f, 3f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("4", 4f, 4f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("5", 5f, 5f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("6", 6f, 6f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("7", 7f, 7f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("8", 8f, 8f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("9+", 9f, 5000f, -1f, -1f));
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.ChallengeStat:
			this.CreateBuckets(ref list);
			break;
		}
		return list;
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x000788D4 File Offset: 0x00076AD4
	private void CreateBuckets(ref List<Codex_GlobalStatsDisplay.StatBucket> buckets)
	{
		int statMin = BookClubPanel.Challenge.StatMin;
		int statMax = BookClubPanel.Challenge.StatMax;
		float num = (float)Mathf.FloorToInt((float)(statMax - statMin) / 8f);
		buckets.Add((statMin == 0) ? new Codex_GlobalStatsDisplay.StatBucket("0", 0f, (float)statMin, -1f, -1f) : new Codex_GlobalStatsDisplay.StatBucket("Under" + statMin.ToString(), 0f, (float)statMin, -1f, -1f));
		for (int i = 1; i < 9; i++)
		{
			int num2 = this.RoundToNiceNumber((float)statMin + Mathf.Max((float)(i - 1) * num, 0f), num) + 1;
			int num3 = this.RoundToNiceNumber((float)statMin + (float)i * num, num);
			if (i == 1)
			{
				num2 = statMin + 1;
			}
			if (i == 8)
			{
				num3 = statMax;
			}
			buckets.Add(new Codex_GlobalStatsDisplay.StatBucket(this.FormatNumber(num2) + "-" + this.FormatNumber(num3), (float)num2, (float)num3, -1f, -1f));
		}
		buckets.Add(new Codex_GlobalStatsDisplay.StatBucket(this.FormatNumber(statMax) + "+", (float)(statMax + 1), 2.1474836E+09f, -1f, -1f));
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x00078A08 File Offset: 0x00076C08
	private string FormatNumber(int value)
	{
		if (value < 10000)
		{
			return value.ToString();
		}
		if (value < 1000000)
		{
			return Mathf.FloorToInt((float)(value / 1000)).ToString("0.##") + "k";
		}
		if (value < 1000000000)
		{
			return Mathf.FloorToInt((float)(value / 1000000)).ToString("0.##") + "m";
		}
		return "1b";
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x00078A84 File Offset: 0x00076C84
	private int RoundToNiceNumber(float value, float perBucket)
	{
		int num = Mathf.FloorToInt(value);
		if (perBucket < 10f)
		{
			return num;
		}
		if (perBucket < 100f)
		{
			return BookClubPanel.TruncateToZeros(num, 1);
		}
		if (perBucket < 1000f)
		{
			return BookClubPanel.TruncateToZeros(num, 2);
		}
		if (perBucket < 10000f)
		{
			return BookClubPanel.TruncateToZeros(num, 3);
		}
		if (perBucket < 100000f)
		{
			return BookClubPanel.TruncateToZeros(num, 4);
		}
		return num;
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x00078AE4 File Offset: 0x00076CE4
	public static int TruncateToZeros(int value, int zeroes)
	{
		int num = (int)Mathf.Pow(10f, (float)zeroes);
		return value / num * num;
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x00078B04 File Offset: 0x00076D04
	public void RefreshChallengeStats()
	{
		if (this.isFetching)
		{
			return;
		}
		base.StartCoroutine("FetchStats");
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x00078B1B File Offset: 0x00076D1B
	private IEnumerator FetchStats()
	{
		if (this.isFetching)
		{
			yield break;
		}
		this.isFetching = true;
		Task<JSONNode> task = ParseManager.FetchChallengeStats(this.challengeID, MetaDB.GetCurrentChallengeNumber());
		float safetyTime = 4f;
		while (safetyTime > 0f && !task.IsCompleted)
		{
			safetyTime -= Time.deltaTime;
			yield return true;
		}
		yield return true;
		JSONNode result = task.Result;
		this.isFetching = false;
		this.ReadStatData(result);
		this.SetupStats();
		this.fetchedChallengeID = this.challengeID;
		yield break;
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x00078B2C File Offset: 0x00076D2C
	private void ReadStatData(JSONNode data)
	{
		this.Timers = new Dictionary<int, int>();
		this.Appendix = new Dictionary<int, int>();
		this.UniqueStat = new Dictionary<int, int>();
		this.TotalStats = 0;
		if (data == null)
		{
			return;
		}
		if (data.HasKey("Timers"))
		{
			JSONNode jsonnode = data["Timers"];
			List<int> list = new List<int>();
			foreach (KeyValuePair<string, JSONNode> keyValuePair in jsonnode)
			{
				int item;
				int.TryParse(keyValuePair.Key, out item);
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			list.Sort();
			foreach (int key in list)
			{
				this.Timers.Add(key, jsonnode[key.ToString()].AsInt);
			}
		}
		if (data.HasKey("Appendix"))
		{
			JSONNode jsonnode2 = data["Appendix"];
			List<int> list2 = new List<int>();
			foreach (KeyValuePair<string, JSONNode> keyValuePair2 in jsonnode2)
			{
				int item2;
				int.TryParse(keyValuePair2.Key, out item2);
				list2.Add(item2);
			}
			list2.Sort();
			foreach (int key2 in list2)
			{
				this.Appendix.Add(key2, jsonnode2[key2.ToString()].AsInt);
			}
		}
		if (data.HasKey("SpecialStats"))
		{
			JSONNode jsonnode3 = data["SpecialStats"];
			List<int> list3 = new List<int>();
			foreach (KeyValuePair<string, JSONNode> keyValuePair3 in jsonnode3)
			{
				int item3;
				int.TryParse(keyValuePair3.Key, out item3);
				list3.Add(item3);
			}
			list3.Sort();
			foreach (int key3 in list3)
			{
				this.UniqueStat.Add(key3, jsonnode3[key3.ToString()].AsInt);
			}
		}
		foreach (KeyValuePair<int, int> keyValuePair4 in this.Timers)
		{
			this.TotalStats += keyValuePair4.Value;
		}
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x00078DEC File Offset: 0x00076FEC
	private void UpdateRewards()
	{
		int compositStat = GameStats.GetCompositStat(GameStats.CompositeStat.BookClubsCompleted);
		this.SetupAchievementDisplay(compositStat);
		bool flag = GameStats.GetBookClubStat(this.statID, GameStats.BookClubStat.TimesCompleted, 0) > 0;
		this.FirstBonus.SetActive(!flag);
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x00078E28 File Offset: 0x00077028
	private void SetupAchievementDisplay(int completed)
	{
		int num = this.Achievements.Count - 1;
		for (int i = 0; i < num; i++)
		{
			if (completed < this.Achievements[i].NumRequired)
			{
				num = i;
				break;
			}
		}
		int numRequired = this.Achievements[num].NumRequired;
		Sprite unlockIcon = UnlockDB.GetUnlockIcon(this.Achievements[num].Reward);
		this.CurrentGoalTitle.text = string.Format("Complete <style=\"pos\">{0}</style> Book Clubs", numRequired);
		this.ProgressText.text = string.Format("{0:N0} <color=#4d431f><font=\"Alegreya_Black\">/ {1:N0}</font></color>", completed, numRequired);
		this.ProgressFill.fillAmount = (float)completed / Mathf.Max((float)numRequired, 1f);
		this.CompletedDisplay.SetActive(completed >= numRequired);
		this.RewardIcon.sprite = unlockIcon;
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x00078F04 File Offset: 0x00077104
	public void PrepareChallenge()
	{
		if (GameplayManager.IsChallengeActive)
		{
			return;
		}
		GameplayManager.instance.TrySetChallenge();
		PanelManager.instance.PopPanel();
		PanelManager.instance.PopPanel();
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x00078F2C File Offset: 0x0007712C
	private void OnGenreChanged(GenreTree tome)
	{
		this.UpdatePrepareButton();
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x00078F34 File Offset: 0x00077134
	private void UpdateTimerText()
	{
		if (this.challengeCache != BookClubPanel.Challenge)
		{
			this.LoadChallenge(BookClubPanel.Challenge);
		}
		TimeSpan timeSpan = MetaDB.GetTimeUntilNextChallenge().Add(new TimeSpan(0, 0, 1));
		if (timeSpan.TotalDays >= 1.0)
		{
			int num = timeSpan.Days + 1;
			this.Timer.text = num.ToString() + " Days";
			return;
		}
		if (timeSpan.TotalHours > 0.0)
		{
			this.Timer.text = (timeSpan.Hours + 1).ToString() + " Hours";
			return;
		}
		if (timeSpan.TotalMinutes > 1.0)
		{
			this.Timer.text = timeSpan.Minutes.ToString() + " Minutes";
			return;
		}
		this.Timer.text = 1.ToString() + " Minute";
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x00079036 File Offset: 0x00077236
	public static bool HasSeenCurrentChallenge()
	{
		if (string.IsNullOrEmpty(Progression.LastSeenBookClubChallenge))
		{
			return false;
		}
		MetaDB.BookClubChallenge currentChallenge = MetaDB.CurrentChallenge;
		return ((currentChallenge != null) ? currentChallenge.ID : null) == Progression.LastSeenBookClubChallenge;
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x00079064 File Offset: 0x00077264
	private void OnInputChanged()
	{
		foreach (GameObject gameObject in this.TabControlPrompts)
		{
			gameObject.SetActive(InputManager.IsUsingController);
		}
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x000790BC File Offset: 0x000772BC
	public BookClubPanel()
	{
	}

	// Token: 0x04001262 RID: 4706
	public static BookClubPanel instance;

	// Token: 0x04001263 RID: 4707
	private UIPanel panel;

	// Token: 0x04001264 RID: 4708
	public static MetaDB.BookClubChallenge editorOverrideChallenge;

	// Token: 0x04001265 RID: 4709
	private string challengeID;

	// Token: 0x04001266 RID: 4710
	private string statID;

	// Token: 0x04001267 RID: 4711
	private string fetchedChallengeID;

	// Token: 0x04001268 RID: 4712
	private MetaDB.BookClubChallenge challengeCache;

	// Token: 0x04001269 RID: 4713
	public TextMeshProUGUI Title;

	// Token: 0x0400126A RID: 4714
	public TextMeshProUGUI Timer;

	// Token: 0x0400126B RID: 4715
	public TextMeshProUGUI TomeName;

	// Token: 0x0400126C RID: 4716
	public TextMeshProUGUI DetailText;

	// Token: 0x0400126D RID: 4717
	public TextMeshProUGUI LocalStatText;

	// Token: 0x0400126E RID: 4718
	public Image TomeIcon;

	// Token: 0x0400126F RID: 4719
	public TextMeshProUGUI Difficulty;

	// Token: 0x04001270 RID: 4720
	public Image DifficultyBanner;

	// Token: 0x04001271 RID: 4721
	public List<BookClubPanel.DifficultyBannerDisplay> DifficultyBanners;

	// Token: 0x04001272 RID: 4722
	public List<Image> DifficultyPips;

	// Token: 0x04001273 RID: 4723
	public Button PrepareButton;

	// Token: 0x04001274 RID: 4724
	public TextMeshProUGUI PrepareText;

	// Token: 0x04001275 RID: 4725
	public TextMeshProUGUI BindingText;

	// Token: 0x04001276 RID: 4726
	public GridLayoutGroup BindingLayout;

	// Token: 0x04001277 RID: 4727
	public TextMeshProUGUI BonusBindingLabel;

	// Token: 0x04001278 RID: 4728
	public AugmentTree BonusBindingAugment;

	// Token: 0x04001279 RID: 4729
	public GameObject BonusDisplay;

	// Token: 0x0400127A RID: 4730
	public GameObject BindingDisplayRef;

	// Token: 0x0400127B RID: 4731
	public GameObject TornPageGroup;

	// Token: 0x0400127C RID: 4732
	public GameObject TornPageDisplayRef;

	// Token: 0x0400127D RID: 4733
	private List<GameObject> BindingPages = new List<GameObject>();

	// Token: 0x0400127E RID: 4734
	private List<GameObject> TornPages = new List<GameObject>();

	// Token: 0x0400127F RID: 4735
	public RectTransform LoadoutRect;

	// Token: 0x04001280 RID: 4736
	public float LoadoutMiddleY;

	// Token: 0x04001281 RID: 4737
	public float LoadoutUpperY;

	// Token: 0x04001282 RID: 4738
	public PlayerStatAbilityUIGroup Signature;

	// Token: 0x04001283 RID: 4739
	public Image SignatureIcon;

	// Token: 0x04001284 RID: 4740
	public PlayerStatAbilityUIGroup PrimaryAbility;

	// Token: 0x04001285 RID: 4741
	public PlayerStatAbilityUIGroup SecondaryAbility;

	// Token: 0x04001286 RID: 4742
	public PlayerStatAbilityUIGroup MovementAbility;

	// Token: 0x04001287 RID: 4743
	public GameObject PlayerPageGroup;

	// Token: 0x04001288 RID: 4744
	public GameObject PlayerPageRef;

	// Token: 0x04001289 RID: 4745
	private List<GameObject> PlayerPages = new List<GameObject>();

	// Token: 0x0400128A RID: 4746
	public CanvasGroup StatGroupFader;

	// Token: 0x0400128B RID: 4747
	public GameObject statBarRef;

	// Token: 0x0400128C RID: 4748
	public Transform statBarHolder;

	// Token: 0x0400128D RID: 4749
	public VerticalLayoutGroup statGroup;

	// Token: 0x0400128E RID: 4750
	public TextMeshProUGUI StatTypeTitle;

	// Token: 0x0400128F RID: 4751
	public CanvasGroup NoDataFader;

	// Token: 0x04001290 RID: 4752
	public List<GameObject> TabControlPrompts;

	// Token: 0x04001291 RID: 4753
	public Codex_GlobalStatsDisplay.ComparisonType CurrentStat;

	// Token: 0x04001292 RID: 4754
	public GameObject FirstBonus;

	// Token: 0x04001293 RID: 4755
	public List<BookClubPanel.ChallengeAchievement> Achievements;

	// Token: 0x04001294 RID: 4756
	public TextMeshProUGUI CurrentGoalTitle;

	// Token: 0x04001295 RID: 4757
	public Image RewardIcon;

	// Token: 0x04001296 RID: 4758
	public Image ProgressFill;

	// Token: 0x04001297 RID: 4759
	public TextMeshProUGUI ProgressText;

	// Token: 0x04001298 RID: 4760
	public GameObject CompletedDisplay;

	// Token: 0x04001299 RID: 4761
	private List<Codex_RunStatBar> statBarObjects = new List<Codex_RunStatBar>();

	// Token: 0x0400129A RID: 4762
	private int TotalStats;

	// Token: 0x0400129B RID: 4763
	private Dictionary<int, int> Timers = new Dictionary<int, int>();

	// Token: 0x0400129C RID: 4764
	private Dictionary<int, int> Appendix = new Dictionary<int, int>();

	// Token: 0x0400129D RID: 4765
	private Dictionary<int, int> UniqueStat = new Dictionary<int, int>();

	// Token: 0x0400129E RID: 4766
	private bool isFetching;

	// Token: 0x0200059B RID: 1435
	[Serializable]
	public class DifficultyBannerDisplay
	{
		// Token: 0x0600258A RID: 9610 RVA: 0x000D1C3A File Offset: 0x000CFE3A
		public DifficultyBannerDisplay()
		{
		}

		// Token: 0x040027E6 RID: 10214
		public MetaDB.BookClubChallenge.Difficulty Difficulty;

		// Token: 0x040027E7 RID: 10215
		public string Label;

		// Token: 0x040027E8 RID: 10216
		public Color TextColor;

		// Token: 0x040027E9 RID: 10217
		public Color HSVColor;
	}

	// Token: 0x0200059C RID: 1436
	[Serializable]
	public class ChallengeAchievement
	{
		// Token: 0x0600258B RID: 9611 RVA: 0x000D1C42 File Offset: 0x000CFE42
		public ChallengeAchievement()
		{
		}

		// Token: 0x040027EA RID: 10218
		public AchievementTree Achievement;

		// Token: 0x040027EB RID: 10219
		public int NumRequired;

		// Token: 0x040027EC RID: 10220
		public UnlockCategory Reward;
	}

	// Token: 0x0200059D RID: 1437
	[CompilerGenerated]
	private sealed class <FetchStats>d__88 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600258C RID: 9612 RVA: 0x000D1C4A File Offset: 0x000CFE4A
		[DebuggerHidden]
		public <FetchStats>d__88(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000D1C59 File Offset: 0x000CFE59
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000D1C5C File Offset: 0x000CFE5C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			BookClubPanel bookClubPanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				if (bookClubPanel.isFetching)
				{
					return false;
				}
				bookClubPanel.isFetching = true;
				task = ParseManager.FetchChallengeStats(bookClubPanel.challengeID, MetaDB.GetCurrentChallengeNumber());
				safetyTime = 4f;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
			{
				this.<>1__state = -1;
				JSONNode result = task.Result;
				bookClubPanel.isFetching = false;
				bookClubPanel.ReadStatData(result);
				bookClubPanel.SetupStats();
				bookClubPanel.fetchedChallengeID = bookClubPanel.challengeID;
				return false;
			}
			default:
				return false;
			}
			if (safetyTime <= 0f || task.IsCompleted)
			{
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			safetyTime -= Time.deltaTime;
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x000D1D57 File Offset: 0x000CFF57
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000D1D5F File Offset: 0x000CFF5F
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x000D1D66 File Offset: 0x000CFF66
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040027ED RID: 10221
		private int <>1__state;

		// Token: 0x040027EE RID: 10222
		private object <>2__current;

		// Token: 0x040027EF RID: 10223
		public BookClubPanel <>4__this;

		// Token: 0x040027F0 RID: 10224
		private Task<JSONNode> <task>5__2;

		// Token: 0x040027F1 RID: 10225
		private float <safetyTime>5__3;
	}
}
