using System;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents a query for events in an event log and the settings that define how the query is executed and on what computer the query is executed on.</summary>
	// Token: 0x020003A8 RID: 936
	public class EventLogQuery
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogQuery" /> class by specifying the target of the query. The target can be an active event log or a log file.</summary>
		/// <param name="path">The name of the event log to query, or the path to the event log file to query.</param>
		/// <param name="pathType">Specifies whether the string used in the path parameter specifies the name of an event log, or the path to an event log file.</param>
		// Token: 0x06001BC9 RID: 7113 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogQuery(string path, PathType pathType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogQuery" /> class by specifying the target of the query and the event query. The target can be an active event log or a log file.</summary>
		/// <param name="path">The name of the event log to query, or the path to the event log file to query.</param>
		/// <param name="pathType">Specifies whether the string used in the path parameter specifies the name of an event log, or the path to an event log file.</param>
		/// <param name="query">The event query used to retrieve events that match the query conditions.</param>
		// Token: 0x06001BCA RID: 7114 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogQuery(string path, PathType pathType, string query)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the Boolean value that determines whether to read events from the newest event in an event log to the oldest event in the log.</summary>
		/// <returns>Returns <see langword="true" /> if events are read from the newest event in the log to the oldest event, and returns <see langword="false" /> if events are read from the oldest event in the log to the newest event.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0005A7DC File Offset: 0x000589DC
		// (set) Token: 0x06001BCC RID: 7116 RVA: 0x0000235B File Offset: 0x0000055B
		public bool ReverseDirection
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the session that access the Event Log service on the local computer or a remote computer. This object can be set to access a remote event log by creating a <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReader" /> object or an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogWatcher" /> object with this <see cref="T:System.Diagnostics.Eventing.Reader.EventLogQuery" /> object.</summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogSession" /> object.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001BCE RID: 7118 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogSession Session
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether this query will continue to retrieve events when the query has an error.</summary>
		/// <returns>
		///     <see langword="true" /> indicates that the query will continue to retrieve events even if the query fails for some logs, and <see langword="false" /> indicates that this query will not continue to retrieve events when the query fails.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x0005A7F8 File Offset: 0x000589F8
		// (set) Token: 0x06001BD0 RID: 7120 RVA: 0x0000235B File Offset: 0x0000055B
		public bool TolerateQueryErrors
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
