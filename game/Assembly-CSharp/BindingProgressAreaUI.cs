using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200013D RID: 317
public class BindingProgressAreaUI : MonoBehaviour
{
	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0005B619 File Offset: 0x00059819
	private bool IsOverbound
	{
		get
		{
			return this.AttuneLevel != BindingProgressAreaUI.OverboundRank.Matched && this.AttuneLevel > BindingProgressAreaUI.OverboundRank.Under;
		}
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x0005B630 File Offset: 0x00059830
	private void Awake()
	{
		this.AllyAttuneRef.SetActive(false);
		this.ReadyButton.onClick.AddListener(new UnityAction(this.SubmitInteraction));
		this.SetupGlobalNodes();
		this.cLerp = this.ProgressFill.color;
		this.RewardBoxRef.gameObject.SetActive(false);
		for (int i = 0; i < 5; i++)
		{
			this.CreateRewardBox();
		}
		UISelector.SetupHorizontalListNav<BindingRewardDisplay>(this.rewardBoxes, null, null, false);
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x0005B6B0 File Offset: 0x000598B0
	public void Setup(GenreTree g)
	{
		this.genre = g;
		this.BindingsUpdated();
		this.ClearVotes();
		if (Progression.BindingAttunement >= 20)
		{
			this.AttuneTarget.text = "Fully Attuned?";
			this.AttuneTargetMarker.gameObject.SetActive(false);
			this.SelfAttunePingable.SetupAsAttunement(-1);
		}
		else
		{
			this.AttuneTarget.text = "<sprite name=\"binding\"> Target:  <size=52>" + Progression.AttunementTarget.ToString();
			this.AttuneTargetPointText.text = Progression.AttunementTarget.ToString();
			float num = BindingProgressAreaUI.GetPointOnRing((float)Progression.AttunementTarget) * 360f;
			this.AttuneTargetMarker.gameObject.SetActive(true);
			float num2 = -num - 1f;
			this.AttuneTargetMarker.localEulerAngles = new Vector3(0f, 0f, num2);
			this.AttuneTargetInner.localEulerAngles = new Vector3(0f, 0f, -num2);
			this.SelfAttunePingable.SetupAsAttunement(Progression.AttunementTarget);
		}
		this.RandomBindingTarget.text = Mathf.Clamp(Progression.AttunementTarget, 1, 20).ToString();
		if (PhotonNetwork.InRoom && !PhotonNetwork.OfflineMode)
		{
			this.SetupAllyAttunementDisplays();
		}
		this.curTornPercent = (float)this.wantTornPercent;
		this.curRewardPercent = (float)this.wantRewardPercent;
		this.ClearRewards();
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x0005B80C File Offset: 0x00059A0C
	private void SetupAllyAttunementDisplays()
	{
		foreach (BindingAllyAttunementTargetDisplay bindingAllyAttunementTargetDisplay in this.AllyAttuneTargets)
		{
			UnityEngine.Object.Destroy(bindingAllyAttunementTargetDisplay.gameObject);
		}
		this.AllyAttuneTargets.Clear();
		foreach (PlayerControl playerControl in PlayerControl.AllPlayers)
		{
			if (!playerControl.IsMine && playerControl.Net.CurrentAttunement < 20)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AllyAttuneRef, this.AllyAttuneRef.transform.parent);
				gameObject.SetActive(true);
				BindingAllyAttunementTargetDisplay component = gameObject.GetComponent<BindingAllyAttunementTargetDisplay>();
				component.Setup(playerControl, playerControl.Net.CurrentAttunement + 1);
				this.AllyAttuneTargets.Add(component);
			}
		}
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0005B908 File Offset: 0x00059B08
	public void BindingsUpdated()
	{
		int currentBindingLevel = BindingsPanel.instance.CurrentBindingLevel;
		this.SetOverbound(currentBindingLevel);
		this.TotalLevel.text = currentBindingLevel.ToString();
		this.TotalLevel.colorGradient = ((this.AttuneLevel == BindingProgressAreaUI.OverboundRank.Under) ? this.LevelGradBase : this.LevelGradMet);
		this.wantTornPercent = currentBindingLevel * 10;
		float num = Progression.GetBindingRewardMult(currentBindingLevel) - 1f;
		this.wantRewardPercent = (int)(num * 100f);
		if (BindingsPanel.instance.CurrentBindingLevel >= Progression.AttunementTarget && Progression.BindingAttunement < 20)
		{
			int num2 = 1;
			if (Progression.OverbindAllowed > 0)
			{
				num2 = Progression.GetAttunementBoost(BindingsPanel.instance.CurrentBindingLevel);
			}
			this.AttuneBonusText.text = string.Format("+{0} Attunement", num2);
		}
		this.UpdateRewards(currentBindingLevel);
		if (VoteManager.PlayerVotes.ContainsKey(PlayerControl.myInstance.view.OwnerActorNr))
		{
			VoteManager.Vote(-1);
		}
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x0005B9F8 File Offset: 0x00059BF8
	public void TickUpdate()
	{
		int currentBindingLevel = BindingsPanel.instance.CurrentBindingLevel;
		this.ProgressFill.fillAmount = Mathf.Lerp(this.ProgressFill.fillAmount, BindingProgressAreaUI.GetPointOnRing((float)currentBindingLevel), Time.deltaTime * 6f);
		float num = -this.ProgressFill.fillAmount * 360f;
		num -= 5f - 10f * this.ProgressFill.fillAmount;
		this.FillEndMarker.localEulerAngles = new Vector3(0f, 0f, num);
		bool flag = currentBindingLevel >= Progression.AttunementTarget;
		this.AttuneMetDisplay.UpdateOpacity(flag, 4f, true);
		this.AttuneReward.UpdateOpacity(flag && Progression.BindingAttunement < 20, 4f, true);
		foreach (BindingGlobalNode bindingGlobalNode in this.globals)
		{
			bindingGlobalNode.TickUpdate(currentBindingLevel);
		}
		this.EndMarkerGroup.UpdateOpacity(currentBindingLevel > 0 && currentBindingLevel < 20, 6f, true);
		this.SetOverbound(currentBindingLevel);
		this.UpdateOverboundDisplay();
		this.UpdateBonusTexts();
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0005BB38 File Offset: 0x00059D38
	private void UpdateOverboundDisplay()
	{
		BindingProgressAreaUI.OverboundInfo attuneInfo = this.GetAttuneInfo();
		this.OverboundGroup.UpdateOpacity(this.IsOverbound, 4f, true);
		this.cLerp = Color.Lerp(this.cLerp, attuneInfo.FillColor, Time.deltaTime * 4f);
		this.ProgressFill.color = this.cLerp;
		this.FillEndMarkDisplay.color = this.ProgressFill.color;
		this.OverboundText.color = this.ProgressFill.color;
		if (this.textRank != this.AttuneLevel || Progression.OverbindAllowed > 0)
		{
			this.textRank = this.AttuneLevel;
			this.OverboundText.text = attuneInfo.DetailText;
		}
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x0005BBF8 File Offset: 0x00059DF8
	private void UpdateBonusTexts()
	{
		this.curTornPercent = Mathf.Lerp(this.curTornPercent, (float)this.wantTornPercent, Time.deltaTime * 4f);
		this.curTornPercent = Mathf.MoveTowards(this.curTornPercent, (float)this.wantTornPercent, 1f);
		string text = string.Format("+{0}%", (int)this.curTornPercent);
		if (this.TornBonus.text != text)
		{
			this.TornBonus.text = text;
		}
		this.curRewardPercent = Mathf.Lerp(this.curRewardPercent, (float)this.wantRewardPercent, Time.deltaTime * 4f);
		this.curRewardPercent = Mathf.MoveTowards(this.curRewardPercent, (float)this.wantRewardPercent, 1f);
		text = string.Format("+{0}%", (int)this.curRewardPercent);
		if (this.IsOverbound)
		{
			text = "<sprite name=\"lock\">" + text;
		}
		if (this.RewardBonus.text != text)
		{
			this.RewardBonus.text = text;
		}
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x0005BD0C File Offset: 0x00059F0C
	public void OnVotesChanged()
	{
		string text = "Embark";
		if (VoteManager.PlayerVotes.ContainsKey(PhotonNetwork.LocalPlayer.ActorNumber))
		{
			text = "Wait!";
		}
		this.ReadyButtonText.text = text;
		this.ClearVotes();
		foreach (KeyValuePair<int, int> keyValuePair in VoteManager.PlayerVotes)
		{
			this.AddVoteDisplay(keyValuePair.Key);
		}
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x0005BD98 File Offset: 0x00059F98
	private void AddVoteDisplay(int PlayerID)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StartVoteRef, this.StartVoteRef.transform.parent);
		gameObject.gameObject.SetActive(true);
		UIPlayerVoteDisplay component = gameObject.GetComponent<UIPlayerVoteDisplay>();
		component.Setup(PlayerID);
		this.VoteRefs.Add(component);
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x0005BDE8 File Offset: 0x00059FE8
	private void ClearVotes()
	{
		foreach (UIPlayerVoteDisplay uiplayerVoteDisplay in this.VoteRefs)
		{
			UnityEngine.Object.Destroy(uiplayerVoteDisplay.gameObject);
		}
		this.VoteRefs.Clear();
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x0005BE48 File Offset: 0x0005A048
	public void SubmitInteraction()
	{
		if (VoteManager.PlayerVotes.ContainsKey(PlayerControl.myInstance.view.OwnerActorNr))
		{
			VoteManager.Vote(-1);
			return;
		}
		VoteManager.Vote(1);
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x0005BE74 File Offset: 0x0005A074
	private void SetupGlobalNodes()
	{
		foreach (WaveDB.GlobalBinding b in this.GlobalBindingDB.GlobalBindings)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GlobalNodeRef, this.GlobalNodeRef.transform.parent);
			gameObject.SetActive(true);
			BindingGlobalNode component = gameObject.GetComponent<BindingGlobalNode>();
			component.Setup(b);
			this.globals.Add(component);
		}
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x0005BF00 File Offset: 0x0005A100
	private void UpdateRewards(int bindingLevel)
	{
		List<Unlockable> genreRewards = UnlockDB.GetGenreRewards(this.genre, bindingLevel);
		for (int i = genreRewards.Count - 1; i >= 0; i--)
		{
			if (UnlockManager.IsUnlocked(genreRewards[i]))
			{
				genreRewards.RemoveAt(i);
			}
		}
		foreach (BindingRewardDisplay bindingRewardDisplay in this.rewardBoxes)
		{
			if (bindingRewardDisplay.ulRef != null && !genreRewards.Contains(bindingRewardDisplay.ulRef))
			{
				bindingRewardDisplay.Clear();
			}
		}
		foreach (Unlockable ul in genreRewards)
		{
			this.TryAddRewardBox(ul);
		}
		this.TryAddBindingReward();
		this.TryAddIncentiveReward();
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0005BFEC File Offset: 0x0005A1EC
	private void TryAddBindingReward()
	{
		bool flag = false;
		using (List<BindingRewardDisplay>.Enumerator enumerator = this.rewardBoxes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsBindingReward)
				{
					flag = true;
				}
			}
		}
		if (UnlockDB.GetAvailableBindings(BindingsPanel.instance.selectedBindings).Count > 0)
		{
			if (flag)
			{
				return;
			}
			using (List<BindingRewardDisplay>.Enumerator enumerator = this.rewardBoxes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BindingRewardDisplay bindingRewardDisplay = enumerator.Current;
					if (bindingRewardDisplay.IsEmpty)
					{
						bindingRewardDisplay.Setup(null);
						break;
					}
				}
				return;
			}
		}
		if (flag)
		{
			foreach (BindingRewardDisplay bindingRewardDisplay2 in this.rewardBoxes)
			{
				if (bindingRewardDisplay2.IsBindingReward)
				{
					bindingRewardDisplay2.Clear();
					break;
				}
			}
		}
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x0005C0FC File Offset: 0x0005A2FC
	private void TryAddIncentiveReward()
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
		if (GoalManager.IsIncentiveTome(this.genre))
		{
			num += MetaDB.TomeIncentiveReward;
		}
		if (num > 0)
		{
			foreach (BindingRewardDisplay bindingRewardDisplay in this.rewardBoxes)
			{
				if (bindingRewardDisplay.IsIncentiveReward)
				{
					bindingRewardDisplay.SetupAsIncentive(num);
					break;
				}
				if (bindingRewardDisplay.IsEmpty)
				{
					bindingRewardDisplay.SetupAsIncentive(num);
					break;
				}
			}
		}
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x0005C19C File Offset: 0x0005A39C
	private void TryAddRewardBox(Unlockable ul)
	{
		using (List<BindingRewardDisplay>.Enumerator enumerator = this.rewardBoxes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ulRef == ul)
				{
					return;
				}
			}
		}
		foreach (BindingRewardDisplay bindingRewardDisplay in this.rewardBoxes)
		{
			if (bindingRewardDisplay.IsEmpty)
			{
				bindingRewardDisplay.Setup(ul);
				break;
			}
		}
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x0005C240 File Offset: 0x0005A440
	private void CreateRewardBox()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardBoxRef, this.RewardBoxRef.transform.parent);
		gameObject.SetActive(true);
		BindingRewardDisplay component = gameObject.GetComponent<BindingRewardDisplay>();
		this.rewardBoxes.Add(component);
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0005C284 File Offset: 0x0005A484
	private void ClearRewards()
	{
		foreach (BindingRewardDisplay bindingRewardDisplay in this.rewardBoxes)
		{
			bindingRewardDisplay.Clear();
		}
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x0005C2D4 File Offset: 0x0005A4D4
	public void BindingActivated(BindingRankUI rank)
	{
		this.BindingActivateFX.transform.position = rank.transform.position;
		this.BindingActivateFX.Play();
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x0005C2FC File Offset: 0x0005A4FC
	private static float MapValueToRange(float value, float x, float y)
	{
		return 0f + x + value * (1f - y - (0f + x));
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x0005C318 File Offset: 0x0005A518
	public static float GetPointOnRing(float bindingLevel)
	{
		float num = 20f;
		float value = Mathf.Min(bindingLevel, num) / num;
		float x = 0.06f;
		float y = 0.06f;
		return Mathf.Min(BindingProgressAreaUI.MapValueToRange(value, x, y), 1f);
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x0005C354 File Offset: 0x0005A554
	private void SetOverbound(int curLevel)
	{
		int attunementTarget = Progression.AttunementTarget;
		bool flag = attunementTarget >= 20;
		if (curLevel < attunementTarget)
		{
			this.AttuneLevel = BindingProgressAreaUI.OverboundRank.Under;
			return;
		}
		if (curLevel == attunementTarget)
		{
			this.AttuneLevel = BindingProgressAreaUI.OverboundRank.Matched;
			return;
		}
		if (curLevel > attunementTarget + 10 && !flag)
		{
			this.AttuneLevel = BindingProgressAreaUI.OverboundRank.ExtremeOver;
			return;
		}
		if (curLevel > attunementTarget + 5 && !flag)
		{
			this.AttuneLevel = BindingProgressAreaUI.OverboundRank.HighOver;
			return;
		}
		if (curLevel > attunementTarget && !flag)
		{
			this.AttuneLevel = BindingProgressAreaUI.OverboundRank.Over;
			return;
		}
		this.AttuneLevel = BindingProgressAreaUI.OverboundRank.Matched;
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x0005C3C4 File Offset: 0x0005A5C4
	private BindingProgressAreaUI.OverboundInfo GetAttuneInfo()
	{
		if (Progression.OverbindAllowed > 0)
		{
			int num = BindingsPanel.instance.CurrentBindingLevel - Progression.BindingAttunement;
			int num2 = 5 - num % 5;
			int num3 = 2 + Mathf.FloorToInt((float)num / 5f);
			return new BindingProgressAreaUI.OverboundInfo
			{
				Rank = BindingProgressAreaUI.OverboundRank.Over,
				FillColor = new Color(0.9f, 0.8f, 1f),
				DetailText = string.Format("<size=20>Ascendant: <b>{0}</b>Binding Levels\nneeded for <b>+{1}</b> Attunement.</size>", num2, num3)
			};
		}
		foreach (BindingProgressAreaUI.OverboundInfo overboundInfo in this.AttunementInfo)
		{
			if (overboundInfo.Rank == this.AttuneLevel)
			{
				return overboundInfo;
			}
		}
		return null;
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x0005C49C File Offset: 0x0005A69C
	public BindingProgressAreaUI()
	{
	}

	// Token: 0x04000BE2 RID: 3042
	public TextMeshProUGUI TotalLevel;

	// Token: 0x04000BE3 RID: 3043
	public TextMeshProUGUI AttuneTarget;

	// Token: 0x04000BE4 RID: 3044
	public VertexGradient LevelGradBase;

	// Token: 0x04000BE5 RID: 3045
	public VertexGradient LevelGradMet;

	// Token: 0x04000BE6 RID: 3046
	public Transform AttuneTargetMarker;

	// Token: 0x04000BE7 RID: 3047
	public Transform AttuneTargetInner;

	// Token: 0x04000BE8 RID: 3048
	public UIPingable SelfAttunePingable;

	// Token: 0x04000BE9 RID: 3049
	public TextMeshProUGUI AttuneTargetPointText;

	// Token: 0x04000BEA RID: 3050
	public TextMeshProUGUI RandomBindingTarget;

	// Token: 0x04000BEB RID: 3051
	public CanvasGroup AttuneMetDisplay;

	// Token: 0x04000BEC RID: 3052
	public List<BindingProgressAreaUI.OverboundInfo> AttunementInfo;

	// Token: 0x04000BED RID: 3053
	public GameObject AllyAttuneRef;

	// Token: 0x04000BEE RID: 3054
	private List<BindingAllyAttunementTargetDisplay> AllyAttuneTargets = new List<BindingAllyAttunementTargetDisplay>();

	// Token: 0x04000BEF RID: 3055
	public Image ProgressFill;

	// Token: 0x04000BF0 RID: 3056
	public CanvasGroup EndMarkerGroup;

	// Token: 0x04000BF1 RID: 3057
	public Transform FillEndMarker;

	// Token: 0x04000BF2 RID: 3058
	public ParticleSystem BindingActivateFX;

	// Token: 0x04000BF3 RID: 3059
	public Image FillEndMarkDisplay;

	// Token: 0x04000BF4 RID: 3060
	private Color cLerp;

	// Token: 0x04000BF5 RID: 3061
	public TextMeshProUGUI RewardBonus;

	// Token: 0x04000BF6 RID: 3062
	public TextMeshProUGUI TornBonus;

	// Token: 0x04000BF7 RID: 3063
	public CanvasGroup OverboundGroup;

	// Token: 0x04000BF8 RID: 3064
	public TextMeshProUGUI OverboundText;

	// Token: 0x04000BF9 RID: 3065
	public WaveDB GlobalBindingDB;

	// Token: 0x04000BFA RID: 3066
	public GameObject GlobalNodeRef;

	// Token: 0x04000BFB RID: 3067
	[Header("Voting Prepare")]
	public Button ReadyButton;

	// Token: 0x04000BFC RID: 3068
	public TextMeshProUGUI ReadyButtonText;

	// Token: 0x04000BFD RID: 3069
	public GameObject StartVoteRef;

	// Token: 0x04000BFE RID: 3070
	private List<UIPlayerVoteDisplay> VoteRefs = new List<UIPlayerVoteDisplay>();

	// Token: 0x04000BFF RID: 3071
	private int wantTornPercent;

	// Token: 0x04000C00 RID: 3072
	private float curTornPercent;

	// Token: 0x04000C01 RID: 3073
	private int wantRewardPercent;

	// Token: 0x04000C02 RID: 3074
	private float curRewardPercent;

	// Token: 0x04000C03 RID: 3075
	private List<BindingGlobalNode> globals = new List<BindingGlobalNode>();

	// Token: 0x04000C04 RID: 3076
	public GameObject RewardBoxRef;

	// Token: 0x04000C05 RID: 3077
	public CanvasGroup AttuneReward;

	// Token: 0x04000C06 RID: 3078
	public TextMeshProUGUI AttuneBonusText;

	// Token: 0x04000C07 RID: 3079
	private List<BindingRewardDisplay> rewardBoxes = new List<BindingRewardDisplay>();

	// Token: 0x04000C08 RID: 3080
	private GenreTree genre;

	// Token: 0x04000C09 RID: 3081
	private BindingProgressAreaUI.OverboundRank AttuneLevel;

	// Token: 0x04000C0A RID: 3082
	private BindingProgressAreaUI.OverboundRank textRank;

	// Token: 0x0200053A RID: 1338
	public enum OverboundRank
	{
		// Token: 0x04002661 RID: 9825
		Under,
		// Token: 0x04002662 RID: 9826
		Matched,
		// Token: 0x04002663 RID: 9827
		Over,
		// Token: 0x04002664 RID: 9828
		HighOver,
		// Token: 0x04002665 RID: 9829
		ExtremeOver,
		// Token: 0x04002666 RID: 9830
		Ascendant
	}

	// Token: 0x0200053B RID: 1339
	[Serializable]
	public class OverboundInfo
	{
		// Token: 0x0600241F RID: 9247 RVA: 0x000CD0D7 File Offset: 0x000CB2D7
		public OverboundInfo()
		{
		}

		// Token: 0x04002667 RID: 9831
		public BindingProgressAreaUI.OverboundRank Rank;

		// Token: 0x04002668 RID: 9832
		public Color FillColor;

		// Token: 0x04002669 RID: 9833
		[TextArea(4, 5)]
		public string DetailText;
	}
}
