using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A0 RID: 160
	internal abstract class MobileTlsContext : IDisposable
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00008CA4 File Offset: 0x00006EA4
		protected MobileTlsContext(MobileAuthenticatedStream parent, MonoSslAuthenticationOptions options)
		{
			this.Parent = parent;
			this.Options = options;
			this.IsServer = options.ServerMode;
			this.EnabledProtocols = options.EnabledSslProtocols;
			if (options.ServerMode)
			{
				this.LocalServerCertificate = options.ServerCertificate;
				this.AskForClientCertificate = options.ClientCertificateRequired;
			}
			else
			{
				this.ClientCertificates = options.ClientCertificates;
				this.TargetHost = options.TargetHost;
				this.ServerName = options.TargetHost;
				if (!string.IsNullOrEmpty(this.ServerName))
				{
					int num = this.ServerName.IndexOf(':');
					if (num > 0)
					{
						this.ServerName = this.ServerName.Substring(0, num);
					}
				}
			}
			this.certificateValidator = ChainValidationHelper.GetInternalValidator(parent.SslStream, parent.Provider, parent.Settings);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00008D72 File Offset: 0x00006F72
		internal MonoSslAuthenticationOptions Options
		{
			[CompilerGenerated]
			get
			{
				return this.<Options>k__BackingField;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00008D7A File Offset: 0x00006F7A
		internal MobileAuthenticatedStream Parent
		{
			[CompilerGenerated]
			get
			{
				return this.<Parent>k__BackingField;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00008D82 File Offset: 0x00006F82
		public MonoTlsSettings Settings
		{
			get
			{
				return this.Parent.Settings;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00008D8F File Offset: 0x00006F8F
		public MonoTlsProvider Provider
		{
			get
			{
				return this.Parent.Provider;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_TLS_DEBUG")]
		protected void Debug(string message, params object[] args)
		{
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002BE RID: 702
		public abstract bool HasContext { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002BF RID: 703
		public abstract bool IsAuthenticated { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00008D9C File Offset: 0x00006F9C
		public bool IsServer
		{
			[CompilerGenerated]
			get
			{
				return this.<IsServer>k__BackingField;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00008DA4 File Offset: 0x00006FA4
		internal string TargetHost
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetHost>k__BackingField;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00008DAC File Offset: 0x00006FAC
		protected string ServerName
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerName>k__BackingField;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00008DB4 File Offset: 0x00006FB4
		protected bool AskForClientCertificate
		{
			[CompilerGenerated]
			get
			{
				return this.<AskForClientCertificate>k__BackingField;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00008DBC File Offset: 0x00006FBC
		protected SslProtocols EnabledProtocols
		{
			[CompilerGenerated]
			get
			{
				return this.<EnabledProtocols>k__BackingField;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00008DC4 File Offset: 0x00006FC4
		protected X509CertificateCollection ClientCertificates
		{
			[CompilerGenerated]
			get
			{
				return this.<ClientCertificates>k__BackingField;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00003062 File Offset: 0x00001262
		internal bool AllowRenegotiation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008DCC File Offset: 0x00006FCC
		protected void GetProtocolVersions(out TlsProtocolCode? min, out TlsProtocolCode? max)
		{
			if ((this.EnabledProtocols & SslProtocols.Tls) != SslProtocols.None)
			{
				min = new TlsProtocolCode?(TlsProtocolCode.Tls10);
			}
			else if ((this.EnabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				min = new TlsProtocolCode?(TlsProtocolCode.Tls11);
			}
			else if ((this.EnabledProtocols & SslProtocols.Tls12) != SslProtocols.None)
			{
				min = new TlsProtocolCode?(TlsProtocolCode.Tls12);
			}
			else
			{
				min = null;
			}
			if ((this.EnabledProtocols & SslProtocols.Tls12) != SslProtocols.None)
			{
				max = new TlsProtocolCode?(TlsProtocolCode.Tls12);
				return;
			}
			if ((this.EnabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				max = new TlsProtocolCode?(TlsProtocolCode.Tls11);
				return;
			}
			if ((this.EnabledProtocols & SslProtocols.Tls) != SslProtocols.None)
			{
				max = new TlsProtocolCode?(TlsProtocolCode.Tls10);
				return;
			}
			max = null;
		}

		// Token: 0x060002C8 RID: 712
		public abstract void StartHandshake();

		// Token: 0x060002C9 RID: 713
		public abstract bool ProcessHandshake();

		// Token: 0x060002CA RID: 714
		public abstract void FinishHandshake();

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002CB RID: 715
		public abstract MonoTlsConnectionInfo ConnectionInfo { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00008EA4 File Offset: 0x000070A4
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00008EAC File Offset: 0x000070AC
		internal X509Certificate LocalServerCertificate
		{
			[CompilerGenerated]
			get
			{
				return this.<LocalServerCertificate>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LocalServerCertificate>k__BackingField = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002CE RID: 718
		internal abstract bool IsRemoteCertificateAvailable { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002CF RID: 719
		internal abstract X509Certificate LocalClientCertificate { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002D0 RID: 720
		public abstract X509Certificate2 RemoteCertificate { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002D1 RID: 721
		public abstract TlsProtocols NegotiatedProtocol { get; }

		// Token: 0x060002D2 RID: 722
		public abstract void Flush();

		// Token: 0x060002D3 RID: 723
		[return: TupleElementNames(new string[]
		{
			"ret",
			"wantMore"
		})]
		public abstract ValueTuple<int, bool> Read(byte[] buffer, int offset, int count);

		// Token: 0x060002D4 RID: 724
		[return: TupleElementNames(new string[]
		{
			"ret",
			"wantMore"
		})]
		public abstract ValueTuple<int, bool> Write(byte[] buffer, int offset, int count);

		// Token: 0x060002D5 RID: 725
		public abstract void Shutdown();

		// Token: 0x060002D6 RID: 726
		public abstract bool PendingRenegotiation();

		// Token: 0x060002D7 RID: 727 RVA: 0x00008EB8 File Offset: 0x000070B8
		protected bool ValidateCertificate(X509Certificate2 leaf, X509Chain chain)
		{
			ValidationResult validationResult = this.certificateValidator.ValidateCertificate(this.TargetHost, this.IsServer, leaf, chain);
			return validationResult != null && validationResult.Trusted && !validationResult.UserDenied;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008EF8 File Offset: 0x000070F8
		protected bool ValidateCertificate(X509Certificate2Collection certificates)
		{
			ValidationResult validationResult = this.certificateValidator.ValidateCertificate(this.TargetHost, this.IsServer, certificates);
			return validationResult != null && validationResult.Trusted && !validationResult.UserDenied;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008F34 File Offset: 0x00007134
		protected X509Certificate SelectServerCertificate(string serverIdentity)
		{
			if (this.Options.ServerCertSelectionDelegate != null)
			{
				this.LocalServerCertificate = this.Options.ServerCertSelectionDelegate(serverIdentity);
				if (this.LocalServerCertificate == null)
				{
					throw new AuthenticationException("The server mode SSL must use a certificate with the associated private key.");
				}
			}
			else if (this.Settings.ClientCertificateSelectionCallback != null)
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.Add(this.Options.ServerCertificate);
				this.LocalServerCertificate = this.Settings.ClientCertificateSelectionCallback(string.Empty, x509CertificateCollection, null, Array.Empty<string>());
			}
			else
			{
				this.LocalServerCertificate = this.Options.ServerCertificate;
			}
			if (this.LocalServerCertificate == null)
			{
				throw new NotSupportedException("The server mode SSL must use a certificate with the associated private key.");
			}
			return this.LocalServerCertificate;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00008FEC File Offset: 0x000071EC
		protected X509Certificate SelectClientCertificate(string[] acceptableIssuers)
		{
			if (this.Settings.DisallowUnauthenticatedCertificateRequest && !this.IsAuthenticated)
			{
				return null;
			}
			if (this.RemoteCertificate == null)
			{
				throw new TlsException(AlertDescription.InternalError, "Cannot request client certificate before receiving one from the server.");
			}
			X509Certificate result;
			if (this.certificateValidator.SelectClientCertificate(this.TargetHost, this.ClientCertificates, this.IsAuthenticated ? this.RemoteCertificate : null, acceptableIssuers, out result))
			{
				return result;
			}
			if (this.ClientCertificates == null || this.ClientCertificates.Count == 0)
			{
				return null;
			}
			if (acceptableIssuers == null || acceptableIssuers.Length == 0)
			{
				return this.ClientCertificates[0];
			}
			for (int i = 0; i < this.ClientCertificates.Count; i++)
			{
				X509Certificate2 x509Certificate = this.ClientCertificates[i] as X509Certificate2;
				if (x509Certificate != null)
				{
					X509Chain x509Chain = null;
					try
					{
						x509Chain = new X509Chain();
						x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
						x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreInvalidName;
						x509Chain.Build(x509Certificate);
						if (x509Chain.ChainElements.Count != 0)
						{
							for (int j = 0; j < x509Chain.ChainElements.Count; j++)
							{
								string issuer = x509Chain.ChainElements[j].Certificate.Issuer;
								if (Array.IndexOf<string>(acceptableIssuers, issuer) != -1)
								{
									return x509Certificate;
								}
							}
						}
					}
					catch
					{
					}
					finally
					{
						if (x509Chain != null)
						{
							x509Chain.Reset();
						}
					}
				}
			}
			return null;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002DB RID: 731
		public abstract bool CanRenegotiate { get; }

		// Token: 0x060002DC RID: 732
		public abstract void Renegotiate();

		// Token: 0x060002DD RID: 733 RVA: 0x00009160 File Offset: 0x00007360
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009170 File Offset: 0x00007370
		~MobileTlsContext()
		{
			this.Dispose(false);
		}

		// Token: 0x0400026A RID: 618
		private ChainValidationHelper certificateValidator;

		// Token: 0x0400026B RID: 619
		[CompilerGenerated]
		private readonly MonoSslAuthenticationOptions <Options>k__BackingField;

		// Token: 0x0400026C RID: 620
		[CompilerGenerated]
		private readonly MobileAuthenticatedStream <Parent>k__BackingField;

		// Token: 0x0400026D RID: 621
		[CompilerGenerated]
		private readonly bool <IsServer>k__BackingField;

		// Token: 0x0400026E RID: 622
		[CompilerGenerated]
		private readonly string <TargetHost>k__BackingField;

		// Token: 0x0400026F RID: 623
		[CompilerGenerated]
		private readonly string <ServerName>k__BackingField;

		// Token: 0x04000270 RID: 624
		[CompilerGenerated]
		private readonly bool <AskForClientCertificate>k__BackingField;

		// Token: 0x04000271 RID: 625
		[CompilerGenerated]
		private readonly SslProtocols <EnabledProtocols>k__BackingField;

		// Token: 0x04000272 RID: 626
		[CompilerGenerated]
		private readonly X509CertificateCollection <ClientCertificates>k__BackingField;

		// Token: 0x04000273 RID: 627
		[CompilerGenerated]
		private X509Certificate <LocalServerCertificate>k__BackingField;
	}
}
