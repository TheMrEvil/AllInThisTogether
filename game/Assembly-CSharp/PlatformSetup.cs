using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Photon.Pun;
using Steamworks;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class PlatformSetup : MonoBehaviour
{
	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00048EEC File Offset: 0x000470EC
	public static bool IsSteamInitialized
	{
		get
		{
			PlatformSetup platformSetup = PlatformSetup.instance;
			return platformSetup != null && platformSetup._steamInitialized;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00048EFE File Offset: 0x000470FE
	// (set) Token: 0x06000B31 RID: 2865 RVA: 0x00048F05 File Offset: 0x00047105
	public static PlatformSetup.Platform CurPlatform
	{
		[CompilerGenerated]
		get
		{
			return PlatformSetup.<CurPlatform>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			PlatformSetup.<CurPlatform>k__BackingField = value;
		}
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x00048F0D File Offset: 0x0004710D
	private void Awake()
	{
		PlatformSetup.instance = this;
		PlatformSetup.CurPlatform = PlatformSetup.Platform.None;
		PlatformSetup.Username = "";
		PlatformSetup.UserID = "";
		PlatformSetup.Initialized = false;
		PlatformSetup.IsSteamDeck = false;
		this.Init();
		base.StartCoroutine("WaitForLogin");
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00048F4D File Offset: 0x0004714D
	private void Init()
	{
		this.Init_Steam();
		if (!PlatformSetup.UserID.Contains("ANON"))
		{
			ParseManager.Initialize();
			ParseManager.Login(PlatformSetup.UserID, PlatformSetup.Username);
		}
		PlatformSetup.Initialized = true;
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00048F80 File Offset: 0x00047180
	private IEnumerator WaitForLogin()
	{
		while (!PlatformSetup.Initialized)
		{
			yield return true;
		}
		Action onLoggedIn = PlatformSetup.OnLoggedIn;
		if (onLoggedIn != null)
		{
			onLoggedIn();
		}
		AchievementManager.CheckInitialAchievements();
		PhotonNetwork.NickName = PlatformSetup.Username;
		Amplitude.SetUserID(PlatformSetup.UserID);
		this.HandleLaunchOptions();
		yield break;
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00048F8F File Offset: 0x0004718F
	private void HandleLaunchOptions()
	{
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00048F94 File Offset: 0x00047194
	private void Init_Steam()
	{
		try
		{
			if (!Packsize.Test())
			{
				UnityEngine.Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
			}
			if (!DllCheck.Test())
			{
				UnityEngine.Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
			}
			this._steamInitialized = SteamAPI.Init();
			if (!this._steamInitialized)
			{
				UnityEngine.Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			}
			else
			{
				PlatformSetup.CheckLaunchOptions(Environment.GetCommandLineArgs());
				PlatformSetup.Username = SteamFriends.GetPersonaName();
				PlatformSetup.UserID = "STEAM_" + SteamUser.GetSteamID().ToString();
				PlatformSetup.IsSteamDeck = SteamUtils.IsSteamRunningOnSteamDeck();
				UnityEngine.Debug.Log("Steam Connected : " + SteamUser.GetSteamID().ToString() + " : " + SteamFriends.GetPersonaName());
				PlatformSetup.CurPlatform = PlatformSetup.Platform.Steam;
				Action onLoggedIn = PlatformSetup.OnLoggedIn;
				if (onLoggedIn != null)
				{
					onLoggedIn();
				}
				this.m_GameRichPresenceJoinRequested = Callback<GameRichPresenceJoinRequested_t>.Create(new Callback<GameRichPresenceJoinRequested_t>.DispatchDelegate(this.OnSteamInvite));
				this.m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(new Callback<GameOverlayActivated_t>.DispatchDelegate(this.OnSteamOverlayActivated));
			}
		}
		catch (Exception ex)
		{
			PlatformSetup.UserID = "ANONYMOUS";
			string str = "Steam Init Exception: ";
			Exception ex2 = ex;
			UnityEngine.Debug.LogError(str + ((ex2 != null) ? ex2.ToString() : null));
			PlatformSetup.Username = "User " + UnityEngine.Random.Range(10, 99).ToString();
		}
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x000490F4 File Offset: 0x000472F4
	public static void SetSteamRoomCode()
	{
		if (!PlatformSetup.Initialized || PlatformSetup.CurPlatform != PlatformSetup.Platform.Steam)
		{
			return;
		}
		try
		{
			if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.OfflineMode)
			{
				SteamFriends.SetRichPresence("connect", "");
			}
			else
			{
				SteamFriends.SetRichPresence("connect", PhotonNetwork.CurrentRoom.Name);
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x0004915C File Offset: 0x0004735C
	public static void ClearSteamRoomCode()
	{
		if (!PlatformSetup.Initialized || !Application.isPlaying || PlatformSetup.CurPlatform != PlatformSetup.Platform.Steam)
		{
			return;
		}
		try
		{
			SteamFriends.SetRichPresence("connect", "");
		}
		catch
		{
		}
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x000491A8 File Offset: 0x000473A8
	private void OnSteamInvite(GameRichPresenceJoinRequested_t pCallback)
	{
		PlatformSetup.ExternalInvite(pCallback.m_rgchConnect);
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x000491B6 File Offset: 0x000473B6
	private void OnSteamOverlayActivated(GameOverlayActivated_t pCallback)
	{
		if (pCallback.m_bActive == 1)
		{
			return;
		}
		if (GameplayUI.InGame && PanelManager.CurPanel == PanelType.GameInvisible)
		{
			CanvasController.GoToPause();
		}
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x000491D8 File Offset: 0x000473D8
	private static void CheckLaunchOptions(string[] commands)
	{
		Regex regex = new Regex("^[A-Z]{5}$");
		foreach (string text in commands)
		{
			if (!string.IsNullOrEmpty(text) && regex.Match(text.Trim()).Success)
			{
				UnityEngine.Debug.Log("Got valid Join Code from launch options: " + text);
				NetworkManager.AutoJoinCode = text.Trim().ToUpperInvariant();
				return;
			}
		}
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x00049240 File Offset: 0x00047440
	public static void ExternalInvite(string joinCode)
	{
		if (!PlatformSetup.Initialized || !Application.isPlaying || PlatformSetup.CurPlatform != PlatformSetup.Platform.Steam)
		{
			return;
		}
		joinCode = joinCode.Trim().ToUpperInvariant();
		UnityEngine.Debug.Log("Join Code: '" + joinCode + "'");
		NetworkManager.instance.TryJoinInvite(joinCode);
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00049291 File Offset: 0x00047491
	private void Update()
	{
		if (this._steamInitialized)
		{
			SteamAPI.RunCallbacks();
		}
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x000492A0 File Offset: 0x000474A0
	private void OnApplicationQuit()
	{
		UnityEngine.Debug.Log("Steam Shutdown");
		SteamAPI.Shutdown();
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x000492B1 File Offset: 0x000474B1
	public PlatformSetup()
	{
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x000492B9 File Offset: 0x000474B9
	// Note: this type is marked as 'beforefieldinit'.
	static PlatformSetup()
	{
	}

	// Token: 0x04000942 RID: 2370
	public static PlatformSetup instance;

	// Token: 0x04000943 RID: 2371
	public static bool Initialized;

	// Token: 0x04000944 RID: 2372
	public static string Username = "";

	// Token: 0x04000945 RID: 2373
	public static string UserID = "";

	// Token: 0x04000946 RID: 2374
	public static Action OnLoggedIn;

	// Token: 0x04000947 RID: 2375
	public const uint STEAM_APP_ID = 917950U;

	// Token: 0x04000948 RID: 2376
	private bool _steamInitialized;

	// Token: 0x04000949 RID: 2377
	protected Callback<GameRichPresenceJoinRequested_t> m_GameRichPresenceJoinRequested;

	// Token: 0x0400094A RID: 2378
	protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;

	// Token: 0x0400094B RID: 2379
	[CompilerGenerated]
	private static PlatformSetup.Platform <CurPlatform>k__BackingField;

	// Token: 0x0400094C RID: 2380
	public static bool IsSteamDeck;

	// Token: 0x020004EB RID: 1259
	public enum Platform
	{
		// Token: 0x04002504 RID: 9476
		None,
		// Token: 0x04002505 RID: 9477
		Steam
	}

	// Token: 0x020004EC RID: 1260
	[CompilerGenerated]
	private sealed class <WaitForLogin>d__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002332 RID: 9010 RVA: 0x000C8B6A File Offset: 0x000C6D6A
		[DebuggerHidden]
		public <WaitForLogin>d__18(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000C8B79 File Offset: 0x000C6D79
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000C8B7C File Offset: 0x000C6D7C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PlatformSetup platformSetup = this;
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
			}
			if (PlatformSetup.Initialized)
			{
				Action onLoggedIn = PlatformSetup.OnLoggedIn;
				if (onLoggedIn != null)
				{
					onLoggedIn();
				}
				AchievementManager.CheckInitialAchievements();
				PhotonNetwork.NickName = PlatformSetup.Username;
				Amplitude.SetUserID(PlatformSetup.UserID);
				platformSetup.HandleLaunchOptions();
				return false;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x000C8BFC File Offset: 0x000C6DFC
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000C8C04 File Offset: 0x000C6E04
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x000C8C0B File Offset: 0x000C6E0B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002506 RID: 9478
		private int <>1__state;

		// Token: 0x04002507 RID: 9479
		private object <>2__current;

		// Token: 0x04002508 RID: 9480
		public PlatformSetup <>4__this;
	}
}
