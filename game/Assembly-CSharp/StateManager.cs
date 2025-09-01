using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class StateManager : MonoBehaviour
{
	// Token: 0x06000CA9 RID: 3241 RVA: 0x000516A8 File Offset: 0x0004F8A8
	private void Awake()
	{
		StateManager.instance = this;
		this.view = base.GetComponent<PhotonView>();
		base.InvokeRepeating("CheckAFKTimer", 1f, 1f);
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x000516D1 File Offset: 0x0004F8D1
	private void Start()
	{
		StateManager.SetRoomOpen(PhotonNetwork.CurrentRoom.IsVisible);
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x000516E2 File Offset: 0x0004F8E2
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
			this.InvokeNetEvent();
		}
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x000516F3 File Offset: 0x0004F8F3
	private void InvokeNetEvent()
	{
		this.view.RPC("InvokeNetEventNetworked", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x0005170B File Offset: 0x0004F90B
	[PunRPC]
	private void InvokeNetEventNetworked()
	{
		Action onNetworkEvent = StateManager.OnNetworkEvent;
		if (onNetworkEvent == null)
		{
			return;
		}
		onNetworkEvent();
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x0005171C File Offset: 0x0004F91C
	public void StartedFishing(Vector3 landPos)
	{
		this.view.RPC("StartedFishingNetwork", RpcTarget.All, new object[]
		{
			PlayerControl.MyViewID,
			landPos
		});
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x0005174B File Offset: 0x0004F94B
	[PunRPC]
	public void StartedFishingNetwork(int playerID, Vector3 pos)
	{
		if (Fishing.instance == null)
		{
			return;
		}
		Fishing.instance.ApplyFishing(PlayerControl.GetPlayerFromViewID(playerID), pos);
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x0005176C File Offset: 0x0004F96C
	public void StopFishing()
	{
		this.view.RPC("StopFishingNetwork", RpcTarget.All, new object[]
		{
			PlayerControl.MyViewID
		});
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00051792 File Offset: 0x0004F992
	[PunRPC]
	public void StopFishingNetwork(int playerID)
	{
		if (Fishing.instance == null)
		{
			return;
		}
		Fishing.instance.RemoveFishing(PlayerControl.GetPlayerFromViewID(playerID));
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x000517B2 File Offset: 0x0004F9B2
	public static void VignetteAction(string ActionID)
	{
		StateManager.instance.ActivateVignetteAction(ActionID);
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x000517BF File Offset: 0x0004F9BF
	public void VignetteOpenExit()
	{
		this.view.RPC("VignetteOpenExitNetworked", RpcTarget.All, Array.Empty<object>());
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x000517D8 File Offset: 0x0004F9D8
	private void ActivateVignetteAction(string actionID)
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		int num = (myInstance != null) ? myInstance.ViewID : -1;
		this.view.RPC("TryVignetteActionNetworked", RpcTarget.MasterClient, new object[]
		{
			actionID,
			num
		});
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x0005181C File Offset: 0x0004FA1C
	[PunRPC]
	private void TryVignetteActionNetworked(string actionID, int sourceID)
	{
		if (VignetteControl.instance == null || !VignetteControl.instance.CanActivate(actionID, sourceID))
		{
			return;
		}
		int num = UnityEngine.Random.Range(0, 99999999);
		this.view.RPC("VignetteActionNetworked", RpcTarget.All, new object[]
		{
			actionID,
			sourceID,
			num
		});
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x0005187E File Offset: 0x0004FA7E
	[PunRPC]
	private void VignetteActionNetworked(string actionID, int sourceID, int seed)
	{
		if (VignetteControl.instance == null)
		{
			return;
		}
		VignetteControl.instance.OnActionTaken(actionID, sourceID, seed);
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x0005189B File Offset: 0x0004FA9B
	[PunRPC]
	private void VignetteOpenExitNetworked()
	{
		VignetteControl vignetteControl = VignetteControl.instance;
		if (vignetteControl == null)
		{
			return;
		}
		vignetteControl.ActivateExit();
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x000518AC File Offset: 0x0004FAAC
	public static bool IsInAFKDanger
	{
		get
		{
			return PhotonNetwork.InRoom && !PhotonNetwork.OfflineMode && StateManager.TimeToAFKKick <= 30f;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x000518CD File Offset: 0x0004FACD
	public static float TimeToAFKKick
	{
		get
		{
			if (StateManager.instance.isRoomAFKKicking)
			{
				return 120f - StateManager.instance.afkTimer;
			}
			if (MapManager.InLobbyScene)
			{
				return 3600f - StateManager.instance.afkTimer;
			}
			return 100000f;
		}
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x0005190C File Offset: 0x0004FB0C
	private void CheckAFKTimer()
	{
		if (!PhotonNetwork.InRoom || PhotonNetwork.OfflineMode)
		{
			this.afkTimer = 0f;
			return;
		}
		this.isRoomAFKKicking = StateManager.GetBool("KickAFK", false);
		if (!MapManager.InLobbyScene && !this.isRoomAFKKicking)
		{
			this.afkTimer = 0f;
		}
		if (RaidManager.IsInRaid)
		{
			this.afkTimer = 0f;
		}
		if (!StateManager.HasActed)
		{
			this.afkTimer += 1f;
		}
		else
		{
			this.afkTimer = 0f;
		}
		StateManager.HasActed = false;
		if (this.isRoomAFKKicking && this.afkTimer >= 120f)
		{
			this.DoAFKKick();
		}
		if (PhotonNetwork.IsMasterClient && MapManager.InLobbyScene && this.afkTimer > 180f && PhotonNetwork.CurrentRoom.IsVisible)
		{
			InfoDisplay.SetText("Lobby is now Private", 5f, InfoArea.DetailTop);
			StateManager.SetRoomOpen(false);
		}
		if (!this.isRoomAFKKicking && this.afkTimer >= 3600f)
		{
			this.DoAFKKick();
		}
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00051A0F File Offset: 0x0004FC0F
	private void DoAFKKick()
	{
		StateManager.DidGetAFKKicked = true;
		if (GameplayManager.IsInGame)
		{
			Progression.RequestQuillmarkReward(Vector3.zero, GameplayManager.CurrentTome, GameplayManager.BindingLevel, false);
			StateManager.DoesHaveKickRewards = true;
		}
		PausePanel.instance.LeaveGame();
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00051A44 File Offset: 0x0004FC44
	public void KickPlayer(PlayerControl plr)
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		Player owner = plr.view.Owner;
		this.view.RPC("MasterKickedNetwork", owner, Array.Empty<object>());
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00051A7B File Offset: 0x0004FC7B
	[PunRPC]
	private void MasterKickedNetwork()
	{
		StateManager.DidGetServerKicked = true;
		if (GameplayManager.IsInGame)
		{
			Progression.RequestQuillmarkReward(Vector3.zero, GameplayManager.CurrentTome, GameplayManager.BindingLevel, false);
			StateManager.DoesHaveKickRewards = true;
		}
		PausePanel.instance.LeaveGame();
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00051AAF File Offset: 0x0004FCAF
	public static void SetRoomOpen(bool wantOpen)
	{
		if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		StateManager.SetValue("public", wantOpen);
		PhotonNetwork.CurrentRoom.IsVisible = (wantOpen && !GameplayManager.IsInGame);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00051AE8 File Offset: 0x0004FCE8
	public void RoomPrestige()
	{
		this.view.RPC("RoomPrestigeNetworked", RpcTarget.Others, Array.Empty<object>());
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00051B00 File Offset: 0x0004FD00
	[PunRPC]
	private void RoomPrestigeNetworked()
	{
		SignatureInkUIControl signatureInkUIControl = SignatureInkUIControl.instance;
		if (signatureInkUIControl == null)
		{
			return;
		}
		signatureInkUIControl.BeginPrestigeSequence(false);
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00051B14 File Offset: 0x0004FD14
	public static float GetNumber(string key, float defaultVal = 0f)
	{
		object value = StateManager.GetValue(key);
		if (value == null || (!(value is float) && !(value is int)))
		{
			return defaultVal;
		}
		return (float)value;
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00051B44 File Offset: 0x0004FD44
	public static bool GetBool(string key, bool defaultVal = true)
	{
		object value = StateManager.GetValue(key);
		if (value == null || !(value is bool))
		{
			return defaultVal;
		}
		return (bool)value;
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x00051B6C File Offset: 0x0004FD6C
	public static string GetID(string key, string defaultVal = "")
	{
		object value = StateManager.GetValue(key);
		if (value == null || !(value is string))
		{
			return defaultVal;
		}
		return (string)value;
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00051B94 File Offset: 0x0004FD94
	public static void SetValue(string key, object value)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		Hashtable customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
		if (customProperties.ContainsKey(key))
		{
			customProperties[key] = value;
		}
		else
		{
			customProperties.Add(key, value);
		}
		PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties, null, null);
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00051BE0 File Offset: 0x0004FDE0
	private static object GetValue(string key)
	{
		if (!PhotonNetwork.InRoom)
		{
			return null;
		}
		Hashtable customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
		if (customProperties.ContainsKey(key))
		{
			return customProperties[key];
		}
		return null;
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x00051C13 File Offset: 0x0004FE13
	public StateManager()
	{
	}

	// Token: 0x04000A17 RID: 2583
	public static StateManager instance;

	// Token: 0x04000A18 RID: 2584
	private PhotonView view;

	// Token: 0x04000A19 RID: 2585
	public static Action OnNetworkEvent;

	// Token: 0x04000A1A RID: 2586
	private float afkTimer;

	// Token: 0x04000A1B RID: 2587
	public const float AFK_DANGER_TIME = 30f;

	// Token: 0x04000A1C RID: 2588
	private const float KICK_TIME = 120f;

	// Token: 0x04000A1D RID: 2589
	private const float LOBBY_ALWAYS_KICK_TIME = 3600f;

	// Token: 0x04000A1E RID: 2590
	public static bool HasActed;

	// Token: 0x04000A1F RID: 2591
	private bool isRoomAFKKicking;

	// Token: 0x04000A20 RID: 2592
	public static bool DidGetAFKKicked;

	// Token: 0x04000A21 RID: 2593
	public static bool DidGetServerKicked;

	// Token: 0x04000A22 RID: 2594
	public static bool DoesHaveKickRewards;
}
