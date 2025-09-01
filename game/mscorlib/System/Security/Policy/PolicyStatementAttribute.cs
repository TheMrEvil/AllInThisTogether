using System;

namespace System.Security.Policy
{
	/// <summary>Defines special attribute flags for security policy on code groups.</summary>
	// Token: 0x020003FB RID: 1019
	[Flags]
	public enum PolicyStatementAttribute
	{
		/// <summary>All attribute flags are set.</summary>
		// Token: 0x04001F4D RID: 8013
		All = 3,
		/// <summary>The exclusive code group flag. When a code group has this flag set, only the permissions associated with that code group are granted to code belonging to the code group. At most, one code group matching a given piece of code can be set as exclusive.</summary>
		// Token: 0x04001F4E RID: 8014
		Exclusive = 1,
		/// <summary>The flag representing a policy statement that causes lower policy levels to not be evaluated as part of the resolve operation, effectively allowing the policy level to override lower levels.</summary>
		// Token: 0x04001F4F RID: 8015
		LevelFinal = 2,
		/// <summary>No flags are set.</summary>
		// Token: 0x04001F50 RID: 8016
		Nothing = 0
	}
}
