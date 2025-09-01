using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000229 RID: 553
	internal class XmlDownloadManager
	{
		// Token: 0x060014FA RID: 5370 RVA: 0x00082A11 File Offset: 0x00080C11
		internal Stream GetStream(Uri uri, ICredentials credentials, IWebProxy proxy, RequestCachePolicy cachePolicy)
		{
			if (uri.Scheme == "file")
			{
				return new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read, 1);
			}
			return this.GetNonFileStream(uri, credentials, proxy, cachePolicy);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00082A40 File Offset: 0x00080C40
		private Stream GetNonFileStream(Uri uri, ICredentials credentials, IWebProxy proxy, RequestCachePolicy cachePolicy)
		{
			WebRequest webRequest = WebRequest.Create(uri);
			if (credentials != null)
			{
				webRequest.Credentials = credentials;
			}
			if (proxy != null)
			{
				webRequest.Proxy = proxy;
			}
			if (cachePolicy != null)
			{
				webRequest.CachePolicy = cachePolicy;
			}
			WebResponse response = webRequest.GetResponse();
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest != null)
			{
				lock (this)
				{
					if (this.connections == null)
					{
						this.connections = new Hashtable();
					}
					OpenedHost openedHost = (OpenedHost)this.connections[httpWebRequest.Address.Host];
					if (openedHost == null)
					{
						openedHost = new OpenedHost();
					}
					if (openedHost.nonCachedConnectionsCount < httpWebRequest.ServicePoint.ConnectionLimit - 1)
					{
						if (openedHost.nonCachedConnectionsCount == 0)
						{
							this.connections.Add(httpWebRequest.Address.Host, openedHost);
						}
						openedHost.nonCachedConnectionsCount++;
						return new XmlRegisteredNonCachedStream(response.GetResponseStream(), this, httpWebRequest.Address.Host);
					}
					return new XmlCachedStream(response.ResponseUri, response.GetResponseStream());
				}
			}
			return response.GetResponseStream();
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00082B6C File Offset: 0x00080D6C
		internal void Remove(string host)
		{
			lock (this)
			{
				OpenedHost openedHost = (OpenedHost)this.connections[host];
				if (openedHost != null)
				{
					OpenedHost openedHost2 = openedHost;
					int num = openedHost2.nonCachedConnectionsCount - 1;
					openedHost2.nonCachedConnectionsCount = num;
					if (num == 0)
					{
						this.connections.Remove(host);
					}
				}
			}
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00082BD8 File Offset: 0x00080DD8
		internal Task<Stream> GetStreamAsync(Uri uri, ICredentials credentials, IWebProxy proxy, RequestCachePolicy cachePolicy)
		{
			if (uri.Scheme == "file")
			{
				return Task.Run<Stream>(() => new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read, 1, true));
			}
			return this.GetNonFileStreamAsync(uri, credentials, proxy, cachePolicy);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00082C2C File Offset: 0x00080E2C
		private Task<Stream> GetNonFileStreamAsync(Uri uri, ICredentials credentials, IWebProxy proxy, RequestCachePolicy cachePolicy)
		{
			XmlDownloadManager.<GetNonFileStreamAsync>d__5 <GetNonFileStreamAsync>d__;
			<GetNonFileStreamAsync>d__.<>4__this = this;
			<GetNonFileStreamAsync>d__.uri = uri;
			<GetNonFileStreamAsync>d__.credentials = credentials;
			<GetNonFileStreamAsync>d__.proxy = proxy;
			<GetNonFileStreamAsync>d__.cachePolicy = cachePolicy;
			<GetNonFileStreamAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Stream>.Create();
			<GetNonFileStreamAsync>d__.<>1__state = -1;
			<GetNonFileStreamAsync>d__.<>t__builder.Start<XmlDownloadManager.<GetNonFileStreamAsync>d__5>(ref <GetNonFileStreamAsync>d__);
			return <GetNonFileStreamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0000216B File Offset: 0x0000036B
		public XmlDownloadManager()
		{
		}

		// Token: 0x040012BE RID: 4798
		private Hashtable connections;

		// Token: 0x0200022A RID: 554
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06001500 RID: 5376 RVA: 0x0000216B File Offset: 0x0000036B
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06001501 RID: 5377 RVA: 0x00082C90 File Offset: 0x00080E90
			internal Stream <GetStreamAsync>b__0()
			{
				return new FileStream(this.uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read, 1, true);
			}

			// Token: 0x040012BF RID: 4799
			public Uri uri;
		}

		// Token: 0x0200022B RID: 555
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <GetNonFileStreamAsync>d__5 : IAsyncStateMachine
		{
			// Token: 0x06001502 RID: 5378 RVA: 0x00082CA8 File Offset: 0x00080EA8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlDownloadManager xmlDownloadManager = this.<>4__this;
				Stream result2;
				try
				{
					ConfiguredTaskAwaitable<WebResponse>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						this.<req>5__2 = WebRequest.Create(this.uri);
						if (this.credentials != null)
						{
							this.<req>5__2.Credentials = this.credentials;
						}
						if (this.proxy != null)
						{
							this.<req>5__2.Proxy = this.proxy;
						}
						if (this.cachePolicy != null)
						{
							this.<req>5__2.CachePolicy = this.cachePolicy;
						}
						awaiter = Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.<req>5__2.BeginGetResponse), new Func<IAsyncResult, WebResponse>(this.<req>5__2.EndGetResponse), null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<WebResponse>.ConfiguredTaskAwaiter, XmlDownloadManager.<GetNonFileStreamAsync>d__5>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<WebResponse>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
					}
					WebResponse result = awaiter.GetResult();
					HttpWebRequest httpWebRequest = this.<req>5__2 as HttpWebRequest;
					if (httpWebRequest != null)
					{
						XmlDownloadManager obj = xmlDownloadManager;
						bool flag = false;
						try
						{
							Monitor.Enter(obj, ref flag);
							if (xmlDownloadManager.connections == null)
							{
								xmlDownloadManager.connections = new Hashtable();
							}
							OpenedHost openedHost = (OpenedHost)xmlDownloadManager.connections[httpWebRequest.Address.Host];
							if (openedHost == null)
							{
								openedHost = new OpenedHost();
							}
							if (openedHost.nonCachedConnectionsCount < httpWebRequest.ServicePoint.ConnectionLimit - 1)
							{
								if (openedHost.nonCachedConnectionsCount == 0)
								{
									xmlDownloadManager.connections.Add(httpWebRequest.Address.Host, openedHost);
								}
								openedHost.nonCachedConnectionsCount++;
								result2 = new XmlRegisteredNonCachedStream(result.GetResponseStream(), xmlDownloadManager, httpWebRequest.Address.Host);
								goto IL_211;
							}
							result2 = new XmlCachedStream(result.ResponseUri, result.GetResponseStream());
							goto IL_211;
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(obj);
							}
						}
					}
					result2 = result.GetResponseStream();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<req>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_211:
				this.<>1__state = -2;
				this.<req>5__2 = null;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06001503 RID: 5379 RVA: 0x00082F18 File Offset: 0x00081118
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040012C0 RID: 4800
			public int <>1__state;

			// Token: 0x040012C1 RID: 4801
			public AsyncTaskMethodBuilder<Stream> <>t__builder;

			// Token: 0x040012C2 RID: 4802
			public Uri uri;

			// Token: 0x040012C3 RID: 4803
			public ICredentials credentials;

			// Token: 0x040012C4 RID: 4804
			public IWebProxy proxy;

			// Token: 0x040012C5 RID: 4805
			public RequestCachePolicy cachePolicy;

			// Token: 0x040012C6 RID: 4806
			public XmlDownloadManager <>4__this;

			// Token: 0x040012C7 RID: 4807
			private WebRequest <req>5__2;

			// Token: 0x040012C8 RID: 4808
			private ConfiguredTaskAwaitable<WebResponse>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
