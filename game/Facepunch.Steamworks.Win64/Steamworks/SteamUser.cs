using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x020000A5 RID: 165
	public class SteamUser : SteamClientClass<SteamUser>
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0000F291 File Offset: 0x0000D491
		internal static ISteamUser Internal
		{
			get
			{
				return SteamClientClass<SteamUser>.Interface as ISteamUser;
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0000F29D File Offset: 0x0000D49D
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamUser(server));
			SteamUser.InstallEvents();
			SteamUser.richPresence = new Dictionary<string, string>();
			SteamUser.SampleRate = SteamUser.OptimalSampleRate;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		internal static void InstallEvents()
		{
			Dispatch.Install<SteamServersConnected_t>(delegate(SteamServersConnected_t x)
			{
				Action onSteamServersConnected = SteamUser.OnSteamServersConnected;
				if (onSteamServersConnected != null)
				{
					onSteamServersConnected();
				}
			}, false);
			Dispatch.Install<SteamServerConnectFailure_t>(delegate(SteamServerConnectFailure_t x)
			{
				Action onSteamServerConnectFailure = SteamUser.OnSteamServerConnectFailure;
				if (onSteamServerConnectFailure != null)
				{
					onSteamServerConnectFailure();
				}
			}, false);
			Dispatch.Install<SteamServersDisconnected_t>(delegate(SteamServersDisconnected_t x)
			{
				Action onSteamServersDisconnected = SteamUser.OnSteamServersDisconnected;
				if (onSteamServersDisconnected != null)
				{
					onSteamServersDisconnected();
				}
			}, false);
			Dispatch.Install<ClientGameServerDeny_t>(delegate(ClientGameServerDeny_t x)
			{
				Action onClientGameServerDeny = SteamUser.OnClientGameServerDeny;
				if (onClientGameServerDeny != null)
				{
					onClientGameServerDeny();
				}
			}, false);
			Dispatch.Install<LicensesUpdated_t>(delegate(LicensesUpdated_t x)
			{
				Action onLicensesUpdated = SteamUser.OnLicensesUpdated;
				if (onLicensesUpdated != null)
				{
					onLicensesUpdated();
				}
			}, false);
			Dispatch.Install<ValidateAuthTicketResponse_t>(delegate(ValidateAuthTicketResponse_t x)
			{
				Action<SteamId, SteamId, AuthResponse> onValidateAuthTicketResponse = SteamUser.OnValidateAuthTicketResponse;
				if (onValidateAuthTicketResponse != null)
				{
					onValidateAuthTicketResponse(x.SteamID, x.OwnerSteamID, x.AuthSessionResponse);
				}
			}, false);
			Dispatch.Install<MicroTxnAuthorizationResponse_t>(delegate(MicroTxnAuthorizationResponse_t x)
			{
				Action<AppId, ulong, bool> onMicroTxnAuthorizationResponse = SteamUser.OnMicroTxnAuthorizationResponse;
				if (onMicroTxnAuthorizationResponse != null)
				{
					onMicroTxnAuthorizationResponse(x.AppID, x.OrderID, x.Authorized > 0);
				}
			}, false);
			Dispatch.Install<GameWebCallback_t>(delegate(GameWebCallback_t x)
			{
				Action<string> onGameWebCallback = SteamUser.OnGameWebCallback;
				if (onGameWebCallback != null)
				{
					onGameWebCallback(x.URLUTF8());
				}
			}, false);
			Dispatch.Install<GetAuthSessionTicketResponse_t>(delegate(GetAuthSessionTicketResponse_t x)
			{
				Action<GetAuthSessionTicketResponse_t> onGetAuthSessionTicketResponse = SteamUser.OnGetAuthSessionTicketResponse;
				if (onGetAuthSessionTicketResponse != null)
				{
					onGetAuthSessionTicketResponse(x);
				}
			}, false);
			Dispatch.Install<DurationControl_t>(delegate(DurationControl_t x)
			{
				Action<DurationControl> onDurationControl = SteamUser.OnDurationControl;
				if (onDurationControl != null)
				{
					onDurationControl(new DurationControl
					{
						_inner = x
					});
				}
			}, false);
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060008D4 RID: 2260 RVA: 0x0000F458 File Offset: 0x0000D658
		// (remove) Token: 0x060008D5 RID: 2261 RVA: 0x0000F48C File Offset: 0x0000D68C
		public static event Action OnSteamServersConnected
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamUser.OnSteamServersConnected;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnSteamServersConnected, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamUser.OnSteamServersConnected;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnSteamServersConnected, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060008D6 RID: 2262 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		// (remove) Token: 0x060008D7 RID: 2263 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public static event Action OnSteamServerConnectFailure
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamUser.OnSteamServerConnectFailure;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnSteamServerConnectFailure, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamUser.OnSteamServerConnectFailure;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnSteamServerConnectFailure, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060008D8 RID: 2264 RVA: 0x0000F528 File Offset: 0x0000D728
		// (remove) Token: 0x060008D9 RID: 2265 RVA: 0x0000F55C File Offset: 0x0000D75C
		public static event Action OnSteamServersDisconnected
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamUser.OnSteamServersDisconnected;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnSteamServersDisconnected, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamUser.OnSteamServersDisconnected;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnSteamServersDisconnected, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060008DA RID: 2266 RVA: 0x0000F590 File Offset: 0x0000D790
		// (remove) Token: 0x060008DB RID: 2267 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
		public static event Action OnClientGameServerDeny
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamUser.OnClientGameServerDeny;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnClientGameServerDeny, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamUser.OnClientGameServerDeny;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnClientGameServerDeny, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060008DC RID: 2268 RVA: 0x0000F5F8 File Offset: 0x0000D7F8
		// (remove) Token: 0x060008DD RID: 2269 RVA: 0x0000F62C File Offset: 0x0000D82C
		public static event Action OnLicensesUpdated
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamUser.OnLicensesUpdated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnLicensesUpdated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamUser.OnLicensesUpdated;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamUser.OnLicensesUpdated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060008DE RID: 2270 RVA: 0x0000F660 File Offset: 0x0000D860
		// (remove) Token: 0x060008DF RID: 2271 RVA: 0x0000F694 File Offset: 0x0000D894
		public static event Action<SteamId, SteamId, AuthResponse> OnValidateAuthTicketResponse
		{
			[CompilerGenerated]
			add
			{
				Action<SteamId, SteamId, AuthResponse> action = SteamUser.OnValidateAuthTicketResponse;
				Action<SteamId, SteamId, AuthResponse> action2;
				do
				{
					action2 = action;
					Action<SteamId, SteamId, AuthResponse> value2 = (Action<SteamId, SteamId, AuthResponse>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<SteamId, SteamId, AuthResponse>>(ref SteamUser.OnValidateAuthTicketResponse, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<SteamId, SteamId, AuthResponse> action = SteamUser.OnValidateAuthTicketResponse;
				Action<SteamId, SteamId, AuthResponse> action2;
				do
				{
					action2 = action;
					Action<SteamId, SteamId, AuthResponse> value2 = (Action<SteamId, SteamId, AuthResponse>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<SteamId, SteamId, AuthResponse>>(ref SteamUser.OnValidateAuthTicketResponse, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060008E0 RID: 2272 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		// (remove) Token: 0x060008E1 RID: 2273 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		internal static event Action<GetAuthSessionTicketResponse_t> OnGetAuthSessionTicketResponse
		{
			[CompilerGenerated]
			add
			{
				Action<GetAuthSessionTicketResponse_t> action = SteamUser.OnGetAuthSessionTicketResponse;
				Action<GetAuthSessionTicketResponse_t> action2;
				do
				{
					action2 = action;
					Action<GetAuthSessionTicketResponse_t> value2 = (Action<GetAuthSessionTicketResponse_t>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<GetAuthSessionTicketResponse_t>>(ref SteamUser.OnGetAuthSessionTicketResponse, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<GetAuthSessionTicketResponse_t> action = SteamUser.OnGetAuthSessionTicketResponse;
				Action<GetAuthSessionTicketResponse_t> action2;
				do
				{
					action2 = action;
					Action<GetAuthSessionTicketResponse_t> value2 = (Action<GetAuthSessionTicketResponse_t>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<GetAuthSessionTicketResponse_t>>(ref SteamUser.OnGetAuthSessionTicketResponse, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060008E2 RID: 2274 RVA: 0x0000F730 File Offset: 0x0000D930
		// (remove) Token: 0x060008E3 RID: 2275 RVA: 0x0000F764 File Offset: 0x0000D964
		public static event Action<AppId, ulong, bool> OnMicroTxnAuthorizationResponse
		{
			[CompilerGenerated]
			add
			{
				Action<AppId, ulong, bool> action = SteamUser.OnMicroTxnAuthorizationResponse;
				Action<AppId, ulong, bool> action2;
				do
				{
					action2 = action;
					Action<AppId, ulong, bool> value2 = (Action<AppId, ulong, bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<AppId, ulong, bool>>(ref SteamUser.OnMicroTxnAuthorizationResponse, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<AppId, ulong, bool> action = SteamUser.OnMicroTxnAuthorizationResponse;
				Action<AppId, ulong, bool> action2;
				do
				{
					action2 = action;
					Action<AppId, ulong, bool> value2 = (Action<AppId, ulong, bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<AppId, ulong, bool>>(ref SteamUser.OnMicroTxnAuthorizationResponse, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060008E4 RID: 2276 RVA: 0x0000F798 File Offset: 0x0000D998
		// (remove) Token: 0x060008E5 RID: 2277 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		public static event Action<string> OnGameWebCallback
		{
			[CompilerGenerated]
			add
			{
				Action<string> action = SteamUser.OnGameWebCallback;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> value2 = (Action<string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref SteamUser.OnGameWebCallback, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string> action = SteamUser.OnGameWebCallback;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> value2 = (Action<string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref SteamUser.OnGameWebCallback, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060008E6 RID: 2278 RVA: 0x0000F800 File Offset: 0x0000DA00
		// (remove) Token: 0x060008E7 RID: 2279 RVA: 0x0000F834 File Offset: 0x0000DA34
		public static event Action<DurationControl> OnDurationControl
		{
			[CompilerGenerated]
			add
			{
				Action<DurationControl> action = SteamUser.OnDurationControl;
				Action<DurationControl> action2;
				do
				{
					action2 = action;
					Action<DurationControl> value2 = (Action<DurationControl>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<DurationControl>>(ref SteamUser.OnDurationControl, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<DurationControl> action = SteamUser.OnDurationControl;
				Action<DurationControl> action2;
				do
				{
					action2 = action;
					Action<DurationControl> value2 = (Action<DurationControl>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<DurationControl>>(ref SteamUser.OnDurationControl, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0000F867 File Offset: 0x0000DA67
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x0000F870 File Offset: 0x0000DA70
		public static bool VoiceRecord
		{
			get
			{
				return SteamUser._recordingVoice;
			}
			set
			{
				SteamUser._recordingVoice = value;
				if (value)
				{
					SteamUser.Internal.StartVoiceRecording();
				}
				else
				{
					SteamUser.Internal.StopVoiceRecording();
				}
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		public static bool HasVoiceData
		{
			get
			{
				uint num = 0U;
				uint num2 = 0U;
				bool flag = SteamUser.Internal.GetAvailableVoice(ref num, ref num2, 0U) > VoiceResult.OK;
				return !flag && num > 0U;
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		public unsafe static int ReadVoiceData(Stream stream)
		{
			bool flag = !SteamUser.HasVoiceData;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				uint num = 0U;
				uint num2 = 0U;
				byte[] array;
				byte* value;
				if ((array = SteamUser.readBuffer) == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				bool flag2 = SteamUser.Internal.GetVoice(true, (IntPtr)((void*)value), (uint)SteamUser.readBuffer.Length, ref num, false, IntPtr.Zero, 0U, ref num2, 0U) > VoiceResult.OK;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					array = null;
					bool flag3 = num == 0U;
					if (flag3)
					{
						result = 0;
					}
					else
					{
						stream.Write(SteamUser.readBuffer, 0, (int)num);
						result = (int)num;
					}
				}
			}
			return result;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0000F974 File Offset: 0x0000DB74
		public unsafe static byte[] ReadVoiceDataBytes()
		{
			bool flag = !SteamUser.HasVoiceData;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				uint num = 0U;
				uint num2 = 0U;
				byte[] array;
				byte* value;
				if ((array = SteamUser.readBuffer) == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				bool flag2 = SteamUser.Internal.GetVoice(true, (IntPtr)((void*)value), (uint)SteamUser.readBuffer.Length, ref num, false, IntPtr.Zero, 0U, ref num2, 0U) > VoiceResult.OK;
				if (flag2)
				{
					result = null;
				}
				else
				{
					array = null;
					bool flag3 = num == 0U;
					if (flag3)
					{
						result = null;
					}
					else
					{
						byte[] array2 = new byte[num];
						Array.Copy(SteamUser.readBuffer, 0L, array2, 0L, (long)((ulong)num));
						result = array2;
					}
				}
			}
			return result;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0000FA22 File Offset: 0x0000DC22
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		public static uint SampleRate
		{
			get
			{
				return SteamUser.sampleRate;
			}
			set
			{
				bool flag = SteamUser.SampleRate < 11025U;
				if (flag)
				{
					throw new Exception("Sample Rate must be between 11025 and 48000");
				}
				bool flag2 = SteamUser.SampleRate > 48000U;
				if (flag2)
				{
					throw new Exception("Sample Rate must be between 11025 and 48000");
				}
				SteamUser.sampleRate = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0000FA76 File Offset: 0x0000DC76
		public static uint OptimalSampleRate
		{
			get
			{
				return SteamUser.Internal.GetVoiceOptimalSampleRate();
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0000FA84 File Offset: 0x0000DC84
		public unsafe static int DecompressVoice(Stream input, int length, Stream output)
		{
			byte[] array = Helpers.TakeBuffer(length);
			byte[] array2 = Helpers.TakeBuffer(65536);
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				input.CopyTo(memoryStream);
			}
			uint num = 0U;
			byte[] array3;
			byte* value;
			if ((array3 = array) == null || array3.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array3[0];
			}
			byte[] array4;
			byte* value2;
			if ((array4 = array2) == null || array4.Length == 0)
			{
				value2 = null;
			}
			else
			{
				value2 = &array4[0];
			}
			bool flag = SteamUser.Internal.DecompressVoice((IntPtr)((void*)value), (uint)length, (IntPtr)((void*)value2), (uint)array2.Length, ref num, SteamUser.SampleRate) > VoiceResult.OK;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				array4 = null;
				array3 = null;
				bool flag2 = num == 0U;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					output.Write(array2, 0, (int)num);
					result = (int)num;
				}
			}
			return result;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0000FB68 File Offset: 0x0000DD68
		public unsafe static int DecompressVoice(byte[] from, Stream output)
		{
			byte[] array = Helpers.TakeBuffer(65536);
			uint num = 0U;
			fixed (byte[] array2 = from)
			{
				byte* value;
				if (from == null || array2.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array2[0];
				}
				byte[] array3;
				byte* value2;
				if ((array3 = array) == null || array3.Length == 0)
				{
					value2 = null;
				}
				else
				{
					value2 = &array3[0];
				}
				bool flag = SteamUser.Internal.DecompressVoice((IntPtr)((void*)value), (uint)from.Length, (IntPtr)((void*)value2), (uint)array.Length, ref num, SteamUser.SampleRate) > VoiceResult.OK;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					array3 = null;
					array2 = null;
					bool flag2 = num == 0U;
					if (flag2)
					{
						result = 0;
					}
					else
					{
						output.Write(array, 0, (int)num);
						result = (int)num;
					}
				}
				return result;
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0000FC14 File Offset: 0x0000DE14
		public unsafe static AuthTicket GetAuthSessionTicket()
		{
			byte[] array = Helpers.TakeBuffer(1024);
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
			uint count = 0U;
			uint num = SteamUser.Internal.GetAuthSessionTicket((IntPtr)((void*)value), array.Length, ref count);
			bool flag = num == 0U;
			AuthTicket result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new AuthTicket
				{
					Data = array.Take((int)count).ToArray<byte>(),
					Handle = num
				};
			}
			return result;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		public static async Task<AuthTicket> GetAuthSessionTicketAsync(double timeoutSeconds = 10.0)
		{
			SteamUser.<>c__DisplayClass53_0 CS$<>8__locals1 = new SteamUser.<>c__DisplayClass53_0();
			CS$<>8__locals1.result = Result.Pending;
			CS$<>8__locals1.ticket = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			SteamUser.OnGetAuthSessionTicketResponse += CS$<>8__locals1.<GetAuthSessionTicketAsync>g__f|0;
			AuthTicket result;
			try
			{
				CS$<>8__locals1.ticket = SteamUser.GetAuthSessionTicket();
				bool flag = CS$<>8__locals1.ticket == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					while (CS$<>8__locals1.result == Result.Pending)
					{
						await Task.Delay(10);
						if (stopwatch.Elapsed.TotalSeconds > timeoutSeconds)
						{
							CS$<>8__locals1.ticket.Cancel();
							return null;
						}
					}
					if (CS$<>8__locals1.result == Result.OK)
					{
						result = CS$<>8__locals1.ticket;
					}
					else
					{
						CS$<>8__locals1.ticket.Cancel();
						result = null;
					}
				}
			}
			finally
			{
				SteamUser.OnGetAuthSessionTicketResponse -= CS$<>8__locals1.<GetAuthSessionTicketAsync>g__f|0;
			}
			return result;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0000FCE4 File Offset: 0x0000DEE4
		public unsafe static BeginAuthResult BeginAuthSession(byte[] ticketData, SteamId steamid)
		{
			byte* value;
			if (ticketData == null || ticketData.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &ticketData[0];
			}
			return SteamUser.Internal.BeginAuthSession((IntPtr)((void*)value), ticketData.Length, steamid);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0000FD23 File Offset: 0x0000DF23
		public static void EndAuthSession(SteamId steamid)
		{
			SteamUser.Internal.EndAuthSession(steamid);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0000FD31 File Offset: 0x0000DF31
		public static bool IsBehindNAT
		{
			get
			{
				return SteamUser.Internal.BIsBehindNAT();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0000FD3D File Offset: 0x0000DF3D
		public static int SteamLevel
		{
			get
			{
				return SteamUser.Internal.GetPlayerSteamLevel();
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		public static async Task<string> GetStoreAuthUrlAsync(string url)
		{
			StoreAuthURLResponse_t? storeAuthURLResponse_t = await SteamUser.Internal.RequestStoreAuthURL(url);
			StoreAuthURLResponse_t? response = storeAuthURLResponse_t;
			storeAuthURLResponse_t = null;
			string result;
			if (response == null)
			{
				result = null;
			}
			else
			{
				result = response.Value.URLUTF8();
			}
			return result;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0000FD93 File Offset: 0x0000DF93
		public static bool IsPhoneVerified
		{
			get
			{
				return SteamUser.Internal.BIsPhoneVerified();
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0000FD9F File Offset: 0x0000DF9F
		public static bool IsTwoFactorEnabled
		{
			get
			{
				return SteamUser.Internal.BIsTwoFactorEnabled();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0000FDAB File Offset: 0x0000DFAB
		public static bool IsPhoneIdentifying
		{
			get
			{
				return SteamUser.Internal.BIsPhoneIdentifying();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0000FDB7 File Offset: 0x0000DFB7
		public static bool IsPhoneRequiringVerification
		{
			get
			{
				return SteamUser.Internal.BIsPhoneRequiringVerification();
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		public static async Task<byte[]> RequestEncryptedAppTicketAsync(byte[] dataToInclude)
		{
			IntPtr dataPtr = Marshal.AllocHGlobal(dataToInclude.Length);
			Marshal.Copy(dataToInclude, 0, dataPtr, dataToInclude.Length);
			byte[] result2;
			try
			{
				EncryptedAppTicketResponse_t? encryptedAppTicketResponse_t = await SteamUser.Internal.RequestEncryptedAppTicket(dataPtr, dataToInclude.Length);
				EncryptedAppTicketResponse_t? result = encryptedAppTicketResponse_t;
				encryptedAppTicketResponse_t = null;
				if (result == null || result.Value.Result != Result.OK)
				{
					result2 = null;
				}
				else
				{
					IntPtr ticketData = Marshal.AllocHGlobal(1024);
					uint outSize = 0U;
					byte[] data = null;
					if (SteamUser.Internal.GetEncryptedAppTicket(ticketData, 1024, ref outSize))
					{
						data = new byte[outSize];
						Marshal.Copy(ticketData, data, 0, (int)outSize);
					}
					Marshal.FreeHGlobal(ticketData);
					result2 = data;
				}
			}
			finally
			{
				Marshal.FreeHGlobal(dataPtr);
			}
			return result2;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000FE0C File Offset: 0x0000E00C
		public static async Task<byte[]> RequestEncryptedAppTicketAsync()
		{
			EncryptedAppTicketResponse_t? encryptedAppTicketResponse_t = await SteamUser.Internal.RequestEncryptedAppTicket(IntPtr.Zero, 0);
			EncryptedAppTicketResponse_t? result = encryptedAppTicketResponse_t;
			encryptedAppTicketResponse_t = null;
			byte[] result2;
			if (result == null || result.Value.Result != Result.OK)
			{
				result2 = null;
			}
			else
			{
				IntPtr ticketData = Marshal.AllocHGlobal(1024);
				uint outSize = 0U;
				byte[] data = null;
				if (SteamUser.Internal.GetEncryptedAppTicket(ticketData, 1024, ref outSize))
				{
					data = new byte[outSize];
					Marshal.Copy(ticketData, data, 0, (int)outSize);
				}
				Marshal.FreeHGlobal(ticketData);
				result2 = data;
			}
			return result2;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0000FE4C File Offset: 0x0000E04C
		public static async Task<DurationControl> GetDurationControl()
		{
			DurationControl_t? durationControl_t = await SteamUser.Internal.GetDurationControl();
			DurationControl_t? response = durationControl_t;
			durationControl_t = null;
			DurationControl result;
			if (response == null)
			{
				result = default(DurationControl);
			}
			else
			{
				result = new DurationControl
				{
					_inner = response.Value
				};
			}
			return result;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0000FE8C File Offset: 0x0000E08C
		public SteamUser()
		{
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000FE95 File Offset: 0x0000E095
		// Note: this type is marked as 'beforefieldinit'.
		static SteamUser()
		{
		}

		// Token: 0x04000727 RID: 1831
		private static Dictionary<string, string> richPresence;

		// Token: 0x04000728 RID: 1832
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnSteamServersConnected;

		// Token: 0x04000729 RID: 1833
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnSteamServerConnectFailure;

		// Token: 0x0400072A RID: 1834
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnSteamServersDisconnected;

		// Token: 0x0400072B RID: 1835
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnClientGameServerDeny;

		// Token: 0x0400072C RID: 1836
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnLicensesUpdated;

		// Token: 0x0400072D RID: 1837
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<SteamId, SteamId, AuthResponse> OnValidateAuthTicketResponse;

		// Token: 0x0400072E RID: 1838
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<GetAuthSessionTicketResponse_t> OnGetAuthSessionTicketResponse;

		// Token: 0x0400072F RID: 1839
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<AppId, ulong, bool> OnMicroTxnAuthorizationResponse;

		// Token: 0x04000730 RID: 1840
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<string> OnGameWebCallback;

		// Token: 0x04000731 RID: 1841
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<DurationControl> OnDurationControl;

		// Token: 0x04000732 RID: 1842
		private static bool _recordingVoice;

		// Token: 0x04000733 RID: 1843
		private static byte[] readBuffer = new byte[131072];

		// Token: 0x04000734 RID: 1844
		private static uint sampleRate = 48000U;

		// Token: 0x0200024A RID: 586
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001175 RID: 4469 RVA: 0x0001F442 File Offset: 0x0001D642
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001176 RID: 4470 RVA: 0x0001F44E File Offset: 0x0001D64E
			public <>c()
			{
			}

			// Token: 0x06001177 RID: 4471 RVA: 0x0001F457 File Offset: 0x0001D657
			internal void <InstallEvents>b__4_0(SteamServersConnected_t x)
			{
				Action onSteamServersConnected = SteamUser.OnSteamServersConnected;
				if (onSteamServersConnected != null)
				{
					onSteamServersConnected();
				}
			}

			// Token: 0x06001178 RID: 4472 RVA: 0x0001F46A File Offset: 0x0001D66A
			internal void <InstallEvents>b__4_1(SteamServerConnectFailure_t x)
			{
				Action onSteamServerConnectFailure = SteamUser.OnSteamServerConnectFailure;
				if (onSteamServerConnectFailure != null)
				{
					onSteamServerConnectFailure();
				}
			}

			// Token: 0x06001179 RID: 4473 RVA: 0x0001F47D File Offset: 0x0001D67D
			internal void <InstallEvents>b__4_2(SteamServersDisconnected_t x)
			{
				Action onSteamServersDisconnected = SteamUser.OnSteamServersDisconnected;
				if (onSteamServersDisconnected != null)
				{
					onSteamServersDisconnected();
				}
			}

			// Token: 0x0600117A RID: 4474 RVA: 0x0001F490 File Offset: 0x0001D690
			internal void <InstallEvents>b__4_3(ClientGameServerDeny_t x)
			{
				Action onClientGameServerDeny = SteamUser.OnClientGameServerDeny;
				if (onClientGameServerDeny != null)
				{
					onClientGameServerDeny();
				}
			}

			// Token: 0x0600117B RID: 4475 RVA: 0x0001F4A3 File Offset: 0x0001D6A3
			internal void <InstallEvents>b__4_4(LicensesUpdated_t x)
			{
				Action onLicensesUpdated = SteamUser.OnLicensesUpdated;
				if (onLicensesUpdated != null)
				{
					onLicensesUpdated();
				}
			}

			// Token: 0x0600117C RID: 4476 RVA: 0x0001F4B6 File Offset: 0x0001D6B6
			internal void <InstallEvents>b__4_5(ValidateAuthTicketResponse_t x)
			{
				Action<SteamId, SteamId, AuthResponse> onValidateAuthTicketResponse = SteamUser.OnValidateAuthTicketResponse;
				if (onValidateAuthTicketResponse != null)
				{
					onValidateAuthTicketResponse(x.SteamID, x.OwnerSteamID, x.AuthSessionResponse);
				}
			}

			// Token: 0x0600117D RID: 4477 RVA: 0x0001F4E5 File Offset: 0x0001D6E5
			internal void <InstallEvents>b__4_6(MicroTxnAuthorizationResponse_t x)
			{
				Action<AppId, ulong, bool> onMicroTxnAuthorizationResponse = SteamUser.OnMicroTxnAuthorizationResponse;
				if (onMicroTxnAuthorizationResponse != null)
				{
					onMicroTxnAuthorizationResponse(x.AppID, x.OrderID, x.Authorized > 0);
				}
			}

			// Token: 0x0600117E RID: 4478 RVA: 0x0001F512 File Offset: 0x0001D712
			internal void <InstallEvents>b__4_7(GameWebCallback_t x)
			{
				Action<string> onGameWebCallback = SteamUser.OnGameWebCallback;
				if (onGameWebCallback != null)
				{
					onGameWebCallback(x.URLUTF8());
				}
			}

			// Token: 0x0600117F RID: 4479 RVA: 0x0001F52C File Offset: 0x0001D72C
			internal void <InstallEvents>b__4_8(GetAuthSessionTicketResponse_t x)
			{
				Action<GetAuthSessionTicketResponse_t> onGetAuthSessionTicketResponse = SteamUser.OnGetAuthSessionTicketResponse;
				if (onGetAuthSessionTicketResponse != null)
				{
					onGetAuthSessionTicketResponse(x);
				}
			}

			// Token: 0x06001180 RID: 4480 RVA: 0x0001F540 File Offset: 0x0001D740
			internal void <InstallEvents>b__4_9(DurationControl_t x)
			{
				Action<DurationControl> onDurationControl = SteamUser.OnDurationControl;
				if (onDurationControl != null)
				{
					onDurationControl(new DurationControl
					{
						_inner = x
					});
				}
			}

			// Token: 0x04000D96 RID: 3478
			public static readonly SteamUser.<>c <>9 = new SteamUser.<>c();

			// Token: 0x04000D97 RID: 3479
			public static Action<SteamServersConnected_t> <>9__4_0;

			// Token: 0x04000D98 RID: 3480
			public static Action<SteamServerConnectFailure_t> <>9__4_1;

			// Token: 0x04000D99 RID: 3481
			public static Action<SteamServersDisconnected_t> <>9__4_2;

			// Token: 0x04000D9A RID: 3482
			public static Action<ClientGameServerDeny_t> <>9__4_3;

			// Token: 0x04000D9B RID: 3483
			public static Action<LicensesUpdated_t> <>9__4_4;

			// Token: 0x04000D9C RID: 3484
			public static Action<ValidateAuthTicketResponse_t> <>9__4_5;

			// Token: 0x04000D9D RID: 3485
			public static Action<MicroTxnAuthorizationResponse_t> <>9__4_6;

			// Token: 0x04000D9E RID: 3486
			public static Action<GameWebCallback_t> <>9__4_7;

			// Token: 0x04000D9F RID: 3487
			public static Action<GetAuthSessionTicketResponse_t> <>9__4_8;

			// Token: 0x04000DA0 RID: 3488
			public static Action<DurationControl_t> <>9__4_9;
		}

		// Token: 0x0200024B RID: 587
		[CompilerGenerated]
		private sealed class <>c__DisplayClass53_0
		{
			// Token: 0x06001181 RID: 4481 RVA: 0x0001F56F File Offset: 0x0001D76F
			public <>c__DisplayClass53_0()
			{
			}

			// Token: 0x06001182 RID: 4482 RVA: 0x0001F578 File Offset: 0x0001D778
			internal void <GetAuthSessionTicketAsync>g__f|0(GetAuthSessionTicketResponse_t t)
			{
				bool flag = t.AuthTicket != this.ticket.Handle;
				if (!flag)
				{
					this.result = t.Result;
				}
			}

			// Token: 0x04000DA1 RID: 3489
			public AuthTicket ticket;

			// Token: 0x04000DA2 RID: 3490
			public Result result;
		}

		// Token: 0x0200024C RID: 588
		[CompilerGenerated]
		private sealed class <GetAuthSessionTicketAsync>d__53 : IAsyncStateMachine
		{
			// Token: 0x06001183 RID: 4483 RVA: 0x0001F5AE File Offset: 0x0001D7AE
			public <GetAuthSessionTicketAsync>d__53()
			{
			}

			// Token: 0x06001184 RID: 4484 RVA: 0x0001F5B8 File Offset: 0x0001D7B8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				AuthTicket result;
				try
				{
					if (num != 0)
					{
						CS$<>8__locals1 = new SteamUser.<>c__DisplayClass53_0();
						CS$<>8__locals1.result = Result.Pending;
						CS$<>8__locals1.ticket = null;
						stopwatch = Stopwatch.StartNew();
						SteamUser.OnGetAuthSessionTicketResponse += CS$<>8__locals1.<GetAuthSessionTicketAsync>g__f|0;
					}
					try
					{
						TaskAwaiter taskAwaiter;
						if (num != 0)
						{
							CS$<>8__locals1.ticket = SteamUser.GetAuthSessionTicket();
							bool flag = CS$<>8__locals1.ticket == null;
							if (flag)
							{
								result = null;
								goto IL_1B3;
							}
							goto IL_12C;
						}
						else
						{
							TaskAwaiter taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter);
							num = (num2 = -1);
						}
						IL_E8:
						taskAwaiter.GetResult();
						bool flag2 = stopwatch.Elapsed.TotalSeconds > timeoutSeconds;
						if (flag2)
						{
							CS$<>8__locals1.ticket.Cancel();
							result = null;
							goto IL_1B3;
						}
						IL_12C:
						if (CS$<>8__locals1.result != Result.Pending)
						{
							bool flag3 = CS$<>8__locals1.result == Result.OK;
							if (flag3)
							{
								result = CS$<>8__locals1.ticket;
							}
							else
							{
								CS$<>8__locals1.ticket.Cancel();
								result = null;
							}
						}
						else
						{
							taskAwaiter = Task.Delay(10).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								TaskAwaiter taskAwaiter2 = taskAwaiter;
								SteamUser.<GetAuthSessionTicketAsync>d__53 <GetAuthSessionTicketAsync>d__ = this;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SteamUser.<GetAuthSessionTicketAsync>d__53>(ref taskAwaiter, ref <GetAuthSessionTicketAsync>d__);
								return;
							}
							goto IL_E8;
						}
					}
					finally
					{
						if (num < 0)
						{
							SteamUser.OnGetAuthSessionTicketResponse -= CS$<>8__locals1.<GetAuthSessionTicketAsync>g__f|0;
						}
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1B3:
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001185 RID: 4485 RVA: 0x0001F7C4 File Offset: 0x0001D9C4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DA3 RID: 3491
			public int <>1__state;

			// Token: 0x04000DA4 RID: 3492
			public AsyncTaskMethodBuilder<AuthTicket> <>t__builder;

			// Token: 0x04000DA5 RID: 3493
			public double timeoutSeconds;

			// Token: 0x04000DA6 RID: 3494
			private SteamUser.<>c__DisplayClass53_0 <>8__1;

			// Token: 0x04000DA7 RID: 3495
			private Stopwatch <stopwatch>5__2;

			// Token: 0x04000DA8 RID: 3496
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200024D RID: 589
		[CompilerGenerated]
		private sealed class <GetStoreAuthUrlAsync>d__60 : IAsyncStateMachine
		{
			// Token: 0x06001186 RID: 4486 RVA: 0x0001F7C6 File Offset: 0x0001D9C6
			public <GetStoreAuthUrlAsync>d__60()
			{
			}

			// Token: 0x06001187 RID: 4487 RVA: 0x0001F7D0 File Offset: 0x0001D9D0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				string result;
				try
				{
					CallResult<StoreAuthURLResponse_t> callResult;
					if (num != 0)
					{
						callResult = SteamUser.Internal.RequestStoreAuthURL(url).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<StoreAuthURLResponse_t> callResult2 = callResult;
							SteamUser.<GetStoreAuthUrlAsync>d__60 <GetStoreAuthUrlAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<StoreAuthURLResponse_t>, SteamUser.<GetStoreAuthUrlAsync>d__60>(ref callResult, ref <GetStoreAuthUrlAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<StoreAuthURLResponse_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<StoreAuthURLResponse_t>);
						num2 = -1;
					}
					storeAuthURLResponse_t = callResult.GetResult();
					response = storeAuthURLResponse_t;
					storeAuthURLResponse_t = null;
					bool flag = response == null;
					if (flag)
					{
						result = null;
					}
					else
					{
						result = response.Value.URLUTF8();
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001188 RID: 4488 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DA9 RID: 3497
			public int <>1__state;

			// Token: 0x04000DAA RID: 3498
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04000DAB RID: 3499
			public string url;

			// Token: 0x04000DAC RID: 3500
			private StoreAuthURLResponse_t? <response>5__1;

			// Token: 0x04000DAD RID: 3501
			private StoreAuthURLResponse_t? <>s__2;

			// Token: 0x04000DAE RID: 3502
			private CallResult<StoreAuthURLResponse_t> <>u__1;
		}

		// Token: 0x0200024E RID: 590
		[CompilerGenerated]
		private sealed class <RequestEncryptedAppTicketAsync>d__69 : IAsyncStateMachine
		{
			// Token: 0x06001189 RID: 4489 RVA: 0x0001F8EA File Offset: 0x0001DAEA
			public <RequestEncryptedAppTicketAsync>d__69()
			{
			}

			// Token: 0x0600118A RID: 4490 RVA: 0x0001F8F4 File Offset: 0x0001DAF4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				byte[] result2;
				try
				{
					if (num != 0)
					{
						dataPtr = Marshal.AllocHGlobal(dataToInclude.Length);
						Marshal.Copy(dataToInclude, 0, dataPtr, dataToInclude.Length);
					}
					try
					{
						CallResult<EncryptedAppTicketResponse_t> callResult;
						if (num != 0)
						{
							callResult = SteamUser.Internal.RequestEncryptedAppTicket(dataPtr, dataToInclude.Length).GetAwaiter();
							if (!callResult.IsCompleted)
							{
								num = (num2 = 0);
								CallResult<EncryptedAppTicketResponse_t> callResult2 = callResult;
								SteamUser.<RequestEncryptedAppTicketAsync>d__69 <RequestEncryptedAppTicketAsync>d__ = this;
								this.<>t__builder.AwaitOnCompleted<CallResult<EncryptedAppTicketResponse_t>, SteamUser.<RequestEncryptedAppTicketAsync>d__69>(ref callResult, ref <RequestEncryptedAppTicketAsync>d__);
								return;
							}
						}
						else
						{
							CallResult<EncryptedAppTicketResponse_t> callResult2;
							callResult = callResult2;
							callResult2 = default(CallResult<EncryptedAppTicketResponse_t>);
							num = (num2 = -1);
						}
						encryptedAppTicketResponse_t = callResult.GetResult();
						result = encryptedAppTicketResponse_t;
						encryptedAppTicketResponse_t = null;
						bool flag = result == null || result.Value.Result != Result.OK;
						if (flag)
						{
							result2 = null;
						}
						else
						{
							ticketData = Marshal.AllocHGlobal(1024);
							outSize = 0U;
							data = null;
							bool encryptedAppTicket = SteamUser.Internal.GetEncryptedAppTicket(ticketData, 1024, ref outSize);
							if (encryptedAppTicket)
							{
								data = new byte[outSize];
								Marshal.Copy(ticketData, data, 0, (int)outSize);
							}
							Marshal.FreeHGlobal(ticketData);
							result2 = data;
						}
					}
					finally
					{
						if (num < 0)
						{
							Marshal.FreeHGlobal(dataPtr);
						}
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x0600118B RID: 4491 RVA: 0x0001FB04 File Offset: 0x0001DD04
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DAF RID: 3503
			public int <>1__state;

			// Token: 0x04000DB0 RID: 3504
			public AsyncTaskMethodBuilder<byte[]> <>t__builder;

			// Token: 0x04000DB1 RID: 3505
			public byte[] dataToInclude;

			// Token: 0x04000DB2 RID: 3506
			private IntPtr <dataPtr>5__1;

			// Token: 0x04000DB3 RID: 3507
			private EncryptedAppTicketResponse_t? <result>5__2;

			// Token: 0x04000DB4 RID: 3508
			private IntPtr <ticketData>5__3;

			// Token: 0x04000DB5 RID: 3509
			private uint <outSize>5__4;

			// Token: 0x04000DB6 RID: 3510
			private byte[] <data>5__5;

			// Token: 0x04000DB7 RID: 3511
			private EncryptedAppTicketResponse_t? <>s__6;

			// Token: 0x04000DB8 RID: 3512
			private CallResult<EncryptedAppTicketResponse_t> <>u__1;
		}

		// Token: 0x0200024F RID: 591
		[CompilerGenerated]
		private sealed class <RequestEncryptedAppTicketAsync>d__70 : IAsyncStateMachine
		{
			// Token: 0x0600118C RID: 4492 RVA: 0x0001FB06 File Offset: 0x0001DD06
			public <RequestEncryptedAppTicketAsync>d__70()
			{
			}

			// Token: 0x0600118D RID: 4493 RVA: 0x0001FB10 File Offset: 0x0001DD10
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				byte[] result2;
				try
				{
					CallResult<EncryptedAppTicketResponse_t> callResult;
					if (num != 0)
					{
						callResult = SteamUser.Internal.RequestEncryptedAppTicket(IntPtr.Zero, 0).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<EncryptedAppTicketResponse_t> callResult2 = callResult;
							SteamUser.<RequestEncryptedAppTicketAsync>d__70 <RequestEncryptedAppTicketAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<EncryptedAppTicketResponse_t>, SteamUser.<RequestEncryptedAppTicketAsync>d__70>(ref callResult, ref <RequestEncryptedAppTicketAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<EncryptedAppTicketResponse_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<EncryptedAppTicketResponse_t>);
						num2 = -1;
					}
					encryptedAppTicketResponse_t = callResult.GetResult();
					result = encryptedAppTicketResponse_t;
					encryptedAppTicketResponse_t = null;
					bool flag = result == null || result.Value.Result != Result.OK;
					if (flag)
					{
						result2 = null;
					}
					else
					{
						ticketData = Marshal.AllocHGlobal(1024);
						outSize = 0U;
						data = null;
						bool encryptedAppTicket = SteamUser.Internal.GetEncryptedAppTicket(ticketData, 1024, ref outSize);
						if (encryptedAppTicket)
						{
							data = new byte[outSize];
							Marshal.Copy(ticketData, data, 0, (int)outSize);
						}
						Marshal.FreeHGlobal(ticketData);
						result2 = data;
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x0600118E RID: 4494 RVA: 0x0001FCB8 File Offset: 0x0001DEB8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DB9 RID: 3513
			public int <>1__state;

			// Token: 0x04000DBA RID: 3514
			public AsyncTaskMethodBuilder<byte[]> <>t__builder;

			// Token: 0x04000DBB RID: 3515
			private EncryptedAppTicketResponse_t? <result>5__1;

			// Token: 0x04000DBC RID: 3516
			private IntPtr <ticketData>5__2;

			// Token: 0x04000DBD RID: 3517
			private uint <outSize>5__3;

			// Token: 0x04000DBE RID: 3518
			private byte[] <data>5__4;

			// Token: 0x04000DBF RID: 3519
			private EncryptedAppTicketResponse_t? <>s__5;

			// Token: 0x04000DC0 RID: 3520
			private CallResult<EncryptedAppTicketResponse_t> <>u__1;
		}

		// Token: 0x02000250 RID: 592
		[CompilerGenerated]
		private sealed class <GetDurationControl>d__71 : IAsyncStateMachine
		{
			// Token: 0x0600118F RID: 4495 RVA: 0x0001FCBA File Offset: 0x0001DEBA
			public <GetDurationControl>d__71()
			{
			}

			// Token: 0x06001190 RID: 4496 RVA: 0x0001FCC4 File Offset: 0x0001DEC4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				DurationControl result;
				try
				{
					CallResult<DurationControl_t> callResult;
					if (num != 0)
					{
						callResult = SteamUser.Internal.GetDurationControl().GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<DurationControl_t> callResult2 = callResult;
							SteamUser.<GetDurationControl>d__71 <GetDurationControl>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<DurationControl_t>, SteamUser.<GetDurationControl>d__71>(ref callResult, ref <GetDurationControl>d__);
							return;
						}
					}
					else
					{
						CallResult<DurationControl_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<DurationControl_t>);
						num2 = -1;
					}
					durationControl_t = callResult.GetResult();
					response = durationControl_t;
					durationControl_t = null;
					bool flag = response == null;
					if (flag)
					{
						result = default(DurationControl);
					}
					else
					{
						result = new DurationControl
						{
							_inner = response.Value
						};
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06001191 RID: 4497 RVA: 0x0001FDE4 File Offset: 0x0001DFE4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000DC1 RID: 3521
			public int <>1__state;

			// Token: 0x04000DC2 RID: 3522
			public AsyncTaskMethodBuilder<DurationControl> <>t__builder;

			// Token: 0x04000DC3 RID: 3523
			private DurationControl_t? <response>5__1;

			// Token: 0x04000DC4 RID: 3524
			private DurationControl_t? <>s__2;

			// Token: 0x04000DC5 RID: 3525
			private CallResult<DurationControl_t> <>u__1;
		}
	}
}
