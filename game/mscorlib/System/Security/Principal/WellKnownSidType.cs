using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines a set of commonly used security identifiers (SIDs).</summary>
	// Token: 0x020004E9 RID: 1257
	[ComVisible(false)]
	public enum WellKnownSidType
	{
		/// <summary>Indicates a null SID.</summary>
		// Token: 0x040022CD RID: 8909
		NullSid,
		/// <summary>Indicates a SID that matches everyone.</summary>
		// Token: 0x040022CE RID: 8910
		WorldSid,
		/// <summary>Indicates a local SID.</summary>
		// Token: 0x040022CF RID: 8911
		LocalSid,
		/// <summary>Indicates a SID that matches the owner or creator of an object.</summary>
		// Token: 0x040022D0 RID: 8912
		CreatorOwnerSid,
		/// <summary>Indicates a SID that matches the creator group of an object.</summary>
		// Token: 0x040022D1 RID: 8913
		CreatorGroupSid,
		/// <summary>Indicates a creator owner server SID.</summary>
		// Token: 0x040022D2 RID: 8914
		CreatorOwnerServerSid,
		/// <summary>Indicates a creator group server SID.</summary>
		// Token: 0x040022D3 RID: 8915
		CreatorGroupServerSid,
		/// <summary>Indicates a SID for the Windows NT authority.</summary>
		// Token: 0x040022D4 RID: 8916
		NTAuthoritySid,
		/// <summary>Indicates a SID for a dial-up account.</summary>
		// Token: 0x040022D5 RID: 8917
		DialupSid,
		/// <summary>Indicates a SID for a network account. This SID is added to the process of a token when it logs on across a network.</summary>
		// Token: 0x040022D6 RID: 8918
		NetworkSid,
		/// <summary>Indicates a SID for a batch process. This SID is added to the process of a token when it logs on as a batch job.</summary>
		// Token: 0x040022D7 RID: 8919
		BatchSid,
		/// <summary>Indicates a SID for an interactive account. This SID is added to the process of a token when it logs on interactively.</summary>
		// Token: 0x040022D8 RID: 8920
		InteractiveSid,
		/// <summary>Indicates a SID for a service. This SID is added to the process of a token when it logs on as a service.</summary>
		// Token: 0x040022D9 RID: 8921
		ServiceSid,
		/// <summary>Indicates a SID for the anonymous account.</summary>
		// Token: 0x040022DA RID: 8922
		AnonymousSid,
		/// <summary>Indicates a proxy SID.</summary>
		// Token: 0x040022DB RID: 8923
		ProxySid,
		/// <summary>Indicates a SID for an enterprise controller.</summary>
		// Token: 0x040022DC RID: 8924
		EnterpriseControllersSid,
		/// <summary>Indicates a SID for self.</summary>
		// Token: 0x040022DD RID: 8925
		SelfSid,
		/// <summary>Indicates a SID for an authenticated user.</summary>
		// Token: 0x040022DE RID: 8926
		AuthenticatedUserSid,
		/// <summary>Indicates a SID for restricted code.</summary>
		// Token: 0x040022DF RID: 8927
		RestrictedCodeSid,
		/// <summary>Indicates a SID that matches a terminal server account.</summary>
		// Token: 0x040022E0 RID: 8928
		TerminalServerSid,
		/// <summary>Indicates a SID that matches remote logons.</summary>
		// Token: 0x040022E1 RID: 8929
		RemoteLogonIdSid,
		/// <summary>Indicates a SID that matches logon IDs.</summary>
		// Token: 0x040022E2 RID: 8930
		LogonIdsSid,
		/// <summary>Indicates a SID that matches the local system.</summary>
		// Token: 0x040022E3 RID: 8931
		LocalSystemSid,
		/// <summary>Indicates a SID that matches a local service.</summary>
		// Token: 0x040022E4 RID: 8932
		LocalServiceSid,
		/// <summary>Indicates a SID that matches a network service.</summary>
		// Token: 0x040022E5 RID: 8933
		NetworkServiceSid,
		/// <summary>Indicates a SID that matches the domain account.</summary>
		// Token: 0x040022E6 RID: 8934
		BuiltinDomainSid,
		/// <summary>Indicates a SID that matches the administrator account.</summary>
		// Token: 0x040022E7 RID: 8935
		BuiltinAdministratorsSid,
		/// <summary>Indicates a SID that matches built-in user accounts.</summary>
		// Token: 0x040022E8 RID: 8936
		BuiltinUsersSid,
		/// <summary>Indicates a SID that matches the guest account.</summary>
		// Token: 0x040022E9 RID: 8937
		BuiltinGuestsSid,
		/// <summary>Indicates a SID that matches the power users group.</summary>
		// Token: 0x040022EA RID: 8938
		BuiltinPowerUsersSid,
		/// <summary>Indicates a SID that matches the account operators account.</summary>
		// Token: 0x040022EB RID: 8939
		BuiltinAccountOperatorsSid,
		/// <summary>Indicates a SID that matches the system operators group.</summary>
		// Token: 0x040022EC RID: 8940
		BuiltinSystemOperatorsSid,
		/// <summary>Indicates a SID that matches the print operators group.</summary>
		// Token: 0x040022ED RID: 8941
		BuiltinPrintOperatorsSid,
		/// <summary>Indicates a SID that matches the backup operators group.</summary>
		// Token: 0x040022EE RID: 8942
		BuiltinBackupOperatorsSid,
		/// <summary>Indicates a SID that matches the replicator account.</summary>
		// Token: 0x040022EF RID: 8943
		BuiltinReplicatorSid,
		/// <summary>Indicates a SID that matches pre-Windows 2000 compatible accounts.</summary>
		// Token: 0x040022F0 RID: 8944
		BuiltinPreWindows2000CompatibleAccessSid,
		/// <summary>Indicates a SID that matches remote desktop users.</summary>
		// Token: 0x040022F1 RID: 8945
		BuiltinRemoteDesktopUsersSid,
		/// <summary>Indicates a SID that matches the network operators group.</summary>
		// Token: 0x040022F2 RID: 8946
		BuiltinNetworkConfigurationOperatorsSid,
		/// <summary>Indicates a SID that matches the account administrators group.</summary>
		// Token: 0x040022F3 RID: 8947
		AccountAdministratorSid,
		/// <summary>Indicates a SID that matches the account guest group.</summary>
		// Token: 0x040022F4 RID: 8948
		AccountGuestSid,
		/// <summary>Indicates a SID that matches the account Kerberos target group.</summary>
		// Token: 0x040022F5 RID: 8949
		AccountKrbtgtSid,
		/// <summary>Indicates a SID that matches the account domain administrator group.</summary>
		// Token: 0x040022F6 RID: 8950
		AccountDomainAdminsSid,
		/// <summary>Indicates a SID that matches the account domain users group.</summary>
		// Token: 0x040022F7 RID: 8951
		AccountDomainUsersSid,
		/// <summary>Indicates a SID that matches the account domain guests group.</summary>
		// Token: 0x040022F8 RID: 8952
		AccountDomainGuestsSid,
		/// <summary>Indicates a SID that matches the account computer group.</summary>
		// Token: 0x040022F9 RID: 8953
		AccountComputersSid,
		/// <summary>Indicates a SID that matches the account controller group.</summary>
		// Token: 0x040022FA RID: 8954
		AccountControllersSid,
		/// <summary>Indicates a SID that matches the certificate administrators group.</summary>
		// Token: 0x040022FB RID: 8955
		AccountCertAdminsSid,
		/// <summary>Indicates a SID that matches the schema administrators group.</summary>
		// Token: 0x040022FC RID: 8956
		AccountSchemaAdminsSid,
		/// <summary>Indicates a SID that matches the enterprise administrators group.</summary>
		// Token: 0x040022FD RID: 8957
		AccountEnterpriseAdminsSid,
		/// <summary>Indicates a SID that matches the policy administrators group.</summary>
		// Token: 0x040022FE RID: 8958
		AccountPolicyAdminsSid,
		/// <summary>Indicates a SID that matches the RAS and IAS server account.</summary>
		// Token: 0x040022FF RID: 8959
		AccountRasAndIasServersSid,
		/// <summary>Indicates a SID present when the Microsoft NTLM authentication package authenticated the client.</summary>
		// Token: 0x04002300 RID: 8960
		NtlmAuthenticationSid,
		/// <summary>Indicates a SID present when the Microsoft Digest authentication package authenticated the client.</summary>
		// Token: 0x04002301 RID: 8961
		DigestAuthenticationSid,
		/// <summary>Indicates a SID present when the Secure Channel (SSL/TLS) authentication package authenticated the client.</summary>
		// Token: 0x04002302 RID: 8962
		SChannelAuthenticationSid,
		/// <summary>Indicates a SID present when the user authenticated from within the forest or across a trust that does not have the selective authentication option enabled. If this SID is present, then <see cref="F:System.Security.Principal.WellKnownSidType.OtherOrganizationSid" /> cannot be present.</summary>
		// Token: 0x04002303 RID: 8963
		ThisOrganizationSid,
		/// <summary>Indicates a SID present when the user authenticated across a forest with the selective authentication option enabled. If this SID is present, then <see cref="F:System.Security.Principal.WellKnownSidType.ThisOrganizationSid" /> cannot be present.</summary>
		// Token: 0x04002304 RID: 8964
		OtherOrganizationSid,
		/// <summary>Indicates a SID that allows a user to create incoming forest trusts. It is added to the token of users who are a member of the Incoming Forest Trust Builders built-in group in the root domain of the forest.</summary>
		// Token: 0x04002305 RID: 8965
		BuiltinIncomingForestTrustBuildersSid,
		/// <summary>Indicates a SID that matches the group of users that have remote access to schedule logging of performance counters on this computer.</summary>
		// Token: 0x04002306 RID: 8966
		BuiltinPerformanceMonitoringUsersSid,
		/// <summary>Indicates a SID that matches the group of users that have remote access to monitor the computer.</summary>
		// Token: 0x04002307 RID: 8967
		BuiltinPerformanceLoggingUsersSid,
		/// <summary>Indicates a SID that matches the Windows Authorization Access group.</summary>
		// Token: 0x04002308 RID: 8968
		BuiltinAuthorizationAccessSid,
		/// <summary>Indicates a SID is present in a server that can issue Terminal Server licenses.</summary>
		// Token: 0x04002309 RID: 8969
		WinBuiltinTerminalServerLicenseServersSid,
		/// <summary>Indicates the maximum defined SID in the <see cref="T:System.Security.Principal.WellKnownSidType" /> enumeration.</summary>
		// Token: 0x0400230A RID: 8970
		MaxDefined = 60,
		// Token: 0x0400230B RID: 8971
		WinBuiltinDCOMUsersSid,
		// Token: 0x0400230C RID: 8972
		WinBuiltinIUsersSid,
		// Token: 0x0400230D RID: 8973
		WinIUserSid,
		// Token: 0x0400230E RID: 8974
		WinBuiltinCryptoOperatorsSid,
		// Token: 0x0400230F RID: 8975
		WinUntrustedLabelSid,
		// Token: 0x04002310 RID: 8976
		WinLowLabelSid,
		// Token: 0x04002311 RID: 8977
		WinMediumLabelSid,
		// Token: 0x04002312 RID: 8978
		WinHighLabelSid,
		// Token: 0x04002313 RID: 8979
		WinSystemLabelSid,
		// Token: 0x04002314 RID: 8980
		WinWriteRestrictedCodeSid,
		// Token: 0x04002315 RID: 8981
		WinCreatorOwnerRightsSid,
		// Token: 0x04002316 RID: 8982
		WinCacheablePrincipalsGroupSid,
		// Token: 0x04002317 RID: 8983
		WinNonCacheablePrincipalsGroupSid,
		// Token: 0x04002318 RID: 8984
		WinEnterpriseReadonlyControllersSid,
		// Token: 0x04002319 RID: 8985
		WinAccountReadonlyControllersSid,
		// Token: 0x0400231A RID: 8986
		WinBuiltinEventLogReadersGroup,
		// Token: 0x0400231B RID: 8987
		WinNewEnterpriseReadonlyControllersSid,
		// Token: 0x0400231C RID: 8988
		WinBuiltinCertSvcDComAccessGroup,
		// Token: 0x0400231D RID: 8989
		WinMediumPlusLabelSid,
		// Token: 0x0400231E RID: 8990
		WinLocalLogonSid,
		// Token: 0x0400231F RID: 8991
		WinConsoleLogonSid,
		// Token: 0x04002320 RID: 8992
		WinThisOrganizationCertificateSid,
		// Token: 0x04002321 RID: 8993
		WinApplicationPackageAuthoritySid,
		// Token: 0x04002322 RID: 8994
		WinBuiltinAnyPackageSid,
		// Token: 0x04002323 RID: 8995
		WinCapabilityInternetClientSid,
		// Token: 0x04002324 RID: 8996
		WinCapabilityInternetClientServerSid,
		// Token: 0x04002325 RID: 8997
		WinCapabilityPrivateNetworkClientServerSid,
		// Token: 0x04002326 RID: 8998
		WinCapabilityPicturesLibrarySid,
		// Token: 0x04002327 RID: 8999
		WinCapabilityVideosLibrarySid,
		// Token: 0x04002328 RID: 9000
		WinCapabilityMusicLibrarySid,
		// Token: 0x04002329 RID: 9001
		WinCapabilityDocumentsLibrarySid,
		// Token: 0x0400232A RID: 9002
		WinCapabilitySharedUserCertificatesSid,
		// Token: 0x0400232B RID: 9003
		WinCapabilityEnterpriseAuthenticationSid,
		// Token: 0x0400232C RID: 9004
		WinCapabilityRemovableStorageSid
	}
}
