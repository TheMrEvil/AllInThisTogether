using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CB RID: 459
public class UIPlayerScroll : MonoBehaviour
{
	// Token: 0x17000159 RID: 345
	// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0007361C File Offset: 0x0007181C
	private UIPlayerScroll.ScrollFX RarityFX
	{
		get
		{
			if (this.Augment == null)
			{
				return this.VFXGroup[0];
			}
			foreach (UIPlayerScroll.ScrollFX scrollFX in this.VFXGroup)
			{
				if (scrollFX.Quality == this.Augment.Root.DisplayQuality)
				{
					return scrollFX;
				}
			}
			return this.VFXGroup[0];
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x060012B5 RID: 4789 RVA: 0x000736B0 File Offset: 0x000718B0
	public AugmentTree Augment
	{
		get
		{
			return this.modifiers;
		}
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x000736B8 File Offset: 0x000718B8
	private void Awake()
	{
		this.ScrollAnim.speed = 0.0001f;
		base.Invoke("EntryAnim", UnityEngine.Random.Range(0f, 0.5f));
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x000736E4 File Offset: 0x000718E4
	private void EntryAnim()
	{
		this.ScrollAnim.speed = 1f;
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x000736F8 File Offset: 0x000718F8
	public void Setup(Choice c, bool startVisible, float openDelay, int backgroundID)
	{
		this.choice = c;
		this.choice.scrollUI = this;
		this.maskRect.localEulerAngles = new Vector3(0f, 0f, UnityEngine.Random.Range(-2f, 2f));
		this.maskRect.localPosition = new Vector3((float)UnityEngine.Random.Range(-48, 48), 0f, 0f);
		this.maskRect.localScale = Vector3.one * UnityEngine.Random.Range(0.95f, 1.05f);
		if (Screen.height <= 804)
		{
			this.KeywordList.localScale = Vector3.one * 1.25f;
			this.KeywordList.localPosition -= new Vector3(100f, 0f, 0f);
		}
		this.modifiers = this.choice.Augment;
		this.glowT = UnityEngine.Random.Range(0f, 1f);
		this.ActionIcon.sprite = this.choice.Icon;
		this.TitleText.text = this.choice.Title;
		GameDB.QualityInfo qualityInfo = GameDB.Quality(this.choice.Rarity);
		this.TitleText.color = qualityInfo.DarkTextColor;
		this.DetailText.text = this.choice.Detail;
		this.RarityDetailImage.sprite = this.RarityBorders[Mathf.Clamp((int)this.choice.Rarity, 0, this.RarityBorders.Count - 1)];
		this.IconBorder.sprite = qualityInfo.Border;
		this.RarityBanner.color = this.RarityFX.BannerColor;
		this.RarityLabel.text = qualityInfo.Label;
		UIPlayerScroll.ScrollOption scrollOption = this.ScrollOptions[Mathf.Clamp(backgroundID, 0, this.ScrollOptions.Count - 1)];
		this.Background.sprite = scrollOption.Parchment;
		this.BackgroundShadowed.sprite = scrollOption.Parchment;
		this.GlowImage.sprite = scrollOption.Glow;
		this.FlashImage.sprite = scrollOption.Flash;
		if (startVisible)
		{
			this.HasOpened = true;
			this.IsOpening = false;
			this.maskRect.sizeDelta = new Vector2(this.UnwrapWidth, this.maskRect.sizeDelta.y);
			this.ScrollObj.SetActive(false);
		}
		this.ClearKeywords();
		this.SetupKeywords();
		if (!this.HasOpened)
		{
			this.OpenThis(openDelay);
		}
		base.GetComponent<UIPingable>().Setup(this.choice.Augment);
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x000739A8 File Offset: 0x00071BA8
	public void OnSelect()
	{
		if (this.IsOpening || this.isReleasing)
		{
			return;
		}
		this.isHovering = true;
		if (this.HasOpened)
		{
			this.KeywordList.gameObject.SetActive(true);
			PlayerControl.myInstance.Display.ShowRange(this.Augment.Root.DisplayRadius);
			return;
		}
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x00073A08 File Offset: 0x00071C08
	public void OnDeselect()
	{
		if (this.IsOpening || this.isReleasing)
		{
			return;
		}
		this.isHovering = false;
		if (this.HasOpened)
		{
			this.KeywordList.gameObject.SetActive(false);
			PlayerControl.myInstance.Display.ReleaseRange();
			return;
		}
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x00073A56 File Offset: 0x00071C56
	public void OnClick()
	{
		if (this.IsOpening || this.isReleasing)
		{
			return;
		}
		if (this.HasOpened)
		{
			this.Choose();
			return;
		}
		this.OpenThis(0f);
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x00073A83 File Offset: 0x00071C83
	public void Choose()
	{
		this.choice.Choose();
		PlayerChoicePanel.instance.RemoveUnchosen(this);
		base.StopAllCoroutines();
		this.isReleasing = true;
		this.isHovering = false;
		base.StartCoroutine("ChosenSequence");
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x00073ABB File Offset: 0x00071CBB
	private IEnumerator ChosenSequence()
	{
		this.SplashVFX.Play();
		this.FlashGroup.alpha = 1f;
		float t;
		for (t = 1f; t > 0f; t -= Time.deltaTime * 4f)
		{
			yield return true;
			this.FlashGroup.alpha = t;
		}
		this.FlashGroup.alpha = 0f;
		yield return new WaitForSeconds(0.5f);
		t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			this.UIGroup.UpdateOpacity(false, 3f, false);
			yield return true;
		}
		yield break;
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x00073ACA File Offset: 0x00071CCA
	public void ReleaseFromChoice()
	{
		this.isReleasing = true;
		base.StopAllCoroutines();
		base.StartCoroutine("ReleaseSequence");
		UnityEngine.Object.Destroy(base.gameObject, 0.75f);
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x00073AF5 File Offset: 0x00071CF5
	private IEnumerator ReleaseSequence()
	{
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			this.UIGroup.UpdateOpacity(false, 4f, false);
			yield return true;
		}
		yield break;
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x00073B04 File Offset: 0x00071D04
	private void OpenThis(float delay)
	{
		this.HasOpened = true;
		base.StartCoroutine(this.OpenSequence(delay));
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x00073B1C File Offset: 0x00071D1C
	private void Update()
	{
		if (this.HasOpened)
		{
			this.OpenedUpdate();
			return;
		}
		if (this.IsOpening)
		{
			return;
		}
		this.glowT += Time.deltaTime;
		if (this.glowT >= 1f)
		{
			this.glowT -= 1f;
		}
		float num = this.isHovering ? 1.5f : 0f;
		num *= this.GlowCurve.Evaluate(this.glowT);
		Color b = new Color(this.RarityFX.GlowColor.r, this.RarityFX.GlowColor.g, this.RarityFX.GlowColor.b, 1f) * num;
		Color color = this.PaperMesh.InstanceMaterial.GetColor(UIPlayerScroll.EmissiveColor);
		this.PaperMesh.InstanceMaterial.SetColor(UIPlayerScroll.EmissiveColor, Color.Lerp(color, b, Time.deltaTime * 6f));
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x00073C19 File Offset: 0x00071E19
	private void OpenedUpdate()
	{
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x00073C1C File Offset: 0x00071E1C
	public void Reset()
	{
		if (this.IsOpening)
		{
			return;
		}
		this.HasOpened = false;
		this.maskRect.sizeDelta = new Vector2(this.maskRect.sizeDelta.x, 0f);
		this.ScrollAnim.CrossFade("Idle_Base", 0.2f, 0, UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x00073C83 File Offset: 0x00071E83
	public void StopSequences()
	{
		base.StopAllCoroutines();
		this.RevealFX.Stop();
		this.RarityFX.SpinFX.Stop();
		if (this.IsOpening)
		{
			this.IsOpening = false;
			this.HasOpened = true;
		}
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x00073CBC File Offset: 0x00071EBC
	private IEnumerator OpenSequence(float delay)
	{
		this.IsOpening = true;
		yield return new WaitForSeconds(delay);
		AudioManager.PlayInterfaceSFX(this.SpawnSFX.GetRandomClip(-1), 1f, UnityEngine.Random.Range(0.93f, 1.07f));
		this.ScrollAnim.CrossFade("Scroll_Horizontal", 0.05f);
		this.RevealFX.Play();
		yield return new WaitForSeconds(0.8f + delay * 2.1666665f);
		this.SealBurstFX.Play();
		this.RarityFX.OpenFX.Play();
		this.ScrollAnim.CrossFade("Open", 0.1f);
		AudioManager.PlayInterfaceSFX(this.RarityFX.OpenSFX.GetRandomClip(-1), 1f, UnityEngine.Random.Range(0.93f, 1.07f));
		yield return new WaitForSeconds(0.594f);
		this.RarityFX.SpinFX.Play();
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / this.UnwrapTime;
			float x = this.UnwrapCurve.Evaluate(t) * this.UnwrapWidth;
			this.maskRect.sizeDelta = new Vector2(x, this.maskRect.sizeDelta.y);
			yield return true;
		}
		this.IsOpening = false;
		yield break;
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x00073CD4 File Offset: 0x00071ED4
	private void SetupKeywords()
	{
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(this.choice.Augment.Root.Detail, PlayerControl.myInstance))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordList, ref this.keywords, PlayerControl.myInstance);
		}
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x00073D50 File Offset: 0x00071F50
	private void ClearKeywords()
	{
		foreach (KeywordBoxUI keywordBoxUI in this.keywords)
		{
			if (keywordBoxUI != null)
			{
				UnityEngine.Object.Destroy(keywordBoxUI.gameObject);
			}
		}
		this.keywords.Clear();
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x00073DBC File Offset: 0x00071FBC
	public UIPlayerScroll.ScrollFX GetFX(AugmentQuality rarity)
	{
		foreach (UIPlayerScroll.ScrollFX scrollFX in this.VFXGroup)
		{
			if (scrollFX.Quality == rarity)
			{
				return scrollFX;
			}
		}
		return this.VFXGroup[0];
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x00073E24 File Offset: 0x00072024
	public UIPlayerScroll()
	{
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x00073E42 File Offset: 0x00072042
	// Note: this type is marked as 'beforefieldinit'.
	static UIPlayerScroll()
	{
	}

	// Token: 0x040011C4 RID: 4548
	public GameObject ScrollObj;

	// Token: 0x040011C5 RID: 4549
	public Animator ScrollAnim;

	// Token: 0x040011C6 RID: 4550
	public UIMeshRenderer PaperMesh;

	// Token: 0x040011C7 RID: 4551
	public AnimationCurve GlowCurve;

	// Token: 0x040011C8 RID: 4552
	public ParticleSystem RevealFX;

	// Token: 0x040011C9 RID: 4553
	public ParticleSystem SealBurstFX;

	// Token: 0x040011CA RID: 4554
	public List<UIPlayerScroll.ScrollFX> VFXGroup;

	// Token: 0x040011CB RID: 4555
	public float UnwrapTime = 0.33f;

	// Token: 0x040011CC RID: 4556
	public RectTransform maskRect;

	// Token: 0x040011CD RID: 4557
	public float UnwrapWidth;

	// Token: 0x040011CE RID: 4558
	public AnimationCurve UnwrapCurve;

	// Token: 0x040011CF RID: 4559
	public CanvasGroup UIGroup;

	// Token: 0x040011D0 RID: 4560
	public Image ActionIcon;

	// Token: 0x040011D1 RID: 4561
	public TextMeshProUGUI TitleText;

	// Token: 0x040011D2 RID: 4562
	public TextMeshProUGUI DetailText;

	// Token: 0x040011D3 RID: 4563
	public Image RarityDetailImage;

	// Token: 0x040011D4 RID: 4564
	public List<Sprite> RarityBorders;

	// Token: 0x040011D5 RID: 4565
	public Image IconBorder;

	// Token: 0x040011D6 RID: 4566
	public Image RarityBanner;

	// Token: 0x040011D7 RID: 4567
	public TextMeshProUGUI RarityLabel;

	// Token: 0x040011D8 RID: 4568
	public RectTransform KeywordList;

	// Token: 0x040011D9 RID: 4569
	private List<KeywordBoxUI> keywords = new List<KeywordBoxUI>();

	// Token: 0x040011DA RID: 4570
	public Button Button;

	// Token: 0x040011DB RID: 4571
	public Image Background;

	// Token: 0x040011DC RID: 4572
	public Image BackgroundShadowed;

	// Token: 0x040011DD RID: 4573
	public Image GlowImage;

	// Token: 0x040011DE RID: 4574
	public Image FlashImage;

	// Token: 0x040011DF RID: 4575
	public CanvasGroup FlashGroup;

	// Token: 0x040011E0 RID: 4576
	public List<UIPlayerScroll.ScrollOption> ScrollOptions;

	// Token: 0x040011E1 RID: 4577
	public ParticleSystem SplashVFX;

	// Token: 0x040011E2 RID: 4578
	public bool HasOpened;

	// Token: 0x040011E3 RID: 4579
	public bool IsOpening;

	// Token: 0x040011E4 RID: 4580
	private bool isHovering;

	// Token: 0x040011E5 RID: 4581
	private bool isReleasing;

	// Token: 0x040011E6 RID: 4582
	private float glowT;

	// Token: 0x040011E7 RID: 4583
	public List<AudioClip> SpawnSFX;

	// Token: 0x040011E8 RID: 4584
	public List<AudioClip> OpenSFX;

	// Token: 0x040011E9 RID: 4585
	private AugmentTree modifiers;

	// Token: 0x040011EA RID: 4586
	private Choice choice;

	// Token: 0x040011EB RID: 4587
	private static readonly int EmissiveColor = Shader.PropertyToID("_EmissiveColor");

	// Token: 0x040011EC RID: 4588
	private static int OpenSFXID = -1;

	// Token: 0x02000584 RID: 1412
	[Serializable]
	public struct ScrollFX
	{
		// Token: 0x04002781 RID: 10113
		public AugmentQuality Quality;

		// Token: 0x04002782 RID: 10114
		[ColorUsage(false, true)]
		public Color GlowColor;

		// Token: 0x04002783 RID: 10115
		public ParticleSystem OpenFX;

		// Token: 0x04002784 RID: 10116
		public ParticleSystem SpinFX;

		// Token: 0x04002785 RID: 10117
		public List<AudioClip> OpenSFX;

		// Token: 0x04002786 RID: 10118
		public Color BannerColor;
	}

	// Token: 0x02000585 RID: 1413
	[Serializable]
	public class ScrollOption
	{
		// Token: 0x06002537 RID: 9527 RVA: 0x000D0D7B File Offset: 0x000CEF7B
		public ScrollOption()
		{
		}

		// Token: 0x04002787 RID: 10119
		public Sprite Parchment;

		// Token: 0x04002788 RID: 10120
		public Sprite Glow;

		// Token: 0x04002789 RID: 10121
		public Sprite Flash;
	}

	// Token: 0x02000586 RID: 1414
	[CompilerGenerated]
	private sealed class <ChosenSequence>d__52 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002538 RID: 9528 RVA: 0x000D0D83 File Offset: 0x000CEF83
		[DebuggerHidden]
		public <ChosenSequence>d__52(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000D0D92 File Offset: 0x000CEF92
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000D0D94 File Offset: 0x000CEF94
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			UIPlayerScroll uiplayerScroll = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				uiplayerScroll.SplashVFX.Play();
				uiplayerScroll.FlashGroup.alpha = 1f;
				t = 1f;
				break;
			case 1:
				this.<>1__state = -1;
				uiplayerScroll.FlashGroup.alpha = t;
				t -= Time.deltaTime * 4f;
				break;
			case 2:
				this.<>1__state = -1;
				t = 1f;
				goto IL_124;
			case 3:
				this.<>1__state = -1;
				goto IL_124;
			default:
				return false;
			}
			if (t <= 0f)
			{
				uiplayerScroll.FlashGroup.alpha = 0f;
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 2;
				return true;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
			IL_124:
			if (t <= 0f)
			{
				return false;
			}
			t -= Time.deltaTime;
			uiplayerScroll.UIGroup.UpdateOpacity(false, 3f, false);
			this.<>2__current = true;
			this.<>1__state = 3;
			return true;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000D0ED3 File Offset: 0x000CF0D3
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000D0EDB File Offset: 0x000CF0DB
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000D0EE2 File Offset: 0x000CF0E2
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400278A RID: 10122
		private int <>1__state;

		// Token: 0x0400278B RID: 10123
		private object <>2__current;

		// Token: 0x0400278C RID: 10124
		public UIPlayerScroll <>4__this;

		// Token: 0x0400278D RID: 10125
		private float <t>5__2;
	}

	// Token: 0x02000587 RID: 1415
	[CompilerGenerated]
	private sealed class <ReleaseSequence>d__54 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600253E RID: 9534 RVA: 0x000D0EEA File Offset: 0x000CF0EA
		[DebuggerHidden]
		public <ReleaseSequence>d__54(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000D0EF9 File Offset: 0x000CF0F9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000D0EFC File Offset: 0x000CF0FC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			UIPlayerScroll uiplayerScroll = this;
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
				t = 1f;
			}
			if (t <= 0f)
			{
				return false;
			}
			t -= Time.deltaTime;
			uiplayerScroll.UIGroup.UpdateOpacity(false, 4f, false);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x000D0F82 File Offset: 0x000CF182
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x000D0F8A File Offset: 0x000CF18A
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x000D0F91 File Offset: 0x000CF191
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400278E RID: 10126
		private int <>1__state;

		// Token: 0x0400278F RID: 10127
		private object <>2__current;

		// Token: 0x04002790 RID: 10128
		public UIPlayerScroll <>4__this;

		// Token: 0x04002791 RID: 10129
		private float <t>5__2;
	}

	// Token: 0x02000588 RID: 1416
	[CompilerGenerated]
	private sealed class <OpenSequence>d__60 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002544 RID: 9540 RVA: 0x000D0F99 File Offset: 0x000CF199
		[DebuggerHidden]
		public <OpenSequence>d__60(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000D0FA8 File Offset: 0x000CF1A8
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x000D0FAC File Offset: 0x000CF1AC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			UIPlayerScroll uiplayerScroll = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				uiplayerScroll.IsOpening = true;
				this.<>2__current = new WaitForSeconds(delay);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				AudioManager.PlayInterfaceSFX(uiplayerScroll.SpawnSFX.GetRandomClip(-1), 1f, UnityEngine.Random.Range(0.93f, 1.07f));
				uiplayerScroll.ScrollAnim.CrossFade("Scroll_Horizontal", 0.05f);
				uiplayerScroll.RevealFX.Play();
				this.<>2__current = new WaitForSeconds(0.8f + delay * 2.1666665f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				uiplayerScroll.SealBurstFX.Play();
				uiplayerScroll.RarityFX.OpenFX.Play();
				uiplayerScroll.ScrollAnim.CrossFade("Open", 0.1f);
				AudioManager.PlayInterfaceSFX(uiplayerScroll.RarityFX.OpenSFX.GetRandomClip(-1), 1f, UnityEngine.Random.Range(0.93f, 1.07f));
				this.<>2__current = new WaitForSeconds(0.594f);
				this.<>1__state = 3;
				return true;
			case 3:
				this.<>1__state = -1;
				uiplayerScroll.RarityFX.SpinFX.Play();
				t = 0f;
				break;
			case 4:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t >= 1f)
			{
				uiplayerScroll.IsOpening = false;
				return false;
			}
			t += Time.deltaTime / uiplayerScroll.UnwrapTime;
			float x = uiplayerScroll.UnwrapCurve.Evaluate(t) * uiplayerScroll.UnwrapWidth;
			uiplayerScroll.maskRect.sizeDelta = new Vector2(x, uiplayerScroll.maskRect.sizeDelta.y);
			this.<>2__current = true;
			this.<>1__state = 4;
			return true;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x000D119F File Offset: 0x000CF39F
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000D11A7 File Offset: 0x000CF3A7
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x000D11AE File Offset: 0x000CF3AE
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002792 RID: 10130
		private int <>1__state;

		// Token: 0x04002793 RID: 10131
		private object <>2__current;

		// Token: 0x04002794 RID: 10132
		public UIPlayerScroll <>4__this;

		// Token: 0x04002795 RID: 10133
		public float delay;

		// Token: 0x04002796 RID: 10134
		private float <t>5__2;
	}
}
