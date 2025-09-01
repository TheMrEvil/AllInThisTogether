using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Mono.Security.Interface
{
	// Token: 0x0200004A RID: 74
	public sealed class MonoTlsSettings
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000F082 File Offset: 0x0000D282
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000F08A File Offset: 0x0000D28A
		public MonoRemoteCertificateValidationCallback RemoteCertificateValidationCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<RemoteCertificateValidationCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RemoteCertificateValidationCallback>k__BackingField = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000F093 File Offset: 0x0000D293
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000F09B File Offset: 0x0000D29B
		public MonoLocalCertificateSelectionCallback ClientCertificateSelectionCallback
		{
			[CompilerGenerated]
			get
			{
				return this.<ClientCertificateSelectionCallback>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ClientCertificateSelectionCallback>k__BackingField = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		public bool CheckCertificateName
		{
			get
			{
				return this.checkCertName;
			}
			set
			{
				this.checkCertName = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000F0B5 File Offset: 0x0000D2B5
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000F0BD File Offset: 0x0000D2BD
		public bool CheckCertificateRevocationStatus
		{
			get
			{
				return this.checkCertRevocationStatus;
			}
			set
			{
				this.checkCertRevocationStatus = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000F0C6 File Offset: 0x0000D2C6
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public bool? UseServicePointManagerCallback
		{
			get
			{
				return this.useServicePointManagerCallback;
			}
			set
			{
				this.useServicePointManagerCallback = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000F0D7 File Offset: 0x0000D2D7
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000F0DF File Offset: 0x0000D2DF
		public bool SkipSystemValidators
		{
			get
			{
				return this.skipSystemValidators;
			}
			set
			{
				this.skipSystemValidators = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		public bool CallbackNeedsCertificateChain
		{
			get
			{
				return this.callbackNeedsChain;
			}
			set
			{
				this.callbackNeedsChain = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000F0F9 File Offset: 0x0000D2F9
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000F101 File Offset: 0x0000D301
		public DateTime? CertificateValidationTime
		{
			[CompilerGenerated]
			get
			{
				return this.<CertificateValidationTime>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CertificateValidationTime>k__BackingField = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000F10A File Offset: 0x0000D30A
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000F112 File Offset: 0x0000D312
		public X509CertificateCollection TrustAnchors
		{
			[CompilerGenerated]
			get
			{
				return this.<TrustAnchors>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TrustAnchors>k__BackingField = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000F11B File Offset: 0x0000D31B
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000F123 File Offset: 0x0000D323
		public object UserSettings
		{
			[CompilerGenerated]
			get
			{
				return this.<UserSettings>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UserSettings>k__BackingField = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000F12C File Offset: 0x0000D32C
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000F134 File Offset: 0x0000D334
		internal string[] CertificateSearchPaths
		{
			[CompilerGenerated]
			get
			{
				return this.<CertificateSearchPaths>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CertificateSearchPaths>k__BackingField = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000F13D File Offset: 0x0000D33D
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000F145 File Offset: 0x0000D345
		internal bool SendCloseNotify
		{
			[CompilerGenerated]
			get
			{
				return this.<SendCloseNotify>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SendCloseNotify>k__BackingField = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000F14E File Offset: 0x0000D34E
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000F156 File Offset: 0x0000D356
		public string[] ClientCertificateIssuers
		{
			[CompilerGenerated]
			get
			{
				return this.<ClientCertificateIssuers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ClientCertificateIssuers>k__BackingField = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000F15F File Offset: 0x0000D35F
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000F167 File Offset: 0x0000D367
		public bool DisallowUnauthenticatedCertificateRequest
		{
			[CompilerGenerated]
			get
			{
				return this.<DisallowUnauthenticatedCertificateRequest>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisallowUnauthenticatedCertificateRequest>k__BackingField = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000F170 File Offset: 0x0000D370
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000F178 File Offset: 0x0000D378
		public TlsProtocols? EnabledProtocols
		{
			[CompilerGenerated]
			get
			{
				return this.<EnabledProtocols>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnabledProtocols>k__BackingField = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000F181 File Offset: 0x0000D381
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000F189 File Offset: 0x0000D389
		[CLSCompliant(false)]
		public CipherSuiteCode[] EnabledCiphers
		{
			[CompilerGenerated]
			get
			{
				return this.<EnabledCiphers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnabledCiphers>k__BackingField = value;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F192 File Offset: 0x0000D392
		public MonoTlsSettings()
		{
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000F1C7 File Offset: 0x0000D3C7
		public static MonoTlsSettings DefaultSettings
		{
			get
			{
				if (MonoTlsSettings.defaultSettings == null)
				{
					Interlocked.CompareExchange<MonoTlsSettings>(ref MonoTlsSettings.defaultSettings, new MonoTlsSettings(), null);
				}
				return MonoTlsSettings.defaultSettings;
			}
			set
			{
				MonoTlsSettings.defaultSettings = (value ?? new MonoTlsSettings());
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
		public static MonoTlsSettings CopyDefaultSettings()
		{
			return MonoTlsSettings.DefaultSettings.Clone();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000F1E4 File Offset: 0x0000D3E4
		[Obsolete("Do not use outside System.dll!")]
		public ICertificateValidator CertificateValidator
		{
			get
			{
				return this.certificateValidator;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		[Obsolete("Do not use outside System.dll!")]
		public MonoTlsSettings CloneWithValidator(ICertificateValidator validator)
		{
			if (this.cloned)
			{
				this.certificateValidator = validator;
				return this;
			}
			return new MonoTlsSettings(this)
			{
				certificateValidator = validator
			};
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000F20C File Offset: 0x0000D40C
		public MonoTlsSettings Clone()
		{
			return new MonoTlsSettings(this);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000F214 File Offset: 0x0000D414
		private MonoTlsSettings(MonoTlsSettings other)
		{
			this.RemoteCertificateValidationCallback = other.RemoteCertificateValidationCallback;
			this.ClientCertificateSelectionCallback = other.ClientCertificateSelectionCallback;
			this.checkCertName = other.checkCertName;
			this.checkCertRevocationStatus = other.checkCertRevocationStatus;
			this.UseServicePointManagerCallback = other.useServicePointManagerCallback;
			this.skipSystemValidators = other.skipSystemValidators;
			this.callbackNeedsChain = other.callbackNeedsChain;
			this.UserSettings = other.UserSettings;
			this.EnabledProtocols = other.EnabledProtocols;
			this.EnabledCiphers = other.EnabledCiphers;
			this.CertificateValidationTime = other.CertificateValidationTime;
			this.SendCloseNotify = other.SendCloseNotify;
			this.ClientCertificateIssuers = other.ClientCertificateIssuers;
			this.DisallowUnauthenticatedCertificateRequest = other.DisallowUnauthenticatedCertificateRequest;
			if (other.TrustAnchors != null)
			{
				this.TrustAnchors = new X509CertificateCollection(other.TrustAnchors);
			}
			if (other.CertificateSearchPaths != null)
			{
				this.CertificateSearchPaths = new string[other.CertificateSearchPaths.Length];
				other.CertificateSearchPaths.CopyTo(this.CertificateSearchPaths, 0);
			}
			this.cloned = true;
		}

		// Token: 0x0400027A RID: 634
		[CompilerGenerated]
		private MonoRemoteCertificateValidationCallback <RemoteCertificateValidationCallback>k__BackingField;

		// Token: 0x0400027B RID: 635
		[CompilerGenerated]
		private MonoLocalCertificateSelectionCallback <ClientCertificateSelectionCallback>k__BackingField;

		// Token: 0x0400027C RID: 636
		[CompilerGenerated]
		private DateTime? <CertificateValidationTime>k__BackingField;

		// Token: 0x0400027D RID: 637
		[CompilerGenerated]
		private X509CertificateCollection <TrustAnchors>k__BackingField;

		// Token: 0x0400027E RID: 638
		[CompilerGenerated]
		private object <UserSettings>k__BackingField;

		// Token: 0x0400027F RID: 639
		[CompilerGenerated]
		private string[] <CertificateSearchPaths>k__BackingField;

		// Token: 0x04000280 RID: 640
		[CompilerGenerated]
		private bool <SendCloseNotify>k__BackingField;

		// Token: 0x04000281 RID: 641
		[CompilerGenerated]
		private string[] <ClientCertificateIssuers>k__BackingField;

		// Token: 0x04000282 RID: 642
		[CompilerGenerated]
		private bool <DisallowUnauthenticatedCertificateRequest>k__BackingField;

		// Token: 0x04000283 RID: 643
		[CompilerGenerated]
		private TlsProtocols? <EnabledProtocols>k__BackingField;

		// Token: 0x04000284 RID: 644
		[CompilerGenerated]
		private CipherSuiteCode[] <EnabledCiphers>k__BackingField;

		// Token: 0x04000285 RID: 645
		private bool cloned;

		// Token: 0x04000286 RID: 646
		private bool checkCertName = true;

		// Token: 0x04000287 RID: 647
		private bool checkCertRevocationStatus;

		// Token: 0x04000288 RID: 648
		private bool? useServicePointManagerCallback;

		// Token: 0x04000289 RID: 649
		private bool skipSystemValidators;

		// Token: 0x0400028A RID: 650
		private bool callbackNeedsChain = true;

		// Token: 0x0400028B RID: 651
		private ICertificateValidator certificateValidator;

		// Token: 0x0400028C RID: 652
		private static MonoTlsSettings defaultSettings;
	}
}
