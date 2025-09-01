using System;
using System.IO;

namespace System.Reflection.Emit
{
	// Token: 0x02000910 RID: 2320
	internal struct MonoResource
	{
		// Token: 0x040030D8 RID: 12504
		public byte[] data;

		// Token: 0x040030D9 RID: 12505
		public string name;

		// Token: 0x040030DA RID: 12506
		public string filename;

		// Token: 0x040030DB RID: 12507
		public ResourceAttributes attrs;

		// Token: 0x040030DC RID: 12508
		public int offset;

		// Token: 0x040030DD RID: 12509
		public Stream stream;
	}
}
