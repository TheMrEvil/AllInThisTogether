using System;

namespace System.Xml.Schema
{
	// Token: 0x020004F6 RID: 1270
	internal struct Position
	{
		// Token: 0x06003404 RID: 13316 RVA: 0x00127650 File Offset: 0x00125850
		public Position(int symbol, object particle)
		{
			this.symbol = symbol;
			this.particle = particle;
		}

		// Token: 0x040026D1 RID: 9937
		public int symbol;

		// Token: 0x040026D2 RID: 9938
		public object particle;
	}
}
