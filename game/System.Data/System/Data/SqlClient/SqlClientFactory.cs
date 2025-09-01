using System;
using System.Data.Common;
using System.Data.Sql;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a set of methods for creating instances of the <see cref="N:System.Data.SqlClient" /> provider's implementation of the data source classes.</summary>
	// Token: 0x020001AF RID: 431
	public sealed class SqlClientFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06001531 RID: 5425 RVA: 0x0005ADE6 File Offset: 0x00058FE6
		private SqlClientFactory()
		{
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		// Token: 0x06001532 RID: 5426 RVA: 0x00060C0C File Offset: 0x0005EE0C
		public override DbCommand CreateCommand()
		{
			return new SqlCommand();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		// Token: 0x06001533 RID: 5427 RVA: 0x00060C13 File Offset: 0x0005EE13
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new SqlCommandBuilder();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		// Token: 0x06001534 RID: 5428 RVA: 0x00060C1A File Offset: 0x0005EE1A
		public override DbConnection CreateConnection()
		{
			return new SqlConnection();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		// Token: 0x06001535 RID: 5429 RVA: 0x00060C21 File Offset: 0x0005EE21
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlConnectionStringBuilder();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		// Token: 0x06001536 RID: 5430 RVA: 0x00060C28 File Offset: 0x0005EE28
		public override DbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		// Token: 0x06001537 RID: 5431 RVA: 0x00060C2F File Offset: 0x0005EE2F
		public override DbParameter CreateParameter()
		{
			return new SqlParameter();
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> can be created.</summary>
		/// <returns>
		///   <see langword="true" /> if a <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> can be created; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" />.</summary>
		/// <returns>A new data source enumerator.</returns>
		// Token: 0x06001539 RID: 5433 RVA: 0x00060C36 File Offset: 0x0005EE36
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return SqlDataSourceEnumerator.Instance;
		}

		/// <summary>Returns a new <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <returns>A strongly typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		// Token: 0x0600153A RID: 5434 RVA: 0x00060C3D File Offset: 0x0005EE3D
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new SqlClientPermission(state);
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00060C45 File Offset: 0x0005EE45
		// Note: this type is marked as 'beforefieldinit'.
		static SqlClientFactory()
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.IServiceProvider.GetService(System.Type)" />.</summary>
		/// <param name="serviceType">An object that specifies the type of service object to get.</param>
		/// <returns>A service object.</returns>
		// Token: 0x0600153C RID: 5436 RVA: 0x00060C51 File Offset: 0x0005EE51
		object IServiceProvider.GetService(Type serviceType)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.SqlClient.SqlClientFactory" />. This can be used to retrieve strongly typed data objects.</summary>
		// Token: 0x04000D82 RID: 3458
		public static readonly SqlClientFactory Instance = new SqlClientFactory();
	}
}
