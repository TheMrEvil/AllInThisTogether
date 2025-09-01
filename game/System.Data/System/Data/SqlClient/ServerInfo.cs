using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200020D RID: 525
	internal sealed class ServerInfo
	{
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x000766C8 File Offset: 0x000748C8
		// (set) Token: 0x06001985 RID: 6533 RVA: 0x000766D0 File Offset: 0x000748D0
		internal string ExtendedServerName
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtendedServerName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ExtendedServerName>k__BackingField = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x000766D9 File Offset: 0x000748D9
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x000766E1 File Offset: 0x000748E1
		internal string ResolvedServerName
		{
			[CompilerGenerated]
			get
			{
				return this.<ResolvedServerName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ResolvedServerName>k__BackingField = value;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x000766EA File Offset: 0x000748EA
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x000766F2 File Offset: 0x000748F2
		internal string ResolvedDatabaseName
		{
			[CompilerGenerated]
			get
			{
				return this.<ResolvedDatabaseName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ResolvedDatabaseName>k__BackingField = value;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x000766FB File Offset: 0x000748FB
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x00076703 File Offset: 0x00074903
		internal string UserProtocol
		{
			[CompilerGenerated]
			get
			{
				return this.<UserProtocol>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UserProtocol>k__BackingField = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0007670C File Offset: 0x0007490C
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x00076714 File Offset: 0x00074914
		internal string UserServerName
		{
			get
			{
				return this._userServerName;
			}
			private set
			{
				this._userServerName = value;
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0007671D File Offset: 0x0007491D
		internal ServerInfo(SqlConnectionString userOptions) : this(userOptions, userOptions.DataSource)
		{
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0007672C File Offset: 0x0007492C
		internal ServerInfo(SqlConnectionString userOptions, string serverName)
		{
			this.UserServerName = (serverName ?? string.Empty);
			this.UserProtocol = string.Empty;
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
			this.PreRoutingServerName = null;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00076764 File Offset: 0x00074964
		internal ServerInfo(SqlConnectionString userOptions, RoutingInfo routing, string preRoutingServerName)
		{
			if (routing == null || routing.ServerName == null)
			{
				this.UserServerName = string.Empty;
			}
			else
			{
				this.UserServerName = string.Format(CultureInfo.InvariantCulture, "{0},{1}", routing.ServerName, routing.Port);
			}
			this.PreRoutingServerName = preRoutingServerName;
			this.UserProtocol = "tcp";
			this.SetDerivedNames(this.UserProtocol, this.UserServerName);
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000767E5 File Offset: 0x000749E5
		internal void SetDerivedNames(string protocol, string serverName)
		{
			if (!string.IsNullOrEmpty(protocol))
			{
				this.ExtendedServerName = protocol + ":" + serverName;
			}
			else
			{
				this.ExtendedServerName = serverName;
			}
			this.ResolvedServerName = serverName;
		}

		// Token: 0x04001087 RID: 4231
		[CompilerGenerated]
		private string <ExtendedServerName>k__BackingField;

		// Token: 0x04001088 RID: 4232
		[CompilerGenerated]
		private string <ResolvedServerName>k__BackingField;

		// Token: 0x04001089 RID: 4233
		[CompilerGenerated]
		private string <ResolvedDatabaseName>k__BackingField;

		// Token: 0x0400108A RID: 4234
		[CompilerGenerated]
		private string <UserProtocol>k__BackingField;

		// Token: 0x0400108B RID: 4235
		private string _userServerName;

		// Token: 0x0400108C RID: 4236
		internal readonly string PreRoutingServerName;
	}
}
