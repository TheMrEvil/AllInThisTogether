using System;

namespace System.Security.Principal
{
	/// <summary>Defines the privileges of the user account associated with the access token.</summary>
	// Token: 0x020004DD RID: 1245
	[Flags]
	public enum TokenAccessLevels
	{
		/// <summary>The user can attach a primary token to a process.</summary>
		// Token: 0x040022A6 RID: 8870
		AssignPrimary = 1,
		/// <summary>The user can duplicate the token.</summary>
		// Token: 0x040022A7 RID: 8871
		Duplicate = 2,
		/// <summary>The user can impersonate a client.</summary>
		// Token: 0x040022A8 RID: 8872
		Impersonate = 4,
		/// <summary>The user can query the token.</summary>
		// Token: 0x040022A9 RID: 8873
		Query = 8,
		/// <summary>The user can query the source of the token.</summary>
		// Token: 0x040022AA RID: 8874
		QuerySource = 16,
		/// <summary>The user can enable or disable privileges in the token.</summary>
		// Token: 0x040022AB RID: 8875
		AdjustPrivileges = 32,
		/// <summary>The user can change the attributes of the groups in the token.</summary>
		// Token: 0x040022AC RID: 8876
		AdjustGroups = 64,
		/// <summary>The user can change the default owner, primary group, or discretionary access control list (DACL) of the token.</summary>
		// Token: 0x040022AD RID: 8877
		AdjustDefault = 128,
		/// <summary>The user can adjust the session identifier of the token.</summary>
		// Token: 0x040022AE RID: 8878
		AdjustSessionId = 256,
		/// <summary>The user has standard read rights and the <see cref="F:System.Security.Principal.TokenAccessLevels.Query" /> privilege for the token.</summary>
		// Token: 0x040022AF RID: 8879
		Read = 131080,
		/// <summary>The user has standard write rights and the <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustPrivileges" />, <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustGroups" /> and <see cref="F:System.Security.Principal.TokenAccessLevels.AdjustDefault" /> privileges for the token.</summary>
		// Token: 0x040022B0 RID: 8880
		Write = 131296,
		/// <summary>The user has all possible access to the token.</summary>
		// Token: 0x040022B1 RID: 8881
		AllAccess = 983551,
		/// <summary>The maximum value that can be assigned for the <see cref="T:System.Security.Principal.TokenAccessLevels" /> enumeration.</summary>
		// Token: 0x040022B2 RID: 8882
		MaximumAllowed = 33554432
	}
}
