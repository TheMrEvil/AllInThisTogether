using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FE RID: 510
public class PlayerChoicePanel : MonoBehaviour
{
	// Token: 0x1700016B RID: 363
	// (get) Token: 0x060015BA RID: 5562 RVA: 0x0008915A File Offset: 0x0008735A
	// (set) Token: 0x060015BB RID: 5563 RVA: 0x00089162 File Offset: 0x00087362
	public bool ShouldShow
	{
		[CompilerGenerated]
		get
		{
			return this.<ShouldShow>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<ShouldShow>k__BackingField = value;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x060015BC RID: 5564 RVA: 0x0008916B File Offset: 0x0008736B
	public bool HasChoices
	{
		get
		{
			return this.PlayerChoices.Count > 0;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x060015BD RID: 5565 RVA: 0x0008917E File Offset: 0x0008737E
	public int RerollsRemaining
	{
		get
		{
			if (PlayerControl.myInstance == null)
			{
				return 0;
			}
			return (int)PlayerControl.myInstance.GetPassiveMod(Passive.EntityValue.P_PageRerolls, 0f) - PlayerChoicePanel.RerollsUsed;
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x060015BE RID: 5566 RVA: 0x000891AC File Offset: 0x000873AC
	public List<AugmentTree> CurrentAugmentChoices
	{
		get
		{
			List<AugmentTree> list = new List<AugmentTree>();
			if (!this.HasChoices)
			{
				return list;
			}
			foreach (Choice choice in this.PlayerChoices)
			{
				list.Add(choice.Augment);
			}
			return list;
		}
	}

	// Token: 0x060015BF RID: 5567 RVA: 0x00089218 File Offset: 0x00087418
	private void Awake()
	{
		PlayerChoicePanel.instance = this;
		PlayerChoicePanel.IsSelecting = false;
		PlayerChoicePanel.InApplySequence = false;
		PlayerChoicePanel.RerollsUsed = 0;
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(delegate(GameState from, GameState to)
		{
			if (to == GameState.Ended || to == GameState.Hub || to == GameState.Pregame)
			{
				PlayerChoicePanel.RerollsUsed = 0;
			}
		}));
	}

	// Token: 0x060015C0 RID: 5568 RVA: 0x0008927C File Offset: 0x0008747C
	public void Reset()
	{
		base.StopAllCoroutines();
		for (int i = this.options.Count - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(this.options[i].gameObject);
			this.options.RemoveAt(i);
		}
		this.options.Clear();
		this.PlayerChoices.Clear();
	}

	// Token: 0x060015C1 RID: 5569 RVA: 0x000892E0 File Offset: 0x000874E0
	private void Update()
	{
		this.canvasGroup.UpdateOpacity(this.ShouldShow, 2f, false);
		if (!this.ShouldShow)
		{
			return;
		}
		this.MultiGroupFader.UpdateOpacity(PlayerChoicePanel.ChoiceTotal > 1, 4f, true);
		if (PlayerChoicePanel.ChoiceTotal > 1)
		{
			string text = Mathf.Min(PlayerChoicePanel.ChoicesChosen + 1, PlayerChoicePanel.ChoiceTotal).ToString() + "/" + PlayerChoicePanel.ChoiceTotal.ToString();
			if (this.MultiGroupText.text != text)
			{
				this.MultiGroupText.text = text;
			}
		}
		int rerollsRemaining = this.RerollsRemaining;
		bool flag = true;
		foreach (UIPlayerScroll uiplayerScroll in this.options)
		{
			flag &= !uiplayerScroll.IsOpening;
		}
		this.RerollGroup.UpdateOpacity(rerollsRemaining > 0 && flag && this.options.Count > 0 && this.PlayerChoices.Count > 0 && !PlayerChoicePanel.InApplySequence, 4f, false);
		string text2 = "<size=36>x</size>" + rerollsRemaining.ToString();
		if (this.RollCountText.text != text2)
		{
			this.RollCountText.text = text2;
		}
		if (InputManager.IsUsingController && !AugmentsPanel.instance.IsOnBookSelection && !this.IsSelectingViable())
		{
			this.SelectDefault();
		}
	}

	// Token: 0x060015C2 RID: 5570 RVA: 0x00089468 File Offset: 0x00087668
	public void SelectDefault()
	{
		if (this.options.Count <= 0)
		{
			return;
		}
		UnityEngine.Debug.Log("Selecting Scroll Current");
		UISelector.SelectSelectable(this.options[0].GetComponent<Button>());
	}

	// Token: 0x060015C3 RID: 5571 RVA: 0x00089499 File Offset: 0x00087699
	public void Open()
	{
		this.LoadOptions(this.PlayerChoices, this.seenChoices);
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x000894B0 File Offset: 0x000876B0
	public static void LoadPlayerScrolls(List<AugmentTree> mods)
	{
		PlayerChoicePanel.IsSelecting = true;
		PlayerChoicePanel.instance.seenChoices = false;
		PlayerChoicePanel.instance.PlayerChoices = new List<Choice>();
		for (int i = 0; i < mods.Count; i++)
		{
			Choice item = new Choice(ChoiceType.PlayerScroll, mods[i], -1);
			PlayerChoicePanel.instance.PlayerChoices.Add(item);
		}
		PlayerChoicePanel.instance.LoadOptions(PlayerChoicePanel.instance.PlayerChoices, PlayerChoicePanel.instance.seenChoices);
		PlayerChoicePanel.instance.seenChoices = true;
		PlayerChoicePanel.instance.ControllerSelectDelayed();
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x00089540 File Offset: 0x00087740
	private void ControllerSelectDelayed()
	{
		if (InputManager.IsUsingController)
		{
			UISelector.ResetSelected();
		}
		base.Invoke("ControllerSelectAfterLoad", 1.5f);
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x0008955E File Offset: 0x0008775E
	private void ControllerSelectAfterLoad()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (!PlayerChoicePanel.IsSelecting)
		{
			return;
		}
		if (AugmentsPanel.instance.IsOnBookSelection)
		{
			AugmentsPanel.instance.TryToggleFocus();
		}
	}

	// Token: 0x060015C7 RID: 5575 RVA: 0x00089588 File Offset: 0x00087788
	private bool IsSelectingViable()
	{
		Selectable currentSelection = UISelector.instance.CurrentSelection;
		if (currentSelection == this.RerollButton)
		{
			return true;
		}
		foreach (UIPlayerScroll uiplayerScroll in this.options)
		{
			if (currentSelection == uiplayerScroll.Button)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060015C8 RID: 5576 RVA: 0x00089604 File Offset: 0x00087804
	public void RemoveUnchosen(UIPlayerScroll opt)
	{
		for (int i = this.options.Count - 1; i >= 0; i--)
		{
			if (!(this.options[i] == opt))
			{
				this.options[i].ReleaseFromChoice();
				this.options.RemoveAt(i);
			}
		}
	}

	// Token: 0x060015C9 RID: 5577 RVA: 0x0008965C File Offset: 0x0008785C
	public void ConfirmPlayerScroll(AugmentTree modifiers, UIPlayerScroll opt)
	{
		PlayerChoicePanel.ChoicesChosen++;
		PlayerControl.myInstance.AddAugment(modifiers, 1);
		PlayerControl.myInstance.Net.AugmentChosen(modifiers, this.PlayerChoices);
		this.PlayerChoices.Clear();
		if (GameplayManager.IsChallengeActive)
		{
			for (int i = 0; i < (int)modifiers.Root.DisplayQuality; i++)
			{
				PlayerControl.myInstance.GetRandom(0, 1);
			}
		}
		PlayerChoicePanel.IsSelecting = false;
		PlayerChoicePanel.InApplySequence = true;
		AudioManager.PlayInterfaceSFX(this.ChosenSFX, 1f, 0f);
		base.StartCoroutine(this.PlayerChosenSequence());
		Action<AugmentTree> playerScrollChosen = this.PlayerScrollChosen;
		if (playerScrollChosen == null)
		{
			return;
		}
		playerScrollChosen(modifiers);
	}

	// Token: 0x060015CA RID: 5578 RVA: 0x0008970B File Offset: 0x0008790B
	private IEnumerator PlayerChosenSequence()
	{
		yield return new WaitForSeconds(1.8f);
		PlayerChoicePanel.InApplySequence = false;
		if (!AugmentsPanel.instance.TryNextUpgrade())
		{
			this.ShouldShow = false;
		}
		AugmentsPanel.instance.Refresh();
		yield break;
	}

	// Token: 0x060015CB RID: 5579 RVA: 0x0008971C File Offset: 0x0008791C
	public void LoadOptions(List<Choice> choices, bool startVisible)
	{
		AudioManager.PlayInterfaceSFX(this.StartSFX, 1f, 0f);
		this.ClearOptions();
		List<int> list = new List<int>
		{
			0,
			1,
			2
		};
		list.Shuffle(null);
		List<string> list2 = new List<string>();
		for (int i = 0; i < choices.Count; i++)
		{
			this.AddOption(choices[i], startVisible, (float)i * 0.3f, list[i % list.Count]);
			list2.Add(choices[i].Augment.ID);
		}
		Button component = this.RerollGroup.GetComponent<Button>();
		UISelector.SetupVerticalListNav<UIPlayerScroll>(this.options, component, null, false);
		if (this.options.Count > 0)
		{
			component.SetNavigation(this.options[0].GetComponent<Button>(), UIDirection.Down, false);
		}
		Progression.SawAugment(list2);
		this.ShouldShow = true;
		Tooltip.Release();
	}

	// Token: 0x060015CC RID: 5580 RVA: 0x0008980C File Offset: 0x00087A0C
	public void StopSequences()
	{
		base.StopAllCoroutines();
		if (this.isRerolling)
		{
			this.PlayerChoices.Clear();
			PlayerChoicePanel.ChoiceTotal--;
			this.seenChoices = false;
			AugmentsPanel.instance.TryNextUpgrade();
			this.isRerolling = false;
		}
		PlayerChoicePanel.InApplySequence = false;
		foreach (UIPlayerScroll uiplayerScroll in this.options)
		{
			if (uiplayerScroll != null)
			{
				uiplayerScroll.StopSequences();
			}
		}
		if (!AugmentsPanel.instance.TryNextUpgrade())
		{
			this.ShouldShow = false;
		}
		AugmentsPanel.instance.Refresh();
	}

	// Token: 0x060015CD RID: 5581 RVA: 0x000898C8 File Offset: 0x00087AC8
	private void AddOption(Choice c, bool startVisible, float delay, int backID)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChoiceRef, this.ChoiceRef.transform.parent);
		gameObject.SetActive(true);
		UIPlayerScroll component = gameObject.GetComponent<UIPlayerScroll>();
		component.Setup(c, startVisible, delay, backID);
		Progression.UnseenAugments.Remove(c.Augment.ID);
		this.options.Add(component);
	}

	// Token: 0x060015CE RID: 5582 RVA: 0x0008992C File Offset: 0x00087B2C
	private void ClearOptions()
	{
		foreach (UIPlayerScroll uiplayerScroll in this.options)
		{
			if (uiplayerScroll != null)
			{
				UnityEngine.Object.Destroy(uiplayerScroll.gameObject);
			}
		}
		this.options.Clear();
	}

	// Token: 0x060015CF RID: 5583 RVA: 0x00089998 File Offset: 0x00087B98
	public void TryReroll()
	{
		if (this.RerollsRemaining <= 0 || this.PlayerChoices.Count == 0 || PlayerChoicePanel.InApplySequence)
		{
			return;
		}
		PlayerChoicePanel.RerollsUsed++;
		List<AugmentTree> list = new List<AugmentTree>();
		foreach (UIPlayerScroll uiplayerScroll in this.options)
		{
			if (uiplayerScroll.Augment != null)
			{
				list.Add(uiplayerScroll.Augment);
			}
		}
		PlayerControl.myInstance.Net.AugmentRerolled(list);
		InkManager.instance.AwardPlayerPage(null, list);
		this.RemoveUnchosen(null);
		base.StartCoroutine("RerollSequence");
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x00089A60 File Offset: 0x00087C60
	private IEnumerator RerollSequence()
	{
		this.isRerolling = true;
		yield return true;
		this.PlayerChoices.Clear();
		this.seenChoices = false;
		yield return new WaitForSeconds(0.8f);
		PlayerChoicePanel.IsSelecting = false;
		PlayerChoicePanel.ChoiceTotal--;
		AugmentsPanel.instance.TryNextUpgrade();
		this.isRerolling = false;
		yield break;
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x00089A6F File Offset: 0x00087C6F
	public static void ForceSelection()
	{
		if (PlayerChoicePanel.instance.options.Count > 0)
		{
			PlayerChoicePanel.instance.options[UnityEngine.Random.Range(0, PlayerChoicePanel.instance.options.Count)].Choose();
		}
	}

	// Token: 0x060015D2 RID: 5586 RVA: 0x00089AAC File Offset: 0x00087CAC
	public PlayerChoicePanel()
	{
	}

	// Token: 0x04001568 RID: 5480
	public static PlayerChoicePanel instance;

	// Token: 0x04001569 RID: 5481
	private CanvasGroup canvasGroup;

	// Token: 0x0400156A RID: 5482
	public GameObject ChoiceRef;

	// Token: 0x0400156B RID: 5483
	public AudioClip StartSFX;

	// Token: 0x0400156C RID: 5484
	public AudioClip ChosenSFX;

	// Token: 0x0400156D RID: 5485
	[CompilerGenerated]
	private bool <ShouldShow>k__BackingField;

	// Token: 0x0400156E RID: 5486
	[Header("Rerolling")]
	public CanvasGroup RerollGroup;

	// Token: 0x0400156F RID: 5487
	public Button RerollButton;

	// Token: 0x04001570 RID: 5488
	public TextMeshProUGUI RollCountText;

	// Token: 0x04001571 RID: 5489
	public static int RerollsUsed;

	// Token: 0x04001572 RID: 5490
	[Header("Multichoice Display")]
	public CanvasGroup MultiGroupFader;

	// Token: 0x04001573 RID: 5491
	public TextMeshProUGUI MultiGroupText;

	// Token: 0x04001574 RID: 5492
	public static int ChoiceTotal;

	// Token: 0x04001575 RID: 5493
	public static int ChoicesChosen;

	// Token: 0x04001576 RID: 5494
	private List<Choice> PlayerChoices = new List<Choice>();

	// Token: 0x04001577 RID: 5495
	private bool seenChoices;

	// Token: 0x04001578 RID: 5496
	private List<UIPlayerScroll> options = new List<UIPlayerScroll>();

	// Token: 0x04001579 RID: 5497
	public static bool IsSelecting;

	// Token: 0x0400157A RID: 5498
	public static bool InApplySequence;

	// Token: 0x0400157B RID: 5499
	public Action<AugmentTree> PlayerScrollChosen;

	// Token: 0x0400157C RID: 5500
	private bool isRerolling;

	// Token: 0x020005E1 RID: 1505
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600266C RID: 9836 RVA: 0x000D38E9 File Offset: 0x000D1AE9
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000D38F5 File Offset: 0x000D1AF5
		public <>c()
		{
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000D38FD File Offset: 0x000D1AFD
		internal void <Awake>b__29_0(GameState from, GameState to)
		{
			if (to == GameState.Ended || to == GameState.Hub || to == GameState.Pregame)
			{
				PlayerChoicePanel.RerollsUsed = 0;
			}
		}

		// Token: 0x040028F4 RID: 10484
		public static readonly PlayerChoicePanel.<>c <>9 = new PlayerChoicePanel.<>c();

		// Token: 0x040028F5 RID: 10485
		public static Action<GameState, GameState> <>9__29_0;
	}

	// Token: 0x020005E2 RID: 1506
	[CompilerGenerated]
	private sealed class <PlayerChosenSequence>d__40 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600266F RID: 9839 RVA: 0x000D3912 File Offset: 0x000D1B12
		[DebuggerHidden]
		public <PlayerChosenSequence>d__40(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000D3921 File Offset: 0x000D1B21
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000D3924 File Offset: 0x000D1B24
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerChoicePanel playerChoicePanel = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(1.8f);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			PlayerChoicePanel.InApplySequence = false;
			if (!AugmentsPanel.instance.TryNextUpgrade())
			{
				playerChoicePanel.ShouldShow = false;
			}
			AugmentsPanel.instance.Refresh();
			return false;
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000D3993 File Offset: 0x000D1B93
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000D399B File Offset: 0x000D1B9B
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000D39A2 File Offset: 0x000D1BA2
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028F6 RID: 10486
		private int <>1__state;

		// Token: 0x040028F7 RID: 10487
		private object <>2__current;

		// Token: 0x040028F8 RID: 10488
		public PlayerChoicePanel <>4__this;
	}

	// Token: 0x020005E3 RID: 1507
	[CompilerGenerated]
	private sealed class <RerollSequence>d__47 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002675 RID: 9845 RVA: 0x000D39AA File Offset: 0x000D1BAA
		[DebuggerHidden]
		public <RerollSequence>d__47(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000D39B9 File Offset: 0x000D1BB9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000D39BC File Offset: 0x000D1BBC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlayerChoicePanel playerChoicePanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				playerChoicePanel.isRerolling = true;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				playerChoicePanel.PlayerChoices.Clear();
				playerChoicePanel.seenChoices = false;
				this.<>2__current = new WaitForSeconds(0.8f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				PlayerChoicePanel.IsSelecting = false;
				PlayerChoicePanel.ChoiceTotal--;
				AugmentsPanel.instance.TryNextUpgrade();
				playerChoicePanel.isRerolling = false;
				return false;
			default:
				return false;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000D3A6C File Offset: 0x000D1C6C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000D3A74 File Offset: 0x000D1C74
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000D3A7B File Offset: 0x000D1C7B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028F9 RID: 10489
		private int <>1__state;

		// Token: 0x040028FA RID: 10490
		private object <>2__current;

		// Token: 0x040028FB RID: 10491
		public PlayerChoicePanel <>4__this;
	}
}
