using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class GoalManager : MonoBehaviourPunCallbacks, IPunObservable
{
	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600081D RID: 2077 RVA: 0x00039020 File Offset: 0x00037220
	public static string TomeIncentive
	{
		get
		{
			return StateManager.GetID("TomeIncentive", "");
		}
	}

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x0600081E RID: 2078 RVA: 0x00039031 File Offset: 0x00037231
	public static bool InBonusObjective
	{
		get
		{
			return GoalManager.HasObjective && !GoalManager.ObjectiveCompleted;
		}
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x0600081F RID: 2079 RVA: 0x00039044 File Offset: 0x00037244
	public static bool InVignette
	{
		get
		{
			if (RaidManager.IsInRaid && RaidManager.PreparedEncounter != null)
			{
				return RaidManager.PreparedEncounter.Type == RaidDB.EncounterType.Vignette;
			}
			bool result;
			switch (GameplayManager.CurState)
			{
			case GameState.Vignette_Traveling:
				result = true;
				break;
			case GameState.Vignette_Inside:
				result = true;
				break;
			case GameState.Vignette_Completed:
				result = true;
				break;
			default:
				result = false;
				break;
			}
			return result;
		}
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x0003909C File Offset: 0x0003729C
	private void Awake()
	{
		GoalManager.instance = this;
		GoalManager.BonusObjective = null;
		GoalManager.ObjectiveCompleted = false;
		GoalManager.DoneObjectiveCompleteEvent = false;
		GoalManager.HasObjective = false;
		GoalManager.StartedObjective = false;
		GoalManager.InLastChance = false;
		this.gaveEliteReward = false;
		GoalManager.ObjectiveLocation = Vector3.zero;
		this.view = base.GetComponent<PhotonView>();
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x000390F0 File Offset: 0x000372F0
	public static void Reset(bool hard = false)
	{
		if (GoalManager.instance == null)
		{
			return;
		}
		GoalManager.instance.ClearBonusObjective();
		GoalManager.instance.CollectedBossRewards.Clear();
		if (hard)
		{
			GoalManager.instance.lastBonusID = "";
		}
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0003912C File Offset: 0x0003732C
	private void Update()
	{
		if (GoalManager.HasObjective && GoalManager.StartedObjective && !GoalManager.ObjectiveCompleted && GameplayManager.CurState == GameState.InWave)
		{
			if (WaveManager.WaveConfig.IsFinished() && !GoalManager.InLastChance)
			{
				this.FinalChanceObjective();
			}
			else if (GoalManager.InLastChance)
			{
				GoalManager.LastChanceT -= GameplayManager.deltaTime;
				if (GoalManager.LastChanceT <= 0f)
				{
					this.CancelBonusObjective();
				}
			}
		}
		LibraryMoteManager.TickUpdate();
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0003919F File Offset: 0x0003739F
	public void WaitToStartVignette()
	{
		this.rewardCollectsNeeded = PlayerControl.PlayerCount;
		this.CollectedBossRewards.Clear();
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x000391B7 File Offset: 0x000373B7
	public void PlayerPageRewardSelected()
	{
		this.view.RPC("CollectedRewardNet", RpcTarget.All, new object[]
		{
			PlayerControl.MyViewID
		});
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x000391E0 File Offset: 0x000373E0
	public void NextBonusObjective()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		GoalManager.ObjectiveCompleted = false;
		GoalManager.DoneObjectiveCompleteEvent = false;
		GoalManager.BonusObjective = null;
		GoalManager.HasObjective = (WaveManager.WaveConfig.Event == GenreWaveNode.EventType.BonusObjective);
		if (GoalManager.HasObjective)
		{
			AugmentTree bonusObjective = WaveManager.WaveConfig.GetBonusObjective(new List<string>
			{
				this.lastBonusID
			}, GameplayManager.BindingLevel);
			if (bonusObjective == null)
			{
				GoalManager.HasObjective = false;
				Debug.LogError("No Bonus Objective matched filter requirements");
			}
			else
			{
				this.SetBonusObjective(bonusObjective, false);
			}
		}
		this.SyncBonusObjective();
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x0003926C File Offset: 0x0003746C
	public void SetBonusObjective(AugmentTree objective, bool sync = false)
	{
		GoalManager.HasObjective = true;
		GoalManager.ObjectiveCompleted = false;
		GoalManager.DoneObjectiveCompleteEvent = false;
		GoalManager.BonusObjective = (objective.Clone() as AugmentTree);
		this.lastBonusID = GoalManager.BonusObjective.Root.guid;
		if (sync)
		{
			this.SyncBonusObjective();
		}
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x000392BC File Offset: 0x000374BC
	public void SyncBonusObjective()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("SyncBonusObjectiveNetwork", RpcTarget.Others, new object[]
		{
			GoalManager.HasObjective,
			GoalManager.ObjectiveCompleted,
			(GoalManager.BonusObjective == null) ? "" : GoalManager.BonusObjective.ID
		});
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00039324 File Offset: 0x00037524
	[PunRPC]
	public void SyncBonusObjectiveNetwork(bool hasObjective, bool objectiveCompleted, string objectiveString)
	{
		GoalManager.HasObjective = hasObjective;
		GoalManager.ObjectiveCompleted = objectiveCompleted;
		if (objectiveString.Length > 1)
		{
			GoalManager.BonusObjective = (GraphDB.GetAugment(objectiveString).Clone() as AugmentTree);
		}
		else
		{
			GoalManager.BonusObjective = null;
		}
		if (!GoalManager.InBonusObjective)
		{
			GoalManager.InLastChance = false;
		}
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x00039370 File Offset: 0x00037570
	public void BeginBonusObjective()
	{
		if (!PhotonNetwork.IsMasterClient || GoalManager.BonusObjective == null)
		{
			return;
		}
		this.view.RPC("BeginBonusNetwork", RpcTarget.All, Array.Empty<object>());
		GameplayManager.instance.TriggerWorldAugments(EventTrigger.WaveBonusStarted);
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x000393AC File Offset: 0x000375AC
	[PunRPC]
	private void BeginBonusNetwork()
	{
		BonusUI.instance.Setup();
		GoalManager.StartedObjective = true;
		GoalManager.InLastChance = false;
		GoalManager.ObjectiveLocation = SpawnPoint.GetObjectiveLocation(null);
		WaveProgressBar.instance.PulseBonusObjective();
		this.LastCancelAttempt = 0f;
		this.LastCompleteAttempt = 0f;
		AudioManager.PlaySFX2D(this.BonusStartedSFX, 1f, 0.1f);
		if (GoalManager.BonusObjective != null)
		{
			GameRecord.RecordEvent(GameRecord.EventType.Bonus_Spawned, GoalManager.BonusObjective.Root.Name, GoalManager.ObjectiveLocation);
			Action onBonusStarted = this.OnBonusStarted;
			if (onBonusStarted == null)
			{
				return;
			}
			onBonusStarted();
		}
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00039448 File Offset: 0x00037648
	public void TriggerAugments(EventTrigger Trigger)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		int num = UnityEngine.Random.Range(0, 9999999);
		this.view.RPC("TriggerBonusAugment", RpcTarget.All, new object[]
		{
			(int)Trigger,
			num
		});
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00039494 File Offset: 0x00037694
	[PunRPC]
	public void TriggerBonusAugment(int trigger, int randomSeed)
	{
		bool flag = false;
		if (trigger == 25 && !GoalManager.DoneObjectiveCompleteEvent)
		{
			flag = true;
			GoalManager.DoneObjectiveCompleteEvent = true;
		}
		if (!GoalManager.InBonusObjective && !flag)
		{
			return;
		}
		if (trigger == 26)
		{
			GoalManager.HasObjective = false;
		}
		EffectProperties effectProperties = new EffectProperties();
		effectProperties.IsWorld = true;
		effectProperties.SourceType = ActionSource.Genre;
		effectProperties.OverrideSeed(randomSeed, 0);
		try
		{
			GoalManager.BonusObjective.Root.TryTriggerFromProps((EventTrigger)trigger, effectProperties);
		}
		catch
		{
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00039514 File Offset: 0x00037714
	public void UpdateObjectiveText(string newText)
	{
		if (!GoalManager.HasObjective || GoalManager.ObjectiveCompleted)
		{
			return;
		}
		BonusUI.instance.UpdateDetail(newText);
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x00039530 File Offset: 0x00037730
	public static void TryCompleteBonusObjective(Vector3 point)
	{
		if (!GoalManager.InBonusObjective)
		{
			return;
		}
		if (Time.realtimeSinceStartup - GoalManager.instance.LastCompleteAttempt < 0.75f)
		{
			return;
		}
		GoalManager.instance.LastCompleteAttempt = Time.realtimeSinceStartup;
		GoalManager.instance.view.RPC("TryCompleteBonusNetwork", RpcTarget.All, new object[]
		{
			point
		});
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x00039590 File Offset: 0x00037790
	public static void TryCancelBonusObjective()
	{
		if (!GoalManager.InBonusObjective)
		{
			return;
		}
		if (Time.realtimeSinceStartup - GoalManager.instance.LastCancelAttempt < 0.75f)
		{
			return;
		}
		GoalManager.instance.LastCancelAttempt = Time.realtimeSinceStartup;
		GoalManager.instance.view.RPC("CancelBonusObjective", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x000395E6 File Offset: 0x000377E6
	private void FinalChanceObjective()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (!GoalManager.InBonusObjective)
		{
			return;
		}
		GoalManager.instance.view.RPC("SetupFinalChance", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x00039612 File Offset: 0x00037812
	[PunRPC]
	private void SetupFinalChance()
	{
		GoalManager.InLastChance = true;
		GoalManager.LastChanceT = 15f;
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00039624 File Offset: 0x00037824
	[PunRPC]
	public void CancelBonusObjective()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (!GoalManager.InBonusObjective)
		{
			return;
		}
		GameplayManager.instance.TriggerWorldAugments(EventTrigger.WaveBonusCanceled);
		GoalManager.HasObjective = false;
		BonusUI.instance.Failed();
		this.SyncBonusObjective();
		Action onBonusCanceled = this.OnBonusCanceled;
		if (onBonusCanceled != null)
		{
			onBonusCanceled();
		}
		AudioManager.PlayLoudSFX2D(this.BonusFailedSFX, 1f, 0.1f);
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0003968C File Offset: 0x0003788C
	[PunRPC]
	public void TryCompleteBonusNetwork(Vector3 point)
	{
		if (!PhotonNetwork.IsMasterClient || GoalManager.BonusObjective == null)
		{
			return;
		}
		if (!GoalManager.InBonusObjective)
		{
			return;
		}
		GoalManager.ObjectiveCompleted = true;
		point = AIManager.NearestNavPoint(point, -1f);
		this.view.RPC("CompleteObjectiveNetwork", RpcTarget.All, new object[]
		{
			GoalManager.BonusObjective.ID,
			point
		});
		GameplayManager.instance.TriggerWorldAugments(EventTrigger.WaveBonusCompleted);
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x00039704 File Offset: 0x00037904
	[PunRPC]
	public void CompleteObjectiveNetwork(string BonusGraphID, Vector3 point)
	{
		GoalManager.BonusObjective = (GraphDB.GetAugment(BonusGraphID).Clone() as AugmentTree);
		GoalManager.ObjectiveCompleted = true;
		GoalManager.InLastChance = false;
		if (GoalManager.BonusObjective == null)
		{
			return;
		}
		if (!TutorialManager.InTutorial)
		{
			AugmentFilter augmentFilter = GoalManager.BonusObjective.Root.Filter.Copy();
			augmentFilter.RestrictTags = true;
			augmentFilter.ExcludeModFilter.Filters.Add(ModFilter.Mech_Tradeoff);
			this.GiveAugmentScroll(augmentFilter, point);
		}
		else
		{
			TutorialManager.instance.ChangeStep(TutorialStep.EnemyChoice);
		}
		BonusUI.instance.Completed();
		GameRecord.RecordEvent(GameRecord.EventType.Bonus_Completed, GoalManager.BonusObjective.Root.Name);
		Action onBonusCompleted = this.OnBonusCompleted;
		if (onBonusCompleted != null)
		{
			onBonusCompleted();
		}
		AudioManager.PlayLoudSFX2D(this.BonusCompletedSFX, 1f, 0.1f);
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x000397D5 File Offset: 0x000379D5
	public void TestAugmentReward()
	{
		this.GiveAugmentScroll(new AugmentFilter(), Fountain.instance.transform.position);
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x000397F4 File Offset: 0x000379F4
	public void EliteReward(Vector3 pos)
	{
		GenreWaveNode waveConfig = WaveManager.WaveConfig;
		if (waveConfig == null || waveConfig.Spawns == null || waveConfig.Event != GenreWaveNode.EventType.Elite || this.gaveEliteReward)
		{
			return;
		}
		AugmentFilter augmentFilter = waveConfig.EliteReward.Copy();
		augmentFilter.RestrictTags = true;
		augmentFilter.ExcludeModFilter.Filters.Add(ModFilter.Mech_Tradeoff);
		this.GiveAugmentScroll(augmentFilter, pos);
		this.gaveEliteReward = true;
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00039868 File Offset: 0x00037A68
	public void TryMidgameBossReward(Vector3 pos)
	{
		this.GiveAugmentScroll(new AugmentFilter
		{
			RestrictRarity = true,
			MinRarity = AugmentQuality.Epic,
			MaxRarity = AugmentQuality.Legendary,
			RestrictTags = true,
			ExcludeModFilter = 
			{
				Filters = 
				{
					ModFilter.Mech_Tradeoff
				}
			}
		}, pos);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x000398B8 File Offset: 0x00037AB8
	public ScrollPickup GiveAugmentScroll(AugmentFilter filter, Vector3 pos)
	{
		List<AugmentTree> validMods = GraphDB.GetValidMods(ModType.Player);
		filter.FilterList(validMods, PlayerControl.myInstance);
		if (validMods.Count == 0)
		{
			AudioManager.PlayInterfaceSFX(this.NoScrollMatchSFX, 1f, 0f);
			InfoDisplay.SetText("No Page met the Requirements...", 2f, InfoArea.DetailBottom);
			return null;
		}
		AugmentTree augment = validMods[GameplayManager.IsChallengeActive ? PlayerControl.myInstance.GetRandom(0, validMods.Count) : UnityEngine.Random.Range(0, validMods.Count)];
		return this.CreateScroll(augment, pos);
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x0003993C File Offset: 0x00037B3C
	public ScrollPickup CreateScroll(AugmentTree augment, Vector3 pos)
	{
		Vector3 position = pos + new Vector3(0f, 2.75f, 0f);
		ScrollPickup component = UnityEngine.Object.Instantiate<GameObject>(this.ScrollRewardRef, position, Quaternion.identity).GetComponent<ScrollPickup>();
		component.Setup(augment);
		AudioManager.PlayLoudClipAtPoint(this.ScrollSpawnSFX, pos, 1f, 1f, 1f, 10f, 250f);
		AudioManager.ClipPlayed(this.ScrollSpawnSFX, 0.075f);
		return component;
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x000399BC File Offset: 0x00037BBC
	public RaidScrollTrigger CreatePageSelect(AugmentFilter filter, Vector3 pos)
	{
		pos += new Vector3(0f, 2.75f, 0f);
		RaidScrollTrigger component = UnityEngine.Object.Instantiate<GameObject>(this.PageSelectRewardRef, pos, Quaternion.identity).GetComponent<RaidScrollTrigger>();
		component.HasFilter = true;
		component.Filter = filter;
		AudioManager.PlayLoudClipAtPoint(this.ScrollSpawnSFX, pos, 1f, 1f, 1f, 10f, 250f);
		AudioManager.ClipPlayed(this.ScrollSpawnSFX, 0.075f);
		return component;
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00039A40 File Offset: 0x00037C40
	private int GetPersonalValue(int inputVal)
	{
		if (!PhotonNetwork.InRoom)
		{
			return inputVal;
		}
		int num = 0;
		int playerIndex = PlayerControl.GetPlayerIndex(PlayerControl.myInstance);
		int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
		int wavesCompleted = WaveManager.instance.WavesCompleted;
		for (int i = 0; i < inputVal; i++)
		{
			if ((i + wavesCompleted) % playerCount == playerIndex)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00039A97 File Offset: 0x00037C97
	private void ClearBonusObjective()
	{
		GoalManager.HasObjective = false;
		GoalManager.ObjectiveCompleted = false;
		GoalManager.StartedObjective = false;
		GoalManager.BonusObjective = null;
		GoalManager.InLastChance = false;
		this.gaveEliteReward = false;
		this.LastCancelAttempt = 0f;
		this.LastCompleteAttempt = 0f;
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00039AD4 File Offset: 0x00037CD4
	public void CreatePortals()
	{
		Vector3 a = Fountain.instance.transform.position + Vector3.up * 2f;
		GenreRootNode gameRoot = GameplayManager.instance.GameRoot;
		if (((gameRoot != null) ? gameRoot.Appendix.Count : 0) > 0 && this.CanEndless())
		{
			this.CreatePortal(a - Fountain.instance.transform.right * 5f, PortalType.Endless);
			return;
		}
		this.CreatePortal(a + Fountain.instance.transform.right * 5f, PortalType.Library);
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00039B78 File Offset: 0x00037D78
	public bool CanEndless()
	{
		return !TutorialManager.InTutorial;
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00039B84 File Offset: 0x00037D84
	public BossRewardTrigger CreateBossReward(Vector3 pt, Progression.BossRewardType rewardType, GraphTree rewardItem, int amount = 0)
	{
		return this.CreateBossReward(pt, rewardType, new List<GraphTree>
		{
			rewardItem
		}, amount);
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00039BAC File Offset: 0x00037DAC
	public BossRewardTrigger CreateBossReward(Vector3 pt, Progression.BossRewardType rewardType, List<GraphTree> rewardItems, int amount = 0)
	{
		GameObject gameObject;
		switch (rewardType)
		{
		case Progression.BossRewardType.Tome:
			gameObject = this.TomeRewardRef;
			break;
		case Progression.BossRewardType.Binding:
			gameObject = this.BindingRewardRef;
			break;
		case Progression.BossRewardType.Pages:
			gameObject = this.PageRewardRef;
			break;
		case Progression.BossRewardType.CosmCurrency:
			gameObject = this.CurrencyRewardRef;
			break;
		case Progression.BossRewardType.Quillmarks:
			gameObject = this.QuillmarkRewardRef;
			break;
		case Progression.BossRewardType.Cosmetic:
			gameObject = this.CosmeticRewardRef;
			break;
		case Progression.BossRewardType.BindingsAvailable:
			gameObject = this.BindingRewardRef;
			break;
		default:
			gameObject = this.CurrencyRewardRef;
			break;
		}
		GameObject gameObject2 = gameObject;
		Vector3 position = pt + Vector3.up * 2.1f;
		BossRewardTrigger component = UnityEngine.Object.Instantiate<GameObject>(gameObject2, position, gameObject2.transform.rotation).GetComponent<BossRewardTrigger>();
		component.Rewards = rewardItems;
		component.Currency = amount;
		component.CurType = rewardType;
		this.rewardCollectsNeeded = PlayerControl.PlayerCount;
		return component;
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00039C78 File Offset: 0x00037E78
	public BossRewardTrigger CreateBossReward(Vector3 pt, Progression.BossRewardType rewardType, Cosmetic rewardItem)
	{
		Vector3 position = pt + Vector3.up * 2.1f;
		BossRewardTrigger component = UnityEngine.Object.Instantiate<GameObject>(this.CosmeticRewardRef, position, this.CosmeticRewardRef.transform.rotation).GetComponent<BossRewardTrigger>();
		component.CosmeticReward = rewardItem;
		component.Currency = 0;
		this.rewardCollectsNeeded = PlayerControl.PlayerCount;
		return component;
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00039CD8 File Offset: 0x00037ED8
	public BossRewardTrigger CreateBossReward(Vector3 pt, Progression.BossRewardType rewardType, NookDB.NookObject rewardItem)
	{
		Vector3 position = pt + Vector3.up * 2.1f;
		BossRewardTrigger component = UnityEngine.Object.Instantiate<GameObject>(this.NookItemRewardRef, position, this.CosmeticRewardRef.transform.rotation).GetComponent<BossRewardTrigger>();
		component.NookReward = rewardItem;
		component.Currency = 0;
		component.TrySetup();
		this.rewardCollectsNeeded = PlayerControl.PlayerCount;
		return component;
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00039D3B File Offset: 0x00037F3B
	public void OpenReward(Vector3 pt, Progression.BossRewardType rewardType, List<GraphTree> rewardItems, int currency, string detailOverride)
	{
		BossRewardPanel.Open(rewardType, rewardItems, currency, detailOverride);
		this.CheckFinalReward(pt);
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x00039D4F File Offset: 0x00037F4F
	public void OpenReward(Vector3 pt, Cosmetic c, string detailOverride = "")
	{
		BossRewardPanel.Open(c, detailOverride);
		this.CheckFinalReward(pt);
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00039D5F File Offset: 0x00037F5F
	public void OpenReward(Vector3 pt, NookDB.NookObject c, string detailOverride = "")
	{
		BossRewardPanel.Open(c, detailOverride);
		this.CheckFinalReward(pt);
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00039D70 File Offset: 0x00037F70
	private void CheckFinalReward(Vector3 pt)
	{
		if (MapManager.InLobbyScene)
		{
			return;
		}
		if (RaidManager.IsInRaid)
		{
			RaidManager.instance.CheckLocalReadyToContinueNextEncounter();
			return;
		}
		BossRewardTrigger[] array = UnityEngine.Object.FindObjectsOfType<BossRewardTrigger>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IsAvailable)
			{
				return;
			}
		}
		this.CollectedFinalReward(pt);
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00039DBD File Offset: 0x00037FBD
	private void CollectedFinalReward(Vector3 pt)
	{
		if (TutorialManager.InTutorial)
		{
			this.CreatePortal(pt, PortalType.Library);
		}
		this.view.RPC("CollectedRewardNet", RpcTarget.All, new object[]
		{
			PlayerControl.MyViewID
		});
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00039DF2 File Offset: 0x00037FF2
	[PunRPC]
	private void CollectedRewardNet(int playerID)
	{
		if (!this.CollectedBossRewards.Contains(playerID))
		{
			this.CollectedBossRewards.Add(playerID);
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x00039E0E File Offset: 0x0003800E
	public bool AllCollectedRewards()
	{
		return TutorialManager.InTutorial || this.CollectedBossRewards.Count >= this.rewardCollectsNeeded;
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00039E30 File Offset: 0x00038030
	public static Vector3 FixPointOnNav(Vector3 pt)
	{
		Vector3 vector = AIManager.NearestNavPoint(pt, -1f);
		if (vector.IsValid())
		{
			return vector;
		}
		return pt;
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00039E54 File Offset: 0x00038054
	public void DeactivateAllPortals()
	{
		PortalTrigger[] array = UnityEngine.Object.FindObjectsOfType<PortalTrigger>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Deactivate();
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00039E80 File Offset: 0x00038080
	public void CreatePortal(Vector3 atPoint, PortalType pType)
	{
		GameObject gameObject;
		if (pType != PortalType.Library)
		{
			if (pType != PortalType.Endless)
			{
				gameObject = null;
			}
			else
			{
				gameObject = this.EndlessPortalRef;
			}
		}
		else
		{
			gameObject = this.LibraryPortalRef;
		}
		GameObject gameObject2 = gameObject;
		if (gameObject2 == null)
		{
			return;
		}
		UnityEngine.Object.Instantiate<GameObject>(gameObject2, atPoint, gameObject2.transform.rotation);
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x00039ECC File Offset: 0x000380CC
	public static void SetupNewIncentives()
	{
		List<AbilityTree> list = new List<AbilityTree>();
		AbilityTree ability = GraphDB.GetAbility(GoalManager.AbilityIncentive);
		foreach (UnlockDB.AbilityUnlock abilityUnlock in UnlockDB.DB.Abilities)
		{
			if (!(abilityUnlock.Ability == ability) && !abilityUnlock.Ability.Root.Usage.AbilityMetadata.Locked)
			{
				list.Add(abilityUnlock.Ability);
			}
		}
		GoalManager.SetAbilityIncentive(list[UnityEngine.Random.Range(0, list.Count)]);
		List<GenreTree> list2 = new List<GenreTree>();
		GenreTree genre = GraphDB.GetGenre(GoalManager.TomeIncentive);
		foreach (UnlockDB.GenreUnlock genreUnlock in UnlockDB.DB.Genres)
		{
			if (!(genreUnlock.Genre == genre) && UnlockManager.IsGenreUnlocked(genreUnlock.Genre))
			{
				list2.Add(genreUnlock.Genre);
			}
		}
		if (list2.Count > 0)
		{
			GoalManager.SetTomeIncentive(list2[UnityEngine.Random.Range(0, list2.Count)]);
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0003A01C File Offset: 0x0003821C
	public static void SetAbilityIncentive(AbilityTree ability)
	{
		GoalManager.AbilityIncentive = (((ability != null) ? ability.ID : null) ?? "");
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0003A038 File Offset: 0x00038238
	public static AbilityTree GetAbilityIncentive()
	{
		if (string.IsNullOrEmpty(GoalManager.AbilityIncentive))
		{
			return null;
		}
		return GraphDB.GetAbility(GoalManager.AbilityIncentive);
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0003A052 File Offset: 0x00038252
	public static bool IsIncentiveAbility(AbilityTree ability)
	{
		return !(ability == null) && ability.ID == GoalManager.AbilityIncentive;
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x0003A070 File Offset: 0x00038270
	public static bool IsIncentiveAbilityEquipped()
	{
		return !string.IsNullOrEmpty(GoalManager.AbilityIncentive) && !(PlayerControl.myInstance == null) && (GoalManager.IsIncentiveAbility(PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Primary).AbilityTree) || GoalManager.IsIncentiveAbility(PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Secondary).AbilityTree) || GoalManager.IsIncentiveAbility(PlayerControl.myInstance.actions.GetAbility(PlayerAbilityType.Movement).AbilityTree));
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0003A0F3 File Offset: 0x000382F3
	public static void SetTomeIncentive(GenreTree tome)
	{
		if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		StateManager.SetValue("TomeIncentive", ((tome != null) ? tome.ID : null) ?? "");
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0003A123 File Offset: 0x00038323
	public static bool IsIncentiveTome(GenreTree tome)
	{
		return !(tome == null) && GoalManager.TomeIncentive == tome.ID;
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0003A140 File Offset: 0x00038340
	public static bool IsIncentiveTome(GenreRootNode tome)
	{
		return !(tome == null) && GoalManager.TomeIncentive == tome.guid;
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0003A15D File Offset: 0x0003835D
	public void LibMoteCollectedLocal(Vector3 point)
	{
		this.view.RPC("LibMoteCollectedNetwork", RpcTarget.Others, new object[]
		{
			point
		});
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0003A17F File Offset: 0x0003837F
	[PunRPC]
	private void LibMoteCollectedNetwork(Vector3 point)
	{
		LibraryMoteManager.MoteCollected(point);
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0003A187 File Offset: 0x00038387
	public void LibSpawnMote(Vector3 pt, int typeIndex)
	{
		this.view.RPC("LibMoteSpawnedNetwork", RpcTarget.All, new object[]
		{
			pt,
			typeIndex
		});
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0003A1B2 File Offset: 0x000383B2
	[PunRPC]
	private void LibMoteSpawnedNetwork(Vector3 pt, int typeIndex)
	{
		LibraryMoteManager.SpawnMote(pt, typeIndex);
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0003A1BB File Offset: 0x000383BB
	[PunRPC]
	private void LibSpawnAllMotesNetwork(string motestring)
	{
		LibraryMoteManager.SpawnAllMotes(motestring);
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0003A1C3 File Offset: 0x000383C3
	public void SyncLibraryObjects()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("LibraryEnablingNetwork", RpcTarget.All, new object[]
		{
			LibraryObjectEnabler.GetToggleValues()
		});
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0003A1EC File Offset: 0x000383EC
	[PunRPC]
	private void LibraryEnablingNetwork(string vals)
	{
		List<bool> list = new List<bool>();
		foreach (char c in vals)
		{
			list.Add(c == '1');
		}
		LibraryObjectEnabler libraryObjectEnabler = UnityEngine.Object.FindObjectOfType<LibraryObjectEnabler>();
		if (libraryObjectEnabler != null)
		{
			libraryObjectEnabler.LoadRoomObjects(list);
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0003A23D File Offset: 0x0003843D
	public void LibraryRaceEvent(string type, float value, string data)
	{
		this.view.RPC("LibraryRaceEventNetwork", RpcTarget.All, new object[]
		{
			type,
			value,
			data
		});
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0003A267 File Offset: 0x00038467
	[PunRPC]
	private void LibraryRaceEventNetwork(string type, float value, string data)
	{
		LibraryRaces libraryRaces = LibraryRaces.instance;
		if (libraryRaces == null)
		{
			return;
		}
		libraryRaces.GetNetMessage(type, value, data);
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0003A27C File Offset: 0x0003847C
	public override void OnPlayerEnteredRoom(Player Player)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (MapManager.InLobbyScene)
		{
			this.view.RPC("LibraryEnablingNetwork", Player, new object[]
			{
				LibraryObjectEnabler.GetToggleValues()
			});
			string moteString = LibraryMoteManager.GetMoteString();
			if (string.IsNullOrEmpty(moteString))
			{
				return;
			}
			this.view.RPC("LibSpawnAllMotesNetwork", Player, new object[]
			{
				moteString
			});
		}
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0003A2E1 File Offset: 0x000384E1
	public override void OnPlayerLeftRoom(Player Player)
	{
		if (!this.CollectedBossRewards.Contains(Player.ActorNumber))
		{
			this.rewardCollectsNeeded--;
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0003A304 File Offset: 0x00038504
	public void DebugGoalInfo()
	{
		Debug.Log("Logging Goal Info");
		Progression.TrySpecialRewards(Vector3.zero, GameplayManager.instance.GameGraph, 0);
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0003A325 File Offset: 0x00038525
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0003A327 File Offset: 0x00038527
	public GoalManager()
	{
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x0003A33A File Offset: 0x0003853A
	// Note: this type is marked as 'beforefieldinit'.
	static GoalManager()
	{
	}

	// Token: 0x040006DB RID: 1755
	public GameObject ScrollRewardRef;

	// Token: 0x040006DC RID: 1756
	public GameObject PageSelectRewardRef;

	// Token: 0x040006DD RID: 1757
	public GameObject BindingRewardRef;

	// Token: 0x040006DE RID: 1758
	public GameObject TomeRewardRef;

	// Token: 0x040006DF RID: 1759
	public GameObject PageRewardRef;

	// Token: 0x040006E0 RID: 1760
	public GameObject CurrencyRewardRef;

	// Token: 0x040006E1 RID: 1761
	public GameObject QuillmarkRewardRef;

	// Token: 0x040006E2 RID: 1762
	public GameObject CosmeticRewardRef;

	// Token: 0x040006E3 RID: 1763
	public GameObject NookItemRewardRef;

	// Token: 0x040006E4 RID: 1764
	public GameObject LibraryPortalRef;

	// Token: 0x040006E5 RID: 1765
	public GameObject EndlessPortalRef;

	// Token: 0x040006E6 RID: 1766
	public AudioClip ScrollSpawnSFX;

	// Token: 0x040006E7 RID: 1767
	public AudioClip NoScrollMatchSFX;

	// Token: 0x040006E8 RID: 1768
	public AudioClip BonusStartedSFX;

	// Token: 0x040006E9 RID: 1769
	public AudioClip BonusFailedSFX;

	// Token: 0x040006EA RID: 1770
	public AudioClip BonusCompletedSFX;

	// Token: 0x040006EB RID: 1771
	public GameObject ExitZonePrefab;

	// Token: 0x040006EC RID: 1772
	public const float LAST_CHANCE_DURATION = 15f;

	// Token: 0x040006ED RID: 1773
	public static AugmentTree BonusObjective;

	// Token: 0x040006EE RID: 1774
	public static bool ObjectiveCompleted = false;

	// Token: 0x040006EF RID: 1775
	private static bool DoneObjectiveCompleteEvent = false;

	// Token: 0x040006F0 RID: 1776
	public static bool HasObjective = false;

	// Token: 0x040006F1 RID: 1777
	public static bool StartedObjective = false;

	// Token: 0x040006F2 RID: 1778
	public static bool InLastChance = false;

	// Token: 0x040006F3 RID: 1779
	public static float LastChanceT = 0f;

	// Token: 0x040006F4 RID: 1780
	public static Vector3 ObjectiveLocation = Vector3.zero;

	// Token: 0x040006F5 RID: 1781
	public static string AbilityIncentive = "";

	// Token: 0x040006F6 RID: 1782
	private string lastBonusID;

	// Token: 0x040006F7 RID: 1783
	private bool gaveEliteReward;

	// Token: 0x040006F8 RID: 1784
	private int rewardCollectsNeeded;

	// Token: 0x040006F9 RID: 1785
	private List<int> CollectedBossRewards = new List<int>();

	// Token: 0x040006FA RID: 1786
	public static GoalManager instance;

	// Token: 0x040006FB RID: 1787
	public Action OnBonusStarted;

	// Token: 0x040006FC RID: 1788
	public Action OnBonusCompleted;

	// Token: 0x040006FD RID: 1789
	public Action OnBonusCanceled;

	// Token: 0x040006FE RID: 1790
	private PhotonView view;

	// Token: 0x040006FF RID: 1791
	private float LastCompleteAttempt;

	// Token: 0x04000700 RID: 1792
	private float LastCancelAttempt;
}
