using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F7 RID: 503
public class CodexEnemyPanel : MonoBehaviour
{
	// Token: 0x06001561 RID: 5473 RVA: 0x00086914 File Offset: 0x00084B14
	private void Awake()
	{
		CodexEnemyPanel.instance = this;
		this.SetupTabs();
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnteredPanel));
		component.OnPageNext = (Action)Delegate.Combine(component.OnPageNext, new Action(this.NextTab));
		component.OnPagePrev = (Action)Delegate.Combine(component.OnPagePrev, new Action(this.PrevTab));
		component.OnNextTab = (Action)Delegate.Combine(component.OnNextTab, new Action(this.NextInfoType));
		component.OnPrevTab = (Action)Delegate.Combine(component.OnPrevTab, new Action(this.PrevInfoType));
		this.ListEnemyRef.SetActive(false);
		this.ListCategoryRef.SetActive(false);
		this.AbilityInfoRef.SetActive(false);
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.OnInputChanged));
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x00086A20 File Offset: 0x00084C20
	private void OnEnteredPanel()
	{
		if (RaidManager.IsInRaid)
		{
			this.SelectTab(EnemyLevel.Minion);
			UITutorial.TryTutorial(UITutorial.Tutorial.RaidCodex);
			return;
		}
		this.SelectTab(EnemyLevel.Default);
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x00086A40 File Offset: 0x00084C40
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Codex_Enemies)
		{
			return;
		}
		if (InputManager.UIAct.UIBack.WasPressed)
		{
			base.StartCoroutine("GoBack");
		}
		if (InputManager.IsUsingController)
		{
			this.AutoScroller.TickUpdate();
		}
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x00086A7B File Offset: 0x00084C7B
	private IEnumerator GoBack()
	{
		yield return new WaitForEndOfFrame();
		PanelManager.instance.PopPanel();
		yield break;
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x00086A84 File Offset: 0x00084C84
	private void SetupTabs()
	{
		foreach (CodexEnemyPanel.EnemyTypeTab enemyTypeTab in this.Tabs)
		{
			EnemyLevel level = enemyTypeTab.Level;
			enemyTypeTab.ButtonRef.onClick.AddListener(delegate()
			{
				this.SelectTab(level);
			});
		}
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x00086B08 File Offset: 0x00084D08
	private void NextTab()
	{
		int num = 0;
		CodexEnemyPanel.EnemyTypeTab tab = this.GetTab(this.curLevel);
		if (tab != null)
		{
			num = this.Tabs.IndexOf(tab);
		}
		num++;
		if (num >= this.Tabs.Count)
		{
			num = 0;
		}
		this.SelectTab(this.Tabs[num].Level);
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x00086B60 File Offset: 0x00084D60
	private void PrevTab()
	{
		int num = 0;
		CodexEnemyPanel.EnemyTypeTab tab = this.GetTab(this.curLevel);
		if (tab != null)
		{
			num = this.Tabs.IndexOf(tab);
		}
		num--;
		if (num <= 0)
		{
			num = this.Tabs.Count - 1;
		}
		this.SelectTab(this.Tabs[num].Level);
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x00086BBC File Offset: 0x00084DBC
	private void SelectTab(EnemyLevel level)
	{
		CodexEnemyPanel.EnemyTypeTab tab = this.GetTab(level);
		foreach (CodexEnemyPanel.EnemyTypeTab enemyTypeTab in this.Tabs)
		{
			enemyTypeTab.SelectedDisplay.SetActive(enemyTypeTab == tab);
		}
		if (tab == null)
		{
			return;
		}
		this.curLevel = level;
		this.GroupTitle.text = tab.Title;
		this.SetupEnemyList(level);
		bool flag = false;
		if (RaidManager.IsInRaid)
		{
			foreach (CodexEnemyListItem codexEnemyListItem in this.EnemyList)
			{
				if (codexEnemyListItem.HasSeen && codexEnemyListItem.Enemy.IDMatches(RaidManager.instance.CurrentEncounter))
				{
					this.SelectEnemy(codexEnemyListItem);
					float num = 1f - (float)this.EnemyList.IndexOf(codexEnemyListItem) / (float)this.EnemyList.Count;
					num += ((num > 0.5f) ? 0.25f : -0.25f);
					num = Mathf.Clamp01(num);
					this.Scroll.verticalNormalizedPosition = num;
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			foreach (CodexEnemyListItem codexEnemyListItem2 in this.EnemyList)
			{
				if (codexEnemyListItem2.HasSeen)
				{
					this.SelectEnemy(codexEnemyListItem2);
					break;
				}
			}
		}
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x00086D64 File Offset: 0x00084F64
	private CodexEnemyPanel.EnemyTypeTab GetTab(EnemyLevel level)
	{
		foreach (CodexEnemyPanel.EnemyTypeTab enemyTypeTab in this.Tabs)
		{
			if (enemyTypeTab.Level == level)
			{
				return enemyTypeTab;
			}
		}
		return null;
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x00086DC0 File Offset: 0x00084FC0
	private void SetupEnemyList(EnemyLevel level)
	{
		foreach (GameObject obj in this.FullEnemyList)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.FullEnemyList.Clear();
		this.EnemyList.Clear();
		List<AIData.EnemyCodexEntry> enemies = AIManager.instance.DB.GetEnemies(level);
		if (level == EnemyLevel.Minion)
		{
			enemies.Sort((AIData.EnemyCodexEntry a, AIData.EnemyCodexEntry b) => CodexEnemyPanel.<SetupEnemyList>g__GetPriority|44_2(a.Type).CompareTo(CodexEnemyPanel.<SetupEnemyList>g__GetPriority|44_2(b.Type)));
		}
		else
		{
			enemies.Sort((AIData.EnemyCodexEntry a, AIData.EnemyCodexEntry b) => a.Type.CompareTo(b.Type));
		}
		EnemyType enemyType = EnemyType._;
		int count = enemies.Count;
		int num = 0;
		foreach (AIData.EnemyCodexEntry enemyCodexEntry in enemies)
		{
			EnemyType type = enemyCodexEntry.Type;
			if (enemyType != type)
			{
				enemyType = type;
				this.AddEnemyTypeBreak(enemyType);
			}
			ValueTuple<int, int> enemyStat = enemyCodexEntry.GetEnemyStat(level == EnemyLevel.Minion);
			int item = enemyStat.Item1;
			int item2 = enemyStat.Item2;
			bool flag = item > 0 || item2 > 0;
			if (flag)
			{
				num++;
			}
			this.AddEnemySelector(enemyCodexEntry, flag);
		}
		UISelector.SetupVerticalListNav<CodexEnemyListItem>(this.EnemyList, null, null, true);
		this.GroupCount.text = num.ToString() + " / " + count.ToString();
		this.GroupProgressFill.fillAmount = Mathf.Clamp01((float)num / (float)count);
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x00086F64 File Offset: 0x00085164
	private void AddEnemyTypeBreak(EnemyType type)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListCategoryRef, this.ListEnemyRef.transform.parent);
		gameObject.SetActive(true);
		string nameOverride = "";
		if (this.curLevel == EnemyLevel.Minion)
		{
			nameOverride = RaidDB.GetRaidByEnemyType(type).RaidName;
		}
		gameObject.GetComponent<CodexEnemyListCategory>().Setup(type, nameOverride);
		this.FullEnemyList.Add(gameObject);
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x00086FC8 File Offset: 0x000851C8
	private void AddEnemySelector(AIData.EnemyCodexEntry entry, bool hasSeen)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListEnemyRef, this.ListEnemyRef.transform.parent);
		gameObject.SetActive(true);
		CodexEnemyListItem component = gameObject.GetComponent<CodexEnemyListItem>();
		component.Setup(entry, hasSeen);
		this.FullEnemyList.Add(gameObject);
		this.EnemyList.Add(component);
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x00087020 File Offset: 0x00085220
	public void SelectEnemy(CodexEnemyListItem enemy)
	{
		if (this.SelectedEnemy == enemy)
		{
			return;
		}
		this.SelectedEnemy = enemy;
		foreach (CodexEnemyListItem codexEnemyListItem in this.EnemyList)
		{
			codexEnemyListItem.SelectedDisplay.SetActive(codexEnemyListItem == this.SelectedEnemy);
		}
		AIData.EnemyCodexEntry enemy2 = this.SelectedEnemy.Enemy;
		this.EnemyName.text = enemy2.Name;
		ValueTuple<int, int> enemyStat = enemy2.GetEnemyStat(this.curLevel == EnemyLevel.Minion);
		int item = enemyStat.Item1;
		int item2 = enemyStat.Item2;
		this.StatKillsLabel.text = item.ToString();
		this.StatKilledByLabel.text = item2.ToString();
		this.EnemyImage.sprite = enemy2.Portrait;
		this.curPage = 0;
		this.LoadInfo();
		this.InfoSelectorGroup.SetActive(enemy2.Pages.Count > 0);
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x00087130 File Offset: 0x00085330
	public void NextInfoType()
	{
		this.curPage++;
		if (this.curPage >= this.SelectedEnemy.Enemy.Pages.Count)
		{
			this.curPage = 0;
		}
		this.LoadInfo();
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x0008716A File Offset: 0x0008536A
	public void PrevInfoType()
	{
		this.curPage--;
		if (this.curPage < 0)
		{
			this.curPage = this.SelectedEnemy.Enemy.Pages.Count - 1;
		}
		this.LoadInfo();
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x000871A6 File Offset: 0x000853A6
	private void LoadInfo(int page)
	{
		this.curPage = Mathf.Clamp(page, 0, this.SelectedEnemy.Enemy.Pages.Count - 1);
		this.LoadInfo();
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x000871D2 File Offset: 0x000853D2
	private void LoadInfo()
	{
		this.LoadOverview();
		if (this.SelectedEnemy.Enemy.Pages.Count > 0)
		{
			this.SetupNavigationPips();
		}
		base.StartCoroutine(this.InfoLayoutDelayed());
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x00087208 File Offset: 0x00085408
	private void LoadOverview()
	{
		AIData.EnemyCodexEntry enemy = this.SelectedEnemy.Enemy;
		if (enemy == null)
		{
			return;
		}
		this.ActivatePageElements(enemy);
		this.OverviewSummary.text = TextParser.AugmentDetail(enemy.TopDetail, null, null, false);
		this.OverviewFamilyIcon.sprite = AIManager.instance.DB.GetFamilyData(enemy.Type).FamilySprite;
		this.OverviewFamily.text = enemy.Type.ToString();
		this.OverviewFamilySummary.text = TextParser.AugmentDetail(enemy.TypeDetail, null, null, false);
		if (enemy.Tips.Count > 0 && this.TipsHeader.activeSelf)
		{
			string text = "";
			foreach (string input in enemy.Tips)
			{
				text = text + "- " + TextParser.AugmentDetail(input, null, null, false) + "\n\n";
			}
			this.OverviewTips.text = text;
		}
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x00087328 File Offset: 0x00085528
	private void LoadAbilities(int start = -1, int end = -1)
	{
		AIData.EnemyCodexEntry enemy = this.SelectedEnemy.Enemy;
		if (enemy == null)
		{
			return;
		}
		List<AIData.EnemyCodexAbility> list = enemy.Abilities;
		if (start >= 0 || end >= 0)
		{
			list = enemy.Abilities.GetRange(Mathf.Max(0, start - 1), Mathf.Min(end - start, enemy.Abilities.Count + 1 - start));
		}
		foreach (AIData.EnemyCodexAbility ability in list)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AbilityInfoRef, this.AbilityInfoRef.transform.parent);
			gameObject.SetActive(true);
			gameObject.GetComponent<CodexEnemyAbilityItem>().Setup(ability, this.curLevel == EnemyLevel.Minion);
			this.AbilityInfoRefs.Add(gameObject);
		}
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x00087404 File Offset: 0x00085604
	private void ActivatePageElements(AIData.EnemyCodexEntry e)
	{
		foreach (GameObject obj in this.AbilityInfoRefs)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.AbilityInfoRefs.Clear();
		if (this.SelectedEnemy.Enemy.Pages.Count == 0)
		{
			this.OverviewSummary.gameObject.SetActive(true);
			bool active = e.Tips.Count > 0;
			this.TipsHeader.SetActive(active);
			this.OverviewTips.gameObject.SetActive(active);
			this.FamilyHeader.SetActive(true);
			this.OverviewFamilySummary.gameObject.SetActive(true);
			if (e.Abilities.Count > 0)
			{
				this.LoadAbilities(-1, -1);
				return;
			}
		}
		else
		{
			AIData.EnemyCodexPageInfo enemyCodexPageInfo = this.SelectedEnemy.Enemy.Pages[this.curPage];
			bool active2 = enemyCodexPageInfo.Start <= AIData.EnemyCodexPageInfo.PageFeature.Description;
			this.OverviewSummary.gameObject.SetActive(active2);
			bool flag = enemyCodexPageInfo.Start <= AIData.EnemyCodexPageInfo.PageFeature.Tips;
			flag &= (enemyCodexPageInfo.End >= AIData.EnemyCodexPageInfo.PageFeature.Tips);
			flag &= (e.Tips.Count > 0);
			this.TipsHeader.SetActive(flag);
			this.OverviewTips.gameObject.SetActive(flag);
			bool flag2 = enemyCodexPageInfo.Start <= AIData.EnemyCodexPageInfo.PageFeature.FamilyInfo;
			flag2 &= (enemyCodexPageInfo.End >= AIData.EnemyCodexPageInfo.PageFeature.FamilyInfo);
			this.FamilyHeader.SetActive(flag2);
			this.OverviewFamilySummary.gameObject.SetActive(flag2);
			if (e.Abilities.Count > 0)
			{
				int start = (enemyCodexPageInfo.Start == AIData.EnemyCodexPageInfo.PageFeature.Ability) ? enemyCodexPageInfo.StartAbility : 0;
				int endAbility = enemyCodexPageInfo.EndAbility;
				if (enemyCodexPageInfo.End >= AIData.EnemyCodexPageInfo.PageFeature.Ability)
				{
					this.LoadAbilities(start, endAbility);
				}
			}
		}
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x000875F0 File Offset: 0x000857F0
	private void SetupNavigationPips()
	{
		foreach (GameObject obj in this.pips)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.pips.Clear();
		for (int i = 0; i < this.SelectedEnemy.Enemy.Pages.Count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PipRef, this.PipRef.transform.parent);
			gameObject.SetActive(true);
			int x = i;
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.LoadInfo(x);
			});
			this.pips.Add(gameObject);
			gameObject.GetComponent<Image>().sprite = ((i == this.curPage) ? this.Pip_Filled : this.Pip_Empty);
		}
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x000876F4 File Offset: 0x000858F4
	private IEnumerator InfoLayoutDelayed()
	{
		this.RebuildInfoLayout();
		this.RebuildInfoLayout();
		yield return true;
		this.RebuildInfoLayout();
		yield break;
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x00087703 File Offset: 0x00085903
	private void RebuildInfoLayout()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.OverviewRect);
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x00087710 File Offset: 0x00085910
	private void OnInputChanged()
	{
		bool isUsingController = InputManager.IsUsingController;
		foreach (GameObject gameObject in this.TabControlPrompts)
		{
			gameObject.SetActive(isUsingController);
		}
	}

	// Token: 0x06001579 RID: 5497 RVA: 0x00087768 File Offset: 0x00085968
	public CodexEnemyPanel()
	{
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x0008779C File Offset: 0x0008599C
	[CompilerGenerated]
	internal static int <SetupEnemyList>g__GetPriority|44_2(EnemyType type)
	{
		if ((type & EnemyType.Raving) != EnemyType.Any)
		{
			return 0;
		}
		if ((type & EnemyType.Splice) != EnemyType.Any)
		{
			return 1;
		}
		if ((type & EnemyType.Tangent) != EnemyType.Any)
		{
			return 2;
		}
		return 3;
	}

	// Token: 0x040014FF RID: 5375
	public static CodexEnemyPanel instance;

	// Token: 0x04001500 RID: 5376
	public List<CodexEnemyPanel.EnemyTypeTab> Tabs;

	// Token: 0x04001501 RID: 5377
	public List<GameObject> TabControlPrompts;

	// Token: 0x04001502 RID: 5378
	public TextMeshProUGUI GroupTitle;

	// Token: 0x04001503 RID: 5379
	public TextMeshProUGUI GroupCount;

	// Token: 0x04001504 RID: 5380
	public Image GroupProgressFill;

	// Token: 0x04001505 RID: 5381
	public ScrollRect Scroll;

	// Token: 0x04001506 RID: 5382
	public AutoScrollRect AutoScroller;

	// Token: 0x04001507 RID: 5383
	public GameObject ListEnemyRef;

	// Token: 0x04001508 RID: 5384
	public GameObject ListCategoryRef;

	// Token: 0x04001509 RID: 5385
	private List<GameObject> FullEnemyList = new List<GameObject>();

	// Token: 0x0400150A RID: 5386
	private List<CodexEnemyListItem> EnemyList = new List<CodexEnemyListItem>();

	// Token: 0x0400150B RID: 5387
	private CodexEnemyListItem SelectedEnemy;

	// Token: 0x0400150C RID: 5388
	private EnemyLevel curLevel;

	// Token: 0x0400150D RID: 5389
	public TextMeshProUGUI EnemyName;

	// Token: 0x0400150E RID: 5390
	public Image EnemyImage;

	// Token: 0x0400150F RID: 5391
	public TextMeshProUGUI StatKillsLabel;

	// Token: 0x04001510 RID: 5392
	public TextMeshProUGUI StatKilledByLabel;

	// Token: 0x04001511 RID: 5393
	public GameObject OverviewHolder;

	// Token: 0x04001512 RID: 5394
	public RectTransform OverviewRect;

	// Token: 0x04001513 RID: 5395
	public TextMeshProUGUI OverviewSummary;

	// Token: 0x04001514 RID: 5396
	public GameObject FamilyHeader;

	// Token: 0x04001515 RID: 5397
	public Image OverviewFamilyIcon;

	// Token: 0x04001516 RID: 5398
	public GameObject TipsHeader;

	// Token: 0x04001517 RID: 5399
	public TextMeshProUGUI OverviewTips;

	// Token: 0x04001518 RID: 5400
	public TextMeshProUGUI OverviewFamily;

	// Token: 0x04001519 RID: 5401
	public TextMeshProUGUI OverviewFamilySummary;

	// Token: 0x0400151A RID: 5402
	public GameObject AbilityInfoRef;

	// Token: 0x0400151B RID: 5403
	private List<GameObject> AbilityInfoRefs = new List<GameObject>();

	// Token: 0x0400151C RID: 5404
	private int curPage;

	// Token: 0x0400151D RID: 5405
	public GameObject InfoSelectorGroup;

	// Token: 0x0400151E RID: 5406
	public GameObject PipRef;

	// Token: 0x0400151F RID: 5407
	public Sprite Pip_Filled;

	// Token: 0x04001520 RID: 5408
	public Sprite Pip_Empty;

	// Token: 0x04001521 RID: 5409
	private List<GameObject> pips = new List<GameObject>();

	// Token: 0x020005C9 RID: 1481
	[Serializable]
	public class EnemyTypeTab
	{
		// Token: 0x06002621 RID: 9761 RVA: 0x000D31AC File Offset: 0x000D13AC
		public EnemyTypeTab()
		{
		}

		// Token: 0x040028A8 RID: 10408
		public GameObject SelectedDisplay;

		// Token: 0x040028A9 RID: 10409
		public Button ButtonRef;

		// Token: 0x040028AA RID: 10410
		public EnemyLevel Level;

		// Token: 0x040028AB RID: 10411
		public string Title;
	}

	// Token: 0x020005CA RID: 1482
	[CompilerGenerated]
	private sealed class <GoBack>d__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002622 RID: 9762 RVA: 0x000D31B4 File Offset: 0x000D13B4
		[DebuggerHidden]
		public <GoBack>d__38(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000D31C3 File Offset: 0x000D13C3
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000D31C8 File Offset: 0x000D13C8
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

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x000D3212 File Offset: 0x000D1412
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000D321A File Offset: 0x000D141A
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x000D3221 File Offset: 0x000D1421
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028AC RID: 10412
		private int <>1__state;

		// Token: 0x040028AD RID: 10413
		private object <>2__current;
	}

	// Token: 0x020005CB RID: 1483
	[CompilerGenerated]
	private sealed class <>c__DisplayClass39_0
	{
		// Token: 0x06002628 RID: 9768 RVA: 0x000D3229 File Offset: 0x000D1429
		public <>c__DisplayClass39_0()
		{
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000D3231 File Offset: 0x000D1431
		internal void <SetupTabs>b__0()
		{
			this.<>4__this.SelectTab(this.level);
		}

		// Token: 0x040028AE RID: 10414
		public EnemyLevel level;

		// Token: 0x040028AF RID: 10415
		public CodexEnemyPanel <>4__this;
	}

	// Token: 0x020005CC RID: 1484
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600262A RID: 9770 RVA: 0x000D3244 File Offset: 0x000D1444
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000D3250 File Offset: 0x000D1450
		public <>c()
		{
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000D3258 File Offset: 0x000D1458
		internal int <SetupEnemyList>b__44_0(AIData.EnemyCodexEntry a, AIData.EnemyCodexEntry b)
		{
			return CodexEnemyPanel.<SetupEnemyList>g__GetPriority|44_2(a.Type).CompareTo(CodexEnemyPanel.<SetupEnemyList>g__GetPriority|44_2(b.Type));
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000D3283 File Offset: 0x000D1483
		internal int <SetupEnemyList>b__44_1(AIData.EnemyCodexEntry a, AIData.EnemyCodexEntry b)
		{
			return a.Type.CompareTo(b.Type);
		}

		// Token: 0x040028B0 RID: 10416
		public static readonly CodexEnemyPanel.<>c <>9 = new CodexEnemyPanel.<>c();

		// Token: 0x040028B1 RID: 10417
		public static Comparison<AIData.EnemyCodexEntry> <>9__44_0;

		// Token: 0x040028B2 RID: 10418
		public static Comparison<AIData.EnemyCodexEntry> <>9__44_1;
	}

	// Token: 0x020005CD RID: 1485
	[CompilerGenerated]
	private sealed class <>c__DisplayClass55_0
	{
		// Token: 0x0600262E RID: 9774 RVA: 0x000D32A1 File Offset: 0x000D14A1
		public <>c__DisplayClass55_0()
		{
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000D32A9 File Offset: 0x000D14A9
		internal void <SetupNavigationPips>b__0()
		{
			this.<>4__this.LoadInfo(this.x);
		}

		// Token: 0x040028B3 RID: 10419
		public int x;

		// Token: 0x040028B4 RID: 10420
		public CodexEnemyPanel <>4__this;
	}

	// Token: 0x020005CE RID: 1486
	[CompilerGenerated]
	private sealed class <InfoLayoutDelayed>d__56 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002630 RID: 9776 RVA: 0x000D32BC File Offset: 0x000D14BC
		[DebuggerHidden]
		public <InfoLayoutDelayed>d__56(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000D32CB File Offset: 0x000D14CB
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000D32D0 File Offset: 0x000D14D0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			CodexEnemyPanel codexEnemyPanel = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				codexEnemyPanel.RebuildInfoLayout();
				codexEnemyPanel.RebuildInfoLayout();
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			codexEnemyPanel.RebuildInfoLayout();
			return false;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x000D332A File Offset: 0x000D152A
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x000D3332 File Offset: 0x000D1532
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000D3339 File Offset: 0x000D1539
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028B5 RID: 10421
		private int <>1__state;

		// Token: 0x040028B6 RID: 10422
		private object <>2__current;

		// Token: 0x040028B7 RID: 10423
		public CodexEnemyPanel <>4__this;
	}
}
