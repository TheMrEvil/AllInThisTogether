using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014E RID: 334
public class EnemyChapterProgressItem : MonoBehaviour
{
	// Token: 0x06000EE8 RID: 3816 RVA: 0x0005EE48 File Offset: 0x0005D048
	public void SetupAsVignette(int index, int curIndex)
	{
		this.IsCompleted = (index == curIndex);
		this.BackroundRing.enabled = false;
		this.FillObj.SetActive(false);
		this.Vignette_Back.SetActive(true);
		if (GameplayManager.CurState == GameState.Reward_Enemy || GameplayManager.CurState == GameState.Reward_PreEnemy)
		{
			curIndex++;
		}
		this.Vignette_Filled.SetActive(curIndex > index + 1);
		this.CurDisplay.SetActive(index == curIndex - 1 && VignetteControl.instance != null);
		base.GetComponent<RectTransform>().sizeDelta = new Vector2(20f, 25f);
		UIPingable component = base.GetComponent<UIPingable>();
		if (component == null)
		{
			return;
		}
		component.SetupAsVignette(index);
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0005EEF8 File Offset: 0x0005D0F8
	public void SetupDisplay(GenreWaveNode waveRef, int index, int curIndex)
	{
		this.IsCompleted = (index == curIndex);
		this.IsBoss = (waveRef.chapterType == GenreWaveNode.ChapterType.Boss);
		bool flag = RewardManager.InRewards || GameplayManager.CurState == GameState.Vignette_PreWait || VignetteControl.instance != null;
		int num = Mathf.Max(-1, flag ? curIndex : (curIndex - 1));
		this.FillObj.SetActive(num > index);
		if (GameplayManager.CurState == GameState.Reward_Enemy || GameplayManager.CurState == GameState.Reward_PreEnemy)
		{
			curIndex++;
		}
		int num2 = Mathf.Max(0, flag ? (curIndex - 1) : (curIndex - 1));
		this.CurDisplay.SetActive(index == num2 && VignetteControl.instance == null);
		this.Skull.gameObject.SetActive(this.IsBoss);
		if (this.Skull.gameObject.activeSelf)
		{
			this.BackroundRing.enabled = false;
			AIManager instance = AIManager.instance;
			EnemyType? enemyType;
			if (instance == null)
			{
				enemyType = null;
			}
			else
			{
				AILayout layout = instance.Layout;
				enemyType = ((layout != null) ? new EnemyType?(layout.GetBossType(waveRef.CalculatedBossIndex - 1, waveRef.BossType)) : null);
			}
			EnemyType enemyType2 = enemyType ?? EnemyType.__;
			AIManager instance2 = AIManager.instance;
			AIData.TornFamilyInfo tornFamilyInfo = (instance2 != null) ? instance2.DB.GetFamilyData(enemyType2) : null;
			if (tornFamilyInfo != null)
			{
				this.Skull.sprite = tornFamilyInfo.BossSprite;
			}
			UIPingable component = base.GetComponent<UIPingable>();
			if (component == null)
			{
				return;
			}
			component.SetupAsBoss(enemyType2, index + 1);
			return;
		}
		else
		{
			UIPingable component2 = base.GetComponent<UIPingable>();
			if (component2 == null)
			{
				return;
			}
			component2.SetupAsChapter(index + 1);
			return;
		}
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x0005F08C File Offset: 0x0005D28C
	public void SetupRaid(int index, bool completed, bool isCurrent, bool isBoss, bool isVignette)
	{
		this.IsCompleted = completed;
		this.CurDisplay.SetActive(isCurrent);
		if (isVignette)
		{
			this.BackroundRing.enabled = false;
			this.FillObj.SetActive(false);
			this.Vignette_Back.SetActive(true);
			this.Vignette_Filled.SetActive(completed);
			UIPingable component = base.GetComponent<UIPingable>();
			if (component == null)
			{
				return;
			}
			component.SetupAsVignette(index);
			return;
		}
		else
		{
			this.FillObj.SetActive(this.IsCompleted);
			this.Skull.gameObject.SetActive(isBoss);
			UIPingable component2 = base.GetComponent<UIPingable>();
			if (component2 == null)
			{
				return;
			}
			component2.SetupAsRaidEncounter(index);
			return;
		}
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x0005F126 File Offset: 0x0005D326
	public void ToggleBossFX(bool isOn)
	{
		if (!this.IsBoss)
		{
			return;
		}
		if (!isOn)
		{
			this.BossFX.Stop();
			return;
		}
		if (isOn)
		{
			this.BossFX.Play();
		}
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x0005F14E File Offset: 0x0005D34E
	public EnemyChapterProgressItem()
	{
	}

	// Token: 0x04000C9B RID: 3227
	public Image BackroundRing;

	// Token: 0x04000C9C RID: 3228
	public GameObject FillObj;

	// Token: 0x04000C9D RID: 3229
	public Image Skull;

	// Token: 0x04000C9E RID: 3230
	public GameObject CurDisplay;

	// Token: 0x04000C9F RID: 3231
	public ParticleSystem BossFX;

	// Token: 0x04000CA0 RID: 3232
	[Header("Vignette")]
	public GameObject Vignette_Back;

	// Token: 0x04000CA1 RID: 3233
	public GameObject Vignette_Filled;

	// Token: 0x04000CA2 RID: 3234
	[NonSerialized]
	public bool IsBoss;

	// Token: 0x04000CA3 RID: 3235
	[NonSerialized]
	public bool IsCompleted;
}
