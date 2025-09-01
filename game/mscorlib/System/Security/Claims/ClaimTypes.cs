using System;

namespace System.Security.Claims
{
	/// <summary>Defines constants for the well-known claim types that can be assigned to a subject. This class cannot be inherited.</summary>
	// Token: 0x020004EF RID: 1263
	public static class ClaimTypes
	{
		// Token: 0x04002348 RID: 9032
		internal const string ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";

		/// <summary>The URI for a claim that specifies the instant at which an entity was authenticated; http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant.</summary>
		// Token: 0x04002349 RID: 9033
		public const string AuthenticationInstant = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant";

		/// <summary>The URI for a claim that specifies the method with which an entity was authenticated; http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod.</summary>
		// Token: 0x0400234A RID: 9034
		public const string AuthenticationMethod = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";

		/// <summary>The URI for a claim that specifies the cookie path; http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath.</summary>
		// Token: 0x0400234B RID: 9035
		public const string CookiePath = "http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath";

		/// <summary>The URI for a claim that specifies the deny-only primary SID on an entity; http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid. A deny-only SID denies the specified entity to a securable object.</summary>
		// Token: 0x0400234C RID: 9036
		public const string DenyOnlyPrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid";

		/// <summary>The URI for a claim that specifies the deny-only primary group SID on an entity; http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid. A deny-only SID denies the specified entity to a securable object.</summary>
		// Token: 0x0400234D RID: 9037
		public const string DenyOnlyPrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup.</summary>
		// Token: 0x0400234E RID: 9038
		public const string DenyOnlyWindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa.</summary>
		// Token: 0x0400234F RID: 9039
		public const string Dsa = "http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration.</summary>
		// Token: 0x04002350 RID: 9040
		public const string Expiration = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/expired.</summary>
		// Token: 0x04002351 RID: 9041
		public const string Expired = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expired";

		/// <summary>The URI for a claim that specifies the SID for the group of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid.</summary>
		// Token: 0x04002352 RID: 9042
		public const string GroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent.</summary>
		// Token: 0x04002353 RID: 9043
		public const string IsPersistent = "http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent";

		/// <summary>The URI for a claim that specifies the primary group SID of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid.</summary>
		// Token: 0x04002354 RID: 9044
		public const string PrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid";

		/// <summary>The URI for a claim that specifies the primary SID of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid.</summary>
		// Token: 0x04002355 RID: 9045
		public const string PrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";

		/// <summary>The URI for a claim that specifies the role of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/role.</summary>
		// Token: 0x04002356 RID: 9046
		public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		/// <summary>The URI for a claim that specifies a serial number, http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber.</summary>
		// Token: 0x04002357 RID: 9047
		public const string SerialNumber = "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata.</summary>
		// Token: 0x04002358 RID: 9048
		public const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/version.</summary>
		// Token: 0x04002359 RID: 9049
		public const string Version = "http://schemas.microsoft.com/ws/2008/06/identity/claims/version";

		/// <summary>The URI for a claim that specifies the Windows domain account name of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname.</summary>
		// Token: 0x0400235A RID: 9050
		public const string WindowsAccountName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim.</summary>
		// Token: 0x0400235B RID: 9051
		public const string WindowsDeviceClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup.</summary>
		// Token: 0x0400235C RID: 9052
		public const string WindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim.</summary>
		// Token: 0x0400235D RID: 9053
		public const string WindowsUserClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion.</summary>
		// Token: 0x0400235E RID: 9054
		public const string WindowsFqbnVersion = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority.</summary>
		// Token: 0x0400235F RID: 9055
		public const string WindowsSubAuthority = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority";

		// Token: 0x04002360 RID: 9056
		internal const string ClaimType2005Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";

		/// <summary>The URI for a claim that specifies the anonymous user; http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous.</summary>
		// Token: 0x04002361 RID: 9057
		public const string Anonymous = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";

		/// <summary>The URI for a claim that specifies details about whether an identity is authenticated, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authenticated.</summary>
		// Token: 0x04002362 RID: 9058
		public const string Authentication = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";

		/// <summary>The URI for a claim that specifies an authorization decision on an entity; http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision.</summary>
		// Token: 0x04002363 RID: 9059
		public const string AuthorizationDecision = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";

		/// <summary>The URI for a claim that specifies the country/region in which an entity resides, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country.</summary>
		// Token: 0x04002364 RID: 9060
		public const string Country = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";

		/// <summary>The URI for a claim that specifies the date of birth of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth.</summary>
		// Token: 0x04002365 RID: 9061
		public const string DateOfBirth = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";

		/// <summary>The URI for a claim that specifies the DNS name associated with the computer name or with the alternative name of either the subject or issuer of an X.509 certificate, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns.</summary>
		// Token: 0x04002366 RID: 9062
		public const string Dns = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";

		/// <summary>The URI for a claim that specifies a deny-only security identifier (SID) for an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid. A deny-only SID denies the specified entity to a securable object.</summary>
		// Token: 0x04002367 RID: 9063
		public const string DenyOnlySid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";

		/// <summary>The URI for a claim that specifies the email address of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress.</summary>
		// Token: 0x04002368 RID: 9064
		public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

		/// <summary>The URI for a claim that specifies the gender of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender.</summary>
		// Token: 0x04002369 RID: 9065
		public const string Gender = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";

		/// <summary>The URI for a claim that specifies the given name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname.</summary>
		// Token: 0x0400236A RID: 9066
		public const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";

		/// <summary>The URI for a claim that specifies a hash value, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash.</summary>
		// Token: 0x0400236B RID: 9067
		public const string Hash = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";

		/// <summary>The URI for a claim that specifies the home phone number of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone.</summary>
		// Token: 0x0400236C RID: 9068
		public const string HomePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";

		/// <summary>The URI for a claim that specifies the locale in which an entity resides, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality.</summary>
		// Token: 0x0400236D RID: 9069
		public const string Locality = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";

		/// <summary>The URI for a claim that specifies the mobile phone number of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone.</summary>
		// Token: 0x0400236E RID: 9070
		public const string MobilePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";

		/// <summary>The URI for a claim that specifies the name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name.</summary>
		// Token: 0x0400236F RID: 9071
		public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		/// <summary>The URI for a claim that specifies the name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier.</summary>
		// Token: 0x04002370 RID: 9072
		public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

		/// <summary>The URI for a claim that specifies the alternative phone number of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone.</summary>
		// Token: 0x04002371 RID: 9073
		public const string OtherPhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";

		/// <summary>The URI for a claim that specifies the postal code of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode.</summary>
		// Token: 0x04002372 RID: 9074
		public const string PostalCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";

		/// <summary>The URI for a claim that specifies an RSA key, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa.</summary>
		// Token: 0x04002373 RID: 9075
		public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";

		/// <summary>The URI for a claim that specifies a security identifier (SID), http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid.</summary>
		// Token: 0x04002374 RID: 9076
		public const string Sid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";

		/// <summary>The URI for a claim that specifies a service principal name (SPN) claim, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn.</summary>
		// Token: 0x04002375 RID: 9077
		public const string Spn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";

		/// <summary>The URI for a claim that specifies the state or province in which an entity resides, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince.</summary>
		// Token: 0x04002376 RID: 9078
		public const string StateOrProvince = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";

		/// <summary>The URI for a claim that specifies the street address of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress.</summary>
		// Token: 0x04002377 RID: 9079
		public const string StreetAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";

		/// <summary>The URI for a claim that specifies the surname of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname.</summary>
		// Token: 0x04002378 RID: 9080
		public const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

		/// <summary>The URI for a claim that identifies the system entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system.</summary>
		// Token: 0x04002379 RID: 9081
		public const string System = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";

		/// <summary>The URI for a claim that specifies a thumbprint, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint. A thumbprint is a globally unique SHA-1 hash of an X.509 certificate.</summary>
		// Token: 0x0400237A RID: 9082
		public const string Thumbprint = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";

		/// <summary>The URI for a claim that specifies a user principal name (UPN), http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn.</summary>
		// Token: 0x0400237B RID: 9083
		public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";

		/// <summary>The URI for a claim that specifies a URI, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri.</summary>
		// Token: 0x0400237C RID: 9084
		public const string Uri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";

		/// <summary>The URI for a claim that specifies the webpage of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage.</summary>
		// Token: 0x0400237D RID: 9085
		public const string Webpage = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";

		/// <summary>The URI for a distinguished name claim of an X.509 certificate, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname. The X.500 standard defines the methodology for defining distinguished names that are used by X.509 certificates.</summary>
		// Token: 0x0400237E RID: 9086
		public const string X500DistinguishedName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";

		// Token: 0x0400237F RID: 9087
		internal const string ClaimType2009Namespace = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims";

		/// <summary>http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor.</summary>
		// Token: 0x04002380 RID: 9088
		public const string Actor = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor";
	}
}
