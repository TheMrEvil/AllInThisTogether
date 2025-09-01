using System;

namespace System.Data.SqlClient
{
	/// <summary>Indicates the source of the notification received by the dependency event handler.</summary>
	// Token: 0x02000214 RID: 532
	public enum SqlNotificationSource
	{
		/// <summary>Data has changed; for example, an insert, update, delete, or truncate operation occurred.</summary>
		// Token: 0x040010BF RID: 4287
		Data,
		/// <summary>The subscription time-out expired.</summary>
		// Token: 0x040010C0 RID: 4288
		Timeout,
		/// <summary>A database object changed; for example, an underlying object related to the query was dropped or modified.</summary>
		// Token: 0x040010C1 RID: 4289
		Object,
		/// <summary>The database state changed; for example, the database related to the query was dropped or detached.</summary>
		// Token: 0x040010C2 RID: 4290
		Database,
		/// <summary>A system-related event occurred. For example, there was an internal error, the server was restarted, or resource pressure caused the invalidation.</summary>
		// Token: 0x040010C3 RID: 4291
		System,
		/// <summary>The Transact-SQL statement is not valid for notifications; for example, a SELECT statement that could not be notified or a non-SELECT statement was executed.</summary>
		// Token: 0x040010C4 RID: 4292
		Statement,
		/// <summary>The run-time environment was not compatible with notifications; for example, the isolation level was set to snapshot, or one or more SET options are not compatible.</summary>
		// Token: 0x040010C5 RID: 4293
		Environment,
		/// <summary>A run-time error occurred during execution.</summary>
		// Token: 0x040010C6 RID: 4294
		Execution,
		/// <summary>Internal only; not intended to be used in your code.</summary>
		// Token: 0x040010C7 RID: 4295
		Owner,
		/// <summary>Used when the source option sent by the server was not recognized by the client.</summary>
		// Token: 0x040010C8 RID: 4296
		Unknown = -1,
		/// <summary>A client-initiated notification occurred, such as a client-side time-out or as a result of attempting to add a command to a dependency that has already fired.</summary>
		// Token: 0x040010C9 RID: 4297
		Client = -2
	}
}
