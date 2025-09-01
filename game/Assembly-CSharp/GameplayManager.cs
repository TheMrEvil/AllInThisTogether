using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MEC;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class GameplayManager : MonoBehaviourPunCallbacks, IPunObservable
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0004455C File Offset: 0x0004275C
	public static bool IsInGame
	{
		get
		{
			if (GameplayManager.instance == null)
			{
				return false;
			}
			if (RaidManager.IsInRaid)
			{
				return true;
			}
			GameState currentState = GameplayManager.instance.CurrentState;
			if (currentState <= GameState.InWave)
			{
				if (currentState == GameState.Pregame)
				{
					return true;
				}
				if (currentState == GameState.InWave)
				{
					return true;
				}
			}
			else
			{
				if (currentState == GameState.PostRewards)
				{
					return true;
				}
				if (currentState == GameState.Vignette_PreWait)
				{
					return true;
				}
			}
			return RewardManager.InRewards || GoalManager.InVignette;
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000445C1 File Offset: 0x000427C1
	public static bool ShouldTrackInGameStats
	{
		get
		{
			if (RaidManager.IsInRaid)
			{
				return GameplayManager.ShouldTrackRaidStats;
			}
			return !(GameplayManager.instance == null) && !TutorialManager.InTutorial && GameplayManager.instance.CurrentState == GameState.InWave;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000445F8 File Offset: 0x000427F8
	private static bool ShouldTrackRaidStats
	{
		get
		{
			return !(RaidManager.instance == null) && !(VignetteControl.instance != null) && RaidManager.IsEncounterStarted;
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00044620 File Offset: 0x00042820
	public static string TimerText
	{
		get
		{
			return TimeSpan.FromSeconds((double)GameplayManager.instance.GameTime).ToString("hh':'mm':'ss");
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0004464A File Offset: 0x0004284A
	public static GenreTree CurrentTome
	{
		get
		{
			GameplayManager gameplayManager = GameplayManager.instance;
			if (gameplayManager == null)
			{
				return null;
			}
			return gameplayManager.GameGraph;
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0004465C File Offset: 0x0004285C
	public static GenreRootNode CurTomeRoot
	{
		get
		{
			GameplayManager gameplayManager = GameplayManager.instance;
			if (gameplayManager == null)
			{
				return null;
			}
			return gameplayManager.GameRoot;
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0004466E File Offset: 0x0004286E
	public GenreRootNode GameRoot
	{
		get
		{
			GenreTree gameGraph = this.GameGraph;
			if (gameGraph == null)
			{
				return null;
			}
			return gameGraph.Root;
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00044681 File Offset: 0x00042881
	public static GameState CurState
	{
		get
		{
			if (!(GameplayManager.instance == null))
			{
				return GameplayManager.instance.CurrentState;
			}
			return GameState._;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0004469C File Offset: 0x0004289C
	public static float deltaTime
	{
		get
		{
			if (PausePanel.IsGamePaused)
			{
				return 0f;
			}
			return Time.deltaTime;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000446B0 File Offset: 0x000428B0
	public static float GenreProgress
	{
		get
		{
			if (GameplayManager.instance == null || GameplayManager.instance.GameGraph == null)
			{
				return 0f;
			}
			return (float)(WaveManager.CurrentWave + 1) / (float)GameplayManager.instance.GameGraph.Root.TotalWaves;
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00044700 File Offset: 0x00042900
	// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00044707 File Offset: 0x00042907
	public static PagePreset PageFocus
	{
		[CompilerGenerated]
		get
		{
			return GameplayManager.<PageFocus>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			GameplayManager.<PageFocus>k__BackingField = value;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00044710 File Offset: 0x00042910
	public static int BindingLevel
	{
		get
		{
			if (GameplayManager.instance == null || GameplayManager.instance.GenreBindings == null)
			{
				return 0;
			}
			if (GameplayManager._bindingLevelCache >= 0)
			{
				return GameplayManager._bindingLevelCache;
			}
			int num = 0;
			foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
			{
				AugmentRootNode augmentRootNode;
				int num2;
				keyValuePair.Deconstruct(out augmentRootNode, out num2);
				AugmentRootNode augmentRootNode2 = augmentRootNode;
				num += augmentRootNode2.HeatLevel;
			}
			if (GameplayManager.IsChallengeActive)
			{
				num += GameplayManager.Challenge.BindingBoost;
			}
			GameplayManager._bindingLevelCache = num;
			return GameplayManager._bindingLevelCache;
		}
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x000447C8 File Offset: 0x000429C8
	private void Awake()
	{
		GameplayManager.instance = this;
		GameplayManager.WorldEffects = new List<ActionEffect>();
		this.view = base.GetComponent<PhotonView>();
		this.ResetGameState(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
		this.StartingState();
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.OnSceneChanged));
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		GameplayManager.IsChallengeActive = false;
		if (!MapManager.InLobbyScene)
		{
			Action<GenreTree> onGenereChanged = GameplayManager.OnGenereChanged;
			if (onGenereChanged == null)
			{
				return;
			}
			onGenereChanged(this.GameGraph);
		}
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x00044859 File Offset: 0x00042A59
	private void StartingState()
	{
		if (MapManager.InLobbyScene)
		{
			this.UpdateGameState(GameState.Hub);
			return;
		}
		this.Timer = 5f;
		ActionPool.ClearAll();
		this.UpdateGameState(GameState.Pregame);
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x00044884 File Offset: 0x00042A84
	private void Update()
	{
		if (RaidManager.IsInRaid)
		{
			this.GameTime += GameplayManager.deltaTime;
			this.Timer = Mathf.Max(this.Timer - Time.unscaledDeltaTime, 0f);
			return;
		}
		this.Timer = Mathf.Max(this.Timer - GameplayManager.deltaTime, 0f);
		this.UpdateGameStateAll();
		if (GameplayManager.IsInGame)
		{
			this.GameTime += GameplayManager.deltaTime;
		}
		if (!PhotonNetwork.InRoom || PhotonNetwork.IsMasterClient)
		{
			this.UpdateGameStateMaster();
		}
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x00044916 File Offset: 0x00042B16
	private void LateUpdate()
	{
		this.worldModCache = null;
		this.playerModCache = null;
		this.enemyModCache = null;
		GameplayManager._bindingLevelCache = -1;
		this.OverrideCache.Clear();
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x00044940 File Offset: 0x00042B40
	public void UpdateGameState(GameState newState)
	{
		if (this.CurrentState == newState)
		{
			return;
		}
		GameState currentState = this.CurrentState;
		this.CurrentState = newState;
		switch (newState)
		{
		case GameState.Hub:
			if (currentState != GameState.Hub_ReadyUp)
			{
				this.GameGraph = null;
				Action<GenreTree> onGenereChanged = GameplayManager.OnGenereChanged;
				if (onGenereChanged != null)
				{
					onGenereChanged(null);
				}
				WaveManager waveManager = WaveManager.instance;
				if (waveManager != null)
				{
					waveManager.ClearWaveConfig();
				}
			}
			break;
		case GameState.Hub_Preparing:
		case GameState.Hub_Traveling:
		case GameState.InWave:
			break;
		case GameState.Pregame:
			if (!RaidManager.IsInRaid)
			{
				UnityMainThreadDispatcher.Instance().Invoke(new Action(this.StartingBookTitle), 1f);
				UnityMainThreadDispatcher.Instance().Invoke(new Action(this.OpenBookStarting), 2.75f);
			}
			break;
		default:
			switch (newState)
			{
			case GameState.PostRewards:
				this.Timer = 3f;
				break;
			case GameState.Ended:
			{
				PlayerControl myInstance = PlayerControl.myInstance;
				if (myInstance != null)
				{
					myInstance.Net.SendStats();
				}
				AIManager.RemoveAllAI();
				break;
			}
			case GameState.PreWave:
			case GameState.ExplicitWait:
				break;
			case GameState.Hub_Bindings:
				this.InfoMessage("Bind the Tome...", 3f, InfoArea.Title);
				break;
			default:
				switch (newState)
				{
				case GameState.Vignette_PreWait:
					GoalManager.instance.WaitToStartVignette();
					break;
				case GameState.Vignette_Inside:
					if (VignetteControl.instance == null)
					{
						UnityEngine.Debug.Log("No Vignette Control Instance - Needed to Start Vignette!");
						return;
					}
					VignetteControl.instance.StartVignette();
					break;
				case GameState.Vignette_Completed:
					UnityEngine.Debug.Log("GameplayManager - Vignette Completed");
					VignetteControl.ClearZones();
					VignetteControl.instance.EndCleanup();
					VignetteInfoDisplay.instance.Release();
					UnityMainThreadDispatcher.Instance().Invoke(new Action(RewardManager.instance.PostVignetteReward), 1f);
					break;
				}
				break;
			}
			break;
		}
		this.TriggerWorldAugments(EventTrigger.GameStateChanged);
		Action<GameState, GameState> onGameStateChanged = GameplayManager.OnGameStateChanged;
		if (onGameStateChanged == null)
		{
			return;
		}
		onGameStateChanged(currentState, this.CurrentState);
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x00044B0B File Offset: 0x00042D0B
	private void StartingBookTitle()
	{
		if (this.CurrentState != GameState.Pregame || TutorialManager.InTutorial)
		{
			return;
		}
		GameplayUI.instance.GameStartDisplay.SetupStart();
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x00044B2D File Offset: 0x00042D2D
	private void OpenBookStarting()
	{
		if (this.CurrentState != GameState.Pregame || TutorialManager.InTutorial)
		{
			return;
		}
		AugmentsPanel.TryOpen();
		AugmentsPanel.instance.SelectTab(AugmentsPanel.BookTab.Map, false);
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x00044B54 File Offset: 0x00042D54
	private void UpdateGameStateAll()
	{
		if (this.CurrentState == GameState.Hub_Preparing && this.Timer < 5f)
		{
			InfoDisplay.SetText("Traveling in " + Mathf.CeilToInt(this.Timer).ToString() + "...", 1.5f, InfoArea.DetailTop);
		}
		GameState currentState = this.CurrentState;
		if (currentState != GameState.InWave)
		{
			if (currentState != GameState.Ended)
			{
				if (currentState != GameState.PreWave)
				{
					return;
				}
				if (WaveManager.CurrentWave < 0)
				{
					InfoDisplay.SetText("Story begins in " + Mathf.CeilToInt(this.Timer).ToString() + "...", 1.5f, InfoArea.DetailTop);
				}
				else
				{
					InfoDisplay.SetText("Next Wave in " + Mathf.CeilToInt(this.Timer).ToString() + "...", 1.5f, InfoArea.DetailTop);
				}
				if (this.Timer <= 0f)
				{
					WaveManager.instance.NextWave();
				}
			}
		}
		else if (this.heartbeatTimer < this.GameTime)
		{
			GameRecord.Heartbeat();
			this.heartbeatTimer += 2.5f;
			return;
		}
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x00044C64 File Offset: 0x00042E64
	private void UpdateGameStateMaster()
	{
		GameState currentState = this.CurrentState;
		if (currentState != GameState.Hub_Preparing)
		{
			if (currentState == GameState.Hub_ReadyUp)
			{
				if (LibraryMPStarter.instance != null && LibraryMPStarter.instance.AllZonesReady())
				{
					this.TryStartBindings();
				}
			}
		}
		else if (this.Timer <= 0f)
		{
			Fountain.instance.UpdateFountainLoc(null);
			string scene = "Fantasy";
			MapManager.SetupSeed(GameplayManager.GameSeed, 0);
			if (this.GameGraph.Root != null)
			{
				scene = this.GameGraph.Root.GetFirstMap().Scene.SceneName;
			}
			MapManager.instance.ChangeLevelSequence(scene);
		}
		currentState = this.CurrentState;
		if (currentState <= GameState.Reward_Enemy)
		{
			if (currentState != GameState.Pregame)
			{
				if (currentState != GameState.Reward_Enemy)
				{
					return;
				}
				if (VoteManager.IsVoting && VoteManager.IsTimed && this.Timer <= 0f)
				{
					VoteManager.ForceEndVote();
					this.Timer = 1f;
					return;
				}
			}
			else
			{
				this.GameTime = 0f;
				this.heartbeatTimer = 0f;
				if (this.Timer <= 0f)
				{
					this.StartOfGame();
					return;
				}
			}
		}
		else if (currentState != GameState.Ended)
		{
			if (currentState != GameState.Vignette_PreWait)
			{
				return;
			}
			if (GoalManager.instance.AllCollectedRewards())
			{
				MapManager.instance.GoToVignette();
			}
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x00044D98 File Offset: 0x00042F98
	public override void OnPlayerEnteredRoom(Player Player)
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		if (myInstance != null)
		{
			myInstance.Net.PlayerConnected(Player);
		}
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (!(entityControl is PlayerControl) && entityControl.IsMine)
			{
				entityControl.net.PlayerConnected(Player);
			}
		}
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (this.GameGraph != null)
		{
			this.view.RPC("GenereNetwork", Player, new object[]
			{
				this.GameGraph.RootNode.guid,
				GameplayManager.GameSeed
			});
			AIManager.instance.SyncLayout();
		}
		if (GameplayManager.IsChallengeActive)
		{
			this.view.RPC("SyncChallengeNetwork", Player, new object[]
			{
				GameplayManager.Challenge.ID
			});
		}
		MapManager.instance.SyncBiome();
		this.SyncMods();
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x00044EA8 File Offset: 0x000430A8
	[PunRPC]
	private void SyncChallengeNetwork(string ChallengeID)
	{
		GameplayManager.Challenge = MetaDB.GetBookClubChallenge(ChallengeID);
		GameplayManager.IsChallengeActive = true;
		UnityEngine.Debug.Log("Book Club Challenge Loaded: " + ChallengeID);
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00044ECC File Offset: 0x000430CC
	private void ResetGame()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("ResetGameState", RpcTarget.All, new object[]
		{
			GameplayManager.IsChallengeActive ? GameplayManager.GameSeed : UnityEngine.Random.Range(int.MinValue, int.MaxValue)
		});
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x00044F20 File Offset: 0x00043120
	[PunRPC]
	private void ResetGameState(int seed)
	{
		UnityEngine.Debug.Log("Resetting Game State with seed: " + seed.ToString());
		this.GameTime = 0f;
		this.heartbeatTimer = 0f;
		GameplayManager.GameSeed = seed;
		this.PlayerTeamMods = new Augments();
		GoalManager.Reset(true);
		StateControl.ResetToDefault();
		MapManager.Reset(true);
		Progression.NewAttumnentLevel = false;
		GameplayManager.PageFocus = GraphDB.GetRandomPagePreset(null);
		PlayerNetwork.PlayerCache.Clear();
		WaveManager waveManager = WaveManager.instance;
		if (waveManager != null)
		{
			waveManager.Reset();
		}
		RewardManager rewardManager = RewardManager.instance;
		if (rewardManager == null)
		{
			return;
		}
		rewardManager.Reset();
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x00044FB8 File Offset: 0x000431B8
	private Augments GetGenreAugments(ModType modType)
	{
		if (!GameplayManager.IsInGame || this.GameGraph == null)
		{
			return null;
		}
		Augments augments = new Augments();
		foreach (AugmentTree t in this.GameGraph.Root.GetAugments(WaveManager.CurrentWave, modType))
		{
			augments.Add(t, 1);
		}
		return augments;
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x00045040 File Offset: 0x00043240
	public Augments GetGameAugments(ModType modType)
	{
		if (modType != ModType.Player)
		{
			if (modType == ModType.Enemy)
			{
				if (this.enemyModCache != null)
				{
					return this.enemyModCache;
				}
				this.enemyModCache = new Augments();
				this.enemyModCache.Add(this.GetGenreAugments(modType));
				return this.enemyModCache;
			}
			else
			{
				if (this.worldModCache != null)
				{
					return this.worldModCache;
				}
				this.worldModCache = new Augments();
				this.worldModCache.Add(this.GetGenreAugments(modType));
				return this.worldModCache;
			}
		}
		else
		{
			if (this.playerModCache != null)
			{
				return this.playerModCache;
			}
			this.playerModCache = new Augments();
			this.playerModCache.Add(this.GetGenreAugments(modType));
			return this.playerModCache;
		}
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x000450EC File Offset: 0x000432EC
	public void SyncMods()
	{
		AIManager.SyncBossMods();
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("GlobalModsNetwork", RpcTarget.All, new object[]
		{
			AIManager.GlobalEnemyMods.ToString(),
			this.PlayerTeamMods.ToString(),
			InkManager.PurchasedMods.ToString(),
			this.GenreBindings.ToString(),
			AIManager.AugmentIDs.ToNetString()
		});
		GoalManager.instance.SyncBonusObjective();
		ActionPool.WarmGlobalAugments();
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x00045170 File Offset: 0x00043370
	[PunRPC]
	private void GlobalModsNetwork(string aimods, string playermods, string InkMods, string bindings, string enemyModIDs)
	{
		AIManager.GlobalEnemyMods = new Augments(aimods);
		this.PlayerTeamMods = new Augments(playermods);
		InkManager.PurchasedMods = new Augments(InkMods);
		this.GenreBindings = new Augments(bindings);
		AIManager.AugmentIDs.FromNetString(enemyModIDs);
		if (PanelManager.CurPanel == PanelType.Augments)
		{
			AugmentsPanel.instance.BindingPage.Setup();
		}
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x000451D0 File Offset: 0x000433D0
	public void InfoMessage(string message, float duration, InfoArea area)
	{
		if (message == null || message.Length < 2)
		{
			return;
		}
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("InfoMessageNetwork", RpcTarget.All, new object[]
		{
			message,
			duration,
			(int)area
		});
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0004521F File Offset: 0x0004341F
	[PunRPC]
	private void InfoMessageNetwork(string message, float duration, int level)
	{
		InfoDisplay.SetText(message, duration, (InfoArea)level);
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0004522C File Offset: 0x0004342C
	public void ClearWorldEffects()
	{
		for (int i = AreaOfEffect.AllAreas.Count - 1; i >= 0; i--)
		{
			AreaOfEffect areaOfEffect = AreaOfEffect.AllAreas[i];
			areaOfEffect.Expire(areaOfEffect.actionProps, ActionEffect.EffectExpireReason.Cancel, true);
		}
		for (int j = Projectile.AllProjectiles.Count - 1; j >= 0; j--)
		{
			Projectile.AllProjectiles[j].Expire(null, ActionEffect.EffectExpireReason.Cancel, true);
		}
		for (int k = Beam.AllBeams.Count - 1; k >= 0; k--)
		{
			Beam.AllBeams[k].Expire(null, ActionEffect.EffectExpireReason.Cancel, true);
		}
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x000452BC File Offset: 0x000434BC
	[PunRPC]
	public void TriggerAugmentsNetwork(int trigger, int seed)
	{
		EffectProperties effectProperties = new EffectProperties();
		effectProperties.IsWorld = true;
		effectProperties.SourceType = ActionSource.Genre;
		effectProperties.OverrideSeed(seed, 0);
		EventTrigger trigger2 = (EventTrigger)trigger;
		this.GetGameAugments(ModType.Binding).ApplySnippetsFromProps(trigger2, effectProperties.Copy(false));
		try
		{
			foreach (EntityControl entityControl in EntityControl.AllEntities)
			{
				if (entityControl != null)
				{
					entityControl.TriggerSnippets(trigger2, null, 1f);
				}
			}
		}
		catch (Exception message)
		{
			UnityEngine.Debug.LogError("Error Triggering Gameplay Augments on All Entities: " + trigger.ToString());
			UnityEngine.Debug.LogError(message);
		}
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0004537C File Offset: 0x0004357C
	public static void AddIndicatorStatus(EntityControl entity)
	{
		if (entity.IsDead || entity.HasStatusEffectGUID(GameplayManager.instance.GoalStatus.RootNode.guid))
		{
			return;
		}
		entity.net.ApplyStatus(GameplayManager.instance.GoalStatus.HashCode, -1, 0f, 1, false, 0);
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x000453D4 File Offset: 0x000435D4
	private void OnSceneChanged()
	{
		if (MapManager.InLobbyScene || this.CurrentState == GameState.Hub_Traveling || this.CurrentState == GameState.Post_Traveling)
		{
			if (PlayerControl.myInstance != null)
			{
				PlayerControl.myInstance.Reset(true);
			}
			AIManager.instance.Reset();
			InkManager.instance.Reset();
			this.ResetGame();
			if (MapManager.InLobbyScene)
			{
				GameplayManager.IsChallengeActive = false;
			}
			base.Invoke("StartingState", 0.15f);
		}
		else
		{
			base.StartCoroutine("MapWaitForNextReward");
		}
		if (PlayerControl.myInstance != null && !MapManager.InLobbyScene)
		{
			int b = PlayerControl.AllPlayers.IndexOf(PlayerControl.myInstance);
			List<SpawnPoint> allSpawns = SpawnPoint.GetAllSpawns(SpawnType.Player, EnemyLevel.None);
			SpawnPoint spawnPoint = allSpawns[Mathf.Min(allSpawns.Count - 1, b)];
			PlayerControl.myInstance.Movement.SetPositionWithCamera(spawnPoint.point, spawnPoint.transform.forward, true, true);
			PlayerControl.myInstance.Mana.Recharge(99f);
			PlayerControl.ToggleMultiplayerCollision(true);
		}
		this.ClearWorldEffects();
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x000454D9 File Offset: 0x000436D9
	private IEnumerator MapWaitForNextReward()
	{
		yield return new WaitForSeconds(1f);
		while (!MapManager.AllPlayersLoaded)
		{
			yield return true;
		}
		if (this.CurrentState == GameState.Vignette_Traveling)
		{
			this.UpdateGameState(GameState.Vignette_Inside);
		}
		else
		{
			RewardManager.instance.NextReward();
		}
		yield break;
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x000454E8 File Offset: 0x000436E8
	public void TrySetGenre(GenreTree graph)
	{
		if (graph == this.GameGraph)
		{
			return;
		}
		this.view.RPC("TrySetGenreMaster", RpcTarget.MasterClient, new object[]
		{
			graph.Root.guid
		});
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x00045520 File Offset: 0x00043720
	[PunRPC]
	public void TrySetGenreMaster(string guid)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		GenreTree genre = GraphDB.GetGenre(guid);
		if (genre == null || genre == this.GameGraph)
		{
			return;
		}
		if (GameplayManager.CurState == GameState.Hub_ReadyUp)
		{
			this.UpdateGameState(GameState.Hub);
		}
		this.view.RPC("GenereNetwork", RpcTarget.All, new object[]
		{
			genre.RootNode.guid,
			UnityEngine.Random.Range(0, int.MaxValue)
		});
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0004559C File Offset: 0x0004379C
	[PunRPC]
	private void GenereNetwork(string GraphGUID, int seed)
	{
		GenreTree genre = GraphDB.GetGenre(GraphGUID);
		GameplayManager.GameSeed = seed;
		if (this.GameGraph == genre)
		{
			return;
		}
		UnityEngine.Debug.Log(string.Concat(new string[]
		{
			"Tome set to ",
			genre.Root.ShortName,
			" [Seed: ",
			GameplayManager.GameSeed.ToString(),
			"]"
		}));
		GameplayManager.IsChallengeActive = false;
		this.GameGraph = genre;
		WaveManager.instance.ClearWaveConfig();
		Action<GenreTree> onGenereChanged = GameplayManager.OnGenereChanged;
		if (onGenereChanged == null)
		{
			return;
		}
		onGenereChanged(genre);
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0004562F File Offset: 0x0004382F
	public void TrySetChallenge()
	{
		if (GameplayManager.IsChallengeActive)
		{
			return;
		}
		this.view.RPC("TrySetChallengeMaster", RpcTarget.MasterClient, Array.Empty<object>());
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x00045650 File Offset: 0x00043850
	[PunRPC]
	public void TrySetChallengeMaster()
	{
		MetaDB.BookClubChallenge currentChallenge = MetaDB.CurrentChallenge;
		if (currentChallenge == null || GameplayManager.IsChallengeActive)
		{
			return;
		}
		this.LoadChallenge(currentChallenge);
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x00045678 File Offset: 0x00043878
	public void LoadChallenge(MetaDB.BookClubChallenge challenge)
	{
		if (!PhotonNetwork.IsMasterClient || challenge == null || (GameplayManager.IsChallengeActive && challenge.ID != GameplayManager.Challenge.ID))
		{
			return;
		}
		if (GameplayManager.CurState == GameState.Hub_ReadyUp)
		{
			this.UpdateGameState(GameState.Hub);
		}
		int num = challenge.ID.GetHashCode() + MetaDB.GetCurrentChallengeNumber();
		this.view.RPC("SetupChallengeNetwork", RpcTarget.All, new object[]
		{
			challenge.ID,
			num
		});
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x000456FC File Offset: 0x000438FC
	[PunRPC]
	private void SetupChallengeNetwork(string challengeID, int seed)
	{
		GameplayManager.Challenge = MetaDB.GetBookClubChallenge(challengeID);
		GameplayManager.GameSeed = seed;
		GameplayManager.IsChallengeActive = true;
		UnityEngine.Debug.Log(string.Concat(new string[]
		{
			"Book Club Challenge Set: ",
			challengeID,
			" [Seed: ",
			GameplayManager.GameSeed.ToString(),
			"]"
		}));
		this.GameGraph = GameplayManager.Challenge.Tome;
		WaveManager.instance.ClearWaveConfig();
		Action<GenreTree> onGenereChanged = GameplayManager.OnGenereChanged;
		if (onGenereChanged == null)
		{
			return;
		}
		onGenereChanged(this.GameGraph);
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x00045788 File Offset: 0x00043988
	public void PrepareForStart()
	{
		Room currentRoom = PhotonNetwork.CurrentRoom;
		if (((currentRoom != null) ? currentRoom.PlayerCount : 1) <= 1)
		{
			this.view.RPC("TryStartBindings", RpcTarget.MasterClient, Array.Empty<object>());
			return;
		}
		this.view.RPC("TryStartingZones", RpcTarget.MasterClient, Array.Empty<object>());
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x000457D6 File Offset: 0x000439D6
	[PunRPC]
	public void TryStartingZones()
	{
		if (!PhotonNetwork.IsMasterClient || GameplayManager.CurState != GameState.Hub)
		{
			return;
		}
		this.UpdateGameState(GameState.Hub_ReadyUp);
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x000457F0 File Offset: 0x000439F0
	[PunRPC]
	public void TryStartBindings()
	{
		if (!PhotonNetwork.IsMasterClient || (GameplayManager.CurState != GameState.Hub && GameplayManager.CurState != GameState.Hub_ReadyUp))
		{
			return;
		}
		if (!(((!TutorialManager.InTutorial && (GameStats.GetTomeStat(GenrePanel.instance.bindingReqGenre, GameStats.Stat.TomesWon, 0) > 0 || Settings.HasCompletedUITutorial(UITutorial.Tutorial.Bindings))) | Progression.PrestigeCount > 0) & !GameplayManager.IsChallengeActive))
		{
			this.PrepareToStart();
			return;
		}
		this.UpdateGameState(GameState.Hub_Bindings);
		base.Invoke("StartBingingVote", 4f);
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x00045870 File Offset: 0x00043A70
	private void StartBingingVote()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		VoteManager.StartVote(ChoiceType.Bindings);
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x00045880 File Offset: 0x00043A80
	public void SetBindings(List<AugmentTree> augments, bool prepareToStart = true)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.GenreBindings = new Augments();
		foreach (AugmentTree t in augments)
		{
			this.GenreBindings.Add(t, 1);
		}
		this.AddGlobalBindings();
		this.SyncMods();
		if (prepareToStart)
		{
			this.PrepareToStart();
		}
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x00045904 File Offset: 0x00043B04
	public void AddBinding(AugmentTree augment)
	{
		if (augment.Root.modType != ModType.Binding)
		{
			return;
		}
		this.GenreBindings.Add(augment, 1);
		this.AddGlobalBindings();
		this.SyncMods();
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x00045934 File Offset: 0x00043B34
	public void AddGlobalBindings()
	{
		GameplayManager._bindingLevelCache = -1;
		foreach (AugmentTree t in WaveDB.GetGlobalBindings(GameplayManager.BindingLevel, WaveManager.instance.AppendixLevel))
		{
			if (!this.GenreBindings.trees.ContainsKey(t))
			{
				this.GenreBindings.Add(t, 1);
			}
		}
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x000459C0 File Offset: 0x00043BC0
	private void PrepareToStart()
	{
		this.UpdateGameState(GameState.Hub_Preparing);
		AIManager.instance.GetRandomLayout(GameplayManager.IsChallengeActive ? GameplayManager.GameSeed : -1);
		MapManager.SelectBiome(GameplayManager.IsChallengeActive ? GameplayManager.GameSeed : -1);
		this.Timer = 3f;
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x00045A0C File Offset: 0x00043C0C
	private void StartOfGame()
	{
		GameRecord.NewGame();
		if (GameplayManager.IsChallengeActive)
		{
			this.view.RPC("OnChallengeStarted", RpcTarget.All, Array.Empty<object>());
			if (PhotonNetwork.IsMasterClient)
			{
				foreach (AugmentTree augmentTree in GameplayManager.Challenge.TornPages)
				{
					if (augmentTree.Root.ApplyPlayerTeam)
					{
						this.PlayerTeamMods.Add(augmentTree, 1);
					}
				}
				AIManager.instance.AddAugments(GameplayManager.Challenge.TornPages, true);
				this.SetBindings(GameplayManager.Challenge.Bindings, false);
			}
			this.UpdateGameState(GameState.Reward_Start);
			base.StartCoroutine(this.ChallengeStartRewardDelayed());
		}
		else
		{
			this.UpdateGameState(GameState.Reward_Start);
			RewardManager.instance.StartGameReward();
		}
		InkManager.instance.SelectPauseQuote(this.GameGraph, GameplayManager.BindingLevel);
		this.view.RPC("OnStartOfGameNetwork", RpcTarget.All, Array.Empty<object>());
		if (PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode)
		{
			PhotonNetwork.CurrentRoom.IsVisible = false;
		}
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x00045B40 File Offset: 0x00043D40
	private IEnumerator ChallengeStartRewardDelayed()
	{
		yield return new WaitForSeconds(0.5f);
		RewardManager.instance.StartGameReward();
		yield break;
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x00045B48 File Offset: 0x00043D48
	[PunRPC]
	private void OnChallengeStarted()
	{
		MapManager.LastVignettes.Clear();
		GameplayManager.Challenge.SetLoadout();
		UnityEngine.Debug.Log("Setting Player Random Seed: " + GameplayManager.GameSeed.ToString());
		PlayerControl.myInstance.SetupRandom(GameplayManager.GameSeed, 0);
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x00045B87 File Offset: 0x00043D87
	[PunRPC]
	private void OnStartOfGameNetwork()
	{
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x00045B8C File Offset: 0x00043D8C
	public void TriggerWorldAugments(EventTrigger e)
	{
		if (!PhotonNetwork.IsMasterClient || InkManager.instance == null || AIManager.instance == null || GoalManager.instance == null || !PhotonNetwork.InRoom)
		{
			return;
		}
		InkManager.instance.TriggerAugments(e);
		AIManager.instance.TriggerAugments(e);
		GoalManager.instance.TriggerAugments(e);
		int num = UnityEngine.Random.Range(0, 9999999);
		this.view.RPC("TriggerAugmentsNetwork", RpcTarget.All, new object[]
		{
			(int)e,
			num
		});
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x00045C28 File Offset: 0x00043E28
	public Transform GetSpawnPoint()
	{
		List<SpawnPoint> allSpawns = SpawnPoint.GetAllSpawns(SpawnType.Player, EnemyLevel.None);
		return allSpawns[UnityEngine.Random.Range(0, allSpawns.Count)].transform;
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00045C54 File Offset: 0x00043E54
	public void SetTimescale(float scale)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("SetTimescaleNetwork", RpcTarget.All, new object[]
		{
			scale
		});
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00045C7E File Offset: 0x00043E7E
	[PunRPC]
	private void SetTimescaleNetwork(float scale)
	{
		Time.timeScale = Mathf.Clamp(scale, 0.05f, 4f);
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x00045C95 File Offset: 0x00043E95
	public void BossDied(Vector3 point)
	{
		GameplayManager.lastBossPoint = point;
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x00045CA0 File Offset: 0x00043EA0
	public void GameWon()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		GoalManager.instance.CancelBonusObjective();
		this.UpdateGameState(GameState.PostGame);
		this.view.RPC("GameWonNetwork", RpcTarget.All, new object[]
		{
			AIManager.NearestNavPoint(GameplayManager.lastBossPoint, -1f)
		});
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x00045CF8 File Offset: 0x00043EF8
	[PunRPC]
	private void GameWonNetwork(Vector3 originPt)
	{
		this.ClearWorldEffects();
		AIManager.KillAllEnemies();
		if (WaveManager.instance.AppendixLevel == 0)
		{
			if (GameplayManager.IsChallengeActive)
			{
				GameplayManager.ChallengeBaseTime = this.GameTime;
				GameplayManager.ChallengeUniqueStat = GameplayManager.Challenge.GetCurrentRunStat();
			}
			this.EndOfGameStats(true, originPt);
			Progression.TryIncrementAttunement(GameplayManager.BindingLevel);
			PlayerControl.myInstance.TriggerSnippets(EventTrigger.Game_Won, null, 1f);
		}
		else
		{
			GameStats.TryUpdateMax(this.GameGraph, PlayerControl.myInstance.SignatureColor, GameStats.Stat.MaxAppendix, WaveManager.instance.AppendixLevel);
			PlayerControl.myInstance.TriggerSnippets(EventTrigger.Appendix_Won, null, 1f);
		}
		if (!TutorialManager.InTutorial)
		{
			PlayerControl.myInstance.TryRespawn();
			Settings.DoneTutorial();
			if (WaveManager.instance.AppendixLevel == 0)
			{
				Progression.RequestQuillmarkReward(originPt, this.GameGraph, GameplayManager.BindingLevel, true);
				Progression.EndGameReward(originPt, true, this.GameGraph, GameplayManager.BindingLevel);
				Progression.TrySpecialRewards(originPt, this.GameGraph, GameplayManager.BindingLevel);
			}
			else
			{
				Progression.RequestAppendixReward(originPt, GameplayManager.BindingLevel, WaveManager.instance.AppendixLevel, WaveManager.instance.AppendixChapterNumber, true);
			}
			GoalManager.instance.CreatePortals();
			Timing.Instance.Invoke(delegate
			{
				MapManager.instance.SpawnDPSTrigger();
			}, 3f);
		}
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x00045E4C File Offset: 0x0004404C
	public void PostGame_PortalInteraction(PortalType pType)
	{
		if (!GoalManager.instance.AllCollectedRewards())
		{
			InfoDisplay.SetText("Scribes have outstanding rewards!", 2f, InfoArea.DetailBottom);
			return;
		}
		this.view.RPC("PostGameOptionChosen", RpcTarget.MasterClient, new object[]
		{
			(int)pType
		});
		this.PortalChosen();
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x00045E9C File Offset: 0x0004409C
	[PunRPC]
	private void PostGameOptionChosen(int value)
	{
		if (!PhotonNetwork.IsMasterClient || this.CurrentState != GameState.PostGame)
		{
			return;
		}
		UnityEngine.Debug.Log("Portal Chosen");
		if (GoalManager.instance.CanEndless())
		{
			VoteManager.StartVote(ChoiceType.Endless);
		}
		else
		{
			this.EndGame(true);
		}
		this.view.RPC("PortalChosen", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x00045EF6 File Offset: 0x000440F6
	[PunRPC]
	private void PortalChosen()
	{
		GoalManager.instance.DeactivateAllPortals();
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x00045F04 File Offset: 0x00044104
	public void EndlessDecisionMade(bool goingEndless)
	{
		if (!PhotonNetwork.IsMasterClient || this.CurrentState != GameState.PostGame)
		{
			return;
		}
		UnityMainThreadDispatcher.Instance().Invoke(delegate
		{
			if (goingEndless)
			{
				WaveManager.instance.NextAppendix();
				return;
			}
			this.EndGame(true);
		}, 2f);
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x00045F52 File Offset: 0x00044152
	public void AbortGame()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		GameplayManager.instance.EndGame(false);
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x00045F67 File Offset: 0x00044167
	public void EndGame(bool success)
	{
		this.Timer = 10f;
		this.UpdateGameState(GameState.Ended);
		this.view.RPC("EndGameNetwork", RpcTarget.All, new object[]
		{
			success
		});
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x00045F9C File Offset: 0x0004419C
	[PunRPC]
	private void EndGameNetwork(bool didWin)
	{
		if (TutorialManager.InTutorial)
		{
			UnityEngine.Debug.Log("Tutorial Completed");
			UnityMainThreadDispatcher.Instance().Invoke(new Action(MapManager.ReturnToLibrary), 2.5f);
			return;
		}
		if (PlayerControl.myInstance != null)
		{
			PlayerControl.myInstance.Net.SendStats();
		}
		if (GameplayManager.IsChallengeActive)
		{
			UnityEngine.Debug.Log("Challenge Completed: Win -> " + didWin.ToString());
			GameRecord.UploadChallenge(didWin, GameplayManager.ChallengeBaseTime, this.GameTime, GameplayManager.ChallengeUniqueStat);
		}
		else if (WaveManager.instance.AppendixLevel > 0)
		{
			GameRecord.UploadAppendix(didWin);
		}
		if (!didWin)
		{
			if (WaveManager.instance.AppendixLevel <= 0)
			{
				this.EndOfGameStats(false, Vector3.zero);
				PlayerControl.myInstance.TriggerSnippets(EventTrigger.Game_Lost, null, 1f);
				Progression.RequestQuillmarkReward(Vector3.zero, this.GameGraph, GameplayManager.BindingLevel, false);
				Progression.EndGameReward(Vector3.zero, false, this.GameGraph, GameplayManager.BindingLevel);
			}
			else
			{
				PlayerControl.myInstance.TriggerSnippets(EventTrigger.Appendix_Lost, null, 1f);
				Progression.RequestAppendixReward(Vector3.zero, GameplayManager.BindingLevel, WaveManager.instance.AppendixLevel, WaveManager.instance.AppendixChapterNumber, false);
			}
			EndGameAnim.Display(false);
		}
		try
		{
			if (WaveManager.instance.AppendixLevel <= 0)
			{
				GameRecord.GameCompleted(didWin, this.GameTime);
			}
		}
		catch (Exception message)
		{
			UnityEngine.Debug.LogError(message);
			throw;
		}
		GameplayManager.InvokeDelayed(didWin ? 1f : 3.5f, delegate()
		{
			PostGamePanel.instance.Ended(didWin || WaveManager.instance.AppendixLevel > 0);
		});
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00046154 File Offset: 0x00044354
	private void EndOfGameStats(bool didWin, Vector3 point)
	{
		if (TutorialManager.InTutorial)
		{
			return;
		}
		GameStats.IncrementStat(this.GameGraph, PlayerControl.myInstance.SignatureColor, GameStats.Stat.TomesPlayed, 1, true);
		if (GameplayManager.IsChallengeActive)
		{
			MetaDB.ChallengeEnded(didWin, GameplayManager.ChallengeBaseTime, GameplayManager.ChallengeUniqueStat, point);
		}
		if (didWin)
		{
			GameStats.IncrementStat(this.GameGraph, PlayerControl.myInstance.SignatureColor, GameStats.Stat.TomesWon, 1, true);
			GameStats.TryUpdateMax(this.GameGraph, PlayerControl.myInstance.SignatureColor, GameStats.Stat.MaxBinding, GameplayManager.BindingLevel);
		}
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x000461CF File Offset: 0x000443CF
	public static void InvokeDelayed(float delay, Action action)
	{
		GameplayManager.instance.StartCoroutine(GameplayManager.instance.InvokeRoutine(delay, action));
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x000461E8 File Offset: 0x000443E8
	private IEnumerator InvokeRoutine(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action();
		yield break;
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x000461FE File Offset: 0x000443FE
	public static int GetLayer(EntityControl control)
	{
		if (!(control is PlayerControl))
		{
			return 9;
		}
		if (control.IsMine)
		{
			return 8;
		}
		return 11;
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x00046218 File Offset: 0x00044418
	public static bool HasGameOverride(string id)
	{
		if (GameplayManager.instance == null)
		{
			return false;
		}
		bool result;
		if (GameplayManager.instance.OverrideCache.TryGetValue(id, out result))
		{
			return result;
		}
		bool gameOverride = GameplayManager.instance.GetGameOverride(id);
		GameplayManager.instance.OverrideCache.Add(id, gameOverride);
		return gameOverride;
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x00046268 File Offset: 0x00044468
	private bool GetGameOverride(string id)
	{
		using (List<WorldOverrideNode>.Enumerator enumerator = this.GenreBindings.GetWorldOverrides().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ID == id)
				{
					return true;
				}
			}
		}
		using (List<WorldOverrideNode>.Enumerator enumerator = InkManager.PurchasedMods.GetWorldOverrides().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ID == id)
				{
					return true;
				}
			}
		}
		using (List<WorldOverrideNode>.Enumerator enumerator = AIManager.GlobalEnemyMods.GetWorldOverrides().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ID == id)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x00046370 File Offset: 0x00044570
	public static bool PointIsVisible(EntityControl entity, Vector3 targetPoint, Vector2 validRange)
	{
		if (entity == null)
		{
			return false;
		}
		EntityDisplay display = entity.display;
		Vector3? vector;
		if (display == null)
		{
			vector = null;
		}
		else
		{
			Transform eyelineLocation = display.EyelineLocation;
			vector = ((eyelineLocation != null) ? new Vector3?(eyelineLocation.position) : null);
		}
		return GameplayManager.PointIsVisible(targetPoint, vector ?? entity.transform.position, validRange);
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x000463E0 File Offset: 0x000445E0
	public static bool PointIsVisible(Vector3 targetPoint, Vector3 inputOverride, Vector2 validRange)
	{
		Vector3 vector = targetPoint - inputOverride;
		if (vector.magnitude < validRange.x || vector.magnitude > validRange.y)
		{
			return false;
		}
		Ray ray = new Ray(inputOverride, vector.normalized);
		float maxDistance = vector.magnitude * 0.98f;
		FlyingNavmesh flyingNavmesh = FlyingNavmesh.instance;
		RaycastHit raycastHit;
		return !Physics.Raycast(ray, out raycastHit, maxDistance, (flyingNavmesh != null) ? flyingNavmesh.checkMask : LayerMask.GetMask(new string[]
		{
			"StaticLevel"
		}));
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x00046468 File Offset: 0x00044668
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(this.Timer);
			stream.SendNext(this.GameTime);
			stream.SendNext((int)this.CurrentState);
			return;
		}
		float num = (float)stream.ReceiveNext();
		if (Mathf.Abs(num - this.Timer) > 1f)
		{
			this.Timer = num - (float)PhotonNetwork.GetPing() / 1000f;
		}
		this.GameTime = (float)stream.ReceiveNext();
		GameState newState = (GameState)((int)stream.ReceiveNext());
		this.UpdateGameState(newState);
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x00046509 File Offset: 0x00044709
	private void OnDestroy()
	{
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.OnSceneChanged));
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x0004652B File Offset: 0x0004472B
	public GameplayManager()
	{
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x00046554 File Offset: 0x00044754
	// Note: this type is marked as 'beforefieldinit'.
	static GameplayManager()
	{
	}

	// Token: 0x040008E1 RID: 2273
	public static GameplayManager instance;

	// Token: 0x040008E2 RID: 2274
	private PhotonView view;

	// Token: 0x040008E3 RID: 2275
	public static Action<GameState, GameState> OnGameStateChanged;

	// Token: 0x040008E4 RID: 2276
	public static Action<GenreTree> OnGenereChanged;

	// Token: 0x040008E5 RID: 2277
	public LayerMask EnvironmentMask;

	// Token: 0x040008E6 RID: 2278
	public LayerMask EntityMask;

	// Token: 0x040008E7 RID: 2279
	public static bool IsChallengeActive;

	// Token: 0x040008E8 RID: 2280
	public static MetaDB.BookClubChallenge Challenge;

	// Token: 0x040008E9 RID: 2281
	public static float ChallengeBaseTime;

	// Token: 0x040008EA RID: 2282
	public static int ChallengeUniqueStat;

	// Token: 0x040008EB RID: 2283
	public Augments PlayerTeamMods = new Augments();

	// Token: 0x040008EC RID: 2284
	public Augments GenreBindings = new Augments();

	// Token: 0x040008ED RID: 2285
	public static int GameSeed;

	// Token: 0x040008EE RID: 2286
	public StatusTree GoalStatus;

	// Token: 0x040008EF RID: 2287
	public GenreTree EditorGraph;

	// Token: 0x040008F0 RID: 2288
	public GenreTree GameGraph;

	// Token: 0x040008F1 RID: 2289
	public float Timer;

	// Token: 0x040008F2 RID: 2290
	public GameState CurrentState;

	// Token: 0x040008F3 RID: 2291
	public float GameTime;

	// Token: 0x040008F4 RID: 2292
	private float heartbeatTimer;

	// Token: 0x040008F5 RID: 2293
	[NonSerialized]
	public static List<ActionEffect> WorldEffects = new List<ActionEffect>();

	// Token: 0x040008F6 RID: 2294
	[CompilerGenerated]
	private static PagePreset <PageFocus>k__BackingField;

	// Token: 0x040008F7 RID: 2295
	private static int _bindingLevelCache = -1;

	// Token: 0x040008F8 RID: 2296
	private Augments worldModCache;

	// Token: 0x040008F9 RID: 2297
	private Augments playerModCache;

	// Token: 0x040008FA RID: 2298
	private Augments enemyModCache;

	// Token: 0x040008FB RID: 2299
	public static Vector3 lastBossPoint = Vector3.zero;

	// Token: 0x040008FC RID: 2300
	private Dictionary<string, bool> OverrideCache = new Dictionary<string, bool>();

	// Token: 0x020004DB RID: 1243
	[CompilerGenerated]
	private sealed class <MapWaitForNextReward>d__74 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002311 RID: 8977 RVA: 0x000C85CB File Offset: 0x000C67CB
		[DebuggerHidden]
		public <MapWaitForNextReward>d__74(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000C85DA File Offset: 0x000C67DA
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000C85DC File Offset: 0x000C67DC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			GameplayManager gameplayManager = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(1f);
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
				if (gameplayManager.CurrentState == GameState.Vignette_Traveling)
				{
					gameplayManager.UpdateGameState(GameState.Vignette_Inside);
				}
				else
				{
					RewardManager.instance.NextReward();
				}
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x000C8676 File Offset: 0x000C6876
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000C867E File Offset: 0x000C687E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x000C8685 File Offset: 0x000C6885
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400249A RID: 9370
		private int <>1__state;

		// Token: 0x0400249B RID: 9371
		private object <>2__current;

		// Token: 0x0400249C RID: 9372
		public GameplayManager <>4__this;
	}

	// Token: 0x020004DC RID: 1244
	[CompilerGenerated]
	private sealed class <ChallengeStartRewardDelayed>d__91 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x000C868D File Offset: 0x000C688D
		[DebuggerHidden]
		public <ChallengeStartRewardDelayed>d__91(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000C869C File Offset: 0x000C689C
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000C86A0 File Offset: 0x000C68A0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			RewardManager.instance.StartGameReward();
			return false;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x000C86EF File Offset: 0x000C68EF
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000C86F7 File Offset: 0x000C68F7
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x000C86FE File Offset: 0x000C68FE
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400249D RID: 9373
		private int <>1__state;

		// Token: 0x0400249E RID: 9374
		private object <>2__current;
	}

	// Token: 0x020004DD RID: 1245
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x0600231D RID: 8989 RVA: 0x000C8706 File Offset: 0x000C6906
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x000C8712 File Offset: 0x000C6912
		public <>c()
		{
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000C871A File Offset: 0x000C691A
		internal void <GameWonNetwork>b__101_0()
		{
			MapManager.instance.SpawnDPSTrigger();
		}

		// Token: 0x0400249F RID: 9375
		public static readonly GameplayManager.<>c <>9 = new GameplayManager.<>c();

		// Token: 0x040024A0 RID: 9376
		public static Action <>9__101_0;
	}

	// Token: 0x020004DE RID: 1246
	[CompilerGenerated]
	private sealed class <>c__DisplayClass105_0
	{
		// Token: 0x06002320 RID: 8992 RVA: 0x000C8726 File Offset: 0x000C6926
		public <>c__DisplayClass105_0()
		{
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000C872E File Offset: 0x000C692E
		internal void <EndlessDecisionMade>b__0()
		{
			if (this.goingEndless)
			{
				WaveManager.instance.NextAppendix();
				return;
			}
			this.<>4__this.EndGame(true);
		}

		// Token: 0x040024A1 RID: 9377
		public bool goingEndless;

		// Token: 0x040024A2 RID: 9378
		public GameplayManager <>4__this;
	}

	// Token: 0x020004DF RID: 1247
	[CompilerGenerated]
	private sealed class <>c__DisplayClass108_0
	{
		// Token: 0x06002322 RID: 8994 RVA: 0x000C874F File Offset: 0x000C694F
		public <>c__DisplayClass108_0()
		{
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000C8757 File Offset: 0x000C6957
		internal void <EndGameNetwork>b__0()
		{
			PostGamePanel.instance.Ended(this.didWin || WaveManager.instance.AppendixLevel > 0);
		}

		// Token: 0x040024A3 RID: 9379
		public bool didWin;
	}

	// Token: 0x020004E0 RID: 1248
	[CompilerGenerated]
	private sealed class <InvokeRoutine>d__111 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002324 RID: 8996 RVA: 0x000C877B File Offset: 0x000C697B
		[DebuggerHidden]
		public <InvokeRoutine>d__111(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000C878A File Offset: 0x000C698A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000C878C File Offset: 0x000C698C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(delay);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			action();
			return false;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x000C87DD File Offset: 0x000C69DD
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000C87E5 File Offset: 0x000C69E5
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x000C87EC File Offset: 0x000C69EC
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040024A4 RID: 9380
		private int <>1__state;

		// Token: 0x040024A5 RID: 9381
		private object <>2__current;

		// Token: 0x040024A6 RID: 9382
		public float delay;

		// Token: 0x040024A7 RID: 9383
		public Action action;
	}
}
