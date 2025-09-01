using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

// Token: 0x02000178 RID: 376
public class PostGame_CurrencyRewards : MonoBehaviour
{
	// Token: 0x06001002 RID: 4098 RVA: 0x00064A70 File Offset: 0x00062C70
	private void Awake()
	{
		PostGame_CurrencyRewards.instance = this;
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x00064A78 File Offset: 0x00062C78
	public void Setup(bool didWin)
	{
		this.wonGame = didWin;
		int num = 0;
		foreach (UnlockDB.RewardType reward in UnlockDB.RewardType.GameBase.GetTypes<UnlockDB.RewardType>())
		{
			if (this.ShouldReward(reward))
			{
				ValueTuple<int, UnlockDB.RewardInfo> rewardValue = UnlockDB.DB.GetRewardValue(reward, didWin);
				if (rewardValue.Item1 >= 0)
				{
					this.AddLineItem(rewardValue.Item2.Name, rewardValue.Item1, true);
					num += rewardValue.Item1;
				}
			}
		}
		this.AddLineItem("Quillmarks", PostGame_CurrencyRewards.CompletionQuillmarks, false);
		this.SetupIncentiveReward();
		if (WaveManager.instance.AppendixLevel > 0)
		{
			this.AddLineItem("Appendix " + WaveManager.instance.AppendixLevel.ToString(), PostGame_CurrencyRewards.AppendixQuillmarks, false);
		}
		this.SetTotal(num, PostGame_CurrencyRewards.CompletionQuillmarks + PostGame_CurrencyRewards.AppendixQuillmarks);
		Currency.Add(num, true);
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x00064B70 File Offset: 0x00062D70
	public void SetupRaid(bool didWin)
	{
		this.wonGame = didWin;
		this.AddLineItem("Quillmarks", PostGame_CurrencyRewards.CompletionQuillmarks, false);
		this.AddLineItem("Progress", PostGame_CurrencyRewards.GildingReward, true);
		int num = 0;
		foreach (UnlockDB.RewardType reward in UnlockDB.RewardType.GameBase.GetTypes<UnlockDB.RewardType>())
		{
			if (this.ShouldReward(reward))
			{
				ValueTuple<int, UnlockDB.RewardInfo> rewardValue = UnlockDB.DB.GetRewardValue(reward, didWin);
				if (rewardValue.Item1 >= 0)
				{
					this.AddLineItem(rewardValue.Item2.Name, rewardValue.Item1, true);
					num += rewardValue.Item1;
				}
			}
		}
		this.SetTotal(PostGame_CurrencyRewards.GildingReward, PostGame_CurrencyRewards.CompletionQuillmarks);
		Currency.Add(num, true);
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x00064C40 File Offset: 0x00062E40
	private void SetupIncentiveReward()
	{
		if (!QuestboardPanel.AreIncentivesUnlocked)
		{
			return;
		}
		int num = 0;
		if (GoalManager.IsIncentiveAbilityEquipped())
		{
			num += MetaDB.AbilityIncentiveReward;
		}
		if (GoalManager.IsIncentiveTome(GameplayManager.instance.GameRoot))
		{
			num += MetaDB.TomeIncentiveReward;
		}
		if (num > 0)
		{
			this.AddLineItem("Required Reading", num, false);
		}
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x00064C90 File Offset: 0x00062E90
	public static void SetCompletionQuills(int amount)
	{
		PostGame_CurrencyRewards.CompletionQuillmarks = amount;
		PostGame_CurrencyRewards.AppendixQuillmarks = 0;
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x00064C9E File Offset: 0x00062E9E
	public static void AddAppendixQuills(int amount)
	{
		PostGame_CurrencyRewards.AppendixQuillmarks += amount;
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x00064CAC File Offset: 0x00062EAC
	public static void SetGildingInfo(int amount)
	{
		PostGame_CurrencyRewards.GildingReward = amount;
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x00064CB4 File Offset: 0x00062EB4
	private bool ShouldReward(UnlockDB.RewardType reward)
	{
		bool result;
		switch (reward)
		{
		case UnlockDB.RewardType.Bindings:
			result = (GameplayManager.instance.GenreBindings.TreeIDs.Count > 0 && !RaidManager.IsInRaid);
			break;
		case UnlockDB.RewardType.SuccessBonus:
			result = (this.wonGame && !RaidManager.IsInRaid);
			break;
		case UnlockDB.RewardType.Friends:
			result = (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1);
			break;
		case UnlockDB.RewardType.PerWave:
			result = !RaidManager.IsInRaid;
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x00064D40 File Offset: 0x00062F40
	private void AddLineItem(string label, int val, bool isCosmetic = true)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardItemRef, this.RewardList);
		gameObject.gameObject.SetActive(true);
		PostGame_Reward_LineItem component = gameObject.GetComponent<PostGame_Reward_LineItem>();
		component.SetupBasic(label, val, isCosmetic, true);
		this.RewardItems.Add(component);
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x00064D86 File Offset: 0x00062F86
	private void SetTotal(int cosmet, int quillmarks)
	{
		this.CurrencyTotal.text = cosmet.ToString();
		this.QuillmarkTotal.text = quillmarks.ToString();
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x00064DAC File Offset: 0x00062FAC
	public void Clear()
	{
		foreach (PostGame_Reward_LineItem postGame_Reward_LineItem in this.RewardItems)
		{
			UnityEngine.Object.Destroy(postGame_Reward_LineItem.gameObject);
		}
		this.RewardItems.Clear();
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00064E0C File Offset: 0x0006300C
	public PostGame_CurrencyRewards()
	{
	}

	// Token: 0x04000E18 RID: 3608
	public static PostGame_CurrencyRewards instance;

	// Token: 0x04000E19 RID: 3609
	public Transform RewardList;

	// Token: 0x04000E1A RID: 3610
	public GameObject RewardItemRef;

	// Token: 0x04000E1B RID: 3611
	public TextMeshProUGUI CurrencyTotal;

	// Token: 0x04000E1C RID: 3612
	public TextMeshProUGUI QuillmarkTotal;

	// Token: 0x04000E1D RID: 3613
	private bool wonGame;

	// Token: 0x04000E1E RID: 3614
	private List<PostGame_Reward_LineItem> RewardItems = new List<PostGame_Reward_LineItem>();

	// Token: 0x04000E1F RID: 3615
	private static int CompletionQuillmarks;

	// Token: 0x04000E20 RID: 3616
	private static int AppendixQuillmarks;

	// Token: 0x04000E21 RID: 3617
	private static int GildingReward;
}
