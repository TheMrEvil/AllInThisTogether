using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

// Token: 0x02000200 RID: 512
public class UITutorial : MonoBehaviour
{
	// Token: 0x060015E2 RID: 5602 RVA: 0x0008A28B File Offset: 0x0008848B
	private void Awake()
	{
		UITutorial.instance = this;
		UITutorial.InTutorial = false;
		this.Fader.alpha = 0f;
		this.Fader.interactable = false;
		this.Fader.blocksRaycasts = false;
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x0008A2C1 File Offset: 0x000884C1
	private void Start()
	{
		PanelManager panelManager = PanelManager.instance;
		panelManager.OnPanelChanged = (Action<PanelType, PanelType>)Delegate.Combine(panelManager.OnPanelChanged, new Action<PanelType, PanelType>(delegate(PanelType from, PanelType to)
		{
			if (from != PanelType.GameInvisible)
			{
				UITutorial.CancelTutorial();
			}
		}));
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x0008A2FC File Offset: 0x000884FC
	private void Update()
	{
		if (!UITutorial.InTutorial && this.Fader.alpha <= 0f)
		{
			return;
		}
		this.Fader.UpdateOpacity(UITutorial.InTutorial && this.CurStep >= 0, 6f, false);
		if (!UITutorial.InTutorial)
		{
			return;
		}
		this.Box.anchoredPosition = Vector3.Lerp(this.Box.anchoredPosition, this.targetLoc, Time.deltaTime * 5f);
		if (this.buttonT > 0f)
		{
			this.buttonT -= Time.deltaTime;
			return;
		}
		bool isUsingController = InputManager.IsUsingController;
		this.ButtonGroup.UpdateOpacity(!isUsingController, 4f, false);
		this.ControllerBtnGroup.UpdateOpacity(isUsingController, 4f, false);
		if (isUsingController && InputManager.UIAct.UIPrimary.WasPressed)
		{
			this.NextStep();
		}
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x0008A3F0 File Offset: 0x000885F0
	public static bool TryTutorial(UITutorial.Tutorial tut)
	{
		if (Settings.HasCompletedUITutorial(tut))
		{
			return false;
		}
		UnityEngine.Debug.Log("Getting Sequence for " + tut.ToString());
		UITutorial.TutSequence sequence = UITutorial.instance.GetSequence(tut);
		if (sequence == null)
		{
			return false;
		}
		UnityEngine.Debug.Log("Starting Tutorial");
		UITutorial.instance.StartTutorial(sequence);
		return true;
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x0008A44C File Offset: 0x0008864C
	private void StartTutorial(UITutorial.TutSequence sequence)
	{
		UITutorial.InTutorial = true;
		this.startedThisFrame = true;
		this.CurSequence = sequence;
		this.CurStep = -1;
		UITutorial.CurTut = sequence.TutType;
		base.StartCoroutine("StartDelayFrame");
		base.Invoke("StartTutorialDelayed", 0.6f);
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x0008A49B File Offset: 0x0008869B
	private IEnumerator StartDelayFrame()
	{
		yield return true;
		this.startedThisFrame = false;
		yield break;
	}

	// Token: 0x060015E8 RID: 5608 RVA: 0x0008A4AA File Offset: 0x000886AA
	private void StartTutorialDelayed()
	{
		this.NextStep();
		AudioManager.PlayInterfaceSFX(this.StartSFX, 1f, 0f);
	}

	// Token: 0x060015E9 RID: 5609 RVA: 0x0008A4C8 File Offset: 0x000886C8
	public void NextStep()
	{
		this.CurStep++;
		this.ButtonGroup.alpha = 0f;
		this.ButtonGroup.interactable = false;
		this.buttonT = 1f;
		if (this.CurStep >= this.CurSequence.Steps.Count)
		{
			this.CompleteTutorial();
			return;
		}
		foreach (TextMeshProUGUI textMeshProUGUI in this.ButtonText)
		{
			textMeshProUGUI.text = ((this.CurStep + 1 >= this.CurSequence.Steps.Count) ? "Finish" : "Continue");
		}
		Vector3 localPosition = this.Box.localPosition;
		this.SetAnchor(this.CurSequence.Steps[this.CurStep].ScreenAnchor);
		this.Box.anchoredPosition = this.Box.anchoredPosition;
		this.Box.localPosition = localPosition;
		this.targetLoc = this.CurSequence.Steps[this.CurStep].Loc;
		this.InfoText.text = this.CurSequence.Steps[this.CurStep].Text;
		this.ActivateArrow(this.CurSequence.Steps[this.CurStep].Arrow);
		if (this.CurStep != 0)
		{
			AudioManager.PlayInterfaceSFX(this.NextSFX, 0.75f, 0f);
		}
	}

	// Token: 0x060015EA RID: 5610 RVA: 0x0008A668 File Offset: 0x00088868
	public static void CancelTutorial()
	{
		if (!UITutorial.InTutorial || UITutorial.instance.startedThisFrame)
		{
			return;
		}
		UITutorial.InTutorial = false;
	}

	// Token: 0x060015EB RID: 5611 RVA: 0x0008A684 File Offset: 0x00088884
	private void CompleteTutorial()
	{
		if (!UITutorial.InTutorial)
		{
			return;
		}
		UITutorial.InTutorial = false;
		AudioManager.PlayInterfaceSFX(this.CompletedSFX, 1f, 0f);
		Settings.UITutorialCompleted(UITutorial.CurTut);
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x0008A6B4 File Offset: 0x000888B4
	private void ActivateArrow(UITutorial.ArrowDir dir)
	{
		foreach (UITutorial.ArrowBox arrowBox in this.Arrows)
		{
			arrowBox.arrow.SetActive(dir == arrowBox.dir);
		}
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x0008A714 File Offset: 0x00088914
	private UITutorial.TutSequence GetSequence(UITutorial.Tutorial tut)
	{
		UITutorial.TutSequence result;
		switch (tut)
		{
		case UITutorial.Tutorial.Abilities:
			result = this.Ability;
			break;
		case UITutorial.Tutorial.Tomes:
			result = this.Tomes;
			break;
		case UITutorial.Tutorial.Meta:
			result = this.Meta;
			break;
		case UITutorial.Tutorial.Bindings:
			result = this.Bindings;
			break;
		case UITutorial.Tutorial.Fountain:
			result = this.Fountain;
			break;
		case UITutorial.Tutorial.Inscription:
			result = this.Inscriptions;
			break;
		case UITutorial.Tutorial.Signatures:
			result = this.Signature;
			break;
		case UITutorial.Tutorial.EnemyUpgrade:
			result = this.EnemyUpgrade;
			break;
		case UITutorial.Tutorial.Ascend_Bindings:
			result = this.AscendBind;
			break;
		case UITutorial.Tutorial.BindingTomes:
			result = this.BindTomes;
			break;
		case UITutorial.Tutorial.RaidCodex:
			result = this.RaidCodex;
			break;
		default:
			result = null;
			break;
		}
		return result;
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x0008A7BC File Offset: 0x000889BC
	private void SetAnchor(TextAnchor anchor)
	{
		RectTransform box = this.Box;
		Vector2 anchorMin;
		switch (anchor)
		{
		case TextAnchor.UpperLeft:
			anchorMin = new Vector2(0f, 1f);
			break;
		case TextAnchor.UpperCenter:
			anchorMin = new Vector2(0.5f, 1f);
			break;
		case TextAnchor.UpperRight:
			anchorMin = new Vector2(1f, 1f);
			break;
		case TextAnchor.MiddleLeft:
			anchorMin = new Vector2(0f, 0.5f);
			break;
		case TextAnchor.MiddleCenter:
			anchorMin = new Vector2(0.5f, 0.5f);
			break;
		case TextAnchor.MiddleRight:
			anchorMin = new Vector2(1f, 0.5f);
			break;
		case TextAnchor.LowerLeft:
			anchorMin = new Vector2(0f, 0f);
			break;
		case TextAnchor.LowerCenter:
			anchorMin = new Vector2(0.5f, 0f);
			break;
		case TextAnchor.LowerRight:
			anchorMin = new Vector2(1f, 0f);
			break;
		default:
			anchorMin = new Vector2(0.5f, 0.5f);
			break;
		}
		box.anchorMin = anchorMin;
		this.Box.anchorMax = this.Box.anchorMin;
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x0008A8D4 File Offset: 0x00088AD4
	public UITutorial()
	{
	}

	// Token: 0x0400158C RID: 5516
	public static UITutorial instance;

	// Token: 0x0400158D RID: 5517
	public static bool InTutorial;

	// Token: 0x0400158E RID: 5518
	public static UITutorial.Tutorial CurTut;

	// Token: 0x0400158F RID: 5519
	private UITutorial.TutSequence CurSequence;

	// Token: 0x04001590 RID: 5520
	[NonSerialized]
	public int CurStep;

	// Token: 0x04001591 RID: 5521
	[NonSerialized]
	public Vector3 targetLoc;

	// Token: 0x04001592 RID: 5522
	private float buttonT;

	// Token: 0x04001593 RID: 5523
	public CanvasGroup Fader;

	// Token: 0x04001594 RID: 5524
	public RectTransform Box;

	// Token: 0x04001595 RID: 5525
	public TextMeshProUGUI InfoText;

	// Token: 0x04001596 RID: 5526
	public List<UITutorial.ArrowBox> Arrows;

	// Token: 0x04001597 RID: 5527
	public CanvasGroup ControllerBtnGroup;

	// Token: 0x04001598 RID: 5528
	public CanvasGroup ButtonGroup;

	// Token: 0x04001599 RID: 5529
	public List<TextMeshProUGUI> ButtonText;

	// Token: 0x0400159A RID: 5530
	public AudioClip StartSFX;

	// Token: 0x0400159B RID: 5531
	public AudioClip NextSFX;

	// Token: 0x0400159C RID: 5532
	public AudioClip CompletedSFX;

	// Token: 0x0400159D RID: 5533
	private bool startedThisFrame;

	// Token: 0x0400159E RID: 5534
	public UITutorial.TutSequence Ability;

	// Token: 0x0400159F RID: 5535
	public UITutorial.TutSequence Tomes;

	// Token: 0x040015A0 RID: 5536
	public UITutorial.TutSequence Meta;

	// Token: 0x040015A1 RID: 5537
	public UITutorial.TutSequence Inscriptions;

	// Token: 0x040015A2 RID: 5538
	public UITutorial.TutSequence Signature;

	// Token: 0x040015A3 RID: 5539
	public UITutorial.TutSequence BindTomes;

	// Token: 0x040015A4 RID: 5540
	public UITutorial.TutSequence Bindings;

	// Token: 0x040015A5 RID: 5541
	public UITutorial.TutSequence AscendBind;

	// Token: 0x040015A6 RID: 5542
	public UITutorial.TutSequence Fountain;

	// Token: 0x040015A7 RID: 5543
	public UITutorial.TutSequence EnemyUpgrade;

	// Token: 0x040015A8 RID: 5544
	public UITutorial.TutSequence RaidCodex;

	// Token: 0x020005E5 RID: 1509
	public enum Tutorial
	{
		// Token: 0x0400291A RID: 10522
		Abilities,
		// Token: 0x0400291B RID: 10523
		Tomes,
		// Token: 0x0400291C RID: 10524
		Meta,
		// Token: 0x0400291D RID: 10525
		Bindings,
		// Token: 0x0400291E RID: 10526
		Fountain,
		// Token: 0x0400291F RID: 10527
		Inscription,
		// Token: 0x04002920 RID: 10528
		Signatures,
		// Token: 0x04002921 RID: 10529
		EnemyUpgrade,
		// Token: 0x04002922 RID: 10530
		Ascend_Bindings,
		// Token: 0x04002923 RID: 10531
		BindingTomes,
		// Token: 0x04002924 RID: 10532
		RaidCodex
	}

	// Token: 0x020005E6 RID: 1510
	public enum ArrowDir
	{
		// Token: 0x04002926 RID: 10534
		None,
		// Token: 0x04002927 RID: 10535
		Top,
		// Token: 0x04002928 RID: 10536
		Bottom,
		// Token: 0x04002929 RID: 10537
		Left,
		// Token: 0x0400292A RID: 10538
		Right
	}

	// Token: 0x020005E7 RID: 1511
	[Serializable]
	public class ArrowBox
	{
		// Token: 0x0600267B RID: 9851 RVA: 0x000D3A83 File Offset: 0x000D1C83
		public ArrowBox()
		{
		}

		// Token: 0x0400292B RID: 10539
		public UITutorial.ArrowDir dir;

		// Token: 0x0400292C RID: 10540
		public GameObject arrow;
	}

	// Token: 0x020005E8 RID: 1512
	[Serializable]
	public class TutSequence
	{
		// Token: 0x0600267C RID: 9852 RVA: 0x000D3A8B File Offset: 0x000D1C8B
		public TutSequence()
		{
		}

		// Token: 0x0400292D RID: 10541
		public UITutorial.Tutorial TutType;

		// Token: 0x0400292E RID: 10542
		public List<UITutorial.TutStep> Steps;
	}

	// Token: 0x020005E9 RID: 1513
	[Serializable]
	public class TutStep
	{
		// Token: 0x0600267D RID: 9853 RVA: 0x000D3A93 File Offset: 0x000D1C93
		public TutStep()
		{
		}

		// Token: 0x0400292F RID: 10543
		public Vector3 Loc;

		// Token: 0x04002930 RID: 10544
		public UITutorial.ArrowDir Arrow;

		// Token: 0x04002931 RID: 10545
		public TextAnchor ScreenAnchor;

		// Token: 0x04002932 RID: 10546
		[TextArea(4, 5)]
		public string Text;
	}

	// Token: 0x020005EA RID: 1514
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600267E RID: 9854 RVA: 0x000D3A9B File Offset: 0x000D1C9B
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000D3AA7 File Offset: 0x000D1CA7
		public <>c()
		{
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000D3AAF File Offset: 0x000D1CAF
		internal void <Start>b__30_0(PanelType from, PanelType to)
		{
			if (from != PanelType.GameInvisible)
			{
				UITutorial.CancelTutorial();
			}
		}

		// Token: 0x04002933 RID: 10547
		public static readonly UITutorial.<>c <>9 = new UITutorial.<>c();

		// Token: 0x04002934 RID: 10548
		public static Action<PanelType, PanelType> <>9__30_0;
	}

	// Token: 0x020005EB RID: 1515
	[CompilerGenerated]
	private sealed class <StartDelayFrame>d__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002681 RID: 9857 RVA: 0x000D3ABA File Offset: 0x000D1CBA
		[DebuggerHidden]
		public <StartDelayFrame>d__34(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000D3AC9 File Offset: 0x000D1CC9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x000D3ACC File Offset: 0x000D1CCC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			UITutorial uitutorial = this;
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
			uitutorial.startedThisFrame = false;
			return false;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x000D3B1B File Offset: 0x000D1D1B
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000D3B23 File Offset: 0x000D1D23
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x000D3B2A File Offset: 0x000D1D2A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002935 RID: 10549
		private int <>1__state;

		// Token: 0x04002936 RID: 10550
		private object <>2__current;

		// Token: 0x04002937 RID: 10551
		public UITutorial <>4__this;
	}
}
