using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000149 RID: 329
public class Codex_RunEntry : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	// Token: 0x06000ED6 RID: 3798 RVA: 0x0005E624 File Offset: 0x0005C824
	public bool Setup(LocalRunRecord record)
	{
		this.Record = record;
		bool result;
		try
		{
			if (record.IsRaid)
			{
				result = this.SetupAsRaid(record);
			}
			else
			{
				this.TomeTitle.text = GraphDB.GetGenre(record.TomeID).Root.ShortName;
				if (record.IsChallenge)
				{
					MetaDB.BookClubChallenge bookClubChallenge = MetaDB.GetBookClubChallenge(record.ChallengeID);
					if (bookClubChallenge != null)
					{
						this.TomeTitle.text = bookClubChallenge.Name;
					}
					else
					{
						this.TomeTitle.text = "Book Club Challenge";
					}
				}
				this.ChallengeEmblem.SetActive(record.IsChallenge);
				this.GameResult.text = (record.Won ? "Victory" : "Defeat");
				this.Timer.text = TimeSpan.FromSeconds((double)record.Timer).ToString("hh':'mm':'ss");
				this.AppendixGroup.alpha = ((record.Appendix > 0) ? 1f : 0.25f);
				this.Appendix.text = record.Appendix.ToString();
				this.BindingGroup.SetActive(true);
				this.Bindings.text = record.BindingLevel.ToString();
				this.RaidStamp.enabled = false;
				this.RaidDifficultyImage.enabled = false;
				this.SignatureIcon.sprite = PlayerDB.GetCore(record.MyInfo.Color).MajorIcon;
				this.PrimaryAbilityIcon.sprite = GraphDB.GetAbility(record.MyInfo.PrimaryAbility).Root.Usage.AbilityMetadata.Icon;
				this.SecondaryAbilityIcon.sprite = GraphDB.GetAbility(record.MyInfo.SecondaryAbility).Root.Usage.AbilityMetadata.Icon;
				this.MovementAbilityIcon.sprite = GraphDB.GetAbility(record.MyInfo.MovementAbility).Root.Usage.AbilityMetadata.Icon;
				this.DateText.text = record.RunDate.ToString("MM/dd/yyyy - HH:mm");
				result = true;
			}
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0005E860 File Offset: 0x0005CA60
	private bool SetupAsRaid(LocalRunRecord record)
	{
		bool result;
		try
		{
			RaidDB.Raid raid = RaidDB.GetRaid(record.Raid);
			this.AppendixGroup.alpha = 0f;
			this.RaidStamp.enabled = true;
			this.RaidStamp.sprite = (record.HardMode ? raid.StampHard : raid.StampNormal);
			this.BindingGroup.SetActive(false);
			this.RaidDifficultyImage.enabled = true;
			this.RaidDifficultyImage.sprite = (record.HardMode ? RaidDB.instance.RaidIconHard : RaidDB.instance.RaidIcon);
			this.GameResult.text = raid.RaidName;
			this.TomeTitle.text = (record.Won ? "Completed" : "Failed");
			this.SignatureIcon.sprite = PlayerDB.GetCore(record.MyInfo.Color).MajorIcon;
			this.PrimaryAbilityIcon.sprite = GraphDB.GetAbility(record.MyInfo.PrimaryAbility).Root.Usage.AbilityMetadata.Icon;
			this.SecondaryAbilityIcon.sprite = GraphDB.GetAbility(record.MyInfo.SecondaryAbility).Root.Usage.AbilityMetadata.Icon;
			this.MovementAbilityIcon.sprite = GraphDB.GetAbility(record.MyInfo.MovementAbility).Root.Usage.AbilityMetadata.Icon;
			this.DateText.text = record.RunDate.ToString("MM/dd/yyyy - HH:mm");
			this.Timer.text = TimeSpan.FromSeconds((double)record.Timer).ToString("hh':'mm':'ss");
			result = true;
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0005EA3C File Offset: 0x0005CC3C
	public void OnSelect(BaseEventData eventData)
	{
		CodexStatsPanel.instance.RunHistory.SelectRecord(this);
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x0005EA4E File Offset: 0x0005CC4E
	public void Click()
	{
		PostGamePanel.instance.OpenFromCodex(this.Record);
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x0005EA60 File Offset: 0x0005CC60
	public Codex_RunEntry()
	{
	}

	// Token: 0x04000C77 RID: 3191
	[Header("Base Info")]
	public TextMeshProUGUI GameResult;

	// Token: 0x04000C78 RID: 3192
	public TextMeshProUGUI TomeTitle;

	// Token: 0x04000C79 RID: 3193
	public TextMeshProUGUI Timer;

	// Token: 0x04000C7A RID: 3194
	public TextMeshProUGUI DateText;

	// Token: 0x04000C7B RID: 3195
	public GameObject ChallengeEmblem;

	// Token: 0x04000C7C RID: 3196
	[Header("Difficulty")]
	public GameObject BindingGroup;

	// Token: 0x04000C7D RID: 3197
	public TextMeshProUGUI Bindings;

	// Token: 0x04000C7E RID: 3198
	public CanvasGroup AppendixGroup;

	// Token: 0x04000C7F RID: 3199
	public TextMeshProUGUI Appendix;

	// Token: 0x04000C80 RID: 3200
	[Header("Loadout")]
	public Image SignatureIcon;

	// Token: 0x04000C81 RID: 3201
	public Image PrimaryAbilityIcon;

	// Token: 0x04000C82 RID: 3202
	public Image SecondaryAbilityIcon;

	// Token: 0x04000C83 RID: 3203
	public Image MovementAbilityIcon;

	// Token: 0x04000C84 RID: 3204
	[Header("Raid")]
	public Image RaidStamp;

	// Token: 0x04000C85 RID: 3205
	public Image RaidDifficultyImage;

	// Token: 0x04000C86 RID: 3206
	[NonSerialized]
	public LocalRunRecord Record;
}
