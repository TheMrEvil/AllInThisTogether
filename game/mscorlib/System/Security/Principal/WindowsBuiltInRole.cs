using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Specifies common roles to be used with <see cref="M:System.Security.Principal.WindowsPrincipal.IsInRole(System.String)" />.</summary>
	// Token: 0x020004EB RID: 1259
	[ComVisible(true)]
	[Serializable]
	public enum WindowsBuiltInRole
	{
		/// <summary>Administrators have complete and unrestricted access to the computer or domain.</summary>
		// Token: 0x04002333 RID: 9011
		Administrator = 544,
		/// <summary>Users are prevented from making accidental or intentional system-wide changes. Thus, users can run certified applications, but not most legacy applications.</summary>
		// Token: 0x04002334 RID: 9012
		User,
		/// <summary>Guests are more restricted than users.</summary>
		// Token: 0x04002335 RID: 9013
		Guest,
		/// <summary>Power users possess most administrative permissions with some restrictions. Thus, power users can run legacy applications, in addition to certified applications.</summary>
		// Token: 0x04002336 RID: 9014
		PowerUser,
		/// <summary>Account operators manage the user accounts on a computer or domain.</summary>
		// Token: 0x04002337 RID: 9015
		AccountOperator,
		/// <summary>System operators manage a particular computer.</summary>
		// Token: 0x04002338 RID: 9016
		SystemOperator,
		/// <summary>Print operators can take control of a printer.</summary>
		// Token: 0x04002339 RID: 9017
		PrintOperator,
		/// <summary>Backup operators can override security restrictions for the sole purpose of backing up or restoring files.</summary>
		// Token: 0x0400233A RID: 9018
		BackupOperator,
		/// <summary>Replicators support file replication in a domain.</summary>
		// Token: 0x0400233B RID: 9019
		Replicator
	}
}
