using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WebSocketSharp.Net
{
	// Token: 0x0200003D RID: 61
	public class ClientSslConfiguration
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x00018390 File Offset: 0x00016590
		public ClientSslConfiguration(string targetHost)
		{
			bool flag = targetHost == null;
			if (flag)
			{
				throw new ArgumentNullException("targetHost");
			}
			bool flag2 = targetHost.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "targetHost");
			}
			this._targetHost = targetHost;
			this._enabledSslProtocols = SslProtocols.None;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000183E4 File Offset: 0x000165E4
		public ClientSslConfiguration(ClientSslConfiguration configuration)
		{
			bool flag = configuration == null;
			if (flag)
			{
				throw new ArgumentNullException("configuration");
			}
			this._checkCertRevocation = configuration._checkCertRevocation;
			this._clientCertSelectionCallback = configuration._clientCertSelectionCallback;
			this._clientCerts = configuration._clientCerts;
			this._enabledSslProtocols = configuration._enabledSslProtocols;
			this._serverCertValidationCallback = configuration._serverCertValidationCallback;
			this._targetHost = configuration._targetHost;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00018454 File Offset: 0x00016654
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0001846C File Offset: 0x0001666C
		public bool CheckCertificateRevocation
		{
			get
			{
				return this._checkCertRevocation;
			}
			set
			{
				this._checkCertRevocation = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00018478 File Offset: 0x00016678
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00018490 File Offset: 0x00016690
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return this._clientCerts;
			}
			set
			{
				this._clientCerts = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0001849C File Offset: 0x0001669C
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x000184D3 File Offset: 0x000166D3
		public LocalCertificateSelectionCallback ClientCertificateSelectionCallback
		{
			get
			{
				bool flag = this._clientCertSelectionCallback == null;
				if (flag)
				{
					this._clientCertSelectionCallback = new LocalCertificateSelectionCallback(ClientSslConfiguration.defaultSelectClientCertificate);
				}
				return this._clientCertSelectionCallback;
			}
			set
			{
				this._clientCertSelectionCallback = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000184E0 File Offset: 0x000166E0
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x000184F8 File Offset: 0x000166F8
		public SslProtocols EnabledSslProtocols
		{
			get
			{
				return this._enabledSslProtocols;
			}
			set
			{
				this._enabledSslProtocols = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00018504 File Offset: 0x00016704
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0001853B File Offset: 0x0001673B
		public RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				bool flag = this._serverCertValidationCallback == null;
				if (flag)
				{
					this._serverCertValidationCallback = new RemoteCertificateValidationCallback(ClientSslConfiguration.defaultValidateServerCertificate);
				}
				return this._serverCertValidationCallback;
			}
			set
			{
				this._serverCertValidationCallback = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00018548 File Offset: 0x00016748
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00018560 File Offset: 0x00016760
		public string TargetHost
		{
			get
			{
				return this._targetHost;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException("value");
				}
				bool flag2 = value.Length == 0;
				if (flag2)
				{
					throw new ArgumentException("An empty string.", "value");
				}
				this._targetHost = value;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000185A8 File Offset: 0x000167A8
		private static X509Certificate defaultSelectClientCertificate(object sender, string targetHost, X509CertificateCollection clientCertificates, X509Certificate serverCertificate, string[] acceptableIssuers)
		{
			return null;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000185BC File Offset: 0x000167BC
		private static bool defaultValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x040001A1 RID: 417
		private bool _checkCertRevocation;

		// Token: 0x040001A2 RID: 418
		private LocalCertificateSelectionCallback _clientCertSelectionCallback;

		// Token: 0x040001A3 RID: 419
		private X509CertificateCollection _clientCerts;

		// Token: 0x040001A4 RID: 420
		private SslProtocols _enabledSslProtocols;

		// Token: 0x040001A5 RID: 421
		private RemoteCertificateValidationCallback _serverCertValidationCallback;

		// Token: 0x040001A6 RID: 422
		private string _targetHost;
	}
}
