using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200028C RID: 652
	internal class SNISMUXHeader
	{
		// Token: 0x06001E36 RID: 7734 RVA: 0x00003D93 File Offset: 0x00001F93
		public SNISMUXHeader()
		{
		}

		// Token: 0x040014D9 RID: 5337
		public const int HEADER_LENGTH = 16;

		// Token: 0x040014DA RID: 5338
		public byte SMID;

		// Token: 0x040014DB RID: 5339
		public byte flags;

		// Token: 0x040014DC RID: 5340
		public ushort sessionId;

		// Token: 0x040014DD RID: 5341
		public uint length;

		// Token: 0x040014DE RID: 5342
		public uint sequenceNumber;

		// Token: 0x040014DF RID: 5343
		public uint highwater;
	}
}
