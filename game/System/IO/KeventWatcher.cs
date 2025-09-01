using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000522 RID: 1314
	internal class KeventWatcher : IFileWatcher
	{
		// Token: 0x06002A72 RID: 10866 RVA: 0x0000219B File Offset: 0x0000039B
		private KeventWatcher()
		{
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000928D4 File Offset: 0x00090AD4
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (KeventWatcher.failed)
			{
				watcher = null;
				return false;
			}
			if (KeventWatcher.instance != null)
			{
				watcher = KeventWatcher.instance;
				return true;
			}
			KeventWatcher.watches = Hashtable.Synchronized(new Hashtable());
			int num = KeventWatcher.kqueue();
			if (num == -1)
			{
				KeventWatcher.failed = true;
				watcher = null;
				return false;
			}
			KeventWatcher.close(num);
			KeventWatcher.instance = new KeventWatcher();
			watcher = KeventWatcher.instance;
			return true;
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0009293C File Offset: 0x00090B3C
		public void StartDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			KqueueMonitor kqueueMonitor;
			if (KeventWatcher.watches.ContainsKey(fileSystemWatcher))
			{
				kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[fileSystemWatcher];
			}
			else
			{
				kqueueMonitor = new KqueueMonitor(fileSystemWatcher);
				KeventWatcher.watches.Add(fileSystemWatcher, kqueueMonitor);
			}
			kqueueMonitor.Start();
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0009298C File Offset: 0x00090B8C
		public void StopDispatching(object handle)
		{
			FileSystemWatcher key = handle as FileSystemWatcher;
			KqueueMonitor kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[key];
			if (kqueueMonitor == null)
			{
				return;
			}
			kqueueMonitor.Stop();
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x00003917 File Offset: 0x00001B17
		public void Dispose(object handle)
		{
		}

		// Token: 0x06002A77 RID: 10871
		[DllImport("libc")]
		private static extern int close(int fd);

		// Token: 0x06002A78 RID: 10872
		[DllImport("libc")]
		private static extern int kqueue();

		// Token: 0x040016E2 RID: 5858
		private static bool failed;

		// Token: 0x040016E3 RID: 5859
		private static KeventWatcher instance;

		// Token: 0x040016E4 RID: 5860
		private static Hashtable watches;
	}
}
