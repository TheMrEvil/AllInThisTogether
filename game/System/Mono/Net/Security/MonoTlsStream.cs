using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security.Private;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A6 RID: 166
	internal class MonoTlsStream : IDisposable
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600033F RID: 831 RVA: 0x000099A0 File Offset: 0x00007BA0
		internal HttpWebRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000340 RID: 832 RVA: 0x000099A8 File Offset: 0x00007BA8
		internal SslStream SslStream
		{
			get
			{
				return this.sslStream;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000099B0 File Offset: 0x00007BB0
		internal WebExceptionStatus ExceptionStatus
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000342 RID: 834 RVA: 0x000099B8 File Offset: 0x00007BB8
		// (set) Token: 0x06000343 RID: 835 RVA: 0x000099C0 File Offset: 0x00007BC0
		internal bool CertificateValidationFailed
		{
			[CompilerGenerated]
			get
			{
				return this.<CertificateValidationFailed>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CertificateValidationFailed>k__BackingField = value;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000099CC File Offset: 0x00007BCC
		public MonoTlsStream(HttpWebRequest request, NetworkStream networkStream)
		{
			this.request = request;
			this.networkStream = networkStream;
			this.settings = request.TlsSettings;
			if (this.settings == null)
			{
				this.settings = MonoTlsSettings.CopyDefaultSettings();
			}
			if (this.settings.RemoteCertificateValidationCallback == null)
			{
				this.settings.RemoteCertificateValidationCallback = CallbackHelpers.PublicToMono(request.ServerCertificateValidationCallback);
			}
			this.provider = (request.TlsProvider ?? MonoTlsProviderFactory.GetProviderInternal());
			this.status = WebExceptionStatus.SecureChannelFailure;
			ChainValidationHelper.Create(this.provider, ref this.settings, this);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00009A6C File Offset: 0x00007C6C
		internal Task<Stream> CreateStream(WebConnectionTunnel tunnel, CancellationToken cancellationToken)
		{
			MonoTlsStream.<CreateStream>d__18 <CreateStream>d__;
			<CreateStream>d__.<>4__this = this;
			<CreateStream>d__.tunnel = tunnel;
			<CreateStream>d__.cancellationToken = cancellationToken;
			<CreateStream>d__.<>t__builder = AsyncTaskMethodBuilder<Stream>.Create();
			<CreateStream>d__.<>1__state = -1;
			<CreateStream>d__.<>t__builder.Start<MonoTlsStream.<CreateStream>d__18>(ref <CreateStream>d__);
			return <CreateStream>d__.<>t__builder.Task;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00009ABF File Offset: 0x00007CBF
		public void Dispose()
		{
			this.CloseSslStream();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00009AC8 File Offset: 0x00007CC8
		private void CloseSslStream()
		{
			object obj = this.sslStreamLock;
			lock (obj)
			{
				if (this.sslStream != null)
				{
					this.sslStream.Dispose();
					this.sslStream = null;
				}
			}
		}

		// Token: 0x04000280 RID: 640
		private readonly MobileTlsProvider provider;

		// Token: 0x04000281 RID: 641
		private readonly NetworkStream networkStream;

		// Token: 0x04000282 RID: 642
		private readonly HttpWebRequest request;

		// Token: 0x04000283 RID: 643
		private readonly MonoTlsSettings settings;

		// Token: 0x04000284 RID: 644
		private SslStream sslStream;

		// Token: 0x04000285 RID: 645
		private readonly object sslStreamLock = new object();

		// Token: 0x04000286 RID: 646
		private WebExceptionStatus status;

		// Token: 0x04000287 RID: 647
		[CompilerGenerated]
		private bool <CertificateValidationFailed>k__BackingField;

		// Token: 0x020000A7 RID: 167
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CreateStream>d__18 : IAsyncStateMachine
		{
			// Token: 0x06000348 RID: 840 RVA: 0x00009B1C File Offset: 0x00007D1C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				MonoTlsStream monoTlsStream = this.<>4__this;
				Stream sslStream;
				try
				{
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_16C;
						}
						this.<socket>5__2 = monoTlsStream.networkStream.InternalSocket;
						monoTlsStream.sslStream = new SslStream(monoTlsStream.networkStream, false, monoTlsStream.provider, monoTlsStream.settings);
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							string text = monoTlsStream.request.Host;
							if (!string.IsNullOrEmpty(text))
							{
								int num2 = text.IndexOf(':');
								if (num2 > 0)
								{
									text = text.Substring(0, num2);
								}
							}
							awaiter = monoTlsStream.sslStream.AuthenticateAsClientAsync(text, monoTlsStream.request.ClientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, MonoTlsStream.<CreateStream>d__18>(ref awaiter, ref this);
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
						monoTlsStream.status = WebExceptionStatus.Success;
						monoTlsStream.request.ServicePoint.UpdateClientCertificate(monoTlsStream.sslStream.LocalCertificate);
					}
					catch (Exception)
					{
						if (this.<socket>5__2.CleanedUp)
						{
							monoTlsStream.status = WebExceptionStatus.RequestCanceled;
						}
						else if (monoTlsStream.CertificateValidationFailed)
						{
							monoTlsStream.status = WebExceptionStatus.TrustFailure;
						}
						else
						{
							monoTlsStream.status = WebExceptionStatus.SecureChannelFailure;
						}
						monoTlsStream.request.ServicePoint.UpdateClientCertificate(null);
						monoTlsStream.CloseSslStream();
						throw;
					}
					IL_16C:
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 1)
						{
							WebConnectionTunnel webConnectionTunnel = this.tunnel;
							if (((webConnectionTunnel != null) ? webConnectionTunnel.Data : null) == null)
							{
								goto IL_211;
							}
							awaiter = monoTlsStream.sslStream.WriteAsync(this.tunnel.Data, 0, this.tunnel.Data.Length, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, MonoTlsStream.<CreateStream>d__18>(ref awaiter, ref this);
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
						IL_211:;
					}
					catch
					{
						monoTlsStream.status = WebExceptionStatus.SendFailure;
						monoTlsStream.CloseSslStream();
						throw;
					}
					sslStream = monoTlsStream.sslStream;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<socket>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<socket>5__2 = null;
				this.<>t__builder.SetResult(sslStream);
			}

			// Token: 0x06000349 RID: 841 RVA: 0x00009DDC File Offset: 0x00007FDC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000288 RID: 648
			public int <>1__state;

			// Token: 0x04000289 RID: 649
			public AsyncTaskMethodBuilder<Stream> <>t__builder;

			// Token: 0x0400028A RID: 650
			public MonoTlsStream <>4__this;

			// Token: 0x0400028B RID: 651
			public WebConnectionTunnel tunnel;

			// Token: 0x0400028C RID: 652
			public CancellationToken cancellationToken;

			// Token: 0x0400028D RID: 653
			private Socket <socket>5__2;

			// Token: 0x0400028E RID: 654
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
