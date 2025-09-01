using System;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x02000218 RID: 536
	internal class TextDataFeed : DataFeed
	{
		// Token: 0x060019C2 RID: 6594 RVA: 0x0007730C File Offset: 0x0007550C
		internal TextDataFeed(TextReader source)
		{
			this._source = source;
		}

		// Token: 0x040010CF RID: 4303
		internal TextReader _source;
	}
}
