using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001D5 RID: 469
	internal sealed class SqlConnectionString : DbConnectionOptions
	{
		// Token: 0x060016E4 RID: 5860 RVA: 0x000683BC File Offset: 0x000665BC
		internal SqlConnectionString(string connectionString) : base(connectionString, SqlConnectionString.GetParseSynonyms())
		{
			this.ThrowUnsupportedIfKeywordSet("asynchronous processing");
			this.ThrowUnsupportedIfKeywordSet("connection reset");
			this.ThrowUnsupportedIfKeywordSet("context connection");
			if (base.ContainsKey("network library"))
			{
				throw SQL.NetworkLibraryKeywordNotSupported();
			}
			this._integratedSecurity = base.ConvertValueToIntegratedSecurity();
			this._encrypt = base.ConvertValueToBoolean("encrypt", false);
			this._enlist = base.ConvertValueToBoolean("enlist", true);
			this._mars = base.ConvertValueToBoolean("multipleactiveresultsets", false);
			this._persistSecurityInfo = base.ConvertValueToBoolean("persist security info", false);
			this._pooling = base.ConvertValueToBoolean("pooling", true);
			this._replication = base.ConvertValueToBoolean("replication", false);
			this._userInstance = base.ConvertValueToBoolean("user instance", false);
			this._multiSubnetFailover = base.ConvertValueToBoolean("multisubnetfailover", false);
			this._connectTimeout = base.ConvertValueToInt32("connect timeout", 15);
			this._loadBalanceTimeout = base.ConvertValueToInt32("load balance timeout", 0);
			this._maxPoolSize = base.ConvertValueToInt32("max pool size", 100);
			this._minPoolSize = base.ConvertValueToInt32("min pool size", 0);
			this._packetSize = base.ConvertValueToInt32("packet size", 8000);
			this._connectRetryCount = base.ConvertValueToInt32("connectretrycount", 1);
			this._connectRetryInterval = base.ConvertValueToInt32("connectretryinterval", 10);
			this._applicationIntent = this.ConvertValueToApplicationIntent();
			this._applicationName = base.ConvertValueToString("application name", "Core .Net SqlClient Data Provider");
			this._attachDBFileName = base.ConvertValueToString("attachdbfilename", "");
			this._currentLanguage = base.ConvertValueToString("current language", "");
			this._dataSource = base.ConvertValueToString("data source", "");
			this._localDBInstance = LocalDBAPI.GetLocalDbInstanceNameFromServerName(this._dataSource);
			this._failoverPartner = base.ConvertValueToString("failover partner", "");
			this._initialCatalog = base.ConvertValueToString("initial catalog", "");
			this._password = base.ConvertValueToString("password", "");
			this._trustServerCertificate = base.ConvertValueToBoolean("trustservercertificate", false);
			string text = base.ConvertValueToString("type system version", null);
			string text2 = base.ConvertValueToString("transaction binding", null);
			this._userID = base.ConvertValueToString("user id", "");
			this._workstationId = base.ConvertValueToString("workstation id", null);
			if (this._loadBalanceTimeout < 0)
			{
				throw ADP.InvalidConnectionOptionValue("load balance timeout");
			}
			if (this._connectTimeout < 0)
			{
				throw ADP.InvalidConnectionOptionValue("connect timeout");
			}
			if (this._maxPoolSize < 1)
			{
				throw ADP.InvalidConnectionOptionValue("max pool size");
			}
			if (this._minPoolSize < 0)
			{
				throw ADP.InvalidConnectionOptionValue("min pool size");
			}
			if (this._maxPoolSize < this._minPoolSize)
			{
				throw ADP.InvalidMinMaxPoolSizeValues();
			}
			if (this._packetSize < 512 || 32768 < this._packetSize)
			{
				throw SQL.InvalidPacketSizeValue();
			}
			this.ValidateValueLength(this._applicationName, 128, "application name");
			this.ValidateValueLength(this._currentLanguage, 128, "current language");
			this.ValidateValueLength(this._dataSource, 128, "data source");
			this.ValidateValueLength(this._failoverPartner, 128, "failover partner");
			this.ValidateValueLength(this._initialCatalog, 128, "initial catalog");
			this.ValidateValueLength(this._password, 128, "password");
			this.ValidateValueLength(this._userID, 128, "user id");
			if (this._workstationId != null)
			{
				this.ValidateValueLength(this._workstationId, 128, "workstation id");
			}
			if (!string.Equals("", this._failoverPartner, StringComparison.OrdinalIgnoreCase))
			{
				if (this._multiSubnetFailover)
				{
					throw SQL.MultiSubnetFailoverWithFailoverPartner(false, null);
				}
				if (string.Equals("", this._initialCatalog, StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.MissingConnectionOptionValue("failover partner", "initial catalog");
				}
			}
			if (0 <= this._attachDBFileName.IndexOf('|'))
			{
				throw ADP.InvalidConnectionOptionValue("attachdbfilename");
			}
			this.ValidateValueLength(this._attachDBFileName, 260, "attachdbfilename");
			this._typeSystemAssemblyVersion = SqlConnectionString.constTypeSystemAsmVersion10;
			if (this._userInstance && !string.IsNullOrEmpty(this._failoverPartner))
			{
				throw SQL.UserInstanceFailoverNotCompatible();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "Latest";
			}
			if (text.Equals("Latest", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.Latest;
			}
			else if (text.Equals("SQL Server 2000", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2000;
			}
			else if (text.Equals("SQL Server 2005", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2005;
			}
			else if (text.Equals("SQL Server 2008", StringComparison.OrdinalIgnoreCase))
			{
				this._typeSystemVersion = SqlConnectionString.TypeSystem.Latest;
			}
			else
			{
				if (!text.Equals("SQL Server 2012", StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.InvalidConnectionOptionValue("type system version");
				}
				this._typeSystemVersion = SqlConnectionString.TypeSystem.SQLServer2012;
				this._typeSystemAssemblyVersion = SqlConnectionString.constTypeSystemAsmVersion11;
			}
			if (string.IsNullOrEmpty(text2))
			{
				text2 = "Implicit Unbind";
			}
			if (text2.Equals("Implicit Unbind", StringComparison.OrdinalIgnoreCase))
			{
				this._transactionBinding = SqlConnectionString.TransactionBindingEnum.ImplicitUnbind;
			}
			else
			{
				if (!text2.Equals("Explicit Unbind", StringComparison.OrdinalIgnoreCase))
				{
					throw ADP.InvalidConnectionOptionValue("transaction binding");
				}
				this._transactionBinding = SqlConnectionString.TransactionBindingEnum.ExplicitUnbind;
			}
			if (this._applicationIntent == ApplicationIntent.ReadOnly && !string.IsNullOrEmpty(this._failoverPartner))
			{
				throw SQL.ROR_FailoverNotSupportedConnString();
			}
			if (this._connectRetryCount < 0 || this._connectRetryCount > 255)
			{
				throw ADP.InvalidConnectRetryCountValue();
			}
			if (this._connectRetryInterval < 1 || this._connectRetryInterval > 60)
			{
				throw ADP.InvalidConnectRetryIntervalValue();
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00068954 File Offset: 0x00066B54
		internal SqlConnectionString(SqlConnectionString connectionOptions, string dataSource, bool userInstance, bool? setEnlistValue) : base(connectionOptions)
		{
			this._integratedSecurity = connectionOptions._integratedSecurity;
			this._encrypt = connectionOptions._encrypt;
			if (setEnlistValue != null)
			{
				this._enlist = setEnlistValue.Value;
			}
			else
			{
				this._enlist = connectionOptions._enlist;
			}
			this._mars = connectionOptions._mars;
			this._persistSecurityInfo = connectionOptions._persistSecurityInfo;
			this._pooling = connectionOptions._pooling;
			this._replication = connectionOptions._replication;
			this._userInstance = userInstance;
			this._connectTimeout = connectionOptions._connectTimeout;
			this._loadBalanceTimeout = connectionOptions._loadBalanceTimeout;
			this._maxPoolSize = connectionOptions._maxPoolSize;
			this._minPoolSize = connectionOptions._minPoolSize;
			this._multiSubnetFailover = connectionOptions._multiSubnetFailover;
			this._packetSize = connectionOptions._packetSize;
			this._applicationName = connectionOptions._applicationName;
			this._attachDBFileName = connectionOptions._attachDBFileName;
			this._currentLanguage = connectionOptions._currentLanguage;
			this._dataSource = dataSource;
			this._localDBInstance = LocalDBAPI.GetLocalDbInstanceNameFromServerName(this._dataSource);
			this._failoverPartner = connectionOptions._failoverPartner;
			this._initialCatalog = connectionOptions._initialCatalog;
			this._password = connectionOptions._password;
			this._userID = connectionOptions._userID;
			this._workstationId = connectionOptions._workstationId;
			this._typeSystemVersion = connectionOptions._typeSystemVersion;
			this._transactionBinding = connectionOptions._transactionBinding;
			this._applicationIntent = connectionOptions._applicationIntent;
			this._connectRetryCount = connectionOptions._connectRetryCount;
			this._connectRetryInterval = connectionOptions._connectRetryInterval;
			this.ValidateValueLength(this._dataSource, 128, "data source");
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00068AED File Offset: 0x00066CED
		internal bool IntegratedSecurity
		{
			get
			{
				return this._integratedSecurity;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00006D61 File Offset: 0x00004F61
		internal bool Asynchronous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00006D61 File Offset: 0x00004F61
		internal bool ConnectionReset
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00068AF5 File Offset: 0x00066CF5
		internal bool Encrypt
		{
			get
			{
				return this._encrypt;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00068AFD File Offset: 0x00066CFD
		internal bool TrustServerCertificate
		{
			get
			{
				return this._trustServerCertificate;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00068B05 File Offset: 0x00066D05
		internal bool Enlist
		{
			get
			{
				return this._enlist;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00068B0D File Offset: 0x00066D0D
		internal bool MARS
		{
			get
			{
				return this._mars;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00068B15 File Offset: 0x00066D15
		internal bool MultiSubnetFailover
		{
			get
			{
				return this._multiSubnetFailover;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00068B1D File Offset: 0x00066D1D
		internal bool PersistSecurityInfo
		{
			get
			{
				return this._persistSecurityInfo;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00068B25 File Offset: 0x00066D25
		internal bool Pooling
		{
			get
			{
				return this._pooling;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x00068B2D File Offset: 0x00066D2D
		internal bool Replication
		{
			get
			{
				return this._replication;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00068B35 File Offset: 0x00066D35
		internal bool UserInstance
		{
			get
			{
				return this._userInstance;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00068B3D File Offset: 0x00066D3D
		internal int ConnectTimeout
		{
			get
			{
				return this._connectTimeout;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00068B45 File Offset: 0x00066D45
		internal int LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00068B4D File Offset: 0x00066D4D
		internal int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00068B55 File Offset: 0x00066D55
		internal int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00068B5D File Offset: 0x00066D5D
		internal int PacketSize
		{
			get
			{
				return this._packetSize;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00068B65 File Offset: 0x00066D65
		internal int ConnectRetryCount
		{
			get
			{
				return this._connectRetryCount;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x00068B6D File Offset: 0x00066D6D
		internal int ConnectRetryInterval
		{
			get
			{
				return this._connectRetryInterval;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00068B75 File Offset: 0x00066D75
		internal ApplicationIntent ApplicationIntent
		{
			get
			{
				return this._applicationIntent;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x00068B7D File Offset: 0x00066D7D
		internal string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00068B85 File Offset: 0x00066D85
		internal string AttachDBFilename
		{
			get
			{
				return this._attachDBFileName;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00068B8D File Offset: 0x00066D8D
		internal string CurrentLanguage
		{
			get
			{
				return this._currentLanguage;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00068B95 File Offset: 0x00066D95
		internal string DataSource
		{
			get
			{
				return this._dataSource;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00068B9D File Offset: 0x00066D9D
		internal string LocalDBInstance
		{
			get
			{
				return this._localDBInstance;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00068BA5 File Offset: 0x00066DA5
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x00068BAD File Offset: 0x00066DAD
		internal string InitialCatalog
		{
			get
			{
				return this._initialCatalog;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00068BB5 File Offset: 0x00066DB5
		internal string Password
		{
			get
			{
				return this._password;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x00068BBD File Offset: 0x00066DBD
		internal string UserID
		{
			get
			{
				return this._userID;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00068BC5 File Offset: 0x00066DC5
		internal string WorkstationId
		{
			get
			{
				return this._workstationId;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00068BCD File Offset: 0x00066DCD
		internal SqlConnectionString.TypeSystem TypeSystemVersion
		{
			get
			{
				return this._typeSystemVersion;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00068BD5 File Offset: 0x00066DD5
		internal Version TypeSystemAssemblyVersion
		{
			get
			{
				return this._typeSystemAssemblyVersion;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00068BDD File Offset: 0x00066DDD
		internal SqlConnectionString.TransactionBindingEnum TransactionBinding
		{
			get
			{
				return this._transactionBinding;
			}
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00068BE8 File Offset: 0x00066DE8
		internal static Dictionary<string, string> GetParseSynonyms()
		{
			Dictionary<string, string> dictionary = SqlConnectionString.s_sqlClientSynonyms;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, string>(54)
				{
					{
						"applicationintent",
						"applicationintent"
					},
					{
						"application name",
						"application name"
					},
					{
						"asynchronous processing",
						"asynchronous processing"
					},
					{
						"attachdbfilename",
						"attachdbfilename"
					},
					{
						"connect timeout",
						"connect timeout"
					},
					{
						"connection reset",
						"connection reset"
					},
					{
						"context connection",
						"context connection"
					},
					{
						"current language",
						"current language"
					},
					{
						"data source",
						"data source"
					},
					{
						"encrypt",
						"encrypt"
					},
					{
						"enlist",
						"enlist"
					},
					{
						"failover partner",
						"failover partner"
					},
					{
						"initial catalog",
						"initial catalog"
					},
					{
						"integrated security",
						"integrated security"
					},
					{
						"load balance timeout",
						"load balance timeout"
					},
					{
						"multipleactiveresultsets",
						"multipleactiveresultsets"
					},
					{
						"max pool size",
						"max pool size"
					},
					{
						"min pool size",
						"min pool size"
					},
					{
						"multisubnetfailover",
						"multisubnetfailover"
					},
					{
						"network library",
						"network library"
					},
					{
						"packet size",
						"packet size"
					},
					{
						"password",
						"password"
					},
					{
						"persist security info",
						"persist security info"
					},
					{
						"pooling",
						"pooling"
					},
					{
						"replication",
						"replication"
					},
					{
						"trustservercertificate",
						"trustservercertificate"
					},
					{
						"transaction binding",
						"transaction binding"
					},
					{
						"type system version",
						"type system version"
					},
					{
						"user id",
						"user id"
					},
					{
						"user instance",
						"user instance"
					},
					{
						"workstation id",
						"workstation id"
					},
					{
						"connectretrycount",
						"connectretrycount"
					},
					{
						"connectretryinterval",
						"connectretryinterval"
					},
					{
						"app",
						"application name"
					},
					{
						"async",
						"asynchronous processing"
					},
					{
						"extended properties",
						"attachdbfilename"
					},
					{
						"initial file name",
						"attachdbfilename"
					},
					{
						"connection timeout",
						"connect timeout"
					},
					{
						"timeout",
						"connect timeout"
					},
					{
						"language",
						"current language"
					},
					{
						"addr",
						"data source"
					},
					{
						"address",
						"data source"
					},
					{
						"network address",
						"data source"
					},
					{
						"server",
						"data source"
					},
					{
						"database",
						"initial catalog"
					},
					{
						"trusted_connection",
						"integrated security"
					},
					{
						"connection lifetime",
						"load balance timeout"
					},
					{
						"net",
						"network library"
					},
					{
						"network",
						"network library"
					},
					{
						"pwd",
						"password"
					},
					{
						"persistsecurityinfo",
						"persist security info"
					},
					{
						"uid",
						"user id"
					},
					{
						"user",
						"user id"
					},
					{
						"wsid",
						"workstation id"
					}
				};
				SqlConnectionString.s_sqlClientSynonyms = dictionary;
			}
			return dictionary;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00068F70 File Offset: 0x00067170
		internal string ObtainWorkstationId()
		{
			string text = this.WorkstationId;
			if (text == null)
			{
				text = ADP.MachineName();
				this.ValidateValueLength(text, 128, "workstation id");
			}
			return text;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00068F9F File Offset: 0x0006719F
		private void ValidateValueLength(string value, int limit, string key)
		{
			if (limit < value.Length)
			{
				throw ADP.InvalidConnectionOptionValueLength(key, limit);
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00068FB4 File Offset: 0x000671B4
		internal ApplicationIntent ConvertValueToApplicationIntent()
		{
			string value;
			if (!base.TryGetParsetableValue("applicationintent", out value))
			{
				return ApplicationIntent.ReadWrite;
			}
			ApplicationIntent result;
			try
			{
				result = DbConnectionStringBuilderUtil.ConvertToApplicationIntent("applicationintent", value);
			}
			catch (FormatException inner)
			{
				throw ADP.InvalidConnectionOptionValue("applicationintent", inner);
			}
			catch (OverflowException inner2)
			{
				throw ADP.InvalidConnectionOptionValue("applicationintent", inner2);
			}
			return result;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00069018 File Offset: 0x00067218
		internal void ThrowUnsupportedIfKeywordSet(string keyword)
		{
			if (base.ContainsKey(keyword))
			{
				throw SQL.UnsupportedKeyword(keyword);
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0006902A File Offset: 0x0006722A
		// Note: this type is marked as 'beforefieldinit'.
		static SqlConnectionString()
		{
		}

		// Token: 0x04000E4E RID: 3662
		internal const int SynonymCount = 18;

		// Token: 0x04000E4F RID: 3663
		internal const int DeprecatedSynonymCount = 3;

		// Token: 0x04000E50 RID: 3664
		private static Dictionary<string, string> s_sqlClientSynonyms;

		// Token: 0x04000E51 RID: 3665
		private readonly bool _integratedSecurity;

		// Token: 0x04000E52 RID: 3666
		private readonly bool _encrypt;

		// Token: 0x04000E53 RID: 3667
		private readonly bool _trustServerCertificate;

		// Token: 0x04000E54 RID: 3668
		private readonly bool _enlist;

		// Token: 0x04000E55 RID: 3669
		private readonly bool _mars;

		// Token: 0x04000E56 RID: 3670
		private readonly bool _persistSecurityInfo;

		// Token: 0x04000E57 RID: 3671
		private readonly bool _pooling;

		// Token: 0x04000E58 RID: 3672
		private readonly bool _replication;

		// Token: 0x04000E59 RID: 3673
		private readonly bool _userInstance;

		// Token: 0x04000E5A RID: 3674
		private readonly bool _multiSubnetFailover;

		// Token: 0x04000E5B RID: 3675
		private readonly int _connectTimeout;

		// Token: 0x04000E5C RID: 3676
		private readonly int _loadBalanceTimeout;

		// Token: 0x04000E5D RID: 3677
		private readonly int _maxPoolSize;

		// Token: 0x04000E5E RID: 3678
		private readonly int _minPoolSize;

		// Token: 0x04000E5F RID: 3679
		private readonly int _packetSize;

		// Token: 0x04000E60 RID: 3680
		private readonly int _connectRetryCount;

		// Token: 0x04000E61 RID: 3681
		private readonly int _connectRetryInterval;

		// Token: 0x04000E62 RID: 3682
		private readonly ApplicationIntent _applicationIntent;

		// Token: 0x04000E63 RID: 3683
		private readonly string _applicationName;

		// Token: 0x04000E64 RID: 3684
		private readonly string _attachDBFileName;

		// Token: 0x04000E65 RID: 3685
		private readonly string _currentLanguage;

		// Token: 0x04000E66 RID: 3686
		private readonly string _dataSource;

		// Token: 0x04000E67 RID: 3687
		private readonly string _localDBInstance;

		// Token: 0x04000E68 RID: 3688
		private readonly string _failoverPartner;

		// Token: 0x04000E69 RID: 3689
		private readonly string _initialCatalog;

		// Token: 0x04000E6A RID: 3690
		private readonly string _password;

		// Token: 0x04000E6B RID: 3691
		private readonly string _userID;

		// Token: 0x04000E6C RID: 3692
		private readonly string _workstationId;

		// Token: 0x04000E6D RID: 3693
		private readonly SqlConnectionString.TransactionBindingEnum _transactionBinding;

		// Token: 0x04000E6E RID: 3694
		private readonly SqlConnectionString.TypeSystem _typeSystemVersion;

		// Token: 0x04000E6F RID: 3695
		private readonly Version _typeSystemAssemblyVersion;

		// Token: 0x04000E70 RID: 3696
		private static readonly Version constTypeSystemAsmVersion10 = new Version("10.0.0.0");

		// Token: 0x04000E71 RID: 3697
		private static readonly Version constTypeSystemAsmVersion11 = new Version("11.0.0.0");

		// Token: 0x020001D6 RID: 470
		internal static class DEFAULT
		{
			// Token: 0x04000E72 RID: 3698
			internal const ApplicationIntent ApplicationIntent = ApplicationIntent.ReadWrite;

			// Token: 0x04000E73 RID: 3699
			internal const string Application_Name = "Core .Net SqlClient Data Provider";

			// Token: 0x04000E74 RID: 3700
			internal const string AttachDBFilename = "";

			// Token: 0x04000E75 RID: 3701
			internal const int Connect_Timeout = 15;

			// Token: 0x04000E76 RID: 3702
			internal const string Current_Language = "";

			// Token: 0x04000E77 RID: 3703
			internal const string Data_Source = "";

			// Token: 0x04000E78 RID: 3704
			internal const bool Encrypt = false;

			// Token: 0x04000E79 RID: 3705
			internal const bool Enlist = true;

			// Token: 0x04000E7A RID: 3706
			internal const string FailoverPartner = "";

			// Token: 0x04000E7B RID: 3707
			internal const string Initial_Catalog = "";

			// Token: 0x04000E7C RID: 3708
			internal const bool Integrated_Security = false;

			// Token: 0x04000E7D RID: 3709
			internal const int Load_Balance_Timeout = 0;

			// Token: 0x04000E7E RID: 3710
			internal const bool MARS = false;

			// Token: 0x04000E7F RID: 3711
			internal const int Max_Pool_Size = 100;

			// Token: 0x04000E80 RID: 3712
			internal const int Min_Pool_Size = 0;

			// Token: 0x04000E81 RID: 3713
			internal const bool MultiSubnetFailover = false;

			// Token: 0x04000E82 RID: 3714
			internal const int Packet_Size = 8000;

			// Token: 0x04000E83 RID: 3715
			internal const string Password = "";

			// Token: 0x04000E84 RID: 3716
			internal const bool Persist_Security_Info = false;

			// Token: 0x04000E85 RID: 3717
			internal const bool Pooling = true;

			// Token: 0x04000E86 RID: 3718
			internal const bool TrustServerCertificate = false;

			// Token: 0x04000E87 RID: 3719
			internal const string Type_System_Version = "";

			// Token: 0x04000E88 RID: 3720
			internal const string User_ID = "";

			// Token: 0x04000E89 RID: 3721
			internal const bool User_Instance = false;

			// Token: 0x04000E8A RID: 3722
			internal const bool Replication = false;

			// Token: 0x04000E8B RID: 3723
			internal const int Connect_Retry_Count = 1;

			// Token: 0x04000E8C RID: 3724
			internal const int Connect_Retry_Interval = 10;
		}

		// Token: 0x020001D7 RID: 471
		internal static class KEY
		{
			// Token: 0x04000E8D RID: 3725
			internal const string ApplicationIntent = "applicationintent";

			// Token: 0x04000E8E RID: 3726
			internal const string Application_Name = "application name";

			// Token: 0x04000E8F RID: 3727
			internal const string AsynchronousProcessing = "asynchronous processing";

			// Token: 0x04000E90 RID: 3728
			internal const string AttachDBFilename = "attachdbfilename";

			// Token: 0x04000E91 RID: 3729
			internal const string Connect_Timeout = "connect timeout";

			// Token: 0x04000E92 RID: 3730
			internal const string Connection_Reset = "connection reset";

			// Token: 0x04000E93 RID: 3731
			internal const string Context_Connection = "context connection";

			// Token: 0x04000E94 RID: 3732
			internal const string Current_Language = "current language";

			// Token: 0x04000E95 RID: 3733
			internal const string Data_Source = "data source";

			// Token: 0x04000E96 RID: 3734
			internal const string Encrypt = "encrypt";

			// Token: 0x04000E97 RID: 3735
			internal const string Enlist = "enlist";

			// Token: 0x04000E98 RID: 3736
			internal const string FailoverPartner = "failover partner";

			// Token: 0x04000E99 RID: 3737
			internal const string Initial_Catalog = "initial catalog";

			// Token: 0x04000E9A RID: 3738
			internal const string Integrated_Security = "integrated security";

			// Token: 0x04000E9B RID: 3739
			internal const string Load_Balance_Timeout = "load balance timeout";

			// Token: 0x04000E9C RID: 3740
			internal const string MARS = "multipleactiveresultsets";

			// Token: 0x04000E9D RID: 3741
			internal const string Max_Pool_Size = "max pool size";

			// Token: 0x04000E9E RID: 3742
			internal const string Min_Pool_Size = "min pool size";

			// Token: 0x04000E9F RID: 3743
			internal const string MultiSubnetFailover = "multisubnetfailover";

			// Token: 0x04000EA0 RID: 3744
			internal const string Network_Library = "network library";

			// Token: 0x04000EA1 RID: 3745
			internal const string Packet_Size = "packet size";

			// Token: 0x04000EA2 RID: 3746
			internal const string Password = "password";

			// Token: 0x04000EA3 RID: 3747
			internal const string Persist_Security_Info = "persist security info";

			// Token: 0x04000EA4 RID: 3748
			internal const string Pooling = "pooling";

			// Token: 0x04000EA5 RID: 3749
			internal const string TransactionBinding = "transaction binding";

			// Token: 0x04000EA6 RID: 3750
			internal const string TrustServerCertificate = "trustservercertificate";

			// Token: 0x04000EA7 RID: 3751
			internal const string Type_System_Version = "type system version";

			// Token: 0x04000EA8 RID: 3752
			internal const string User_ID = "user id";

			// Token: 0x04000EA9 RID: 3753
			internal const string User_Instance = "user instance";

			// Token: 0x04000EAA RID: 3754
			internal const string Workstation_Id = "workstation id";

			// Token: 0x04000EAB RID: 3755
			internal const string Replication = "replication";

			// Token: 0x04000EAC RID: 3756
			internal const string Connect_Retry_Count = "connectretrycount";

			// Token: 0x04000EAD RID: 3757
			internal const string Connect_Retry_Interval = "connectretryinterval";
		}

		// Token: 0x020001D8 RID: 472
		private static class SYNONYM
		{
			// Token: 0x04000EAE RID: 3758
			internal const string APP = "app";

			// Token: 0x04000EAF RID: 3759
			internal const string Async = "async";

			// Token: 0x04000EB0 RID: 3760
			internal const string EXTENDED_PROPERTIES = "extended properties";

			// Token: 0x04000EB1 RID: 3761
			internal const string INITIAL_FILE_NAME = "initial file name";

			// Token: 0x04000EB2 RID: 3762
			internal const string CONNECTION_TIMEOUT = "connection timeout";

			// Token: 0x04000EB3 RID: 3763
			internal const string TIMEOUT = "timeout";

			// Token: 0x04000EB4 RID: 3764
			internal const string LANGUAGE = "language";

			// Token: 0x04000EB5 RID: 3765
			internal const string ADDR = "addr";

			// Token: 0x04000EB6 RID: 3766
			internal const string ADDRESS = "address";

			// Token: 0x04000EB7 RID: 3767
			internal const string SERVER = "server";

			// Token: 0x04000EB8 RID: 3768
			internal const string NETWORK_ADDRESS = "network address";

			// Token: 0x04000EB9 RID: 3769
			internal const string DATABASE = "database";

			// Token: 0x04000EBA RID: 3770
			internal const string TRUSTED_CONNECTION = "trusted_connection";

			// Token: 0x04000EBB RID: 3771
			internal const string Connection_Lifetime = "connection lifetime";

			// Token: 0x04000EBC RID: 3772
			internal const string NET = "net";

			// Token: 0x04000EBD RID: 3773
			internal const string NETWORK = "network";

			// Token: 0x04000EBE RID: 3774
			internal const string Pwd = "pwd";

			// Token: 0x04000EBF RID: 3775
			internal const string PERSISTSECURITYINFO = "persistsecurityinfo";

			// Token: 0x04000EC0 RID: 3776
			internal const string UID = "uid";

			// Token: 0x04000EC1 RID: 3777
			internal const string User = "user";

			// Token: 0x04000EC2 RID: 3778
			internal const string WSID = "wsid";
		}

		// Token: 0x020001D9 RID: 473
		internal enum TypeSystem
		{
			// Token: 0x04000EC4 RID: 3780
			Latest = 2008,
			// Token: 0x04000EC5 RID: 3781
			SQLServer2000 = 2000,
			// Token: 0x04000EC6 RID: 3782
			SQLServer2005 = 2005,
			// Token: 0x04000EC7 RID: 3783
			SQLServer2008 = 2008,
			// Token: 0x04000EC8 RID: 3784
			SQLServer2012 = 2012
		}

		// Token: 0x020001DA RID: 474
		internal static class TYPESYSTEMVERSION
		{
			// Token: 0x04000EC9 RID: 3785
			internal const string Latest = "Latest";

			// Token: 0x04000ECA RID: 3786
			internal const string SQL_Server_2000 = "SQL Server 2000";

			// Token: 0x04000ECB RID: 3787
			internal const string SQL_Server_2005 = "SQL Server 2005";

			// Token: 0x04000ECC RID: 3788
			internal const string SQL_Server_2008 = "SQL Server 2008";

			// Token: 0x04000ECD RID: 3789
			internal const string SQL_Server_2012 = "SQL Server 2012";
		}

		// Token: 0x020001DB RID: 475
		internal enum TransactionBindingEnum
		{
			// Token: 0x04000ECF RID: 3791
			ImplicitUnbind,
			// Token: 0x04000ED0 RID: 3792
			ExplicitUnbind
		}

		// Token: 0x020001DC RID: 476
		internal static class TRANSACTIONBINDING
		{
			// Token: 0x04000ED1 RID: 3793
			internal const string ImplicitUnbind = "Implicit Unbind";

			// Token: 0x04000ED2 RID: 3794
			internal const string ExplicitUnbind = "Explicit Unbind";
		}
	}
}
