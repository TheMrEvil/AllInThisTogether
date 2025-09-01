using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200000E RID: 14
	public abstract class IPhotonSocket
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00006EB0 File Offset: 0x000050B0
		protected IPhotonPeerListener Listener
		{
			get
			{
				return this.peerBase.Listener;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00006ED0 File Offset: 0x000050D0
		protected internal int MTU
		{
			get
			{
				return this.peerBase.mtu;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00006EED File Offset: 0x000050ED
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00006EF5 File Offset: 0x000050F5
		public PhotonSocketState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<State>k__BackingField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00006EFE File Offset: 0x000050FE
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00006F06 File Offset: 0x00005106
		public int SocketErrorCode
		{
			[CompilerGenerated]
			get
			{
				return this.<SocketErrorCode>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<SocketErrorCode>k__BackingField = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00006F10 File Offset: 0x00005110
		public bool Connected
		{
			get
			{
				return this.State == PhotonSocketState.Connected;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00006F2B File Offset: 0x0000512B
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00006F33 File Offset: 0x00005133
		public string ServerAddress
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerAddress>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ServerAddress>k__BackingField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00006F3C File Offset: 0x0000513C
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00006F44 File Offset: 0x00005144
		public string ProxyServerAddress
		{
			[CompilerGenerated]
			get
			{
				return this.<ProxyServerAddress>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ProxyServerAddress>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00006F4D File Offset: 0x0000514D
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00006F54 File Offset: 0x00005154
		public static string ServerIpAddress
		{
			[CompilerGenerated]
			get
			{
				return IPhotonSocket.<ServerIpAddress>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				IPhotonSocket.<ServerIpAddress>k__BackingField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00006F5C File Offset: 0x0000515C
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00006F64 File Offset: 0x00005164
		public int ServerPort
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerPort>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ServerPort>k__BackingField = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00006F6D File Offset: 0x0000516D
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00006F75 File Offset: 0x00005175
		public bool AddressResolvedAsIpv6
		{
			[CompilerGenerated]
			get
			{
				return this.<AddressResolvedAsIpv6>k__BackingField;
			}
			[CompilerGenerated]
			protected internal set
			{
				this.<AddressResolvedAsIpv6>k__BackingField = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00006F7E File Offset: 0x0000517E
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00006F86 File Offset: 0x00005186
		public string UrlProtocol
		{
			[CompilerGenerated]
			get
			{
				return this.<UrlProtocol>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<UrlProtocol>k__BackingField = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00006F8F File Offset: 0x0000518F
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00006F97 File Offset: 0x00005197
		public string UrlPath
		{
			[CompilerGenerated]
			get
			{
				return this.<UrlPath>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<UrlPath>k__BackingField = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00006FA0 File Offset: 0x000051A0
		protected internal string SerializationProtocol
		{
			get
			{
				bool flag = this.peerBase == null || this.peerBase.photonPeer == null;
				string result;
				if (flag)
				{
					result = "GpBinaryV18";
				}
				else
				{
					result = Enum.GetName(typeof(SerializationProtocol), this.peerBase.photonPeer.SerializationProtocolType);
				}
				return result;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006FFC File Offset: 0x000051FC
		public IPhotonSocket(PeerBase peerBase)
		{
			bool flag = peerBase == null;
			if (flag)
			{
				throw new Exception("Can't init without peer");
			}
			this.Protocol = peerBase.usedTransportProtocol;
			this.peerBase = peerBase;
			this.ConnectAddress = this.peerBase.ServerAddress;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000704C File Offset: 0x0000524C
		public virtual bool Connect()
		{
			bool flag = this.State > PhotonSocketState.Disconnected;
			bool result;
			if (flag)
			{
				bool flag2 = this.peerBase.debugOut >= DebugLevel.ERROR;
				if (flag2)
				{
					this.peerBase.Listener.DebugReturn(DebugLevel.ERROR, "Connect() failed: connection in State: " + this.State.ToString());
				}
				result = false;
			}
			else
			{
				bool flag3 = this.peerBase == null || this.Protocol != this.peerBase.usedTransportProtocol;
				if (flag3)
				{
					result = false;
				}
				else
				{
					string serverAddress;
					ushort serverPort;
					string urlProtocol;
					string urlPath;
					bool flag4 = !this.TryParseAddress(this.peerBase.ServerAddress, out serverAddress, out serverPort, out urlProtocol, out urlPath);
					if (flag4)
					{
						bool flag5 = this.peerBase.debugOut >= DebugLevel.ERROR;
						if (flag5)
						{
							this.peerBase.Listener.DebugReturn(DebugLevel.ERROR, "Failed parsing address: " + this.peerBase.ServerAddress);
						}
						result = false;
					}
					else
					{
						IPhotonSocket.ServerIpAddress = string.Empty;
						this.ServerAddress = serverAddress;
						this.ServerPort = (int)serverPort;
						this.UrlProtocol = urlProtocol;
						this.UrlPath = urlPath;
						bool flag6 = this.peerBase.debugOut >= DebugLevel.ALL;
						if (flag6)
						{
							this.Listener.DebugReturn(DebugLevel.ALL, string.Concat(new string[]
							{
								"IPhotonSocket.Connect() ",
								this.ServerAddress,
								":",
								this.ServerPort.ToString(),
								" this.Protocol: ",
								this.Protocol.ToString()
							}));
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000093 RID: 147
		public abstract bool Disconnect();

		// Token: 0x06000094 RID: 148
		public abstract PhotonSocketError Send(byte[] data, int length);

		// Token: 0x06000095 RID: 149
		public abstract PhotonSocketError Receive(out byte[] data);

		// Token: 0x06000096 RID: 150 RVA: 0x00007208 File Offset: 0x00005408
		public void HandleReceivedDatagram(byte[] inBuffer, int length, bool willBeReused)
		{
			ITrafficRecorder trafficRecorder = this.peerBase.photonPeer.TrafficRecorder;
			bool flag = trafficRecorder != null && trafficRecorder.Enabled;
			if (flag)
			{
				trafficRecorder.Record(inBuffer, length, true, this.peerBase.peerID, this);
			}
			bool isSimulationEnabled = this.peerBase.NetworkSimulationSettings.IsSimulationEnabled;
			if (isSimulationEnabled)
			{
				if (willBeReused)
				{
					byte[] array = new byte[length];
					Buffer.BlockCopy(inBuffer, 0, array, 0, length);
					this.peerBase.ReceiveNetworkSimulated(array);
				}
				else
				{
					this.peerBase.ReceiveNetworkSimulated(inBuffer);
				}
			}
			else
			{
				this.peerBase.ReceiveIncomingCommands(inBuffer, length);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000072B4 File Offset: 0x000054B4
		public bool ReportDebugOfLevel(DebugLevel levelOfMessage)
		{
			return this.peerBase.debugOut >= levelOfMessage;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000072D7 File Offset: 0x000054D7
		public void EnqueueDebugReturn(DebugLevel debugLevel, string message)
		{
			this.peerBase.EnqueueDebugReturn(debugLevel, message);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000072E8 File Offset: 0x000054E8
		protected internal void HandleException(StatusCode statusCode)
		{
			this.State = PhotonSocketState.Disconnecting;
			this.peerBase.EnqueueStatusCallback(statusCode);
			this.peerBase.EnqueueActionForDispatch(delegate
			{
				this.peerBase.Disconnect();
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007318 File Offset: 0x00005518
		protected internal bool TryParseAddress(string url, out string host, out ushort port, out string scheme, out string absolutePath)
		{
			host = string.Empty;
			port = 0;
			scheme = string.Empty;
			absolutePath = string.Empty;
			bool flag = string.IsNullOrEmpty(url);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = url.Contains("://");
				string uriString = flag2 ? url : ("net.tcp://" + url);
				Uri uri;
				bool flag3 = Uri.TryCreate(uriString, UriKind.Absolute, out uri);
				bool flag4 = flag3;
				if (flag4)
				{
					host = uri.Host;
					port = ((!flag2 && !url.Contains(string.Format(":{0}", uri.Port))) ? 0 : ((ushort)uri.Port));
					scheme = (flag2 ? uri.Scheme : string.Empty);
					absolutePath = ("/".Equals(uri.AbsolutePath) ? string.Empty : uri.AbsolutePath);
				}
				result = flag3;
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000073F8 File Offset: 0x000055F8
		private bool IpAddressTryParse(string strIP, out IPAddress address)
		{
			address = null;
			bool flag = string.IsNullOrEmpty(strIP);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string[] array = strIP.Split(new char[]
				{
					'.'
				});
				bool flag2 = array.Length != 4;
				if (flag2)
				{
					result = false;
				}
				else
				{
					byte[] array2 = new byte[4];
					for (int i = 0; i < array.Length; i++)
					{
						string s = array[i];
						byte b = 0;
						bool flag3 = !byte.TryParse(s, out b);
						if (flag3)
						{
							return false;
						}
						array2[i] = b;
					}
					bool flag4 = array2[0] == 0;
					if (flag4)
					{
						result = false;
					}
					else
					{
						address = new IPAddress(array2);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000074A8 File Offset: 0x000056A8
		protected internal IPAddress[] GetIpAddresses(string hostname)
		{
			IPAddress ipaddress = null;
			bool flag = IPAddress.TryParse(hostname, out ipaddress);
			IPAddress[] result;
			if (flag)
			{
				bool flag2 = ipaddress.AddressFamily == AddressFamily.InterNetworkV6 || this.IpAddressTryParse(hostname, out ipaddress);
				if (flag2)
				{
					result = new IPAddress[]
					{
						ipaddress
					};
				}
				else
				{
					this.HandleException(StatusCode.ServerAddressInvalid);
					result = null;
				}
			}
			else
			{
				IPAddress[] array;
				try
				{
					array = Dns.GetHostAddresses(this.ServerAddress);
				}
				catch (Exception ex)
				{
					try
					{
						IPHostEntry hostByName = Dns.GetHostByName(this.ServerAddress);
						array = hostByName.AddressList;
					}
					catch (Exception ex2)
					{
						bool flag3 = this.ReportDebugOfLevel(DebugLevel.WARNING);
						if (flag3)
						{
							DebugLevel debugLevel = DebugLevel.WARNING;
							string[] array2 = new string[6];
							array2[0] = "GetHostAddresses and GetHostEntry() failed for: ";
							array2[1] = this.ServerAddress;
							array2[2] = ". Caught and handled exceptions:\n";
							int num = 3;
							Exception ex3 = ex;
							array2[num] = ((ex3 != null) ? ex3.ToString() : null);
							array2[4] = "\n";
							int num2 = 5;
							Exception ex4 = ex2;
							array2[num2] = ((ex4 != null) ? ex4.ToString() : null);
							this.EnqueueDebugReturn(debugLevel, string.Concat(array2));
						}
						this.HandleException(StatusCode.DnsExceptionOnConnect);
						return null;
					}
				}
				Array.Sort<IPAddress>(array, new Comparison<IPAddress>(this.AddressSortComparer));
				bool flag4 = this.ReportDebugOfLevel(DebugLevel.INFO);
				if (flag4)
				{
					string[] array3 = (from x in array
					select string.Concat(new string[]
					{
						x.ToString(),
						" (",
						x.AddressFamily.ToString(),
						"(",
						((int)x.AddressFamily).ToString(),
						"))"
					})).ToArray<string>();
					string text = string.Join(", ", array3);
					bool flag5 = this.ReportDebugOfLevel(DebugLevel.INFO);
					if (flag5)
					{
						this.EnqueueDebugReturn(DebugLevel.INFO, string.Concat(new string[]
						{
							this.ServerAddress,
							" resolved to ",
							array3.Length.ToString(),
							" address(es): ",
							text
						}));
					}
				}
				result = array;
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007688 File Offset: 0x00005888
		private int AddressSortComparer(IPAddress x, IPAddress y)
		{
			bool flag = x.AddressFamily == y.AddressFamily;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = ((x.AddressFamily == AddressFamily.InterNetworkV6) ? -1 : 1);
			}
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000076C0 File Offset: 0x000058C0
		[Obsolete("Use GetIpAddresses instead.")]
		protected internal static IPAddress GetIpAddress(string address)
		{
			IPAddress ipaddress = null;
			bool flag = IPAddress.TryParse(address, out ipaddress);
			IPAddress result;
			if (flag)
			{
				result = ipaddress;
			}
			else
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(address);
				IPAddress[] addressList = hostEntry.AddressList;
				foreach (IPAddress ipaddress2 in addressList)
				{
					bool flag2 = ipaddress2.AddressFamily == AddressFamily.InterNetworkV6;
					if (flag2)
					{
						IPhotonSocket.ServerIpAddress = ipaddress2.ToString();
						return ipaddress2;
					}
					bool flag3 = ipaddress == null && ipaddress2.AddressFamily == AddressFamily.InterNetwork;
					if (flag3)
					{
						ipaddress = ipaddress2;
					}
				}
				IPhotonSocket.ServerIpAddress = ((ipaddress != null) ? ipaddress.ToString() : (address + " not resolved"));
				result = ipaddress;
			}
			return result;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007778 File Offset: 0x00005978
		[CompilerGenerated]
		private void <HandleException>b__56_0()
		{
			this.peerBase.Disconnect();
		}

		// Token: 0x04000068 RID: 104
		protected internal PeerBase peerBase;

		// Token: 0x04000069 RID: 105
		protected readonly ConnectionProtocol Protocol;

		// Token: 0x0400006A RID: 106
		public bool PollReceive;

		// Token: 0x0400006B RID: 107
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PhotonSocketState <State>k__BackingField;

		// Token: 0x0400006C RID: 108
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <SocketErrorCode>k__BackingField;

		// Token: 0x0400006D RID: 109
		public string ConnectAddress;

		// Token: 0x0400006E RID: 110
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <ServerAddress>k__BackingField;

		// Token: 0x0400006F RID: 111
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <ProxyServerAddress>k__BackingField;

		// Token: 0x04000070 RID: 112
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static string <ServerIpAddress>k__BackingField;

		// Token: 0x04000071 RID: 113
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <ServerPort>k__BackingField;

		// Token: 0x04000072 RID: 114
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <AddressResolvedAsIpv6>k__BackingField;

		// Token: 0x04000073 RID: 115
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <UrlProtocol>k__BackingField;

		// Token: 0x04000074 RID: 116
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <UrlPath>k__BackingField;

		// Token: 0x02000052 RID: 82
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000430 RID: 1072 RVA: 0x00020E3F File Offset: 0x0001F03F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x00020E4B File Offset: 0x0001F04B
			public <>c()
			{
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x00020E54 File Offset: 0x0001F054
			internal string <GetIpAddresses>b__59_0(IPAddress x)
			{
				return string.Concat(new string[]
				{
					x.ToString(),
					" (",
					x.AddressFamily.ToString(),
					"(",
					((int)x.AddressFamily).ToString(),
					"))"
				});
			}

			// Token: 0x04000223 RID: 547
			public static readonly IPhotonSocket.<>c <>9 = new IPhotonSocket.<>c();

			// Token: 0x04000224 RID: 548
			public static Func<IPAddress, string> <>9__59_0;
		}
	}
}
