using System;
using System.Security;

namespace System.Data.SqlClient
{
	/// <summary>
	///   <see cref="T:System.Data.SqlClient.SqlCredential" /> provides a more secure way to specify the password for a login attempt using SQL Server Authentication.  
	/// <see cref="T:System.Data.SqlClient.SqlCredential" /> is comprised of a user id and a password that will be used for SQL Server Authentication. The password in a <see cref="T:System.Data.SqlClient.SqlCredential" /> object is of type <see cref="T:System.Security.SecureString" />.  
	/// <see cref="T:System.Data.SqlClient.SqlCredential" /> cannot be inherited.  
	/// Windows Authentication (<see langword="Integrated Security = true" />) remains the most secure way to log in to a SQL Server database.</summary>
	// Token: 0x02000282 RID: 642
	[Serializable]
	public sealed class SqlCredential
	{
		/// <summary>Creates an object of type <see cref="T:System.Data.SqlClient.SqlCredential" />.</summary>
		/// <param name="userId">The user id.</param>
		/// <param name="password">The password; a <see cref="T:System.Security.SecureString" /> value marked as read-only.  Passing a read/write <see cref="T:System.Security.SecureString" /> parameter will raise an <see cref="T:System.ArgumentException" />.</param>
		// Token: 0x06001E23 RID: 7715 RVA: 0x0008EF1C File Offset: 0x0008D11C
		public SqlCredential(string userId, SecureString password)
		{
			if (userId == null)
			{
				throw new ArgumentNullException("userId");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this.uid = userId;
			this.pwd = password;
		}

		/// <summary>Gets the user ID component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</summary>
		/// <returns>The user ID component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</returns>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0008EF59 File Offset: 0x0008D159
		public string UserId
		{
			get
			{
				return this.uid;
			}
		}

		/// <summary>Gets the password component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</summary>
		/// <returns>The password component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</returns>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x0008EF61 File Offset: 0x0008D161
		public SecureString Password
		{
			get
			{
				return this.pwd;
			}
		}

		// Token: 0x040014AC RID: 5292
		private string uid = "";

		// Token: 0x040014AD RID: 5293
		private SecureString pwd;
	}
}
