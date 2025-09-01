using System;
using System.Configuration;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace System.Net.Configuration
{
	// Token: 0x0200075A RID: 1882
	internal sealed class SettingsSectionInternal
	{
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000CC290 File Offset: 0x000CA490
		internal static SettingsSectionInternal Section
		{
			get
			{
				return SettingsSectionInternal.instance;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x000CC297 File Offset: 0x000CA497
		// (set) Token: 0x06003B54 RID: 15188 RVA: 0x000CC29F File Offset: 0x000CA49F
		internal bool UseNagleAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<UseNagleAlgorithm>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UseNagleAlgorithm>k__BackingField = value;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x000CC2A8 File Offset: 0x000CA4A8
		// (set) Token: 0x06003B56 RID: 15190 RVA: 0x000CC2B0 File Offset: 0x000CA4B0
		internal bool Expect100Continue
		{
			[CompilerGenerated]
			get
			{
				return this.<Expect100Continue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Expect100Continue>k__BackingField = value;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x000CC2B9 File Offset: 0x000CA4B9
		// (set) Token: 0x06003B58 RID: 15192 RVA: 0x000CC2C1 File Offset: 0x000CA4C1
		internal bool CheckCertificateName
		{
			[CompilerGenerated]
			get
			{
				return this.<CheckCertificateName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CheckCertificateName>k__BackingField = value;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x000CC2CA File Offset: 0x000CA4CA
		// (set) Token: 0x06003B5A RID: 15194 RVA: 0x000CC2D2 File Offset: 0x000CA4D2
		internal int DnsRefreshTimeout
		{
			[CompilerGenerated]
			get
			{
				return this.<DnsRefreshTimeout>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DnsRefreshTimeout>k__BackingField = value;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x000CC2DB File Offset: 0x000CA4DB
		// (set) Token: 0x06003B5C RID: 15196 RVA: 0x000CC2E3 File Offset: 0x000CA4E3
		internal bool EnableDnsRoundRobin
		{
			[CompilerGenerated]
			get
			{
				return this.<EnableDnsRoundRobin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnableDnsRoundRobin>k__BackingField = value;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000CC2EC File Offset: 0x000CA4EC
		// (set) Token: 0x06003B5E RID: 15198 RVA: 0x000CC2F4 File Offset: 0x000CA4F4
		internal bool CheckCertificateRevocationList
		{
			[CompilerGenerated]
			get
			{
				return this.<CheckCertificateRevocationList>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CheckCertificateRevocationList>k__BackingField = value;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x000CC2FD File Offset: 0x000CA4FD
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x000CC305 File Offset: 0x000CA505
		internal EncryptionPolicy EncryptionPolicy
		{
			[CompilerGenerated]
			get
			{
				return this.<EncryptionPolicy>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EncryptionPolicy>k__BackingField = value;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x000CC310 File Offset: 0x000CA510
		internal bool Ipv6Enabled
		{
			get
			{
				try
				{
					SettingsSection settingsSection = (SettingsSection)ConfigurationManager.GetSection("system.net/settings");
					if (settingsSection != null)
					{
						return settingsSection.Ipv6.Enabled;
					}
				}
				catch
				{
				}
				return true;
			}
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000CC358 File Offset: 0x000CA558
		public SettingsSectionInternal()
		{
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000CC36E File Offset: 0x000CA56E
		// Note: this type is marked as 'beforefieldinit'.
		static SettingsSectionInternal()
		{
		}

		// Token: 0x0400236E RID: 9070
		private static readonly SettingsSectionInternal instance = new SettingsSectionInternal();

		// Token: 0x0400236F RID: 9071
		internal UnicodeEncodingConformance WebUtilityUnicodeEncodingConformance;

		// Token: 0x04002370 RID: 9072
		internal UnicodeDecodingConformance WebUtilityUnicodeDecodingConformance;

		// Token: 0x04002371 RID: 9073
		internal readonly bool HttpListenerUnescapeRequestUrl = true;

		// Token: 0x04002372 RID: 9074
		internal readonly IPProtectionLevel IPProtectionLevel = IPProtectionLevel.Unspecified;

		// Token: 0x04002373 RID: 9075
		[CompilerGenerated]
		private bool <UseNagleAlgorithm>k__BackingField;

		// Token: 0x04002374 RID: 9076
		[CompilerGenerated]
		private bool <Expect100Continue>k__BackingField;

		// Token: 0x04002375 RID: 9077
		[CompilerGenerated]
		private bool <CheckCertificateName>k__BackingField;

		// Token: 0x04002376 RID: 9078
		[CompilerGenerated]
		private int <DnsRefreshTimeout>k__BackingField;

		// Token: 0x04002377 RID: 9079
		[CompilerGenerated]
		private bool <EnableDnsRoundRobin>k__BackingField;

		// Token: 0x04002378 RID: 9080
		[CompilerGenerated]
		private bool <CheckCertificateRevocationList>k__BackingField;

		// Token: 0x04002379 RID: 9081
		[CompilerGenerated]
		private EncryptionPolicy <EncryptionPolicy>k__BackingField;
	}
}
