using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EC RID: 492
public class QuestboardPanel : MonoBehaviour
{
	// Token: 0x17000168 RID: 360
	// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00083A95 File Offset: 0x00081C95
	public static bool AreIncentivesUnlocked
	{
		get
		{
			return GameStats.GetTomeStat(QuestboardPanel.instance.IncentiveTomeRequirement, GameStats.Stat.TomesWon, 0) > 0;
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00083AAB File Offset: 0x00081CAB
	public static bool IsBookClubUnlocked
	{
		get
		{
			return GameStats.GetGlobalStat(GameStats.Stat.MaxBinding, 0) >= 10;
		}
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x00083ABB File Offset: 0x00081CBB
	private void Awake()
	{
		QuestboardPanel.instance = this;
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnEntered));
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x00083AEC File Offset: 0x00081CEC
	private void OnEntered()
	{
		this.UnclaimedSignature.gameObject.SetActive(SignatureChallengePanel.HasUnclaimed());
		this.QuestsUnclaimed.gameObject.SetActive(DailyQuestPanel.HasUnclaimed());
		this.SetupIncentiveDisplay();
		this.BookClubLockedDisplay.SetActive(!QuestboardPanel.IsBookClubUnlocked);
		this.BookClubUnlockedDisplay.SetActive(QuestboardPanel.IsBookClubUnlocked);
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x00083B4C File Offset: 0x00081D4C
	public void GoToSignatureChallenges()
	{
		PanelManager.instance.PushPanel(PanelType.SignatureChallenges);
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x00083B5A File Offset: 0x00081D5A
	public void GoToDailyQuests()
	{
		PanelManager.instance.PushPanel(PanelType.DailyQuests);
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x00083B68 File Offset: 0x00081D68
	public void GoToBookClub()
	{
		if (!QuestboardPanel.IsBookClubUnlocked)
		{
			return;
		}
		PanelManager.instance.PushPanel(PanelType.BookClub);
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x00083B80 File Offset: 0x00081D80
	private void SetupIncentiveDisplay()
	{
		if (!QuestboardPanel.AreIncentivesUnlocked)
		{
			this.AbilityIncentiveGroup.SetActive(false);
			this.TomeIncentiveHolder.SetActive(false);
			this.NoTomeIncentiveHolder.SetActive(false);
			this.HeaderIncentiveGroup.SetActive(false);
			this.IncentivesLockedDisplay.SetActive(true);
			return;
		}
		this.HeaderIncentiveGroup.gameObject.SetActive(true);
		this.IncentivesLockedDisplay.SetActive(false);
		AbilityTree ability = GraphDB.GetAbility(GoalManager.AbilityIncentive);
		if (ability != null)
		{
			this.AbilityIncentiveGroup.SetActive(true);
			this.IncentiveAbilityIcon.sprite = ability.Root.Usage.AbilityMetadata.Icon;
		}
		GenreTree genre = GraphDB.GetGenre(GoalManager.TomeIncentive);
		if (genre != null)
		{
			this.TomeIncentiveHolder.gameObject.SetActive(true);
			this.NoTomeIncentiveHolder.gameObject.SetActive(false);
			this.IncentiveTomeIcon.sprite = genre.Root.Icon;
			return;
		}
		this.TomeIncentiveHolder.gameObject.SetActive(false);
		this.NoTomeIncentiveHolder.gameObject.SetActive(true);
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x00083C9D File Offset: 0x00081E9D
	public static bool HasUnclaimedRewards()
	{
		return SignatureChallengePanel.HasUnclaimed() || DailyQuestPanel.HasUnclaimed();
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x00083CB2 File Offset: 0x00081EB2
	public QuestboardPanel()
	{
	}

	// Token: 0x0400145F RID: 5215
	public static QuestboardPanel instance;

	// Token: 0x04001460 RID: 5216
	public GenreTree IncentiveTomeRequirement;

	// Token: 0x04001461 RID: 5217
	public GameObject IncentivesLockedDisplay;

	// Token: 0x04001462 RID: 5218
	public Image IncentiveAbilityIcon;

	// Token: 0x04001463 RID: 5219
	public Image IncentiveTomeIcon;

	// Token: 0x04001464 RID: 5220
	public GameObject HeaderIncentiveGroup;

	// Token: 0x04001465 RID: 5221
	public GameObject AbilityIncentiveGroup;

	// Token: 0x04001466 RID: 5222
	public GameObject TomeIncentiveHolder;

	// Token: 0x04001467 RID: 5223
	public GameObject NoTomeIncentiveHolder;

	// Token: 0x04001468 RID: 5224
	public GameObject BookClubLockedDisplay;

	// Token: 0x04001469 RID: 5225
	public GameObject BookClubUnlockedDisplay;

	// Token: 0x0400146A RID: 5226
	public GameObject UnclaimedSignature;

	// Token: 0x0400146B RID: 5227
	public GameObject QuestsUnclaimed;
}
