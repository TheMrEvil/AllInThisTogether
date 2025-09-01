using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CD RID: 461
public class WaveProgressBar : MonoBehaviour
{
	// Token: 0x1700015B RID: 347
	// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00073FE6 File Offset: 0x000721E6
	private bool ShouldShow
	{
		get
		{
			return !BossHealthbar.ShouldShow() && GameplayManager.instance.CurrentState == GameState.InWave;
		}
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x00073FFE File Offset: 0x000721FE
	private void Awake()
	{
		WaveProgressBar.instance = this;
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x00074008 File Offset: 0x00072208
	private void Update()
	{
		if (GameplayManager.instance == null || PlayerControl.myInstance == null)
		{
			return;
		}
		this.GameProgress.UpdateOpacity(this.ShouldShow, 2f, false);
		if (GameplayManager.CurState == GameState.InWave)
		{
			this.GoalFill.fillAmount = Mathf.Lerp(this.GoalFill.fillAmount, WaveManager.GoalProportion, Time.deltaTime * 3f);
			this.GoalCompletionFill.fillAmount = Mathf.Lerp(this.GoalCompletionFill.fillAmount, WaveManager.GoalCompletionProportion, Time.deltaTime * 3f);
		}
		else
		{
			this.GoalFill.fillAmount = 0f;
			this.GoalCompletionFill.fillAmount = 0f;
		}
		if (this.ShouldShow && GameplayManager.instance.GameGraph != null)
		{
			string str = GameplayManager.instance.GameGraph.Root.ShortName + " - ";
			if (GameplayManager.IsChallengeActive)
			{
				str = "<sprite name=\"bookclub\"> " + GameplayManager.Challenge.Name + " - ";
			}
			string str2 = (WaveManager.instance.AppendixLevel <= 0) ? string.Format("Chapter {0}", WaveManager.CurrentWave + 1) : string.Format("Appendix {0}.{1}", WaveManager.instance.AppendixLevel, WaveManager.instance.AppendixChapterNumber);
			string text = str + str2;
			if (this.ChapterTitle.text != text)
			{
				this.ChapterTitle.text = text;
			}
		}
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x0007419C File Offset: 0x0007239C
	private void ClearWaveBar()
	{
		foreach (WaveProgressPoint waveProgressPoint in this.wavePoints)
		{
			UnityEngine.Object.Destroy(waveProgressPoint.gameObject);
		}
		this.wavePoints.Clear();
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x000741FC File Offset: 0x000723FC
	private void SetupWaveBar()
	{
		this.ClearWaveBar();
		GenreWaveNode waveConfig = WaveManager.WaveConfig;
		if (waveConfig == null)
		{
			return;
		}
		GenreSpawnNode genreSpawnNode = waveConfig.Spawns as GenreSpawnNode;
		if (genreSpawnNode == null)
		{
			return;
		}
		float num = (float)waveConfig.ProgressRequired();
		float num2 = (float)genreSpawnNode.GroupPoints;
		if (waveConfig.Event == GenreWaveNode.EventType.BonusObjective)
		{
			this.CreateWavePoint(num2 / num, WaveProgressPoint.WavePointType.BonusObjective, EnemyType.Any);
			return;
		}
		if (waveConfig.Event == GenreWaveNode.EventType.Elite)
		{
			foreach (GenreSpawnNode.EliteSpawn eliteSpawn in waveConfig.Elites)
			{
				int number = eliteSpawn.Index + WaveManager.instance.AppendixLevel;
				this.CreateWavePoint(eliteSpawn.At, WaveProgressPoint.WavePointType.Elite, AIManager.instance.Layout.GetElite(number).GetComponent<AIControl>().EnemyType);
			}
		}
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x000742E4 File Offset: 0x000724E4
	private void CreateWavePoint(float atProgress, WaveProgressPoint.WavePointType wType, EnemyType eliteType)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.WaveElementRef, this.WaveElementRef.transform.parent);
		gameObject.SetActive(true);
		WaveProgressPoint component = gameObject.GetComponent<WaveProgressPoint>();
		component.Setup(Mathf.Clamp01(atProgress), wType, eliteType);
		this.wavePoints.Add(component);
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x00074333 File Offset: 0x00072533
	public void PulseElite()
	{
		this.PulseWavePoint(WaveProgressPoint.WavePointType.Elite);
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x0007433C File Offset: 0x0007253C
	public void PulseBonusObjective()
	{
		this.PulseWavePoint(WaveProgressPoint.WavePointType.BonusObjective);
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x00074348 File Offset: 0x00072548
	private void PulseWavePoint(WaveProgressPoint.WavePointType wType)
	{
		List<WaveProgressPoint> list = new List<WaveProgressPoint>();
		foreach (WaveProgressPoint waveProgressPoint in this.wavePoints)
		{
			if (wType == waveProgressPoint.WaveType)
			{
				list.Add(waveProgressPoint);
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		float waveProgress = WaveManager.GoalProportion;
		(from n in list
		orderby Mathf.Abs(n.AtPoint - waveProgress)
		select n).First<WaveProgressPoint>().Pulse();
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x000743E0 File Offset: 0x000725E0
	public void OnGameStateChanged(GameState from, GameState to)
	{
		if (to == GameState.InWave)
		{
			this.ClearWaveBar();
			base.Invoke("SetupWaveBar", 1f);
		}
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x000743FC File Offset: 0x000725FC
	public WaveProgressBar()
	{
	}

	// Token: 0x040011F9 RID: 4601
	public static WaveProgressBar instance;

	// Token: 0x040011FA RID: 4602
	public CanvasGroup GameProgress;

	// Token: 0x040011FB RID: 4603
	public Image GoalFill;

	// Token: 0x040011FC RID: 4604
	public Image GoalCompletionFill;

	// Token: 0x040011FD RID: 4605
	public GameObject WaveElementRef;

	// Token: 0x040011FE RID: 4606
	public TextMeshProUGUI ChapterTitle;

	// Token: 0x040011FF RID: 4607
	private List<WaveProgressPoint> wavePoints = new List<WaveProgressPoint>();

	// Token: 0x0200058B RID: 1419
	[CompilerGenerated]
	private sealed class <>c__DisplayClass16_0
	{
		// Token: 0x06002556 RID: 9558 RVA: 0x000D12ED File Offset: 0x000CF4ED
		public <>c__DisplayClass16_0()
		{
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000D12F5 File Offset: 0x000CF4F5
		internal float <PulseWavePoint>b__0(WaveProgressPoint n)
		{
			return Mathf.Abs(n.AtPoint - this.waveProgress);
		}

		// Token: 0x0400279E RID: 10142
		public float waveProgress;
	}
}
