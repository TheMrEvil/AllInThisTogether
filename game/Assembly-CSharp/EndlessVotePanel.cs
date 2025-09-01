using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001DD RID: 477
public class EndlessVotePanel : MonoBehaviour
{
	// Token: 0x060013E2 RID: 5090 RVA: 0x0007BCC4 File Offset: 0x00079EC4
	private void Awake()
	{
		EndlessVotePanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnteredPanel));
		VoteManager.OnVotesChanged = (Action<ChoiceType>)Delegate.Combine(VoteManager.OnVotesChanged, new Action<ChoiceType>(this.OnVotesChanged));
		this.startWidth = this.ChoiceHolder.rect.width;
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x0007BD38 File Offset: 0x00079F38
	private void OnEnteredPanel()
	{
		this.ChoiceHolder.sizeDelta = new Vector2(this.startWidth, this.ChoiceHolder.rect.height);
		this.EndlessGrp.ShowImmediate();
		this.LibraryGrp.ShowImmediate();
		AudioManager.PlayInterfaceSFX(this.OpenSFX, 1f, 0f);
		this.ReloadVotes();
		this.UpdateVoting();
		this.BaseHeader.SetActive(!GameplayManager.IsChallengeActive);
		this.ChallengeDisplay.SetActive(GameplayManager.IsChallengeActive);
		if (GameplayManager.IsChallengeActive)
		{
			this.SetupChallengeDisplay();
		}
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x0007BDD5 File Offset: 0x00079FD5
	public void VoteStarted()
	{
		if (PanelManager.CurPanel == PanelType.EndlessSelection)
		{
			return;
		}
		PanelManager.instance.PushPanel(PanelType.EndlessSelection);
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x0007BDED File Offset: 0x00079FED
	public void VoteLibrary()
	{
		VoteManager.Vote(0);
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x0007BDF5 File Offset: 0x00079FF5
	public void VoteEndless()
	{
		VoteManager.Vote(1);
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x0007BDFD File Offset: 0x00079FFD
	public void VoteCompleted(int winner)
	{
		base.StartCoroutine("CompleteRoutine", (winner == 0) ? this.EndlessGrp : this.LibraryGrp);
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x0007BE1C File Offset: 0x0007A01C
	private IEnumerator CompleteRoutine(CanvasGroup toFade)
	{
		AudioManager.PlayInterfaceSFX(this.CompletedSFX, 1f, 0f);
		float t = 0f;
		float x = this.startWidth;
		while (t < 1f)
		{
			t += Time.deltaTime;
			toFade.UpdateOpacity(false, 6f, false);
			x = Mathf.Lerp(x, 0f, Time.deltaTime * 4f);
			this.ChoiceHolder.sizeDelta = new Vector2(x, 0f);
			yield return true;
		}
		yield return new WaitForSeconds(0.25f);
		PostFXManager.instance.ExitBlur();
		yield return new WaitForSeconds(0.4f);
		if (PanelManager.CurPanel == PanelType.EndlessSelection)
		{
			PanelManager.instance.PopPanel();
		}
		yield break;
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x0007BE32 File Offset: 0x0007A032
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.EndlessSelection)
		{
			return;
		}
		this.UpdateVoting();
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x0007BE44 File Offset: 0x0007A044
	private void UpdateVoting()
	{
		bool flag = PlayerControl.PlayerCount > 1 && VoteManager.IsVoting && VoteManager.CurrentVote == ChoiceType.Endless;
		flag &= (VoteManager.PlayerVotes.Count > 0 && VoteManager.IsTimed);
		this.VoteGroup.UpdateOpacity(flag, 4f, true);
		if (!flag)
		{
			return;
		}
		this.VoteProgress.fillAmount = GameplayManager.instance.Timer / 30f;
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x0007BEB5 File Offset: 0x0007A0B5
	private void OnVotesChanged(ChoiceType ct)
	{
		if (ct != ChoiceType.Endless)
		{
			return;
		}
		this.ReloadVotes();
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x0007BEC4 File Offset: 0x0007A0C4
	private void ReloadVotes()
	{
		foreach (UIPlayerVoteDisplay uiplayerVoteDisplay in this.votePips)
		{
			UnityEngine.Object.Destroy(uiplayerVoteDisplay.gameObject);
		}
		this.votePips.Clear();
		foreach (KeyValuePair<int, int> keyValuePair in VoteManager.PlayerVotes)
		{
			Transform holder = (keyValuePair.Value == 0) ? this.LibraryVoteHolder : this.EndlessVoteHolder;
			this.AddVoteDisplay(keyValuePair.Key, holder);
		}
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x0007BF84 File Offset: 0x0007A184
	private void AddVoteDisplay(int PlayerID, Transform holder)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.VotePipRef, holder);
		gameObject.gameObject.SetActive(true);
		UIPlayerVoteDisplay component = gameObject.GetComponent<UIPlayerVoteDisplay>();
		component.Setup(PlayerID);
		this.votePips.Add(component);
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x0007BFC4 File Offset: 0x0007A1C4
	private void SetupChallengeDisplay()
	{
		this.ChallengeTimeText.text = TimeSpan.FromSeconds((double)GameplayManager.ChallengeBaseTime).ToString("hh':'mm':'ss");
		this.ChallengeStatTitle.text = GameplayManager.Challenge.GetStatLabel() + ":";
		this.ChallengeStatText.text = string.Format("{0:n0}", GameplayManager.ChallengeUniqueStat);
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x0007C032 File Offset: 0x0007A232
	public EndlessVotePanel()
	{
	}

	// Token: 0x040012FA RID: 4858
	public static EndlessVotePanel instance;

	// Token: 0x040012FB RID: 4859
	public AudioClip OpenSFX;

	// Token: 0x040012FC RID: 4860
	public CanvasGroup VoteGroup;

	// Token: 0x040012FD RID: 4861
	public Image VoteProgress;

	// Token: 0x040012FE RID: 4862
	public GameObject VotePipRef;

	// Token: 0x040012FF RID: 4863
	public Transform LibraryVoteHolder;

	// Token: 0x04001300 RID: 4864
	public Transform EndlessVoteHolder;

	// Token: 0x04001301 RID: 4865
	private List<UIPlayerVoteDisplay> votePips = new List<UIPlayerVoteDisplay>();

	// Token: 0x04001302 RID: 4866
	public RectTransform ChoiceHolder;

	// Token: 0x04001303 RID: 4867
	private float startWidth = 500f;

	// Token: 0x04001304 RID: 4868
	public CanvasGroup EndlessGrp;

	// Token: 0x04001305 RID: 4869
	public CanvasGroup LibraryGrp;

	// Token: 0x04001306 RID: 4870
	public AudioClip CompletedSFX;

	// Token: 0x04001307 RID: 4871
	public GameObject BaseHeader;

	// Token: 0x04001308 RID: 4872
	public GameObject ChallengeDisplay;

	// Token: 0x04001309 RID: 4873
	public TextMeshProUGUI ChallengeTimeText;

	// Token: 0x0400130A RID: 4874
	public TextMeshProUGUI ChallengeStatTitle;

	// Token: 0x0400130B RID: 4875
	public TextMeshProUGUI ChallengeStatText;

	// Token: 0x020005A7 RID: 1447
	[CompilerGenerated]
	private sealed class <CompleteRoutine>d__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025A4 RID: 9636 RVA: 0x000D1EB7 File Offset: 0x000D00B7
		[DebuggerHidden]
		public <CompleteRoutine>d__24(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000D1EC6 File Offset: 0x000D00C6
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000D1EC8 File Offset: 0x000D00C8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			EndlessVotePanel endlessVotePanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				AudioManager.PlayInterfaceSFX(endlessVotePanel.CompletedSFX, 1f, 0f);
				t = 0f;
				x = endlessVotePanel.startWidth;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				PostFXManager.instance.ExitBlur();
				this.<>2__current = new WaitForSeconds(0.4f);
				this.<>1__state = 3;
				return true;
			case 3:
				this.<>1__state = -1;
				if (PanelManager.CurPanel == PanelType.EndlessSelection)
				{
					PanelManager.instance.PopPanel();
				}
				return false;
			default:
				return false;
			}
			if (t >= 1f)
			{
				this.<>2__current = new WaitForSeconds(0.25f);
				this.<>1__state = 2;
				return true;
			}
			t += Time.deltaTime;
			toFade.UpdateOpacity(false, 6f, false);
			x = Mathf.Lerp(x, 0f, Time.deltaTime * 4f);
			endlessVotePanel.ChoiceHolder.sizeDelta = new Vector2(x, 0f);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000D201A File Offset: 0x000D021A
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000D2022 File Offset: 0x000D0222
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000D2029 File Offset: 0x000D0229
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002810 RID: 10256
		private int <>1__state;

		// Token: 0x04002811 RID: 10257
		private object <>2__current;

		// Token: 0x04002812 RID: 10258
		public EndlessVotePanel <>4__this;

		// Token: 0x04002813 RID: 10259
		public CanvasGroup toFade;

		// Token: 0x04002814 RID: 10260
		private float <t>5__2;

		// Token: 0x04002815 RID: 10261
		private float <x>5__3;
	}
}
