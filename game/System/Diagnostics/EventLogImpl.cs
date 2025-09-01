using System;
using System.Globalization;

namespace System.Diagnostics
{
	// Token: 0x0200025E RID: 606
	internal abstract class EventLogImpl
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x00050A6C File Offset: 0x0004EC6C
		protected EventLogImpl(EventLog coreEventLog)
		{
			this._coreEventLog = coreEventLog;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00050A7B File Offset: 0x0004EC7B
		protected EventLog CoreEventLog
		{
			get
			{
				return this._coreEventLog;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00050A84 File Offset: 0x0004EC84
		public int EntryCount
		{
			get
			{
				if (this._coreEventLog.Log == null || this._coreEventLog.Log.Length == 0)
				{
					throw new ArgumentException("Log property is not set.");
				}
				if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", this._coreEventLog.Log, this._coreEventLog.MachineName));
				}
				return this.GetEntryCount();
			}
		}

		// Token: 0x17000370 RID: 880
		public EventLogEntry this[int index]
		{
			get
			{
				if (this._coreEventLog.Log == null || this._coreEventLog.Log.Length == 0)
				{
					throw new ArgumentException("Log property is not set.");
				}
				if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", this._coreEventLog.Log, this._coreEventLog.MachineName));
				}
				if (index < 0 || index >= this.EntryCount)
				{
					throw new ArgumentException("Index out of range");
				}
				return this.GetEntry(index);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00050BAC File Offset: 0x0004EDAC
		public string LogDisplayName
		{
			get
			{
				if (this._coreEventLog.Log != null && this._coreEventLog.Log.Length == 0)
				{
					throw new InvalidOperationException("Event log names must consist of printable characters and cannot contain \\, *, ?, or spaces.");
				}
				if (this._coreEventLog.Log != null)
				{
					if (this._coreEventLog.Log.Length == 0)
					{
						return string.Empty;
					}
					if (!EventLog.Exists(this._coreEventLog.Log, this._coreEventLog.MachineName))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cannot find Log {0} on computer {1}.", this._coreEventLog.Log, this._coreEventLog.MachineName));
					}
				}
				return this.GetLogDisplayName();
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00050C58 File Offset: 0x0004EE58
		public EventLogEntry[] GetEntries()
		{
			string log = this.CoreEventLog.Log;
			if (log == null || log.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (!EventLog.Exists(log))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The event log '{0}' on  computer '{1}' does not exist.", log, this._coreEventLog.MachineName));
			}
			int entryCount = this.GetEntryCount();
			EventLogEntry[] array = new EventLogEntry[entryCount];
			for (int i = 0; i < entryCount; i++)
			{
				array[i] = this.GetEntry(i);
			}
			return array;
		}

		// Token: 0x060012EA RID: 4842
		public abstract void DisableNotification();

		// Token: 0x060012EB RID: 4843
		public abstract void EnableNotification();

		// Token: 0x060012EC RID: 4844
		public abstract void BeginInit();

		// Token: 0x060012ED RID: 4845
		public abstract void Clear();

		// Token: 0x060012EE RID: 4846
		public abstract void Close();

		// Token: 0x060012EF RID: 4847
		public abstract void CreateEventSource(EventSourceCreationData sourceData);

		// Token: 0x060012F0 RID: 4848
		public abstract void Delete(string logName, string machineName);

		// Token: 0x060012F1 RID: 4849
		public abstract void DeleteEventSource(string source, string machineName);

		// Token: 0x060012F2 RID: 4850
		public abstract void Dispose(bool disposing);

		// Token: 0x060012F3 RID: 4851
		public abstract void EndInit();

		// Token: 0x060012F4 RID: 4852
		public abstract bool Exists(string logName, string machineName);

		// Token: 0x060012F5 RID: 4853
		protected abstract int GetEntryCount();

		// Token: 0x060012F6 RID: 4854
		protected abstract EventLogEntry GetEntry(int index);

		// Token: 0x060012F7 RID: 4855 RVA: 0x00050CD8 File Offset: 0x0004EED8
		public EventLog[] GetEventLogs(string machineName)
		{
			string[] logNames = this.GetLogNames(machineName);
			EventLog[] array = new EventLog[logNames.Length];
			for (int i = 0; i < logNames.Length; i++)
			{
				EventLog eventLog = new EventLog(logNames[i], machineName);
				array[i] = eventLog;
			}
			return array;
		}

		// Token: 0x060012F8 RID: 4856
		protected abstract string GetLogDisplayName();

		// Token: 0x060012F9 RID: 4857
		public abstract string LogNameFromSourceName(string source, string machineName);

		// Token: 0x060012FA RID: 4858
		public abstract bool SourceExists(string source, string machineName);

		// Token: 0x060012FB RID: 4859
		public abstract void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData);

		// Token: 0x060012FC RID: 4860
		protected abstract string FormatMessage(string source, uint messageID, string[] replacementStrings);

		// Token: 0x060012FD RID: 4861
		protected abstract string[] GetLogNames(string machineName);

		// Token: 0x060012FE RID: 4862 RVA: 0x00050D14 File Offset: 0x0004EF14
		protected void ValidateCustomerLogName(string logName, string machineName)
		{
			if (logName.Length >= 8)
			{
				string text = logName.Substring(0, 8);
				if (string.Compare(text, "AppEvent", true) == 0 || string.Compare(text, "SysEvent", true) == 0 || string.Compare(text, "SecEvent", true) == 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The log name: '{0}' is invalid for customer log creation.", logName));
				}
				foreach (string text2 in this.GetLogNames(machineName))
				{
					if (text2.Length >= 8 && string.Compare(text2, 0, text, 0, 8, true) == 0)
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Only the first eight characters of a custom log name are significant, and there is already another log on the system using the first eight characters of the name given. Name given: '{0}', name of existing log: '{1}'.", logName, text2));
					}
				}
			}
			if (!this.SourceExists(logName, machineName))
			{
				return;
			}
			if (machineName == ".")
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Log {0} has already been registered as a source on the local computer.", logName));
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Log {0} has already been registered as a source on the computer {1}.", logName, machineName));
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060012FF RID: 4863
		public abstract OverflowAction OverflowAction { get; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001300 RID: 4864
		public abstract int MinimumRetentionDays { get; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001301 RID: 4865
		// (set) Token: 0x06001302 RID: 4866
		public abstract long MaximumKilobytes { get; set; }

		// Token: 0x06001303 RID: 4867
		public abstract void ModifyOverflowPolicy(OverflowAction action, int retentionDays);

		// Token: 0x06001304 RID: 4868
		public abstract void RegisterDisplayName(string resourceFile, long resourceId);

		// Token: 0x04000ACE RID: 2766
		private readonly EventLog _coreEventLog;
	}
}
