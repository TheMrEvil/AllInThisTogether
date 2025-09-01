using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000E9 RID: 233
	internal sealed class DataExpression : IFilter
	{
		// Token: 0x06000E14 RID: 3604 RVA: 0x0003ADF8 File Offset: 0x00038FF8
		internal DataExpression(DataTable table, string expression) : this(table, expression, null)
		{
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0003AE04 File Offset: 0x00039004
		internal DataExpression(DataTable table, string expression, Type type)
		{
			ExpressionParser expressionParser = new ExpressionParser(table);
			expressionParser.LoadExpression(expression);
			this._originalExpression = expression;
			this._expr = null;
			if (expression != null)
			{
				this._storageType = DataStorage.GetStorageType(type);
				if (this._storageType == StorageType.BigInteger)
				{
					throw ExprException.UnsupportedDataType(type);
				}
				this._dataType = type;
				this._expr = expressionParser.Parse();
				this._parsed = true;
				if (this._expr != null && table != null)
				{
					this.Bind(table);
					return;
				}
				this._bound = false;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003AE92 File Offset: 0x00039092
		internal string Expression
		{
			get
			{
				if (this._originalExpression == null)
				{
					return "";
				}
				return this._originalExpression;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003AEA8 File Offset: 0x000390A8
		internal ExpressionNode ExpressionNode
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0003AEB0 File Offset: 0x000390B0
		internal bool HasValue
		{
			get
			{
				return this._expr != null;
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003AEBC File Offset: 0x000390BC
		internal void Bind(DataTable table)
		{
			this._table = table;
			if (table == null)
			{
				return;
			}
			if (this._expr != null)
			{
				List<DataColumn> list = new List<DataColumn>();
				this._expr.Bind(table, list);
				this._expr = this._expr.Optimize();
				this._table = table;
				this._bound = true;
				this._dependency = list.ToArray();
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0003AF1A File Offset: 0x0003911A
		internal bool DependsOn(DataColumn column)
		{
			return this._expr != null && this._expr.DependsOn(column);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003AF32 File Offset: 0x00039132
		internal object Evaluate()
		{
			return this.Evaluate(null, DataRowVersion.Default);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003AF40 File Offset: 0x00039140
		internal object Evaluate(DataRow row, DataRowVersion version)
		{
			if (!this._bound)
			{
				this.Bind(this._table);
			}
			object obj;
			if (this._expr != null)
			{
				obj = this._expr.Eval(row, version);
				if (obj == DBNull.Value && StorageType.Uri >= this._storageType)
				{
					return obj;
				}
				try
				{
					if (StorageType.Object != this._storageType)
					{
						obj = SqlConvert.ChangeType2(obj, this._storageType, this._dataType, this._table.FormatProvider);
					}
					return obj;
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(obj, this._dataType, ex);
				}
			}
			obj = null;
			return obj;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003AFF4 File Offset: 0x000391F4
		internal object Evaluate(DataRow[] rows)
		{
			return this.Evaluate(rows, DataRowVersion.Default);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003B004 File Offset: 0x00039204
		internal object Evaluate(DataRow[] rows, DataRowVersion version)
		{
			if (!this._bound)
			{
				this.Bind(this._table);
			}
			if (this._expr != null)
			{
				List<int> list = new List<int>();
				foreach (DataRow dataRow in rows)
				{
					if (dataRow.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow._oldRecord != -1))
					{
						list.Add(dataRow.GetRecordFromVersion(version));
					}
				}
				int[] recordNos = list.ToArray();
				return this._expr.Eval(recordNos);
			}
			return DBNull.Value;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0003B08C File Offset: 0x0003928C
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			if (this._expr == null)
			{
				return true;
			}
			if (row == null)
			{
				throw ExprException.InvokeArgument();
			}
			object value = this._expr.Eval(row, version);
			bool result;
			try
			{
				result = DataExpression.ToBoolean(value);
			}
			catch (EvaluateException)
			{
				throw ExprException.FilterConvertion(this.Expression);
			}
			return result;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003B0E4 File Offset: 0x000392E4
		internal DataColumn[] GetDependency()
		{
			return this._dependency;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003B0EC File Offset: 0x000392EC
		internal bool IsTableAggregate()
		{
			return this._expr != null && this._expr.IsTableConstant();
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003B103 File Offset: 0x00039303
		internal static bool IsUnknown(object value)
		{
			return DataStorage.IsObjectNull(value);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003B10B File Offset: 0x0003930B
		internal bool HasLocalAggregate()
		{
			return this._expr != null && this._expr.HasLocalAggregate();
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003B122 File Offset: 0x00039322
		internal bool HasRemoteAggregate()
		{
			return this._expr != null && this._expr.HasRemoteAggregate();
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003B13C File Offset: 0x0003933C
		internal static bool ToBoolean(object value)
		{
			if (DataExpression.IsUnknown(value))
			{
				return false;
			}
			if (value is bool)
			{
				return (bool)value;
			}
			if (value is SqlBoolean)
			{
				return ((SqlBoolean)value).IsTrue;
			}
			if (value is string)
			{
				try
				{
					return bool.Parse((string)value);
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(value, typeof(bool), ex);
				}
			}
			throw ExprException.DatavalueConvertion(value, typeof(bool), null);
		}

		// Token: 0x040008C0 RID: 2240
		internal string _originalExpression;

		// Token: 0x040008C1 RID: 2241
		private bool _parsed;

		// Token: 0x040008C2 RID: 2242
		private bool _bound;

		// Token: 0x040008C3 RID: 2243
		private ExpressionNode _expr;

		// Token: 0x040008C4 RID: 2244
		private DataTable _table;

		// Token: 0x040008C5 RID: 2245
		private readonly StorageType _storageType;

		// Token: 0x040008C6 RID: 2246
		private readonly Type _dataType;

		// Token: 0x040008C7 RID: 2247
		private DataColumn[] _dependency = Array.Empty<DataColumn>();
	}
}
