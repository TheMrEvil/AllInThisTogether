using System;
using System.Runtime.CompilerServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200009A RID: 154
	public class SteamNetworking : SteamSharedClass<SteamNetworking>
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0000CFDA File Offset: 0x0000B1DA
		internal static ISteamNetworking Internal
		{
			get
			{
				return SteamSharedClass<SteamNetworking>.Interface as ISteamNetworking;
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0000CFE6 File Offset: 0x0000B1E6
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamNetworking(server));
			SteamNetworking.InstallEvents(server);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0000D000 File Offset: 0x0000B200
		internal static void InstallEvents(bool server)
		{
			Dispatch.Install<P2PSessionRequest_t>(delegate(P2PSessionRequest_t x)
			{
				Action<SteamId> onP2PSessionRequest = SteamNetworking.OnP2PSessionRequest;
				if (onP2PSessionRequest != null)
				{
					onP2PSessionRequest(x.SteamIDRemote);
				}
			}, server);
			Dispatch.Install<P2PSessionConnectFail_t>(delegate(P2PSessionConnectFail_t x)
			{
				Action<SteamId, P2PSessionError> onP2PConnectionFailed = SteamNetworking.OnP2PConnectionFailed;
				if (onP2PConnectionFailed != null)
				{
					onP2PConnectionFailed(x.SteamIDRemote, (P2PSessionError)x.P2PSessionError);
				}
			}, server);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0000D05A File Offset: 0x0000B25A
		public static bool AcceptP2PSessionWithUser(SteamId user)
		{
			return SteamNetworking.Internal.AcceptP2PSessionWithUser(user);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0000D067 File Offset: 0x0000B267
		public static bool AllowP2PPacketRelay(bool allow)
		{
			return SteamNetworking.Internal.AllowP2PPacketRelay(allow);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0000D074 File Offset: 0x0000B274
		public static bool CloseP2PSessionWithUser(SteamId user)
		{
			return SteamNetworking.Internal.CloseP2PSessionWithUser(user);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0000D084 File Offset: 0x0000B284
		public static bool IsP2PPacketAvailable(int channel = 0)
		{
			uint num = 0U;
			return SteamNetworking.Internal.IsP2PPacketAvailable(ref num, channel);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		public unsafe static P2Packet? ReadP2PPacket(int channel = 0)
		{
			uint num = 0U;
			bool flag = !SteamNetworking.Internal.IsP2PPacketAvailable(ref num, channel);
			P2Packet? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] array = Helpers.TakeBuffer((int)num);
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
				SteamId steamId = 1UL;
				bool flag2 = !SteamNetworking.Internal.ReadP2PPacket((IntPtr)((void*)value), (uint)array.Length, ref num, ref steamId, channel) || num == 0U;
				if (flag2)
				{
					result = null;
				}
				else
				{
					byte[] array3 = new byte[num];
					Array.Copy(array, 0L, array3, 0L, (long)((ulong)num));
					result = new P2Packet?(new P2Packet
					{
						SteamId = steamId,
						Data = array3
					});
				}
			}
			return result;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0000D180 File Offset: 0x0000B380
		public unsafe static bool ReadP2PPacket(byte[] buffer, ref uint size, ref SteamId steamid, int channel = 0)
		{
			byte* value;
			if (buffer == null || buffer.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &buffer[0];
			}
			return SteamNetworking.Internal.ReadP2PPacket((IntPtr)((void*)value), (uint)buffer.Length, ref size, ref steamid, channel);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		public unsafe static bool ReadP2PPacket(byte* buffer, uint cbuf, ref uint size, ref SteamId steamid, int channel = 0)
		{
			return SteamNetworking.Internal.ReadP2PPacket((IntPtr)((void*)buffer), cbuf, ref size, ref steamid, channel);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		public unsafe static bool SendP2PPacket(SteamId steamid, byte[] data, int length = -1, int nChannel = 0, P2PSend sendType = P2PSend.Reliable)
		{
			bool flag = length <= 0;
			if (flag)
			{
				length = data.Length;
			}
			byte* value;
			if (data == null || data.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &data[0];
			}
			return SteamNetworking.Internal.SendP2PPacket(steamid, (IntPtr)((void*)value), (uint)length, sendType, nChannel);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0000D23C File Offset: 0x0000B43C
		public unsafe static bool SendP2PPacket(SteamId steamid, byte* data, uint length, int nChannel = 1, P2PSend sendType = P2PSend.Reliable)
		{
			return SteamNetworking.Internal.SendP2PPacket(steamid, (IntPtr)((void*)data), length, sendType, nChannel);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0000D263 File Offset: 0x0000B463
		public SteamNetworking()
		{
		}

		// Token: 0x04000704 RID: 1796
		public static Action<SteamId> OnP2PSessionRequest;

		// Token: 0x04000705 RID: 1797
		public static Action<SteamId, P2PSessionError> OnP2PConnectionFailed;

		// Token: 0x02000235 RID: 565
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001129 RID: 4393 RVA: 0x0001E2B4 File Offset: 0x0001C4B4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600112A RID: 4394 RVA: 0x0001E2C0 File Offset: 0x0001C4C0
			public <>c()
			{
			}

			// Token: 0x0600112B RID: 4395 RVA: 0x0001E2C9 File Offset: 0x0001C4C9
			internal void <InstallEvents>b__3_0(P2PSessionRequest_t x)
			{
				Action<SteamId> onP2PSessionRequest = SteamNetworking.OnP2PSessionRequest;
				if (onP2PSessionRequest != null)
				{
					onP2PSessionRequest(x.SteamIDRemote);
				}
			}

			// Token: 0x0600112C RID: 4396 RVA: 0x0001E2E7 File Offset: 0x0001C4E7
			internal void <InstallEvents>b__3_1(P2PSessionConnectFail_t x)
			{
				Action<SteamId, P2PSessionError> onP2PConnectionFailed = SteamNetworking.OnP2PConnectionFailed;
				if (onP2PConnectionFailed != null)
				{
					onP2PConnectionFailed(x.SteamIDRemote, (P2PSessionError)x.P2PSessionError);
				}
			}

			// Token: 0x04000D39 RID: 3385
			public static readonly SteamNetworking.<>c <>9 = new SteamNetworking.<>c();

			// Token: 0x04000D3A RID: 3386
			public static Action<P2PSessionRequest_t> <>9__3_0;

			// Token: 0x04000D3B RID: 3387
			public static Action<P2PSessionConnectFail_t> <>9__3_1;
		}
	}
}
