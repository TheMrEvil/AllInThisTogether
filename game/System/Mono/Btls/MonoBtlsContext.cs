using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x020000D8 RID: 216
	internal class MonoBtlsContext : MobileTlsContext, IMonoBtlsBioMono
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0000D17D File Offset: 0x0000B37D
		public MonoBtlsContext(MobileAuthenticatedStream parent, MonoSslAuthenticationOptions options) : base(parent, options)
		{
			if (base.IsServer && base.LocalServerCertificate != null)
			{
				this.nativeServerCertificate = MonoBtlsContext.GetPrivateCertificate(base.LocalServerCertificate);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		private static X509CertificateImplBtls GetPrivateCertificate(X509Certificate certificate)
		{
			X509CertificateImplBtls x509CertificateImplBtls = certificate.Impl as X509CertificateImplBtls;
			if (x509CertificateImplBtls != null)
			{
				return (X509CertificateImplBtls)x509CertificateImplBtls.Clone();
			}
			string password = Guid.NewGuid().ToString();
			X509CertificateImplBtls result;
			using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(password))
			{
				result = new X509CertificateImplBtls(certificate.Export(X509ContentType.Pfx, password), safePasswordHandle, X509KeyStorageFlags.DefaultKeySet);
			}
			return result;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000D21C File Offset: 0x0000B41C
		public new MonoBtlsProvider Provider
		{
			get
			{
				return (MonoBtlsProvider)base.Provider;
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000D22C File Offset: 0x0000B42C
		private int VerifyCallback(MonoBtlsX509StoreCtx storeCtx)
		{
			int result;
			using (X509ChainImplBtls x509ChainImplBtls = new X509ChainImplBtls(storeCtx))
			{
				using (X509Chain x509Chain = new X509Chain(x509ChainImplBtls))
				{
					X509Certificate2 certificate = x509Chain.ChainElements[0].Certificate;
					bool flag = base.ValidateCertificate(certificate, x509Chain);
					this.certificateValidated = true;
					result = (flag ? 1 : 0);
				}
			}
			return result;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		private int SelectCallback(string[] acceptableIssuers)
		{
			if (this.nativeClientCertificate != null)
			{
				return 1;
			}
			this.GetPeerCertificate();
			X509Certificate x509Certificate = base.SelectClientCertificate(acceptableIssuers);
			if (x509Certificate == null)
			{
				return 1;
			}
			this.nativeClientCertificate = MonoBtlsContext.GetPrivateCertificate(x509Certificate);
			this.clientCertificate = new X509Certificate(this.nativeClientCertificate);
			this.SetPrivateCertificate(this.nativeClientCertificate);
			return 1;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		private int ServerNameCallback()
		{
			string serverName = this.ssl.GetServerName();
			X509Certificate x509Certificate = base.SelectServerCertificate(serverName);
			if (x509Certificate == null)
			{
				return 1;
			}
			this.nativeServerCertificate = MonoBtlsContext.GetPrivateCertificate(x509Certificate);
			this.SetPrivateCertificate(this.nativeServerCertificate);
			return 1;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000D338 File Offset: 0x0000B538
		public override void StartHandshake()
		{
			this.InitializeConnection();
			this.ssl = new MonoBtlsSsl(this.ctx);
			this.bio = new MonoBtlsBioMono(this);
			this.ssl.SetBio(this.bio);
			if (base.IsServer)
			{
				if (this.nativeServerCertificate != null)
				{
					this.SetPrivateCertificate(this.nativeServerCertificate);
				}
			}
			else
			{
				this.ssl.SetServerName(base.ServerName);
			}
			if (base.Options.AllowRenegotiation)
			{
				this.ssl.SetRenegotiateMode(MonoBtlsSslRenegotiateMode.FREELY);
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
		private void SetPrivateCertificate(X509CertificateImplBtls privateCert)
		{
			this.ssl.SetCertificate(privateCert.X509);
			this.ssl.SetPrivateKey(privateCert.NativePrivateKey);
			X509CertificateImplCollection intermediateCertificates = privateCert.IntermediateCertificates;
			if (intermediateCertificates == null)
			{
				X509Chain x509Chain = new X509Chain(false);
				x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
				x509Chain.Build(new X509Certificate2(privateCert.X509.GetRawData(MonoBtlsX509Format.DER), ""));
				X509ChainElementCollection chainElements = x509Chain.ChainElements;
				for (int i = 1; i < chainElements.Count; i++)
				{
					X509Certificate2 certificate = chainElements[i].Certificate;
					if (certificate.SubjectName.RawData.SequenceEqual(certificate.IssuerName.RawData))
					{
						return;
					}
					this.ssl.AddIntermediateCertificate(MonoBtlsX509.LoadFromData(certificate.RawData, MonoBtlsX509Format.DER));
				}
				return;
			}
			for (int j = 0; j < intermediateCertificates.Count; j++)
			{
				X509CertificateImplBtls x509CertificateImplBtls = (X509CertificateImplBtls)intermediateCertificates[j];
				this.ssl.AddIntermediateCertificate(x509CertificateImplBtls.X509);
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000D4C0 File Offset: 0x0000B6C0
		private static Exception GetException(MonoBtlsSslError status)
		{
			string text;
			int num;
			int error = MonoBtlsError.GetError(out text, out num);
			if (error == 0)
			{
				return new MonoBtlsException(status);
			}
			int errorReason = MonoBtlsError.GetErrorReason(error);
			if (errorReason > 0)
			{
				return new TlsException((AlertDescription)errorReason);
			}
			string errorString = MonoBtlsError.GetErrorString(error);
			string message;
			if (text != null)
			{
				message = string.Format("{0} {1}\n  at {2}:{3}", new object[]
				{
					status,
					errorString,
					text,
					num
				});
			}
			else
			{
				message = string.Format("{0} {1}", status, errorString);
			}
			return new MonoBtlsException(message);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000D54C File Offset: 0x0000B74C
		public override bool ProcessHandshake()
		{
			bool flag = false;
			while (!flag)
			{
				MonoBtlsError.ClearError();
				MonoBtlsSslError monoBtlsSslError = this.DoProcessHandshake();
				if (monoBtlsSslError != MonoBtlsSslError.None)
				{
					if (monoBtlsSslError - MonoBtlsSslError.WantRead > 1)
					{
						this.ctx.CheckLastError("ProcessHandshake");
						throw MonoBtlsContext.GetException(monoBtlsSslError);
					}
					return false;
				}
				else if (this.connected)
				{
					flag = true;
				}
				else
				{
					this.connected = true;
				}
			}
			this.ssl.PrintErrors();
			return true;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000D5B1 File Offset: 0x0000B7B1
		private MonoBtlsSslError DoProcessHandshake()
		{
			if (this.connected)
			{
				return this.ssl.Handshake();
			}
			if (base.IsServer)
			{
				return this.ssl.Accept();
			}
			return this.ssl.Connect();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000D5E6 File Offset: 0x0000B7E6
		public override void FinishHandshake()
		{
			this.InitializeSession();
			this.isAuthenticated = true;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000D5F8 File Offset: 0x0000B7F8
		private void InitializeConnection()
		{
			this.ctx = new MonoBtlsSslCtx();
			MonoBtlsProvider.SetupCertificateStore(this.ctx.CertificateStore, base.Settings, base.IsServer);
			if (!base.IsServer || base.AskForClientCertificate)
			{
				this.ctx.SetVerifyCallback(new MonoBtlsVerifyCallback(this.VerifyCallback), false);
			}
			if (!base.IsServer)
			{
				this.ctx.SetSelectCallback(new MonoBtlsSelectCallback(this.SelectCallback));
			}
			if (base.IsServer && (base.Options.ServerCertSelectionDelegate != null || base.Settings.ClientCertificateSelectionCallback != null))
			{
				this.ctx.SetServerNameCallback(new MonoBtlsServerNameCallback(this.ServerNameCallback));
			}
			this.ctx.SetVerifyParam(MonoBtlsProvider.GetVerifyParam(base.Settings, base.ServerName, base.IsServer));
			TlsProtocolCode? tlsProtocolCode;
			TlsProtocolCode? tlsProtocolCode2;
			base.GetProtocolVersions(out tlsProtocolCode, out tlsProtocolCode2);
			if (tlsProtocolCode != null)
			{
				this.ctx.SetMinVersion((int)tlsProtocolCode.Value);
			}
			if (tlsProtocolCode2 != null)
			{
				this.ctx.SetMaxVersion((int)tlsProtocolCode2.Value);
			}
			if (base.Settings != null && base.Settings.EnabledCiphers != null)
			{
				short[] array = new short[base.Settings.EnabledCiphers.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (short)base.Settings.EnabledCiphers[i];
				}
				this.ctx.SetCiphers(array, true);
			}
			if (base.IsServer)
			{
				MonoTlsSettings settings = base.Settings;
				if (((settings != null) ? settings.ClientCertificateIssuers : null) != null)
				{
					this.ctx.SetClientCertificateIssuers(base.Settings.ClientCertificateIssuers);
				}
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000D794 File Offset: 0x0000B994
		private void GetPeerCertificate()
		{
			if (this.remoteCertificate != null)
			{
				return;
			}
			using (MonoBtlsX509 peerCertificate = this.ssl.GetPeerCertificate())
			{
				if (peerCertificate != null)
				{
					this.remoteCertificate = MonoBtlsProvider.CreateCertificate(peerCertificate);
				}
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		private void InitializeSession()
		{
			this.GetPeerCertificate();
			if (base.IsServer && base.AskForClientCertificate && !this.certificateValidated && !base.ValidateCertificate(null, null))
			{
				throw new TlsException(AlertDescription.CertificateUnknown);
			}
			CipherSuiteCode cipherSuiteCode = (CipherSuiteCode)this.ssl.GetCipher();
			TlsProtocolCode protocol = (TlsProtocolCode)this.ssl.GetVersion();
			string serverName = this.ssl.GetServerName();
			this.connectionInfo = new MonoTlsConnectionInfo
			{
				CipherSuiteCode = cipherSuiteCode,
				ProtocolVersion = MonoBtlsContext.GetProtocol(protocol),
				PeerDomainName = serverName
			};
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000D86C File Offset: 0x0000BA6C
		private static TlsProtocols GetProtocol(TlsProtocolCode protocol)
		{
			switch (protocol)
			{
			case TlsProtocolCode.Tls10:
				return TlsProtocols.Tls10;
			case TlsProtocolCode.Tls11:
				return TlsProtocols.Tls11;
			case TlsProtocolCode.Tls12:
				return TlsProtocols.Tls12;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000829A File Offset: 0x0000649A
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		[return: TupleElementNames(new string[]
		{
			"ret",
			"wantMore"
		})]
		public override ValueTuple<int, bool> Read(byte[] buffer, int offset, int size)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			ValueTuple<int, bool> result;
			try
			{
				MonoBtlsError.ClearError();
				MonoBtlsSslError monoBtlsSslError = this.ssl.Read(intPtr, ref size);
				if (monoBtlsSslError == MonoBtlsSslError.WantRead)
				{
					result = new ValueTuple<int, bool>(0, true);
				}
				else if (monoBtlsSslError == MonoBtlsSslError.ZeroReturn)
				{
					result = new ValueTuple<int, bool>(size, false);
				}
				else
				{
					if (monoBtlsSslError != MonoBtlsSslError.None)
					{
						throw MonoBtlsContext.GetException(monoBtlsSslError);
					}
					if (size > 0)
					{
						Marshal.Copy(intPtr, buffer, offset, size);
					}
					result = new ValueTuple<int, bool>(size, false);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000D934 File Offset: 0x0000BB34
		[return: TupleElementNames(new string[]
		{
			"ret",
			"wantMore"
		})]
		public override ValueTuple<int, bool> Write(byte[] buffer, int offset, int size)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(size);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			ValueTuple<int, bool> result;
			try
			{
				MonoBtlsError.ClearError();
				Marshal.Copy(buffer, offset, intPtr, size);
				MonoBtlsSslError monoBtlsSslError = this.ssl.Write(intPtr, ref size);
				if (monoBtlsSslError == MonoBtlsSslError.WantWrite)
				{
					result = new ValueTuple<int, bool>(0, true);
				}
				else
				{
					if (monoBtlsSslError != MonoBtlsSslError.None)
					{
						throw MonoBtlsContext.GetException(monoBtlsSslError);
					}
					result = new ValueTuple<int, bool>(size, false);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRenegotiate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000044FA File Offset: 0x000026FA
		public override void Renegotiate()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		public override void Shutdown()
		{
			if (base.Settings == null || !base.Settings.SendCloseNotify)
			{
				this.ssl.SetQuietShutdown();
			}
			this.ssl.Shutdown();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000D9E1 File Offset: 0x0000BBE1
		public override bool PendingRenegotiation()
		{
			return this.ssl.RenegotiatePending();
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		private void Dispose<T>(ref T disposable) where T : class, IDisposable
		{
			try
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			catch
			{
			}
			finally
			{
				disposable = default(T);
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000DA40 File Offset: 0x0000BC40
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Dispose<MonoBtlsSsl>(ref this.ssl);
					this.Dispose<MonoBtlsSslCtx>(ref this.ctx);
					this.Dispose<X509Certificate2>(ref this.remoteCertificate);
					this.Dispose<X509CertificateImplBtls>(ref this.nativeServerCertificate);
					this.Dispose<X509CertificateImplBtls>(ref this.nativeClientCertificate);
					this.Dispose<X509Certificate>(ref this.clientCertificate);
					this.Dispose<MonoBtlsBio>(ref this.bio);
					this.Dispose<MonoBtlsBio>(ref this.errbio);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000DACC File Offset: 0x0000BCCC
		int IMonoBtlsBioMono.Read(byte[] buffer, int offset, int size, out bool wantMore)
		{
			return base.Parent.InternalRead(buffer, offset, size, out wantMore);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000DADE File Offset: 0x0000BCDE
		bool IMonoBtlsBioMono.Write(byte[] buffer, int offset, int size)
		{
			return base.Parent.InternalWrite(buffer, offset, size);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00003917 File Offset: 0x00001B17
		void IMonoBtlsBioMono.Flush()
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00003917 File Offset: 0x00001B17
		void IMonoBtlsBioMono.Close()
		{
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000DAEE File Offset: 0x0000BCEE
		public override bool HasContext
		{
			get
			{
				return this.ssl != null && this.ssl.IsValid;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000DB05 File Offset: 0x0000BD05
		public override bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000DB0D File Offset: 0x0000BD0D
		public override MonoTlsConnectionInfo ConnectionInfo
		{
			get
			{
				return this.connectionInfo;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000DB15 File Offset: 0x0000BD15
		internal override bool IsRemoteCertificateAvailable
		{
			get
			{
				return this.remoteCertificate != null;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000DB20 File Offset: 0x0000BD20
		internal override X509Certificate LocalClientCertificate
		{
			get
			{
				return this.clientCertificate;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000DB28 File Offset: 0x0000BD28
		public override X509Certificate2 RemoteCertificate
		{
			get
			{
				return this.remoteCertificate;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000DB30 File Offset: 0x0000BD30
		public override TlsProtocols NegotiatedProtocol
		{
			get
			{
				return this.connectionInfo.ProtocolVersion;
			}
		}

		// Token: 0x040003A3 RID: 931
		private X509Certificate2 remoteCertificate;

		// Token: 0x040003A4 RID: 932
		private X509Certificate clientCertificate;

		// Token: 0x040003A5 RID: 933
		private X509CertificateImplBtls nativeServerCertificate;

		// Token: 0x040003A6 RID: 934
		private X509CertificateImplBtls nativeClientCertificate;

		// Token: 0x040003A7 RID: 935
		private MonoBtlsSslCtx ctx;

		// Token: 0x040003A8 RID: 936
		private MonoBtlsSsl ssl;

		// Token: 0x040003A9 RID: 937
		private MonoBtlsBio bio;

		// Token: 0x040003AA RID: 938
		private MonoBtlsBio errbio;

		// Token: 0x040003AB RID: 939
		private MonoTlsConnectionInfo connectionInfo;

		// Token: 0x040003AC RID: 940
		private bool certificateValidated;

		// Token: 0x040003AD RID: 941
		private bool isAuthenticated;

		// Token: 0x040003AE RID: 942
		private bool connected;
	}
}
