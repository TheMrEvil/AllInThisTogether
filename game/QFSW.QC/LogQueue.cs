using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace QFSW.QC
{
	// Token: 0x02000017 RID: 23
	public class LogQueue : ILogQueue
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002E07 File Offset: 0x00001007
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002E0F File Offset: 0x0000100F
		public int MaxStoredLogs
		{
			[CompilerGenerated]
			get
			{
				return this.<MaxStoredLogs>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MaxStoredLogs>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002E18 File Offset: 0x00001018
		public bool IsEmpty
		{
			get
			{
				return this._queuedLogs.IsEmpty;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E25 File Offset: 0x00001025
		public LogQueue(int maxStoredLogs = -1)
		{
			this.MaxStoredLogs = maxStoredLogs;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E40 File Offset: 0x00001040
		public void QueueLog(ILog log)
		{
			this._queuedLogs.Enqueue(log);
			if (this.MaxStoredLogs > 0 && this._queuedLogs.Count > this.MaxStoredLogs)
			{
				ILog log2;
				this._queuedLogs.TryDequeue(out log2);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E83 File Offset: 0x00001083
		public bool TryDequeue(out ILog log)
		{
			return this._queuedLogs.TryDequeue(out log);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E94 File Offset: 0x00001094
		public void Clear()
		{
			ILog log;
			while (this.TryDequeue(out log))
			{
			}
		}

		// Token: 0x04000029 RID: 41
		private readonly ConcurrentQueue<ILog> _queuedLogs = new ConcurrentQueue<ILog>();

		// Token: 0x0400002A RID: 42
		[CompilerGenerated]
		private int <MaxStoredLogs>k__BackingField;
	}
}
