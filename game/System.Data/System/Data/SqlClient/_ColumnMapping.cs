using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000194 RID: 404
	internal sealed class _ColumnMapping
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x0005C946 File Offset: 0x0005AB46
		internal _ColumnMapping(int columnId, _SqlMetaData metadata)
		{
			this._sourceColumnOrdinal = columnId;
			this._metadata = metadata;
		}

		// Token: 0x04000CE4 RID: 3300
		internal int _sourceColumnOrdinal;

		// Token: 0x04000CE5 RID: 3301
		internal _SqlMetaData _metadata;
	}
}
