using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Unity;

namespace System.Net
{
	/// <summary>Provides connection management for HTTP connections.</summary>
	// Token: 0x020006AD RID: 1709
	public class ServicePoint
	{
		// Token: 0x060036B1 RID: 14001 RVA: 0x000BFF40 File Offset: 0x000BE140
		internal ServicePoint(ServicePointManager.SPKey key, Uri uri, int connectionLimit, int maxIdleTime)
		{
			this.sendContinue = true;
			this.hostE = new object();
			this.connectionLeaseTimeout = -1;
			this.receiveBufferSize = -1;
			base..ctor();
			this.Key = key;
			this.uri = uri;
			this.connectionLimit = connectionLimit;
			this.maxIdleTime = maxIdleTime;
			this.Scheduler = new ServicePointScheduler(this, connectionLimit, maxIdleTime);
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060036B2 RID: 14002 RVA: 0x000BFF9F File Offset: 0x000BE19F
		internal ServicePointManager.SPKey Key
		{
			[CompilerGenerated]
			get
			{
				return this.<Key>k__BackingField;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x000BFFA7 File Offset: 0x000BE1A7
		// (set) Token: 0x060036B4 RID: 14004 RVA: 0x000BFFAF File Offset: 0x000BE1AF
		private ServicePointScheduler Scheduler
		{
			[CompilerGenerated]
			get
			{
				return this.<Scheduler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Scheduler>k__BackingField = value;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</summary>
		/// <returns>An instance of the <see cref="T:System.Uri" /> class that contains the URI of the Internet server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.ServicePoint" /> is in host mode.</exception>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060036B5 RID: 14005 RVA: 0x000BFFB8 File Offset: 0x000BE1B8
		public Uri Address
		{
			get
			{
				return this.uri;
			}
		}

		/// <summary>Specifies the delegate to associate a local <see cref="T:System.Net.IPEndPoint" /> with a <see cref="T:System.Net.ServicePoint" />.</summary>
		/// <returns>A delegate that forces a <see cref="T:System.Net.ServicePoint" /> to use a particular local Internet Protocol (IP) address and port number. The default value is <see langword="null" />.</returns>
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000BFFC0 File Offset: 0x000BE1C0
		// (set) Token: 0x060036B7 RID: 14007 RVA: 0x000BFFC8 File Offset: 0x000BE1C8
		public BindIPEndPoint BindIPEndPointDelegate
		{
			get
			{
				return this.endPointCallback;
			}
			set
			{
				this.endPointCallback = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds after which an active <see cref="T:System.Net.ServicePoint" /> connection is closed.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the number of milliseconds that an active <see cref="T:System.Net.ServicePoint" /> connection remains open. The default is -1, which allows an active <see cref="T:System.Net.ServicePoint" /> connection to stay connected indefinitely. Set this property to 0 to force <see cref="T:System.Net.ServicePoint" /> connections to close after servicing a request.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is a negative number less than -1.</exception>
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060036B8 RID: 14008 RVA: 0x000BFFD1 File Offset: 0x000BE1D1
		// (set) Token: 0x060036B9 RID: 14009 RVA: 0x000BFFD9 File Offset: 0x000BE1D9
		public int ConnectionLeaseTimeout
		{
			get
			{
				return this.connectionLeaseTimeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.connectionLeaseTimeout = value;
			}
		}

		/// <summary>Gets or sets the maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The connection limit is equal to or less than 0.</exception>
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000BFFF1 File Offset: 0x000BE1F1
		// (set) Token: 0x060036BB RID: 14011 RVA: 0x000BFFF9 File Offset: 0x000BE1F9
		public int ConnectionLimit
		{
			get
			{
				return this.connectionLimit;
			}
			set
			{
				this.connectionLimit = value;
				if (!this.disposed)
				{
					this.Scheduler.ConnectionLimit = value;
				}
			}
		}

		/// <summary>Gets the connection name.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the connection name.</returns>
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000C0016 File Offset: 0x000BE216
		public string ConnectionName
		{
			get
			{
				return this.uri.Scheme;
			}
		}

		/// <summary>Gets the number of open connections associated with this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The number of open connections associated with this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060036BD RID: 14013 RVA: 0x000C0023 File Offset: 0x000BE223
		public int CurrentConnections
		{
			get
			{
				if (!this.disposed)
				{
					return this.Scheduler.CurrentConnections;
				}
				return 0;
			}
		}

		/// <summary>Gets the date and time that the <see cref="T:System.Net.ServicePoint" /> object was last connected to a host.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the date and time at which the <see cref="T:System.Net.ServicePoint" /> object was last connected.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x000C003C File Offset: 0x000BE23C
		public DateTime IdleSince
		{
			get
			{
				if (this.disposed)
				{
					return DateTime.MinValue;
				}
				return this.Scheduler.IdleSince.ToLocalTime();
			}
		}

		/// <summary>Gets or sets the amount of time a connection associated with the <see cref="T:System.Net.ServicePoint" /> object can remain idle before the connection is closed.</summary>
		/// <returns>The length of time, in milliseconds, that a connection associated with the <see cref="T:System.Net.ServicePoint" /> object can remain idle before it is closed and reused for another connection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePoint.MaxIdleTime" /> is set to less than <see cref="F:System.Threading.Timeout.Infinite" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x000C006A File Offset: 0x000BE26A
		// (set) Token: 0x060036C0 RID: 14016 RVA: 0x000C0072 File Offset: 0x000BE272
		public int MaxIdleTime
		{
			get
			{
				return this.maxIdleTime;
			}
			set
			{
				this.maxIdleTime = value;
				if (!this.disposed)
				{
					this.Scheduler.MaxIdleTime = value;
				}
			}
		}

		/// <summary>Gets the version of the HTTP protocol that the <see cref="T:System.Net.ServicePoint" /> object uses.</summary>
		/// <returns>A <see cref="T:System.Version" /> object that contains the HTTP protocol version that the <see cref="T:System.Net.ServicePoint" /> object uses.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000C008F File Offset: 0x000BE28F
		public virtual Version ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
		}

		/// <summary>Gets or sets the size of the receiving buffer for the socket used by this <see cref="T:System.Net.ServicePoint" />.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the size, in bytes, of the receive buffer. The default is 8192.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x000C0097 File Offset: 0x000BE297
		// (set) Token: 0x060036C3 RID: 14019 RVA: 0x000C009F File Offset: 0x000BE29F
		public int ReceiveBufferSize
		{
			get
			{
				return this.receiveBufferSize;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.receiveBufferSize = value;
			}
		}

		/// <summary>Indicates whether the <see cref="T:System.Net.ServicePoint" /> object supports pipelined connections.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.ServicePoint" /> object supports pipelined connections; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x000C00B7 File Offset: 0x000BE2B7
		public bool SupportsPipelining
		{
			get
			{
				return HttpVersion.Version11.Equals(this.protocolVersion);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>
		///   <see langword="true" /> to expect 100-Continue responses for <see langword="POST" /> requests; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000C00C9 File Offset: 0x000BE2C9
		// (set) Token: 0x060036C6 RID: 14022 RVA: 0x000C00D1 File Offset: 0x000BE2D1
		public bool Expect100Continue
		{
			get
			{
				return this.SendContinue;
			}
			set
			{
				this.SendContinue = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether the Nagle algorithm is used on connections managed by this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> to use the Nagle algorithm; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000C00DA File Offset: 0x000BE2DA
		// (set) Token: 0x060036C8 RID: 14024 RVA: 0x000C00E2 File Offset: 0x000BE2E2
		public bool UseNagleAlgorithm
		{
			get
			{
				return this.useNagle;
			}
			set
			{
				this.useNagle = value;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000C00EB File Offset: 0x000BE2EB
		// (set) Token: 0x060036CA RID: 14026 RVA: 0x000C0117 File Offset: 0x000BE317
		internal bool SendContinue
		{
			get
			{
				return this.sendContinue && (this.protocolVersion == null || this.protocolVersion == HttpVersion.Version11);
			}
			set
			{
				this.sendContinue = value;
			}
		}

		/// <summary>Enables or disables the keep-alive option on a TCP connection.</summary>
		/// <param name="enabled">If set to true, then the TCP keep-alive option on a TCP connection will be enabled using the specified <paramref name="keepAliveTime" /> and <paramref name="keepAliveInterval" /> values.  
		///  If set to false, then the TCP keep-alive option is disabled and the remaining parameters are ignored.  
		///  The default value is false.</param>
		/// <param name="keepAliveTime">Specifies the timeout, in milliseconds, with no activity until the first keep-alive packet is sent.  
		///  The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <param name="keepAliveInterval">Specifies the interval, in milliseconds, between when successive keep-alive packets are sent if no acknowledgement is received.  
		///  The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for <paramref name="keepAliveTime" /> or <paramref name="keepAliveInterval" /> parameter is less than or equal to 0.</exception>
		// Token: 0x060036CB RID: 14027 RVA: 0x000C0120 File Offset: 0x000BE320
		public void SetTcpKeepAlive(bool enabled, int keepAliveTime, int keepAliveInterval)
		{
			if (enabled)
			{
				if (keepAliveTime <= 0)
				{
					throw new ArgumentOutOfRangeException("keepAliveTime", "Must be greater than 0");
				}
				if (keepAliveInterval <= 0)
				{
					throw new ArgumentOutOfRangeException("keepAliveInterval", "Must be greater than 0");
				}
			}
			this.tcp_keepalive = enabled;
			this.tcp_keepalive_time = keepAliveTime;
			this.tcp_keepalive_interval = keepAliveInterval;
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000C0170 File Offset: 0x000BE370
		internal void KeepAliveSetup(Socket socket)
		{
			if (!this.tcp_keepalive)
			{
				return;
			}
			byte[] array = new byte[12];
			ServicePoint.PutBytes(array, this.tcp_keepalive ? 1U : 0U, 0);
			ServicePoint.PutBytes(array, (uint)this.tcp_keepalive_time, 4);
			ServicePoint.PutBytes(array, (uint)this.tcp_keepalive_interval, 8);
			socket.IOControl((IOControlCode)((ulong)-1744830460), array, null);
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000C01CC File Offset: 0x000BE3CC
		private static void PutBytes(byte[] bytes, uint v, int offset)
		{
			if (BitConverter.IsLittleEndian)
			{
				bytes[offset] = (byte)(v & 255U);
				bytes[offset + 1] = (byte)((v & 65280U) >> 8);
				bytes[offset + 2] = (byte)((v & 16711680U) >> 16);
				bytes[offset + 3] = (byte)((v & 4278190080U) >> 24);
				return;
			}
			bytes[offset + 3] = (byte)(v & 255U);
			bytes[offset + 2] = (byte)((v & 65280U) >> 8);
			bytes[offset + 1] = (byte)((v & 16711680U) >> 16);
			bytes[offset] = (byte)((v & 4278190080U) >> 24);
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060036CE RID: 14030 RVA: 0x000C0255 File Offset: 0x000BE455
		// (set) Token: 0x060036CF RID: 14031 RVA: 0x000C025D File Offset: 0x000BE45D
		internal bool UsesProxy
		{
			get
			{
				return this.usesProxy;
			}
			set
			{
				this.usesProxy = value;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x000C0266 File Offset: 0x000BE466
		// (set) Token: 0x060036D1 RID: 14033 RVA: 0x000C026E File Offset: 0x000BE46E
		internal bool UseConnect
		{
			get
			{
				return this.useConnect;
			}
			set
			{
				this.useConnect = value;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060036D2 RID: 14034 RVA: 0x000C0278 File Offset: 0x000BE478
		private bool HasTimedOut
		{
			get
			{
				int dnsRefreshTimeout = ServicePointManager.DnsRefreshTimeout;
				return dnsRefreshTimeout != -1 && this.lastDnsResolve + TimeSpan.FromMilliseconds((double)dnsRefreshTimeout) < DateTime.UtcNow;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060036D3 RID: 14035 RVA: 0x000C02B0 File Offset: 0x000BE4B0
		internal IPHostEntry HostEntry
		{
			get
			{
				object obj = this.hostE;
				lock (obj)
				{
					string text = this.uri.Host;
					if (this.uri.HostNameType == UriHostNameType.IPv6 || this.uri.HostNameType == UriHostNameType.IPv4)
					{
						if (this.host != null)
						{
							return this.host;
						}
						if (this.uri.HostNameType == UriHostNameType.IPv6)
						{
							text = text.Substring(1, text.Length - 2);
						}
						this.host = new IPHostEntry();
						this.host.AddressList = new IPAddress[]
						{
							IPAddress.Parse(text)
						};
						return this.host;
					}
					else
					{
						if (!this.HasTimedOut && this.host != null)
						{
							return this.host;
						}
						this.lastDnsResolve = DateTime.UtcNow;
						try
						{
							this.host = Dns.GetHostEntry(text);
						}
						catch
						{
							return null;
						}
					}
				}
				return this.host;
			}
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000C03BC File Offset: 0x000BE5BC
		internal void SetVersion(Version version)
		{
			this.protocolVersion = version;
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000C03C8 File Offset: 0x000BE5C8
		internal void SendRequest(WebOperation operation, string groupName)
		{
			lock (this)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(typeof(ServicePoint).FullName);
				}
				this.Scheduler.SendRequest(operation, groupName);
			}
		}

		/// <summary>Removes the specified connection group from this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <param name="connectionGroupName">The name of the connection group that contains the connections to close and remove from this service point.</param>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the connection group was closed.</returns>
		// Token: 0x060036D6 RID: 14038 RVA: 0x000C0428 File Offset: 0x000BE628
		public bool CloseConnectionGroup(string connectionGroupName)
		{
			bool result;
			lock (this)
			{
				if (this.disposed)
				{
					result = true;
				}
				else
				{
					result = this.Scheduler.CloseConnectionGroup(connectionGroupName);
				}
			}
			return result;
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000C0478 File Offset: 0x000BE678
		internal void FreeServicePoint()
		{
			this.disposed = true;
			this.Scheduler = null;
		}

		/// <summary>Gets the certificate received for this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>An instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class that contains the security certificate received for this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000C0488 File Offset: 0x000BE688
		public X509Certificate Certificate
		{
			get
			{
				object serverCertificateOrBytes = this.m_ServerCertificateOrBytes;
				if (serverCertificateOrBytes != null && serverCertificateOrBytes.GetType() == typeof(byte[]))
				{
					return (X509Certificate)(this.m_ServerCertificateOrBytes = new X509Certificate((byte[])serverCertificateOrBytes));
				}
				return serverCertificateOrBytes as X509Certificate;
			}
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000C04D6 File Offset: 0x000BE6D6
		internal void UpdateServerCertificate(X509Certificate certificate)
		{
			if (certificate != null)
			{
				this.m_ServerCertificateOrBytes = certificate.GetRawCertData();
				return;
			}
			this.m_ServerCertificateOrBytes = null;
		}

		/// <summary>Gets the last client certificate sent to the server.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object that contains the public values of the last client certificate sent to the server.</returns>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x000C04F0 File Offset: 0x000BE6F0
		public X509Certificate ClientCertificate
		{
			get
			{
				object clientCertificateOrBytes = this.m_ClientCertificateOrBytes;
				if (clientCertificateOrBytes != null && clientCertificateOrBytes.GetType() == typeof(byte[]))
				{
					return (X509Certificate)(this.m_ClientCertificateOrBytes = new X509Certificate((byte[])clientCertificateOrBytes));
				}
				return clientCertificateOrBytes as X509Certificate;
			}
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000C053E File Offset: 0x000BE73E
		internal void UpdateClientCertificate(X509Certificate certificate)
		{
			if (certificate != null)
			{
				this.m_ClientCertificateOrBytes = certificate.GetRawCertData();
				return;
			}
			this.m_ClientCertificateOrBytes = null;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000C0558 File Offset: 0x000BE758
		internal bool CallEndPointDelegate(Socket sock, IPEndPoint remote)
		{
			if (this.endPointCallback == null)
			{
				return true;
			}
			int num = 0;
			checked
			{
				for (;;)
				{
					IPEndPoint ipendPoint = null;
					try
					{
						ipendPoint = this.endPointCallback(this, remote, num);
					}
					catch
					{
						return false;
					}
					if (ipendPoint == null)
					{
						break;
					}
					try
					{
						sock.Bind(ipendPoint);
					}
					catch (SocketException)
					{
						num++;
						continue;
					}
					return true;
				}
				return true;
			}
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal ServicePoint()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001FE3 RID: 8163
		private readonly Uri uri;

		// Token: 0x04001FE4 RID: 8164
		private DateTime lastDnsResolve;

		// Token: 0x04001FE5 RID: 8165
		private Version protocolVersion;

		// Token: 0x04001FE6 RID: 8166
		private IPHostEntry host;

		// Token: 0x04001FE7 RID: 8167
		private bool usesProxy;

		// Token: 0x04001FE8 RID: 8168
		private bool sendContinue;

		// Token: 0x04001FE9 RID: 8169
		private bool useConnect;

		// Token: 0x04001FEA RID: 8170
		private object hostE;

		// Token: 0x04001FEB RID: 8171
		private bool useNagle;

		// Token: 0x04001FEC RID: 8172
		private BindIPEndPoint endPointCallback;

		// Token: 0x04001FED RID: 8173
		private bool tcp_keepalive;

		// Token: 0x04001FEE RID: 8174
		private int tcp_keepalive_time;

		// Token: 0x04001FEF RID: 8175
		private int tcp_keepalive_interval;

		// Token: 0x04001FF0 RID: 8176
		private bool disposed;

		// Token: 0x04001FF1 RID: 8177
		private int connectionLeaseTimeout;

		// Token: 0x04001FF2 RID: 8178
		private int receiveBufferSize;

		// Token: 0x04001FF3 RID: 8179
		[CompilerGenerated]
		private readonly ServicePointManager.SPKey <Key>k__BackingField;

		// Token: 0x04001FF4 RID: 8180
		[CompilerGenerated]
		private ServicePointScheduler <Scheduler>k__BackingField;

		// Token: 0x04001FF5 RID: 8181
		private int connectionLimit;

		// Token: 0x04001FF6 RID: 8182
		private int maxIdleTime;

		// Token: 0x04001FF7 RID: 8183
		private object m_ServerCertificateOrBytes;

		// Token: 0x04001FF8 RID: 8184
		private object m_ClientCertificateOrBytes;
	}
}
