using System;

namespace System.IO
{
	// Token: 0x02000508 RID: 1288
	internal class FileData
	{
		// Token: 0x060029DD RID: 10717 RVA: 0x0000219B File Offset: 0x0000039B
		public FileData()
		{
		}

		// Token: 0x0400162E RID: 5678
		public string Directory;

		// Token: 0x0400162F RID: 5679
		public FileAttributes Attributes;

		// Token: 0x04001630 RID: 5680
		public bool NotExists;

		// Token: 0x04001631 RID: 5681
		public DateTime CreationTime;

		// Token: 0x04001632 RID: 5682
		public DateTime LastWriteTime;
	}
}
