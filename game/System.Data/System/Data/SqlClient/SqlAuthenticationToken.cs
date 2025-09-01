using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents an AD authentication token.</summary>
	// Token: 0x020003EF RID: 1007
	public class SqlAuthenticationToken
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlAuthenticationToken" /> class.</summary>
		/// <param name="accessToken">The access token.</param>
		/// <param name="expiresOn">The token expiration time.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="accessToken" /> parameter is <see langword="null" /> or empty.</exception>
		// Token: 0x06002F91 RID: 12177 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlAuthenticationToken(string accessToken, DateTimeOffset expiresOn)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the token string.</summary>
		/// <returns>The token string.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string AccessToken
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the token expiration time.</summary>
		/// <returns>The token expiration time.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x000CBACC File Offset: 0x000C9CCC
		public DateTimeOffset ExpiresOn
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(DateTimeOffset);
			}
		}
	}
}
