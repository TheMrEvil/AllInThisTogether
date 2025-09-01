using System;
using System.Runtime.InteropServices;

// Token: 0x02000002 RID: 2
public class DiscordRpc
{
	// Token: 0x06000001 RID: 1
	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Initialize")]
	public static extern void Initialize(string applicationId, ref DiscordRpc.EventHandlers handlers, bool autoRegister, string optionalSteamId);

	// Token: 0x06000002 RID: 2
	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Shutdown")]
	public static extern void Shutdown();

	// Token: 0x06000003 RID: 3
	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_RunCallbacks")]
	public static extern void RunCallbacks();

	// Token: 0x06000004 RID: 4
	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_UpdatePresence")]
	public static extern void UpdatePresence(ref DiscordRpc.RichPresence presence);

	// Token: 0x06000005 RID: 5
	[DllImport("discord-rpc", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Respond")]
	public static extern void Respond(string userId, DiscordRpc.Reply reply);

	// Token: 0x06000006 RID: 6 RVA: 0x00002050 File Offset: 0x00000250
	public DiscordRpc()
	{
	}

	// Token: 0x0200016B RID: 363
	[Serializable]
	public struct JoinRequest
	{
		// Token: 0x04000BCC RID: 3020
		public string userId;

		// Token: 0x04000BCD RID: 3021
		public string username;

		// Token: 0x04000BCE RID: 3022
		public string avatar;
	}

	// Token: 0x0200016C RID: 364
	[Serializable]
	public struct RichPresence
	{
		// Token: 0x04000BCF RID: 3023
		public string state;

		// Token: 0x04000BD0 RID: 3024
		public string details;

		// Token: 0x04000BD1 RID: 3025
		public long startTimestamp;

		// Token: 0x04000BD2 RID: 3026
		public long endTimestamp;

		// Token: 0x04000BD3 RID: 3027
		public string largeImageKey;

		// Token: 0x04000BD4 RID: 3028
		public string largeImageText;

		// Token: 0x04000BD5 RID: 3029
		public string smallImageKey;

		// Token: 0x04000BD6 RID: 3030
		public string smallImageText;

		// Token: 0x04000BD7 RID: 3031
		public string partyId;

		// Token: 0x04000BD8 RID: 3032
		public int partySize;

		// Token: 0x04000BD9 RID: 3033
		public int partyMax;

		// Token: 0x04000BDA RID: 3034
		public string matchSecret;

		// Token: 0x04000BDB RID: 3035
		public string joinSecret;

		// Token: 0x04000BDC RID: 3036
		public string spectateSecret;

		// Token: 0x04000BDD RID: 3037
		public bool instance;
	}

	// Token: 0x0200016D RID: 365
	public enum Reply
	{
		// Token: 0x04000BDF RID: 3039
		No,
		// Token: 0x04000BE0 RID: 3040
		Yes,
		// Token: 0x04000BE1 RID: 3041
		Ignore
	}

	// Token: 0x0200016E RID: 366
	public struct EventHandlers
	{
		// Token: 0x04000BE2 RID: 3042
		public DiscordRpc.ReadyCallback readyCallback;

		// Token: 0x04000BE3 RID: 3043
		public DiscordRpc.DisconnectedCallback disconnectedCallback;

		// Token: 0x04000BE4 RID: 3044
		public DiscordRpc.ErrorCallback errorCallback;

		// Token: 0x04000BE5 RID: 3045
		public DiscordRpc.JoinCallback joinCallback;

		// Token: 0x04000BE6 RID: 3046
		public DiscordRpc.SpectateCallback spectateCallback;

		// Token: 0x04000BE7 RID: 3047
		public DiscordRpc.RequestCallback requestCallback;
	}

	// Token: 0x0200016F RID: 367
	// (Invoke) Token: 0x06000E13 RID: 3603
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ReadyCallback();

	// Token: 0x02000170 RID: 368
	// (Invoke) Token: 0x06000E17 RID: 3607
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void DisconnectedCallback(int errorCode, string message);

	// Token: 0x02000171 RID: 369
	// (Invoke) Token: 0x06000E1B RID: 3611
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ErrorCallback(int errorCode, string message);

	// Token: 0x02000172 RID: 370
	// (Invoke) Token: 0x06000E1F RID: 3615
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void JoinCallback(string secret);

	// Token: 0x02000173 RID: 371
	// (Invoke) Token: 0x06000E23 RID: 3619
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SpectateCallback(string secret);

	// Token: 0x02000174 RID: 372
	// (Invoke) Token: 0x06000E27 RID: 3623
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void RequestCallback(DiscordRpc.JoinRequest request);
}
