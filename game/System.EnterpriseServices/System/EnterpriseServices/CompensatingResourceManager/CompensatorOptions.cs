using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Specifies flags that control which phases of transaction completion should be received by the Compensating Resource Manager (CRM) Compensator, and whether recovery should fail if questionable transactions remain after recovery has been attempted.</summary>
	// Token: 0x02000074 RID: 116
	[Flags]
	[Serializable]
	public enum CompensatorOptions
	{
		/// <summary>Represents the prepare phase.</summary>
		// Token: 0x040000B9 RID: 185
		PreparePhase = 1,
		/// <summary>Represents the commit phase.</summary>
		// Token: 0x040000BA RID: 186
		CommitPhase = 2,
		/// <summary>Represents the abort phase.</summary>
		// Token: 0x040000BB RID: 187
		AbortPhase = 4,
		/// <summary>Represents all phases.</summary>
		// Token: 0x040000BC RID: 188
		AllPhases = 7,
		/// <summary>Fails if in-doubt transactions remain after recovery has been attempted.</summary>
		// Token: 0x040000BD RID: 189
		FailIfInDoubtsRemain = 16
	}
}
