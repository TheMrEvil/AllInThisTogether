using System;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000050 RID: 80
	public static class LogTypeExtensions
	{
		// Token: 0x0600019D RID: 413 RVA: 0x000082B0 File Offset: 0x000064B0
		public static LoggingThreshold ToLoggingThreshold(this LogType logType)
		{
			LoggingThreshold result = LoggingThreshold.Always;
			switch (logType)
			{
			case LogType.Error:
				result = LoggingThreshold.Error;
				break;
			case LogType.Assert:
				result = LoggingThreshold.Error;
				break;
			case LogType.Warning:
				result = LoggingThreshold.Warning;
				break;
			case LogType.Log:
				result = LoggingThreshold.Always;
				break;
			case LogType.Exception:
				result = LoggingThreshold.Exception;
				break;
			}
			return result;
		}
	}
}
