using System;

namespace System.Data.Common
{
	// Token: 0x0200037D RID: 893
	internal sealed class LoadAdapter : DataAdapter
	{
		// Token: 0x06002A23 RID: 10787 RVA: 0x000B89D2 File Offset: 0x000B6BD2
		internal LoadAdapter()
		{
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000B89DA File Offset: 0x000B6BDA
		internal int FillFromReader(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			return this.Fill(dataTables, dataReader, startRecord, maxRecords);
		}
	}
}
