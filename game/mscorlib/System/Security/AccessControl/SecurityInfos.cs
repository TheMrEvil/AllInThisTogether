using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the section of a security descriptor to be queried or set.</summary>
	// Token: 0x0200054A RID: 1354
	[Flags]
	public enum SecurityInfos
	{
		/// <summary>Specifies the owner identifier.</summary>
		// Token: 0x040024FF RID: 9471
		Owner = 1,
		/// <summary>Specifies the primary group identifier.</summary>
		// Token: 0x04002500 RID: 9472
		Group = 2,
		/// <summary>Specifies the discretionary access control list (DACL).</summary>
		// Token: 0x04002501 RID: 9473
		DiscretionaryAcl = 4,
		/// <summary>Specifies the system access control list (SACL).</summary>
		// Token: 0x04002502 RID: 9474
		SystemAcl = 8
	}
}
