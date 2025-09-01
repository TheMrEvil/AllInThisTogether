using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B3 RID: 435
public class GenreProgressBar : MonoBehaviour
{
	// Token: 0x1700014C RID: 332
	// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0006F534 File Offset: 0x0006D734
	public static bool ShouldShow
	{
		get
		{
			return !BossHealthbar.ShouldShow() && (!TutorialManager.InTutorial || TutorialManager.CurrentStep == TutorialStep.EnemyChoice) && GameplayManager.IsInGame && GameplayManager.instance.CurrentState != GameState.InWave;
		}
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x0006F56A File Offset: 0x0006D76A
	private void Awake()
	{
		GenreProgressBar.instance = this;
		this.startGroup = this.StartWavePt.GetComponent<CanvasGroup>();
		this.bossGroup = this.BossWavePt.GetComponent<CanvasGroup>();
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x0006F594 File Offset: 0x0006D794
	public void OnGameStateChanged(GameState from, GameState to)
	{
		if (to != GameState.Reward_Start)
		{
			if (to != GameState.Reward_PreEnemy)
			{
				return;
			}
		}
		else
		{
			this.CreateFillElement();
			foreach (GenereProgressPoint genereProgressPoint in this.genrePoints)
			{
				genereProgressPoint.MoveIn();
			}
			base.StartCoroutine(this.CapOpacity(true, 0));
		}
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x0006F608 File Offset: 0x0006D808
	public void SetupGenre(GenreTree tree)
	{
		if (tree == null)
		{
			return;
		}
		foreach (GenereProgressPoint genereProgressPoint in this.genrePoints)
		{
			UnityEngine.Object.Destroy(genereProgressPoint.gameObject);
		}
		this.genrePoints.Clear();
		for (int i = 0; i < this.fills.Count; i++)
		{
			UnityEngine.Object.Destroy(this.fills[i]);
		}
		this.fills.Clear();
		this.FillMarker.sizeDelta = new Vector2(0f, this.FillMarker.sizeDelta.y);
		this.curEnd = null;
		List<Node> waves = tree.Root.Waves;
		int index = waves.Count - 1;
		GenreWaveNode genreWaveNode = waves[index] as GenreWaveNode;
		if (genreWaveNode == null || genreWaveNode.chapterType != GenreWaveNode.ChapterType.Boss)
		{
			this.bossType = EnemyType.__;
		}
		else
		{
			AIManager aimanager = AIManager.instance;
			EnemyType? enemyType;
			if (aimanager == null)
			{
				enemyType = null;
			}
			else
			{
				AILayout layout = aimanager.Layout;
				enemyType = ((layout != null) ? new EnemyType?(layout.GetBossType(genreWaveNode.CalculatedBossIndex - 1, genreWaveNode.BossType)) : null);
			}
			this.bossType = (enemyType ?? EnemyType.__);
		}
		this.BossWaveImg.sprite = GenreProgressBar.GetBossSprite(this.bossType);
		List<ValueTuple<GenreWaveNode, bool, int>> list = new List<ValueTuple<GenreWaveNode, bool, int>>();
		this.TotalWaves = tree.Root.Waves.Count;
		for (int j = 1; j < tree.Root.Waves.Count - 1; j++)
		{
			GenreWaveNode item = tree.Root.Waves[j] as GenreWaveNode;
			bool item2 = false;
			if (j - 1 > 0)
			{
				GenreWaveNode genreWaveNode2 = tree.Root.Waves[j - 1] as GenreWaveNode;
				item2 = (genreWaveNode2 != null && genreWaveNode2.Reward != null && ((GenreRewardNode)genreWaveNode2.Reward).HasReward(GameState.Reward_Fountain));
			}
			list.Add(new ValueTuple<GenreWaveNode, bool, int>(item, item2, j));
		}
		foreach (ValueTuple<GenreWaveNode, bool, int> valueTuple in list)
		{
			GenreWaveNode item3 = valueTuple.Item1;
			bool item4 = valueTuple.Item2;
			int item5 = valueTuple.Item3;
			this.CreateGenrePoint(item3, item4, item5);
		}
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x0006F8A0 File Offset: 0x0006DAA0
	private void Update()
	{
		if (GameplayManager.instance == null || PlayerControl.myInstance == null)
		{
			return;
		}
		this.GenreProgress.UpdateOpacity(GenreProgressBar.ShouldShow, 1f, false);
		if (this.curEnd != null)
		{
			this.FillMarker.sizeDelta = new Vector2(this.curEnd.localPosition.x + 35f, this.FillMarker.sizeDelta.y);
			return;
		}
		this.FillMarker.sizeDelta = new Vector2(0f, this.FillMarker.sizeDelta.y);
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x0006F948 File Offset: 0x0006DB48
	private void CreateGenrePoint(GenreWaveNode wave, bool showFountain, int index)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GenreElementRef, this.GenreElementRef.transform.parent);
		gameObject.SetActive(true);
		gameObject.name = "Wave_" + index.ToString() + "_Indicator";
		GenereProgressPoint component = gameObject.GetComponent<GenereProgressPoint>();
		float wantPoint = (float)index / ((float)this.TotalWaves - 1f);
		component.Setup(wave, showFountain, wantPoint, index);
		this.genrePoints.Add(component);
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x0006F9C0 File Offset: 0x0006DBC0
	private void CreateFillElement()
	{
		if (WaveManager.instance.WavesCompleted == 0 || this.genrePoints == null || this.genrePoints.Count == 0)
		{
			return;
		}
		GenereProgressPoint curPoint = this.GetCurPoint();
		RectTransform rectTransform = (curPoint != null) ? curPoint.rect : null;
		if (rectTransform == null)
		{
			RectTransform rect = this.genrePoints[this.genrePoints.Count - 1].rect;
			rectTransform = this.BossWavePt;
		}
		else if (rectTransform == this.genrePoints[0].rect)
		{
			RectTransform startWavePt = this.StartWavePt;
		}
		else
		{
			for (int i = 1; i < this.genrePoints.Count; i++)
			{
				if (this.genrePoints[i].rect == rectTransform)
				{
					RectTransform rect2 = this.genrePoints[i - 1].rect;
				}
			}
		}
		this.curEnd = rectTransform;
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x0006FA9E File Offset: 0x0006DC9E
	private IEnumerator GenreBarFocus()
	{
		yield return new WaitForSeconds(1f);
		this.BarAnim.Play("UIProgress_Grow");
		yield return new WaitForSeconds(0.66f);
		if (!TutorialManager.InTutorial)
		{
			InfoDisplay.SetText("<i>" + GameplayManager.instance.GameGraph.Root.Name + "</i>", 0f, InfoArea.Title);
			yield return new WaitForSeconds(0.66f);
		}
		GenereProgressPoint curPoint = this.GetCurPoint();
		if (curPoint != null)
		{
			curPoint.PulseFX.Play();
		}
		else if (WaveManager.instance.WavesCompleted == 0)
		{
			this.StartPulse.Play();
		}
		else
		{
			this.EndPulse.Play();
		}
		yield break;
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x0006FAAD File Offset: 0x0006DCAD
	private IEnumerator GenreBarBlur()
	{
		InfoDisplay.ReleaseText(InfoArea.Title);
		this.BarAnim.Play("UIProgress_Back");
		yield return new WaitForSeconds(1f);
		int wavesCompleted = WaveManager.instance.WavesCompleted;
		foreach (GenereProgressPoint genereProgressPoint in this.genrePoints)
		{
			genereProgressPoint.MoveOut(wavesCompleted);
		}
		base.StartCoroutine(this.CapOpacity(false, wavesCompleted));
		GenereProgressPoint curPoint = this.GetCurPoint();
		if (curPoint != null)
		{
			curPoint.PulseFX.Stop();
		}
		else if (WaveManager.instance.WavesCompleted == 0)
		{
			this.StartPulse.Stop();
		}
		else
		{
			this.EndPulse.Stop();
		}
		yield break;
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x0006FABC File Offset: 0x0006DCBC
	private IEnumerator CapOpacity(bool showingGenre, int wave)
	{
		bool startVisible = showingGenre || wave == 0;
		bool endVisible = showingGenre ?? (wave == this.genrePoints.Count - 1);
		yield return new WaitForSeconds(0.5f);
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime;
			this.startGroup.UpdateOpacity(startVisible, 2f, true);
			this.bossGroup.UpdateOpacity(endVisible, 2f, true);
			yield return true;
		}
		this.startGroup.alpha = (float)(startVisible ? 1 : 0);
		this.bossGroup.alpha = (float)(endVisible ? 1 : 0);
		yield break;
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x0006FADC File Offset: 0x0006DCDC
	public GenereProgressPoint GetCurPoint()
	{
		if (!GameplayManager.IsInGame || GameplayManager.instance.GameGraph == null || this.genrePoints.Count == 0)
		{
			return null;
		}
		int wavesCompleted = WaveManager.instance.WavesCompleted;
		if (this.genrePoints.Count >= wavesCompleted && wavesCompleted > 0)
		{
			return this.genrePoints[wavesCompleted - 1];
		}
		return null;
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x0006FB40 File Offset: 0x0006DD40
	public static Sprite GetBossSprite(EnemyType eType)
	{
		foreach (GenreProgressBar.BossIcon bossIcon in GenreProgressBar.instance.BossIcons)
		{
			if (eType.HasFlag(bossIcon.EType))
			{
				return bossIcon.icon;
			}
		}
		return GenreProgressBar.instance.DefaultBossIcon;
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x0006FBC0 File Offset: 0x0006DDC0
	public GenreProgressBar()
	{
	}

	// Token: 0x040010B2 RID: 4274
	public static GenreProgressBar instance;

	// Token: 0x040010B3 RID: 4275
	public CanvasGroup GenreProgress;

	// Token: 0x040010B4 RID: 4276
	public Animator BarAnim;

	// Token: 0x040010B5 RID: 4277
	public RectTransform StartWavePt;

	// Token: 0x040010B6 RID: 4278
	private CanvasGroup startGroup;

	// Token: 0x040010B7 RID: 4279
	public RectTransform BossWavePt;

	// Token: 0x040010B8 RID: 4280
	public Image BossWaveImg;

	// Token: 0x040010B9 RID: 4281
	public Sprite DefaultBossIcon;

	// Token: 0x040010BA RID: 4282
	public List<GenreProgressBar.BossIcon> BossIcons = new List<GenreProgressBar.BossIcon>();

	// Token: 0x040010BB RID: 4283
	private CanvasGroup bossGroup;

	// Token: 0x040010BC RID: 4284
	public CanvasGroup backgroundGroup;

	// Token: 0x040010BD RID: 4285
	public AnimationCurve FillCurve;

	// Token: 0x040010BE RID: 4286
	public GameObject GenreElementRef;

	// Token: 0x040010BF RID: 4287
	public GameObject GenrePathRef;

	// Token: 0x040010C0 RID: 4288
	public RectTransform FillMarker;

	// Token: 0x040010C1 RID: 4289
	private List<GenereProgressPoint> genrePoints = new List<GenereProgressPoint>();

	// Token: 0x040010C2 RID: 4290
	private List<GameObject> fills = new List<GameObject>();

	// Token: 0x040010C3 RID: 4291
	public ParticleSystem StartPulse;

	// Token: 0x040010C4 RID: 4292
	public ParticleSystem EndPulse;

	// Token: 0x040010C5 RID: 4293
	private int TotalWaves = 1;

	// Token: 0x040010C6 RID: 4294
	private bool shouldUpdateBar;

	// Token: 0x040010C7 RID: 4295
	private float barUpdateT;

	// Token: 0x040010C8 RID: 4296
	private float startFill;

	// Token: 0x040010C9 RID: 4297
	private EnemyType bossType;

	// Token: 0x040010CA RID: 4298
	private RectTransform curEnd;

	// Token: 0x02000576 RID: 1398
	[Serializable]
	public struct BossIcon
	{
		// Token: 0x0400273D RID: 10045
		public EnemyType EType;

		// Token: 0x0400273E RID: 10046
		public Sprite icon;
	}

	// Token: 0x02000577 RID: 1399
	[CompilerGenerated]
	private sealed class <GenreBarFocus>d__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002501 RID: 9473 RVA: 0x000CFEC9 File Offset: 0x000CE0C9
		[DebuggerHidden]
		public <GenreBarFocus>d__33(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000CFED8 File Offset: 0x000CE0D8
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000CFEDC File Offset: 0x000CE0DC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GenreProgressBar genreProgressBar = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(1f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				genreProgressBar.BarAnim.Play("UIProgress_Grow");
				this.<>2__current = new WaitForSeconds(0.66f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				if (!TutorialManager.InTutorial)
				{
					InfoDisplay.SetText("<i>" + GameplayManager.instance.GameGraph.Root.Name + "</i>", 0f, InfoArea.Title);
					this.<>2__current = new WaitForSeconds(0.66f);
					this.<>1__state = 3;
					return true;
				}
				break;
			case 3:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			GenereProgressPoint curPoint = genreProgressBar.GetCurPoint();
			if (curPoint != null)
			{
				curPoint.PulseFX.Play();
			}
			else if (WaveManager.instance.WavesCompleted == 0)
			{
				genreProgressBar.StartPulse.Play();
			}
			else
			{
				genreProgressBar.EndPulse.Play();
			}
			return false;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x000CFFFD File Offset: 0x000CE1FD
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000D0005 File Offset: 0x000CE205
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x000D000C File Offset: 0x000CE20C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400273F RID: 10047
		private int <>1__state;

		// Token: 0x04002740 RID: 10048
		private object <>2__current;

		// Token: 0x04002741 RID: 10049
		public GenreProgressBar <>4__this;
	}

	// Token: 0x02000578 RID: 1400
	[CompilerGenerated]
	private sealed class <GenreBarBlur>d__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002507 RID: 9479 RVA: 0x000D0014 File Offset: 0x000CE214
		[DebuggerHidden]
		public <GenreBarBlur>d__34(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000D0023 File Offset: 0x000CE223
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000D0028 File Offset: 0x000CE228
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GenreProgressBar genreProgressBar = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				InfoDisplay.ReleaseText(InfoArea.Title);
				genreProgressBar.BarAnim.Play("UIProgress_Back");
				this.<>2__current = new WaitForSeconds(1f);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			int wavesCompleted = WaveManager.instance.WavesCompleted;
			foreach (GenereProgressPoint genereProgressPoint in genreProgressBar.genrePoints)
			{
				genereProgressPoint.MoveOut(wavesCompleted);
			}
			genreProgressBar.StartCoroutine(genreProgressBar.CapOpacity(false, wavesCompleted));
			GenereProgressPoint curPoint = genreProgressBar.GetCurPoint();
			if (curPoint != null)
			{
				curPoint.PulseFX.Stop();
			}
			else if (WaveManager.instance.WavesCompleted == 0)
			{
				genreProgressBar.StartPulse.Stop();
			}
			else
			{
				genreProgressBar.EndPulse.Stop();
			}
			return false;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x000D012C File Offset: 0x000CE32C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000D0134 File Offset: 0x000CE334
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x000D013B File Offset: 0x000CE33B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002742 RID: 10050
		private int <>1__state;

		// Token: 0x04002743 RID: 10051
		private object <>2__current;

		// Token: 0x04002744 RID: 10052
		public GenreProgressBar <>4__this;
	}

	// Token: 0x02000579 RID: 1401
	[CompilerGenerated]
	private sealed class <CapOpacity>d__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600250D RID: 9485 RVA: 0x000D0143 File Offset: 0x000CE343
		[DebuggerHidden]
		public <CapOpacity>d__35(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000D0152 File Offset: 0x000CE352
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000D0154 File Offset: 0x000CE354
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GenreProgressBar genreProgressBar = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				startVisible = (showingGenre || wave == 0);
				endVisible = showingGenre;
				if (!showingGenre)
				{
					endVisible = (wave == genreProgressBar.genrePoints.Count - 1);
				}
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (t >= 1f)
			{
				genreProgressBar.startGroup.alpha = (float)(startVisible ? 1 : 0);
				genreProgressBar.bossGroup.alpha = (float)(endVisible ? 1 : 0);
				return false;
			}
			t += Time.deltaTime;
			genreProgressBar.startGroup.UpdateOpacity(startVisible, 2f, true);
			genreProgressBar.bossGroup.UpdateOpacity(endVisible, 2f, true);
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x000D029A File Offset: 0x000CE49A
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000D02A2 File Offset: 0x000CE4A2
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x000D02A9 File Offset: 0x000CE4A9
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002745 RID: 10053
		private int <>1__state;

		// Token: 0x04002746 RID: 10054
		private object <>2__current;

		// Token: 0x04002747 RID: 10055
		public bool showingGenre;

		// Token: 0x04002748 RID: 10056
		public int wave;

		// Token: 0x04002749 RID: 10057
		public GenreProgressBar <>4__this;

		// Token: 0x0400274A RID: 10058
		private bool <startVisible>5__2;

		// Token: 0x0400274B RID: 10059
		private bool <endVisible>5__3;

		// Token: 0x0400274C RID: 10060
		private float <t>5__4;
	}
}
