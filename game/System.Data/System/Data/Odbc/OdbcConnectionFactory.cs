using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;

namespace System.Data.Odbc
{
	// Token: 0x020002D9 RID: 729
	internal sealed class OdbcConnectionFactory : DbConnectionFactory
	{
		// Token: 0x06002020 RID: 8224 RVA: 0x00067DC0 File Offset: 0x00065FC0
		private OdbcConnectionFactory()
		{
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x000966AE File Offset: 0x000948AE
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return OdbcFactory.Instance;
			}
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000966B5 File Offset: 0x000948B5
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningObject)
		{
			return new OdbcConnectionOpen(owningObject as OdbcConnection, options as OdbcConnectionString);
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000966C9 File Offset: 0x000948C9
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new OdbcConnectionString(connectionString, previous != null);
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00003E32 File Offset: 0x00002032
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000966D5 File Offset: 0x000948D5
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new OdbcConnectionPoolGroupProviderInfo();
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000966DC File Offset: 0x000948DC
		protected override DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			OdbcConnection outerConnection = ((OdbcConnectionOpen)internalConnection).OuterConnection;
			string infoStringUnhandled = outerConnection.GetInfoStringUnhandled(ODBC32.SQL_INFO.DRIVER_NAME);
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("System.Data.Odbc.OdbcMetaData.xml");
			cacheMetaDataFactory = true;
			string infoStringUnhandled2 = outerConnection.GetInfoStringUnhandled(ODBC32.SQL_INFO.DBMS_VER);
			return new OdbcMetaDataFactory(manifestResourceStream, infoStringUnhandled2, infoStringUnhandled2, outerConnection);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00096728 File Offset: 0x00094928
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00096748 File Offset: 0x00094948
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00096768 File Offset: 0x00094968
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PermissionDemand();
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00096788 File Offset: 0x00094988
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x000967A8 File Offset: 0x000949A8
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000967C8 File Offset: 0x000949C8
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			return odbcConnection != null && odbcConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000967EC File Offset: 0x000949EC
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0009680A File Offset: 0x00094A0A
		// Note: this type is marked as 'beforefieldinit'.
		static OdbcConnectionFactory()
		{
		}

		// Token: 0x0400175E RID: 5982
		public static readonly OdbcConnectionFactory SingletonInstance = new OdbcConnectionFactory();
	}
}
