using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000002 RID: 2
	internal static class ConnectHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static ValueTask<ValueTuple<Socket, Stream>> ConnectAsync(string host, int port, CancellationToken cancellationToken)
		{
			ConnectHelper.<ConnectAsync>d__2 <ConnectAsync>d__;
			<ConnectAsync>d__.host = host;
			<ConnectAsync>d__.port = port;
			<ConnectAsync>d__.cancellationToken = cancellationToken;
			<ConnectAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<ValueTuple<Socket, Stream>>.Create();
			<ConnectAsync>d__.<>1__state = -1;
			<ConnectAsync>d__.<>t__builder.Start<ConnectHelper.<ConnectAsync>d__2>(ref <ConnectAsync>d__);
			return <ConnectAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A4 File Offset: 0x000002A4
		public static ValueTask<SslStream> EstablishSslConnectionAsync(SslClientAuthenticationOptions sslOptions, HttpRequestMessage request, Stream stream, CancellationToken cancellationToken)
		{
			RemoteCertificateValidationCallback remoteCertificateValidationCallback = sslOptions.RemoteCertificateValidationCallback;
			if (remoteCertificateValidationCallback != null)
			{
				ConnectHelper.CertificateCallbackMapper certificateCallbackMapper = remoteCertificateValidationCallback.Target as ConnectHelper.CertificateCallbackMapper;
				if (certificateCallbackMapper != null)
				{
					sslOptions = sslOptions.ShallowClone();
					Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> localFromHttpClientHandler = certificateCallbackMapper.FromHttpClientHandler;
					HttpRequestMessage localRequest = request;
					sslOptions.RemoteCertificateValidationCallback = ((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => localFromHttpClientHandler(localRequest, certificate as X509Certificate2, chain, sslPolicyErrors));
				}
			}
			return ConnectHelper.EstablishSslConnectionAsyncCore(stream, sslOptions, cancellationToken);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002108 File Offset: 0x00000308
		private static ValueTask<SslStream> EstablishSslConnectionAsyncCore(Stream stream, SslClientAuthenticationOptions sslOptions, CancellationToken cancellationToken)
		{
			ConnectHelper.<EstablishSslConnectionAsyncCore>d__5 <EstablishSslConnectionAsyncCore>d__;
			<EstablishSslConnectionAsyncCore>d__.stream = stream;
			<EstablishSslConnectionAsyncCore>d__.sslOptions = sslOptions;
			<EstablishSslConnectionAsyncCore>d__.cancellationToken = cancellationToken;
			<EstablishSslConnectionAsyncCore>d__.<>t__builder = AsyncValueTaskMethodBuilder<SslStream>.Create();
			<EstablishSslConnectionAsyncCore>d__.<>1__state = -1;
			<EstablishSslConnectionAsyncCore>d__.<>t__builder.Start<ConnectHelper.<EstablishSslConnectionAsyncCore>d__5>(ref <EstablishSslConnectionAsyncCore>d__);
			return <EstablishSslConnectionAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000215B File Offset: 0x0000035B
		// Note: this type is marked as 'beforefieldinit'.
		static ConnectHelper()
		{
		}

		// Token: 0x04000001 RID: 1
		private static readonly ConcurrentQueue<ConnectHelper.ConnectEventArgs>.Segment s_connectEventArgs = new ConcurrentQueue<ConnectHelper.ConnectEventArgs>.Segment(ConcurrentQueue<ConnectHelper.ConnectEventArgs>.Segment.RoundUpToPowerOf2(Math.Max(2, Environment.ProcessorCount)));

		// Token: 0x02000003 RID: 3
		internal sealed class CertificateCallbackMapper
		{
			// Token: 0x06000005 RID: 5 RVA: 0x00002177 File Offset: 0x00000377
			public CertificateCallbackMapper(Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> fromHttpClientHandler)
			{
				this.FromHttpClientHandler = fromHttpClientHandler;
				this.ForSocketsHttpHandler = ((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => this.FromHttpClientHandler(new HttpRequestMessage(HttpMethod.Get, (string)sender), certificate as X509Certificate2, chain, sslPolicyErrors));
			}

			// Token: 0x06000006 RID: 6 RVA: 0x00002198 File Offset: 0x00000398
			[CompilerGenerated]
			private bool <.ctor>b__2_0(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				return this.FromHttpClientHandler(new HttpRequestMessage(HttpMethod.Get, (string)sender), certificate as X509Certificate2, chain, sslPolicyErrors);
			}

			// Token: 0x04000002 RID: 2
			public readonly Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> FromHttpClientHandler;

			// Token: 0x04000003 RID: 3
			public readonly RemoteCertificateValidationCallback ForSocketsHttpHandler;
		}

		// Token: 0x02000004 RID: 4
		private sealed class ConnectEventArgs : SocketAsyncEventArgs
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000007 RID: 7 RVA: 0x000021BE File Offset: 0x000003BE
			// (set) Token: 0x06000008 RID: 8 RVA: 0x000021C6 File Offset: 0x000003C6
			public AsyncTaskMethodBuilder Builder
			{
				[CompilerGenerated]
				get
				{
					return this.<Builder>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Builder>k__BackingField = value;
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000009 RID: 9 RVA: 0x000021CF File Offset: 0x000003CF
			// (set) Token: 0x0600000A RID: 10 RVA: 0x000021D7 File Offset: 0x000003D7
			public CancellationToken CancellationToken
			{
				[CompilerGenerated]
				get
				{
					return this.<CancellationToken>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<CancellationToken>k__BackingField = value;
				}
			}

			// Token: 0x0600000B RID: 11 RVA: 0x000021E0 File Offset: 0x000003E0
			public void Initialize(CancellationToken cancellationToken)
			{
				this.CancellationToken = cancellationToken;
				AsyncTaskMethodBuilder builder = default(AsyncTaskMethodBuilder);
				Task task = builder.Task;
				this.Builder = builder;
			}

			// Token: 0x0600000C RID: 12 RVA: 0x0000220C File Offset: 0x0000040C
			public void Clear()
			{
				this.CancellationToken = default(CancellationToken);
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00002228 File Offset: 0x00000428
			protected override void OnCompleted(SocketAsyncEventArgs _)
			{
				SocketError socketError = base.SocketError;
				if (socketError != SocketError.Success)
				{
					if (socketError == SocketError.OperationAborted || socketError == SocketError.ConnectionAborted)
					{
						if (this.CancellationToken.IsCancellationRequested)
						{
							this.Builder.SetException(CancellationHelper.CreateOperationCanceledException(null, this.CancellationToken));
							return;
						}
					}
					this.Builder.SetException(new SocketException((int)base.SocketError));
					return;
				}
				this.Builder.SetResult();
			}

			// Token: 0x0600000E RID: 14 RVA: 0x000022A4 File Offset: 0x000004A4
			public ConnectEventArgs()
			{
			}

			// Token: 0x04000004 RID: 4
			[CompilerGenerated]
			private AsyncTaskMethodBuilder <Builder>k__BackingField;

			// Token: 0x04000005 RID: 5
			[CompilerGenerated]
			private CancellationToken <CancellationToken>k__BackingField;
		}

		// Token: 0x02000005 RID: 5
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600000F RID: 15 RVA: 0x000022AC File Offset: 0x000004AC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000010 RID: 16 RVA: 0x000022B8 File Offset: 0x000004B8
			public <>c()
			{
			}

			// Token: 0x06000011 RID: 17 RVA: 0x000022C0 File Offset: 0x000004C0
			internal void <ConnectAsync>b__2_0(object s)
			{
				Socket.CancelConnectAsync((SocketAsyncEventArgs)s);
			}

			// Token: 0x06000012 RID: 18 RVA: 0x000022CD File Offset: 0x000004CD
			internal void <EstablishSslConnectionAsyncCore>b__5_0(object s)
			{
				((Stream)s).Dispose();
			}

			// Token: 0x04000006 RID: 6
			public static readonly ConnectHelper.<>c <>9 = new ConnectHelper.<>c();

			// Token: 0x04000007 RID: 7
			public static Action<object> <>9__2_0;

			// Token: 0x04000008 RID: 8
			public static Action<object> <>9__5_0;
		}

		// Token: 0x02000006 RID: 6
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ConnectAsync>d__2 : IAsyncStateMachine
		{
			// Token: 0x06000013 RID: 19 RVA: 0x000022DC File Offset: 0x000004DC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ValueTuple<Socket, Stream> result;
				try
				{
					if (num != 0 && !ConnectHelper.s_connectEventArgs.TryDequeue(out this.<saea>5__2))
					{
						this.<saea>5__2 = new ConnectHelper.ConnectEventArgs();
					}
					try
					{
						if (num != 0)
						{
							this.<saea>5__2.Initialize(this.cancellationToken);
							this.<saea>5__2.RemoteEndPoint = new DnsEndPoint(this.host, this.port);
							if (Socket.ConnectAsync(SocketType.Stream, ProtocolType.Tcp, this.<saea>5__2))
							{
								this.<>7__wrap2 = this.cancellationToken.Register(new Action<object>(ConnectHelper.<>c.<>9.<ConnectAsync>b__2_0), this.<saea>5__2);
							}
							else
							{
								if (this.<saea>5__2.SocketError != SocketError.Success)
								{
									throw new SocketException((int)this.<saea>5__2.SocketError);
								}
								goto IL_15B;
							}
						}
						try
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
							if (num != 0)
							{
								awaiter = this.<saea>5__2.Builder.Task.ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ConnectHelper.<ConnectAsync>d__2>(ref awaiter, ref this);
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
						finally
						{
							if (num < 0)
							{
								((IDisposable)this.<>7__wrap2).Dispose();
							}
						}
						this.<>7__wrap2 = default(CancellationTokenRegistration);
						IL_15B:
						Socket connectSocket = this.<saea>5__2.ConnectSocket;
						connectSocket.NoDelay = true;
						result = new ValueTuple<Socket, Stream>(connectSocket, new NetworkStream(connectSocket, true));
					}
					catch (Exception ex)
					{
						throw CancellationHelper.ShouldWrapInOperationCanceledException(ex, this.cancellationToken) ? CancellationHelper.CreateOperationCanceledException(ex, this.cancellationToken) : new HttpRequestException(ex.Message, ex);
					}
					finally
					{
						if (num < 0)
						{
							this.<saea>5__2.Clear();
							if (!ConnectHelper.s_connectEventArgs.TryEnqueue(this.<saea>5__2))
							{
								this.<saea>5__2.Dispose();
							}
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<saea>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<saea>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000014 RID: 20 RVA: 0x00002564 File Offset: 0x00000764
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000009 RID: 9
			public int <>1__state;

			// Token: 0x0400000A RID: 10
			public AsyncValueTaskMethodBuilder<ValueTuple<Socket, Stream>> <>t__builder;

			// Token: 0x0400000B RID: 11
			public CancellationToken cancellationToken;

			// Token: 0x0400000C RID: 12
			public string host;

			// Token: 0x0400000D RID: 13
			public int port;

			// Token: 0x0400000E RID: 14
			private ConnectHelper.ConnectEventArgs <saea>5__2;

			// Token: 0x0400000F RID: 15
			private CancellationTokenRegistration <>7__wrap2;

			// Token: 0x04000010 RID: 16
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000007 RID: 7
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06000015 RID: 21 RVA: 0x000022B8 File Offset: 0x000004B8
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06000016 RID: 22 RVA: 0x00002572 File Offset: 0x00000772
			internal bool <EstablishSslConnectionAsync>b__0(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				return this.localFromHttpClientHandler(this.localRequest, certificate as X509Certificate2, chain, sslPolicyErrors);
			}

			// Token: 0x04000011 RID: 17
			public Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> localFromHttpClientHandler;

			// Token: 0x04000012 RID: 18
			public HttpRequestMessage localRequest;
		}

		// Token: 0x02000008 RID: 8
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EstablishSslConnectionAsyncCore>d__5 : IAsyncStateMachine
		{
			// Token: 0x06000017 RID: 23 RVA: 0x00002590 File Offset: 0x00000790
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				SslStream result;
				try
				{
					if (num != 0)
					{
						this.<sslStream>5__2 = new SslStream(this.stream);
						this.<ctr>5__3 = this.cancellationToken.Register(new Action<object>(ConnectHelper.<>c.<>9.<EstablishSslConnectionAsyncCore>b__5_0), this.stream);
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = this.<sslStream>5__2.AuthenticateAsClientAsync(this.sslOptions, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, ConnectHelper.<EstablishSslConnectionAsyncCore>d__5>(ref awaiter, ref this);
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
					catch (Exception ex)
					{
						this.<sslStream>5__2.Dispose();
						if (CancellationHelper.ShouldWrapInOperationCanceledException(ex, this.cancellationToken))
						{
							throw CancellationHelper.CreateOperationCanceledException(ex, this.cancellationToken);
						}
						throw new HttpRequestException("The SSL connection could not be established, see inner exception.", ex);
					}
					finally
					{
						if (num < 0)
						{
							this.<ctr>5__3.Dispose();
						}
					}
					if (this.cancellationToken.IsCancellationRequested)
					{
						this.<sslStream>5__2.Dispose();
						throw CancellationHelper.CreateOperationCanceledException(null, this.cancellationToken);
					}
					result = this.<sslStream>5__2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sslStream>5__2 = null;
					this.<ctr>5__3 = default(CancellationTokenRegistration);
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<sslStream>5__2 = null;
				this.<ctr>5__3 = default(CancellationTokenRegistration);
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000018 RID: 24 RVA: 0x00002780 File Offset: 0x00000980
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000013 RID: 19
			public int <>1__state;

			// Token: 0x04000014 RID: 20
			public AsyncValueTaskMethodBuilder<SslStream> <>t__builder;

			// Token: 0x04000015 RID: 21
			public Stream stream;

			// Token: 0x04000016 RID: 22
			public CancellationToken cancellationToken;

			// Token: 0x04000017 RID: 23
			public SslClientAuthenticationOptions sslOptions;

			// Token: 0x04000018 RID: 24
			private SslStream <sslStream>5__2;

			// Token: 0x04000019 RID: 25
			private CancellationTokenRegistration <ctr>5__3;

			// Token: 0x0400001A RID: 26
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
