using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200025E RID: 606
	internal struct FederatedAuthenticationFeatureExtensionData
	{
		// Token: 0x040013AB RID: 5035
		internal TdsEnums.FedAuthLibrary libraryType;

		// Token: 0x040013AC RID: 5036
		internal bool fedAuthRequiredPreLoginResponse;

		// Token: 0x040013AD RID: 5037
		internal byte[] accessToken;
	}
}
