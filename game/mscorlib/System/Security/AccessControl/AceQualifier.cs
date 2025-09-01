using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the function of an access control entry (ACE).</summary>
	// Token: 0x02000507 RID: 1287
	public enum AceQualifier
	{
		/// <summary>Allow access.</summary>
		// Token: 0x0400241D RID: 9245
		AccessAllowed,
		/// <summary>Deny access.</summary>
		// Token: 0x0400241E RID: 9246
		AccessDenied,
		/// <summary>Cause a system audit.</summary>
		// Token: 0x0400241F RID: 9247
		SystemAudit,
		/// <summary>Cause a system alarm.</summary>
		// Token: 0x04002420 RID: 9248
		SystemAlarm
	}
}
