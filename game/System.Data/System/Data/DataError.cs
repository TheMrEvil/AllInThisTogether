using System;

namespace System.Data
{
	// Token: 0x020000B5 RID: 181
	internal sealed class DataError
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x0002DE7F File Offset: 0x0002C07F
		internal DataError()
		{
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002DE92 File Offset: 0x0002C092
		internal DataError(string rowError)
		{
			this.SetText(rowError);
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002DEAC File Offset: 0x0002C0AC
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x0002DEB4 File Offset: 0x0002C0B4
		internal string Text
		{
			get
			{
				return this._rowError;
			}
			set
			{
				this.SetText(value);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002DEBD File Offset: 0x0002C0BD
		internal bool HasErrors
		{
			get
			{
				return this._rowError.Length != 0 || this._count != 0;
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002DED8 File Offset: 0x0002C0D8
		internal void SetColumnError(DataColumn column, string error)
		{
			if (error == null || error.Length == 0)
			{
				this.Clear(column);
				return;
			}
			if (this._errorList == null)
			{
				this._errorList = new DataError.ColumnError[1];
			}
			int num = this.IndexOf(column);
			this._errorList[num]._column = column;
			this._errorList[num]._error = error;
			column._errors++;
			if (num == this._count)
			{
				this._count++;
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002DF60 File Offset: 0x0002C160
		internal string GetColumnError(DataColumn column)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					return this._errorList[i]._error;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002DFAC File Offset: 0x0002C1AC
		internal void Clear(DataColumn column)
		{
			if (this._count == 0)
			{
				return;
			}
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					Array.Copy(this._errorList, i + 1, this._errorList, i, this._count - i - 1);
					this._count--;
					column._errors--;
				}
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002E024 File Offset: 0x0002C224
		internal void Clear()
		{
			for (int i = 0; i < this._count; i++)
			{
				this._errorList[i]._column._errors--;
			}
			this._count = 0;
			this._rowError = string.Empty;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002E074 File Offset: 0x0002C274
		internal DataColumn[] GetColumnsInError()
		{
			DataColumn[] array = new DataColumn[this._count];
			for (int i = 0; i < this._count; i++)
			{
				array[i] = this._errorList[i]._column;
			}
			return array;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002E0B3 File Offset: 0x0002C2B3
		private void SetText(string errorText)
		{
			if (errorText == null)
			{
				errorText = string.Empty;
			}
			this._rowError = errorText;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002E0C8 File Offset: 0x0002C2C8
		internal int IndexOf(DataColumn column)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					return i;
				}
			}
			if (this._count >= this._errorList.Length)
			{
				DataError.ColumnError[] array = new DataError.ColumnError[Math.Min(this._count * 2, column.Table.Columns.Count)];
				Array.Copy(this._errorList, 0, array, 0, this._count);
				this._errorList = array;
			}
			return this._count;
		}

		// Token: 0x0400079E RID: 1950
		private string _rowError = string.Empty;

		// Token: 0x0400079F RID: 1951
		private int _count;

		// Token: 0x040007A0 RID: 1952
		private DataError.ColumnError[] _errorList;

		// Token: 0x040007A1 RID: 1953
		internal const int initialCapacity = 1;

		// Token: 0x020000B6 RID: 182
		internal struct ColumnError
		{
			// Token: 0x040007A2 RID: 1954
			internal DataColumn _column;

			// Token: 0x040007A3 RID: 1955
			internal string _error;
		}
	}
}
