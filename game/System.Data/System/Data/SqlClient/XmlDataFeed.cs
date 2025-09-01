using System;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x02000219 RID: 537
	internal class XmlDataFeed : DataFeed
	{
		// Token: 0x060019C3 RID: 6595 RVA: 0x0007731B File Offset: 0x0007551B
		internal XmlDataFeed(XmlReader source)
		{
			this._source = source;
		}

		// Token: 0x040010D0 RID: 4304
		internal XmlReader _source;
	}
}
