using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Security;
using System.Threading;

namespace System.Net
{
	/// <summary>Manages the collection of <see cref="T:System.Net.ServicePoint" /> objects.</summary>
	// Token: 0x020006AE RID: 1710
	public class ServicePointManager
	{
		// Token: 0x060036DE RID: 14046 RVA: 0x000C05C0 File Offset: 0x000BE7C0
		static ServicePointManager()
		{
			ConnectionManagementSection connectionManagementSection = ConfigurationManager.GetSection("system.net/connectionManagement") as ConnectionManagementSection;
			if (connectionManagementSection != null)
			{
				ServicePointManager.manager = new ConnectionManagementData(null);
				foreach (object obj in connectionManagementSection.ConnectionManagement)
				{
					ConnectionManagementElement connectionManagementElement = (ConnectionManagementElement)obj;
					ServicePointManager.manager.Add(connectionManagementElement.Address, connectionManagementElement.MaxConnection);
				}
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
				return;
			}
			ServicePointManager.manager = (ConnectionManagementData)ConfigurationSettings.GetConfig("system.net/connectionManagement");
			if (ServicePointManager.manager != null)
			{
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
			}
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x0000219B File Offset: 0x0000039B
		private ServicePointManager()
		{
		}

		/// <summary>Gets or sets policy for server certificates.</summary>
		/// <returns>An object that implements the <see cref="T:System.Net.ICertificatePolicy" /> interface.</returns>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060036E0 RID: 14048 RVA: 0x000C06C8 File Offset: 0x000BE8C8
		// (set) Token: 0x060036E1 RID: 14049 RVA: 0x000C06E7 File Offset: 0x000BE8E7
		[Obsolete("Use ServerCertificateValidationCallback instead", false)]
		public static ICertificatePolicy CertificatePolicy
		{
			get
			{
				if (ServicePointManager.policy == null)
				{
					Interlocked.CompareExchange<ICertificatePolicy>(ref ServicePointManager.policy, new DefaultCertificatePolicy(), null);
				}
				return ServicePointManager.policy;
			}
			set
			{
				ServicePointManager.policy = value;
			}
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000C06EF File Offset: 0x000BE8EF
		internal static ICertificatePolicy GetLegacyCertificatePolicy()
		{
			return ServicePointManager.policy;
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate revocation list is checked; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000C06F6 File Offset: 0x000BE8F6
		// (set) Token: 0x060036E4 RID: 14052 RVA: 0x000C06FD File Offset: 0x000BE8FD
		[MonoTODO("CRL checks not implemented")]
		public static bool CheckCertificateRevocationList
		{
			get
			{
				return ServicePointManager._checkCRL;
			}
			set
			{
				ServicePointManager._checkCRL = false;
			}
		}

		/// <summary>Gets or sets the maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object. The default connection limit is 10 for ASP.NET hosted applications and 2 for all others. When an app is running as an ASP.NET host, it is not possible to alter the value of this property through the config file if the autoConfig property is set to <see langword="true" />. However, you can change the value programmatically when the autoConfig property is <see langword="true" />. Set your preferred value once, when the AppDomain loads.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> is less than or equal to 0.</exception>
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x000C0705 File Offset: 0x000BE905
		// (set) Token: 0x060036E6 RID: 14054 RVA: 0x000C070C File Offset: 0x000BE90C
		public static int DefaultConnectionLimit
		{
			get
			{
				return ServicePointManager.defaultConnectionLimit;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ServicePointManager.defaultConnectionLimit = value;
				if (ServicePointManager.manager != null)
				{
					ServicePointManager.manager.Add("*", ServicePointManager.defaultConnectionLimit);
				}
			}
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x0001FD2F File Offset: 0x0001DF2F
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets a value that indicates how long a Domain Name Service (DNS) resolution is considered valid.</summary>
		/// <returns>The time-out value, in milliseconds. A value of -1 indicates an infinite time-out period. The default value is 120,000 milliseconds (two minutes).</returns>
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060036E8 RID: 14056 RVA: 0x000C073E File Offset: 0x000BE93E
		// (set) Token: 0x060036E9 RID: 14057 RVA: 0x000C0745 File Offset: 0x000BE945
		public static int DnsRefreshTimeout
		{
			get
			{
				return ServicePointManager.dnsRefreshTimeout;
			}
			set
			{
				ServicePointManager.dnsRefreshTimeout = Math.Max(-1, value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether a Domain Name Service (DNS) resolution rotates among the applicable Internet Protocol (IP) addresses.</summary>
		/// <returns>
		///   <see langword="false" /> if a DNS resolution always returns the first IP address for a particular host; otherwise <see langword="true" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000C0753 File Offset: 0x000BE953
		// (set) Token: 0x060036EB RID: 14059 RVA: 0x000C0753 File Offset: 0x000BE953
		[MonoTODO]
		public static bool EnableDnsRoundRobin
		{
			get
			{
				throw ServicePointManager.GetMustImplement();
			}
			set
			{
				throw ServicePointManager.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the maximum idle time of a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum idle time, in milliseconds, of a <see cref="T:System.Net.ServicePoint" /> object. The default value is 100,000 milliseconds (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePointIdleTime" /> is less than <see cref="F:System.Threading.Timeout.Infinite" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x000C075A File Offset: 0x000BE95A
		// (set) Token: 0x060036ED RID: 14061 RVA: 0x000C0761 File Offset: 0x000BE961
		public static int MaxServicePointIdleTime
		{
			get
			{
				return ServicePointManager.maxServicePointIdleTime;
			}
			set
			{
				if (value < -2 || value > 2147483647)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ServicePointManager.maxServicePointIdleTime = value;
			}
		}

		/// <summary>Gets or sets the maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain at any time.</summary>
		/// <returns>The maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain. The default value is 0, which means there is no limit to the number of <see cref="T:System.Net.ServicePoint" /> objects.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x000C0781 File Offset: 0x000BE981
		// (set) Token: 0x060036EF RID: 14063 RVA: 0x000C0788 File Offset: 0x000BE988
		public static int MaxServicePoints
		{
			get
			{
				return ServicePointManager.maxServicePoints;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("value");
				}
				ServicePointManager.maxServicePoints = value;
			}
		}

		/// <summary>Setting this property value to <see langword="true" /> causes all outbound TCP connections from HttpWebRequest to use the native socket option SO_REUSE_UNICASTPORT on the socket. This causes the underlying outgoing ports to be shared. This is useful for scenarios where a large number of outgoing connections are made in a short time, and the app risks running out of ports.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x00003062 File Offset: 0x00001262
		// (set) Token: 0x060036F1 RID: 14065 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public static bool ReusePort
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the security protocol used by the <see cref="T:System.Net.ServicePoint" /> objects managed by the <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>One of the values defined in the <see cref="T:System.Net.SecurityProtocolType" /> enumeration.</returns>
		/// <exception cref="T:System.NotSupportedException">The value specified to set the property is not a valid <see cref="T:System.Net.SecurityProtocolType" /> enumeration value.</exception>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000C079F File Offset: 0x000BE99F
		// (set) Token: 0x060036F3 RID: 14067 RVA: 0x000C07A6 File Offset: 0x000BE9A6
		public static SecurityProtocolType SecurityProtocol
		{
			get
			{
				return ServicePointManager._securityProtocol;
			}
			set
			{
				ServicePointManager._securityProtocol = value;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000C07AE File Offset: 0x000BE9AE
		internal static ServerCertValidationCallback ServerCertValidationCallback
		{
			get
			{
				return ServicePointManager.server_cert_cb;
			}
		}

		/// <summary>Gets or sets the callback to validate a server certificate.</summary>
		/// <returns>A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" />. The default value is <see langword="null" />.</returns>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000C07B5 File Offset: 0x000BE9B5
		// (set) Token: 0x060036F6 RID: 14070 RVA: 0x000C07CA File Offset: 0x000BE9CA
		public static RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (ServicePointManager.server_cert_cb == null)
				{
					return null;
				}
				return ServicePointManager.server_cert_cb.ValidationCallback;
			}
			set
			{
				if (value == null)
				{
					ServicePointManager.server_cert_cb = null;
					return;
				}
				ServicePointManager.server_cert_cb = new ServerCertValidationCallback(value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Security.EncryptionPolicy" /> for this <see cref="T:System.Net.ServicePointManager" /> instance.</summary>
		/// <returns>The encryption policy to use for this <see cref="T:System.Net.ServicePointManager" /> instance.</returns>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x00003062 File Offset: 0x00001262
		[MonoTODO("Always returns EncryptionPolicy.RequireEncryption.")]
		public static EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return EncryptionPolicy.RequireEncryption;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>
		///   <see langword="true" /> to enable 100-Continue behavior. The default value is <see langword="true" />.</returns>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000C07E1 File Offset: 0x000BE9E1
		// (set) Token: 0x060036F9 RID: 14073 RVA: 0x000C07E8 File Offset: 0x000BE9E8
		public static bool Expect100Continue
		{
			get
			{
				return ServicePointManager.expectContinue;
			}
			set
			{
				ServicePointManager.expectContinue = value;
			}
		}

		/// <summary>Determines whether the Nagle algorithm is used by the service points managed by this <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> to use the Nagle algorithm; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000C07F0 File Offset: 0x000BE9F0
		// (set) Token: 0x060036FB RID: 14075 RVA: 0x000C07F7 File Offset: 0x000BE9F7
		public static bool UseNagleAlgorithm
		{
			get
			{
				return ServicePointManager.useNagle;
			}
			set
			{
				ServicePointManager.useNagle = value;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x00003062 File Offset: 0x00001262
		internal static bool DisableStrongCrypto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x00003062 File Offset: 0x00001262
		internal static bool DisableSendAuxRecord
		{
			get
			{
				return false;
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
		// Token: 0x060036FE RID: 14078 RVA: 0x000C07FF File Offset: 0x000BE9FF
		public static void SetTcpKeepAlive(bool enabled, int keepAliveTime, int keepAliveInterval)
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
			ServicePointManager.tcp_keepalive = enabled;
			ServicePointManager.tcp_keepalive_time = keepAliveTime;
			ServicePointManager.tcp_keepalive_interval = keepAliveInterval;
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <param name="address">The <see cref="T:System.Uri" /> object of the Internet resource to contact.</param>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached.</exception>
		// Token: 0x060036FF RID: 14079 RVA: 0x000C083E File Offset: 0x000BEA3E
		public static ServicePoint FindServicePoint(Uri address)
		{
			return ServicePointManager.FindServicePoint(address, null);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uriString">The URI of the Internet resource to be contacted.</param>
		/// <param name="proxy">The proxy data for this request.</param>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="uriString" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached.</exception>
		// Token: 0x06003700 RID: 14080 RVA: 0x000C0847 File Offset: 0x000BEA47
		public static ServicePoint FindServicePoint(string uriString, IWebProxy proxy)
		{
			return ServicePointManager.FindServicePoint(new Uri(uriString), proxy);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> object that contains the address of the Internet resource to contact.</param>
		/// <param name="proxy">The proxy data for this request.</param>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached.</exception>
		// Token: 0x06003701 RID: 14081 RVA: 0x000C0858 File Offset: 0x000BEA58
		public static ServicePoint FindServicePoint(Uri address, IWebProxy proxy)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Uri uri = new Uri(address.Scheme + "://" + address.Authority);
			bool flag = false;
			bool flag2 = false;
			if (proxy != null && !proxy.IsBypassed(address))
			{
				flag = true;
				bool flag3 = address.Scheme == "https";
				address = proxy.GetProxy(address);
				if (address.Scheme != "http")
				{
					throw new NotSupportedException("Proxy scheme not supported.");
				}
				if (flag3 && address.Scheme == "http")
				{
					flag2 = true;
				}
			}
			address = new Uri(address.Scheme + "://" + address.Authority);
			ServicePointManager.SPKey key = new ServicePointManager.SPKey(uri, flag ? address : null, flag2);
			ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint> obj = ServicePointManager.servicePoints;
			ServicePoint result;
			lock (obj)
			{
				ServicePoint servicePoint;
				if (ServicePointManager.servicePoints.TryGetValue(key, out servicePoint))
				{
					result = servicePoint;
				}
				else
				{
					if (ServicePointManager.maxServicePoints > 0 && ServicePointManager.servicePoints.Count >= ServicePointManager.maxServicePoints)
					{
						throw new InvalidOperationException("maximum number of service points reached");
					}
					string hostOrIP = address.ToString();
					int maxConnections = (int)ServicePointManager.manager.GetMaxConnections(hostOrIP);
					servicePoint = new ServicePoint(key, address, maxConnections, ServicePointManager.maxServicePointIdleTime);
					servicePoint.Expect100Continue = ServicePointManager.expectContinue;
					servicePoint.UseNagleAlgorithm = ServicePointManager.useNagle;
					servicePoint.UsesProxy = flag;
					servicePoint.UseConnect = flag2;
					servicePoint.SetTcpKeepAlive(ServicePointManager.tcp_keepalive, ServicePointManager.tcp_keepalive_time, ServicePointManager.tcp_keepalive_interval);
					result = ServicePointManager.servicePoints.GetOrAdd(key, servicePoint);
				}
			}
			return result;
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000C09FC File Offset: 0x000BEBFC
		internal static void CloseConnectionGroup(string connectionGroupName)
		{
			ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint> obj = ServicePointManager.servicePoints;
			lock (obj)
			{
				foreach (ServicePoint servicePoint in ServicePointManager.servicePoints.Values)
				{
					servicePoint.CloseConnectionGroup(connectionGroupName);
				}
			}
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000C0A74 File Offset: 0x000BEC74
		internal static void RemoveServicePoint(ServicePoint sp)
		{
			ServicePoint servicePoint;
			ServicePointManager.servicePoints.TryRemove(sp.Key, out servicePoint);
		}

		// Token: 0x04001FF9 RID: 8185
		private static ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint> servicePoints = new ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint>();

		// Token: 0x04001FFA RID: 8186
		private static ICertificatePolicy policy;

		// Token: 0x04001FFB RID: 8187
		private static int defaultConnectionLimit = 2;

		// Token: 0x04001FFC RID: 8188
		private static int maxServicePointIdleTime = 100000;

		// Token: 0x04001FFD RID: 8189
		private static int maxServicePoints = 0;

		// Token: 0x04001FFE RID: 8190
		private static int dnsRefreshTimeout = 120000;

		// Token: 0x04001FFF RID: 8191
		private static bool _checkCRL = false;

		// Token: 0x04002000 RID: 8192
		private static SecurityProtocolType _securityProtocol = SecurityProtocolType.SystemDefault;

		// Token: 0x04002001 RID: 8193
		private static bool expectContinue = true;

		// Token: 0x04002002 RID: 8194
		private static bool useNagle;

		// Token: 0x04002003 RID: 8195
		private static ServerCertValidationCallback server_cert_cb;

		// Token: 0x04002004 RID: 8196
		private static bool tcp_keepalive;

		// Token: 0x04002005 RID: 8197
		private static int tcp_keepalive_time;

		// Token: 0x04002006 RID: 8198
		private static int tcp_keepalive_interval;

		/// <summary>The default number of non-persistent connections (4) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.0 or later server. This field is constant but is no longer used in the .NET Framework 2.0.</summary>
		// Token: 0x04002007 RID: 8199
		public const int DefaultNonPersistentConnectionLimit = 4;

		/// <summary>The default number of persistent connections (2) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.1 or later server. This field is constant and is used to initialize the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property if the value of the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property has not been set either directly or through configuration.</summary>
		// Token: 0x04002008 RID: 8200
		public const int DefaultPersistentConnectionLimit = 2;

		// Token: 0x04002009 RID: 8201
		private const string configKey = "system.net/connectionManagement";

		// Token: 0x0400200A RID: 8202
		private static ConnectionManagementData manager;

		// Token: 0x020006AF RID: 1711
		internal class SPKey
		{
			// Token: 0x06003704 RID: 14084 RVA: 0x000C0A94 File Offset: 0x000BEC94
			public SPKey(Uri uri, Uri proxy, bool use_connect)
			{
				this.uri = uri;
				this.proxy = proxy;
				this.use_connect = use_connect;
			}

			// Token: 0x17000B79 RID: 2937
			// (get) Token: 0x06003705 RID: 14085 RVA: 0x000C0AB1 File Offset: 0x000BECB1
			public Uri Uri
			{
				get
				{
					return this.uri;
				}
			}

			// Token: 0x17000B7A RID: 2938
			// (get) Token: 0x06003706 RID: 14086 RVA: 0x000C0AB9 File Offset: 0x000BECB9
			public bool UseConnect
			{
				get
				{
					return this.use_connect;
				}
			}

			// Token: 0x17000B7B RID: 2939
			// (get) Token: 0x06003707 RID: 14087 RVA: 0x000C0AC1 File Offset: 0x000BECC1
			public bool UsesProxy
			{
				get
				{
					return this.proxy != null;
				}
			}

			// Token: 0x06003708 RID: 14088 RVA: 0x000C0AD0 File Offset: 0x000BECD0
			public override int GetHashCode()
			{
				return ((23 * 31 + (this.use_connect ? 1 : 0)) * 31 + this.uri.GetHashCode()) * 31 + ((this.proxy != null) ? this.proxy.GetHashCode() : 0);
			}

			// Token: 0x06003709 RID: 14089 RVA: 0x000C0B20 File Offset: 0x000BED20
			public override bool Equals(object obj)
			{
				ServicePointManager.SPKey spkey = obj as ServicePointManager.SPKey;
				return obj != null && this.uri.Equals(spkey.uri) && this.use_connect == spkey.use_connect && this.UsesProxy == spkey.UsesProxy && (!this.UsesProxy || this.proxy.Equals(spkey.proxy));
			}

			// Token: 0x0400200B RID: 8203
			private Uri uri;

			// Token: 0x0400200C RID: 8204
			private Uri proxy;

			// Token: 0x0400200D RID: 8205
			private bool use_connect;
		}
	}
}
