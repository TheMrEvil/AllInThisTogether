using System;
using System.Diagnostics;
using System.IO;

namespace WebSocketSharp
{
	// Token: 0x02000012 RID: 18
	public class Logger
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00009116 File Offset: 0x00007316
		public Logger() : this(LogLevel.Error, null, null)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00009123 File Offset: 0x00007323
		public Logger(LogLevel level) : this(level, null, null)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00009130 File Offset: 0x00007330
		public Logger(LogLevel level, string file, Action<LogData, string> output)
		{
			this._level = level;
			this._file = file;
			this._output = (output ?? new Action<LogData, string>(Logger.defaultOutput));
			this._sync = new object();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00009170 File Offset: 0x00007370
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000918C File Offset: 0x0000738C
		public string File
		{
			get
			{
				return this._file;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					this._file = value;
					this.Warn(string.Format("The current path to the log file has been changed to {0}.", this._file));
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000091E8 File Offset: 0x000073E8
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00009204 File Offset: 0x00007404
		public LogLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					this._level = value;
					this.Warn(string.Format("The current logging level has been changed to {0}.", this._level));
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00009264 File Offset: 0x00007464
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000927C File Offset: 0x0000747C
		public Action<LogData, string> Output
		{
			get
			{
				return this._output;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					this._output = (value ?? new Action<LogData, string>(Logger.defaultOutput));
					this.Warn("The current output action has been changed.");
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000092D8 File Offset: 0x000074D8
		private static void defaultOutput(LogData data, string path)
		{
			string value = data.ToString();
			Console.WriteLine(value);
			bool flag = path != null && path.Length > 0;
			if (flag)
			{
				Logger.writeToFile(value, path);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009310 File Offset: 0x00007510
		private void output(string message, LogLevel level)
		{
			object sync = this._sync;
			lock (sync)
			{
				bool flag = this._level > level;
				if (!flag)
				{
					try
					{
						LogData logData = new LogData(level, new StackFrame(2, true), message);
						this._output(logData, this._file);
					}
					catch (Exception ex)
					{
						LogData logData = new LogData(LogLevel.Fatal, new StackFrame(0, true), ex.Message);
						Console.WriteLine(logData.ToString());
					}
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000093B4 File Offset: 0x000075B4
		private static void writeToFile(string value, string path)
		{
			using (StreamWriter streamWriter = new StreamWriter(path, true))
			{
				using (TextWriter textWriter = TextWriter.Synchronized(streamWriter))
				{
					textWriter.WriteLine(value);
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009410 File Offset: 0x00007610
		public void Debug(string message)
		{
			bool flag = this._level > LogLevel.Debug;
			if (!flag)
			{
				this.output(message, LogLevel.Debug);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009438 File Offset: 0x00007638
		public void Error(string message)
		{
			bool flag = this._level > LogLevel.Error;
			if (!flag)
			{
				this.output(message, LogLevel.Error);
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009460 File Offset: 0x00007660
		public void Fatal(string message)
		{
			this.output(message, LogLevel.Fatal);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000946C File Offset: 0x0000766C
		public void Info(string message)
		{
			bool flag = this._level > LogLevel.Info;
			if (!flag)
			{
				this.output(message, LogLevel.Info);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009494 File Offset: 0x00007694
		public void Trace(string message)
		{
			bool flag = this._level > LogLevel.Trace;
			if (!flag)
			{
				this.output(message, LogLevel.Trace);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000094BC File Offset: 0x000076BC
		public void Warn(string message)
		{
			bool flag = this._level > LogLevel.Warn;
			if (!flag)
			{
				this.output(message, LogLevel.Warn);
			}
		}

		// Token: 0x04000077 RID: 119
		private volatile string _file;

		// Token: 0x04000078 RID: 120
		private volatile LogLevel _level;

		// Token: 0x04000079 RID: 121
		private Action<LogData, string> _output;

		// Token: 0x0400007A RID: 122
		private object _sync;
	}
}
