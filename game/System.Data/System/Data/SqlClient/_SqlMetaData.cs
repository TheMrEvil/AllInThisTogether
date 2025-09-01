using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000264 RID: 612
	internal sealed class _SqlMetaData : SqlMetaDataPriv
	{
		// Token: 0x06001CC9 RID: 7369 RVA: 0x00089251 File Offset: 0x00087451
		internal _SqlMetaData(int ordinal)
		{
			this.ordinal = ordinal;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00089260 File Offset: 0x00087460
		internal string serverName
		{
			get
			{
				return this.multiPartTableName.ServerName;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0008926D File Offset: 0x0008746D
		internal string catalogName
		{
			get
			{
				return this.multiPartTableName.CatalogName;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x0008927A File Offset: 0x0008747A
		internal string schemaName
		{
			get
			{
				return this.multiPartTableName.SchemaName;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x00089287 File Offset: 0x00087487
		internal string tableName
		{
			get
			{
				return this.multiPartTableName.TableName;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00089294 File Offset: 0x00087494
		internal bool IsNewKatmaiDateTimeType
		{
			get
			{
				return SqlDbType.Date == this.type || SqlDbType.Time == this.type || SqlDbType.DateTime2 == this.type || SqlDbType.DateTimeOffset == this.type;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x000892C0 File Offset: 0x000874C0
		internal bool IsLargeUdt
		{
			get
			{
				return this.type == SqlDbType.Udt && this.length == int.MaxValue;
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000892DC File Offset: 0x000874DC
		public object Clone()
		{
			_SqlMetaData sqlMetaData = new _SqlMetaData(this.ordinal);
			sqlMetaData.CopyFrom(this);
			sqlMetaData.column = this.column;
			sqlMetaData.baseColumn = this.baseColumn;
			sqlMetaData.multiPartTableName = this.multiPartTableName;
			sqlMetaData.updatability = this.updatability;
			sqlMetaData.tableNum = this.tableNum;
			sqlMetaData.isDifferentName = this.isDifferentName;
			sqlMetaData.isKey = this.isKey;
			sqlMetaData.isHidden = this.isHidden;
			sqlMetaData.isExpression = this.isExpression;
			sqlMetaData.isIdentity = this.isIdentity;
			sqlMetaData.isColumnSet = this.isColumnSet;
			sqlMetaData.op = this.op;
			sqlMetaData.operand = this.operand;
			return sqlMetaData;
		}

		// Token: 0x040013DE RID: 5086
		internal string column;

		// Token: 0x040013DF RID: 5087
		internal string baseColumn;

		// Token: 0x040013E0 RID: 5088
		internal MultiPartTableName multiPartTableName;

		// Token: 0x040013E1 RID: 5089
		internal readonly int ordinal;

		// Token: 0x040013E2 RID: 5090
		internal byte updatability;

		// Token: 0x040013E3 RID: 5091
		internal byte tableNum;

		// Token: 0x040013E4 RID: 5092
		internal bool isDifferentName;

		// Token: 0x040013E5 RID: 5093
		internal bool isKey;

		// Token: 0x040013E6 RID: 5094
		internal bool isHidden;

		// Token: 0x040013E7 RID: 5095
		internal bool isExpression;

		// Token: 0x040013E8 RID: 5096
		internal bool isIdentity;

		// Token: 0x040013E9 RID: 5097
		internal bool isColumnSet;

		// Token: 0x040013EA RID: 5098
		internal byte op;

		// Token: 0x040013EB RID: 5099
		internal ushort operand;
	}
}
