using System;

namespace System.Data.SqlClient
{
	/// <summary>This enumeration provides additional information about the different notifications that can be received by the dependency event handler.</summary>
	// Token: 0x02000213 RID: 531
	public enum SqlNotificationInfo
	{
		/// <summary>One or more tables were truncated.</summary>
		// Token: 0x040010AB RID: 4267
		Truncate,
		/// <summary>Data was changed by an INSERT statement.</summary>
		// Token: 0x040010AC RID: 4268
		Insert,
		/// <summary>Data was changed by an UPDATE statement.</summary>
		// Token: 0x040010AD RID: 4269
		Update,
		/// <summary>Data was changed by a DELETE statement.</summary>
		// Token: 0x040010AE RID: 4270
		Delete,
		/// <summary>An underlying object related to the query was dropped.</summary>
		// Token: 0x040010AF RID: 4271
		Drop,
		/// <summary>An underlying server object related to the query was modified.</summary>
		// Token: 0x040010B0 RID: 4272
		Alter,
		/// <summary>The server was restarted (notifications are sent during restart.).</summary>
		// Token: 0x040010B1 RID: 4273
		Restart,
		/// <summary>An internal server error occurred.</summary>
		// Token: 0x040010B2 RID: 4274
		Error,
		/// <summary>A SELECT statement that cannot be notified or was provided.</summary>
		// Token: 0x040010B3 RID: 4275
		Query,
		/// <summary>A statement was provided that cannot be notified (for example, an UPDATE statement).</summary>
		// Token: 0x040010B4 RID: 4276
		Invalid,
		/// <summary>The SET options were not set appropriately at subscription time.</summary>
		// Token: 0x040010B5 RID: 4277
		Options,
		/// <summary>The statement was executed under an isolation mode that was not valid (for example, Snapshot).</summary>
		// Token: 0x040010B6 RID: 4278
		Isolation,
		/// <summary>The <see langword="SqlDependency" /> object has expired.</summary>
		// Token: 0x040010B7 RID: 4279
		Expired,
		/// <summary>Fires as a result of server resource pressure.</summary>
		// Token: 0x040010B8 RID: 4280
		Resource,
		/// <summary>A previous statement has caused query notifications to fire under the current transaction.</summary>
		// Token: 0x040010B9 RID: 4281
		PreviousFire,
		/// <summary>The subscribing query causes the number of templates on one of the target tables to exceed the maximum allowable limit.</summary>
		// Token: 0x040010BA RID: 4282
		TemplateLimit,
		/// <summary>Used to distinguish the server-side cause for a query notification firing.</summary>
		// Token: 0x040010BB RID: 4283
		Merge,
		/// <summary>Used when the info option sent by the server was not recognized by the client.</summary>
		// Token: 0x040010BC RID: 4284
		Unknown = -1,
		/// <summary>The <see langword="SqlDependency" /> object already fired, and new commands cannot be added to it.</summary>
		// Token: 0x040010BD RID: 4285
		AlreadyChanged = -2
	}
}
