using System;

namespace UnityEngine
{
	// Token: 0x020001B9 RID: 441
	public interface ILogger : ILogHandler
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001347 RID: 4935
		// (set) Token: 0x06001348 RID: 4936
		ILogHandler logHandler { get; set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001349 RID: 4937
		// (set) Token: 0x0600134A RID: 4938
		bool logEnabled { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600134B RID: 4939
		// (set) Token: 0x0600134C RID: 4940
		LogType filterLogType { get; set; }

		// Token: 0x0600134D RID: 4941
		bool IsLogTypeAllowed(LogType logType);

		// Token: 0x0600134E RID: 4942
		void Log(LogType logType, object message);

		// Token: 0x0600134F RID: 4943
		void Log(LogType logType, object message, Object context);

		// Token: 0x06001350 RID: 4944
		void Log(LogType logType, string tag, object message);

		// Token: 0x06001351 RID: 4945
		void Log(LogType logType, string tag, object message, Object context);

		// Token: 0x06001352 RID: 4946
		void Log(object message);

		// Token: 0x06001353 RID: 4947
		void Log(string tag, object message);

		// Token: 0x06001354 RID: 4948
		void Log(string tag, object message, Object context);

		// Token: 0x06001355 RID: 4949
		void LogWarning(string tag, object message);

		// Token: 0x06001356 RID: 4950
		void LogWarning(string tag, object message, Object context);

		// Token: 0x06001357 RID: 4951
		void LogError(string tag, object message);

		// Token: 0x06001358 RID: 4952
		void LogError(string tag, object message, Object context);

		// Token: 0x06001359 RID: 4953
		void LogFormat(LogType logType, string format, params object[] args);

		// Token: 0x0600135A RID: 4954
		void LogException(Exception exception);
	}
}
