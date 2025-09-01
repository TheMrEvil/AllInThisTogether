using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000157 RID: 343
public class TomeProgressSmall : MonoBehaviour
{
	// Token: 0x06000F27 RID: 3879 RVA: 0x00060148 File Offset: 0x0005E348
	public void Refresh()
	{
		foreach (EnemyChapterProgressItem enemyChapterProgressItem in this.chapterPips)
		{
			UnityEngine.Object.Destroy(enemyChapterProgressItem.gameObject);
		}
		this.chapterPips.Clear();
		if (RaidManager.IsInRaid)
		{
			this.SetupRaidDisplay();
			return;
		}
		this.SetupTomeDisplay();
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x000601BC File Offset: 0x0005E3BC
	private void SetupTomeDisplay()
	{
		int num = WaveManager.CurrentWave + 1;
		GenreTree gameGraph = GameplayManager.instance.GameGraph;
		List<Node> list = (gameGraph != null) ? gameGraph.Root.Waves : null;
		if (list == null)
		{
			return;
		}
		if (WaveManager.instance.AppendixLevel > 0)
		{
			list = GameplayManager.instance.GameGraph.Root.Appendix;
			num = WaveManager.instance.AppendixChapterNumber;
		}
		for (int i = 0; i < list.Count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChapterPipRef, this.ChapterPipRef.transform.parent);
			gameObject.SetActive(true);
			EnemyChapterProgressItem component = gameObject.GetComponent<EnemyChapterProgressItem>();
			GenreWaveNode genreWaveNode = list[i] as GenreWaveNode;
			component.SetupDisplay(genreWaveNode, i, num);
			if (i == num)
			{
				this.curWavePt = gameObject.transform;
			}
			this.chapterPips.Add(component);
			if (genreWaveNode.NextVignette != null)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChapterPipRef, this.ChapterPipRef.transform.parent);
				gameObject.SetActive(true);
				component = gameObject.GetComponent<EnemyChapterProgressItem>();
				component.SetupAsVignette(i, num);
				this.chapterPips.Add(component);
			}
		}
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x000602E8 File Offset: 0x0005E4E8
	private void SetupRaidDisplay()
	{
		int currentEncounterIndex = RaidManager.instance.CurrentEncounterIndex;
		List<RaidDB.Encounter> encounters = RaidDB.GetRaid(RaidManager.instance.CurrentRaid).Encounters;
		for (int i = 0; i < encounters.Count; i++)
		{
			RaidDB.Encounter encounter = encounters[i];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChapterPipRef, this.ChapterPipRef.transform.parent);
			gameObject.SetActive(true);
			EnemyChapterProgressItem component = gameObject.GetComponent<EnemyChapterProgressItem>();
			component.SetupRaid(currentEncounterIndex, i < currentEncounterIndex, i == currentEncounterIndex, false, encounter.Type == RaidDB.EncounterType.Vignette);
			if (i == currentEncounterIndex)
			{
				this.curWavePt = gameObject.transform;
			}
			this.chapterPips.Add(component);
		}
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x00060394 File Offset: 0x0005E594
	public EnemyChapterProgressItem GetNextBossPip()
	{
		foreach (EnemyChapterProgressItem enemyChapterProgressItem in this.chapterPips)
		{
			if (!enemyChapterProgressItem.IsCompleted && enemyChapterProgressItem.IsBoss)
			{
				return enemyChapterProgressItem;
			}
		}
		return null;
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x000603F8 File Offset: 0x0005E5F8
	public TomeProgressSmall()
	{
	}

	// Token: 0x04000CE2 RID: 3298
	public GameObject ChapterPipRef;

	// Token: 0x04000CE3 RID: 3299
	[NonSerialized]
	public List<EnemyChapterProgressItem> chapterPips = new List<EnemyChapterProgressItem>();

	// Token: 0x04000CE4 RID: 3300
	[NonSerialized]
	public Transform curWavePt;
}
