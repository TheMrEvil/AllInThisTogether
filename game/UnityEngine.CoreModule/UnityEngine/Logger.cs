using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001BB RID: 443
	public class Logger : ILogger, ILogHandler
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x00008CBB File Offset: 0x00006EBB
		private Logger()
		{
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0001AFB6 File Offset: 0x000191B6
		public Logger(ILogHandler logHandler)
		{
			this.logHandler = logHandler;
			this.logEnabled = true;
			this.filterLogType = LogType.Log;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0001AFD8 File Offset: 0x000191D8
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x0001AFE0 File Offset: 0x000191E0
		public ILogHandler logHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<logHandler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<logHandler>k__BackingField = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0001AFE9 File Offset: 0x000191E9
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x0001AFF1 File Offset: 0x000191F1
		public bool logEnabled
		{
			[CompilerGenerated]
			get
			{
				return this.<logEnabled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<logEnabled>k__BackingField = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0001AFFA File Offset: 0x000191FA
		// (set) Token: 0x06001364 RID: 4964 RVA: 0x0001B002 File Offset: 0x00019202
		public LogType filterLogType
		{
			[CompilerGenerated]
			get
			{
				return this.<filterLogType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<filterLogType>k__BackingField = value;
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0001B00C File Offset: 0x0001920C
		public bool IsLogTypeAllowed(LogType logType)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				bool flag = logType == LogType.Exception;
				if (flag)
				{
					return true;
				}
				bool flag2 = this.filterLogType != LogType.Exception;
				if (flag2)
				{
					return logType <= this.filterLogType;
				}
			}
			return false;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0001B058 File Offset: 0x00019258
		private static string GetString(object message)
		{
			bool flag = message == null;
			string result;
			if (flag)
			{
				result = "Null";
			}
			else
			{
				IFormattable formattable = message as IFormattable;
				bool flag2 = formattable != null;
				if (flag2)
				{
					result = formattable.ToString(null, CultureInfo.InvariantCulture);
				}
				else
				{
					result = message.ToString();
				}
			}
			return result;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0001B0A4 File Offset: 0x000192A4
		public void Log(LogType logType, object message)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, "{0}", new object[]
				{
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0001B0E0 File Offset: 0x000192E0
		public void Log(LogType logType, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, "{0}", new object[]
				{
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0001B11C File Offset: 0x0001931C
		public void Log(LogType logType, string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0001B15C File Offset: 0x0001935C
		public void Log(LogType logType, string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0001B19C File Offset: 0x0001939C
		public void Log(object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}", new object[]
				{
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0001B1D8 File Offset: 0x000193D8
		public void Log(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0001B218 File Offset: 0x00019418
		public void Log(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Log);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Log, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0001B258 File Offset: 0x00019458
		public void LogWarning(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Warning);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Warning, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0001B298 File Offset: 0x00019498
		public void LogWarning(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Warning);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Warning, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0001B2D8 File Offset: 0x000194D8
		public void LogError(string tag, object message)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Error);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Error, null, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0001B318 File Offset: 0x00019518
		public void LogError(string tag, object message, Object context)
		{
			bool flag = this.IsLogTypeAllowed(LogType.Error);
			if (flag)
			{
				this.logHandler.LogFormat(LogType.Error, context, "{0}: {1}", new object[]
				{
					tag,
					Logger.GetString(message)
				});
			}
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0001B358 File Offset: 0x00019558
		public void LogException(Exception exception)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				this.logHandler.LogException(exception, null);
			}
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0001B380 File Offset: 0x00019580
		public void LogException(Exception exception, Object context)
		{
			bool logEnabled = this.logEnabled;
			if (logEnabled)
			{
				this.logHandler.LogException(exception, context);
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0001B3A8 File Offset: 0x000195A8
		public void LogFormat(LogType logType, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, null, format, args);
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0001B3D4 File Offset: 0x000195D4
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			bool flag = this.IsLogTypeAllowed(logType);
			if (flag)
			{
				this.logHandler.LogFormat(logType, context, format, args);
			}
		}

		// Token: 0x04000736 RID: 1846
		private const string kNoTagFormat = "{0}";

		// Token: 0x04000737 RID: 1847
		private const string kTagFormat = "{0}: {1}";

		// Token: 0x04000738 RID: 1848
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ILogHandler <logHandler>k__BackingField;

		// Token: 0x04000739 RID: 1849
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <logEnabled>k__BackingField;

		// Token: 0x0400073A RID: 1850
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private LogType <filterLogType>k__BackingField;
	}
}
