using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;

namespace System.Data.SqlClient
{
	// Token: 0x020001D1 RID: 465
	internal sealed class SqlConnectionFactory : DbConnectionFactory
	{
		// Token: 0x060016C0 RID: 5824 RVA: 0x00067DC0 File Offset: 0x00065FC0
		private SqlConnectionFactory()
		{
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00066410 File Offset: 0x00064610
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00067DC8 File Offset: 0x00065FC8
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection, null);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00067DD8 File Offset: 0x00065FD8
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)options;
			SqlConnectionPoolKey sqlConnectionPoolKey = (SqlConnectionPoolKey)poolKey;
			SessionData reconnectSessionData = null;
			SqlConnection sqlConnection = (SqlConnection)owningConnection;
			bool applyTransientFaultHandling = sqlConnection != null && sqlConnection._applyTransientFaultHandling;
			SqlConnectionString userConnectionOptions = null;
			if (userOptions != null)
			{
				userConnectionOptions = (SqlConnectionString)userOptions;
			}
			else if (sqlConnection != null)
			{
				userConnectionOptions = (SqlConnectionString)sqlConnection.UserConnectionOptions;
			}
			if (sqlConnection != null)
			{
				reconnectSessionData = sqlConnection._recoverySessionData;
			}
			bool redirectedUserInstance = false;
			DbConnectionPoolIdentity identity = null;
			if (sqlConnectionString.IntegratedSecurity)
			{
				if (pool != null)
				{
					identity = pool.Identity;
				}
				else
				{
					identity = DbConnectionPoolIdentity.GetCurrent();
				}
			}
			if (sqlConnectionString.UserInstance)
			{
				redirectedUserInstance = true;
				string instanceName;
				if (pool == null || (pool != null && pool.Count <= 0))
				{
					SqlInternalConnectionTds sqlInternalConnectionTds = null;
					try
					{
						SqlConnectionString connectionOptions = new SqlConnectionString(sqlConnectionString, sqlConnectionString.DataSource, true, new bool?(false));
						sqlInternalConnectionTds = new SqlInternalConnectionTds(identity, connectionOptions, sqlConnectionPoolKey.Credential, null, "", null, false, null, null, applyTransientFaultHandling, null);
						instanceName = sqlInternalConnectionTds.InstanceName;
						if (!instanceName.StartsWith("\\\\.\\", StringComparison.Ordinal))
						{
							throw SQL.NonLocalSSEInstance();
						}
						if (pool != null)
						{
							((SqlConnectionPoolProviderInfo)pool.ProviderInfo).InstanceName = instanceName;
						}
						goto IL_125;
					}
					finally
					{
						if (sqlInternalConnectionTds != null)
						{
							sqlInternalConnectionTds.Dispose();
						}
					}
				}
				instanceName = ((SqlConnectionPoolProviderInfo)pool.ProviderInfo).InstanceName;
				IL_125:
				sqlConnectionString = new SqlConnectionString(sqlConnectionString, instanceName, false, null);
				poolGroupProviderInfo = null;
			}
			return new SqlInternalConnectionTds(identity, sqlConnectionString, sqlConnectionPoolKey.Credential, poolGroupProviderInfo, "", null, redirectedUserInstance, userConnectionOptions, reconnectSessionData, applyTransientFaultHandling, sqlConnectionPoolKey.AccessToken);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00067F54 File Offset: 0x00066154
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new SqlConnectionString(connectionString);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00067F5C File Offset: 0x0006615C
		internal override DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			DbConnectionPoolProviderInfo result = null;
			if (((SqlConnectionString)connectionOptions).UserInstance)
			{
				result = new SqlConnectionPoolProviderInfo();
			}
			return result;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00067F80 File Offset: 0x00066180
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)connectionOptions;
			DbConnectionPoolGroupOptions result = null;
			if (sqlConnectionString.Pooling)
			{
				int num = sqlConnectionString.ConnectTimeout;
				if (0 < num && num < 2147483)
				{
					num *= 1000;
				}
				else if (num >= 2147483)
				{
					num = int.MaxValue;
				}
				result = new DbConnectionPoolGroupOptions(sqlConnectionString.IntegratedSecurity, sqlConnectionString.MinPoolSize, sqlConnectionString.MaxPoolSize, num, sqlConnectionString.LoadBalanceTimeout, sqlConnectionString.Enlist);
			}
			return result;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00067FEF File Offset: 0x000661EF
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new SqlConnectionPoolGroupProviderInfo((SqlConnectionString)connectionOptions);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00067FFC File Offset: 0x000661FC
		internal static SqlConnectionString FindSqlConnectionOptions(SqlConnectionPoolKey key)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)SqlConnectionFactory.SingletonInstance.FindConnectionOptions(key);
			if (sqlConnectionString == null)
			{
				sqlConnectionString = new SqlConnectionString(key.ConnectionString);
			}
			if (sqlConnectionString.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			return sqlConnectionString;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00068038 File Offset: 0x00066238
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00068058 File Offset: 0x00066258
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00068078 File Offset: 0x00066278
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PermissionDemand();
			}
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00068098 File Offset: 0x00066298
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000680B8 File Offset: 0x000662B8
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000680D8 File Offset: 0x000662D8
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			return sqlConnection != null && sqlConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000680FC File Offset: 0x000662FC
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0006811A File Offset: 0x0006631A
		protected override DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("System.Data.SqlClient.SqlMetaData.xml");
			cacheMetaDataFactory = true;
			return new SqlMetaDataFactory(manifestResourceStream, internalConnection.ServerVersion, internalConnection.ServerVersion);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0006813F File Offset: 0x0006633F
		// Note: this type is marked as 'beforefieldinit'.
		static SqlConnectionFactory()
		{
		}

		// Token: 0x04000E45 RID: 3653
		private const string _metaDataXml = "MetaDataXml";

		// Token: 0x04000E46 RID: 3654
		public static readonly SqlConnectionFactory SingletonInstance = new SqlConnectionFactory();
	}
}
