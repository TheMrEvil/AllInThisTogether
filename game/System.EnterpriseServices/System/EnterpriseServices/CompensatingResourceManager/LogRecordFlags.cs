using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Describes the origin of a Compensating Resource Manager (CRM) log record.</summary>
	// Token: 0x02000077 RID: 119
	[Flags]
	[Serializable]
	public enum LogRecordFlags
	{
		/// <summary>Indicates the delivered record should be forgotten.</summary>
		// Token: 0x040000C5 RID: 197
		ForgetTarget = 1,
		/// <summary>Log record was written during prepare.</summary>
		// Token: 0x040000C6 RID: 198
		WrittenDuringPrepare = 2,
		/// <summary>Log record was written during commit.</summary>
		// Token: 0x040000C7 RID: 199
		WrittenDuringCommit = 4,
		/// <summary>Log record was written during abort.</summary>
		// Token: 0x040000C8 RID: 200
		WrittenDuringAbort = 8,
		/// <summary>Log record was written during recovery.</summary>
		// Token: 0x040000C9 RID: 201
		WrittenDurringRecovery = 16,
		/// <summary>Log record was written during replay.</summary>
		// Token: 0x040000CA RID: 202
		WrittenDuringReplay = 32,
		/// <summary>Log record was written when replay was in progress.</summary>
		// Token: 0x040000CB RID: 203
		ReplayInProgress = 64
	}
}
