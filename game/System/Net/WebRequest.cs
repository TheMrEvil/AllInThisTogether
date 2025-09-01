using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net.Cache;
using System.Net.Configuration;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	/// <summary>Makes a request to a Uniform Resource Identifier (URI). This is an <see langword="abstract" /> class.</summary>
	// Token: 0x02000612 RID: 1554
	[Serializable]
	public abstract class WebRequest : MarshalByRefObject, ISerializable
	{
		/// <summary>When overridden in a descendant class, gets the factory object derived from the <see cref="T:System.Net.IWebRequestCreate" /> class used to create the <see cref="T:System.Net.WebRequest" /> instantiated for making the request to the specified URI.</summary>
		/// <returns>The derived <see cref="T:System.Net.WebRequest" /> type returned by the <see cref="M:System.Net.IWebRequestCreate.Create(System.Uri)" /> method.</returns>
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000AB1F1 File Offset: 0x000A93F1
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual IWebRequestCreate CreatorInstance
		{
			get
			{
				return WebRequest.webRequestCreate;
			}
		}

		/// <summary>Register an <see cref="T:System.Net.IWebRequestCreate" /> object.</summary>
		/// <param name="creator">The <see cref="T:System.Net.IWebRequestCreate" /> object to register.</param>
		// Token: 0x06003142 RID: 12610 RVA: 0x00003917 File Offset: 0x00001B17
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public static void RegisterPortableWebRequestCreator(IWebRequestCreate creator)
		{
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000AB1F8 File Offset: 0x000A93F8
		private static object InternalSyncObject
		{
			get
			{
				if (WebRequest.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref WebRequest.s_InternalSyncObject, value, null);
				}
				return WebRequest.s_InternalSyncObject;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000AB224 File Offset: 0x000A9424
		internal static TimerThread.Queue DefaultTimerQueue
		{
			get
			{
				return WebRequest.s_DefaultTimerQueue;
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000AB22C File Offset: 0x000A942C
		private static WebRequest Create(Uri requestUri, bool useUriBase)
		{
			bool on = Logging.On;
			WebRequestPrefixElement webRequestPrefixElement = null;
			bool flag = false;
			string text;
			if (!useUriBase)
			{
				text = requestUri.AbsoluteUri;
			}
			else
			{
				text = requestUri.Scheme + ":";
			}
			int length = text.Length;
			ArrayList prefixList = WebRequest.PrefixList;
			for (int i = 0; i < prefixList.Count; i++)
			{
				webRequestPrefixElement = (WebRequestPrefixElement)prefixList[i];
				if (length >= webRequestPrefixElement.Prefix.Length && string.Compare(webRequestPrefixElement.Prefix, 0, text, 0, webRequestPrefixElement.Prefix.Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				WebRequest result = webRequestPrefixElement.Creator.Create(requestUri);
				bool on2 = Logging.On;
				return result;
			}
			bool on3 = Logging.On;
			throw new NotSupportedException(SR.GetString("The URI prefix is not recognized."));
		}

		/// <summary>Initializes a new <see cref="T:System.Net.WebRequest" /> instance for the specified URI scheme.</summary>
		/// <param name="requestUriString">The URI that identifies the Internet resource.</param>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> descendant for the specific URI scheme.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUriString" /> has not been registered.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUriString" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		/// <exception cref="T:System.UriFormatException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.  
		///
		///
		///
		///
		///  The URI specified in <paramref name="requestUriString" /> is not a valid URI.</exception>
		// Token: 0x06003146 RID: 12614 RVA: 0x000AB2EC File Offset: 0x000A94EC
		public static WebRequest Create(string requestUriString)
		{
			if (requestUriString == null)
			{
				throw new ArgumentNullException("requestUriString");
			}
			return WebRequest.Create(new Uri(requestUriString), false);
		}

		/// <summary>Initializes a new <see cref="T:System.Net.WebRequest" /> instance for the specified URI scheme.</summary>
		/// <param name="requestUri">A <see cref="T:System.Uri" /> containing the URI of the requested resource.</param>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> descendant for the specified URI scheme.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUri" /> is not registered.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		// Token: 0x06003147 RID: 12615 RVA: 0x000AB308 File Offset: 0x000A9508
		public static WebRequest Create(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			return WebRequest.Create(requestUri, false);
		}

		/// <summary>Initializes a new <see cref="T:System.Net.WebRequest" /> instance for the specified URI scheme.</summary>
		/// <param name="requestUri">A <see cref="T:System.Uri" /> containing the URI of the requested resource.</param>
		/// <returns>A <see cref="T:System.Net.WebRequest" /> descendant for the specified URI scheme.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUri" /> is not registered.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		// Token: 0x06003148 RID: 12616 RVA: 0x000AB325 File Offset: 0x000A9525
		public static WebRequest CreateDefault(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			return WebRequest.Create(requestUri, true);
		}

		/// <summary>Initializes a new <see cref="T:System.Net.HttpWebRequest" /> instance for the specified URI string.</summary>
		/// <param name="requestUriString">A URI string that identifies the Internet resource.</param>
		/// <returns>An <see cref="T:System.Net.HttpWebRequest" /> instance for the specific URI string.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUriString" /> is the http or https scheme.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUriString" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="requestUriString" /> is not a valid URI.</exception>
		// Token: 0x06003149 RID: 12617 RVA: 0x000AB342 File Offset: 0x000A9542
		public static HttpWebRequest CreateHttp(string requestUriString)
		{
			if (requestUriString == null)
			{
				throw new ArgumentNullException("requestUriString");
			}
			return WebRequest.CreateHttp(new Uri(requestUriString));
		}

		/// <summary>Initializes a new <see cref="T:System.Net.HttpWebRequest" /> instance for the specified URI.</summary>
		/// <param name="requestUri">A URI that identifies the Internet resource.</param>
		/// <returns>An <see cref="T:System.Net.HttpWebRequest" /> instance for the specific URI string.</returns>
		/// <exception cref="T:System.NotSupportedException">The request scheme specified in <paramref name="requestUri" /> is the http or https scheme.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="requestUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have <see cref="T:System.Net.WebPermissionAttribute" /> permission to connect to the requested URI or a URI that the request is redirected to.</exception>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="requestUri" /> is not a valid URI.</exception>
		// Token: 0x0600314A RID: 12618 RVA: 0x000AB360 File Offset: 0x000A9560
		public static HttpWebRequest CreateHttp(Uri requestUri)
		{
			if (requestUri == null)
			{
				throw new ArgumentNullException("requestUri");
			}
			if (requestUri.Scheme != Uri.UriSchemeHttp && requestUri.Scheme != Uri.UriSchemeHttps)
			{
				throw new NotSupportedException(SR.GetString("The URI prefix is not recognized."));
			}
			return (HttpWebRequest)WebRequest.CreateDefault(requestUri);
		}

		/// <summary>Registers a <see cref="T:System.Net.WebRequest" /> descendant for the specified URI.</summary>
		/// <param name="prefix">The complete URI or URI prefix that the <see cref="T:System.Net.WebRequest" /> descendant services.</param>
		/// <param name="creator">The create method that the <see cref="T:System.Net.WebRequest" /> calls to create the <see cref="T:System.Net.WebRequest" /> descendant.</param>
		/// <returns>
		///   <see langword="true" /> if registration is successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="prefix" /> is <see langword="null" />  
		/// -or-  
		/// <paramref name="creator" /> is <see langword="null" />.</exception>
		// Token: 0x0600314B RID: 12619 RVA: 0x000AB3C0 File Offset: 0x000A95C0
		public static bool RegisterPrefix(string prefix, IWebRequestCreate creator)
		{
			bool flag = false;
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (creator == null)
			{
				throw new ArgumentNullException("creator");
			}
			object internalSyncObject = WebRequest.InternalSyncObject;
			lock (internalSyncObject)
			{
				ArrayList arrayList = (ArrayList)WebRequest.PrefixList.Clone();
				Uri uri;
				if (Uri.TryCreate(prefix, UriKind.Absolute, out uri))
				{
					string text = uri.AbsoluteUri;
					if (!prefix.EndsWith("/", StringComparison.Ordinal) && uri.GetComponents(UriComponents.Path | UriComponents.Query | UriComponents.Fragment, UriFormat.UriEscaped).Equals("/"))
					{
						text = text.Substring(0, text.Length - 1);
					}
					prefix = text;
				}
				int i;
				for (i = 0; i < arrayList.Count; i++)
				{
					WebRequestPrefixElement webRequestPrefixElement = (WebRequestPrefixElement)arrayList[i];
					if (prefix.Length > webRequestPrefixElement.Prefix.Length)
					{
						break;
					}
					if (prefix.Length == webRequestPrefixElement.Prefix.Length && string.Compare(webRequestPrefixElement.Prefix, prefix, StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					arrayList.Insert(i, new WebRequestPrefixElement(prefix, creator));
					WebRequest.PrefixList = arrayList;
				}
			}
			return !flag;
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000AB4F0 File Offset: 0x000A96F0
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x000AB550 File Offset: 0x000A9750
		internal static ArrayList PrefixList
		{
			get
			{
				if (WebRequest.s_PrefixList == null)
				{
					object internalSyncObject = WebRequest.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (WebRequest.s_PrefixList == null)
						{
							WebRequest.s_PrefixList = WebRequest.PopulatePrefixList();
						}
					}
				}
				return WebRequest.s_PrefixList;
			}
			set
			{
				WebRequest.s_PrefixList = value;
			}
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000AB55C File Offset: 0x000A975C
		private static ArrayList PopulatePrefixList()
		{
			ArrayList arrayList = new ArrayList();
			WebRequestModulesSection webRequestModulesSection = ConfigurationManager.GetSection("system.net/webRequestModules") as WebRequestModulesSection;
			if (webRequestModulesSection != null)
			{
				foreach (object obj in webRequestModulesSection.WebRequestModules)
				{
					WebRequestModuleElement webRequestModuleElement = (WebRequestModuleElement)obj;
					arrayList.Add(new WebRequestPrefixElement(webRequestModuleElement.Prefix, webRequestModuleElement.Type));
				}
			}
			return arrayList;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebRequest" /> class.</summary>
		// Token: 0x0600314F RID: 12623 RVA: 0x000AB5E4 File Offset: 0x000A97E4
		protected WebRequest()
		{
			this.m_ImpersonationLevel = TokenImpersonationLevel.Delegation;
			this.m_AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.WebRequest" /> instance.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the source of the serialized stream associated with the new <see cref="T:System.Net.WebRequest" /> instance.</param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the constructor, when the constructor is not overridden in a descendant class.</exception>
		// Token: 0x06003150 RID: 12624 RVA: 0x0002D758 File Offset: 0x0002B958
		protected WebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>When overridden in a descendant class, populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.WebRequest" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" />, which holds the serialized data for the <see cref="T:System.Net.WebRequest" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream associated with the new <see cref="T:System.Net.WebRequest" />.</param>
		/// <exception cref="T:System.NotImplementedException">An attempt is made to serialize the object, when the interface is not overridden in a descendant class.</exception>
		// Token: 0x06003151 RID: 12625 RVA: 0x000AB5FA File Offset: 0x000A97FA
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06003152 RID: 12626 RVA: 0x00003917 File Offset: 0x00001B17
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
		}

		/// <summary>Gets or sets the default cache policy for this request.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> that specifies the cache policy in effect for this request when no other policy is applicable.</returns>
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000AB604 File Offset: 0x000A9804
		// (set) Token: 0x06003154 RID: 12628 RVA: 0x000AB618 File Offset: 0x000A9818
		public static RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return RequestCacheManager.GetBinding(string.Empty).Policy;
			}
			set
			{
				RequestCacheBinding binding = RequestCacheManager.GetBinding(string.Empty);
				RequestCacheManager.SetBinding(string.Empty, new RequestCacheBinding(binding.Cache, binding.Validator, value));
			}
		}

		/// <summary>Gets or sets the cache policy for this request.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> object that defines a cache policy.</returns>
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x000AB64C File Offset: 0x000A984C
		// (set) Token: 0x06003156 RID: 12630 RVA: 0x000AB654 File Offset: 0x000A9854
		public virtual RequestCachePolicy CachePolicy
		{
			get
			{
				return this.m_CachePolicy;
			}
			set
			{
				this.InternalSetCachePolicy(value);
			}
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000AB660 File Offset: 0x000A9860
		private void InternalSetCachePolicy(RequestCachePolicy policy)
		{
			if (this.m_CacheBinding != null && this.m_CacheBinding.Cache != null && this.m_CacheBinding.Validator != null && this.CacheProtocol == null && policy != null && policy.Level != RequestCacheLevel.BypassCache)
			{
				this.CacheProtocol = new RequestCacheProtocol(this.m_CacheBinding.Cache, this.m_CacheBinding.Validator.CreateValidator());
			}
			this.m_CachePolicy = policy;
		}

		/// <summary>When overridden in a descendant class, gets or sets the protocol method to use in this request.</summary>
		/// <returns>The protocol method to use in this request.</returns>
		/// <exception cref="T:System.NotImplementedException">If the property is not overridden in a descendant class, any attempt is made to get or set the property.</exception>
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x06003159 RID: 12633 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual string Method
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets the URI of the Internet resource associated with the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the resource associated with the request</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual Uri RequestUri
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the name of the connection group for the request.</summary>
		/// <returns>The name of the connection group for the request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x0600315C RID: 12636 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual string ConnectionGroupName
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the collection of header name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing the header name/value pairs associated with this request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x0600315E RID: 12638 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual WebHeaderCollection Headers
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the content length of the request data being sent.</summary>
		/// <returns>The number of bytes of request data being sent.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x06003160 RID: 12640 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual long ContentLength
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the content type of the request data being sent.</summary>
		/// <returns>The content type of the request data.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x06003162 RID: 12642 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual string ContentType
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the network credentials used for authenticating the request with the Internet resource.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> containing the authentication credentials associated with the request. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x06003164 RID: 12644 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual ICredentials Credentials
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets a <see cref="T:System.Boolean" /> value that controls whether <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property after the request was sent.</exception>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x06003166 RID: 12646 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual bool UseDefaultCredentials
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, gets or sets the network proxy to use to access this Internet resource.</summary>
		/// <returns>The <see cref="T:System.Net.IWebProxy" /> to use to access the Internet resource.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x06003168 RID: 12648 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual IWebProxy Proxy
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, indicates whether to pre-authenticate the request.</summary>
		/// <returns>
		///   <see langword="true" /> to pre-authenticate; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x0600316A RID: 12650 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual bool PreAuthenticate
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>Gets or sets the length of time, in milliseconds, before the request times out.</summary>
		/// <returns>The length of time, in milliseconds, until the request times out, or the value <see cref="F:System.Threading.Timeout.Infinite" /> to indicate that the request does not time out. The default value is defined by the descendant class.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to get or set the property, when the property is not overridden in a descendant class.</exception>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000A5C43 File Offset: 0x000A3E43
		// (set) Token: 0x0600316C RID: 12652 RVA: 0x000A5C43 File Offset: 0x000A3E43
		public virtual int Timeout
		{
			get
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotImplementedException;
			}
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.IO.Stream" /> for writing data to the Internet resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for writing data to the Internet resource.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x0600316D RID: 12653 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual Stream GetRequestStream()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, returns a response to an Internet request.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response to the Internet request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x0600316E RID: 12654 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual WebResponse GetResponse()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, begins an asynchronous request for an Internet resource.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object containing state information for this asynchronous request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x0600316F RID: 12655 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.Net.WebResponse" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references a pending request for a response.</param>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains a response to the Internet request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06003170 RID: 12656 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, provides an asynchronous version of the <see cref="M:System.Net.WebRequest.GetRequestStream" /> method.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object containing state information for this asynchronous request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06003171 RID: 12657 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.IO.Stream" /> for writing data to the Internet resource.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references a pending request for a stream.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> to write data to.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06003172 RID: 12658 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>When overridden in a descendant class, returns a <see cref="T:System.IO.Stream" /> for writing data to the Internet resource as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06003173 RID: 12659 RVA: 0x000AB6D0 File Offset: 0x000A98D0
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<Stream> GetRequestStreamAsync()
		{
			IWebProxy webProxy = null;
			try
			{
				webProxy = this.Proxy;
			}
			catch (NotImplementedException)
			{
			}
			if (ExecutionContext.IsFlowSuppressed() && (this.UseDefaultCredentials || this.Credentials != null || (webProxy != null && webProxy.Credentials != null)))
			{
				WindowsIdentity currentUser = this.SafeCaptureIdenity();
				return Task.Run<Stream>(delegate()
				{
					Task<Stream> result;
					using (WindowsIdentity currentUser = currentUser)
					{
						using (currentUser.Impersonate())
						{
							result = Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this.EndGetRequestStream), null);
						}
					}
					return result;
				});
			}
			return Task.Run<Stream>(() => Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this.EndGetRequestStream), null));
		}

		/// <summary>When overridden in a descendant class, returns a response to an Internet request as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06003174 RID: 12660 RVA: 0x000AB758 File Offset: 0x000A9958
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<WebResponse> GetResponseAsync()
		{
			IWebProxy webProxy = null;
			try
			{
				webProxy = this.Proxy;
			}
			catch (NotImplementedException)
			{
			}
			if (ExecutionContext.IsFlowSuppressed() && (this.UseDefaultCredentials || this.Credentials != null || (webProxy != null && webProxy.Credentials != null)))
			{
				WindowsIdentity currentUser = this.SafeCaptureIdenity();
				return Task.Run<WebResponse>(delegate()
				{
					Task<WebResponse> result;
					using (WindowsIdentity currentUser = currentUser)
					{
						using (currentUser.Impersonate())
						{
							result = Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.EndGetResponse), null);
						}
					}
					return result;
				});
			}
			return Task.Run<WebResponse>(() => Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.EndGetResponse), null));
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000AB7E0 File Offset: 0x000A99E0
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private WindowsIdentity SafeCaptureIdenity()
		{
			return WindowsIdentity.GetCurrent();
		}

		/// <summary>Aborts the request.</summary>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method, when the method is not overridden in a descendant class.</exception>
		// Token: 0x06003176 RID: 12662 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual void Abort()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x000AB7E7 File Offset: 0x000A99E7
		// (set) Token: 0x06003178 RID: 12664 RVA: 0x000AB7EF File Offset: 0x000A99EF
		internal RequestCacheProtocol CacheProtocol
		{
			get
			{
				return this.m_CacheProtocol;
			}
			set
			{
				this.m_CacheProtocol = value;
			}
		}

		/// <summary>Gets or sets values indicating the level of authentication and impersonation used for this request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Net.Security.AuthenticationLevel" /> values. The default value is <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequested" />.  
		///  In mutual authentication, both the client and server present credentials to establish their identity. The <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequired" /> and <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequested" /> values are relevant for Kerberos authentication. Kerberos authentication can be supported directly, or can be used if the Negotiate security protocol is used to select the actual security protocol. For more information about authentication protocols, see Internet Authentication.  
		///  To determine whether mutual authentication occurred, check the <see cref="P:System.Net.WebResponse.IsMutuallyAuthenticated" /> property.  
		///  If you specify the <see cref="F:System.Net.Security.AuthenticationLevel.MutualAuthRequired" /> authentication flag value and mutual authentication does not occur, your application will receive an <see cref="T:System.IO.IOException" /> with a <see cref="T:System.Net.ProtocolViolationException" /> inner exception indicating that mutual authentication failed.</returns>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x000AB7F8 File Offset: 0x000A99F8
		// (set) Token: 0x0600317A RID: 12666 RVA: 0x000AB800 File Offset: 0x000A9A00
		public AuthenticationLevel AuthenticationLevel
		{
			get
			{
				return this.m_AuthenticationLevel;
			}
			set
			{
				this.m_AuthenticationLevel = value;
			}
		}

		/// <summary>Gets or sets the impersonation level for the current request.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.</returns>
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000AB809 File Offset: 0x000A9A09
		// (set) Token: 0x0600317C RID: 12668 RVA: 0x000AB811 File Offset: 0x000A9A11
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.m_ImpersonationLevel;
			}
			set
			{
				this.m_ImpersonationLevel = value;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x000AB81C File Offset: 0x000A9A1C
		// (set) Token: 0x0600317E RID: 12670 RVA: 0x000AB88C File Offset: 0x000A9A8C
		internal static IWebProxy InternalDefaultWebProxy
		{
			get
			{
				if (!WebRequest.s_DefaultWebProxyInitialized)
				{
					object internalSyncObject = WebRequest.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (!WebRequest.s_DefaultWebProxyInitialized)
						{
							DefaultProxySectionInternal section = DefaultProxySectionInternal.GetSection();
							if (section != null)
							{
								WebRequest.s_DefaultWebProxy = section.WebProxy;
							}
							WebRequest.s_DefaultWebProxyInitialized = true;
						}
					}
				}
				return WebRequest.s_DefaultWebProxy;
			}
			set
			{
				if (!WebRequest.s_DefaultWebProxyInitialized)
				{
					object internalSyncObject = WebRequest.InternalSyncObject;
					lock (internalSyncObject)
					{
						WebRequest.s_DefaultWebProxy = value;
						WebRequest.s_DefaultWebProxyInitialized = true;
						return;
					}
				}
				WebRequest.s_DefaultWebProxy = value;
			}
		}

		/// <summary>Gets or sets the global HTTP proxy.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> used by every call to instances of <see cref="T:System.Net.WebRequest" />.</returns>
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000AB8E8 File Offset: 0x000A9AE8
		// (set) Token: 0x06003180 RID: 12672 RVA: 0x000AB8EF File Offset: 0x000A9AEF
		public static IWebProxy DefaultWebProxy
		{
			get
			{
				return WebRequest.InternalDefaultWebProxy;
			}
			set
			{
				WebRequest.InternalDefaultWebProxy = value;
			}
		}

		/// <summary>Returns a proxy configured with the Internet Explorer settings of the currently impersonated user.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> used by every call to instances of <see cref="T:System.Net.WebRequest" />.</returns>
		// Token: 0x06003181 RID: 12673 RVA: 0x000AB8F7 File Offset: 0x000A9AF7
		public static IWebProxy GetSystemWebProxy()
		{
			return WebRequest.InternalGetSystemWebProxy();
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000AB8FE File Offset: 0x000A9AFE
		internal static IWebProxy InternalGetSystemWebProxy()
		{
			return WebProxy.CreateDefaultProxy();
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000AB905 File Offset: 0x000A9B05
		internal void SetupCacheProtocol(Uri uri)
		{
			this.m_CacheBinding = RequestCacheManager.GetBinding(uri.Scheme);
			this.InternalSetCachePolicy(this.m_CacheBinding.Policy);
			if (this.m_CachePolicy == null)
			{
				this.InternalSetCachePolicy(WebRequest.DefaultCachePolicy);
			}
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000AB93C File Offset: 0x000A9B3C
		// Note: this type is marked as 'beforefieldinit'.
		static WebRequest()
		{
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000AB957 File Offset: 0x000A9B57
		[CompilerGenerated]
		private Task<Stream> <GetRequestStreamAsync>b__78_0()
		{
			return Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this.EndGetRequestStream), null);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000AB97E File Offset: 0x000A9B7E
		[CompilerGenerated]
		private Task<WebResponse> <GetResponseAsync>b__79_0()
		{
			return Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.EndGetResponse), null);
		}

		// Token: 0x04001CA1 RID: 7329
		internal const int DefaultTimeout = 100000;

		// Token: 0x04001CA2 RID: 7330
		private static volatile ArrayList s_PrefixList;

		// Token: 0x04001CA3 RID: 7331
		private static object s_InternalSyncObject;

		// Token: 0x04001CA4 RID: 7332
		private static TimerThread.Queue s_DefaultTimerQueue = TimerThread.CreateQueue(100000);

		// Token: 0x04001CA5 RID: 7333
		private AuthenticationLevel m_AuthenticationLevel;

		// Token: 0x04001CA6 RID: 7334
		private TokenImpersonationLevel m_ImpersonationLevel;

		// Token: 0x04001CA7 RID: 7335
		private RequestCachePolicy m_CachePolicy;

		// Token: 0x04001CA8 RID: 7336
		private RequestCacheProtocol m_CacheProtocol;

		// Token: 0x04001CA9 RID: 7337
		private RequestCacheBinding m_CacheBinding;

		// Token: 0x04001CAA RID: 7338
		private static WebRequest.DesignerWebRequestCreate webRequestCreate = new WebRequest.DesignerWebRequestCreate();

		// Token: 0x04001CAB RID: 7339
		private static volatile IWebProxy s_DefaultWebProxy;

		// Token: 0x04001CAC RID: 7340
		private static volatile bool s_DefaultWebProxyInitialized;

		// Token: 0x02000613 RID: 1555
		internal class DesignerWebRequestCreate : IWebRequestCreate
		{
			// Token: 0x06003187 RID: 12679 RVA: 0x000AB9A5 File Offset: 0x000A9BA5
			public WebRequest Create(Uri uri)
			{
				return WebRequest.Create(uri);
			}

			// Token: 0x06003188 RID: 12680 RVA: 0x0000219B File Offset: 0x0000039B
			public DesignerWebRequestCreate()
			{
			}
		}

		// Token: 0x02000614 RID: 1556
		internal class WebProxyWrapperOpaque : IAutoWebProxy, IWebProxy
		{
			// Token: 0x06003189 RID: 12681 RVA: 0x000AB9AD File Offset: 0x000A9BAD
			internal WebProxyWrapperOpaque(WebProxy webProxy)
			{
				this.webProxy = webProxy;
			}

			// Token: 0x0600318A RID: 12682 RVA: 0x000AB9BC File Offset: 0x000A9BBC
			public Uri GetProxy(Uri destination)
			{
				return this.webProxy.GetProxy(destination);
			}

			// Token: 0x0600318B RID: 12683 RVA: 0x000AB9CA File Offset: 0x000A9BCA
			public bool IsBypassed(Uri host)
			{
				return this.webProxy.IsBypassed(host);
			}

			// Token: 0x170009F3 RID: 2547
			// (get) Token: 0x0600318C RID: 12684 RVA: 0x000AB9D8 File Offset: 0x000A9BD8
			// (set) Token: 0x0600318D RID: 12685 RVA: 0x000AB9E5 File Offset: 0x000A9BE5
			public ICredentials Credentials
			{
				get
				{
					return this.webProxy.Credentials;
				}
				set
				{
					this.webProxy.Credentials = value;
				}
			}

			// Token: 0x0600318E RID: 12686 RVA: 0x000AB9F3 File Offset: 0x000A9BF3
			public ProxyChain GetProxies(Uri destination)
			{
				return ((IAutoWebProxy)this.webProxy).GetProxies(destination);
			}

			// Token: 0x04001CAD RID: 7341
			protected readonly WebProxy webProxy;
		}

		// Token: 0x02000615 RID: 1557
		internal class WebProxyWrapper : WebRequest.WebProxyWrapperOpaque
		{
			// Token: 0x0600318F RID: 12687 RVA: 0x000ABA01 File Offset: 0x000A9C01
			internal WebProxyWrapper(WebProxy webProxy) : base(webProxy)
			{
			}

			// Token: 0x170009F4 RID: 2548
			// (get) Token: 0x06003190 RID: 12688 RVA: 0x000ABA0A File Offset: 0x000A9C0A
			internal WebProxy WebProxy
			{
				get
				{
					return this.webProxy;
				}
			}
		}

		// Token: 0x02000616 RID: 1558
		[CompilerGenerated]
		private sealed class <>c__DisplayClass78_0
		{
			// Token: 0x06003191 RID: 12689 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass78_0()
			{
			}

			// Token: 0x06003192 RID: 12690 RVA: 0x000ABA14 File Offset: 0x000A9C14
			internal Task<Stream> <GetRequestStreamAsync>b__1()
			{
				Task<Stream> result;
				using (this.currentUser)
				{
					using (this.currentUser.Impersonate())
					{
						result = Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.<>4__this.BeginGetRequestStream), new Func<IAsyncResult, Stream>(this.<>4__this.EndGetRequestStream), null);
					}
				}
				return result;
			}

			// Token: 0x04001CAE RID: 7342
			public WindowsIdentity currentUser;

			// Token: 0x04001CAF RID: 7343
			public WebRequest <>4__this;
		}

		// Token: 0x02000617 RID: 1559
		[CompilerGenerated]
		private sealed class <>c__DisplayClass79_0
		{
			// Token: 0x06003193 RID: 12691 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass79_0()
			{
			}

			// Token: 0x06003194 RID: 12692 RVA: 0x000ABA98 File Offset: 0x000A9C98
			internal Task<WebResponse> <GetResponseAsync>b__1()
			{
				Task<WebResponse> result;
				using (this.currentUser)
				{
					using (this.currentUser.Impersonate())
					{
						result = Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.<>4__this.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.<>4__this.EndGetResponse), null);
					}
				}
				return result;
			}

			// Token: 0x04001CB0 RID: 7344
			public WindowsIdentity currentUser;

			// Token: 0x04001CB1 RID: 7345
			public WebRequest <>4__this;
		}
	}
}
