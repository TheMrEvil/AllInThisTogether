using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F9 RID: 505
public class CodexStatsPanel : MonoBehaviour
{
	// Token: 0x06001586 RID: 5510 RVA: 0x00087CE0 File Offset: 0x00085EE0
	private void Awake()
	{
		CodexStatsPanel.instance = this;
		using (List<CodexStatsPanel.StatsPanelTab>.Enumerator enumerator = this.Tabs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CodexStatsPanel.StatsPanelTab v = enumerator.Current;
				v.TabButton.onClick.AddListener(delegate()
				{
					this.SetTab(v.Setting);
				});
				v.SelectedGroup.alpha = 0f;
			}
		}
		this.panel = base.GetComponent<UIPanel>();
		UIPanel uipanel = this.panel;
		uipanel.OnNextTab = (Action)Delegate.Combine(uipanel.OnNextTab, new Action(this.NextTab));
		UIPanel uipanel2 = this.panel;
		uipanel2.OnPrevTab = (Action)Delegate.Combine(uipanel2.OnPrevTab, new Action(this.PrevTab));
		UIPanel uipanel3 = this.panel;
		uipanel3.OnPageNext = (Action)Delegate.Combine(uipanel3.OnPageNext, new Action(this.OnNextPage));
		UIPanel uipanel4 = this.panel;
		uipanel4.OnPagePrev = (Action)Delegate.Combine(uipanel4.OnPagePrev, new Action(this.OnPrevPage));
		UIPanel uipanel5 = this.panel;
		uipanel5.OnEnteredPanel = (Action)Delegate.Combine(uipanel5.OnEnteredPanel, new Action(this.OnEnteredPanel));
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.OnInputChanged));
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x00087E6C File Offset: 0x0008606C
	private void OnEnteredPanel()
	{
		if (!this.didInit)
		{
			this.SetTab(CodexStatsPanel.StatsTab.Overview);
		}
		this.RunHistory.LoadRuns();
		if (this.currentSetting == CodexStatsPanel.StatsTab.RunHistory && InputManager.IsUsingController)
		{
			this.RunHistory.AutoSelect();
		}
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x00087EA4 File Offset: 0x000860A4
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Codex_Stats)
		{
			return;
		}
		if (this.currentSetting == CodexStatsPanel.StatsTab.RunHistory)
		{
			this.RunHistory.AutoScroll.TickUpdate();
		}
		if (InputManager.UIAct.UIBack.WasPressed)
		{
			base.StartCoroutine("GoBack");
		}
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x00087EF1 File Offset: 0x000860F1
	private IEnumerator GoBack()
	{
		yield return new WaitForEndOfFrame();
		PanelManager.instance.PopPanel();
		yield break;
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x00087EFC File Offset: 0x000860FC
	private void SetTab(CodexStatsPanel.StatsTab t)
	{
		foreach (CodexStatsPanel.StatsPanelTab statsPanelTab in this.Tabs)
		{
			statsPanelTab.SelectedGroup.alpha = ((t == statsPanelTab.Setting) ? 1f : 0f);
		}
		this.currentSetting = t;
		this.Overview.ToggleVisibility(t == CodexStatsPanel.StatsTab.Overview);
		this.Signatures.ToggleVisibility(t == CodexStatsPanel.StatsTab.Signatures);
		this.RunHistory.ToggleVisibility(t == CodexStatsPanel.StatsTab.RunHistory);
		this.didInit = true;
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x00087FA4 File Offset: 0x000861A4
	private void NextTab()
	{
		int num = this.CurrentTabIndex();
		num++;
		if (num >= this.Tabs.Count)
		{
			num = 0;
		}
		this.SetTab(this.Tabs[num].Setting);
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x00087FE4 File Offset: 0x000861E4
	private void PrevTab()
	{
		int num = this.CurrentTabIndex();
		num--;
		if (num < 0)
		{
			num = this.Tabs.Count - 1;
		}
		this.SetTab(this.Tabs[num].Setting);
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x00088028 File Offset: 0x00086228
	private int CurrentTabIndex()
	{
		for (int i = 0; i < this.Tabs.Count; i++)
		{
			if (this.Tabs[i].Setting == this.currentSetting)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x00088067 File Offset: 0x00086267
	private void OnNextPage()
	{
		if (this.currentSetting == CodexStatsPanel.StatsTab.Signatures)
		{
			this.Signatures.NextPage();
			return;
		}
		if (this.currentSetting == CodexStatsPanel.StatsTab.RunHistory)
		{
			this.RunHistory.NextPage();
		}
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x00088092 File Offset: 0x00086292
	private void OnPrevPage()
	{
		if (this.currentSetting == CodexStatsPanel.StatsTab.Signatures)
		{
			this.Signatures.PrevPage();
			return;
		}
		if (this.currentSetting == CodexStatsPanel.StatsTab.RunHistory)
		{
			this.RunHistory.PrevPage();
		}
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x000880C0 File Offset: 0x000862C0
	private void OnInputChanged()
	{
		foreach (GameObject gameObject in this.TabControlPrompts)
		{
			gameObject.SetActive(InputManager.IsUsingController);
		}
		this.Signatures.OnInputChanged();
		this.RunHistory.OnInputChanged();
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x0008812C File Offset: 0x0008632C
	public CodexStatsPanel()
	{
	}

	// Token: 0x0400152F RID: 5423
	public static CodexStatsPanel instance;

	// Token: 0x04001530 RID: 5424
	private UIPanel panel;

	// Token: 0x04001531 RID: 5425
	public List<CodexStatsPanel.StatsPanelTab> Tabs;

	// Token: 0x04001532 RID: 5426
	public List<GameObject> TabControlPrompts;

	// Token: 0x04001533 RID: 5427
	public CodexStatsPanel.StatsTab currentSetting;

	// Token: 0x04001534 RID: 5428
	public CodexStats_Overview Overview;

	// Token: 0x04001535 RID: 5429
	public CodexStats_Signatures Signatures;

	// Token: 0x04001536 RID: 5430
	public CodexStats_History RunHistory;

	// Token: 0x04001537 RID: 5431
	private bool didInit;

	// Token: 0x020005D4 RID: 1492
	public enum StatsTab
	{
		// Token: 0x040028C3 RID: 10435
		Overview,
		// Token: 0x040028C4 RID: 10436
		Signatures,
		// Token: 0x040028C5 RID: 10437
		RunHistory
	}

	// Token: 0x020005D5 RID: 1493
	[Serializable]
	public class StatsPanelTab
	{
		// Token: 0x06002644 RID: 9796 RVA: 0x000D3418 File Offset: 0x000D1618
		public StatsPanelTab()
		{
		}

		// Token: 0x040028C6 RID: 10438
		public CodexStatsPanel.StatsTab Setting;

		// Token: 0x040028C7 RID: 10439
		public CanvasGroup SelectedGroup;

		// Token: 0x040028C8 RID: 10440
		public Button TabButton;
	}

	// Token: 0x020005D6 RID: 1494
	[CompilerGenerated]
	private sealed class <>c__DisplayClass9_0
	{
		// Token: 0x06002645 RID: 9797 RVA: 0x000D3420 File Offset: 0x000D1620
		public <>c__DisplayClass9_0()
		{
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000D3428 File Offset: 0x000D1628
		internal void <Awake>b__0()
		{
			this.<>4__this.SetTab(this.v.Setting);
		}

		// Token: 0x040028C9 RID: 10441
		public CodexStatsPanel.StatsPanelTab v;

		// Token: 0x040028CA RID: 10442
		public CodexStatsPanel <>4__this;
	}

	// Token: 0x020005D7 RID: 1495
	[CompilerGenerated]
	private sealed class <GoBack>d__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002647 RID: 9799 RVA: 0x000D3440 File Offset: 0x000D1640
		[DebuggerHidden]
		public <GoBack>d__12(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000D344F File Offset: 0x000D164F
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000D3454 File Offset: 0x000D1654
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

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x000D349E File Offset: 0x000D169E
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000D34A6 File Offset: 0x000D16A6
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x000D34AD File Offset: 0x000D16AD
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028CB RID: 10443
		private int <>1__state;

		// Token: 0x040028CC RID: 10444
		private object <>2__current;
	}
}
