using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using ExitGames.Client.Photon.Encryption;
using Photon.SocketServer.Security;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000022 RID: 34
	public class PhotonPeer
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000B5FE File Offset: 0x000097FE
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000B606 File Offset: 0x00009806
		[Obsolete("See remarks.")]
		public int CommandBufferSize
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandBufferSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CommandBufferSize>k__BackingField = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000B60F File Offset: 0x0000980F
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000B617 File Offset: 0x00009817
		[Obsolete("See remarks.")]
		public int LimitOfUnreliableCommands
		{
			[CompilerGenerated]
			get
			{
				return this.<LimitOfUnreliableCommands>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LimitOfUnreliableCommands>k__BackingField = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000B620 File Offset: 0x00009820
		[Obsolete("Returns SupportClass.GetTickCount(). Should be replaced by a StopWatch or the per peer PhotonPeer.ClientTime.")]
		public int LocalTimeInMilliSeconds
		{
			get
			{
				return SupportClass.GetTickCount();
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000B638 File Offset: 0x00009838
		[Obsolete("Use the ITrafficRecorder to capture all traffic instead.")]
		public string CommandLogToString()
		{
			return string.Empty;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000B650 File Offset: 0x00009850
		protected internal byte ClientSdkIdShifted
		{
			get
			{
				return (byte)((int)this.ClientSdkId << 1 | 0);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000B670 File Offset: 0x00009870
		[Obsolete("The static string Version should be preferred.")]
		public string ClientVersion
		{
			get
			{
				bool flag = string.IsNullOrEmpty(PhotonPeer.clientVersion);
				if (flag)
				{
					PhotonPeer.clientVersion = string.Format("{0}.{1}.{2}.{3}", new object[]
					{
						ExitGames.Client.Photon.Version.clientVersion[0],
						ExitGames.Client.Photon.Version.clientVersion[1],
						ExitGames.Client.Photon.Version.clientVersion[2],
						ExitGames.Client.Photon.Version.clientVersion[3]
					});
				}
				return PhotonPeer.clientVersion;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000B6E8 File Offset: 0x000098E8
		public static string Version
		{
			get
			{
				bool flag = string.IsNullOrEmpty(PhotonPeer.clientVersion);
				if (flag)
				{
					PhotonPeer.clientVersion = string.Format("{0}.{1}.{2}.{3}", new object[]
					{
						ExitGames.Client.Photon.Version.clientVersion[0],
						ExitGames.Client.Photon.Version.clientVersion[1],
						ExitGames.Client.Photon.Version.clientVersion[2],
						ExitGames.Client.Photon.Version.clientVersion[3]
					});
				}
				return PhotonPeer.clientVersion;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000B760 File Offset: 0x00009960
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000B768 File Offset: 0x00009968
		public SerializationProtocol SerializationProtocolType
		{
			[CompilerGenerated]
			get
			{
				return this.<SerializationProtocolType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SerializationProtocolType>k__BackingField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000B771 File Offset: 0x00009971
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000B779 File Offset: 0x00009979
		public Type SocketImplementation
		{
			[CompilerGenerated]
			get
			{
				return this.<SocketImplementation>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<SocketImplementation>k__BackingField = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000B784 File Offset: 0x00009984
		public int SocketErrorCode
		{
			get
			{
				return (this.peerBase != null && this.peerBase.PhotonSocket != null) ? this.peerBase.PhotonSocket.SocketErrorCode : 0;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000B7BE File Offset: 0x000099BE
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000B7C6 File Offset: 0x000099C6
		public IPhotonPeerListener Listener
		{
			[CompilerGenerated]
			get
			{
				return this.<Listener>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Listener>k__BackingField = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600014F RID: 335 RVA: 0x0000B7D0 File Offset: 0x000099D0
		// (remove) Token: 0x06000150 RID: 336 RVA: 0x0000B808 File Offset: 0x00009A08
		public event Action<DisconnectMessage> OnDisconnectMessage
		{
			[CompilerGenerated]
			add
			{
				Action<DisconnectMessage> action = this.OnDisconnectMessage;
				Action<DisconnectMessage> action2;
				do
				{
					action2 = action;
					Action<DisconnectMessage> value2 = (Action<DisconnectMessage>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<DisconnectMessage>>(ref this.OnDisconnectMessage, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<DisconnectMessage> action = this.OnDisconnectMessage;
				Action<DisconnectMessage> action2;
				do
				{
					action2 = action;
					Action<DisconnectMessage> value2 = (Action<DisconnectMessage>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<DisconnectMessage>>(ref this.OnDisconnectMessage, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000B840 File Offset: 0x00009A40
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000B858 File Offset: 0x00009A58
		public bool ReuseEventInstance
		{
			get
			{
				return this.reuseEventInstance;
			}
			set
			{
				object dispatchLockObject = this.DispatchLockObject;
				lock (dispatchLockObject)
				{
					this.reuseEventInstance = value;
					bool flag2 = !value;
					if (flag2)
					{
						this.peerBase.reusableEventData = null;
					}
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000B8CC File Offset: 0x00009ACC
		public bool UseByteArraySlicePoolForEvents
		{
			get
			{
				return this.useByteArraySlicePoolForEvents;
			}
			set
			{
				this.useByteArraySlicePoolForEvents = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000B8F0 File Offset: 0x00009AF0
		public bool WrapIncomingStructs
		{
			get
			{
				return this.wrapIncomingStructs;
			}
			set
			{
				this.wrapIncomingStructs = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000B8FC File Offset: 0x00009AFC
		public ByteArraySlicePool ByteArraySlicePool
		{
			get
			{
				return this.peerBase.SerializationProtocol.ByteArraySlicePool;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000B920 File Offset: 0x00009B20
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000B938 File Offset: 0x00009B38
		[Obsolete("Use SendWindowSize instead.")]
		public int SequenceDeltaLimitSends
		{
			get
			{
				return this.SendWindowSize;
			}
			set
			{
				this.SendWindowSize = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000B944 File Offset: 0x00009B44
		public long BytesIn
		{
			get
			{
				return this.peerBase.BytesIn;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000B964 File Offset: 0x00009B64
		public long BytesOut
		{
			get
			{
				return this.peerBase.BytesOut;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000B984 File Offset: 0x00009B84
		public int ByteCountCurrentDispatch
		{
			get
			{
				return this.peerBase.ByteCountCurrentDispatch;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public string CommandInfoCurrentDispatch
		{
			get
			{
				return (this.peerBase.CommandInCurrentDispatch != null) ? this.peerBase.CommandInCurrentDispatch.ToString() : string.Empty;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000B9DC File Offset: 0x00009BDC
		public int ByteCountLastOperation
		{
			get
			{
				return this.peerBase.ByteCountLastOperation;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000B9F9 File Offset: 0x00009BF9
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000BA01 File Offset: 0x00009C01
		public bool EnableServerTracing
		{
			[CompilerGenerated]
			get
			{
				return this.<EnableServerTracing>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnableServerTracing>k__BackingField = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000BA0C File Offset: 0x00009C0C
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000BA24 File Offset: 0x00009C24
		public byte QuickResendAttempts
		{
			get
			{
				return this.quickResendAttempts;
			}
			set
			{
				this.quickResendAttempts = value;
				bool flag = this.quickResendAttempts > 4;
				if (flag)
				{
					this.quickResendAttempts = 4;
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000BA50 File Offset: 0x00009C50
		public PeerStateValue PeerState
		{
			get
			{
				bool flag = this.peerBase.peerConnectionState == ConnectionStateValue.Connected && !this.peerBase.ApplicationIsInitialized;
				PeerStateValue result;
				if (flag)
				{
					result = PeerStateValue.InitializingApplication;
				}
				else
				{
					result = (PeerStateValue)this.peerBase.peerConnectionState;
				}
				return result;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000BA98 File Offset: 0x00009C98
		public string PeerID
		{
			get
			{
				return this.peerBase.PeerID;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public int QueuedIncomingCommands
		{
			get
			{
				return this.peerBase.QueuedIncomingCommandsCount;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public int QueuedOutgoingCommands
		{
			get
			{
				return this.peerBase.QueuedOutgoingCommandsCount;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		public static void MessageBufferPoolTrim(int countOfBuffers)
		{
			Queue<StreamBuffer> messageBufferPool = PeerBase.MessageBufferPool;
			lock (messageBufferPool)
			{
				bool flag2 = countOfBuffers <= 0;
				if (flag2)
				{
					PeerBase.MessageBufferPool.Clear();
				}
				else
				{
					bool flag3 = countOfBuffers >= PeerBase.MessageBufferPool.Count;
					if (!flag3)
					{
						while (PeerBase.MessageBufferPool.Count > countOfBuffers)
						{
							PeerBase.MessageBufferPool.Dequeue();
						}
						PeerBase.MessageBufferPool.TrimExcess();
					}
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000BB94 File Offset: 0x00009D94
		public static int MessageBufferPoolSize()
		{
			return PeerBase.MessageBufferPool.Count;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		public bool CrcEnabled
		{
			get
			{
				return this.crcEnabled;
			}
			set
			{
				bool flag = this.crcEnabled == value;
				if (!flag)
				{
					bool flag2 = this.peerBase.peerConnectionState > ConnectionStateValue.Disconnected;
					if (flag2)
					{
						throw new Exception("CrcEnabled can only be set while disconnected.");
					}
					this.crcEnabled = value;
				}
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000BC0C File Offset: 0x00009E0C
		public int PacketLossByCrc
		{
			get
			{
				return this.peerBase.packetLossByCrc;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000BC2C File Offset: 0x00009E2C
		public int PacketLossByChallenge
		{
			get
			{
				return this.peerBase.packetLossByChallenge;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000BC4C File Offset: 0x00009E4C
		public int SentReliableCommandsCount
		{
			get
			{
				return this.peerBase.SentReliableCommandsCount;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public int ResentReliableCommands
		{
			get
			{
				return (this.UsedProtocol != ConnectionProtocol.Udp) ? 0 : ((EnetPeer)this.peerBase).reliableCommandsRepeated;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000BC9C File Offset: 0x00009E9C
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		public int DisconnectTimeout
		{
			get
			{
				return this.disconnectTimeout;
			}
			set
			{
				bool flag = value < 0;
				if (flag)
				{
					this.disconnectTimeout = 10000;
				}
				this.disconnectTimeout = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		public int ServerTimeInMilliSeconds
		{
			get
			{
				return this.peerBase.serverTimeOffsetIsAvailable ? (this.peerBase.serverTimeOffset + this.ConnectionTime) : 0;
			}
		}

		// Token: 0x1700006B RID: 107
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000BD14 File Offset: 0x00009F14
		[Obsolete("The PhotonPeer will no longer use this delegate. It uses a Stopwatch in all cases. You can access PhotonPeer.ConnectionTime.")]
		public SupportClass.IntegerMillisecondsDelegate LocalMsTimestampDelegate
		{
			set
			{
				bool flag = this.PeerState > PeerStateValue.Disconnected;
				if (flag)
				{
					throw new Exception("LocalMsTimestampDelegate only settable while disconnected. State: " + this.PeerState.ToString());
				}
				SupportClass.IntegerMilliseconds = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000BD5C File Offset: 0x00009F5C
		public int ConnectionTime
		{
			get
			{
				return this.peerBase.timeInt;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000BD7C File Offset: 0x00009F7C
		public int LastSendAckTime
		{
			get
			{
				return this.peerBase.timeLastSendAck;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public int LastSendOutgoingTime
		{
			get
			{
				return this.peerBase.timeLastSendOutgoing;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000BDBC File Offset: 0x00009FBC
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		public int LongestSentCall
		{
			get
			{
				return this.peerBase.longestSentCall;
			}
			set
			{
				this.peerBase.longestSentCall = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000BDE8 File Offset: 0x00009FE8
		public int RoundTripTime
		{
			get
			{
				return this.peerBase.roundTripTime;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000BE08 File Offset: 0x0000A008
		public int RoundTripTimeVariance
		{
			get
			{
				return this.peerBase.roundTripTimeVariance;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000BE28 File Offset: 0x0000A028
		public int LastRoundTripTime
		{
			get
			{
				return this.peerBase.lastRoundTripTime;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000BE48 File Offset: 0x0000A048
		public int TimestampOfLastSocketReceive
		{
			get
			{
				return this.peerBase.timestampOfLastReceive;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000BE68 File Offset: 0x0000A068
		public string ServerAddress
		{
			get
			{
				return this.peerBase.ServerAddress;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000BE88 File Offset: 0x0000A088
		public string ServerIpAddress
		{
			get
			{
				return IPhotonSocket.ServerIpAddress;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public ConnectionProtocol UsedProtocol
		{
			get
			{
				return this.peerBase.usedTransportProtocol;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000BEBD File Offset: 0x0000A0BD
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000BEC5 File Offset: 0x0000A0C5
		public ConnectionProtocol TransportProtocol
		{
			[CompilerGenerated]
			get
			{
				return this.<TransportProtocol>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TransportProtocol>k__BackingField = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		public virtual bool IsSimulationEnabled
		{
			get
			{
				return this.NetworkSimulationSettings.IsSimulationEnabled;
			}
			set
			{
				bool flag = value == this.NetworkSimulationSettings.IsSimulationEnabled;
				if (!flag)
				{
					object sendOutgoingLockObject = this.SendOutgoingLockObject;
					lock (sendOutgoingLockObject)
					{
						this.NetworkSimulationSettings.IsSimulationEnabled = value;
					}
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000BF54 File Offset: 0x0000A154
		public NetworkSimulationSet NetworkSimulationSettings
		{
			get
			{
				return this.peerBase.NetworkSimulationSettings;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000BF74 File Offset: 0x0000A174
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000BF8C File Offset: 0x0000A18C
		public int MaximumTransferUnit
		{
			get
			{
				return this.mtu;
			}
			set
			{
				bool flag = this.PeerState > PeerStateValue.Disconnected;
				if (flag)
				{
					throw new Exception("MaximumTransferUnit is only settable while disconnected. State: " + this.PeerState.ToString());
				}
				bool flag2 = value < 576;
				if (flag2)
				{
					value = 576;
				}
				this.mtu = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		public bool IsEncryptionAvailable
		{
			get
			{
				return this.peerBase.isEncryptionAvailable;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000C005 File Offset: 0x0000A205
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000C00D File Offset: 0x0000A20D
		[Obsolete("Internally not used anymore. Call SendAcksOnly() instead.")]
		public bool IsSendingOnlyAcks
		{
			[CompilerGenerated]
			get
			{
				return this.<IsSendingOnlyAcks>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsSendingOnlyAcks>k__BackingField = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000C016 File Offset: 0x0000A216
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000C01E File Offset: 0x0000A21E
		public TrafficStats TrafficStatsIncoming
		{
			[CompilerGenerated]
			get
			{
				return this.<TrafficStatsIncoming>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<TrafficStatsIncoming>k__BackingField = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000C027 File Offset: 0x0000A227
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000C02F File Offset: 0x0000A22F
		public TrafficStats TrafficStatsOutgoing
		{
			[CompilerGenerated]
			get
			{
				return this.<TrafficStatsOutgoing>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<TrafficStatsOutgoing>k__BackingField = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000C038 File Offset: 0x0000A238
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000C040 File Offset: 0x0000A240
		public TrafficStatsGameLevel TrafficStatsGameLevel
		{
			[CompilerGenerated]
			get
			{
				return this.<TrafficStatsGameLevel>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<TrafficStatsGameLevel>k__BackingField = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000C04C File Offset: 0x0000A24C
		public long TrafficStatsElapsedMs
		{
			get
			{
				return (this.trafficStatsStopwatch != null) ? this.trafficStatsStopwatch.ElapsedMilliseconds : 0L;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000C078 File Offset: 0x0000A278
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000C090 File Offset: 0x0000A290
		public bool TrafficStatsEnabled
		{
			get
			{
				return this.trafficStatsEnabled;
			}
			set
			{
				bool flag = this.trafficStatsEnabled == value;
				if (!flag)
				{
					this.trafficStatsEnabled = value;
					bool flag2 = this.trafficStatsEnabled;
					if (flag2)
					{
						bool flag3 = this.trafficStatsStopwatch == null;
						if (flag3)
						{
							this.InitializeTrafficStats();
						}
						this.trafficStatsStopwatch.Start();
					}
					else
					{
						bool flag4 = this.trafficStatsStopwatch != null;
						if (flag4)
						{
							this.trafficStatsStopwatch.Stop();
						}
					}
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000C100 File Offset: 0x0000A300
		public void TrafficStatsReset()
		{
			this.TrafficStatsEnabled = false;
			this.InitializeTrafficStats();
			this.TrafficStatsEnabled = true;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C11C File Offset: 0x0000A31C
		internal void InitializeTrafficStats()
		{
			bool flag = this.trafficStatsStopwatch == null;
			if (flag)
			{
				this.trafficStatsStopwatch = new Stopwatch();
			}
			else
			{
				this.trafficStatsStopwatch.Reset();
			}
			this.TrafficStatsIncoming = new TrafficStats(this.peerBase.TrafficPackageHeaderSize);
			this.TrafficStatsOutgoing = new TrafficStats(this.peerBase.TrafficPackageHeaderSize);
			this.TrafficStatsGameLevel = new TrafficStatsGameLevel(this.trafficStatsStopwatch);
			bool flag2 = this.trafficStatsEnabled;
			if (flag2)
			{
				this.trafficStatsStopwatch.Start();
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		public string VitalStatsToString(bool all)
		{
			string text = "";
			bool flag = this.TrafficStatsGameLevel != null;
			if (flag)
			{
				text = this.TrafficStatsGameLevel.ToStringVitalStats();
			}
			bool flag2 = !all;
			string result;
			if (flag2)
			{
				result = string.Format("Rtt(variance): {0}({1}). Since receive: {2}ms. Longest send: {5}ms. Stats elapsed: {4}sec.\n{3}", new object[]
				{
					this.RoundTripTime,
					this.RoundTripTimeVariance,
					this.ConnectionTime - this.TimestampOfLastSocketReceive,
					text,
					this.TrafficStatsElapsedMs / 1000L,
					this.LongestSentCall
				});
			}
			else
			{
				result = string.Format("Rtt(variance): {0}({1}). Since receive: {2}ms. Longest send: {7}ms. Stats elapsed: {6}sec.\n{3}\n{4}\n{5}", new object[]
				{
					this.RoundTripTime,
					this.RoundTripTimeVariance,
					this.ConnectionTime - this.TimestampOfLastSocketReceive,
					text,
					this.TrafficStatsIncoming,
					this.TrafficStatsOutgoing,
					this.TrafficStatsElapsedMs / 1000L,
					this.LongestSentCall
				});
			}
			return result;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		public Type PayloadEncryptorType
		{
			get
			{
				return this.payloadEncryptorType;
			}
			set
			{
				bool flag = value == null || typeof(ICryptoProvider).IsAssignableFrom(value);
				bool flag2 = flag;
				if (flag2)
				{
					this.payloadEncryptorType = value;
				}
				else
				{
					this.Listener.DebugReturn(DebugLevel.WARNING, "Failed to set the EncryptorType. Type must implement IPhotonEncryptor.");
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000C33C File Offset: 0x0000A53C
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000C354 File Offset: 0x0000A554
		public Type EncryptorType
		{
			get
			{
				return this.encryptorType;
			}
			set
			{
				bool flag = value == null || typeof(IPhotonEncryptor).IsAssignableFrom(value);
				bool flag2 = flag;
				if (flag2)
				{
					this.encryptorType = value;
				}
				else
				{
					this.Listener.DebugReturn(DebugLevel.WARNING, "Failed to set the EncryptorType. Type must implement IPhotonEncryptor.");
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000C3A5 File Offset: 0x0000A5A5
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000C3AD File Offset: 0x0000A5AD
		public int CountDiscarded
		{
			[CompilerGenerated]
			get
			{
				return this.<CountDiscarded>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CountDiscarded>k__BackingField = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000C3B6 File Offset: 0x0000A5B6
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000C3BE File Offset: 0x0000A5BE
		public int DeltaUnreliableNumber
		{
			[CompilerGenerated]
			get
			{
				return this.<DeltaUnreliableNumber>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DeltaUnreliableNumber>k__BackingField = value;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public PhotonPeer(ConnectionProtocol protocolType)
		{
			this.TransportProtocol = protocolType;
			this.SocketImplementationConfig = new Dictionary<ConnectionProtocol, Type>(5);
			this.SocketImplementationConfig[ConnectionProtocol.Udp] = typeof(SocketUdp);
			this.SocketImplementationConfig[ConnectionProtocol.Tcp] = typeof(SocketTcp);
			this.SocketImplementationConfig[ConnectionProtocol.WebSocket] = typeof(PhotonClientWebSocket);
			this.SocketImplementationConfig[ConnectionProtocol.WebSocketSecure] = typeof(PhotonClientWebSocket);
			this.CreatePeerBase();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000C4F7 File Offset: 0x0000A6F7
		public PhotonPeer(IPhotonPeerListener listener, ConnectionProtocol protocolType) : this(protocolType)
		{
			this.Listener = listener;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000C50C File Offset: 0x0000A70C
		public virtual bool Connect(string serverAddress, string appId, object photonToken = null, object customInitData = null)
		{
			return this.Connect(serverAddress, null, appId, photonToken, customInitData);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000C52C File Offset: 0x0000A72C
		public virtual bool Connect(string serverAddress, string proxyServerAddress, string appId, object photonToken, object customInitData = null)
		{
			object dispatchLockObject = this.DispatchLockObject;
			bool result;
			lock (dispatchLockObject)
			{
				object sendOutgoingLockObject = this.SendOutgoingLockObject;
				lock (sendOutgoingLockObject)
				{
					bool flag3 = this.peerBase != null && this.peerBase.peerConnectionState > ConnectionStateValue.Disconnected;
					if (flag3)
					{
						this.Listener.DebugReturn(DebugLevel.WARNING, "Connect() can't be called if peer is not Disconnected. Not connecting.");
						result = false;
					}
					else
					{
						bool flag4 = photonToken == null;
						if (flag4)
						{
							this.Encryptor = null;
							this.RandomizedSequenceNumbers = null;
							this.RandomizeSequenceNumbers = false;
							this.GcmDatagramEncryption = false;
						}
						this.CreatePeerBase();
						this.peerBase.Reset();
						this.PingUsedAsInit = false;
						this.peerBase.ServerAddress = serverAddress;
						this.peerBase.ProxyServerAddress = proxyServerAddress;
						this.peerBase.AppId = appId;
						this.peerBase.PhotonToken = photonToken;
						this.peerBase.CustomInitData = customInitData;
						Type socketImplementation = null;
						bool flag5 = !this.SocketImplementationConfig.TryGetValue(this.TransportProtocol, out socketImplementation);
						if (flag5)
						{
							this.peerBase.EnqueueDebugReturn(DebugLevel.ERROR, "Connect failed. SocketImplementationConfig is not set for protocol " + this.TransportProtocol.ToString() + ": " + SupportClass.DictionaryToString(this.SocketImplementationConfig, false));
							result = false;
						}
						else
						{
							this.SocketImplementation = socketImplementation;
							try
							{
								this.peerBase.PhotonSocket = (IPhotonSocket)Activator.CreateInstance(this.SocketImplementation, new object[]
								{
									this.peerBase
								});
							}
							catch (Exception ex)
							{
								IPhotonPeerListener listener = this.Listener;
								DebugLevel level = DebugLevel.ERROR;
								string[] array = new string[6];
								array[0] = "Connect() failed to create a IPhotonSocket instance for ";
								array[1] = this.TransportProtocol.ToString();
								array[2] = ". SocketImplementationConfig: ";
								array[3] = SupportClass.DictionaryToString(this.SocketImplementationConfig, false);
								array[4] = " Exception: ";
								int num = 5;
								Exception ex2 = ex;
								array[num] = ((ex2 != null) ? ex2.ToString() : null);
								listener.DebugReturn(level, string.Concat(array));
								return false;
							}
							result = this.peerBase.Connect(serverAddress, proxyServerAddress, appId, photonToken);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		private void CreatePeerBase()
		{
			ConnectionProtocol transportProtocol = this.TransportProtocol;
			ConnectionProtocol connectionProtocol = transportProtocol;
			if (connectionProtocol != ConnectionProtocol.Tcp && connectionProtocol - ConnectionProtocol.WebSocket > 1)
			{
				bool flag = !(this.peerBase is EnetPeer);
				if (flag)
				{
					this.peerBase = new EnetPeer();
				}
			}
			else
			{
				TPeer tpeer = this.peerBase as TPeer;
				bool flag2 = tpeer == null;
				if (flag2)
				{
					tpeer = new TPeer();
					this.peerBase = tpeer;
				}
				tpeer.DoFraming = (this.TransportProtocol == ConnectionProtocol.Tcp);
			}
			this.peerBase.photonPeer = this;
			this.peerBase.usedTransportProtocol = this.TransportProtocol;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000C840 File Offset: 0x0000AA40
		public virtual void Disconnect()
		{
			object dispatchLockObject = this.DispatchLockObject;
			lock (dispatchLockObject)
			{
				object sendOutgoingLockObject = this.SendOutgoingLockObject;
				lock (sendOutgoingLockObject)
				{
					this.peerBase.Disconnect();
				}
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		internal void OnDisconnectMessageCall(DisconnectMessage dm)
		{
			bool flag = this.OnDisconnectMessage != null;
			if (flag)
			{
				this.OnDisconnectMessage(dm);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		public virtual void StopThread()
		{
			object dispatchLockObject = this.DispatchLockObject;
			lock (dispatchLockObject)
			{
				object sendOutgoingLockObject = this.SendOutgoingLockObject;
				lock (sendOutgoingLockObject)
				{
					this.peerBase.StopConnection();
				}
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public virtual void FetchServerTimestamp()
		{
			this.peerBase.FetchServerTimestamp();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C96C File Offset: 0x0000AB6C
		public bool EstablishEncryption()
		{
			bool asyncKeyExchange = PhotonPeer.AsyncKeyExchange;
			bool result;
			if (asyncKeyExchange)
			{
				SupportClass.StartBackgroundCalls(delegate
				{
					this.peerBase.ExchangeKeysForEncryption(this.SendOutgoingLockObject);
					return false;
				}, 100, "");
				result = true;
			}
			else
			{
				result = this.peerBase.ExchangeKeysForEncryption(this.SendOutgoingLockObject);
			}
			return result;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		public bool InitDatagramEncryption(byte[] encryptionSecret, byte[] hmacSecret, bool randomizedSequenceNumbers = false, bool chainingModeGCM = false)
		{
			bool flag = this.EncryptorType != null;
			if (flag)
			{
				try
				{
					this.Encryptor = (IPhotonEncryptor)Activator.CreateInstance(this.EncryptorType);
					bool flag2 = this.Encryptor == null;
					if (flag2)
					{
						this.Listener.DebugReturn(DebugLevel.WARNING, "Datagram encryptor creation by type failed, Activator.CreateInstance() returned null");
					}
				}
				catch (Exception ex)
				{
					IPhotonPeerListener listener = this.Listener;
					DebugLevel level = DebugLevel.WARNING;
					string str = "Datagram encryptor creation by type failed: ";
					Exception ex2 = ex;
					listener.DebugReturn(level, str + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
			bool flag3 = this.Encryptor == null;
			if (flag3)
			{
				this.Encryptor = new EncryptorNet();
			}
			bool flag4 = this.Encryptor == null;
			if (flag4)
			{
				throw new NullReferenceException("Can not init datagram encryption. No suitable encryptor found or provided.");
			}
			IPhotonPeerListener listener2 = this.Listener;
			DebugLevel level2 = DebugLevel.INFO;
			string str2 = "Datagram encryptor of type ";
			Type type = this.Encryptor.GetType();
			listener2.DebugReturn(level2, str2 + ((type != null) ? type.ToString() : null) + " created. Api version: " + 2.ToString());
			this.Listener.DebugReturn(DebugLevel.INFO, "Datagram encryptor initialization: GCM = " + chainingModeGCM.ToString() + ", random seq num = " + randomizedSequenceNumbers.ToString());
			this.Encryptor.Init(encryptionSecret, hmacSecret, null, chainingModeGCM, this.mtu);
			bool flag5 = randomizedSequenceNumbers;
			if (flag5)
			{
				this.RandomizedSequenceNumbers = encryptionSecret;
				this.RandomizeSequenceNumbers = true;
				this.GcmDatagramEncryption = chainingModeGCM;
			}
			return true;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public void InitPayloadEncryption(byte[] secret)
		{
			this.PayloadEncryptionSecret = secret;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000CB34 File Offset: 0x0000AD34
		public virtual void Service()
		{
			while (this.DispatchIncomingCommands())
			{
			}
			while (this.SendOutgoingCommands())
			{
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000CB60 File Offset: 0x0000AD60
		public virtual bool SendOutgoingCommands()
		{
			bool flag = this.TrafficStatsEnabled;
			if (flag)
			{
				this.TrafficStatsGameLevel.SendOutgoingCommandsCalled();
			}
			object sendOutgoingLockObject = this.SendOutgoingLockObject;
			bool result;
			lock (sendOutgoingLockObject)
			{
				result = this.peerBase.SendOutgoingCommands();
			}
			return result;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		public virtual bool SendAcksOnly()
		{
			bool flag = this.TrafficStatsEnabled;
			if (flag)
			{
				this.TrafficStatsGameLevel.SendOutgoingCommandsCalled();
			}
			object sendOutgoingLockObject = this.SendOutgoingLockObject;
			bool result;
			lock (sendOutgoingLockObject)
			{
				result = this.peerBase.SendAcksOnly();
			}
			return result;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000CC28 File Offset: 0x0000AE28
		public virtual bool DispatchIncomingCommands()
		{
			bool flag = this.TrafficStatsEnabled;
			if (flag)
			{
				this.TrafficStatsGameLevel.DispatchIncomingCommandsCalled();
			}
			object dispatchLockObject = this.DispatchLockObject;
			bool result;
			lock (dispatchLockObject)
			{
				this.peerBase.ByteCountCurrentDispatch = 0;
				result = this.peerBase.DispatchIncomingCommands();
			}
			return result;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000CC98 File Offset: 0x0000AE98
		public virtual bool SendOperation(byte operationCode, Dictionary<byte, object> operationParameters, SendOptions sendOptions)
		{
			bool flag = sendOptions.Encrypt && !this.IsEncryptionAvailable && this.peerBase.usedTransportProtocol != ConnectionProtocol.WebSocketSecure;
			if (flag)
			{
				throw new ArgumentException("Can't use encryption yet. Exchange keys first.");
			}
			bool flag2 = this.peerBase.peerConnectionState != ConnectionStateValue.Connected;
			bool result;
			if (flag2)
			{
				bool flag3 = this.DebugOut >= DebugLevel.ERROR;
				if (flag3)
				{
					this.Listener.DebugReturn(DebugLevel.ERROR, "Cannot send op: " + operationCode.ToString() + " Not connected. PeerState: " + this.peerBase.peerConnectionState.ToString());
				}
				this.Listener.OnStatusChanged(StatusCode.SendError);
				result = false;
			}
			else
			{
				bool flag4 = sendOptions.Channel >= this.ChannelCount;
				if (flag4)
				{
					bool flag5 = this.DebugOut >= DebugLevel.ERROR;
					if (flag5)
					{
						this.Listener.DebugReturn(DebugLevel.ERROR, string.Concat(new string[]
						{
							"Cannot send op: Selected channel (",
							sendOptions.Channel.ToString(),
							")>= channelCount (",
							this.ChannelCount.ToString(),
							")."
						}));
					}
					this.Listener.OnStatusChanged(StatusCode.SendError);
					result = false;
				}
				else
				{
					object enqueueLock = this.EnqueueLock;
					lock (enqueueLock)
					{
						StreamBuffer opBytes = this.peerBase.SerializeOperationToMessage(operationCode, operationParameters, EgMessageType.Operation, sendOptions.Encrypt);
						result = this.peerBase.EnqueuePhotonMessage(opBytes, sendOptions);
					}
				}
			}
			return result;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000CE3C File Offset: 0x0000B03C
		public virtual bool SendOperation(byte operationCode, ParameterDictionary operationParameters, SendOptions sendOptions)
		{
			bool flag = sendOptions.Encrypt && !this.IsEncryptionAvailable && this.peerBase.usedTransportProtocol != ConnectionProtocol.WebSocketSecure;
			if (flag)
			{
				throw new ArgumentException("Can't use encryption yet. Exchange keys first.");
			}
			bool flag2 = this.peerBase.peerConnectionState != ConnectionStateValue.Connected;
			bool result;
			if (flag2)
			{
				bool flag3 = this.DebugOut >= DebugLevel.ERROR;
				if (flag3)
				{
					this.Listener.DebugReturn(DebugLevel.ERROR, "Cannot send op: " + operationCode.ToString() + " Not connected. PeerState: " + this.peerBase.peerConnectionState.ToString());
				}
				this.Listener.OnStatusChanged(StatusCode.SendError);
				result = false;
			}
			else
			{
				bool flag4 = sendOptions.Channel >= this.ChannelCount;
				if (flag4)
				{
					bool flag5 = this.DebugOut >= DebugLevel.ERROR;
					if (flag5)
					{
						this.Listener.DebugReturn(DebugLevel.ERROR, string.Concat(new string[]
						{
							"Cannot send op: Selected channel (",
							sendOptions.Channel.ToString(),
							")>= channelCount (",
							this.ChannelCount.ToString(),
							")."
						}));
					}
					this.Listener.OnStatusChanged(StatusCode.SendError);
					result = false;
				}
				else
				{
					object enqueueLock = this.EnqueueLock;
					lock (enqueueLock)
					{
						StreamBuffer opBytes = this.peerBase.SerializeOperationToMessage(operationCode, operationParameters, EgMessageType.Operation, sendOptions.Encrypt);
						result = this.peerBase.EnqueuePhotonMessage(opBytes, sendOptions);
					}
				}
			}
			return result;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000CFE0 File Offset: 0x0000B1E0
		public static bool RegisterType(Type customType, byte code, SerializeMethod serializeMethod, DeserializeMethod constructor)
		{
			return Protocol.TryRegisterType(customType, code, serializeMethod, constructor);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		public static bool RegisterType(Type customType, byte code, SerializeStreamMethod serializeMethod, DeserializeStreamMethod constructor)
		{
			return Protocol.TryRegisterType(customType, code, serializeMethod, constructor);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000D017 File Offset: 0x0000B217
		// Note: this type is marked as 'beforefieldinit'.
		static PhotonPeer()
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000D03C File Offset: 0x0000B23C
		[CompilerGenerated]
		private bool <EstablishEncryption>b__225_0()
		{
			this.peerBase.ExchangeKeysForEncryption(this.SendOutgoingLockObject);
			return false;
		}

		// Token: 0x04000125 RID: 293
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <CommandBufferSize>k__BackingField;

		// Token: 0x04000126 RID: 294
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <LimitOfUnreliableCommands>k__BackingField;

		// Token: 0x04000127 RID: 295
		[Obsolete("Check QueuedOutgoingCommands and QueuedIncomingCommands on demand instead.")]
		public int WarningSize;

		// Token: 0x04000128 RID: 296
		[Obsolete("Where dynamic linking is available, this library will attempt to load it and fallback to a managed implementation. This value is always true.")]
		public const bool NativeDatagramEncrypt = true;

		// Token: 0x04000129 RID: 297
		[Obsolete("Use the ITrafficRecorder to capture all traffic instead.")]
		public int CommandLogSize;

		// Token: 0x0400012A RID: 298
		public const bool NoSocket = false;

		// Token: 0x0400012B RID: 299
		public const bool DebugBuild = true;

		// Token: 0x0400012C RID: 300
		public const int NativeEncryptorApiVersion = 2;

		// Token: 0x0400012D RID: 301
		public TargetFrameworks TargetFramework = TargetFrameworks.NetStandard20;

		// Token: 0x0400012E RID: 302
		public static bool NoNativeCallbacks;

		// Token: 0x0400012F RID: 303
		public bool RemoveAppIdFromWebSocketPath;

		// Token: 0x04000130 RID: 304
		public byte ClientSdkId = 15;

		// Token: 0x04000131 RID: 305
		private static string clientVersion;

		// Token: 0x04000132 RID: 306
		[Obsolete("A Native Socket implementation is no longer part of this DLL but delivered in a separate add-on. This value always returns false.")]
		public static readonly bool NativeSocketLibAvailable = false;

		// Token: 0x04000133 RID: 307
		[Obsolete("Native Payload Encryption is no longer part of this DLL but delivered in a separate add-on. This value always returns false.")]
		public static readonly bool NativePayloadEncryptionLibAvailable = false;

		// Token: 0x04000134 RID: 308
		[Obsolete("Native Datagram Encryption is no longer part of this DLL but delivered in a separate add-on. This value always returns false.")]
		public static readonly bool NativeDatagramEncryptionLibAvailable = false;

		// Token: 0x04000135 RID: 309
		internal bool UseInitV3;

		// Token: 0x04000136 RID: 310
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SerializationProtocol <SerializationProtocolType>k__BackingField;

		// Token: 0x04000137 RID: 311
		public Dictionary<ConnectionProtocol, Type> SocketImplementationConfig;

		// Token: 0x04000138 RID: 312
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Type <SocketImplementation>k__BackingField;

		// Token: 0x04000139 RID: 313
		public DebugLevel DebugOut = DebugLevel.ERROR;

		// Token: 0x0400013A RID: 314
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IPhotonPeerListener <Listener>k__BackingField;

		// Token: 0x0400013B RID: 315
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<DisconnectMessage> OnDisconnectMessage;

		// Token: 0x0400013C RID: 316
		private bool reuseEventInstance;

		// Token: 0x0400013D RID: 317
		private bool useByteArraySlicePoolForEvents = false;

		// Token: 0x0400013E RID: 318
		private bool wrapIncomingStructs = false;

		// Token: 0x0400013F RID: 319
		public bool SendInCreationOrder = true;

		// Token: 0x04000140 RID: 320
		public int SendWindowSize = 50;

		// Token: 0x04000141 RID: 321
		public ITrafficRecorder TrafficRecorder;

		// Token: 0x04000142 RID: 322
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <EnableServerTracing>k__BackingField;

		// Token: 0x04000143 RID: 323
		private byte quickResendAttempts;

		// Token: 0x04000144 RID: 324
		public byte ChannelCount = 2;

		// Token: 0x04000145 RID: 325
		public bool EnableEncryptedFlag = false;

		// Token: 0x04000146 RID: 326
		private bool crcEnabled;

		// Token: 0x04000147 RID: 327
		public int SentCountAllowance = 7;

		// Token: 0x04000148 RID: 328
		public int InitialResendTimeMax = 400;

		// Token: 0x04000149 RID: 329
		public int TimePingInterval = 1000;

		// Token: 0x0400014A RID: 330
		public bool PingUsedAsInit = false;

		// Token: 0x0400014B RID: 331
		private int disconnectTimeout = 10000;

		// Token: 0x0400014C RID: 332
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ConnectionProtocol <TransportProtocol>k__BackingField;

		// Token: 0x0400014D RID: 333
		public static int OutgoingStreamBufferSize = 1200;

		// Token: 0x0400014E RID: 334
		private int mtu = 1200;

		// Token: 0x0400014F RID: 335
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <IsSendingOnlyAcks>k__BackingField;

		// Token: 0x04000150 RID: 336
		public static bool AsyncKeyExchange = false;

		// Token: 0x04000151 RID: 337
		internal bool RandomizeSequenceNumbers;

		// Token: 0x04000152 RID: 338
		internal byte[] RandomizedSequenceNumbers;

		// Token: 0x04000153 RID: 339
		internal bool GcmDatagramEncryption;

		// Token: 0x04000154 RID: 340
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TrafficStats <TrafficStatsIncoming>k__BackingField;

		// Token: 0x04000155 RID: 341
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TrafficStats <TrafficStatsOutgoing>k__BackingField;

		// Token: 0x04000156 RID: 342
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TrafficStatsGameLevel <TrafficStatsGameLevel>k__BackingField;

		// Token: 0x04000157 RID: 343
		private Stopwatch trafficStatsStopwatch;

		// Token: 0x04000158 RID: 344
		private bool trafficStatsEnabled = false;

		// Token: 0x04000159 RID: 345
		internal PeerBase peerBase;

		// Token: 0x0400015A RID: 346
		private readonly object SendOutgoingLockObject = new object();

		// Token: 0x0400015B RID: 347
		private readonly object DispatchLockObject = new object();

		// Token: 0x0400015C RID: 348
		private readonly object EnqueueLock = new object();

		// Token: 0x0400015D RID: 349
		private Type payloadEncryptorType;

		// Token: 0x0400015E RID: 350
		protected internal byte[] PayloadEncryptionSecret;

		// Token: 0x0400015F RID: 351
		private Type encryptorType;

		// Token: 0x04000160 RID: 352
		protected internal IPhotonEncryptor Encryptor;

		// Token: 0x04000161 RID: 353
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <CountDiscarded>k__BackingField;

		// Token: 0x04000162 RID: 354
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <DeltaUnreliableNumber>k__BackingField;
	}
}
