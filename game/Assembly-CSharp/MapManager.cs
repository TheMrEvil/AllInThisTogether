using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AtmosphericHeightFog;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Token: 0x020000C2 RID: 194
public class MapManager : MonoBehaviour
{
	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0003BABC File Offset: 0x00039CBC
	public static string LobbySceneName
	{
		get
		{
			return "LibraryV2";
		}
	}

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0003BAC4 File Offset: 0x00039CC4
	public static bool InLobbyScene
	{
		get
		{
			return SceneManager.GetActiveScene().name == "LibraryV2" || SceneManager.GetActiveScene().buildIndex == 0;
		}
	}

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0003BAFC File Offset: 0x00039CFC
	public static string CurrentSceneName
	{
		get
		{
			return SceneManager.GetActiveScene().name;
		}
	}

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0003BB16 File Offset: 0x00039D16
	public static bool AllPlayersLoaded
	{
		get
		{
			return PhotonNetwork.OfflineMode || MapManager.instance.PlayersLoaded.Count >= PlayerControl.PlayerCount;
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0003BB3C File Offset: 0x00039D3C
	private void Awake()
	{
		MapManager.instance = this;
		MapManager.Initialized = false;
		MapManager.UsedMaps = new List<string>();
		MapManager.UsedVignettes = new List<string>();
		MapManager.LastVignettes = new List<string>();
		this.view = base.GetComponent<PhotonView>();
		if (!MapManager.Initialized)
		{
			MapManager.Initialized = true;
			SceneManager.sceneLoaded -= MapManager.SceneChangedData;
			SceneManager.sceneLoaded += MapManager.SceneChangedData;
		}
		MapManager.ApplyCameraEffects();
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0003BBB3 File Offset: 0x00039DB3
	public static void ReturnToLibrary()
	{
		MapManager.instance.ChangeLevelSequence(MapManager.LobbySceneName);
		if (TutorialManager.InTutorial)
		{
			InfoDisplay.SetText("Tutorial Complete", 3f, InfoArea.Title);
			TutorialManager.ResetTutorial();
		}
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0003BBE0 File Offset: 0x00039DE0
	public static void SetupSeed(int seed, int counter = 0)
	{
		MapManager.rand = new System.Random(seed);
		MapManager.instance.RandomGens.Clear();
		MapManager.RandomCounter = 0;
		for (int i = 0; i < counter; i++)
		{
			MapManager.GetRandom(0, 0);
		}
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0003BC24 File Offset: 0x00039E24
	public static int GetRandom(int min, int max)
	{
		MapManager.RandomCounter++;
		int num = MapManager.rand.Next(min, max);
		MapManager.instance.RandomGens.Add(new Vector2((float)MapManager.RandomCounter, (float)num));
		if (PhotonNetwork.IsMasterClient)
		{
			MapManager.instance.view.RPC("SyncRandomCounter", RpcTarget.Others, new object[]
			{
				MapManager.RandomCounter
			});
		}
		return num;
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x0003BC98 File Offset: 0x00039E98
	[PunRPC]
	private void SyncRandomCounter(int counter)
	{
		for (int i = MapManager.RandomCounter; i < counter; i++)
		{
			MapManager.GetRandom(0, 1);
		}
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0003BCBD File Offset: 0x00039EBD
	private static bool IsLobbyScene(string sceneName)
	{
		return sceneName == MapManager.LobbySceneName;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0003BCCA File Offset: 0x00039ECA
	private static void MapRemained()
	{
		Action sceneChanged = MapManager.SceneChanged;
		if (sceneChanged != null)
		{
			sceneChanged();
		}
		TomeTerrain.ApplyLayer(GameplayManager.GenreProgress);
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0003BCE8 File Offset: 0x00039EE8
	private static void SceneChangedData(Scene scene, LoadSceneMode mode)
	{
		if (mode == LoadSceneMode.Additive)
		{
			return;
		}
		if (MapManager.instance != null)
		{
			MapManager.instance.StopAllCoroutines();
		}
		Action sceneChanged = MapManager.SceneChanged;
		if (sceneChanged != null)
		{
			sceneChanged();
		}
		if (PlayerControl.myInstance != null)
		{
			PlayerControl.MyCamera.enabled = true;
			PlayerControl.myInstance.Audio.Listener.enabled = true;
		}
		MapManager.ApplyCameraEffects();
		SpawnZone.CurZones.Clear();
		if (MapManager.IsLobbyScene(scene.name))
		{
			GameplayManager.instance.GenreBindings = new Augments();
		}
		if (MapManager.InLobbyScene)
		{
			MapManager.Reset(true);
			ActionPool.ClearAll();
			return;
		}
		UnityMainThreadDispatcher.Instance().Invoke(new Action(MapManager.instance.ConfirmMapChanged), 1f);
		if (UnityEngine.Object.FindObjectOfType<VignetteControl>() == null)
		{
			MapManager.UsedMaps.Add(MapManager.CurrentSceneName);
			MapManager.LastMap = MapManager.CurrentSceneName;
		}
		else
		{
			MapManager.UsedVignettes.Add(MapManager.CurrentSceneName);
			MapManager.LastVignettes.Add(MapManager.CurrentSceneName);
			if (MapManager.LastVignettes.Count > 8)
			{
				MapManager.LastVignettes.RemoveAt(0);
			}
		}
		TomeTerrain.ApplyLayer(GameplayManager.GenreProgress);
		UnityEngine.Debug.Log("Scene Changed to " + scene.name);
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0003BE2C File Offset: 0x0003A02C
	public void ChangeLevelSequence(string scene)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		if (scene.Length == 0)
		{
			scene = "Fantasy";
		}
		GameState newState = GameState.Reward_Traveling;
		if (MapManager.InLobbyScene)
		{
			newState = GameState.Hub_Traveling;
		}
		if (GameplayManager.instance.CurrentState == GameState.Vignette_PreWait)
		{
			newState = GameState.Vignette_Traveling;
		}
		if (MapManager.IsLobbyScene(scene))
		{
			newState = GameState.Post_Traveling;
		}
		GameplayManager.instance.UpdateGameState(newState);
		this.view.RPC("ChangeLevelNetwork", RpcTarget.All, new object[]
		{
			scene
		});
		if (this.sceneRoutine != null)
		{
			base.StopCoroutine(this.sceneRoutine);
		}
		this.sceneRoutine = base.StartCoroutine(this.LoadSceneDelayed(scene, PostFXManager.instance.SketchTransitionDuration));
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0003BED0 File Offset: 0x0003A0D0
	[PunRPC]
	private void ChangeLevelNetwork(string scene)
	{
		if (PanelManager.CurPanel == PanelType.Pause)
		{
			PanelManager.instance.PopPanel();
		}
		this.PlayersLoaded.Clear();
		if (PanelManager.CurPanel == PanelType.PostGame)
		{
			PanelManager.instance.PopPanel();
		}
		AugmentsPanel.TryClose();
		EntityControl.ClearForSceneChange();
		PostFXManager.instance.ActivateSketch(!MapManager.InLobbyScene || RaidManager.IsInRaid, true);
		if (RaidManager.IsInRaid && MapManager.IsLobbyScene(scene))
		{
			LibraryManager.WantAntechamberSpawn = true;
		}
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0003BF46 File Offset: 0x0003A146
	private IEnumerator LoadSceneDelayed(string scene, float delay)
	{
		yield return new WaitForSeconds(delay);
		PhotonNetwork.LoadLevel(scene);
		yield break;
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0003BF5C File Offset: 0x0003A15C
	public void ConfirmMapChanged()
	{
		this.view.RPC("MapChangeConfirmNetwork", RpcTarget.All, new object[]
		{
			PlayerControl.myInstance.ViewID
		});
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0003BF87 File Offset: 0x0003A187
	[PunRPC]
	private void MapChangeConfirmNetwork(int id)
	{
		if (!this.PlayersLoaded.Contains(id))
		{
			this.PlayersLoaded.Add(id);
		}
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0003BFA4 File Offset: 0x0003A1A4
	public void GoToVignette()
	{
		GenreVignetteNode vignette = GameplayManager.CurTomeRoot.GetVignette(WaveManager.CurrentWave, WaveManager.instance.AppendixLevel);
		if (vignette == null)
		{
			UnityEngine.Debug.LogError("Couldn't find Vignette Selector in Tome Graph! - " + GameplayManager.CurTomeRoot.Name + ": " + string.Format("{0} (Ap:{1}", WaveManager.CurrentWave, WaveManager.instance.AppendixLevel));
			return;
		}
		Vignette vignette2 = vignette.GetVignette();
		if (vignette2 == null)
		{
			UnityEngine.Debug.LogError("No Vignette matched filtering data!");
			return;
		}
		this.ChangeMap(vignette2.Scene.SceneName);
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0003C03C File Offset: 0x0003A23C
	public void ChangeMap(string mapID)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("MapSelectedNetwork", RpcTarget.All, new object[]
		{
			mapID
		});
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0003C061 File Offset: 0x0003A261
	[PunRPC]
	private void MapSelectedNetwork(string mapID)
	{
		this.ChangeLevelSequence(mapID);
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0003C06A File Offset: 0x0003A26A
	public void StayMap()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		GameplayManager.instance.UpdateGameState(GameState.Reward_Traveling);
		this.view.RPC("MapStayNetwork", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0003C096 File Offset: 0x0003A296
	[PunRPC]
	private void MapStayNetwork()
	{
		PostFXManager.instance.ActivateSketch(true, true);
		base.StartCoroutine("MapStayEnd");
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0003C0B0 File Offset: 0x0003A2B0
	private IEnumerator MapStayEnd()
	{
		yield return new WaitForSeconds(4f);
		MapManager.MapRemained();
		PlayerControl.myInstance.ResetPosition(false);
		yield break;
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0003C0B8 File Offset: 0x0003A2B8
	public void RequestNook()
	{
		this.view.RPC("RequestNookNetwork", RpcTarget.MasterClient, new object[]
		{
			PlayerControl.myInstance.view.OwnerActorNr
		});
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0003C0E8 File Offset: 0x0003A2E8
	[PunRPC]
	private void RequestNookNetwork(int playerID)
	{
		int availableNookID = NookManager.instance.GetAvailableNookID();
		if (availableNookID < 0)
		{
			return;
		}
		this.view.RPC("AssignNook", RpcTarget.All, new object[]
		{
			playerID,
			availableNookID
		});
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0003C130 File Offset: 0x0003A330
	[PunRPC]
	private void AssignNook(int PlayerID, int NookID)
	{
		PlayerControl player = PlayerControl.GetPlayer(PlayerID);
		NookManager.instance.AssignNook(player, NookID);
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0003C150 File Offset: 0x0003A350
	public void SendNookLayout(int nookID, string layout)
	{
		this.view.RPC("NookLayoutNetwork", RpcTarget.Others, new object[]
		{
			nookID,
			layout
		});
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0003C176 File Offset: 0x0003A376
	[PunRPC]
	private void NookLayoutNetwork(int nookID, string layout)
	{
		if (NookManager.instance == null)
		{
			return;
		}
		NookManager.instance.LoadNookLayout(nookID, layout);
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0003C194 File Offset: 0x0003A394
	public void SendNookToPlayer(Player plr, int playerID, int nookID, string layout)
	{
		this.view.RPC("AssignNook", plr, new object[]
		{
			playerID,
			nookID
		});
		this.view.RPC("NookLayoutNetwork", plr, new object[]
		{
			nookID,
			layout
		});
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0003C1EF File Offset: 0x0003A3EF
	public void SetLibWeather(int index)
	{
		this.view.RPC("SetLibWeatherNetwork", RpcTarget.Others, new object[]
		{
			index
		});
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0003C211 File Offset: 0x0003A411
	[PunRPC]
	private void SetLibWeatherNetwork(int index)
	{
		if (Library_Weather.instance != null)
		{
			Library_Weather.instance.SetWeather(index, false);
		}
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0003C22C File Offset: 0x0003A42C
	public void TryRollDice()
	{
		int ownerActorNr = PlayerControl.myInstance.view.OwnerActorNr;
		this.view.RPC("TryRollDiceNetwork", RpcTarget.MasterClient, new object[]
		{
			ownerActorNr
		});
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0003C26C File Offset: 0x0003A46C
	[PunRPC]
	private void TryRollDiceNetwork(int playerID)
	{
		if (!Library_DiceGame.CanRoll())
		{
			return;
		}
		int num = UnityEngine.Random.Range(0, int.MaxValue);
		this.view.RPC("RollDiceNetwork", RpcTarget.All, new object[]
		{
			playerID,
			num
		});
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x0003C2B6 File Offset: 0x0003A4B6
	[PunRPC]
	private void RollDiceNetwork(int playerID, int seed)
	{
		if (Library_DiceGame.instance == null)
		{
			return;
		}
		Library_DiceGame.instance.Roll(playerID, seed);
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0003C2D2 File Offset: 0x0003A4D2
	public void LibraryTargetWantStart()
	{
		this.view.RPC("LibraryTargetWantStartNetwork", RpcTarget.MasterClient, Array.Empty<object>());
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0003C2EA File Offset: 0x0003A4EA
	[PunRPC]
	private void LibraryTargetWantStartNetwork()
	{
		if (Library_TargetPractice.CanStart())
		{
			this.view.RPC("LibraryTargetsNetwork", RpcTarget.All, new object[]
			{
				UnityEngine.Random.Range(0, int.MaxValue)
			});
		}
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x0003C31D File Offset: 0x0003A51D
	[PunRPC]
	private void LibraryTargetsNetwork(int seed)
	{
		if (Library_TargetPractice.instance == null)
		{
			return;
		}
		Library_TargetPractice.instance.StartEventNetworked(seed);
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x0003C338 File Offset: 0x0003A538
	public void LibraryCrackLevel(int level)
	{
		this.view.RPC("LibraryCrackLevelNetwork", RpcTarget.All, new object[]
		{
			level
		});
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0003C35A File Offset: 0x0003A55A
	[PunRPC]
	private void LibraryCrackLevelNetwork(int level)
	{
		if (Library_TornCracks.instance == null)
		{
			return;
		}
		Library_TornCracks.instance.SetLevel(level);
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x0003C375 File Offset: 0x0003A575
	public void UseNookItem(Vector3 pos, int seed)
	{
		this.view.RPC("UseNookItemNetwork", RpcTarget.All, new object[]
		{
			pos,
			seed
		});
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x0003C3A0 File Offset: 0x0003A5A0
	[PunRPC]
	private void UseNookItemNetwork(Vector3 pos, int seed)
	{
		NookNetItem.UseNetwork(pos, seed);
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x0003C3A9 File Offset: 0x0003A5A9
	public void RequestNextSecretRoomStage(LibrarySecretRoom.PuzzleState stage)
	{
		this.view.RPC("RequestNextSecretRoomStageNetwork", RpcTarget.MasterClient, new object[]
		{
			stage
		});
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0003C3CB File Offset: 0x0003A5CB
	public void ResetSecretRoomState()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			this.view.RPC("SendNextSecretRoomStage", RpcTarget.All, new object[]
			{
				LibrarySecretRoom.PuzzleState.Start,
				false
			});
		}
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x0003C3FD File Offset: 0x0003A5FD
	[PunRPC]
	private void RequestNextSecretRoomStageNetwork(LibrarySecretRoom.PuzzleState stage)
	{
		if (!LibrarySecretRoom.CanMoveNextState(stage))
		{
			return;
		}
		stage++;
		this.view.RPC("SendNextSecretRoomStage", RpcTarget.All, new object[]
		{
			stage,
			true
		});
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x0003C436 File Offset: 0x0003A636
	public void SyncSecretRoomState(Player plr, LibrarySecretRoom.PuzzleState state)
	{
		this.view.RPC("SendNextSecretRoomStage", plr, new object[]
		{
			state,
			false
		});
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x0003C461 File Offset: 0x0003A661
	[PunRPC]
	private void SendNextSecretRoomStage(LibrarySecretRoom.PuzzleState stage, bool doTransition)
	{
		UnityEngine.Debug.Log("Got secret room state from Host -> " + stage.ToString());
		LibrarySecretRoom.instance.UpdateStage(stage, doTransition);
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x0003C48C File Offset: 0x0003A68C
	public void TryOpenRaidDoors(bool myriad, bool verse, bool horizon, bool mHM, bool vHM, bool hHM)
	{
		if (Library_RaidControl.instance == null)
		{
			return;
		}
		this.view.RPC("TryOpenRaidDoorsNetwork", RpcTarget.All, new object[]
		{
			myriad,
			verse,
			horizon,
			mHM,
			vHM,
			hHM
		});
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x0003C4F7 File Offset: 0x0003A6F7
	[PunRPC]
	private void TryOpenRaidDoorsNetwork(bool myriad, bool verse, bool horizon, bool mHM, bool vHM, bool hHM)
	{
		Library_RaidControl.instance.UpdateRaidUnlocked(myriad, verse, horizon, mHM, vHM, hHM);
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x0003C50C File Offset: 0x0003A70C
	public static void SelectBiome(int seed)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		List<GameMap.Biome> list = new List<GameMap.Biome>();
		foreach (GameMap.Biome biome in MapManager.ValidBiomes)
		{
			if (biome != MapManager.TomeBiome)
			{
				list.Add(biome);
			}
		}
		if (seed == -1)
		{
			seed = UnityEngine.Random.Range(0, 9999999);
		}
		System.Random random = new System.Random(seed);
		MapManager.ChangeBiome(list[random.Next(0, list.Count)]);
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0003C581 File Offset: 0x0003A781
	public static void ChangeBiome(GameMap.Biome biome)
	{
		MapManager.TomeBiome = biome;
		UnityEngine.Debug.Log("Map Biome: " + MapManager.TomeBiome.ToString());
		MapManager.instance.SyncBiome();
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x0003C5B2 File Offset: 0x0003A7B2
	public void SyncBiome()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		this.view.RPC("SyncBiomeNetwork", RpcTarget.All, new object[]
		{
			(int)MapManager.TomeBiome
		});
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0003C5E0 File Offset: 0x0003A7E0
	[PunRPC]
	private void SyncBiomeNetwork(int biome)
	{
		MapManager.TomeBiome = (GameMap.Biome)biome;
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0003C5E8 File Offset: 0x0003A7E8
	public void SpawnDPSTrigger()
	{
		List<SpawnPoint> validSpawns = SpawnPoint.GetValidSpawns(SpawnType.AI_Ground, EnemyLevel.Boss);
		if (validSpawns.Count <= 0)
		{
			return;
		}
		SpawnPoint spawnPoint = validSpawns[0];
		Transform transform = spawnPoint.transform;
		Vector3 forward = transform.forward;
		Vector3 vector = AIManager.NearestNavPoint(spawnPoint.point, 10f);
		vector -= forward * 8f;
		this.curDPSTrigger = UnityEngine.Object.Instantiate<GameObject>(this.DPSSpawner, vector, transform.rotation);
		SimpleDiagetic component = this.curDPSTrigger.GetComponent<SimpleDiagetic>();
		component.OnInteract = (Action)Delegate.Combine(component.OnInteract, new Action(this.TrySpawnDPSMeter));
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x0003C683 File Offset: 0x0003A883
	private void TrySpawnDPSMeter()
	{
		this.view.RPC("TrySpawnDPSMeterNetwork", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x0003C69C File Offset: 0x0003A89C
	[PunRPC]
	private void TrySpawnDPSMeterNetwork()
	{
		if (this.curDPSTrigger == null)
		{
			return;
		}
		UnityEvent onActivate = this.curDPSTrigger.GetComponent<SimpleDiagetic>().OnActivate;
		if (onActivate != null)
		{
			onActivate.Invoke();
		}
		this.curDPSTrigger.GetComponent<SimpleDiagetic>().Deactivate();
		UnityEngine.Object.Destroy(this.curDPSTrigger, 2.5f);
		this.curDPSTrigger = null;
		this.SpawnDPSMeter();
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0003C700 File Offset: 0x0003A900
	private void SpawnDPSMeter()
	{
		List<SpawnPoint> validSpawns = SpawnPoint.GetValidSpawns(SpawnType.AI_Ground, EnemyLevel.Boss);
		if (validSpawns.Count > 0)
		{
			SpawnPoint spawnPoint = validSpawns[0];
			Transform transform = spawnPoint.transform;
			Vector3 forward = transform.forward;
			Vector3 a = AIManager.NearestNavPoint(spawnPoint.point, 10f);
			a -= forward * 15f;
			UnityEngine.Object.Instantiate<GameObject>(this.DPSMeter, AIManager.NearestNavPoint(a - forward * 5f, 10f) + Vector3.up * 4.5f, transform.rotation);
			Vector3 vector = AIManager.NearestNavPoint(a + transform.right * 12f, -1f);
			if (PhotonNetwork.IsMasterClient)
			{
				AIManager.SpawnAIExplicit("Unique/TDummy_Elite", vector, Quaternion.Euler(0f, -25f, 0f) * forward);
			}
			vector = AIManager.NearestNavPoint(a - transform.right * 14f, -1f);
			Vector3 position = AIManager.NearestNavPoint(vector - transform.right * 4f - transform.forward * 1f, -1f);
			Vector3 position2 = AIManager.NearestNavPoint(vector + transform.right * 3f - transform.forward * 3f, -1f);
			if (PhotonNetwork.IsMasterClient)
			{
				AIManager.SpawnAIExplicit("Splice/TDummy_Splice", vector, Quaternion.Euler(0f, 25f, 0f) * forward);
				AIManager.SpawnAIExplicit("Tangent/TDummy_Tangent", position, Quaternion.Euler(0f, 25f, 0f) * forward);
				AIManager.SpawnAIExplicit("Raving/TDummy_Raving", position2, Quaternion.Euler(0f, 25f, 0f) * forward);
			}
		}
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0003C8F8 File Offset: 0x0003AAF8
	public static void Reset(bool includeVignettes)
	{
		MapManager.UsedMaps.Clear();
		MapManager.UsedMaps.Add(MapManager.CurrentSceneName);
		MapManager.LastMap = MapManager.CurrentSceneName;
		if (includeVignettes)
		{
			MapManager.UsedVignettes.Clear();
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x0003C92C File Offset: 0x0003AB2C
	public static void ApplyCameraEffects()
	{
		if (PlayerControl.MyCamera == null)
		{
			return;
		}
		HeightFogGlobal heightFogGlobal = UnityEngine.Object.FindObjectOfType<HeightFogGlobal>();
		if (heightFogGlobal == null)
		{
			return;
		}
		heightFogGlobal.mainCamera = PlayerControl.MyCamera;
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0003C962 File Offset: 0x0003AB62
	public MapManager()
	{
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x0003C980 File Offset: 0x0003AB80
	// Note: this type is marked as 'beforefieldinit'.
	static MapManager()
	{
	}

	// Token: 0x0400076A RID: 1898
	private static bool Initialized;

	// Token: 0x0400076B RID: 1899
	public static MapManager instance;

	// Token: 0x0400076C RID: 1900
	public static Action SceneChanged;

	// Token: 0x0400076D RID: 1901
	public static Action OnMapChangeFinished;

	// Token: 0x0400076E RID: 1902
	public static GameMap.Biome TomeBiome;

	// Token: 0x0400076F RID: 1903
	public static GameMap.Biome[] ValidBiomes = new GameMap.Biome[]
	{
		GameMap.Biome.Forest,
		GameMap.Biome.Desert,
		GameMap.Biome.Snow,
		GameMap.Biome.Cave,
		GameMap.Biome.Island
	};

	// Token: 0x04000770 RID: 1904
	private PhotonView view;

	// Token: 0x04000771 RID: 1905
	public static List<string> UsedMaps;

	// Token: 0x04000772 RID: 1906
	public static string LastMap;

	// Token: 0x04000773 RID: 1907
	public static List<string> UsedVignettes;

	// Token: 0x04000774 RID: 1908
	public static List<string> LastVignettes;

	// Token: 0x04000775 RID: 1909
	[NonSerialized]
	private List<int> PlayersLoaded = new List<int>();

	// Token: 0x04000776 RID: 1910
	[NonSerialized]
	public List<Vector2> RandomGens = new List<Vector2>();

	// Token: 0x04000777 RID: 1911
	public GameObject DPSSpawner;

	// Token: 0x04000778 RID: 1912
	public GameObject DPSMeter;

	// Token: 0x04000779 RID: 1913
	private GameObject curDPSTrigger;

	// Token: 0x0400077A RID: 1914
	private static System.Random rand = new System.Random();

	// Token: 0x0400077B RID: 1915
	public static int RandomCounter;

	// Token: 0x0400077C RID: 1916
	private Coroutine sceneRoutine;

	// Token: 0x020004BB RID: 1211
	[CompilerGenerated]
	private sealed class <LoadSceneDelayed>d__37 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002281 RID: 8833 RVA: 0x000C7080 File Offset: 0x000C5280
		[DebuggerHidden]
		public <LoadSceneDelayed>d__37(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000C708F File Offset: 0x000C528F
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000C7094 File Offset: 0x000C5294
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
			PhotonNetwork.LoadLevel(scene);
			return false;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000C70E5 File Offset: 0x000C52E5
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000C70ED File Offset: 0x000C52ED
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x000C70F4 File Offset: 0x000C52F4
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400242D RID: 9261
		private int <>1__state;

		// Token: 0x0400242E RID: 9262
		private object <>2__current;

		// Token: 0x0400242F RID: 9263
		public float delay;

		// Token: 0x04002430 RID: 9264
		public string scene;
	}

	// Token: 0x020004BC RID: 1212
	[CompilerGenerated]
	private sealed class <MapStayEnd>d__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002287 RID: 8839 RVA: 0x000C70FC File Offset: 0x000C52FC
		[DebuggerHidden]
		public <MapStayEnd>d__45(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000C710B File Offset: 0x000C530B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000C7110 File Offset: 0x000C5310
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(4f);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			MapManager.MapRemained();
			PlayerControl.myInstance.ResetPosition(false);
			return false;
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x000C7165 File Offset: 0x000C5365
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000C716D File Offset: 0x000C536D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x000C7174 File Offset: 0x000C5374
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002431 RID: 9265
		private int <>1__state;

		// Token: 0x04002432 RID: 9266
		private object <>2__current;
	}
}
