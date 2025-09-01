using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class RaidManager : MonoBehaviourPunCallbacks, IPunObservable
{
	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06000908 RID: 2312 RVA: 0x0003D2C8 File Offset: 0x0003B4C8
	public static bool InEncounterIntro
	{
		get
		{
			RaidManager raidManager = RaidManager.instance;
			return raidManager != null && raidManager.isStartingEncounter;
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06000909 RID: 2313 RVA: 0x0003D2DA File Offset: 0x0003B4DA
	public static RaidDB.RaidType RaidType
	{
		get
		{
			RaidManager raidManager = RaidManager.instance;
			if (raidManager == null)
			{
				return RaidDB.RaidType.Myriad;
			}
			return raidManager.CurrentRaid;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x0600090A RID: 2314 RVA: 0x0003D2EC File Offset: 0x0003B4EC
	public static bool IsHardMode
	{
		get
		{
			RaidManager raidManager = RaidManager.instance;
			return raidManager != null && raidManager.Difficulty == RaidDB.Difficulty.Hard;
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x0600090B RID: 2315 RVA: 0x0003D301 File Offset: 0x0003B501
	private bool AllPlayersReady
	{
		get
		{
			return this.ReadyPlayers.Count >= this.readyNeeded;
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0003D31C File Offset: 0x0003B51C
	private void Awake()
	{
		this.view = base.GetComponent<PhotonView>();
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.OnSceneChanged));
		RaidDB.SetInstance(this.db);
		RaidManager.instance = this;
		RaidManager.IsInRaid = false;
		RaidManager.IsEncounterStarted = false;
		RaidManager.CurrentEncounterCompleted = false;
		RaidScene raidScene = UnityEngine.Object.FindObjectOfType<RaidScene>();
		if (raidScene != null)
		{
			RaidManager.IsInRaid = true;
			AudioManager.instance.ExitCombatMusic();
			if (raidScene.DebugHardMode)
			{
				this.Difficulty = RaidDB.Difficulty.Hard;
			}
			if (!string.IsNullOrEmpty(raidScene.DebugEncounterID))
			{
				this.CurrentRaid = RaidDB.GetRaidFromEncounter(raidScene.DebugEncounterID);
				this.LoadEncounter(raidScene.DebugEncounterID);
				base.Invoke("PreEncounter", 0.5f);
			}
			if (raidScene.AutoStartEncounter)
			{
				base.Invoke("StartEncounter", 1.5f);
			}
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0003D3F8 File Offset: 0x0003B5F8
	private void Update()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			this.UpdateMaster();
		}
		if (!RaidManager.IsInRaid)
		{
			return;
		}
		if (RaidManager.IsEncounterStarted)
		{
			this.UpdateEncounter();
		}
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0003D41C File Offset: 0x0003B61C
	private void UpdateMaster()
	{
		if (MapManager.InLobbyScene && Library_RaidControl.IsPreparing && Library_RaidControl.instance.AllZonesReady())
		{
			this.view.RPC("StartDifficultyVoteNetwork", RpcTarget.All, new object[]
			{
				this.CurrentRaid,
				Library_RaidControl.instance.CanDoHardMode(this.CurrentRaid)
			});
		}
		bool isInRaid = RaidManager.IsInRaid;
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0003D488 File Offset: 0x0003B688
	public void TryPrepareRaid(RaidDB.RaidType raidType)
	{
		Room currentRoom = PhotonNetwork.CurrentRoom;
		if (((currentRoom != null) ? currentRoom.PlayerCount : 1) <= 1)
		{
			this.view.RPC("TryStartDifficulty", RpcTarget.MasterClient, new object[]
			{
				(int)raidType
			});
			return;
		}
		this.view.RPC("TryStartingZones", RpcTarget.MasterClient, new object[]
		{
			(int)raidType
		});
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0003D4EA File Offset: 0x0003B6EA
	public void PrepareReadyUpForPlayer(Player player)
	{
		this.view.RPC("RaidReadyUpZonesNetwork", player, new object[]
		{
			this.CurrentRaid
		});
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0003D511 File Offset: 0x0003B711
	public void CancelRaidPrep()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("CancelRaidPrepNetwork", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0003D531 File Offset: 0x0003B731
	[PunRPC]
	private void CancelRaidPrepNetwork()
	{
		Library_RaidControl.instance.CancelPreparation();
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0003D540 File Offset: 0x0003B740
	[PunRPC]
	public void TryStartingZones(RaidDB.RaidType raidType)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (Library_RaidControl.IsPreparing)
		{
			return;
		}
		if (GameplayManager.instance.CurrentState == GameState.Hub_ReadyUp)
		{
			GameplayManager.instance.UpdateGameState(GameState.Hub);
		}
		this.view.RPC("RaidReadyUpZonesNetwork", RpcTarget.All, new object[]
		{
			raidType
		});
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x0003D596 File Offset: 0x0003B796
	[PunRPC]
	private void RaidReadyUpZonesNetwork(RaidDB.RaidType raidType)
	{
		if (this.CurrentRaid != raidType)
		{
			Library_RaidControl.instance.CancelPreparation();
		}
		this.CurrentRaid = raidType;
		Library_RaidControl.instance.RaidZonePrepare();
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0003D5BC File Offset: 0x0003B7BC
	[PunRPC]
	public void TryStartDifficulty(RaidDB.RaidType raidType)
	{
		if (GameplayManager.CurState == GameState.Hub_Bindings)
		{
			return;
		}
		this.view.RPC("StartDifficultyVoteNetwork", RpcTarget.All, new object[]
		{
			raidType,
			Library_RaidControl.instance.CanDoHardMode(raidType)
		});
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0003D5FB File Offset: 0x0003B7FB
	[PunRPC]
	private void StartDifficultyVoteNetwork(RaidDB.RaidType raidType, bool hardmodeAllowed)
	{
		UnityEngine.Debug.Log("Starting Raid Difficulty Vote - Can Hardmode: " + hardmodeAllowed.ToString());
		this.CurrentRaid = raidType;
		VoteManager.StartVote(ChoiceType.RaidDifficulty);
		Library_RaidControl.instance.CancelPreparation();
		RaidDifficultyUI.instance.AllowHardMode(hardmodeAllowed);
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0003D635 File Offset: 0x0003B835
	public void RaidDifficultySelected()
	{
		Library_RaidControl.instance.PrepareTraveling(this.CurrentRaid);
		UnityMainThreadDispatcher.Instance().Invoke(new Action(this.StartRaid), 2f);
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x0003D664 File Offset: 0x0003B864
	private void StartRaid()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.AttemptCounts.Clear();
		this.CurrentEncounterIndex = 0;
		RaidManager.CurrentEncounterCompleted = false;
		RaidDB.Encounter encounter = RaidDB.GetRaid(this.CurrentRaid).Encounters[this.CurrentEncounterIndex];
		this.LoadEncounter(encounter.ID);
		MapManager.instance.ChangeLevelSequence(encounter.SceneID);
		RaidManager.IsInRaid = true;
		if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
		{
			PhotonNetwork.CurrentRoom.IsOpen = false;
		}
		PlatformSetup.ClearSteamRoomCode();
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0003D6F0 File Offset: 0x0003B8F0
	public void PortalInteraction(PortalType portalType)
	{
		UnityEngine.Debug.Log("Portal Interaction - " + this.ReadyPlayers.Count.ToString() + "/" + this.readyNeeded.ToString());
		if (!this.AllPlayersReady)
		{
			InfoDisplay.SetText("Scribes have outstanding rewards!", 2f, InfoArea.DetailBottom);
			return;
		}
		this.DeactivatePortals();
		this.view.RPC("PortalChosenNetwork", RpcTarget.MasterClient, new object[]
		{
			(int)portalType
		});
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0003D770 File Offset: 0x0003B970
	[PunRPC]
	private void PortalChosenNetwork(int value)
	{
		if (!PhotonNetwork.IsMasterClient || this.isGoingToNextEncounter)
		{
			return;
		}
		this.isGoingToNextEncounter = true;
		if (!RaidDB.IsFinalEncounter(this.CurrentRaid, this.CurrentEncounter))
		{
			this.view.RPC("GoToNextEncounterNetwork", RpcTarget.All, Array.Empty<object>());
		}
		else
		{
			this.view.RPC("RaidCompletedNetwork", RpcTarget.All, Array.Empty<object>());
		}
		this.view.RPC("DeactivatePortals", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x0003D7EB File Offset: 0x0003B9EB
	[PunRPC]
	private void DeactivatePortals()
	{
		GoalManager.instance.DeactivateAllPortals();
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x0003D7F8 File Offset: 0x0003B9F8
	[PunRPC]
	public void GoToNextEncounterNetwork()
	{
		this.CurrentEncounterIndex++;
		this.CurrentEnemyAugment = null;
		RaidManager.CurrentEncounterCompleted = false;
		this.CurrentAttempts = 1;
		RaidDB.Encounter encounter = RaidDB.GetRaid(this.CurrentRaid).Encounters[this.CurrentEncounterIndex];
		this.LoadEncounter(encounter.ID);
		if (PhotonNetwork.IsMasterClient)
		{
			MapManager.instance.ChangeLevelSequence(encounter.SceneID);
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0003D868 File Offset: 0x0003BA68
	[PunRPC]
	public void RaidFailedNetwork()
	{
		RaidScene raidScene = RaidScene.instance;
		if (raidScene != null)
		{
			raidScene.StartTrigger.Deactivate();
		}
		EndGameAnim.Display(false);
		this.EndOfRaidStats(false);
		PlayerControl.myInstance.TriggerSnippets(EventTrigger.Game_Lost, null, 1f);
		Progression.RequestRaidCurrencyReward(Vector3.zero, this.CurrentEncounterIndex, this.Difficulty, this.CurrentRaid, false);
		RaidRecord.UploadResult(RaidRecord.Result.Lost, this.currentEncounterTime);
		UnityMainThreadDispatcher.Instance().Invoke(delegate
		{
			PostGamePanel.instance.EndedRaid(false);
		}, 1.5f);
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0003D900 File Offset: 0x0003BB00
	[PunRPC]
	public void RaidCompletedNetwork()
	{
		this.EndOfRaidStats(true);
		UnityMainThreadDispatcher.Instance().Invoke(delegate
		{
			PostGamePanel.instance.EndedRaid(true);
		}, 1f);
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x0003D937 File Offset: 0x0003BB37
	private void EndOfRaidStats(bool didWin)
	{
		PlayerControl.myInstance.Net.SendStats();
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x0003D948 File Offset: 0x0003BB48
	private void SaveStickerData()
	{
		List<string> list = new List<string>();
		foreach (string item in PlayerControl.myInstance.Augment.TreeIDs)
		{
			list.Add(item);
		}
		PlayerActions actions = PlayerControl.myInstance.actions;
		list.Add(actions.primary.ID);
		list.Add(actions.secondary.ID);
		list.Add(actions.movement.ID);
		list.Add(actions.core.ID);
		GameStats.RaidStickerType sticker;
		switch (this.CurrentRaid)
		{
		case RaidDB.RaidType.Myriad:
			sticker = (RaidManager.IsHardMode ? GameStats.RaidStickerType.Raving_Hard : GameStats.RaidStickerType.Raving);
			break;
		case RaidDB.RaidType.Verse:
			sticker = (RaidManager.IsHardMode ? GameStats.RaidStickerType.Splice_Hard : GameStats.RaidStickerType.Splice);
			break;
		case RaidDB.RaidType.Horizon:
			sticker = (RaidManager.IsHardMode ? GameStats.RaidStickerType.Tangent_Hard : GameStats.RaidStickerType.Tangent);
			break;
		default:
			sticker = GameStats.RaidStickerType.Raving;
			break;
		}
		GameStats.AddStickerProgress(sticker, list);
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x0003DA50 File Offset: 0x0003BC50
	public void LoadEncounter(string id)
	{
		this.view.RPC("SetEncounterNetwork", RpcTarget.All, new object[]
		{
			id
		});
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x0003DA70 File Offset: 0x0003BC70
	[PunRPC]
	private void SetEncounterNetwork(string id)
	{
		UnityEngine.Debug.Log("Set Encounter: " + id + " - " + this.Difficulty.ToString());
		this.CurrentEncounter = id;
		RaidManager.PreparedEncounter = RaidDB.GetEncounter(this.CurrentRaid, this.CurrentEncounter);
		RaidRecord.NewRecord(this.CurrentEncounter, this.Difficulty == RaidDB.Difficulty.Hard);
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0003DAD4 File Offset: 0x0003BCD4
	public void PreEncounter()
	{
		if (RaidDB.GetEncounter(this.CurrentRaid, this.CurrentEncounter) == null)
		{
			UnityEngine.Debug.LogError("Encounter not found: " + this.CurrentEncounter);
			return;
		}
		if (RaidScene.instance != null && RaidScene.instance.IsFirstEncounter)
		{
			base.StartCoroutine("WaitForFirstReward");
			return;
		}
		this.PrepareEncounter();
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0003DB36 File Offset: 0x0003BD36
	private IEnumerator WaitForFirstReward()
	{
		this.readyNeeded = PlayerControl.PlayerCount;
		while (!this.AllPlayersReady)
		{
			yield return true;
		}
		if (PhotonNetwork.IsMasterClient)
		{
			this.view.RPC("AllFirstRewardNetwork", RpcTarget.All, Array.Empty<object>());
		}
		this.PrepareEncounter();
		yield break;
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0003DB45 File Offset: 0x0003BD45
	[PunRPC]
	private void AllFirstRewardNetwork()
	{
		RaidScene.instance.OnAllFirstReward.Invoke();
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0003DB58 File Offset: 0x0003BD58
	public void PrepareEncounter()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		UnityEngine.Debug.Log("Preparing Encounter: " + this.CurrentEncounter);
		RaidDB.Encounter encounter = RaidDB.GetEncounter(this.CurrentRaid, this.CurrentEncounter);
		this.OngoingEncounter = encounter;
		if (encounter == null || RaidManager.CurrentEncounterCompleted)
		{
			return;
		}
		switch (encounter.Type)
		{
		case RaidDB.EncounterType.Boss:
		{
			GameObject boss = encounter.Boss;
			Transform bossSpawn = RaidScene.instance.BossSpawn;
			EntityControl entityControl = AIManager.SpawnAIExplicit(AIData.AIDetails.GetResourcePath(boss), bossSpawn.position, bossSpawn.forward);
			this.wasBossAffectable = entityControl.Affectable;
			this.wasBossTargetable = entityControl.Targetable;
			entityControl.GetComponent<AINetworked>().ToggleTargetable(false, false);
			if (this.CurrentEnemyAugment != null)
			{
				entityControl.GetComponent<AINetworked>().AddAugment(this.CurrentEnemyAugment.ID);
			}
			break;
		}
		}
		this.view.RPC("EncounterPreparedNetwork", RpcTarget.All, new object[]
		{
			this.wasBossAffectable
		});
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x0003DC58 File Offset: 0x0003BE58
	[PunRPC]
	private void EncounterPreparedNetwork(bool bossStartAffectable)
	{
		RaidManager.CanStartEncounter = true;
		RaidManager.PreparedEncounter = RaidDB.GetEncounter(this.CurrentRaid, this.CurrentEncounter);
		Action onEncounterPrepared = RaidManager.OnEncounterPrepared;
		if (onEncounterPrepared != null)
		{
			onEncounterPrepared();
		}
		RaidScene raidScene = RaidScene.instance;
		if (raidScene != null)
		{
			raidScene.StartTrigger.Reset();
		}
		this.wasBossAffectable = bossStartAffectable;
		if (VignetteControl.instance != null)
		{
			VignetteControl.instance.StartVignette();
			this.AttemptCounts.Add(1);
		}
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x0003DCD0 File Offset: 0x0003BED0
	public void StartEncounter()
	{
		if (RaidManager.IsEncounterStarted || this.isStartingEncounter || !PhotonNetwork.IsMasterClient || RaidManager.CurrentEncounterCompleted)
		{
			return;
		}
		this.view.RPC("StartEncounterNetwork", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0003DD08 File Offset: 0x0003BF08
	[PunRPC]
	private void StartEncounterNetwork()
	{
		if (RaidManager.IsEncounterStarted || this.isStartingEncounter || !RaidManager.CanStartEncounter)
		{
			return;
		}
		RaidRecord.NextAttempt(this.currentEncounterTime);
		this.currentEncounterTime = 0f;
		if (this.hasTriedEncounter)
		{
			this.ForcePlayerInside();
			AudioManager.instance.EnterCombatMusic(true, 0.5f);
			this.EncounterStarted();
			return;
		}
		base.StartCoroutine("StartEncounterSequence");
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x0003DD73 File Offset: 0x0003BF73
	private IEnumerator StartEncounterSequence()
	{
		this.hasTriedEncounter = true;
		this.isStartingEncounter = true;
		RaidScene.instance.EncounterStartSequence.Invoke();
		RaidDB.Encounter encounter = RaidDB.GetEncounter(this.CurrentRaid, this.CurrentEncounter);
		if (encounter != null && encounter.Type == RaidDB.EncounterType.Boss && encounter.DoIntroAnimation)
		{
			yield return base.StartCoroutine("RaidBossIntroSequence", encounter);
		}
		yield return true;
		this.isStartingEncounter = false;
		this.EncounterStarted();
		yield break;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0003DD82 File Offset: 0x0003BF82
	private IEnumerator RaidBossIntroSequence(RaidDB.Encounter encounter)
	{
		PlayerControl.myInstance.Input.OverrideCamera(RaidScene.instance.BossCameraPoint, 6f, true);
		UnityMainThreadDispatcher.Instance().Invoke(new Action(encounter.DoBossIntro), encounter.StartDelay);
		RaidBossIntroUI.instance.Setup(encounter.BossName, encounter.BossSubname);
		yield return new WaitForSeconds(0.5f);
		float t = 0f;
		while (t < 1f)
		{
			t += Time.unscaledDeltaTime / this.IntroSlowDuration;
			Time.timeScale = this.IntroTimeCurve.Evaluate(t);
			yield return true;
		}
		Time.timeScale = 0f;
		if (encounter.StartWithEnemyPage)
		{
			if (PhotonNetwork.IsMasterClient)
			{
				VoteManager.PrepareVote(ModType.Enemy);
			}
			yield return new WaitForSecondsRealtime(3f);
			while (VoteManager.IsVoting)
			{
				yield return true;
			}
		}
		AudioManager.instance.EnterCombatMusic(true, 1.25f);
		yield return new WaitForSecondsRealtime(1f);
		t = 0f;
		while (t < 1f)
		{
			t += Time.unscaledDeltaTime * 1f;
			Time.timeScale = this.IntroTimeCurve.Evaluate(1f - t);
			yield return true;
		}
		Time.timeScale = 1f;
		this.ForcePlayerInside();
		PlayerControl.myInstance.Input.ReturnCamera(6f, false);
		yield return new WaitForSeconds(0.75f);
		yield break;
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0003DD98 File Offset: 0x0003BF98
	private void ForcePlayerInside()
	{
		if (RaidScene.instance == null)
		{
			return;
		}
		RaidScene.instance.TryForcePlayerInside();
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0003DDB4 File Offset: 0x0003BFB4
	private void EncounterStarted()
	{
		UnityEngine.Debug.Log("Raid Encounter Started: " + this.CurrentEncounter);
		RaidManager.IsEncounterStarted = true;
		this.isStartingEncounter = false;
		RaidManager.PlayerLeftEncounter = false;
		this.OngoingEncounter = RaidDB.GetEncounter(this.CurrentRaid, this.CurrentEncounter);
		AIControl boss = AIManager.GetBoss();
		if (boss != null)
		{
			if (PhotonNetwork.IsMasterClient)
			{
				boss.Net.ToggleTargetable(this.wasBossTargetable, this.wasBossAffectable);
			}
			boss.ResetAllCooldowns();
		}
		Action onEncounterStarted = RaidManager.OnEncounterStarted;
		if (onEncounterStarted != null)
		{
			onEncounterStarted();
		}
		GameStats.IncrementRaidStat(this.OngoingEncounter.ID, (this.Difficulty == RaidDB.Difficulty.Normal) ? GameStats.RaidStat.Attempts : GameStats.RaidStat.HardMode_Attempts, 1, false);
		AIManager.instance.TriggerAugmentsOnAllEnemies(EventTrigger.RaidEncounterStarted);
		PlayerControl.myInstance.TriggerSnippets(EventTrigger.RaidEncounterStarted, null, 1f);
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x0003DE80 File Offset: 0x0003C080
	private void UpdateEncounter()
	{
		if (this.isEncounterCompleted())
		{
			Vector3 input = GameplayManager.lastBossPoint;
			if (RaidScene.instance != null && RaidScene.instance.EndOverridePoint != null)
			{
				input = RaidScene.instance.EndOverridePoint.position;
			}
			this.CompleteEncounter(AIManager.NearestNavPoint(input, -1f));
			return;
		}
		this.currentEncounterTime += GameplayManager.deltaTime;
		if (PlayerControl.PlayerCount > 1 && PlayerControl.DeadPlayersCount >= PlayerControl.PlayerCount)
		{
			bool flag = false;
			using (List<PlayerControl>.Enumerator enumerator = PlayerControl.AllPlayers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Health.GroupRezTimer > 0f)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				this.EncounterFailed();
			}
		}
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x0003DF60 File Offset: 0x0003C160
	public void BossDied(AIControl entity)
	{
		UnityEngine.Debug.Log("Raid Boss " + entity.DisplayName + " Died");
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0003DF7C File Offset: 0x0003C17C
	private bool isEncounterCompleted()
	{
		if (this.OngoingEncounter == null || !RaidManager.IsEncounterStarted)
		{
			return false;
		}
		switch (this.OngoingEncounter.Type)
		{
		case RaidDB.EncounterType.Boss:
		{
			AIControl boss = AIManager.GetBoss();
			return boss == null || boss.IsDead;
		}
		case RaidDB.EncounterType.CombatEvent:
			return false;
		case RaidDB.EncounterType.Vignette:
			return false;
		default:
			return false;
		}
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0003DFD8 File Offset: 0x0003C1D8
	private void CompleteEncounter(Vector3 point)
	{
		if (!PhotonNetwork.IsMasterClient || !RaidManager.IsEncounterStarted)
		{
			return;
		}
		UnityEngine.Debug.Log("Raid Encounter Completed Successfully! - In Encounter: " + RaidManager.IsEncounterStarted.ToString());
		this.view.RPC("EncounterCompletedNetwork", RpcTarget.All, new object[]
		{
			point
		});
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x0003E030 File Offset: 0x0003C230
	[PunRPC]
	public void EncounterCompletedNetwork(Vector3 point)
	{
		RaidDB.Encounter encounter = RaidDB.GetEncounter(this.CurrentRaid, this.CurrentEncounter);
		EndGameAnim.Display(true);
		RaidManager.CurrentEncounterCompleted = true;
		this.AttemptCounts.Add(this.CurrentAttempts);
		Action onEncounterEnded = RaidManager.OnEncounterEnded;
		if (onEncounterEnded != null)
		{
			onEncounterEnded();
		}
		AIManager.KillAllEnemies();
		RaidManager.IsEncounterStarted = false;
		AudioManager.instance.ExitCombatMusic();
		GameplayManager.instance.ClearWorldEffects();
		GameStats.IncrementRaidStat(this.CurrentEncounter, (this.Difficulty == RaidDB.Difficulty.Normal) ? GameStats.RaidStat.Completed : GameStats.RaidStat.HardMode_Completed, 1, false);
		if (RaidDB.IsFinalEncounter(this.CurrentRaid, this.CurrentEncounter))
		{
			this.SaveStickerData();
		}
		Progression.RequestRaidCurrencyReward(point, this.CurrentEncounterIndex, this.Difficulty, this.CurrentRaid, true);
		Progression.RaidEncounterReward(point, encounter, this.Difficulty, this.CurrentRaid);
		RaidRecord.UploadResult(RaidRecord.Result.Won, this.currentEncounterTime);
		this.currentEncounterTime = 0f;
		if (PlayerControl.myInstance.IsDead)
		{
			PlayerControl.myInstance.TryRespawn();
		}
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance != null)
		{
			myInstance.TriggerSnippets(EventTrigger.RaidEncounterCompleted, null, 1f);
		}
		PlayerControl myInstance2 = PlayerControl.myInstance;
		if (myInstance2 != null)
		{
			myInstance2.Health.Heal(new DamageInfo(PlayerControl.myInstance.Health.MaxHealth));
		}
		PlayerChoicePanel.ChoiceTotal = 0;
		if (encounter.HasPageReward)
		{
			Vector3 position = GoalManager.FixPointOnNav(point + UnityEngine.Random.insideUnitSphere * 4f) + Vector3.up * 2.1f;
			RaidScrollTrigger component = UnityEngine.Object.Instantiate<GameObject>(this.RaidScrollPrefab, position, this.RaidScrollPrefab.transform.rotation).GetComponent<RaidScrollTrigger>();
			component.HasFilter = true;
			component.Filter = encounter.RewardFilter;
		}
		this.readyNeeded = PlayerControl.PlayerCount;
		this.ReadyPlayers.Clear();
		UnityEngine.Debug.Log("Ready Needed: " + this.readyNeeded.ToString());
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x0003E208 File Offset: 0x0003C408
	public void EncounterFailed()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		UnityEngine.Debug.Log("Raid Encounter Failed");
		RaidManager.IsEncounterStarted = false;
		this.view.RPC("EncounterFailedNetwork", RpcTarget.All, Array.Empty<object>());
		this.view.RPC("ResetEncounterNetwork", RpcTarget.All, new object[]
		{
			true
		});
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x0003E264 File Offset: 0x0003C464
	[PunRPC]
	private void EncounterFailedNetwork()
	{
		RaidManager.IsEncounterStarted = false;
		RaidManager.CanStartEncounter = false;
		this.CurrentAttempts++;
		RaidScene raidScene = RaidScene.instance;
		if (raidScene != null)
		{
			raidScene.StartTrigger.Deactivate();
		}
		if (RaidScene.instance != null && RaidScene.instance.RaidCodex != null)
		{
			RaidScene.instance.RaidCodex.SetActive(true);
		}
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0003E2D0 File Offset: 0x0003C4D0
	[PunRPC]
	private void ResetEncounterNetwork(bool shouldPrepare)
	{
		int b = PlayerControl.AllPlayers.IndexOf(PlayerControl.myInstance);
		List<SpawnPoint> allSpawns = SpawnPoint.GetAllSpawns(SpawnType.Player, EnemyLevel.None);
		SpawnPoint spawnPoint = allSpawns[Mathf.Min(allSpawns.Count - 1, b)];
		PlayerControl.myInstance.Respawn(spawnPoint.transform, 1f);
		PlayerControl.myInstance.Mana.Recharge(10f);
		PlayerControl.myInstance.actions.ResetCooldown(PlayerAbilityType.Movement, true);
		PlayerControl.myInstance.actions.ResetCooldown(PlayerAbilityType.Utility, true);
		PlayerControl.myInstance.TriggerSnippets(EventTrigger.RaidEncounterReset, null, 1f);
		RaidScene raidScene = RaidScene.instance;
		if (raidScene != null)
		{
			raidScene.StartTrigger.Deactivate();
		}
		AIManager.ClearAllEnemies(true);
		VFXSpawnRef.ClearNonPlayer();
		DelayEffectNode.CleanDelayList();
		GameplayManager.instance.ClearWorldEffects();
		RaidManager.IsEncounterStarted = false;
		RaidManager.CanStartEncounter = false;
		Action onEncounterReset = RaidManager.OnEncounterReset;
		if (onEncounterReset != null)
		{
			onEncounterReset();
		}
		AudioManager.instance.ExitCombatMusic();
		UnityEngine.Debug.Log("Raid Encounter Reset");
		if (shouldPrepare && GameplayManager.CurState != GameState.Post_Traveling)
		{
			base.Invoke("PrepareEncounter", 1f);
		}
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x0003E3E0 File Offset: 0x0003C5E0
	public void VignetteComplete()
	{
		VignetteControl.ClearZones();
		VignetteControl.instance.EndCleanup();
		VignetteInfoDisplay.instance.Release();
		UnityMainThreadDispatcher.Instance().Invoke(new Action(this.LeaveVignette), 1f);
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x0003E416 File Offset: 0x0003C616
	private void LeaveVignette()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("GoToNextEncounterNetwork", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0003E438 File Offset: 0x0003C638
	public static List<AugmentTree> GetRaidEnemyOptions()
	{
		List<AugmentTree> list = RaidDB.GetEncounter(RaidManager.instance.CurrentRaid, RaidManager.instance.CurrentEncounter).BossAugments.Clone<AugmentTree>();
		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (AIManager.AugmentIDs.Contains(list[i].ID))
			{
				list.RemoveAt(i);
			}
		}
		List<AugmentTree> list2 = new List<AugmentTree>();
		for (int j = 0; j < 3; j++)
		{
			AugmentTree augmentTree = GraphDB.ChooseModFromList(ModType.Enemy, list, false, false);
			if (!(augmentTree == null))
			{
				list.Remove(augmentTree);
				list2.Add(augmentTree);
			}
		}
		return list2;
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0003E4D4 File Offset: 0x0003C6D4
	public static void OnEnemyPageSelected(AugmentTree mod)
	{
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			AIControl aicontrol = entityControl as AIControl;
			if (aicontrol != null && mod.Root.ApplyTo.HasFlag(aicontrol.Level) && (mod.Root.ApplyType == EnemyType.Any || aicontrol.EnemyIsType(mod.Root.ApplyType)))
			{
				aicontrol.AddAugment(mod, 1);
			}
		}
		RaidManager.instance.CurrentEnemyAugment = mod;
		RaidRecord.PageSelected(mod.Root.Name);
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0003E58C File Offset: 0x0003C78C
	public void AwardPlayerPages(AugmentFilter filter = null)
	{
		List<AugmentTree> list = new List<AugmentTree>();
		List<AugmentTree> list2 = (filter == null) ? GraphDB.GetValidMods(ModType.Player) : filter.GetModifiers(ModType.Player, null);
		int num = Mathf.Min(3, list2.Count);
		for (int i = 0; i < num; i++)
		{
			AugmentTree item = GraphDB.ChooseModFromList(ModType.Player, list2, false, false);
			list2.Remove(item);
			list.Add(item);
		}
		AugmentsPanel.AwardUpgradeChoice(list);
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
	public void CheckLocalReadyToContinueNextEncounter()
	{
		BossRewardTrigger[] array = UnityEngine.Object.FindObjectsOfType<BossRewardTrigger>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IsAvailable)
			{
				return;
			}
		}
		RaidScrollTrigger[] array2 = UnityEngine.Object.FindObjectsOfType<RaidScrollTrigger>();
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].IsAvailable)
			{
				return;
			}
		}
		if (AugmentsPanel.UpgradesAvailable > 0 || PlayerChoicePanel.instance.HasChoices)
		{
			return;
		}
		this.view.RPC("ReadyToContinueNetwork", RpcTarget.All, new object[]
		{
			PlayerControl.MyViewID
		});
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x0003E674 File Offset: 0x0003C874
	[PunRPC]
	private void ReadyToContinueNetwork(int playerID)
	{
		if (!this.ReadyPlayers.Contains(playerID))
		{
			this.ReadyPlayers.Add(playerID);
		}
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x0003E690 File Offset: 0x0003C890
	public void TryTriggerSceneEvent(string id, bool localOnly)
	{
		if (localOnly && RaidScene.CanTriggerEvent(id))
		{
			RaidScene.TriggerSceneEvent(id);
			return;
		}
		this.view.RPC("TryTriggerSceneEventNetwork", RpcTarget.MasterClient, new object[]
		{
			id
		});
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x0003E6BF File Offset: 0x0003C8BF
	[PunRPC]
	private void TryTriggerSceneEventNetwork(string id)
	{
		if (RaidScene.CanTriggerEvent(id))
		{
			this.view.RPC("TriggerSceneEventNetwork", RpcTarget.All, new object[]
			{
				id
			});
		}
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0003E6E4 File Offset: 0x0003C8E4
	[PunRPC]
	private void TriggerSceneEventNetwork(string id)
	{
		RaidScene.TriggerSceneEvent(id);
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x0003E6EC File Offset: 0x0003C8EC
	private void OnSceneChanged()
	{
		this.isGoingToNextEncounter = false;
		RaidManager.PlayerLeftEncounter = false;
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.ReadyPlayers.Clear();
		if (MapManager.InLobbyScene)
		{
			base.StopAllCoroutines();
			this.CurrentEnemyAugment = null;
			RaidManager.IsInRaid = false;
			if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient)
			{
				PhotonNetwork.CurrentRoom.IsOpen = true;
			}
			if (PlayerControl.myInstance != null && Library_RaidControl.instance != null)
			{
				Library_RaidControl.instance.CheckRaidAccess();
				return;
			}
		}
		else if (RaidManager.IsInRaid)
		{
			this.hasTriedEncounter = false;
			GameplayManager.instance.UpdateGameState(GameState.IN_RAID);
			base.StopAllCoroutines();
			base.StartCoroutine(this.WaitForAllPlayersToStart());
		}
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0003E79E File Offset: 0x0003C99E
	private IEnumerator WaitForAllPlayersToStart()
	{
		yield return new WaitForSeconds(0.3f);
		while (!MapManager.AllPlayersLoaded)
		{
			yield return true;
		}
		this.PreEncounter();
		yield break;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x0003E7AD File Offset: 0x0003C9AD
	public override void OnPlayerLeftRoom(Player Player)
	{
		if (!this.ReadyPlayers.Contains(Player.ActorNumber))
		{
			this.readyNeeded--;
		}
		if (RaidManager.IsInRaid)
		{
			RaidManager.PlayerLeftEncounter = true;
		}
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0003E7E0 File Offset: 0x0003C9E0
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(RaidManager.IsInRaid);
			stream.SendNext(RaidManager.IsEncounterStarted);
			stream.SendNext(this.hasTriedEncounter);
			stream.SendNext((int)this.CurrentRaid);
			stream.SendNext(this.CurrentEncounterIndex);
			return;
		}
		RaidManager.IsInRaid = (bool)stream.ReceiveNext();
		RaidManager.IsEncounterStarted = (bool)stream.ReceiveNext();
		this.hasTriedEncounter = (bool)stream.ReceiveNext();
		this.CurrentRaid = (RaidDB.RaidType)((int)stream.ReceiveNext());
		this.CurrentEncounterIndex = (int)stream.ReceiveNext();
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0003E89C File Offset: 0x0003CA9C
	private void OnDestroy()
	{
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.OnSceneChanged));
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x0003E8BE File Offset: 0x0003CABE
	public RaidManager()
	{
	}

	// Token: 0x04000797 RID: 1943
	public static RaidManager instance;

	// Token: 0x04000798 RID: 1944
	public RaidDB db;

	// Token: 0x04000799 RID: 1945
	public GameObject RaidScrollPrefab;

	// Token: 0x0400079A RID: 1946
	public float IntroSlowDuration = 1f;

	// Token: 0x0400079B RID: 1947
	public AnimationCurve IntroTimeCurve;

	// Token: 0x0400079C RID: 1948
	public RaidDB.RaidType CurrentRaid;

	// Token: 0x0400079D RID: 1949
	public RaidDB.Difficulty Difficulty;

	// Token: 0x0400079E RID: 1950
	public string CurrentEncounter;

	// Token: 0x0400079F RID: 1951
	public int CurrentEncounterIndex;

	// Token: 0x040007A0 RID: 1952
	private PhotonView view;

	// Token: 0x040007A1 RID: 1953
	[NonSerialized]
	public List<int> AttemptCounts = new List<int>();

	// Token: 0x040007A2 RID: 1954
	public static bool IsInRaid;

	// Token: 0x040007A3 RID: 1955
	public static bool IsEncounterStarted;

	// Token: 0x040007A4 RID: 1956
	public static bool CanStartEncounter;

	// Token: 0x040007A5 RID: 1957
	public static bool CurrentEncounterCompleted;

	// Token: 0x040007A6 RID: 1958
	public static bool PlayerLeftEncounter;

	// Token: 0x040007A7 RID: 1959
	private bool hasTriedEncounter;

	// Token: 0x040007A8 RID: 1960
	private bool isStartingEncounter;

	// Token: 0x040007A9 RID: 1961
	public static RaidDB.Encounter PreparedEncounter;

	// Token: 0x040007AA RID: 1962
	private RaidDB.Encounter OngoingEncounter;

	// Token: 0x040007AB RID: 1963
	[NonSerialized]
	public float currentEncounterTime;

	// Token: 0x040007AC RID: 1964
	[NonSerialized]
	public int CurrentAttempts;

	// Token: 0x040007AD RID: 1965
	public AugmentTree CurrentEnemyAugment;

	// Token: 0x040007AE RID: 1966
	private int readyNeeded;

	// Token: 0x040007AF RID: 1967
	private List<int> ReadyPlayers = new List<int>();

	// Token: 0x040007B0 RID: 1968
	private bool isGoingToNextEncounter;

	// Token: 0x040007B1 RID: 1969
	public static Action OnEncounterPrepared;

	// Token: 0x040007B2 RID: 1970
	public static Action OnEncounterStarted;

	// Token: 0x040007B3 RID: 1971
	public static Action OnEncounterReset;

	// Token: 0x040007B4 RID: 1972
	public static Action OnEncounterEnded;

	// Token: 0x040007B5 RID: 1973
	private bool wasBossAffectable = true;

	// Token: 0x040007B6 RID: 1974
	private bool wasBossTargetable = true;

	// Token: 0x020004BD RID: 1213
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600228D RID: 8845 RVA: 0x000C717C File Offset: 0x000C537C
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000C7188 File Offset: 0x000C5388
		public <>c()
		{
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000C7190 File Offset: 0x000C5390
		internal void <RaidFailedNetwork>b__55_0()
		{
			PostGamePanel.instance.EndedRaid(false);
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000C719D File Offset: 0x000C539D
		internal void <RaidCompletedNetwork>b__56_0()
		{
			PostGamePanel.instance.EndedRaid(true);
		}

		// Token: 0x04002433 RID: 9267
		public static readonly RaidManager.<>c <>9 = new RaidManager.<>c();

		// Token: 0x04002434 RID: 9268
		public static Action <>9__55_0;

		// Token: 0x04002435 RID: 9269
		public static Action <>9__56_0;
	}

	// Token: 0x020004BE RID: 1214
	[CompilerGenerated]
	private sealed class <WaitForFirstReward>d__62 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002291 RID: 8849 RVA: 0x000C71AA File Offset: 0x000C53AA
		[DebuggerHidden]
		public <WaitForFirstReward>d__62(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000C71B9 File Offset: 0x000C53B9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000C71BC File Offset: 0x000C53BC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			RaidManager raidManager = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				raidManager.readyNeeded = PlayerControl.PlayerCount;
			}
			if (raidManager.AllPlayersReady)
			{
				if (PhotonNetwork.IsMasterClient)
				{
					raidManager.view.RPC("AllFirstRewardNetwork", RpcTarget.All, Array.Empty<object>());
				}
				raidManager.PrepareEncounter();
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x000C723C File Offset: 0x000C543C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000C7244 File Offset: 0x000C5444
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x000C724B File Offset: 0x000C544B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002436 RID: 9270
		private int <>1__state;

		// Token: 0x04002437 RID: 9271
		private object <>2__current;

		// Token: 0x04002438 RID: 9272
		public RaidManager <>4__this;
	}

	// Token: 0x020004BF RID: 1215
	[CompilerGenerated]
	private sealed class <StartEncounterSequence>d__70 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002297 RID: 8855 RVA: 0x000C7253 File Offset: 0x000C5453
		[DebuggerHidden]
		public <StartEncounterSequence>d__70(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000C7262 File Offset: 0x000C5462
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000C7264 File Offset: 0x000C5464
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			RaidManager raidManager = this;
			switch (num)
			{
			case 0:
			{
				this.<>1__state = -1;
				raidManager.hasTriedEncounter = true;
				raidManager.isStartingEncounter = true;
				RaidScene.instance.EncounterStartSequence.Invoke();
				RaidDB.Encounter encounter = RaidDB.GetEncounter(raidManager.CurrentRaid, raidManager.CurrentEncounter);
				if (encounter != null && encounter.Type == RaidDB.EncounterType.Boss && encounter.DoIntroAnimation)
				{
					this.<>2__current = raidManager.StartCoroutine("RaidBossIntroSequence", encounter);
					this.<>1__state = 1;
					return true;
				}
				break;
			}
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				raidManager.isStartingEncounter = false;
				raidManager.EncounterStarted();
				return false;
			default:
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x000C7328 File Offset: 0x000C5528
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000C7330 File Offset: 0x000C5530
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x000C7337 File Offset: 0x000C5537
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002439 RID: 9273
		private int <>1__state;

		// Token: 0x0400243A RID: 9274
		private object <>2__current;

		// Token: 0x0400243B RID: 9275
		public RaidManager <>4__this;
	}

	// Token: 0x020004C0 RID: 1216
	[CompilerGenerated]
	private sealed class <RaidBossIntroSequence>d__71 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600229D RID: 8861 RVA: 0x000C733F File Offset: 0x000C553F
		[DebuggerHidden]
		public <RaidBossIntroSequence>d__71(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000C734E File Offset: 0x000C554E
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000C7350 File Offset: 0x000C5550
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			RaidManager raidManager = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				PlayerControl.myInstance.Input.OverrideCamera(RaidScene.instance.BossCameraPoint, 6f, true);
				UnityMainThreadDispatcher.Instance().Invoke(new Action(encounter.DoBossIntro), encounter.StartDelay);
				RaidBossIntroUI.instance.Setup(encounter.BossName, encounter.BossSubname);
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
			case 3:
				this.<>1__state = -1;
				goto IL_189;
			case 4:
				this.<>1__state = -1;
				goto IL_189;
			case 5:
				this.<>1__state = -1;
				t = 0f;
				goto IL_21D;
			case 6:
				this.<>1__state = -1;
				goto IL_21D;
			case 7:
				this.<>1__state = -1;
				return false;
			default:
				return false;
			}
			if (t < 1f)
			{
				t += Time.unscaledDeltaTime / raidManager.IntroSlowDuration;
				Time.timeScale = raidManager.IntroTimeCurve.Evaluate(t);
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			Time.timeScale = 0f;
			if (encounter.StartWithEnemyPage)
			{
				if (PhotonNetwork.IsMasterClient)
				{
					VoteManager.PrepareVote(ModType.Enemy);
				}
				this.<>2__current = new WaitForSecondsRealtime(3f);
				this.<>1__state = 3;
				return true;
			}
			goto IL_190;
			IL_189:
			if (VoteManager.IsVoting)
			{
				this.<>2__current = true;
				this.<>1__state = 4;
				return true;
			}
			IL_190:
			AudioManager.instance.EnterCombatMusic(true, 1.25f);
			this.<>2__current = new WaitForSecondsRealtime(1f);
			this.<>1__state = 5;
			return true;
			IL_21D:
			if (t >= 1f)
			{
				Time.timeScale = 1f;
				raidManager.ForcePlayerInside();
				PlayerControl.myInstance.Input.ReturnCamera(6f, false);
				this.<>2__current = new WaitForSeconds(0.75f);
				this.<>1__state = 7;
				return true;
			}
			t += Time.unscaledDeltaTime * 1f;
			Time.timeScale = raidManager.IntroTimeCurve.Evaluate(1f - t);
			this.<>2__current = true;
			this.<>1__state = 6;
			return true;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x000C75CD File Offset: 0x000C57CD
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000C75D5 File Offset: 0x000C57D5
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000C75DC File Offset: 0x000C57DC
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400243C RID: 9276
		private int <>1__state;

		// Token: 0x0400243D RID: 9277
		private object <>2__current;

		// Token: 0x0400243E RID: 9278
		public RaidDB.Encounter encounter;

		// Token: 0x0400243F RID: 9279
		public RaidManager <>4__this;

		// Token: 0x04002440 RID: 9280
		private float <t>5__2;
	}

	// Token: 0x020004C1 RID: 1217
	[CompilerGenerated]
	private sealed class <WaitForAllPlayersToStart>d__93 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022A3 RID: 8867 RVA: 0x000C75E4 File Offset: 0x000C57E4
		[DebuggerHidden]
		public <WaitForAllPlayersToStart>d__93(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000C75F3 File Offset: 0x000C57F3
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000C75F8 File Offset: 0x000C57F8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			RaidManager raidManager = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.3f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				break;
			default:
				return false;
			}
			if (MapManager.AllPlayersLoaded)
			{
				raidManager.PreEncounter();
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x000C767A File Offset: 0x000C587A
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000C7682 File Offset: 0x000C5882
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000C7689 File Offset: 0x000C5889
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002441 RID: 9281
		private int <>1__state;

		// Token: 0x04002442 RID: 9282
		private object <>2__current;

		// Token: 0x04002443 RID: 9283
		public RaidManager <>4__this;
	}
}
