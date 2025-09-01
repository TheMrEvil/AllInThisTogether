using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000225 RID: 549
public class DiscordManager : MonoBehaviour
{
	// Token: 0x060016CD RID: 5837 RVA: 0x00090C9C File Offset: 0x0008EE9C
	private void OnEnable()
	{
		DiscordManager.instance = this;
		this.callbackCalls = 0;
		this.handlers = default(DiscordRpc.EventHandlers);
		this.handlers.readyCallback = new DiscordRpc.ReadyCallback(this.ReadyCallback);
		this.handlers.disconnectedCallback = (DiscordRpc.DisconnectedCallback)Delegate.Combine(this.handlers.disconnectedCallback, new DiscordRpc.DisconnectedCallback(this.DisconnectedCallback));
		this.handlers.errorCallback = (DiscordRpc.ErrorCallback)Delegate.Combine(this.handlers.errorCallback, new DiscordRpc.ErrorCallback(this.ErrorCallback));
		try
		{
			DiscordRpc.Initialize("1143019046783160421", ref this.handlers, true, "917950");
		}
		catch (Exception ex)
		{
			Debug.LogError("Error Initializing Discord");
			Debug.LogError(ex.ToString());
		}
		base.InvokeRepeating("CheckState", 1f, 5.379f);
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x00090D74 File Offset: 0x0008EF74
	private void OnDisable()
	{
		DiscordRpc.Shutdown();
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x00090D7C File Offset: 0x0008EF7C
	private void CheckState()
	{
		if (this.callbackCalls == 0)
		{
			return;
		}
		string detail = "In Menu";
		long startTime = 0L;
		string lgImgKey = "in_menu";
		bool isMultiplayer = false;
		int partyCurrent = 0;
		string state;
		if (PlayerControl.myInstance != null)
		{
			isMultiplayer = !PhotonNetwork.OfflineMode;
			Room currentRoom = PhotonNetwork.CurrentRoom;
			partyCurrent = ((currentRoom != null) ? currentRoom.PlayerCount : 0);
			if (LibraryManager.instance != null)
			{
				detail = "Preparing";
				state = "In the Library";
				lgImgKey = "in_spgame";
			}
			else
			{
				GenreTree gameGraph = GameplayManager.instance.GameGraph;
				detail = (((gameGraph != null) ? gameGraph.Root.Name : null) ?? "Unknown");
				state = "Binding Level " + GameplayManager.BindingLevel.ToString();
			}
		}
		else
		{
			state = PanelManager.GetCurrentPanelName();
		}
		DiscordManager.SetPresence(state, detail, lgImgKey, startTime, isMultiplayer, partyCurrent);
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x00090E4B File Offset: 0x0008F04B
	public void ReadyCallback()
	{
		this.callbackCalls++;
		Debug.Log("Discord: ready");
		this.onConnect.Invoke();
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x00090E70 File Offset: 0x0008F070
	public void DisconnectedCallback(int errorCode, string message)
	{
		this.callbackCalls++;
		Debug.Log(string.Format("Discord: disconnect {0}: {1}", errorCode, message));
		this.onDisconnect.Invoke();
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x00090EA1 File Offset: 0x0008F0A1
	public void ErrorCallback(int errorCode, string message)
	{
		this.callbackCalls++;
		Debug.Log(string.Format("Discord: error {0}: {1}", errorCode, message));
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x00090EC8 File Offset: 0x0008F0C8
	private void Update()
	{
		try
		{
			DiscordRpc.RunCallbacks();
		}
		catch
		{
		}
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x00090EF0 File Offset: 0x0008F0F0
	private void OnApplicationQuit()
	{
		DiscordRpc.Shutdown();
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x00090EF8 File Offset: 0x0008F0F8
	public static void SetPresence(string state, string detail, string lgImgKey, long startTime, bool isMultiplayer, int partyCurrent)
	{
		if (DiscordManager.instance != null)
		{
			DiscordRpc.RichPresence richPresence = DiscordManager.instance.presence;
			richPresence.state = state;
			richPresence.details = detail;
			richPresence.largeImageKey = lgImgKey;
			richPresence.startTimestamp = startTime;
			if (!isMultiplayer)
			{
				richPresence.partyMax = 0;
			}
			else
			{
				richPresence.partyMax = 4;
				richPresence.partySize = partyCurrent;
			}
			try
			{
				DiscordRpc.UpdatePresence(ref richPresence);
			}
			catch
			{
			}
		}
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x00090F7C File Offset: 0x0008F17C
	public DiscordManager()
	{
	}

	// Token: 0x040016E0 RID: 5856
	public DiscordRpc.RichPresence presence;

	// Token: 0x040016E1 RID: 5857
	private const string APP_ID = "1143019046783160421";

	// Token: 0x040016E2 RID: 5858
	private const string STEAM_ID = "917950";

	// Token: 0x040016E3 RID: 5859
	public int callbackCalls;

	// Token: 0x040016E4 RID: 5860
	public UnityEvent onConnect;

	// Token: 0x040016E5 RID: 5861
	public UnityEvent onDisconnect;

	// Token: 0x040016E6 RID: 5862
	private DiscordRpc.EventHandlers handlers;

	// Token: 0x040016E7 RID: 5863
	public static DiscordManager instance;
}
