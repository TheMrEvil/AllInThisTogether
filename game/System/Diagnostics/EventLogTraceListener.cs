using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a simple listener that directs tracing or debugging output to an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	// Token: 0x02000265 RID: 613
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class EventLogTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class without a trace listener.</summary>
		// Token: 0x0600132A RID: 4906 RVA: 0x00046FA4 File Offset: 0x000451A4
		public EventLogTraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified event log.</summary>
		/// <param name="eventLog">The event log to write to.</param>
		// Token: 0x0600132B RID: 4907 RVA: 0x00051167 File Offset: 0x0004F367
		public EventLogTraceListener(EventLog eventLog)
		{
			if (eventLog == null)
			{
				throw new ArgumentNullException("eventLog");
			}
			this.event_log = eventLog;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogTraceListener" /> class using the specified source.</summary>
		/// <param name="source">The name of an existing event log source.</param>
		// Token: 0x0600132C RID: 4908 RVA: 0x00051184 File Offset: 0x0004F384
		public EventLogTraceListener(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.event_log = new EventLog();
			this.event_log.Source = source;
		}

		/// <summary>Gets or sets the event log to write to.</summary>
		/// <returns>The event log to write to.</returns>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x000511B1 File Offset: 0x0004F3B1
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x000511B9 File Offset: 0x0004F3B9
		public EventLog EventLog
		{
			get
			{
				return this.event_log;
			}
			set
			{
				this.event_log = value;
			}
		}

		/// <summary>Gets or sets the name of this <see cref="T:System.Diagnostics.EventLogTraceListener" />.</summary>
		/// <returns>The name of this trace listener.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x000511C2 File Offset: 0x0004F3C2
		// (set) Token: 0x06001330 RID: 4912 RVA: 0x000511DE File Offset: 0x0004F3DE
		public override string Name
		{
			get
			{
				if (this.name == null)
				{
					return this.event_log.Source;
				}
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Closes the event log so that it no longer receives tracing or debugging output.</summary>
		// Token: 0x06001331 RID: 4913 RVA: 0x000511E7 File Offset: 0x0004F3E7
		public override void Close()
		{
			this.event_log.Close();
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000511F4 File Offset: 0x0004F3F4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.event_log.Dispose();
			}
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		// Token: 0x06001333 RID: 4915 RVA: 0x00051204 File Offset: 0x0004F404
		public override void Write(string message)
		{
			this.TraceData(new TraceEventCache(), this.event_log.Source, TraceEventType.Information, 0, message);
		}

		/// <summary>Writes a message to the event log for this instance.</summary>
		/// <param name="message">The message to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="message" /> exceeds 32,766 characters.</exception>
		// Token: 0x06001334 RID: 4916 RVA: 0x0005121F File Offset: 0x0004F41F
		public override void WriteLine(string message)
		{
			this.Write(message);
		}

		/// <summary>Writes trace information, a data object, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">A data object to write to the output file or stream.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.  
		/// -or-  
		/// The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x06001335 RID: 4917 RVA: 0x00051228 File Offset: 0x0004F428
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, object data)
		{
			EventLogEntryType type;
			if (severity - TraceEventType.Critical > 1)
			{
				if (severity != TraceEventType.Warning)
				{
					type = EventLogEntryType.Information;
				}
				else
				{
					type = EventLogEntryType.Warning;
				}
			}
			else
			{
				type = EventLogEntryType.Error;
			}
			this.event_log.WriteEntry((data != null) ? data.ToString() : string.Empty, type, id, 0);
		}

		/// <summary>Writes trace information, an array of data objects, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="data">An array of data objects.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.  
		/// -or-  
		/// The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x06001336 RID: 4918 RVA: 0x0005126C File Offset: 0x0004F46C
		[ComVisible(false)]
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType severity, int id, params object[] data)
		{
			string data2 = string.Empty;
			if (data != null)
			{
				string[] array = new string[data.Length];
				for (int i = 0; i < data.Length; i++)
				{
					array[i] = ((data[i] != null) ? data[i].ToString() : string.Empty);
				}
				data2 = string.Join(", ", array);
			}
			this.TraceData(eventCache, source, severity, id, data2);
		}

		/// <summary>Writes trace information, a message, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="message">The trace message.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.  
		/// -or-  
		/// The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x06001337 RID: 4919 RVA: 0x000512CD File Offset: 0x0004F4CD
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string message)
		{
			this.TraceData(eventCache, source, severity, id, message);
		}

		/// <summary>Writes trace information, a formatted array of objects, and event information to the event log.</summary>
		/// <param name="eventCache">An object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output; typically the name of the application that generated the trace event.</param>
		/// <param name="severity">One of the enumeration values that specifies the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event. The combination of <paramref name="source" /> and <paramref name="id" /> uniquely identifies an event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is not specified.  
		/// -or-  
		/// The log entry string exceeds 32,766 characters.</exception>
		// Token: 0x06001338 RID: 4920 RVA: 0x000512DC File Offset: 0x0004F4DC
		[ComVisible(false)]
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
		{
			this.TraceEvent(eventCache, source, severity, id, (format != null) ? string.Format(format, args) : null);
		}

		// Token: 0x04000ADC RID: 2780
		private EventLog event_log;

		// Token: 0x04000ADD RID: 2781
		private string name;
	}
}
