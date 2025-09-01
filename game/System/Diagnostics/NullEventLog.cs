using System;

namespace System.Diagnostics
{
	// Token: 0x0200026E RID: 622
	internal class NullEventLog : EventLogImpl
	{
		// Token: 0x060013A3 RID: 5027 RVA: 0x000518CC File Offset: 0x0004FACC
		public NullEventLog(EventLog coreEventLog) : base(coreEventLog)
		{
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00003917 File Offset: 0x00001B17
		public override void BeginInit()
		{
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Clear()
		{
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Close()
		{
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00003917 File Offset: 0x00001B17
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Delete(string logName, string machineName)
		{
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00003917 File Offset: 0x00001B17
		public override void DeleteEventSource(string source, string machineName)
		{
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Dispose(bool disposing)
		{
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00003917 File Offset: 0x00001B17
		public override void DisableNotification()
		{
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00003917 File Offset: 0x00001B17
		public override void EnableNotification()
		{
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00003917 File Offset: 0x00001B17
		public override void EndInit()
		{
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool Exists(string logName, string machineName)
		{
			return true;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00051AEE File Offset: 0x0004FCEE
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00003062 File Offset: 0x00001262
		protected override int GetEntryCount()
		{
			return 0;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00002F6A File Offset: 0x0000116A
		protected override EventLogEntry GetEntry(int index)
		{
			return null;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00051D08 File Offset: 0x0004FF08
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00052308 File Offset: 0x00050508
		protected override string[] GetLogNames(string machineName)
		{
			return new string[0];
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00002F6A File Offset: 0x0000116A
		public override string LogNameFromSourceName(string source, string machineName)
		{
			return null;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00003062 File Offset: 0x00001262
		public override bool SourceExists(string source, string machineName)
		{
			return false;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00003917 File Offset: 0x00001B17
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x000521EC File Offset: 0x000503EC
		public override OverflowAction OverflowAction
		{
			get
			{
				return OverflowAction.DoNotOverwrite;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000521EF File Offset: 0x000503EF
		public override int MinimumRetentionDays
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x000521F6 File Offset: 0x000503F6
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00052201 File Offset: 0x00050401
		public override long MaximumKilobytes
		{
			get
			{
				return long.MaxValue;
			}
			set
			{
				throw new NotSupportedException("This EventLog implementation does not support setting max kilobytes policy");
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0005220D File Offset: 0x0005040D
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotSupportedException("This EventLog implementation does not support modifying overflow policy");
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00052219 File Offset: 0x00050419
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotSupportedException("This EventLog implementation does not support registering display name");
		}
	}
}
