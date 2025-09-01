using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000A2 RID: 162
	public class SteamServer : SteamServerClass<SteamServer>
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0000E415 File Offset: 0x0000C615
		internal static ISteamGameServer Internal
		{
			get
			{
				return SteamServerClass<SteamServer>.Interface as ISteamGameServer;
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0000E421 File Offset: 0x0000C621
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamGameServer(server));
			SteamServer.InstallEvents();
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0000E438 File Offset: 0x0000C638
		public static bool IsValid
		{
			get
			{
				return SteamServer.Internal != null && SteamServer.Internal.IsValid;
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0000E450 File Offset: 0x0000C650
		internal static void InstallEvents()
		{
			Dispatch.Install<ValidateAuthTicketResponse_t>(delegate(ValidateAuthTicketResponse_t x)
			{
				Action<SteamId, SteamId, AuthResponse> onValidateAuthTicketResponse = SteamServer.OnValidateAuthTicketResponse;
				if (onValidateAuthTicketResponse != null)
				{
					onValidateAuthTicketResponse(x.SteamID, x.OwnerSteamID, x.AuthSessionResponse);
				}
			}, true);
			Dispatch.Install<SteamServersConnected_t>(delegate(SteamServersConnected_t x)
			{
				Action onSteamServersConnected = SteamServer.OnSteamServersConnected;
				if (onSteamServersConnected != null)
				{
					onSteamServersConnected();
				}
			}, true);
			Dispatch.Install<SteamServerConnectFailure_t>(delegate(SteamServerConnectFailure_t x)
			{
				Action<Result, bool> onSteamServerConnectFailure = SteamServer.OnSteamServerConnectFailure;
				if (onSteamServerConnectFailure != null)
				{
					onSteamServerConnectFailure(x.Result, x.StillRetrying);
				}
			}, true);
			Dispatch.Install<SteamServersDisconnected_t>(delegate(SteamServersDisconnected_t x)
			{
				Action<Result> onSteamServersDisconnected = SteamServer.OnSteamServersDisconnected;
				if (onSteamServersDisconnected != null)
				{
					onSteamServersDisconnected(x.Result);
				}
			}, true);
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000885 RID: 2181 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		// (remove) Token: 0x06000886 RID: 2182 RVA: 0x0000E52C File Offset: 0x0000C72C
		public static event Action<SteamId, SteamId, AuthResponse> OnValidateAuthTicketResponse
		{
			[CompilerGenerated]
			add
			{
				Action<SteamId, SteamId, AuthResponse> action = SteamServer.OnValidateAuthTicketResponse;
				Action<SteamId, SteamId, AuthResponse> action2;
				do
				{
					action2 = action;
					Action<SteamId, SteamId, AuthResponse> value2 = (Action<SteamId, SteamId, AuthResponse>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<SteamId, SteamId, AuthResponse>>(ref SteamServer.OnValidateAuthTicketResponse, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<SteamId, SteamId, AuthResponse> action = SteamServer.OnValidateAuthTicketResponse;
				Action<SteamId, SteamId, AuthResponse> action2;
				do
				{
					action2 = action;
					Action<SteamId, SteamId, AuthResponse> value2 = (Action<SteamId, SteamId, AuthResponse>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<SteamId, SteamId, AuthResponse>>(ref SteamServer.OnValidateAuthTicketResponse, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000887 RID: 2183 RVA: 0x0000E560 File Offset: 0x0000C760
		// (remove) Token: 0x06000888 RID: 2184 RVA: 0x0000E594 File Offset: 0x0000C794
		public static event Action OnSteamServersConnected
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamServer.OnSteamServersConnected;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamServer.OnSteamServersConnected, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamServer.OnSteamServersConnected;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamServer.OnSteamServersConnected, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000889 RID: 2185 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		// (remove) Token: 0x0600088A RID: 2186 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		public static event Action<Result, bool> OnSteamServerConnectFailure
		{
			[CompilerGenerated]
			add
			{
				Action<Result, bool> action = SteamServer.OnSteamServerConnectFailure;
				Action<Result, bool> action2;
				do
				{
					action2 = action;
					Action<Result, bool> value2 = (Action<Result, bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Result, bool>>(ref SteamServer.OnSteamServerConnectFailure, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Result, bool> action = SteamServer.OnSteamServerConnectFailure;
				Action<Result, bool> action2;
				do
				{
					action2 = action;
					Action<Result, bool> value2 = (Action<Result, bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Result, bool>>(ref SteamServer.OnSteamServerConnectFailure, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600088B RID: 2187 RVA: 0x0000E630 File Offset: 0x0000C830
		// (remove) Token: 0x0600088C RID: 2188 RVA: 0x0000E664 File Offset: 0x0000C864
		public static event Action<Result> OnSteamServersDisconnected
		{
			[CompilerGenerated]
			add
			{
				Action<Result> action = SteamServer.OnSteamServersDisconnected;
				Action<Result> action2;
				do
				{
					action2 = action;
					Action<Result> value2 = (Action<Result>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Result>>(ref SteamServer.OnSteamServersDisconnected, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Result> action = SteamServer.OnSteamServersDisconnected;
				Action<Result> action2;
				do
				{
					action2 = action;
					Action<Result> value2 = (Action<Result>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Result>>(ref SteamServer.OnSteamServersDisconnected, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0000E698 File Offset: 0x0000C898
		public static void Init(AppId appid, SteamServerInit init, bool asyncCallbacks = true)
		{
			bool isValid = SteamServer.IsValid;
			if (isValid)
			{
				throw new Exception("Calling SteamServer.Init but is already initialized");
			}
			uint num = 0U;
			bool flag = init.SteamPort == 0;
			if (flag)
			{
				init = init.WithRandomSteamPort();
			}
			bool flag2 = init.IpAddress != null;
			if (flag2)
			{
				num = init.IpAddress.IpToInt32();
			}
			Environment.SetEnvironmentVariable("SteamAppId", appid.ToString());
			Environment.SetEnvironmentVariable("SteamGameId", appid.ToString());
			int num2 = init.Secure ? 3 : 2;
			bool flag3 = !SteamInternal.GameServer_Init(num, init.SteamPort, init.GamePort, init.QueryPort, num2, init.VersionString);
			if (flag3)
			{
				throw new Exception(string.Format("InitGameServer returned false ({0},{1},{2},{3},{4},\"{5}\")", new object[]
				{
					num,
					init.SteamPort,
					init.GamePort,
					init.QueryPort,
					num2,
					init.VersionString
				}));
			}
			Dispatch.Init();
			Dispatch.ServerPipe = SteamGameServer.GetHSteamPipe();
			SteamServer.AddInterface<SteamServer>();
			SteamServer.AddInterface<SteamUtils>();
			SteamServer.AddInterface<SteamNetworking>();
			SteamServer.AddInterface<SteamServerStats>();
			SteamServer.AddInterface<SteamInventory>();
			SteamServer.AddInterface<SteamUGC>();
			SteamServer.AddInterface<SteamApps>();
			SteamServer.AddInterface<SteamNetworkingUtils>();
			SteamServer.AddInterface<SteamNetworkingSockets>();
			SteamServer.AutomaticHeartbeats = true;
			SteamServer.MaxPlayers = 32;
			SteamServer.BotCount = 0;
			SteamServer.Product = string.Format("{0}", appid.Value);
			SteamServer.ModDir = init.ModDir;
			SteamServer.GameDescription = init.GameDescription;
			SteamServer.Passworded = false;
			SteamServer.DedicatedServer = init.DedicatedServer;
			if (asyncCallbacks)
			{
				Dispatch.LoopServerAsync();
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0000E860 File Offset: 0x0000CA60
		internal static void AddInterface<T>() where T : SteamClass, new()
		{
			T t = Activator.CreateInstance<T>();
			t.InitializeInterface(true);
			SteamServer.openInterfaces.Add(t);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0000E894 File Offset: 0x0000CA94
		internal static void ShutdownInterfaces()
		{
			foreach (SteamClass steamClass in SteamServer.openInterfaces)
			{
				steamClass.DestroyInterface(true);
			}
			SteamServer.openInterfaces.Clear();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		public static void Shutdown()
		{
			Dispatch.ShutdownServer();
			SteamServer.ShutdownInterfaces();
			SteamGameServer.Shutdown();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0000E910 File Offset: 0x0000CB10
		public static void RunCallbacks()
		{
			bool flag = Dispatch.ServerPipe != 0;
			if (flag)
			{
				Dispatch.Frame(Dispatch.ServerPipe);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0000E93F File Offset: 0x0000CB3F
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0000E948 File Offset: 0x0000CB48
		public static bool DedicatedServer
		{
			get
			{
				return SteamServer._dedicatedServer;
			}
			set
			{
				bool flag = SteamServer._dedicatedServer == value;
				if (!flag)
				{
					SteamServer.Internal.SetDedicatedServer(value);
					SteamServer._dedicatedServer = value;
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000E976 File Offset: 0x0000CB76
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0000E980 File Offset: 0x0000CB80
		public static int MaxPlayers
		{
			get
			{
				return SteamServer._maxplayers;
			}
			set
			{
				bool flag = SteamServer._maxplayers == value;
				if (!flag)
				{
					SteamServer.Internal.SetMaxPlayerCount(value);
					SteamServer._maxplayers = value;
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0000E9AE File Offset: 0x0000CBAE
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		public static int BotCount
		{
			get
			{
				return SteamServer._botcount;
			}
			set
			{
				bool flag = SteamServer._botcount == value;
				if (!flag)
				{
					SteamServer.Internal.SetBotPlayerCount(value);
					SteamServer._botcount = value;
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0000E9E6 File Offset: 0x0000CBE6
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		public static string MapName
		{
			get
			{
				return SteamServer._mapname;
			}
			set
			{
				bool flag = SteamServer._mapname == value;
				if (!flag)
				{
					SteamServer.Internal.SetMapName(value);
					SteamServer._mapname = value;
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0000EA21 File Offset: 0x0000CC21
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0000EA28 File Offset: 0x0000CC28
		public static string ModDir
		{
			get
			{
				return SteamServer._modDir;
			}
			internal set
			{
				bool flag = SteamServer._modDir == value;
				if (!flag)
				{
					SteamServer.Internal.SetModDir(value);
					SteamServer._modDir = value;
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0000EA59 File Offset: 0x0000CC59
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0000EA60 File Offset: 0x0000CC60
		public static string Product
		{
			get
			{
				return SteamServer._product;
			}
			internal set
			{
				bool flag = SteamServer._product == value;
				if (!flag)
				{
					SteamServer.Internal.SetProduct(value);
					SteamServer._product = value;
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0000EA91 File Offset: 0x0000CC91
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0000EA98 File Offset: 0x0000CC98
		public static string GameDescription
		{
			get
			{
				return SteamServer._gameDescription;
			}
			internal set
			{
				bool flag = SteamServer._gameDescription == value;
				if (!flag)
				{
					SteamServer.Internal.SetGameDescription(value);
					SteamServer._gameDescription = value;
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0000EAC9 File Offset: 0x0000CCC9
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		public static string ServerName
		{
			get
			{
				return SteamServer._serverName;
			}
			set
			{
				bool flag = SteamServer._serverName == value;
				if (!flag)
				{
					SteamServer.Internal.SetServerName(value);
					SteamServer._serverName = value;
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0000EB01 File Offset: 0x0000CD01
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x0000EB08 File Offset: 0x0000CD08
		public static bool Passworded
		{
			get
			{
				return SteamServer._passworded;
			}
			set
			{
				bool flag = SteamServer._passworded == value;
				if (!flag)
				{
					SteamServer.Internal.SetPasswordProtected(value);
					SteamServer._passworded = value;
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0000EB36 File Offset: 0x0000CD36
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0000EB40 File Offset: 0x0000CD40
		public static string GameTags
		{
			get
			{
				return SteamServer._gametags;
			}
			set
			{
				bool flag = SteamServer._gametags == value;
				if (!flag)
				{
					SteamServer.Internal.SetGameTags(value);
					SteamServer._gametags = value;
				}
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0000EB71 File Offset: 0x0000CD71
		public static void LogOnAnonymous()
		{
			SteamServer.Internal.LogOnAnonymous();
			SteamServer.ForceHeartbeat();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0000EB85 File Offset: 0x0000CD85
		public static void LogOff()
		{
			SteamServer.Internal.LogOff();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0000EB93 File Offset: 0x0000CD93
		public static bool LoggedOn
		{
			get
			{
				return SteamServer.Internal.BLoggedOn();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0000EB9F File Offset: 0x0000CD9F
		public static IPAddress PublicIp
		{
			get
			{
				return SteamServer.Internal.GetPublicIP();
			}
		}

		// Token: 0x1700005F RID: 95
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		public static bool AutomaticHeartbeats
		{
			set
			{
				SteamServer.Internal.EnableHeartbeats(value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0000EBBF File Offset: 0x0000CDBF
		public static int AutomaticHeartbeatRate
		{
			set
			{
				SteamServer.Internal.SetHeartbeatInterval(value);
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0000EBCE File Offset: 0x0000CDCE
		public static void ForceHeartbeat()
		{
			SteamServer.Internal.ForceHeartbeat();
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		public static void UpdatePlayer(SteamId steamid, string name, int score)
		{
			SteamServer.Internal.BUpdateUserData(steamid, name, (uint)score);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public static void SetKey(string Key, string Value)
		{
			bool flag = SteamServer.KeyValue.ContainsKey(Key);
			if (flag)
			{
				bool flag2 = SteamServer.KeyValue[Key] == Value;
				if (flag2)
				{
					return;
				}
				SteamServer.KeyValue[Key] = Value;
			}
			else
			{
				SteamServer.KeyValue.Add(Key, Value);
			}
			SteamServer.Internal.SetKeyValue(Key, Value);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0000EC51 File Offset: 0x0000CE51
		public static void ClearKeys()
		{
			SteamServer.KeyValue.Clear();
			SteamServer.Internal.ClearAllKeyValues();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public unsafe static bool BeginAuthSession(byte[] data, SteamId steamid)
		{
			byte* value;
			if (data == null || data.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &data[0];
			}
			BeginAuthResult beginAuthResult = SteamServer.Internal.BeginAuthSession((IntPtr)((void*)value), data.Length, steamid);
			return beginAuthResult == BeginAuthResult.OK;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		public static void EndSession(SteamId steamid)
		{
			SteamServer.Internal.EndAuthSession(steamid);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0000ECCC File Offset: 0x0000CECC
		public unsafe static bool GetOutgoingPacket(out OutgoingPacket packet)
		{
			byte[] array = Helpers.TakeBuffer(32768);
			packet = default(OutgoingPacket);
			byte[] array2;
			byte* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array2[0];
			}
			uint address = 0U;
			ushort port = 0;
			int nextOutgoingPacket = SteamServer.Internal.GetNextOutgoingPacket((IntPtr)((void*)value), array.Length, ref address, ref port);
			bool flag = nextOutgoingPacket == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				packet.Size = nextOutgoingPacket;
				packet.Data = array;
				packet.Address = address;
				packet.Port = port;
				result = true;
			}
			return result;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		public unsafe static void HandleIncomingPacket(byte[] data, int size, uint address, ushort port)
		{
			fixed (byte[] array = data)
			{
				byte* value;
				if (data == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				SteamServer.HandleIncomingPacket((IntPtr)((void*)value), size, address, port);
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0000ED95 File Offset: 0x0000CF95
		public static void HandleIncomingPacket(IntPtr ptr, int size, uint address, ushort port)
		{
			SteamServer.Internal.HandleIncomingPacket(ptr, size, address, port);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		public static UserHasLicenseForAppResult UserHasLicenseForApp(SteamId steamid, AppId appid)
		{
			return SteamServer.Internal.UserHasLicenseForApp(steamid, appid);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0000EDC6 File Offset: 0x0000CFC6
		public SteamServer()
		{
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		// Note: this type is marked as 'beforefieldinit'.
		static SteamServer()
		{
		}

		// Token: 0x04000716 RID: 1814
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<SteamId, SteamId, AuthResponse> OnValidateAuthTicketResponse;

		// Token: 0x04000717 RID: 1815
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnSteamServersConnected;

		// Token: 0x04000718 RID: 1816
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Result, bool> OnSteamServerConnectFailure;

		// Token: 0x04000719 RID: 1817
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Result> OnSteamServersDisconnected;

		// Token: 0x0400071A RID: 1818
		private static readonly List<SteamClass> openInterfaces = new List<SteamClass>();

		// Token: 0x0400071B RID: 1819
		private static bool _dedicatedServer;

		// Token: 0x0400071C RID: 1820
		private static int _maxplayers = 0;

		// Token: 0x0400071D RID: 1821
		private static int _botcount = 0;

		// Token: 0x0400071E RID: 1822
		private static string _mapname;

		// Token: 0x0400071F RID: 1823
		private static string _modDir = "";

		// Token: 0x04000720 RID: 1824
		private static string _product = "";

		// Token: 0x04000721 RID: 1825
		private static string _gameDescription = "";

		// Token: 0x04000722 RID: 1826
		private static string _serverName = "";

		// Token: 0x04000723 RID: 1827
		private static bool _passworded;

		// Token: 0x04000724 RID: 1828
		private static string _gametags = "";

		// Token: 0x04000725 RID: 1829
		private static Dictionary<string, string> KeyValue = new Dictionary<string, string>();

		// Token: 0x0200023F RID: 575
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001152 RID: 4434 RVA: 0x0001E79F File Offset: 0x0001C99F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001153 RID: 4435 RVA: 0x0001E7AB File Offset: 0x0001C9AB
			public <>c()
			{
			}

			// Token: 0x06001154 RID: 4436 RVA: 0x0001E7B4 File Offset: 0x0001C9B4
			internal void <InstallEvents>b__5_0(ValidateAuthTicketResponse_t x)
			{
				Action<SteamId, SteamId, AuthResponse> onValidateAuthTicketResponse = SteamServer.OnValidateAuthTicketResponse;
				if (onValidateAuthTicketResponse != null)
				{
					onValidateAuthTicketResponse(x.SteamID, x.OwnerSteamID, x.AuthSessionResponse);
				}
			}

			// Token: 0x06001155 RID: 4437 RVA: 0x0001E7E3 File Offset: 0x0001C9E3
			internal void <InstallEvents>b__5_1(SteamServersConnected_t x)
			{
				Action onSteamServersConnected = SteamServer.OnSteamServersConnected;
				if (onSteamServersConnected != null)
				{
					onSteamServersConnected();
				}
			}

			// Token: 0x06001156 RID: 4438 RVA: 0x0001E7F6 File Offset: 0x0001C9F6
			internal void <InstallEvents>b__5_2(SteamServerConnectFailure_t x)
			{
				Action<Result, bool> onSteamServerConnectFailure = SteamServer.OnSteamServerConnectFailure;
				if (onSteamServerConnectFailure != null)
				{
					onSteamServerConnectFailure(x.Result, x.StillRetrying);
				}
			}

			// Token: 0x06001157 RID: 4439 RVA: 0x0001E815 File Offset: 0x0001CA15
			internal void <InstallEvents>b__5_3(SteamServersDisconnected_t x)
			{
				Action<Result> onSteamServersDisconnected = SteamServer.OnSteamServersDisconnected;
				if (onSteamServersDisconnected != null)
				{
					onSteamServersDisconnected(x.Result);
				}
			}

			// Token: 0x04000D5A RID: 3418
			public static readonly SteamServer.<>c <>9 = new SteamServer.<>c();

			// Token: 0x04000D5B RID: 3419
			public static Action<ValidateAuthTicketResponse_t> <>9__5_0;

			// Token: 0x04000D5C RID: 3420
			public static Action<SteamServersConnected_t> <>9__5_1;

			// Token: 0x04000D5D RID: 3421
			public static Action<SteamServerConnectFailure_t> <>9__5_2;

			// Token: 0x04000D5E RID: 3422
			public static Action<SteamServersDisconnected_t> <>9__5_3;
		}
	}
}
