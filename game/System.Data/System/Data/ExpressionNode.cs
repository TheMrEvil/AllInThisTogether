using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000EA RID: 234
	internal abstract class ExpressionNode
	{
		// Token: 0x06000E26 RID: 3622 RVA: 0x0003B1E4 File Offset: 0x000393E4
		protected ExpressionNode(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0003B1F4 File Offset: 0x000393F4
		internal IFormatProvider FormatProvider
		{
			get
			{
				if (this._table == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._table.FormatProvider;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00006D64 File Offset: 0x00004F64
		internal virtual bool IsSqlColumn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0003B21C File Offset: 0x0003941C
		protected DataTable table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003B224 File Offset: 0x00039424
		protected void BindTable(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x06000E2B RID: 3627
		internal abstract void Bind(DataTable table, List<DataColumn> list);

		// Token: 0x06000E2C RID: 3628
		internal abstract object Eval();

		// Token: 0x06000E2D RID: 3629
		internal abstract object Eval(DataRow row, DataRowVersion version);

		// Token: 0x06000E2E RID: 3630
		internal abstract object Eval(int[] recordNos);

		// Token: 0x06000E2F RID: 3631
		internal abstract bool IsConstant();

		// Token: 0x06000E30 RID: 3632
		internal abstract bool IsTableConstant();

		// Token: 0x06000E31 RID: 3633
		internal abstract bool HasLocalAggregate();

		// Token: 0x06000E32 RID: 3634
		internal abstract bool HasRemoteAggregate();

		// Token: 0x06000E33 RID: 3635
		internal abstract ExpressionNode Optimize();

		// Token: 0x06000E34 RID: 3636 RVA: 0x00006D64 File Offset: 0x00004F64
		internal virtual bool DependsOn(DataColumn column)
		{
			return false;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003B22D File Offset: 0x0003942D
		internal static bool IsInteger(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003B255 File Offset: 0x00039455
		internal static bool IsIntegerSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SByte || type == StorageType.Byte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || type == StorageType.SqlByte;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003B291 File Offset: 0x00039491
		internal static bool IsSigned(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || ExpressionNode.IsFloat(type);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003B2AD File Offset: 0x000394AD
		internal static bool IsSignedSql(StorageType type)
		{
			return type == StorageType.Int16 || type == StorageType.Int32 || type == StorageType.Int64 || type == StorageType.SByte || type == StorageType.SqlInt64 || type == StorageType.SqlInt32 || type == StorageType.SqlInt16 || ExpressionNode.IsFloatSql(type);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003B2D8 File Offset: 0x000394D8
		internal static bool IsUnsigned(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.Byte;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003B2EE File Offset: 0x000394EE
		internal static bool IsUnsignedSql(StorageType type)
		{
			return type == StorageType.UInt16 || type == StorageType.UInt32 || type == StorageType.UInt64 || type == StorageType.SqlByte || type == StorageType.Byte;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003B309 File Offset: 0x00039509
		internal static bool IsNumeric(StorageType type)
		{
			return ExpressionNode.IsFloat(type) || ExpressionNode.IsInteger(type);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003B31B File Offset: 0x0003951B
		internal static bool IsNumericSql(StorageType type)
		{
			return ExpressionNode.IsFloatSql(type) || ExpressionNode.IsIntegerSql(type);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003B32D File Offset: 0x0003952D
		internal static bool IsFloat(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003B340 File Offset: 0x00039540
		internal static bool IsFloatSql(StorageType type)
		{
			return type == StorageType.Single || type == StorageType.Double || type == StorageType.Decimal || type == StorageType.SqlDouble || type == StorageType.SqlDecimal || type == StorageType.SqlMoney || type == StorageType.SqlSingle;
		}

		// Token: 0x040008C8 RID: 2248
		private DataTable _table;
	}
}
