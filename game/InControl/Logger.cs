using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace InControl
{
	// Token: 0x02000032 RID: 50
	public static class Logger
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001E7 RID: 487 RVA: 0x000068E8 File Offset: 0x00004AE8
		// (remove) Token: 0x060001E8 RID: 488 RVA: 0x0000691C File Offset: 0x00004B1C
		public static event Action<LogMessage> OnLogMessage
		{
			[CompilerGenerated]
			add
			{
				Action<LogMessage> action = Logger.OnLogMessage;
				Action<LogMessage> action2;
				do
				{
					action2 = action;
					Action<LogMessage> value2 = (Action<LogMessage>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<LogMessage>>(ref Logger.OnLogMessage, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<LogMessage> action = Logger.OnLogMessage;
				Action<LogMessage> action2;
				do
				{
					action2 = action;
					Action<LogMessage> value2 = (Action<LogMessage>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<LogMessage>>(ref Logger.OnLogMessage, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006950 File Offset: 0x00004B50
		public static void LogInfo(string text)
		{
			if (Logger.OnLogMessage != null)
			{
				LogMessage obj = new LogMessage
				{
					Text = text,
					Type = LogMessageType.Info
				};
				Logger.OnLogMessage(obj);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000698C File Offset: 0x00004B8C
		public static void LogWarning(string text)
		{
			if (Logger.OnLogMessage != null)
			{
				LogMessage obj = new LogMessage
				{
					Text = text,
					Type = LogMessageType.Warning
				};
				Logger.OnLogMessage(obj);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000069C8 File Offset: 0x00004BC8
		public static void LogError(string text)
		{
			if (Logger.OnLogMessage != null)
			{
				LogMessage obj = new LogMessage
				{
					Text = text,
					Type = LogMessageType.Error
				};
				Logger.OnLogMessage(obj);
			}
		}

		// Token: 0x04000236 RID: 566
		[CompilerGenerated]
		private static Action<LogMessage> OnLogMessage;
	}
}
