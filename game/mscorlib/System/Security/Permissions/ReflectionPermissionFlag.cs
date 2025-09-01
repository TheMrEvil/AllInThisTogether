using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted use of the <see cref="N:System.Reflection" /> and <see cref="N:System.Reflection.Emit" /> namespaces.</summary>
	// Token: 0x0200042E RID: 1070
	[Flags]
	public enum ReflectionPermissionFlag
	{
		/// <summary>
		///   <see langword="TypeInformation" /> , <see langword="MemberAccess" />, and <see langword="ReflectionEmit" /> are set. <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.AllFlags" /> does not include <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.RestrictedMemberAccess" />.</summary>
		// Token: 0x04001FEF RID: 8175
		[Obsolete("This permission has been deprecated. Use PermissionState.Unrestricted to get full access.")]
		AllFlags = 7,
		/// <summary>Invocation operations on all members are allowed, regardless of grant set. If this flag is not set, invocation operations are allowed only on visible members.</summary>
		// Token: 0x04001FF0 RID: 8176
		MemberAccess = 2,
		/// <summary>Enumeration of types and members is allowed. Invocation operations are allowed on visible types and members.</summary>
		// Token: 0x04001FF1 RID: 8177
		NoFlags = 0,
		/// <summary>Emitting debug symbols is allowed. Beginning with the .NET Framework 2.0 Service Pack 1, this flag is no longer required to emit code.</summary>
		// Token: 0x04001FF2 RID: 8178
		[Obsolete("This permission is no longer used by the CLR.")]
		ReflectionEmit = 4,
		/// <summary>Restricted member access is provided for partially trusted code. Partially trusted code can access nonpublic types and members, but only if the grant set of the partially trusted code includes all permissions in the grant set of the assembly that contains the nonpublic types and members being accessed. This flag is new in the .NET Framework 2.0 SP1.</summary>
		// Token: 0x04001FF3 RID: 8179
		RestrictedMemberAccess = 8,
		/// <summary>This flag is obsolete. No flags are necessary to enumerate types and members and to examine their metadata. Use <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.NoFlags" /> instead.</summary>
		// Token: 0x04001FF4 RID: 8180
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		TypeInformation = 1
	}
}
