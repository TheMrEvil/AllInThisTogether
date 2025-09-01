using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F5 RID: 501
public class AugmentBook_Map_Item : MonoBehaviour
{
	// Token: 0x0600154E RID: 5454 RVA: 0x00085C6C File Offset: 0x00083E6C
	public void Setup(GenreWaveNode waveData, AugmentTree enemyUpgrade, int waveIndex)
	{
		this.SetupUpgrade(enemyUpgrade, waveIndex);
		bool flag = WaveManager.instance.WavesCompleted > waveIndex;
		this.ColorDisplay.alpha = (float)(flag ? 1 : 0);
		foreach (AugmentBook_Map_Item.PathElement pathElement in this.Path)
		{
			pathElement.Filled.enabled = flag;
		}
		bool flag2 = waveData.chapterType == GenreWaveNode.ChapterType.Boss;
		this.EnemyBorder_Normal.SetActive(!flag2);
		this.EnemyBorder_Boss.SetActive(flag2);
		this.BossIndicator.gameObject.SetActive(flag2);
		this.QuestionMark.SetActive(!flag2);
		this.BossIconPreview.SetActive(flag2);
		if (flag2)
		{
			AIManager instance = AIManager.instance;
			EnemyType? enemyType;
			if (instance == null)
			{
				enemyType = null;
			}
			else
			{
				AILayout layout = instance.Layout;
				enemyType = ((layout != null) ? new EnemyType?(layout.GetBossType(waveData.CalculatedBossIndex - 1, waveData.BossType)) : null);
			}
			EnemyType eType = enemyType ?? EnemyType.__;
			AIManager instance2 = AIManager.instance;
			AIData.TornFamilyInfo tornFamilyInfo = (instance2 != null) ? instance2.DB.GetFamilyData(eType) : null;
			this.BossIndicator.sprite = ((tornFamilyInfo != null) ? tornFamilyInfo.BossSprite : null);
			this.BossText.text = (Settings.LowRez ? "" : eType.ToString());
		}
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x00085DF4 File Offset: 0x00083FF4
	public void SetupAsRaid(RaidDB.Encounter encounter, int index, bool hasCompleted, AugmentTree enemyUpgrade)
	{
		this.SetupUpgrade(enemyUpgrade, index);
		this.ColorDisplay.alpha = (float)(hasCompleted ? 1 : 0);
		foreach (AugmentBook_Map_Item.PathElement pathElement in this.Path)
		{
			pathElement.Filled.enabled = hasCompleted;
		}
		this.EnemyBorder_Normal.SetActive(false);
		this.EnemyBorder_Boss.SetActive(true);
		this.BossIndicator.gameObject.SetActive(false);
		this.QuestionMark.SetActive(false);
		this.BossIconPreview.SetActive(true);
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x00085EA8 File Offset: 0x000840A8
	private void SetupUpgrade(AugmentTree enemyUpgrade, int waveIndex)
	{
		bool flag = enemyUpgrade != null;
		this.EnemyIcon.enabled = flag;
		this.EnemyButton.interactable = flag;
		this.enemyAugment = enemyUpgrade;
		if (flag)
		{
			this.EnemyIcon.sprite = this.enemyAugment.Root.Icon;
			this.PingRef.PingType = UIPing.UIPingType.Augment_Enemy;
			this.PingRef.Setup(enemyUpgrade);
			return;
		}
		if (RaidManager.IsInRaid)
		{
			this.PingRef.SetupAsRaidEncounter(waveIndex);
			return;
		}
		this.PingRef.SetupAsChapter(waveIndex + 2);
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x00085F36 File Offset: 0x00084136
	public void OnSelected()
	{
		if (!this.EnemyButton.interactable || this.enemyAugment == null)
		{
			return;
		}
		Tooltip.Show(this.TooltipDisplay.position, TextAnchor.MiddleLeft, this.enemyAugment, 1, null);
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x00085F6D File Offset: 0x0008416D
	public void OnDeselected()
	{
		Tooltip.Release();
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x00085F74 File Offset: 0x00084174
	public AugmentBook_Map_Item()
	{
	}

	// Token: 0x040014D2 RID: 5330
	public Image EnemyIcon;

	// Token: 0x040014D3 RID: 5331
	public CanvasGroup ColorDisplay;

	// Token: 0x040014D4 RID: 5332
	public CanvasGroup CurrentGlow;

	// Token: 0x040014D5 RID: 5333
	public List<AugmentBook_Map_Item.PathElement> Path;

	// Token: 0x040014D6 RID: 5334
	public GameObject QuestionMark;

	// Token: 0x040014D7 RID: 5335
	public GameObject BossIconPreview;

	// Token: 0x040014D8 RID: 5336
	public Button EnemyButton;

	// Token: 0x040014D9 RID: 5337
	public GameObject EnemyBorder_Normal;

	// Token: 0x040014DA RID: 5338
	public GameObject EnemyBorder_Boss;

	// Token: 0x040014DB RID: 5339
	public Image BossIndicator;

	// Token: 0x040014DC RID: 5340
	public RectTransform TooltipDisplay;

	// Token: 0x040014DD RID: 5341
	public TextMeshProUGUI BossText;

	// Token: 0x040014DE RID: 5342
	private AugmentTree enemyAugment;

	// Token: 0x040014DF RID: 5343
	public UIPingable PingRef;

	// Token: 0x020005C6 RID: 1478
	[Serializable]
	public class PathElement
	{
		// Token: 0x0600261C RID: 9756 RVA: 0x000D3163 File Offset: 0x000D1363
		public PathElement()
		{
		}

		// Token: 0x040028A2 RID: 10402
		public RectTransform Step;

		// Token: 0x040028A3 RID: 10403
		public Image Filled;
	}
}
