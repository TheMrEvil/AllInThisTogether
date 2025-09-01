using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x0200026C RID: 620
	internal class LocalFileEventLog : EventLogImpl
	{
		// Token: 0x0600137F RID: 4991 RVA: 0x000518CC File Offset: 0x0004FACC
		public LocalFileEventLog(EventLog coreEventLog) : base(coreEventLog)
		{
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00003917 File Offset: 0x00001B17
		public override void BeginInit()
		{
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000518D8 File Offset: 0x0004FAD8
		public override void Clear()
		{
			string path = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(path))
			{
				return;
			}
			string[] files = Directory.GetFiles(path, "*.log");
			for (int i = 0; i < files.Length; i++)
			{
				File.Delete(files[i]);
			}
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00051922 File Offset: 0x0004FB22
		public override void Close()
		{
			if (this.file_watcher != null)
			{
				this.file_watcher.EnableRaisingEvents = false;
				this.file_watcher = null;
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00051940 File Offset: 0x0004FB40
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			string text = this.FindLogStore(sourceData.LogName);
			if (!Directory.Exists(text))
			{
				base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
				Directory.CreateDirectory(text);
				Directory.CreateDirectory(Path.Combine(text, sourceData.LogName));
				if (this.RunningOnUnix)
				{
					LocalFileEventLog.ModifyAccessPermissions(text, "777");
					LocalFileEventLog.ModifyAccessPermissions(text, "+t");
				}
			}
			Directory.CreateDirectory(Path.Combine(text, sourceData.Source));
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000519BD File Offset: 0x0004FBBD
		public override void Delete(string logName, string machineName)
		{
			string path = this.FindLogStore(logName);
			if (!Directory.Exists(path))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", logName, machineName));
			}
			Directory.Delete(path, true);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000519EC File Offset: 0x0004FBEC
		public override void DeleteEventSource(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", source, machineName));
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", source, machineName));
			}
			Directory.Delete(text);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00051A43 File Offset: 0x0004FC43
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00051A4B File Offset: 0x0004FC4B
		public override void DisableNotification()
		{
			if (this.file_watcher == null)
			{
				return;
			}
			this.file_watcher.EnableRaisingEvents = false;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00051A64 File Offset: 0x0004FC64
		public override void EnableNotification()
		{
			if (this.file_watcher == null)
			{
				string path = this.FindLogStore(base.CoreEventLog.Log);
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				this.file_watcher = new FileSystemWatcher();
				this.file_watcher.Path = path;
				this.file_watcher.Created += delegate(object o, FileSystemEventArgs e)
				{
					LocalFileEventLog obj = this;
					lock (obj)
					{
						if (this._notifying)
						{
							return;
						}
						this._notifying = true;
					}
					Thread.Sleep(100);
					try
					{
						while (this.GetLatestIndex() > this.last_notification_index)
						{
							try
							{
								EventLog coreEventLog = base.CoreEventLog;
								int num = this.last_notification_index;
								this.last_notification_index = num + 1;
								coreEventLog.OnEntryWritten(this.GetEntry(num));
							}
							catch (Exception)
							{
							}
						}
					}
					finally
					{
						obj = this;
						lock (obj)
						{
							this._notifying = false;
						}
					}
				};
			}
			this.last_notification_index = this.GetLatestIndex();
			this.file_watcher.EnableRaisingEvents = true;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00003917 File Offset: 0x00001B17
		public override void EndInit()
		{
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00051AE0 File Offset: 0x0004FCE0
		public override bool Exists(string logName, string machineName)
		{
			return Directory.Exists(this.FindLogStore(logName));
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00051AEE File Offset: 0x0004FCEE
		[MonoTODO("Use MessageTable from PE for lookup")]
		protected override string FormatMessage(string source, uint eventID, string[] replacementStrings)
		{
			return string.Join(", ", replacementStrings);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00051AFC File Offset: 0x0004FCFC
		protected override int GetEntryCount()
		{
			string path = this.FindLogStore(base.CoreEventLog.Log);
			if (!Directory.Exists(path))
			{
				return 0;
			}
			return Directory.GetFiles(path, "*.log").Length;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00051B34 File Offset: 0x0004FD34
		protected override EventLogEntry GetEntry(int index)
		{
			string path = Path.Combine(this.FindLogStore(base.CoreEventLog.Log), (index + 1).ToString(CultureInfo.InvariantCulture) + ".log");
			EventLogEntry result;
			using (TextReader textReader = File.OpenText(path))
			{
				int index2 = int.Parse(Path.GetFileNameWithoutExtension(path), CultureInfo.InvariantCulture);
				uint num = uint.Parse(textReader.ReadLine().Substring(12), CultureInfo.InvariantCulture);
				EventLogEntryType entryType = (EventLogEntryType)Enum.Parse(typeof(EventLogEntryType), textReader.ReadLine().Substring(11));
				string source = textReader.ReadLine().Substring(8);
				string text = textReader.ReadLine().Substring(10);
				short categoryNumber = short.Parse(text, CultureInfo.InvariantCulture);
				string category = "(" + text + ")";
				DateTime timeGenerated = DateTime.ParseExact(textReader.ReadLine().Substring(15), "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
				DateTime lastWriteTime = File.GetLastWriteTime(path);
				int num2 = int.Parse(textReader.ReadLine().Substring(20));
				List<string> list = new List<string>();
				StringBuilder stringBuilder = new StringBuilder();
				while (list.Count < num2)
				{
					char c = (char)textReader.Read();
					if (c == '\0')
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				string[] replacementStrings = list.ToArray();
				string message = this.FormatMessage(source, num, replacementStrings);
				int eventID = EventLog.GetEventID((long)((ulong)num));
				byte[] data = Convert.FromBase64String(textReader.ReadToEnd());
				result = new EventLogEntry(category, categoryNumber, index2, eventID, source, message, null, Environment.MachineName, entryType, timeGenerated, lastWriteTime, data, replacementStrings, (long)((ulong)num));
			}
			return result;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00051D08 File Offset: 0x0004FF08
		[MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00051D18 File Offset: 0x0004FF18
		protected override string[] GetLogNames(string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return new string[0];
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			string[] array = new string[directories.Length];
			for (int i = 0; i < directories.Length; i++)
			{
				array[i] = Path.GetFileName(directories[i]);
			}
			return array;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00051D70 File Offset: 0x0004FF70
		public override string LogNameFromSourceName(string source, string machineName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return string.Empty;
			}
			string text = this.FindSourceDirectory(source);
			if (text == null)
			{
				return string.Empty;
			}
			return new DirectoryInfo(text).Parent.Name;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00051DB1 File Offset: 0x0004FFB1
		public override bool SourceExists(string source, string machineName)
		{
			return Directory.Exists(this.EventLogStore) && this.FindSourceDirectory(source) != null;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00051DCC File Offset: 0x0004FFCC
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			object obj = LocalFileEventLog.lockObject;
			lock (obj)
			{
				string path = Path.Combine(this.FindLogStore(base.CoreEventLog.Log), (this.GetLatestIndex() + 1).ToString(CultureInfo.InvariantCulture) + ".log");
				try
				{
					using (TextWriter textWriter = File.CreateText(path))
					{
						textWriter.WriteLine("InstanceID: {0}", instanceID.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("EntryType: {0}", (int)type);
						textWriter.WriteLine("Source: {0}", base.CoreEventLog.Source);
						textWriter.WriteLine("Category: {0}", category.ToString(CultureInfo.InvariantCulture));
						textWriter.WriteLine("TimeGenerated: {0}", DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
						textWriter.WriteLine("ReplacementStrings: {0}", replacementStrings.Length.ToString(CultureInfo.InvariantCulture));
						StringBuilder stringBuilder = new StringBuilder();
						foreach (string value in replacementStrings)
						{
							stringBuilder.Append(value);
							stringBuilder.Append('\0');
						}
						textWriter.Write(stringBuilder.ToString());
						textWriter.Write(Convert.ToBase64String(rawData));
					}
				}
				catch (IOException)
				{
					File.Delete(path);
				}
			}
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00051F80 File Offset: 0x00050180
		private string FindSourceDirectory(string source)
		{
			string result = null;
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				string[] directories2 = Directory.GetDirectories(directories[i], "*");
				for (int j = 0; j < directories2.Length; j++)
				{
					if (string.Compare(Path.GetFileName(directories2[j]), source, true, CultureInfo.InvariantCulture) == 0)
					{
						result = directories2[j];
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00051FF0 File Offset: 0x000501F0
		private bool RunningOnUnix
		{
			get
			{
				int platform = (int)Environment.OSVersion.Platform;
				return platform == 4 || platform == 128 || platform == 6;
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0005201C File Offset: 0x0005021C
		private string FindLogStore(string logName)
		{
			if (!Directory.Exists(this.EventLogStore))
			{
				return Path.Combine(this.EventLogStore, logName);
			}
			string[] directories = Directory.GetDirectories(this.EventLogStore, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				if (string.Compare(Path.GetFileName(directories[i]), logName, true, CultureInfo.InvariantCulture) == 0)
				{
					return directories[i];
				}
			}
			return Path.Combine(this.EventLogStore, logName);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00052088 File Offset: 0x00050288
		private string EventLogStore
		{
			get
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (environmentVariable != null && environmentVariable.Length > "local".Length + 1)
				{
					return environmentVariable.Substring("local".Length + 1);
				}
				if (this.RunningOnUnix)
				{
					return "/var/lib/mono/eventlog";
				}
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "mono\\eventlog");
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000520EC File Offset: 0x000502EC
		private int GetLatestIndex()
		{
			int num = 0;
			string[] files = Directory.GetFiles(this.FindLogStore(base.CoreEventLog.Log), "*.log");
			for (int i = 0; i < files.Length; i++)
			{
				try
				{
					int num2 = int.Parse(Path.GetFileNameWithoutExtension(files[i]), CultureInfo.InvariantCulture);
					if (num2 > num)
					{
						num = num2;
					}
				}
				catch
				{
				}
			}
			return num;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00052158 File Offset: 0x00050358
		private static void ModifyAccessPermissions(string path, string permissions)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "chmod";
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.Arguments = string.Format("{0} \"{1}\"", permissions, path);
			Process process = null;
			try
			{
				process = Process.Start(processStartInfo);
			}
			catch (Exception inner)
			{
				throw new SecurityException("Access permissions could not be modified.", inner);
			}
			process.WaitForExit();
			if (process.ExitCode != 0)
			{
				process.Close();
				throw new SecurityException("Access permissions could not be modified.");
			}
			process.Close();
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x000521EC File Offset: 0x000503EC
		public override OverflowAction OverflowAction
		{
			get
			{
				return OverflowAction.DoNotOverwrite;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x000521EF File Offset: 0x000503EF
		public override int MinimumRetentionDays
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x000521F6 File Offset: 0x000503F6
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x00052201 File Offset: 0x00050401
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

		// Token: 0x0600139D RID: 5021 RVA: 0x0005220D File Offset: 0x0005040D
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotSupportedException("This EventLog implementation does not support modifying overflow policy");
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00052219 File Offset: 0x00050419
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotSupportedException("This EventLog implementation does not support registering display name");
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00052225 File Offset: 0x00050425
		// Note: this type is marked as 'beforefieldinit'.
		static LocalFileEventLog()
		{
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00052234 File Offset: 0x00050434
		[CompilerGenerated]
		private void <EnableNotification>b__14_0(object o, FileSystemEventArgs e)
		{
			LocalFileEventLog obj = this;
			lock (obj)
			{
				if (this._notifying)
				{
					return;
				}
				this._notifying = true;
			}
			Thread.Sleep(100);
			try
			{
				while (this.GetLatestIndex() > this.last_notification_index)
				{
					try
					{
						EventLog coreEventLog = base.CoreEventLog;
						int num = this.last_notification_index;
						this.last_notification_index = num + 1;
						coreEventLog.OnEntryWritten(this.GetEntry(num));
					}
					catch (Exception)
					{
					}
				}
			}
			finally
			{
				obj = this;
				lock (obj)
				{
					this._notifying = false;
				}
			}
		}

		// Token: 0x04000B03 RID: 2819
		private const string DateFormat = "yyyyMMddHHmmssfff";

		// Token: 0x04000B04 RID: 2820
		private static readonly object lockObject = new object();

		// Token: 0x04000B05 RID: 2821
		private FileSystemWatcher file_watcher;

		// Token: 0x04000B06 RID: 2822
		private int last_notification_index;

		// Token: 0x04000B07 RID: 2823
		private bool _notifying;
	}
}
