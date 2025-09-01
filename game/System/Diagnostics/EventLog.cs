using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides interaction with Windows event logs.</summary>
	// Token: 0x02000259 RID: 601
	[InstallerType(typeof(EventLogInstaller))]
	[MonitoringDescription("Represents an event log")]
	[DefaultEvent("EntryWritten")]
	public class EventLog : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Does not associate the instance with any log.</summary>
		// Token: 0x06001280 RID: 4736 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		public EventLog() : this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the local computer.</summary>
		/// <param name="logName">The name of the log on the local computer.</param>
		/// <exception cref="T:System.ArgumentNullException">The log name is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.</exception>
		// Token: 0x06001281 RID: 4737 RVA: 0x0004FCC5 File Offset: 0x0004DEC5
		public EventLog(string logName) : this(logName, ".")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer.</summary>
		/// <param name="logName">The name of the log on the specified computer.</param>
		/// <param name="machineName">The computer on which the log exists.</param>
		/// <exception cref="T:System.ArgumentNullException">The log name is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.  
		///  -or-  
		///  The computer name is invalid.</exception>
		// Token: 0x06001282 RID: 4738 RVA: 0x0004FCD3 File Offset: 0x0004DED3
		public EventLog(string logName, string machineName) : this(logName, machineName, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer and creates or assigns the specified source to the <see cref="T:System.Diagnostics.EventLog" />.</summary>
		/// <param name="logName">The name of the log on the specified computer</param>
		/// <param name="machineName">The computer on which the log exists.</param>
		/// <param name="source">The source of event log entries.</param>
		/// <exception cref="T:System.ArgumentNullException">The log name is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.  
		///  -or-  
		///  The computer name is invalid.</exception>
		// Token: 0x06001283 RID: 4739 RVA: 0x0004FCE4 File Offset: 0x0004DEE4
		public EventLog(string logName, string machineName, string source)
		{
			if (logName == null)
			{
				throw new ArgumentNullException("logName");
			}
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", machineName));
			}
			this.source = source;
			this.machineName = machineName;
			this.logName = logName;
			this.Impl = EventLog.CreateEventLogImpl(this);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Diagnostics.EventLog" /> receives <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event notifications.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Diagnostics.EventLog" /> receives notification when an entry is written to the log; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The event log is on a remote computer.</exception>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0004FD4C File Offset: 0x0004DF4C
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x0004FD54 File Offset: 0x0004DF54
		[Browsable(false)]
		[DefaultValue(false)]
		[MonitoringDescription("If enabled raises event when a log is written.")]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.doRaiseEvents;
			}
			set
			{
				if (value == this.doRaiseEvents)
				{
					return;
				}
				if (value)
				{
					this.Impl.EnableNotification();
				}
				else
				{
					this.Impl.DisableNotification();
				}
				this.doRaiseEvents = value;
			}
		}

		/// <summary>Gets the contents of the event log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntryCollection" /> holding the entries in the event log. Each entry is associated with an instance of the <see cref="T:System.Diagnostics.EventLogEntry" /> class.</returns>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0004FD82 File Offset: 0x0004DF82
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The entries in the log.")]
		public EventLogEntryCollection Entries
		{
			get
			{
				return new EventLogEntryCollection(this.Impl);
			}
		}

		/// <summary>Gets or sets the name of the log to read from or write to.</summary>
		/// <returns>The name of the log. This can be Application, System, Security, or a custom log name. The default is an empty string ("").</returns>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0004FD8F File Offset: 0x0004DF8F
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x0004FDB4 File Offset: 0x0004DFB4
		[ReadOnly(true)]
		[DefaultValue("")]
		[RecommendedAsConfigurable(true)]
		[TypeConverter("System.Diagnostics.Design.LogConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("Name of the log that is read and written.")]
		public string Log
		{
			get
			{
				if (this.source != null && this.source.Length > 0)
				{
					return this.GetLogName();
				}
				return this.logName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (string.Compare(this.logName, value, true) != 0)
				{
					this.logName = value;
					this.Reset();
				}
			}
		}

		/// <summary>Gets the event log's friendly name.</summary>
		/// <returns>A name that represents the event log in the system's event viewer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified <see cref="P:System.Diagnostics.EventLog.Log" /> does not exist in the registry for this computer.</exception>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0004FDE0 File Offset: 0x0004DFE0
		[Browsable(false)]
		public string LogDisplayName
		{
			get
			{
				return this.Impl.LogDisplayName;
			}
		}

		/// <summary>Gets or sets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the server on which the event log resides. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid.</exception>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0004FDED File Offset: 0x0004DFED
		// (set) Token: 0x0600128B RID: 4747 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		[RecommendedAsConfigurable(true)]
		[DefaultValue(".")]
		[ReadOnly(true)]
		[MonitoringDescription("Name of the machine that this log get written to.")]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				if (value == null || value.Trim().Length == 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value {0} for property MachineName.", value));
				}
				if (string.Compare(this.machineName, value, true) != 0)
				{
					this.Close();
					this.machineName = value;
				}
			}
		}

		/// <summary>Gets or sets the source name to register and use when writing to the event log.</summary>
		/// <returns>The name registered with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The source name results in a registry key path longer than 254 characters.</exception>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x0004FE47 File Offset: 0x0004E047
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x0004FE50 File Offset: 0x0004E050
		[ReadOnly(true)]
		[DefaultValue("")]
		[MonitoringDescription("The application name that writes the log.")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RecommendedAsConfigurable(true)]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.source == null || (this.source.Length == 0 && (this.logName == null || this.logName.Length == 0)))
				{
					this.source = value;
					return;
				}
				if (string.Compare(this.source, value, true) != 0)
				{
					this.source = value;
					this.Reset();
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of an <see cref="T:System.Diagnostics.EventLog" /> entry written event.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> used to marshal event-handler calls issued as a result of an <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event on the event log.</returns>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0004FEB5 File Offset: 0x0004E0B5
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x0004FEBD File Offset: 0x0004E0BD
		[MonitoringDescription("An object that synchronizes event handler calls.")]
		[Browsable(false)]
		[DefaultValue(null)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Gets the configured behavior for storing new entries when the event log reaches its maximum log file size.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.OverflowAction" /> value that specifies the configured behavior for storing new entries when the event log reaches its maximum log size. The default is <see cref="F:System.Diagnostics.OverflowAction.OverwriteOlder" />.</returns>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x0004FEC6 File Offset: 0x0004E0C6
		[MonoTODO]
		[Browsable(false)]
		[ComVisible(false)]
		public OverflowAction OverflowAction
		{
			get
			{
				return this.Impl.OverflowAction;
			}
		}

		/// <summary>Gets the number of days to retain entries in the event log.</summary>
		/// <returns>The number of days that entries in the event log are retained. The default value is 7.</returns>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0004FED3 File Offset: 0x0004E0D3
		[ComVisible(false)]
		[MonoTODO]
		[Browsable(false)]
		public int MinimumRetentionDays
		{
			get
			{
				return this.Impl.MinimumRetentionDays;
			}
		}

		/// <summary>Gets or sets the maximum event log size in kilobytes.</summary>
		/// <returns>The maximum event log size in kilobytes. The default is 512, indicating a maximum file size of 512 kilobytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 64, or greater than 4194240, or not an even multiple of 64.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.  
		/// -or-
		///  The registry key for the event log could not be opened on the target computer.</exception>
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x0004FEE0 File Offset: 0x0004E0E0
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x0004FEED File Offset: 0x0004E0ED
		[MonoTODO]
		[ComVisible(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long MaximumKilobytes
		{
			get
			{
				return this.Impl.MaximumKilobytes;
			}
			set
			{
				this.Impl.MaximumKilobytes = value;
			}
		}

		/// <summary>Changes the configured behavior for writing new entries when the event log reaches its maximum file size.</summary>
		/// <param name="action">The overflow behavior for writing new entries to the event log.</param>
		/// <param name="retentionDays">The minimum number of days each event log entry is retained. This parameter is used only if <paramref name="action" /> is set to <see cref="F:System.Diagnostics.OverflowAction.OverwriteOlder" />.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="P:System.Diagnostics.EventLog.OverflowAction" /> value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="retentionDays" /> is less than one, or larger than 365.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.  
		/// -or-
		///  The registry key for the event log could not be opened on the target computer.</exception>
		// Token: 0x06001294 RID: 4756 RVA: 0x0004FEFB File Offset: 0x0004E0FB
		[MonoTODO]
		[ComVisible(false)]
		public void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			this.Impl.ModifyOverflowPolicy(action, retentionDays);
		}

		/// <summary>Specifies the localized name of the event log, which is displayed in the server Event Viewer.</summary>
		/// <param name="resourceFile">The fully specified path to a localized resource file.</param>
		/// <param name="resourceId">The resource identifier that indexes a localized string within the resource file.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.EventLog.Log" /> value is not a valid log name.  
		/// -or-
		///  The registry key for the event log could not be opened on the target computer.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceFile" /> is <see langword="null" />.</exception>
		// Token: 0x06001295 RID: 4757 RVA: 0x0004FF0A File Offset: 0x0004E10A
		[ComVisible(false)]
		[MonoTODO]
		public void RegisterDisplayName(string resourceFile, long resourceId)
		{
			this.Impl.RegisterDisplayName(resourceFile, resourceId);
		}

		/// <summary>Begins the initialization of an <see cref="T:System.Diagnostics.EventLog" /> used on a form or used by another component. The initialization occurs at runtime.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Diagnostics.EventLog" /> is already initialized.</exception>
		// Token: 0x06001296 RID: 4758 RVA: 0x0004FF19 File Offset: 0x0004E119
		public void BeginInit()
		{
			this.Impl.BeginInit();
		}

		/// <summary>Removes all entries from the event log.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.  
		///  -or-  
		///  The log cannot be opened. A Windows error code is not available.</exception>
		/// <exception cref="T:System.ArgumentException">A value is not specified for the <see cref="P:System.Diagnostics.EventLog.Log" /> property. Make sure the log name is not an empty string.</exception>
		/// <exception cref="T:System.InvalidOperationException">The log does not exist.</exception>
		// Token: 0x06001297 RID: 4759 RVA: 0x0004FF28 File Offset: 0x0004E128
		public void Clear()
		{
			string log = this.Log;
			if (log == null || log.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (!EventLog.Exists(log, this.MachineName))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", log, this.machineName));
			}
			this.Impl.Clear();
			this.Reset();
		}

		/// <summary>Closes the event log and releases read and write handles.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log's read handle or write handle was not released successfully.</exception>
		// Token: 0x06001298 RID: 4760 RVA: 0x0004FF8D File Offset: 0x0004E18D
		public void Close()
		{
			this.Impl.Close();
			this.EnableRaisingEvents = false;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0004FFA4 File Offset: 0x0004E1A4
		internal void Reset()
		{
			bool enableRaisingEvents = this.EnableRaisingEvents;
			this.Close();
			this.EnableRaisingEvents = enableRaisingEvents;
		}

		/// <summary>Establishes the specified source name as a valid event source for writing entries to a log on the local computer. This method can also create a new custom log on the local computer.</summary>
		/// <param name="source">The source name by which the application is registered on the local computer.</param>
		/// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is an empty string ("") or <see langword="null" />.  
		/// -or-
		///  <paramref name="logName" /> is not a valid event log name. Event log names must consist of printable characters, and cannot include the characters '*', '?', or '\'.  
		/// -or-
		///  <paramref name="logName" /> is not valid for user log creation. The event log names AppEvent, SysEvent, and SecEvent are reserved for system use.  
		/// -or-
		///  The log name matches an existing event source name.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.  
		/// -or-
		///  The first 8 characters of <paramref name="logName" /> match the first 8 characters of an existing event log name.  
		/// -or-
		///  The source cannot be registered because it already exists on the local computer.  
		/// -or-
		///  The source name matches an existing event log name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the local computer.</exception>
		// Token: 0x0600129A RID: 4762 RVA: 0x0004FFC5 File Offset: 0x0004E1C5
		public static void CreateEventSource(string source, string logName)
		{
			EventLog.CreateEventSource(source, logName, ".");
		}

		/// <summary>Establishes the specified source name as a valid event source for writing entries to a log on the specified computer. This method can also be used to create a new custom log on the specified computer.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log. If you do not specify a value, <paramref name="logName" /> defaults to Application.</param>
		/// <param name="machineName">The name of the computer to register this event source with, or "." for the local computer.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> is not a valid computer name.  
		/// -or-
		///  <paramref name="source" /> is an empty string ("") or <see langword="null" />.  
		/// -or-
		///  <paramref name="logName" /> is not a valid event log name. Event log names must consist of printable characters, and cannot include the characters '*', '?', or '\'.  
		/// -or-
		///  <paramref name="logName" /> is not valid for user log creation. The event log names AppEvent, SysEvent, and SecEvent are reserved for system use.  
		/// -or-
		///  The log name matches an existing event source name.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.  
		/// -or-
		///  The first 8 characters of <paramref name="logName" /> match the first 8 characters of an existing event log name on the specified computer.  
		/// -or-
		///  The source cannot be registered because it already exists on the specified computer.  
		/// -or-
		///  The source name matches an existing event source name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the specified computer.</exception>
		// Token: 0x0600129B RID: 4763 RVA: 0x0004FFD3 File Offset: 0x0004E1D3
		[Obsolete("use CreateEventSource(EventSourceCreationData) instead")]
		public static void CreateEventSource(string source, string logName, string machineName)
		{
			EventLog.CreateEventSource(new EventSourceCreationData(source, logName, machineName));
		}

		/// <summary>Establishes a valid event source for writing localized event messages, using the specified configuration properties for the event source and the corresponding event log.</summary>
		/// <param name="sourceData">The configuration properties for the event source and its target event log.</param>
		/// <exception cref="T:System.ArgumentException">The computer name specified in <paramref name="sourceData" /> is not valid.  
		/// -or-
		///  The source name specified in <paramref name="sourceData" /> is <see langword="null" />.  
		/// -or-
		///  The log name specified in <paramref name="sourceData" /> is not valid. Event log names must consist of printable characters and cannot include the characters '*', '?', or '\'.  
		/// -or-
		///  The log name specified in <paramref name="sourceData" /> is not valid for user log creation. The Event log names AppEvent, SysEvent, and SecEvent are reserved for system use.  
		/// -or-
		///  The log name matches an existing event source name.  
		/// -or-
		///  The source name specified in <paramref name="sourceData" /> results in a registry key path longer than 254 characters.  
		/// -or-
		///  The first 8 characters of the log name specified in <paramref name="sourceData" /> are not unique.  
		/// -or-
		///  The source name specified in <paramref name="sourceData" /> is already registered.  
		/// -or-
		///  The source name specified in <paramref name="sourceData" /> matches an existing event log name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceData" /> is <see langword="null" />.</exception>
		// Token: 0x0600129C RID: 4764 RVA: 0x0004FFE4 File Offset: 0x0004E1E4
		[MonoNotSupported("remote machine is not supported")]
		public static void CreateEventSource(EventSourceCreationData sourceData)
		{
			if (sourceData.Source == null || sourceData.Source.Length == 0)
			{
				throw new ArgumentException("Source property value has not been specified.");
			}
			if (sourceData.LogName == null || sourceData.LogName.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (EventLog.SourceExists(sourceData.Source, sourceData.MachineName))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Source '{0}' already exists on '{1}'.", sourceData.Source, sourceData.MachineName));
			}
			EventLog.CreateEventLogImpl(sourceData.LogName, sourceData.MachineName, sourceData.Source).CreateEventSource(sourceData);
		}

		/// <summary>Removes an event log from the local computer.</summary>
		/// <param name="logName">The name of the log to delete. Possible values include: Application, Security, System, and any custom event logs on the computer.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="logName" /> is an empty string ("") or <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the local computer.  
		/// -or-
		///  The log does not exist on the local computer.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.  
		///  -or-  
		///  The log cannot be opened. A Windows error code is not available.</exception>
		// Token: 0x0600129D RID: 4765 RVA: 0x00050082 File Offset: 0x0004E282
		public static void Delete(string logName)
		{
			EventLog.Delete(logName, ".");
		}

		/// <summary>Removes an event log from the specified computer.</summary>
		/// <param name="logName">The name of the log to delete. Possible values include: Application, Security, System, and any custom event logs on the specified computer.</param>
		/// <param name="machineName">The name of the computer to delete the log from, or "." for the local computer.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="logName" /> is an empty string ("") or <see langword="null" />.  
		/// -or-
		///  <paramref name="machineName" /> is not a valid computer name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the specified computer.  
		/// -or-
		///  The log does not exist on the specified computer.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log was not cleared successfully.  
		///  -or-  
		///  The log cannot be opened. A Windows error code is not available.</exception>
		// Token: 0x0600129E RID: 4766 RVA: 0x00050090 File Offset: 0x0004E290
		[MonoNotSupported("remote machine is not supported")]
		public static void Delete(string logName, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException("Invalid format for argument machineName.");
			}
			if (logName == null || logName.Length == 0)
			{
				throw new ArgumentException("Log to delete was not specified.");
			}
			EventLog.CreateEventLogImpl(logName, machineName, string.Empty).Delete(logName, machineName);
		}

		/// <summary>Removes the event source registration from the event log of the local computer.</summary>
		/// <param name="source">The name by which the application is registered in the event log system.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> parameter does not exist in the registry of the local computer.  
		/// -or-
		///  You do not have write access on the registry key for the event log.</exception>
		// Token: 0x0600129F RID: 4767 RVA: 0x000500E1 File Offset: 0x0004E2E1
		public static void DeleteEventSource(string source)
		{
			EventLog.DeleteEventSource(source, ".");
		}

		/// <summary>Removes the application's event source registration from the specified computer.</summary>
		/// <param name="source">The name by which the application is registered in the event log system.</param>
		/// <param name="machineName">The name of the computer to remove the registration from, or "." for the local computer.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is invalid.  
		/// -or-
		///  The <paramref name="source" /> parameter does not exist in the registry of the specified computer.  
		/// -or-
		///  You do not have write access on the registry key for the event log.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> cannot be deleted because in the registry, the parent registry key for <paramref name="source" /> does not contain a subkey with the same name.</exception>
		// Token: 0x060012A0 RID: 4768 RVA: 0x000500EE File Offset: 0x0004E2EE
		[MonoNotSupported("remote machine is not supported")]
		public static void DeleteEventSource(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", machineName));
			}
			EventLog.CreateEventLogImpl(string.Empty, machineName, source).DeleteEventSource(source, machineName);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.EventLog" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060012A1 RID: 4769 RVA: 0x00050129 File Offset: 0x0004E329
		protected override void Dispose(bool disposing)
		{
			if (this.Impl != null)
			{
				this.Impl.Dispose(disposing);
			}
		}

		/// <summary>Ends the initialization of an <see cref="T:System.Diagnostics.EventLog" /> used on a form or by another component. The initialization occurs at runtime.</summary>
		// Token: 0x060012A2 RID: 4770 RVA: 0x0005013F File Offset: 0x0004E33F
		public void EndInit()
		{
			this.Impl.EndInit();
		}

		/// <summary>Determines whether the log exists on the local computer.</summary>
		/// <param name="logName">The name of the log to search for. Possible values include: Application, Security, System, other application-specific logs (such as those associated with Active Directory), or any custom log on the computer.</param>
		/// <returns>
		///   <see langword="true" /> if the log exists on the local computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The logName is <see langword="null" /> or the value is empty.</exception>
		// Token: 0x060012A3 RID: 4771 RVA: 0x0005014C File Offset: 0x0004E34C
		public static bool Exists(string logName)
		{
			return EventLog.Exists(logName, ".");
		}

		/// <summary>Determines whether the log exists on the specified computer.</summary>
		/// <param name="logName">The log for which to search. Possible values include: Application, Security, System, other application-specific logs (such as those associated with Active Directory), or any custom log on the computer.</param>
		/// <param name="machineName">The name of the computer on which to search for the log, or "." for the local computer.</param>
		/// <returns>
		///   <see langword="true" /> if the log exists on the specified computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is an invalid format. Make sure you have used proper syntax for the computer on which you are searching.  
		///  -or-  
		///  The <paramref name="logName" /> is <see langword="null" /> or the value is empty.</exception>
		// Token: 0x060012A4 RID: 4772 RVA: 0x00050159 File Offset: 0x0004E359
		[MonoNotSupported("remote machine is not supported")]
		public static bool Exists(string logName, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException("Invalid format for argument machineName.");
			}
			return logName != null && logName.Length != 0 && EventLog.CreateEventLogImpl(logName, machineName, string.Empty).Exists(logName, machineName);
		}

		/// <summary>Searches for all event logs on the local computer and creates an array of <see cref="T:System.Diagnostics.EventLog" /> objects that contain the list.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.EventLog" /> that represents the logs on the local computer.</returns>
		/// <exception cref="T:System.SystemException">You do not have read access to the registry.  
		///  -or-  
		///  There is no event log service on the computer.</exception>
		// Token: 0x060012A5 RID: 4773 RVA: 0x00050196 File Offset: 0x0004E396
		public static EventLog[] GetEventLogs()
		{
			return EventLog.GetEventLogs(".");
		}

		/// <summary>Searches for all event logs on the given computer and creates an array of <see cref="T:System.Diagnostics.EventLog" /> objects that contain the list.</summary>
		/// <param name="machineName">The computer on which to search for event logs.</param>
		/// <returns>An array of type <see cref="T:System.Diagnostics.EventLog" /> that represents the logs on the given computer.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is an invalid computer name.</exception>
		/// <exception cref="T:System.InvalidOperationException">You do not have read access to the registry.  
		///  -or-  
		///  There is no event log service on the computer.</exception>
		// Token: 0x060012A6 RID: 4774 RVA: 0x000501A2 File Offset: 0x0004E3A2
		[MonoNotSupported("remote machine is not supported")]
		public static EventLog[] GetEventLogs(string machineName)
		{
			return EventLog.CreateEventLogImpl(new EventLog()).GetEventLogs(machineName);
		}

		/// <summary>Gets the name of the log to which the specified source is registered.</summary>
		/// <param name="source">The name of the event source.</param>
		/// <param name="machineName">The name of the computer on which to look, or "." for the local computer.</param>
		/// <returns>The name of the log associated with the specified source in the registry.</returns>
		// Token: 0x060012A7 RID: 4775 RVA: 0x000501B4 File Offset: 0x0004E3B4
		[MonoNotSupported("remote machine is not supported")]
		public static string LogNameFromSourceName(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'MachineName'.", machineName));
			}
			return EventLog.CreateEventLogImpl(string.Empty, machineName, source).LogNameFromSourceName(source, machineName);
		}

		/// <summary>Determines whether an event source is registered on the local computer.</summary>
		/// <param name="source">The name of the event source.</param>
		/// <returns>
		///   <see langword="true" /> if the event source is registered on the local computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="source" /> was not found, but some or all of the event logs could not be searched.</exception>
		// Token: 0x060012A8 RID: 4776 RVA: 0x000501EF File Offset: 0x0004E3EF
		public static bool SourceExists(string source)
		{
			return EventLog.SourceExists(source, ".");
		}

		/// <summary>Determines whether an event source is registered on a specified computer.</summary>
		/// <param name="source">The name of the event source.</param>
		/// <param name="machineName">The name the computer on which to look, or "." for the local computer.</param>
		/// <returns>
		///   <see langword="true" /> if the event source is registered on the given computer; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="machineName" /> is an invalid computer name.</exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="source" /> was not found, but some or all of the event logs could not be searched.</exception>
		// Token: 0x060012A9 RID: 4777 RVA: 0x000501FC File Offset: 0x0004E3FC
		[MonoNotSupported("remote machine is not supported")]
		public static bool SourceExists(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", machineName));
			}
			return EventLog.CreateEventLogImpl(string.Empty, machineName, source).SourceExists(source, machineName);
		}

		/// <summary>Writes an information type entry, with the given message text, to the event log.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012AA RID: 4778 RVA: 0x00050237 File Offset: 0x0004E437
		public void WriteEntry(string message)
		{
			this.WriteEntry(message, EventLogEntryType.Information);
		}

		/// <summary>Writes an error, warning, information, success audit, or failure audit entry with the given message text to the event log.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012AB RID: 4779 RVA: 0x00050241 File Offset: 0x0004E441
		public void WriteEntry(string message, EventLogEntryType type)
		{
			this.WriteEntry(message, type, 0);
		}

		/// <summary>Writes an entry with the given message text and application-defined event identifier to the event log.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012AC RID: 4780 RVA: 0x0005024C File Offset: 0x0004E44C
		public void WriteEntry(string message, EventLogEntryType type, int eventID)
		{
			this.WriteEntry(message, type, eventID, 0);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <param name="category">The application-specific subcategory associated with the message.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012AD RID: 4781 RVA: 0x00050258 File Offset: 0x0004E458
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category)
		{
			this.WriteEntry(message, type, eventID, category, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, and appends binary data to the message.</summary>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <param name="category">The application-specific subcategory associated with the message.</param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012AE RID: 4782 RVA: 0x00050266 File Offset: 0x0004E466
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			this.WriteEntry(new string[]
			{
				message
			}, type, (long)eventID, category, rawData);
		}

		/// <summary>Writes an information type entry with the given message text to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012AF RID: 4783 RVA: 0x0005027F File Offset: 0x0004E47F
		public static void WriteEntry(string source, string message)
		{
			EventLog.WriteEntry(source, message, EventLogEntryType.Information);
		}

		/// <summary>Writes an error, warning, information, success audit, or failure audit entry with the given message text to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012B0 RID: 4784 RVA: 0x00050289 File Offset: 0x0004E489
		public static void WriteEntry(string source, string message, EventLogEntryType type)
		{
			EventLog.WriteEntry(source, message, type, 0);
		}

		/// <summary>Writes an entry with the given message text and application-defined event identifier to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012B1 RID: 4785 RVA: 0x00050294 File Offset: 0x0004E494
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID)
		{
			EventLog.WriteEntry(source, message, type, eventID, 0);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, using the specified registered event source. The <paramref name="category" /> can be used by the Event Viewer to filter events in the log.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <param name="category">The application-specific subcategory associated with the message.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012B2 RID: 4786 RVA: 0x000502A0 File Offset: 0x0004E4A0
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category)
		{
			EventLog.WriteEntry(source, message, type, eventID, category, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log (using the specified registered event source) and appends binary data to the message.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer.</param>
		/// <param name="message">The string to write to the event log.</param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values.</param>
		/// <param name="eventID">The application-specific identifier for the event.</param>
		/// <param name="category">The application-specific subcategory associated with the message.</param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012B3 RID: 4787 RVA: 0x000502B0 File Offset: 0x0004E4B0
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			using (EventLog eventLog = new EventLog())
			{
				eventLog.Source = source;
				eventLog.WriteEntry(message, type, eventID, category, rawData);
			}
		}

		/// <summary>Writes a localized entry to the event log.</summary>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry.</param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  <paramref name="values" /> has more than 256 elements.  
		/// -or-
		///  One of the <paramref name="values" /> elements is longer than 32766 bytes.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012B4 RID: 4788 RVA: 0x000502F4 File Offset: 0x0004E4F4
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, params object[] values)
		{
			this.WriteEvent(instance, null, values);
		}

		/// <summary>Writes an event log entry with the given event data, message replacement strings, and associated binary data.</summary>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry.</param>
		/// <param name="data">An array of bytes that holds the binary data associated with the entry.</param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.  
		///  -or-  
		///  The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.  
		/// -or-
		///  The source is already registered for a different event log.  
		/// -or-
		///  <paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  <paramref name="values" /> has more than 256 elements.  
		/// -or-
		///  One of the <paramref name="values" /> elements is longer than 32766 bytes.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012B5 RID: 4789 RVA: 0x00050300 File Offset: 0x0004E500
		[ComVisible(false)]
		public void WriteEvent(EventInstance instance, byte[] data, params object[] values)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			string[] array;
			if (values != null)
			{
				array = new string[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] == null)
					{
						array[i] = string.Empty;
					}
					else
					{
						array[i] = values[i].ToString();
					}
				}
			}
			else
			{
				array = new string[0];
			}
			this.WriteEntry(array, instance.EntryType, instance.InstanceId, (short)instance.CategoryId, data);
		}

		/// <summary>Writes an event log entry with the given event data and message replacement strings, using the specified registered event source.</summary>
		/// <param name="source">The name of the event source registered for the application on the specified computer.</param>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry.</param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  <paramref name="values" /> has more than 256 elements.  
		/// -or-
		///  One of the <paramref name="values" /> elements is longer than 32766 bytes.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012B6 RID: 4790 RVA: 0x00050375 File Offset: 0x0004E575
		public static void WriteEvent(string source, EventInstance instance, params object[] values)
		{
			EventLog.WriteEvent(source, instance, null, values);
		}

		/// <summary>Writes an event log entry with the given event data, message replacement strings, and associated binary data, and using the specified registered event source.</summary>
		/// <param name="source">The name of the event source registered for the application on the specified computer.</param>
		/// <param name="instance">An <see cref="T:System.Diagnostics.EventInstance" /> instance that represents a localized event log entry.</param>
		/// <param name="data">An array of bytes that holds the binary data associated with the entry.</param>
		/// <param name="values">An array of strings to merge into the message text of the event log entry.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").  
		/// -or-
		///  The <paramref name="source" /> value is <see langword="null" />.  
		/// -or-
		///  <paramref name="instance.InstanceId" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-
		///  <paramref name="values" /> has more than 256 elements.  
		/// -or-
		///  One of the <paramref name="values" /> elements is longer than 32766 bytes.  
		/// -or-
		///  The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		// Token: 0x060012B7 RID: 4791 RVA: 0x00050380 File Offset: 0x0004E580
		public static void WriteEvent(string source, EventInstance instance, byte[] data, params object[] values)
		{
			using (EventLog eventLog = new EventLog())
			{
				eventLog.Source = source;
				eventLog.WriteEvent(instance, data, values);
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000503C0 File Offset: 0x0004E5C0
		internal void OnEntryWritten(EventLogEntry newEntry)
		{
			if (this.doRaiseEvents && this.EntryWritten != null)
			{
				this.EntryWritten(this, new EntryWrittenEventArgs(newEntry));
			}
		}

		/// <summary>Occurs when an entry is written to an event log on the local computer.</summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060012B9 RID: 4793 RVA: 0x000503E4 File Offset: 0x0004E5E4
		// (remove) Token: 0x060012BA RID: 4794 RVA: 0x0005041C File Offset: 0x0004E61C
		[MonitoringDescription("Raised for each EventLog entry written.")]
		public event EntryWrittenEventHandler EntryWritten
		{
			[CompilerGenerated]
			add
			{
				EntryWrittenEventHandler entryWrittenEventHandler = this.EntryWritten;
				EntryWrittenEventHandler entryWrittenEventHandler2;
				do
				{
					entryWrittenEventHandler2 = entryWrittenEventHandler;
					EntryWrittenEventHandler value2 = (EntryWrittenEventHandler)Delegate.Combine(entryWrittenEventHandler2, value);
					entryWrittenEventHandler = Interlocked.CompareExchange<EntryWrittenEventHandler>(ref this.EntryWritten, value2, entryWrittenEventHandler2);
				}
				while (entryWrittenEventHandler != entryWrittenEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EntryWrittenEventHandler entryWrittenEventHandler = this.EntryWritten;
				EntryWrittenEventHandler entryWrittenEventHandler2;
				do
				{
					entryWrittenEventHandler2 = entryWrittenEventHandler;
					EntryWrittenEventHandler value2 = (EntryWrittenEventHandler)Delegate.Remove(entryWrittenEventHandler2, value);
					entryWrittenEventHandler = Interlocked.CompareExchange<EntryWrittenEventHandler>(ref this.EntryWritten, value2, entryWrittenEventHandler2);
				}
				while (entryWrittenEventHandler != entryWrittenEventHandler2);
			}
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00050451 File Offset: 0x0004E651
		internal string GetLogName()
		{
			if (this.logName != null && this.logName.Length > 0)
			{
				return this.logName;
			}
			this.logName = EventLog.LogNameFromSourceName(this.source, this.machineName);
			return this.logName;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0005048D File Offset: 0x0004E68D
		private static EventLogImpl CreateEventLogImpl(string logName, string machineName, string source)
		{
			return EventLog.CreateEventLogImpl(new EventLog(logName, machineName, source));
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0005049C File Offset: 0x0004E69C
		private static EventLogImpl CreateEventLogImpl(EventLog eventLog)
		{
			string eventLogImplType = EventLog.EventLogImplType;
			if (eventLogImplType == "local")
			{
				return new LocalFileEventLog(eventLog);
			}
			if (eventLogImplType == "win32")
			{
				return new Win32EventLog(eventLog);
			}
			if (!(eventLogImplType == "null"))
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Eventlog implementation '{0}' is not supported.", EventLog.EventLogImplType));
			}
			return new NullEventLog(eventLog);
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00050506 File Offset: 0x0004E706
		private static bool Win32EventLogEnabled
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x00050518 File Offset: 0x0004E718
		private static string EventLogImplType
		{
			get
			{
				string text = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (text == null)
				{
					if (EventLog.Win32EventLogEnabled)
					{
						return "win32";
					}
					text = "null";
				}
				else if (EventLog.Win32EventLogEnabled && string.Compare(text, "win32", true) == 0)
				{
					text = "win32";
				}
				else if (string.Compare(text, "null", true) == 0)
				{
					text = "null";
				}
				else
				{
					if (string.Compare(text, 0, "local", 0, "local".Length, true) != 0)
					{
						throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Eventlog implementation '{0}' is not supported.", text));
					}
					text = "local";
				}
				return text;
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x000505B4 File Offset: 0x0004E7B4
		private void WriteEntry(string[] replacementStrings, EventLogEntryType type, long instanceID, short category, byte[] rawData)
		{
			if (this.Source.Length == 0)
			{
				throw new ArgumentException("Source property was not setbefore writing to the event log.");
			}
			if (!Enum.IsDefined(typeof(EventLogEntryType), type))
			{
				throw new InvalidEnumArgumentException("type", (int)type, typeof(EventLogEntryType));
			}
			this.ValidateEventID(instanceID);
			if (!EventLog.SourceExists(this.Source, this.MachineName))
			{
				if (this.Log == null || this.Log.Length == 0)
				{
					this.Log = "Application";
				}
				EventLog.CreateEventSource(this.Source, this.Log, this.MachineName);
			}
			else if (this.logName != null && this.logName.Length != 0)
			{
				string text = EventLog.LogNameFromSourceName(this.Source, this.MachineName);
				if (string.Compare(this.logName, text, true, CultureInfo.InvariantCulture) != 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered in log '{1}' (it is registered in log '{2}'). The Source and Log properties must be matched, or you may set Log to the empty string, and it will automatically be matched to the Source property.", this.Source, this.logName, text));
				}
			}
			if (rawData == null)
			{
				rawData = new byte[0];
			}
			this.Impl.WriteEntry(replacementStrings, type, (uint)instanceID, category, rawData);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000506D8 File Offset: 0x0004E8D8
		private void ValidateEventID(long instanceID)
		{
			int eventID = EventLog.GetEventID(instanceID);
			if (eventID < 0 || eventID > 65535)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid eventID value '{0}'. It must be in the range between '{1}' and '{2}'.", instanceID, 0, ushort.MaxValue));
			}
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00050724 File Offset: 0x0004E924
		internal static int GetEventID(long instanceID)
		{
			int num = (int)(((instanceID < 0L) ? (-instanceID) : instanceID) & 1073741823L);
			if (instanceID >= 0L)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x04000AAB RID: 2731
		private string source;

		// Token: 0x04000AAC RID: 2732
		private string logName;

		// Token: 0x04000AAD RID: 2733
		private string machineName;

		// Token: 0x04000AAE RID: 2734
		private bool doRaiseEvents;

		// Token: 0x04000AAF RID: 2735
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04000AB0 RID: 2736
		internal const string LOCAL_FILE_IMPL = "local";

		// Token: 0x04000AB1 RID: 2737
		private const string WIN32_IMPL = "win32";

		// Token: 0x04000AB2 RID: 2738
		private const string NULL_IMPL = "null";

		// Token: 0x04000AB3 RID: 2739
		internal const string EVENTLOG_TYPE_VAR = "MONO_EVENTLOG_TYPE";

		// Token: 0x04000AB4 RID: 2740
		private EventLogImpl Impl;

		// Token: 0x04000AB5 RID: 2741
		[CompilerGenerated]
		private EntryWrittenEventHandler EntryWritten;
	}
}
