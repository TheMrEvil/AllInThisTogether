using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event.</summary>
	// Token: 0x02000256 RID: 598
	public class EntryWrittenEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class.</summary>
		// Token: 0x06001271 RID: 4721 RVA: 0x0004FBE4 File Offset: 0x0004DDE4
		public EntryWrittenEventArgs() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> class with the specified event log entry.</summary>
		/// <param name="entry">An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written.</param>
		// Token: 0x06001272 RID: 4722 RVA: 0x0004FBED File Offset: 0x0004DDED
		public EntryWrittenEventArgs(EventLogEntry entry)
		{
			this.entry = entry;
		}

		/// <summary>Gets the event log entry that was written to the log.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.EventLogEntry" /> that represents the entry that was written to the event log.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0004FBFC File Offset: 0x0004DDFC
		public EventLogEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x04000AA7 RID: 2727
		private EventLogEntry entry;
	}
}
