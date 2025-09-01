using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x02000009 RID: 9
	[Obsolete("The UNET transport will be removed in the future as soon a replacement is ready.")]
	[Serializable]
	public class ConnectionConfig
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public ConnectionConfig()
		{
			this.m_PacketSize = 1440;
			this.m_FragmentSize = 500;
			this.m_ResendTimeout = 1200U;
			this.m_DisconnectTimeout = 2000U;
			this.m_ConnectTimeout = 2000U;
			this.m_MinUpdateTimeout = 10U;
			this.m_PingTimeout = 500U;
			this.m_ReducedPingTimeout = 100U;
			this.m_AllCostTimeout = 20U;
			this.m_NetworkDropThreshold = 5;
			this.m_OverflowDropThreshold = 5;
			this.m_MaxConnectionAttempt = 10;
			this.m_AckDelay = 33U;
			this.m_SendDelay = 10U;
			this.m_MaxCombinedReliableMessageSize = 100;
			this.m_MaxCombinedReliableMessageCount = 10;
			this.m_MaxSentMessageQueueSize = 512;
			this.m_AcksType = ConnectionAcksType.Acks32;
			this.m_UsePlatformSpecificProtocols = false;
			this.m_InitialBandwidth = 0U;
			this.m_BandwidthPeakFactor = 2f;
			this.m_WebSocketReceiveBufferMaxSize = 0;
			this.m_UdpSocketReceiveBufferMaxSize = 0U;
			this.m_SSLCertFilePath = null;
			this.m_SSLPrivateKeyFilePath = null;
			this.m_SSLCAFilePath = null;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public ConnectionConfig(ConnectionConfig config)
		{
			bool flag = config == null;
			if (flag)
			{
				throw new NullReferenceException("config is not defined");
			}
			this.m_PacketSize = config.m_PacketSize;
			this.m_FragmentSize = config.m_FragmentSize;
			this.m_ResendTimeout = config.m_ResendTimeout;
			this.m_DisconnectTimeout = config.m_DisconnectTimeout;
			this.m_ConnectTimeout = config.m_ConnectTimeout;
			this.m_MinUpdateTimeout = config.m_MinUpdateTimeout;
			this.m_PingTimeout = config.m_PingTimeout;
			this.m_ReducedPingTimeout = config.m_ReducedPingTimeout;
			this.m_AllCostTimeout = config.m_AllCostTimeout;
			this.m_NetworkDropThreshold = config.m_NetworkDropThreshold;
			this.m_OverflowDropThreshold = config.m_OverflowDropThreshold;
			this.m_MaxConnectionAttempt = config.m_MaxConnectionAttempt;
			this.m_AckDelay = config.m_AckDelay;
			this.m_SendDelay = config.m_SendDelay;
			this.m_MaxCombinedReliableMessageSize = config.MaxCombinedReliableMessageSize;
			this.m_MaxCombinedReliableMessageCount = config.m_MaxCombinedReliableMessageCount;
			this.m_MaxSentMessageQueueSize = config.m_MaxSentMessageQueueSize;
			this.m_AcksType = config.m_AcksType;
			this.m_UsePlatformSpecificProtocols = config.m_UsePlatformSpecificProtocols;
			this.m_InitialBandwidth = config.m_InitialBandwidth;
			bool flag2 = this.m_InitialBandwidth == 0U;
			if (flag2)
			{
				this.m_InitialBandwidth = (uint)(this.m_PacketSize * 1000) / this.m_MinUpdateTimeout;
			}
			this.m_BandwidthPeakFactor = config.m_BandwidthPeakFactor;
			this.m_WebSocketReceiveBufferMaxSize = config.m_WebSocketReceiveBufferMaxSize;
			this.m_UdpSocketReceiveBufferMaxSize = config.m_UdpSocketReceiveBufferMaxSize;
			this.m_SSLCertFilePath = config.m_SSLCertFilePath;
			this.m_SSLPrivateKeyFilePath = config.m_SSLPrivateKeyFilePath;
			this.m_SSLCAFilePath = config.m_SSLCAFilePath;
			foreach (ChannelQOS channel in config.m_Channels)
			{
				this.m_Channels.Add(new ChannelQOS(channel));
			}
			foreach (List<byte> item in config.m_SharedOrderChannels)
			{
				this.m_SharedOrderChannels.Add(item);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002E38 File Offset: 0x00001038
		public static void Validate(ConnectionConfig config)
		{
			bool flag = config.m_PacketSize < 128;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("PacketSize should be > " + 128.ToString());
			}
			bool flag2 = config.m_FragmentSize >= config.m_PacketSize - 128;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("FragmentSize should be < PacketSize - " + 128.ToString());
			}
			bool flag3 = config.m_Channels.Count > 255;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("Channels number should be less than 256");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002ED0 File Offset: 0x000010D0
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002EE8 File Offset: 0x000010E8
		public ushort PacketSize
		{
			get
			{
				return this.m_PacketSize;
			}
			set
			{
				this.m_PacketSize = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002EF4 File Offset: 0x000010F4
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002F0C File Offset: 0x0000110C
		public ushort FragmentSize
		{
			get
			{
				return this.m_FragmentSize;
			}
			set
			{
				this.m_FragmentSize = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002F18 File Offset: 0x00001118
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002F30 File Offset: 0x00001130
		public uint ResendTimeout
		{
			get
			{
				return this.m_ResendTimeout;
			}
			set
			{
				this.m_ResendTimeout = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002F3C File Offset: 0x0000113C
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002F54 File Offset: 0x00001154
		public uint DisconnectTimeout
		{
			get
			{
				return this.m_DisconnectTimeout;
			}
			set
			{
				this.m_DisconnectTimeout = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002F60 File Offset: 0x00001160
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002F78 File Offset: 0x00001178
		public uint ConnectTimeout
		{
			get
			{
				return this.m_ConnectTimeout;
			}
			set
			{
				this.m_ConnectTimeout = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002F84 File Offset: 0x00001184
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002F9C File Offset: 0x0000119C
		public uint MinUpdateTimeout
		{
			get
			{
				return this.m_MinUpdateTimeout;
			}
			set
			{
				bool flag = value == 0U;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Minimal update timeout should be > 0");
				}
				this.m_MinUpdateTimeout = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002FC4 File Offset: 0x000011C4
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002FDC File Offset: 0x000011DC
		public uint PingTimeout
		{
			get
			{
				return this.m_PingTimeout;
			}
			set
			{
				this.m_PingTimeout = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003000 File Offset: 0x00001200
		public uint ReducedPingTimeout
		{
			get
			{
				return this.m_ReducedPingTimeout;
			}
			set
			{
				this.m_ReducedPingTimeout = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000300C File Offset: 0x0000120C
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003024 File Offset: 0x00001224
		public uint AllCostTimeout
		{
			get
			{
				return this.m_AllCostTimeout;
			}
			set
			{
				this.m_AllCostTimeout = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003030 File Offset: 0x00001230
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003048 File Offset: 0x00001248
		public byte NetworkDropThreshold
		{
			get
			{
				return this.m_NetworkDropThreshold;
			}
			set
			{
				this.m_NetworkDropThreshold = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003054 File Offset: 0x00001254
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000306C File Offset: 0x0000126C
		public byte OverflowDropThreshold
		{
			get
			{
				return this.m_OverflowDropThreshold;
			}
			set
			{
				this.m_OverflowDropThreshold = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003078 File Offset: 0x00001278
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003090 File Offset: 0x00001290
		public byte MaxConnectionAttempt
		{
			get
			{
				return this.m_MaxConnectionAttempt;
			}
			set
			{
				this.m_MaxConnectionAttempt = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000309C File Offset: 0x0000129C
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000030B4 File Offset: 0x000012B4
		public uint AckDelay
		{
			get
			{
				return this.m_AckDelay;
			}
			set
			{
				this.m_AckDelay = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000030C0 File Offset: 0x000012C0
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000030D8 File Offset: 0x000012D8
		public uint SendDelay
		{
			get
			{
				return this.m_SendDelay;
			}
			set
			{
				this.m_SendDelay = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000030E4 File Offset: 0x000012E4
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000030FC File Offset: 0x000012FC
		public ushort MaxCombinedReliableMessageSize
		{
			get
			{
				return this.m_MaxCombinedReliableMessageSize;
			}
			set
			{
				this.m_MaxCombinedReliableMessageSize = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003108 File Offset: 0x00001308
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003120 File Offset: 0x00001320
		public ushort MaxCombinedReliableMessageCount
		{
			get
			{
				return this.m_MaxCombinedReliableMessageCount;
			}
			set
			{
				this.m_MaxCombinedReliableMessageCount = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000312C File Offset: 0x0000132C
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003144 File Offset: 0x00001344
		public ushort MaxSentMessageQueueSize
		{
			get
			{
				return this.m_MaxSentMessageQueueSize;
			}
			set
			{
				this.m_MaxSentMessageQueueSize = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003150 File Offset: 0x00001350
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003168 File Offset: 0x00001368
		public ConnectionAcksType AcksType
		{
			get
			{
				return this.m_AcksType;
			}
			set
			{
				this.m_AcksType = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003174 File Offset: 0x00001374
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003194 File Offset: 0x00001394
		[Obsolete("IsAcksLong is deprecated. Use AcksType = ConnectionAcksType.Acks64", false)]
		public bool IsAcksLong
		{
			get
			{
				return this.m_AcksType != ConnectionAcksType.Acks32;
			}
			set
			{
				bool flag = value && this.m_AcksType == ConnectionAcksType.Acks32;
				if (flag)
				{
					this.m_AcksType = ConnectionAcksType.Acks64;
				}
				else
				{
					bool flag2 = !value;
					if (flag2)
					{
						this.m_AcksType = ConnectionAcksType.Acks32;
					}
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000031D0 File Offset: 0x000013D0
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000031E8 File Offset: 0x000013E8
		public bool UsePlatformSpecificProtocols
		{
			get
			{
				return this.m_UsePlatformSpecificProtocols;
			}
			set
			{
				bool flag = value && Application.platform != RuntimePlatform.PS4;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Platform specific protocols are not supported on this platform");
				}
				this.m_UsePlatformSpecificProtocols = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003220 File Offset: 0x00001420
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003238 File Offset: 0x00001438
		public uint InitialBandwidth
		{
			get
			{
				return this.m_InitialBandwidth;
			}
			set
			{
				this.m_InitialBandwidth = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003244 File Offset: 0x00001444
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000325C File Offset: 0x0000145C
		public float BandwidthPeakFactor
		{
			get
			{
				return this.m_BandwidthPeakFactor;
			}
			set
			{
				this.m_BandwidthPeakFactor = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003268 File Offset: 0x00001468
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003280 File Offset: 0x00001480
		public ushort WebSocketReceiveBufferMaxSize
		{
			get
			{
				return this.m_WebSocketReceiveBufferMaxSize;
			}
			set
			{
				this.m_WebSocketReceiveBufferMaxSize = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000328C File Offset: 0x0000148C
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000032A4 File Offset: 0x000014A4
		public uint UdpSocketReceiveBufferMaxSize
		{
			get
			{
				return this.m_UdpSocketReceiveBufferMaxSize;
			}
			set
			{
				this.m_UdpSocketReceiveBufferMaxSize = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000032B0 File Offset: 0x000014B0
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000032C8 File Offset: 0x000014C8
		public string SSLCertFilePath
		{
			get
			{
				return this.m_SSLCertFilePath;
			}
			set
			{
				this.m_SSLCertFilePath = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000032D4 File Offset: 0x000014D4
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000032EC File Offset: 0x000014EC
		public string SSLPrivateKeyFilePath
		{
			get
			{
				return this.m_SSLPrivateKeyFilePath;
			}
			set
			{
				this.m_SSLPrivateKeyFilePath = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000032F8 File Offset: 0x000014F8
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003310 File Offset: 0x00001510
		public string SSLCAFilePath
		{
			get
			{
				return this.m_SSLCAFilePath;
			}
			set
			{
				this.m_SSLCAFilePath = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000331C File Offset: 0x0000151C
		public int ChannelCount
		{
			get
			{
				return this.m_Channels.Count;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000333C File Offset: 0x0000153C
		public int SharedOrderChannelCount
		{
			get
			{
				return this.m_SharedOrderChannels.Count;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000335C File Offset: 0x0000155C
		public byte AddChannel(QosType value)
		{
			bool flag = this.m_Channels.Count > 255;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("Channels Count should be less than 256");
			}
			bool flag2 = !Enum.IsDefined(typeof(QosType), value);
			if (flag2)
			{
				string str = "requested qos type doesn't exist: ";
				int num = (int)value;
				throw new ArgumentOutOfRangeException(str + num.ToString());
			}
			ChannelQOS item = new ChannelQOS(value);
			this.m_Channels.Add(item);
			return (byte)(this.m_Channels.Count - 1);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000033EC File Offset: 0x000015EC
		public void MakeChannelsSharedOrder(List<byte> channelIndices)
		{
			bool flag = channelIndices == null;
			if (flag)
			{
				throw new NullReferenceException("channelIndices must not be null");
			}
			bool flag2 = channelIndices.Count == 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("Received empty list of shared order channel indexes");
			}
			byte b = 0;
			while ((int)b < channelIndices.Count)
			{
				byte b2 = channelIndices[(int)b];
				bool flag3 = (int)b2 >= this.m_Channels.Count;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("Shared order channel list contains wrong channel index " + b2.ToString());
				}
				ChannelQOS channelQOS = this.m_Channels[(int)b2];
				bool belongsToSharedOrderChannel = channelQOS.BelongsToSharedOrderChannel;
				if (belongsToSharedOrderChannel)
				{
					throw new ArgumentException("Channel with index " + b2.ToString() + " has been already included to other shared order channel");
				}
				bool flag4 = channelQOS.QOS != QosType.Reliable && channelQOS.QOS > QosType.Unreliable;
				if (flag4)
				{
					throw new ArgumentException("Only Reliable and Unreliable QoS are allowed for shared order channel, wrong channel is with index " + b2.ToString());
				}
				b += 1;
			}
			byte b3 = 0;
			while ((int)b3 < channelIndices.Count)
			{
				byte index = channelIndices[(int)b3];
				this.m_Channels[(int)index].m_BelongsSharedOrderChannel = true;
				b3 += 1;
			}
			List<byte> item = new List<byte>(channelIndices);
			this.m_SharedOrderChannels.Add(item);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003538 File Offset: 0x00001738
		public QosType GetChannel(byte idx)
		{
			bool flag = (int)idx >= this.m_Channels.Count;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("requested index greater than maximum channels count");
			}
			return this.m_Channels[(int)idx].QOS;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000357C File Offset: 0x0000177C
		public IList<byte> GetSharedOrderChannels(byte idx)
		{
			bool flag = (int)idx >= this.m_SharedOrderChannels.Count;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("requested index greater than maximum shared order channels count");
			}
			return this.m_SharedOrderChannels[(int)idx].AsReadOnly();
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000035C0 File Offset: 0x000017C0
		public List<ChannelQOS> Channels
		{
			get
			{
				return this.m_Channels;
			}
		}

		// Token: 0x0400002C RID: 44
		private const int g_MinPacketSize = 128;

		// Token: 0x0400002D RID: 45
		[SerializeField]
		private ushort m_PacketSize;

		// Token: 0x0400002E RID: 46
		[SerializeField]
		private ushort m_FragmentSize;

		// Token: 0x0400002F RID: 47
		[SerializeField]
		private uint m_ResendTimeout;

		// Token: 0x04000030 RID: 48
		[SerializeField]
		private uint m_DisconnectTimeout;

		// Token: 0x04000031 RID: 49
		[SerializeField]
		private uint m_ConnectTimeout;

		// Token: 0x04000032 RID: 50
		[SerializeField]
		private uint m_MinUpdateTimeout;

		// Token: 0x04000033 RID: 51
		[SerializeField]
		private uint m_PingTimeout;

		// Token: 0x04000034 RID: 52
		[SerializeField]
		private uint m_ReducedPingTimeout;

		// Token: 0x04000035 RID: 53
		[SerializeField]
		private uint m_AllCostTimeout;

		// Token: 0x04000036 RID: 54
		[SerializeField]
		private byte m_NetworkDropThreshold;

		// Token: 0x04000037 RID: 55
		[SerializeField]
		private byte m_OverflowDropThreshold;

		// Token: 0x04000038 RID: 56
		[SerializeField]
		private byte m_MaxConnectionAttempt;

		// Token: 0x04000039 RID: 57
		[SerializeField]
		private uint m_AckDelay;

		// Token: 0x0400003A RID: 58
		[SerializeField]
		private uint m_SendDelay;

		// Token: 0x0400003B RID: 59
		[SerializeField]
		private ushort m_MaxCombinedReliableMessageSize;

		// Token: 0x0400003C RID: 60
		[SerializeField]
		private ushort m_MaxCombinedReliableMessageCount;

		// Token: 0x0400003D RID: 61
		[SerializeField]
		private ushort m_MaxSentMessageQueueSize;

		// Token: 0x0400003E RID: 62
		[SerializeField]
		private ConnectionAcksType m_AcksType;

		// Token: 0x0400003F RID: 63
		[SerializeField]
		private bool m_UsePlatformSpecificProtocols;

		// Token: 0x04000040 RID: 64
		[SerializeField]
		private uint m_InitialBandwidth;

		// Token: 0x04000041 RID: 65
		[SerializeField]
		private float m_BandwidthPeakFactor;

		// Token: 0x04000042 RID: 66
		[SerializeField]
		private ushort m_WebSocketReceiveBufferMaxSize;

		// Token: 0x04000043 RID: 67
		[SerializeField]
		private uint m_UdpSocketReceiveBufferMaxSize;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		private string m_SSLCertFilePath;

		// Token: 0x04000045 RID: 69
		[SerializeField]
		private string m_SSLPrivateKeyFilePath;

		// Token: 0x04000046 RID: 70
		[SerializeField]
		private string m_SSLCAFilePath;

		// Token: 0x04000047 RID: 71
		[SerializeField]
		internal List<ChannelQOS> m_Channels = new List<ChannelQOS>();

		// Token: 0x04000048 RID: 72
		[SerializeField]
		internal List<List<byte>> m_SharedOrderChannels = new List<List<byte>>();
	}
}
