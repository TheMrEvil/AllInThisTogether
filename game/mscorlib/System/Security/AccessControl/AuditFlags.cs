using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the conditions for auditing attempts to access a securable object.</summary>
	// Token: 0x02000509 RID: 1289
	[Flags]
	public enum AuditFlags
	{
		/// <summary>No access attempts are to be audited.</summary>
		// Token: 0x04002435 RID: 9269
		None = 0,
		/// <summary>Successful access attempts are to be audited.</summary>
		// Token: 0x04002436 RID: 9270
		Success = 1,
		/// <summary>Failed access attempts are to be audited.</summary>
		// Token: 0x04002437 RID: 9271
		Failure = 2
	}
}
