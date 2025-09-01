using System;

namespace System.IO
{
	// Token: 0x02000524 RID: 1316
	internal class NullFileWatcher : IFileWatcher
	{
		// Token: 0x06002A79 RID: 10873 RVA: 0x00003917 File Offset: 0x00001B17
		public void StartDispatching(object handle)
		{
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00003917 File Offset: 0x00001B17
		public void StopDispatching(object handle)
		{
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose(object handle)
		{
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000929BC File Offset: 0x00090BBC
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (NullFileWatcher.instance != null)
			{
				watcher = NullFileWatcher.instance;
				return true;
			}
			IFileWatcher fileWatcher;
			watcher = (fileWatcher = new NullFileWatcher());
			NullFileWatcher.instance = fileWatcher;
			return true;
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x0000219B File Offset: 0x0000039B
		public NullFileWatcher()
		{
		}

		// Token: 0x040016EE RID: 5870
		private static IFileWatcher instance;
	}
}
