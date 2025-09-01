using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;

namespace WebSocketSharp.Net
{
	// Token: 0x02000020 RID: 32
	public sealed class HttpListener : IDisposable
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000F060 File Offset: 0x0000D260
		static HttpListener()
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000F070 File Offset: 0x0000D270
		public HttpListener()
		{
			this._authSchemes = AuthenticationSchemes.Anonymous;
			this._contextQueue = new Queue<HttpListenerContext>();
			this._contextRegistry = new LinkedList<HttpListenerContext>();
			this._contextRegistrySync = ((ICollection)this._contextRegistry).SyncRoot;
			this._log = new Logger();
			this._objectName = base.GetType().ToString();
			this._prefixes = new HttpListenerPrefixCollection(this);
			this._waitQueue = new Queue<HttpListenerAsyncResult>();
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000F104 File Offset: 0x0000D304
		internal bool ReuseAddress
		{
			get
			{
				return this._reuseAddress;
			}
			set
			{
				this._reuseAddress = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000F110 File Offset: 0x0000D310
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000F140 File Offset: 0x0000D340
		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._authSchemes;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._authSchemes = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000F16C File Offset: 0x0000D36C
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000F19C File Offset: 0x0000D39C
		public Func<HttpListenerRequest, AuthenticationSchemes> AuthenticationSchemeSelector
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._authSchemeSelector;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._authSchemeSelector = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public string CertificateFolderPath
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._certFolderPath;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._certFolderPath = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000F224 File Offset: 0x0000D424
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000F254 File Offset: 0x0000D454
		public bool IgnoreWriteExceptions
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._ignoreWriteExceptions;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._ignoreWriteExceptions = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000F280 File Offset: 0x0000D480
		public bool IsListening
		{
			get
			{
				return this._listening;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000F29C File Offset: 0x0000D49C
		public static bool IsSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		public Logger Log
		{
			get
			{
				return this._log;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		public HttpListenerPrefixCollection Prefixes
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._prefixes;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000F328 File Offset: 0x0000D528
		public string Realm
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._realm;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._realm = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000F354 File Offset: 0x0000D554
		public ServerSslConfiguration SslConfiguration
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				bool flag = this._sslConfig == null;
				if (flag)
				{
					this._sslConfig = new ServerSslConfiguration();
				}
				return this._sslConfig;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000F39A File Offset: 0x0000D59A
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000F39A File Offset: 0x0000D59A
		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		public Func<IIdentity, NetworkCredential> UserCredentialsFinder
		{
			get
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				return this._userCredFinder;
			}
			set
			{
				bool disposed = this._disposed;
				if (disposed)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				this._userCredFinder = value;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000F400 File Offset: 0x0000D600
		private bool authenticateContext(HttpListenerContext context)
		{
			HttpListenerRequest request = context.Request;
			AuthenticationSchemes authenticationSchemes = this.selectAuthenticationScheme(request);
			bool flag = authenticationSchemes == AuthenticationSchemes.Anonymous;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = authenticationSchemes == AuthenticationSchemes.None;
				if (flag2)
				{
					string message = "Authentication not allowed";
					context.SendError(403, message);
					result = false;
				}
				else
				{
					string realm = this.getRealm();
					IPrincipal principal = HttpUtility.CreateUser(request.Headers["Authorization"], authenticationSchemes, realm, request.HttpMethod, this._userCredFinder);
					bool flag3 = principal != null && principal.Identity.IsAuthenticated;
					bool flag4 = !flag3;
					if (flag4)
					{
						context.SendAuthenticationChallenge(authenticationSchemes, realm);
						result = false;
					}
					else
					{
						context.User = principal;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		private HttpListenerAsyncResult beginGetContext(AsyncCallback callback, object state)
		{
			object contextRegistrySync = this._contextRegistrySync;
			HttpListenerAsyncResult result;
			lock (contextRegistrySync)
			{
				bool flag = !this._listening;
				if (flag)
				{
					string message = this._disposed ? "The listener is closed." : "The listener is stopped.";
					throw new HttpListenerException(995, message);
				}
				HttpListenerAsyncResult httpListenerAsyncResult = new HttpListenerAsyncResult(callback, state);
				bool flag2 = this._contextQueue.Count == 0;
				if (flag2)
				{
					this._waitQueue.Enqueue(httpListenerAsyncResult);
					result = httpListenerAsyncResult;
				}
				else
				{
					HttpListenerContext context = this._contextQueue.Dequeue();
					httpListenerAsyncResult.Complete(context, true);
					result = httpListenerAsyncResult;
				}
			}
			return result;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000F574 File Offset: 0x0000D774
		private void cleanupContextQueue(bool force)
		{
			bool flag = this._contextQueue.Count == 0;
			if (!flag)
			{
				if (force)
				{
					this._contextQueue.Clear();
				}
				else
				{
					HttpListenerContext[] array = this._contextQueue.ToArray();
					this._contextQueue.Clear();
					foreach (HttpListenerContext httpListenerContext in array)
					{
						httpListenerContext.SendError(503);
					}
				}
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		private void cleanupContextRegistry()
		{
			int count = this._contextRegistry.Count;
			bool flag = count == 0;
			if (!flag)
			{
				HttpListenerContext[] array = new HttpListenerContext[count];
				this._contextRegistry.CopyTo(array, 0);
				this._contextRegistry.Clear();
				foreach (HttpListenerContext httpListenerContext in array)
				{
					httpListenerContext.Connection.Close(true);
				}
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000F65C File Offset: 0x0000D85C
		private void cleanupWaitQueue(string message)
		{
			bool flag = this._waitQueue.Count == 0;
			if (!flag)
			{
				HttpListenerAsyncResult[] array = this._waitQueue.ToArray();
				this._waitQueue.Clear();
				foreach (HttpListenerAsyncResult httpListenerAsyncResult in array)
				{
					HttpListenerException exception = new HttpListenerException(995, message);
					httpListenerAsyncResult.Complete(exception);
				}
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		private void close(bool force)
		{
			bool flag = !this._listening;
			if (flag)
			{
				this._disposed = true;
			}
			else
			{
				this._listening = false;
				this.cleanupContextQueue(force);
				this.cleanupContextRegistry();
				string message = "The listener is closed.";
				this.cleanupWaitQueue(message);
				EndPointManager.RemoveListener(this);
				this._disposed = true;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000F724 File Offset: 0x0000D924
		private string getRealm()
		{
			string realm = this._realm;
			return (realm != null && realm.Length > 0) ? realm : HttpListener._defaultRealm;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000F754 File Offset: 0x0000D954
		private bool registerContext(HttpListenerContext context)
		{
			object contextRegistrySync = this._contextRegistrySync;
			bool result;
			lock (contextRegistrySync)
			{
				bool flag = !this._listening;
				if (flag)
				{
					result = false;
				}
				else
				{
					context.Listener = this;
					this._contextRegistry.AddLast(context);
					bool flag2 = this._waitQueue.Count == 0;
					if (flag2)
					{
						this._contextQueue.Enqueue(context);
						result = true;
					}
					else
					{
						HttpListenerAsyncResult httpListenerAsyncResult = this._waitQueue.Dequeue();
						httpListenerAsyncResult.Complete(context, false);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		private AuthenticationSchemes selectAuthenticationScheme(HttpListenerRequest request)
		{
			Func<HttpListenerRequest, AuthenticationSchemes> authSchemeSelector = this._authSchemeSelector;
			bool flag = authSchemeSelector == null;
			AuthenticationSchemes result;
			if (flag)
			{
				result = this._authSchemes;
			}
			else
			{
				try
				{
					result = authSchemeSelector(request);
				}
				catch
				{
					result = AuthenticationSchemes.None;
				}
			}
			return result;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000F840 File Offset: 0x0000DA40
		internal void CheckDisposed()
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000F864 File Offset: 0x0000DA64
		internal bool RegisterContext(HttpListenerContext context)
		{
			bool flag = !this.authenticateContext(context);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.registerContext(context);
				if (flag2)
				{
					context.SendError(503);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		internal void UnregisterContext(HttpListenerContext context)
		{
			object contextRegistrySync = this._contextRegistrySync;
			lock (contextRegistrySync)
			{
				this._contextRegistry.Remove(context);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000F8EC File Offset: 0x0000DAEC
		public void Abort()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				object contextRegistrySync = this._contextRegistrySync;
				lock (contextRegistrySync)
				{
					bool disposed2 = this._disposed;
					if (!disposed2)
					{
						this.close(true);
					}
				}
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000F944 File Offset: 0x0000DB44
		public IAsyncResult BeginGetContext(AsyncCallback callback, object state)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			bool flag = !this._listening;
			if (flag)
			{
				string message = "The listener has not been started.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = this._prefixes.Count == 0;
			if (flag2)
			{
				string message2 = "The listener has no URI prefix on which listens.";
				throw new InvalidOperationException(message2);
			}
			return this.beginGetContext(callback, state);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
		public void Close()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				object contextRegistrySync = this._contextRegistrySync;
				lock (contextRegistrySync)
				{
					bool disposed2 = this._disposed;
					if (!disposed2)
					{
						this.close(false);
					}
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000FA10 File Offset: 0x0000DC10
		public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			bool flag = !this._listening;
			if (flag)
			{
				string message = "The listener has not been started.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = asyncResult == null;
			if (flag2)
			{
				throw new ArgumentNullException("asyncResult");
			}
			HttpListenerAsyncResult httpListenerAsyncResult = asyncResult as HttpListenerAsyncResult;
			bool flag3 = httpListenerAsyncResult == null;
			if (flag3)
			{
				string message2 = "A wrong IAsyncResult instance.";
				throw new ArgumentException(message2, "asyncResult");
			}
			object syncRoot = httpListenerAsyncResult.SyncRoot;
			lock (syncRoot)
			{
				bool endCalled = httpListenerAsyncResult.EndCalled;
				if (endCalled)
				{
					string message3 = "This IAsyncResult instance cannot be reused.";
					throw new InvalidOperationException(message3);
				}
				httpListenerAsyncResult.EndCalled = true;
			}
			bool flag4 = !httpListenerAsyncResult.IsCompleted;
			if (flag4)
			{
				httpListenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			return httpListenerAsyncResult.Context;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000FB04 File Offset: 0x0000DD04
		public HttpListenerContext GetContext()
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			bool flag = !this._listening;
			if (flag)
			{
				string message = "The listener has not been started.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = this._prefixes.Count == 0;
			if (flag2)
			{
				string message2 = "The listener has no URI prefix on which listens.";
				throw new InvalidOperationException(message2);
			}
			HttpListenerAsyncResult httpListenerAsyncResult = this.beginGetContext(null, null);
			httpListenerAsyncResult.EndCalled = true;
			bool flag3 = !httpListenerAsyncResult.IsCompleted;
			if (flag3)
			{
				httpListenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			return httpListenerAsyncResult.Context;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public void Start()
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			object contextRegistrySync = this._contextRegistrySync;
			lock (contextRegistrySync)
			{
				bool disposed2 = this._disposed;
				if (disposed2)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				bool listening = this._listening;
				if (!listening)
				{
					EndPointManager.AddListener(this);
					this._listening = true;
				}
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000FC28 File Offset: 0x0000DE28
		public void Stop()
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(this._objectName);
			}
			object contextRegistrySync = this._contextRegistrySync;
			lock (contextRegistrySync)
			{
				bool disposed2 = this._disposed;
				if (disposed2)
				{
					throw new ObjectDisposedException(this._objectName);
				}
				bool flag = !this._listening;
				if (!flag)
				{
					this._listening = false;
					this.cleanupContextQueue(false);
					this.cleanupContextRegistry();
					string message = "The listener is stopped.";
					this.cleanupWaitQueue(message);
					EndPointManager.RemoveListener(this);
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000FCCC File Offset: 0x0000DECC
		void IDisposable.Dispose()
		{
			bool disposed = this._disposed;
			if (!disposed)
			{
				object contextRegistrySync = this._contextRegistrySync;
				lock (contextRegistrySync)
				{
					bool disposed2 = this._disposed;
					if (!disposed2)
					{
						this.close(true);
					}
				}
			}
		}

		// Token: 0x040000DB RID: 219
		private AuthenticationSchemes _authSchemes;

		// Token: 0x040000DC RID: 220
		private Func<HttpListenerRequest, AuthenticationSchemes> _authSchemeSelector;

		// Token: 0x040000DD RID: 221
		private string _certFolderPath;

		// Token: 0x040000DE RID: 222
		private Queue<HttpListenerContext> _contextQueue;

		// Token: 0x040000DF RID: 223
		private LinkedList<HttpListenerContext> _contextRegistry;

		// Token: 0x040000E0 RID: 224
		private object _contextRegistrySync;

		// Token: 0x040000E1 RID: 225
		private static readonly string _defaultRealm = "SECRET AREA";

		// Token: 0x040000E2 RID: 226
		private bool _disposed;

		// Token: 0x040000E3 RID: 227
		private bool _ignoreWriteExceptions;

		// Token: 0x040000E4 RID: 228
		private volatile bool _listening;

		// Token: 0x040000E5 RID: 229
		private Logger _log;

		// Token: 0x040000E6 RID: 230
		private string _objectName;

		// Token: 0x040000E7 RID: 231
		private HttpListenerPrefixCollection _prefixes;

		// Token: 0x040000E8 RID: 232
		private string _realm;

		// Token: 0x040000E9 RID: 233
		private bool _reuseAddress;

		// Token: 0x040000EA RID: 234
		private ServerSslConfiguration _sslConfig;

		// Token: 0x040000EB RID: 235
		private Func<IIdentity, NetworkCredential> _userCredFinder;

		// Token: 0x040000EC RID: 236
		private Queue<HttpListenerAsyncResult> _waitQueue;
	}
}
