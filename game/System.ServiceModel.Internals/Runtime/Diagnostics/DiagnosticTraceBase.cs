using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000040 RID: 64
	internal abstract class DiagnosticTraceBase
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000992E File Offset: 0x00007B2E
		public DiagnosticTraceBase(string traceSourceName)
		{
			this.thisLock = new object();
			this.TraceSourceName = traceSourceName;
			this.LastFailure = DateTime.MinValue;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000995A File Offset: 0x00007B5A
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00009962 File Offset: 0x00007B62
		protected DateTime LastFailure
		{
			[CompilerGenerated]
			get
			{
				return this.<LastFailure>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LastFailure>k__BackingField = value;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000996B File Offset: 0x00007B6B
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private static void UnsafeRemoveDefaultTraceListener(TraceSource traceSource)
		{
			traceSource.Listeners.Remove("Default");
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000997D File Offset: 0x00007B7D
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00009985 File Offset: 0x00007B85
		public TraceSource TraceSource
		{
			get
			{
				return this.traceSource;
			}
			set
			{
				this.SetTraceSource(value);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000998E File Offset: 0x00007B8E
		[SecuritySafeCritical]
		protected void SetTraceSource(TraceSource traceSource)
		{
			if (traceSource != null)
			{
				DiagnosticTraceBase.UnsafeRemoveDefaultTraceListener(traceSource);
				this.traceSource = traceSource;
				this.haveListeners = (this.traceSource.Listeners.Count > 0);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000099B9 File Offset: 0x00007BB9
		public bool HaveListeners
		{
			get
			{
				return this.haveListeners;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000099C4 File Offset: 0x00007BC4
		private SourceLevels FixLevel(SourceLevels level)
		{
			if ((level & (SourceLevels)(-16) & SourceLevels.Verbose) != SourceLevels.Off)
			{
				level |= SourceLevels.Verbose;
			}
			else if ((level & (SourceLevels)(-8) & SourceLevels.Information) != SourceLevels.Off)
			{
				level |= SourceLevels.Information;
			}
			else if ((level & (SourceLevels)(-4) & SourceLevels.Warning) != SourceLevels.Off)
			{
				level |= SourceLevels.Warning;
			}
			if ((level & ~SourceLevels.Critical & SourceLevels.Error) != SourceLevels.Off)
			{
				level |= SourceLevels.Error;
			}
			if ((level & SourceLevels.Critical) != SourceLevels.Off)
			{
				level |= SourceLevels.Critical;
			}
			if (level == SourceLevels.ActivityTracing)
			{
				level = SourceLevels.Off;
			}
			return level;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009A23 File Offset: 0x00007C23
		protected virtual void OnSetLevel(SourceLevels level)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009A28 File Offset: 0x00007C28
		[SecurityCritical]
		private void SetLevel(SourceLevels level)
		{
			SourceLevels sourceLevels = this.FixLevel(level);
			this.level = sourceLevels;
			if (this.TraceSource != null)
			{
				this.haveListeners = (this.TraceSource.Listeners.Count > 0);
				this.OnSetLevel(level);
				this.tracingEnabled = (this.HaveListeners && level > SourceLevels.Off);
				this.TraceSource.Switch.Level = level;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009A94 File Offset: 0x00007C94
		[SecurityCritical]
		private void SetLevelThreadSafe(SourceLevels level)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.SetLevel(level);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00009AD8 File Offset: 0x00007CD8
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00009B16 File Offset: 0x00007D16
		public SourceLevels Level
		{
			get
			{
				if (this.TraceSource != null && this.TraceSource.Switch.Level != this.level)
				{
					this.level = this.TraceSource.Switch.Level;
				}
				return this.level;
			}
			[SecurityCritical]
			set
			{
				this.SetLevelThreadSafe(value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00009B1F File Offset: 0x00007D1F
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00009B27 File Offset: 0x00007D27
		protected string EventSourceName
		{
			[SecuritySafeCritical]
			get
			{
				return this.eventSourceName;
			}
			[SecurityCritical]
			set
			{
				this.eventSourceName = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00009B30 File Offset: 0x00007D30
		public bool TracingEnabled
		{
			get
			{
				return this.tracingEnabled && this.traceSource != null;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00009B48 File Offset: 0x00007D48
		protected static string ProcessName
		{
			[SecuritySafeCritical]
			get
			{
				string result = null;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					result = currentProcess.ProcessName;
				}
				return result;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00009B84 File Offset: 0x00007D84
		protected static int ProcessId
		{
			[SecuritySafeCritical]
			get
			{
				int result = -1;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					result = currentProcess.Id;
				}
				return result;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009BC0 File Offset: 0x00007DC0
		public virtual bool ShouldTrace(TraceEventLevel level)
		{
			return this.ShouldTraceToTraceSource(level);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009BC9 File Offset: 0x00007DC9
		public bool ShouldTrace(TraceEventType type)
		{
			return this.TracingEnabled && this.HaveListeners && this.TraceSource != null && (type & (TraceEventType)this.Level) > (TraceEventType)0;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009BF0 File Offset: 0x00007DF0
		public bool ShouldTraceToTraceSource(TraceEventLevel level)
		{
			return this.ShouldTrace(TraceLevelHelper.GetTraceEventType(level));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009C00 File Offset: 0x00007E00
		public static string XmlEncode(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			int length = text.Length;
			StringBuilder stringBuilder = new StringBuilder(length + 8);
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c != '&')
				{
					if (c != '<')
					{
						if (c != '>')
						{
							stringBuilder.Append(c);
						}
						else
						{
							stringBuilder.Append("&gt;");
						}
					}
					else
					{
						stringBuilder.Append("&lt;");
					}
				}
				else
				{
					stringBuilder.Append("&amp;");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009C84 File Offset: 0x00007E84
		[SecuritySafeCritical]
		protected void AddDomainEventHandlersForCleanup()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			if (this.TraceSource != null)
			{
				this.haveListeners = (this.TraceSource.Listeners.Count > 0);
			}
			this.tracingEnabled = this.haveListeners;
			if (this.TracingEnabled)
			{
				currentDomain.UnhandledException += this.UnhandledExceptionHandler;
				this.SetLevel(this.TraceSource.Switch.Level);
				currentDomain.DomainUnload += this.ExitOrUnloadEventHandler;
				currentDomain.ProcessExit += this.ExitOrUnloadEventHandler;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009D18 File Offset: 0x00007F18
		private void ExitOrUnloadEventHandler(object sender, EventArgs e)
		{
			this.ShutdownTracing();
		}

		// Token: 0x06000252 RID: 594
		protected abstract void OnUnhandledException(Exception exception);

		// Token: 0x06000253 RID: 595 RVA: 0x00009D20 File Offset: 0x00007F20
		protected void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception exception = (Exception)args.ExceptionObject;
			this.OnUnhandledException(exception);
			this.ShutdownTracing();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009D48 File Offset: 0x00007F48
		protected static string CreateSourceString(object source)
		{
			ITraceSourceStringProvider traceSourceStringProvider = source as ITraceSourceStringProvider;
			if (traceSourceStringProvider != null)
			{
				return traceSourceStringProvider.GetSourceString();
			}
			return DiagnosticTraceBase.CreateDefaultSourceString(source);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009D6C File Offset: 0x00007F6C
		internal static string CreateDefaultSourceString(object source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return string.Format(CultureInfo.CurrentCulture, "{0}/{1}", source.GetType().ToString(), source.GetHashCode());
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009DA4 File Offset: 0x00007FA4
		protected static void AddExceptionToTraceString(XmlWriter xml, Exception exception)
		{
			xml.WriteElementString("ExceptionType", DiagnosticTraceBase.XmlEncode(exception.GetType().AssemblyQualifiedName));
			xml.WriteElementString("Message", DiagnosticTraceBase.XmlEncode(exception.Message));
			xml.WriteElementString("StackTrace", DiagnosticTraceBase.XmlEncode(DiagnosticTraceBase.StackTraceString(exception)));
			xml.WriteElementString("ExceptionString", DiagnosticTraceBase.XmlEncode(exception.ToString()));
			Win32Exception ex = exception as Win32Exception;
			if (ex != null)
			{
				xml.WriteElementString("NativeErrorCode", ex.NativeErrorCode.ToString("X", CultureInfo.InvariantCulture));
			}
			if (exception.Data != null && exception.Data.Count > 0)
			{
				xml.WriteStartElement("DataItems");
				foreach (object obj in exception.Data.Keys)
				{
					xml.WriteStartElement("Data");
					xml.WriteElementString("Key", DiagnosticTraceBase.XmlEncode(obj.ToString()));
					xml.WriteElementString("Value", DiagnosticTraceBase.XmlEncode(exception.Data[obj].ToString()));
					xml.WriteEndElement();
				}
				xml.WriteEndElement();
			}
			if (exception.InnerException != null)
			{
				xml.WriteStartElement("InnerException");
				DiagnosticTraceBase.AddExceptionToTraceString(xml, exception.InnerException);
				xml.WriteEndElement();
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009F20 File Offset: 0x00008120
		protected static string StackTraceString(Exception exception)
		{
			string text = exception.StackTrace;
			if (string.IsNullOrEmpty(text))
			{
				StackFrame[] frames = new StackTrace(false).GetFrames();
				int num = 0;
				bool flag = false;
				StackFrame[] array = frames;
				for (int i = 0; i < array.Length; i++)
				{
					string name = array[i].GetMethod().Name;
					if (name == "StackTraceString" || name == "AddExceptionToTraceString" || name == "BuildTrace" || name == "TraceEvent" || name == "TraceException" || name == "GetAdditionalPayload")
					{
						num++;
					}
					else if (name.StartsWith("ThrowHelper", StringComparison.Ordinal))
					{
						num++;
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						break;
					}
				}
				text = new StackTrace(num, false).ToString();
			}
			return text;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009FFC File Offset: 0x000081FC
		[SecuritySafeCritical]
		protected void LogTraceFailure(string traceString, Exception exception)
		{
			TimeSpan t = TimeSpan.FromMinutes(10.0);
			try
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (DateTime.UtcNow.Subtract(this.LastFailure) >= t)
					{
						this.LastFailure = DateTime.UtcNow;
						EventLogger eventLogger = EventLogger.UnsafeCreateEventLogger(this.eventSourceName, this);
						if (exception == null)
						{
							eventLogger.UnsafeLogEvent(TraceEventType.Error, 4, 3221291112U, false, new string[]
							{
								traceString
							});
						}
						else
						{
							eventLogger.UnsafeLogEvent(TraceEventType.Error, 4, 3221291113U, false, new string[]
							{
								traceString,
								exception.ToString()
							});
						}
					}
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
			}
		}

		// Token: 0x06000259 RID: 601
		protected abstract void OnShutdownTracing();

		// Token: 0x0600025A RID: 602 RVA: 0x0000A0D4 File Offset: 0x000082D4
		private void ShutdownTracing()
		{
			if (!this.calledShutdown)
			{
				this.calledShutdown = true;
				try
				{
					this.OnShutdownTracing();
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					this.LogTraceFailure(null, exception);
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A120 File Offset: 0x00008320
		protected bool CalledShutdown
		{
			get
			{
				return this.calledShutdown;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A128 File Offset: 0x00008328
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000A154 File Offset: 0x00008354
		public static Guid ActivityId
		{
			[SecuritySafeCritical]
			get
			{
				object obj = Trace.CorrelationManager.ActivityId;
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			[SecuritySafeCritical]
			set
			{
				Trace.CorrelationManager.ActivityId = value;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A164 File Offset: 0x00008364
		protected static string LookupSeverity(TraceEventType type)
		{
			if (type <= TraceEventType.Verbose)
			{
				switch (type)
				{
				case TraceEventType.Critical:
					return "Critical";
				case TraceEventType.Error:
					return "Error";
				case (TraceEventType)3:
					break;
				case TraceEventType.Warning:
					return "Warning";
				default:
					if (type == TraceEventType.Information)
					{
						return "Information";
					}
					if (type == TraceEventType.Verbose)
					{
						return "Verbose";
					}
					break;
				}
			}
			else if (type <= TraceEventType.Stop)
			{
				if (type == TraceEventType.Start)
				{
					return "Start";
				}
				if (type == TraceEventType.Stop)
				{
					return "Stop";
				}
			}
			else
			{
				if (type == TraceEventType.Suspend)
				{
					return "Suspend";
				}
				if (type == TraceEventType.Transfer)
				{
					return "Transfer";
				}
			}
			return type.ToString();
		}

		// Token: 0x0600025F RID: 607
		public abstract bool IsEnabled();

		// Token: 0x06000260 RID: 608
		public abstract void TraceEventLogEvent(TraceEventType type, TraceRecord traceRecord);

		// Token: 0x06000261 RID: 609 RVA: 0x0000A21C File Offset: 0x0000841C
		// Note: this type is marked as 'beforefieldinit'.
		static DiagnosticTraceBase()
		{
		}

		// Token: 0x04000149 RID: 329
		protected const string DefaultTraceListenerName = "Default";

		// Token: 0x0400014A RID: 330
		protected const string TraceRecordVersion = "http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord";

		// Token: 0x0400014B RID: 331
		protected static string AppDomainFriendlyName = AppDomain.CurrentDomain.FriendlyName;

		// Token: 0x0400014C RID: 332
		private const ushort TracingEventLogCategory = 4;

		// Token: 0x0400014D RID: 333
		private object thisLock;

		// Token: 0x0400014E RID: 334
		private bool tracingEnabled = true;

		// Token: 0x0400014F RID: 335
		private bool calledShutdown;

		// Token: 0x04000150 RID: 336
		private bool haveListeners;

		// Token: 0x04000151 RID: 337
		private SourceLevels level;

		// Token: 0x04000152 RID: 338
		protected string TraceSourceName;

		// Token: 0x04000153 RID: 339
		private TraceSource traceSource;

		// Token: 0x04000154 RID: 340
		[SecurityCritical]
		private string eventSourceName;

		// Token: 0x04000155 RID: 341
		[CompilerGenerated]
		private DateTime <LastFailure>k__BackingField;
	}
}
