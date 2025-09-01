using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking
{
	// Token: 0x02000002 RID: 2
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	[NativeConditional("ENABLE_NETWORK && ENABLE_UNET", true)]
	[NativeHeader("Modules/UNET/UNETConfiguration.h")]
	[NativeHeader("Modules/UNET/UNetTypes.h")]
	[NativeHeader("Modules/UNET/UNETManager.h")]
	public sealed class NetworkTransport
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool DoesEndPointUsePlatformProtocols(EndPoint endPoint)
		{
			bool flag = endPoint.GetType().FullName == "UnityEngine.PS4.SceEndPoint";
			if (flag)
			{
				SocketAddress socketAddress = endPoint.Serialize();
				bool flag2 = socketAddress[8] != 0 || socketAddress[9] > 0;
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A4 File Offset: 0x000002A4
		public static int ConnectEndPoint(int hostId, EndPoint endPoint, int exceptionConnectionId, out byte error)
		{
			error = 0;
			byte[] array = new byte[]
			{
				95,
				36,
				19,
				246
			};
			bool flag = endPoint == null;
			if (flag)
			{
				throw new NullReferenceException("Null EndPoint provided");
			}
			bool flag2 = endPoint.GetType().FullName != "UnityEngine.XboxOne.XboxOneEndPoint" && endPoint.GetType().FullName != "UnityEngine.PS4.SceEndPoint" && endPoint.GetType().FullName != "UnityEngine.PSVita.SceEndPoint";
			if (flag2)
			{
				throw new ArgumentException("Endpoint of type XboxOneEndPoint or SceEndPoint  required");
			}
			bool flag3 = endPoint.GetType().FullName == "UnityEngine.XboxOne.XboxOneEndPoint";
			int result;
			if (flag3)
			{
				bool flag4 = endPoint.AddressFamily != AddressFamily.InterNetworkV6;
				if (flag4)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid family");
				}
				SocketAddress socketAddress = endPoint.Serialize();
				bool flag5 = socketAddress.Size != 14;
				if (flag5)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid size");
				}
				bool flag6 = socketAddress[0] != 0 || socketAddress[1] > 0;
				if (flag6)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid family signature");
				}
				bool flag7 = socketAddress[2] != array[0] || socketAddress[3] != array[1] || socketAddress[4] != array[2] || socketAddress[5] != array[3];
				if (flag7)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid signature");
				}
				byte[] array2 = new byte[8];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = socketAddress[6 + i];
				}
				IntPtr intPtr = new IntPtr(BitConverter.ToInt64(array2, 0));
				bool flag8 = intPtr == IntPtr.Zero;
				if (flag8)
				{
					throw new ArgumentException("XboxOneEndPoint has an invalid SOCKET_STORAGE pointer");
				}
				byte[] array3 = new byte[2];
				Marshal.Copy(intPtr, array3, 0, array3.Length);
				AddressFamily addressFamily = (AddressFamily)(((int)array3[1] << 8) + (int)array3[0]);
				bool flag9 = addressFamily != AddressFamily.InterNetworkV6;
				if (flag9)
				{
					throw new ArgumentException("XboxOneEndPoint has corrupt or invalid SOCKET_STORAGE pointer");
				}
				result = NetworkTransport.Internal_ConnectEndPoint(hostId, array2, 128, exceptionConnectionId, out error);
			}
			else
			{
				SocketAddress socketAddress2 = endPoint.Serialize();
				bool flag10 = socketAddress2.Size != 16;
				if (flag10)
				{
					throw new ArgumentException("EndPoint has an invalid size");
				}
				bool flag11 = (int)socketAddress2[0] != socketAddress2.Size;
				if (flag11)
				{
					throw new ArgumentException("EndPoint has an invalid size value");
				}
				bool flag12 = socketAddress2[1] != 2;
				if (flag12)
				{
					throw new ArgumentException("EndPoint has an invalid family value");
				}
				byte[] array4 = new byte[16];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = socketAddress2[j];
				}
				int num = NetworkTransport.Internal_ConnectEndPoint(hostId, array4, 16, exceptionConnectionId, out error);
				result = num;
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002371 File Offset: 0x00000571
		private NetworkTransport()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000237B File Offset: 0x0000057B
		public static void Init()
		{
			NetworkTransport.InitializeClass();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002384 File Offset: 0x00000584
		public static void Init(GlobalConfig config)
		{
			bool flag = config.NetworkEventAvailable != null;
			if (flag)
			{
				NetworkTransport.SetNetworkEventAvailableCallback(config.NetworkEventAvailable);
			}
			bool flag2 = config.ConnectionReadyForSend != null;
			if (flag2)
			{
				NetworkTransport.SetConnectionReadyForSendCallback(config.ConnectionReadyForSend);
			}
			NetworkTransport.InitializeClassWithConfig(new GlobalConfigInternal(config));
		}

		// Token: 0x06000006 RID: 6
		[FreeFunction("UNETManager::InitializeClass")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeClass();

		// Token: 0x06000007 RID: 7
		[FreeFunction("UNETManager::InitializeClassWithConfig")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeClassWithConfig(GlobalConfigInternal config);

		// Token: 0x06000008 RID: 8 RVA: 0x000023D0 File Offset: 0x000005D0
		public static void Shutdown()
		{
			NetworkTransport.Cleanup();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023DC File Offset: 0x000005DC
		[Obsolete("This function has been deprecated. Use AssetDatabase utilities instead.")]
		public static string GetAssetId(GameObject go)
		{
			return "";
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023F4 File Offset: 0x000005F4
		public static void AddSceneId(int id)
		{
			bool flag = id > NetworkTransport.s_nextSceneId;
			if (flag)
			{
				NetworkTransport.s_nextSceneId = id + 1;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002418 File Offset: 0x00000618
		public static int GetNextSceneId()
		{
			return NetworkTransport.s_nextSceneId++;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002438 File Offset: 0x00000638
		public static int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout, int port, string ip)
		{
			bool flag = topology == null;
			if (flag)
			{
				throw new NullReferenceException("topology is not defined");
			}
			NetworkTransport.CheckTopology(topology);
			return NetworkTransport.AddHostInternal(new HostTopologyInternal(topology), ip, port, minTimeout, maxTimeout);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002474 File Offset: 0x00000674
		public static int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout, int port)
		{
			return NetworkTransport.AddHostWithSimulator(topology, minTimeout, maxTimeout, port, null);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002490 File Offset: 0x00000690
		public static int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout)
		{
			return NetworkTransport.AddHostWithSimulator(topology, minTimeout, maxTimeout, 0, null);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024AC File Offset: 0x000006AC
		public static int AddHost(HostTopology topology, int port, string ip)
		{
			return NetworkTransport.AddHostWithSimulator(topology, 0, 0, port, ip);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024C8 File Offset: 0x000006C8
		public static int AddHost(HostTopology topology, int port)
		{
			return NetworkTransport.AddHost(topology, port, null);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024E4 File Offset: 0x000006E4
		public static int AddHost(HostTopology topology)
		{
			return NetworkTransport.AddHost(topology, 0, null);
		}

		// Token: 0x06000012 RID: 18
		[FreeFunction("UNETManager::Get()->AddHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddHostInternal(HostTopologyInternal topologyInt, string ip, int port, int minTimeout, int maxTimeout);

		// Token: 0x06000013 RID: 19 RVA: 0x00002500 File Offset: 0x00000700
		public static int AddWebsocketHost(HostTopology topology, int port, string ip)
		{
			bool flag = port != 0;
			if (flag)
			{
				bool flag2 = NetworkTransport.IsPortOpen(ip, port);
				if (flag2)
				{
					throw new InvalidOperationException("Cannot open web socket on port " + port.ToString() + " It has been already occupied.");
				}
			}
			bool flag3 = topology == null;
			if (flag3)
			{
				throw new NullReferenceException("topology is not defined");
			}
			NetworkTransport.CheckTopology(topology);
			return NetworkTransport.AddWsHostInternal(new HostTopologyInternal(topology), ip, port);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000256C File Offset: 0x0000076C
		public static int AddWebsocketHost(HostTopology topology, int port)
		{
			return NetworkTransport.AddWebsocketHost(topology, port, null);
		}

		// Token: 0x06000015 RID: 21
		[FreeFunction("UNETManager::Get()->AddWsHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddWsHostInternal(HostTopologyInternal topologyInt, string ip, int port);

		// Token: 0x06000016 RID: 22 RVA: 0x00002588 File Offset: 0x00000788
		private static bool IsPortOpen(string ip, int port)
		{
			TimeSpan timeout = TimeSpan.FromMilliseconds(500.0);
			string host = (ip == null) ? "127.0.0.1" : ip;
			try
			{
				using (TcpClient tcpClient = new TcpClient())
				{
					IAsyncResult asyncResult = tcpClient.BeginConnect(host, port, null, null);
					bool flag = asyncResult.AsyncWaitHandle.WaitOne(timeout);
					bool flag2 = !flag;
					if (flag2)
					{
						return false;
					}
					tcpClient.EndConnect(asyncResult);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002628 File Offset: 0x00000828
		public static void ConnectAsNetworkHost(int hostId, string address, int port, NetworkID network, SourceID source, NodeID node, out byte error)
		{
			NetworkTransport.ConnectAsNetworkHostInternal(hostId, address, port, (ulong)network, (ulong)source, (ushort)node, out error);
		}

		// Token: 0x06000018 RID: 24
		[FreeFunction("UNETManager::Get()->ConnectAsNetworkHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ConnectAsNetworkHostInternal(int hostId, string address, int port, ulong network, ulong source, ushort node, out byte error);

		// Token: 0x06000019 RID: 25
		[FreeFunction("UNETManager::Get()->DisconnectNetworkHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisconnectNetworkHost(int hostId, out byte error);

		// Token: 0x0600001A RID: 26 RVA: 0x0000263C File Offset: 0x0000083C
		public static NetworkEventType ReceiveRelayEventFromHost(int hostId, out byte error)
		{
			return (NetworkEventType)NetworkTransport.ReceiveRelayEventFromHostInternal(hostId, out error);
		}

		// Token: 0x0600001B RID: 27
		[FreeFunction("UNETManager::Get()->PopRelayHostData", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReceiveRelayEventFromHostInternal(int hostId, out byte error);

		// Token: 0x0600001C RID: 28 RVA: 0x00002658 File Offset: 0x00000858
		public static int ConnectToNetworkPeer(int hostId, string address, int port, int exceptionConnectionId, int relaySlotId, NetworkID network, SourceID source, NodeID node, int bytesPerSec, float bucketSizeFactor, out byte error)
		{
			return NetworkTransport.ConnectToNetworkPeerInternal(hostId, address, port, exceptionConnectionId, relaySlotId, (ulong)network, (ulong)source, (ushort)node, bytesPerSec, bucketSizeFactor, out error);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002684 File Offset: 0x00000884
		public static int ConnectToNetworkPeer(int hostId, string address, int port, int exceptionConnectionId, int relaySlotId, NetworkID network, SourceID source, NodeID node, out byte error)
		{
			return NetworkTransport.ConnectToNetworkPeer(hostId, address, port, exceptionConnectionId, relaySlotId, network, source, node, 0, 0f, out error);
		}

		// Token: 0x0600001E RID: 30
		[FreeFunction("UNETManager::Get()->ConnectToNetworkPeer", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ConnectToNetworkPeerInternal(int hostId, string address, int port, int exceptionConnectionId, int relaySlotId, ulong network, ulong source, ushort node, int bytesPerSec, float bucketSizeFactor, out byte error);

		// Token: 0x0600001F RID: 31 RVA: 0x000026B0 File Offset: 0x000008B0
		[Obsolete("GetCurrentIncomingMessageAmount has been deprecated.")]
		public static int GetCurrentIncomingMessageAmount()
		{
			return 0;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026C4 File Offset: 0x000008C4
		[Obsolete("GetCurrentOutgoingMessageAmount has been deprecated.")]
		public static int GetCurrentOutgoingMessageAmount()
		{
			return 0;
		}

		// Token: 0x06000021 RID: 33
		[FreeFunction("UNETManager::Get()->GetIncomingMessageQueueSize", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingMessageQueueSize(int hostId, out byte error);

		// Token: 0x06000022 RID: 34
		[FreeFunction("UNETManager::Get()->GetOutgoingMessageQueueSize", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingMessageQueueSize(int hostId, out byte error);

		// Token: 0x06000023 RID: 35
		[FreeFunction("UNETManager::Get()->GetCurrentRTT", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCurrentRTT(int hostId, int connectionId, out byte error);

		// Token: 0x06000024 RID: 36 RVA: 0x000026D8 File Offset: 0x000008D8
		[Obsolete("GetCurrentRtt() has been deprecated.")]
		public static int GetCurrentRtt(int hostId, int connectionId, out byte error)
		{
			return NetworkTransport.GetCurrentRTT(hostId, connectionId, out error);
		}

		// Token: 0x06000025 RID: 37
		[FreeFunction("UNETManager::Get()->GetIncomingPacketLossCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingPacketLossCount(int hostId, int connectionId, out byte error);

		// Token: 0x06000026 RID: 38 RVA: 0x000026F4 File Offset: 0x000008F4
		[Obsolete("GetNetworkLostPacketNum() has been deprecated.")]
		public static int GetNetworkLostPacketNum(int hostId, int connectionId, out byte error)
		{
			return NetworkTransport.GetIncomingPacketLossCount(hostId, connectionId, out error);
		}

		// Token: 0x06000027 RID: 39
		[FreeFunction("UNETManager::Get()->GetIncomingPacketCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingPacketCount(int hostId, int connectionId, out byte error);

		// Token: 0x06000028 RID: 40
		[FreeFunction("UNETManager::Get()->GetOutgoingPacketNetworkLossPercent", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketNetworkLossPercent(int hostId, int connectionId, out byte error);

		// Token: 0x06000029 RID: 41
		[FreeFunction("UNETManager::Get()->GetOutgoingPacketOverflowLossPercent", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketOverflowLossPercent(int hostId, int connectionId, out byte error);

		// Token: 0x0600002A RID: 42
		[FreeFunction("UNETManager::Get()->GetMaxAllowedBandwidth", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetMaxAllowedBandwidth(int hostId, int connectionId, out byte error);

		// Token: 0x0600002B RID: 43
		[FreeFunction("UNETManager::Get()->GetAckBufferCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAckBufferCount(int hostId, int connectionId, out byte error);

		// Token: 0x0600002C RID: 44
		[FreeFunction("UNETManager::Get()->GetIncomingPacketDropCountForAllHosts", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingPacketDropCountForAllHosts();

		// Token: 0x0600002D RID: 45
		[FreeFunction("UNETManager::Get()->GetIncomingPacketCountForAllHosts", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIncomingPacketCountForAllHosts();

		// Token: 0x0600002E RID: 46
		[FreeFunction("UNETManager::Get()->GetOutgoingPacketCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketCount();

		// Token: 0x0600002F RID: 47
		[FreeFunction("UNETManager::Get()->GetOutgoingPacketCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketCountForHost(int hostId, out byte error);

		// Token: 0x06000030 RID: 48
		[FreeFunction("UNETManager::Get()->GetOutgoingPacketCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingPacketCountForConnection(int hostId, int connectionId, out byte error);

		// Token: 0x06000031 RID: 49
		[FreeFunction("UNETManager::Get()->GetOutgoingMessageCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingMessageCount();

		// Token: 0x06000032 RID: 50
		[FreeFunction("UNETManager::Get()->GetOutgoingMessageCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingMessageCountForHost(int hostId, out byte error);

		// Token: 0x06000033 RID: 51
		[FreeFunction("UNETManager::Get()->GetOutgoingMessageCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingMessageCountForConnection(int hostId, int connectionId, out byte error);

		// Token: 0x06000034 RID: 52
		[FreeFunction("UNETManager::Get()->GetOutgoingUserBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingUserBytesCount();

		// Token: 0x06000035 RID: 53
		[FreeFunction("UNETManager::Get()->GetOutgoingUserBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingUserBytesCountForHost(int hostId, out byte error);

		// Token: 0x06000036 RID: 54
		[FreeFunction("UNETManager::Get()->GetOutgoingUserBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingUserBytesCountForConnection(int hostId, int connectionId, out byte error);

		// Token: 0x06000037 RID: 55
		[FreeFunction("UNETManager::Get()->GetOutgoingSystemBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingSystemBytesCount();

		// Token: 0x06000038 RID: 56
		[FreeFunction("UNETManager::Get()->GetOutgoingSystemBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingSystemBytesCountForHost(int hostId, out byte error);

		// Token: 0x06000039 RID: 57
		[FreeFunction("UNETManager::Get()->GetOutgoingSystemBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingSystemBytesCountForConnection(int hostId, int connectionId, out byte error);

		// Token: 0x0600003A RID: 58
		[FreeFunction("UNETManager::Get()->GetOutgoingFullBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingFullBytesCount();

		// Token: 0x0600003B RID: 59
		[FreeFunction("UNETManager::Get()->GetOutgoingFullBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingFullBytesCountForHost(int hostId, out byte error);

		// Token: 0x0600003C RID: 60
		[FreeFunction("UNETManager::Get()->GetOutgoingFullBytesCount", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetOutgoingFullBytesCountForConnection(int hostId, int connectionId, out byte error);

		// Token: 0x0600003D RID: 61 RVA: 0x00002710 File Offset: 0x00000910
		[Obsolete("GetPacketSentRate has been deprecated.")]
		public static int GetPacketSentRate(int hostId, int connectionId, out byte error)
		{
			error = 0;
			return 0;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002728 File Offset: 0x00000928
		[Obsolete("GetPacketReceivedRate has been deprecated.")]
		public static int GetPacketReceivedRate(int hostId, int connectionId, out byte error)
		{
			error = 0;
			return 0;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002740 File Offset: 0x00000940
		[Obsolete("GetRemotePacketReceivedRate has been deprecated.")]
		public static int GetRemotePacketReceivedRate(int hostId, int connectionId, out byte error)
		{
			error = 0;
			return 0;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002758 File Offset: 0x00000958
		[Obsolete("GetNetIOTimeuS has been deprecated.")]
		public static int GetNetIOTimeuS()
		{
			return 0;
		}

		// Token: 0x06000041 RID: 65
		[FreeFunction("UNETManager::Get()->GetConnectionInfo", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetConnectionInfo(int hostId, int connectionId, out int port, out ulong network, out ushort dstNode, out byte error);

		// Token: 0x06000042 RID: 66 RVA: 0x0000276C File Offset: 0x0000096C
		public static void GetConnectionInfo(int hostId, int connectionId, out string address, out int port, out NetworkID network, out NodeID dstNode, out byte error)
		{
			ulong num;
			ushort num2;
			address = NetworkTransport.GetConnectionInfo(hostId, connectionId, out port, out num, out num2, out error);
			network = (NetworkID)num;
			dstNode = (NodeID)num2;
		}

		// Token: 0x06000043 RID: 67
		[FreeFunction("UNETManager::Get()->GetNetworkTimestamp", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNetworkTimestamp();

		// Token: 0x06000044 RID: 68
		[FreeFunction("UNETManager::Get()->GetRemoteDelayTimeMS", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetRemoteDelayTimeMS(int hostId, int connectionId, int remoteTime, out byte error);

		// Token: 0x06000045 RID: 69 RVA: 0x00002794 File Offset: 0x00000994
		public static bool StartSendMulticast(int hostId, int channelId, byte[] buffer, int size, out byte error)
		{
			return NetworkTransport.StartSendMulticastInternal(hostId, channelId, buffer, size, out error);
		}

		// Token: 0x06000046 RID: 70
		[FreeFunction("UNETManager::Get()->StartSendMulticast", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool StartSendMulticastInternal(int hostId, int channelId, [Out] byte[] buffer, int size, out byte error);

		// Token: 0x06000047 RID: 71
		[FreeFunction("UNETManager::Get()->SendMulticast", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SendMulticast(int hostId, int connectionId, out byte error);

		// Token: 0x06000048 RID: 72
		[FreeFunction("UNETManager::Get()->FinishSendMulticast", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool FinishSendMulticast(int hostId, out byte error);

		// Token: 0x06000049 RID: 73
		[FreeFunction("UNETManager::Get()->GetMaxPacketSize", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxPacketSize();

		// Token: 0x0600004A RID: 74
		[FreeFunction("UNETManager::Get()->RemoveHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RemoveHost(int hostId);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000027B4 File Offset: 0x000009B4
		public static bool IsStarted
		{
			get
			{
				return NetworkTransport.IsStartedInternal();
			}
		}

		// Token: 0x0600004C RID: 76
		[FreeFunction("UNETManager::IsStarted")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsStartedInternal();

		// Token: 0x0600004D RID: 77
		[FreeFunction("UNETManager::Get()->Connect", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Connect(int hostId, string address, int port, int exeptionConnectionId, out byte error);

		// Token: 0x0600004E RID: 78
		[FreeFunction("UNETManager::Get()->ConnectWithSimulator", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ConnectWithSimulatorInternal(int hostId, string address, int port, int exeptionConnectionId, out byte error, ConnectionSimulatorConfigInternal conf);

		// Token: 0x0600004F RID: 79 RVA: 0x000027CC File Offset: 0x000009CC
		public static int ConnectWithSimulator(int hostId, string address, int port, int exeptionConnectionId, out byte error, ConnectionSimulatorConfig conf)
		{
			return NetworkTransport.ConnectWithSimulatorInternal(hostId, address, port, exeptionConnectionId, out error, new ConnectionSimulatorConfigInternal(conf));
		}

		// Token: 0x06000050 RID: 80
		[FreeFunction("UNETManager::Get()->Disconnect", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Disconnect(int hostId, int connectionId, out byte error);

		// Token: 0x06000051 RID: 81
		[FreeFunction("UNETManager::Get()->ConnectSockAddr", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_ConnectEndPoint(int hostId, [Out] byte[] sockAddrStorage, int sockAddrStorageLen, int exceptionConnectionId, out byte error);

		// Token: 0x06000052 RID: 82 RVA: 0x000027F0 File Offset: 0x000009F0
		public static bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int size, out byte error)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new NullReferenceException("send buffer is not initialized");
			}
			return NetworkTransport.SendWrapper(hostId, connectionId, channelId, buffer, size, out error);
		}

		// Token: 0x06000053 RID: 83
		[FreeFunction("UNETManager::Get()->Send", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SendWrapper(int hostId, int connectionId, int channelId, [Out] byte[] buffer, int size, out byte error);

		// Token: 0x06000054 RID: 84 RVA: 0x00002824 File Offset: 0x00000A24
		public static bool QueueMessageForSending(int hostId, int connectionId, int channelId, byte[] buffer, int size, out byte error)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new NullReferenceException("send buffer is not initialized");
			}
			return NetworkTransport.QueueMessageForSendingWrapper(hostId, connectionId, channelId, buffer, size, out error);
		}

		// Token: 0x06000055 RID: 85
		[FreeFunction("UNETManager::Get()->QueueMessageForSending", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool QueueMessageForSendingWrapper(int hostId, int connectionId, int channelId, [Out] byte[] buffer, int size, out byte error);

		// Token: 0x06000056 RID: 86
		[FreeFunction("UNETManager::Get()->SendQueuedMessages", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SendQueuedMessages(int hostId, int connectionId, out byte error);

		// Token: 0x06000057 RID: 87 RVA: 0x00002858 File Offset: 0x00000A58
		public static NetworkEventType Receive(out int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
		{
			return (NetworkEventType)NetworkTransport.PopData(out hostId, out connectionId, out channelId, buffer, bufferSize, out receivedSize, out error);
		}

		// Token: 0x06000058 RID: 88
		[FreeFunction("UNETManager::Get()->PopData", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopData(out int hostId, out int connectionId, out int channelId, [Out] byte[] buffer, int bufferSize, out int receivedSize, out byte error);

		// Token: 0x06000059 RID: 89 RVA: 0x0000287C File Offset: 0x00000A7C
		public static NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
		{
			return (NetworkEventType)NetworkTransport.PopDataFromHost(hostId, out connectionId, out channelId, buffer, bufferSize, out receivedSize, out error);
		}

		// Token: 0x0600005A RID: 90
		[FreeFunction("UNETManager::Get()->PopDataFromHost", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PopDataFromHost(int hostId, out int connectionId, out int channelId, [Out] byte[] buffer, int bufferSize, out int receivedSize, out byte error);

		// Token: 0x0600005B RID: 91
		[FreeFunction("UNETManager::Get()->SetPacketStat", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetPacketStat(int direction, int packetStatId, int numMsgs, int numBytes);

		// Token: 0x0600005C RID: 92
		[FreeFunction("UNETManager::SetNetworkEventAvailableCallback")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNetworkEventAvailableCallback(Action<int> callback);

		// Token: 0x0600005D RID: 93
		[FreeFunction("UNETManager::Cleanup")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cleanup();

		// Token: 0x0600005E RID: 94
		[FreeFunction("UNETManager::SetConnectionReadyForSendCallback")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConnectionReadyForSendCallback(Action<int, int> callback);

		// Token: 0x0600005F RID: 95
		[FreeFunction("UNETManager::Get()->NotifyWhenConnectionReadyForSend", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool NotifyWhenConnectionReadyForSend(int hostId, int connectionId, int notificationLevel, out byte error);

		// Token: 0x06000060 RID: 96
		[FreeFunction("UNETManager::Get()->GetHostPort", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetHostPort(int hostId);

		// Token: 0x06000061 RID: 97
		[FreeFunction("UNETManager::Get()->StartBroadcastDiscoveryWithData", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool StartBroadcastDiscoveryWithData(int hostId, int broadcastPort, int key, int version, int subversion, [Out] byte[] buffer, int size, int timeout, out byte error);

		// Token: 0x06000062 RID: 98
		[FreeFunction("UNETManager::Get()->StartBroadcastDiscoveryWithoutData", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool StartBroadcastDiscoveryWithoutData(int hostId, int broadcastPort, int key, int version, int subversion, int timeout, out byte error);

		// Token: 0x06000063 RID: 99 RVA: 0x000028A0 File Offset: 0x00000AA0
		public static bool StartBroadcastDiscovery(int hostId, int broadcastPort, int key, int version, int subversion, byte[] buffer, int size, int timeout, out byte error)
		{
			bool flag = buffer != null;
			if (flag)
			{
				bool flag2 = buffer.Length < size;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Size: " + size.ToString() + " > buffer.Length " + buffer.Length.ToString());
				}
				bool flag3 = size == 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("Size is zero while buffer exists, please pass null and 0 as buffer and size parameters");
				}
			}
			bool flag4 = buffer == null;
			bool result;
			if (flag4)
			{
				result = NetworkTransport.StartBroadcastDiscoveryWithoutData(hostId, broadcastPort, key, version, subversion, timeout, out error);
			}
			else
			{
				result = NetworkTransport.StartBroadcastDiscoveryWithData(hostId, broadcastPort, key, version, subversion, buffer, size, timeout, out error);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100
		[FreeFunction("UNETManager::Get()->StopBroadcastDiscovery", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopBroadcastDiscovery();

		// Token: 0x06000065 RID: 101
		[FreeFunction("UNETManager::Get()->IsBroadcastDiscoveryRunning", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBroadcastDiscoveryRunning();

		// Token: 0x06000066 RID: 102
		[FreeFunction("UNETManager::Get()->SetBroadcastCredentials", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBroadcastCredentials(int hostId, int key, int version, int subversion, out byte error);

		// Token: 0x06000067 RID: 103
		[FreeFunction("UNETManager::Get()->GetBroadcastConnectionInfoInternal", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetBroadcastConnectionInfo(int hostId, out int port, out byte error);

		// Token: 0x06000068 RID: 104 RVA: 0x00002938 File Offset: 0x00000B38
		public static void GetBroadcastConnectionInfo(int hostId, out string address, out int port, out byte error)
		{
			address = NetworkTransport.GetBroadcastConnectionInfo(hostId, out port, out error);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002945 File Offset: 0x00000B45
		public static void GetBroadcastConnectionMessage(int hostId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
		{
			NetworkTransport.GetBroadcastConnectionMessageInternal(hostId, buffer, bufferSize, out receivedSize, out error);
		}

		// Token: 0x0600006A RID: 106
		[FreeFunction("UNETManager::SetMulticastLock")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMulticastLock(bool enabled);

		// Token: 0x0600006B RID: 107
		[FreeFunction("UNETManager::Get()->GetBroadcastConnectionMessage", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBroadcastConnectionMessageInternal(int hostId, [Out] byte[] buffer, int bufferSize, out int receivedSize, out byte error);

		// Token: 0x0600006C RID: 108 RVA: 0x00002954 File Offset: 0x00000B54
		private static void CheckTopology(HostTopology topology)
		{
			int maxPacketSize = NetworkTransport.GetMaxPacketSize();
			bool flag = (int)topology.DefaultConfig.PacketSize > maxPacketSize;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("Default config: packet size should be less than packet size defined in global config: " + maxPacketSize.ToString());
			}
			for (int i = 0; i < topology.SpecialConnectionConfigs.Count; i++)
			{
				bool flag2 = (int)topology.SpecialConnectionConfigs[i].PacketSize > maxPacketSize;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Special config " + i.ToString() + ": packet size should be less than packet size defined in global config: " + maxPacketSize.ToString());
				}
			}
		}

		// Token: 0x0600006D RID: 109
		[FreeFunction("UNETManager::Get()->LoadEncryptionLibrary", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadEncryptionLibraryInternal(string libraryName);

		// Token: 0x0600006E RID: 110 RVA: 0x000029EC File Offset: 0x00000BEC
		public static bool LoadEncryptionLibrary(string libraryName)
		{
			return NetworkTransport.LoadEncryptionLibraryInternal(libraryName);
		}

		// Token: 0x0600006F RID: 111
		[FreeFunction("UNETManager::Get()->UnloadEncryptionLibrary", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnloadEncryptionLibraryInternal();

		// Token: 0x06000070 RID: 112 RVA: 0x00002A04 File Offset: 0x00000C04
		public static void UnloadEncryptionLibrary()
		{
			NetworkTransport.UnloadEncryptionLibraryInternal();
		}

		// Token: 0x06000071 RID: 113
		[FreeFunction("UNETManager::Get()->IsEncryptionActive", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEncryptionActiveInternal();

		// Token: 0x06000072 RID: 114 RVA: 0x00002A10 File Offset: 0x00000C10
		public static bool IsEncryptionActive()
		{
			return NetworkTransport.IsEncryptionActiveInternal();
		}

		// Token: 0x06000073 RID: 115
		[FreeFunction("UNETManager::Get()->GetEncryptionSafeMaxPacketSize", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern short GetEncryptionSafeMaxPacketSizeInternal(short maxPacketSize);

		// Token: 0x06000074 RID: 116 RVA: 0x00002A28 File Offset: 0x00000C28
		public static short GetEncryptionSafeMaxPacketSize(short maxPacketSize)
		{
			return NetworkTransport.GetEncryptionSafeMaxPacketSizeInternal(maxPacketSize);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002A40 File Offset: 0x00000C40
		// Note: this type is marked as 'beforefieldinit'.
		static NetworkTransport()
		{
		}

		// Token: 0x04000001 RID: 1
		private static int s_nextSceneId = 1;
	}
}
