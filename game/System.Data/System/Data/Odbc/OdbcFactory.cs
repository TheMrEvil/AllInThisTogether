using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	/// <summary>Represents a set of methods for creating instances of the ODBC provider's implementation of the data source classes.</summary>
	// Token: 0x020002EA RID: 746
	public sealed class OdbcFactory : DbProviderFactory
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x0005ADE6 File Offset: 0x00058FE6
		private OdbcFactory()
		{
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		// Token: 0x06002114 RID: 8468 RVA: 0x0009A79A File Offset: 0x0009899A
		public override DbCommand CreateCommand()
		{
			return new OdbcCommand();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		// Token: 0x06002115 RID: 8469 RVA: 0x0009A7A1 File Offset: 0x000989A1
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new OdbcCommandBuilder();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		// Token: 0x06002116 RID: 8470 RVA: 0x0009A7A8 File Offset: 0x000989A8
		public override DbConnection CreateConnection()
		{
			return new OdbcConnection();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		// Token: 0x06002117 RID: 8471 RVA: 0x0009A7AF File Offset: 0x000989AF
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OdbcConnectionStringBuilder();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		// Token: 0x06002118 RID: 8472 RVA: 0x0009A7B6 File Offset: 0x000989B6
		public override DbDataAdapter CreateDataAdapter()
		{
			return new OdbcDataAdapter();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		// Token: 0x06002119 RID: 8473 RVA: 0x00094874 File Offset: 0x00092A74
		public override DbParameter CreateParameter()
		{
			return new OdbcParameter();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Security.CodeAccessPermission" /> instance.</summary>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		// Token: 0x0600211A RID: 8474 RVA: 0x0009A7BD File Offset: 0x000989BD
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new OdbcPermission(state);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0009A7C5 File Offset: 0x000989C5
		// Note: this type is marked as 'beforefieldinit'.
		static OdbcFactory()
		{
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.Odbc.OdbcFactory" />, which can be used to retrieve strongly-typed data objects.</summary>
		// Token: 0x040017BC RID: 6076
		public static readonly OdbcFactory Instance = new OdbcFactory();
	}
}
