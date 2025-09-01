using System;

namespace UnityEngine
{
	// Token: 0x020001BA RID: 442
	public interface ILogHandler
	{
		// Token: 0x0600135B RID: 4955
		void LogFormat(LogType logType, Object context, string format, params object[] args);

		// Token: 0x0600135C RID: 4956
		void LogException(Exception exception, Object context);
	}
}
