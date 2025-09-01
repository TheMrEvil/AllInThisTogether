using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class NetworkManager : MonoBehaviourPunCallbacks
{
	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0004FE0A File Offset: 0x0004E00A
	// (set) Token: 0x06000C5D RID: 3165 RVA: 0x0004FE12 File Offset: 0x0004E012
	public GameObject loadingCamera
	{
		[CompilerGenerated]
		get
		{
			return this.<loadingCamera>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			this.<loadingCamera>k__BackingField = value;
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0004FE1B File Offset: 0x0004E01B
	public static string RoomCode
	{
		get
		{
			if (PhotonNetwork.InRoom)
			{
				return PhotonNetwork.CurrentRoom.Name;
			}
			return "Offline";
		}
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x0004FE34 File Offset: 0x0004E034
	public virtual void Awake()
	{
		NetworkManager.RegisterCustomTypes();
		NetworkManager.instance = this;
		PhotonNetwork.AutomaticallySyncScene = true;
		PhotonNetwork.MaxResendsBeforeDisconnect = 10;
		PhotonNetwork.SerializationRate = 5;
		PhotonNetwork.UseRpcMonoBehaviourCache = true;
		PhotonNetwork.MinimalTimeScaleToDispatchInFixedUpdate = 0f;
		NetworkManager.username = "User" + UnityEngine.Random.Range(0, int.MaxValue).ToString();
		this.view = base.GetComponent<PhotonView>();
		MapManager.SceneChanged = (Action)Delegate.Combine(MapManager.SceneChanged, new Action(this.Setup));
		this.Setup();
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x0004FEC3 File Offset: 0x0004E0C3
	private void Setup()
	{
		Camera loadingCamera = Scene_Settings.instance.LoadingCamera;
		this.loadingCamera = ((loadingCamera != null) ? loadingCamera.gameObject : null);
		if (PlayerControl.myInstance != null)
		{
			GameObject loadingCamera2 = this.loadingCamera;
			if (loadingCamera2 == null)
			{
				return;
			}
			loadingCamera2.SetActive(false);
		}
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x0004FEFF File Offset: 0x0004E0FF
	private IEnumerator Start()
	{
		PhotonNetwork.GameVersion = NetworkManager.GetVersionCode();
		this.ConnectToNetwork();
		yield return true;
		int @int = Settings.GetInt(SystemSetting.ServerRegion, 0);
		if (@int != 0)
		{
			this.SetRegion(@int);
		}
		if (GlobalSetup.StartedExternal)
		{
			UnityEngine.Debug.Log("Started External");
			this.WantOpenRoom = true;
			while (!PhotonNetwork.InLobby && !PhotonNetwork.OfflineMode)
			{
				yield return true;
			}
			yield return true;
			PhotonNetwork.JoinRoom("EDITOR_TEST", null);
		}
		yield break;
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0004FF0E File Offset: 0x0004E10E
	public void DebugDisconnect()
	{
		PhotonNetwork.Disconnect();
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0004FF18 File Offset: 0x0004E118
	public void TryJoinInvite(string code)
	{
		if (PhotonNetwork.InRoom)
		{
			if (PlayerControl.myInstance != null)
			{
				InfoDisplay.SetText("Can't join while in game!", 4f, InfoArea.DetailBottom);
			}
			return;
		}
		NetworkManager.AutoJoinCode = code;
		UnityEngine.Debug.Log("Current State: " + PhotonNetwork.NetworkClientState.ToString());
		if (!PhotonNetwork.IsConnectedAndReady || PhotonNetwork.NetworkClientState == ClientState.Disconnected || PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
		{
			if (!MainPanel.instance.InMP)
			{
				MainPanel.instance.MPOpen(NetworkManager.AutoJoinCode);
				return;
			}
			MainPanel.instance.FixConnectionAndJoin(NetworkManager.AutoJoinCode);
			return;
		}
		else
		{
			if (PhotonNetwork.IsConnected && !PhotonNetwork.InLobby && PhotonNetwork.NetworkClientState != ClientState.JoiningLobby)
			{
				PhotonNetwork.JoinLobby();
				return;
			}
			if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
			{
				this.TryJoinRoom(NetworkManager.AutoJoinCode);
				NetworkManager.AutoJoinCode = "";
			}
			return;
		}
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x0004FFF8 File Offset: 0x0004E1F8
	public void ConnectToNetwork()
	{
		if (!PhotonNetwork.IsConnected)
		{
			this.SetupAuth();
			PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = NetworkManager.GetVersionCode();
			if (GlobalSetup.StartedExternal || NetworkManager.AutoJoinCode.Length > 0)
			{
				this.GoOnline();
				return;
			}
		}
		else
		{
			UnityEngine.Debug.LogError("Already connected to network");
		}
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x0005004B File Offset: 0x0004E24B
	public void GoOffline()
	{
		base.StartCoroutine("GoOfflineSequence");
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00050059 File Offset: 0x0004E259
	private IEnumerator GoOfflineSequence()
	{
		if (PhotonNetwork.IsConnected)
		{
			PhotonNetwork.Disconnect();
		}
		while (PhotonNetwork.IsConnected)
		{
			yield return true;
		}
		PhotonNetwork.OfflineMode = true;
		yield break;
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x00050061 File Offset: 0x0004E261
	public void GoOnline()
	{
		PhotonNetwork.OfflineMode = false;
		PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = NetworkManager.Region;
		PhotonNetwork.ConnectUsingSettings();
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x00050083 File Offset: 0x0004E283
	public void Reconnect()
	{
		base.StartCoroutine("ReconnectSequence");
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x00050091 File Offset: 0x0004E291
	private IEnumerator ReconnectSequence()
	{
		yield return this.GoOfflineSequence();
		yield return true;
		this.GoOnline();
		yield break;
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x000500A0 File Offset: 0x0004E2A0
	private void SetupAuth()
	{
		AuthenticationValues authenticationValues = new AuthenticationValues();
		authenticationValues.AuthType = CustomAuthenticationType.Custom;
		authenticationValues.AddAuthParameter("user", PlatformSetup.UserID);
		authenticationValues.AddAuthParameter("username", PlatformSetup.Username);
		authenticationValues.UserId = (PlatformSetup.UserID.Contains("ANONYMOUS") ? ("ANON_" + UnityEngine.Random.Range(0, 999).ToString()) : PlatformSetup.UserID);
		PhotonNetwork.AuthValues = authenticationValues;
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x00050119 File Offset: 0x0004E319
	public override void OnConnectedToMaster()
	{
		if (!PhotonNetwork.OfflineMode)
		{
			PhotonNetwork.JoinLobby();
		}
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x00050128 File Offset: 0x0004E328
	public override void OnJoinedLobby()
	{
		if (NetworkManager.AutoJoinCode.Length > 0)
		{
			UnityEngine.Debug.Log("Attempting to autmatically join room: " + NetworkManager.AutoJoinCode);
			this.TryJoinRoom(NetworkManager.AutoJoinCode);
			NetworkManager.AutoJoinCode = "";
		}
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x00050160 File Offset: 0x0004E360
	public void CreateOrJoinRandom()
	{
		if (!PhotonNetwork.OfflineMode)
		{
			PhotonNetwork.JoinRandomRoom();
			return;
		}
		RoomOptions vellumRoomOptions = this.GetVellumRoomOptions();
		this.CreateNewGame(vellumRoomOptions);
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0005018C File Offset: 0x0004E38C
	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		UnityEngine.Debug.Log("JoinRandom Failed [" + returnCode.ToString() + "] : " + message);
		base.OnJoinRandomFailed(returnCode, message);
		RoomOptions vellumRoomOptions = this.GetVellumRoomOptions();
		MainPanel.instance.FailedToJoinRandom();
		this.CreateNewGame(vellumRoomOptions);
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x000501D8 File Offset: 0x0004E3D8
	public bool CreateNewGame(RoomOptions options)
	{
		string text = NetworkManager.GenerateInviteCode();
		int num = 0;
		while (this.roomNames.Contains(text) && num < 100)
		{
			text = NetworkManager.GenerateInviteCode();
			num++;
		}
		if (num >= 100)
		{
			UnityEngine.Debug.LogError("Could not create unique room ID after 100 attempts!");
			return false;
		}
		PhotonNetwork.CreateRoom(text, options, null, null);
		return true;
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x00050228 File Offset: 0x0004E428
	public void SetRegion(int regionID)
	{
		string region;
		switch (regionID)
		{
		case 0:
			region = "usw";
			break;
		case 1:
			region = "us";
			break;
		case 2:
			region = "eu";
			break;
		case 3:
			region = "au";
			break;
		case 4:
			region = "asia";
			break;
		case 5:
			region = "sa";
			break;
		default:
			region = "usw";
			break;
		}
		this.SetRegion(region);
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00050292 File Offset: 0x0004E492
	public void SetRegion(string regionCode)
	{
		NetworkManager.Region = regionCode;
		UnityEngine.Debug.Log("Set Server Region: " + NetworkManager.Region);
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x000502AE File Offset: 0x0004E4AE
	public void TryJoinRoom(string code)
	{
		PhotonNetwork.JoinRoom(code, null);
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x000502B8 File Offset: 0x0004E4B8
	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		UnityEngine.Debug.Log("RandomRoom Failed [" + returnCode.ToString() + "] : " + message);
		MainPanel.instance.JoinFailed(returnCode);
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x000502E4 File Offset: 0x0004E4E4
	public override void OnJoinedRoom()
	{
		UnityEngine.Debug.Log("- Joined Room (" + PhotonNetwork.CurrentRoom.Name + ") - Region: " + PhotonNetwork.CloudRegion);
		CanvasController.EnterGameplay();
		if (this.loadingCamera != null)
		{
			this.loadingCamera.SetActive(false);
		}
		if (!PhotonNetwork.OfflineMode)
		{
			PlatformSetup.SetSteamRoomCode();
		}
		if (PhotonNetwork.IsMasterClient)
		{
			if (PhotonNetwork.OfflineMode)
			{
				base.StartCoroutine("CreateRoomManager");
			}
			else
			{
				PhotonNetwork.InstantiateRoomObject("RoomManager", Vector3.zero, Quaternion.identity, 0, null);
			}
		}
		List<SpawnPoint> allSpawns = SpawnPoint.GetAllSpawns(SpawnType.Player, EnemyLevel.None);
		Transform transform = allSpawns[UnityEngine.Random.Range(0, allSpawns.Count)].transform;
		PhotonNetwork.Instantiate("Player", transform.position, transform.rotation, 0, null);
		PlayerNetwork.PlayerCache.Clear();
		Action joinedRoom = NetworkManager.JoinedRoom;
		if (joinedRoom == null)
		{
			return;
		}
		joinedRoom();
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x000503C4 File Offset: 0x0004E5C4
	private IEnumerator CreateRoomManager()
	{
		yield return new WaitForEndOfFrame();
		PhotonNetwork.InstantiateRoomObject("RoomManager", Vector3.zero, Quaternion.identity, 0, null);
		yield break;
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x000503CC File Offset: 0x0004E5CC
	public override void OnDisconnected(DisconnectCause cause)
	{
		if (cause == DisconnectCause.DisconnectByClientLogic || cause == DisconnectCause.ApplicationQuit || cause == DisconnectCause.DisconnectByServerLogic || cause == DisconnectCause.None)
		{
			return;
		}
		UnityEngine.Debug.Log("Disconnected from server. Reason: " + cause.ToString());
		ErrorPrompt.OpenPrompt("Lost connection to other Scribes!\n(Error Code: " + cause.ToString() + ")", null);
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0005042C File Offset: 0x0004E62C
	public override void OnLeftRoom()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (PhotonNetwork.OfflineMode)
		{
			PhotonNetwork.DestroyAll();
		}
		if (!Network_OnlineToggle.IsToggling)
		{
			CanvasController.LeaveGameplay();
		}
		if (this.loadingCamera != null)
		{
			this.loadingCamera.SetActive(true);
		}
		PlatformSetup.ClearSteamRoomCode();
		PlayerNetwork.PlayerCache.Clear();
		BindingsPanel.instance.Reset();
		GameplayManager.IsChallengeActive = false;
		Action leftRoom = NetworkManager.LeftRoom;
		if (leftRoom != null)
		{
			leftRoom();
		}
		if (!MapManager.InLobbyScene)
		{
			SceneControl.GoToLibrary();
		}
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x000504AE File Offset: 0x0004E6AE
	public override void OnPlayerEnteredRoom(Player plr)
	{
		Action onRoomPlayersChanged = NetworkManager.OnRoomPlayersChanged;
		if (onRoomPlayersChanged != null)
		{
			onRoomPlayersChanged();
		}
		NookManager.PlayerJoinedRoom(plr);
		Action<Player> onPlayerJoinedGame = NetworkManager.OnPlayerJoinedGame;
		if (onPlayerJoinedGame == null)
		{
			return;
		}
		onPlayerJoinedGame(plr);
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x000504D6 File Offset: 0x0004E6D6
	public override void OnPlayerLeftRoom(Player plr)
	{
		if (PhotonNetwork.InRoom)
		{
			Action onRoomPlayersChanged = NetworkManager.OnRoomPlayersChanged;
			if (onRoomPlayersChanged != null)
			{
				onRoomPlayersChanged();
			}
			NookManager.PlayerLeftRoom(plr);
			Action<Player> onPlayerLeftGame = NetworkManager.OnPlayerLeftGame;
			if (onPlayerLeftGame == null)
			{
				return;
			}
			onPlayerLeftGame(plr);
		}
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x00050505 File Offset: 0x0004E705
	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		if (PhotonNetwork.IsMasterClient)
		{
			PauseLobbyControl.instance.OnRoomSettingChanged();
		}
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00050518 File Offset: 0x0004E718
	public static string GetVersionCode()
	{
		return Application.version + NetworkManager.instance.versionAdd;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x0005052E File Offset: 0x0004E72E
	public void TryLeaveRoom()
	{
		if (PlayerControl.myInstance == null)
		{
			return;
		}
		PhotonNetwork.LeaveRoom(true);
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00050548 File Offset: 0x0004E748
	public static string GenerateInviteCode()
	{
		string text = "";
		for (int i = 0; i < NetworkManager.CODE_COUNT; i++)
		{
			text += NetworkManager.CodeCharacters[UnityEngine.Random.Range(0, NetworkManager.CodeCharacters.Count)];
		}
		return text;
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x00050590 File Offset: 0x0004E790
	public static bool IsValidCode(string code)
	{
		if (code.Length != NetworkManager.CODE_COUNT)
		{
			return false;
		}
		foreach (char c in code)
		{
			if (!NetworkManager.CodeCharacters.Contains(c.ToString() ?? ""))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x000505E8 File Offset: 0x0004E7E8
	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		this.roomNames.Clear();
		foreach (RoomInfo roomInfo in roomList)
		{
			this.roomNames.Add(roomInfo.Name);
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x0005064C File Offset: 0x0004E84C
	public void DebugLoadGraph()
	{
		XGraphCollector.CollectData();
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x00050653 File Offset: 0x0004E853
	private void OnDestroy()
	{
		MapManager.SceneChanged = (Action)Delegate.Remove(MapManager.SceneChanged, new Action(this.Setup));
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x00050675 File Offset: 0x0004E875
	private RoomOptions GetVellumRoomOptions()
	{
		return new RoomOptions
		{
			MaxPlayers = 4,
			IsVisible = this.WantOpenRoom
		};
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0005068F File Offset: 0x0004E88F
	private static void RegisterCustomTypes()
	{
		PhotonPeer.RegisterType(typeof(DamageInfo), 68, new SerializeStreamMethod(DamageInfo.SerializeDamageInfo), new DeserializeStreamMethod(DamageInfo.DeserializeDamageInfo));
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x000506BC File Offset: 0x0004E8BC
	public static string RegionCodeName(string region)
	{
		if (string.IsNullOrEmpty(region))
		{
			return "";
		}
		string result;
		if (!(region == "usw"))
		{
			if (!(region == "us"))
			{
				if (!(region == "eu"))
				{
					if (!(region == "au"))
					{
						if (!(region == "asia"))
						{
							if (!(region == "sa"))
							{
								result = "Unknown Region";
							}
							else
							{
								result = "South America";
							}
						}
						else
						{
							result = "Asia";
						}
					}
					else
					{
						result = "Australia";
					}
				}
				else
				{
					result = "Europe";
				}
			}
			else
			{
				result = "US East";
			}
		}
		else
		{
			result = "US West";
		}
		return result;
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0005075E File Offset: 0x0004E95E
	public NetworkManager()
	{
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0005077C File Offset: 0x0004E97C
	// Note: this type is marked as 'beforefieldinit'.
	static NetworkManager()
	{
	}

	// Token: 0x040009F0 RID: 2544
	public static NetworkManager instance;

	// Token: 0x040009F1 RID: 2545
	[CompilerGenerated]
	private GameObject <loadingCamera>k__BackingField;

	// Token: 0x040009F2 RID: 2546
	public static string username = "";

	// Token: 0x040009F3 RID: 2547
	public string versionAdd = "";

	// Token: 0x040009F4 RID: 2548
	public const int MAX_PLAYERS = 4;

	// Token: 0x040009F5 RID: 2549
	public bool WantOpenRoom;

	// Token: 0x040009F6 RID: 2550
	private static string Region = "usw";

	// Token: 0x040009F7 RID: 2551
	public bool DebugOfflineMode;

	// Token: 0x040009F8 RID: 2552
	public static Action JoinedRoom;

	// Token: 0x040009F9 RID: 2553
	public static Action LeftRoom;

	// Token: 0x040009FA RID: 2554
	public static Action OnRoomPlayersChanged;

	// Token: 0x040009FB RID: 2555
	public static Action<Player> OnPlayerLeftGame;

	// Token: 0x040009FC RID: 2556
	public static Action<Player> OnPlayerJoinedGame;

	// Token: 0x040009FD RID: 2557
	private GameObject player;

	// Token: 0x040009FE RID: 2558
	private bool initialCalibration;

	// Token: 0x040009FF RID: 2559
	public static int CODE_COUNT = 5;

	// Token: 0x04000A00 RID: 2560
	private static List<string> CodeCharacters = new List<string>
	{
		"B",
		"C",
		"D",
		"F",
		"G",
		"H",
		"J",
		"K",
		"L",
		"M",
		"N",
		"Q",
		"R",
		"S",
		"T",
		"V",
		"W",
		"X",
		"Y",
		"Z"
	};

	// Token: 0x04000A01 RID: 2561
	public static string AutoJoinCode = "";

	// Token: 0x04000A02 RID: 2562
	private PhotonView view;

	// Token: 0x04000A03 RID: 2563
	private List<string> roomNames = new List<string>();

	// Token: 0x02000504 RID: 1284
	[CompilerGenerated]
	private sealed class <Start>d__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002385 RID: 9093 RVA: 0x000CAD40 File Offset: 0x000C8F40
		[DebuggerHidden]
		public <Start>d__26(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000CAD4F File Offset: 0x000C8F4F
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000CAD54 File Offset: 0x000C8F54
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			NetworkManager networkManager = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				PhotonNetwork.GameVersion = NetworkManager.GetVersionCode();
				networkManager.ConnectToNetwork();
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			case 1:
			{
				this.<>1__state = -1;
				int @int = Settings.GetInt(SystemSetting.ServerRegion, 0);
				if (@int != 0)
				{
					networkManager.SetRegion(@int);
				}
				if (!GlobalSetup.StartedExternal)
				{
					return false;
				}
				UnityEngine.Debug.Log("Started External");
				networkManager.WantOpenRoom = true;
				break;
			}
			case 2:
				this.<>1__state = -1;
				break;
			case 3:
				this.<>1__state = -1;
				PhotonNetwork.JoinRoom("EDITOR_TEST", null);
				return false;
			default:
				return false;
			}
			if (PhotonNetwork.InLobby || PhotonNetwork.OfflineMode)
			{
				this.<>2__current = true;
				this.<>1__state = 3;
				return true;
			}
			this.<>2__current = true;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x000CAE3A File Offset: 0x000C903A
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000CAE42 File Offset: 0x000C9042
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x000CAE49 File Offset: 0x000C9049
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002589 RID: 9609
		private int <>1__state;

		// Token: 0x0400258A RID: 9610
		private object <>2__current;

		// Token: 0x0400258B RID: 9611
		public NetworkManager <>4__this;
	}

	// Token: 0x02000505 RID: 1285
	[CompilerGenerated]
	private sealed class <GoOfflineSequence>d__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600238B RID: 9099 RVA: 0x000CAE51 File Offset: 0x000C9051
		[DebuggerHidden]
		public <GoOfflineSequence>d__31(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000CAE60 File Offset: 0x000C9060
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000CAE64 File Offset: 0x000C9064
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
				if (PhotonNetwork.IsConnected)
				{
					PhotonNetwork.Disconnect();
				}
			}
			if (!PhotonNetwork.IsConnected)
			{
				PhotonNetwork.OfflineMode = true;
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x000CAEC0 File Offset: 0x000C90C0
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000CAEC8 File Offset: 0x000C90C8
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x000CAECF File Offset: 0x000C90CF
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400258C RID: 9612
		private int <>1__state;

		// Token: 0x0400258D RID: 9613
		private object <>2__current;
	}

	// Token: 0x02000506 RID: 1286
	[CompilerGenerated]
	private sealed class <ReconnectSequence>d__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002391 RID: 9105 RVA: 0x000CAED7 File Offset: 0x000C90D7
		[DebuggerHidden]
		public <ReconnectSequence>d__34(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000CAEE6 File Offset: 0x000C90E6
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000CAEE8 File Offset: 0x000C90E8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			NetworkManager networkManager = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = networkManager.GoOfflineSequence();
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				networkManager.GoOnline();
				return false;
			default:
				return false;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x000CAF5D File Offset: 0x000C915D
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000CAF65 File Offset: 0x000C9165
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x000CAF6C File Offset: 0x000C916C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400258E RID: 9614
		private int <>1__state;

		// Token: 0x0400258F RID: 9615
		private object <>2__current;

		// Token: 0x04002590 RID: 9616
		public NetworkManager <>4__this;
	}

	// Token: 0x02000507 RID: 1287
	[CompilerGenerated]
	private sealed class <CreateRoomManager>d__46 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002397 RID: 9111 RVA: 0x000CAF74 File Offset: 0x000C9174
		[DebuggerHidden]
		public <CreateRoomManager>d__46(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000CAF83 File Offset: 0x000C9183
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000CAF88 File Offset: 0x000C9188
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForEndOfFrame();
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			PhotonNetwork.InstantiateRoomObject("RoomManager", Vector3.zero, Quaternion.identity, 0, null);
			return false;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x000CAFDF File Offset: 0x000C91DF
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000CAFE7 File Offset: 0x000C91E7
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x000CAFEE File Offset: 0x000C91EE
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002591 RID: 9617
		private int <>1__state;

		// Token: 0x04002592 RID: 9618
		private object <>2__current;
	}
}
