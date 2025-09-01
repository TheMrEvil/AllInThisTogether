using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x02000507 RID: 1287
	internal class DefaultWatcherData
	{
		// Token: 0x060029DC RID: 10716 RVA: 0x0008FA04 File Offset: 0x0008DC04
		public DefaultWatcherData()
		{
		}

		// Token: 0x04001625 RID: 5669
		public FileSystemWatcher FSW;

		// Token: 0x04001626 RID: 5670
		public string Directory;

		// Token: 0x04001627 RID: 5671
		public string FileMask;

		// Token: 0x04001628 RID: 5672
		public bool IncludeSubdirs;

		// Token: 0x04001629 RID: 5673
		public bool Enabled;

		// Token: 0x0400162A RID: 5674
		public bool NoWildcards;

		// Token: 0x0400162B RID: 5675
		public DateTime DisabledTime;

		// Token: 0x0400162C RID: 5676
		public object FilesLock = new object();

		// Token: 0x0400162D RID: 5677
		public Dictionary<string, FileData> Files;
	}
}
