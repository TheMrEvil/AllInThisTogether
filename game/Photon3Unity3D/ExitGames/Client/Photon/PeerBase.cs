using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using ExitGames.Client.Photon.Encryption;
using Photon.SocketServer.Security;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200001C RID: 28
	public abstract class PeerBase
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00008C6A File Offset: 0x00006E6A
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00008C72 File Offset: 0x00006E72
		public string ServerAddress
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerAddress>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ServerAddress>k__BackingField = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00008C7B File Offset: 0x00006E7B
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00008C83 File Offset: 0x00006E83
		public string ProxyServerAddress
		{
			[CompilerGenerated]
			get
			{
				return this.<ProxyServerAddress>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ProxyServerAddress>k__BackingField = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00008C8C File Offset: 0x00006E8C
		internal IPhotonPeerListener Listener
		{
			get
			{
				return this.photonPeer.Listener;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00008CAC File Offset: 0x00006EAC
		public DebugLevel debugOut
		{
			get
			{
				return this.photonPeer.DebugOut;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00008CCC File Offset: 0x00006ECC
		internal int DisconnectTimeout
		{
			get
			{
				return this.photonPeer.DisconnectTimeout;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00008CEC File Offset: 0x00006EEC
		internal int timePingInterval
		{
			get
			{
				return this.photonPeer.TimePingInterval;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00008D0C File Offset: 0x00006F0C
		internal byte ChannelCount
		{
			get
			{
				return this.photonPeer.ChannelCount;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00008D2C File Offset: 0x00006F2C
		internal long BytesOut
		{
			get
			{
				return this.bytesOut;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00008D44 File Offset: 0x00006F44
		internal long BytesIn
		{
			get
			{
				return this.bytesIn;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000109 RID: 265
		internal abstract int QueuedIncomingCommandsCount { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600010A RID: 266
		internal abstract int QueuedOutgoingCommandsCount { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00008D5C File Offset: 0x00006F5C
		internal virtual int SentReliableCommandsCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00008D70 File Offset: 0x00006F70
		public virtual string PeerID
		{
			get
			{
				return ((ushort)this.peerID).ToString();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00008D94 File Offset: 0x00006F94
		internal int timeInt
		{
			get
			{
				return (int)this.watch.ElapsedMilliseconds;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00008DB4 File Offset: 0x00006FB4
		internal static int outgoingStreamBufferSize
		{
			get
			{
				return PhotonPeer.OutgoingStreamBufferSize;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00008DCC File Offset: 0x00006FCC
		internal int mtu
		{
			get
			{
				return this.photonPeer.MaximumTransferUnit;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00008DEC File Offset: 0x00006FEC
		protected internal bool IsIpv6
		{
			get
			{
				return this.PhotonSocket != null && this.PhotonSocket.AddressResolvedAsIpv6;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008E14 File Offset: 0x00007014
		protected PeerBase()
		{
			this.networkSimulationSettings.peerBase = this;
			PeerBase.peerCount += 1;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00008E94 File Offset: 0x00007094
		public static StreamBuffer MessageBufferPoolGet()
		{
			Queue<StreamBuffer> messageBufferPool = PeerBase.MessageBufferPool;
			StreamBuffer result;
			lock (messageBufferPool)
			{
				bool flag2 = PeerBase.MessageBufferPool.Count > 0;
				if (flag2)
				{
					result = PeerBase.MessageBufferPool.Dequeue();
				}
				else
				{
					result = new StreamBuffer(75);
				}
			}
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008EFC File Offset: 0x000070FC
		public static void MessageBufferPoolPut(StreamBuffer buff)
		{
			buff.Position = 0;
			buff.SetLength(0L);
			Queue<StreamBuffer> messageBufferPool = PeerBase.MessageBufferPool;
			lock (messageBufferPool)
			{
				PeerBase.MessageBufferPool.Enqueue(buff);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008F58 File Offset: 0x00007158
		internal virtual void Reset()
		{
			this.SerializationProtocol = SerializationProtocolFactory.Create(this.photonPeer.SerializationProtocolType);
			this.photonPeer.InitializeTrafficStats();
			this.ByteCountLastOperation = 0;
			this.ByteCountCurrentDispatch = 0;
			this.bytesIn = 0L;
			this.bytesOut = 0L;
			this.packetLossByCrc = 0;
			this.packetLossByChallenge = 0;
			this.networkSimulationSettings.LostPackagesIn = 0;
			this.networkSimulationSettings.LostPackagesOut = 0;
			LinkedList<SimulationItem> netSimListOutgoing = this.NetSimListOutgoing;
			lock (netSimListOutgoing)
			{
				this.NetSimListOutgoing.Clear();
			}
			LinkedList<SimulationItem> netSimListIncoming = this.NetSimListIncoming;
			lock (netSimListIncoming)
			{
				this.NetSimListIncoming.Clear();
			}
			Queue<PeerBase.MyAction> actionQueue = this.ActionQueue;
			lock (actionQueue)
			{
				this.ActionQueue.Clear();
			}
			this.peerConnectionState = ConnectionStateValue.Disconnected;
			this.watch.Reset();
			this.watch.Start();
			this.isEncryptionAvailable = false;
			this.ApplicationIsInitialized = false;
			this.CryptoProvider = null;
			this.roundTripTime = 200;
			this.roundTripTimeVariance = 5;
			this.serverTimeOffsetIsAvailable = false;
			this.serverTimeOffset = 0;
		}

		// Token: 0x06000115 RID: 277
		internal abstract bool Connect(string serverAddress, string proxyServerAddress, string appID, object photonToken);

		// Token: 0x06000116 RID: 278 RVA: 0x000090D8 File Offset: 0x000072D8
		private string GetHttpKeyValueString(Dictionary<string, string> dic)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in dic)
			{
				stringBuilder.Append(keyValuePair.Key).Append("=").Append(keyValuePair.Value).Append("&");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009164 File Offset: 0x00007364
		internal byte[] WriteInitRequest()
		{
			bool flag = this.PhotonSocket == null || !this.PhotonSocket.Connected;
			if (flag)
			{
				this.EnqueueDebugReturn(DebugLevel.WARNING, "The peer attempts to prepare an Init-Request but the socket is not connected!?");
			}
			bool useInitV = this.photonPeer.UseInitV3;
			byte[] result;
			if (useInitV)
			{
				result = this.WriteInitV3();
			}
			else
			{
				bool flag2 = this.PhotonToken == null;
				if (flag2)
				{
					byte[] array = new byte[41];
					byte[] clientVersion = Version.clientVersion;
					array[0] = 243;
					array[1] = 0;
					array[2] = this.SerializationProtocol.VersionBytes[0];
					array[3] = this.SerializationProtocol.VersionBytes[1];
					array[4] = this.photonPeer.ClientSdkIdShifted;
					array[5] = ((byte)(clientVersion[0] << 4) | clientVersion[1]);
					array[6] = clientVersion[2];
					array[7] = clientVersion[3];
					array[8] = 0;
					bool flag3 = string.IsNullOrEmpty(this.AppId);
					if (flag3)
					{
						this.AppId = "LoadBalancing";
					}
					for (int i = 0; i < 32; i++)
					{
						array[i + 9] = ((i < this.AppId.Length) ? ((byte)this.AppId[i]) : 0);
					}
					bool isIpv = this.IsIpv6;
					if (isIpv)
					{
						byte[] array2 = array;
						int num = 5;
						array2[num] |= 128;
					}
					else
					{
						byte[] array3 = array;
						int num2 = 5;
						array3[num2] &= 127;
					}
					result = array;
				}
				else
				{
					bool flag4 = this.PhotonToken != null;
					if (flag4)
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						dictionary["init"] = null;
						dictionary["app"] = this.AppId;
						dictionary["clientversion"] = this.photonPeer.ClientVersion;
						dictionary["protocol"] = this.SerializationProtocol.ProtocolType;
						dictionary["sid"] = this.photonPeer.ClientSdkIdShifted.ToString();
						byte[] array4 = null;
						int num3 = 0;
						bool flag5 = this.PhotonToken != null;
						if (flag5)
						{
							array4 = this.SerializationProtocol.Serialize(this.PhotonToken);
							num3 += array4.Length;
						}
						string text = this.GetHttpKeyValueString(dictionary);
						bool isIpv2 = this.IsIpv6;
						if (isIpv2)
						{
							text += "&IPv6";
						}
						string text2 = string.Format("POST /?{0} HTTP/1.1\r\nHost: {1}\r\nContent-Length: {2}\r\n\r\n", text, this.ServerAddress, num3);
						byte[] array5 = new byte[text2.Length + num3];
						bool flag6 = array4 != null;
						if (flag6)
						{
							Buffer.BlockCopy(array4, 0, array5, text2.Length, array4.Length);
						}
						Buffer.BlockCopy(Encoding.UTF8.GetBytes(text2), 0, array5, 0, text2.Length);
						result = array5;
					}
					else
					{
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009438 File Offset: 0x00007638
		private byte[] WriteInitV3()
		{
			StreamBuffer streamBuffer = new StreamBuffer(0);
			streamBuffer.WriteByte(245);
			InitV3Flags initV3Flags = InitV3Flags.NoFlags;
			bool isIpv = this.IsIpv6;
			if (isIpv)
			{
				initV3Flags |= InitV3Flags.IPv6Flag;
			}
			IPhotonEncryptor encryptor = this.photonPeer.Encryptor;
			bool flag = encryptor != null;
			if (flag)
			{
				initV3Flags |= InitV3Flags.EncryptionFlag;
			}
			streamBuffer.WriteBytes((byte)(initV3Flags >> 8), (byte)initV3Flags);
			byte b = this.SerializationProtocol.VersionBytes[1];
			byte b2 = b;
			if (b2 != 6)
			{
				if (b2 != 8)
				{
					throw new Exception("Unknown protocol version: " + this.SerializationProtocol.VersionBytes[1].ToString());
				}
				streamBuffer.WriteByte(18);
			}
			else
			{
				streamBuffer.WriteByte(16);
			}
			streamBuffer.Write(Version.clientVersion, 0, 4);
			streamBuffer.WriteByte(this.photonPeer.ClientSdkIdShifted);
			streamBuffer.WriteByte(0);
			bool flag2 = string.IsNullOrEmpty(this.AppId);
			if (flag2)
			{
				this.AppId = "Master";
			}
			byte[] bytes = Encoding.UTF8.GetBytes(this.AppId);
			int num = bytes.Length;
			bool flag3 = num > 255;
			if (flag3)
			{
				throw new Exception("AppId is too long. Limited by 255 symbols.");
			}
			streamBuffer.WriteByte((byte)num);
			streamBuffer.Write(bytes, 0, bytes.Length);
			byte[] array = this.PhotonToken as byte[];
			bool flag4 = array != null;
			if (flag4)
			{
				num = array.Length;
				streamBuffer.WriteBytes((byte)(num >> 8), (byte)num);
				streamBuffer.Write(array, 0, num);
			}
			else
			{
				streamBuffer.WriteBytes(0, 0);
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			bool flag5 = this.CustomInitData != null;
			if (flag5)
			{
				dictionary.Add(0, this.CustomInitData);
			}
			bool flag6 = encryptor != null;
			if (flag6)
			{
				throw new NotImplementedException("InitV3 with encryption is not implemented yet.");
			}
			this.SerializationProtocol.Serialize(streamBuffer, dictionary, true);
			return streamBuffer.ToArray();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009624 File Offset: 0x00007824
		internal string PrepareWebSocketUrl(string serverAddress, string appId, object photonToken)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append(serverAddress);
			stringBuilder.AppendFormat("/?libversion={0}&sid={1}", this.photonPeer.ClientVersion, this.photonPeer.ClientSdkIdShifted);
			bool flag = !this.photonPeer.RemoveAppIdFromWebSocketPath;
			if (flag)
			{
				stringBuilder.AppendFormat("&app={0}", appId);
			}
			bool isIpv = this.IsIpv6;
			if (isIpv)
			{
				stringBuilder.Append("&IPv6");
			}
			bool flag2 = photonToken != null;
			if (flag2)
			{
				stringBuilder.Append("&xInit=");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600011A RID: 282
		public abstract void OnConnect();

		// Token: 0x0600011B RID: 283 RVA: 0x000096C8 File Offset: 0x000078C8
		internal void InitCallback()
		{
			bool flag = this.peerConnectionState == ConnectionStateValue.Connecting;
			if (flag)
			{
				this.peerConnectionState = ConnectionStateValue.Connected;
			}
			this.ApplicationIsInitialized = true;
			this.FetchServerTimestamp();
			this.Listener.OnStatusChanged(StatusCode.Connect);
		}

		// Token: 0x0600011C RID: 284
		internal abstract void Disconnect();

		// Token: 0x0600011D RID: 285
		internal abstract void StopConnection();

		// Token: 0x0600011E RID: 286
		internal abstract void FetchServerTimestamp();

		// Token: 0x0600011F RID: 287
		internal abstract bool IsTransportEncrypted();

		// Token: 0x06000120 RID: 288
		internal abstract bool EnqueuePhotonMessage(StreamBuffer opBytes, SendOptions sendParams);

		// Token: 0x06000121 RID: 289 RVA: 0x0000970C File Offset: 0x0000790C
		internal StreamBuffer SerializeOperationToMessage(byte opCode, Dictionary<byte, object> parameters, EgMessageType messageType, bool encrypt)
		{
			bool flag = encrypt && !this.IsTransportEncrypted();
			StreamBuffer streamBuffer = PeerBase.MessageBufferPoolGet();
			streamBuffer.SetLength(0L);
			bool flag2 = !flag;
			if (flag2)
			{
				streamBuffer.Write(this.messageHeader, 0, this.messageHeader.Length);
			}
			this.SerializationProtocol.SerializeOperationRequest(streamBuffer, opCode, parameters, false);
			bool flag3 = flag;
			if (flag3)
			{
				byte[] array = this.CryptoProvider.Encrypt(streamBuffer.GetBuffer(), 0, streamBuffer.Length);
				streamBuffer.SetLength(0L);
				streamBuffer.Write(this.messageHeader, 0, this.messageHeader.Length);
				streamBuffer.Write(array, 0, array.Length);
			}
			byte[] buffer = streamBuffer.GetBuffer();
			bool flag4 = messageType != EgMessageType.Operation;
			if (flag4)
			{
				buffer[this.messageHeader.Length - 1] = (byte)messageType;
			}
			bool flag5 = flag || (encrypt && this.photonPeer.EnableEncryptedFlag);
			if (flag5)
			{
				buffer[this.messageHeader.Length - 1] = (buffer[this.messageHeader.Length - 1] | 128);
			}
			return streamBuffer;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009824 File Offset: 0x00007A24
		internal StreamBuffer SerializeOperationToMessage(byte opCode, ParameterDictionary parameters, EgMessageType messageType, bool encrypt)
		{
			bool flag = encrypt && !this.IsTransportEncrypted();
			StreamBuffer streamBuffer = PeerBase.MessageBufferPoolGet();
			streamBuffer.SetLength(0L);
			bool flag2 = !flag;
			if (flag2)
			{
				streamBuffer.Write(this.messageHeader, 0, this.messageHeader.Length);
			}
			this.SerializationProtocol.SerializeOperationRequest(streamBuffer, opCode, parameters, false);
			bool flag3 = flag;
			if (flag3)
			{
				byte[] array = this.CryptoProvider.Encrypt(streamBuffer.GetBuffer(), 0, streamBuffer.Length);
				streamBuffer.SetLength(0L);
				streamBuffer.Write(this.messageHeader, 0, this.messageHeader.Length);
				streamBuffer.Write(array, 0, array.Length);
			}
			byte[] buffer = streamBuffer.GetBuffer();
			bool flag4 = messageType != EgMessageType.Operation;
			if (flag4)
			{
				buffer[this.messageHeader.Length - 1] = (byte)messageType;
			}
			bool flag5 = flag || (encrypt && this.photonPeer.EnableEncryptedFlag);
			if (flag5)
			{
				buffer[this.messageHeader.Length - 1] = (buffer[this.messageHeader.Length - 1] | 128);
			}
			return streamBuffer;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000993C File Offset: 0x00007B3C
		internal StreamBuffer SerializeMessageToMessage(object message, bool encrypt)
		{
			bool flag = encrypt && !this.IsTransportEncrypted();
			StreamBuffer streamBuffer = PeerBase.MessageBufferPoolGet();
			streamBuffer.SetLength(0L);
			bool flag2 = !flag;
			if (flag2)
			{
				streamBuffer.Write(this.messageHeader, 0, this.messageHeader.Length);
			}
			bool flag3 = message is byte[];
			bool flag4 = flag3;
			if (flag4)
			{
				byte[] array = message as byte[];
				streamBuffer.Write(array, 0, array.Length);
			}
			else
			{
				this.SerializationProtocol.SerializeMessage(streamBuffer, message);
			}
			bool flag5 = flag;
			if (flag5)
			{
				byte[] array2 = this.CryptoProvider.Encrypt(streamBuffer.GetBuffer(), 0, streamBuffer.Length);
				streamBuffer.SetLength(0L);
				streamBuffer.Write(this.messageHeader, 0, this.messageHeader.Length);
				streamBuffer.Write(array2, 0, array2.Length);
			}
			byte[] buffer = streamBuffer.GetBuffer();
			buffer[this.messageHeader.Length - 1] = (flag3 ? 9 : 8);
			bool flag6 = flag || (encrypt && this.photonPeer.EnableEncryptedFlag);
			if (flag6)
			{
				buffer[this.messageHeader.Length - 1] = (buffer[this.messageHeader.Length - 1] | 128);
			}
			return streamBuffer;
		}

		// Token: 0x06000124 RID: 292
		internal abstract bool SendOutgoingCommands();

		// Token: 0x06000125 RID: 293 RVA: 0x00009A78 File Offset: 0x00007C78
		internal virtual bool SendAcksOnly()
		{
			return false;
		}

		// Token: 0x06000126 RID: 294
		internal abstract void ReceiveIncomingCommands(byte[] inBuff, int dataLength);

		// Token: 0x06000127 RID: 295
		internal abstract bool DispatchIncomingCommands();

		// Token: 0x06000128 RID: 296 RVA: 0x00009A8C File Offset: 0x00007C8C
		internal virtual bool DeserializeMessageAndCallback(StreamBuffer stream)
		{
			bool flag = stream.Length < 2;
			bool result;
			if (flag)
			{
				bool flag2 = this.debugOut >= DebugLevel.ERROR;
				if (flag2)
				{
					this.Listener.DebugReturn(DebugLevel.ERROR, "Incoming UDP data too short! " + stream.Length.ToString());
				}
				result = false;
			}
			else
			{
				byte b = stream.ReadByte();
				bool flag3 = b != 243 && b != 253;
				if (flag3)
				{
					bool flag4 = this.debugOut >= DebugLevel.ERROR;
					if (flag4)
					{
						this.Listener.DebugReturn(DebugLevel.ALL, "No regular operation UDP message: " + b.ToString());
					}
					result = false;
				}
				else
				{
					byte b2 = stream.ReadByte();
					byte b3 = b2 & 127;
					bool flag5 = (b2 & 128) > 0;
					bool flag6 = b3 != 1;
					if (flag6)
					{
						try
						{
							bool flag7 = flag5;
							if (flag7)
							{
								byte[] buf = this.CryptoProvider.Decrypt(stream.GetBuffer(), 2, stream.Length - 2);
								stream = new StreamBuffer(buf);
							}
							else
							{
								stream.Seek(2L, SeekOrigin.Begin);
							}
						}
						catch (Exception ex)
						{
							bool flag8 = this.debugOut >= DebugLevel.ERROR;
							if (flag8)
							{
								this.Listener.DebugReturn(DebugLevel.ERROR, "msgType: " + b3.ToString() + " exception: " + ex.ToString());
							}
							SupportClass.WriteStackTrace(ex);
							return false;
						}
					}
					int num = 0;
					IProtocol.DeserializationFlags flags = (this.photonPeer.UseByteArraySlicePoolForEvents ? IProtocol.DeserializationFlags.AllowPooledByteArray : IProtocol.DeserializationFlags.None) | (this.photonPeer.WrapIncomingStructs ? IProtocol.DeserializationFlags.WrapIncomingStructs : IProtocol.DeserializationFlags.None);
					switch (b3)
					{
					case 1:
						this.InitCallback();
						goto IL_5AE;
					case 3:
					{
						OperationResponse operationResponse = null;
						try
						{
							operationResponse = this.SerializationProtocol.DeserializeOperationResponse(stream, flags);
						}
						catch (Exception ex2)
						{
							DebugLevel level = DebugLevel.ERROR;
							string str = "Deserialization failed for Operation Response. ";
							Exception ex3 = ex2;
							this.EnqueueDebugReturn(level, str + ((ex3 != null) ? ex3.ToString() : null));
							return false;
						}
						bool trafficStatsEnabled = this.TrafficStatsEnabled;
						if (trafficStatsEnabled)
						{
							this.TrafficStatsGameLevel.CountResult(this.ByteCountCurrentDispatch);
							num = this.timeInt;
						}
						this.Listener.OnOperationResponse(operationResponse);
						bool trafficStatsEnabled2 = this.TrafficStatsEnabled;
						if (trafficStatsEnabled2)
						{
							this.TrafficStatsGameLevel.TimeForResponseCallback(operationResponse.OperationCode, this.timeInt - num);
						}
						goto IL_5AE;
					}
					case 4:
					{
						EventData eventData = null;
						try
						{
							eventData = this.SerializationProtocol.DeserializeEventData(stream, this.reusableEventData, flags);
						}
						catch (Exception ex4)
						{
							DebugLevel level2 = DebugLevel.ERROR;
							string str2 = "Deserialization failed for Event. ";
							Exception ex5 = ex4;
							this.EnqueueDebugReturn(level2, str2 + ((ex5 != null) ? ex5.ToString() : null));
							return false;
						}
						bool trafficStatsEnabled3 = this.TrafficStatsEnabled;
						if (trafficStatsEnabled3)
						{
							this.TrafficStatsGameLevel.CountEvent(this.ByteCountCurrentDispatch);
							num = this.timeInt;
						}
						this.Listener.OnEvent(eventData);
						bool trafficStatsEnabled4 = this.TrafficStatsEnabled;
						if (trafficStatsEnabled4)
						{
							this.TrafficStatsGameLevel.TimeForEventCallback(eventData.Code, this.timeInt - num);
						}
						bool reuseEventInstance = this.photonPeer.ReuseEventInstance;
						if (reuseEventInstance)
						{
							this.reusableEventData = eventData;
						}
						goto IL_5AE;
					}
					case 5:
						try
						{
							DisconnectMessage dm = this.SerializationProtocol.DeserializeDisconnectMessage(stream);
							this.photonPeer.OnDisconnectMessageCall(dm);
						}
						catch (Exception ex6)
						{
							DebugLevel level3 = DebugLevel.ERROR;
							string str3 = "Deserialization failed for disconnect message. ";
							Exception ex7 = ex6;
							this.EnqueueDebugReturn(level3, str3 + ((ex7 != null) ? ex7.ToString() : null));
							return false;
						}
						goto IL_5AE;
					case 7:
					{
						OperationResponse operationResponse;
						try
						{
							operationResponse = this.SerializationProtocol.DeserializeOperationResponse(stream, IProtocol.DeserializationFlags.None);
						}
						catch (Exception ex8)
						{
							DebugLevel level4 = DebugLevel.ERROR;
							string str4 = "Deserialization failed for internal Operation Response. ";
							Exception ex9 = ex8;
							this.EnqueueDebugReturn(level4, str4 + ((ex9 != null) ? ex9.ToString() : null));
							return false;
						}
						bool trafficStatsEnabled5 = this.TrafficStatsEnabled;
						if (trafficStatsEnabled5)
						{
							this.TrafficStatsGameLevel.CountResult(this.ByteCountCurrentDispatch);
							num = this.timeInt;
						}
						bool flag9 = operationResponse.OperationCode == PhotonCodes.InitEncryption;
						if (flag9)
						{
							this.DeriveSharedKey(operationResponse);
						}
						else
						{
							bool flag10 = operationResponse.OperationCode == PhotonCodes.Ping;
							if (flag10)
							{
								bool flag11 = this.peerConnectionState == ConnectionStateValue.Connecting && (this.usedTransportProtocol == ConnectionProtocol.WebSocket || this.usedTransportProtocol == ConnectionProtocol.WebSocketSecure);
								if (flag11)
								{
									this.photonPeer.PingUsedAsInit = true;
									this.InitCallback();
								}
								TPeer tpeer = this as TPeer;
								bool flag12 = tpeer != null;
								if (flag12)
								{
									tpeer.ReadPingResult(operationResponse);
								}
							}
							else
							{
								this.EnqueueDebugReturn(DebugLevel.ERROR, "Received unknown internal operation. " + operationResponse.ToStringFull());
							}
						}
						bool trafficStatsEnabled6 = this.TrafficStatsEnabled;
						if (trafficStatsEnabled6)
						{
							this.TrafficStatsGameLevel.TimeForResponseCallback(operationResponse.OperationCode, this.timeInt - num);
						}
						goto IL_5AE;
					}
					case 8:
					{
						object obj = this.SerializationProtocol.DeserializeMessage(stream);
						bool trafficStatsEnabled7 = this.TrafficStatsEnabled;
						if (trafficStatsEnabled7)
						{
							this.TrafficStatsGameLevel.CountEvent(this.ByteCountCurrentDispatch);
							num = this.timeInt;
						}
						bool trafficStatsEnabled8 = this.TrafficStatsEnabled;
						if (trafficStatsEnabled8)
						{
							this.TrafficStatsGameLevel.TimeForMessageCallback(this.timeInt - num);
						}
						goto IL_5AE;
					}
					case 9:
					{
						bool trafficStatsEnabled9 = this.TrafficStatsEnabled;
						if (trafficStatsEnabled9)
						{
							this.TrafficStatsGameLevel.CountEvent(this.ByteCountCurrentDispatch);
							num = this.timeInt;
						}
						byte[] array = stream.ToArrayFromPos();
						bool trafficStatsEnabled10 = this.TrafficStatsEnabled;
						if (trafficStatsEnabled10)
						{
							this.TrafficStatsGameLevel.TimeForRawMessageCallback(this.timeInt - num);
						}
						goto IL_5AE;
					}
					}
					this.EnqueueDebugReturn(DebugLevel.ERROR, "unexpected msgType " + b3.ToString());
					IL_5AE:
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000A090 File Offset: 0x00008290
		internal void UpdateRoundTripTimeAndVariance(int lastRoundtripTime)
		{
			bool flag = lastRoundtripTime < 0;
			if (!flag)
			{
				this.roundTripTimeVariance -= this.roundTripTimeVariance / 4;
				bool flag2 = lastRoundtripTime >= this.roundTripTime;
				if (flag2)
				{
					this.roundTripTime += (lastRoundtripTime - this.roundTripTime) / 8;
					this.roundTripTimeVariance += (lastRoundtripTime - this.roundTripTime) / 4;
				}
				else
				{
					this.roundTripTime += (lastRoundtripTime - this.roundTripTime) / 8;
					this.roundTripTimeVariance -= (lastRoundtripTime - this.roundTripTime) / 4;
				}
				bool flag3 = this.roundTripTime < this.lowestRoundTripTime;
				if (flag3)
				{
					this.lowestRoundTripTime = this.roundTripTime;
				}
				bool flag4 = this.roundTripTimeVariance > this.highestRoundTripTimeVariance;
				if (flag4)
				{
					this.highestRoundTripTimeVariance = this.roundTripTimeVariance;
				}
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000A174 File Offset: 0x00008374
		internal bool ExchangeKeysForEncryption(object lockObject)
		{
			this.isEncryptionAvailable = false;
			bool flag = this.CryptoProvider != null;
			if (flag)
			{
				this.CryptoProvider.Dispose();
				this.CryptoProvider = null;
			}
			bool flag2 = this.photonPeer.PayloadEncryptorType != null;
			if (flag2)
			{
				try
				{
					this.CryptoProvider = (ICryptoProvider)Activator.CreateInstance(this.photonPeer.PayloadEncryptorType);
					bool flag3 = this.CryptoProvider == null;
					if (flag3)
					{
						IPhotonPeerListener listener = this.Listener;
						DebugLevel level = DebugLevel.WARNING;
						string str = "Payload encryptor creation by type failed, Activator.CreateInstance() returned null for: ";
						Type payloadEncryptorType = this.photonPeer.PayloadEncryptorType;
						listener.DebugReturn(level, str + ((payloadEncryptorType != null) ? payloadEncryptorType.ToString() : null));
					}
				}
				catch (Exception ex)
				{
					IPhotonPeerListener listener2 = this.Listener;
					DebugLevel level2 = DebugLevel.WARNING;
					string str2 = "Payload encryptor creation by type failed: ";
					Exception ex2 = ex;
					listener2.DebugReturn(level2, str2 + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
			bool flag4 = this.CryptoProvider == null;
			if (flag4)
			{
				this.CryptoProvider = new DiffieHellmanCryptoProvider();
			}
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>(1);
			dictionary[PhotonCodes.ClientKey] = this.CryptoProvider.PublicKey;
			bool flag5 = lockObject != null;
			if (flag5)
			{
				lock (lockObject)
				{
					SendOptions sendOptions = new SendOptions
					{
						Channel = 0,
						Encrypt = false,
						DeliveryMode = DeliveryMode.Reliable
					};
					StreamBuffer opBytes = this.SerializeOperationToMessage(PhotonCodes.InitEncryption, dictionary, EgMessageType.InternalOperationRequest, sendOptions.Encrypt);
					return this.EnqueuePhotonMessage(opBytes, sendOptions);
				}
			}
			SendOptions sendOptions2 = new SendOptions
			{
				Channel = 0,
				Encrypt = false,
				DeliveryMode = DeliveryMode.Reliable
			};
			StreamBuffer opBytes2 = this.SerializeOperationToMessage(PhotonCodes.InitEncryption, dictionary, EgMessageType.InternalOperationRequest, sendOptions2.Encrypt);
			return this.EnqueuePhotonMessage(opBytes2, sendOptions2);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A360 File Offset: 0x00008560
		internal void DeriveSharedKey(OperationResponse operationResponse)
		{
			bool flag = operationResponse.ReturnCode != 0;
			if (flag)
			{
				this.EnqueueDebugReturn(DebugLevel.ERROR, "Establishing encryption keys failed. " + operationResponse.ToStringFull());
				this.EnqueueStatusCallback(StatusCode.EncryptionFailedToEstablish);
			}
			else
			{
				byte[] array = (byte[])operationResponse.Parameters[PhotonCodes.ServerKey];
				bool flag2 = array == null || array.Length == 0;
				if (flag2)
				{
					this.EnqueueDebugReturn(DebugLevel.ERROR, "Establishing encryption keys failed. Server's public key is null or empty. " + operationResponse.ToStringFull());
					this.EnqueueStatusCallback(StatusCode.EncryptionFailedToEstablish);
				}
				else
				{
					this.CryptoProvider.DeriveSharedKey(array);
					this.isEncryptionAvailable = true;
					this.EnqueueStatusCallback(StatusCode.EncryptionEstablished);
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A410 File Offset: 0x00008610
		internal virtual void InitEncryption(byte[] secret)
		{
			bool flag = this.photonPeer.PayloadEncryptorType != null;
			if (flag)
			{
				try
				{
					this.CryptoProvider = (ICryptoProvider)Activator.CreateInstance(this.photonPeer.PayloadEncryptorType, new object[]
					{
						secret
					});
					bool flag2 = this.CryptoProvider == null;
					if (flag2)
					{
						IPhotonPeerListener listener = this.Listener;
						DebugLevel level = DebugLevel.WARNING;
						string str = "Payload encryptor creation by type failed, Activator.CreateInstance() returned null for: ";
						Type payloadEncryptorType = this.photonPeer.PayloadEncryptorType;
						listener.DebugReturn(level, str + ((payloadEncryptorType != null) ? payloadEncryptorType.ToString() : null));
					}
					else
					{
						this.isEncryptionAvailable = true;
					}
				}
				catch (Exception ex)
				{
					IPhotonPeerListener listener2 = this.Listener;
					DebugLevel level2 = DebugLevel.WARNING;
					string str2 = "Payload encryptor creation by type failed: ";
					Exception ex2 = ex;
					listener2.DebugReturn(level2, str2 + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
			bool flag3 = this.CryptoProvider == null;
			if (flag3)
			{
				this.CryptoProvider = new DiffieHellmanCryptoProvider(secret);
				this.isEncryptionAvailable = true;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A504 File Offset: 0x00008704
		internal void EnqueueActionForDispatch(PeerBase.MyAction action)
		{
			Queue<PeerBase.MyAction> actionQueue = this.ActionQueue;
			lock (actionQueue)
			{
				this.ActionQueue.Enqueue(action);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A550 File Offset: 0x00008750
		internal void EnqueueDebugReturn(DebugLevel level, string debugReturn)
		{
			Queue<PeerBase.MyAction> actionQueue = this.ActionQueue;
			lock (actionQueue)
			{
				this.ActionQueue.Enqueue(delegate
				{
					this.Listener.DebugReturn(level, debugReturn);
				});
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000A5C4 File Offset: 0x000087C4
		internal void EnqueueStatusCallback(StatusCode statusValue)
		{
			Queue<PeerBase.MyAction> actionQueue = this.ActionQueue;
			lock (actionQueue)
			{
				this.ActionQueue.Enqueue(delegate
				{
					this.Listener.OnStatusChanged(statusValue);
				});
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000A630 File Offset: 0x00008830
		public NetworkSimulationSet NetworkSimulationSettings
		{
			get
			{
				return this.networkSimulationSettings;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A648 File Offset: 0x00008848
		internal void SendNetworkSimulated(byte[] dataToSend)
		{
			bool flag = !this.NetworkSimulationSettings.IsSimulationEnabled;
			if (flag)
			{
				throw new NotImplementedException("SendNetworkSimulated was called, despite NetworkSimulationSettings.IsSimulationEnabled == false.");
			}
			bool flag2 = this.usedTransportProtocol == ConnectionProtocol.Udp && this.NetworkSimulationSettings.OutgoingLossPercentage > 0 && this.lagRandomizer.Next(101) < this.NetworkSimulationSettings.OutgoingLossPercentage;
			if (flag2)
			{
				NetworkSimulationSet networkSimulationSet = this.networkSimulationSettings;
				int lostPackagesOut = networkSimulationSet.LostPackagesOut;
				networkSimulationSet.LostPackagesOut = lostPackagesOut + 1;
			}
			else
			{
				int num = (this.networkSimulationSettings.OutgoingJitter <= 0) ? 0 : (this.lagRandomizer.Next(this.networkSimulationSettings.OutgoingJitter * 2) - this.networkSimulationSettings.OutgoingJitter);
				int num2 = this.networkSimulationSettings.OutgoingLag + num;
				int num3 = this.timeInt + num2;
				SimulationItem value = new SimulationItem
				{
					DelayedData = dataToSend,
					TimeToExecute = num3,
					Delay = num2
				};
				LinkedList<SimulationItem> netSimListOutgoing = this.NetSimListOutgoing;
				lock (netSimListOutgoing)
				{
					bool flag4 = this.NetSimListOutgoing.Count == 0 || this.usedTransportProtocol == ConnectionProtocol.Tcp;
					if (flag4)
					{
						this.NetSimListOutgoing.AddLast(value);
					}
					else
					{
						LinkedListNode<SimulationItem> linkedListNode = this.NetSimListOutgoing.First;
						while (linkedListNode != null && linkedListNode.Value.TimeToExecute < num3)
						{
							linkedListNode = linkedListNode.Next;
						}
						bool flag5 = linkedListNode == null;
						if (flag5)
						{
							this.NetSimListOutgoing.AddLast(value);
						}
						else
						{
							this.NetSimListOutgoing.AddBefore(linkedListNode, value);
						}
					}
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A804 File Offset: 0x00008A04
		internal void ReceiveNetworkSimulated(byte[] dataReceived)
		{
			bool flag = !this.networkSimulationSettings.IsSimulationEnabled;
			if (flag)
			{
				throw new NotImplementedException("ReceiveNetworkSimulated was called, despite NetworkSimulationSettings.IsSimulationEnabled == false.");
			}
			bool flag2 = this.usedTransportProtocol == ConnectionProtocol.Udp && this.networkSimulationSettings.IncomingLossPercentage > 0 && this.lagRandomizer.Next(101) < this.networkSimulationSettings.IncomingLossPercentage;
			if (flag2)
			{
				NetworkSimulationSet networkSimulationSet = this.networkSimulationSettings;
				int lostPackagesIn = networkSimulationSet.LostPackagesIn;
				networkSimulationSet.LostPackagesIn = lostPackagesIn + 1;
			}
			else
			{
				int num = (this.networkSimulationSettings.IncomingJitter <= 0) ? 0 : (this.lagRandomizer.Next(this.networkSimulationSettings.IncomingJitter * 2) - this.networkSimulationSettings.IncomingJitter);
				int num2 = this.networkSimulationSettings.IncomingLag + num;
				int num3 = this.timeInt + num2;
				SimulationItem value = new SimulationItem
				{
					DelayedData = dataReceived,
					TimeToExecute = num3,
					Delay = num2
				};
				LinkedList<SimulationItem> netSimListIncoming = this.NetSimListIncoming;
				lock (netSimListIncoming)
				{
					bool flag4 = this.NetSimListIncoming.Count == 0 || this.usedTransportProtocol == ConnectionProtocol.Tcp;
					if (flag4)
					{
						this.NetSimListIncoming.AddLast(value);
					}
					else
					{
						LinkedListNode<SimulationItem> linkedListNode = this.NetSimListIncoming.First;
						while (linkedListNode != null && linkedListNode.Value.TimeToExecute < num3)
						{
							linkedListNode = linkedListNode.Next;
						}
						bool flag5 = linkedListNode == null;
						if (flag5)
						{
							this.NetSimListIncoming.AddLast(value);
						}
						else
						{
							this.NetSimListIncoming.AddBefore(linkedListNode, value);
						}
					}
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		protected internal void NetworkSimRun()
		{
			for (;;)
			{
				bool flag = false;
				ManualResetEvent netSimManualResetEvent = this.networkSimulationSettings.NetSimManualResetEvent;
				lock (netSimManualResetEvent)
				{
					flag = this.networkSimulationSettings.IsSimulationEnabled;
				}
				bool flag3 = !flag;
				if (flag3)
				{
					this.networkSimulationSettings.NetSimManualResetEvent.WaitOne();
				}
				else
				{
					LinkedList<SimulationItem> netSimListIncoming = this.NetSimListIncoming;
					lock (netSimListIncoming)
					{
						while (this.NetSimListIncoming.First != null)
						{
							SimulationItem value = this.NetSimListIncoming.First.Value;
							bool flag5 = value.stopw.ElapsedMilliseconds < (long)value.Delay;
							if (flag5)
							{
								break;
							}
							this.ReceiveIncomingCommands(value.DelayedData, value.DelayedData.Length);
							this.NetSimListIncoming.RemoveFirst();
						}
					}
					LinkedList<SimulationItem> netSimListOutgoing = this.NetSimListOutgoing;
					lock (netSimListOutgoing)
					{
						while (this.NetSimListOutgoing.First != null)
						{
							SimulationItem value2 = this.NetSimListOutgoing.First.Value;
							bool flag7 = value2.stopw.ElapsedMilliseconds < (long)value2.Delay;
							if (flag7)
							{
								break;
							}
							bool flag8 = this.PhotonSocket != null && this.PhotonSocket.Connected;
							if (flag8)
							{
								this.PhotonSocket.Send(value2.DelayedData, value2.DelayedData.Length);
							}
							this.NetSimListOutgoing.RemoveFirst();
						}
					}
					Thread.Sleep(0);
				}
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		internal bool TrafficStatsEnabled
		{
			get
			{
				return this.photonPeer.TrafficStatsEnabled;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		internal TrafficStats TrafficStatsIncoming
		{
			get
			{
				return this.photonPeer.TrafficStatsIncoming;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		internal TrafficStats TrafficStatsOutgoing
		{
			get
			{
				return this.photonPeer.TrafficStatsOutgoing;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000AC10 File Offset: 0x00008E10
		internal TrafficStatsGameLevel TrafficStatsGameLevel
		{
			get
			{
				return this.photonPeer.TrafficStatsGameLevel;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000AC2D File Offset: 0x00008E2D
		// Note: this type is marked as 'beforefieldinit'.
		static PeerBase()
		{
		}

		// Token: 0x040000DE RID: 222
		internal PhotonPeer photonPeer;

		// Token: 0x040000DF RID: 223
		public IProtocol SerializationProtocol;

		// Token: 0x040000E0 RID: 224
		internal ConnectionProtocol usedTransportProtocol;

		// Token: 0x040000E1 RID: 225
		internal IPhotonSocket PhotonSocket;

		// Token: 0x040000E2 RID: 226
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <ServerAddress>k__BackingField;

		// Token: 0x040000E3 RID: 227
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <ProxyServerAddress>k__BackingField;

		// Token: 0x040000E4 RID: 228
		internal ConnectionStateValue peerConnectionState;

		// Token: 0x040000E5 RID: 229
		internal int ByteCountLastOperation;

		// Token: 0x040000E6 RID: 230
		internal int ByteCountCurrentDispatch;

		// Token: 0x040000E7 RID: 231
		internal NCommand CommandInCurrentDispatch;

		// Token: 0x040000E8 RID: 232
		internal int packetLossByCrc;

		// Token: 0x040000E9 RID: 233
		internal int packetLossByChallenge;

		// Token: 0x040000EA RID: 234
		internal readonly Queue<PeerBase.MyAction> ActionQueue = new Queue<PeerBase.MyAction>();

		// Token: 0x040000EB RID: 235
		internal short peerID = -1;

		// Token: 0x040000EC RID: 236
		internal int serverTimeOffset;

		// Token: 0x040000ED RID: 237
		internal bool serverTimeOffsetIsAvailable;

		// Token: 0x040000EE RID: 238
		internal int roundTripTime;

		// Token: 0x040000EF RID: 239
		internal int roundTripTimeVariance;

		// Token: 0x040000F0 RID: 240
		internal int lastRoundTripTime;

		// Token: 0x040000F1 RID: 241
		internal int lowestRoundTripTime;

		// Token: 0x040000F2 RID: 242
		internal int highestRoundTripTimeVariance;

		// Token: 0x040000F3 RID: 243
		internal int timestampOfLastReceive;

		// Token: 0x040000F4 RID: 244
		internal static short peerCount;

		// Token: 0x040000F5 RID: 245
		internal long bytesOut;

		// Token: 0x040000F6 RID: 246
		internal long bytesIn;

		// Token: 0x040000F7 RID: 247
		internal object PhotonToken;

		// Token: 0x040000F8 RID: 248
		internal object CustomInitData;

		// Token: 0x040000F9 RID: 249
		public string AppId;

		// Token: 0x040000FA RID: 250
		internal EventData reusableEventData;

		// Token: 0x040000FB RID: 251
		private Stopwatch watch = Stopwatch.StartNew();

		// Token: 0x040000FC RID: 252
		internal int timeoutInt;

		// Token: 0x040000FD RID: 253
		internal int timeLastAckReceive;

		// Token: 0x040000FE RID: 254
		internal int longestSentCall;

		// Token: 0x040000FF RID: 255
		internal int timeLastSendAck;

		// Token: 0x04000100 RID: 256
		internal int timeLastSendOutgoing;

		// Token: 0x04000101 RID: 257
		internal bool ApplicationIsInitialized;

		// Token: 0x04000102 RID: 258
		internal bool isEncryptionAvailable;

		// Token: 0x04000103 RID: 259
		internal int outgoingCommandsInStream = 0;

		// Token: 0x04000104 RID: 260
		protected internal static Queue<StreamBuffer> MessageBufferPool = new Queue<StreamBuffer>(32);

		// Token: 0x04000105 RID: 261
		internal byte[] messageHeader;

		// Token: 0x04000106 RID: 262
		internal ICryptoProvider CryptoProvider;

		// Token: 0x04000107 RID: 263
		private readonly Random lagRandomizer = new Random();

		// Token: 0x04000108 RID: 264
		internal readonly LinkedList<SimulationItem> NetSimListOutgoing = new LinkedList<SimulationItem>();

		// Token: 0x04000109 RID: 265
		internal readonly LinkedList<SimulationItem> NetSimListIncoming = new LinkedList<SimulationItem>();

		// Token: 0x0400010A RID: 266
		private readonly NetworkSimulationSet networkSimulationSettings = new NetworkSimulationSet();

		// Token: 0x0400010B RID: 267
		internal int TrafficPackageHeaderSize;

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x06000434 RID: 1076
		internal delegate void MyAction();

		// Token: 0x02000055 RID: 85
		private static class GpBinaryV3Parameters
		{
			// Token: 0x04000229 RID: 553
			public const byte CustomObject = 0;

			// Token: 0x0400022A RID: 554
			public const byte ExtraPlatformParams = 1;
		}

		// Token: 0x02000056 RID: 86
		[CompilerGenerated]
		private sealed class <>c__DisplayClass108_0
		{
			// Token: 0x06000437 RID: 1079 RVA: 0x00020EB5 File Offset: 0x0001F0B5
			public <>c__DisplayClass108_0()
			{
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x00020EBE File Offset: 0x0001F0BE
			internal void <EnqueueDebugReturn>b__0()
			{
				this.<>4__this.Listener.DebugReturn(this.level, this.debugReturn);
			}

			// Token: 0x0400022B RID: 555
			public PeerBase <>4__this;

			// Token: 0x0400022C RID: 556
			public DebugLevel level;

			// Token: 0x0400022D RID: 557
			public string debugReturn;
		}

		// Token: 0x02000057 RID: 87
		[CompilerGenerated]
		private sealed class <>c__DisplayClass109_0
		{
			// Token: 0x06000439 RID: 1081 RVA: 0x00020EDE File Offset: 0x0001F0DE
			public <>c__DisplayClass109_0()
			{
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x00020EE7 File Offset: 0x0001F0E7
			internal void <EnqueueStatusCallback>b__0()
			{
				this.<>4__this.Listener.OnStatusChanged(this.statusValue);
			}

			// Token: 0x0400022E RID: 558
			public PeerBase <>4__this;

			// Token: 0x0400022F RID: 559
			public StatusCode statusValue;
		}
	}
}
