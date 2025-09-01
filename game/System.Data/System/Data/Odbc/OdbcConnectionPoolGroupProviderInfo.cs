using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x020002DC RID: 732
	internal sealed class OdbcConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x00096B78 File Offset: 0x00094D78
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x00096B80 File Offset: 0x00094D80
		internal string DriverName
		{
			get
			{
				return this._driverName;
			}
			set
			{
				this._driverName = value;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x00096B89 File Offset: 0x00094D89
		// (set) Token: 0x06002040 RID: 8256 RVA: 0x00096B91 File Offset: 0x00094D91
		internal string DriverVersion
		{
			get
			{
				return this._driverVersion;
			}
			set
			{
				this._driverVersion = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x00096B9A File Offset: 0x00094D9A
		internal bool HasQuoteChar
		{
			get
			{
				return this._hasQuoteChar;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x00096BA2 File Offset: 0x00094DA2
		internal bool HasEscapeChar
		{
			get
			{
				return this._hasEscapeChar;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00096BAA File Offset: 0x00094DAA
		// (set) Token: 0x06002044 RID: 8260 RVA: 0x00096BB2 File Offset: 0x00094DB2
		internal string QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				this._quoteChar = value;
				this._hasQuoteChar = true;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00096BC2 File Offset: 0x00094DC2
		// (set) Token: 0x06002046 RID: 8262 RVA: 0x00096BCA File Offset: 0x00094DCA
		internal char EscapeChar
		{
			get
			{
				return this._escapeChar;
			}
			set
			{
				this._escapeChar = value;
				this._hasEscapeChar = true;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x00096BDA File Offset: 0x00094DDA
		// (set) Token: 0x06002048 RID: 8264 RVA: 0x00096BE2 File Offset: 0x00094DE2
		internal bool IsV3Driver
		{
			get
			{
				return this._isV3Driver;
			}
			set
			{
				this._isV3Driver = value;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x00096BEB File Offset: 0x00094DEB
		// (set) Token: 0x0600204A RID: 8266 RVA: 0x00096BF3 File Offset: 0x00094DF3
		internal int SupportedSQLTypes
		{
			get
			{
				return this._supportedSQLTypes;
			}
			set
			{
				this._supportedSQLTypes = value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x00096BFC File Offset: 0x00094DFC
		// (set) Token: 0x0600204C RID: 8268 RVA: 0x00096C04 File Offset: 0x00094E04
		internal int TestedSQLTypes
		{
			get
			{
				return this._testedSQLTypes;
			}
			set
			{
				this._testedSQLTypes = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x00096C0D File Offset: 0x00094E0D
		// (set) Token: 0x0600204E RID: 8270 RVA: 0x00096C15 File Offset: 0x00094E15
		internal int RestrictedSQLBindTypes
		{
			get
			{
				return this._restrictedSQLBindTypes;
			}
			set
			{
				this._restrictedSQLBindTypes = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x00096C1E File Offset: 0x00094E1E
		// (set) Token: 0x06002050 RID: 8272 RVA: 0x00096C26 File Offset: 0x00094E26
		internal bool NoCurrentCatalog
		{
			get
			{
				return this._noCurrentCatalog;
			}
			set
			{
				this._noCurrentCatalog = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x00096C2F File Offset: 0x00094E2F
		// (set) Token: 0x06002052 RID: 8274 RVA: 0x00096C37 File Offset: 0x00094E37
		internal bool NoConnectionDead
		{
			get
			{
				return this._noConnectionDead;
			}
			set
			{
				this._noConnectionDead = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x00096C40 File Offset: 0x00094E40
		// (set) Token: 0x06002054 RID: 8276 RVA: 0x00096C48 File Offset: 0x00094E48
		internal bool NoQueryTimeout
		{
			get
			{
				return this._noQueryTimeout;
			}
			set
			{
				this._noQueryTimeout = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x00096C51 File Offset: 0x00094E51
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x00096C59 File Offset: 0x00094E59
		internal bool NoSqlSoptSSNoBrowseTable
		{
			get
			{
				return this._noSqlSoptSSNoBrowseTable;
			}
			set
			{
				this._noSqlSoptSSNoBrowseTable = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00096C62 File Offset: 0x00094E62
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x00096C6A File Offset: 0x00094E6A
		internal bool NoSqlSoptSSHiddenColumns
		{
			get
			{
				return this._noSqlSoptSSHiddenColumns;
			}
			set
			{
				this._noSqlSoptSSHiddenColumns = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00096C73 File Offset: 0x00094E73
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x00096C7B File Offset: 0x00094E7B
		internal bool NoSqlCASSColumnKey
		{
			get
			{
				return this._noSqlCASSColumnKey;
			}
			set
			{
				this._noSqlCASSColumnKey = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00096C84 File Offset: 0x00094E84
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x00096C8C File Offset: 0x00094E8C
		internal bool NoSqlPrimaryKeys
		{
			get
			{
				return this._noSqlPrimaryKeys;
			}
			set
			{
				this._noSqlPrimaryKeys = value;
			}
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x00096C95 File Offset: 0x00094E95
		public OdbcConnectionPoolGroupProviderInfo()
		{
		}

		// Token: 0x04001765 RID: 5989
		private string _driverName;

		// Token: 0x04001766 RID: 5990
		private string _driverVersion;

		// Token: 0x04001767 RID: 5991
		private string _quoteChar;

		// Token: 0x04001768 RID: 5992
		private char _escapeChar;

		// Token: 0x04001769 RID: 5993
		private bool _hasQuoteChar;

		// Token: 0x0400176A RID: 5994
		private bool _hasEscapeChar;

		// Token: 0x0400176B RID: 5995
		private bool _isV3Driver;

		// Token: 0x0400176C RID: 5996
		private int _supportedSQLTypes;

		// Token: 0x0400176D RID: 5997
		private int _testedSQLTypes;

		// Token: 0x0400176E RID: 5998
		private int _restrictedSQLBindTypes;

		// Token: 0x0400176F RID: 5999
		private bool _noCurrentCatalog;

		// Token: 0x04001770 RID: 6000
		private bool _noConnectionDead;

		// Token: 0x04001771 RID: 6001
		private bool _noQueryTimeout;

		// Token: 0x04001772 RID: 6002
		private bool _noSqlSoptSSNoBrowseTable;

		// Token: 0x04001773 RID: 6003
		private bool _noSqlSoptSSHiddenColumns;

		// Token: 0x04001774 RID: 6004
		private bool _noSqlCASSColumnKey;

		// Token: 0x04001775 RID: 6005
		private bool _noSqlPrimaryKeys;
	}
}
