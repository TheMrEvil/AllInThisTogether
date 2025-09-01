using System;
using System.Threading.Tasks;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Defines the core behavior of authentication providers and provides a base class for derived classes.</summary>
	// Token: 0x020003EE RID: 1006
	public abstract class SqlAuthenticationProvider
	{
		/// <summary>Called from constructors in derived classes to initialize the <see cref="T:System.Data.SqlClient.SqlAuthenticationProvider" /> class.</summary>
		// Token: 0x06002F8A RID: 12170 RVA: 0x000108A6 File Offset: 0x0000EAA6
		protected SqlAuthenticationProvider()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Acquires a security token from the authority.</summary>
		/// <param name="parameters">The Active Directory authentication parameters passed by the driver to authentication providers.</param>
		/// <returns>Represents an asynchronous operation that returns the AD authentication token.</returns>
		// Token: 0x06002F8B RID: 12171
		public abstract Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters);

		/// <summary>This method is called immediately before the provider is added to SQL drivers registry.</summary>
		/// <param name="authenticationMethod">The authentication method.</param>
		// Token: 0x06002F8C RID: 12172 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public virtual void BeforeLoad(SqlAuthenticationMethod authenticationMethod)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>This method is called immediately before the provider is removed from the SQL drivers registry.</summary>
		/// <param name="authenticationMethod">The authentication method.</param>
		// Token: 0x06002F8D RID: 12173 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public virtual void BeforeUnload(SqlAuthenticationMethod authenticationMethod)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets an authentication provider by method.</summary>
		/// <param name="authenticationMethod">The authentication method.</param>
		/// <returns>The authentication provider or <see langword="null" /> if not found.</returns>
		// Token: 0x06002F8E RID: 12174 RVA: 0x00060C51 File Offset: 0x0005EE51
		public static SqlAuthenticationProvider GetProvider(SqlAuthenticationMethod authenticationMethod)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Indicates whether the specified authentication method is supported.</summary>
		/// <param name="authenticationMethod">The authentication method.</param>
		/// <returns>
		///   <see langword="true" /> if the specified authentication method is supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002F8F RID: 12175
		public abstract bool IsSupported(SqlAuthenticationMethod authenticationMethod);

		/// <summary>Sets an authentication provider by method.</summary>
		/// <param name="authenticationMethod">The authentication method.</param>
		/// <param name="provider">The authentication provider.</param>
		/// <returns>
		///   <see langword="true" /> if the operation succeeded; otherwise, <see langword="false" /> (for example, the existing provider disallows overriding).</returns>
		// Token: 0x06002F90 RID: 12176 RVA: 0x000CBAB0 File Offset: 0x000C9CB0
		public static bool SetProvider(SqlAuthenticationMethod authenticationMethod, SqlAuthenticationProvider provider)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
