using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001ED RID: 493
public class RaidDifficultyUI : MonoBehaviour
{
	// Token: 0x06001500 RID: 5376 RVA: 0x00083CBC File Offset: 0x00081EBC
	private void Awake()
	{
		RaidDifficultyUI.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEnteredPanel));
		VoteManager.OnVotesChanged = (Action<ChoiceType>)Delegate.Combine(VoteManager.OnVotesChanged, new Action<ChoiceType>(this.OnVotesChanged));
		this.startWidth = this.ChoiceHolder.rect.width;
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x00083D30 File Offset: 0x00081F30
	private void OnEnteredPanel()
	{
		this.ChoiceHolder.sizeDelta = new Vector2(this.startWidth, this.ChoiceHolder.rect.height);
		this.NormalGrp.ShowImmediate();
		this.HardGrp.ShowImmediate();
		AudioManager.PlayInterfaceSFX(this.OpenSFX, 1f, 0f);
		this.ReloadVotes();
		this.UpdateVoting();
		this.HardModeDisplay.SetActive(Progression.InkLevel < 30);
		RaidDB.Raid raid = RaidDB.GetRaid(RaidManager.RaidType);
		if (raid != null)
		{
			this.Title.text = raid.RaidName;
			this.NormalBackground.sprite = raid.NormalDifficultyBgr;
			this.HardBackground.sprite = raid.HardDifficultyBgr;
			this.NormalInfo.text = raid.NormalDifficultyInfo;
			this.HardInfo.text = raid.HardDifficultyInfo;
		}
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x00083E14 File Offset: 0x00082014
	public void VoteStarted()
	{
		if (PanelManager.CurPanel == PanelType.RaidDifficulty)
		{
			return;
		}
		PanelManager.instance.PushPanel(PanelType.RaidDifficulty);
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x00083E2C File Offset: 0x0008202C
	public void AllowHardMode(bool allow)
	{
		this.hardModeAllowed = allow;
		foreach (GameObject gameObject in this.HardUnlockedDisplays)
		{
			gameObject.SetActive(allow);
		}
		foreach (GameObject gameObject2 in this.HardLockedDisplays)
		{
			gameObject2.SetActive(!allow);
		}
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x00083EC8 File Offset: 0x000820C8
	public void VoteNormal()
	{
		VoteManager.Vote(0);
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x00083ED0 File Offset: 0x000820D0
	public void VoteHard()
	{
		if (!this.hardModeAllowed)
		{
			return;
		}
		VoteManager.Vote(1);
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x00083EE1 File Offset: 0x000820E1
	public void VoteCompleted(int winner)
	{
		base.StartCoroutine("CompleteRoutine", (winner == 0) ? this.HardGrp : this.NormalGrp);
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x00083F00 File Offset: 0x00082100
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
		if (PanelManager.CurPanel == PanelType.RaidDifficulty)
		{
			PanelManager.instance.PopPanel();
		}
		yield break;
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x00083F16 File Offset: 0x00082116
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.RaidDifficulty)
		{
			return;
		}
		this.UpdateVoting();
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x00083F28 File Offset: 0x00082128
	private void UpdateVoting()
	{
		bool flag = PlayerControl.PlayerCount > 1 && VoteManager.IsVoting && VoteManager.CurrentVote == ChoiceType.RaidDifficulty;
		flag &= (VoteManager.PlayerVotes.Count > 0 && VoteManager.IsTimed);
		this.VoteGroup.UpdateOpacity(flag, 4f, true);
		if (!flag)
		{
			return;
		}
		this.VoteProgress.fillAmount = GameplayManager.instance.Timer / 30f;
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x00083F99 File Offset: 0x00082199
	private void OnVotesChanged(ChoiceType ct)
	{
		if (ct != ChoiceType.RaidDifficulty)
		{
			return;
		}
		this.ReloadVotes();
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x00083FA8 File Offset: 0x000821A8
	private void ReloadVotes()
	{
		foreach (UIPlayerVoteDisplay uiplayerVoteDisplay in this.votePips)
		{
			UnityEngine.Object.Destroy(uiplayerVoteDisplay.gameObject);
		}
		this.votePips.Clear();
		foreach (KeyValuePair<int, int> keyValuePair in VoteManager.PlayerVotes)
		{
			Transform holder = (keyValuePair.Value == 0) ? this.NormalVoteHolder : this.HardVoteHolder;
			this.AddVoteDisplay(keyValuePair.Key, holder);
		}
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x00084068 File Offset: 0x00082268
	private void AddVoteDisplay(int PlayerID, Transform holder)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.VotePipRef, holder);
		gameObject.gameObject.SetActive(true);
		UIPlayerVoteDisplay component = gameObject.GetComponent<UIPlayerVoteDisplay>();
		component.Setup(PlayerID);
		this.votePips.Add(component);
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x000840A6 File Offset: 0x000822A6
	public RaidDifficultyUI()
	{
	}

	// Token: 0x0400146C RID: 5228
	public static RaidDifficultyUI instance;

	// Token: 0x0400146D RID: 5229
	public TextMeshProUGUI Title;

	// Token: 0x0400146E RID: 5230
	public AudioClip OpenSFX;

	// Token: 0x0400146F RID: 5231
	public Image NormalBackground;

	// Token: 0x04001470 RID: 5232
	public TextMeshProUGUI NormalInfo;

	// Token: 0x04001471 RID: 5233
	public Image HardBackground;

	// Token: 0x04001472 RID: 5234
	public TextMeshProUGUI HardInfo;

	// Token: 0x04001473 RID: 5235
	public GameObject HardModeDisplay;

	// Token: 0x04001474 RID: 5236
	public CanvasGroup VoteGroup;

	// Token: 0x04001475 RID: 5237
	public Image VoteProgress;

	// Token: 0x04001476 RID: 5238
	public GameObject VotePipRef;

	// Token: 0x04001477 RID: 5239
	public Transform NormalVoteHolder;

	// Token: 0x04001478 RID: 5240
	public Transform HardVoteHolder;

	// Token: 0x04001479 RID: 5241
	public List<GameObject> HardUnlockedDisplays;

	// Token: 0x0400147A RID: 5242
	public List<GameObject> HardLockedDisplays;

	// Token: 0x0400147B RID: 5243
	private List<UIPlayerVoteDisplay> votePips = new List<UIPlayerVoteDisplay>();

	// Token: 0x0400147C RID: 5244
	public RectTransform ChoiceHolder;

	// Token: 0x0400147D RID: 5245
	private float startWidth = 500f;

	// Token: 0x0400147E RID: 5246
	public CanvasGroup NormalGrp;

	// Token: 0x0400147F RID: 5247
	public CanvasGroup HardGrp;

	// Token: 0x04001480 RID: 5248
	public AudioClip CompletedSFX;

	// Token: 0x04001481 RID: 5249
	private bool hardModeAllowed;

	// Token: 0x020005BA RID: 1466
	[CompilerGenerated]
	private sealed class <CompleteRoutine>d__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025F4 RID: 9716 RVA: 0x000D2843 File Offset: 0x000D0A43
		[DebuggerHidden]
		public <CompleteRoutine>d__29(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000D2852 File Offset: 0x000D0A52
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000D2854 File Offset: 0x000D0A54
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			RaidDifficultyUI raidDifficultyUI = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				AudioManager.PlayInterfaceSFX(raidDifficultyUI.CompletedSFX, 1f, 0f);
				t = 0f;
				x = raidDifficultyUI.startWidth;
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
				if (PanelManager.CurPanel == PanelType.RaidDifficulty)
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
			raidDifficultyUI.ChoiceHolder.sizeDelta = new Vector2(x, 0f);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x000D29A6 File Offset: 0x000D0BA6
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000D29AE File Offset: 0x000D0BAE
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x000D29B5 File Offset: 0x000D0BB5
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002856 RID: 10326
		private int <>1__state;

		// Token: 0x04002857 RID: 10327
		private object <>2__current;

		// Token: 0x04002858 RID: 10328
		public RaidDifficultyUI <>4__this;

		// Token: 0x04002859 RID: 10329
		public CanvasGroup toFade;

		// Token: 0x0400285A RID: 10330
		private float <t>5__2;

		// Token: 0x0400285B RID: 10331
		private float <x>5__3;
	}
}
