using System;

namespace System.Data.SqlClient
{
	/// <summary>Describes the different SQL authentication methods that can be used by a client connecting to Azure SQL Database. For details, see Connecting to SQL Database By Using Azure Active Directory Authentication.</summary>
	// Token: 0x02000284 RID: 644
	public enum SqlAuthenticationMethod
	{
		/// <summary>The authentication method is not specified.</summary>
		// Token: 0x040014B2 RID: 5298
		NotSpecified,
		/// <summary>The authentication method is Sql Password.</summary>
		// Token: 0x040014B3 RID: 5299
		SqlPassword,
		/// <summary>The authentication method uses Active Directory Password. Use Active Directory Password to connect to a SQL Database using an Azure AD principal name and password.</summary>
		// Token: 0x040014B4 RID: 5300
		ActiveDirectoryPassword,
		/// <summary>The authentication method uses Active Directory Integrated. Use Active Directory Integrated to connect to a SQL Database using integrated Windows authentication.</summary>
		// Token: 0x040014B5 RID: 5301
		ActiveDirectoryIntegrated,
		/// <summary>The authentication method uses Active Directory Interactive. Available since the .NET Framework 4.7.2.</summary>
		// Token: 0x040014B6 RID: 5302
		ActiveDirectoryInteractive
	}
}
