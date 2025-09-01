using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WebSocketSharp.Net
{
	// Token: 0x0200003E RID: 62
	public class ServerSslConfiguration
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x000185CF File Offset: 0x000167CF
		public ServerSslConfiguration()
		{
			this._enabledSslProtocols = SslProtocols.None;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000185E0 File Offset: 0x000167E0
		public ServerSslConfiguration(ServerSslConfiguration configuration)
		{
			bool flag = configuration == null;
			if (flag)
			{
				throw new ArgumentNullException("configuration");
			}
			this._checkCertRevocation = configuration._checkCertRevocation;
			this._clientCertRequired = configuration._clientCertRequired;
			this._clientCertValidationCallback = configuration._clientCertValidationCallback;
			this._enabledSslProtocols = configuration._enabledSslProtocols;
			this._serverCert = configuration._serverCert;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00018644 File Offset: 0x00016844
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0001865C File Offset: 0x0001685C
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00018668 File Offset: 0x00016868
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00018680 File Offset: 0x00016880
		public bool ClientCertificateRequired
		{
			get
			{
				return this._clientCertRequired;
			}
			set
			{
				this._clientCertRequired = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001868C File Offset: 0x0001688C
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x000186C3 File Offset: 0x000168C3
		public RemoteCertificateValidationCallback ClientCertificateValidationCallback
		{
			get
			{
				bool flag = this._clientCertValidationCallback == null;
				if (flag)
				{
					this._clientCertValidationCallback = new RemoteCertificateValidationCallback(ServerSslConfiguration.defaultValidateClientCertificate);
				}
				return this._clientCertValidationCallback;
			}
			set
			{
				this._clientCertValidationCallback = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x000186D0 File Offset: 0x000168D0
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x000186E8 File Offset: 0x000168E8
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

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000186F4 File Offset: 0x000168F4
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0001870C File Offset: 0x0001690C
		public X509Certificate2 ServerCertificate
		{
			get
			{
				return this._serverCert;
			}
			set
			{
				this._serverCert = value;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00018718 File Offset: 0x00016918
		private static bool defaultValidateClientCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x040001A7 RID: 423
		private bool _checkCertRevocation;

		// Token: 0x040001A8 RID: 424
		private bool _clientCertRequired;

		// Token: 0x040001A9 RID: 425
		private RemoteCertificateValidationCallback _clientCertValidationCallback;

		// Token: 0x040001AA RID: 426
		private SslProtocols _enabledSslProtocols;

		// Token: 0x040001AB RID: 427
		private X509Certificate2 _serverCert;
	}
}
