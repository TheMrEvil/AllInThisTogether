using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000108 RID: 264
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	internal sealed class DebugLogHandler : ILogHandler
	{
		// Token: 0x0600062C RID: 1580
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Log(LogType level, LogOption options, string msg, Object obj);

		// Token: 0x0600062D RID: 1581
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_LogException(Exception ex, Object obj);

		// Token: 0x0600062E RID: 1582 RVA: 0x00008750 File Offset: 0x00006950
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, LogOption.None, string.Format(format, args), context);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00008764 File Offset: 0x00006964
		public void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, logOptions, string.Format(format, args), context);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0000877C File Offset: 0x0000697C
		public void LogException(Exception exception, Object context)
		{
			bool flag = exception == null;
			if (flag)
			{
				throw new ArgumentNullException("exception");
			}
			DebugLogHandler.Internal_LogException(exception, context);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00002072 File Offset: 0x00000272
		public DebugLogHandler()
		{
		}
	}
}
