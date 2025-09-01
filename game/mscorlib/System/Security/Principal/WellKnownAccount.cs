using System;
using System.Runtime.CompilerServices;

namespace System.Security.Principal
{
	// Token: 0x020004E8 RID: 1256
	internal class WellKnownAccount
	{
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000B8715 File Offset: 0x000B6915
		// (set) Token: 0x06003233 RID: 12851 RVA: 0x000B871D File Offset: 0x000B691D
		public WellKnownSidType WellKnownValue
		{
			[CompilerGenerated]
			get
			{
				return this.<WellKnownValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WellKnownValue>k__BackingField = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000B8726 File Offset: 0x000B6926
		// (set) Token: 0x06003235 RID: 12853 RVA: 0x000B872E File Offset: 0x000B692E
		public bool IsAbsolute
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAbsolute>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsAbsolute>k__BackingField = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000B8737 File Offset: 0x000B6937
		// (set) Token: 0x06003237 RID: 12855 RVA: 0x000B873F File Offset: 0x000B693F
		public string Sid
		{
			[CompilerGenerated]
			get
			{
				return this.<Sid>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Sid>k__BackingField = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x000B8748 File Offset: 0x000B6948
		// (set) Token: 0x06003239 RID: 12857 RVA: 0x000B8750 File Offset: 0x000B6950
		public string Rid
		{
			[CompilerGenerated]
			get
			{
				return this.<Rid>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Rid>k__BackingField = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000B8759 File Offset: 0x000B6959
		// (set) Token: 0x0600323B RID: 12859 RVA: 0x000B8761 File Offset: 0x000B6961
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000B876A File Offset: 0x000B696A
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x000B8772 File Offset: 0x000B6972
		public string SddlForm
		{
			[CompilerGenerated]
			get
			{
				return this.<SddlForm>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SddlForm>k__BackingField = value;
			}
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000B877C File Offset: 0x000B697C
		public static WellKnownAccount LookupByType(WellKnownSidType sidType)
		{
			foreach (WellKnownAccount wellKnownAccount in WellKnownAccount.accounts)
			{
				if (wellKnownAccount.WellKnownValue == sidType)
				{
					return wellKnownAccount;
				}
			}
			return null;
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000B87B0 File Offset: 0x000B69B0
		public static WellKnownAccount LookupBySid(string s)
		{
			foreach (WellKnownAccount wellKnownAccount in WellKnownAccount.accounts)
			{
				if (wellKnownAccount.Sid == s)
				{
					return wellKnownAccount;
				}
			}
			return null;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000B87E8 File Offset: 0x000B69E8
		public static WellKnownAccount LookupByName(string s)
		{
			foreach (WellKnownAccount wellKnownAccount in WellKnownAccount.accounts)
			{
				if (wellKnownAccount.Name == s)
				{
					return wellKnownAccount;
				}
			}
			return null;
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000B8820 File Offset: 0x000B6A20
		public static WellKnownAccount LookupBySddlForm(string s)
		{
			foreach (WellKnownAccount wellKnownAccount in WellKnownAccount.accounts)
			{
				if (wellKnownAccount.SddlForm == s)
				{
					return wellKnownAccount;
				}
			}
			return null;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x0000259F File Offset: 0x0000079F
		public WellKnownAccount()
		{
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000B8858 File Offset: 0x000B6A58
		// Note: this type is marked as 'beforefieldinit'.
		static WellKnownAccount()
		{
		}

		// Token: 0x040022C5 RID: 8901
		[CompilerGenerated]
		private WellKnownSidType <WellKnownValue>k__BackingField;

		// Token: 0x040022C6 RID: 8902
		[CompilerGenerated]
		private bool <IsAbsolute>k__BackingField;

		// Token: 0x040022C7 RID: 8903
		[CompilerGenerated]
		private string <Sid>k__BackingField;

		// Token: 0x040022C8 RID: 8904
		[CompilerGenerated]
		private string <Rid>k__BackingField;

		// Token: 0x040022C9 RID: 8905
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040022CA RID: 8906
		[CompilerGenerated]
		private string <SddlForm>k__BackingField;

		// Token: 0x040022CB RID: 8907
		private static readonly WellKnownAccount[] accounts = new WellKnownAccount[]
		{
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NullSid,
				IsAbsolute = true,
				Sid = "S-1-0-0",
				Name = "NULL SID"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WorldSid,
				IsAbsolute = true,
				Sid = "S-1-1-0",
				Name = "Everyone",
				SddlForm = "WD"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.LocalSid,
				IsAbsolute = true,
				Sid = "S-1-2-0",
				Name = "LOCAL"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.CreatorOwnerSid,
				IsAbsolute = true,
				Sid = "S-1-3-0",
				Name = "CREATOR OWNER",
				SddlForm = "CO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.CreatorGroupSid,
				IsAbsolute = true,
				Sid = "S-1-3-1",
				Name = "CREATOR GROUP",
				SddlForm = "CG"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.CreatorOwnerServerSid,
				IsAbsolute = true,
				Sid = "S-1-3-2",
				Name = "CREATOR OWNER SERVER"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.CreatorGroupServerSid,
				IsAbsolute = true,
				Sid = "S-1-3-3",
				Name = "CREATOR GROUP SERVER"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NTAuthoritySid,
				IsAbsolute = true,
				Sid = "S-1-5",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.DialupSid,
				IsAbsolute = true,
				Sid = "S-1-5-1",
				Name = "NT AUTHORITY\\DIALUP"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NetworkSid,
				IsAbsolute = true,
				Sid = "S-1-5-2",
				Name = "NT AUTHORITY\\NETWORK",
				SddlForm = "NU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BatchSid,
				IsAbsolute = true,
				Sid = "S-1-5-3",
				Name = "NT AUTHORITY\\BATCH"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.InteractiveSid,
				IsAbsolute = true,
				Sid = "S-1-5-4",
				Name = "NT AUTHORITY\\INTERACTIVE",
				SddlForm = "IU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.ServiceSid,
				IsAbsolute = true,
				Sid = "S-1-5-6",
				Name = "NT AUTHORITY\\SERVICE",
				SddlForm = "SU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AnonymousSid,
				IsAbsolute = true,
				Sid = "S-1-5-7",
				Name = "NT AUTHORITY\\ANONYMOUS LOGON",
				SddlForm = "AN"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.ProxySid,
				IsAbsolute = true,
				Sid = "S-1-5-8",
				Name = "NT AUTHORITY\\PROXY"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.EnterpriseControllersSid,
				IsAbsolute = true,
				Sid = "S-1-5-9",
				Name = "NT AUTHORITY\\ENTERPRISE DOMAIN CONTROLLERS",
				SddlForm = "ED"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.SelfSid,
				IsAbsolute = true,
				Sid = "S-1-5-10",
				Name = "NT AUTHORITY\\SELF",
				SddlForm = "PS"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AuthenticatedUserSid,
				IsAbsolute = true,
				Sid = "S-1-5-11",
				Name = "NT AUTHORITY\\Authenticated Users",
				SddlForm = "AU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.RestrictedCodeSid,
				IsAbsolute = true,
				Sid = "S-1-5-12",
				Name = "NT AUTHORITY\\RESTRICTED",
				SddlForm = "RC"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.TerminalServerSid,
				IsAbsolute = true,
				Sid = "S-1-5-13",
				Name = "NT AUTHORITY\\TERMINAL SERVER USER"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.RemoteLogonIdSid,
				IsAbsolute = true,
				Sid = "S-1-5-14",
				Name = "NT AUTHORITY\\REMOTE INTERACTIVE LOGON"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.LocalSystemSid,
				IsAbsolute = true,
				Sid = "S-1-5-18",
				Name = "NT AUTHORITY\\SYSTEM",
				SddlForm = "SY"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.LocalServiceSid,
				IsAbsolute = true,
				Sid = "S-1-5-19",
				Name = "NT AUTHORITY\\LOCAL SERVICE",
				SddlForm = "LS"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NetworkServiceSid,
				IsAbsolute = true,
				Sid = "S-1-5-20",
				Name = "NT AUTHORITY\\NETWORK SERVICE",
				SddlForm = "NS"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinDomainSid,
				IsAbsolute = true,
				Sid = "S-1-5-32",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinAdministratorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-544",
				Name = "BUILTIN\\Administrators",
				SddlForm = "BA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-545",
				Name = "BUILTIN\\Users",
				SddlForm = "BU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinGuestsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-546",
				Name = "BUILTIN\\Guests",
				SddlForm = "BG"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPowerUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-547",
				Name = null,
				SddlForm = "PU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinAccountOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-548",
				Name = null,
				SddlForm = "AO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinSystemOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-549",
				Name = null,
				SddlForm = "SO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPrintOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-550",
				Name = null,
				SddlForm = "PO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinBackupOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-551",
				Name = null,
				SddlForm = "BO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinReplicatorSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-552",
				Name = null,
				SddlForm = "RE"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPreWindows2000CompatibleAccessSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-554",
				Name = null,
				SddlForm = "RU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinRemoteDesktopUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-555",
				Name = null,
				SddlForm = "RD"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinNetworkConfigurationOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-556",
				Name = null,
				SddlForm = "NO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountAdministratorSid,
				IsAbsolute = false,
				Rid = "500",
				SddlForm = "LA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountGuestSid,
				IsAbsolute = false,
				Rid = "501",
				SddlForm = "LG"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountKrbtgtSid,
				IsAbsolute = false,
				Rid = "502"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountDomainAdminsSid,
				IsAbsolute = false,
				Rid = "512",
				SddlForm = "DA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountDomainUsersSid,
				IsAbsolute = false,
				Rid = "513",
				SddlForm = "DU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountDomainGuestsSid,
				IsAbsolute = false,
				Rid = "514",
				SddlForm = "DG"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountComputersSid,
				IsAbsolute = false,
				Rid = "515",
				SddlForm = "DC"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountControllersSid,
				IsAbsolute = false,
				Rid = "516",
				SddlForm = "DD"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountCertAdminsSid,
				IsAbsolute = false,
				Rid = "517",
				SddlForm = "CA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountSchemaAdminsSid,
				IsAbsolute = false,
				Rid = "518",
				SddlForm = "SA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountEnterpriseAdminsSid,
				IsAbsolute = false,
				Rid = "519",
				SddlForm = "EA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountPolicyAdminsSid,
				IsAbsolute = false,
				Rid = "520",
				SddlForm = "PA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountRasAndIasServersSid,
				IsAbsolute = false,
				Rid = "553",
				SddlForm = "RS"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NtlmAuthenticationSid,
				IsAbsolute = true,
				Sid = "S-1-5-64-10",
				Name = "NT AUTHORITY\\NTLM Authentication"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.DigestAuthenticationSid,
				IsAbsolute = true,
				Sid = "S-1-5-64-21",
				Name = "NT AUTHORITY\\Digest Authentication"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.SChannelAuthenticationSid,
				IsAbsolute = true,
				Sid = "S-1-5-64-14",
				Name = "NT AUTHORITY\\SChannel Authentication"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.ThisOrganizationSid,
				IsAbsolute = true,
				Sid = "S-1-5-15",
				Name = "NT AUTHORITY\\This Organization"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.OtherOrganizationSid,
				IsAbsolute = true,
				Sid = "S-1-5-1000",
				Name = "NT AUTHORITY\\Other Organization"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinIncomingForestTrustBuildersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-557",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPerformanceMonitoringUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-558",
				Name = "BUILTIN\\Performance Monitor Users",
				SddlForm = "MU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPerformanceLoggingUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-559",
				Name = "BUILTIN\\Performance Log Users"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinAuthorizationAccessSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-560",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinBuiltinTerminalServerLicenseServersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-561",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinLowLabelSid,
				IsAbsolute = false,
				Rid = "4096",
				SddlForm = "LW"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinMediumLabelSid,
				IsAbsolute = false,
				Rid = "8192",
				SddlForm = "ME"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinHighLabelSid,
				IsAbsolute = false,
				Rid = "12288",
				SddlForm = "HI"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinSystemLabelSid,
				IsAbsolute = false,
				Rid = "16384",
				SddlForm = "SI"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinEnterpriseReadonlyControllersSid,
				IsAbsolute = false,
				Rid = "521",
				SddlForm = "RO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinBuiltinCertSvcDComAccessGroup,
				IsAbsolute = false,
				Rid = "574",
				SddlForm = "CD"
			}
		};
	}
}
