using System;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x02000217 RID: 535
	internal class StreamDataFeed : DataFeed
	{
		// Token: 0x060019C1 RID: 6593 RVA: 0x000772FD File Offset: 0x000754FD
		internal StreamDataFeed(Stream source)
		{
			this._source = source;
		}

		// Token: 0x040010CE RID: 4302
		internal Stream _source;
	}
}
