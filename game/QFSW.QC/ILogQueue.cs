using System;

namespace QFSW.QC
{
	// Token: 0x02000014 RID: 20
	public interface ILogQueue
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59
		// (set) Token: 0x0600003C RID: 60
		int MaxStoredLogs { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61
		bool IsEmpty { get; }

		// Token: 0x0600003E RID: 62
		void QueueLog(ILog log);

		// Token: 0x0600003F RID: 63
		bool TryDequeue(out ILog log);

		// Token: 0x06000040 RID: 64
		void Clear();
	}
}
