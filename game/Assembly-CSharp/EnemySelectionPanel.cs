using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000194 RID: 404
public class EnemySelectionPanel : MonoBehaviour
{
	// Token: 0x060010F6 RID: 4342 RVA: 0x00069850 File Offset: 0x00067A50
	private void Awake()
	{
		EnemySelectionPanel.instance = this;
		EnemySelectionPanel.IsLoadingScrolls = false;
		EnemySelectionPanel.CompletedLoad = false;
		this.panel = base.GetComponent<UIPanel>();
		UIPanel uipanel = this.panel;
		uipanel.OnEnteredPanel = (Action)Delegate.Combine(uipanel.OnEnteredPanel, new Action(this.OnEnteredPanel));
		foreach (EnemySelectionPanel.EnemyBox enemyBox in this.Boxes)
		{
			enemyBox.StartPoint = enemyBox.Holder.anchoredPosition;
		}
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x000698F8 File Offset: 0x00067AF8
	private void OnEnteredPanel()
	{
		this.SetupChapterPips();
		this.panel.CanBackOut = !RaidManager.IsInRaid;
		if (EnemySelectionPanel.CompletedLoad && InputManager.IsUsingController)
		{
			UISelector.SelectSelectable(this.DefaultSelect);
		}
		if (TutorialManager.InTutorial)
		{
			UITutorial.TryTutorial(UITutorial.Tutorial.EnemyUpgrade);
		}
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x00069945 File Offset: 0x00067B45
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.EnemyAugments)
		{
			return;
		}
		this.ProgressGroup.UpdateOpacity(!RaidManager.IsInRaid, 4f, true);
		this.UpdateVoting();
		if (GameplayManager.CurState == GameState.InWave)
		{
			PanelManager.instance.PopPanel();
		}
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x00069984 File Offset: 0x00067B84
	private void SetupChapterPips()
	{
		int num = WaveManager.CurrentWave + 1;
		if (RaidManager.IsInRaid)
		{
			RaidDB.Encounter encounter = RaidDB.GetEncounter(RaidManager.RaidType, RaidManager.instance.CurrentEncounter);
			this.ChapterText.text = encounter.EnemyTopText;
			this.CoreText.text = encounter.EnemyMainText;
		}
		else
		{
			this.ChapterText.text = "Chapter  <size=48>" + (num + 1).ToString();
			this.CoreText.text = "The Torn Hunger...";
		}
		GenreTree gameGraph = GameplayManager.instance.GameGraph;
		if (((gameGraph != null) ? gameGraph.Root.Waves : null) == null)
		{
			return;
		}
		if (WaveManager.instance.AppendixLevel > 0)
		{
			num = WaveManager.instance.AppendixChapterNumber;
			this.ChapterText.text = string.Format("Appendix  <size=48>{0}.{1}", WaveManager.instance.AppendixLevel, num + 1);
		}
		this.ProgressDisplay.Refresh();
		UnityMainThreadDispatcher.Instance().Invoke(delegate
		{
			this.ProgressDisplay.Refresh();
		}, 0.5f);
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x00069A94 File Offset: 0x00067C94
	private void UpdateVoting()
	{
		bool flag = PlayerControl.PlayerCount > 1 && VoteManager.IsVoting && VoteManager.CurrentVote == ChoiceType.EnemyScroll;
		flag &= (VoteManager.PlayerVotes.Count > 0);
		this.VoteGroup.UpdateOpacity(flag, 4f, true);
		if (!flag)
		{
			return;
		}
		this.VoteProgress.fillAmount = GameplayManager.instance.Timer / 30f;
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x00069B00 File Offset: 0x00067D00
	public static void LoadEnemyScrolls(List<AugmentTree> mods)
	{
		EnemySelectionPanel.IsLoadingScrolls = true;
		EnemySelectionPanel.CompletedLoad = false;
		List<ValueTuple<AugmentTree, int>> list = new List<ValueTuple<AugmentTree, int>>();
		for (int i = 0; i < mods.Count; i++)
		{
			list.Add(new ValueTuple<AugmentTree, int>(mods[i], i));
		}
		float x = 900f;
		if (mods.Count == 1)
		{
			x = 0f;
		}
		EnemySelectionPanel.instance.ChoiceHolder.sizeDelta = new Vector2(x, 100f);
		EnemySelectionPanel.instance.OpenChoices(list);
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x00069B7D File Offset: 0x00067D7D
	public void GoToUI()
	{
		if (PanelManager.CurPanel == PanelType.EnemyAugments)
		{
			return;
		}
		if (PanelManager.CurPanel != PanelType.GameInvisible)
		{
			PanelManager.instance.GoToPanel(PanelType.GameInvisible);
		}
		PanelManager.instance.PushPanel(PanelType.EnemyAugments);
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x00069BA8 File Offset: 0x00067DA8
	private void OpenChoices(List<ValueTuple<AugmentTree, int>> options)
	{
		this.GoToUI();
		List<string> list = new List<string>();
		foreach (ValueTuple<AugmentTree, int> valueTuple in options)
		{
			list.Add(valueTuple.Item1.ID);
		}
		Progression.SawAugment(list);
		if (this.curRoutine != null)
		{
			base.StopCoroutine(this.curRoutine);
		}
		this.curRoutine = base.StartCoroutine(this.LoadSequence(options));
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x00069C3C File Offset: 0x00067E3C
	private IEnumerator LoadSequence([TupleElementNames(new string[]
	{
		"tree",
		"id"
	})] List<ValueTuple<AugmentTree, int>> options)
	{
		GenreRewardNode genreRewardNode = RewardManager.instance.RewardConfig();
		AugmentQuality danger = this.DifficultyToRarityDisplay((genreRewardNode != null) ? genreRewardNode.EnemyDifficulty : 0);
		int num = Mathf.Min(options.Count, this.Boxes.Count);
		int num2;
		for (int i = 0; i < num; i = num2 + 1)
		{
			AudioManager.PlayInterfaceSFX(this.ChoiceCreated, 1f, 0f);
			ValueTuple<AugmentTree, int> valueTuple = options[i];
			this.Boxes[i].Reset();
			this.Boxes[i].Choice.gameObject.SetActive(true);
			this.Boxes[i].Choice.Setup(valueTuple.Item1, valueTuple.Item2, danger);
			yield return new WaitForSecondsRealtime(this.ChoiceDelay);
			num2 = i;
		}
		foreach (EnemySelectionPanel.EnemyBox enemyBox in this.Boxes)
		{
			enemyBox.Choice.Button.interactable = true;
		}
		EnemySelectionPanel.CompletedLoad = true;
		if (InputManager.IsUsingController)
		{
			UISelector.SelectSelectable(this.DefaultSelect);
		}
		yield break;
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x00069C54 File Offset: 0x00067E54
	private AugmentQuality DifficultyToRarityDisplay(int difficulty)
	{
		AugmentQuality result;
		if (difficulty < 50)
		{
			if (difficulty >= 25)
			{
				result = AugmentQuality.Normal;
			}
			else
			{
				result = AugmentQuality.Basic;
			}
		}
		else if (difficulty >= 75)
		{
			result = AugmentQuality.Legendary;
		}
		else
		{
			result = AugmentQuality.Epic;
		}
		return result;
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x00069C83 File Offset: 0x00067E83
	public void VoteEnemyScroll(int ChoiceID)
	{
		AudioManager.PlayInterfaceSFX(this.EnemyScrollSelected.GetRandomClip(-1), 1f, 0f);
		if (VoteManager.MyCurrentVote != ChoiceID)
		{
			VoteManager.Vote(ChoiceID);
		}
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x00069CAE File Offset: 0x00067EAE
	public void AugmentChosen(int id)
	{
		if (this.curRoutine != null)
		{
			base.StopCoroutine(this.curRoutine);
		}
		this.curRoutine = base.StartCoroutine(this.ExitSequence(id));
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x00069CD7 File Offset: 0x00067ED7
	private IEnumerator ExitSequence(int winnerID)
	{
		EnemySelectionPanel.EnemyBox winnerBox = null;
		AudioManager.PlayInterfaceSFX(this.CloseOthers, 1f, 0f);
		PlayerControl.myInstance.Display.ReleaseRange();
		foreach (EnemySelectionPanel.EnemyBox enemyBox in this.Boxes)
		{
			if (enemyBox.Choice.ChoiceID == winnerID)
			{
				winnerBox = enemyBox;
				enemyBox.Choice.anim.Play("EnemyBox_SelectedTop");
				if (winnerBox.Choice.Augment != null)
				{
					UnityEngine.Debug.Log("Torn Page Added: " + winnerBox.Choice.Augment.Root.Name);
				}
				if (enemyBox.Choice.isHidden)
				{
					GameHUD.instance.GotRewardAugment(enemyBox.Choice.Augment);
				}
			}
			else
			{
				enemyBox.Choice.anim.Play("EnemyBox_Close");
			}
			enemyBox.Choice.RevealIfHidden();
			enemyBox.Choice.Button.interactable = false;
		}
		yield return new WaitForSecondsRealtime(1f);
		if (winnerBox != null)
		{
			winnerBox.Choice.anim.Play("EnemyBox_SelectedTopIn");
			yield return new WaitForSecondsRealtime(0.5f);
			Transform curWavePt = this.ProgressDisplay.curWavePt;
			Vector3 wPos = (curWavePt != null) ? curWavePt.position : winnerBox.Choice.transform.position;
			AudioManager.PlayInterfaceSFX(this.MoveCenter, 1f, 0f);
			float t = 0f;
			Vector3 startPt = winnerBox.Holder.position;
			while (t < 1f)
			{
				t += Time.unscaledDeltaTime * 2f;
				winnerBox.Holder.position = Vector3.Lerp(startPt, wPos, this.moveCurve.Evaluate(t));
				yield return true;
			}
			winnerBox.Choice.anim.Play("EnemyBox_SelectedFade");
			wPos = default(Vector3);
			startPt = default(Vector3);
		}
		yield return new WaitForSecondsRealtime(0.5f);
		InfoDisplay.ReleaseText(InfoArea.Title);
		yield return new WaitForSecondsRealtime(0.25f);
		RewardManager.instance.NextReward();
		if (PanelManager.CurPanel == PanelType.EnemyAugments)
		{
			PanelManager.instance.GoToPanel(PanelType.GameInvisible);
		}
		yield break;
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x00069CF0 File Offset: 0x00067EF0
	public void EnemySelectionHovered(EnemyChoiceUI sel)
	{
		this.curSel = sel;
		EnemyChapterProgressItem nextBossPip = this.ProgressDisplay.GetNextBossPip();
		if (nextBossPip == null)
		{
			return;
		}
		nextBossPip.ToggleBossFX(this.curSel.NeedsAnyBossWarning);
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x00069D2C File Offset: 0x00067F2C
	public void EnemySelectionUnhovered(EnemyChoiceUI sel)
	{
		if (this.curSel != sel)
		{
			return;
		}
		EnemyChapterProgressItem nextBossPip = this.ProgressDisplay.GetNextBossPip();
		if (nextBossPip == null)
		{
			return;
		}
		nextBossPip.ToggleBossFX(false);
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x00069D65 File Offset: 0x00067F65
	public EnemySelectionPanel()
	{
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x00069D78 File Offset: 0x00067F78
	[CompilerGenerated]
	private void <SetupChapterPips>b__23_0()
	{
		this.ProgressDisplay.Refresh();
	}

	// Token: 0x04000F52 RID: 3922
	public static EnemySelectionPanel instance;

	// Token: 0x04000F53 RID: 3923
	public List<AudioClip> EnemyScrollSelected;

	// Token: 0x04000F54 RID: 3924
	public AudioClip ChoiceCreated;

	// Token: 0x04000F55 RID: 3925
	public AudioClip CloseOthers;

	// Token: 0x04000F56 RID: 3926
	public AudioClip MoveCenter;

	// Token: 0x04000F57 RID: 3927
	public AnimationCurve moveCurve;

	// Token: 0x04000F58 RID: 3928
	public List<EnemySelectionPanel.EnemyBox> Boxes;

	// Token: 0x04000F59 RID: 3929
	public RectTransform ChoiceHolder;

	// Token: 0x04000F5A RID: 3930
	public float ChoiceDelay = 0.66f;

	// Token: 0x04000F5B RID: 3931
	public Button DefaultSelect;

	// Token: 0x04000F5C RID: 3932
	public CanvasGroup ProgressGroup;

	// Token: 0x04000F5D RID: 3933
	public TextMeshProUGUI ChapterText;

	// Token: 0x04000F5E RID: 3934
	public TextMeshProUGUI CoreText;

	// Token: 0x04000F5F RID: 3935
	public TomeProgressSmall ProgressDisplay;

	// Token: 0x04000F60 RID: 3936
	public CanvasGroup VoteGroup;

	// Token: 0x04000F61 RID: 3937
	public Image VoteProgress;

	// Token: 0x04000F62 RID: 3938
	public static bool IsLoadingScrolls;

	// Token: 0x04000F63 RID: 3939
	public static bool CompletedLoad;

	// Token: 0x04000F64 RID: 3940
	private UIPanel panel;

	// Token: 0x04000F65 RID: 3941
	private Coroutine curRoutine;

	// Token: 0x04000F66 RID: 3942
	private EnemyChoiceUI curSel;

	// Token: 0x02000563 RID: 1379
	[Serializable]
	public class EnemyBox
	{
		// Token: 0x060024B3 RID: 9395 RVA: 0x000CEF9B File Offset: 0x000CD19B
		public void Reset()
		{
			this.Holder.anchoredPosition = this.StartPoint;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000CEFB3 File Offset: 0x000CD1B3
		public EnemyBox()
		{
		}

		// Token: 0x040026F4 RID: 9972
		public Vector3 StartPoint;

		// Token: 0x040026F5 RID: 9973
		public RectTransform Holder;

		// Token: 0x040026F6 RID: 9974
		public EnemyChoiceUI Choice;
	}

	// Token: 0x02000564 RID: 1380
	[CompilerGenerated]
	private sealed class <LoadSequence>d__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024B5 RID: 9397 RVA: 0x000CEFBB File Offset: 0x000CD1BB
		[DebuggerHidden]
		public <LoadSequence>d__28(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000CEFCA File Offset: 0x000CD1CA
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000CEFCC File Offset: 0x000CD1CC
		bool IEnumerator.MoveNext()
		{
			int num2 = this.<>1__state;
			EnemySelectionPanel enemySelectionPanel = this;
			if (num2 != 0)
			{
				if (num2 != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				int num3 = i;
				i = num3 + 1;
			}
			else
			{
				this.<>1__state = -1;
				EnemySelectionPanel enemySelectionPanel2 = enemySelectionPanel;
				GenreRewardNode genreRewardNode = RewardManager.instance.RewardConfig();
				danger = enemySelectionPanel2.DifficultyToRarityDisplay((genreRewardNode != null) ? genreRewardNode.EnemyDifficulty : 0);
				num = Mathf.Min(options.Count, enemySelectionPanel.Boxes.Count);
				i = 0;
			}
			if (i >= num)
			{
				foreach (EnemySelectionPanel.EnemyBox enemyBox in enemySelectionPanel.Boxes)
				{
					enemyBox.Choice.Button.interactable = true;
				}
				EnemySelectionPanel.CompletedLoad = true;
				if (InputManager.IsUsingController)
				{
					UISelector.SelectSelectable(enemySelectionPanel.DefaultSelect);
				}
				return false;
			}
			AudioManager.PlayInterfaceSFX(enemySelectionPanel.ChoiceCreated, 1f, 0f);
			ValueTuple<AugmentTree, int> valueTuple = options[i];
			enemySelectionPanel.Boxes[i].Reset();
			enemySelectionPanel.Boxes[i].Choice.gameObject.SetActive(true);
			enemySelectionPanel.Boxes[i].Choice.Setup(valueTuple.Item1, valueTuple.Item2, danger);
			this.<>2__current = new WaitForSecondsRealtime(enemySelectionPanel.ChoiceDelay);
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x000CF180 File Offset: 0x000CD380
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000CF188 File Offset: 0x000CD388
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x000CF18F File Offset: 0x000CD38F
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026F7 RID: 9975
		private int <>1__state;

		// Token: 0x040026F8 RID: 9976
		private object <>2__current;

		// Token: 0x040026F9 RID: 9977
		public EnemySelectionPanel <>4__this;

		// Token: 0x040026FA RID: 9978
		[TupleElementNames(new string[]
		{
			"tree",
			"id"
		})]
		public List<ValueTuple<AugmentTree, int>> options;

		// Token: 0x040026FB RID: 9979
		private AugmentQuality <danger>5__2;

		// Token: 0x040026FC RID: 9980
		private int <num>5__3;

		// Token: 0x040026FD RID: 9981
		private int <i>5__4;
	}

	// Token: 0x02000565 RID: 1381
	[CompilerGenerated]
	private sealed class <ExitSequence>d__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060024BB RID: 9403 RVA: 0x000CF197 File Offset: 0x000CD397
		[DebuggerHidden]
		public <ExitSequence>d__32(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000CF1A6 File Offset: 0x000CD3A6
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000CF1A8 File Offset: 0x000CD3A8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			EnemySelectionPanel enemySelectionPanel = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				winnerBox = null;
				AudioManager.PlayInterfaceSFX(enemySelectionPanel.CloseOthers, 1f, 0f);
				PlayerControl.myInstance.Display.ReleaseRange();
				foreach (EnemySelectionPanel.EnemyBox enemyBox in enemySelectionPanel.Boxes)
				{
					if (enemyBox.Choice.ChoiceID == winnerID)
					{
						winnerBox = enemyBox;
						enemyBox.Choice.anim.Play("EnemyBox_SelectedTop");
						if (winnerBox.Choice.Augment != null)
						{
							UnityEngine.Debug.Log("Torn Page Added: " + winnerBox.Choice.Augment.Root.Name);
						}
						if (enemyBox.Choice.isHidden)
						{
							GameHUD.instance.GotRewardAugment(enemyBox.Choice.Augment);
						}
					}
					else
					{
						enemyBox.Choice.anim.Play("EnemyBox_Close");
					}
					enemyBox.Choice.RevealIfHidden();
					enemyBox.Choice.Button.interactable = false;
				}
				this.<>2__current = new WaitForSecondsRealtime(1f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				if (winnerBox != null)
				{
					winnerBox.Choice.anim.Play("EnemyBox_SelectedTopIn");
					this.<>2__current = new WaitForSecondsRealtime(0.5f);
					this.<>1__state = 2;
					return true;
				}
				goto IL_2DA;
			case 2:
			{
				this.<>1__state = -1;
				Transform curWavePt = enemySelectionPanel.ProgressDisplay.curWavePt;
				wPos = ((curWavePt != null) ? curWavePt.position : winnerBox.Choice.transform.position);
				AudioManager.PlayInterfaceSFX(enemySelectionPanel.MoveCenter, 1f, 0f);
				t = 0f;
				startPt = winnerBox.Holder.position;
				break;
			}
			case 3:
				this.<>1__state = -1;
				break;
			case 4:
				this.<>1__state = -1;
				InfoDisplay.ReleaseText(InfoArea.Title);
				this.<>2__current = new WaitForSecondsRealtime(0.25f);
				this.<>1__state = 5;
				return true;
			case 5:
				this.<>1__state = -1;
				RewardManager.instance.NextReward();
				if (PanelManager.CurPanel == PanelType.EnemyAugments)
				{
					PanelManager.instance.GoToPanel(PanelType.GameInvisible);
				}
				return false;
			default:
				return false;
			}
			if (t < 1f)
			{
				t += Time.unscaledDeltaTime * 2f;
				winnerBox.Holder.position = Vector3.Lerp(startPt, wPos, enemySelectionPanel.moveCurve.Evaluate(t));
				this.<>2__current = true;
				this.<>1__state = 3;
				return true;
			}
			winnerBox.Choice.anim.Play("EnemyBox_SelectedFade");
			wPos = default(Vector3);
			startPt = default(Vector3);
			IL_2DA:
			this.<>2__current = new WaitForSecondsRealtime(0.5f);
			this.<>1__state = 4;
			return true;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x000CF504 File Offset: 0x000CD704
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000CF50C File Offset: 0x000CD70C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x000CF513 File Offset: 0x000CD713
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026FE RID: 9982
		private int <>1__state;

		// Token: 0x040026FF RID: 9983
		private object <>2__current;

		// Token: 0x04002700 RID: 9984
		public EnemySelectionPanel <>4__this;

		// Token: 0x04002701 RID: 9985
		public int winnerID;

		// Token: 0x04002702 RID: 9986
		private EnemySelectionPanel.EnemyBox <winnerBox>5__2;

		// Token: 0x04002703 RID: 9987
		private Vector3 <wPos>5__3;

		// Token: 0x04002704 RID: 9988
		private float <t>5__4;

		// Token: 0x04002705 RID: 9989
		private Vector3 <startPt>5__5;
	}
}
