using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F8 RID: 504
public class CodexLorePanel : MonoBehaviour
{
	// Token: 0x0600157B RID: 5499 RVA: 0x000877B4 File Offset: 0x000859B4
	private void Awake()
	{
		CodexLorePanel.instance = this;
		this.panel = base.GetComponent<UIPanel>();
		UIPanel uipanel = this.panel;
		uipanel.OnNextTab = (Action)Delegate.Combine(uipanel.OnNextTab, new Action(this.NextTab));
		UIPanel uipanel2 = this.panel;
		uipanel2.OnPrevTab = (Action)Delegate.Combine(uipanel2.OnPrevTab, new Action(this.PrevTab));
		UIPanel uipanel3 = this.panel;
		uipanel3.OnEnteredPanel = (Action)Delegate.Combine(uipanel3.OnEnteredPanel, new Action(this.OnEnteredPanel));
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.OnInputChanged));
		this.PageItemRef.SetActive(false);
		foreach (CodexLorePanel.LoreTab loreTab in this.Tabs)
		{
			LoreDB.Character c = loreTab.Character;
			loreTab.Button.onClick.AddListener(delegate()
			{
				this.SelectTab(c);
			});
		}
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x000878E8 File Offset: 0x00085AE8
	private void OnEnteredPanel()
	{
		this.SelectTab(this.Tabs[0].Character);
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x00087901 File Offset: 0x00085B01
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.Codex_Journal)
		{
			return;
		}
		if (InputManager.UIAct.UIBack.WasPressed)
		{
			base.StartCoroutine("GoBack");
		}
		if (InputManager.IsUsingController)
		{
			this.PageScroller.TickUpdate();
		}
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x0008793C File Offset: 0x00085B3C
	private IEnumerator GoBack()
	{
		yield return new WaitForEndOfFrame();
		PanelManager.instance.PopPanel();
		yield break;
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x00087944 File Offset: 0x00085B44
	public void NextTab()
	{
		int num = this.Tabs.IndexOf(this.curTab);
		num++;
		if (num >= this.Tabs.Count)
		{
			num = 0;
		}
		this.SelectTab(this.Tabs[num].Character);
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x00087990 File Offset: 0x00085B90
	public void PrevTab()
	{
		int num = this.Tabs.IndexOf(this.curTab);
		num--;
		if (num < 0)
		{
			num = this.Tabs.Count - 1;
		}
		this.SelectTab(this.Tabs[num].Character);
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000879DC File Offset: 0x00085BDC
	private void SelectTab(LoreDB.Character character)
	{
		foreach (CodexLorePanel.LoreTab loreTab in this.Tabs)
		{
			loreTab.SelectedDisplay.alpha = (float)((loreTab.Character == character) ? 1 : 0);
			if (loreTab.Character == character)
			{
				this.curTab = loreTab;
			}
		}
		this.LoadPages(character);
		TMP_Text characterName = this.CharacterName;
		LoreDB.CharacterInfo character2 = LoreDB.GetCharacter(character, LoreDB.Era.OldWorld);
		characterName.text = (((character2 != null) ? character2.Name : null) ?? "Unknown");
		int count = this.PageItems.Count;
		int num = 0;
		using (List<GameObject>.Enumerator enumerator2 = this.PageItems.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.GetComponent<CodexLorePageSelector>().HasSeenPage)
				{
					num++;
				}
			}
		}
		this.PageCountText.text = string.Format("{0} / {1}", num, count);
		this.PageCountBar.fillAmount = (float)num / (float)count;
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x00087B0C File Offset: 0x00085D0C
	private void LoadPages(LoreDB.Character character)
	{
		foreach (GameObject obj in this.PageItems)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.PageItems.Clear();
		List<LoreDB.LorePage> list = LoreDB.instance.Pages.FindAll((LoreDB.LorePage v) => v.Character == character);
		list.Sort((LoreDB.LorePage a, LoreDB.LorePage b) => a.PageNumber.CompareTo(b.PageNumber));
		foreach (LoreDB.LorePage page in list)
		{
			this.CreatePageItem(page);
		}
		UISelector.SetupVerticalListNav(this.PageItems, null, null, true);
		UISelector.SelectSelectable(this.PageItems[0].GetComponent<Button>());
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x00087C14 File Offset: 0x00085E14
	private void CreatePageItem(LoreDB.LorePage page)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PageItemRef, this.PageItemRef.transform.parent);
		gameObject.SetActive(true);
		gameObject.GetComponent<CodexLorePageSelector>().Setup(page);
		this.PageItems.Add(gameObject);
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x00087C5C File Offset: 0x00085E5C
	private void OnInputChanged()
	{
		foreach (GameObject gameObject in this.TabControlPrompts)
		{
			gameObject.SetActive(InputManager.IsUsingController);
		}
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x00087CB4 File Offset: 0x00085EB4
	public CodexLorePanel()
	{
	}

	// Token: 0x04001522 RID: 5410
	public static CodexLorePanel instance;

	// Token: 0x04001523 RID: 5411
	private UIPanel panel;

	// Token: 0x04001524 RID: 5412
	public List<CodexLorePanel.LoreTab> Tabs = new List<CodexLorePanel.LoreTab>();

	// Token: 0x04001525 RID: 5413
	public List<GameObject> TabControlPrompts;

	// Token: 0x04001526 RID: 5414
	private CodexLorePanel.LoreTab curTab;

	// Token: 0x04001527 RID: 5415
	public TextMeshProUGUI CharacterName;

	// Token: 0x04001528 RID: 5416
	public TextMeshProUGUI PageCountText;

	// Token: 0x04001529 RID: 5417
	public Image PageCountBar;

	// Token: 0x0400152A RID: 5418
	public LorePageUIDisplay PageDisplay;

	// Token: 0x0400152B RID: 5419
	public GameObject PageItemRef;

	// Token: 0x0400152C RID: 5420
	public AutoScrollRect PageScroller;

	// Token: 0x0400152D RID: 5421
	private List<GameObject> PageItems = new List<GameObject>();

	// Token: 0x0400152E RID: 5422
	public List<string> AlwaysAvailablePages = new List<string>();

	// Token: 0x020005CF RID: 1487
	[Serializable]
	public class LoreTab
	{
		// Token: 0x06002636 RID: 9782 RVA: 0x000D3341 File Offset: 0x000D1541
		public LoreTab()
		{
		}

		// Token: 0x040028B8 RID: 10424
		public LoreDB.Character Character;

		// Token: 0x040028B9 RID: 10425
		public CanvasGroup SelectedDisplay;

		// Token: 0x040028BA RID: 10426
		public Button Button;
	}

	// Token: 0x020005D0 RID: 1488
	[CompilerGenerated]
	private sealed class <>c__DisplayClass13_0
	{
		// Token: 0x06002637 RID: 9783 RVA: 0x000D3349 File Offset: 0x000D1549
		public <>c__DisplayClass13_0()
		{
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000D3351 File Offset: 0x000D1551
		internal void <Awake>b__0()
		{
			this.<>4__this.SelectTab(this.c);
		}

		// Token: 0x040028BB RID: 10427
		public LoreDB.Character c;

		// Token: 0x040028BC RID: 10428
		public CodexLorePanel <>4__this;
	}

	// Token: 0x020005D1 RID: 1489
	[CompilerGenerated]
	private sealed class <GoBack>d__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002639 RID: 9785 RVA: 0x000D3364 File Offset: 0x000D1564
		[DebuggerHidden]
		public <GoBack>d__16(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000D3373 File Offset: 0x000D1573
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000D3378 File Offset: 0x000D1578
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

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x000D33C2 File Offset: 0x000D15C2
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000D33CA File Offset: 0x000D15CA
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000D33D1 File Offset: 0x000D15D1
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028BD RID: 10429
		private int <>1__state;

		// Token: 0x040028BE RID: 10430
		private object <>2__current;
	}

	// Token: 0x020005D2 RID: 1490
	[CompilerGenerated]
	private sealed class <>c__DisplayClass20_0
	{
		// Token: 0x0600263F RID: 9791 RVA: 0x000D33D9 File Offset: 0x000D15D9
		public <>c__DisplayClass20_0()
		{
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000D33E1 File Offset: 0x000D15E1
		internal bool <LoadPages>b__0(LoreDB.LorePage v)
		{
			return v.Character == this.character;
		}

		// Token: 0x040028BF RID: 10431
		public LoreDB.Character character;
	}

	// Token: 0x020005D3 RID: 1491
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002641 RID: 9793 RVA: 0x000D33F1 File Offset: 0x000D15F1
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000D33FD File Offset: 0x000D15FD
		public <>c()
		{
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000D3405 File Offset: 0x000D1605
		internal int <LoadPages>b__20_1(LoreDB.LorePage a, LoreDB.LorePage b)
		{
			return a.PageNumber.CompareTo(b.PageNumber);
		}

		// Token: 0x040028C0 RID: 10432
		public static readonly CodexLorePanel.<>c <>9 = new CodexLorePanel.<>c();

		// Token: 0x040028C1 RID: 10433
		public static Comparison<LoreDB.LorePage> <>9__20_1;
	}
}
