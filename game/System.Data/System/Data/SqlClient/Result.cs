using System;
using System.Collections.Generic;

namespace System.Data.SqlClient
{
	// Token: 0x02000196 RID: 406
	internal sealed class Result
	{
		// Token: 0x06001473 RID: 5235 RVA: 0x0005C982 File Offset: 0x0005AB82
		internal Result(_SqlMetaDataSet metadata)
		{
			this._metadata = metadata;
			this._rowset = new List<Row>();
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0005C99C File Offset: 0x0005AB9C
		internal int Count
		{
			get
			{
				return this._rowset.Count;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0005C9A9 File Offset: 0x0005ABA9
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x1700038A RID: 906
		internal Row this[int index]
		{
			get
			{
				return this._rowset[index];
			}
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0005C9BF File Offset: 0x0005ABBF
		internal void AddRow(Row row)
		{
			this._rowset.Add(row);
		}

		// Token: 0x04000CE7 RID: 3303
		private readonly _SqlMetaDataSet _metadata;

		// Token: 0x04000CE8 RID: 3304
		private readonly List<Row> _rowset;
	}
}
