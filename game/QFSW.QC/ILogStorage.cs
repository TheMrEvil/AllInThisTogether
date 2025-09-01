using System;
using System.Collections.Generic;

namespace QFSW.QC
{
	// Token: 0x02000015 RID: 21
	public interface ILogStorage
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000041 RID: 65
		// (set) Token: 0x06000042 RID: 66
		int MaxStoredLogs { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000043 RID: 67
		IReadOnlyList<ILog> Logs { get; }

		// Token: 0x06000044 RID: 68
		void AddLog(ILog log);

		// Token: 0x06000045 RID: 69
		void RemoveLog();

		// Token: 0x06000046 RID: 70
		void Clear();

		// Token: 0x06000047 RID: 71
		string GetLogString();
	}
}
