using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Net.Security.Private;
using Mono.Security.Interface;

namespace System.Net.Security
{
	/// <summary>Provides a stream used for client-server communication that uses the Secure Socket Layer (SSL) security protocol to authenticate the server and optionally the client.</summary>
	// Token: 0x02000863 RID: 2147
	public class SslStream : AuthenticatedStream
	{
		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x000EC975 File Offset: 0x000EAB75
		internal MobileAuthenticatedStream Impl
		{
			get
			{
				this.CheckDisposed();
				return this.impl;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x000EC983 File Offset: 0x000EAB83
		internal MonoTlsProvider Provider
		{
			get
			{
				this.CheckDisposed();
				return this.provider;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x000EC991 File Offset: 0x000EAB91
		internal string InternalTargetHost
		{
			get
			{
				this.CheckDisposed();
				return this.impl.TargetHost;
			}
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x000EC9A4 File Offset: 0x000EABA4
		private static MobileTlsProvider GetProvider()
		{
			return (MobileTlsProvider)Mono.Security.Interface.MonoTlsProviderFactory.GetProvider();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.SslStream" /> class using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="innerStream" /> is not readable.  
		/// -or-  
		/// <paramref name="innerStream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06004457 RID: 17495 RVA: 0x000EC9B0 File Offset: 0x000EABB0
		public SslStream(Stream innerStream) : this(innerStream, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.SslStream" /> class using the specified <see cref="T:System.IO.Stream" /> and stream closure behavior.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A Boolean value that indicates the closure behavior of the <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data. This parameter indicates if the inner stream is left open.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="innerStream" /> is not readable.  
		/// -or-  
		/// <paramref name="innerStream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06004458 RID: 17496 RVA: 0x000EC9BA File Offset: 0x000EABBA
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen) : base(innerStream, leaveInnerStreamOpen)
		{
			this.provider = SslStream.GetProvider();
			this.settings = MonoTlsSettings.CopyDefaultSettings();
			this.impl = this.provider.CreateSslStream(this, innerStream, leaveInnerStreamOpen, this.settings);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.SslStream" /> class using the specified <see cref="T:System.IO.Stream" />, stream closure behavior and certificate validation delegate.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A Boolean value that indicates the closure behavior of the <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data. This parameter indicates if the inner stream is left open.</param>
		/// <param name="userCertificateValidationCallback">A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" /> delegate responsible for validating the certificate supplied by the remote party.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="innerStream" /> is not readable.  
		/// -or-  
		/// <paramref name="innerStream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06004459 RID: 17497 RVA: 0x000EC9F4 File Offset: 0x000EABF4
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback) : this(innerStream, leaveInnerStreamOpen, userCertificateValidationCallback, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.SslStream" /> class using the specified <see cref="T:System.IO.Stream" />, stream closure behavior, certificate validation delegate and certificate selection delegate.</summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A Boolean value that indicates the closure behavior of the <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data. This parameter indicates if the inner stream is left open.</param>
		/// <param name="userCertificateValidationCallback">A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" /> delegate responsible for validating the certificate supplied by the remote party.</param>
		/// <param name="userCertificateSelectionCallback">A <see cref="T:System.Net.Security.LocalCertificateSelectionCallback" /> delegate responsible for selecting the certificate used for authentication.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="innerStream" /> is not readable.  
		/// -or-  
		/// <paramref name="innerStream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x0600445A RID: 17498 RVA: 0x000ECA00 File Offset: 0x000EAC00
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback, LocalCertificateSelectionCallback userCertificateSelectionCallback) : base(innerStream, leaveInnerStreamOpen)
		{
			this.provider = SslStream.GetProvider();
			this.settings = MonoTlsSettings.CopyDefaultSettings();
			this.SetAndVerifyValidationCallback(userCertificateValidationCallback);
			this.SetAndVerifySelectionCallback(userCertificateSelectionCallback);
			this.impl = this.provider.CreateSslStream(this, innerStream, leaveInnerStreamOpen, this.settings);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.SslStream" /> class using the specified <see cref="T:System.IO.Stream" /></summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A Boolean value that indicates the closure behavior of the <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.SslStream" /> for sending and receiving data. This parameter indicates if the inner stream is left open.</param>
		/// <param name="userCertificateValidationCallback">A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" /> delegate responsible for validating the certificate supplied by the remote party.</param>
		/// <param name="userCertificateSelectionCallback">A <see cref="T:System.Net.Security.LocalCertificateSelectionCallback" /> delegate responsible for selecting the certificate used for authentication.</param>
		/// <param name="encryptionPolicy">The <see cref="T:System.Net.Security.EncryptionPolicy" /> to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="innerStream" /> is not readable.  
		/// -or-  
		/// <paramref name="innerStream" /> is not writable.  
		/// -or-  
		/// <paramref name="encryptionPolicy" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x0600445B RID: 17499 RVA: 0x000ECA54 File Offset: 0x000EAC54
		[MonoLimitation("encryptionPolicy is ignored")]
		public SslStream(Stream innerStream, bool leaveInnerStreamOpen, RemoteCertificateValidationCallback userCertificateValidationCallback, LocalCertificateSelectionCallback userCertificateSelectionCallback, EncryptionPolicy encryptionPolicy) : this(innerStream, leaveInnerStreamOpen, userCertificateValidationCallback, userCertificateSelectionCallback)
		{
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x000ECA61 File Offset: 0x000EAC61
		internal SslStream(Stream innerStream, bool leaveInnerStreamOpen, MonoTlsProvider provider, MonoTlsSettings settings) : base(innerStream, leaveInnerStreamOpen)
		{
			this.provider = (MobileTlsProvider)provider;
			this.settings = settings.Clone();
			this.explicitSettings = true;
			this.impl = this.provider.CreateSslStream(this, innerStream, leaveInnerStreamOpen, settings);
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x000ECAA1 File Offset: 0x000EACA1
		internal static IMonoSslStream CreateMonoSslStream(Stream innerStream, bool leaveInnerStreamOpen, MobileTlsProvider provider, MonoTlsSettings settings)
		{
			return new SslStream(innerStream, leaveInnerStreamOpen, provider, settings).Impl;
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x000ECAB4 File Offset: 0x000EACB4
		private void SetAndVerifyValidationCallback(RemoteCertificateValidationCallback callback)
		{
			if (this.validationCallback == null)
			{
				this.validationCallback = callback;
				this.settings.RemoteCertificateValidationCallback = CallbackHelpers.PublicToMono(callback);
				return;
			}
			if ((callback != null && this.validationCallback != callback) || (this.explicitSettings & this.settings.RemoteCertificateValidationCallback != null))
			{
				throw new InvalidOperationException(SR.Format("The '{0}' option was already set in the SslStream constructor.", "RemoteCertificateValidationCallback"));
			}
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x000ECB20 File Offset: 0x000EAD20
		private void SetAndVerifySelectionCallback(LocalCertificateSelectionCallback callback)
		{
			if (this.selectionCallback == null)
			{
				this.selectionCallback = callback;
				if (callback == null)
				{
					this.settings.ClientCertificateSelectionCallback = null;
					return;
				}
				this.settings.ClientCertificateSelectionCallback = ((string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(this, t, lc, rc, ai));
				return;
			}
			else
			{
				if ((callback != null && this.selectionCallback != callback) || (this.explicitSettings && this.settings.ClientCertificateSelectionCallback != null))
				{
					throw new InvalidOperationException(SR.Format("The '{0}' option was already set in the SslStream constructor.", "LocalCertificateSelectionCallback"));
				}
				return;
			}
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x000ECBC8 File Offset: 0x000EADC8
		private MonoSslServerAuthenticationOptions CreateAuthenticationOptions(SslServerAuthenticationOptions sslServerAuthenticationOptions)
		{
			if (sslServerAuthenticationOptions.ServerCertificate == null && sslServerAuthenticationOptions.ServerCertificateSelectionCallback == null && this.selectionCallback == null)
			{
				throw new ArgumentNullException("ServerCertificate");
			}
			if ((sslServerAuthenticationOptions.ServerCertificate != null || this.selectionCallback != null) && sslServerAuthenticationOptions.ServerCertificateSelectionCallback != null)
			{
				throw new InvalidOperationException(SR.Format("The '{0}' option was already set in the SslStream constructor.", "ServerCertificateSelectionCallback"));
			}
			MonoSslServerAuthenticationOptions monoSslServerAuthenticationOptions = new MonoSslServerAuthenticationOptions(sslServerAuthenticationOptions);
			ServerCertificateSelectionCallback serverSelectionCallback = sslServerAuthenticationOptions.ServerCertificateSelectionCallback;
			if (serverSelectionCallback != null)
			{
				monoSslServerAuthenticationOptions.ServerCertSelectionDelegate = ((string x) => serverSelectionCallback(this, x));
			}
			return monoSslServerAuthenticationOptions;
		}

		/// <summary>Called by clients to authenticate the server and optionally the client in a client-server connection.</summary>
		/// <param name="targetHost">The name of the server that shares this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetHost" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Server authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004461 RID: 17505 RVA: 0x000ECC60 File Offset: 0x000EAE60
		public virtual void AuthenticateAsClient(string targetHost)
		{
			this.AuthenticateAsClient(targetHost, new X509CertificateCollection(), SslProtocols.None, false);
		}

		/// <summary>Called by clients to authenticate the server and optionally the client in a client-server connection. The authentication process uses the specified certificate collection, and the system default SSL protocol.</summary>
		/// <param name="targetHost">The name of the server that will share this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="clientCertificates">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains client certificates.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		// Token: 0x06004462 RID: 17506 RVA: 0x000ECC70 File Offset: 0x000EAE70
		public virtual void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, bool checkCertificateRevocation)
		{
			this.AuthenticateAsClient(targetHost, clientCertificates, SslProtocols.None, checkCertificateRevocation);
		}

		/// <summary>Called by clients to authenticate the server and optionally the client in a client-server connection. The authentication process uses the specified certificate collection and SSL protocol.</summary>
		/// <param name="targetHost">The name of the server that will share this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="clientCertificates">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains client certificates.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		// Token: 0x06004463 RID: 17507 RVA: 0x000ECC7C File Offset: 0x000EAE7C
		public virtual void AuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this.Impl.AuthenticateAsClient(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation);
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the server and optionally the client.</summary>
		/// <param name="targetHost">The name of the server that shares this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object that contains information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetHost" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Server authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004464 RID: 17508 RVA: 0x000ECC8E File Offset: 0x000EAE8E
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(targetHost, new X509CertificateCollection(), SslProtocols.None, false, asyncCallback, asyncState);
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the server and optionally the client using the specified certificates and the system default security protocol.</summary>
		/// <param name="targetHost">The name of the server that shares this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="clientCertificates">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> containing client certificates.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object that contains information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetHost" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Server authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004465 RID: 17509 RVA: 0x000ECCA0 File Offset: 0x000EAEA0
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsClient(targetHost, clientCertificates, SslProtocols.None, checkCertificateRevocation, asyncCallback, asyncState);
		}

		/// <summary>Called by clients to begin an asynchronous operation to authenticate the server and optionally the client using the specified certificates and security protocol.</summary>
		/// <param name="targetHost">The name of the server that shares this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="clientCertificates">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> containing client certificates.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object that contains information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetHost" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enabledSslProtocols" /> is not a valid <see cref="T:System.Security.Authentication.SslProtocols" /> value.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Server authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004466 RID: 17510 RVA: 0x000ECCB0 File Offset: 0x000EAEB0
		public virtual IAsyncResult BeginAuthenticateAsClient(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.Impl.AuthenticateAsClientAsync(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation), asyncCallback, asyncState);
		}

		/// <summary>Ends a pending asynchronous server authentication operation started with a previous call to <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsClient" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsClient" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsClient" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending server authentication to complete.</exception>
		// Token: 0x06004467 RID: 17511 RVA: 0x0009571A File Offset: 0x0009391A
		public virtual void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		/// <summary>Called by servers to authenticate the server and optionally the client in a client-server connection using the specified certificate.</summary>
		/// <param name="serverCertificate">The certificate used to authenticate the server.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverCertificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Client authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="Overload:System.Net.Security.SslStream.AuthenticateAsServer" /> method is not supported on Windows 95, Windows 98, or Windows Millennium.</exception>
		// Token: 0x06004468 RID: 17512 RVA: 0x000ECCCB File Offset: 0x000EAECB
		public virtual void AuthenticateAsServer(X509Certificate serverCertificate)
		{
			this.Impl.AuthenticateAsServer(serverCertificate, false, SslProtocols.None, false);
		}

		/// <summary>Called by servers to authenticate the server and optionally the client in a client-server connection using the specified certificates and requirements, and using the sytem default security protocol.</summary>
		/// <param name="serverCertificate">The X509Certificate used to authenticate the server.</param>
		/// <param name="clientCertificateRequired">A <see cref="T:System.Boolean" /> value that specifies whether the client is asked for a certificate for authentication. Note that this is only a request -- if no certificate is provided, the server still accepts the connection request.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverCertificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Client authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="Overload:System.Net.Security.SslStream.AuthenticateAsServer" /> method is not supported on Windows 95, Windows 98, or Windows Millennium.</exception>
		// Token: 0x06004469 RID: 17513 RVA: 0x000ECCDC File Offset: 0x000EAEDC
		public virtual void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, bool checkCertificateRevocation)
		{
			this.Impl.AuthenticateAsServer(serverCertificate, clientCertificateRequired, SslProtocols.None, checkCertificateRevocation);
		}

		/// <summary>Called by servers to authenticate the server and optionally the client in a client-server connection using the specified certificates, requirements and security protocol.</summary>
		/// <param name="serverCertificate">The X509Certificate used to authenticate the server.</param>
		/// <param name="clientCertificateRequired">A <see cref="T:System.Boolean" /> value that specifies whether the client is asked for a certificate for authentication. Note that this is only a request -- if no certificate is provided, the server still accepts the connection request.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverCertificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enabledSslProtocols" /> is not a valid <see cref="T:System.Security.Authentication.SslProtocols" /> value.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Client authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="Overload:System.Net.Security.SslStream.AuthenticateAsServer" /> method is not supported on Windows 95, Windows 98, or Windows Millennium.</exception>
		// Token: 0x0600446A RID: 17514 RVA: 0x000ECCED File Offset: 0x000EAEED
		public virtual void AuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			this.Impl.AuthenticateAsServer(serverCertificate, clientCertificateRequired, enabledSslProtocols, checkCertificateRevocation);
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the client and optionally the server in a client-server connection.</summary>
		/// <param name="serverCertificate">The X509Certificate used to authenticate the server.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object that contains information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverCertificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Client authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsServer" /> method is not supported on Windows 95, Windows 98, or Windows Millennium.</exception>
		// Token: 0x0600446B RID: 17515 RVA: 0x000ECCFF File Offset: 0x000EAEFF
		public virtual IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(serverCertificate, false, SslProtocols.None, false, asyncCallback, asyncState);
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the server and optionally the client using the specified certificates and requirements, and the system default security protocol.</summary>
		/// <param name="serverCertificate">The X509Certificate used to authenticate the server.</param>
		/// <param name="clientCertificateRequired">A <see cref="T:System.Boolean" /> value that specifies whether the client is asked for a certificate for authentication. Note that this is only a request -- if no certificate is provided, the server still accepts the connection request.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object that contains information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverCertificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Server authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsServer" /> method is not supported on Windows 95, Windows 98, or Windows Millennium.</exception>
		// Token: 0x0600446C RID: 17516 RVA: 0x000ECD0D File Offset: 0x000EAF0D
		public virtual IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginAuthenticateAsServer(serverCertificate, clientCertificateRequired, SslProtocols.None, checkCertificateRevocation, asyncCallback, asyncState);
		}

		/// <summary>Called by servers to begin an asynchronous operation to authenticate the server and optionally the client using the specified certificates, requirements and security protocol.</summary>
		/// <param name="serverCertificate">The X509Certificate used to authenticate the server.</param>
		/// <param name="clientCertificateRequired">A <see cref="T:System.Boolean" /> value that specifies whether the client is asked for a certificate for authentication. Note that this is only a request -- if no certificate is provided, the server still accepts the connection request.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the authentication is complete.</param>
		/// <param name="asyncState">A user-defined object that contains information about the operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverCertificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enabledSslProtocols" /> is not a valid <see cref="T:System.Security.Authentication.SslProtocols" /> value.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Server authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsServer" /> method is not supported on Windows 95, Windows 98, or Windows Millennium.</exception>
		// Token: 0x0600446D RID: 17517 RVA: 0x000ECD1D File Offset: 0x000EAF1D
		public virtual IAsyncResult BeginAuthenticateAsServer(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.Impl.AuthenticateAsServerAsync(serverCertificate, clientCertificateRequired, enabledSslProtocols, checkCertificateRevocation), asyncCallback, asyncState);
		}

		/// <summary>Ends a pending asynchronous client authentication operation started with a previous call to <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsClient" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsClient" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="Overload:System.Net.Security.SslStream.BeginAuthenticateAsClient" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending client authentication to complete.</exception>
		// Token: 0x0600446E RID: 17518 RVA: 0x0009571A File Offset: 0x0009391A
		public virtual void EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		/// <summary>Gets the <see cref="T:System.Net.TransportContext" /> used for authentication using extended protection.</summary>
		/// <returns>The <see cref="T:System.Net.TransportContext" /> object that contains the channel binding token (CBT) used for extended protection.</returns>
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x00002F6A File Offset: 0x0000116A
		public TransportContext TransportContext
		{
			get
			{
				return null;
			}
		}

		/// <summary>Called by clients to authenticate the server and optionally the client in a client-server connection as an asynchronous operation.</summary>
		/// <param name="targetHost">The name of the server that shares this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetHost" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Server authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x06004470 RID: 17520 RVA: 0x000ECD38 File Offset: 0x000EAF38
		public virtual Task AuthenticateAsClientAsync(string targetHost)
		{
			return this.Impl.AuthenticateAsClientAsync(targetHost, new X509CertificateCollection(), SslProtocols.None, false);
		}

		/// <summary>Called by clients to authenticate the server and optionally the client in a client-server connection as an asynchronous operation. The authentication process uses the specified certificate collection and the system default SSL protocol.</summary>
		/// <param name="targetHost">The name of the server that will share this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="clientCertificates">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains client certificates.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06004471 RID: 17521 RVA: 0x000ECD4D File Offset: 0x000EAF4D
		public virtual Task AuthenticateAsClientAsync(string targetHost, X509CertificateCollection clientCertificates, bool checkCertificateRevocation)
		{
			return this.Impl.AuthenticateAsClientAsync(targetHost, clientCertificates, SslProtocols.None, checkCertificateRevocation);
		}

		/// <summary>Called by clients to authenticate the server and optionally the client in a client-server connection as an asynchronous operation. The authentication process uses the specified certificate collection and SSL protocol.</summary>
		/// <param name="targetHost">The name of the server that will share this <see cref="T:System.Net.Security.SslStream" />.</param>
		/// <param name="clientCertificates">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that contains client certificates.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06004472 RID: 17522 RVA: 0x000ECD5E File Offset: 0x000EAF5E
		public virtual Task AuthenticateAsClientAsync(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			return this.Impl.AuthenticateAsClientAsync(targetHost, clientCertificates, enabledSslProtocols, checkCertificateRevocation);
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x000ECD70 File Offset: 0x000EAF70
		public Task AuthenticateAsClientAsync(SslClientAuthenticationOptions sslClientAuthenticationOptions, CancellationToken cancellationToken)
		{
			this.SetAndVerifyValidationCallback(sslClientAuthenticationOptions.RemoteCertificateValidationCallback);
			this.SetAndVerifySelectionCallback(sslClientAuthenticationOptions.LocalCertificateSelectionCallback);
			return this.Impl.AuthenticateAsClientAsync(new MonoSslClientAuthenticationOptions(sslClientAuthenticationOptions), cancellationToken);
		}

		/// <summary>Called by servers to authenticate the server and optionally the client in a client-server connection using the specified certificate as an asynchronous operation.</summary>
		/// <param name="serverCertificate">The certificate used to authenticate the server.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverCertificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The authentication failed and left this object in an unusable state.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has already occurred.  
		///  -or-  
		///  Client authentication using this <see cref="T:System.Net.Security.SslStream" /> was tried previously.  
		///  -or-  
		///  Authentication is already in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="Overload:System.Net.Security.SslStream.AuthenticateAsServerAsync" /> method is not supported on Windows 95, Windows 98, or Windows Millennium.</exception>
		// Token: 0x06004474 RID: 17524 RVA: 0x000ECD9C File Offset: 0x000EAF9C
		public virtual Task AuthenticateAsServerAsync(X509Certificate serverCertificate)
		{
			return this.Impl.AuthenticateAsServerAsync(serverCertificate, false, SslProtocols.None, false);
		}

		/// <summary>Called by servers to authenticate the server and optionally the client in a client-server connection using the specified certificates, requirements and security protocol as an asynchronous operation.</summary>
		/// <param name="serverCertificate">The X509Certificate used to authenticate the server.</param>
		/// <param name="clientCertificateRequired">A <see cref="T:System.Boolean" /> value that specifies whether the client is asked for a certificate for authentication. Note that this is only a request -- if no certificate is provided, the server still accepts the connection request.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06004475 RID: 17525 RVA: 0x000ECDAD File Offset: 0x000EAFAD
		public virtual Task AuthenticateAsServerAsync(X509Certificate serverCertificate, bool clientCertificateRequired, bool checkCertificateRevocation)
		{
			return this.Impl.AuthenticateAsServerAsync(serverCertificate, clientCertificateRequired, SslProtocols.None, checkCertificateRevocation);
		}

		/// <summary>Called by servers to authenticate the server and optionally the client in a client-server connection using the specified certificates, requirements and security protocol as an asynchronous operation.</summary>
		/// <param name="serverCertificate">The X509Certificate used to authenticate the server.</param>
		/// <param name="clientCertificateRequired">A <see cref="T:System.Boolean" /> value that specifies whether the client is asked for a certificate for authentication. Note that this is only a request -- if no certificate is provided, the server still accepts the connection request.</param>
		/// <param name="enabledSslProtocols">The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</param>
		/// <param name="checkCertificateRevocation">A <see cref="T:System.Boolean" /> value that specifies whether the certificate revocation list is checked during authentication.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06004476 RID: 17526 RVA: 0x000ECDBE File Offset: 0x000EAFBE
		public virtual Task AuthenticateAsServerAsync(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
		{
			return this.Impl.AuthenticateAsServerAsync(serverCertificate, clientCertificateRequired, enabledSslProtocols, checkCertificateRevocation);
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x000ECDD0 File Offset: 0x000EAFD0
		public Task AuthenticateAsServerAsync(SslServerAuthenticationOptions sslServerAuthenticationOptions, CancellationToken cancellationToken)
		{
			return this.Impl.AuthenticateAsServerAsync(this.CreateAuthenticationOptions(sslServerAuthenticationOptions), cancellationToken);
		}

		/// <summary>Shuts down this SslStream.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06004478 RID: 17528 RVA: 0x000ECDE5 File Offset: 0x000EAFE5
		public virtual Task ShutdownAsync()
		{
			return this.Impl.ShutdownAsync();
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether authentication was successful.</summary>
		/// <returns>
		///   <see langword="true" /> if successful authentication occurred; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06004479 RID: 17529 RVA: 0x000ECDF2 File Offset: 0x000EAFF2
		public override bool IsAuthenticated
		{
			get
			{
				return this.Impl.IsAuthenticated;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both server and client have been authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the server has been authenticated; otherwise <see langword="false" />.</returns>
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x000ECDFF File Offset: 0x000EAFFF
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				return this.Impl.IsMutuallyAuthenticated;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this <see cref="T:System.Net.Security.SslStream" /> uses data encryption.</summary>
		/// <returns>
		///   <see langword="true" /> if data is encrypted before being transmitted over the network and decrypted when it reaches the remote endpoint; otherwise <see langword="false" />.</returns>
		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x000ECE0C File Offset: 0x000EB00C
		public override bool IsEncrypted
		{
			get
			{
				return this.Impl.IsEncrypted;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the data sent using this stream is signed.</summary>
		/// <returns>
		///   <see langword="true" /> if the data is signed before being transmitted; otherwise <see langword="false" />.</returns>
		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x0600447C RID: 17532 RVA: 0x000ECE19 File Offset: 0x000EB019
		public override bool IsSigned
		{
			get
			{
				return this.Impl.IsSigned;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the local side of the connection used by this <see cref="T:System.Net.Security.SslStream" /> was authenticated as the server.</summary>
		/// <returns>
		///   <see langword="true" /> if the local endpoint was successfully authenticated as the server side of the authenticated connection; otherwise <see langword="false" />.</returns>
		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x000ECE26 File Offset: 0x000EB026
		public override bool IsServer
		{
			get
			{
				return this.Impl.IsServer;
			}
		}

		/// <summary>Gets a value that indicates the security protocol used to authenticate this connection.</summary>
		/// <returns>The <see cref="T:System.Security.Authentication.SslProtocols" /> value that represents the protocol used for authentication.</returns>
		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x000ECE33 File Offset: 0x000EB033
		public virtual SslProtocols SslProtocol
		{
			get
			{
				return this.Impl.SslProtocol;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the certificate revocation list is checked during the certificate validation process.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate revocation list is checked; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x000ECE40 File Offset: 0x000EB040
		public virtual bool CheckCertRevocationStatus
		{
			get
			{
				return this.Impl.CheckCertRevocationStatus;
			}
		}

		/// <summary>Gets the certificate used to authenticate the local endpoint.</summary>
		/// <returns>An X509Certificate object that represents the certificate supplied for authentication or <see langword="null" /> if no certificate was supplied.</returns>
		/// <exception cref="T:System.InvalidOperationException">Authentication failed or has not occurred.</exception>
		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x000ECE4D File Offset: 0x000EB04D
		public virtual X509Certificate LocalCertificate
		{
			get
			{
				return this.Impl.LocalCertificate;
			}
		}

		/// <summary>Gets the certificate used to authenticate the remote endpoint.</summary>
		/// <returns>An X509Certificate object that represents the certificate supplied for authentication or <see langword="null" /> if no certificate was supplied.</returns>
		/// <exception cref="T:System.InvalidOperationException">Authentication failed or has not occurred.</exception>
		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x000ECE5A File Offset: 0x000EB05A
		public virtual X509Certificate RemoteCertificate
		{
			get
			{
				return this.Impl.RemoteCertificate;
			}
		}

		/// <summary>Gets a value that identifies the bulk encryption algorithm used by this <see cref="T:System.Net.Security.SslStream" />.</summary>
		/// <returns>A value that identifies the bulk encryption algorithm used by this <see cref="T:System.Net.Security.SslStream" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Net.Security.SslStream.CipherAlgorithm" /> property was accessed before the completion of the authentication process or the authentication process failed.</exception>
		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x000ECE67 File Offset: 0x000EB067
		public virtual System.Security.Authentication.CipherAlgorithmType CipherAlgorithm
		{
			get
			{
				return this.Impl.CipherAlgorithm;
			}
		}

		/// <summary>Gets a value that identifies the strength of the cipher algorithm used by this <see cref="T:System.Net.Security.SslStream" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the strength of the algorithm, in bits.</returns>
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x000ECE74 File Offset: 0x000EB074
		public virtual int CipherStrength
		{
			get
			{
				return this.Impl.CipherStrength;
			}
		}

		/// <summary>Gets the algorithm used for generating message authentication codes (MACs).</summary>
		/// <returns>The algorithm used for generating message authentication codes (MACs).</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Net.Security.SslStream.HashAlgorithm" /> property was accessed before the completion of the authentication process or the authentication process failed.</exception>
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06004484 RID: 17540 RVA: 0x000ECE81 File Offset: 0x000EB081
		public virtual System.Security.Authentication.HashAlgorithmType HashAlgorithm
		{
			get
			{
				return this.Impl.HashAlgorithm;
			}
		}

		/// <summary>Gets a value that identifies the strength of the hash algorithm used by this instance.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the strength of the <see cref="T:System.Security.Authentication.HashAlgorithmType" /> algorithm, in bits. Valid values are 128 or 160.</returns>
		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06004485 RID: 17541 RVA: 0x000ECE8E File Offset: 0x000EB08E
		public virtual int HashStrength
		{
			get
			{
				return this.Impl.HashStrength;
			}
		}

		/// <summary>Gets the key exchange algorithm used by this <see cref="T:System.Net.Security.SslStream" />.</summary>
		/// <returns>An <see cref="T:System.Security.Authentication.ExchangeAlgorithmType" /> value.</returns>
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x000ECE9B File Offset: 0x000EB09B
		public virtual System.Security.Authentication.ExchangeAlgorithmType KeyExchangeAlgorithm
		{
			get
			{
				return this.Impl.KeyExchangeAlgorithm;
			}
		}

		/// <summary>Gets a value that identifies the strength of the key exchange algorithm used by this instance.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the strength of the <see cref="T:System.Security.Authentication.ExchangeAlgorithmType" /> algorithm, in bits.</returns>
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x000ECEA8 File Offset: 0x000EB0A8
		public virtual int KeyExchangeStrength
		{
			get
			{
				return this.Impl.KeyExchangeStrength;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06004488 RID: 17544 RVA: 0x000ECEB5 File Offset: 0x000EB0B5
		public SslApplicationProtocol NegotiatedApplicationProtocol
		{
			get
			{
				throw new PlatformNotSupportedException("https://github.com/mono/mono/issues/12880");
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is seekable.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06004489 RID: 17545 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is readable.</summary>
		/// <returns>
		///   <see langword="true" /> if authentication has occurred and the underlying stream is readable; otherwise <see langword="false" />.</returns>
		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x0600448A RID: 17546 RVA: 0x000ECEC1 File Offset: 0x000EB0C1
		public override bool CanRead
		{
			get
			{
				return this.impl != null && this.impl.CanRead;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream supports time-outs.</summary>
		/// <returns>
		///   <see langword="true" /> if the underlying stream supports time-outs; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x0600448B RID: 17547 RVA: 0x00008055 File Offset: 0x00006255
		public override bool CanTimeout
		{
			get
			{
				return base.InnerStream.CanTimeout;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the underlying stream is writable.</summary>
		/// <returns>
		///   <see langword="true" /> if authentication has occurred and the underlying stream is writable; otherwise <see langword="false" />.</returns>
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x000ECED8 File Offset: 0x000EB0D8
		public override bool CanWrite
		{
			get
			{
				return this.impl != null && this.impl.CanWrite;
			}
		}

		/// <summary>Gets or sets the amount of time a read operation blocks waiting for data.</summary>
		/// <returns>The amount of time that elapses before a synchronous read operation fails.</returns>
		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x0600448D RID: 17549 RVA: 0x000ECEEF File Offset: 0x000EB0EF
		// (set) Token: 0x0600448E RID: 17550 RVA: 0x000ECEFC File Offset: 0x000EB0FC
		public override int ReadTimeout
		{
			get
			{
				return this.Impl.ReadTimeout;
			}
			set
			{
				this.Impl.ReadTimeout = value;
			}
		}

		/// <summary>Gets or sets the amount of time a write operation blocks waiting for data.</summary>
		/// <returns>The amount of time that elapses before a synchronous write operation fails.</returns>
		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x000ECF0A File Offset: 0x000EB10A
		// (set) Token: 0x06004490 RID: 17552 RVA: 0x000ECF17 File Offset: 0x000EB117
		public override int WriteTimeout
		{
			get
			{
				return this.Impl.WriteTimeout;
			}
			set
			{
				this.Impl.WriteTimeout = value;
			}
		}

		/// <summary>Gets the length of the underlying stream.</summary>
		/// <returns>The length of the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06004491 RID: 17553 RVA: 0x000ECF25 File Offset: 0x000EB125
		public override long Length
		{
			get
			{
				return this.Impl.Length;
			}
		}

		/// <summary>Gets or sets the current position in the underlying stream.</summary>
		/// <returns>The current position in the underlying stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Setting this property is not supported.  
		///  -or-  
		///  Getting the value of this property is not supported when the underlying stream is a <see cref="T:System.Net.Sockets.NetworkStream" />.</exception>
		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x000ECF32 File Offset: 0x000EB132
		// (set) Token: 0x06004493 RID: 17555 RVA: 0x000ECF3F File Offset: 0x000EB13F
		public override long Position
		{
			get
			{
				return this.Impl.Position;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("This stream does not support seek operations."));
			}
		}

		/// <summary>Sets the length of the underlying stream.</summary>
		/// <param name="value">An <see cref="T:System.Int64" /> value that specifies the length of the stream.</param>
		// Token: 0x06004494 RID: 17556 RVA: 0x000ECF50 File Offset: 0x000EB150
		public override void SetLength(long value)
		{
			this.Impl.SetLength(value);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="offset">This value is ignored.</param>
		/// <param name="origin">This value is ignored.</param>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Seeking is not supported by <see cref="T:System.Net.Security.SslStream" /> objects.</exception>
		// Token: 0x06004495 RID: 17557 RVA: 0x000ECF3F File Offset: 0x000EB13F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("This stream does not support seek operations."));
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x000ECF5E File Offset: 0x000EB15E
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return base.InnerStream.FlushAsync(cancellationToken);
		}

		/// <summary>Causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06004497 RID: 17559 RVA: 0x00007E6C File Offset: 0x0000606C
		public override void Flush()
		{
			base.InnerStream.Flush();
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x000ECF6C File Offset: 0x000EB16C
		private void CheckDisposed()
		{
			if (this.impl == null)
			{
				throw new ObjectDisposedException("SslStream");
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Security.SslStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06004499 RID: 17561 RVA: 0x000ECF84 File Offset: 0x000EB184
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.impl != null && disposing)
				{
					this.impl.Dispose();
					this.impl = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Reads data from this stream and stores it in the specified array.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from this stream.</param>
		/// <param name="offset">A <see cref="T:System.Int32" /> that contains the zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> that contains the maximum number of bytes to read from this stream.</param>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read. When there is no more data to be read, returns 0.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" />
		///   <paramref name="&lt;" />
		///   <paramref name="0" />.  
		/// <paramref name="-or-" /><paramref name="offset" /> &gt; the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="offset" /> + count &gt; the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed. Check the inner exception, if present to determine the cause of the failure.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a read operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x0600449A RID: 17562 RVA: 0x000ECFCC File Offset: 0x000EB1CC
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.Impl.Read(buffer, offset, count);
		}

		/// <summary>Writes the specified data to this stream.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes written to the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x0600449B RID: 17563 RVA: 0x000ECFDC File Offset: 0x000EB1DC
		public void Write(byte[] buffer)
		{
			this.Impl.Write(buffer);
		}

		/// <summary>Write the specified number of <see cref="T:System.Byte" />s to the underlying stream using the specified buffer and offset.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes written to the stream.</param>
		/// <param name="offset">A <see cref="T:System.Int32" /> that contains the zero-based location in <paramref name="buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">A <see cref="T:System.Int32" /> that contains the number of bytes to read from <paramref name="buffer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" />
		///   <paramref name="&lt;" />
		///   <paramref name="0" />.  
		/// <paramref name="-or-" /><paramref name="offset" /> &gt; the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="offset" /> + count &gt; the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x0600449C RID: 17564 RVA: 0x000ECFEF File Offset: 0x000EB1EF
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.Impl.Write(buffer, offset, count);
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x000ECFFF File Offset: 0x000EB1FF
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.Impl.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x000ED011 File Offset: 0x000EB211
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.Impl.WriteAsync(buffer, offset, count, cancellationToken);
		}

		/// <summary>Begins an asynchronous read operation that reads data from the stream and stores it in the specified array.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that receives the bytes read from the stream.</param>
		/// <param name="offset">The zero-based location in <paramref name="buffer" /> at which to begin storing the data read from this stream.</param>
		/// <param name="count">The maximum number of bytes to read from the stream.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the read operation is complete.</param>
		/// <param name="asyncState">A user-defined object that contains information about the read operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" />
		///   <paramref name="&lt;" />
		///   <paramref name="0" />.  
		/// <paramref name="-or-" /><paramref name="offset" /> &gt; the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="offset" /> + count &gt; the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.  
		///  -or-  
		///  Encryption is in use, but the data could not be decrypted.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a read operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x0600449F RID: 17567 RVA: 0x000ED023 File Offset: 0x000EB223
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.Impl.ReadAsync(buffer, offset, count), callback, state);
		}

		/// <summary>Ends an asynchronous read operation started with a previous call to <see cref="M:System.Net.Security.SslStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.SslStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <returns>A <see cref="T:System.Int32" /> value that specifies the number of bytes read from the underlying stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Security.SslStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending read operation to complete.
		/// -or-
		/// Authentication has not occurred.</exception>
		/// <exception cref="T:System.IO.IOException">The read operation failed.</exception>
		// Token: 0x060044A0 RID: 17568 RVA: 0x0008F0E9 File Offset: 0x0008D2E9
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		/// <summary>Begins an asynchronous write operation that writes <see cref="T:System.Byte" />s from the specified buffer to the stream.</summary>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that supplies the bytes to be written to the stream.</param>
		/// <param name="offset">The zero-based location in <paramref name="buffer" /> at which to begin reading bytes to be written to the stream.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> value that specifies the number of bytes to read from <paramref name="buffer" />.</param>
		/// <param name="asyncCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the write operation is complete.</param>
		/// <param name="asyncState">A user-defined object that contains information about the write operation. This object is passed to the <paramref name="asyncCallback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object indicating the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" />
		///   <paramref name="&lt;" />
		///   <paramref name="0" />.  
		/// <paramref name="-or-" /><paramref name="offset" /> &gt; the length of <paramref name="buffer" />.  
		/// -or-  
		/// <paramref name="offset" /> + count &gt; the length of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		/// <exception cref="T:System.NotSupportedException">There is already a write operation in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">Authentication has not occurred.</exception>
		// Token: 0x060044A1 RID: 17569 RVA: 0x000ED03C File Offset: 0x000EB23C
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.Impl.WriteAsync(buffer, offset, count), callback, state);
		}

		/// <summary>Ends an asynchronous write operation started with a previous call to <see cref="M:System.Net.Security.SslStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> instance returned by a call to <see cref="M:System.Net.Security.SslStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not created by a call to <see cref="M:System.Net.Security.SslStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no pending write operation to complete.
		/// -or-
		/// Authentication has not occurred.</exception>
		/// <exception cref="T:System.IO.IOException">The write operation failed.</exception>
		// Token: 0x060044A2 RID: 17570 RVA: 0x0009571A File Offset: 0x0009391A
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x0400292A RID: 10538
		private MobileTlsProvider provider;

		// Token: 0x0400292B RID: 10539
		private MonoTlsSettings settings;

		// Token: 0x0400292C RID: 10540
		private RemoteCertificateValidationCallback validationCallback;

		// Token: 0x0400292D RID: 10541
		private LocalCertificateSelectionCallback selectionCallback;

		// Token: 0x0400292E RID: 10542
		private MobileAuthenticatedStream impl;

		// Token: 0x0400292F RID: 10543
		private bool explicitSettings;

		// Token: 0x02000864 RID: 2148
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x060044A3 RID: 17571 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x060044A4 RID: 17572 RVA: 0x000ED055 File Offset: 0x000EB255
			internal X509Certificate <SetAndVerifySelectionCallback>b__0(string t, X509CertificateCollection lc, X509Certificate rc, string[] ai)
			{
				return this.callback(this.<>4__this, t, lc, rc, ai);
			}

			// Token: 0x04002930 RID: 10544
			public LocalCertificateSelectionCallback callback;

			// Token: 0x04002931 RID: 10545
			public SslStream <>4__this;
		}

		// Token: 0x02000865 RID: 2149
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x060044A5 RID: 17573 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x060044A6 RID: 17574 RVA: 0x000ED06D File Offset: 0x000EB26D
			internal X509Certificate <CreateAuthenticationOptions>b__0(string x)
			{
				return this.serverSelectionCallback(this.<>4__this, x);
			}

			// Token: 0x04002932 RID: 10546
			public ServerCertificateSelectionCallback serverSelectionCallback;

			// Token: 0x04002933 RID: 10547
			public SslStream <>4__this;
		}
	}
}
