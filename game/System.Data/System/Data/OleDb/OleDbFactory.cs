using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Represents a set of methods for creating instances of the OLEDB provider's implementation of the data source classes.</summary>
	// Token: 0x02000167 RID: 359
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbFactory : DbProviderFactory
	{
		// Token: 0x06001367 RID: 4967 RVA: 0x0005ADE6 File Offset: 0x00058FE6
		internal OleDbFactory()
		{
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		// Token: 0x06001368 RID: 4968 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override DbCommand CreateCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		// Token: 0x06001369 RID: 4969 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override DbCommandBuilder CreateCommandBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		// Token: 0x0600136A RID: 4970 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override DbConnection CreateConnection()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		// Token: 0x0600136B RID: 4971 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		// Token: 0x0600136C RID: 4972 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override DbDataAdapter CreateDataAdapter()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		// Token: 0x0600136D RID: 4973 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override DbParameter CreateParameter()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Security.CodeAccessPermission" /> instance.</summary>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <returns>A strongly-typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		// Token: 0x0600136E RID: 4974 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.OleDb.OleDbFactory" />. This can be used to retrieve strongly-typed data objects.</summary>
		// Token: 0x04000BD4 RID: 3028
		public static readonly OleDbFactory Instance;
	}
}
