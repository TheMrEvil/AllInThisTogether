using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides the <see langword="abstract" /> base class for the listeners who monitor trace and debug output.</summary>
	// Token: 0x02000231 RID: 561
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public abstract class TraceListener : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		// Token: 0x06001095 RID: 4245 RVA: 0x00048A78 File Offset: 0x00046C78
		protected TraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceListener" /> class using the specified name as the listener.</summary>
		/// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener" />.</param>
		// Token: 0x06001096 RID: 4246 RVA: 0x00048A8E File Offset: 0x00046C8E
		protected TraceListener(string name)
		{
			this.listenerName = name;
		}

		/// <summary>Gets the custom trace listener attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the custom attributes for the trace listener.</returns>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00048AAB File Offset: 0x00046CAB
		public StringDictionary Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets or sets a name for this <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <returns>A name for this <see cref="T:System.Diagnostics.TraceListener" />. The default is an empty string ("").</returns>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x00048AC6 File Offset: 0x00046CC6
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x00048ADC File Offset: 0x00046CDC
		public virtual string Name
		{
			get
			{
				if (this.listenerName != null)
				{
					return this.listenerName;
				}
				return "";
			}
			set
			{
				this.listenerName = value;
			}
		}

		/// <summary>Gets a value indicating whether the trace listener is thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> if the trace listener is thread safe; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool IsThreadSafe
		{
			get
			{
				return false;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		// Token: 0x0600109B RID: 4251 RVA: 0x00048AE5 File Offset: 0x00046CE5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.TraceListener" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600109C RID: 4252 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, closes the output stream so it no longer receives tracing or debugging output.</summary>
		// Token: 0x0600109D RID: 4253 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Close()
		{
		}

		/// <summary>When overridden in a derived class, flushes the output buffer.</summary>
		// Token: 0x0600109E RID: 4254 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Flush()
		{
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is zero.</returns>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00048AF4 File Offset: 0x00046CF4
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x00048AFC File Offset: 0x00046CFC
		public int IndentLevel
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = ((value < 0) ? 0 : value);
			}
		}

		/// <summary>Gets or sets the number of spaces in an indent.</summary>
		/// <returns>The number of spaces in an indent. The default is four spaces.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is less than zero.</exception>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x00048B0C File Offset: 0x00046D0C
		// (set) Token: 0x060010A2 RID: 4258 RVA: 0x00048B14 File Offset: 0x00046D14
		public int IndentSize
		{
			get
			{
				return this.indentSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("IndentSize", value, SR.GetString("The IndentSize property must be non-negative."));
				}
				this.indentSize = value;
			}
		}

		/// <summary>Gets or sets the trace filter for the trace listener.</summary>
		/// <returns>An object derived from the <see cref="T:System.Diagnostics.TraceFilter" /> base class.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00048B3C File Offset: 0x00046D3C
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x00048B44 File Offset: 0x00046D44
		[ComVisible(false)]
		public TraceFilter Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to indent the output.</summary>
		/// <returns>
		///   <see langword="true" /> if the output should be indented; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00048B4D File Offset: 0x00046D4D
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x00048B55 File Offset: 0x00046D55
		protected bool NeedIndent
		{
			get
			{
				return this.needIndent;
			}
			set
			{
				this.needIndent = value;
			}
		}

		/// <summary>Gets or sets the trace output options.</summary>
		/// <returns>A bitwise combination of the enumeration values. The default is <see cref="F:System.Diagnostics.TraceOptions.None" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is invalid.</exception>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00048B5E File Offset: 0x00046D5E
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00048B66 File Offset: 0x00046D66
		[ComVisible(false)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return this.traceOptions;
			}
			set
			{
				if (value >> 6 != TraceOptions.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.traceOptions = value;
			}
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00048B7F File Offset: 0x00046D7F
		internal void SetAttributes(Hashtable attribs)
		{
			TraceUtils.VerifyAttributes(attribs, this.GetSupportedAttributes(), this);
			this.attributes = new StringDictionary();
			this.attributes.ReplaceHashtable(attribs);
		}

		/// <summary>Emits an error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit.</param>
		// Token: 0x060010AA RID: 4266 RVA: 0x00048BA5 File Offset: 0x00046DA5
		public virtual void Fail(string message)
		{
			this.Fail(message, null);
		}

		/// <summary>Emits an error message and a detailed error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit.</param>
		/// <param name="detailMessage">A detailed message to emit.</param>
		// Token: 0x060010AB RID: 4267 RVA: 0x00048BB0 File Offset: 0x00046DB0
		public virtual void Fail(string message, string detailMessage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(SR.GetString("Fail:"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			if (detailMessage != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(detailMessage);
			}
			this.WriteLine(stringBuilder.ToString());
		}

		/// <summary>Gets the custom attributes supported by the trace listener.</summary>
		/// <returns>A string array naming the custom attributes supported by the trace listener, or <see langword="null" /> if there are no custom attributes.</returns>
		// Token: 0x060010AC RID: 4268 RVA: 0x00002F6A File Offset: 0x0000116A
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, writes the specified message to the listener you create in the derived class.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x060010AD RID: 4269
		public abstract void Write(string message);

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write.</param>
		// Token: 0x060010AE RID: 4270 RVA: 0x00048C0B File Offset: 0x00046E0B
		public virtual void Write(object o)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, null, null, o))
			{
				return;
			}
			if (o == null)
			{
				return;
			}
			this.Write(o.ToString());
		}

		/// <summary>Writes a category name and a message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x060010AF RID: 4271 RVA: 0x00048C40 File Offset: 0x00046E40
		public virtual void Write(string message, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, message))
			{
				return;
			}
			if (category == null)
			{
				this.Write(message);
				return;
			}
			this.Write(category + ": " + ((message == null) ? string.Empty : message));
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x060010B0 RID: 4272 RVA: 0x00048C94 File Offset: 0x00046E94
		public virtual void Write(object o, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, category, null, o))
			{
				return;
			}
			if (category == null)
			{
				this.Write(o);
				return;
			}
			this.Write((o == null) ? "" : o.ToString(), category);
		}

		/// <summary>Writes the indent to the listener you create when you implement this class, and resets the <see cref="P:System.Diagnostics.TraceListener.NeedIndent" /> property to <see langword="false" />.</summary>
		// Token: 0x060010B1 RID: 4273 RVA: 0x00048CE8 File Offset: 0x00046EE8
		protected virtual void WriteIndent()
		{
			this.NeedIndent = false;
			for (int i = 0; i < this.indentLevel; i++)
			{
				if (this.indentSize == 4)
				{
					this.Write("    ");
				}
				else
				{
					for (int j = 0; j < this.indentSize; j++)
					{
						this.Write(" ");
					}
				}
			}
		}

		/// <summary>When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x060010B2 RID: 4274
		public abstract void WriteLine(string message);

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write.</param>
		// Token: 0x060010B3 RID: 4275 RVA: 0x00048D3F File Offset: 0x00046F3F
		public virtual void WriteLine(object o)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, null, null, o))
			{
				return;
			}
			this.WriteLine((o == null) ? "" : o.ToString());
		}

		/// <summary>Writes a category name and a message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x060010B4 RID: 4276 RVA: 0x00048D7C File Offset: 0x00046F7C
		public virtual void WriteLine(string message, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, message))
			{
				return;
			}
			if (category == null)
			{
				this.WriteLine(message);
				return;
			}
			this.WriteLine(category + ": " + ((message == null) ? string.Empty : message));
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class, followed by a line terminator.</summary>
		/// <param name="o">An <see cref="T:System.Object" /> whose fully qualified class name you want to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x060010B5 RID: 4277 RVA: 0x00048DD0 File Offset: 0x00046FD0
		public virtual void WriteLine(object o, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, category, null, o))
			{
				return;
			}
			this.WriteLine((o == null) ? "" : o.ToString(), category);
		}

		/// <summary>Writes trace information, a data object and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The trace data to emit.</param>
		// Token: 0x060010B6 RID: 4278 RVA: 0x00048E0C File Offset: 0x0004700C
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			string message = string.Empty;
			if (data != null)
			{
				message = data.ToString();
			}
			this.WriteLine(message);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, an array of data objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of objects to emit as data.</param>
		// Token: 0x060010B7 RID: 4279 RVA: 0x00048E64 File Offset: 0x00047064
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			StringBuilder stringBuilder = new StringBuilder();
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					if (data[i] != null)
					{
						stringBuilder.Append(data[i].ToString());
					}
				}
			}
			this.WriteLine(stringBuilder.ToString());
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		// Token: 0x060010B8 RID: 4280 RVA: 0x00048EEC File Offset: 0x000470EC
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			this.TraceEvent(eventCache, source, eventType, id, string.Empty);
		}

		/// <summary>Writes trace information, a message, and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x060010B9 RID: 4281 RVA: 0x00048EFE File Offset: 0x000470FE
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.WriteLine(message);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a formatted array of objects and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		// Token: 0x060010BA RID: 4282 RVA: 0x00048F38 File Offset: 0x00047138
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, format, args))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			if (args != null)
			{
				this.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
			}
			else
			{
				this.WriteLine(format);
			}
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a message, a related activity identity and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="relatedActivityId">A <see cref="T:System.Guid" /> object identifying a related activity.</param>
		// Token: 0x060010BB RID: 4283 RVA: 0x00048F97 File Offset: 0x00047197
		[ComVisible(false)]
		public virtual void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			this.TraceEvent(eventCache, source, TraceEventType.Transfer, id, message + ", relatedActivityId=" + relatedActivityId.ToString());
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00048FC0 File Offset: 0x000471C0
		private void WriteHeader(string source, TraceEventType eventType, int id)
		{
			this.Write(string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2} : ", source, eventType.ToString(), id.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00048FF4 File Offset: 0x000471F4
		private void WriteFooter(TraceEventCache eventCache)
		{
			if (eventCache == null)
			{
				return;
			}
			this.indentLevel++;
			if (this.IsEnabled(TraceOptions.ProcessId))
			{
				this.WriteLine("ProcessId=" + eventCache.ProcessId.ToString());
			}
			if (this.IsEnabled(TraceOptions.LogicalOperationStack))
			{
				this.Write("LogicalOperationStack=");
				Stack logicalOperationStack = eventCache.LogicalOperationStack;
				bool flag = true;
				foreach (object obj in logicalOperationStack)
				{
					if (!flag)
					{
						this.Write(", ");
					}
					else
					{
						flag = false;
					}
					this.Write(obj.ToString());
				}
				this.WriteLine(string.Empty);
			}
			if (this.IsEnabled(TraceOptions.ThreadId))
			{
				this.WriteLine("ThreadId=" + eventCache.ThreadId);
			}
			if (this.IsEnabled(TraceOptions.DateTime))
			{
				this.WriteLine("DateTime=" + eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			if (this.IsEnabled(TraceOptions.Timestamp))
			{
				this.WriteLine("Timestamp=" + eventCache.Timestamp.ToString());
			}
			if (this.IsEnabled(TraceOptions.Callstack))
			{
				this.WriteLine("Callstack=" + eventCache.Callstack);
			}
			this.indentLevel--;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00049164 File Offset: 0x00047364
		internal bool IsEnabled(TraceOptions opts)
		{
			return (opts & this.TraceOutputOptions) > TraceOptions.None;
		}

		// Token: 0x040009F3 RID: 2547
		private int indentLevel;

		// Token: 0x040009F4 RID: 2548
		private int indentSize = 4;

		// Token: 0x040009F5 RID: 2549
		private TraceOptions traceOptions;

		// Token: 0x040009F6 RID: 2550
		private bool needIndent = true;

		// Token: 0x040009F7 RID: 2551
		private string listenerName;

		// Token: 0x040009F8 RID: 2552
		private TraceFilter filter;

		// Token: 0x040009F9 RID: 2553
		private StringDictionary attributes;

		// Token: 0x040009FA RID: 2554
		internal string initializeData;
	}
}
