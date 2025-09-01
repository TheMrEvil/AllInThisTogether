using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class GlobalSetup : MonoBehaviour
{
	// Token: 0x06000CC7 RID: 3271 RVA: 0x00051C1C File Offset: 0x0004FE1C
	private void Awake()
	{
		if (GlobalSetup.instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		GlobalSetup.instance = this;
		Progression.BadLoad = false;
		ES3.CacheFile();
		XGraphCollector.CollectData();
		GraphDB.SetInstance(Resources.Load<GraphDB>("GraphDB"));
		GlobalSetup.ResetInits();
		Settings.LoadSettings();
		Progression.ValidateSaveFile();
		Debug.Log("Initlaized Progression: " + (UnlockManager.Initialized && Currency.Initialized && AchievementManager.Initialized && GameStats.Initialized && Progression.Initialized).ToString());
		GlobalSetup.StartedExternal = !MapManager.InLobbyScene;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		base.gameObject.AddComponent<UnityMainThreadDispatcher>();
		base.gameObject.AddComponent<PlatformSetup>();
		UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Global_State"), base.transform);
		UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Global_UI"), base.transform);
		new GameObject("ActionPool").AddComponent<ActionPool>();
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00051D1D File Offset: 0x0004FF1D
	private void Start()
	{
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00051D20 File Offset: 0x0004FF20
	private static void ResetInits()
	{
		UnlockManager.Initialized = false;
		Progression.Initialized = false;
		GameStats.Initialized = false;
		Currency.Initialized = false;
		LibraryManager.InLibraryTutorial = false;
		AchievementManager.Initialized = false;
		GameRecord.Reset();
		EntityControl.AllEntities = new List<EntityControl>();
		PlayerControl.myInstance = null;
		PlayerNetwork.PlayerCache = new Dictionary<string, PlayerNetwork.PlayerDataCache>();
		PlayerControl.AllPlayers = new List<PlayerControl>();
		Settings.Reset();
		InfoDisplay.Init();
		PlayerControl.LocalPlayerSpawned = null;
		MapManager.SceneChanged = null;
		StateManager.OnNetworkEvent = null;
		MapManager.OnMapChangeFinished = null;
		GameplayManager.OnGameStateChanged = null;
		GameplayManager.OnGenereChanged = null;
		PlatformSetup.OnLoggedIn = null;
		InputManager.OnInputMethodChanged = null;
		InputManager.OnBindingChanged = null;
		VoteManager.OnVotesChanged = null;
		NetworkManager.JoinedRoom = null;
		NetworkManager.LeftRoom = null;
		NetworkManager.OnRoomPlayersChanged = null;
		TutorialManager.OnStepChanged = null;
		Progression.OnPrestige = null;
		RaidManager.OnEncounterPrepared = null;
		RaidManager.OnEncounterStarted = null;
		RaidManager.OnEncounterReset = null;
		RaidManager.OnEncounterEnded = null;
		DelayEffectNode.CleanDelayList();
		TerrainObject.terrainObjects.Clear();
		LibraryManager.DidLoad = false;
		GameplayManager.IsChallengeActive = false;
		GameplayManager.Challenge = null;
		GreyscaleAreas.Init();
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00051E1C File Offset: 0x0005001C
	public GlobalSetup()
	{
	}

	// Token: 0x04000A26 RID: 2598
	public static bool StartedExternal;

	// Token: 0x04000A27 RID: 2599
	public static GlobalSetup instance;
}
