using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using Mono.Net;

namespace System.Net
{
	/// <summary>Contains HTTP proxy settings for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x0200065D RID: 1629
	[Serializable]
	public class WebProxy : IAutoWebProxy, IWebProxy, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.WebProxy" /> class.</summary>
		// Token: 0x06003362 RID: 13154 RVA: 0x000B3340 File Offset: 0x000B1540
		public WebProxy() : this(null, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class from the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</param>
		// Token: 0x06003363 RID: 13155 RVA: 0x000B334C File Offset: 0x000B154C
		public WebProxy(Uri Address) : this(Address, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the <see cref="T:System.Uri" /> instance and bypass setting.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		// Token: 0x06003364 RID: 13156 RVA: 0x000B3358 File Offset: 0x000B1558
		public WebProxy(Uri Address, bool BypassOnLocal) : this(Address, BypassOnLocal, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified <see cref="T:System.Uri" /> instance, bypass setting, and list of URIs to bypass.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass.</param>
		// Token: 0x06003365 RID: 13157 RVA: 0x000B3364 File Offset: 0x000B1564
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList) : this(Address, BypassOnLocal, BypassList, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified <see cref="T:System.Uri" /> instance, bypass setting, list of URIs to bypass, and credentials.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass.</param>
		/// <param name="Credentials">An <see cref="T:System.Net.ICredentials" /> instance to submit to the proxy server for authentication.</param>
		// Token: 0x06003366 RID: 13158 RVA: 0x000B3370 File Offset: 0x000B1570
		public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials)
		{
			this._ProxyAddress = Address;
			this._BypassOnLocal = BypassOnLocal;
			if (BypassList != null)
			{
				this._BypassList = new ArrayList(BypassList);
				this.UpdateRegExList(true);
			}
			this._Credentials = Credentials;
			this.m_EnableAutoproxy = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified host and port number.</summary>
		/// <param name="Host">The name of the proxy host.</param>
		/// <param name="Port">The port number on <paramref name="Host" /> to use.</param>
		/// <exception cref="T:System.UriFormatException">The URI formed by combining <paramref name="Host" /> and <paramref name="Port" /> is not a valid URI.</exception>
		// Token: 0x06003367 RID: 13159 RVA: 0x000B33AB File Offset: 0x000B15AB
		public WebProxy(string Host, int Port) : this(new Uri("http://" + Host + ":" + Port.ToString(CultureInfo.InvariantCulture)), false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI.</summary>
		/// <param name="Address">The URI of the proxy server.</param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI.</exception>
		// Token: 0x06003368 RID: 13160 RVA: 0x000B33D7 File Offset: 0x000B15D7
		public WebProxy(string Address) : this(WebProxy.CreateProxyUri(Address), false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI and bypass setting.</summary>
		/// <param name="Address">The URI of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI.</exception>
		// Token: 0x06003369 RID: 13161 RVA: 0x000B33E8 File Offset: 0x000B15E8
		public WebProxy(string Address, bool BypassOnLocal) : this(WebProxy.CreateProxyUri(Address), BypassOnLocal, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI, bypass setting, and list of URIs to bypass.</summary>
		/// <param name="Address">The URI of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <param name="BypassList">An array of regular expression strings that contain the URIs of the servers to bypass.</param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI.</exception>
		// Token: 0x0600336A RID: 13162 RVA: 0x000B33F9 File Offset: 0x000B15F9
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList) : this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI, bypass setting, list of URIs to bypass, and credentials.</summary>
		/// <param name="Address">The URI of the proxy server.</param>
		/// <param name="BypassOnLocal">
		///   <see langword="true" /> to bypass the proxy for local addresses; otherwise, <see langword="false" />.</param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass.</param>
		/// <param name="Credentials">An <see cref="T:System.Net.ICredentials" /> instance to submit to the proxy server for authentication.</param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI.</exception>
		// Token: 0x0600336B RID: 13163 RVA: 0x000B340A File Offset: 0x000B160A
		public WebProxy(string Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials) : this(WebProxy.CreateProxyUri(Address), BypassOnLocal, BypassList, Credentials)
		{
		}

		/// <summary>Gets or sets the address of the proxy server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</returns>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x000B341C File Offset: 0x000B161C
		// (set) Token: 0x0600336D RID: 13165 RVA: 0x000B3424 File Offset: 0x000B1624
		public Uri Address
		{
			get
			{
				return this._ProxyAddress;
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._ProxyHostAddresses = null;
				this._ProxyAddress = value;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (set) Token: 0x0600336E RID: 13166 RVA: 0x000B3441 File Offset: 0x000B1641
		internal bool AutoDetect
		{
			set
			{
				if (this.ScriptEngine == null)
				{
					this.ScriptEngine = new AutoWebProxyScriptEngine(this, false);
				}
				this.ScriptEngine.AutomaticallyDetectSettings = value;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x000B3464 File Offset: 0x000B1664
		internal Uri ScriptLocation
		{
			set
			{
				if (this.ScriptEngine == null)
				{
					this.ScriptEngine = new AutoWebProxyScriptEngine(this, false);
				}
				this.ScriptEngine.AutomaticConfigurationScript = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to bypass the proxy server for local addresses.</summary>
		/// <returns>
		///   <see langword="true" /> to bypass the proxy server for local addresses; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000B3487 File Offset: 0x000B1687
		// (set) Token: 0x06003371 RID: 13169 RVA: 0x000B348F File Offset: 0x000B168F
		public bool BypassProxyOnLocal
		{
			get
			{
				return this._BypassOnLocal;
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._BypassOnLocal = value;
			}
		}

		/// <summary>Gets or sets an array of addresses that do not use the proxy server.</summary>
		/// <returns>An array that contains a list of regular expressions that describe URIs that do not use the proxy server when accessed.</returns>
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x000B34A5 File Offset: 0x000B16A5
		// (set) Token: 0x06003373 RID: 13171 RVA: 0x000B34D4 File Offset: 0x000B16D4
		public string[] BypassList
		{
			get
			{
				if (this._BypassList == null)
				{
					this._BypassList = new ArrayList();
				}
				return (string[])this._BypassList.ToArray(typeof(string));
			}
			set
			{
				this._UseRegistry = false;
				this.DeleteScriptEngine();
				this._BypassList = new ArrayList(value);
				this.UpdateRegExList(true);
			}
		}

		/// <summary>Gets or sets the credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials to submit to the proxy server for authentication.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.UseDefaultCredentials" /> property was set to <see langword="true" />.</exception>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000B34F6 File Offset: 0x000B16F6
		// (set) Token: 0x06003375 RID: 13173 RVA: 0x000B34FE File Offset: 0x000B16FE
		public ICredentials Credentials
		{
			get
			{
				return this._Credentials;
			}
			set
			{
				this._Credentials = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.Credentials" /> property contains credentials other than the default credentials.</exception>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x000B3507 File Offset: 0x000B1707
		// (set) Token: 0x06003377 RID: 13175 RVA: 0x000B3519 File Offset: 0x000B1719
		public bool UseDefaultCredentials
		{
			get
			{
				return this.Credentials is SystemNetworkCredential;
			}
			set
			{
				this._Credentials = (value ? CredentialCache.DefaultCredentials : null);
			}
		}

		/// <summary>Gets a list of addresses that do not use the proxy server.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains a list of <see cref="P:System.Net.WebProxy.BypassList" /> arrays that represents URIs that do not use the proxy server when accessed.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000B352C File Offset: 0x000B172C
		public ArrayList BypassArrayList
		{
			get
			{
				if (this._BypassList == null)
				{
					this._BypassList = new ArrayList();
				}
				return this._BypassList;
			}
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000B3547 File Offset: 0x000B1747
		internal void CheckForChanges()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.CheckForChanges();
			}
		}

		/// <summary>Returns the proxied URI for a request.</summary>
		/// <param name="destination">The <see cref="T:System.Uri" /> instance of the requested Internet resource.</param>
		/// <returns>The <see cref="T:System.Uri" /> instance of the Internet resource, if the resource is on the bypass list; otherwise, the <see cref="T:System.Uri" /> instance of the proxy.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destination" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600337A RID: 13178 RVA: 0x000B355C File Offset: 0x000B175C
		public Uri GetProxy(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Uri result;
			if (this.GetProxyAuto(destination, out result))
			{
				return result;
			}
			if (this.IsBypassedManual(destination))
			{
				return destination;
			}
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			Uri uri = (proxyHostAddresses != null) ? (proxyHostAddresses[destination.Scheme] as Uri) : this._ProxyAddress;
			if (!(uri != null))
			{
				return destination;
			}
			return uri;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000B35C5 File Offset: 0x000B17C5
		private static Uri CreateProxyUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://") == -1)
			{
				address = "http://" + address;
			}
			return new Uri(address);
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000B35F0 File Offset: 0x000B17F0
		private void UpdateRegExList(bool canThrow)
		{
			Regex[] array = null;
			ArrayList bypassList = this._BypassList;
			try
			{
				if (bypassList != null && bypassList.Count > 0)
				{
					array = new Regex[bypassList.Count];
					for (int i = 0; i < bypassList.Count; i++)
					{
						array[i] = new Regex((string)bypassList[i], RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
					}
				}
			}
			catch
			{
				if (!canThrow)
				{
					this._RegExBypassList = null;
					return;
				}
				throw;
			}
			this._RegExBypassList = array;
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000B3670 File Offset: 0x000B1870
		private bool IsMatchInBypassList(Uri input)
		{
			this.UpdateRegExList(false);
			if (this._RegExBypassList == null)
			{
				return false;
			}
			string input2 = input.Scheme + "://" + input.Host + ((!input.IsDefaultPort) ? (":" + input.Port.ToString()) : "");
			for (int i = 0; i < this._BypassList.Count; i++)
			{
				if (this._RegExBypassList[i].IsMatch(input2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000B36F8 File Offset: 0x000B18F8
		private bool IsLocal(Uri host)
		{
			string host2 = host.Host;
			IPAddress ipaddress;
			if (IPAddress.TryParse(host2, out ipaddress))
			{
				return IPAddress.IsLoopback(ipaddress) || NclUtilities.IsAddressLocal(ipaddress);
			}
			int num = host2.IndexOf('.');
			if (num == -1)
			{
				return true;
			}
			string text = "." + IPGlobalProperties.InternalGetIPGlobalProperties().DomainName;
			return text != null && text.Length == host2.Length - num && string.Compare(text, 0, host2, num, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000B3774 File Offset: 0x000B1974
		private bool IsLocalInProxyHash(Uri host)
		{
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			return proxyHostAddresses != null && (Uri)proxyHostAddresses[host.Scheme] == null;
		}

		/// <summary>Indicates whether to use the proxy server for the specified host.</summary>
		/// <param name="host">The <see cref="T:System.Uri" /> instance of the host to check for proxy use.</param>
		/// <returns>
		///   <see langword="true" /> if the proxy server should not be used for <paramref name="host" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003380 RID: 13184 RVA: 0x000B37A8 File Offset: 0x000B19A8
		public bool IsBypassed(Uri host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			bool result;
			if (this.IsBypassedAuto(host, out result))
			{
				return result;
			}
			return this.IsBypassedManual(host);
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000B37E0 File Offset: 0x000B19E0
		private bool IsBypassedManual(Uri host)
		{
			return host.IsLoopback || (this._ProxyAddress == null && this._ProxyHostAddresses == null) || (this._BypassOnLocal && this.IsLocal(host)) || this.IsMatchInBypassList(host) || this.IsLocalInProxyHash(host);
		}

		/// <summary>Reads the Internet Explorer nondynamic proxy settings.</summary>
		/// <returns>A <see cref="T:System.Net.WebProxy" /> instance that contains the nondynamic proxy settings from Internet Explorer 5.5 and later.</returns>
		// Token: 0x06003382 RID: 13186 RVA: 0x000B3830 File Offset: 0x000B1A30
		[Obsolete("This method has been deprecated. Please use the proxy selected for you by default. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static WebProxy GetDefaultProxy()
		{
			return new WebProxy(true);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Net.WebProxy" /> class using previously serialized content.</summary>
		/// <param name="serializationInfo">The serialization data.</param>
		/// <param name="streamingContext">The context for the serialized data.</param>
		// Token: 0x06003383 RID: 13187 RVA: 0x000B3838 File Offset: 0x000B1A38
		protected WebProxy(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			bool flag = false;
			try
			{
				flag = serializationInfo.GetBoolean("_UseRegistry");
			}
			catch
			{
			}
			if (flag)
			{
				this.UnsafeUpdateFromRegistry();
				return;
			}
			this._ProxyAddress = (Uri)serializationInfo.GetValue("_ProxyAddress", typeof(Uri));
			this._BypassOnLocal = serializationInfo.GetBoolean("_BypassOnLocal");
			this._BypassList = (ArrayList)serializationInfo.GetValue("_BypassList", typeof(ArrayList));
			try
			{
				this.UseDefaultCredentials = serializationInfo.GetBoolean("_UseDefaultCredentials");
			}
			catch
			{
			}
		}

		/// <summary>Creates the serialization data and context that are used by the system to serialize a <see cref="T:System.Net.WebProxy" /> object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that indicates the destination for this serialization.</param>
		// Token: 0x06003384 RID: 13188 RVA: 0x000B38EC File Offset: 0x000B1AEC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06003385 RID: 13189 RVA: 0x000B38F8 File Offset: 0x000B1AF8
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("_BypassOnLocal", this._BypassOnLocal);
			serializationInfo.AddValue("_ProxyAddress", this._ProxyAddress);
			serializationInfo.AddValue("_BypassList", this._BypassList);
			serializationInfo.AddValue("_UseDefaultCredentials", this.UseDefaultCredentials);
			if (this._UseRegistry)
			{
				serializationInfo.AddValue("_UseRegistry", true);
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000B395D File Offset: 0x000B1B5D
		// (set) Token: 0x06003387 RID: 13191 RVA: 0x000B3965 File Offset: 0x000B1B65
		internal AutoWebProxyScriptEngine ScriptEngine
		{
			get
			{
				return this.m_ScriptEngine;
			}
			set
			{
				this.m_ScriptEngine = value;
			}
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000B3970 File Offset: 0x000B1B70
		public static IWebProxy CreateDefaultProxy()
		{
			if (Platform.IsMacOS)
			{
				IWebProxy defaultProxy = CFNetwork.GetDefaultProxy();
				if (defaultProxy != null)
				{
					return defaultProxy;
				}
			}
			return new WebProxy(true);
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000B3995 File Offset: 0x000B1B95
		internal WebProxy(bool enableAutoproxy)
		{
			this.m_EnableAutoproxy = enableAutoproxy;
			this.UnsafeUpdateFromRegistry();
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000B39AA File Offset: 0x000B1BAA
		internal void DeleteScriptEngine()
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Close();
				this.ScriptEngine = null;
			}
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000B39C8 File Offset: 0x000B1BC8
		internal void UnsafeUpdateFromRegistry()
		{
			this._UseRegistry = true;
			this.ScriptEngine = new AutoWebProxyScriptEngine(this, true);
			WebProxyData webProxyData = this.ScriptEngine.GetWebProxyData();
			this.Update(webProxyData);
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000B39FC File Offset: 0x000B1BFC
		internal void Update(WebProxyData webProxyData)
		{
			lock (this)
			{
				this._BypassOnLocal = webProxyData.bypassOnLocal;
				this._ProxyAddress = webProxyData.proxyAddress;
				this._ProxyHostAddresses = webProxyData.proxyHostAddresses;
				this._BypassList = webProxyData.bypassList;
				this.ScriptEngine.AutomaticallyDetectSettings = (this.m_EnableAutoproxy && webProxyData.automaticallyDetectSettings);
				this.ScriptEngine.AutomaticConfigurationScript = (this.m_EnableAutoproxy ? webProxyData.scriptLocation : null);
			}
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000B3A9C File Offset: 0x000B1C9C
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			return new ProxyScriptChain(this, destination);
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000B3ABC File Offset: 0x000B1CBC
		private bool GetProxyAuto(Uri destination, out Uri proxyUri)
		{
			proxyUri = null;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list = null;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count > 0)
			{
				if (WebProxy.AreAllBypassed(list, true))
				{
					proxyUri = destination;
				}
				else
				{
					proxyUri = WebProxy.ProxyUri(list[0]);
				}
			}
			return true;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000B3B10 File Offset: 0x000B1D10
		private bool IsBypassedAuto(Uri destination, out bool isBypassed)
		{
			isBypassed = true;
			if (this.ScriptEngine == null)
			{
				return false;
			}
			IList<string> list;
			if (!this.ScriptEngine.GetProxies(destination, out list))
			{
				return false;
			}
			if (list.Count == 0)
			{
				isBypassed = false;
			}
			else
			{
				isBypassed = WebProxy.AreAllBypassed(list, true);
			}
			return true;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000B3B54 File Offset: 0x000B1D54
		internal Uri[] GetProxiesAuto(Uri destination, ref int syncStatus)
		{
			if (this.ScriptEngine == null)
			{
				return null;
			}
			IList<string> list = null;
			if (!this.ScriptEngine.GetProxies(destination, out list, ref syncStatus))
			{
				return null;
			}
			Uri[] array;
			if (list.Count == 0)
			{
				array = new Uri[0];
			}
			else if (WebProxy.AreAllBypassed(list, false))
			{
				array = new Uri[1];
			}
			else
			{
				array = new Uri[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					array[i] = WebProxy.ProxyUri(list[i]);
				}
			}
			return array;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000B3BD2 File Offset: 0x000B1DD2
		internal void AbortGetProxiesAuto(ref int syncStatus)
		{
			if (this.ScriptEngine != null)
			{
				this.ScriptEngine.Abort(ref syncStatus);
			}
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000B3BE8 File Offset: 0x000B1DE8
		internal Uri GetProxyAutoFailover(Uri destination)
		{
			if (this.IsBypassedManual(destination))
			{
				return null;
			}
			Uri result = this._ProxyAddress;
			Hashtable proxyHostAddresses = this._ProxyHostAddresses;
			if (proxyHostAddresses != null)
			{
				result = (proxyHostAddresses[destination.Scheme] as Uri);
			}
			return result;
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000B3C24 File Offset: 0x000B1E24
		private static bool AreAllBypassed(IEnumerable<string> proxies, bool checkFirstOnly)
		{
			bool flag = true;
			foreach (string value in proxies)
			{
				flag = string.IsNullOrEmpty(value);
				if (checkFirstOnly)
				{
					break;
				}
				if (!flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000B3C78 File Offset: 0x000B1E78
		private static Uri ProxyUri(string proxyName)
		{
			if (proxyName != null && proxyName.Length != 0)
			{
				return new Uri("http://" + proxyName);
			}
			return null;
		}

		// Token: 0x04001E20 RID: 7712
		private bool _UseRegistry;

		// Token: 0x04001E21 RID: 7713
		private bool _BypassOnLocal;

		// Token: 0x04001E22 RID: 7714
		private bool m_EnableAutoproxy;

		// Token: 0x04001E23 RID: 7715
		private Uri _ProxyAddress;

		// Token: 0x04001E24 RID: 7716
		private ArrayList _BypassList;

		// Token: 0x04001E25 RID: 7717
		private ICredentials _Credentials;

		// Token: 0x04001E26 RID: 7718
		private Regex[] _RegExBypassList;

		// Token: 0x04001E27 RID: 7719
		private Hashtable _ProxyHostAddresses;

		// Token: 0x04001E28 RID: 7720
		private AutoWebProxyScriptEngine m_ScriptEngine;
	}
}
