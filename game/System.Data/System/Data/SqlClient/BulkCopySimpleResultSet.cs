using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000197 RID: 407
	internal sealed class BulkCopySimpleResultSet
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0005C9CD File Offset: 0x0005ABCD
		internal BulkCopySimpleResultSet()
		{
			this._results = new List<Result>();
		}

		// Token: 0x1700038B RID: 907
		internal Result this[int idx]
		{
			get
			{
				return this._results[idx];
			}
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0005C9F0 File Offset: 0x0005ABF0
		internal void SetMetaData(_SqlMetaDataSet metadata)
		{
			this._resultSet = new Result(metadata);
			this._results.Add(this._resultSet);
			this._indexmap = new int[this._resultSet.MetaData.Length];
			for (int i = 0; i < this._indexmap.Length; i++)
			{
				this._indexmap[i] = i;
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0005CA51 File Offset: 0x0005AC51
		internal int[] CreateIndexMap()
		{
			return this._indexmap;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0005CA5C File Offset: 0x0005AC5C
		internal object[] CreateRowBuffer()
		{
			Row row = new Row(this._resultSet.MetaData.Length);
			this._resultSet.AddRow(row);
			return row.DataFields;
		}

		// Token: 0x04000CE9 RID: 3305
		private readonly List<Result> _results;

		// Token: 0x04000CEA RID: 3306
		private Result _resultSet;

		// Token: 0x04000CEB RID: 3307
		private int[] _indexmap;
	}
}
