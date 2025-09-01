using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml
{
	/// <summary>Resolves external XML resources named by a Uniform Resource Identifier (URI).</summary>
	// Token: 0x02000248 RID: 584
	public class XmlUrlResolver : XmlResolver
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00084C14 File Offset: 0x00082E14
		private static XmlDownloadManager DownloadManager
		{
			get
			{
				if (XmlUrlResolver.s_DownloadManager == null)
				{
					object value = new XmlDownloadManager();
					Interlocked.CompareExchange<object>(ref XmlUrlResolver.s_DownloadManager, value, null);
				}
				return (XmlDownloadManager)XmlUrlResolver.s_DownloadManager;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlUrlResolver" /> class.</summary>
		// Token: 0x060015B6 RID: 5558 RVA: 0x000021F2 File Offset: 0x000003F2
		public XmlUrlResolver()
		{
		}

		/// <summary>Sets credentials used to authenticate web requests.</summary>
		/// <returns>The credentials to be used to authenticate web requests. If this property is not set, the value defaults to <see langword="null" />; that is, the <see langword="XmlUrlResolver" /> has no user credentials.</returns>
		// Token: 0x170003D9 RID: 985
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x00084C45 File Offset: 0x00082E45
		public override ICredentials Credentials
		{
			set
			{
				this._credentials = value;
			}
		}

		/// <summary>Gets or sets the network proxy for the underlying <see cref="T:System.Net.WebRequest" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.IWebProxy" /> to use to access the Internet resource.</returns>
		// Token: 0x170003DA RID: 986
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x00084C4E File Offset: 0x00082E4E
		public IWebProxy Proxy
		{
			set
			{
				this._proxy = value;
			}
		}

		/// <summary>Gets or sets the cache policy for the underlying <see cref="T:System.Net.WebRequest" /> object.</summary>
		/// <returns>The cache policy for the underlying web request.</returns>
		// Token: 0x170003DB RID: 987
		// (set) Token: 0x060015B9 RID: 5561 RVA: 0x00084C57 File Offset: 0x00082E57
		public RequestCachePolicy CachePolicy
		{
			set
			{
				this._cachePolicy = value;
			}
		}

		/// <summary>Maps a URI to an object that contains the actual resource.</summary>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">Currently not used.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns <see cref="T:System.IO.Stream" /> objects.</param>
		/// <returns>A stream object or <see langword="null" /> if a type other than stream is specified.</returns>
		/// <exception cref="T:System.Xml.XmlException">
		///         <paramref name="ofObjectToReturn" /> is neither <see langword="null" /> nor a <see langword="Stream" /> type.</exception>
		/// <exception cref="T:System.UriFormatException">The specified URI is not an absolute URI.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="absoluteUri" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Exception">There is a runtime error (for example, an interrupted server connection).</exception>
		// Token: 0x060015BA RID: 5562 RVA: 0x00084C60 File Offset: 0x00082E60
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
			{
				return XmlUrlResolver.DownloadManager.GetStream(absoluteUri, this._credentials, this._proxy, this._cachePolicy);
			}
			throw new XmlException("Object type is not supported.", string.Empty);
		}

		/// <summary>Resolves the absolute URI from the base and relative URIs.</summary>
		/// <param name="baseUri">The base URI used to resolve the relative URI.</param>
		/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative. If absolute, this value effectively replaces the <paramref name="baseUri" /> value. If relative, it combines with the <paramref name="baseUri" /> to make an absolute URI.</param>
		/// <returns>The absolute URI, or <see langword="null" /> if the relative URI cannot be resolved.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="baseUri" /> is <see langword="null" /> or <paramref name="relativeUri" /> is <see langword="null" />.</exception>
		// Token: 0x060015BB RID: 5563 RVA: 0x00084CC7 File Offset: 0x00082EC7
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public override Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			return base.ResolveUri(baseUri, relativeUri);
		}

		/// <summary>Asynchronously maps a URI to an object that contains the actual resource.</summary>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">Currently not used.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns <see cref="T:System.IO.Stream" /> objects.</param>
		/// <returns>A stream object or <see langword="null" /> if a type other than stream is specified.</returns>
		// Token: 0x060015BC RID: 5564 RVA: 0x00084CD4 File Offset: 0x00082ED4
		public override Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			XmlUrlResolver.<GetEntityAsync>d__15 <GetEntityAsync>d__;
			<GetEntityAsync>d__.<>4__this = this;
			<GetEntityAsync>d__.absoluteUri = absoluteUri;
			<GetEntityAsync>d__.ofObjectToReturn = ofObjectToReturn;
			<GetEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<GetEntityAsync>d__.<>1__state = -1;
			<GetEntityAsync>d__.<>t__builder.Start<XmlUrlResolver.<GetEntityAsync>d__15>(ref <GetEntityAsync>d__);
			return <GetEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04001325 RID: 4901
		private static object s_DownloadManager;

		// Token: 0x04001326 RID: 4902
		private ICredentials _credentials;

		// Token: 0x04001327 RID: 4903
		private IWebProxy _proxy;

		// Token: 0x04001328 RID: 4904
		private RequestCachePolicy _cachePolicy;

		// Token: 0x02000249 RID: 585
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <GetEntityAsync>d__15 : IAsyncStateMachine
		{
			// Token: 0x060015BD RID: 5565 RVA: 0x00084D28 File Offset: 0x00082F28
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUrlResolver xmlUrlResolver = this.<>4__this;
				object result;
				try
				{
					ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (!(this.ofObjectToReturn == null) && !(this.ofObjectToReturn == typeof(Stream)) && !(this.ofObjectToReturn == typeof(object)))
						{
							throw new XmlException("Object type is not supported.", string.Empty);
						}
						awaiter = XmlUrlResolver.DownloadManager.GetStreamAsync(this.absoluteUri, xmlUrlResolver._credentials, xmlUrlResolver._proxy, xmlUrlResolver._cachePolicy).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter, XmlUrlResolver.<GetEntityAsync>d__15>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060015BE RID: 5566 RVA: 0x00084E58 File Offset: 0x00083058
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001329 RID: 4905
			public int <>1__state;

			// Token: 0x0400132A RID: 4906
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x0400132B RID: 4907
			public Type ofObjectToReturn;

			// Token: 0x0400132C RID: 4908
			public Uri absoluteUri;

			// Token: 0x0400132D RID: 4909
			public XmlUrlResolver <>4__this;

			// Token: 0x0400132E RID: 4910
			private ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
