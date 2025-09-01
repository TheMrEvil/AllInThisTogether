using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200019E RID: 414
public class ChoiceOption : MonoBehaviour
{
	// Token: 0x0600114B RID: 4427 RVA: 0x0006B2FA File Offset: 0x000694FA
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0006B308 File Offset: 0x00069508
	public void Setup(Choice choice, bool startVisible)
	{
		this.choice = choice;
		this.choice.optionUI = this;
		this.ActionIcon.sprite = choice.Icon;
		this.TitleText.text = choice.Title;
		this.TitleText.color = GameDB.Quality(choice.Rarity).PlayerColor;
		this.DetailText.text = choice.Detail;
		this.VoteGroup.alpha = (float)(choice.NeedsVote ? 1 : 0);
		this.SetupBackground();
		this.SetupIndicators();
		this.dissolveMat = new Material(this.DissolveImage.material);
		this.DissolveImage.material = this.dissolveMat;
		this.dissolveMat.SetFloat("_Dissolve", 0f);
		this.dissolveMat.SetFloat("_Offset", UnityEngine.Random.Range(0f, 0.95f));
		this.mainGroup.alpha = 0f;
		this.ClearKeywords();
		if (choice.IsAugment)
		{
			this.SetupKeywords();
		}
		if (startVisible)
		{
			this.Reveal();
		}
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Click));
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x0006B440 File Offset: 0x00069640
	private void SetupKeywords()
	{
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(this.choice.Augment.Root.Detail, PlayerControl.myInstance))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordList, ref this.keywords, PlayerControl.myInstance);
		}
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x0006B4BC File Offset: 0x000696BC
	private void ShowGenreAugments()
	{
		if (this.choice == null || this.choice.Genre == null || this.choice.Genre.Root.WorldOptions == null)
		{
			return;
		}
		GenreWorldNode genreWorldNode = this.choice.Genre.Root.WorldOptions as GenreWorldNode;
		foreach (AugmentTree augment in genreWorldNode.PlayerAugments)
		{
			KeywordBoxUI.CreateBox(augment, this.KeywordList, ref this.keywords, PlayerControl.myInstance);
		}
		foreach (AugmentTree augment2 in genreWorldNode.EnemyAugments)
		{
			KeywordBoxUI.CreateBox(augment2, this.KeywordList, ref this.keywords, PlayerControl.myInstance);
		}
		foreach (AugmentTree augment3 in genreWorldNode.WorldAugments)
		{
			KeywordBoxUI.CreateBox(augment3, this.KeywordList, ref this.keywords, PlayerControl.myInstance);
		}
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x0006B614 File Offset: 0x00069814
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

	// Token: 0x06001150 RID: 4432 RVA: 0x0006B680 File Offset: 0x00069880
	private void Update()
	{
		this.UpdateVotePips();
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0006B688 File Offset: 0x00069888
	private void UpdateVotePips()
	{
		if (this.choice == null || !this.choice.NeedsVote || this.choice.VoteIndex < 0)
		{
			return;
		}
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, int> keyValuePair in VoteManager.PlayerVotes)
		{
			if (keyValuePair.Value == this.choice.VoteIndex && keyValuePair.Key != PlayerControl.myInstance.view.OwnerActorNr)
			{
				list.Add(keyValuePair.Key);
			}
		}
		if (VoteManager.MyCurrentVote == this.choice.VoteIndex)
		{
			this.MyVotePip.Setup(true);
		}
		else
		{
			this.MyVotePip.gameObject.SetActive(false);
		}
		for (int i = 0; i < this.VotePips.Count; i++)
		{
			if (i >= list.Count)
			{
				this.VotePips[i].gameObject.SetActive(false);
			}
			else
			{
				this.VotePips[i].gameObject.SetActive(true);
				this.VotePips[i].Setup(list[i]);
			}
		}
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0006B7D0 File Offset: 0x000699D0
	public void SetupBackground()
	{
		foreach (ChoiceOption.VisualElement visualElement in this.Backgrounds)
		{
			visualElement.ActiveObj.SetActive(visualElement.Choice == this.choice.ChoiceType);
		}
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x0006B83C File Offset: 0x00069A3C
	public void SetupIndicators()
	{
		this.ElementDisplay.SetActive(this.choice.ChoiceType == ChoiceType.PlayerScroll);
		if (this.ElementDisplay.activeSelf)
		{
			this.ElementIcon.sprite = GameDB.GetElement(this.choice.Augment.Root.magicColor).Icon;
		}
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x0006B899 File Offset: 0x00069A99
	public void Click()
	{
		if (!this.isRevealed)
		{
			this.Reveal();
			return;
		}
		this.Choose();
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x0006B8B0 File Offset: 0x00069AB0
	public void Choose()
	{
		Choice choice = this.choice;
		if (choice == null)
		{
			return;
		}
		choice.Choose();
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x0006B8C4 File Offset: 0x00069AC4
	public void PointerEnter()
	{
		this.Reveal();
		this.KeywordList.gameObject.SetActive(true);
		if (this.choice != null && this.choice.Augment != null)
		{
			PlayerControl.myInstance.Display.ShowRange(this.choice.Augment.Root.DisplayRadius);
		}
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x0006B927 File Offset: 0x00069B27
	public void PointerExit()
	{
		PlayerControl.myInstance.Display.ReleaseRange();
		this.KeywordList.gameObject.SetActive(false);
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0006B949 File Offset: 0x00069B49
	private void Reveal()
	{
		if (this.isRevealing || this.isRevealed)
		{
			return;
		}
		this.isRevealing = true;
		this.mainGroup.alpha = 1f;
		base.StartCoroutine("RevealRoutine");
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x0006B97F File Offset: 0x00069B7F
	private IEnumerator RevealRoutine()
	{
		float t = 0f;
		this.RevealFX.Play();
		while (t < 1f)
		{
			t += Time.deltaTime;
			Material material = this.dissolveMat;
			if (material != null)
			{
				material.SetFloat("_Dissolve", t);
			}
			yield return true;
		}
		this.isRevealed = true;
		yield break;
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x0006B98E File Offset: 0x00069B8E
	public ChoiceOption()
	{
	}

	// Token: 0x04000FC3 RID: 4035
	public CanvasGroup mainGroup;

	// Token: 0x04000FC4 RID: 4036
	public Image ActionIcon;

	// Token: 0x04000FC5 RID: 4037
	public TextMeshProUGUI TitleText;

	// Token: 0x04000FC6 RID: 4038
	public TextMeshProUGUI DetailText;

	// Token: 0x04000FC7 RID: 4039
	[Header("Voting")]
	public int ChoiceID;

	// Token: 0x04000FC8 RID: 4040
	public CanvasGroup VoteGroup;

	// Token: 0x04000FC9 RID: 4041
	public VoteIcon MyVotePip;

	// Token: 0x04000FCA RID: 4042
	public List<VoteIcon> VotePips;

	// Token: 0x04000FCB RID: 4043
	[Header("Reveal")]
	public Image DissolveImage;

	// Token: 0x04000FCC RID: 4044
	public ParticleSystem RevealFX;

	// Token: 0x04000FCD RID: 4045
	private Material dissolveMat;

	// Token: 0x04000FCE RID: 4046
	[Header("Choice Types")]
	public List<ChoiceOption.VisualElement> Backgrounds;

	// Token: 0x04000FCF RID: 4047
	public GameObject ElementDisplay;

	// Token: 0x04000FD0 RID: 4048
	public Image ElementIcon;

	// Token: 0x04000FD1 RID: 4049
	public GameObject InkHolder;

	// Token: 0x04000FD2 RID: 4050
	public TextMeshProUGUI InkText;

	// Token: 0x04000FD3 RID: 4051
	public RectTransform KeywordList;

	// Token: 0x04000FD4 RID: 4052
	private List<KeywordBoxUI> keywords = new List<KeywordBoxUI>();

	// Token: 0x04000FD5 RID: 4053
	private Animator animator;

	// Token: 0x04000FD6 RID: 4054
	private Choice choice;

	// Token: 0x04000FD7 RID: 4055
	private bool isRevealed;

	// Token: 0x04000FD8 RID: 4056
	private bool isRevealing;

	// Token: 0x02000569 RID: 1385
	[Serializable]
	public class VisualElement
	{
		// Token: 0x060024D3 RID: 9427 RVA: 0x000CF6BE File Offset: 0x000CD8BE
		public VisualElement()
		{
		}

		// Token: 0x0400270F RID: 9999
		public ChoiceType Choice;

		// Token: 0x04002710 RID: 10000
		public GameObject ActiveObj;
	}

	// Token: 0x0200056A RID: 1386
	[CompilerGenerated]
	private sealed class <RevealRoutine>d__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024D4 RID: 9428 RVA: 0x000CF6C6 File Offset: 0x000CD8C6
		[DebuggerHidden]
		public <RevealRoutine>d__36(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000CF6D5 File Offset: 0x000CD8D5
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000CF6D8 File Offset: 0x000CD8D8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ChoiceOption choiceOption = this;
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
				t = 0f;
				choiceOption.RevealFX.Play();
			}
			if (t >= 1f)
			{
				choiceOption.isRevealed = true;
				return false;
			}
			t += Time.deltaTime;
			Material dissolveMat = choiceOption.dissolveMat;
			if (dissolveMat != null)
			{
				dissolveMat.SetFloat("_Dissolve", t);
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x000CF77A File Offset: 0x000CD97A
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000CF782 File Offset: 0x000CD982
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x000CF789 File Offset: 0x000CD989
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002711 RID: 10001
		private int <>1__state;

		// Token: 0x04002712 RID: 10002
		private object <>2__current;

		// Token: 0x04002713 RID: 10003
		public ChoiceOption <>4__this;

		// Token: 0x04002714 RID: 10004
		private float <t>5__2;
	}
}
