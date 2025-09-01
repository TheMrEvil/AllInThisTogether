using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;

namespace System.Net
{
	// Token: 0x020006BE RID: 1726
	internal class WebConnection : IDisposable
	{
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x000C28BB File Offset: 0x000C0ABB
		public ServicePoint ServicePoint
		{
			[CompilerGenerated]
			get
			{
				return this.<ServicePoint>k__BackingField;
			}
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000C28C3 File Offset: 0x000C0AC3
		public WebConnection(ServicePoint sPoint)
		{
			this.ServicePoint = sPoint;
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_WEB_DEBUG")]
		internal static void Debug(string message, params object[] args)
		{
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_WEB_DEBUG")]
		internal static void Debug(string message)
		{
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000C28D2 File Offset: 0x000C0AD2
		private bool CanReuse()
		{
			return !this.socket.Poll(0, SelectMode.SelectRead);
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000C28E4 File Offset: 0x000C0AE4
		private bool CheckReusable()
		{
			if (this.socket != null && this.socket.Connected)
			{
				try
				{
					if (this.CanReuse())
					{
						return true;
					}
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000C292C File Offset: 0x000C0B2C
		private Task Connect(WebOperation operation, CancellationToken cancellationToken)
		{
			WebConnection.<Connect>d__16 <Connect>d__;
			<Connect>d__.<>4__this = this;
			<Connect>d__.operation = operation;
			<Connect>d__.cancellationToken = cancellationToken;
			<Connect>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Connect>d__.<>1__state = -1;
			<Connect>d__.<>t__builder.Start<WebConnection.<Connect>d__16>(ref <Connect>d__);
			return <Connect>d__.<>t__builder.Task;
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000C2980 File Offset: 0x000C0B80
		private Task<bool> CreateStream(WebOperation operation, bool reused, CancellationToken cancellationToken)
		{
			WebConnection.<CreateStream>d__18 <CreateStream>d__;
			<CreateStream>d__.<>4__this = this;
			<CreateStream>d__.operation = operation;
			<CreateStream>d__.reused = reused;
			<CreateStream>d__.cancellationToken = cancellationToken;
			<CreateStream>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CreateStream>d__.<>1__state = -1;
			<CreateStream>d__.<>t__builder.Start<WebConnection.<CreateStream>d__18>(ref <CreateStream>d__);
			return <CreateStream>d__.<>t__builder.Task;
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000C29DC File Offset: 0x000C0BDC
		internal Task<WebRequestStream> InitConnection(WebOperation operation, CancellationToken cancellationToken)
		{
			WebConnection.<InitConnection>d__19 <InitConnection>d__;
			<InitConnection>d__.<>4__this = this;
			<InitConnection>d__.operation = operation;
			<InitConnection>d__.cancellationToken = cancellationToken;
			<InitConnection>d__.<>t__builder = AsyncTaskMethodBuilder<WebRequestStream>.Create();
			<InitConnection>d__.<>1__state = -1;
			<InitConnection>d__.<>t__builder.Start<WebConnection.<InitConnection>d__19>(ref <InitConnection>d__);
			return <InitConnection>d__.<>t__builder.Task;
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000C2A30 File Offset: 0x000C0C30
		internal static WebException GetException(WebExceptionStatus status, Exception error)
		{
			if (error == null)
			{
				return new WebException(string.Format("Error: {0}", status), status);
			}
			WebException ex = error as WebException;
			if (ex != null)
			{
				return ex;
			}
			return new WebException(string.Format("Error: {0} ({1})", status, error.Message), status, WebExceptionInternalStatus.RequestFatal, error);
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000C2A84 File Offset: 0x000C0C84
		internal static bool ReadLine(byte[] buffer, ref int start, int max, ref string output)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (start < max)
			{
				int num2 = start;
				start = num2 + 1;
				num = (int)buffer[num2];
				if (num == 10)
				{
					if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\r')
					{
						StringBuilder stringBuilder2 = stringBuilder;
						num2 = stringBuilder2.Length;
						stringBuilder2.Length = num2 - 1;
					}
					flag = false;
					break;
				}
				if (flag)
				{
					StringBuilder stringBuilder3 = stringBuilder;
					num2 = stringBuilder3.Length;
					stringBuilder3.Length = num2 - 1;
					break;
				}
				if (num == 13)
				{
					flag = true;
				}
				stringBuilder.Append((char)num);
			}
			if (num != 10 && num != 13)
			{
				return false;
			}
			if (stringBuilder.Length == 0)
			{
				output = null;
				return num == 10 || num == 13;
			}
			if (flag)
			{
				StringBuilder stringBuilder4 = stringBuilder;
				int num2 = stringBuilder4.Length;
				stringBuilder4.Length = num2 - 1;
			}
			output = stringBuilder.ToString();
			return true;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000C2B48 File Offset: 0x000C0D48
		internal bool CanReuseConnection(WebOperation operation)
		{
			bool result;
			lock (this)
			{
				if (this.Closed || this.currentOperation != null)
				{
					result = false;
				}
				else if (!this.NtlmAuthenticated)
				{
					result = true;
				}
				else
				{
					NetworkCredential ntlmCredential = this.NtlmCredential;
					HttpWebRequest request = operation.Request;
					ICredentials credentials = (request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials;
					NetworkCredential networkCredential = (credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null;
					if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
					{
						result = false;
					}
					else
					{
						bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
						bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
						result = (unsafeAuthenticatedConnectionSharing && unsafeAuthenticatedConnectionSharing == unsafeAuthenticatedConnectionSharing2);
					}
				}
			}
			return result;
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000C2C70 File Offset: 0x000C0E70
		private bool PrepareSharingNtlm(WebOperation operation)
		{
			if (operation == null || !this.NtlmAuthenticated)
			{
				return true;
			}
			bool flag = false;
			NetworkCredential ntlmCredential = this.NtlmCredential;
			HttpWebRequest request = operation.Request;
			ICredentials credentials = (request.Proxy == null || request.Proxy.IsBypassed(request.RequestUri)) ? request.Credentials : request.Proxy.Credentials;
			NetworkCredential networkCredential = (credentials != null) ? credentials.GetCredential(request.RequestUri, "NTLM") : null;
			if (ntlmCredential == null || networkCredential == null || ntlmCredential.Domain != networkCredential.Domain || ntlmCredential.UserName != networkCredential.UserName || ntlmCredential.Password != networkCredential.Password)
			{
				flag = true;
			}
			if (!flag)
			{
				bool unsafeAuthenticatedConnectionSharing = request.UnsafeAuthenticatedConnectionSharing;
				bool unsafeAuthenticatedConnectionSharing2 = this.UnsafeAuthenticatedConnectionSharing;
				flag = (!unsafeAuthenticatedConnectionSharing || unsafeAuthenticatedConnectionSharing != unsafeAuthenticatedConnectionSharing2);
			}
			return flag;
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000C2D54 File Offset: 0x000C0F54
		private void Reset()
		{
			lock (this)
			{
				this.tunnel = null;
				this.ResetNtlm();
			}
		}

		// Token: 0x0600377D RID: 14205 RVA: 0x000C2D98 File Offset: 0x000C0F98
		private void Close(bool reset)
		{
			lock (this)
			{
				this.CloseSocket();
				if (reset)
				{
					this.Reset();
				}
			}
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000C2DDC File Offset: 0x000C0FDC
		private void CloseSocket()
		{
			lock (this)
			{
				if (this.networkStream != null)
				{
					try
					{
						this.networkStream.Dispose();
					}
					catch
					{
					}
					this.networkStream = null;
				}
				if (this.monoTlsStream != null)
				{
					try
					{
						this.monoTlsStream.Dispose();
					}
					catch
					{
					}
					this.monoTlsStream = null;
				}
				if (this.socket != null)
				{
					try
					{
						this.socket.Dispose();
					}
					catch
					{
					}
					this.socket = null;
				}
				this.monoTlsStream = null;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600377F RID: 14207 RVA: 0x000C2E9C File Offset: 0x000C109C
		public bool Closed
		{
			get
			{
				return this.disposed != 0;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06003780 RID: 14208 RVA: 0x000C2EA7 File Offset: 0x000C10A7
		public bool Busy
		{
			get
			{
				return this.currentOperation != null;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x000C2EB2 File Offset: 0x000C10B2
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000C2EBC File Offset: 0x000C10BC
		public bool StartOperation(WebOperation operation, bool reused)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.currentOperation, operation, null) != null)
				{
					return false;
				}
				this.idleSince = DateTime.UtcNow + TimeSpan.FromDays(3650.0);
				if (reused && !this.PrepareSharingNtlm(operation))
				{
					this.Close(true);
				}
				operation.RegisterRequest(this.ServicePoint, this);
			}
			operation.Run();
			return true;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000C2F58 File Offset: 0x000C1158
		public bool Continue(WebOperation next)
		{
			lock (this)
			{
				if (this.Closed)
				{
					return false;
				}
				if (this.socket == null || !this.socket.Connected || !this.PrepareSharingNtlm(next))
				{
					this.Close(true);
					return false;
				}
				this.currentOperation = next;
				if (next == null)
				{
					return true;
				}
				next.RegisterRequest(this.ServicePoint, this);
			}
			next.Run();
			return true;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000C2FE8 File Offset: 0x000C11E8
		private void Dispose(bool disposing)
		{
			if (Interlocked.CompareExchange(ref this.disposed, 1, 0) != 0)
			{
				return;
			}
			this.Close(true);
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000C3001 File Offset: 0x000C1201
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000C300A File Offset: 0x000C120A
		private void ResetNtlm()
		{
			this.ntlm_authenticated = false;
			this.ntlm_credentials = null;
			this.unsafe_sharing = false;
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003787 RID: 14215 RVA: 0x000C3021 File Offset: 0x000C1221
		// (set) Token: 0x06003788 RID: 14216 RVA: 0x000C3029 File Offset: 0x000C1229
		internal bool NtlmAuthenticated
		{
			get
			{
				return this.ntlm_authenticated;
			}
			set
			{
				this.ntlm_authenticated = value;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000C3032 File Offset: 0x000C1232
		// (set) Token: 0x0600378A RID: 14218 RVA: 0x000C303A File Offset: 0x000C123A
		internal NetworkCredential NtlmCredential
		{
			get
			{
				return this.ntlm_credentials;
			}
			set
			{
				this.ntlm_credentials = value;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600378B RID: 14219 RVA: 0x000C3043 File Offset: 0x000C1243
		// (set) Token: 0x0600378C RID: 14220 RVA: 0x000C304B File Offset: 0x000C124B
		internal bool UnsafeAuthenticatedConnectionSharing
		{
			get
			{
				return this.unsafe_sharing;
			}
			set
			{
				this.unsafe_sharing = value;
			}
		}

		// Token: 0x04002051 RID: 8273
		private NetworkCredential ntlm_credentials;

		// Token: 0x04002052 RID: 8274
		private bool ntlm_authenticated;

		// Token: 0x04002053 RID: 8275
		private bool unsafe_sharing;

		// Token: 0x04002054 RID: 8276
		private Stream networkStream;

		// Token: 0x04002055 RID: 8277
		private Socket socket;

		// Token: 0x04002056 RID: 8278
		private MonoTlsStream monoTlsStream;

		// Token: 0x04002057 RID: 8279
		private WebConnectionTunnel tunnel;

		// Token: 0x04002058 RID: 8280
		private int disposed;

		// Token: 0x04002059 RID: 8281
		[CompilerGenerated]
		private readonly ServicePoint <ServicePoint>k__BackingField;

		// Token: 0x0400205A RID: 8282
		internal readonly int ID;

		// Token: 0x0400205B RID: 8283
		private DateTime idleSince;

		// Token: 0x0400205C RID: 8284
		private WebOperation currentOperation;

		// Token: 0x020006BF RID: 1727
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600378D RID: 14221 RVA: 0x000C3054 File Offset: 0x000C1254
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600378E RID: 14222 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x0600378F RID: 14223 RVA: 0x000C3060 File Offset: 0x000C1260
			internal IAsyncResult <Connect>b__16_0(IPEndPoint targetEndPoint, AsyncCallback callback, object state)
			{
				return ((Socket)state).BeginConnect(targetEndPoint, callback, state);
			}

			// Token: 0x06003790 RID: 14224 RVA: 0x000C3070 File Offset: 0x000C1270
			internal void <Connect>b__16_1(IAsyncResult asyncResult)
			{
				((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
			}

			// Token: 0x0400205D RID: 8285
			public static readonly WebConnection.<>c <>9 = new WebConnection.<>c();

			// Token: 0x0400205E RID: 8286
			public static Func<IPEndPoint, AsyncCallback, object, IAsyncResult> <>9__16_0;

			// Token: 0x0400205F RID: 8287
			public static Action<IAsyncResult> <>9__16_1;
		}

		// Token: 0x020006C0 RID: 1728
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Connect>d__16 : IAsyncStateMachine
		{
			// Token: 0x06003791 RID: 14225 RVA: 0x000C3084 File Offset: 0x000C1284
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebConnection webConnection = this.<>4__this;
				try
				{
					if (num != 0)
					{
						IPHostEntry hostEntry = webConnection.ServicePoint.HostEntry;
						if (hostEntry == null || hostEntry.AddressList.Length == 0)
						{
							throw WebConnection.GetException(webConnection.ServicePoint.UsesProxy ? WebExceptionStatus.ProxyNameResolutionFailure : WebExceptionStatus.NameResolutionFailure, null);
						}
						this.<connectException>5__2 = null;
						this.<>7__wrap2 = hostEntry.AddressList;
						this.<>7__wrap3 = 0;
						goto IL_22E;
					}
					IL_11E:
					IPEndPoint ipendPoint;
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							this.operation.ThrowIfDisposed(this.cancellationToken);
							awaiter = Task.Factory.FromAsync<IPEndPoint>(new Func<IPEndPoint, AsyncCallback, object, IAsyncResult>(WebConnection.<>c.<>9.<Connect>b__16_0), new Action<IAsyncResult>(WebConnection.<>c.<>9.<Connect>b__16_1), ipendPoint, webConnection.socket).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebConnection.<Connect>d__16>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter.GetResult();
					}
					catch (ObjectDisposedException)
					{
						throw;
					}
					catch (Exception error)
					{
						Socket socket = Interlocked.Exchange<Socket>(ref webConnection.socket, null);
						if (socket != null)
						{
							socket.Close();
						}
						this.<connectException>5__2 = WebConnection.GetException(WebExceptionStatus.ConnectFailure, error);
						goto IL_220;
					}
					if (webConnection.socket != null)
					{
						goto IL_284;
					}
					IL_220:
					this.<>7__wrap3++;
					IL_22E:
					if (this.<>7__wrap3 >= this.<>7__wrap2.Length)
					{
						this.<>7__wrap2 = null;
						if (this.<connectException>5__2 == null)
						{
							this.<connectException>5__2 = WebConnection.GetException(WebExceptionStatus.ConnectFailure, null);
						}
						throw this.<connectException>5__2;
					}
					IPAddress ipaddress = this.<>7__wrap2[this.<>7__wrap3];
					this.operation.ThrowIfDisposed(this.cancellationToken);
					try
					{
						webConnection.socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
					}
					catch (Exception error2)
					{
						throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, error2);
					}
					ipendPoint = new IPEndPoint(ipaddress, webConnection.ServicePoint.Address.Port);
					webConnection.socket.NoDelay = !webConnection.ServicePoint.UseNagleAlgorithm;
					try
					{
						webConnection.ServicePoint.KeepAliveSetup(webConnection.socket);
					}
					catch
					{
					}
					if (webConnection.ServicePoint.CallEndPointDelegate(webConnection.socket, ipendPoint))
					{
						goto IL_11E;
					}
					Socket socket2 = Interlocked.Exchange<Socket>(ref webConnection.socket, null);
					if (socket2 == null)
					{
						goto IL_220;
					}
					socket2.Close();
					goto IL_220;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<connectException>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_284:
				this.<>1__state = -2;
				this.<connectException>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06003792 RID: 14226 RVA: 0x000C33AC File Offset: 0x000C15AC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002060 RID: 8288
			public int <>1__state;

			// Token: 0x04002061 RID: 8289
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002062 RID: 8290
			public WebConnection <>4__this;

			// Token: 0x04002063 RID: 8291
			public WebOperation operation;

			// Token: 0x04002064 RID: 8292
			public CancellationToken cancellationToken;

			// Token: 0x04002065 RID: 8293
			private Exception <connectException>5__2;

			// Token: 0x04002066 RID: 8294
			private IPAddress[] <>7__wrap2;

			// Token: 0x04002067 RID: 8295
			private int <>7__wrap3;

			// Token: 0x04002068 RID: 8296
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020006C1 RID: 1729
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CreateStream>d__18 : IAsyncStateMachine
		{
			// Token: 0x06003793 RID: 14227 RVA: 0x000C33BC File Offset: 0x000C15BC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebConnection webConnection = this.<>4__this;
				bool result;
				try
				{
					try
					{
						ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter awaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
								goto IL_1BA;
							}
							this.<stream>5__2 = new NetworkStream(webConnection.socket, false);
							if (!(this.operation.Request.Address.Scheme == Uri.UriSchemeHttps))
							{
								webConnection.networkStream = this.<stream>5__2;
								result = true;
								goto IL_239;
							}
							if (this.reused && webConnection.monoTlsStream != null)
							{
								goto IL_1CB;
							}
							if (!webConnection.ServicePoint.UseConnect)
							{
								goto IL_12C;
							}
							if (webConnection.tunnel == null)
							{
								webConnection.tunnel = new WebConnectionTunnel(this.operation.Request, webConnection.ServicePoint.Address);
							}
							awaiter2 = webConnection.tunnel.Initialize(this.<stream>5__2, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebConnection.<CreateStream>d__18>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter2.GetResult();
						if (!webConnection.tunnel.Success)
						{
							result = false;
							goto IL_239;
						}
						IL_12C:
						webConnection.monoTlsStream = new MonoTlsStream(this.operation.Request, this.<stream>5__2);
						awaiter = webConnection.monoTlsStream.CreateStream(webConnection.tunnel, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter, WebConnection.<CreateStream>d__18>(ref awaiter, ref this);
							return;
						}
						IL_1BA:
						Stream result2 = awaiter.GetResult();
						webConnection.networkStream = result2;
						IL_1CB:
						result = true;
					}
					catch (Exception ex)
					{
						ex = HttpWebRequest.FlattenException(ex);
						if (this.operation.Aborted || webConnection.monoTlsStream == null)
						{
							throw WebConnection.GetException(WebExceptionStatus.ConnectFailure, ex);
						}
						throw WebConnection.GetException(webConnection.monoTlsStream.ExceptionStatus, ex);
					}
					finally
					{
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_239:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06003794 RID: 14228 RVA: 0x000C3664 File Offset: 0x000C1864
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002069 RID: 8297
			public int <>1__state;

			// Token: 0x0400206A RID: 8298
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400206B RID: 8299
			public WebConnection <>4__this;

			// Token: 0x0400206C RID: 8300
			public WebOperation operation;

			// Token: 0x0400206D RID: 8301
			public bool reused;

			// Token: 0x0400206E RID: 8302
			public CancellationToken cancellationToken;

			// Token: 0x0400206F RID: 8303
			private NetworkStream <stream>5__2;

			// Token: 0x04002070 RID: 8304
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04002071 RID: 8305
			private ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020006C2 RID: 1730
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InitConnection>d__19 : IAsyncStateMachine
		{
			// Token: 0x06003795 RID: 14229 RVA: 0x000C3674 File Offset: 0x000C1874
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebConnection webConnection = this.<>4__this;
				WebRequestStream result;
				try
				{
					if (num == 0)
					{
						goto IL_51;
					}
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num == 1)
					{
						awaiter = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_141;
					}
					bool flag = true;
					IL_1A:
					this.operation.ThrowIfClosedOrDisposed(this.cancellationToken);
					this.<reused>5__2 = webConnection.CheckReusable();
					if (this.<reused>5__2)
					{
						goto IL_CE;
					}
					webConnection.CloseSocket();
					if (flag)
					{
						webConnection.Reset();
					}
					IL_51:
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
						if (num != 0)
						{
							awaiter2 = webConnection.Connect(this.operation, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebConnection.<InitConnection>d__19>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter2.GetResult();
					}
					catch (Exception)
					{
						throw;
					}
					IL_CE:
					awaiter = webConnection.CreateStream(this.operation, this.<reused>5__2, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 1);
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, WebConnection.<InitConnection>d__19>(ref awaiter, ref this);
						return;
					}
					IL_141:
					if (!awaiter.GetResult())
					{
						WebConnectionTunnel tunnel = webConnection.tunnel;
						if (((tunnel != null) ? tunnel.Challenge : null) == null)
						{
							throw WebConnection.GetException(WebExceptionStatus.ProtocolError, null);
						}
						if (webConnection.tunnel.CloseConnection)
						{
							webConnection.CloseSocket();
						}
						flag = false;
						goto IL_1A;
					}
					else
					{
						webConnection.networkStream.ReadTimeout = this.operation.Request.ReadWriteTimeout;
						result = new WebRequestStream(webConnection, this.operation, webConnection.networkStream, webConnection.tunnel);
					}
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

			// Token: 0x06003796 RID: 14230 RVA: 0x000C3898 File Offset: 0x000C1A98
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002072 RID: 8306
			public int <>1__state;

			// Token: 0x04002073 RID: 8307
			public AsyncTaskMethodBuilder<WebRequestStream> <>t__builder;

			// Token: 0x04002074 RID: 8308
			public WebOperation operation;

			// Token: 0x04002075 RID: 8309
			public CancellationToken cancellationToken;

			// Token: 0x04002076 RID: 8310
			public WebConnection <>4__this;

			// Token: 0x04002077 RID: 8311
			private bool <reused>5__2;

			// Token: 0x04002078 RID: 8312
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04002079 RID: 8313
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
