using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents AD authentication parameters passed by a driver to authentication providers.</summary>
	// Token: 0x020003ED RID: 1005
	public class SqlAuthenticationParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlAuthenticationParameters" /> class using the specified authentication method, server name, database name, resource URI, authority URI, user login name/ID, user password and connection ID.</summary>
		/// <param name="authenticationMethod">One of the enumeration values that specifies the authentication method.</param>
		/// <param name="serverName">The server name.</param>
		/// <param name="databaseName">The database name.</param>
		/// <param name="resource">The resource URI.</param>
		/// <param name="authority">The authority URI.</param>
		/// <param name="userId">The user login name/ID.</param>
		/// <param name="password">The user password.</param>
		/// <param name="connectionId">The connection ID.</param>
		// Token: 0x06002F81 RID: 12161 RVA: 0x000108A6 File Offset: 0x0000EAA6
		protected SqlAuthenticationParameters(SqlAuthenticationMethod authenticationMethod, string serverName, string databaseName, string resource, string authority, string userId, string password, Guid connectionId)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the authentication method.</summary>
		/// <returns>The authentication method.</returns>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x000CBA78 File Offset: 0x000C9C78
		public SqlAuthenticationMethod AuthenticationMethod
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return SqlAuthenticationMethod.NotSpecified;
			}
		}

		/// <summary>Gets the authority URI.</summary>
		/// <returns>The authority URI.</returns>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string Authority
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the connection ID.</summary>
		/// <returns>The connection ID.</returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002F84 RID: 12164 RVA: 0x000CBA94 File Offset: 0x000C9C94
		public Guid ConnectionId
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(Guid);
			}
		}

		/// <summary>Gets the database name.</summary>
		/// <returns>The database name.</returns>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string DatabaseName
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the user password.</summary>
		/// <returns>The user password.</returns>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002F86 RID: 12166 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string Password
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the resource URI.</summary>
		/// <returns>The resource URI.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string Resource
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the server name.</summary>
		/// <returns>The server name.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string ServerName
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the user login name/ID.</summary>
		/// <returns>The user login name/ID.</returns>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002F89 RID: 12169 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string UserId
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
