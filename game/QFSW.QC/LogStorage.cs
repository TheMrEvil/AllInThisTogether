using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000018 RID: 24
	public class LogStorage : ILogStorage
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002EAB File Offset: 0x000010AB
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002EB3 File Offset: 0x000010B3
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002EBC File Offset: 0x000010BC
		public IReadOnlyList<ILog> Logs
		{
			get
			{
				return this._consoleLogs;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EC4 File Offset: 0x000010C4
		public LogStorage(int maxStoredLogs = -1)
		{
			this.MaxStoredLogs = maxStoredLogs;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EF0 File Offset: 0x000010F0
		public void AddLog(ILog log)
		{
			this._consoleLogs.Add(log);
			int num = this._logTraceBuilder.Length + log.Text.Length;
			if (log.NewLine && this._logTraceBuilder.Length > 0)
			{
				num += Environment.NewLine.Length;
			}
			if (this.MaxStoredLogs > 0)
			{
				while (this._consoleLogs.Count > this.MaxStoredLogs)
				{
					int num2 = this._consoleLogs[0].Text.Length;
					if (this._consoleLogs.Count > 1 && this._consoleLogs[1].NewLine)
					{
						num2 += Environment.NewLine.Length;
					}
					num2 = Mathf.Min(num2, this._logTraceBuilder.Length);
					num -= num2;
					this._logTraceBuilder.Remove(0, num2);
					this._consoleLogs.RemoveAt(0);
				}
			}
			int i;
			for (i = this._logTraceBuilder.Capacity; i < num; i *= 2)
			{
			}
			this._logTraceBuilder.EnsureCapacity(i);
			if (log.NewLine && this._logTraceBuilder.Length > 0)
			{
				this._logTraceBuilder.Append(Environment.NewLine);
			}
			this._logTraceBuilder.Append(log.Text);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000303C File Offset: 0x0000123C
		public void RemoveLog()
		{
			if (this._consoleLogs.Count > 0)
			{
				ILog log = this._consoleLogs[this._consoleLogs.Count - 1];
				this._consoleLogs.RemoveAt(this._consoleLogs.Count - 1);
				int num = log.Text.Length;
				if (log.NewLine && this._consoleLogs.Count > 0)
				{
					num += Environment.NewLine.Length;
				}
				this._logTraceBuilder.Remove(this._logTraceBuilder.Length - num, num);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030CF File Offset: 0x000012CF
		public void Clear()
		{
			this._consoleLogs.Clear();
			this._logTraceBuilder.Length = 0;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000030E8 File Offset: 0x000012E8
		public string GetLogString()
		{
			return this._logTraceBuilder.ToString();
		}

		// Token: 0x0400002B RID: 43
		private readonly List<ILog> _consoleLogs = new List<ILog>(10);

		// Token: 0x0400002C RID: 44
		private readonly StringBuilder _logTraceBuilder = new StringBuilder(2048);

		// Token: 0x0400002D RID: 45
		[CompilerGenerated]
		private int <MaxStoredLogs>k__BackingField;
	}
}
