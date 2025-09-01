using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001F4 RID: 500
	internal class SqlDbColumn : DbColumn
	{
		// Token: 0x0600185C RID: 6236 RVA: 0x0007044E File Offset: 0x0006E64E
		internal SqlDbColumn(_SqlMetaData md)
		{
			this._metadata = md;
			this.Populate();
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00070464 File Offset: 0x0006E664
		private void Populate()
		{
			base.AllowDBNull = new bool?(this._metadata.isNullable);
			base.BaseCatalogName = this._metadata.catalogName;
			base.BaseColumnName = this._metadata.baseColumn;
			base.BaseSchemaName = this._metadata.schemaName;
			base.BaseServerName = this._metadata.serverName;
			base.BaseTableName = this._metadata.tableName;
			base.ColumnName = this._metadata.column;
			base.ColumnOrdinal = new int?(this._metadata.ordinal);
			base.ColumnSize = new int?((this._metadata.metaType.IsSizeInCharacters && this._metadata.length != int.MaxValue) ? (this._metadata.length / 2) : this._metadata.length);
			base.IsAutoIncrement = new bool?(this._metadata.isIdentity);
			base.IsIdentity = new bool?(this._metadata.isIdentity);
			base.IsLong = new bool?(this._metadata.metaType.IsLong);
			if (SqlDbType.Timestamp == this._metadata.type)
			{
				base.IsUnique = new bool?(true);
			}
			else
			{
				base.IsUnique = new bool?(false);
			}
			if (255 != this._metadata.precision)
			{
				base.NumericPrecision = new int?((int)this._metadata.precision);
			}
			else
			{
				base.NumericPrecision = new int?((int)this._metadata.metaType.Precision);
			}
			base.IsReadOnly = new bool?(this._metadata.updatability == 0);
			base.UdtAssemblyQualifiedName = this._metadata.udtAssemblyQualifiedName;
		}

		// Token: 0x17000455 RID: 1109
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x0007062B File Offset: 0x0006E82B
		internal bool? SqlIsAliased
		{
			set
			{
				base.IsAliased = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (set) Token: 0x0600185F RID: 6239 RVA: 0x00070634 File Offset: 0x0006E834
		internal bool? SqlIsKey
		{
			set
			{
				base.IsKey = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x0007063D File Offset: 0x0006E83D
		internal bool? SqlIsHidden
		{
			set
			{
				base.IsHidden = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x00070646 File Offset: 0x0006E846
		internal bool? SqlIsExpression
		{
			set
			{
				base.IsExpression = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (set) Token: 0x06001862 RID: 6242 RVA: 0x0007064F File Offset: 0x0006E84F
		internal Type SqlDataType
		{
			set
			{
				base.DataType = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (set) Token: 0x06001863 RID: 6243 RVA: 0x00070658 File Offset: 0x0006E858
		internal string SqlDataTypeName
		{
			set
			{
				base.DataTypeName = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x00070661 File Offset: 0x0006E861
		internal int? SqlNumericScale
		{
			set
			{
				base.NumericScale = value;
			}
		}

		// Token: 0x04000FA0 RID: 4000
		private readonly _SqlMetaData _metadata;
	}
}
