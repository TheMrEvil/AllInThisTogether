using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Enables you to read events from an event log based on an event query. The events that are read by this object are returned as <see cref="T:System.Diagnostics.Eventing.Reader.EventRecord" /> objects.</summary>
	// Token: 0x020003A9 RID: 937
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogReader : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReader" /> class by specifying an event query.</summary>
		/// <param name="eventQuery">The event query used to retrieve events.</param>
		// Token: 0x06001BD1 RID: 7121 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogReader(EventLogQuery eventQuery)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReader" /> class by specifying an event query and a bookmark that is used as starting position for the query.</summary>
		/// <param name="eventQuery">The event query used to retrieve events.</param>
		/// <param name="bookmark">The bookmark (placeholder) used as a starting position in the event log or stream of events. Only events logged after the bookmark event will be returned by the query.</param>
		// Token: 0x06001BD2 RID: 7122 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public EventLogReader(EventLogQuery eventQuery, EventBookmark bookmark)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReader" /> class by specifying an active event log to retrieve events from.</summary>
		/// <param name="path">The name of the event log to retrieve events from.</param>
		// Token: 0x06001BD3 RID: 7123 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogReader(string path)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReader" /> class by specifying the name of an event log to retrieve events from or the path to a log file to retrieve events from.</summary>
		/// <param name="path">The name of the event log to retrieve events from, or the path to the event log file to retrieve events from.</param>
		/// <param name="pathType">Specifies whether the string used in the path parameter specifies the name of an event log, or the path to an event log file.</param>
		// Token: 0x06001BD4 RID: 7124 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogReader(string path, PathType pathType)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the number of events retrieved from the stream of events on every read operation.</summary>
		/// <returns>Returns an integer value.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0005A814 File Offset: 0x00058A14
		// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x0000235B File Offset: 0x0000055B
		public int BatchSize
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the status of each event log or log file associated with the event query in this object.</summary>
		/// <returns>Returns a list of <see cref="T:System.Diagnostics.Eventing.Reader.EventLogStatus" /> objects that each contain status information about an event log associated with the event query in this object.</returns>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0005A6D7 File Offset: 0x000588D7
		public IList<EventLogStatus> LogStatus
		{
			[SecurityCritical]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Cancels the current query operation.</summary>
		// Token: 0x06001BD8 RID: 7128 RVA: 0x0000235B File Offset: 0x0000055B
		public void CancelReading()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases all the resources used by this object.</summary>
		// Token: 0x06001BD9 RID: 7129 RVA: 0x0000235B File Offset: 0x0000055B
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001BDA RID: 7130 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Reads the next event that is returned from the event query in this object.</summary>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventRecord" /> object.</returns>
		// Token: 0x06001BDB RID: 7131 RVA: 0x0005A05A File Offset: 0x0005825A
		public EventRecord ReadEvent()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Reads the next event that is returned from the event query in this object.</summary>
		/// <param name="timeout">The maximum time to allow the read operation to run before canceling the operation.</param>
		/// <returns>Returns an <see cref="T:System.Diagnostics.Eventing.Reader.EventRecord" /> object.</returns>
		// Token: 0x06001BDC RID: 7132 RVA: 0x0005A05A File Offset: 0x0005825A
		[SecurityCritical]
		public EventRecord ReadEvent(TimeSpan timeout)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Changes the position in the event stream where the next event that is read will come from by specifying a bookmark event. No events logged before the bookmark event will be retrieved.</summary>
		/// <param name="bookmark">The bookmark (placeholder) used as a starting position in the event log or stream of events. Only events that have been logged after the bookmark event will be returned by the query.</param>
		// Token: 0x06001BDD RID: 7133 RVA: 0x0000235B File Offset: 0x0000055B
		public void Seek(EventBookmark bookmark)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Changes the position in the event stream where the next event that is read will come from by specifying a bookmark event and an offset number of events from the bookmark. No events logged before the bookmark plus the offset will be retrieved.</summary>
		/// <param name="bookmark">The bookmark (placeholder) used as a starting position in the event log or stream of events. Only events that have been logged after the bookmark event will be returned by the query.</param>
		/// <param name="offset">The offset number of events to change the position of the bookmark.</param>
		// Token: 0x06001BDE RID: 7134 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public void Seek(EventBookmark bookmark, long offset)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Changes the position in the event stream where the next event that is read will come from by specifying a starting position and an offset from the starting position. No events logged before the starting position plus the offset will be retrieved.</summary>
		/// <param name="origin">A value from the <see cref="T:System.IO.SeekOrigin" /> enumeration defines where in the stream of events to start querying for events.</param>
		/// <param name="offset">The offset number of events to add to the origin.</param>
		// Token: 0x06001BDF RID: 7135 RVA: 0x0000235B File Offset: 0x0000055B
		[SecurityCritical]
		public void Seek(SeekOrigin origin, long offset)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
