using System;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Represents a set of methods for creating instances of a provider's implementation of the data source classes.</summary>
	// Token: 0x0200039D RID: 925
	public abstract class DbProviderFactory
	{
		/// <summary>Returns a new instance of the provider's class that implements the provider's version of the <see cref="T:System.Security.CodeAccessPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <returns>A <see cref="T:System.Security.CodeAccessPermission" /> object for the specified <see cref="T:System.Security.Permissions.PermissionState" />.</returns>
		// Token: 0x06002CF5 RID: 11509 RVA: 0x00003E32 File Offset: 0x00002032
		public virtual CodeAccessPermission CreatePermission(PermissionState state)
		{
			return null;
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> class.</summary>
		// Token: 0x06002CF6 RID: 11510 RVA: 0x00003D93 File Offset: 0x00001F93
		protected DbProviderFactory()
		{
		}

		/// <summary>Specifies whether the specific <see cref="T:System.Data.Common.DbProviderFactory" /> supports the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class.</summary>
		/// <returns>
		///   <see langword="true" /> if the instance of the <see cref="T:System.Data.Common.DbProviderFactory" /> supports the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class; otherwise <see langword="false" />.</returns>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x00006D64 File Offset: 0x00004F64
		public virtual bool CanCreateDataSourceEnumerator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x000BEF18 File Offset: 0x000BD118
		public virtual bool CanCreateDataAdapter
		{
			get
			{
				if (this._canCreateDataAdapter == null)
				{
					using (DbDataAdapter dbDataAdapter = this.CreateDataAdapter())
					{
						this._canCreateDataAdapter = new bool?(dbDataAdapter != null);
					}
				}
				return this._canCreateDataAdapter.Value;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x000BEF70 File Offset: 0x000BD170
		public virtual bool CanCreateCommandBuilder
		{
			get
			{
				if (this._canCreateCommandBuilder == null)
				{
					using (DbCommandBuilder dbCommandBuilder = this.CreateCommandBuilder())
					{
						this._canCreateCommandBuilder = new bool?(dbCommandBuilder != null);
					}
				}
				return this._canCreateCommandBuilder.Value;
			}
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommand" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		// Token: 0x06002CFA RID: 11514 RVA: 0x00003E32 File Offset: 0x00002032
		public virtual DbCommand CreateCommand()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		// Token: 0x06002CFB RID: 11515 RVA: 0x00003E32 File Offset: 0x00002032
		public virtual DbCommandBuilder CreateCommandBuilder()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnection" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		// Token: 0x06002CFC RID: 11516 RVA: 0x00003E32 File Offset: 0x00002032
		public virtual DbConnection CreateConnection()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		// Token: 0x06002CFD RID: 11517 RVA: 0x00003E32 File Offset: 0x00002032
		public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbDataAdapter" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		// Token: 0x06002CFE RID: 11518 RVA: 0x00003E32 File Offset: 0x00002032
		public virtual DbDataAdapter CreateDataAdapter()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbParameter" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		// Token: 0x06002CFF RID: 11519 RVA: 0x00003E32 File Offset: 0x00002032
		public virtual DbParameter CreateParameter()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbDataSourceEnumerator" />.</returns>
		// Token: 0x06002D00 RID: 11520 RVA: 0x00003E32 File Offset: 0x00002032
		public virtual DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return null;
		}

		// Token: 0x04001B89 RID: 7049
		private bool? _canCreateDataAdapter;

		// Token: 0x04001B8A RID: 7050
		private bool? _canCreateCommandBuilder;
	}
}
