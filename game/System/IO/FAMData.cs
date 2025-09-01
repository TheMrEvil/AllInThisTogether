using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x0200050F RID: 1295
	internal class FAMData
	{
		// Token: 0x060029F0 RID: 10736 RVA: 0x0000219B File Offset: 0x0000039B
		public FAMData()
		{
		}

		// Token: 0x04001645 RID: 5701
		public FileSystemWatcher FSW;

		// Token: 0x04001646 RID: 5702
		public string Directory;

		// Token: 0x04001647 RID: 5703
		public string FileMask;

		// Token: 0x04001648 RID: 5704
		public bool IncludeSubdirs;

		// Token: 0x04001649 RID: 5705
		public bool Enabled;

		// Token: 0x0400164A RID: 5706
		public FAMRequest Request;

		// Token: 0x0400164B RID: 5707
		public Hashtable SubDirs;
	}
}
